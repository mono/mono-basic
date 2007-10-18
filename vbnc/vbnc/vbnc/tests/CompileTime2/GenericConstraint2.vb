Imports System
Imports System.Collections
Imports System.Reflection

Namespace GenericConstraint2
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
    Public Class TypeDeclaration

    End Class
    Public Class AssemblyDeclaration

    End Class
    Public Class Span

    End Class
    Public Class ResolveInfo

    End Class
    Public Class EmitInfo

    End Class
    Public Class IndentedTextWriter

    End Class
    Public Class ParsedObject
        Inherits BaseObject
    End Class
    Public Class tm

    End Class
    Public Class Report

    End Class
    Public MustInherit Class BaseObject
        Implements IBaseObject

        ''' <summary>
        ''' The parent of this object
        ''' </summary>
        ''' <remarks></remarks>
        Private m_Parent As IBaseObject

        ''' <summary>
        ''' The location in the source of this object.
        ''' </summary>
        ''' <remarks></remarks>
        Private m_Location As Span

        ''' <summary>
        ''' The location in the source of this object.
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Friend Property Location() As Span Implements IBaseObject.Location
            Get
                Return m_Location
            End Get
            Set(ByVal value As Span)
                m_Location = value
            End Set
        End Property

        Sub New()

        End Sub

        Overridable ReadOnly Property FullName() As String Implements IBaseObject.FullName
            Get
                'Dim nameable As INameable = TryCast(Me, INameable)
                'Helper.Assert(nameable IsNot Nothing)
                'Dim nstpparent As IBaseObject = Me.FindFirstParent(Of IType)()
                'If TypeOf Me Is TypeParameter Then Return Nothing
                'If nstpparent IsNot Nothing Then
                '    If TypeOf nstpparent Is IType Then
                '        Return nstpparent.FullName & "+" & nameable.Name
                '    Else
                '        Return nstpparent.FullName & "." & nameable.Name
                '    End If
                'Else
                '    Return nameable.Name
                'End If
            End Get
        End Property

        Friend Function FindTypeParent() As TypeDeclaration
            'Return Me.FindFirstParent(Of TypeDeclaration)()
        End Function

        Friend Function FindMethod() As IBaseObject
            'Dim found As IBaseObject
            'found = FindFirstParent(Of IMethod)()
            'If found Is Nothing Then found = FindFirstParent(Of IPropertyMember)()
            'Return found
        End Function

        Function FindFirstParent(Of T As Class)() As T Implements IBaseObject.FindFirstParent
            'Dim found As T = TryCast(Parent, T)
            'If found IsNot Nothing Then
            '    Return found
            'ElseIf Parent Is Nothing Then
            '    Return Nothing
            'Else
            '    Return Parent.FindFirstParent(Of T)()
            'End If
        End Function

        Function FindFirstParent(Of T1 As {BaseObject}, T2 As {BaseObject})() As IBaseObject 'Implements IBaseObject.FindFirstParent
            'Dim found As IBaseObject = TryCast(Parent, T1)
            'If found Is Nothing Then found = TryCast(Parent, T2)
            'If found IsNot Nothing Then
            '    Return found
            'ElseIf Parent Is Nothing Then
            '    Return Nothing
            'Else
            '    Return Parent.FindFirstParent(Of T1, T2)()
            'End If
        End Function

        ''' <summary>
        ''' Create a new base object with the specified Parent.
        ''' </summary>
        Protected Sub New(ByVal Parent As IBaseObject)
            '            m_Parent = Parent
            '            If m_Parent IsNot Nothing AndAlso tm IsNot Nothing AndAlso tm.IsCurrentTokenValid Then m_Location = tm.CurrentToken.Location
            '#If DEBUG Then
            '        Helper.Assert(Parent IsNot Me)
            '        Helper.Assert(Parent IsNot Nothing OrElse TypeOf Me Is Compiler OrElse TypeOf Me Is Modifiers)
            '        'Make sure there aren't any circular references.
            '        Dim tmp As IBaseObject = Parent
            '        Do While tmp IsNot Nothing
            '            tmp = tmp.Parent
            '            Helper.Assert(tmp IsNot Me)
            '        Loop
            '#End If
        End Sub

        ''' <summary>
        ''' Create a new base object with the specified Parent.
        ''' </summary>
        Protected Sub New(ByVal Parent As IBaseObject, ByVal Location As Span)
            '            m_Parent = Parent
            '            m_Location = Location
            '#If DEBUG Then
            '        Helper.Assert(Parent IsNot Me)
            '        Helper.Assert(Parent IsNot Nothing OrElse TypeOf Me Is Compiler OrElse TypeOf Me Is Modifiers)
            '        'Make sure there aren't any circular references.
            '        Dim tmp As IBaseObject = Parent
            '        Do While tmp IsNot Nothing
            '            tmp = tmp.Parent
            '            Helper.Assert(tmp IsNot Me)
            '        Loop
            '#End If
        End Sub

        Property Parent() As BaseObject
            Get
                '     Return DirectCast(Me.pParent, BaseObject)
            End Get
            Set(ByVal value As BaseObject)
                '   m_Parent = value
            End Set
        End Property

        ''' <summary>
        ''' The parent of this type. Is nothing if this type is an assembly.
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Private Property pParent() As IBaseObject Implements IBaseObject.Parent
            Get
                'Dim tmpPTD As PartialTypeDeclaration = TryCast(m_Parent, PartialTypeDeclaration)
                'If tmpPTD IsNot Nothing AndAlso tmpPTD.IsPartial AndAlso tmpPTD.IsMainPartialDeclaration = False Then
                '    Helper.Assert(tmpPTD.MainPartialDeclaration IsNot Nothing)
                '    m_Parent = tmpPTD.MainPartialDeclaration
                '    'Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Parent of " & Me.GetType.ToString & " set to " & CObj(m_Parent).GetType.ToString)
                'End If
                'Return m_Parent
            End Get
            Set(ByVal value As IBaseObject)
                'm_Parent = value
            End Set
        End Property

        ReadOnly Property ParentAsParsedObject() As ParsedObject
            Get
                'Return DirectCast(Me.Parent, ParsedObject)
            End Get
        End Property

        ''' <summary>
        ''' Get the current compiling assembly.
        ''' </summary>
        Friend Overridable ReadOnly Property Assembly() As AssemblyDeclaration Implements IBaseObject.Assembly
            Get
                'If TypeOf Me Is AssemblyDeclaration Then
                '    Return DirectCast(Me, AssemblyDeclaration)
                'ElseIf TypeOf Me Is Compiler Then
                '    Return DirectCast(Me, Compiler).theAss
                'Else
                '    Helper.Assert(m_Parent IsNot Nothing)
                '    Return m_Parent.Assembly
                'End If
            End Get
        End Property

        Friend ReadOnly Property Report() As Report
            Get
                '         Return Compiler.Report
            End Get
        End Property

        ''' <summary>
        ''' Get the compiler compiling right now.
        ''' </summary>
        Overridable ReadOnly Property Compiler() As Compiler Implements IBaseObject.Compiler
            Get
                'If TypeOf m_Parent Is Compiler Then
                '    Return DirectCast(m_Parent, Compiler)
                'ElseIf TypeOf Me Is Compiler Then
                '    Return DirectCast(Me, vbnc.Compiler)
                'Else
                '    Helper.Assert(m_Parent IsNot Nothing)
                '    Return m_Parent.Compiler
                'End If
            End Get
        End Property

        ''' <summary>
        ''' Get the token manager used for quick token management.
        ''' </summary>
        Friend Overridable ReadOnly Property tm() As tm
            Get
                'Helper.Assert(Compiler IsNot Nothing)
                'Return Compiler.tm
            End Get
        End Property

        Overridable Function ResolveCode(ByVal Info As ResolveInfo) As Boolean Implements IBaseObject.ResolveCode
            'Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "ResolveInfo ignored for '" & Me.GetType.ToString & "'")
            'Helper.NotImplemented()
            ''Return ResolveCode()
        End Function

        Friend Overridable Function GenerateCode(ByVal Info As EmitInfo) As Boolean Implements IBaseObject.GenerateCode
            'Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "The class " & Me.GetType.ToString & " does not implement GenerateCode()")
            'Helper.NotImplemented()
        End Function

        ''' <summary>
        ''' Define = create a builder for the object.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Obsolete("Throws NotImplementedException() - The class you are using does not override this method!")> _
        Overridable Function Define() As Boolean Implements IBaseObject.Define
            'Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "The class " & Me.GetType.ToString & " does not implement Define()")
            'Helper.NotImplemented()
        End Function

#If DEBUG Then
    Overridable Sub Dump(ByVal Dumper As IndentedTextWriter) Implements IBaseObject.Dump
        'Dumper.WriteLine("Dump of " & Me.GetType.ToString & "!")
    End Sub

    Private m_ObjectID As Integer = NewID()
    Public Shared ObjectIDStop As Integer
    Public Shared NextID As Integer
    ReadOnly Property ObjectID() As Integer Implements IBaseObject.ObjectID
        Get
           ' Return m_ObjectID
        End Get
    End Property
    Shared Function NewID() As Integer
        'NextID += 1
        'If ObjectIDStop = NextID Then
        '    Helper.StopIfDebugging()
        'End If
        'Return NextID
    End Function
#End If
    End Class


    Public Interface ILiteralToken
        ReadOnly Property LiteralValue() As Object
        ReadOnly Property LiteralType() As TypeCode
        Function Equals(ByVal Token As ILiteralToken) As Boolean
    End Interface
    Public MustInherit Class Token
        Inherits BaseObject

        Sub New(ByVal Span As Span, ByVal Compiler As Compiler)
            MyBase.New(Compiler, Span)
        End Sub

        Function IdentiferOrKeywordIdentifier() As String
            'If IsKeyword() Then
            '    Return AsKeyword.Identifier
            'ElseIf IsIdentifier() Then
            '    Return AsIdentifier.Identifier
            'Else
            '    Throw New InternalException
            'End If
        End Function

        Function IsSpecial() As Boolean
            ' Return IsKeyword() OrElse IsSymbol()
        End Function

        'ReadOnly Property AsSpecial() As KS
        '    Get
        '        'If IsKeyword() Then
        '        '    Return AsKeyword.Keyword
        '        'ElseIf IsSymbol() Then
        '        '    Return AsSymbol.Symbol
        '        'Else
        '        '    Throw New InternalException
        '        'End If
        '    End Get
        'End Property

        'Function IsKeyword() As Boolean
        '    Return TypeOf Me Is KeywordToken
        'End Function

        'Function AsKeyword() As KeywordToken
        '    If IsKeyword() = False Then Throw New InternalException 'Helper.Assert(IsKeyword)
        '    Return DirectCast(Me, KeywordToken)
        'End Function

        'Function IsSymbol() As Boolean
        '    Return TypeOf Me Is SymbolToken
        'End Function

        'Function AsSymbol() As SymbolToken
        '    If IsSymbol() = False Then Throw New InternalException 'Helper.Assert(IsSymbol)
        '    Return DirectCast(Me, SymbolToken)
        'End Function

        'Function IsIdentifierOrKeyword() As Boolean
        '    Return IsIdentifier() OrElse IsKeyword()
        'End Function

        'Function IsIdentifier() As Boolean
        '    Return TypeOf Me Is IdentifierToken
        'End Function

        'Function AsIdentifier() As IdentifierToken
        '    If IsIdentifier() = False Then Throw New InternalException 'Helper.Assert(IsIdentifier)
        '    Return DirectCast(Me, IdentifierToken)
        'End Function

        'Function IsLiteral() As Boolean
        '    Return TypeOf Me Is ILiteralToken
        'End Function

        'Function AsLiteral() As ILiteralToken
        '    Return DirectCast(Me, ILiteralToken)
        'End Function

        'Function IsDateLiteral() As Boolean
        '    Return TypeOf Me Is DateLiteralToken
        'End Function

        'Function AsDateLiteral() As DateLiteralToken
        '    If IsDateLiteral() = False Then Throw New InternalException ' Helper.Assert(IsDateLiteral)
        '    Return DirectCast(Me, DateLiteralToken)
        'End Function

        'Function IsIntegerLiteral() As Boolean
        '    Return TypeOf Me Is IIntegralLiteralToken
        'End Function

        'Function AsIntegerLiteral() As IIntegralLiteralToken
        '    If IsIntegerLiteral() = False Then Throw New InternalException ' Helper.Assert(IsIntegerLiteral)
        '    Return DirectCast(Me, IIntegralLiteralToken)
        'End Function

        'Function IsCharLiteral() As Boolean
        '    Return TypeOf Me Is CharLiteralToken
        'End Function

        'Function AsCharLiteral() As CharLiteralToken
        '    If IsCharLiteral() = False Then Throw New InternalException ' Helper.Assert(IsCharLiteral)
        '    Return DirectCast(Me, CharLiteralToken)
        'End Function

        'Function IsStringLiteral() As Boolean
        '    Return TypeOf Me Is StringLiteralToken
        'End Function

        'Function AsStringLiteral() As StringLiteralToken
        '    If IsStringLiteral() = False Then Throw New InternalException ' Helper.Assert(IsStringLiteral)
        '    Return DirectCast(Me, StringLiteralToken)
        'End Function

        'Function IsDecimalLiteral() As Boolean
        '    Return TypeOf Me Is DecimalLiteralToken
        'End Function

        'Function AsDecimalLiterl() As DecimalLiteralToken
        '    If IsDecimalLiteral() = False Then Throw New InternalException ' Helper.Assert(IsDecimalLiteral)
        '    Return DirectCast(Me, DecimalLiteralToken)
        'End Function

        ''' <summary>
        ''' Compares this token to any of the specified tokens. 
        ''' Returns true if any token matches.
        ''' </summary>
        ''' <param name="AnySpecial"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Overloads Function Equals(ByVal ParamArray AnySpecial() As KS) As Boolean
            'For i As Integer = 0 To VB.UBound(AnySpecial)
            '    If Equals(AnySpecial(i)) = True Then Return True
            'Next
            Return False
        End Function

        Public Overridable Overloads Function Equals(ByVal Special As KS) As Boolean
            'Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Token.Equals(KS) called on type " & Me.GetType.ToString)
            Return False
        End Function

        Public Overridable Overloads Function Equals(ByVal Identifier As String) As Boolean
            'Compiler.Report.WriteLine(vbnc.Report.ReportLevels.Debug, "Token.Equals(String) called on type " & Me.GetType.ToString)
            Return False
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            'If TypeOf obj Is Token Then
            '    Return Equals(DirectCast(obj, Token))
            'Else
            '    Throw New InternalException
            'End If
        End Function

        Overloads Function Equals(ByVal obj As Token) As Boolean
            'If Me.IsIdentifier AndAlso obj.IsIdentifier Then
            '    Return Me.AsIdentifier.Equals(obj.AsIdentifier)
            'ElseIf Me.IsLiteral AndAlso obj.IsLiteral Then
            '    Return Me.AsLiteral.Equals(obj.AsLiteral)
            'Else
            '    Return False
            'End If
        End Function

        ''' <summary>
        ''' Returns true if currenttoken = Public,Private,Friend,Protected or Static.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function IsScopeKeyword() As Boolean
            'Dim keyword As KeywordToken = TryCast(Me, KeywordToken)
            'If keyword IsNot Nothing Then
            '    Select Case keyword.Keyword
            '        Case KS.Public, KS.Private, KS.Friend, KS.Protected, KS.Static
            '            Return True
            '        Case KS.Else
            '            Return False
            '    End Select
            'Else
            '    Return False
            'End If
        End Function

        Function IsEndOfCode() As Boolean
            'Return TypeOf Me Is EndOfCodeToken
        End Function

        Function IsEndOfFile() As Boolean
            'Return TypeOf Me Is EndOfFileToken
        End Function

        Function IsEndOfStatement() As Boolean
            ' Return isEndOfLine(True) OrElse Equals(KS.Colon)
        End Function

        Function IsEndOfLine(Optional ByVal onlyEndOfLine As Boolean = False) As Boolean
            'If onlyEndOfLine Then
            '    Return TypeOf Me Is EndOfLineToken AndAlso TypeOf Me Is EndOfFileToken = False
            'Else
            '    Return TypeOf Me Is EndOfLineToken
            'End If
        End Function

