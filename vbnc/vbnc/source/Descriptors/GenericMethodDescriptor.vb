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
#Const DEBUGMETHODACCESS = 0
#Const EXTENDEDDEBUG = 0
#End If

Public Class GenericMethodDescriptor
    Inherits MethodDescriptor

    Private m_ClosedType As Type
    Private m_OpenMethodDescriptor As MethodDescriptor
    Private m_OpenMethod As MethodInfo
    Private m_ClosedMethodDescriptor As MethodDescriptor
    Private m_ClosedMethod As MethodInfo


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

    ''' <summary>
    ''' Creates a closed method on a generic type.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="OpenMethod"></param>
    ''' <param name="TypeParameters"></param>
    ''' <param name="TypeArguments"></param>
    ''' <param name="ClosedType"></param>
    ''' <remarks></remarks>
    Sub New(ByVal Parent As ParsedObject, ByVal OpenMethod As MethodInfo, ByVal TypeParameters As Type(), ByVal TypeArguments() As Type, ByVal ClosedType As Type)
        MyBase.New(Parent)

        m_ClosedType = ClosedType
        m_OpenMethod = OpenMethod
        m_OpenMethodDescriptor = TryCast(m_OpenMethod, MethodDescriptor)

        m_TypeParameters = TypeParameters
        m_TypeArguments = TypeArguments

        Helper.Assert(m_TypeParameters.Length = m_TypeArguments.Length)
#If DEBUG Then
        Try
            Dim tmp As Type() = Nothing
            'Dim t As Type = Me.ReturnType
            If ClosedType.IsGenericTypeDefinition Then
                tmp = ClosedType.GetGenericArguments
            ElseIf ClosedType.IsGenericType Then
                tmp = ClosedType.GetGenericTypeDefinition.GetGenericArguments
            End If
            If tmp IsNot Nothing Then
                Helper.Assert(m_TypeParameters.Length = tmp.Length)
                For i As Integer = 0 To tmp.Length - 1
                    Helper.Assert(tmp(i).Name = m_TypeParameters(i).Name)
                Next
            End If
        Catch ex As Exception
            Helper.Assert(False, ex.Message)
        End Try
