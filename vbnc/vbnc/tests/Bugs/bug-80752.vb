Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Resources
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Drawing.Imaging

' Allgemeine Informationen über eine Assembly werden über die folgende 
' Attributgruppe gesteuert. Ändern Sie diese Attributwerte, um Informationen,
' die mit einer Assembly verknüpft sind, zu bearbeiten.

' Die Werte der Assemblyattribute überprüfen

<Assembly: AssemblyTitle("XPCommonControls")> 
<Assembly: AssemblyDescription("commonly used xp styled controls")> 
<Assembly: AssemblyCompany("SteepValley.net (Michael Dobler)")> 
<Assembly: AssemblyProduct("")> 
<Assembly: AssemblyCopyright("")> 
<Assembly: AssemblyTrademark("")> 
<Assembly: CLSCompliant(True)> 
<Assembly: ComVisible(False)> 

'Die folgende GUID ist für die ID der Typbibliothek, wenn dieses Projekt in COM angezeigt wird
'<Assembly: Guid("C9E9B6E7-FA09-4E9B-A4BA-9EFC5F5636A2")> 

' Versionsinformationen für eine Assembly bestehen aus den folgenden vier Werten:
'
'      Haupversion
'      Nebenversion 
'      Buildnummer
'      Revisionsnummer
'
' Sie können alle Werte angeben oder auf die standardmäßigen Build- und Revisionsnummern 
' zurückgreifen, indem Sie '*' wie unten angezeigt verwenden:

<Assembly: AssemblyVersion("2.2.4.0")> 

Namespace ThemedControls

    Public MustInherit Class ThemeFormatBase
        Public Delegate Sub PropertyChangedHandler(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
        Public Event PropertyChanged As PropertyChangedHandler
        Protected Overridable Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
            RaiseEvent PropertyChanged(Me, e)
        End Sub

        Protected Sub New()
            '...
        End Sub
    End Class


    ''' -----------------------------------------------------------------------------
    ''' Project	 : XPCommonControls
    ''' Class	 : Windows.Forms.TaskBoxFormat
    ''' 
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' this class holds all format information for the xpTaskBox and xpTaskBoxSpecial
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Mike]	18.08.2004	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Serializable()> Public Class TaskBoxFormat
        Inherits ThemeFormatBase

        Private _leftHeaderColor As Color
        Private _rightHeaderColor As Color
        Private _bodyColor As Color
        Private _borderColor As Color
        Private _headerTextColor As Color
        Private _headerTextHighlightColor As Color
        Private _bodyTextColor As Color
        Private _headerFont As Font
        Private _bodyFont As Font
        Private _chevronUp As Bitmap
        Private _chevronUpHighlight As Bitmap
        Private _chevronDown As Bitmap
        Private _chevronDownHighlight As Bitmap

#Region "Public Properties"
        Public Property LeftHeaderColor() As Color
            Get
                Return _leftHeaderColor
            End Get
            Set(ByVal Value As Color)
                _leftHeaderColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("LeftHeaderColor"))
            End Set
        End Property

        Public Property RightHeaderColor() As Color
            Get
                Return _rightHeaderColor
            End Get
            Set(ByVal Value As Color)
                _rightHeaderColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("RightHeaderColor"))
            End Set
        End Property

        Public Property BodyColor() As Color
            Get
                Return _bodyColor
            End Get
            Set(ByVal Value As Color)
                _bodyColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("BodyColor"))
            End Set
        End Property

        Public Property BorderColor() As Color
            Get
                Return _borderColor
            End Get
            Set(ByVal Value As Color)
                _borderColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("BorderColor"))
            End Set
        End Property

        Public Property HeaderTextColor() As Color
            Get
                Return _headerTextColor
            End Get
            Set(ByVal Value As Color)
                _headerTextColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("HeaderTextColor"))
            End Set
        End Property

        Public Property HeaderTextHighlightColor() As Color
            Get
                Return _headerTextHighlightColor
            End Get
            Set(ByVal Value As Color)
                _headerTextHighlightColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("HeaderTextHighlightColor"))
            End Set
        End Property

        Public Property BodyTextColor() As Color
            Get
                Return _bodyTextColor
            End Get
            Set(ByVal Value As Color)
                _bodyTextColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("BodyTextColor"))
            End Set
        End Property

        Public Property HeaderFont() As Font
            Get
                Return _headerFont
            End Get
            Set(ByVal Value As Font)
                _headerFont = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("HeaderFont"))
            End Set
        End Property

        Public Property BodyFont() As Font
            Get
                Return _bodyFont
            End Get
            Set(ByVal Value As Font)
                _bodyFont = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("BodyFont"))
            End Set
        End Property

        Public Property ChevronUp() As Bitmap
            Get
                Return _chevronUp
            End Get
            Set(ByVal Value As Bitmap)
                _chevronUp = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("ChevronUp"))
            End Set
        End Property

        Public Property ChevronUpHighlight() As Bitmap
            Get
                Return _chevronUpHighlight
            End Get
            Set(ByVal Value As Bitmap)
                _chevronUpHighlight = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("ChevronUpHighlight"))
            End Set
        End Property

        Public Property ChevronDown() As Bitmap
            Get
                Return _chevronDown
            End Get
            Set(ByVal Value As Bitmap)
                _chevronDown = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("ChevronDown"))
            End Set
        End Property

        Public Property ChevronDownHighlight() As Bitmap
            Get
                Return _chevronDownHighlight
            End Get
            Set(ByVal Value As Bitmap)
                _chevronDownHighlight = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("ChevronDownHighlight"))
            End Set
        End Property
