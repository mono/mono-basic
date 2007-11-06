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
''' A set of properties overloaded on the same name.
''' A property group may have an associated instance expression.
''' 
''' Can be reclassified as a property access. The property group expression is
''' interpreted as an index expression with empty parenthesis (that is, 
''' "f" is interpreted "f()")
''' </summary>
''' <remarks></remarks>
Public Class PropertyGroupClassification
    Inherits ExpressionClassification

    Private m_InstanceExpression As Expression
    Private m_Parameters As ArgumentList

    Private m_Members As Generic.List(Of PropertyInfo)

    Private m_ResolvedProperty As PropertyInfo
    Private m_Resolved As Boolean
    Private m_Resolver As MethodResolver

    ReadOnly Property Parameters() As ArgumentList
        Get
            Return m_Parameters
        End Get
    End Property

    Public Overrides ReadOnly Property IsConstant() As Boolean
        Get
            Return False
        End Get
    End Property

    Function ResolveGroup(ByVal SourceParameters As ArgumentList, ByRef FinalSourceArguments As Generic.List(Of Argument)) As Boolean
        Dim result As Boolean = True
        Dim destinationParameterTypes()() As Type
        Dim destinationParameters()() As ParameterInfo
        Dim sourceParameterTypes() As Type

        ReDim destinationParameterTypes(m_Members.Count - 1)
        ReDim destinationParameters(m_Members.Count - 1)
        For i As Integer = 0 To m_Members.Count - 1
            destinationParameters(i) = m_Members(i).GetIndexParameters
            destinationParameterTypes(i) = Helper.GetTypes(destinationParameters(i))
        Next

        sourceParameterTypes = SourceParameters.ToTypes

        Dim resolvedGroup As New Generic.List(Of MemberInfo)
        Dim inputGroup As New Generic.List(Of MemberInfo)(m_Members.ToArray)


        If m_Resolver Is Nothing Then m_Resolver = New MethodResolver(Parent)
        m_Resolver.ShowErrors = False
        m_Resolver.Init(inputGroup, SourceParameters, Nothing)
        result = m_Resolver.Resolve AndAlso result

        If result Then
            If m_Resolver.IsLateBound = False Then
                FinalSourceArguments = m_Resolver.ResolvedCandidate.ExactArguments
                resolvedGroup.Add(m_Resolver.ResolvedMember)
            End If
        End If

        'result = Helper.ResolveGroup(Me.Parent, inputGroup, resolvedGroup, SourceParameters, Nothing, Nothing, False)

        If result Then
            m_ResolvedProperty = TryCast(resolvedGroup(0), PropertyInfo)
            result = m_ResolvedProperty IsNot Nothing AndAlso result
        End If

        m_Parameters = SourceParameters
        m_Resolved = True

        Return result
    End Function

    ''' <summary>
    ''' If ResolveGroup has been called.
    ''' Does not say if the resolution was successful or not.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsResolved() As Boolean
        Get
            Return m_Resolved
        End Get
    End Property

    ReadOnly Property Type() As Type
        Get
            If m_ResolvedProperty IsNot Nothing Then
                Return m_ResolvedProperty.PropertyType
            ElseIf m_Members IsNot Nothing Then
                If m_Members.Count = 1 Then
                    m_ResolvedProperty = m_Members(0)
                    Return m_Members(0).PropertyType
                Else
                    Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
                End If
            Else
                Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
            End If
            Compiler.Report.ShowMessage(Messages.VBNC99997, Parent.Location)
            Return Nothing
        End Get
    End Property

    ReadOnly Property ResolvedProperty() As PropertyInfo
        Get
            Return m_ResolvedProperty
        End Get
    End Property

    Function GenerateCodeAsValue(ByVal Info As EmitInfo) As Boolean
        Dim result As Boolean = True

        Dim constant As Object = Nothing 'm_ResolvedProperty.GetConstantValue
        If constant IsNot Nothing Then
            Emitter.EmitLoadValue(Info, constant)
        Else
            Helper.EmitArgumentsAndCallOrCallVirt(Info, m_InstanceExpression, m_Parameters, m_ResolvedProperty.GetGetMethod(True))
        End If

        Return result
    End Function

    Overloads Function ReclassifyToValue() As ValueClassification
        Return New ValueClassification(Me)
    End Function

    ReadOnly Property InstanceExpression() As Expression
        Get
            Return m_InstanceExpression
        End Get
    End Property

    Property [Group]() As Reflection.PropertyInfo()
        Get
            Return m_Members.ToArray
        End Get
        Set(ByVal value As Reflection.PropertyInfo())
            m_Members.Clear()
            m_Members.AddRange(value)
        End Set
    End Property

    Sub New(ByVal Parent As ParsedObject, ByVal InstanceExpression As Expression)
        MyBase.New(Classifications.PropertyGroup, Parent)
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal InstanceExpression As Expression, ByVal Members As Generic.List(Of MemberInfo))
        MyBase.New(Classifications.PropertyGroup, Parent)
        m_InstanceExpression = InstanceExpression

        m_Members = New Generic.List(Of PropertyInfo)(Members.Count)
        For i As Integer = 0 To Members.Count - 1
            Dim tmp As PropertyInfo = TryCast(Members(i), PropertyInfo)
            If tmp IsNot Nothing Then
                m_Members.Add(tmp)
            Else
                Throw New InternalException(Me)
            End If
        Next

#If DEBUG Then
        For i As Integer = 0 To Members.Count - 1
            For j As Integer = i + 1 To Members.Count - 1
                Helper.Assert(Members(i) IsNot Members(j))
            Next
        Next
#End If
    End Sub

    Sub New(ByVal Parent As ParsedObject, ByVal InstanceExpression As Expression, ByVal Members As Generic.List(Of PropertyInfo))
        MyBase.New(Classifications.PropertyGroup, Parent)
        m_InstanceExpression = InstanceExpression

        m_Members = New Generic.List(Of PropertyInfo)(Members)

#If DEBUG Then
        For i As Integer = 0 To Members.Count - 1
            For j As Integer = i + 1 To Members.Count - 1
                Helper.Assert(Members(i) IsNot Members(j))
            Next
        Next
#End If
    End Sub
End Class
