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

#If DEBUG Then
#Const EXTENDEDDEBUG = 0
#End If
''' <summary>
''' ArgumentList  ::=	PositionalArgumentList  ,  NamedArgumentList  |
'''                     PositionalArgumentList  |
'''	                    NamedArgumentList
''' 
''' PositionalArgumentList  ::=  Expression  |  PositionalArgumentList  ","  [  Expression  ]
''' 
''' NamedArgumentList  ::=  IdentifierOrKeyword  ":="  Expression  |  NamedArgumentList  ,  IdentifierOrKeyword  :=  Expression
'''
''' </summary>
''' <remarks></remarks>
Public Class ArgumentList
    Inherits ParsedObject

    Private m_Arguments As New BaseObjects(Of Argument)(Me)

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal ParamArray Expressions() As Expression)
        MyBase.New(Parent)
        If Expressions IsNot Nothing Then
            For Each item As Expression In Expressions
                m_Arguments.Add(New PositionalArgument(Me, m_Arguments.Count, item))
            Next
        End If
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal Arguments As Generic.List(Of Argument))
        MyBase.New(Parent)
        If Arguments IsNot Nothing Then
            m_Arguments.AddRange(Arguments)
        End If
    End Sub

    Sub Init(ByVal Arguments As BaseObjects(Of Argument))
        m_Arguments = Arguments
    End Sub

    Sub ReplaceArguments(ByVal NewArguments As ArgumentList)
        m_Arguments.Clear()
        If NewArguments IsNot Nothing Then m_Arguments.AddRange(NewArguments.Arguments)
    End Sub

    Function ReplaceAndVerifyArguments(ByVal NewArguments As ArgumentList, ByVal Method As Mono.Cecil.MethodReference, ByVal ShowErrors As Boolean) As Boolean
        Dim result As Boolean = True

        ReplaceArguments(NewArguments)
        result = VerifyArguments(Method, ShowErrors) AndAlso result

        Return result
    End Function

    Function ReplaceAndVerifyArguments(ByVal NewArguments As ArgumentList, ByVal Method As Mono.Cecil.PropertyReference, ByVal ShowErrors As Boolean) As Boolean
        Dim result As Boolean = True

        ReplaceArguments(NewArguments)
        result = VerifyArguments(Method, ShowErrors) AndAlso result

        Return result
    End Function

    Function VerifyArguments(ByVal Method As Mono.Cecil.PropertyReference, ByVal ShowErrors As Boolean) As Boolean
        Return VerifyArguments(Method.Parameters, ShowErrors)
    End Function

    Function VerifyArguments(ByVal Method As Mono.Cecil.MethodReference, ByVal ShowErrors As Boolean) As Boolean
        Return VerifyArguments(Method.Parameters, ShowErrors)
    End Function

    ''' <summary>
    ''' This function only verifies the expression type of the argument,
    ''' it does not expand paramarray arguments nor optional arguments
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function VerifyArguments(ByVal parameters As Mono.Collections.Generic.Collection(Of ParameterDefinition), ByVal ShowErrors As Boolean) As Boolean
        Dim result As Boolean = True

#If EXTENDEDDEBUG Then
        Compiler.Report.WriteLine(Me.Location.ToString & ": VerifyArguments: " & Method.DeclaringType.FullName & "::" & Helper.ToString(Compiler, Method))
