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
''' Every object that can be put in the parse tree should derive
''' from this class.
''' </summary>
''' <remarks></remarks>
Public MustInherit Class BaseObject
    Implements IBaseObject

    ''' <summary>
    ''' The parent of this object
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Parent As BaseObject

    ''' <summary>
    ''' The location in the source of this object.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_Location As Span

    ''' <summary>
    ''' Don't use this one!
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared m_Compiler As Compiler

    ''' <summary>
    ''' An empty constructor
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub New()

    End Sub

    ''' <summary>
    ''' Create a new base object with the specified Parent.
    ''' </summary>
    Protected Sub New(ByVal Parent As BaseObject)
        m_Parent = Parent
        'Helper.Assert(Not (TypeOf m_Parent Is ClassDeclaration AndAlso TypeOf Me Is FunctionSignature))
        If m_Parent IsNot Nothing AndAlso tm IsNot Nothing AndAlso tm.IsCurrentTokenValid Then m_Location = tm.CurrentLocation
#If DEBUG Then
        Helper.Assert(Parent IsNot Me)
        'Make sure there aren't any circular references.
        Dim tmp As IBaseObject = Parent
        Do While tmp IsNot Nothing
            tmp = tmp.Parent
            Helper.Assert(tmp IsNot Me)
        Loop
#End If
    End Sub

    ''' <summary>
    ''' Create a new base object with the specified Parent.
    ''' </summary>
    Protected Sub New(ByVal Parent As BaseObject, ByVal Location As Span)
        m_Parent = Parent
        Helper.Assert(Not (TypeOf m_Parent Is ClassDeclaration AndAlso TypeOf Me Is FunctionSignature))
        m_Location = Location
#If DEBUG Then
        Helper.Assert(Parent IsNot Me)
        'Make sure there aren't any circular references.
        Dim tmp As IBaseObject = Parent
        Do While tmp IsNot Nothing
            tmp = tmp.Parent
            Helper.Assert(tmp IsNot Me)
        Loop
#End If
    End Sub

    Public Shared Sub ClearCache()
        m_Compiler = Nothing
    End Sub

    ''' <summary>
    ''' The location in the source of this object.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Friend Property Location() As Span Implements IBaseObject.Location
        Get
            If m_Location.HasFile = False AndAlso m_Location.Column = 0 AndAlso m_Location.Line = 0 AndAlso m_Parent IsNot Nothing Then
                Return m_Parent.Location
            End If
            Return m_Location
        End Get
        Set(ByVal value As Span)
            m_Location = value
        End Set
    End Property

    ReadOnly Property File() As CodeFile
        Get
            Return Location.File(Compiler)
        End Get
    End Property

    Overridable ReadOnly Property FullName() As String Implements IBaseObject.FullName
        Get
            Dim nameable As INameable = TryCast(Me, INameable)
            Helper.Assert(nameable IsNot Nothing)
            Dim nstpparent As IBaseObject = Me.FindFirstParent(Of IType)()
            If TypeOf Me Is TypeParameter Then Return Nothing
            If nstpparent IsNot Nothing Then
                If TypeOf nstpparent Is IType Then
                    Return nstpparent.FullName & "+" & nameable.Name
                Else
                    Return nstpparent.FullName & "." & nameable.Name
                End If
            Else
                Return nameable.Name
            End If
        End Get
    End Property

    Friend Function FindTypeParent() As TypeDeclaration
        Return Me.FindFirstParent(Of TypeDeclaration)()
    End Function

    Friend Function FindMethod() As IBaseObject
        Dim found As IBaseObject
        found = FindFirstParent(Of IMethod)()
        If found Is Nothing Then found = FindFirstParent(Of PropertyDeclaration)()
        Return found
    End Function

    Function FindFirstParent_IType() As IType
        If Parent Is Nothing Then
            Return Nothing
        ElseIf TypeOf Parent Is IType Then
            Return CType(CObj(Parent), IType)
        Else
            Return Parent.FindFirstParent(Of IType)()
        End If
    End Function

    Function FindFirstParent(Of T)() As T
        If Parent Is Nothing Then
            Return Nothing
        ElseIf TypeOf Parent Is T Then
            Return CType(CObj(Parent), T)
        Else
            Return Parent.FindFirstParent(Of T)()
        End If
    End Function

    Function FindFirstParent(Of T1, T2)() As IBaseObject
        If Parent Is Nothing Then
            Return Nothing
        ElseIf TypeOf Parent Is T1 Then
            Return CType(CObj(Parent), IBaseObject)
        ElseIf TypeOf Parent Is T2 Then
            Return CType(CObj(Parent), IBaseObject)
        Else
            Return Parent.FindFirstParent(Of T1, T2)()
        End If
    End Function

    ''' <summary>
    ''' The parent of this type. Is nothing if this type is an assembly.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property Parent() As BaseObject Implements IBaseObject.Parent
        Get
            Return m_Parent
        End Get
        Set(ByVal value As BaseObject)
            m_Parent = value
            Helper.Assert(Not (TypeOf m_Parent Is ClassDeclaration AndAlso TypeOf Me Is FunctionSignature))
        End Set
    End Property

    ReadOnly Property ParentAsParsedObject() As ParsedObject
        Get
            Return DirectCast(Me.Parent, ParsedObject)
        End Get
    End Property

    ''' <summary>
    ''' Get the current compiling assembly.
    ''' </summary>
    Friend Overridable ReadOnly Property Assembly() As AssemblyDeclaration Implements IBaseObject.Assembly
        Get
            If TypeOf Me Is AssemblyDeclaration Then
                Return DirectCast(Me, AssemblyDeclaration)
            ElseIf TypeOf Me Is Compiler Then
                Return DirectCast(Me, Compiler).theAss
            Else
                Helper.Assert(m_Parent IsNot Nothing)
                Return m_Parent.Assembly
            End If
        End Get
    End Property

    Friend ReadOnly Property Report() As Report
        Get
            Return Compiler.Report
        End Get
    End Property

    ''' <summary>
    ''' Get the compiler compiling right now.
    ''' </summary>
    Overridable ReadOnly Property Compiler() As Compiler Implements IBaseObject.Compiler
        Get
            If m_Compiler IsNot Nothing Then
                Return m_Compiler
            End If

            If TypeOf m_Parent Is Compiler Then
                m_Compiler = DirectCast(m_Parent, Compiler)
            ElseIf TypeOf Me Is Compiler Then
                m_Compiler = DirectCast(Me, vbnc.Compiler)
            ElseIf m_Parent Is Nothing Then
                Return Nothing
            Else
                Helper.Assert(m_Parent IsNot Nothing)
                m_Compiler = m_Parent.Compiler
            End If

            Return m_Compiler
        End Get
    End Property

