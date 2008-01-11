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

''' <summary>
''' Every value has an associated type.
''' 
''' Can be reclassified as a value. The value stored in the variable is fetched.
''' </summary>
''' <remarks></remarks>
Public Class ValueClassification
    Inherits ExpressionClassification

    Private m_Type As Type
    Private m_IsLiteralNothing As Boolean
    Private m_ConstantValue As Object
    Private m_Value As Expression
    Private m_Constant As ConstantDeclaration
    Private m_EnumVariable As EnumMemberDeclaration
    Private m_Field As FieldInfo
    Private m_Classification As ExpressionClassification

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            Dim result As Boolean
            Static recursive As Boolean
            Helper.Assert(recursive = False)
            recursive = True
            If ReclassifiedClassification IsNot Nothing Then
                result = ReclassifiedClassification.IsConstant
                If result Then
                    ConstantValue = ReclassifiedClassification.ConstantValue
                End If
            ElseIf m_Value IsNot Nothing AndAlso m_Value.IsConstant Then
                result = True
            Else
                result = False
            End If
            recursive = False
            Return result
        End Get
    End Property

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(Info.DesiredType IsNot Nothing)

        If m_Value IsNot Nothing Then
            result = m_Value.GenerateCode(Info) AndAlso result
        ElseIf m_ConstantValue IsNot Nothing Then
            Emitter.EmitLoadValueConstantOrValueAddress(Info, m_ConstantValue)
        ElseIf m_Classification IsNot Nothing Then
            Select Case m_Classification.Classification
                Case Classifications.Value
                    Throw New InternalException(Me)
                Case Classifications.Variable
                    result = m_Classification.AsVariableClassification.GenerateCodeAsValue(Info) AndAlso result
                Case Classifications.MethodPointer
                    If Info.DesiredType IsNot Nothing Then
                        result = m_Classification.AsMethodPointerClassification.GenerateCode(Info) AndAlso result
                    Else
                        Throw New InternalException(Me)
                    End If
                Case Classifications.MethodGroup
                    result = m_Classification.AsMethodGroupClassification.GenerateCodeAsValue(Info) AndAlso result
                Case Classifications.PropertyGroup
                    result = m_Classification.AsPropertyGroup.GenerateCodeAsValue(Info) AndAlso result
                Case Else
                    Return Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
            End Select
        Else
            Return Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
        End If

        Return result
    End Function

    ReadOnly Property ReclassifiedClassification() As ExpressionClassification
        Get
            Return m_Classification
        End Get
    End Property

    ReadOnly Property Value_IsLiteralNothing() As Boolean
        Get
            Return m_IsLiteralNothing
        End Get
    End Property

    ReadOnly Property Value_ConstantValue() As Object
        Get
            Return m_ConstantValue
        End Get
    End Property

    ReadOnly Property Value_Constant() As ConstantDeclaration
        Get
            Return m_Constant
        End Get
    End Property

    ReadOnly Property Value_EnumVariable() As EnumMemberDeclaration
        Get
            Return m_EnumVariable
        End Get
    End Property

    ReadOnly Property Value_Field() As FieldInfo
        Get
            Return m_Field
        End Get
    End Property

    Property Type() As Type
        Get
            Helper.Assert(m_Type IsNot Nothing)
            Return m_Type
        End Get
        Set(ByVal value As Type)
            m_Type = value
        End Set
    End Property

    Private Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Classifications.Value, Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal EnumVariable As EnumMemberDeclaration)
        MyBase.New(Classifications.Value, Parent)
        Helper.Assert(EnumVariable IsNot Nothing)
        m_EnumVariable = EnumVariable
        m_Type = m_EnumVariable.FindFirstParent(Of EnumDeclaration).EnumConstantType
        Helper.Assert(m_Type IsNot Nothing)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Variable As VariableDeclaration)
        MyBase.New(Classifications.Value, Parent)
        Helper.Assert(Variable IsNot Nothing)
        m_Classification = New VariableClassification(Me.Parent, Variable)
        m_Type = DirectCast(m_Classification, VariableClassification).Type
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Variable As FieldInfo, ByVal InstanceExpression As Expression)
        MyBase.New(Classifications.Value, Parent)
        Helper.Assert(Variable IsNot Nothing)
        m_Classification = New VariableClassification(Me.Parent, Variable, InstanceExpression)
        m_Type = DirectCast(m_Classification, VariableClassification).Type
        Helper.Assert(m_Type IsNot Nothing)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Constant As ConstantDeclaration)
        MyBase.New(Classifications.Value, Parent)
        Helper.Assert(Constant IsNot Nothing)
        m_Constant = Constant
        m_Type = Constant.FieldType
        Helper.Assert(m_Type IsNot Nothing)
    End Sub

    Sub New(ByVal ParentAndValue As Expression)
        Me.New(DirectCast(ParentAndValue, ParsedObject))
        m_Type = ParentAndValue.ExpressionType
        m_Value = ParentAndValue
        'Helper.Assert(m_Type IsNot Nothing)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Value As Expression)
        Me.New(DirectCast(Parent, ParsedObject))
        m_Type = Value.ExpressionType
        m_Value = Value
        'Helper.Assert(m_Type IsNot Nothing)
    End Sub
    Sub New(ByVal ParentAndValue As Expression, ByVal ExpressionType As Type)
        Me.New(DirectCast(ParentAndValue, ParsedObject))
        m_Type = ExpressionType
        m_Value = ParentAndValue
        Helper.Assert(m_Type IsNot Nothing)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Type As Type, ByVal Value As Object)
        Me.New(Parent)
        m_Type = Type
        m_ConstantValue = Value
    End Sub

    Sub New(ByVal Classification As VariableClassification)
        Me.new(Classification.Parent)
        m_Classification = Classification
        m_Type = Classification.Type
        Helper.Assert(m_Type IsNot Nothing)
    End Sub

    Sub New(ByVal Classification As PropertyGroupClassification)
        Me.new(Classification.Parent)
        m_Classification = Classification
        m_Type = Classification.Type
        Helper.Assert(m_Type IsNot Nothing)
    End Sub

    Sub New(ByVal Classification As MethodGroupClassification)
        Me.New(Classification.Parent)
        m_Classification = Classification
        m_Type = Classification.Type
        Helper.Assert(m_Type IsNot Nothing)
    End Sub

    Sub New(ByVal Classification As MethodPointerClassification)
        Me.New(Classification.Parent)
        m_Classification = Classification
        m_Type = Classification.Type
        Helper.Assert(m_Type IsNot Nothing)
    End Sub

    Sub New(ByVal Classification As LateBoundAccessClassification)
        Me.New(Classification.Parent)
        m_Classification = Classification
        m_Type = Classification.Type
        Helper.Assert(m_Type IsNot Nothing)
    End Sub

    Sub New(ByVal Classification As PropertyAccessClassification)
        Me.New(Classification.Parent)
        m_Classification = Classification
        m_Type = Classification.Type
        Helper.Assert(m_Type IsNot Nothing)
    End Sub
End Class