#End Region

#Region "constructors"
        Public Sub New()

        End Sub

        Public Sub New(ByVal lHdrCol As Color, _
            ByVal rHdrCol As Color, _
            ByVal bodyCol As Color, _
            ByVal borderCol As Color, _
            ByVal hdrTxtCol As Color, _
            ByVal hdrTxtOverCol As Color, _
            ByVal bodyTxtCol As Color, _
            ByVal hdrFnt As Font, _
            ByVal bodyFnt As Font, _
            ByVal chevUp As Bitmap, _
            ByVal chevUpOver As Bitmap, _
            ByVal chevDown As Bitmap, _
            ByVal chevDownOver As Bitmap)

            _leftHeaderColor = lHdrCol
            _rightHeaderColor = rHdrCol
            _bodyColor = bodyCol
            _borderColor = borderCol
            _headerTextColor = hdrTxtCol
            _headerTextHighlightColor = hdrTxtOverCol
            _bodyTextColor = bodyTxtCol
            _headerFont = hdrFnt
            _bodyFont = bodyFnt
            _chevronUp = chevUp
            _chevronUpHighlight = chevUpOver
            _chevronDown = chevDown
            _chevronDownHighlight = chevDownOver
        End Sub
#End Region

        Overrides Function ToString() As String
            Return "TaskBoxFormat"
        End Function
    End Class

    ''' -----------------------------------------------------------------------------
    ''' Project	 : XPCommonControls
    ''' Class	 : Windows.Forms.TaskPanelFormat
    ''' 
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' this class holds all format information for the xpTaskPanel
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Mike]	18.08.2004	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Serializable()> Public Class TaskPanelFormat
        Inherits ThemeFormatBase

        Private _topColor As Color
        Private _bottomColor As Color

#Region "public properties"
        Public Property TopColor() As Color
            Get
                Return _topColor
            End Get
            Set(ByVal Value As Color)
                _topColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("TopColor"))
            End Set
        End Property

        Public Property BottomColor() As Color
            Get
                Return _bottomColor
            End Get
            Set(ByVal Value As Color)
                _bottomColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("BottomColor"))
            End Set
        End Property
#End Region

#Region "constructors"
        Sub New()

        End Sub

        Public Sub New(ByVal topCol As Color, ByVal bottomCol As Color)
            _topColor = topCol
            _bottomColor = bottomCol
        End Sub
