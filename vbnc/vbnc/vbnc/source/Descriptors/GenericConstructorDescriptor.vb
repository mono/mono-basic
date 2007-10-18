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

#If DEBUG Then
#Const DEBUGCONSTRUCTORACCESS = 0
#End If

Public Class GenericConstructorDescriptor
    Inherits ConstructorDescriptor

    Private m_ClosedType As Type
    Private m_OpenConstructorDescriptor As ConstructorDescriptor
    Private m_OpenConstructor As ConstructorInfo
    Private m_ClosedConstructorDescriptor As ConstructorDescriptor
    Private m_ClosedConstructor As ConstructorInfo

    ''' <summary>
    ''' The open type parameters for this method.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TypeParameters As Type()
    ''' <summary>
    ''' The types to close this method with.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TypeArguments As Type()

    Sub New(ByVal Parent As ParsedObject, ByVal OpenConstructor As ConstructorInfo, ByVal TypeParameters() As Type, ByVal TypeArguments() As Type, ByVal ClosedType As Type)
        MyBase.New(Parent)

        m_OpenConstructor = OpenConstructor
        m_OpenConstructorDescriptor = TryCast(m_OpenConstructor, ConstructorDescriptor)
        m_ClosedType = ClosedType
        m_TypeParameters = TypeParameters
        m_TypeArguments = TypeArguments

    End Sub

    Public Overrides Function GetGenericArguments() As System.Type()
        Dim result As Type()
        result = Type.EmptyTypes
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property Attributes() As System.Reflection.MethodAttributes
        Get
            Dim result As MethodAttributes
            result = m_OpenConstructor.Attributes
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides Function GetParameters() As System.Reflection.ParameterInfo()
        Static result As ParameterInfo()

        If result Is Nothing Then
            If m_OpenConstructorDescriptor IsNot Nothing Then
                result = Helper.GetParameters(Compiler, m_OpenConstructorDescriptor)
            Else
                result = Helper.GetParameters(compiler, m_OpenConstructor)
            End If

            result = Helper.ApplyTypeArguments(Me.Parent, result, m_TypeParameters, m_TypeArguments)
        End If

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property DeclaringType() As System.Type
        Get
            Return m_ClosedType
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Return m_OpenConstructor.Name
        End Get
    End Property

    Public Overrides ReadOnly Property IsGenericMethod() As Boolean
        Get
            Dim result As Boolean
            result = True
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Overrides ReadOnly Property ConstructorInReflection() As ConstructorInfo
        Get
            If m_ClosedConstructor Is Nothing Then
                m_ClosedType = Helper.GetTypeOrTypeBuilder(m_ClosedType)
                m_OpenConstructor = Helper.GetCtorOrCtorBuilder(m_OpenConstructor)
                If m_ClosedType.GetType.FullName = "System.RuntimeType" Then
                    Dim paramTypes() As Type
                    paramTypes = Helper.GetTypes(GetParameters)
                    paramTypes = Helper.GetTypeOrTypeBuilders(paramTypes)
                    m_ClosedConstructor = m_ClosedType.GetConstructor(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance, Nothing, CallingConventions.Standard, paramTypes, Nothing)
                Else
                    m_ClosedConstructor = TypeBuilder.GetConstructor(m_ClosedType, m_OpenConstructor)
                End If
                Compiler.TypeManager.RegisterReflectionMember(m_ClosedConstructor, Me)
            End If
            Return m_ClosedConstructor
        End Get
    End Property
End Class
