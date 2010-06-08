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

Public Class EnhancedProgressBar
    Public Class Progress
        Implements IDisposable

        Public PercentDone As Double
        Public Text As String
        Public Number As Integer

        Private m_ColorBrush As Brush

        Property ColorBrush() As Brush
            Get
                Return m_ColorBrush
            End Get
            Set(ByVal value As Brush)
                If m_ColorBrush IsNot Nothing Then m_ColorBrush.Dispose()
                m_ColorBrush = value
            End Set
        End Property

        Private disposed As Boolean = False

        ' IDisposable
        Private Overloads Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposed Then
                If disposing Then
                    ColorBrush = Nothing
                End If
            End If
            Me.disposed = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Overloads Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overrides Sub Finalize()
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(False)
            MyBase.Finalize()
        End Sub
#End Region

    End Class

    Private m_BackColorBrush As Brush
    Private m_DrawingRegion As Drawing2D.GraphicsPath
    Private Const RECTRADIUS As Integer = 2

    Private m_Values() As Progress
    Private m_TextFormat As StringFormat

    Property ValueCount() As Integer
        Get
            If m_Values IsNot Nothing Then
                Return m_Values.Length
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            ReDim m_Values(value - 1)
            For i As Integer = 0 To value - 1
                m_Values(i) = New Progress
            Next
        End Set
    End Property

    ReadOnly Property Value(ByVal Index As Integer) As Progress
        Get
            Return m_Values(Index)
        End Get
    End Property

    Private Property BackColorBrush() As Brush
        Get
            If m_BackColorBrush Is Nothing Then m_BackColorBrush = New SolidBrush(BackColor)
            Return m_BackColorBrush
        End Get
        Set(ByVal value As Brush)
            If m_BackColorBrush IsNot Nothing Then m_BackColorBrush.Dispose()
            m_BackColorBrush = value
        End Set
    End Property

    Public Overrides Property BackColor() As System.Drawing.Color
        Get
            Return MyBase.BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            If value <> MyBase.BackColor Then
                MyBase.BackColor = value
                BackColorBrush = Nothing
                Me.Invalidate()
            End If
        End Set
    End Property

    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        Dim r As New RectangleF(New PointF(0, 0), New SizeF(Me.Size.Width, Me.Size.Height))
        r.Height -= 1
        r.Width -= 1
        m_DrawingRegion = GetRoundedRect(r, RECTRADIUS)
    End Sub

    Private Function GetRoundedRect(ByVal BaseRect As RectangleF, ByVal Radius As Single, Optional ByVal OnlyLeftRounded As Boolean = False, Optional ByVal OnlyRightRounded As Boolean = False) As Drawing2D.GraphicsPath
        Dim RR As New Drawing2D.GraphicsPath()
        ' If corner radius is less than or equal to zero, return the original Rectangle
        If Radius <= 0 Then
            RR.AddRectangle(BaseRect)
            Return RR
        End If
        ' If corner radius is greater than or equal to half the width or height (whichever is shorter) then
        ' return a capsule instead of a lozenge.
        If Radius >= (Math.Min(BaseRect.Width, BaseRect.Height) / 2.0) Then Return GetCapsule(BaseRect)

        Dim Diameter As Single = Radius + Radius
        Dim ArcRect As New RectangleF(BaseRect.Location, New SizeF(Diameter, Diameter))

        With RR
            If OnlyRightRounded = False Then
                ' top left arc
                .AddArc(ArcRect, 180, 90)
            Else
                .AddLine(BaseRect.Location, BaseRect.Location)
            End If
            ArcRect.X = BaseRect.Right - Diameter
            If OnlyLeftRounded = False Then
                ' top right arc
                .AddArc(ArcRect, 270, 90)
            Else
                .AddLine(New PointF(BaseRect.Right, BaseRect.Top), New PointF(BaseRect.Right, BaseRect.Top))
            End If
            ArcRect.Y = BaseRect.Bottom - Diameter
            If OnlyLeftRounded = False Then
                ' bottom right arc
                .AddArc(ArcRect, 0, 90)
            Else
                .AddLine(New PointF(BaseRect.Right, BaseRect.Bottom), New PointF(BaseRect.Right, BaseRect.Bottom))
            End If
            If OnlyRightRounded = False Then
                ' bottom left arc
                ArcRect.X = BaseRect.Left
                .AddArc(ArcRect, 90, 90)
            Else
                .AddLine(New PointF(BaseRect.Left, BaseRect.Bottom), New PointF(BaseRect.Left, BaseRect.Bottom))
            End If
            .CloseFigure()
        End With

        Return RR
    End Function

    Private Function GetCapsule(ByVal BaseRect As RectangleF) As Drawing2D.GraphicsPath
        Dim Diameter As Single
        Dim ArcRect As RectangleF
        Dim RR As New Drawing2D.GraphicsPath()

        With RR
            Try
                If BaseRect.Width > BaseRect.Height AndAlso BaseRect.Height > 0 Then
                    ' Return horizontal capsule
                    Diameter = BaseRect.Height
                    ArcRect = New RectangleF(BaseRect.Location, New SizeF(Diameter, Diameter))
                    .AddArc(ArcRect, 90, 180)
                    ArcRect.X = BaseRect.Right - Diameter
                    .AddArc(ArcRect, 270, 180)

                ElseIf BaseRect.Height > BaseRect.Width AndAlso BaseRect.Width > 0 Then
                    ' Return vertical capsule
                    Diameter = BaseRect.Width
                    ArcRect = New RectangleF(BaseRect.Location, New SizeF(Diameter, Diameter))
                    .AddArc(ArcRect, 180, 180)
                    ArcRect.Y = BaseRect.Bottom - Diameter
                    .AddArc(ArcRect, 0, 180)

                Else
                    ' return circle
                    .AddEllipse(BaseRect)
                End If

            Catch e As Exception
                .AddEllipse(BaseRect)
            Finally
                .CloseFigure()
            End Try
        End With

        Return RR
    End Function

    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer, True)

        m_TextFormat = New StringFormat(StringFormat.GenericTypographic)
        m_TextFormat.Alignment = StringAlignment.Center
        m_TextFormat.LineAlignment = StringAlignment.Center
    End Sub


    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Try
            Dim str As String = String.Empty
            Dim total As Integer

            If DesignMode Then Return

            If m_DrawingRegion Is Nothing Then Exit Sub
            e.Graphics.DrawPath(Pens.LightGray, m_DrawingRegion)

            If m_Values IsNot Nothing AndAlso UBound(m_Values) > 0 Then
                Dim accumulated(UBound(m_Values)) As Double

                For i As Integer = 0 To UBound(m_Values)
                    total += m_Values(i).Number
                    If i = 0 Then
                        accumulated(0) = m_Values(0).PercentDone
                    Else
                        accumulated(i) = accumulated(i - 1) + m_Values(i).PercentDone
                    End If
                Next

                For i As Integer = UBound(m_Values) To 0 Step -1
                    If accumulated(i) >= 1 Then
                        e.Graphics.FillPath(m_Values(i).ColorBrush, m_DrawingRegion)
                    Else
                        Dim lrect As RectangleF

                        lrect.Size = New SizeF(Me.Size.Width, Me.Size.Height) 'lrect = e.ClipRectangle
                        If Double.IsInfinity(accumulated(i)) OrElse Double.IsNaN(accumulated(i)) Then accumulated(i) = 0
                        lrect.Width = CInt(Me.Width * accumulated(i)) 'lrect.Width = CInt(e.ClipRectangle.Width * accumulated(i) + e.ClipRectangle.Left)
                        lrect.Height -= 1

                        Using lReg As Drawing2D.GraphicsPath = Me.GetRoundedRect(New RectangleF(New PointF(0, 0), New SizeF(lrect.Size.Width, lrect.Size.Height)), RECTRADIUS, True)
                            e.Graphics.FillPath(m_Values(i).ColorBrush, lReg)
                        End Using
                    End If

                    If m_Values(i).Number <> 0 Then
                        If str.Length > 0 Then str &= "   "
                        str = str & String.Format(m_Values(i).Text, m_Values(i).Number, m_Values(i).PercentDone)
                    End If
                Next
                If str.Length > 0 Then
                    str &= "   Total: " & total
                    e.Graphics.DrawString(str, Me.Font, Brushes.White, New RectangleF(0, 0, Me.Size.Width, Me.Size.Height), m_TextFormat)
                End If
            End If
        Catch ex As System.Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Exclamation)
        End Try
    End Sub

End Class