#If DEBUG Then
    MustOverride Overloads Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
    MustOverride Overrides Function ToString() As String
#End If

        Shared Operator =(ByVal Token As Token, ByVal Identifier As String) As Boolean
            'If Token Is Nothing AndAlso Identifier = "" Then
            '    Return True
            'ElseIf Token Is Nothing Then
            '    Return False
            'ElseIf Not TypeOf Token Is IdentifierToken Then
            '    Return False
            'Else
            '    Return DirectCast(Token, IdentifierToken) = Identifier
            'End If
        End Operator

        Shared Operator <>(ByVal Token As Token, ByVal Identifier As String) As Boolean
            'Return Not Token = Identifier
        End Operator

        Shared Operator =(ByVal Token As Token, ByVal Special As KS) As Boolean
            'If Token Is Nothing Then Return False
            'Return (Token.IsKeyword() AndAlso Token.AsKeyword.Keyword = Special) OrElse _
            '       (Token.IsSymbol AndAlso Token.AsSymbol.Symbol = Special)
        End Operator

        Shared Operator <>(ByVal Token As Token, ByVal Special As KS) As Boolean
            'Return Not Token = Special
        End Operator
    End Class
    Public MustInherit Class LiteralToken(Of Type)
        Inherits Token
        Implements ILiteralToken

        Private m_Value As Type
        Private m_Type As BuiltInDataTypes
        Private m_LiteralTypeCharacter As LiteralTypeCharacters_Characters

        Public Overloads Function Equals(ByVal Token As ILiteralToken) As Boolean Implements ILiteralToken.Equals
            'If Me.LiteralType = Token.LiteralType Then
            '    Return CBool(Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(Token.LiteralValue, Me.LiteralValue, False))
            'Else
            '    Return False
            'End If
        End Function

