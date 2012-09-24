Module Module1

    'Tests not included:
    ' Lambda expressions
    ' LINQ expressions

    Sub Main()
    End Sub

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

        End If

        Dim UnusedElseifExpr As Boolean
        Dim UsedElseIfExpr As Boolean
        Dim UsedElseIfExpr2 As Integer

        If False Then
        ElseIf UsedElseIfExpr Then
        ElseIf UsedElseIfExpr2 > 10 Then
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

        Next

    End Sub

    Private Sub ForTest()

        Dim UsedCtrlVar As Integer
        Dim UnusedCtrlVar As Integer

        For UsedCtrlVar = 10 To 20

        Next

        Dim UsedStepVar As Integer
        Dim UsedCtrlVar2 As Integer
        Dim UnusedStepVar As Integer

        For UsedCtrlVar2 = 2 To 20 Step UsedStepVar

        Next

    End Sub

    Private Sub WhileTest()

        Dim UsedWhileLoopVar As Boolean
        Dim UnusedWhileLoopVar As Boolean

        While UsedWhileLoopVar

        End While

    End Sub

    Private Sub DoWhileTest()

        Dim UsedDoWhileVar As Boolean
        Dim UnusedDoWhileVar As Boolean

        Do While UsedDoWhileVar <> False
        Loop

    End Sub

    Private Sub EventTest()

        Dim UsedInEvent As Integer
        Dim UnusedInEvent As Integer

        RaiseEvent DummyEvent(UsedInEvent)

    End Sub

    Private Sub SyncLockTest()

        Dim UsedInSyncLock As Object
        Dim UnusedInSyncLock As Object

        SyncLock UsedInSyncLock

        End SyncLock

    End Sub

    Private Sub TernaryIfTest()

        Dim UsedInTernaryIf As Integer
        Dim NotUsedInTernaryIf As Integer
        Dim Test As Integer = If(UsedInTernaryIf = 42, 1, 0)

    End Sub

End Module