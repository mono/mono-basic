Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Globalization
Imports System.Math
Imports System.Resources
Imports System.Windows.Forms

Public Class MainForm
    Inherits Form

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
    End Sub

    Shared Sub Main
        Application.Run (New MainForm ())
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As IContainer

    Friend WithEvents Button1 As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents RImage1 As RImage
    Friend WithEvents angle As NumericUpDown
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents CheckBox1 As CheckBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New Container
        Me.Button1 = New Button
        Me.Timer1 = New Timer(Me.components)
        Me.RImage1 = New RImage
        Me.angle = New NumericUpDown
        Me.ComboBox1 = New ComboBox
        Me.CheckBox1 = New CheckBox
        CType(Me.angle, ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 50
        '
        'RImage1
        '
        Me.RImage1.BackColor = Color.Transparent
        Me.RImage1.BorderStyle = BorderStyle.FixedSingle
        Me.RImage1.Direction = RImage.DirectionEnum.Clockwise
        Me.RImage1.Image = Icon.ToBitmap ()
        Me.RImage1.Location = New Point(48, 64)
        Me.RImage1.Name = "RImage1"
        Me.RImage1.Rotation = 0
        Me.RImage1.ShowThrough = False
        Me.RImage1.Size = New Size(186, 96)
        Me.RImage1.SizeMode = PictureBoxSizeMode.CenterImage
        Me.RImage1.TabIndex = 2
        Me.RImage1.TabStop = False
        Me.RImage1.TransparentColor = Color.Transparent
        Me.Controls.Add(Me.RImage1)
        '
        'Button1
        '
        Me.Button1.BackColor = SystemColors.ActiveCaption
        Me.Button1.Location = New Point(48, 64)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New Size(186, 96)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Button1"
        Me.Controls.Add(Me.Button1)
        '
        'angle
        '
        Me.angle.Location = New Point(8, 8)
        Me.angle.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.angle.Minimum = New Decimal(New Integer() {1, 0, 0, -2147483648})
        Me.angle.Name = "angle"
        Me.angle.Size = New Size(72, 22)
        Me.angle.TabIndex = 3
        Me.Controls.Add(Me.angle)
        '
        'ComboBox1
        '
        Me.ComboBox1.Items.AddRange(New Object() {"Normal", "AutoSize", "Center", "Stretch"})
        Me.ComboBox1.Location = New Point(96, 8)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New Size(168, 24)
        Me.ComboBox1.TabIndex = 4
        Me.ComboBox1.Text = "Center"
        Me.Controls.Add(Me.ComboBox1)
        '
        'CheckBox1
        '
        Me.CheckBox1.Location = New Point(80, 32)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New Size(128, 24)
        Me.CheckBox1.TabIndex = 5
        Me.CheckBox1.Text = "Show Through"
        Me.Controls.Add(Me.CheckBox1)
        '
        'MainForm
        '
        Me.AutoScaleBaseSize = New Size(6, 15)
        Me.BackColor = SystemColors.Control
        Me.ClientSize = New Size(292, 180)
		Me.Location = new Point (200, 100)
		Me.StartPosition = FormStartPosition.Manual
        Me.Text = "bug #82370"
        CType(Me.angle, ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		Dim instructionsForm As InstructionsForm = New InstructionsForm ()
		instructionsForm.Show ()
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        RImage1.Rotation = 45
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        If RImage1.Rotation = 350 Then
            RImage1.Rotation += 10
            If RImage1.Direction = RImage.DirectionEnum.Clockwise Then
                RImage1.Direction = RImage.DirectionEnum.Counter_Clockwise
                RImage1.ShowThrough = True
            Else
                RImage1.Direction = RImage.DirectionEnum.Clockwise
                RImage1.ShowThrough = False
            End If
            Exit Sub
        End If
        RImage1.Rotation += 10
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles angle.ValueChanged
        If angle.Value > 359.9 Then
            angle.Value = 0
            Exit Sub
        End If
        If angle.Value < 0.0 Then
            angle.Value = 359
            Exit Sub
        End If
        RImage1.Rotation = angle.Value
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.Text
            Case "Normal"
                RImage1.SizeMode = PictureBoxSizeMode.Normal
            Case "AutoSize"
                RImage1.SizeMode = PictureBoxSizeMode.AutoSize
            Case "Stretch"
                RImage1.SizeMode = PictureBoxSizeMode.StretchImage
            Case "Center"
                RImage1.SizeMode = PictureBoxSizeMode.CenterImage
        End Select
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBox1.CheckedChanged
        RImage1.ShowThrough = CheckBox1.Checked
    End Sub
End Class

Public Class InstructionsForm
    Inherits Form

	Public Sub New ()
		' 
		' _tabControl
		' 
		_tabControl = new TabControl ()
		_tabControl.Dock = DockStyle.Fill
		Controls.Add (_tabControl)
		' 
		' _bugDescriptionText1
		' 
		_bugDescriptionText1 = new TextBox ()
		_bugDescriptionText1.Dock = DockStyle.Fill
		_bugDescriptionText1.Multiline = true
		_bugDescriptionText1.Text = String.Format (CultureInfo.InvariantCulture, _
			"Expected result on start-up:{0}{0}" _
			+ "1. The image is displayed in the center of the PictureBox.{0}{0}" _
            + "2. The button is not displayed.", _
			Environment.NewLine)
		' 
		' _tabPage1
		' 
		_tabPage1 = new TabPage ()
		_tabPage1.Text = "#1"
		_tabPage1.Controls.Add (_bugDescriptionText1)
		_tabControl.Controls.Add (_tabPage1)
		' 
		' _bugDescriptionText2
		' 
		_bugDescriptionText2 = new TextBox ()
		_bugDescriptionText2.Dock = DockStyle.Fill
		_bugDescriptionText2.Multiline = true
		_bugDescriptionText2.Text = String.Format (CultureInfo.InvariantCulture, _
			"Steps to execute:{0}{0}" _
			+ "1. Check the Show Through checkbox.{0}{0}" _
			+ "2. Uncheck the Show Through checkbox.{0}{0}" _
			+ "3. Check the Show Through checkbox.{0}{0}" _
            + "Expected result:{0}{0}" _
            + "1. On step 1 and 3, the button is displayed underneath the " _
            + "image.{0}{0}" _
            + "1. On step 2, the button is not displayed.", _
			Environment.NewLine)
		' 
		' _tabPage2
		' 
		_tabPage2 = new TabPage ()
		_tabPage2.Text = "#2"
		_tabPage2.Controls.Add (_bugDescriptionText2)
		_tabControl.Controls.Add (_tabPage2)
		' 
		' InstructionsForm
		' 
		ClientSize = new Size (300, 230)
		Location = new Point (650, 100)
		StartPosition = FormStartPosition.Manual
		Text = "Instructions - bug #82376"
	End Sub

	Private _bugDescriptionText1 As TextBox
	Private _bugDescriptionText2 As TextBox
	Private _tabControl As TabControl
	Private _tabPage1 as TabPage
	Private _tabPage2 as TabPage
End Class

Public Class RImage
    Inherits PictureBox

    Private _degree As Integer = 0
    Private _sizemode As PictureBoxSizeMode
    Private _transColor As Color
    Private _direction As DirectionEnum = DirectionEnum.Clockwise
    Private _showThrough As Boolean

    Public Sub New()
        MyBase.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
    End Sub

    <Description("Space not filled in by image shows the controls beneath it.")> Public Property ShowThrough() As Boolean
        Get
            Return _showThrough
        End Get
        Set(ByVal Value As Boolean)
            _showThrough = Value
            Me.Invalidate()
        End Set
    End Property

    <Description("Controls the direction of the rotation.")> Public Property Direction() As DirectionEnum
        Get
            Return _direction
        End Get
        Set(ByVal Value As DirectionEnum)
            _direction = Value
            Me.Invalidate()
        End Set
    End Property

    <Description("The angle of rotation (in degrees).")> Public Property Rotation() As Integer
        Get
            Return _degree
        End Get
        Set(ByVal Value As Integer)
            _degree = ValidRotation(Value)
            Me.Invalidate()
        End Set
    End Property

    <Description("The color in the image to make transparent.  Web->Transparent is none.")> Public Property TransparentColor() As Color
        Get
            Return _transColor
        End Get
        Set(ByVal Value As Color)
            _transColor = Value
            Me.Invalidate()
        End Set
    End Property

    Public Shadows Property SizeMode() As PictureBoxSizeMode
        Get
            Return _sizemode
        End Get
        Set(ByVal Value As PictureBoxSizeMode)
            _sizemode = Value
            Me.Invalidate()
        End Set
    End Property

    Private Function ValidRotation(ByVal Value As Integer) As Integer
        If Value >= 0 And Value < 360 Then
            Return Value
        End If
        If Value >= 360 Then
            Value -= 360
        ElseIf Value < 0 Then
            Value += 360
        End If
        Value = ValidRotation(Value)
        Return Value
    End Function

    Protected Overrides Sub OnPaint(ByVal pe As PaintEventArgs)
        If MyBase.Image Is Nothing Then
            Dim b As Brush
            b = New SolidBrush(Me.BackColor)
            pe.Graphics.FillRectangle(b, 0, 0, MyBase.Width, MyBase.Height)
            Exit Sub
        End If
        Dim bm_in As New Bitmap(MyBase.Image)

        Dim wid As Single = bm_in.Width
        Dim hgt As Single = bm_in.Height

        Dim corners As Point() = { _
             New Point(0, 0), _
             New Point(wid, 0), _
             New Point(0, hgt), _
             New Point(wid, hgt)}

        Dim cx As Single = wid / 2
        Dim cy As Single = hgt / 2
        Dim i As Long
        For i = 0 To 3
            corners(i).X -= cx
            corners(i).Y -= cy
        Next

        Dim theta As Single = CSng((_degree) * _direction) * PI / 180

        Dim sin_theta As Single = Sin(theta)
        Dim cos_theta As Single = Cos(theta)

        Dim X As Single
        Dim Y As Single
        For i = 0 To 3
            X = corners(i).X
            Y = corners(i).Y
            corners(i).X = (X * cos_theta) - (Y * sin_theta)
            corners(i).Y = (Y * cos_theta) + (X * sin_theta)
        Next

        Dim xmin As Single = corners(0).X
        Dim ymin As Single = corners(0).Y
        For i = 1 To 3
            If xmin > corners(i).X Then xmin = corners(i).X
            If ymin > corners(i).Y Then ymin = corners(i).Y
        Next
        For i = 0 To 3
            corners(i).X -= xmin
            corners(i).Y -= ymin
        Next
        Dim bm_out As New Bitmap(CInt(-2 * xmin), CInt(-2 * ymin))
        Dim bgr As Graphics = Graphics.FromImage(bm_out)
        Dim rg As Region = CreateTransRegion(corners)
        Dim tp As Point = corners(3)
        ReDim Preserve corners(2)
        bgr.DrawImage(bm_in, corners)

        Dim gr_out As Graphics = pe.Graphics
        gr_out.FillRectangle(New SolidBrush(Me.BackColor), 0, 0, Me.Width, Me.Height)
        bm_in.MakeTransparent(_transColor)
        If _sizemode = PictureBoxSizeMode.StretchImage Then
            Dim maxW As Integer = tp.X
            Dim maxH As Integer = tp.Y
            For t As Integer = 0 To 2
                If maxW < corners(t).X Then maxW = corners(t).X
                If maxH < corners(t).Y Then maxH = corners(t).Y
            Next
            'get hscale
            Dim hscale As Double = Me.Width / maxW
            'get vscale
            Dim vscale As Double = Me.Height / maxH
            'convert points
            corners(0) = New Point(corners(0).X * hscale, corners(0).Y * vscale)
            corners(1) = New Point(corners(1).X * hscale, corners(1).Y * vscale)
            corners(2) = New Point(corners(2).X * hscale, corners(2).Y * vscale)
            gr_out.DrawImage(bm_out, 0, 0, Me.Width, Me.Height)
            Dim np(3) As Point
            np(0) = corners(0)
            np(1) = corners(1)
            np(2) = corners(2)
            np(3) = New Point(tp.X * hscale, tp.Y * vscale)
            rg = CreateTransRegion(np)
        ElseIf _sizemode = PictureBoxSizeMode.CenterImage Then
            Dim wadd As Integer = CInt((Me.Width / 2) - (bm_out.Width / 2))
            Dim hadd As Integer = CInt((Me.Height / 2) - (bm_out.Height / 2))
            corners(0) = New Point(corners(0).X + wadd, corners(0).Y + hadd)
            corners(1) = New Point(corners(1).X + wadd, corners(1).Y + hadd)
            corners(2) = New Point(corners(2).X + wadd, corners(2).Y + hadd)
            gr_out.DrawImage(bm_in, corners)
            Dim np(3) As Point
            np(0) = corners(0)
            np(1) = corners(1)
            np(2) = corners(2)
            np(3) = New Point(tp.X + wadd, tp.Y + hadd)
            rg = CreateTransRegion(np)
        Else
            gr_out.DrawImage(bm_in, corners)
        End If
        If _sizemode = PictureBoxSizeMode.AutoSize Then
            MyBase.Width = bm_out.Width
            MyBase.Height = bm_out.Height
        End If
        Me.Region = Nothing
        If _showThrough Then
            Me.Region = rg
        End If
    End Sub

    Private Function CreateTransRegion(ByVal points() As Point) As Region
        Dim m_p(3) As Point
        m_p(0) = points(0)
        m_p(1) = points(1)
        m_p(2) = points(3)
        m_p(3) = points(2)
        Dim p_types(3) As Byte
        For i As Integer = 0 To 3
            p_types(i) = CByte(PathPointType.Line)
        Next
        Dim path As New GraphicsPath(m_p, p_types)
        Dim p_region As New Region(path)
        Return p_region
    End Function

    Public Enum DirectionEnum
        Counter_Clockwise = -1
        Clockwise = 1
    End Enum
End Class
