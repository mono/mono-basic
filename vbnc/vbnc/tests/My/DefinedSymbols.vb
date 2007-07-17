Imports System

Class DefinedSymbols
    Shared Sub Main(ByVal args As String())
        MYTYPE()
        MyApplicationType()
        MYCOMPUTERTYPE()
        MYFORMS()
        MYUSERTYPE()
        MYWEBSERVICES()
    End Sub
    Shared Sub MyApplicationType()
#If _MyApplicationType = "Console" Then
        Console.WriteLine ("_MYAPPLICATIONTYPE=Console")
#End If
#If _MyApplicationType = "CONSOLE" Then
        Console.WriteLine ("_MYAPPLICATIONTYPE=CONSOLE")
#End If
#If _MyApplicationType = "Windows" Then
        Console.WriteLine ("_MYAPPLICATIONTYPE=Windows")
#End If
#If _MyApplicationType = "WINDOWS" Then
        Console.WriteLine ("_MYAPPLICATIONTYPE=WINDOWS")
#End If
#If _MyApplicationType = "WindowsForms" Then
        Console.WriteLine ("_MYAPPLICATIONTYPE=WindowsForms")
#End If
#If _MyApplicationType = "WINDOWSFORMS" Then
        Console.WriteLine ("_MYAPPLICATIONTYPE=WINDOWSFORMS")
#End If
#If _MyApplicationType = "" Then
        Console.WriteLine("_MYAPPLICATIONTYPE=")
#End If
    End Sub
    Shared Sub MYCOMPUTERTYPE()
#If _MYCOMPUTERTYPE = "Web" Then
        Console.WriteLine ("_MYCOMPUTERTYPE=Web")
#End If
#If _MYCOMPUTERTYPE = "WEB" Then
        Console.WriteLine ("_MYCOMPUTERTYPE=WEB")
#End If
#If _MYCOMPUTERTYPE = "Windows" Then
        Console.WriteLine ("_MYCOMPUTERTYPE=Windows")
#End If
#If _MYCOMPUTERTYPE = "WINDOWS" Then
        Console.WriteLine ("_MYCOMPUTERTYPE=WINDOWS")
#End If
#If _MYCOMPUTERTYPE = "" Then
        Console.WriteLine("_MYCOMPUTERTYPE=")
#End If
    End Sub
    Shared Sub MYFORMS()
#If _MYFORMS = True Then
        Console.WriteLine ("_MYFORMS=True")
#End If
#If _MYFORMS = False Then
        Console.WriteLine("_MYFORMS=False")
#End If
#If _MYFORMS = "" Then
        Console.WriteLine("_MYFORMS=")
#End If
    End Sub
    Shared Sub MYUSERTYPE()
#If _MYUSERTYPE = "Web" Then
        Console.WriteLine ("_MYUSERTYPE=Web")   
#End If
#If _MYUSERTYPE = "WEB" Then
        Console.WriteLine ("_MYUSERTYPE=WEB")
#End If
#If _MYUSERTYPE = "Windows" Then
        Console.WriteLine ("_MYUSERTYPE=Windows")
#End If
#If _MYUSERTYPE = "WINDOWS" Then
        Console.WriteLine ("_MYUSERTYPE=WINDOWS")
#End If
#If _MYUSERTYPE = "" Then
        Console.WriteLine("_MYUSERTYPE=")
#End If
    End Sub
    Shared Sub MYWEBSERVICES()
#If _MYWEBSERVICES = True Then
        Console.WriteLine ("_MYWEBSERVICES=True")
#End If
#If _MYWEBSERVICES = False Then
        Console.WriteLine("_MYWEBSERVICES=False")
#End If
#If _MYWEBSERVICES = "" Then
        Console.WriteLine("_MYWEBSERVICES=")
#End If
    End Sub
    Shared Sub MYTYPE()
#If _MYTYPE = "Console" Then
        Console.WriteLine ("_MYTYPE=Console")   
#End If
#If _MYTYPE = "CONSOLE" Then
        Console.WriteLine ("_MYTYPE=CONSOLE")
#End If
#If _MYTYPE = "Windows" Then
        Console.WriteLine ("_MYTYPE=Windows")   
#End If
#If _MYTYPE = "WINDOWS" Then
        Console.WriteLine ("_MYTYPE=WINDOWS")
#End If
#If _MYTYPE = "Custom" Then
        Console.WriteLine ("_MYTYPE=Custom")   
#End If
#If _MYTYPE = "CUSTOM" Then
        Console.WriteLine ("_MYTYPE=CUSTOM")
#End If
#If _MYTYPE = "Web" Then
        Console.WriteLine ("_MYTYPE=Web")   
#End If
#If _MYTYPE = "WEB" Then
        Console.WriteLine ("_MYTYPE=WEB")
#End If
#If _MYTYPE = "Empty" Then
        Console.WriteLine ("_MYTYPE=Empty")
#End If
#If _MYTYPE = "EMPTY" Then
        Console.WriteLine ("_MYTYPE=EMPTY")
#End If
#If _MYTYPE = "WebControl" Then
        Console.WriteLine ("_MYTYPE=WebControl")
#End If
#If _MYTYPE = "WEBCONTROL" Then
        Console.WriteLine ("_MYTYPE=WEBCONTROL")
#End If
#If _MYTYPE = "WindowsForms" Then
        Console.WriteLine("_MYTYPE=WindowsForms")
#End If
#If _MYTYPE = "WINDOWSFORMS" Then
        Console.WriteLine ("_MYTYPE=WINDOWSFORMS")
#End If
#If _MYTYPE = "WindowsFormsWithSubMain" Then
        Console.WriteLine ("_MYTYPE=WindowsFormsWithSubMain")
#End If
#If _MYTYPE = "WINDOWSFORMSWITHSUBMAIN" Then
        Console.WriteLine ("_MYTYPE=WINDOWSFORMSWITHSUBMAIN")
#End If
#If _MYTYPE = "" Then
        Console.WriteLine ("_MYTYPE=")
#End If
    End Sub
End Class