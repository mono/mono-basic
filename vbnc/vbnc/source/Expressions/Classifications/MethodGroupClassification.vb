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
#Const DEBUGMETHODRESOLUTION = 0
#Const EXTENDEDDEBUG = 0
#End If

Imports System.Reflection
''' <summary>
''' A set of methods overloaded on the same name. 
''' A method group may have an associated instance expression and
''' an associated type argument list.
''' 
''' Can be reclassified as a value. The method group expression is 
''' interpreted as an invocation expression with the associated type parameter 
''' list and empty parenthesis (that is, "f" is interpreted as "f()" and "f(Of Integer)"
''' is interpreted as "f(Of Integer)()". This reclassification may actually result in
''' the expression being reclassified as void.
''' </summary>
''' <remarks></remarks>
Public Class MethodGroupClassification
    Inherits ExpressionClassification

    ''' <summary>
    ''' The instance expression to the method.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_InstanceExpression As Expression
    Private m_Parameters As Expression()
    Private m_TypeArguments As Type()

    ''' <summary>
    ''' The group of possible methods.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Group As Generic.List(Of MemberInfo)
    ''' <summary>
    ''' The type where the calling code is found.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_CallingType As TypeDeclaration

    ''' <summary>
    ''' Has ResolveGroup been called?
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Resolved As Boolean
    Private m_Resolver As MethodResolver

#If DEBUG Then

    Private m_OriginalGroup As Generic.List(Of MemberInfo)

    Sub RevertResolveGroup()
        m_Group = New Generic.List(Of MemberInfo)(m_OriginalGroup)
    End Sub
#End If

    <Diagnostics.Conditional("DEBUGMETHODRESOLUTION")> Sub LogResolutionMessage(ByVal msg As String)
        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, msg)
    End Sub

    ReadOnly Property Parameters() As Expression()
        Get
            Return m_Parameters
        End Get
    End Property

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            Return False
        End Get
    End Property
    ''' <summary>
    ''' Has ResolveGroup been called?
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Resolved() As Boolean
        Get
            Return m_Resolved
        End Get
    End Property

    Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_Resolved)

        If Info.IsRHS Then
            result = GenerateCodeAsValue(Info) AndAlso result
        ElseIf Info.IsLHS Then
            Return Info.Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
        Else
            Throw New InternalException(Me)
        End If

        Return result
    End Function

    Function GenerateCodeAsValue(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Helper.Assert(m_Resolved)

        'If m_InstanceExpression IsNot Nothing Then
        '    Dim instanceExpType As Type
        '    If ResolvedMethod.DeclaringType.IsValueType Then
        '        instanceExpType = ResolvedMethod.DeclaringType.MakeByRefType
        '    Else
        '        instanceExpType = ResolvedMethod.DeclaringType
        '    End If
        '    result = m_InstanceExpression.GenerateCode(Info.Clone(True, False, instanceExpType)) AndAlso result
        '    'Emitter.EmitConversion(instanceExpType, Info)
        'End If
        'Helper.Assert(Type IsNot Nothing)
        'If m_Parameters IsNot Nothing Then
        '    Helper.Assert(ResolvedMethod.GetParameters.Length = m_Parameters.Length)
        '    Dim expInfo As EmitInfo
        '    For i As Integer = 0 To m_Parameters.GetUpperBound(0)
        '        expInfo = Info.Clone(True, False, ResolvedMethod.GetParameters(i).ParameterType)
        '        result = m_Parameters(i).GenerateCode(Info) AndAlso result
        '    Next
        'Else
        '    Helper.Assert(ResolvedMethod.GetParameters.Length = 0)
        'End If

        'Emitter.EmitCallOrCallVirt(Info, ResolvedMethod)

        Helper.EmitArgumentsAndCallOrCallVirt(Info, m_InstanceExpression, New ArgumentList(Parent, m_Parameters), ResolvedMethod)

        Return result
    End Function

    ''' <summary>
    ''' Reclassifies the method group to a value, at the same time the method
    ''' group might be resolved using an empty argument list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overloads Function ReclassifyToValue() As ValueClassification
        Dim result As ValueClassification
        If m_Resolved = False Then
            Me.ResolveGroup(New ArgumentList(Me.Parent), Nothing)
        End If
        result = New ValueClassification(Me)
        Return result
    End Function

    ''' <summary>
    ''' The instance expression of the method group.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property InstanceExpression() As Expression
        Get
            Return m_InstanceExpression
        End Get
    End Property

    ''' <summary>
    ''' The type of the method group, is the return type of the method.
    ''' If this method is a sub, the return type is System.Void.
    ''' If this is a constructor, the return type is nothing.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Type() As Type
        Get
            If SuccessfullyResolved = False Then Throw New InternalException(Me)
            Dim m As MethodInfo = ResolvedMethodInfo
            If m Is Nothing Then
                Dim c As ConstructorInfo = ResolvedConstructor
                If c Is Nothing Then
                    Throw New InternalException(Me)
                Else
                    Return Nothing
                End If
            Else
                If m.ReturnType Is Nothing Then
                    Return Compiler.TypeCache.System_Void
                Else
                    Return m.ReturnType
                End If
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns nothing if the resolved method isn't a methodinfo.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property ResolvedMethodInfo() As MethodInfo
        Get
            If SuccessfullyResolved = False Then Throw New InternalException(Me)
            If TypeOf m_Group(0) Is MethodInfo Then
                Return DirectCast(m_Group(0), MethodInfo)
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns nothing if the resolved method isn't a constructorinfo.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ResolvedConstructor() As ConstructorInfo
        Get
            If SuccessfullyResolved = False Then Throw New InternalException(Me)
            If TypeOf m_Group(0) Is ConstructorInfo Then
                Return DirectCast(m_Group(0), ConstructorInfo)
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns the resolved method.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ResolvedMethod() As MethodBase
        Get
            If SuccessfullyResolved = False Then Throw New InternalException(Me)
            If m_Group.Count = 0 Then Return Nothing
            Return DirectCast(m_Group(0), MethodBase)
        End Get
    End Property

    ReadOnly Property Resolver() As MethodResolver
        Get
            Return m_Resolver
        End Get
    End Property

    ''' <summary>
    ''' Returns true if the method group has successfully been resolved.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property SuccessfullyResolved() As Boolean
        Get
            Return m_Resolved AndAlso (m_Group.Count = 1 OrElse m_Resolver.IsLateBound)
        End Get
    End Property

    Private Sub SetMethods(ByVal lst As Generic.List(Of MemberInfo))
        m_Group = New Generic.List(Of MemberInfo)
        For i As Integer = 0 To lst.Count - 1
            Dim member As MemberInfo = lst(i)
            Dim method As MethodBase
            method = TryCast(member, MethodBase)
            Helper.Assert(method IsNot Nothing)
            m_Group.Add(method)
        Next
#If DEBUG Then
        m_OriginalGroup = New Generic.List(Of MemberInfo)(m_Group)
#End If
    End Sub

    Private Sub SetMethods(ByVal lst As Generic.List(Of MethodBase))
        m_Group = New Generic.List(Of MemberInfo)
        For i As Integer = 0 To lst.Count - 1
            Dim method As MethodBase = lst(i)
            m_Group.Add(method)
        Next
#If DEBUG Then
        m_OriginalGroup = New Generic.List(Of MemberInfo)(lst.ToArray)
#End If
    End Sub

    ''' <summary>
    ''' The name of the method. (Any method actually, since they should all have the same name).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MethodName() As String
        Get
            For i As Integer = 0 To m_Group.Count - 1
                If m_Group(i) IsNot Nothing Then
                    Return m_Group(i).Name
                End If
            Next
            Throw New InternalException(Me)
        End Get
    End Property

    ''' <summary>
    ''' The group of methods.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property [Group]() As Generic.List(Of MemberInfo)
        Get
            Return m_Group
        End Get
        Set(ByVal value As Generic.List(Of MemberInfo))
            m_Group = value
        End Set
    End Property

    Shared Function ResolveInterfaceGroup(ByVal grp As Generic.List(Of MemberInfo), ByVal codedMember As IMember) As MemberInfo
        Helper.Assert(codedMember IsNot Nothing)

        Dim methodtypes() As Type
        Dim grptypes() As Type
        Dim result As MemberInfo = Nothing

        Select Case codedMember.MemberDescriptor.MemberType
            Case MemberTypes.Method
                Dim method As IMethod = TryCast(codedMember, IMethod)
                methodtypes = method.Signature.Parameters.ToTypeArray
            Case MemberTypes.Property
                Dim prop As PropertyDeclaration = TryCast(codedMember, PropertyDeclaration)
                methodtypes = prop.Signature.Parameters.ToTypeArray
            Case MemberTypes.Event
                methodtypes = Type.EmptyTypes
            Case Else
                methodtypes = Nothing
                codedMember.Compiler.Report.ShowMessage(Messages.VBNC99997, codedMember.Location)
        End Select

        For Each member As MemberInfo In grp
            Select Case member.MemberType
                Case MemberTypes.Method
                    grptypes = Helper.GetParameterTypes(codedMember.Parent, DirectCast(member, MethodInfo))
                Case MemberTypes.Property
                    grptypes = Helper.GetParameterTypes(Helper.GetParameters(codedMember.Compiler, DirectCast(member, PropertyInfo)))
                Case MemberTypes.Event
                    grptypes = Type.EmptyTypes
                Case Else
                    Throw New InternalException(codedMember)
            End Select
            If Helper.CompareTypes(methodtypes, grptypes) Then
                Helper.Assert(result Is Nothing)
                result = member
#If Not DEBUG Then
                Exit For
#End If
            End If
        Next
        Return result
    End Function

    ReadOnly Property IsLateBound() As Boolean
        Get
            Return m_Resolver IsNot Nothing AndAlso m_Resolved AndAlso m_Resolver.IsLateBound
        End Get
    End Property

    ''' <summary>
    ''' Resolve this group with the specified parameters.
    ''' </summary>
    ''' <param name="SourceParameters"></param>
    ''' <remarks></remarks>
    Function ResolveGroup(ByVal SourceParameters As ArgumentList, ByRef FinalSourceArguments As Generic.List(Of Argument), Optional ByVal TypeArguments As TypeArgumentList = Nothing, Optional ByVal ShowErrors As Boolean = False) As Boolean
        Dim result As Boolean = True

        If SourceParameters Is Nothing Then Throw New InternalException("SourceParameters is nothing.")
        If Resolved Then
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Method group is beeing resolved more than once.")
        End If
        If m_Group.Count <= 0 Then Throw New InternalException("Nothing to resolve...")

        Dim resolvedGroup As New Generic.List(Of MemberInfo)

        If m_Resolver Is Nothing Then m_Resolver = New MethodResolver(Parent)
        m_Resolver.ShowErrors = ShowErrors
        m_Resolver.Init(m_Group, SourceParameters, TypeArguments)
        result = m_Resolver.Resolve AndAlso result

        If result Then
            If m_Resolver.IsLateBound = False Then
                FinalSourceArguments = m_Resolver.ResolvedCandidate.ExactArguments
                resolvedGroup.Add(m_Resolver.ResolvedMember)
            End If
        End If

        'result = Helper.ResolveGroup(Me.Parent, m_Group, resolvedGroup, SourceParameters, TypeArguments, FinalSourceArguments, ShowErrors) AndAlso result

        If result Then
            m_Group = resolvedGroup
            m_Resolved = True
            If IsLateBound = False AndAlso ResolvedMethod.IsStatic Then
                'Helper.StopIfDebugging(m_InstanceExpression IsNot Nothing AndAlso TypeOf m_InstanceExpression Is MeExpression = False)
                m_InstanceExpression = Nothing
            End If
#If EXTENDEDDEBUG Then
        Else
            'Don't stop here since method resolution might fail correctly.
            Compiler.Report.WriteLine("")
            Compiler.Report.WriteLine(".......Method resolution failed, showing log.......")
            Dim tmp As Boolean = Helper.LOGMETHODRESOLUTION
            Helper.LOGMETHODRESOLUTION = True
            resolvedGroup.Clear()
            result = Helper.ResolveGroup(Me.Parent, m_Group, resolvedGroup, SourceParameters, TypeArguments, FinalSourceArguments) AndAlso result
            Helper.LOGMETHODRESOLUTION = tmp
            Compiler.Report.WriteLine("...................................................")
#End If
        End If

        Return result
    End Function

    ''' <summary>
    ''' Removes methods that are nothing from the group
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShrinkGroup()
        m_Group.RemoveAll(New Predicate(Of MemberInfo)(AddressOf Helper.IsNothing(Of MemberInfo)))
    End Sub

    Function IsAccessible(ByVal Caller As TypeDeclaration, ByVal Method As MethodBase) As Boolean
        Return Helper.IsAccessible(Compiler, Caller, Method)
    End Function


    Sub New(ByVal Parent As ParsedObject, ByVal InstanceExpression As Expression, ByVal Method As MethodDeclaration)
        MyBase.New(Classifications.MethodGroup, Parent)
        m_Group = New Generic.List(Of MemberInfo)
        m_Group.Add(Method.MethodDescriptor)
        m_Resolved = True
        m_InstanceExpression = InstanceExpression
    End Sub

    Private Sub New(ByVal Parent As ParsedObject, ByVal InstanceExpression As Expression, ByVal Parameters() As Expression)
        MyBase.new(Classifications.MethodGroup, Parent)
        m_InstanceExpression = InstanceExpression
        m_CallingType = Parent.FindFirstParent(Of TypeDeclaration)()
        m_Parameters = Parameters
        'Helper.Assert(m_CallingType IsNot Nothing)
        Helper.Assert(m_InstanceExpression Is Nothing OrElse m_InstanceExpression.IsResolved)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal InstanceExpression As Expression, ByVal Parameters() As Expression, ByVal ParamArray Methods As MemberInfo())
        Me.New(Parent, InstanceExpression, Parameters)
        SetMethods(New Generic.List(Of MemberInfo)(Methods))
        Helper.Assert(Methods.Length > 0)
        Helper.Assert(m_InstanceExpression Is Nothing OrElse m_InstanceExpression.IsResolved)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal InstanceExpression As Expression, ByVal Parameters() As Expression, ByVal Methods As Generic.List(Of MemberInfo))
        Me.new(Parent, InstanceExpression, Parameters)
        SetMethods(Methods)
        Helper.Assert(Methods.Count > 0)
        Helper.Assert(m_InstanceExpression Is Nothing OrElse m_InstanceExpression.IsResolved)
    End Sub

    Shadows ReadOnly Property Parent() As ParsedObject
        Get
            Return DirectCast(MyBase.Parent, ParsedObject)
        End Get
    End Property
End Class
