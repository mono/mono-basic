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

''' <summary>
''' TypeParameterList  ::= 	TypeParameter  | TypeParameterList  ","  TypeParameter
''' CHANGED: Switched name of TypeParameters and TypeParameterList
''' </summary>
''' <remarks></remarks>
Public Class TypeParameterList
    Inherits NamedBaseList(Of TypeParameter)

    Private m_GenericParameters As GenericTypeParameterBuilder()

    Sub New(ByVal Parent As ParsedObject)
        MyBase.New(Parent)
    End Sub

    Function AsTypeArray() As Type()
        Dim result(Me.Count - 1) As Type

        For i As Integer = 0 To Me.Count - 1
            result(i) = Item(i).TypeDescriptor
        Next

        Return result
    End Function

    Function Clone(Optional ByVal NewParent As ParsedObject = Nothing) As TypeParameterList
        If NewParent Is Nothing Then NewParent = Me.Parent
        Dim result As New TypeParameterList(NewParent)
        For Each item As TypeParameter In Me
            result.Add(item.clone(result))
        Next
        Return result
    End Function

    Function DefineGenericParameters(ByVal TypeBuilder As TypeBuilder) As Boolean
        Dim result As Boolean = True

        If Me.Count = 0 Then Return result

        m_GenericParameters = TypeBuilder.DefineGenericParameters(Helper.GetNames(Me))
#If DEBUGREFLECTION Then
        Dim arr As String() = New String() {"2"}
        Helper.DebugReflection_AppendLine(String.Format("{0} = new String () {{{1}}}", Helper.GetObjectName(arr), """" & Helper.GetNames(Me)(0) & """"))
        Helper.DebugReflection_AppendLine(String.Format("{0} = {1}.DefineGenericParameters({2})", Helper.GetObjectName(m_GenericParameters), Helper.GetObjectName(TypeBuilder), Helper.GetObjectName(arr)))
        For i As Integer = 0 To m_GenericParameters.Length - 1
            Helper.DebugReflection_AppendLine(String.Format("{0} = {1}({2})", Helper.GetObjectName(m_GenericParameters(i)), Helper.GetObjectName(m_GenericParameters), i))
        Next
#End If
        result = DefineGenericParameters(m_GenericParameters) AndAlso result

        Return result
    End Function

    Function DefineGenericParameters(ByVal MethodBuilder As MethodBuilder) As Boolean
        Dim result As Boolean = True

        If Me.Count = 0 Then Return result

        m_GenericParameters = MethodBuilder.DefineGenericParameters(Helper.GetNames(Me))
#If DEBUGREFLECTION Then
        Dim arr As String() = New String() {"2"}
        Helper.DebugReflection_AppendLine(String.Format("{0} = new String () {{{1}}}", Helper.GetObjectName(arr), """" & Helper.GetNames(Me)(0) & """"))
        Helper.DebugReflection_AppendLine(String.Format("{0} = {1}.DefineGenericParameters({2})", Helper.GetObjectName(m_GenericParameters), Helper.GetObjectName(MethodBuilder), Helper.GetObjectName(arr)))
        For i As Integer = 0 To m_GenericParameters.Length - 1
            Helper.DebugReflection_AppendLine(String.Format("{0} = {1}({2})", Helper.GetObjectName(m_GenericParameters(i)), Helper.GetObjectName(m_GenericParameters), i))
        Next
#End If

        result = DefineGenericParameters(m_GenericParameters) AndAlso result

        Return result
    End Function

    Function DefineGenericParameters(ByVal Parameters() As GenericTypeParameterBuilder) As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To Count - 1
            Dim parameter As TypeParameter = Item(i)
            result = parameter.DefineParameterConstraints(Parameters(i)) AndAlso result
            'Compiler.TypeManager.RegisterReflectionType(Parameters(i), parameter.TypeDescriptor)
        Next

        Return result
    End Function
End Class