#If DEBUG Then
    ReadOnly Property ParentTree() As String()
        Get
            Dim result As New Generic.List(Of String)

            Dim tmp As BaseObject = Me
            Do Until tmp Is Nothing
                result.Add(tmp.GetType.Name)
                tmp = tmp.Parent
            Loop

            Return result.ToArray
        End Get
    End Property
    ReadOnly Property ParentLocationTree() As String()
        Get
            Dim result As New Generic.List(Of String)

            Dim tmp As BaseObject = Me
            Do Until tmp Is Nothing
                'If tmp.HasLocation = False Then
                '    result &= "(" & tmp.GetType.Name & "): (no location)" & VB.vbNewLine
                'Else
                result.Add("(" & tmp.GetType.Name & "): " & tmp.Location.ToString(Compiler))
                'End If
                tmp = tmp.Parent
            Loop

            Return result.toarray
        End Get
    End Property
#End If

    ''' <summary>
    ''' Get the token manager used for quick token management.
    ''' </summary>
    Friend Overridable ReadOnly Property tm() As tm
        Get
            Helper.Assert(Compiler IsNot Nothing)
            Return Compiler.tm
        End Get
    End Property

    Overridable Function ResolveCode(ByVal Info As ResolveInfo) As Boolean Implements IBaseObject.ResolveCode
        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "ResolveInfo ignored for '" & Me.GetType.ToString & "'")
        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
        'Return ResolveCode()
    End Function

    Friend Overridable Function GenerateCode(ByVal Info As EmitInfo) As Boolean Implements IBaseObject.GenerateCode
        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "The class " & Me.GetType.ToString & " does not implement GenerateCode()")
        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
    End Function

    Public ReadOnly Property IsOptionInferOn As Boolean
        Get
            Return Location.File(Compiler).IsOptionInferOn
        End Get
    End Property

    ''' <summary>
    ''' Define = create a builder for the object.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete("Throws NotImplementedException() - The class you are using does not override this method!")> _
    Overridable Function Define() As Boolean Implements IBaseObject.Define
        Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "The class " & Me.GetType.ToString & " does not implement Define()")
        Return Compiler.Report.ShowMessage(Messages.VBNC99997, Me.Location)
    End Function

    Private m_ObjectID As Integer = NewID()
    Public Shared ObjectIDStop As Integer
    Public Shared NextID As Integer

    ReadOnly Property ObjectID() As Integer Implements IBaseObject.ObjectID
        Get
            Return m_ObjectID
        End Get
    End Property

    Shared Function NewID() As Integer
        NextID += 1
        If ObjectIDStop = NextID Then
            Helper.StopIfDebugging()
        End If

        Return NextID
    End Function
End Class

