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

Imports System.Reflection
Imports System.Reflection.Emit

#If DEBUG Then
#Const EXTENDEDDEBUG = 0
#End If
''' <summary>
'''Expression  ::=
'''	SimpleExpression  |
'''	TypeExpression  |
'''	MemberAccessExpression  |
'''	DictionaryAccessExpression  |
'''	IndexExpression  |
'''	NewExpression  |
'''	CastExpression  |
'''	OperatorExpression
''' 
'''SimpleExpression  ::=
'''	LiteralExpression  |
'''	ParenthesizedExpression  |
'''	InstanceExpression  |
'''	SimpleNameExpression  |
'''	AddressOfExpression
''' 
'''TypeExpression  ::=
'''	GetTypeExpression  |
'''	TypeOfIsExpression  |
'''	IsExpression
''' 
'''NewExpression  ::=
'''	ObjectCreationExpression  |
'''	ArrayCreationExpression  |
'''	DelegateCreationExpression
''' 
'''OperatorExpression  ::=
'''	ArithmeticOperatorExpression  |
'''	RelationalOperatorExpression  |
'''	LikeOperatorExpression  |
'''	ConcatenationOperatorExpression  |
'''	ShortCircuitLogicalOperatorExpression  |
'''	LogicalOperatorExpression  |
'''	ShiftOperatorExpression
''' 
'''ArithmeticOperatorExpression  ::=
'''	UnaryPlusExpression  |
'''	UnaryMinusExpression  |
'''	AdditionOperatorExpression  |
'''	SubtractionOperatorExpression  |
'''	MultiplicationOperatorExpression  |
'''	DivisionOperatorExpression  |
'''	ModuloOperatorExpression  |
'''	ExponentOperatorExpression
''' </summary>
''' <remarks></remarks>
Public MustInherit Class Expression
    Inherits ParsedObject

    ''' <summary>
    ''' The classification of this expression
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Classification As ExpressionClassification

    ''' <summary>
    ''' First finds a code block, then finds the specified type in the code block.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function FindFirstParentOfCodeBlock(Of T)() As T
        Dim cb As CodeBlock = Me.FindFirstParent(Of CodeBlock)()
        If cb IsNot Nothing Then
            Return cb.FindFirstParent(Of T)()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Get the parent code block. Might be nothing!
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function FindParentCodeBlock() As CodeBlock
        If TypeOf Parent Is CodeBlock Then
            Return DirectCast(Parent, CodeBlock)
        Else
            If TypeOf Parent Is Expression Then
                Return DirectCast(Parent, Expression).FindParentCodeBlock
            ElseIf TypeOf Parent Is BlockStatement Then
                Return DirectCast(Parent, BlockStatement).CodeBlock
            ElseIf TypeOf Parent Is Statement Then
                Return DirectCast(Parent, Statement).FindParentCodeBlock
            Else
                Return Nothing
            End If
        End If
    End Function

    Function IsSharedContext() As Boolean
        Dim p As ParsedObject
        Dim td As TypeDeclaration
        Dim md As MethodDeclaration

        p = Parent
        While p IsNot Nothing
            md = TryCast(p, MethodDeclaration)
            If md IsNot Nothing Then Return md.IsShared
            td = TryCast(p, TypeDeclaration)
            If td IsNot Nothing Then Return td.IsShared
            p = p.Parent
        End While

        Return False
    End Function

    ''' <summary>
    ''' The classification of this expression
    ''' </summary>
    ''' <remarks></remarks>
    Property Classification() As ExpressionClassification
        Get
            Return m_Classification
        End Get
        Set(ByVal value As ExpressionClassification)
            m_Classification = value
        End Set
    End Property

    ''' <summary>
    ''' The type of the expression.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overridable ReadOnly Property ExpressionType() As Mono.Cecil.TypeReference
        Get
            Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
            Return Nothing
        End Get
    End Property

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Overridable Function GetConstant(ByRef result As Object, ByVal ShowError As Boolean) As Boolean
        If ShowError Then Show30059()
        Return False
    End Function

    Friend NotOverridable Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True
        Dim constant As Object = Nothing

        Try
            If GetConstant(constant, False) Then
                If Helper.CompareType(Me.ExpressionType, Compiler.TypeCache.Nothing) Then
                    Emitter.EmitLoadValue(Info, constant)
                ElseIf Info.DesiredType IsNot Nothing AndAlso CecilHelper.IsByRef(Info.DesiredType) Then
                    Emitter.EmitLoadValueAddress(Info, constant)
                Else
                    Emitter.EmitLoadValue(Info.Clone(Me, Me.ExpressionType), constant)
                End If
            ElseIf TypeOf Me.Classification Is MethodGroupClassification Then
                result = Me.Classification.AsMethodGroupClassification.GenerateCode(Info) AndAlso result
            Else
                result = GenerateCodeInternal(Info) AndAlso result
            End If
        Catch ex As Exception
            Compiler.Report.ShowMessage(Messages.VBNC99999, Me.Location, "Internal compiler error close to this location")
            Throw
        End Try

        Return result
    End Function

    Protected Overridable Function GenerateCodeInternal(ByVal Info As EmitInfo) As Boolean
        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
    End Function

