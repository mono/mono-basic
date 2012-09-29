Option Strict Off
Module Module1

    'Tests not included:
    ' Lambda expressions
    ' LINQ expressions

    Private Event DummyEvent(ByVal a As Integer)

    Private Sub AssignmentTest()

        Dim Used As Integer = 23
        Dim UsedAsRhs As Integer
        Dim Assigned As Integer = UsedAsRhs
        Dim NotAssigned As Integer

    End Sub

    Private Sub Dummy(ByVal a As Integer)
        'Nothing
    End Sub

    Private Sub FunctionCallTest()

        Dim UsedAsParam As Integer
        Dim UnusedAsParam As Integer

        Dummy(UsedAsParam)

    End Sub

    Private Sub ArithExpressionTest()

        Dim Lhs As Integer
        Dim Rhs As Integer
        Dim UnusedInArithExpr As Integer

        Dim Result As Integer = Lhs * Rhs

    End Sub

    Private Sub IfTest()

        Dim CondExpr As Boolean
        Dim UnusedCondExpr As Boolean

        If CondExpr Then
            Dim inBlock As Boolean
        End If

        Dim UnusedElseifExpr As Boolean
        Dim UsedElseIfExpr As Boolean
        Dim UsedElseIfExpr2 As Integer

        If False Then
            Dim inBlock As Boolean
        ElseIf UsedElseIfExpr Then
            Dim inBlock As Boolean
        ElseIf UsedElseIfExpr2 > 10 Then
            Dim inBlock As Boolean
        End If

    End Sub

    Private Sub SelectCaseTest()

        Dim SelectExpr As Integer
        Dim UnusedSelectExpr As Integer

        Dim CaseExpr As Integer
        Dim UnusedCaseExpr As Integer

        Select Case SelectExpr

            Case CaseExpr
                Dim Temp As Integer = 1

        End Select

    End Sub

    Private Sub ForEachTest()

        Dim UsedItr As Integer
        Dim UnusedItr As Integer

        Dim UsedList As New System.Collections.Generic.LIst(Of Integer)
        Dim UnusedList As New System.Collections.Generic.List(Of Integer)

        For Each UsedItr In UsedLIst
            Dim inBlock As Boolean

        Next

    End Sub

    Private Sub ForTest()

        Dim UsedCtrlVar As Integer
        Dim UnusedCtrlVar As Integer

        For UsedCtrlVar = 10 To 20
            Dim inBlock As Boolean

        Next

        Dim UsedStepVar As Integer
        Dim UsedCtrlVar2 As Integer
        Dim UnusedStepVar As Integer

        For UsedCtrlVar2 = 2 To 20 Step UsedStepVar
            Dim inBlock As Boolean

        Next

    End Sub

    Private Sub WhileTest()

        Dim UsedWhileLoopVar As Boolean
        Dim UnusedWhileLoopVar As Boolean

        While UsedWhileLoopVar
            Dim inBlock As Boolean

        End While

    End Sub

    Private Sub DoWhileTest()

        Dim UsedDoWhileVar As Boolean
        Dim UnusedDoWhileVar As Boolean

        Do While UsedDoWhileVar <> False
            Dim inBlock As Boolean
        Loop

    End Sub

    Private Sub EventTest()

        Dim UsedInEvent As Integer
        Dim UnusedInEvent As Integer

        RaiseEvent DummyEvent(UsedInEvent)

    End Sub

    Private Sub TernaryIfTest()

        Dim UsedInTernaryIf As Integer
        Dim NotUsedInTernaryIf As Integer
        Dim Test As Integer = If(UsedInTernaryIf = 42, 1, 0)

    End Sub

End Module