#End If
    End Sub

    ''' <summary>
    ''' Creates a closed method of an open generic method.
    ''' </summary>
    ''' <param name="Parent"></param>
    ''' <param name="OpenMethod"></param>
    ''' <param name="TypeParameters"></param>
    ''' <param name="TypeArguments"></param>
    ''' <remarks></remarks>
    Sub New(ByVal Parent As ParsedObject, ByVal OpenMethod As MethodInfo, ByVal TypeParameters As Type(), ByVal TypeArguments() As Type)
        MyBase.New(Parent)

        m_OpenMethod = OpenMethod
        m_OpenMethodDescriptor = TryCast(m_OpenMethod, MethodDescriptor)

        m_TypeParameters = TypeParameters
        m_TypeArguments = TypeArguments

        Helper.Assert(m_TypeParameters.Length = m_TypeArguments.Length)
    End Sub

    Public Overrides ReadOnly Property IsGenericMethod() As Boolean
        Get
            Dim result As Boolean

            result = m_OpenMethod.IsGenericMethod

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides Function GetCustomAttributes(ByVal inherit As Boolean) As Object()
        Return MyBase.GetCustomAttributes(inherit)
    End Function

    Public Overrides Function GetCustomAttributes(ByVal attributeType As System.Type, ByVal inherit As Boolean) As Object()
        Dim result As Object()

        If m_OpenMethodDescriptor IsNot Nothing Then
            result = m_OpenMethodDescriptor.GetCustomAttributes(attributeType, inherit)
        ElseIf m_ClosedMethodDescriptor IsNot Nothing Then
            result = Helper.FilterCustomAttributes(attributeType, inherit, m_ClosedMethodDescriptor.Declaration)
        Else
            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
            result = Nothing
        End If
        DumpMethodInfo(result)

        Return result
    End Function

    Public Overrides Function GetGenericArguments() As System.Type()
        Dim result As Type()

        If m_OpenMethod.IsGenericMethodDefinition Then
            If TypeOf m_OpenMethod Is MethodBuilder = False Then
                result = m_OpenMethod.GetGenericArguments
            Else
                Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
                result = Nothing
            End If
        Else
            result = Type.EmptyTypes
        End If


        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property IsShared() As Boolean
        Get
            Return m_OpenMethod.IsStatic
        End Get
    End Property

    Public Overrides ReadOnly Property Attributes() As System.Reflection.MethodAttributes
        Get
            Dim result As MethodAttributes
            result = m_OpenMethod.Attributes
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Dim result As String
            result = m_OpenMethod.Name
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property ReturnType() As System.Type
        Get
            Static result As Type
            If result Is Nothing Then
                If m_OpenMethodDescriptor IsNot Nothing Then
                    result = m_OpenMethodDescriptor.ReturnType
                Else
                    result = m_OpenMethod.ReturnType
                End If
                result = Helper.ApplyTypeArguments(Me.Parent, result, m_TypeParameters, m_TypeArguments)
            End If
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property DeclaringType() As System.Type
        Get
            Dim result As Type
            If m_ClosedType Is Nothing Then
                result = m_OpenMethod.DeclaringType
            Else
                result = m_ClosedType
            End If
            DumpMethodInfo(result)
            Helper.Assert(result IsNot Nothing)
            Return result
        End Get
    End Property

    Public Overrides Function GetParameters() As System.Reflection.ParameterInfo()
        Static result As ParameterInfo()

        If result Is Nothing Then
            If m_OpenMethodDescriptor IsNot Nothing Then
                result = Helper.GetParameters(Me.Parent.Compiler, m_OpenMethodDescriptor)
            Else
                result = Helper.GetParameters(Me.Parent.Compiler, m_OpenMethod)
            End If

            result = Helper.ApplyTypeArguments(Me.Parent, result, m_TypeParameters, m_TypeArguments)
        End If

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property MethodInReflection() As System.Reflection.MethodInfo
        Get
            If m_ClosedMethod Is Nothing Then
#If EXTENDEDDEBUG Then
                Console.WriteLine("GenericMethodDescriptor.MethodInReflection: " & DeclaringType.FullName & "." & Me.Name)
#End If

                Try
                    If m_ClosedType Is Nothing Then
#If EXTENDEDDEBUG Then
                        Console.WriteLine(">m_ClosedType = Nothing")
#End If
                        'Generic method with arguments.
                        m_OpenMethod = Helper.GetMethodOrMethodBuilder(m_OpenMethod)
                        m_TypeArguments = Helper.GetTypeOrTypeBuilders(m_TypeArguments)
                        m_ClosedMethod = m_OpenMethod.MakeGenericMethod(m_TypeArguments)
#If EXTENDEDDEBUG Then
                        Console.WriteLine(">m_OpenMethod = " & m_OpenMethod.Name)
                        Console.WriteLine(">m_TypeArguments.Count = " & m_TypeArguments.Length.ToString)
                        Console.WriteLine(">m_ClosedMethod = " & m_ClosedMethod.Name)
#End If
                    Else
#If EXTENDEDDEBUG Then
                        Console.WriteLine(">m_ClosedType <> Nothing")
#End If
                        'Method on a generic type.
                        m_ClosedType = Helper.GetTypeOrTypeBuilder(m_ClosedType)
                        m_OpenMethod = Helper.GetMethodOrMethodBuilder(m_OpenMethod)
                        m_TypeArguments = Helper.GetTypeOrTypeBuilders(m_TypeArguments)
#If EXTENDEDDEBUG Then
                        Console.WriteLine(">m_ClosedType = " & m_ClosedType.Name)
                        Console.WriteLine(">m_ClosedType.GetType = " & m_ClosedType.GetType.FullName)
                        Console.WriteLine(">m_OpenMethod = " & m_OpenMethod.Name)
                        Console.WriteLine(">m_OpenMethod.GetType = " & m_OpenMethod.GetType.FullName)
                        Console.WriteLine(">m_TypeArguments.Count = " & m_TypeArguments.Length.ToString)
                        For i As Integer = 0 To m_TypeArguments.Length - 1
                            Console.WriteLine(">Arg#" & i.ToString & ".Fullname = " & m_TypeArguments(i).FullName & "; .GetType.Fullname = " & m_TypeArguments(i).GetType.FullName)
                        Next