#Region "Resolution region"
    ''' <summary>
    ''' Has this expression been resolved?
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Resolved As Boolean

    ''' <summary>
    ''' Is this expression beeing resolved (in Resolve / DoResolve)
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Resolving As Boolean

    Function ResolveExpression(ByVal ResolveInfo As ResolveInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(ResolveInfo IsNot Nothing)

        StartResolve()

        Try
            result = ResolveExpressionInternal(ResolveInfo) AndAlso result
        Catch ex As Exception
            Compiler.Report.ShowMessage(Messages.VBNC99999, Me.Location, "Internal compiler error close to this location")
            Throw
        End Try
#If EXTENDEDDEBUG Then
        Helper.Assert(result = False OrElse m_Classification IsNot Nothing, "Classification is nothing! (type of expression = " & Me.GetType.ToString & ")")
        Helper.Assert(ResolveInfo.CanFail OrElse result = (Compiler.Report.Errors = 0))
#End If

        EndResolve(result)
        Return result
    End Function

    <Obsolete()> Function ResolveExpression() As Boolean
        Return ResolveExpression(ResolveInfo.Default(Parent.Compiler))
    End Function

    ''' <summary>
    ''' Call StartResolve to enable check for recursive resolving.
    ''' Call EndResolve when finished resolving.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub StartResolve()
        If m_Resolving Then
            'Recursive resolution.
            'TODO: Find a meaningful error message
            Throw New InternalException(Me)
        End If
        m_Resolving = True
#If EXTENDEDDEBUG Then
        If Me.TypeReferencesResolved = False Then
            Compiler.Report.WriteLine("TypeReferences not resolved for expression  " & Me.ToString)
        End If
#End If
#If EXTENDEDDEBUG Then
        If m_Resolved Then
            Compiler.Report.WriteLine("Resolving expression " & Me.ToString & " more than once (Location: " & Me.Location.ToString & ")")
        End If
#End If
    End Sub

    ''' <summary>
    ''' Is this expression beeing resolved (in Resolve)?
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property IsResolving() As Boolean
        Get
            Return m_Resolving
        End Get
    End Property

    ''' <summary>
    ''' Is this constant resolved?
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Property IsResolved() As Boolean
        Get
            Return m_Resolved
        End Get
        Set(ByVal value As Boolean)
            m_Resolved = value
        End Set
    End Property

    ''' <summary>
    ''' Call StartResolve to enable check for recursive resolving.
    ''' Call EndResolve when finished resolving.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub EndResolve(ByVal result As Boolean)
        If Not m_Resolving Then Throw New InternalException(Me)
        m_Resolving = False
        m_Resolved = result
    End Sub

    <Obsolete("Call ResolveExpression"), ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> Public NotOverridable Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Return ResolveExpression(Info)
    End Function

    Protected Overridable Function ResolveExpressionInternal(ByVal Info As ResolveInfo) As Boolean
        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
    End Function

    Function ResolveAddressOfExpression(ByVal DelegateType As Mono.Cecil.TypeReference, ByVal ShowErrors As Boolean) As Boolean
        Dim result As Boolean = True
        Dim aoe As AddressOfExpression = TryCast(Me, AddressOfExpression)

        If aoe Is Nothing Then
            result = False
        Else
            result = aoe.Resolve(DelegateType, ShowErrors) AndAlso result
        End If

        Return result
    End Function

    Function GetObjectReference() As Expression
        Dim result As Expression
        If TypeOf Me Is GetRefExpression Then
            Return Me
        ElseIf TypeOf Me Is InstanceExpression Then
            Return Me
        ElseIf CecilHelper.IsValueType(ExpressionType) Then
            If TypeOf Me Is DeRefExpression Then
                Dim derefExp As DeRefExpression = DirectCast(Me, DeRefExpression)
                result = derefExp.Expression
            ElseIf Helper.CompareType(CecilHelper.FindDefinition(Me.ExpressionType).BaseType, Compiler.TypeCache.System_Enum) Then
                result = New BoxExpression(Me, Me, CecilHelper.MakeByRefType(Me.ExpressionType))
                'ElseIf Me.ExpressionType.IsValueType AndAlso Helper.IsNullableType(Compiler, Me.ExpressionType) = False Then
                '    result = New BoxExpression(Me, Me, Me.ExpressionType)
            Else
                result = New GetRefExpression(Me, Me)
            End If
        Else
            result = Me
        End If

        Return result
    End Function

    Function DereferenceByRef() As Expression
        Dim result As Expression

        If CecilHelper.IsByRef(ExpressionType) Then
            result = New DeRefExpression(Me, Me)
        Else
            result = Me
        End If

        Return result
    End Function

    ''' <summary>
    ''' The resulting expression is NOT resolved.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ReclassifyToPropertyAccessExpression() As Expression
        Dim result As Expression
        Select Case m_Classification.Classification
            Case ExpressionClassification.Classifications.PropertyGroup
                Dim pgClass As PropertyGroupClassification = Me.Classification.AsPropertyGroup
                result = New PropertyGroupToPropertyAccessExpression(Me, pgClass)
            Case ExpressionClassification.Classifications.Value
                Throw New InternalException(Me)
            Case ExpressionClassification.Classifications.Variable
                Throw New InternalException(Me)
            Case ExpressionClassification.Classifications.EventAccess
                Throw New InternalException(Me)
            Case ExpressionClassification.Classifications.LateBoundAccess
                Return New LateBoundAccessToPropertyAccessExpression(Me, Me.Classification.AsLateBoundAccess)
            Case ExpressionClassification.Classifications.MethodGroup
                Throw New InternalException(Me)
            Case ExpressionClassification.Classifications.MethodPointer
                Throw New InternalException(Me)
            Case ExpressionClassification.Classifications.PropertyAccess
                Throw New InternalException(Me)
            Case ExpressionClassification.Classifications.Void
                Throw New InternalException(Me)
            Case ExpressionClassification.Classifications.Type
                Dim exp As Expression = Nothing
                If m_Classification.AsTypeClassification.CreateAliasExpression(Me, exp) = False Then
                    Throw New InternalException(Me)
                End If
                If exp.Classification.IsPropertyGroupClassification Then
                    exp = exp.ReclassifyToPropertyAccessExpression
                    If exp.ResolveExpression(ResolveInfo.Default(Compiler)) = False Then
                        Throw New InternalException(Me)
                    End If
                End If
                Helper.Assert(exp.Classification.IsPropertyAccessClassification)
                Return exp
            Case ExpressionClassification.Classifications.Namespace
                Throw New InternalException(Me)
            Case Else
                Throw New InternalException(Me)
        End Select

        Return result
    End Function

    Function ReclassifyMethodPointerToValueExpression(ByVal DelegateType As Mono.Cecil.TypeReference) As Expression
        Dim result As Expression = Nothing

        Helper.Assert(Classification.IsMethodPointerClassification)
        Helper.Assert(TypeOf Me Is AddressOfExpression)

        result = New DelegateOrObjectCreationExpression(Me, DelegateType, DirectCast(Me, AddressOfExpression))

        Return result
    End Function

    ''' <summary>
    ''' Reclassifies an expression.
    ''' The resulting expression is NOT resolved.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ReclassifyToValueExpression() As Expression
        Dim result As Expression = Nothing
        Select Case m_Classification.Classification
            Case ExpressionClassification.Classifications.Value
                Return Me 'This expression is already a value expression.
            Case ExpressionClassification.Classifications.Variable
                result = New VariableToValueExpression(Me, Me.Classification.AsVariableClassification)
            Case ExpressionClassification.Classifications.MethodGroup
                result = New MethodGroupToValueExpression(Me, Me.Classification.AsMethodGroupClassification)
            Case ExpressionClassification.Classifications.PropertyAccess
                result = New PropertyAccessToValueExpression(Me, Me.Classification.AsPropertyAccess)
            Case ExpressionClassification.Classifications.PropertyGroup
                result = New PropertyGroupToValueExpression(Me, Me.Classification.AsPropertyGroup)
            Case ExpressionClassification.Classifications.LateBoundAccess
                result = New LateBoundAccessToValueExpression(Me, Me.Classification.AsLateBoundAccess)
            Case ExpressionClassification.Classifications.MethodPointer
                Throw New InternalException(Me, "Use the other overload.")
            Case ExpressionClassification.Classifications.EventAccess
                Throw New InternalException(Me)
            Case ExpressionClassification.Classifications.Void
                Compiler.Report.ShowMessage(Messages.VBNC30491, Me.Location)
                Return Nothing
            Case ExpressionClassification.Classifications.Type
                Dim exp As Expression = Nothing
                If m_Classification.AsTypeClassification.CreateAliasExpression(Me, exp) = False Then
                    Throw New InternalException(Me)
                End If
                Return exp
            Case ExpressionClassification.Classifications.Namespace
                Throw New InternalException(Me)
            Case Else
                Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
                Return Nothing
        End Select

        Return result
    End Function

    Function ReportReclassifyToValueErrorMessage() As Boolean

        Select Case m_Classification.Classification
            Case ExpressionClassification.Classifications.EventAccess
                Compiler.Report.ShowMessage(Messages.VBNC32022, Me.Location)
            Case ExpressionClassification.Classifications.Type
                Compiler.Report.ShowMessage(Messages.VBNC30108, Me.Location)
            Case ExpressionClassification.Classifications.Namespace
                Compiler.Report.ShowMessage(Messages.VBNC30112, Me.Location)
            Case ExpressionClassification.Classifications.Void
                Compiler.Report.ShowMessage(Messages.VBNC30491, Me.Location)
            Case Else
                Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
        End Select

        Return False
    End Function

#End Region

    Overridable ReadOnly Property AsString() As String
        Get
            Return "<String representation of " & Me.GetType.FullName & " not implemented>"
        End Get
    End Property
End Class