#End If
        For i As Integer = 0 To m_Arguments.Count - 1
            Dim exp As Expression
            Dim arg As Argument = m_Arguments(i)
            Dim par As Mono.Cecil.ParameterDefinition = parameters(i)

            If Helper.CompareType(arg.Expression.ExpressionType, Compiler.TypeCache.DelegateUnresolvedType) Then
                Dim aoe As AddressOfExpression = TryCast(arg.Expression, AddressOfExpression)
                Dim delegateType As Mono.Cecil.TypeReference = par.ParameterType

                Helper.Assert(aoe IsNot Nothing)
                Helper.Assert(delegateType IsNot Nothing)

                result = aoe.Resolve(delegateType, True) AndAlso result

                Dim del As DelegateOrObjectCreationExpression
                del = New DelegateOrObjectCreationExpression(Me)
                del.Init(delegateType, New ArgumentList(del, aoe))
                result = del.ResolveExpression(ResolveInfo.Default(Compiler)) AndAlso result
                m_Arguments(i).Expression = del
            End If

            If CecilHelper.IsByRef(par.ParameterType) AndAlso CecilHelper.IsByRef(arg.Expression.ExpressionType) = False AndAlso CecilHelper.IsValueType(CecilHelper.GetElementType(par.ParameterType)) = False Then
                If arg.Expression.Classification.IsPropertyAccessClassification Then
                    Dim propRef As PropertyReference = arg.Expression.Classification.AsPropertyAccess.Property
                    Dim propDef As PropertyDefinition = CecilHelper.FindDefinition(propRef)
                    If propDef.GetMethod Is Nothing Then
                        If ShowErrors Then Compiler.Report.ShowMessage(Messages.VBNC30524, m_Arguments(i).Location, propDef.Name)
                        result = False
                    End If
                    exp = arg.Expression
                ElseIf Helper.CompareType(arg.Expression.ExpressionType, Compiler.TypeCache.Nothing) = False Then
                    exp = New GetRefExpression(Me, arg.Expression)
                Else
                    exp = arg.Expression
                End If
            ElseIf CecilHelper.IsByRef(par.ParameterType) AndAlso CecilHelper.IsByRef(arg.Expression.ExpressionType) AndAlso Helper.CompareType(CecilHelper.GetElementType(par.ParameterType), CecilHelper.GetElementType(arg.Expression.ExpressionType)) Then
                exp = arg.Expression
            ElseIf CecilHelper.IsByRef(par.ParameterType) AndAlso Helper.CompareType(arg.Expression.ExpressionType, CecilHelper.GetElementType(par.ParameterType)) = False AndAlso (arg.Expression.Classification.IsVariableClassification OrElse arg.Expression.Classification.IsPropertyAccessClassification) Then
                Dim varTmp As LocalVariableDeclaration
                Dim assignA, assignB As AssignmentStatement
                Dim block As CodeBlock = Me.FindFirstParent(Of CodeBlock)()
                Dim thisStatement As Statement = Me.FindFirstParent(Of Statement)()

                varTmp = New LocalVariableDeclaration(Me.Parent)
                varTmp.Init(Nothing, "VB$tmp", CecilHelper.GetElementType(par.ParameterType))
                'result = varTmp.ResolveMember(ResolveInfo.Default(Compiler)) AndAlso result

                assignA = New AssignmentStatement(Me.Parent)
                assignA.Init(New VariableExpression(assignA, varTmp), arg.Expression)
                result = assignA.ResolveStatement(ResolveInfo.Default(Compiler)) AndAlso result

                assignB = New AssignmentStatement(Me.Parent)
                assignB.Init(arg.Expression, New VariableExpression(assignB, varTmp))
                result = assignB.ResolveStatement(ResolveInfo.Default(Compiler)) AndAlso result

                block.AddVariable(varTmp)
                block.AddStatementBefore(assignA, thisStatement)
                block.AddStatementAfter(assignB, thisStatement)

                exp = New GetRefExpression(Me, New VariableExpression(Me, varTmp))
            Else
#If EXTENDEDDEBUG Then
                Compiler.Report.WriteLine("VerifyArguments, needs convertion from " & arg.Expression.ExpressionType.FullName & " to " & par.ParameterType.FullName)
