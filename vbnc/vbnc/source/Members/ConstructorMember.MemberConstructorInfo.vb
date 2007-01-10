' 
' Visual Basic.Net COmpiler
' Copyright (C) 2004 - 2006 Rolf Bjarne Kvinge, rbjarnek at users.sourceforge.net
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

Imports System.Reflection

Partial Public Class ConstructorMember
    Public Class MemberConstructorInfo
        Inherits ConstructorInfo
        Private m_Constructor As ConstructorMember

        ''' <summary>
        ''' Returns the MemberConstructor which is holding the info for this class
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Friend ReadOnly Property MemberConstructor() As ConstructorMember
            Get
                Return m_Constructor
            End Get
        End Property

        Shared Operator =(ByVal this As MemberConstructorInfo, ByVal other As ConstructorInfo) As Boolean
            If this Is Nothing Then Throw New ArgumentNullException("this")
            If other Is Nothing Then Throw New ArgumentNullException("other")
            Return this.MemberConstructor.ConstructorBuilder Is other
        End Operator

        Shared Operator <>(ByVal this As MemberConstructorInfo, ByVal other As ConstructorInfo) As Boolean
            Return Not this = other
        End Operator

        Overrides Function Equals(ByVal obj As Object) As Boolean
            If TypeOf obj Is MemberConstructorInfo Then
                Return Equals(DirectCast(obj, MemberConstructorInfo))
            ElseIf TypeOf obj Is ConstructorInfo Then
                Return Equals(DirectCast(obj, ConstructorInfo))
            Else
                Return False
            End If
        End Function

        Overloads Function Equals(ByVal obj As MemberConstructorInfo) As Boolean
            Return Me Is obj
        End Function

        Overloads Function Equals(ByVal obj As ConstructorInfo) As Boolean
            Return Me.MemberConstructor.ConstructorBuilder Is obj
        End Function

        ReadOnly Property HasOnlyOptionalParameters() As Boolean
            Get
                Helper.NotImplemented()
                'For Each p As MemberParameter In m_Constructor.Parameters
                '    If p.Modifiers.Contains(KS.Optional) = False Then
                '        Return False
                '    End If
                'Next
                Return True
            End Get
        End Property

        ReadOnly Property HasParameters() As Boolean
            Get
                Return m_Constructor.Signature.Parameters.Count > 0
            End Get
        End Property

        Sub New(ByVal Constructor As ConstructorMember)
            m_Constructor = Constructor
        End Sub

        Public Overrides ReadOnly Property Attributes() As System.Reflection.MethodAttributes
            Get
                Helper.NotImplemented()
                'If m_Constructor.ConstructorBuilder IsNot Nothing Then
                '    Return m_Constructor.ConstructorBuilder.Attributes Or Reflection.MethodAttributes.RTSpecialName
                'Else
                '    Return m_Constructor.getMethodAttributes() Or Reflection.MethodAttributes.RTSpecialName
                'End If
            End Get
        End Property

        Public Overrides ReadOnly Property DeclaringType() As System.Type
            Get
                Return m_Constructor.ConstructorBuilder.DeclaringType
            End Get
        End Property

        Public Overloads Overrides Function GetCustomAttributes(ByVal inherit As Boolean) As Object()
            Throw New NotImplementedException
        End Function

        Public Overloads Overrides Function GetCustomAttributes(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Object()
            Throw New NotImplementedException
        End Function

        Public Overrides Function GetMethodImplementationFlags() As System.Reflection.MethodImplAttributes
            Throw New NotImplementedException
        End Function

        Public Overrides Function GetParameters() As System.Reflection.ParameterInfo()
            Helper.NotImplemented() : Return Nothing
            'Dim result() As ParameterInfo
            'If m_Constructor.Parameters.Count = 0 Then
            '    Return New ParameterInfo() {}
            'Else
            '    ReDim result(m_Constructor.Parameters.Count - 1)
            '    For i As Integer = 0 To m_Constructor.Parameters.Count - 1
            '        result(i) = DirectCast(m_Constructor.Parameters.Item(i), MemberParameter).GetInfo
            '    Next
            '    Return result
            'End If

        End Function

        Public Overloads Overrides Function Invoke(ByVal obj As Object, ByVal invokeAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal parameters() As Object, ByVal culture As System.Globalization.CultureInfo) As Object
            Throw New NotImplementedException
        End Function

        Public Overloads Overrides Function Invoke(ByVal invokeAttr As System.Reflection.BindingFlags, ByVal binder As System.Reflection.Binder, ByVal parameters() As Object, ByVal culture As System.Globalization.CultureInfo) As Object
            Throw New NotImplementedException
        End Function

        Public Overrides Function IsDefined(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Boolean
            Throw New NotImplementedException
        End Function

        Public Overrides ReadOnly Property MethodHandle() As System.RuntimeMethodHandle
            Get
                Throw New NotImplementedException
            End Get
        End Property

        Public Overrides ReadOnly Property Name() As String
            Get
                Return m_Constructor.ConstructorBuilder.Name
            End Get
        End Property

        Public Overrides ReadOnly Property ReflectedType() As System.Type
            Get
                Return m_Constructor.ConstructorBuilder.ReflectedType
            End Get
        End Property
    End Class
End Class
