Imports System.Windows.Forms

Namespace DefaultInstance1
    Public Class Tester
        Shared Passes As Integer
        Shared Failures As Integer
        Shared Result As Integer

        Public Shared Function Main() As Integer
            Dim o As Object
            Dim created As Integer
            Dim disposed As Integer

            Check(CollectableBase.CreatedCount = created, "expected created = " & created)
            Check(CollectableBase.ClosedCount = disposed, "expected disposed = " & disposed)

            o = Collectable1
            created += 1
            Check(CollectableBase.CreatedCount = created, "expected created = " & created)
            Check(CollectableBase.ClosedCount = disposed, "expected disposed = " & disposed)

            Collectable1 = Nothing
            disposed += 1
            check(collectablebase.createdcount = created, "expected created = " & created)
            check(collectablebase.closedcount = disposed, "expected disposed = " & disposed)

            collectable1.value = 1
            created += 1
            check(collectablebase.createdcount = created, "expected created = " & created)
            check(collectablebase.closedcount = disposed, "expected disposed = " & disposed)

            collectable1.show()
            check(collectablebase.createdcount = created, "expected created = " & created)
            check(collectablebase.closedcount = disposed, "expected disposed = " & disposed)

            o = Collectable1.Value
            Check(CollectableBase.CreatedCount = created, "expected created = " & created)
            Check(CollectableBase.ClosedCount = disposed, "expected disposed = " & disposed)

            Collectable1 = Nothing
            disposed += 1
            Check(CollectableBase.CreatedCount = created, "expected created = " & created)
            Check(CollectableBase.ClosedCount = disposed, "expected disposed = " & disposed)

            o = Collectable1.Value
            created += 1
            Check(CollectableBase.CreatedCount = created, "expected created = " & created)
            Check(CollectableBase.ClosedCount = disposed, "expected disposed = " & disposed)

            Collectable1 = Nothing
            disposed += 1
            Check(CollectableBase.CreatedCount = created, "expected created = " & created)
            Check(CollectableBase.ClosedCount = disposed, "expected disposed = " & disposed)

            o = Collectable1 Is Nothing
            Check(CollectableBase.CreatedCount = created, "expected created = " & created)
            Check(CollectableBase.ClosedCount = disposed, "expected disposed = " & disposed)

            Collectable1.InstanceSub()
            created += 1
            Check(CollectableBase.CreatedCount = created, "expected created = " & created)
            Check(CollectableBase.ClosedCount = disposed, "expected disposed = " & disposed)
            Check(Collectable1.InstanceSubCounter = 1, "expected instancesubcounter = " & 1)

            o = Collectable1.ReadOnlyField
            o = Collectable1.InstanceProperty
            M(Collectable1.Value, Collectable1.ReadOnlyField, Collectable1.InstanceProperty)
            Check(CollectableBase.CreatedCount = created, "expected created = " & created)
            Check(CollectableBase.ClosedCount = disposed, "expected disposed = " & disposed)

            Collectable1 = Nothing
            disposed += 1
            Collectable1.SharedSub()
            o = Collectable1.SharedField
            Collectable1.SharedField = o
            o = Collectable1.SharedProperty
            o = Collectable1.NestedEnum.v
            M(Collectable1.SharedField, Collectable1.SharedProperty)
            Check(CollectableBase.CreatedCount = created, "expected created = " & created)
            Check(CollectableBase.ClosedCount = disposed, "expected disposed = " & disposed)
            Check(Collectable1.SharedSubCounter = 1, "expected sharedsubcounter = " & 1)

            Check(CollectableBase.CreatedCount = created, "expected created = " & created)
            Check(CollectableBase.ClosedCount = disposed, "expected disposed = " & disposed)
            Check(Collectable1.SharedSubCounter = 1, "expected sharedsubcounter = " & 1)

            DefaultInstance1.Collectable1 = Nothing
            Check(CollectableBase.CreatedCount = created, "expected created = " & created)
            Check(CollectableBase.ClosedCount = disposed, "expected disposed = " & disposed)

            o = DefaultInstance1.Collectable1.Value
            o = DefaultInstance1.Collectable1.ReadOnlyField
            o = DefaultInstance1.Collectable1.InstanceProperty
            DefaultInstance1.Collectable1.InstanceSub()
            created += 1
            Check(CollectableBase.CreatedCount = created, "expected created = " & created)
            Check(CollectableBase.ClosedCount = disposed, "expected disposed = " & disposed)

            Console.WriteLine("Passed: {0} Failed: {1}", Passes, Failures)

            Return Result
        End Function

        Shared Sub M(ByVal a As Object, Optional ByVal b As Object = Nothing, Optional ByVal c As Object = Nothing, Optional ByVal d As Object = Nothing, Optional ByVal e As Object = Nothing)

        End Sub


        Shared Sub Check(ByVal value As Boolean, ByVal msg As String)
            If value = False Then
                Console.WriteLine("Failed: {0}", msg)
                Failures += 1
                Result = 1
            Else
                Console.WriteLine("Passed: {0}", msg)
                Passes += 1
            End If
        End Sub
    End Class

    Class CollectableBase
        Inherits System.Windows.Forms.Form

        Public Shared ClosedCount As Integer
        Public Shared CreatedCount As Integer

        Public Sub New()
            Console.WriteLine("Created")
            CreatedCount += 1
        End Sub

        Protected Overrides Sub Dispose(ByVal b As Boolean)
            Console.WriteLine("Closed")
            ClosedCount += 1
            MyBase.Dispose(b)
        End Sub
    End Class

    Class Collectable1
        Inherits CollectableBase

        Public Shared InstanceSubCounter As Integer
        Public Shared SharedSubCounter As Integer
        Public Shared InstancePropertyCounter As Integer
        Public Shared SharedPropertyCounter As Integer


        Public Value As Object
        Public ReadOnly ReadOnlyField As Object
        Public Shared SharedField As Object
        Public Sub InstanceSub()
            InstanceSubCounter += 1
        End Sub
        Public Shared Sub SharedSub()
            SharedSubCounter += 1
        End Sub
        Public ReadOnly Property InstanceProperty() As Object
            Get
                InstancePropertyCounter += 1
                Return Nothing
            End Get
        End Property
        Public Shared ReadOnly Property SharedProperty() As Object
            Get
                SharedPropertyCounter += 1
                Return Nothing
            End Get
        End Property

        Public Enum NestedEnum
            v
        End Enum


    End Class
End Namespace