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

Public Class Dumper
    Private m_Dumper As IndentedTextWriter

    Property Dumper() As IndentedTextWriter
        Get
            Return m_Dumper
        End Get
        Set(ByVal value As IndentedTextWriter)
            m_Dumper = value
        End Set
    End Property

#If EXTENDEDDEBUG Then
    Public Sub Dump(ByVal Element As ConstructorDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Dump(Element.Modifiers)
        Dumper.Write("Sub New")
        Dump(Element.Signature)
        Dumper.WriteLine("")
        Dumper.Indent()
        Dump(Element.Code)
        Dumper.Unindent()
        Dumper.WriteLine("End Sub")
    End Sub

    Public Sub Dump(ByVal Element As CustomEventHandlerDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Dumper.Write(Element.HandlerType.ToString)
        Dumper.Write("(")
        If Element.Signature.Parameters IsNot Nothing Then Dump(Element.Signature.Parameters)
        Dumper.WriteLine(")")
        If Element.Code IsNot Nothing Then Dump(Element.Code)
        Dumper.WriteLine("End " & Element.HandlerType.ToString)
    End Sub

    Public Sub Dump(ByVal Element As ExternalSubDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Dump(Element.Modifiers)
        Dumper.Write("Declare ")
        If Element.CharsetModifier <> KS.None Then Dumper.Write(Element.CharsetModifier.ToString & " ")
        Dumper.Write("Sub ")
        Element.Identifier.Dump(Dumper)
        If Element.LibraryClause IsNot Nothing Then Dump(Element.LibraryClause)
        If Element.AliasClause IsNot Nothing Then Dump(Element.AliasClause)
        If Element.Signature.Parameters IsNot Nothing Then
            Dumper.Write("(")
            Dump(Element.Signature.Parameters)
            Dumper.Write(")")
        End If
        Dumper.WriteLine("")
    End Sub

    Public Sub Dump(ByVal Element As ExternalFunctionDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Dump(Element.Modifiers)
        Dumper.Write("Declare ")
        If Element.CharsetModifier <> KS.None Then Dumper.Write(Element.CharsetModifier.ToString & " ")
        Dumper.Write("Function ")
        Element.Identifier.Dump(Dumper)
        If Element.LibraryClause IsNot Nothing Then Dump(Element.LibraryClause)
        If Element.AliasClause IsNot Nothing Then Dump(Element.AliasClause)
        If Element.Signature.Parameters IsNot Nothing Then
            Dumper.Write("(")
            Dump(Element.Signature.Parameters)
            Dumper.Write(")")
        End If
        If Element.Signature.TypeName IsNot Nothing Then
            Dumper.Write("As ")
            If Element.Signature.ReturnTypeAttributes IsNot Nothing Then Dump(Element.Signature.ReturnTypeAttributes)
            Dump(Element.Signature.TypeName)
        End If
        Dumper.WriteLine("")
    End Sub

    Public Sub Dump(ByVal Element As CaseClause)
        If Element.Comparison <> KS.None Then
            Dumper.Write("Is " & Enums.GetKSStringAttribute(Element.Comparison).Value & " ")
            Element.Expression1.Dump(Dumper)
        Else
            Element.Expression1.Dump(Dumper)
            If Element.Expression2 IsNot Nothing Then
                Dumper.Write(" To ")
                Element.Expression2.Dump(Dumper)
            End If
        End If
    End Sub

    Public Sub Dump(ByVal Element As Constraint)
        If Element.TypeName IsNot Nothing Then
            Dump(Element.TypeName)
        Else
            Dumper.Write("New")
        End If
    End Sub

    Public Sub Dump(ByVal Element As VariableInitializer)
        Dump(Element.Initializer)
    End Sub

    Public Sub Dump(ByVal Element As UsingStatement)
        Dumper.Write("Using ")
        Dump(Element.UsingResources)
        Dumper.WriteLine("")
        Dump(Element.CodeBlock)
        Dumper.WriteLine("End Using")
    End Sub

    Sub Dump(ByVal Element As identifier)
        Dumper.Write("[" & Element.Name & "] ")
    End Sub

    Public Sub Dump(ByVal Element As TypeImplementsClauses)
        For Each clause As NonArrayTypeName In Element.Clauses
            Dumper.Write("Implements ")
            Dump(clause)
            Dumper.WriteLine("")
        Next
    End Sub

    Sub Dump(ByVal Element As ParameterList)
        Dim sep As String = ""
        For Each pa As Parameter In Element.List
            Dumper.Write(sep)
            Dump(pa)
            sep = ", "
        Next
    End Sub

    Sub Dump(ByVal Element As ConstraintList)
        Dim sep As String = ""
        For Each pa As Constraint In Element.List
            Dumper.Write(sep)
            Dump(pa)
            sep = ", "
        Next
    End Sub

    Sub Dump(ByVal Element As MemberImplementsList)
        Dim sep As String = ""
        For Each pa As InterfaceMemberSpecifier In Element.List
            Dumper.Write(sep)
            Dump(pa)
            sep = ", "
        Next
    End Sub
    Public Sub Dump(ByVal Element As SyncLockStatement)
        Dumper.Write("SyncLock ")
        Element.Lock.Dump(Dumper)
        Dumper.WriteLine("")
        Dump(Element.CodeBlock)
        Dumper.WriteLine("End SyncLock")
    End Sub

    Public Sub Dump(ByVal Element As ReDimStatement)
        Dumper.Write("Redim ")
        If Element.IsPreserve Then Dumper.Write("Preserve ")
        Dump(Element.Clauses)
        Dumper.WriteLine("")
    End Sub

    Sub Dump(ByVal Elements As BaseObjects(Of CatchStatement))
        For Each e As CatchStatement In Elements
            Dump(e)
        Next
    End Sub

    Public Sub Dump(ByVal Element As ImportsStatement)
        Dumper.Write("Imports ")
        Dump(Element.Clauses)
        Dumper.WriteLine("")
    End Sub

    Public Sub Dump(ByVal Element As HandlesOrImplements)
        Dump(Element.Clause)
    End Sub

    Sub Dump(ByVal Element As BaseObject)
        If Element Is Nothing Then Return

        If TypeOf Element Is Expression Then Dump(DirectCast(Element, Expression)) : Return
        If TypeOf Element Is Token Then Dump(DirectCast(Element, Token)) : Return

        Dim m As MethodInfo
        m = GetType(Dumper).GetMethod("Dump", BindingFlags.Instance Or BindingFlags.DeclaredOnly Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.ExactBinding, Nothing, CallingConventions.Any, New Type() {Element.GetType}, Nothing)
        If m IsNot Nothing Then
            m.Invoke(Me, New Object() {Element})
            Return
        End If
        Diagnostics.Debug.WriteLine("Dump(" & Element.GetType.Name & ") not implemented yet.")
        Console.WriteLine("Dump(" & Element.GetType.Name & ") not implemented yet.")
    End Sub

    Public Sub Dump(ByVal Element As EraseStatement)
        Dumper.Write("Erase ")
        Dump(Element.Targets)
        Dumper.WriteLine("")
    End Sub

    Sub Dump(ByVal Element As ExpressionList)
        Dim sep As String = ""
        For Each e As Expression In Element
            Dumper.Write(sep)
            Dump(e)
            sep = ", "
        Next
    End Sub

    Sub Dump(ByVal Element As TypeParameterList)
        Dim sep As String = ""
        For Each e As TypeParameter In Element.List
            Dumper.Write(sep)
            Dump(e)
            sep = ", "
        Next
    End Sub

    Sub Dump(ByVal Element As VariableInitializerList)
        Dim sep As String = ""
        For Each e As VariableInitializer In Element.List
            Dumper.Write(sep)
            Dump(e)
            sep = ", "
        Next
    End Sub

    Sub Dump(ByVal Element As GetTypeTypeName)
        Dump(Element.Name)
    End Sub

    Public Sub Dump(ByVal Element As AttributeList)
        Dim sep As String = ""
        For Each a As Attribute In Element.List
            Dumper.Write(sep)
            Dump(a)
            sep = ", "
        Next
    End Sub

    Public Sub Dump(ByVal Element As BaseObjects(Of CaseStatement))
        Dim sep As String = ""
        For Each a As CaseStatement In Element
            Dumper.Write(sep)
            Dump(a)
            sep = ", "
        Next
    End Sub
    Public Sub Dump(ByVal Element As ForEachStatement)
        Dumper.Write("For Each ")
        Dump(Element.LoopControlVariable)
        Dumper.Write(" In ")
        Element.InExpression.Dump(Dumper)
        Dumper.WriteLine("")
        Dump(Element.CodeBlock)
        Dumper.Write("Next ")
        If Element.NextExpression IsNot Nothing Then
            Element.NextExpression.Dump(Dumper)
        End If
        Dumper.WriteLine("")
    End Sub

    Public Sub Dump(ByVal Element As EventAccessorDeclarations)
        For Each e As CustomEventHandlerDeclaration In Element.Handlers
            Dump(e)
        Next
    End Sub
    Public Sub Dump(ByVal Element As EventMemberSpecifier)
        Dump(Element.First)
        Dumper.Write(".")
        Dump(Element.Second)
    End Sub

    Public Sub Dump(ByVal Element As ForStatement)
        Dumper.Write("For ")
        Dump(Element.LoopControlVariable)
        Dumper.Write(" = ")
        Element.LoopStartExpression.Dump(Dumper)
        Dumper.Write(" To ")
        Element.LoopEndExpression.Dump(Dumper)
        If Element.LoopStepExpression IsNot Nothing Then
            Dumper.Write(" Step ")
            Element.LoopStepExpression.Dump(Dumper)
        End If
        Dumper.WriteLine("")
        Dump(Element.CodeBlock)
        Dumper.Write("Next")
        If Element.NextExpressionList IsNot Nothing Then
            Dumper.Write(" ")
            Dump(Element.NextExpressionList)
        End If
        Dumper.WriteLine("")
    End Sub

    Sub Dump(ByVal Element As EventHandlesList)
        For Each e As EventMemberSpecifier In Element
            Dump(e)
        Next
    End Sub

    Public Sub Dump(ByVal Element As HandlesClause)
        Dumper.Write("Handles ")
        Dump(Element.List)
    End Sub

    Public Sub Dump(ByVal Element As ElseIfStatement)
        Dumper.Write("ElseIf ")
        Element.Condition.Dump(Dumper)
        Dumper.WriteLine(" Then")
        Dump(Element.CodeBlock)
    End Sub

    Public Sub Dump(ByVal Element As SelectStatement)
        Dumper.Write("Select Case ")
        Element.Test.Dump(Dumper)
        Dumper.WriteLine("")
        Dumper.Indent()
        Dump(Element.Cases)
        Dumper.Unindent()
        Dumper.WriteLine("End Select")
    End Sub

    Public Sub Dump(ByVal Element As IfStatement)
        Dumper.Write("If ")
        Element.Condition.Dump(Dumper)
        Dumper.WriteLine(" Then")
        Dump(Element.CodeBlock)
        If Element.ElseIfs IsNot Nothing Then
            For Each elseexp As ElseIfStatement In Element.ElseIfs
                Dump(elseexp)
            Next
        End If
        If Element.FalseCode IsNot Nothing Then
            Dumper.WriteLine("Else ")
            Dump(Element.FalseCode)
        End If
        Dumper.WriteLine("End If")
    End Sub

    Public Sub Dump(ByVal Element As LoopControlVariable)
        If Element.Expression IsNot Nothing Then
            Element.Expression.Dump(Dumper)
        Else
            Element.Identifier.Dump(Dumper)
            If Element.ArrayNameModifier IsNot Nothing Then
                Dump(Element.ArrayNameModifier)
            End If
            Dumper.Write(" As ")
            Dump(Element.TypeName)
        End If
    End Sub

    Public Sub Dump(ByVal Element As QualifiedIdentifier)
        If Element.IsFirstQualifiedIdentifier Then
            Dump(Element.FirstAsQualifiedIdentifier)
        ElseIf Element.IsFirstIdentifier Then
            Dump(Element.FirstAsIdentifier)
        ElseIf Element.IsFirstGlobal Then
            Dumper.Write("Global")
        Else
            Throw New InternalException(Me)
        End If
        If Element.Second IsNot Nothing Then
            Dumper.Write(".")
            Element.Second.Dump(Dumper)
        End If
    End Sub

    Public Sub Dump(ByVal Element As TryStatement)
        Dumper.WriteLine("Try")
        Dump(Element.CodeBlock)
        Dump(Element.Catches)
        If Element.FinallyBlock IsNot Nothing Then
            Dumper.WriteLine("Finally")
            Dump(Element.FinallyBlock)
        End If
        Dumper.WriteLine("End Try")
    End Sub

    Public Sub Dump(ByVal Element As ConstructedTypeName)
        Dump(Element.QualifiedIdentifier)
        Dumper.Write("(Of ")
        Dump(Element.TypeArgumentList)
        Dumper.Write(")")
    End Sub

    Public Sub Dump(ByVal Element As WithStatement)
        Dumper.Write("With ")
        Element.WithExpression.Dump(Dumper)
        Dumper.WriteLine("")
        Dump(Element.CodeBlock)
        Dumper.WriteLine("End With")
    End Sub

    Public Sub Dump(ByVal Element As WhileStatement)
        Dumper.Write("While ")
        Element.Condition.Dump(Dumper)
        Dumper.WriteLine("")
        Dump(Element.CodeBlock)
        Dumper.WriteLine("End While")
    End Sub

    Public Sub Dump(ByVal Element As DoStatement)
        Dumper.Write("Do ")
        If Element.PreCondition IsNot Nothing Then
            If Element.IsWhile Then Dumper.Write("While ") Else Dumper.Write("Until ")
            Element.PreCondition.Dump(Dumper)
        End If
        Dumper.WriteLine("")
        Dump(Element.CodeBlock)
        Dumper.Write("Loop ")
        If Element.PostCondition IsNot Nothing Then
            If Element.IsWhile Then Dumper.Write("While ") Else Dumper.Write("Until ")
            Element.PostCondition.Dump(Dumper)
        End If
        Dumper.WriteLine("")
    End Sub

    Public Sub Dump(ByVal Element As CatchStatement)
        Dumper.Write("Catch ")
        If Element.Variable IsNot Nothing Then
            Element.Variable.Dump(Dumper)
            Dumper.Write(" As ")
            Dump(Element.TypeName)
            Dumper.Write(" ")
        End If
        If Element.When IsNot Nothing Then
            Dumper.Write("When ")
            Element.When.Dump(Dumper)
        End If
        Dumper.WriteLine("")
        Dump(Element.CodeBlock)
    End Sub

    Sub Dump(ByVal Element As CaseClauses)
        Dim sep As String = ""
        For Each e As CaseClause In Element
            Dumper.Write(sep)
            Dump(e)
            sep = ", "
        Next
    End Sub

    Public Sub Dump(ByVal Element As CaseStatement)
        Dumper.Write("Case ")
        If Element.IsElse Then
            Dumper.WriteLine("Else")
        Else
            Dump(Element.Clauses)
            Dumper.WriteLine("")
        End If
        Dump(Element.CodeBlock)
    End Sub

    Public Sub Dump(ByVal Element As ArrayElementInitializer)
        Dumper.Write("{")
        If Element.Initializers IsNot Nothing Then
            Dump(Element.Initializers)
        End If
        Dumper.Write("}")
    End Sub

    Public Sub Dump(ByVal Element As AssemblyDeclaration)
        Dumper.WriteLine("'Dump of assembly:")
        Dumper.Indent()
        If Element.Attributes IsNot Nothing Then Dump(Element.Attributes)
        Dumper.WriteLine("")
        Dump(Element.TypeDeclarations)
        Dumper.Unindent()
        Dumper.WriteLine("'End of dump of assembly.")
    End Sub

    Sub Dump(ByVal Element As ClassDeclaration)
        Dumper.Write("Class " & Element.Name)
        Dump(Element.TypeParameters)
        Dumper.WriteLine("")
        Dumper.Indent()
        Dump(Element.Members)
        Dumper.Unindent()
        Dumper.WriteLine("End Class")
    End Sub

    Public Sub Dump(ByVal Element As Argument)
        If Element.Expression IsNot Nothing Then Dump(Element.Expression)
    End Sub

    Sub Dump(ByVal Expression As Expression)
        'Helper.NotImplemented() 'Expression.dump()
        Expression.Dump(Dumper)
    End Sub

    Public Sub Dump(ByVal Element As ArgumentList)
        If Element Is Nothing Then Return
        Dim sep As String = ""
        For Each arg As Argument In Element.Arguments
            Dumper.Write(sep)
            Dump(arg)
            sep = ", "
        Next
    End Sub

    Sub Dump(ByVal Element As TypeArgumentList)
        Dim sep As String = ""
        For Each t As TypeName In Element
            Dumper.Write(sep)
            Dump(t)
            sep = ", "
        Next
    End Sub
    Public Sub Dump(ByVal Element As OptionExplicitStatement)
        Dumper.Write("Option Explicit ")
        If Element.Off Then
            Dumper.WriteLine("Off")
        Else
            Dumper.WriteLine("On")
        End If
    End Sub

    Public Sub Dump(ByVal Element As OptionCompareStatement)
        Dumper.Write("Option Compare ")
        If Element.IsBinary Then
            Dumper.WriteLine("Binary")
        Else
            Dumper.WriteLine("Text")
        End If
    End Sub

    Public Sub Dump(ByVal Element As OptionStrictStatement)
        Dumper.Write("Option Strict ")
        If Not Element.IsOn Then
            Dumper.WriteLine("Off")
        Else
            Dumper.WriteLine("On")
        End If
    End Sub

    Sub Dump(ByVal Element As ImportsClauses)
        For Each e As ImportsClause In Element
            Dump(e)
        Next
    End Sub

    Public Sub Dump(ByVal Element As ImportsClause)
        Helper.NotImplementedYet("Dump(Element.Clause)")
    End Sub

    Sub Dump(ByVal Element As ImportsNamespaceClause)
        Helper.NotImplementedYet("Dump(Element.Object)")
    End Sub

    Sub Dump(ByVal Element As BoundList)
        If Element.Expressions.GetUpperBound(0) >= 0 AndAlso Element.Expressions(0) IsNot Nothing Then
            Element.Expressions(0).Dump(Dumper)
        End If
        For i As Integer = 1 To Element.Expressions.GetUpperBound(0)
            Dumper.Write(", ")
            If Element.Expressions(i) IsNot Nothing Then
                Element.Expressions(i).Dump(Dumper)
            End If
        Next
    End Sub

    Public Sub Dump(ByVal Element As ImportsAliasClause)
        Element.Identifier.Dump(Dumper)
        Dumper.Write(" = ")
        Dump(Element.Second)
    End Sub

    Sub Dump(ByVal Element As CodeFiles)
        Helper.NotImplementedYet("Dump(CodeFiles)")
        'Dumper.WriteLine("Dump of codefiles:")
        'Dumper.Indent()
        'For Each cf As CodeFile In Me
        '    cf.Dump(Dumper)
        'Next
        'Dumper.Unindent()
        'Dumper.WriteLine("End of dump of codefiles.")
    End Sub

    Sub Dump(ByVal Element As CodeFile)
        Dumper.WriteLine(Element.FileName & ":")
        Dumper.Indent()
        If Element.OptionCompare IsNot Nothing Then Dump(Element.OptionCompare)
        If Element.OptionStrict IsNot Nothing Then Dump(Element.OptionStrict)
        If Element.OptionExplicit IsNot Nothing Then Dump(Element.OptionExplicit)
        Dump(Element.Imports)
        Dumper.Unindent()
    End Sub

    'Public Sub Dump(ByVal Element As ArrayTypeName)
    '    Dumper.Write("New ")
    '    Dump(Element.NonArrayTypeName)
    '    Dump(Element.ArrayNameModifier)
    '    Dump(Element.ArrayElementInitializer)
    'End Sub

    Public Sub Dump(ByVal Element As ArrayTypeName)
        Dump(Element.TypeName)
        Dump(Element.ArrayTypeModifiers)
    End Sub

    Public Sub Dump(ByVal Element As CodeBlock)
        Dumper.Indent()
        'Helper.DumpCollection(m_Variables, Dumper)
        'Helper.DumpCollection(m_Statements, Dumper)
        Dumper.WriteLine("'Skipped codedump")
        Dumper.Unindent()
    End Sub

    Sub Dump(ByVal Elements As TypeDeclaration())
        For Each member As TypeDeclaration In Elements
            Dump(member)
        Next
    End Sub

    Sub Dump(ByVal Element As FunctionSignature)
        Dump(DirectCast(Element, SubSignature))
        If Element.TypeName IsNot Nothing Then
            Dumper.Write(" As ")
            If Element.ReturnTypeAttributes IsNot Nothing Then Dump(Element.ReturnTypeAttributes)
            Dumper.Write(" ")
            Dump(Element.TypeName)
            Dumper.Write(" ")
        End If
    End Sub

    Sub Dump(ByVal Element As TypeDeclaration)
        If TypeOf Element Is ClassDeclaration Then Dump(DirectCast(Element, ClassDeclaration)) : Return
        If TypeOf Element Is StructureDeclaration Then Dump(DirectCast(Element, StructureDeclaration)) : Return
        If TypeOf Element Is InterfaceDeclaration Then Dump(DirectCast(Element, InterfaceDeclaration)) : Return
        If TypeOf Element Is EnumDeclaration Then Dump(DirectCast(Element, EnumDeclaration)) : Return
        If TypeOf Element Is DelegateDeclaration Then Dump(DirectCast(Element, DelegateDeclaration)) : Return
        If TypeOf Element Is ModuleDeclaration Then Dump(DirectCast(Element, ModuleDeclaration)) : Return
        Helper.NotImplemented()
    End Sub

    Sub Dump(ByVal Element As MemberDeclarations)
        For Each member As IMember In Element
            If TypeOf member Is TypeDeclaration Then Dump(DirectCast(member, TypeDeclaration)) : Continue For
            If TypeOf member Is FunctionDeclaration Then Dump(DirectCast(member, FunctionDeclaration)) : Continue For
            If TypeOf member Is SubDeclaration Then Dump(DirectCast(member, SubDeclaration)) : Continue For
            If TypeOf member Is RegularPropertyDeclaration Then Dump(DirectCast(member, RegularPropertyDeclaration)) : Continue For
            If TypeOf member Is VariableDeclaration Then Dump(DirectCast(member, VariableDeclaration)) : Continue For
            If TypeOf member Is ConstructorDeclaration Then Dump(DirectCast(member, ConstructorDeclaration)) : Continue For
            If TypeOf member Is ConstantDeclaration Then Dump(DirectCast(member, ConstantDeclaration)) : Continue For
            If TypeOf member Is MustOverridePropertyDeclaration Then Dump(DirectCast(member, MustOverridePropertyDeclaration)) : Continue For
            If TypeOf member Is EnumMemberDeclaration Then Dump(DirectCast(member, EnumMemberDeclaration)) : Continue For
            If TypeOf member Is InterfacePropertyMemberDeclaration Then Dump(DirectCast(member, InterfacePropertyMemberDeclaration)) : Continue For
            If TypeOf member Is InterfaceSubDeclaration Then Dump(DirectCast(member, InterfaceSubDeclaration)) : Continue For
            If TypeOf member Is InterfaceFunctionDeclaration Then Dump(DirectCast(member, InterfaceFunctionDeclaration)) : Continue For
            Helper.NotImplemented()
        Next
    End Sub

    Sub Dump(ByVal obj As IList, ByVal Dumper As IndentedTextWriter, Optional ByVal Delimiter As String = "")
        Dim tmpDelimiter As String = ""
        For Each o As BaseObject In obj
            Dumper.Write(tmpDelimiter)
            Dump(o)
            tmpDelimiter = Delimiter
        Next
    End Sub

    Public Sub Dump(ByVal Element As RaiseEventStatement)
        Dumper.Write("RaiseEvent ")
        Element.Event.Dump(Dumper)
        If Element.Arguments IsNot Nothing Then
            Dumper.Write("(")
            Dump(Element.Arguments)
            Dumper.Write(")")
        End If
        Dumper.WriteLine("")
    End Sub

    Public Sub Dump(ByVal Element As UsingDeclarator)
        Element.Identifier.Dump(Dumper)
        If Element.TypeName IsNot Nothing Then
            Dumper.Write(" As ")
            If Element.IsNew Then Dumper.Write("New ")
            Dump(Element.TypeName)
        End If
        If Element.ArgumentList IsNot Nothing Then
            Dumper.Write("(")
            Dump(Element.ArgumentList)
            Dumper.Write(")")
        End If
        If Element.VariableInitializer IsNot Nothing Then
            Dumper.Write(" = ")
            Dump(Element.VariableInitializer)
        End If
    End Sub

    Public Sub Dump(ByVal Element As AttributeArgumentExpression)
        Element.Expression.Dump(Dumper)
    End Sub

    Sub Dump(ByVal Element As AttributePositionalArgumentList)
        Dim sep As String = ""
        For Each e As AttributeArgumentExpression In Element
            Dumper.Write(sep)
            Dump(e)
            sep = ", "
        Next
    End Sub

    Sub Dump(ByVal Element As VariablePropertyInitializerList)
        Dim sep As String = ""
        For Each e As VariablePropertyInitializer In Element
            Dumper.Write(sep)
            Dump(e)
            sep = ", "
        Next
    End Sub

    Public Sub Dump(ByVal Element As VariablePropertyInitializer)
        Dump(Element.IdentifierOrKeyword)
        Dumper.Write(" := ")
        Dump(Element.AttributeArgumentExpression)
    End Sub

    Public Sub Dump(ByVal Element As Attribute)
        If Element.IsAssembly Then Dumper.Write("Assembly: ")
        If Element.IsModule Then Dumper.Write("Module: ")
        If Element.SimpleTypeName IsNot Nothing Then
            Dump(Element.SimpleTypeName)
        ElseIf Element.ResolvedType IsNot Nothing Then
            Dumper.Write(Element.ResolvedType.FullName)
        End If

        Dumper.Write("(")
        If Element.AttributeArguments IsNot Nothing Then
            Dump(Element.AttributeArguments)
        End If
        Dumper.Write(")")
    End Sub

    Public Sub Dump(ByVal Element As AttributeArguments)
        If Element.PositionalArgumentList IsNot Nothing Then Dump(Element.PositionalArgumentList)
        If Element.VariablePropertyInitializerList IsNot Nothing Then Dump(Element.VariablePropertyInitializerList)
    End Sub

    Public Sub Dump(ByVal Element As IdentifierOrKeyword)
        Dumper.Write("[")
        Element.Token.Dump(Dumper)
        dumper.write("]")
    End Sub

    Sub Dump(ByVal Element As IdentifierOrKeywordWithTypeArguments)
        Dump(DirectCast(Element, IdentifierOrKeyword))
        If Element.TypeArguments IsNot Nothing Then
            Dumper.Write("(Of ")
            Dump(Element.TypeArguments)
            Dumper.Write(")")
        End If
    End Sub

    Public Sub Dump(ByVal Element As VariableDeclaration)
        If Element.Modifiers Is Nothing Then Return 'Auto generated variable, such as in a for loop.
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Dump(Element.Modifiers)
        Dumper.Write(Element.Name)
        If Element.TypeName IsNot Nothing Then
            Dumper.Write(" As ")
            If Element.IsNew Then Dumper.Write("New ")
            Dump(Element.TypeName)
        End If
        If Element.VariableInitializer IsNot Nothing Then
            Dumper.Write(" = ")
            Dump(Element.VariableInitializer)
        ElseIf Element.ArgumentList IsNot Nothing Then
            Dumper.Write("(")
            Dump(Element.ArgumentList)
            Dumper.Write(")")
        End If
        Dumper.WriteLine("")
    End Sub
    Public Sub Dump(ByVal Element As EnumMemberDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Element.Identifier.Dump(Dumper)
        If Element.ConstantExpression IsNot Nothing Then
            Dumper.Write(" = ")
            Element.ConstantExpression.Dump(Dumper)
        End If
        Dumper.WriteLine("")
    End Sub
    Public Sub Dump(ByVal Element As ConversionOperatorDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Dump(Element.Modifiers)
        Dumper.Write("Operator CType(")
        Dump(Element.Operand)
        Dumper.Write(")")
        If Element.Signature.TypeName IsNot Nothing Then
            Dumper.Write(" As ")
            If Element.Signature.ReturnTypeAttributes IsNot Nothing Then Dump(Element.Signature.ReturnTypeAttributes)
            Dump(Element.Signature.TypeName)
        End If
        Dumper.WriteLine("")
        Dumper.Indent()
        Dump(Element.Code)
        Dumper.Unindent()
        Dumper.WriteLine("End Operator")
    End Sub

    Public Sub Dump(ByVal Element As ConstantDeclaration)
        Dumper.Write("Const ")
        Element.Identifier.Dump(Dumper)
        If Element.TypeName IsNot Nothing Then
            Dumper.Write(" As ")
            Dump(Element.TypeName)
        End If
        Dumper.Write(" = ")
        Element.ConstantExpression.Dump(Dumper)
        Dumper.WriteLine("")
    End Sub
    Public Sub Dump(ByVal Element As MustOverridePropertyDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Dump(Element.Modifiers)
        Dumper.Write("Property ")
        Dump(Element.Signature)
        If Element.ImplementsClause IsNot Nothing Then Dump(Element.ImplementsClause)
        Dumper.WriteLine("")
    End Sub

    Public Sub Dump(ByVal Element As PropertySetDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        ' Dump(Element.Modifiers)
        Dumper.Write("Set ")
        If Element.Signature.Parameters IsNot Nothing Then
            Dumper.Write("(")
            Dump(Element.Signature.Parameters)
            dumper.write(")")
        End If
        Dumper.WriteLine("")
        Dumper.Indent()
        Dump(Element.Code)
        Dumper.Unindent()
        Dumper.WriteLine("End Set")
    End Sub

    Public Sub Dump(ByVal Element As RegularPropertyDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Dump(Element.Modifiers)
        Dumper.Write("Property ")
        Dump(Element.Signature)
        If Element.ImplementsClause IsNot Nothing Then Dump(Element.ImplementsClause)
        Dumper.WriteLine("")
        Dumper.Indent()
        If Element.GetDeclaration IsNot Nothing Then Dump(Element.GetDeclaration)
        If Element.SetDeclaration IsNot Nothing Then Dump(Element.SetDeclaration)
        Dumper.Unindent()
        Dumper.WriteLine("End Property")
    End Sub
    Public Sub Dump(ByVal Element As PropertyGetDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        'Dump(Element.Modifiers)
        Dumper.WriteLine("Get ")
        Dumper.Indent()
        Dump(Element.Code)
        Dumper.Unindent()
        Dumper.WriteLine("End Get")
    End Sub
    Public Sub Dump(ByVal Element As InterfacePropertyMemberDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        If Element.Modifiers IsNot Nothing Then Dump(Element.Modifiers)
        Dumper.Write("Property ")
        Dump(Element.Signature)
        Dumper.WriteLine("")
    End Sub

    Public Sub Dump(ByVal Element As InterfaceSubDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        If Element.Modifiers IsNot Nothing Then Dump(Element.Modifiers)
        Dumper.Write("Sub ")
        Dump(Element.Signature)
        Dumper.WriteLine("")
    End Sub

    Public Sub Dump(ByVal Element As FunctionDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Dump(Element.Modifiers)
        Dumper.Write("Function ")
        Dump(Element.Signature)
        If Element.HandlesOrImplements IsNot Nothing Then Dump(Element.HandlesOrImplements)
        Dumper.WriteLine("")
        If Element.IsMustOverride = False Then
            Dump(Element.Code)
            Dumper.WriteLine("End Function")
        End If
    End Sub

    Public Sub Dump(ByVal Element As InterfaceEventMemberDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        If Element.Modifiers IsNot Nothing Then Dump(Element.Modifiers)
        Dumper.Write("Event ")
        Dump(Element.Identifier)
        If Element.Parameters IsNot Nothing Then Dump(Element.Parameters)
        If Element.Type IsNot Nothing Then Dump(Element.Type)
        Dumper.WriteLine("")
    End Sub

    Public Sub Dump(ByVal Element As SubDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Dump(Element.Modifiers)
        Dumper.Write("Sub ")
        Dump(Element.Signature)
        If Element.HandlesOrImplements IsNot Nothing Then Dump(Element.HandlesOrImplements)
        Dumper.WriteLine("")
        If Element.IsMustOverride = False Then
            Dump(Element.Code)
            Dumper.WriteLine("End Sub")
        End If
    End Sub

    Public Sub Dump(ByVal Element As InterfaceFunctionDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        If Element.Modifiers IsNot Nothing Then Dump(Element.Modifiers)
        Dumper.Write("Function ")
        Dump(Element.Signature)
        Dumper.WriteLine("")
    End Sub

    Public Sub Dump(ByVal Element As CustomEventDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Dump(Element.Modifiers)
        Dumper.Write("Custom Event ")
        Dump(Element.Identifier)
        Dumper.Write(" As ")
        Dump(Element.Type)
        If Element.ImplementsClause IsNot Nothing Then Dump(Element.ImplementsClause)
        Dumper.WriteLine("")
        Dumper.Indent()
        Dumper.WriteLine("not implemented")
        Dumper.Unindent()
        Dumper.WriteLine("End Event")
    End Sub

    Public Sub DumpClassDeclaration(ByVal Element As ClassDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        If Element.Modifiers IsNot Nothing Then Dump(Element.Modifiers)
        Dumper.Write("Class " & Element.Identifier.Name)
        If Element.TypeParameters IsNot Nothing Then Dump(Element.TypeParameters)
        Dumper.WriteLine("")
        Dumper.Indent()
        If Element.Inherits IsNot Nothing Then Dumper.WriteLine("Inherits " & Element.Inherits.Name)
        If Element.Implements IsNot Nothing Then Dump(Element.Implements)
        If Element.Members IsNot Nothing Then Dump(Element.Members)
        Dumper.Unindent()
        Dumper.WriteLine("End Class")
    End Sub

    Public Sub Dump(ByVal Element As RegularEventDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        If Element.Modifiers IsNot Nothing Then Dump(Element.Modifiers)
        Dumper.Write("Event ")
        Dump(Element.Identifier)
        If Element.Parameters IsNot Nothing Then Dump(Element.Parameters)
        If Element.Type IsNot Nothing Then Dump(Element.Type)
        If Element.ImplementsClause IsNot Nothing Then Dump(Element.ImplementsClause)
        Dumper.WriteLine("")
    End Sub

    Public Sub Dump(ByVal Element As Operand)
        Dumper.Write("ByVal ")
        Element.Identifier.Dump(Dumper)
        If Element.TypeName IsNot Nothing Then
            Dumper.Write(" As ")
            Dump(Element.TypeName)
        End If
    End Sub

    Public Sub Dump(ByVal Element As OperatorDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Dump(Element.Modifiers)
        Dumper.Write("Operator " & Element.Operator.ToString & "(")
        Dump(Element.Operand1)
        If Element.Operand2 IsNot Nothing Then
            Dumper.Write(", ")
            Dump(Element.Operand2)
        End If
        Dumper.Write(")")
        If Element.Signature.TypeName IsNot Nothing Then
            Dumper.Write(" As ")
            If Element.Signature.ReturnTypeAttributes IsNot Nothing Then Dump(Element.Signature.ReturnTypeAttributes)
            Dump(Element.Signature.TypeName)
        End If
        Dumper.WriteLine("")
        Dumper.Indent()
        Dump(Element.Code)
        Dumper.Unindent()
        Dumper.WriteLine("End Operator")
    End Sub

    Public Sub Dump(ByVal Element As InterfaceMemberSpecifier)
        Dump(Element.First)
        If Element.Second IsNot Nothing Then
            Dumper.Write(".")
            Dump(Element.Second)
        End If
    End Sub

    Public Sub Dump(ByVal Element As ParametersOrType)
        If Element.Parameters IsNot Nothing Then
            Dumper.Write("(")
            Dump(Element.Parameters)
            Dumper.Write(")")
        ElseIf Element.Type IsNot Nothing Then
            Dumper.Write("As ")
            Dump(Element.Type)
        End If
    End Sub

    Public Sub Dump(ByVal Element As MemberImplementsClause)
        Dumper.Write("Implements ")
        Dump(Element.ImplementsList)
    End Sub

    Public Sub Dump(ByVal Element As StructureDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        If Element.Modifiers IsNot Nothing Then Dump(Element.Modifiers)
        Dumper.Write("Structure ")
        Element.Identifier.Dump(Dumper)
        If Element.TypeParameters IsNot Nothing Then Dump(Element.TypeParameters)
        Dumper.WriteLine("")
        Dumper.Indent()
        If Element.Implements IsNot Nothing Then Dump(Element.Implements)
        Dump(Element.Members)
        Dumper.Unindent()
        Dumper.WriteLine("End Structure")
    End Sub

    Public Sub Dump(ByVal Elements As InterfaceBases)
        For i As Integer = 0 To Elements.Bases.GetUpperBound(0)
            Dumper.Write("Inherits ")
            Dump(Elements.Bases(i))
            Dumper.WriteLine("")
        Next
    End Sub

    Public Sub Dump(ByVal Element As InterfaceDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Dump(Element.Modifiers)
        Dumper.Write("Interface ")
        Element.Identifier.Dump(Dumper)
        If Element.TypeParameters IsNot Nothing Then Dump(Element.TypeParameters)
        Dumper.WriteLine("")
        Dumper.Indent()
        If Element.InterfaceBases IsNot Nothing Then Dump(Element.InterfaceBases)
        Dump(Element.Members)
        Dumper.Unindent()
        Dumper.WriteLine("End Interface")
    End Sub

    Public Sub Dump(ByVal Element As EnumDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        If Element.Modifiers IsNot Nothing Then Dump(Element.Modifiers)
        Dumper.Write("Enum " & Element.Name)
        If Element.QualifiedName <> KS.None Then
            Dumper.Write(" As " & Element.QualifiedName.ToString)
        End If
        Dumper.WriteLine("")
        Dumper.Indent()
        Dump(Element.Members)
        Dumper.Unindent()
        Dumper.WriteLine("End Enum")
    End Sub

    Public Sub Dump(ByVal Element As SubSignature)
        If Element.Identifier IsNot Nothing AndAlso TypeOf Element.FindMethod Is ConstructorDeclaration = False Then
            Element.Identifier.Dump(Dumper)
        End If
        If Element.TypeParameters IsNot Nothing Then Dump(Element.TypeParameters)
        If Element.Parameters IsNot Nothing Then
            Dumper.Write("(")
            Dump(Element.Parameters)
            Dumper.Write(")")
        End If
    End Sub

    Public Sub Dump(ByVal Element As DelegateDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        If Element.Modifiers IsNot Nothing Then Dump(Element.Modifiers)
        Dump(Element.Modifiers)
    End Sub

    Public Sub Dump(ByVal Elements As TypeParameterConstraints)
        Dumper.Write(" As ")
        Dumper.Write("{")
        Dump(Elements.Constraints)
        Dumper.Write("}")
    End Sub

    Public Sub Dump(ByVal Element As TypeParameter)
        Element.Identifier.Dump(Dumper)
        If Element.TypeParameterConstraints IsNot Nothing Then Dump(Element.TypeParameterConstraints)
    End Sub

    Public Sub Dump(ByVal Element As TypeParameters)
        If Element IsNot Nothing AndAlso Element.Parameters IsNot Nothing AndAlso Element.Parameters.Count > 0 Then
            Dumper.Write("(Of ")
            Dump(Element.Parameters)
            Dumper.Write(")")
        End If
    End Sub

    Public Sub Dump(ByVal Elements As Attributes)
        If Elements.Count > 0 Then
            Dumper.Write("<")
            For Each e As Attribute In Elements
                Dump(e)
            Next
            Dumper.Write(">")
        End If
    End Sub

    Public Sub Dump(ByVal Element As BaseList)
        Dim sep As String = ""
        For Each e As BaseObject In Element
            Dumper.Write(sep)
            Dump(e)
            sep = ", "
        Next
    End Sub

    Public Sub Dump(ByVal Element As SimpleTypeName)
        Dump(Element.TypeName)
    End Sub

    Public Sub Dump(ByVal Element As ModuleDeclaration)
        If Element.CustomAttributes IsNot Nothing Then Dump(Element.CustomAttributes)
        Dump(Element.Modifiers)
        Dumper.Write("Module " & Element.Name)
        Dumper.WriteLine("")
        Dumper.Indent()
        Dump(Element.Members)
        Dumper.Unindent()
        Dumper.WriteLine("End Module")
    End Sub

    Public Sub Dump(ByVal Element As VariableIdentifier)
        Dump(Element.Identifier)
        If Element.ArrayNameModifier IsNot Nothing Then
            Dump(Element.ArrayNameModifier)
        End If
    End Sub

    Sub Dump(ByVal Element As Token)
        Element.Dump(Dumper)
    End Sub

    Public Sub Dump(ByVal Element As NamedArgument)
        Dumper.Write(Element.Name & " := ")
        Element.Expression.Dump(Dumper)
    End Sub

    Public Sub Dump(ByVal Element As BuiltInTypeName)
        Dumper.Write(Element.TypeName.ToString)
    End Sub

    Public Sub Dump(ByVal Element As LibraryClause)
        Dumper.Write("Lib """ & Element.StringLiteral.LiteralValue.ToString & "")
    End Sub

    Public Sub Dump(ByVal Element As AliasClause)
        Dumper.Write("Alias """ & Element.StringLiteral.LiteralValue.ToString & "")
    End Sub

    Public Sub Dump(ByVal Element As Modifiers)
        If Element.Modifiers Is Nothing Then Return
        For Each ks As KS In Element.Modifiers
            Dumper.Write(ks.ToString & " ")
        Next
    End Sub
    Public Sub Dump(ByVal Element As ParameterIdentifier)
        Dump(Element.Identifier)
        If Element.ArrayNameModifier IsNot Nothing Then Dump(Element.ArrayNameModifier)
    End Sub

    Public Sub Dump(ByVal Element As ArraySizeInitializationModifier)
        Dumper.Write("(")
        If Element.BoundList IsNot Nothing Then
            Dump(Element.BoundList)
        End If
        If Element.ArrayTypeModifiers IsNot Nothing Then
            Dump(Element.ArrayTypeModifiers)
        End If
        Dumper.Write(")")
    End Sub

    Public Sub Dump(ByVal Element As ArrayNameModifier)
        Dump(Element.ArrayModifier)
    End Sub

    Public Sub Dump(ByVal Element As Parameter)
        If Element.Attributes IsNot Nothing Then Dump(Element.Attributes)
        Dump(Element.Modifiers)
        Dumper.Write(Element.Name)
        If Element.TypeName IsNot Nothing Then
            Dumper.Write(" As ")
            Dump(Element.TypeName)
        End If
        If Element.ConstantExpression IsNot Nothing Then
            Dumper.Write(" = ")
            Element.ConstantExpression.Dump(Dumper)
        End If
    End Sub

    ''' <summary>
    ''' Dumps the type name.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Dump(ByVal Element As TypeName)
        If Element.TypeName IsNot Nothing Then
            Dump(Element.TypeName)
        Else
            Dumper.Write(Element.ToString)
        End If
    End Sub

    Public Sub Dump(ByVal Element As ArrayTypeModifiers)
        Dumper.Write(Element.ToString)
    End Sub

    Public Sub Dump(ByVal Element As NonArrayTypeName)
        Dump(Element.TypeName)
    End Sub

    Public Sub Dump(ByVal Element As AttributeBlock)
        Dumper.Write("<")
        Dump(Element.List)
        Dumper.Write(">")
    End Sub
#Else
    Sub Dump(ByVal o As Object)

    End Sub
#End If
End Class
