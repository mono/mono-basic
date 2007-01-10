' 
' Visual Basic.Net Compiler
' Copyright (C) 2004 - 2007 Rolf Bjarne Kvinge, RKvinge@novell.com
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

'Imports System.Reflection.Emit
'Imports System.Reflection

'Public Class MemberParameter
'    Inherits NamedObject
'    Implements IAttributable

'    Private m_Type As New TypeName(Me)

'    Public ParameterBuilder As ParameterBuilder

'    ''' <summary>
'    ''' Sets the index of the paramter. 0 based.
'    ''' </summary>
'    ''' <remarks></remarks>
'    Public ReadOnly ParameterIndex As Integer

'    Private m_OptionalValue As Expression
'    Private m_Attributes As New AttributeMembers(Me)

'    ''' <summary>
'    ''' The value of the optional expression.
'    ''' </summary>
'    ''' <remarks></remarks>
'    Private m_DefaultValue As Object

'    Shadows Function Clone(Optional ByVal NewParent As MethodMember = Nothing) As MemberParameter
'        Dim result As MemberParameter
'        If NewParent Is Nothing Then
'            result = New MemberParameter(Parent, ParameterIndex)
'        Else
'            result = New MemberParameter(NewParent, ParameterIndex)
'        End If
'        result.m_Type = m_Type
'        result.Name = Name
'        result.Modifiers = New ArrayList(Modifiers)
'        'Helper.Assert(valOptional.Elements.Count = 0)
'        Return result
'    End Function

'    ''' <summary>
'    ''' The value of the default value (if any).
'    ''' Returns nothing even if no default value.
'    ''' </summary>
'    ''' <value></value>
'    ''' <remarks></remarks>
'    ReadOnly Property DefaultValue() As Object
'        Get
'            Return m_DefaultValue
'        End Get
'    End Property

'    ReadOnly Property TypeName() As TypeName
'        Get
'            Return m_Type
'        End Get
'    End Property
'#If DEBUG Then
'    Public Sub Dump(ByVal Xml As System.Xml.XmlWriter)
'        Xml.WriteStartElement("MemberParameter")
'        MyBase.DumpProperties(Xml)
'        Xml.WriteAttributeString("Type", m_Type.ToString)
'        If MyBase.ContainsModifier(KS.Optional) Then
'            Xml.WriteStartElement("OptionalValue")
'            Helper.Assert(m_OptionalValue IsNot Nothing)
'            m_OptionalValue.Dump(Xml)
'            Xml.WriteEndElement()
'        End If
'        Xml.WriteEndElement()
'    End Sub
'#End If
'    ''' <summary>
'    ''' 
'    ''' </summary>
'    ''' <param name="Parent"></param>
'    ''' <param name="ParamIndex">Sets the index of the parameter. 0 based</param>
'    ''' <remarks></remarks>
'    Sub New(ByVal Parent As MethodMember, ByVal ParamIndex As Integer)
'        MyBase.New(Parent)
'        ParameterIndex = ParamIndex
'    End Sub

'    Sub New(ByVal Parent As DelegateType, ByVal ParamIndex As Integer)
'        MyBase.New(Parent)
'        ParameterIndex = ParamIndex
'    End Sub

'    ''' <summary>
'    ''' Creates a compiler generated parameter.
'    ''' ParameterIndex is 0-based.
'    ''' </summary>
'    ''' <param name="Parent"></param>
'    ''' <param name="Name"></param>
'    ''' <param name="ParameterIndex">0 based.</param>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Shared Function CreateGenerated(ByVal Parent As MethodMember, ByVal Name As String, ByVal Type As TypeName, ByVal ParameterIndex As Integer, ByVal isByVal As Boolean) As MemberParameter
'        Dim result As MemberParameter

'        result = New MemberParameter(Parent, ParameterIndex)
'        result.CompilerGenerated = True
'        result.setName(Name)
'        result.m_Type = Type
'        If isByVal Then
'            result.Modifiers.Add(KS.ByVal)
'        Else
'            result.Modifiers.Add(KS.ByRef)
'        End If

'        Return result
'    End Function

'    Function GetInfo() As ParameterInfo
'        Static newInfo As ParameterInfo
'        If newInfo Is Nothing Then newInfo = New MemberParameterInfo(Me)
'        Return newInfo
'    End Function

'    Overrides Function Resolve() As Boolean
'        Return m_Type.Resolve(Me, False)
'    End Function

'    ''' <summary>
'    ''' Returns the type of this parameter. If it is a byref parameter, returns a reference type.
'    ''' </summary>
'    ''' <value></value>
'    ''' <remarks></remarks>
'    ReadOnly Property ParameterType() As Type
'        Get
'            If m_Type.Type Is Nothing Then Throw New InternalException(Me)
'            If IsByRef Then
'                Return m_Type.ResolvedType.MakeByRefType
'            Else
'                Return m_Type.ResolvedType
'            End If
'        End Get
'    End Property

'    ''' <summary>
'    ''' Returns the type of this parameter. Does not change if it is a byref parameter or not.
'    ''' </summary>
'    ''' <value></value>
'    ''' <remarks></remarks>
'    ReadOnly Property ExactType() As Type
'        Get
'            If m_Type.Type Is Nothing Then Throw New InternalException(Me)
'            Return m_Type.ResolvedType
'        End Get
'    End Property

