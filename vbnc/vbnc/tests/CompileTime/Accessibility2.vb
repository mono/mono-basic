Class Accessibility2_Base
    Private PrivateField As Integer
    Friend FriendField As Integer
    Protected ProtectedField As Integer
    Protected Friend ProtectedFriendField As Integer
    Public PublicField As Integer

    Private Sub PrivateSub()

    End Sub
    Friend Sub FriendSub()

    End Sub
    Protected Sub ProtectedSub()

    End Sub
    Protected Friend Sub ProtectedFriendSub()

    End Sub
    Public Sub PublicSub()

    End Sub

    Class Accessibility2_Nested

        'public, friend, protected and private members should be accessible.
        Sub test()
            Dim A As New Accessibility2_Base
            a.privatefield = 0
            a.friendfield = 10
            a.protectedfield = 20
            a.protectedfriendfield = 30
            a.publicfield = 40

            a.privatesub()
            a.friendsub()
            a.protectedsub()
            a.protectedfriendsub()
            a.publicsub()

            MyBase.Finalize()
        End Sub
    End Class
End Class

Class Accessibility2_Derived
    Inherits Accessibility2_Base
    'public, friend and protected members should be accessible.
    Sub test()
        friendfield = 10
        protectedfield = 20
        protectedfriendfield = 30
        publicfield = 40

        friendsub()
        protectedsub()
        protectedfriendsub()
        publicsub()

        MyBase.Finalize()
    End Sub
End Class

Class Accessibility2
    'public and friend members should be accessible.
    Sub Test()
        Dim A As New Accessibility2_Base
        a.friendfield = 10
        a.protectedfriendfield = 30
        a.publicfield = 40

        a.friendsub()
        a.protectedfriendsub()
        a.publicsub()
    End Sub
End Class