#End Region

        Overrides Function ToString() As String
            Return "TaskPanelFormat"
        End Function
    End Class

    ''' -----------------------------------------------------------------------------
    ''' Project	 : XPCommonControls
    ''' Class	 : Windows.Forms.SoftBarrierFormat
    ''' 
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' this clas holds all information for the xpSoftBarrier
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Mike]	18.08.2004	Created
    ''' </history>
    ''' ''' -----------------------------------------------------------------------------
    <Serializable()> Public Class SoftBarrierFormat
        Inherits ThemeFormatBase

        Private _headerHeight As Integer
        Private _headerTextColor As Color
        Private _headerTextFont As Font
        Private _leftHeaderColor As Color
        Private _rightHeaderColor As Color
        Private _topBodyColor As Color
        Private _bottomBodyColor As Color

#Region "public properties"
        Public Property HeaderHeight() As Integer
            Get
                Return _headerHeight
            End Get
            Set(ByVal Value As Integer)
                _headerHeight = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("HeaderHeight"))
            End Set
        End Property

        Public Property HeaderTextColor() As Color
            Get
                Return _headerTextColor
            End Get
            Set(ByVal Value As Color)
                _headerTextColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("HeaderTextColor"))
            End Set
        End Property

        Public Property HeaderTextFont() As Font
            Get
                Return _headerTextFont
            End Get
            Set(ByVal Value As Font)
                _headerTextFont = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("HeaderTextFont"))
            End Set
        End Property

        Public Property LeftHeaderColor() As Color
            Get
                Return _leftHeaderColor
            End Get
            Set(ByVal Value As Color)
                _leftHeaderColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("LeftHeaderColor"))
            End Set
        End Property

        Public Property RightHeaderColor() As Color
            Get
                Return _rightHeaderColor
            End Get
            Set(ByVal Value As Color)
                _rightHeaderColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("RightHeaderColor"))
            End Set
        End Property

        Public Property TopBodyColor() As Color
            Get
                Return _topBodyColor
            End Get
            Set(ByVal Value As Color)
                _topBodyColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("TopBodyColor"))
            End Set
        End Property

        Public Property BottomBodyColor() As Color
            Get
                Return _bottomBodyColor
            End Get
            Set(ByVal Value As Color)
                _bottomBodyColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("BottomBodyColor"))
            End Set
        End Property
#End Region

#Region "constructors"
        Public Sub New()

        End Sub

        Public Sub New(ByVal lHdrCol As Color, _
            ByVal rHdrCol As Color, _
            ByVal topBodyCol As Color, _
            ByVal bottomBodyCol As Color, _
            ByVal hdrTxtCol As Color, _
            ByVal hdrTxtFnt As Font, _
            ByVal hdrHeight As Integer)

            _leftHeaderColor = lHdrCol
            _rightHeaderColor = rHdrCol
            _topBodyColor = topBodyCol
            _bottomBodyColor = bottomBodyCol
            _headerTextColor = hdrTxtCol
            _headerTextFont = hdrTxtFnt
            _headerHeight = hdrHeight
        End Sub
#End Region

        Overrides Function ToString() As String
            Return "SoftBarrierFormat"
        End Function
    End Class

    ''' -----------------------------------------------------------------------------
    ''' Project	 : XPCommonControls
    ''' Class	 : Windows.Forms.LetterBoxFormat
    ''' 
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' this class holds all format information for the letterbox format
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Mike]	18.08.2004	Created
    ''' </history>
    ''' ''' -----------------------------------------------------------------------------
    <Serializable()> Public Class LetterBoxFormat
        Inherits ThemeFormatBase

        Private _headerTextColor As Color
        Private _headerTextFont As Font
        Private _headerColor As Color
        Private _footerColor As Color
        Private _topBodyColor As Color
        Private _bottomBodyColor As Color

#Region "public properties"
        Public Property HeaderTextColor() As Color
            Get
                Return _headerTextColor
            End Get
            Set(ByVal Value As Color)
                _headerTextColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("HeaderTextColor"))
            End Set
        End Property

        Public Property HeaderTextFont() As Font
            Get
                Return _headerTextFont
            End Get
            Set(ByVal Value As Font)
                _headerTextFont = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("HeaderTextFont"))
            End Set
        End Property

        Public Property HeaderColor() As Color
            Get
                Return _headerColor
            End Get
            Set(ByVal Value As Color)
                _headerColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("HeaderColor"))
            End Set
        End Property

        Public Property FooterColor() As Color
            Get
                Return _footerColor
            End Get
            Set(ByVal Value As Color)
                _footerColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("FooterColor"))
            End Set
        End Property

        Public Property TopBodyColor() As Color
            Get
                Return _topBodyColor
            End Get
            Set(ByVal Value As Color)
                _topBodyColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("TopBodyColor"))
            End Set
        End Property

        Public Property BottomBodyColor() As Color
            Get
                Return _bottomBodyColor
            End Get
            Set(ByVal Value As Color)
                _bottomBodyColor = Value
                Me.OnPropertyChanged(New PropertyChangedEventArgs("BottomBodyColor"))
            End Set
        End Property
#End Region