#End If

                        Dim paramTypes As Type()
                        paramTypes = Helper.GetTypes(GetParameters())
                        paramTypes = Helper.GetTypeOrTypeBuilders(paramTypes)

                        Dim isGetMethod As Boolean = False
                        Dim isMakeGenericMethod As Boolean = False
                        Dim isTypeBuilderGetMethod As Boolean = False
                        Dim isOpenMethod As Boolean = False
                        Dim isGetMethodDefinitionAndThenTypeBuilder As Boolean = False

                        Dim isMethodOnTypeBuilderInstantiation As Boolean = m_OpenMethod.GetType.Name.Contains("MethodOnTypeBuilderInst")
                        Dim isMethodBuilder As Boolean = TypeOf m_OpenMethod Is MethodBuilder
                        Dim isMethodBuilderOrInstantiation As Boolean = isMethodBuilder Or isMethodOnTypeBuilderInstantiation
                        Dim isTypeInstantiation As Boolean = m_ClosedType.GetType.Name = "TypeBuilderInstantiation" OrElse m_ClosedType.GetType.Name = "MonoGenericClass"
                        Dim isTypeBuilder As Boolean = TypeOf m_ClosedType Is TypeBuilder
                        Dim isTypeBuilderOrInstantiation As Boolean = isTypeBuilder Or isTypeInstantiation
                        Dim methodHasGenericParameters As Boolean = isMethodBuilder = False AndAlso (isMethodOnTypeBuilderInstantiation = False OrElse m_OpenMethod.GetGenericMethodDefinition.GetType.Name <> "MethodBuilder") AndAlso m_OpenMethod.ContainsGenericParameters
                        Dim methodIsGeneric As Boolean = m_OpenMethod.IsGenericMethod
                        Dim methodIsGenericDefinition As Boolean = m_OpenMethod.IsGenericMethodDefinition
                        Dim methodDeclaringTypeIsGenericDefinition As Boolean = m_OpenMethod.DeclaringType.IsGenericTypeDefinition
                        Dim typeHasGenericParameters As Boolean = m_ClosedType.ContainsGenericParameters
                        Dim typeIsGenericDefinition As Boolean = m_ClosedType.IsGenericTypeDefinition
                        Dim typeIsGenericType As Boolean = m_ClosedType.IsGenericType

#If EXTENDEDDEBUG Then
                        Console.WriteLine(">isMethodOnTypeBuilderInstantiation:     " & isMethodOnTypeBuilderInstantiation)
                        Console.WriteLine(">isMethodBuilder:                        " & isMethodBuilder)
                        Console.WriteLine(">isMethodBuilderOrInstantiation:         " & isMethodBuilderOrInstantiation)
                        Console.WriteLine(">isTypeInstantiation:                    " & isTypeInstantiation)
                        Console.WriteLine(">isTypeBuilder:                          " & isTypeBuilder)
                        Console.WriteLine(">isTypeBuilderOrInstantiation:           " & isTypeBuilderOrInstantiation)
                        Console.WriteLine(">methodHasGenericParameters:             " & methodHasGenericParameters)
                        Console.WriteLine(">methodIsGeneric:                        " & methodIsGeneric)
                        Console.WriteLine(">methodIsGenericDefinition:              " & methodIsGenericDefinition)
                        Console.WriteLine(">methodDeclaringTypeIsGenericDefinition: " & methodDeclaringTypeIsGenericDefinition)
                        Console.WriteLine(">typeHasGenericParameters:               " & typeHasGenericParameters)
                        Console.WriteLine(">typeIsGenericDefinition:                " & typeIsGenericDefinition)
                        Console.WriteLine(">typeIsGenericType:                      " & typeIsGenericType)
