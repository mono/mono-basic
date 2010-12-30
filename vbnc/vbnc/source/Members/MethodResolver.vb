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

''' <summary>
''' A helper class to do method resolution
''' </summary>
''' <remarks></remarks>
Public Class MethodResolver
    Public Shared LOGMETHODRESOLUTION As Boolean = False

    Private m_Parent As ParsedObject

    Private m_Candidates As Generic.List(Of MemberCandidate)
    Private m_InitialCandidates As MemberCandidate()
    Private m_Name As String
    Private m_Arguments As ArgumentList
    Private m_TypeArguments As TypeArgumentList
    Private m_Caller As TypeDeclaration

    Private m_ArgumentsTypesAsString As String

    Private m_ResolvedCandidate As MemberCandidate
    Private m_ShowErrors As Boolean
    Private m_Resolved As Boolean
    Private m_IsLateBound As Boolean

    ReadOnly Property IsLateBound() As Boolean
        Get
            Return m_IsLateBound
        End Get
    End Property

    Property ShowErrors() As Boolean
        Get
            Return m_ShowErrors
        End Get
        Set(ByVal value As Boolean)
            m_ShowErrors = value
        End Set
    End Property

    ReadOnly Property Candidates() As Generic.List(Of MemberCandidate)
        Get
            Return m_Candidates
        End Get
    End Property

    ReadOnly Property ArgumentsTypesAsString() As String
        Get
            If m_ArgumentsTypesAsString Is Nothing Then
                m_ArgumentsTypesAsString = "(" & m_Arguments.ArgumentsTypesAsString & ")"
            End If
            Return m_ArgumentsTypesAsString
        End Get
    End Property

    ReadOnly Property Caller() As TypeDeclaration
        Get
            Return m_Caller
        End Get
    End Property

    ReadOnly Property Parent() As ParsedObject
        Get
            Return m_Parent
        End Get
    End Property

    ReadOnly Property MethodName() As String
        Get
            Return m_Name
        End Get
    End Property

    ReadOnly Property MethodDeclaringType() As Mono.Cecil.TypeReference
        Get
            Return m_InitialCandidates(0).Member.DeclaringType
        End Get
    End Property

    Sub Init(ByVal InitialGroup As Generic.List(Of Mono.Cecil.MemberReference), ByVal Arguments As ArgumentList, ByVal TypeArguments As TypeArgumentList)
        m_Candidates = New Generic.List(Of MemberCandidate)(InitialGroup.Count)
        For i As Integer = 0 To InitialGroup.Count - 1
            Dim member As Mono.Cecil.MemberReference = InitialGroup(i)
            m_Candidates.Add(New MemberCandidate(Me, member))
        Next

        m_InitialCandidates = m_Candidates.ToArray()

        m_Arguments = Arguments
        m_TypeArguments = TypeArguments
        m_Caller = Parent.FindTypeParent()
        m_Name = InitialGroup(0).Name
    End Sub

    Sub New(ByVal Parent As ParsedObject)
        m_Parent = Parent
    End Sub

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Parent.Compiler
        End Get
    End Property

    ReadOnly Property Arguments() As ArgumentList
        Get
            Return m_Arguments
        End Get
    End Property

    ReadOnly Property TypeArguments() As TypeArgumentList
        Get
            Return m_TypeArguments
        End Get
    End Property

    ReadOnly Property CandidatesLeft() As Integer
        Get
            Dim result As Integer
            For i As Integer = 0 To m_Candidates.Count - 1
                If m_Candidates(i) IsNot Nothing Then result += 1
            Next
            Return result
        End Get
    End Property

    Public Function Resolve() As Boolean
        Dim result As Boolean = True

        'If m_Resolved AndAlso ShowErrors = False Then Helper.StopIfDebugging()

        Log("")
        Log("Resolving method {0} with arguments {1}", ArgumentsTypesAsString)

        result = ResolveInternal()

        If result Then
            Helper.Assert(CandidatesLeft = 1 OrElse IsLateBound)

            If IsLateBound Then
                m_ResolvedCandidate = Nothing
            Else
                For Each member As MemberCandidate In m_Candidates
                    If member Is Nothing Then Continue For

                    If IsValidCandidate(member) = False Then
                        result = Compiler.Report.ShowMessage(Messages.VBNC30657, Parent.Location, member.Member.Name)
                        Exit For
                    End If
                    m_ResolvedCandidate = member
                    m_ResolvedCandidate.SelectOutputArguments()
                    Exit For
                Next
            End If
        End If

        m_Resolved = True

        Return result
    End Function

    Private Function ResolveInternal() As Boolean
        Log("There are " & CandidatesLeft & " candidates left.")

        m_IsLateBound = False

        If ShowErrors AndAlso CandidatesLeft = 0 Then
            Helper.AddError(Me.m_Parent, "No candidates: " & Parent.Location.ToString(Compiler))
        End If

        RemoveInaccessible()
        Log("After removing inaccessible candidates, there are " & CandidatesLeft & " candidates left.")
        If ShowErrors AndAlso CandidatesLeft = 0 Then
            If m_InitialCandidates.Length = 1 Then
                Return Compiler.Report.ShowMessage(Messages.VBNC30390, Parent.Location, m_InitialCandidates(0).Member.DeclaringType.Name, m_InitialCandidates(0).Member.Name, Helper.GetMethodAccessibilityString(Helper.GetMethodAttributes(m_InitialCandidates(0).Member)))
            Else
                Return Compiler.Report.ShowMessage(Messages.VBNC30517, Parent.Location, m_InitialCandidates(0).Member.Name)
            End If
        End If

        ExpandParamArrays()
        Log("After expanding paramarrays, there are " & CandidatesLeft & " candidates left.")
        If ShowErrors AndAlso CandidatesLeft = 0 Then
            Throw New InternalException("Expanding paramarrays resulted in fewer candidates: " & Parent.Location.ToString(Compiler))
        End If

        RemoveInapplicable()
        Log("After removing inapplicable candidates, there are " & CandidatesLeft & " candidates left.")
        If ShowErrors AndAlso CandidatesLeft = 0 Then
            If m_InitialCandidates.Length = 1 Then
                Dim argsGiven, argsRequired As Integer
                Dim params As Mono.Collections.Generic.Collection(Of ParameterDefinition)
                params = Helper.GetParameters(Compiler, m_InitialCandidates(0).Member)
                argsRequired = params.Count
                argsGiven = m_Arguments.Length
                If argsGiven >= argsRequired Then
                    Return Compiler.Report.ShowMessage(Messages.VBNC30057, Parent.Location, m_InitialCandidates(0).ToString())
                Else
                    For i As Integer = argsGiven To argsRequired - 1
                        Compiler.Report.ShowMessage(Messages.VBNC30455, Parent.Location, params(i).Name, m_InitialCandidates(0).ToString())
                    Next
                    Return False
                End If
            Else
                Return Compiler.Report.ShowMessage(Messages.VBNC30516, Parent.Location, MethodName)
            End If
        End If

        InferTypeArguments()
        Log("After inferring type arguments, there are " & CandidatesLeft & " candidates left.")
        If CandidatesLeft = 0 Then
            'Type infer code shows the error message if it's supposed to show errors
            Return False
        End If

        If CandidatesLeft <= 1 Then Return CandidatesLeft = 1

        RemoveNarrowingExceptObject()
        Log("After removing narrowing (except object) candidates, there are " & CandidatesLeft & " candidates left.")
        If ShowErrors AndAlso CandidatesLeft = 0 Then
            Helper.AddError(Me.m_Parent, String.Format("After removing narrowing (except object) candidates for method '{0}', nothing was found", Me.m_InitialCandidates(0).Member.Name))
            Helper.AddError(Me.m_Parent, String.Format("Tried to select using invocation list: '{0}' of {1} initial candidates", Me.ArgumentsTypesAsString, m_InitialCandidates.Length))
            Dim reported As Integer = 0
            For i As Integer = 0 To m_InitialCandidates.Length - 1
                reported += 1
                Dim mi As Mono.Cecil.MemberReference = m_InitialCandidates(i).Member
                Helper.AddError(Me.m_Parent, String.Format("Candidate #{0}: {1} {2}", reported, mi.Name, Helper.ToString(Me.m_Parent, Helper.GetParameters(Me.m_Parent, mi))))
            Next
        End If

        If CandidatesLeft <= 1 Then Return CandidatesLeft = 1

        RemoveNarrowing()
        Log("After removing narrowing candidates, there are " & CandidatesLeft & " candidates left.")
        If CandidatesLeft = 1 Then
            Return True
        ElseIf CandidatesLeft = 0 Then
            If Parent.Location.File(Compiler).IsOptionStrictOn = False Then
                m_IsLateBound = True
                Return True
            End If
        End If

        If ShowErrors AndAlso CandidatesLeft = 0 Then
            Helper.AddError(Me.m_Parent, "No non-narrowing: " & Parent.Location.ToString(Compiler))
        End If

        SelectMostApplicable()
        Log("After selecting the most applicable candidates, there are " & CandidatesLeft & " candidates left.")
        If ShowErrors AndAlso CandidatesLeft = 0 Then
            Helper.AddError(Me.m_Parent, "No most applicable: " & Parent.Location.ToString(Compiler))
        End If

        If CandidatesLeft = 1 Then
            Return True
        End If

        SelectLessGeneric()
        Log("After selecting the less generic candidates, there are " & CandidatesLeft & " candidates left.")
        If CandidatesLeft = 1 Then
            Return True
        End If

        RemoveInvalid()

        If ShowErrors AndAlso CandidatesLeft <> 1 Then
            If CandidatesLeft > 1 Then
                Helper.AddError(Me.m_Parent, String.Format("After selecting the less generic method for method '{0}', there are still {1} candidates left", Me.m_InitialCandidates(0).Member.Name, CandidatesLeft))
                Helper.AddError(Me.m_Parent, String.Format("Tried to select using invocation list: '{0}'", Me.ArgumentsTypesAsString))
                Dim reported As Integer = 0
                For i As Integer = 0 To m_Candidates.Count - 1
                    If m_Candidates(i) Is Nothing Then Continue For
                    reported += 1
                    Dim mi As Mono.Cecil.MemberReference = m_InitialCandidates(i).Member
                    Helper.AddError(Me.m_Parent, String.Format("Candidate #{0}: {1} {2}", reported, mi.Name, Helper.ToString(Me.m_Parent, Helper.GetParameters(Me.m_Parent, mi))))
                Next
            Else
                Helper.AddError(Me.m_Parent, String.Format("After selecting the less generic method for method '{0}', nothing was found", Me.m_InitialCandidates(0).Member.Name))
            End If
        End If

        Return CandidatesLeft = 1
    End Function

    Function IsValidCandidate(ByVal candidate As MemberCandidate) As Boolean
        If CecilHelper.IsValidType(candidate.ReturnType) = False Then Return False
        For j As Integer = 0 To candidate.DefinedParametersTypes.Length - 1
            If CecilHelper.IsValidType(candidate.DefinedParametersTypes(j)) = False Then Return False
        Next
        Return True
    End Function

    Sub RemoveInvalid()
        For i As Integer = 0 To m_Candidates.Count - 1
            Dim m As MemberCandidate = m_Candidates(i)
            If m Is Nothing Then Continue For
            If IsValidCandidate(m) = False Then m_Candidates(i) = Nothing
        Next
    End Sub

    Private Function ContainsGenericParameters(ByVal Type As TypeReference, ByVal Find As Mono.Collections.Generic.Collection(Of GenericParameter)) As Boolean
        Dim elementType As TypeReference
        Dim tg As GenericParameter
        Dim git As GenericInstanceType

        If Type Is Nothing Then Return False

        tg = TryCast(Type, GenericParameter)
        If tg IsNot Nothing AndAlso Find.Contains(tg) Then Return True

        git = TryCast(Type, GenericInstanceType)
        If git IsNot Nothing AndAlso git.HasGenericArguments Then
            For i As Integer = 0 To Find.Count - 1
                If git.GenericArguments.Contains(Find(i)) Then Return True
            Next
        End If

        elementType = Type.GetElementType()
        If elementType IsNot Nothing AndAlso elementType IsNot Type Then
            Return ContainsGenericParameters(elementType, Find)
        End If

        Return False
    End Function

    Sub SelectLessGeneric()
        '
        'A member M is determined to be less generic than a member N as follows:
        '1.	If, for each pair of matching parameters Mj and Nj, Mj is less or equally 
        '   generic than Nj with respect to type parameters on the method, and at least 
        '   one Mj is less generic with respect to type parameters on the method.
        '2.	Otherwise, if for each pair of matching parameters Mj and Nj, Mj is less or equally generic than Nj 
        '   with respect to type parameters on the type, and at least one Mj is less generic with respect to 
        '   type parameters on the type, then M is less generic than N.
        '
        'A parameter M is considered to be equally generic to a parameter N if their types Mt and Nt
        'both refer to type parameters or both don't refer to type parameters.
        'M is considered to be less generic than N if Mt does not refer to a type parameter and Nt does.
        '

        Dim gpType As Mono.Collections.Generic.Collection(Of GenericParameter) = Nothing
        Dim gp() As Mono.Collections.Generic.Collection(Of GenericParameter) = Nothing

        For i As Integer = 0 To m_Candidates.Count - 1
            If m_Candidates(i) Is Nothing Then Continue For

            For j As Integer = i + 1 To m_Candidates.Count - 1
                If m_Candidates(j) Is Nothing Then Continue For

                Dim candidateI As MemberCandidate = m_Candidates(i)
                Dim candidateJ As MemberCandidate = m_Candidates(j)
                Dim parametersI As Mono.Collections.Generic.Collection(Of ParameterDefinition) = Helper.GetOriginalParameters(candidateI.Member)
                Dim parametersJ As Mono.Collections.Generic.Collection(Of ParameterDefinition) = Helper.GetOriginalParameters(candidateJ.Member)
                Dim paramCount As Integer = Math.Min(parametersI.Count, parametersJ.Count)
                Dim gpI As Mono.Collections.Generic.Collection(Of GenericParameter)
                Dim gpJ As Mono.Collections.Generic.Collection(Of GenericParameter)
                Dim timesLessGenericI As Integer
                Dim timesLessGenericJ As Integer

                'Not sure if the # of parameters can be different between I and J here
                If paramCount = 0 Then Continue For

                If gp Is Nothing Then ReDim gp(m_Candidates.Count - 1)

                gpI = gp(i)
                gpJ = gp(j)

                If gpI Is Nothing Then
                    gp(i) = Helper.GetGenericParameters(candidateI.Member)
                    gpI = gp(i)
                End If
                If gpJ Is Nothing Then
                    gp(j) = Helper.GetGenericParameters(candidateJ.Member)
                    gpJ = gp(j)
                End If

                '1.	If, for each pair of matching parameters Mj and Nj, Mj is less or equally 
                '   generic than Nj with respect to type parameters on the method, and at least 
                '   one Mj is less generic with respect to type parameters on the method.

                For p As Integer = 0 To paramCount - 1
                    Dim paramI As ParameterDefinition = parametersI(p)
                    Dim paramJ As ParameterDefinition = parametersJ(p)
                    Dim containsI As Boolean = ContainsGenericParameters(paramI.ParameterType, gpI)
                    Dim containsJ As Boolean = ContainsGenericParameters(paramJ.ParameterType, gpJ)

                    If containsI = False AndAlso containsJ = True Then
                        timesLessGenericI += 1
                    ElseIf containsI = True AndAlso containsJ = False Then
                        timesLessGenericJ += 1
                    End If
                Next

                If timesLessGenericI > 0 AndAlso timesLessGenericJ = 0 Then
                    Log("MORE METHOD GENERIC: Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidateJ.DefinedParametersTypes), ArgumentsTypesAsString)
                    m_Candidates(j) = Nothing
                    Exit For
                ElseIf timesLessGenericI = 0 AndAlso timesLessGenericJ > 0 Then
                    Log("MORE METHOD GENERIC: Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidateI.DefinedParametersTypes), ArgumentsTypesAsString)
                    m_Candidates(i) = Nothing
                    Exit For
                End If

                '2.	Otherwise, if for each pair of matching parameters Mj and Nj, Mj is less or equally generic than Nj 
                '   with respect to type parameters on the type, and at least one Mj is less generic with respect to 
                '   type parameters on the type, then M is less generic than N.
                timesLessGenericI = 0
                timesLessGenericJ = 0

                If gpType Is Nothing Then
                    gpType = CecilHelper.FindDefinition(m_Candidates(i).Member.DeclaringType).GenericParameters
                End If

                'Not sure if the # of parameters can be different between I and J here
                For p As Integer = 0 To paramCount - 1
                    Dim paramI As ParameterDefinition = parametersI(p)
                    Dim paramJ As ParameterDefinition = parametersJ(p)
                    Dim containsI As Boolean = ContainsGenericParameters(paramI.ParameterType, gpType)
                    Dim containsJ As Boolean = ContainsGenericParameters(paramJ.ParameterType, gpType)

                    If containsI = False AndAlso containsJ = True Then
                        timesLessGenericI += 1
                    ElseIf containsI = True AndAlso containsJ = False Then
                        timesLessGenericJ += 1
                    End If
                Next

                If timesLessGenericI > 0 AndAlso timesLessGenericJ = 0 Then
                    Log("MORE TYPE GENERIC: Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidateJ.DefinedParametersTypes), ArgumentsTypesAsString)
                    m_Candidates(j) = Nothing
                    Exit For
                ElseIf timesLessGenericI = 0 AndAlso timesLessGenericJ > 0 Then
                    Log("MORE TYPE GENERIC: Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidateI.DefinedParametersTypes), ArgumentsTypesAsString)
                    m_Candidates(i) = Nothing
                    Exit For
                End If

                Log("EQUALLY GENERIC: Method call to '{0}{1}' with arguments '{2}' and with arguments '{3}'", ArgumentsTypesAsString, Helper.ToString(candidateI.DefinedParametersTypes), Helper.ToString(candidateJ.DefinedParametersTypes))
            Next
        Next
    End Sub

    Sub RemoveInaccessible()
        For i As Integer = 0 To m_Candidates.Count - 1
            Dim candidate As MemberCandidate = m_Candidates(i)

            If candidate Is Nothing Then Continue For

            If candidate.IsAccessible = False Then
                Log("NOT ACCESSIBLE: Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidate.DefinedParametersTypes), ArgumentsTypesAsString)
                m_Candidates(i) = Nothing
            Else
                Log("ACCESSIBLE    : Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidate.DefinedParametersTypes), ArgumentsTypesAsString)
            End If
        Next
    End Sub

    Sub InferTypeArguments()
        If m_TypeArguments IsNot Nothing AndAlso m_TypeArguments.Count > 0 Then
            Log("Type arguments specified, not inferring them")
            Return
        End If

        For i As Integer = 0 To m_Candidates.Count - 1
            Dim candidate As MemberCandidate = m_Candidates(i)

            If candidate Is Nothing Then Continue For

            If candidate.InferTypeArguments = False Then
                Log("TYPE INFERENCE FAILED: Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidate.DefinedParametersTypes), ArgumentsTypesAsString)
                m_Candidates(i) = Nothing
            Else
                Log("TYPE INFERENCE PASSED: Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidate.DefinedParametersTypes), ArgumentsTypesAsString)
            End If
        Next
    End Sub

    Sub ExpandParamArrays()
        For i As Integer = 0 To m_Candidates.Count - 1
            Dim candidate As MemberCandidate = m_Candidates(i)

            If candidate Is Nothing Then Continue For

            candidate.ExpandParamArray()
        Next
    End Sub

    Sub RemoveInapplicable()
        For i As Integer = 0 To m_Candidates.Count - 1
            Dim candidate As MemberCandidate = m_Candidates(i)

            If candidate Is Nothing Then Continue For

            If candidate.DefineApplicability = False Then
                Log("NOT APPLICABLE: Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidate.DefinedParametersTypes), ArgumentsTypesAsString)
                m_Candidates(i) = Nothing
            Else
                Log("APPLICABLE    : Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidate.DefinedParametersTypes), ArgumentsTypesAsString)
            End If
        Next
    End Sub

    Sub RemoveNarrowingExceptObject()
        For i As Integer = 0 To m_Candidates.Count - 1
            Dim candidate As MemberCandidate = m_Candidates(i)

            If candidate Is Nothing Then Continue For

            If candidate.IsNarrowingExceptObject Then
                Log("NARROWING (EXCEPT OBJECT)    : Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidate.DefinedParametersTypes), ArgumentsTypesAsString)
                m_Candidates(i) = Nothing
            Else
                Log("NOT NARROWING (EXCEPT OBJECT): Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidate.DefinedParametersTypes), ArgumentsTypesAsString)
            End If
        Next
    End Sub

    Sub RemoveNarrowing()
        For i As Integer = 0 To m_Candidates.Count - 1
            Dim candidate As MemberCandidate = m_Candidates(i)

            If candidate Is Nothing Then Continue For

            If candidate.IsNarrowing Then
                Log("NARROWING    : Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidate.DefinedParametersTypes), ArgumentsTypesAsString)
                m_Candidates(i) = Nothing
            Else
                Log("NOT NARROWING: Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidate.DefinedParametersTypes), ArgumentsTypesAsString)
            End If
        Next
    End Sub

    Sub SelectMostApplicable()
        'Find most applicable methods.
        Dim expandedArgumentTypes(m_Candidates.Count - 1)() As Mono.Cecil.TypeReference

        For i As Integer = 0 To m_Candidates.Count - 1
            If m_Candidates(i) Is Nothing Then Continue For

            For j As Integer = i + 1 To m_Candidates.Count - 1
                If m_Candidates(j) Is Nothing Then Continue For

                Dim candidateI As MemberCandidate = m_Candidates(i)
                Dim candidateJ As MemberCandidate = m_Candidates(j)

                Helper.Assert(candidateI.ExactArguments IsNot Nothing)
                Helper.Assert(candidateJ.ExactArguments IsNot Nothing)

                Dim a, b As Boolean

                If expandedArgumentTypes(i) Is Nothing Then
                    expandedArgumentTypes(i) = candidateI.TypesInInvokedOrder() ' Helper.GetExpandedTypes(Compiler, candidateI.InputParameters, Arguments.Count)
                End If
                If expandedArgumentTypes(j) Is Nothing Then
                    expandedArgumentTypes(j) = candidateJ.TypesInInvokedOrder() 'Helper.GetExpandedTypes(Compiler, candidateJ.InputParameters, Arguments.Count)
                End If

                a = Helper.IsFirstMoreApplicable(m_Parent, Arguments.Arguments, expandedArgumentTypes(i), expandedArgumentTypes(j))
                b = Helper.IsFirstMoreApplicable(m_Parent, Arguments.Arguments, expandedArgumentTypes(j), expandedArgumentTypes(i))

                If a = b Then ' AndAlso b = False Then
                    'It is possible for M and N to have the same signature if one or both contains an expanded 
                    'paramarray parameter. In that case, the member with the fewest number of arguments matching
                    'expanded paramarray parameters is considered more applicable. 
                    Dim iParamArgs, jParamArgs As Integer

                    If candidateI.IsParamArrayCandidate Then
                        iParamArgs = candidateI.ParamArrayExpression.ArrayElementInitalizer.Initializers.Count + 1
                    End If
                    If candidateJ.IsParamArrayCandidate Then
                        jParamArgs = candidateJ.ParamArrayExpression.ArrayElementInitalizer.Initializers.Count + 1
                    End If
                    If jParamArgs > iParamArgs Then
                        a = True : b = False
                    ElseIf iParamArgs > jParamArgs Then
                        b = True : a = False
                    End If
                    Helper.Assert(iParamArgs <> jParamArgs OrElse (iParamArgs = 0 AndAlso jParamArgs = 0), MethodName)
                End If

                If a Xor b Then
                    If a = False Then
                        Log("NOT MOST APPLICABLE: Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidateI.DefinedParametersTypes), ArgumentsTypesAsString)
                        m_Candidates(i) = Nothing
                        Exit For
                    Else
                        Log("NOT MOST APPLICABLE: Method call to '{0}{1}' with arguments '{2}'", Helper.ToString(candidateJ.DefinedParametersTypes), ArgumentsTypesAsString)
                        m_Candidates(j) = Nothing
                    End If
                Else
                    Log("EQUALLY APPLICABLE: Method call to '{0}{1}' with arguments '{2}' and with arguments '{3}'", ArgumentsTypesAsString, Helper.ToString(candidateI.DefinedParametersTypes), Helper.ToString(candidateJ.DefinedParametersTypes))
                End If
            Next
        Next
    End Sub

    ReadOnly Property Resolved() As Boolean
        Get
            Return m_ResolvedCandidate IsNot Nothing
        End Get
    End Property

    ReadOnly Property ResolvedCandidate() As MemberCandidate
        Get
            Return m_ResolvedCandidate
        End Get
    End Property

    ReadOnly Property ResolvedMember() As Mono.Cecil.MemberReference
        Get
            If m_ResolvedCandidate Is Nothing Then Return Nothing
            Return m_ResolvedCandidate.Member
        End Get
    End Property

    ReadOnly Property ResolvedMethod() As Mono.Cecil.MethodReference
        Get
            Return TryCast(ResolvedMember, Mono.Cecil.MethodReference)
        End Get
    End Property

    ReadOnly Property ResolvedConstructor() As Mono.Cecil.MethodReference
        Get
            Return TryCast(ResolvedMember, Mono.Cecil.MethodReference)
        End Get
    End Property

    ReadOnly Property ResolvedProperty() As Mono.Cecil.PropertyReference
        Get
            Return TryCast(ResolvedMember, Mono.Cecil.PropertyReference)
        End Get
    End Property

    <Diagnostics.Conditional("DEBUG")> _
    Sub Log(ByVal Format As String, Optional ByVal P1 As Object = Nothing, Optional ByVal P2 As Object = Nothing, Optional ByVal P3 As Object = Nothing)
        If LOGMETHODRESOLUTION Then
            Dim msg As String
            msg = String.Format(Format, MethodName, P1, P2, P3)
            Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, msg)
        End If
    End Sub
End Class

Public Class MemberCandidate
    Private m_Member As Mono.Cecil.MemberReference
    Private m_DefinedParameters As Mono.Collections.Generic.Collection(Of ParameterDefinition)
    Private m_DefinedParametersTypes As Mono.Cecil.TypeReference()
    Private m_Parent As MethodResolver
    Private m_ReturnType As TypeReference

    Private m_ExactArguments As Generic.List(Of Argument)
    Private m_TypesInInvokedOrder As Mono.Cecil.TypeReference()

    Private m_IsParamArray As Boolean

    Public Overrides Function ToString() As String
        Return Helper.ToString(m_Parent.Parent, m_Member)
    End Function

    Sub New(ByVal Parent As MethodResolver, ByVal Member As Mono.Cecil.MemberReference)
        m_Parent = Parent
        m_Member = Member
    End Sub

    ReadOnly Property TypesInInvokedOrder() As Mono.Cecil.TypeReference()
        Get
            Return m_TypesInInvokedOrder
        End Get
    End Property

    ReadOnly Property ExactArguments() As Generic.List(Of Argument)
        Get
            Return m_ExactArguments
        End Get
    End Property

    ReadOnly Property DefinedParameters() As Mono.Collections.Generic.Collection(Of ParameterDefinition)
        Get
            If m_DefinedParameters Is Nothing Then m_DefinedParameters = Helper.GetParameters(Compiler, Member)
            Return m_DefinedParameters
        End Get
    End Property

    ReadOnly Property DefinedParametersTypes() As Mono.Cecil.TypeReference()
        Get
            If m_DefinedParametersTypes Is Nothing Then m_DefinedParametersTypes = Helper.GetTypes(DefinedParameters)
            Return m_DefinedParametersTypes
        End Get
    End Property

    ReadOnly Property ReturnType As TypeReference
        Get
            If m_ReturnType Is Nothing Then m_ReturnType = Helper.GetReturnType(m_Member)
            Return m_ReturnType
        End Get
    End Property

    ReadOnly Property Compiler() As Compiler
        Get
            Return m_Parent.Compiler
        End Get
    End Property

    ReadOnly Property Member() As Mono.Cecil.MemberReference
        Get
            Return m_Member
        End Get
    End Property

    ReadOnly Property Resolver() As MethodResolver
        Get
            Return m_Parent
        End Get
    End Property

    ReadOnly Property Parent() As ParsedObject
        Get
            Return m_Parent.Parent
        End Get
    End Property

    ReadOnly Property IsParamArrayCandidate() As Boolean
        Get
            Return m_IsParamArray
        End Get
    End Property

    ReadOnly Property ParamArrayExpression() As ArrayCreationExpression
        Get
            If m_IsParamArray = False Then Return Nothing
            Return DirectCast(m_ExactArguments(m_ExactArguments.Count - 1).Expression, ArrayCreationExpression)
        End Get
    End Property

    ReadOnly Property IsAccessible() As Boolean
        Get
            If Resolver.Caller Is Nothing Then
                Return Helper.IsAccessibleExternal(Compiler, m_Member)
            Else
                Return Helper.IsAccessible(Compiler, Resolver.Caller.CecilType, m_Member)
            End If
        End Get
    End Property

    ReadOnly Property IsNarrowingExceptObject() As Boolean
        Get
            Return IsNarrowingInternal(True)
        End Get
    End Property

    ReadOnly Property IsNarrowing() As Boolean
        Get
            Return IsNarrowingInternal(False)
        End Get
    End Property

    Private Function IsNarrowingInternal(ByVal ExceptObject As Boolean) As Boolean
        For j As Integer = 0 To InputParameters.Count - 1
            Dim arg As Argument
            Dim param As Mono.Cecil.ParameterDefinition
            Dim IsConvertible As Boolean
            Dim elementType As Mono.Cecil.TypeReference
            Dim initializer As Expression

            param = InputParameters(j)
            arg = ExactArguments(j)

            If ExceptObject AndAlso Helper.CompareType(arg.Expression.ExpressionType, Compiler.TypeCache.System_Object) Then Continue For

            If m_IsParamArray AndAlso j = InputParameters.Count - 1 AndAlso ParamArrayExpression IsNot Nothing Then
                'To match the automatically created array for the paramarray parameter each argument has to be 
                'implicitly convertible to the element type of the paramarray parameter type.
                IsConvertible = True
                elementType = CType(param.ParameterType, Mono.Cecil.ArrayType).ElementType
                For k As Integer = 0 To ParamArrayExpression.ArrayElementInitalizer.Initializers.Count - 1
                    initializer = ParamArrayExpression.ArrayElementInitalizer.Initializers(k).AsRegularInitializer
                    IsConvertible = IsConvertible AndAlso Compiler.TypeResolution.IsImplicitlyConvertible(arg, initializer.ExpressionType, elementType)
                Next
            Else
                IsConvertible = Compiler.TypeResolution.IsImplicitlyConvertible(arg, arg.Expression.ExpressionType, param.ParameterType)
            End If

            If IsConvertible = False Then
                Return True
            End If
        Next

        Return False
    End Function

    ReadOnly Property Arguments() As ArgumentList
        Get
            Return Resolver.Arguments
        End Get
    End Property

    ReadOnly Property TypeArguments() As TypeArgumentList
        Get
            Return Resolver.TypeArguments
        End Get
    End Property

    ReadOnly Property InputParameters() As Mono.Collections.Generic.Collection(Of ParameterDefinition)
        Get
            Return DefinedParameters
        End Get
    End Property

    ReadOnly Property ParamArrayParameter() As Mono.Cecil.ParameterDefinition
        Get
            If m_IsParamArray = False Then Return Nothing
            Return m_DefinedParameters(m_DefinedParameters.Count - 1)
        End Get
    End Property

    Private Sub CollectGenericParameters(ByVal Type As GenericInstanceType, ByVal Find As Mono.Collections.Generic.Collection(Of GenericParameter), ByRef collected As Generic.List(Of GenericParameter))
        Dim elementType As GenericInstanceType

        If Type Is Nothing Then Return

        If Type.HasGenericArguments Then
            For i As Integer = 0 To Find.Count - 1
                If Type.GenericArguments.Contains(Find(i)) Then
                    If collected Is Nothing Then collected = New Generic.List(Of GenericParameter)
                    If collected.Contains(Find(i)) Then Continue For
                    collected.Add(Find(i))
                End If
            Next
        End If

        elementType = TryCast(Type.GetElementType(), GenericInstanceType)
        If elementType IsNot Nothing AndAlso elementType IsNot Type Then
            CollectGenericParameters(elementType, Find, collected)
        End If
    End Sub

    Function InferTypeArguments() As Boolean
        Dim GenericParameters As Mono.Collections.Generic.Collection(Of GenericParameter)
        Dim methodDef As MethodDefinition = TryCast(m_Member, MethodDefinition)
        Dim methodRef As MethodReference

        If methodDef Is Nothing Then
            methodRef = TryCast(m_Member, MethodReference)
            If methodRef IsNot Nothing Then
                methodDef = CecilHelper.FindDefinition(methodRef)
            End If
        End If

        If methodDef IsNot Nothing Then
            If Not methodDef.HasGenericParameters Then Return True
            GenericParameters = methodDef.GenericParameters
        Else
            Return True
        End If

        If DefinedParameters.Count <> Arguments.Count Then
            If m_Parent.ShowErrors Then Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Parent.Location)
            Return False
        End If

        '* Generate a dependency graph *
        ' Given a set of arguments A1, A2, …, AN, a set of matching parameters P1, P2, …, PN and a set of method type parameters 
        ' T1, T2, …, TN, the dependencies between the arguments and method type parameters are first collected as follows:

        Dim A_deps() As Generic.List(Of GenericParameter) = Nothing
        Dim A_dep As Generic.List(Of GenericParameter)

        For i As Integer = 0 To Arguments.Count - 1
            Dim An As Argument = Arguments(i)
            Dim Pn As ParameterDefinition = m_DefinedParameters(i)
            Dim git As GenericInstanceType
            Dim gp As GenericParameter

            '•	If AN is the Nothing literal, no dependencies are generated.
            If TypeOf An.Expression Is NothingConstantExpression Then Continue For

            git = TryCast(Pn.ParameterType, GenericInstanceType)
            gp = TryCast(Pn.ParameterType, GenericParameter)

            '•	If AN is a lambda method and the type of PN is a constructed delegate type 
            '   or System.Linq.Expressions.Expression(Of T), where T is a constructed delegate type,
            '   •	If the type of a lambda method parameter will be inferred from the type of the corresponding parameter PN, and the type of the parameter depends on a method type parameter TN, then AN has a dependency on TN.
            '   •	If the type of a lambda method parameter is specified and the type of the corresponding parameter PN depends on a method type parameter TN, then TN has a dependency on AN.
            '   •	If the return type of PN depends on a method type parameter TN, then TN has a dependency on AN.
            '* Lambda methods haven't been implemented yet *

            '•	If AN is a method pointer and the type of PN is a constructed delegate type,
            '   •	If the return type of PN depends on a method type parameter TN, then TN has a dependency on AN.
            If An.Expression.Classification.IsMethodPointerClassification AndAlso Helper.IsDelegate(Compiler, Pn.ParameterType) AndAlso git IsNot Nothing Then
                Dim invokeMethod As MethodReference = Helper.GetInvokeMethod(Compiler, Pn.ParameterType)
                A_dep = Nothing
                CollectGenericParameters(TryCast(invokeMethod.ReturnType, GenericInstanceType), GenericParameters, A_dep)
                If A_dep IsNot Nothing Then
                    If A_deps Is Nothing Then ReDim A_deps(Arguments.Count - 1)
                    A_deps(i) = A_dep
                End If
                Continue For
            End If

            '•	If PN is a constructed type and the type of PN depends on a method type parameter TN, then TN has a dependency on AN.
            If git IsNot Nothing Then
                A_dep = Nothing
                CollectGenericParameters(git, GenericParameters, A_dep)
                If A_dep IsNot Nothing Then
                    If A_deps Is Nothing Then ReDim A_deps(Arguments.Count - 1)
                    A_deps(i) = A_dep
                End If
                Continue For
            ElseIf gp IsNot Nothing AndAlso GenericParameters.Contains(gp) Then
                If A_deps Is Nothing Then ReDim A_deps(Arguments.Count - 1)
                A_dep = New Generic.List(Of GenericParameter)
                A_dep.Add(gp)
                A_deps(i) = A_dep
                Continue For
            End If

            '•	Otherwise, no dependency is generated.
        Next

        ' After collecting dependencies, any arguments that have no dependencies are eliminated.
        '* eliminated arguments are represented by null entries in the A_deps array

        ' If any method type parameters have no outgoing dependencies (i.e. the method type parameter does not depend on an argument), then type inference fails. 
        If A_deps Is Nothing Then
            If m_Parent.ShowErrors Then Compiler.Report.ShowMessage(Messages.VBNC32050, Me.Parent.Location, GenericParameters(0).Name, Helper.ToString(Compiler, m_Member))
            Return False '* No dependencies at all
        End If
        For i As Integer = 0 To GenericParameters.Count - 1
            Dim found As Boolean = False
            For a As Integer = 0 To A_deps.Length - 1
                If A_deps(a) Is Nothing Then Continue For
                If A_deps(a).Contains(GenericParameters(i)) Then
                    found = True
                    Exit For
                End If
            Next
            If Not found Then
                If m_Parent.ShowErrors Then Compiler.Report.ShowMessage(Messages.VBNC32050, Me.Parent.Location, GenericParameters(i).Name, Helper.ToString(Compiler, m_Member))
                Return False '* GenericParameter(i) does not have a dependency
            End If
        Next

        Dim hints As TypeHints()
        ReDim hints(GenericParameters.Count - 1)

        ' Otherwise, the remaining arguments and method type parameters are grouped into strongly connected components.
        ' A strongly connected component is a set of arguments and method type parameters,
        ' where any element in the component is reachable via dependencies on other elements.
        ' The strongly connected components are then topologically sorted and processed in topological order:
        '* Since we don't have lambda methods yet, we only have a tree of dependencies (method type parameters on arguments)
        '* We'll always have one element in each the stronly connected component, so just look over each argument and each method type parameter

        '•	If the strongly typed component contains only one element,
        '   •	If the element has already been marked complete, skip it.
        '   •	If the element is an argument, then add type hints from the argument to the method type parameters 
        '       that depend on it and mark the element as complete. If the argument is a lambda method with parameters 
        '       that still need inferred types, then infer Object for the types of those parameters.
        For i As Integer = 0 To A_deps.Length - 1
            Dim Ta As TypeReference
            Dim Tp As TypeReference
            If A_deps(i) Is Nothing Then Continue For

            Ta = Arguments(i).Expression.ExpressionType
            Tp = DefinedParameters(i).ParameterType
            For a As Integer = 0 To A_deps(i).Count - 1
                Dim Tg As GenericParameter = A_deps(i)(a)
                Dim aI As Integer = GenericParameters.IndexOf(Tg)
                Dim hint As TypeHints = hints(aI)

                If hints(aI) Is Nothing Then
                    hint = New TypeHints(Me)
                    hints(aI) = hint
                End If

                If hint.GenerateHint(GenericParameters, Ta, DefinedParameters(i)) = False Then
                    If m_Parent.ShowErrors Then Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Parent.Location)
                    Return False
                End If

            Next
            '       If the argument is a lambda method with parameters 
            '       that still need inferred types, then infer Object for the types of those parameters.
            '* no lambda method support yet *
        Next

        '   •	If the element is a method type parameter, then infer the method type parameter to be the dominant
        '       type among the argument type hints and mark the element as complete. If a type hint has an array element 
        '       restriction on it, then only conversions that are valid between arrays of the given type are considered 
        '       (i.e. covariant and intrinsic array conversions). If a type hint has a generic argument restriction on it, 
        '       then only identity conversions are considered. If no dominant type can be chosen, inference fails. 
        '       If any lambda method argument types depend on this method type parameter, the type is propagated to the lambda method.
        Dim m_InferredTypeArguments As New Mono.Collections.Generic.Collection(Of TypeReference)(GenericParameters.Count)
        For i As Integer = 0 To GenericParameters.Count - 1
            Dim hint As TypeHints = hints(i)
            Dim types As Generic.List(Of TypeReference)
            Dim dominantType As TypeReference
            Dim generic_argument_restriction As Boolean
            Dim array_element_restriction As Boolean
            types = New Generic.List(Of TypeReference)(hint.Hints.Count)

            For h As Integer = 0 To hint.Hints.Count - 1
                Dim hi As TypeHint = hint.Hints(h)
                If hi.GenericArgumentRestriction Then
                    generic_argument_restriction = True
                ElseIf hi.ArrayElementRestriction Then
                    array_element_restriction = True
                End If

                Dim found As Boolean
                For t As Integer = 0 To types.Count - 1
                    If Helper.CompareType(types(t), hi.Hint) Then
                        found = True
                        Exit For
                    End If
                Next
                If Not found Then types.Add(hi.Hint)
            Next

            If types.Count = 0 Then
                If m_Parent.ShowErrors Then Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Parent.Location)
                Return False
            End If

            If generic_argument_restriction Then
                'all types must be identical
                dominantType = types(0)
                For t As Integer = 1 To types.Count - 1
                    If Helper.CompareType(types(t), dominantType) = False Then
                        If m_Parent.ShowErrors Then Compiler.Report.ShowMessage(Messages.VBNC36657, Me.Parent.Location, Helper.ToString(Compiler, m_Member))
                        Return False
                    End If
                Next
            ElseIf array_element_restriction Then
                Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Parent.Location, "Type argument inference with array element restriction")
            Else
                dominantType = Helper.GetDominantType(Compiler, types)
            End If

            If dominantType Is Nothing Then
                If m_Parent.ShowErrors Then Compiler.Report.ShowMessage(Messages.VBNC36651, Me.Parent.Location, Helper.ToString(Compiler, m_Member))
                Return False
            End If
            m_InferredTypeArguments.Add(dominantType)
        Next

        '•	If the strongly typed component contains more than one element, then the component contains a cycle.
        '   •	For each method type parameter that is an element in the component, if the method type parameter depends
        '       on an argument that is not marked complete, convert that dependency into an assertion that will be 
        '       checked at the end of the inference process.
        '   •	Restart the inference process at the point at which the strongly typed components were determined.
        '* This can't happen until lambda methods have been implemented, since only lambda methods can have a dependency 
        '* from method type parameters to arguments, so until then this is just a tree of arguments to method type parameters, 
        '* not a cyclic graph *

        'If type inference succeeds for all of the method type parameters, then any dependencies that were changed 
        'into assertions are checked. An assertion succeeds if the type of the argument is implicitly convertible 
        'to the inferred type of the method type parameter. If an assertion fails, then type argument inference fails.
        '* no assertions are added until lambda support has been implemented, so nothing to do here for now *

        '* type inference succeeded, inflate our method *
        Dim inflated_method As MethodReference = CecilHelper.GetCorrectMember(methodDef, m_InferredTypeArguments)
        Dim gim As New GenericInstanceMethod(inflated_method)
        gim.GenericArguments.AddRange(m_InferredTypeArguments)
        m_Member = gim
        m_DefinedParameters = Nothing
        m_DefinedParametersTypes = Nothing
        m_TypesInInvokedOrder = Nothing

        If DefineApplicability() = False Then
            Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Parent.Location, "Applicability changed after inferring type arguments")
        End If

        'The success of type inference does not, in and of itself, guarantee that the method is applicable.
        Return True
    End Function

    Class TypeHint
        Public Hint As TypeReference
        Public ArrayElementRestriction As Boolean
        Public GenericArgumentRestriction As Boolean
    End Class

    Class TypeHints
        Private m_Candidate As MemberCandidate
        Private m_Hints As New Generic.List(Of TypeHint)

        ReadOnly Property Hints As Generic.List(Of TypeHint)
            Get
                Return m_Hints
            End Get
        End Property

        Public Sub New(ByVal Candidate As MemberCandidate)
            m_Candidate = Candidate
        End Sub

        Private Function InvolvesMethodTypeParameters(ByVal GenericParameters As Mono.Collections.Generic.Collection(Of GenericParameter), ByVal type As TypeReference) As Boolean
            Dim elementType As GenericInstanceType
            Dim genericParam As GenericParameter
            Dim git As GenericInstanceType

            If type Is Nothing Then Return False

            genericParam = TryCast(type, GenericParameter)
            If genericParam IsNot Nothing AndAlso GenericParameters.Contains(genericParam) Then Return True

            git = TryCast(type, GenericInstanceType)
            If git Is Nothing Then Return False

            For i As Integer = 0 To GenericParameters.Count - 1
                If git.GenericArguments.Contains(GenericParameters(i)) Then Return True
            Next

            elementType = TryCast(type.GetElementType(), GenericInstanceType)
            If elementType IsNot type Then Return InvolvesMethodTypeParameters(GenericParameters, elementType)

            Return False
        End Function

        Public Function GenerateHint(ByVal GenericParameters As Mono.Collections.Generic.Collection(Of GenericParameter), ByVal argument_type As TypeReference, ByVal parameter As ParameterDefinition) As Boolean
            Return GenerateHint(GenericParameters, argument_type, parameter.ParameterType, parameter, False, False)
        End Function

        Private Function GenerateHint(ByVal GenericParameters As Mono.Collections.Generic.Collection(Of GenericParameter), ByVal argument_type As TypeReference, ByVal parameter_type As TypeReference, ByVal parameter As ParameterDefinition, ByVal array_element_restriction As Boolean, ByVal generic_argument_restriction As Boolean) As Boolean

            'Given an argument type TA for an argument A and a parameter type TP for a parameter P, type hints are generated as follows:
            '•	If TP does not involve any method type parameters then no hints are generated.
            If InvolvesMethodTypeParameters(GenericParameters, parameter_type) = False Then Return True

            '•	If TP and TA are array types of the same rank, then replace TA and TP with the element types of 
            '   TA and TP and restart this process with an array element restriction.
            Dim arrayA As ArrayType = TryCast(argument_type, ArrayType)
            Dim arrayP As ArrayType = TryCast(parameter_type, ArrayType)
            If arrayA IsNot Nothing AndAlso arrayP IsNot Nothing Then
                If arrayA.Rank = arrayP.Rank Then
                    Return GenerateHint(GenericParameters, arrayA.ElementType, arrayP.ElementType, parameter, True, generic_argument_restriction)
                End If
            End If

            '•	If TP is a method type parameter, then TA is added as a type hint with the current restriction, if any.
            Dim gp As GenericParameter = TryCast(parameter_type, GenericParameter)
            If gp IsNot Nothing AndAlso GenericParameters.Contains(gp) Then
                Dim hint As TypeHint = New TypeHint()
                hint.ArrayElementRestriction = array_element_restriction
                hint.GenericArgumentRestriction = generic_argument_restriction
                hint.Hint = argument_type
                m_Hints.Add(hint)
                Return True
            End If

            '•	If A is a lambda method and TP is a constructed delegate type or System.Linq.Expressions.Expression(Of T), 
            '   where T is a constructed delegate type, for each lambda method parameter type TL and corresponding 
            '   delegate parameter type TD, replace TA with TL and TP with TD and restart the process with no restriction. 
            '   Then replace TA with the return type of the lambda method and TP with the return type of the delegate type 
            '   and restart the process with no restriction.
            '* no lambda support yet *

            '•	If A is a method pointer and TP is a constructed delegate type, use the parameter types of TP to determine 
            '   which method pointed is most applicable to TP. If there is a method that is most applicable, replace TA with 
            '   the return type of the method and TP with the return type of the delegate type and restart the process with 
            '   no restriction.
            '* TODO *

            '•	Otherwise, TP must be a constructed type. Given TG, the generic type of TP,
            '   •	If TA is TG, inherits from TG, or implements the type TG exactly once, then for each matching 
            '       type argument TAX from TA and TPX from TP, replace TA with TAX and TP with TPX and restart 
            '       the process with a generic argument restriction.
            Dim tp_git As GenericInstanceType = TryCast(parameter_type, GenericInstanceType)
            Dim ta_git As GenericInstanceType = TryCast(argument_type, GenericInstanceType)
            If tp_git IsNot Nothing AndAlso ta_git IsNot Nothing Then
                Dim restart As Boolean
                Dim implement_count As Integer
                Dim tp_td As TypeDefinition = CecilHelper.FindDefinition(tp_git)
                Dim ta_td As TypeDefinition = CecilHelper.FindDefinition(ta_git)
                Dim base As TypeReference = ta_td
                Dim base_td As TypeDefinition
                While base IsNot Nothing AndAlso restart = False
                    If base Is tp_td Then restart = True
                    base_td = CecilHelper.FindDefinition(base)
                    If base_td.HasInterfaces Then
                        For i As Integer = 0 To base_td.Interfaces.Count - 1
                            If base_td.Interfaces(i) Is tp_git Then implement_count += 1
                        Next
                    End If

                    base = base_td.BaseType
                End While
                If restart = False Then
                    restart = implement_count = 1
                End If
                If restart Then
                    For i As Integer = 0 To ta_git.GenericArguments.Count - 1
                        If GenerateHint(GenericParameters, ta_git.GenericArguments(i), tp_git.GenericArguments(i), parameter, array_element_restriction, True) = False Then
                            Return False
                        End If
                    Next
                End If
                Return True
            End If

            '•	Otherwise, type inference fails for the generic method.
            Return False
        End Function
    End Class

    Sub ExpandParamArray()
        If DefinedParameters.Count = 0 Then Return
        If Helper.IsParamArrayParameter(Compiler, DefinedParameters(DefinedParameters.Count - 1)) = False Then Return

        Dim candidate As New MemberCandidate(Resolver, m_Member)
        candidate.m_IsParamArray = True
        Resolver.Candidates.Add(candidate)
    End Sub

    Function DefineApplicability() As Boolean
        Dim matchedParameters As Generic.List(Of Mono.Cecil.ParameterReference)
        Dim exactArguments As Generic.List(Of Argument)
        Dim method As Mono.Cecil.MethodReference = TryCast(Member, Mono.Cecil.MethodReference)
        Dim prop As Mono.Cecil.PropertyReference = TryCast(Member, Mono.Cecil.PropertyReference)

        Dim isLastParamArray As Boolean
        Dim paramArrayExpression As ArrayCreationExpression = Nothing
        Dim inputParametersCount As Integer = InputParameters.Count

        isLastParamArray = m_IsParamArray

        '(if there are more arguments than parameters and the last parameter is not a 
        'paramarray parameter the method should not be applicable)
        If Arguments.Count > InputParameters.Count Then
            If InputParameters.Count < 1 Then
                'LogResolutionMessage(Parent.Compiler, "N/A: 1")
                Return False
            End If
            If isLastParamArray = False Then
                'LogResolutionMessage(Parent.Compiler, "N/A: 2")
                Return False
            End If
        End If

        matchedParameters = New Generic.List(Of Mono.Cecil.ParameterReference)
        exactArguments = New Generic.List(Of Argument)(Helper.CreateArray(Of Argument)(Nothing, inputParametersCount))

        ReDim m_TypesInInvokedOrder(Math.Max(Arguments.Count - 1, inputParametersCount - 1))

        If isLastParamArray Then
            Dim paramArrayArg As New PositionalArgument(Parent)

            Helper.Assert(paramArrayExpression Is Nothing)
            paramArrayExpression = New ArrayCreationExpression(paramArrayArg)
            paramArrayExpression.Init(ParamArrayParameter.ParameterType, New Expression() {})

            paramArrayArg.Init(ParamArrayParameter.Sequence, paramArrayExpression)
            exactArguments(inputParametersCount - 1) = paramArrayArg

            m_TypesInInvokedOrder(inputParametersCount - 1) = ParamArrayParameter.ParameterType
        End If

        Dim firstNamedArgument As Integer = Arguments.Count + 1
        For i As Integer = 0 To Arguments.Count - 1
            'First, match each positional argument in order to the list of method parameters. 
            'If there are more positional arguments than parameters and the last parameter 
            'is not a paramarray, the method is not applicable. Otherwise, the paramarray parameter 
            'is expanded with parameters of the paramarray element type to match the number
            'of positional arguments. If a positional argument is omitted, the method is not applicable.
            If Arguments(i).IsNamedArgument Then
                firstNamedArgument = i
                Exit For '(No more positional arguments)
            End If

            If inputParametersCount - 1 < i Then
                '(more positional arguments than parameters)
                If isLastParamArray = False Then '(last parameter is not a paramarray)
                    'LogResolutionMessage(Parent.Compiler, "N/A: 3")
                    Return False
                End If

                'Add the additional expressions to the param array creation expression.
                Helper.Assert(paramArrayExpression.ArrayElementInitalizer.Initializers.Count = 1)
                For j As Integer = i To Arguments.Count - 1
                    'A paramarray element has to be specified.
                    If Arguments(j).Expression Is Nothing Then
                        'LogResolutionMessage(Parent.Compiler, "N/A: 4")
                        Return False
                    End If
                    paramArrayExpression.ArrayElementInitalizer.AddInitializer(Arguments(j).Expression)

                    Helper.Assert(m_TypesInInvokedOrder(j) Is Nothing)
                    m_TypesInInvokedOrder(j) = CecilHelper.GetElementType(ParamArrayParameter.ParameterType)
                Next
                Exit For
            Else
                matchedParameters.Add(InputParameters(i))

                'Helper.Assert(m_TypesInInvokedOrder(i) Is Nothing)
                m_TypesInInvokedOrder(i) = InputParameters(i).ParameterType

                'Get the default value of the parameter if the specified argument has no expression.
                Dim arg As Argument = Nothing
                If Arguments(i).Expression Is Nothing Then
                    If InputParameters(i).IsOptional = False Then
                        Helper.Assert(False)
                    Else
                        Dim exp As Expression
                        Dim pArg As New PositionalArgument(Parent)
                        exp = Helper.GetOptionalValueExpression(pArg, InputParameters(i))
                        pArg.Init(InputParameters(i).Sequence, exp)
                        arg = pArg
                    End If
                Else
                    arg = Arguments(i)
                End If

                If isLastParamArray = False Then exactArguments(i) = arg
                If isLastParamArray AndAlso inputParametersCount - 1 = i Then
                    Helper.Assert(paramArrayExpression.ArrayElementInitalizer.Initializers.Count = 0)
                    paramArrayExpression.ArrayElementInitalizer.AddInitializer(arg.Expression)
                    'Helper.Assert(m_TypesInInvokedOrder(i) Is Nothing)
                    m_TypesInInvokedOrder(i) = CecilHelper.GetElementType(ParamArrayParameter.ParameterType)
                Else
                    If isLastParamArray Then exactArguments(i) = arg
                End If
            End If
            '??? If a positional argument is omitted, the method is not applicable.
        Next


        For i As Integer = firstNamedArgument To Arguments.Count - 1
            Helper.Assert(Arguments(i).IsNamedArgument)

            'Next, match each named argument to a parameter with the given name. 
            'If one of the named arguments fails to match, matches a paramarray parameter, 
            'or matches an argument already matched with another positional or named argument,
            'the method is not applicable.

            Dim namedArgument As NamedArgument = DirectCast(Arguments(i), NamedArgument)

            Dim matched As Boolean = False
            For j As Integer = 0 To inputParametersCount - 1
                'Next, match each named argument to a parameter with the given name. 
                Dim inputParam As Mono.Cecil.ParameterReference = InputParameters(j)
                If Helper.CompareName(inputParam.Name, namedArgument.Name) Then
                    If matchedParameters.Contains(inputParam) Then
                        'If one of the named arguments (...) matches an argument already matched with 
                        'another positional or named argument, the method is not applicable
                        'LogResolutionMessage(Parent.Compiler, "N/A: 5")
                        Return False
                    ElseIf Helper.IsParamArrayParameter(Parent.Compiler, inputParam) Then
                        'If one of the named arguments (...) matches a paramarray parameter, 
                        '(...) the method is not applicable.
                        'LogResolutionMessage(Parent.Compiler, "N/A: 6")
                        Return False
                    Else
                        matchedParameters.Add(inputParam)
                        exactArguments(j) = Arguments(i)

                        Helper.Assert(m_TypesInInvokedOrder(j) Is Nothing)
                        m_TypesInInvokedOrder(j) = inputParam.ParameterType
                        matched = True
                        Exit For
                    End If
                End If
            Next
            'If one of the named arguments fails to match (...) the method is not applicable
            If matched = False Then
                'LogResolutionMessage(Parent.Compiler, "N/A: 7")
                Return False
            End If
        Next

        'Next, if parameters that have not been matched are not optional, 
        'the method is not applicable. If optional parameters remain, the default value 
        'specified in the optional parameter declaration is matched to the parameter. 
        'If an Object parameter does not specify a default value, then the expression 
        'System.Reflection.Missing.Value is used. If an optional Integer parameter 
        'has the Microsoft.VisualBasic.CompilerServices.OptionCompareAttribute attribute, 
        'then the literal 1 is supplied for text comparisons and the literal 0 otherwise.

        For i As Integer = 0 To inputParametersCount - 1
            If matchedParameters.Contains(InputParameters(i)) = False Then
                'if parameters that have not been matched are not optional, the method is not applicable
                If isLastParamArray = False AndAlso Helper.IsParamArrayParameter(Compiler, InputParameters(i)) Then
                    Return False
                End If
                If InputParameters(i).IsOptional = False AndAlso InputParameters(i) Is ParamArrayParameter = False Then
                    'LogResolutionMessage(Parent.Compiler, "N/A: 8")
                    Return False
                End If

                Dim exp As Expression
                Dim arg As New PositionalArgument(Parent)
                exp = Helper.GetOptionalValueExpression(arg, InputParameters(i))
                arg.Init(InputParameters(i).Sequence, exp)
                If isLastParamArray = False Then
                    Helper.Assert(m_TypesInInvokedOrder(i) Is Nothing)
                    m_TypesInInvokedOrder(i) = InputParameters(i).ParameterType
                    exactArguments(i) = arg
                End If
                If Helper.IsParamArrayParameter(Parent.Compiler, InputParameters(i)) = False Then
                    'he arraycreation has already been created and added to the exactArguments(1).
                    If isLastParamArray Then exactArguments(i) = arg
                End If
            End If
        Next

        'Finally, if type arguments have been specified, they are matched against
        'the type parameter list. If the two lists do not have the same number of elements, 
        'the method is not applicable, unless the type argument list is empty. If the 
        'type argument list is empty, type inferencing is used to try and infer 
        'the type argument list. If type inferencing fails, the method is not applicable.
        'Otherwise, the type arguments are filled in the place of the 
        'type parameters in the signature.
        Dim genericTypeArgumentCount As Integer
        Dim genericTypeArguments As Mono.Collections.Generic.Collection(Of TypeReference)
        If method IsNot Nothing AndAlso CecilHelper.IsGenericMethod(method) Then
            genericTypeArguments = CecilHelper.GetGenericArguments(method)
            genericTypeArgumentCount = genericTypeArguments.Count
        ElseIf prop IsNot Nothing Then
            'property cannot be generic.
        End If

        If genericTypeArgumentCount > 0 AndAlso (TypeArguments Is Nothing OrElse TypeArguments.List.Count = 0) Then
            'If the Then type argument list is empty, type inferencing is used to try and infer 
            'the type argument list.
            'Helper.NotImplementedYet("Type argument inference")
        ElseIf TypeArguments IsNot Nothing AndAlso TypeArguments.List.Count > 0 Then
            'If the two lists do not have the same number of elements, the method is not applicable
            If TypeArguments.List.Count <> genericTypeArgumentCount Then
                'LogResolutionMessage(Parent.Compiler, "N/A: 9")
                Return False
            End If

            'Return m_Parent.Compiler.Report.ShowMessage(Messages.VBNC99997, m_Parent.Parent.Location)
            'Helper.NotImplemented("Type argument matching")
        End If

        m_ExactArguments = exactArguments

        Helper.AssertNotNothing(m_TypesInInvokedOrder)

        If ResolveUnresolvedExpressions() = False Then
            Return False
        End If

        Return True 'Method is applicable!!
    End Function

    Function ResolveUnresolvedExpressions() As Boolean
        Dim result As Boolean = True

        For i As Integer = 0 To m_ExactArguments.Count - 1
            Dim exp As Expression
            Dim expType As Mono.Cecil.TypeReference

            exp = m_ExactArguments(0).Expression
            expType = exp.ExpressionType

            If Helper.CompareType(expType, Compiler.TypeCache.DelegateUnresolvedType) = False Then Continue For
            If Helper.IsDelegate(Compiler, DefinedParameters(i).ParameterType) = False Then Return False

            Dim aoe As AddressOfExpression
            aoe = TryCast(exp, AddressOfExpression)

            If aoe IsNot Nothing Then
                Dim exp2 As Expression
                exp2 = exp.ReclassifyMethodPointerToValueExpression(DefinedParameters(i).ParameterType)
                result = exp2.ResolveExpression(ResolveInfo.Default(Compiler)) AndAlso result
                If result Then m_ExactArguments(0).Expression = exp2
            End If
        Next

        Return result
    End Function

    Sub SelectOutputArguments()
        If IsParamArrayCandidate Then
            Dim ace As ArrayCreationExpression
            ace = ParamArrayExpression ' TryCast(OutputArguments.Item(OutputArguments.Count - 1).Expression, ArrayCreationExpression)
            If ace IsNot Nothing AndAlso ace.IsResolved = False AndAlso Helper.IsParamArrayParameter(Compiler, InputParameters(InputParameters.Count - 1)) Then
                If ace.ResolveExpression(ResolveInfo.Default(Compiler)) = False Then
                    Helper.ErrorRecoveryNotImplemented(Parent.Location)
                End If
            End If
        End If
    End Sub
End Class