#End If
                exp = Nothing
                result = Helper.IsConvertible(arg, arg.Expression, arg.Expression.ExpressionType, par.ParameterType, True, exp, ShowErrors, Nothing) AndAlso result
            End If
            If exp IsNot arg.Expression Then
                m_Arguments(i) = New PositionalArgument(Me, i, exp)
            End If
        Next

        Return result
    End Function

    Function FillWithOptionalParameters(ByVal Method As Mono.Cecil.MethodReference) As Boolean
        Dim result As Boolean = True
        Dim parameters As Mono.Collections.Generic.Collection(Of ParameterDefinition)

        If Method Is Nothing Then Return False

        parameters = Helper.GetParameters(Compiler, Method)

        For i As Integer = 0 To parameters.Count - 1
            If i >= Me.Count OrElse m_Arguments(i).Expression Is Nothing Then
                Helper.Assert(parameters(i).IsOptional)

                Dim newExp As ConstantExpression
                Dim newArg As PositionalArgument

                newExp = New ConstantExpression(Me, parameters(i).Constant, parameters(i).ParameterType)
                newArg = New PositionalArgument(Me, i, newExp)

                If i >= Me.Count Then
                    Me.Arguments.Add(newArg)
                Else
                    Me.Arguments(i) = newArg
                End If
            End If
        Next

        Return result
    End Function

    ReadOnly Property AsString() As String
        Get
            Dim result As String = ""
            Dim sep As String = ""

            For Each arg As Argument In m_Arguments
                result = result & sep & arg.AsString
                sep = ", "
            Next
            Return result
        End Get
    End Property

    ReadOnly Property ArgumentsTypesAsString() As String
        Get
            Dim result As String = ""
            Dim sep As String = ""

            For Each arg As Argument In m_Arguments
                result = result & sep & arg.AsTypeString
                sep = ", "
            Next
            Return result
        End Get
    End Property

    Function ToTypes() As Mono.Cecil.TypeReference()
        Dim result(m_Arguments.Count - 1) As Mono.Cecil.TypeReference
        For i As Integer = 0 To m_Arguments.Count - 1
            If m_Arguments(i).Expression Is Nothing Then
                result(i) = New MissingType(Compiler)
            Else
                result(i) = m_Arguments(i).Expression.ExpressionType
            End If
        Next
        Return result
    End Function

    ''' <summary>
    ''' A list of the types of the arguments.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTypes() As Generic.List(Of Mono.Cecil.TypeReference)
        Dim result As New Generic.List(Of Mono.Cecil.TypeReference)

        Helper.Assert(Me.HasNamedArguments = False)

        For Each arg As PositionalArgument In m_Arguments
            If arg.Expression IsNot Nothing Then
                result.Add(arg.Expression.ExpressionType)
            Else
                Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
            End If
        Next

        Return result
    End Function

    ReadOnly Property HasNamedArguments() As Boolean
        Get
            For Each argument As Argument In m_Arguments
                If TypeOf argument Is NamedArgument Then
                    Return True
                End If
            Next
            Return False
        End Get
    End Property

    ReadOnly Property HasPositionalArguments() As Boolean
        Get
            For Each argument As Argument In m_Arguments
                If TypeOf argument Is PositionalArgument Then
                    Return True
                End If
            Next
            Return False
        End Get
    End Property

    ReadOnly Property Arguments() As Generic.List(Of Argument)
        Get
            Return m_Arguments
        End Get
    End Property

    <Obsolete("Use GenerateCode(EmitInfo, Type())")> Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Return Helper.GenerateCodeCollection(m_Arguments, Info)
    End Function

    Friend Overloads Function GenerateCode(ByVal Info As EmitInfo, ByVal Types() As Mono.Cecil.TypeReference) As Boolean
        Dim result As Boolean = True
        Helper.Assert(Types.Length = Me.Count)

        For i As Integer = 0 To Count - 1
            result = Item(i).GenerateCode(Info.Clone(Me, True, False, Types(i)), Nothing) AndAlso result
        Next
        Return result
    End Function

    Friend Overloads Function GenerateCode(ByVal Info As EmitInfo, ByVal params As Mono.Collections.Generic.Collection(Of ParameterDefinition)) As Boolean
        Dim result As Boolean = True

        Helper.Assert(params.Count >= Me.Count)

        For i As Integer = 0 To Count - 1
            result = Item(i).GenerateCode(Info.Clone(Me, True, False, params(i).ParameterType), params(i)) AndAlso result
        Next

        For i As Integer = Count To params.Count - 1
            Helper.Assert(params(i).IsOptional)
            Emitter.EmitLoadValue(Info.Clone(Me, params(i).ParameterType), params(i).Constant)
        Next

        Return result
    End Function

    Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = m_Arguments.ResolveCode(Info) AndAlso result

        Return result
    End Function

    Public Overrides Function ResolveTypeReferences() As Boolean
        Return Helper.ResolveTypeReferencesCollection(m_Arguments)
    End Function

    Default ReadOnly Property Item(ByVal Index As Integer) As Argument
        Get
            Return DirectCast(m_Arguments.Item(Index), Argument)
        End Get
    End Property

    ReadOnly Property Length() As Integer
        Get
            Return Count
        End Get
    End Property

    ReadOnly Property Count() As Integer
        Get
            Return m_Arguments.Count
        End Get
    End Property

End Class
