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
#Const EXTENDEDDEBUG = 0
#Const DEBUGTYPEACCESS = 0
#End If

Public Class GenericTypeDescriptor
    Inherits TypeDescriptor

    Private m_OpenTypeDescriptor As TypeDescriptor
    Private m_OpenType As Type

    ''' <summary>
    ''' The type parameters of the open type.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TypeParameters() As Type

    ''' <summary>
    ''' The specified type arguments to close the type with.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_TypeArguments() As Type

    ''' <summary>
    ''' The final closed type.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_ClosedType As Type

    Private m_AllMembers As Generic.List(Of MemberInfo)
    Private m_AllDeclaredMembers As Generic.List(Of MemberInfo)
    Private m_FullName As String
    Private m_Name As String

    Sub New(ByVal Parent As ParsedObject, ByVal OpenType As Type, ByVal TypeParameters() As Type, ByVal TypeArguments() As Type)
        MyBase.new(Parent)

        m_TypeArguments = TypeArguments
        m_TypeParameters = TypeParameters
        m_OpenType = OpenType
        m_OpenTypeDescriptor = TryCast(m_OpenType, TypeDescriptor)

        'Helper.StopIfDebugging(m_OpenTypeDescriptor Is Nothing AndAlso Helper.IsReflectionType(OpenType))

        Helper.Assert(m_OpenType IsNot Nothing)
        Helper.Assert(m_TypeArguments IsNot Nothing AndAlso m_TypeArguments.Length > 0)
        Helper.Assert(m_TypeArguments(0) IsNot Nothing)
        Helper.AssertNotNothing(m_TypeArguments)
        Helper.AssertNotNothing(m_TypeParameters)
        Helper.Assert(m_TypeArguments.Length = m_TypeParameters.Length)
    End Sub

    Public Overrides ReadOnly Property Assembly() As System.Reflection.Assembly
        Get
            Return m_OpenType.Assembly
        End Get
    End Property

    Public Overrides Function GetInterfaces() As System.Type()
        Static result As Generic.List(Of Type)
        Dim tmp As Type()

        If result Is Nothing Then
            result = New Generic.List(Of Type)
            tmp = Compiler.TypeManager.GetRegisteredType(m_OpenType).GetInterfaces()
            If tmp IsNot Nothing Then result.AddRange(tmp)

            For i As Integer = 0 To result.Count - 1
                If result(i).IsGenericType Then
                    result(i) = Helper.ApplyTypeArguments(Me.Parent, result(i), m_TypeParameters, m_TypeArguments)
                    'result(i) = Compiler.TypeManager.MakeGenericType(Me.Parent, result(i), m_TypeArguments)
                ElseIf result(i).IsGenericTypeDefinition Then
                    Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
                ElseIf result(i).IsGenericParameter Then
                    Compiler.Report.ShowMessage(Messages.VBNC99997, Declaration.Location)
                End If
            Next

            If Me.BaseType IsNot Nothing Then result.AddRange(Compiler.TypeManager.GetRegisteredType(Me.BaseType).GetInterfaces)
        End If

        DumpMethodInfo(result.ToArray)

        Return result.ToArray
    End Function

    Public Overrides ReadOnly Property DeclaringType() As System.Type
        Get
            Dim result As Type
            result = m_OpenType.DeclaringType
            result = Helper.ApplyTypeArguments(Me.Parent, result, m_TypeParameters, m_TypeArguments)
            Return result
        End Get
    End Property

    ''' <summary>
    ''' Gets the Reflection.Emit created type for this descriptor.
    ''' It is a TypeParameterBuilder
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overrides ReadOnly Property TypeInReflection() As Type
        Get
            If m_ClosedType Is Nothing Then
#If EXTENDEDDEBUG Then
                Compiler.Report.WriteLine("GenericTypeDescriptor.TypeInReflection: " & Me.FullName)
#End If
                Try
                    m_OpenType = Helper.GetTypeOrTypeBuilder(m_OpenType)

                    Helper.Assert(m_OpenType IsNot Nothing)
                    Helper.Assert(Helper.GetTypeOrTypeBuilders(m_TypeArguments, False)(0) IsNot Nothing)
                    m_TypeArguments = Helper.GetTypeOrTypeBuilders(m_TypeArguments, False)

#If EXTENDEDDEBUG Then
                    Compiler.Report.WriteLine(">GenericTypeDescriptor (m_OpenType.FullName): " & m_OpenType.FullName)
                    Compiler.Report.WriteLine(">GenericTypeDescriptor (m_OpenType.GetType.Name): " & m_OpenType.GetType.FullName)
                    Compiler.Report.WriteLine(">GenericTypeDescriptor (m_OpenType.IsGenericTypeDefinition): " & m_OpenType.IsGenericTypeDefinition.ToString)
                    Compiler.Report.WriteLine(">TypeArguments.Count: " & m_TypeArguments.Length.ToString)
                    For i As Integer = 0 To m_TypeArguments.Length - 1
                            Compiler.Report.WriteLine(">>#" & i.ToString & " Fullname = " & m_TypeArguments(i).FullName & " GetType.Fullname = " & m_TypeArguments(i).GetType.FullName)
                    Next
#End If
                    If m_OpenType.GetType.Name = "TypeBuilderInstantiation" Then
                        m_OpenType = m_OpenType.GetGenericTypeDefinition
                    ElseIf m_OpenType.IsGenericTypeDefinition = False Then
                        m_OpenType = m_OpenType.GetGenericTypeDefinition
                    End If
                    m_ClosedType = m_OpenType.MakeGenericType(m_TypeArguments)
#If DEBUGREFLECTION Then
                    Helper.DebugReflection_AppendLine(String.Format("ReDim {0}({1})", Helper.GetObjectName(m_TypeArguments), m_TypeArguments.Length - 1))
                    For i As Integer = 0 To m_TypeArguments.Length - 1
                        Helper.DebugReflection_AppendLine(String.Format("{0}({2}) = {1}", Helper.GetObjectName(m_TypeArguments), Helper.GetObjectName(m_TypeArguments(i)), i))
                    Next
                    Helper.DebugReflection_AppendLine(String.Format("{0} = {1}.MakeGenericType({2})", Helper.GetObjectName(m_ClosedType), Helper.GetObjectName(m_OpenType), Helper.GetObjectName(m_TypeArguments)))
#End If

                    Compiler.TypeManager.RegisterReflectionType(m_ClosedType, Me)
                Catch ex As Exception
                    Parent.Compiler.ShowExceptionInfo(ex)
                    Helper.StopIfDebugging()
                    Throw
                End Try
            End If
            Return m_ClosedType
        End Get
    End Property

    Public Overrides ReadOnly Property [Namespace]() As String
        Get
            Return m_OpenType.Namespace
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="c"></param>
    ''' <returns>Return Value
    ''' true if the c parameter and the current Type represent the same type, or if the current Type is in 
    ''' the inheritance hierarchy of c, or if the current Type is an interface that c supports. false if 
    ''' none of  these conditions are the case, or if c is a null reference (Nothing in Visual Basic).
    ''' </returns>
    ''' <remarks></remarks>
    Public Overrides Function IsAssignableFrom(ByVal c As System.Type) As Boolean
        Dim result As Boolean = False

        Helper.Assert(m_OpenType IsNot Nothing)

        If c Is Nothing Then
            result = False
        Else
            Dim base As Type = c
            Do Until base Is Nothing
                If Helper.CompareType(Me, base) Then
                    result = True : Exit Do
                End If
                base = base.BaseType
            Loop
        End If

        DumpMethodInfo(result)
        Return result
    End Function

    Protected Overrides ReadOnly Property AllDeclaredMembers() As System.Collections.Generic.List(Of System.Reflection.MemberInfo)
        Get
            If m_AllDeclaredMembers Is Nothing Then
                Helper.Assert(m_OpenType IsNot Nothing)

                m_AllDeclaredMembers = New Generic.List(Of MemberInfo)
                m_AllDeclaredMembers.AddRange(m_OpenType.GetMembers(Helper.ALLNOBASEMEMBERS))

                For i As Integer = 0 To m_AllDeclaredMembers.Count - 1
                    Dim minfo As MethodInfo = TryCast(m_AllDeclaredMembers(i), MethodInfo)
                    Dim cinfo As ConstructorInfo = TryCast(m_AllDeclaredMembers(i), ConstructorInfo)
                    Dim finfo As FieldInfo = TryCast(m_AllDeclaredMembers(i), FieldInfo)
                    Dim pinfo As PropertyInfo = TryCast(m_AllDeclaredMembers(i), PropertyInfo)
                    Dim tinfo As Type = TryCast(m_AllDeclaredMembers(i), Type)

                    If minfo IsNot Nothing Then
                        m_AllDeclaredMembers(i) = Compiler.TypeManager.MakeGenericMethod(Me.Parent, minfo, m_TypeParameters, m_TypeArguments, Me)
                    ElseIf cinfo IsNot Nothing Then
                        m_AllDeclaredMembers(i) = Compiler.TypeManager.MakeGenericConstructor(Me.Parent, cinfo, m_TypeParameters, m_TypeArguments, Me)
                    ElseIf finfo IsNot Nothing Then
                        m_AllDeclaredMembers(i) = Compiler.TypeManager.MakeGenericField(Me.Parent, finfo, m_TypeParameters, m_TypeArguments, Me)
                    ElseIf pinfo IsNot Nothing Then
                        m_AllDeclaredMembers(i) = Compiler.TypeManager.MakeGenericProperty(Me.Parent, pinfo, m_TypeParameters, m_TypeArguments, Me)
                    ElseIf tinfo IsNot Nothing Then
                        m_AllDeclaredMembers(i) = Compiler.TypeManager.MakeGenericType(Me.Parent, tinfo, m_TypeArguments)
                    Else
                        Throw New InternalException(Me)
                    End If
                Next
            End If
            Return m_AllDeclaredMembers
        End Get
    End Property

    Protected Overrides ReadOnly Property AllMembers() As System.Collections.Generic.List(Of System.Reflection.MemberInfo)
        Get
            If m_AllMembers Is Nothing Then
                Helper.Assert(m_OpenType IsNot Nothing)

                m_AllMembers = New Generic.List(Of MemberInfo)
                m_AllMembers.AddRange(m_OpenType.GetMembers(Helper.ALLMEMBERS))

                For i As Integer = 0 To m_AllMembers.Count - 1
                    Dim minfo As MethodInfo = TryCast(m_AllMembers(i), MethodInfo)
                    Dim cinfo As ConstructorInfo = TryCast(m_AllMembers(i), ConstructorInfo)
                    Dim finfo As FieldInfo = TryCast(m_AllMembers(i), FieldInfo)
                    Dim pinfo As PropertyInfo = TryCast(m_AllMembers(i), PropertyInfo)
                    Dim tinfo As Type = TryCast(m_AllMembers(i), Type)

                    If minfo IsNot Nothing Then
                        m_AllMembers(i) = Compiler.TypeManager.MakeGenericMethod(Me.Parent, minfo, m_TypeParameters, m_TypeArguments, Me)
                    ElseIf cinfo IsNot Nothing Then
                        m_AllMembers(i) = Compiler.TypeManager.MakeGenericConstructor(Me.Parent, cinfo, m_TypeParameters, m_TypeArguments, Me)
                    ElseIf finfo IsNot Nothing Then
                        m_AllMembers(i) = Compiler.TypeManager.MakeGenericField(Me.Parent, finfo, m_TypeParameters, m_TypeArguments, Me)
                    ElseIf pinfo IsNot Nothing Then
                        m_AllMembers(i) = Compiler.TypeManager.MakeGenericProperty(Me.Parent, pinfo, m_TypeParameters, m_TypeArguments, Me)
                    ElseIf tinfo IsNot Nothing Then
                        m_AllMembers(i) = Compiler.TypeManager.MakeGenericType(Me.Parent, tinfo, m_TypeArguments)
                    Else
                        Throw New InternalException(Me)
                    End If
                Next

                Helper.AddMembers(Compiler, Me, m_AllMembers, Helper.GetBaseMembers(Compiler, Me))

            End If

            Return m_AllMembers
        End Get
    End Property

    Protected Overrides Function GetAttributeFlagsImpl() As System.Reflection.TypeAttributes
        Dim result As TypeAttributes

        result = m_OpenType.Attributes

        DumpMethodInfo(result)

        Return result
    End Function

    ReadOnly Property TypeArguments() As Type()
        Get
            Return m_TypeArguments
        End Get
    End Property

    Public Overrides Function GetGenericTypeDefinition() As System.Type
        Dim result As Type

        result = m_OpenType

        If result.IsGenericTypeDefinition = False Then
            result = result.GetGenericTypeDefinition
            If result Is Nothing Then result = m_OpenType
        End If

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property IsGenericParameter() As Boolean
        Get
            Dim result As Boolean
            result = False
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property IsGenericType() As Boolean
        Get
            Dim result As Boolean
            result = True
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property IsGenericTypeDefinition() As Boolean
        Get
            Dim result As Boolean
            result = False
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property FullName() As String
        Get
            Dim result As String

            If m_FullName Is Nothing Then
                Dim name As String = Me.Name
                Dim tmp As Type = m_OpenType.DeclaringType
                Do While tmp IsNot Nothing
                    name = tmp.Name & "+" & name
                    tmp = tmp.DeclaringType
                Loop

                If Me.Namespace <> "" Then
                    m_FullName = Me.Namespace & "." & name
                Else
                    m_FullName = name
                End If
            End If
            result = m_FullName

            DumpMethodInfo(result)
            Helper.Assert(result.IndexOf("\"c) = -1)
            Return result
        End Get
    End Property

    Overrides ReadOnly Property Name() As String
        Get
            Dim result As String
            If m_Name Is Nothing Then
                Dim builder As New System.Text.StringBuilder
                builder.Append(m_OpenType.Name)
                For i As Integer = 0 To builder.Length - 1
                    If builder.Chars(i) = "["c Then
                        builder.Length = i
                        Exit For
                    End If
                Next
                '                If builder..IndexOf("[") > -1 Then m_Name = m_Name.Substring(0, m_Name.IndexOf("["))
                builder.Append("[")
                For i As Integer = 0 To m_TypeArguments.Length - 1
                    If m_TypeArguments(i).FullName = String.Empty Then
                        builder.Append(m_TypeArguments(i).Name)
                    Else
                        builder.Append(m_TypeArguments(i).FullName)
                    End If
                    If i < m_TypeArguments.Length - 1 Then builder.Append(",")
                Next
                builder.Append("]")
                m_Name = builder.ToString
            End If
            result = m_Name
            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property BaseType() As System.Type
        Get
            Static result As Type = Nothing

            If result Is Nothing Then
                Helper.Assert(m_OpenType IsNot Nothing)
                result = m_OpenType.BaseType
                If result IsNot Nothing Then
                    result = Helper.ApplyTypeArguments(Parent, result, m_TypeParameters, m_TypeArguments)
                End If
            End If

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Protected Overrides Function HasElementTypeImpl() As Boolean
        Dim result As Boolean
        result = False
        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides ReadOnly Property UnderlyingSystemType() As System.Type
        Get
            Dim result As Type

            result = Me

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides ReadOnly Property ContainsGenericParameters() As Boolean
        Get
            Dim result As Boolean

            result = False
            For Each arg As Type In m_TypeArguments
                If arg.IsGenericParameter Then
                    result = True
                    Exit For
                End If
            Next

            DumpMethodInfo(result)
            Return result
        End Get
    End Property

    Public Overrides Function GetGenericArguments() As System.Type()
        Dim result As Type() = Nothing

        result = m_TypeArguments

        DumpMethodInfo(result)
        Return result
    End Function

    Public Overrides Function MakeGenericType(ByVal ParamArray typeArguments() As System.Type) As System.Type
        Dim result As Type = Nothing

        Throw New InvalidOperationException("Not a generic type definition")

        DumpMethodInfo(result)
        Return result
    End Function

End Class