#End If

                        If isTypeInstantiation AndAlso typeIsGenericType AndAlso typeIsGenericDefinition = False AndAlso isTypeBuilderOrInstantiation AndAlso methodIsGeneric = False AndAlso methodIsGenericDefinition = False AndAlso methodHasGenericParameters = False AndAlso isMethodOnTypeBuilderInstantiation = False Then
#If EXTENDEDDEBUG Then
                            Console.WriteLine(">#1")
#End If
                            isTypeBuilderGetMethod = True
                        ElseIf isTypeBuilderOrInstantiation = False Then
#If EXTENDEDDEBUG Then
                            Console.WriteLine(">#2")
#End If
                            isGetMethod = True
                        ElseIf methodIsGenericDefinition Then
#If EXTENDEDDEBUG Then
                            Console.WriteLine(">#3")
#End If
                            isMakeGenericMethod = True
                        ElseIf methodIsGeneric Then
#If EXTENDEDDEBUG Then
                            Console.WriteLine(">#4")
#End If
                            isGetMethod = True
                        ElseIf methodHasGenericParameters AndAlso (typeIsGenericDefinition OrElse (isTypeInstantiation And isMethodOnTypeBuilderInstantiation = False)) Then
#If EXTENDEDDEBUG Then
                            Console.WriteLine(">#5")
#End If
                            isTypeBuilderGetMethod = True
                        ElseIf Helper.CompareType(m_OpenMethod.DeclaringType, m_ClosedType) = False AndAlso methodHasGenericParameters = True Then
#If EXTENDEDDEBUG Then
                            Console.WriteLine(">#6")
#End If
                            isGetMethodDefinitionAndThenTypeBuilder = True
                        ElseIf Helper.CompareType(m_OpenMethod.DeclaringType, m_ClosedType) = False AndAlso methodIsGenericDefinition = False Then
#If EXTENDEDDEBUG Then
                            Console.WriteLine(">#7")
#End If
                            isGetMethodDefinitionAndThenTypeBuilder = True
                        Else
#If EXTENDEDDEBUG Then
                            Console.WriteLine(">#8")
#End If
                            isOpenMethod = True
                        End If

                        Helper.Assert(isGetMethod Xor isMakeGenericMethod Xor isTypeBuilderGetMethod Xor isOpenMethod Xor isGetMethodDefinitionAndThenTypeBuilder)

                        If isGetMethod Then
#If EXTENDEDDEBUG Then
                            Console.WriteLine(">isGetMethod")
#End If
                            m_ClosedMethod = m_ClosedType.GetMethod(m_OpenMethod.Name, BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance, Nothing, CallingConventions.Standard, paramTypes, Nothing)
                        ElseIf isOpenMethod Then
#If EXTENDEDDEBUG Then
                            Console.WriteLine(">isOpenMethod")
#End If
                            m_ClosedMethod = m_OpenMethod
                        ElseIf isMakeGenericMethod Then
#If EXTENDEDDEBUG Then
                            Console.WriteLine(">isMakeGenericMethod")
#End If
                            m_ClosedMethod = m_OpenMethod.MakeGenericMethod(m_TypeArguments)
                        ElseIf isTypeBuilderGetMethod Then
#If EXTENDEDDEBUG Then
                            Console.WriteLine(">isTypeBuilderGetMethod")
#End If
                            m_ClosedMethod = TypeBuilder.GetMethod(m_ClosedType, m_OpenMethod)
                        ElseIf isGetMethodDefinitionAndThenTypeBuilder Then
#If EXTENDEDDEBUG Then
                            Console.WriteLine(">isGetMethodDefinitionAndThenTypeBuilder")
#End If
                            m_ClosedMethod = TypeBuilder.GetMethod(m_ClosedType, m_OpenMethod.GetGenericMethodDefinition)
                        Else
                            Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
                        End If

                    End If
                    Compiler.TypeManager.RegisterReflectionMember(m_ClosedMethod, Me)
                Catch ex As Exception
                    Parent.Compiler.ShowExceptionInfo(ex)
                    Helper.StopIfDebugging()
                    Throw
                End Try
            End If

            Helper.Assert(m_ClosedMethod IsNot Nothing)

            Return m_ClosedMethod
        End Get
    End Property

End Class