#Region "constructors"
        Public Sub New()

        End Sub

        Public Sub New(ByVal hdrCol As Color, _
            ByVal ftrCol As Color, _
            ByVal topBodyCol As Color, _
            ByVal bottomBodyCol As Color, _
            ByVal hdrTxtCol As Color, _
            ByVal hdrTxtFnt As Font)

            _headerTextColor = hdrTxtCol
            _headerTextFont = hdrTxtFnt
            _headerColor = hdrCol
            _footerColor = ftrCol
            _topBodyColor = topBodyCol
            _bottomBodyColor = bottomBodyCol
        End Sub
#End Region

        Overrides Function ToString() As String
            Return "LetterBoxFormat"
        End Function
    End Class
End Namespace

Namespace ThemedControls
    ''' -----------------------------------------------------------------------------
    ''' Project	 : XPCommonControls
    ''' Interface	 : XPCommonControls.IThemed
    ''' 
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Interface to apply theme to "themeable" controls
    ''' </summary>
    ''' <remarks>
    ''' any control that should be automatically themed by the theme listener
    ''' must implement this interface
    ''' </remarks>
    ''' <history>
    ''' 	[Mike]	14.03.2004	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Interface IThemed
        Property Theme() As ThemeStyle
    End Interface
End Namespace


Namespace ThemedControls
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' the available theme styles in windows XP
    ''' </summary>
    ''' <remarks>
    ''' if a new theme is added, the theming must be changed...
    ''' </remarks>
    ''' <history>
    ''' 	[Mike]	14.03.2004	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Enum ThemeStyle
        NormalColor
        HomeStead
        Metallic
        Unthemed
    End Enum

    'this class themes the controls manually!
    'no need for UxTheme or WinXP

    ''' -----------------------------------------------------------------------------
    ''' Project	 : XPCommonControls
    ''' Class	 : XPCommonControls.Theming
    ''' 
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Theme definition for manually setting theme colors, fonts, ...
    ''' </summary>
    ''' <remarks>
    ''' this class themes the controls manually! There is no need for UxTheme or WinXP.
    ''' The good side: you do not need WinXP to get the themed look.
    ''' The down side: if a new theme is added to WinXP it has to be hardcoded here.
    ''' </remarks>
    ''' <history>
    ''' 	[Mike]	14.03.2004	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class Theming

        Public Shared Function GetTaskBoxThemeGeneric(ByVal theme As ThemeStyle) As TaskBoxFormat
            Dim fmt As New TaskBoxFormat
            Dim path As String = [Enum].GetName(GetType(ThemeStyle), theme) & "_Generic_"

            Select Case theme
                Case ThemeStyle.NormalColor
                    fmt = New TaskBoxFormat( _
                        Color.White, _
                        Color.FromArgb(197, 210, 240), _
                        Color.FromArgb(197, 210, 240), _
                        Color.White, _
                        Color.FromArgb(33, 93, 198), _
                        Color.FromArgb(66, 142, 255), _
                        Color.FromArgb(33, 93, 198), _
                        New Font("Tahoma", 8, FontStyle.Bold), _
                        New Font("Tahoma", 8, FontStyle.Regular), _
                        New Bitmap(GetType(Theming), path & "up.png"), _
                        New Bitmap(GetType(Theming), path & "up_over.png"), _
                        New Bitmap(GetType(Theming), path & "down.png"), _
                        New Bitmap(GetType(Theming), path & "down_over.png"))
                Case ThemeStyle.HomeStead
                    fmt = New TaskBoxFormat( _
                        Color.FromArgb(255, 252, 236), _
                        Color.FromArgb(224, 231, 187), _
                        Color.FromArgb(246, 246, 236), _
                        Color.White, _
                        Color.FromArgb(86, 102, 45), _
                        Color.FromArgb(114, 146, 29), _
                        Color.FromArgb(150, 168, 103), _
                        New Font("Tahoma", 8, FontStyle.Bold), _
                        New Font("Tahoma", 8, FontStyle.Regular), _
                        New Bitmap(GetType(Theming), path & "up.png"), _
                        New Bitmap(GetType(Theming), path & "up_over.png"), _
                        New Bitmap(GetType(Theming), path & "down.png"), _
                        New Bitmap(GetType(Theming), path & "down_over.png"))
                Case ThemeStyle.Metallic
                    fmt = New TaskBoxFormat( _
                        Color.White, _
                        Color.FromArgb(216, 217, 226), _
                        Color.FromArgb(240, 241, 245), _
                        Color.White, _
                        Color.FromArgb(63, 61, 61), _
                        Color.FromArgb(126, 124, 124), _
                        Color.FromArgb(104, 104, 127), _
                        New Font("Tahoma", 8, FontStyle.Bold), _
                        New Font("Tahoma", 8, FontStyle.Regular), _
                        New Bitmap(GetType(Theming), path & "up.png"), _
                        New Bitmap(GetType(Theming), path & "up_over.png"), _
                        New Bitmap(GetType(Theming), path & "down.png"), _
                        New Bitmap(GetType(Theming), path & "down_over.png"))
                Case ThemeStyle.Unthemed
                    fmt = New TaskBoxFormat( _
                        SystemColors.InactiveCaption, _
                        SystemColors.InactiveCaption, _
                        SystemColors.ControlLightLight, _
                        Color.White, _
                        SystemColors.InactiveCaptionText, _
                        SystemColors.ActiveCaptionText, _
                        SystemColors.WindowText, _
                        SystemInformation.MenuFont, _
                        SystemInformation.MenuFont, _
                        New Bitmap(GetType(Theming), path & "up.png"), _
                        New Bitmap(GetType(Theming), path & "up_over.png"), _
                        New Bitmap(GetType(Theming), path & "down.png"), _
                        New Bitmap(GetType(Theming), path & "down_over.png"))
            End Select

            Return fmt
        End Function

        Public Shared Function GetTaskBoxThemeSpecial(ByVal theme As ThemeStyle) As TaskBoxFormat
            Dim fmt As New TaskBoxFormat
            Dim path As String = [Enum].GetName(GetType(ThemeStyle), theme) & "_Special_"

            Select Case theme
                Case ThemeStyle.NormalColor

                    fmt = New TaskBoxFormat( _
                        Color.FromArgb(1, 72, 178), _
                        Color.FromArgb(40, 91, 197), _
                        Color.FromArgb(239, 243, 255), _
                        Color.White, _
                        Color.White, _
                        Color.FromArgb(66, 142, 255), _
                        Color.FromArgb(33, 93, 198), _
                        New Font("Tahoma", 8, FontStyle.Bold), _
                        New Font("Tahoma", 8, FontStyle.Regular), _
                        New Bitmap(GetType(Theming), path & "up.png"), _
                        New Bitmap(GetType(Theming), path & "up_over.png"), _
                        New Bitmap(GetType(Theming), path & "down.png"), _
                        New Bitmap(GetType(Theming), path & "down_over.png"))

                Case ThemeStyle.HomeStead
                    fmt = New TaskBoxFormat( _
                        Color.FromArgb(122, 142, 67), _
                        Color.FromArgb(150, 168, 103), _
                        Color.FromArgb(246, 246, 236), _
                        Color.White, _
                        Color.White, _
                        Color.FromArgb(224, 231, 184), _
                        Color.FromArgb(150, 168, 103), _
                        New Font("Tahoma", 8, FontStyle.Bold), _
                        New Font("Tahoma", 8, FontStyle.Regular), _
                        New Bitmap(GetType(Theming), path & "up.png"), _
                        New Bitmap(GetType(Theming), path & "up_over.png"), _
                        New Bitmap(GetType(Theming), path & "down.png"), _
                        New Bitmap(GetType(Theming), path & "down_over.png"))
                Case ThemeStyle.Metallic
                    fmt = New TaskBoxFormat( _
                        Color.FromArgb(119, 119, 146), _
                        Color.FromArgb(179, 181, 199), _
                        Color.FromArgb(240, 241, 245), _
                        Color.White, _
                        Color.White, _
                        Color.FromArgb(230, 230, 230), _
                        Color.FromArgb(104, 104, 127), _
                        New Font("Tahoma", 8, FontStyle.Bold), _
                        New Font("Tahoma", 8, FontStyle.Regular), _
                        New Bitmap(GetType(Theming), path & "up.png"), _
                        New Bitmap(GetType(Theming), path & "up_over.png"), _
                        New Bitmap(GetType(Theming), path & "down.png"), _
                        New Bitmap(GetType(Theming), path & "down_over.png"))
                Case ThemeStyle.Unthemed
                    fmt = New TaskBoxFormat( _
                        SystemColors.ActiveCaption, _
                        SystemColors.ActiveCaption, _
                        SystemColors.ControlLightLight, _
                        Color.White, _
                        SystemColors.InactiveCaptionText, _
                        SystemColors.ActiveCaptionText, _
                        SystemColors.WindowText, _
                        SystemInformation.MenuFont, _
                        SystemInformation.MenuFont, _
                        New Bitmap(GetType(Theming), path & "up.png"), _
                        New Bitmap(GetType(Theming), path & "up_over.png"), _
                        New Bitmap(GetType(Theming), path & "down.png"), _
                        New Bitmap(GetType(Theming), path & "down_over.png"))
            End Select

            Return fmt
        End Function

        Public Shared Function GetTaskPanelTheme(ByVal theme As ThemeStyle) As TaskPanelFormat
            Dim fmt As New TaskPanelFormat

            Select Case theme
                Case ThemeStyle.NormalColor
                    fmt = New TaskPanelFormat(Color.FromArgb(82, 130, 210), Color.FromArgb(40, 91, 197))
                Case ThemeStyle.HomeStead
                    fmt = New TaskPanelFormat(Color.FromArgb(203, 216, 172), Color.FromArgb(165, 189, 132))
                Case ThemeStyle.Metallic
                    fmt = New TaskPanelFormat(Color.FromArgb(195, 199, 211), Color.FromArgb(177, 179, 200))
                Case ThemeStyle.Unthemed
                    fmt = New TaskPanelFormat(SystemColors.ControlLight, SystemColors.ControlLight)

            End Select
            Return fmt
        End Function

        Public Shared Function GetSoftBarrierTheme(ByVal theme As ThemeStyle) As SoftBarrierFormat
            Dim fmt As New SoftBarrierFormat

            Select Case theme
                Case ThemeStyle.NormalColor
                    fmt = New SoftBarrierFormat(Color.FromArgb(0, 51, 153), _
                        Color.FromArgb(40, 91, 197), _
                        Color.FromArgb(85, 130, 210), _
                        Color.FromArgb(90, 126, 220), _
                        Color.FromArgb(214, 223, 245), _
                        New Font("Franklin Gothic Medium", 14, FontStyle.Bold), _
                        48)
                Case ThemeStyle.HomeStead
                    fmt = New SoftBarrierFormat(Color.FromArgb(150, 168, 103), _
                        Color.FromArgb(164, 168, 103), _
                        Color.FromArgb(165, 189, 132), _
                        Color.FromArgb(165, 189, 132), _
                        Color.FromArgb(224, 231, 184), _
                        New Font("Franklin Gothic Medium", 14, FontStyle.Bold), _
                        48)
                Case ThemeStyle.Metallic
                    fmt = New SoftBarrierFormat(Color.FromArgb(119, 119, 146), _
                        Color.FromArgb(176, 178, 199), _
                        Color.FromArgb(177, 179, 200), _
                        Color.FromArgb(177, 179, 200), _
                        Color.White, _
                        New Font("Franklin Gothic Medium", 14, FontStyle.Bold), _
                        48)
                Case ThemeStyle.Unthemed
                    fmt = New SoftBarrierFormat(SystemColors.ActiveCaption, _
                        SystemColors.InactiveCaption, _
                        SystemColors.ControlLight, _
                        SystemColors.ControlLightLight, _
                        SystemColors.ActiveCaptionText, _
                        New Font("Franklin Gothic Medium", 14, FontStyle.Bold), _
                        48)
            End Select

            Return fmt
        End Function

        Public Shared Function GetLetterboxTheme(ByVal theme As ThemeStyle) As LetterBoxFormat
            Dim fmt As New LetterBoxFormat

            Select Case theme
                Case ThemeStyle.NormalColor
                    fmt = New LetterBoxFormat( _
                        Color.FromArgb(0, 51, 153), _
                        Color.FromArgb(0, 51, 153), _
                        Color.FromArgb(85, 130, 210), _
                        Color.FromArgb(90, 126, 220), _
                        Color.FromArgb(214, 223, 245), _
                        New Font("Franklin Gothic Medium", 14, FontStyle.Bold))
                Case ThemeStyle.HomeStead
                    fmt = New LetterBoxFormat( _
                        Color.FromArgb(150, 168, 103), _
                        Color.FromArgb(150, 168, 103), _
                        Color.FromArgb(165, 189, 132), _
                        Color.FromArgb(165, 189, 132), _
                        Color.FromArgb(224, 231, 184), _
                        New Font("Franklin Gothic Medium", 14, FontStyle.Bold))
                Case ThemeStyle.Metallic
                    fmt = New LetterBoxFormat( _
                        Color.FromArgb(119, 119, 146), _
                        Color.FromArgb(119, 119, 146), _
                        Color.FromArgb(177, 179, 200), _
                        Color.FromArgb(177, 179, 200), _
                        Color.White, _
                        New Font("Franklin Gothic Medium", 14, FontStyle.Bold))
                Case ThemeStyle.Unthemed
                    fmt = New LetterBoxFormat( _
                        SystemColors.ActiveCaption, _
                        SystemColors.ActiveCaption, _
                        SystemColors.InactiveCaption, _
                        SystemColors.InactiveCaption, _
                        Color.White, _
                        New Font("Franklin Gothic Medium", 14, FontStyle.Bold))
            End Select

            Return fmt
        End Function

    End Class
End Namespace



Namespace ThemedControls

    ''' -----------------------------------------------------------------------------
    ''' Project	 : XPCommonControls
    ''' Class	 : XPCommonControls.XPTaskPanel
    ''' 
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' hosts the taskbox(es)
    ''' </summary>
    ''' <remarks>
    ''' the control is painted with the specified themeing and hosts any taskbox or
    ''' any other control
    ''' </remarks>
    ''' <history>
    ''' 	[Mike]	14.03.2004	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class XPTaskPanel
        Inherits System.Windows.Forms.Panel
        Implements IThemed

        Private mTheme As ThemeStyle = ThemeStyle.NormalColor
        Private mThemeFormat As TaskPanelFormat = Theming.GetTaskPanelTheme(mTheme)

