Namespace Events7
    Module testmodule3
        Delegate Sub testdelegate()
    End Module

    Module testmodule5
        Delegate Sub testdelegate()
        Private Event a As testdelegate
    End Module
End Namespace