'    Public Overrides Function ParseMain() As Boolean
'        Dim result As Boolean
'        result = setName(True)
'        If tm.Accept(KS.As) Then
'            result = m_Type.Parse() AndAlso result
'        Else
'            m_Type.SetType(BuiltInDataTypes.Object) '.Name = "Object"
'        End If
'        If MyBase.Modifiers.Contains(KS.Optional) Then
'            If tm.AcceptIfNotError(KS.Equals, Messages.VBNC90019, "=") Then
'                'tm.GotoAny(True, New KS() {KS.Comma, KS.RParenthesis})
'                m_OptionalValue = Compiler.ExpressionParser.Parse(New Expression.ParseInfo(Me))
'            Else
'                Return False
'            End If
'        End If
'        If Not (Modifiers.Contains(KS.ByVal) OrElse Modifiers.Contains(KS.ByRef)) Then
'            Modifiers.Add(KS.ByVal)
'        End If
'        Return result
'    End Function
'#If DEBUG Then
'    Overrides Sub Dump()
'        DumpModifiers()
'        Debug.Write(Name & " As " & m_Type.Name)
'        If ContainsModifier(KS.Optional) Then
'            Debug.Write(" = ")
'            m_OptionalValue.Dump()
'        End If
'    End Sub
'#End If
'    ReadOnly Property IsParamArray() As Boolean
'        Get
'            Return Me.ContainsModifier(KS.ParamArray)
'        End Get
'    End Property

'    ReadOnly Property IsOptional() As Boolean
'        Get
'            Return Me.ContainsModifier(KS.Optional)
'        End Get
'    End Property

'    ReadOnly Property IsByVal() As Boolean
'        Get
'            Return Not IsByRef
'        End Get
'    End Property

'    Public ReadOnly Property IsByRef() As Boolean
'        Get
'            Return MyBase.ContainsModifier(KS.ByRef)
'        End Get
'    End Property

'    Public Overrides Sub SetupModifiers()
'        AddValidModifier(KS.ByVal, New KS() {KS.ByRef})
'        AddValidModifier(KS.ByRef, New KS() {KS.ByVal})
'        AddValidModifier(KS.Optional, Nothing)
'        AddValidModifier(KS.ParamArray, New KS() {KS.Optional})
'    End Sub

'    Function DefineParameter(Optional ByVal ParameterOffset As Integer = 1) As Boolean
'        Helper.NotImplemented()
'        'Dim result As Boolean = True
'        'Dim pAttr As Reflection.ParameterAttributes
'        'Dim Parent As MethodMember = DirectCast(Me.Parent, MethodMember)

'        'If IsOptional Then
'        '    pAttr = ParameterAttributes.Optional
'        'Else
'        '    pAttr = Reflection.ParameterAttributes.None
'        'End If

'        'If TypeOf Parent Is ConstructorMember Then
'        '    ParameterBuilder = Parent.ConstructorBuilder.DefineParameter(ParameterIndex + ParameterOffset, pAttr, Name)
'        'Else
'        '    ParameterBuilder = Parent.MethodBuilder.DefineParameter(ParameterIndex + ParameterOffset, pAttr, Name)
'        'End If

'        'If IsOptional Then
'        '    Dim optConstant As Object
'        '    If m_OptionalValue.IsConstant Then
'        '        optConstant = m_OptionalValue.ConstantValue
'        '        Dim resultConstant As Object = Nothing
'        '        If TypeResolution.CheckNumericRange(optConstant, resultConstant, Me.ExactType) Then
'        '            If IsByRef Then
'        '                'resultConstant = Convert.ChangeType(resultConstant, Me.ParameterType)
'        '                Throw New NotImplementedException("I don't know how to emit optional byref parameters yet!")
'        '            End If
'        '            ParameterBuilder.SetConstant(resultConstant)
'        '            m_DefaultValue = resultConstant
'        '        Else
'        '            Helper.Stop()
'        '        End If
'        '    Else
'        '        Helper.Stop()
'        '    End If
'        'ElseIf IsParamArray Then
'        '    m_Attributes.Add(New ParamArrayAttributeMember(Me))
'        'End If

'        'm_Attributes.Emit(Me)

'        'Return result
'    End Function

'    Function DefineParameter(ByVal MethodBuilder As MethodBuilder, Optional ByVal ParameterOffset As Integer = 1) As Boolean
'        Dim result As Boolean = True
'        Dim pAttr As Reflection.ParameterAttributes

'        pAttr = Reflection.ParameterAttributes.None
'        ParameterBuilder = MethodBuilder.DefineParameter(ParameterIndex + ParameterOffset, pAttr, Name)

'        Return result
'    End Function

'    Shadows ReadOnly Property Parent() As MethodMember
'        Get
'            'Helper.Assert(TypeOf MyBase.ParentAsNamedObject Is MethodMember)
'            Helper.NotImplemented() : Return Nothing 'Return DirectCast(MyBase.ParentAsNamedObject, MethodMember)
'        End Get
'    End Property

'    Public Property Attributes() As AttributeMembers Implements IAttributable.Attributes
'        Get
'            Return m_Attributes
'        End Get
'        Set(ByVal value As AttributeMembers)
'            m_Attributes = value
'        End Set
'    End Property
'#If DEBUG Then
'    Public Sub DumpAttributes() Implements IAttributable.DumpAttributes

'    End Sub

'    Public Sub DumpAttributes(ByVal Xml As System.Xml.XmlWriter) Implements IAttributable.DumpAttributes

'    End Sub
'#End If
'End Class