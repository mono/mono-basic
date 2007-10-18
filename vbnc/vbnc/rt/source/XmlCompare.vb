' 
' Visual Basic.Net COmpiler
' Copyright (C) 2004 - 2006 Rolf Bjarne Kvinge, rbjarnek at users.sourceforge.net
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
''' Represents the result of a xml compare
''' </summary>
''' <remarks></remarks>
Class XmlCompare

	''' <summary>
	''' First file to compare
	''' </summary>
	''' <remarks></remarks>
	Private m_File1 As String

	''' <summary>
	''' Second file to compare
	''' </summary>
	''' <remarks></remarks>
	Private m_File2 As String

	''' <summary>
	''' The result of the comparison as a descriptive string value
	''' </summary>
	''' <remarks></remarks>
	Private m_Result As String

	''' <summary>
	''' Are the files equal?
	''' </summary>
	''' <remarks></remarks>
	Private m_Equal As Boolean

	''' <summary>
	''' The private constructor.
	''' </summary>
	''' <remarks></remarks>
	Private Sub New()
		'Only a private constructor.
	End Sub

	''' <summary>
	''' Are the files equal?
	''' </summary>
	''' <remarks></remarks>
	ReadOnly Property Equal() As Boolean
		Get
			Return m_Equal
		End Get
    End Property

	''' <summary>
	''' First file to compare
	''' </summary>
	''' <value></value>
	''' <remarks></remarks>
	ReadOnly Property File1() As String
		Get
			Return m_File1
		End Get
	End Property

	''' <summary>
	''' Second file to compare
	''' </summary>
	''' <value></value>
	''' <remarks></remarks>
	ReadOnly Property File2() As String
		Get
			Return m_File2
		End Get
	End Property

	''' <summary>
	''' The result of the comparison as a descriptive string value
	''' </summary>
	''' <remarks></remarks>
	ReadOnly Property Result() As String
		Get
			Return m_Result
		End Get
	End Property

	''' <summary>
	''' Compares two xml files. Never returns nothing.
	''' </summary>
	''' <param name="FileName1"></param>
	''' <param name="FileName2"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Shared Function Compare(ByVal FileName1 As String, ByVal FileName2 As String) As XmlCompare
		Dim result As New XmlCompare()
		Dim compareResult As Boolean
		Dim strResult As String

		Try
            'Dim xmldiff As New FileComparer ' Microsoft.XmlDiffPatch.XmlDiff
			Dim strWriter As New System.IO.StringWriter()
			Dim xmlWriter As New Xml.XmlTextWriter(strWriter)

			xmlWriter.Formatting = Xml.Formatting.Indented
            compareResult = FileComparer.Compare(FileName1, FileName2) ', False, xmlWriter)
			xmlWriter.Flush()
			xmlWriter.Close()
			strResult = strWriter.GetStringBuilder.ToString
			strWriter.Close()
		Catch ex As System.Exception
			compareResult = False
            strResult = "Error comparing xml files: '" & ex.Message & "'"
		End Try

		result.m_File1 = FileName1
		result.m_File2 = FileName2
		result.m_Equal = compareResult
		result.m_Result = strResult

		Return result
	End Function
End Class