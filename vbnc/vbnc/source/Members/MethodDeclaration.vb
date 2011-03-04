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

Public MustInherit Class MethodDeclaration
    Inherits MethodBaseDeclaration

    Protected Sub New(ByVal Parent As TypeDeclaration)
        MyBase.new(Parent)
    End Sub

    Protected Sub New(ByVal Parent As PropertyDeclaration)
        MyBase.new(Parent)
    End Sub

    Protected Sub New(ByVal Parent As EventDeclaration)
        MyBase.new(Parent)
    End Sub

    Public Overrides Function ResolveTypeReferences() As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveTypeReferences AndAlso result

        Return result
    End Function

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Signature As SubSignature)
        MyBase.Init(Modifiers, Signature)
    End Sub

    Shadows Sub Init(ByVal Modifiers As Modifiers, ByVal Signature As SubSignature, ByVal Code As CodeBlock)
        MyBase.Init(Modifiers, Signature, Code)
    End Sub

    Public Overrides Function ResolveMember(ByVal Info As ResolveInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.ResolveMember(Info) AndAlso result

        Return result
    End Function

    Public Overrides Function CreateDefinition() As Boolean
        Dim result As Boolean = True

        result = MyBase.CreateDefinition AndAlso result

        Return result
    End Function

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        result = MyBase.GenerateCode(Info) AndAlso result

        If Signature.Parameters IsNot Nothing Then
            For i As Integer = 0 To Signature.Parameters.Count - 1
                result = Signature.Parameters(i).GenerateCode(Info) AndAlso result
            Next
        End If

        Return result
    End Function
End Class