#Region " Vom Windows Form Designer generierter Code "

        Public Sub New()
            MyBase.New()

            ' Dieser Aufruf ist für den Windows Form-Designer erforderlich.
            InitializeComponent()

            ' Initialisierungen nach dem Aufruf InitializeComponent() hinzufügen
            SetStyle(ControlStyles.ResizeRedraw, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            SetStyle(ControlStyles.UserPaint, True)
            SetStyle(ControlStyles.DoubleBuffer, True)
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            SetStyle(ControlStyles.ContainerControl, True)

            MyBase.BackColor = Color.Transparent
        End Sub

        'UserControl überschreibt den Löschvorgang zum Bereinigen der Komponentenliste.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        ' Für Windows Form-Designer erforderlich
        Private components As System.ComponentModel.IContainer

        'HINWEIS: Die folgende Prozedur ist für den Windows Form-Designer erforderlich
        'Sie kann mit dem Windows Form-Designer modifiziert werden.
        'Verwenden Sie nicht den Code-Editor zur Bearbeitung.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            '
            'TaskPanel
            '
            Me.AutoScroll = True
            Me.DockPadding.Bottom = 8
            Me.DockPadding.Left = 8
            Me.DockPadding.Right = 8

        End Sub

#End Region

#Region "public properties"
        <Description("Sets the theming of the control"), _
         Category("Appearance"), _
         Browsable(True), _
         DefaultValue(GetType(ThemeStyle), "NormalColor")> _
        Public Property Theme() As ThemeStyle Implements IThemed.Theme
            Get
                Return mTheme
            End Get
            Set(ByVal value As ThemeStyle)
                mTheme = value
                mThemeFormat = Theming.GetTaskPanelTheme(mTheme)
                Me.Invalidate()
            End Set
        End Property

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' The Theming Info of this control
        ''' </summary>
        ''' <value></value>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Mike]	18.08.2004	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        <Description("The Theming Info of this control"), _
         Category("Appearance"), _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Content), _
         TypeConverter(GetType(ExpandableObjectConverter))> _
        Public Property ThemeFormat() As TaskPanelFormat
            Get
                Return mThemeFormat
            End Get
            Set(ByVal Value As TaskPanelFormat)
                mThemeFormat = Value
                Me.Invalidate()
            End Set
        End Property

#End Region

#Region "overriden methods"
        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            'Dim lrectCtrl As Rectangle = CtrlHelper.CheckedRectangle(0, 0, Me.Width, Me.Height)
            'Dim lbrushBackground As New LinearGradientBrush(lrectCtrl, mThemeFormat.TopColor, mThemeFormat.BottomColor, LinearGradientMode.Vertical)

            'e.Graphics.FillRectangle(lbrushBackground, lrectCtrl)

            'lbrushBackground.Dispose()

            MyBase.OnPaint(e)
        End Sub
#End Region
    End Class
End Namespace