#If DEBUG Then
    Public Overrides Sub Dump(ByVal Dumper As IndentedTextWriter)
        'Dumper.Write(ToString)
    End Sub
    Sub DumpLiteralTypeCharacter(ByVal Dumper As IndentedTextWriter)
        'If m_LiteralTypeCharacter <> LiteralTypeCharacters_Characters.None Then
        '    Dumper.Write(LiteralTypeCharacters.GetTypeCharacter(m_LiteralTypeCharacter))
        'End If
    End Sub
#End If
        Public Overrides Function ToString() As String
            'Return m_Value.ToString
        End Function

        ReadOnly Property Literal() As Type
            Get
                'Return m_Value
            End Get
        End Property

        ReadOnly Property LiteralValue() As Object Implements ILiteralToken.LiteralValue
            Get
                'Return m_Value
            End Get
        End Property

        Public ReadOnly Property LiteralTypeCharacterCache() As LiteralTypeCharacters_Characters
            Get
                'Return m_LiteralTypeCharacter
            End Get
        End Property

        Sub New(ByVal Span As Span, ByVal Type As BuiltInDataTypes, ByVal Compiler As Compiler, ByVal LiteralTypeCharacter As LiteralTypeCharacters_Characters, ByVal Literal As Type)
            MyBase.New(Span, Compiler)
            'm_Type = Type
            'm_LiteralTypeCharacter = LiteralTypeCharacter
            'm_Value = Literal
            'Helper.Assert(m_Type <> BuiltInDataTypes.Object)
        End Sub

        ReadOnly Property LiteralType() As TypeCode Implements ILiteralToken.LiteralType
            Get
                'Return TypeResolution.BuiltInTypeToTypeCode(m_Type)
            End Get
        End Property

    End Class
    Public Class FloatingPointLiteralToken(Of Type As Structure)
        Inherits LiteralToken(Of Type)

        Sub New(ByVal Range As Span, ByVal Literal As Type, ByVal Type As BuiltInDataTypes, ByVal Compiler As Compiler, ByVal TypeCharacter As LiteralTypeCharacters_Characters)
            MyBase.New(Range, Type, Compiler, TypeCharacter, Literal)
        End Sub
    End Class
    Public Enum BuiltInDataTypes
        [Boolean] = KS.Boolean
        [Byte] = KS.Byte
        [Char] = KS.Char
        [Date] = KS.Date
        [Decimal] = KS.Decimal
        [Double] = KS.Double
        [Integer] = KS.Integer
        [Long] = KS.Long
        [Object] = KS.Object
        [Short] = KS.Short
        [Single] = KS.Single
        [String] = KS.String
        [SByte] = KS.[SByte]
        [UShort] = KS.[UShort]
        [UInteger] = KS.[UInteger]
        [ULong] = KS.[ULong]
    End Enum
    Public Enum LiteralTypeCharacters_Characters
        None = -1
        <KSEnumString("S")> ShortCharacter
        <KSEnumString("US")> UnsignedShortCharacter
        <KSEnumString("I")> IntegerCharacter
        <KSEnumString("UI")> UnsignedIntegerCharacter
        <KSEnumString("L")> LongCharacter
        <KSEnumString("UL")> UnsignedLongCharacter
        <KSEnumString("%")> IntegerTypeCharacter
        <KSEnumString("&")> LongTypeCharacter

        <KSEnumString("F")> SingleCharacter
        <KSEnumString("R")> DoubleCharacter
        DecimalCharacter
        SingleTypeCharacter
        DoubleTypeCharacter
        DecimalTypeCharacter
    End Enum
    Public Class Compiler
        Inherits BaseObject


        Shared Function Main() As Integer

        End Function
    End Class

    Public Class KSEnumStringAttribute
        Inherits EnumStringAttribute
        Protected m_FriendlyValue As String
        Protected m_IsKeyword As Boolean
        Protected m_IsSymbol As Boolean
        Protected m_MultiKeyword As KS
        ReadOnly Property MultiKeyword() As KS
            Get
                Return m_MultiKeyword
            End Get
        End Property
        ReadOnly Property IsMultiKeyword() As Boolean
            Get
                Return m_MultiKeyword <> KS.None
            End Get
        End Property
        ReadOnly Property IsKeyword() As Boolean
            Get
                Return m_IsKeyword
            End Get
        End Property
        ReadOnly Property IsSymbol() As Boolean
            Get
                Return m_IsSymbol
            End Get
        End Property
        Public ReadOnly Property FriendlyValue() As String
            Get
                If m_FriendlyValue Is Nothing Then
                    Return m_Value
                Else
                    Return m_FriendlyValue
                End If
            End Get
        End Property
        ''' <summary>
        ''' Constructor.
        ''' </summary>
        ''' <param name="Value"></param>
        ''' <param name="FriendlyValue"></param>
        ''' <param name="IsKeyword">Is this a real keyword?</param>
        ''' <param name="IsSymbol">Is this a real symbol?</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal Value As String, ByVal FriendlyValue As String, ByVal IsKeyword As Boolean, ByVal IsSymbol As Boolean, Optional ByVal MultiKeyword As KS = KS.None)
            MyBase.New(Value, Nothing)
            m_FriendlyValue = FriendlyValue
            m_IsKeyword = IsKeyword
            m_IsSymbol = IsSymbol
            m_MultiKeyword = MultiKeyword
        End Sub

        Public Sub New(ByVal Value As String, Optional ByVal FriendlyValue As String = Nothing, Optional ByVal Tag As String = Nothing)
            Me.New(Value, FriendlyValue, False, False, KS.None)
            m_Tag = Tag
        End Sub
    End Class

    Public Class EnumStringAttribute
        Inherits System.Attribute
        Protected m_Value As String
        Protected m_Tag As Object
        ReadOnly Property Tag() As Object
            Get
                Return m_Tag
            End Get
        End Property
        Public ReadOnly Property Value() As String
            Get
                Return m_Value
            End Get
        End Property
        ''' <summary>
        ''' Constructor.
        ''' </summary>
        ''' <param name="Value"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal Value As String, Optional ByVal Tag As String = Nothing)
            m_Value = Value
            m_Tag = Tag
        End Sub
    End Class
    Public Enum KS
        <KSEnumString("", "NONE")> None
        'First True keywords
        <KSEnumString("AddHandler", Nothing, True, False, KS.End_AddHandler)> [AddHandler]
        <KSEnumString("AddressOf", Nothing, True, False)> [AddressOf]
        <KSEnumString("AndAlso", Nothing, True, False)> [AndAlso]
        <KSEnumString("Alias", Nothing, True, False)> [Alias]
        <KSEnumString("And", Nothing, True, False)> [And]
        <KSEnumString("Ansi", Nothing, True, False)> [Ansi]
        <KSEnumString("As", Nothing, True, False)> [As]
        ' <KSEnumString("Assembly", Nothing, True, False)> [Assembly]
        <KSEnumString("Auto", Nothing, True, False)> [Auto]
        <KSEnumString("Boolean", Nothing, True, False)> [Boolean]
        <KSEnumString("ByRef", Nothing, True, False)> [ByRef]
        <KSEnumString("Byte", Nothing, True, False)> [Byte]
        <KSEnumString("ByVal", Nothing, True, False)> [ByVal]
        <KSEnumString("Call", Nothing, True, False)> [Call]
        <KSEnumString("Case", Nothing, True, False)> [Case]
        <KSEnumString("Catch", Nothing, True, False)> [Catch]
        <KSEnumString("CBool", Nothing, True, False)> [CBool]
        <KSEnumString("CByte", Nothing, True, False)> [CByte]
        <KSEnumString("CChar", Nothing, True, False)> [CChar]
        <KSEnumString("CDate", Nothing, True, False)> [CDate]
        <KSEnumString("CDec", Nothing, True, False)> [CDec]
        <KSEnumString("CDbl", Nothing, True, False)> [CDbl]
        <KSEnumString("Char", Nothing, True, False)> [Char]
        <KSEnumString("CInt", Nothing, True, False)> [CInt]
        <KSEnumString("Class", Nothing, True, False, KS.End_Class)> [Class]
        <KSEnumString("CLng", Nothing, True, False)> [CLng]
        <KSEnumString("CObj", Nothing, True, False)> [CObj]
        <KSEnumString("Const", Nothing, True, False)> [Const]
        <KSEnumString("Continue", Nothing, True, False)> [Continue]
        <KSEnumString("CSByte", Nothing, True, False)> [CSByte]
        <KSEnumString("CShort", Nothing, True, False)> [CShort]
        <KSEnumString("CSng", Nothing, True, False)> [CSng]
        <KSEnumString("CStr", Nothing, True, False)> [CStr]
        <KSEnumString("CUInt", Nothing, True, False)> [CUInt]
        <KSEnumString("CULng", Nothing, True, False)> [CULng]
        <KSEnumString("CUShort", Nothing, True, False)> [CUShort]
        <KSEnumString("CType", Nothing, True, False)> [CType]
        <KSEnumString("Custom Event", Nothing, True, False)> [CustomEvent]
        <KSEnumString("Date", Nothing, True, False)> [Date]
        <KSEnumString("Decimal", Nothing, True, False)> [Decimal]
        <KSEnumString("Declare", Nothing, True, False)> [Declare]
        <KSEnumString("Default", Nothing, True, False)> [Default]
        <KSEnumString("Delegate", Nothing, True, False)> [Delegate]
        <KSEnumString("Dim", Nothing, True, False)> [Dim]
        <KSEnumString("DirectCast", Nothing, True, False)> [DirectCast]
        <KSEnumString("Do", Nothing, True, False)> [Do]
        <KSEnumString("Double", Nothing, True, False)> [Double]
        <KSEnumString("Each", Nothing, True, False)> [Each]
        <KSEnumString("Else", Nothing, True, False)> [Else]
        <KSEnumString("ElseIf", Nothing, True, False)> [ElseIf]
        <KSEnumString("End", Nothing, True, False)> [End]
        <KSEnumString("Enum", Nothing, True, False, KS.End_Enum)> [Enum]
        <KSEnumString("Erase", Nothing, True, False)> [Erase]
        <KSEnumString("Error", Nothing, True, False)> [Error]
        <KSEnumString("Event", Nothing, True, False, KS.End_Event)> [Event]
        <KSEnumString("Exit", Nothing, True, False)> [Exit]
        <KSEnumString("False", Nothing, True, False)> [False]
        <KSEnumString("Finally", Nothing, True, False)> [Finally]
        <KSEnumString("For", Nothing, True, False)> [For]
        <KSEnumString("Friend", Nothing, True, False)> [Friend]
        <KSEnumString("Function", Nothing, True, False, KS.End_Function)> [Function]
        <KSEnumString("Get", Nothing, True, False, KS.End_Get)> [Get]
        <KSEnumString("GetType", Nothing, True, False)> [GetType]
        <KSEnumString("Global", Nothing, True, False)> [Global]
        <KSEnumString("GoTo", Nothing, True, False)> [GoTo]
        <KSEnumString("Handles", Nothing, True, False)> [Handles]
        <KSEnumString("If", Nothing, True, False, KS.End_If)> [If]
        <KSEnumString("Implements", Nothing, True, False)> [Implements]
        <KSEnumString("Imports", Nothing, True, False)> [Imports]
        <KSEnumString("In", Nothing, True, False)> [In]
        <KSEnumString("Inherits", Nothing, True, False)> [Inherits]
        <KSEnumString("Integer", Nothing, True, False)> [Integer]
        <KSEnumString("Interface", Nothing, True, False, KS.End_Interface)> [Interface]
        <KSEnumString("Is", Nothing, True, False)> [Is]
        <KSEnumString("IsNot", Nothing, True, False)> [IsNot]
        <KSEnumString("Let", Nothing, True, False)> [Let]
        <KSEnumString("Lib", Nothing, True, False)> [Lib]
        <KSEnumString("Like", Nothing, True, False)> [Like]
        <KSEnumString("Long", Nothing, True, False)> [Long]
        <KSEnumString("Loop", Nothing, True, False)> [Loop]
        <KSEnumString("Me", Nothing, True, False)> [Me]
        <KSEnumString("Mod", Nothing, True, False)> [Mod]
        <KSEnumString("Module", Nothing, True, False, KS.End_Module)> [Module]
        <KSEnumString("MustInherit", Nothing, True, False)> [MustInherit]
        <KSEnumString("MustOverride", Nothing, True, False)> [MustOverride]
        <KSEnumString("MyBase", Nothing, True, False)> [MyBase]
        <KSEnumString("MyClass", Nothing, True, False)> [MyClass]
        <KSEnumString("Namespace", Nothing, True, False, KS.End_Namespace)> [Namespace]
        <KSEnumString("Narrowing", Nothing, True, False)> [Narrowing]
        <KSEnumString("New", Nothing, True, False)> [New]
        <KSEnumString("Next", Nothing, True, False)> [Next]
        <KSEnumString("Not", Nothing, True, False)> [Not]
        <KSEnumString("Nothing", Nothing, True, False)> [Nothing]
        <KSEnumString("NotInheritable", Nothing, True, False)> [NotInheritable]
        <KSEnumString("NotOverridable", Nothing, True, False)> [NotOverridable]
        <KSEnumString("Object", Nothing, True, False)> [Object]
        <KSEnumString("Of", Nothing, True, False)> [Of]
        <KSEnumString("On", Nothing, True, False)> [On]
        <KSEnumString("Operator", Nothing, True, False, KS.End_Operator)> [Operator]
        <KSEnumString("Option", Nothing, True, False)> [Option]
        <KSEnumString("Optional", Nothing, True, False)> [Optional]
        <KSEnumString("Or", Nothing, True, False)> [Or]
        <KSEnumString("Orelse", Nothing, True, False)> [OrElse]
        <KSEnumString("Overloads", Nothing, True, False)> [Overloads]
        <KSEnumString("Overridable", Nothing, True, False)> [Overridable]
        <KSEnumString("Overrides", Nothing, True, False)> [Overrides]
        <KSEnumString("Partial", Nothing, True, False)> [Partial]
        <KSEnumString("ParamArray", Nothing, True, False)> [ParamArray]
        '<KSEnumString("Preserve", Nothing, True, False)> Preserve
        <KSEnumString("Private", Nothing, True, False)> [Private]
        <KSEnumString("Property", Nothing, True, False, KS.End_Property)> [Property]
        <KSEnumString("Protected", Nothing, True, False)> [Protected]
        <KSEnumString("Public", Nothing, True, False)> [Public]
        <KSEnumString("RaiseEvent", Nothing, True, False, KS.End_RaiseEvent)> [RaiseEvent]
        <KSEnumString("ReadOnly", Nothing, True, False)> [ReadOnly]
        <KSEnumString("ReDim", Nothing, True, False)> [ReDim]
        <KSEnumString("REM", Nothing, True, False)> [REM]
        <KSEnumString("RemoveHandler", Nothing, True, False, KS.End_RemoveHandler)> [RemoveHandler]
        <KSEnumString("Resume", Nothing, True, False)> [Resume]
        <KSEnumString("Return", Nothing, True, False)> [Return]
        <KSEnumString("SByte", Nothing, True, False)> [SByte]
        <KSEnumString("Select", Nothing, True, False, KS.End_Select)> [Select]
        <KSEnumString("Set", Nothing, True, False, KS.End_Set)> [Set]
        <KSEnumString("Shadows", Nothing, True, False)> [Shadows]
        <KSEnumString("Shared", Nothing, True, False)> [Shared]
        <KSEnumString("Short", Nothing, True, False)> [Short]
        <KSEnumString("Single", Nothing, True, False)> [Single]
        <KSEnumString("Static", Nothing, True, False)> [Static]
        <KSEnumString("Step", Nothing, True, False)> [Step]
        <KSEnumString("Stop", Nothing, True, False)> [Stop]
        <KSEnumString("String", Nothing, True, False)> [String]
        <KSEnumString("Structure", Nothing, True, False, KS.End_Structure)> [Structure]
        <KSEnumString("Sub", Nothing, True, False, KS.End_Sub)> [Sub]
        <KSEnumString("SyncLock", Nothing, True, False, KS.End_SyncLock)> [SyncLock]
        <KSEnumString("Then", Nothing, True, False)> [Then]
        <KSEnumString("Throw", Nothing, True, False)> [Throw]
        <KSEnumString("To", Nothing, True, False)> [To]
        <KSEnumString("True", Nothing, True, False)> [True]
        <KSEnumString("Try", Nothing, True, False, KS.End_Try)> [Try]
        <KSEnumString("TryCast", Nothing, True, False)> [TryCast]
        <KSEnumString("TypeOf", Nothing, True, False)> [TypeOf]
        <KSEnumString("UInteger", Nothing, True, False)> [UInteger]
        <KSEnumString("ULong", Nothing, True, False)> [ULong]
        <KSEnumString("UShort", Nothing, True, False)> [UShort]
        <KSEnumString("Using", Nothing, True, False, KS.End_Using)> [Using]
        <KSEnumString("Unicode", Nothing, True, False)> [Unicode]
        <KSEnumString("Until", Nothing, True, False)> [Until]
        <KSEnumString("Variant", Nothing, True, False)> [Variant]
        <KSEnumString("When", Nothing, True, False)> [When]
        <KSEnumString("While", Nothing, True, False, KS.End_While)> [While]
        <KSEnumString("Widening", Nothing, True, False)> [Widening]
        <KSEnumString("With", Nothing, True, False, KS.End_With)> [With]
        <KSEnumString("WithEvents", Nothing, True, False)> [WithEvents]
        <KSEnumString("WriteOnly", Nothing, True, False)> [WriteOnly]
        <KSEnumString("Xor", Nothing, True, False)> [Xor]
        'Semi keywords
        <KSEnumString("#End", Nothing, False, False)> ConditionalEnd
        <KSEnumString("#Const", Nothing, False, False)> ConditionalConst
        <KSEnumString("#End If", Nothing, False, False, KS.ConditionalIf)> ConditionalEndIf
        <KSEnumString("#End Region", Nothing, False, False, KS.ConditionalRegion)> ConditionalEndRegion
        <KSEnumString("#End ExternalSource", Nothing, False, False, KS.ConditionalExternalSource)> ConditionalEndExternalSource
        <KSEnumString("#Else", Nothing, False, False)> ConditionalElse
        <KSEnumString("#ElseIf", Nothing, False, False)> ConditionalElseIf
        <KSEnumString("#ExternalSource", Nothing, False, False, KS.ConditionalEndExternalSource)> ConditionalExternalSource
        <KSEnumString("#If", Nothing, False, False, KS.ConditionalEndIf)> ConditionalIf
        <KSEnumString("#Region", Nothing, False, False, KS.ConditionalEndRegion)> ConditionalRegion

        <KSEnumString("End AddHandler", Nothing, False, False, KS.AddHandler)> End_AddHandler
        <KSEnumString("End Class", Nothing, False, False, KS.Class)> End_Class
        <KSEnumString("End Enum", Nothing, False, False, KS.Enum)> End_Enum
        <KSEnumString("End Event", Nothing, False, False, KS.Event)> End_Event
        <KSEnumString("End Function", Nothing, False, False, KS.Function)> End_Function
        <KSEnumString("End Get", Nothing, False, False, KS.Get)> End_Get
        <KSEnumString("End If", Nothing, False, False, KS.If)> End_If
        <KSEnumString("End Interface", Nothing, False, False, KS.Interface)> End_Interface
        <KSEnumString("End Module", Nothing, False, False, KS.Module)> End_Module
        <KSEnumString("End Namespace", Nothing, False, False, KS.Namespace)> End_Namespace
        <KSEnumString("End Operator", Nothing, False, False, KS.Operator)> End_Operator
        <KSEnumString("End Property", Nothing, False, False, KS.Property)> End_Property
        <KSEnumString("End RaiseEvent", Nothing, False, False, KS.RaiseEvent)> End_RaiseEvent
        <KSEnumString("End RemoveHandler", Nothing, False, False, KS.RemoveHandler)> End_RemoveHandler
        <KSEnumString("End Select", Nothing, False, False, KS.Select)> End_Select
        <KSEnumString("End Set", Nothing, False, False, KS.Set)> End_Set
        <KSEnumString("End Structure", Nothing, False, False, KS.Structure)> End_Structure
        <KSEnumString("End Sub", Nothing, False, False, KS.Sub)> End_Sub
        <KSEnumString("End SynckLock", Nothing, False, False, KS.SyncLock)> End_SyncLock
        <KSEnumString("End Try", Nothing, False, False, KS.Try)> End_Try
        <KSEnumString("End While", Nothing, False, False, KS.While)> End_While
        <KSEnumString("End With", Nothing, False, False, KS.With)> End_With
        <KSEnumString("End Using", Nothing, False, False, KS.Using)> End_Using

        'Real symbols
        <KSEnumString("<", Nothing, False, True)> LT
        <KSEnumString(">", Nothing, False, True)> GT
        <KSEnumString("=", Nothing, False, True)> Equals
        <KSEnumString("<>", Nothing, False, True)> NotEqual
        <KSEnumString("<=", Nothing, False, True)> LE
        <KSEnumString(">=", Nothing, False, True)> GE
        <KSEnumString("!", Nothing, False, True)> Exclamation
        <KSEnumString("&", Nothing, False, True)> Concat
        <KSEnumString("*", Nothing, False, True)> Mult
        <KSEnumString("+", Nothing, False, True)> Add
        <KSEnumString("-", Nothing, False, True)> Minus
        <KSEnumString("^", Nothing, False, True)> Power
        <KSEnumString("/", Nothing, False, True)> RealDivision
        <KSEnumString("\", Nothing, False, True)> IntDivision
        <KSEnumString("#", Nothing, False, True)> Numeral
        <KSEnumString("{", Nothing, False, True)> LBrace
        <KSEnumString("}", Nothing, False, True)> RBrace
        <KSEnumString("(", Nothing, False, True)> LParenthesis
        <KSEnumString(")", Nothing, False, True)> RParenthesis
        <KSEnumString(".", " .", False, True)> Dot
        <KSEnumString(",", Nothing, False, True)> Comma
        <KSEnumString(":", Nothing, False, True)> Colon
        ''' <summary>
        ''' &lt;&lt;
        ''' </summary>
        ''' <remarks></remarks>
        <KSEnumString("<<", Nothing, False, True)> ShiftLeft
        ''' <summary>
        ''' &gt;&gt;
        ''' </summary>
        ''' <remarks></remarks>
        <KSEnumString(">>", Nothing, False, True)> ShiftRight

        'Assignment symbols
        <KSEnumString("&=", Nothing, False, True)> ConcatAssign '		L"&="
        <KSEnumString("+=", Nothing, False, True)> AddAssign 'L"+="
        <KSEnumString("-=", Nothing, False, True)> MinusAssign 'L"-="
        <KSEnumString("/=", Nothing, False, True)> RealDivAssign 'L"/="
        <KSEnumString("\=", Nothing, False, True)> IntDivAssign 'L"\="
        <KSEnumString("^=", Nothing, False, True)> PowerAssign 'L"^="
        <KSEnumString("*=", Nothing, False, True)> MultAssign 'L"*="
        <KSEnumString("<<=", Nothing, False, True)> ShiftLeftAssign '<<=
        <KSEnumString(">>=", Nothing, False, True)> ShiftRightAssign '>>=
        NumberOfItems 'This is the number of constants in this enum
    End Enum

End Namespace