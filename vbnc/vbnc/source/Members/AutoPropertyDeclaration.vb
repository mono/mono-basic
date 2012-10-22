' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2010 Rolf Bjarne Kvinge, RKvinge@novell.com
' 
' This library is free software; you can redistribute it and/or
' modify it under the terms of the GNU Lesser General Public
' License as published by the Free Software Foundation; either
' version 2.1 of the License, or (at your option) any later version.
' 
' This library is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
' Lesser General Public License for more details.
' 
' You should have received a copy of the GNU Lesser General Public
' License along with this library; if not, write to the Free Software
' Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
' 

''' <summary>
''' AutoPropertyMemberDeclaration  ::=
''' [  Attributes  ]  [  AutoPropertyModifier+  ]  Property  Identifier
'''	[  OpenParenthesis  [  ParameterList  ]  CloseParenthesis  ]
'''	[  As  [  Attributes  ]  TypeName  ]  [  Equals  Expression  ]  [  ImplementsClause  ]  
''' </summary>
''' <remarks></remarks>
Public Class AutoPropertyDeclaration
    Inherits PropertyDeclaration

    ''' <summary>
    ''' Backing field for this property.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_BackingField As TypeVariableDeclaration

    ''' <summary>
    ''' If this property has an initialiser, the statement which will be executed in the body of 
    ''' the containing type's constructor.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_ConstructorAssignStmt As AssignmentStatement

    ''' <summary>
    ''' Constructor to pass in the declaring type of this property declaration.
    ''' </summary>
    ''' <param name="Parent">Parent type declaration containing this property.</param>
    ''' <remarks></remarks>
    Public Sub New(Parent As TypeDeclaration)
        MyBase.New(Parent)
    End Sub

    ''' <summary>
    ''' Instantiates the get/set methods and adds a new backing field to the declaring type declaration then
    ''' punts to the base class's implementation.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function CreateDefinition() As Boolean

        Dim result As Boolean = InstantiateGetSetMethodBodies()
        result = MyBase.CreateDefinition() AndAlso result

        'The backing field for this property is inserted too late for the parent type decl's CreateDefinition
        'to pick it up so handle it ourselves
        If m_BackingField IsNot Nothing Then m_BackingField.CreateDefinition()

        Return result

    End Function

    ''' <summary>
    ''' Punts to the base class for resolving everything besides the property initialiser if one is present.
    ''' </summary>
    ''' <param name="Info"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ResolveCode(Info As ResolveInfo) As Boolean

        Dim result As Boolean = MyBase.ResolveCode(Info)
        result = m_BackingField.ResolveCode(Info) AndAlso result

        If m_ConstructorAssignStmt IsNot Nothing Then
            result = m_ConstructorAssignStmt.ResolveStatement(Info) AndAlso result
        End If

        Return result

    End Function

    ''' <summary>
    ''' If this property has an initialiser, generates code to assign the initialisation expression to the
    ''' property to set the initial value.
    ''' </summary>
    ''' <param name="Info">MSIL emit params.</param>
    ''' <remarks></remarks>
    Public Sub EmitPropertyInitialiser(Info As EmitInfo)
        m_ConstructorAssignStmt.GenerateCode(Info)
    End Sub

    ''' <summary>
    ''' Creates the method bodies for the get/set methods of the property and adds a new backing field
    ''' to the containing type.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BuildGetSetMethodBodies()

        'Add a backing field with the name _<property name> to the containing type declaration 
        Dim BackingFieldIdentifier As New Identifier(String.Format("_{0}", Signature.Name))
        m_BackingField = New TypeVariableDeclaration(Parent, New Modifiers(ModifierMasks.Private), BackingFieldIdentifier,
                                                     False, Signature.TypeName, Nothing, Nothing)

        'Add the CompilerGeneratedAttribute to the backing field 
        m_BackingField.CustomAttributes = New Attributes(m_BackingField)
        m_BackingField.CustomAttributes.Add(New Attribute(m_BackingField, Compiler.TypeCache.System_Runtime_CompilerServices_CompilerGeneratedAttribute, Nothing))

        DirectCast(Parent, TypeDeclaration).Members.Add(m_BackingField)

        'Build the assignment statement that will be inserted into the declaring type's constructor to initialise
        'the property with any default value
        If Signature.Initialiser IsNot Nothing Then
            m_ConstructorAssignStmt = New AssignmentStatement(Me)
            m_ConstructorAssignStmt.Init(New SimpleNameExpression(Me, New Identifier(Name), Nothing), Signature.Initialiser)
        End If

        'Fill out the Get method to return the value of the backing field
        Dim GetMethod As New PropertyGetDeclaration(Me)

        Dim GetBody As New CodeBlock(GetMethod)
        Dim GetReturnStmt As New ReturnStatement(GetBody)
        Dim ReturnExpression As New SimpleNameExpression(GetReturnStmt, BackingFieldIdentifier, Nothing)
        GetReturnStmt.Init(ReturnExpression)
        GetBody.AddStatement(GetReturnStmt)

        GetMethod.Init(Modifiers, ImplementsClause, GetBody)

        'Create the set method body
        Dim SetMethod As New PropertySetDeclaration(Me)

        'Create the parameter which holds the value to assign to the backing field
        Dim ValParmList As New ParameterList(SetMethod)
        Dim ValueParm As New Parameter(ValParmList, "AutoPropertyValue", Signature.TypeName)
        ValParmList.List.Add(ValueParm)

        'Create the body of the Set method to initialise the backing field with the value from
        'the 'value' parameter
        Dim SetBody As New CodeBlock(SetMethod)
        Dim FieldAssignStmt As New AssignmentStatement(SetBody)
        Dim FieldAccessExpr As New SimpleNameExpression(FieldAssignStmt, BackingFieldIdentifier, Nothing)
        Dim ValueParmAccessExpr As New SimpleNameExpression(FieldAssignStmt, New Identifier(ValueParm.Name), Nothing)
        FieldAssignStmt.Init(FieldAccessExpr, ValueParmAccessExpr)
        SetBody.AddStatement(FieldAssignStmt)

        SetMethod.Init(Modifiers, ImplementsClause, SetBody, ValParmList)

        'Update the set/get method definitions to the auto generated versions
        GetDeclaration = GetMethod
        SetDeclaration = SetMethod

    End Sub

    ''' <summary>
    ''' Instantiates the get/set methods for this property declaration and issues any errors for user declared
    ''' fields which conflict with the backing field for an auto-implemented property.
    ''' </summary>
    ''' <returns>True if no errors were issued, otherwise false.</returns>
    ''' <remarks></remarks>
    Private Function InstantiateGetSetMethodBodies() As Boolean

        'Build up a list of field names for cross reference later on
        Dim Result As Boolean = True
        Dim FieldNames As New System.Collections.Generic.Dictionary(Of String, MemberDeclaration)(StringComparer.InvariantCultureIgnoreCase)

        For Each TypeField As MemberDeclaration In DeclaringType.Members.GetSpecificMembers(Of TypeVariableDeclaration)()
            FieldNames.Add(TypeField.Name, TypeField)
        Next

        'Auto-implemented properties add a backing field with the name "_<prop name>" to the containing
        'type. Check now that there isn't a field already defined with that name
        Dim BackingFieldName As String = String.Format("_{0}", Name)

        If FieldNames.ContainsKey(BackingFieldName) Then

            'variable '{0}' conflicts with a member implicitly declared for property '{1}' in {2} '{3}'
            Result = Compiler.Report.ShowMessage(Messages.VBNC31061, FieldNames(BackingFieldName).Location,
                                                 FieldNames(BackingFieldName).Name, Name, DeclaringType.DescriptiveType, DeclaringType.Name)

        Else
            BuildGetSetMethodBodies()
        End If

        Return Result

    End Function

End Class