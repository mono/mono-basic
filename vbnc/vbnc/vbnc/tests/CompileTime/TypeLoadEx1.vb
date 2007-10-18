Imports System
Imports System.Collections
Imports System.Reflection
Imports System.Reflection.Emit

Namespace TypeLoadEx1

    Public Interface IBaseObject
        ReadOnly Property Assembly() As AssemblyDeclaration
        Property Location() As Span
        Property Parent() As IBaseObject
        ReadOnly Property Compiler() As Compiler
        ReadOnly Property FullName() As String
        Function ResolveCode(ByVal Info As ResolveInfo) As Boolean
        Function Define() As Boolean
        Function GenerateCode(ByVal Info As EmitInfo) As Boolean
        Function FindFirstParent(Of T As Class)() As T
#If DEBUG Then
    Sub Dump(ByVal Dumper As IndentedTextWriter)
    ReadOnly Property ObjectID() As Integer
#End If
    End Interface
    Class tm

    End Class
    Class Helper

    End Class
    Class Report

    End Class
    Class TypeDeclaration

    End Class

    Public Class ResolveInfo

    End Class
    Public Class IndentedTextWriter

    End Class
    Public Class EmitInfo

    End Class
    Public Class Compiler

    End Class
    Public Class Span

    End Class
    Public Class AssemblyDeclaration

    End Class

    Public Interface IMethod

    End Interface

    Public MustInherit Class BaseObject
        Implements IBaseObject

        Private m_Parent As IBaseObject

        Private m_Location As Span


        Friend Property Location() As Span Implements IBaseObject.Location
            Get
            End Get
            Set(ByVal value As Span)
            End Set
        End Property

        Overridable ReadOnly Property FullName() As String Implements IBaseObject.FullName
            Get

            End Get
        End Property

        Friend Function FindTypeParent() As TypeDeclaration
        End Function

        Friend Function FindMethod() As IBaseObject

        End Function

        Function FindFirstParent(Of T As Class)() As T Implements IBaseObject.FindFirstParent

        End Function

        Function FindFirstParent(Of T1 As {BaseObject}, T2 As {BaseObject})() As IBaseObject

        End Function

        Protected Sub New(ByVal Parent As IBaseObject)

        End Sub

        Protected Sub New(ByVal Parent As IBaseObject, ByVal Location As Span)

        End Sub

        Property Parent() As BaseObject
            Get
            End Get
            Set(ByVal value As BaseObject)
            End Set
        End Property

        Private Property pParent() As IBaseObject Implements IBaseObject.Parent
            Get
            End Get
            Set(ByVal value As IBaseObject)
            End Set
        End Property

        ReadOnly Property ParentAsParsedObject() As ParsedObject
            Get
            End Get
        End Property

        Friend Overridable ReadOnly Property Assembly() As AssemblyDeclaration Implements IBaseObject.Assembly
            Get

            End Get
        End Property

        Friend ReadOnly Property Report() As Report
            Get
            End Get
        End Property

        Overridable ReadOnly Property Compiler() As Compiler Implements IBaseObject.Compiler
            Get
            End Get
        End Property

        Friend Overridable ReadOnly Property tm() As tm
            Get

            End Get
        End Property

        Overridable Function ResolveCode(ByVal Info As ResolveInfo) As Boolean Implements IBaseObject.ResolveCode

        End Function

        Friend Overridable Function GenerateCode(ByVal Info As EmitInfo) As Boolean Implements IBaseObject.GenerateCode
        End Function


        Overridable Function Define() As Boolean Implements IBaseObject.Define

        End Function

#If DEBUG Then
    Overridable Sub Dump(ByVal Dumper As IndentedTextWriter) Implements IBaseObject.Dump
    End Sub

    Private m_ObjectID As Integer = NewID()
    Public Shared ObjectIDStop As Integer
    Public Shared NextID As Integer
    ReadOnly Property ObjectID() As Integer Implements IBaseObject.ObjectID
        Get
        End Get
    End Property
    Shared Function NewID() As Integer

    End Function
#End If
    End Class


    Public MustInherit Class ParsedObject
        Inherits BaseObject

        Sub CheckTypeReferencesNotResolved()

        End Sub

        Sub CheckCodeNotResolved()
        End Sub

        ReadOnly Property CodeResolved() As Boolean
            Get

            End Get
        End Property

        ReadOnly Property TypeReferencesResolved() As Boolean
            Get
            End Get
        End Property

        Protected Sub New(ByVal Parent As ParsedObject)
            MyBase.new(Parent)
        End Sub

        Protected Sub New(ByVal Parent As ParsedObject, ByVal Location As Span)
            MyBase.new(Parent, Location)
        End Sub

        Property HasErrors() As Boolean
            Get
            End Get
            Set(ByVal value As Boolean)
            End Set
        End Property

        Shadows Property Parent() As ParsedObject
            Get
            End Get
            Set(ByVal value As ParsedObject)
            End Set
        End Property

        Overridable Function ResolveTypeReferences() As Boolean
        End Function
    End Class


    Public Class CodeBlock
        Inherits ParsedObject

        Sub New(ByVal Parent As ParsedObject)
            MyBase.New(Parent)
        End Sub

        ReadOnly Property EndOfMethodLabel() As Label
            Get

            End Get
        End Property

        Property FirstStatement() As BaseObject
            Get
            End Get
            Set(ByVal value As BaseObject)
            End Set
        End Property

        Property HasResume() As Boolean
            Get
            End Get
            Set(ByVal value As Boolean)
            End Set
        End Property

        Property HasUnstructuredExceptionHandling() As Boolean
            Get
            End Get
            Set(ByVal value As Boolean)
            End Set
        End Property

        Property HasStructuredExceptionHandling() As Boolean
            Get
            End Get
            Set(ByVal value As Boolean)
            End Set
        End Property

        Private Function GenerateUnstructuredStart(ByVal Info As EmitInfo) As Boolean

        End Function


        Friend Overloads Function GenerateCode(ByVal Method As IMethod) As Boolean

        End Function

        Private Function CreateLabelForCurrentInstruction(ByVal Info As EmitInfo) As Boolean

        End Function

        Friend Overrides Function GenerateCode(ByVal Info As EmitInfo) As Boolean

        End Function

        Overridable ReadOnly Property IsOneLiner() As Boolean
            Get
            End Get
        End Property

        Public Overrides Function ResolveTypeReferences() As Boolean

        End Function

        Public Overrides Function ResolveCode(ByVal Info As ResolveInfo) As Boolean

        End Function


#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)

    End Sub
#End If
    End Class


End Namespace