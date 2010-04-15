Imports System
Imports System.Diagnostics
Imports System.Collections
Imports System.Reflection
Imports System.Runtime.InteropServices

'<Assembly: AssemblyVersion("1.2.3.4")> 

Namespace FileVersionInfo1
    Class Test
        Shared Function Main() As Integer
            Dim result As Boolean
            Dim v As FileVersionInfo

            v = FileVersionInfo.GetVersionInfo(Reflection.Assembly.GetExecutingAssembly.Location)

            result = Assert("Comments", "", v.Comments) AndAlso result
            result = Assert("CompanyName", "", v.CompanyName) AndAlso result
            result = Assert("FileBuildPart", 0, v.FileBuildPart) AndAlso result
            result = Assert("FileDescription", "", v.FileDescription) AndAlso result
            result = Assert("FileMajorPart", 0, v.FileMajorPart) AndAlso result
            result = Assert("FileMinorPart", 0, v.FileMinorPart) AndAlso result
            result = Assert("FileName", "", v.FileName) AndAlso result
            result = Assert("FilePrivatePart", 0, v.FilePrivatePart) AndAlso result
            result = Assert("FileVersion", "", v.FileVersion) AndAlso result
            result = Assert("InternalName", "", v.InternalName) AndAlso result
            result = Assert("IsDebug", False, v.IsDebug) AndAlso result
            result = Assert("IsPatched", False, v.IsPatched) AndAlso result
            result = Assert("IsPreRelease", False, v.IsPreRelease) AndAlso result
            result = Assert("IsPrivateBuild", False, v.IsPrivateBuild) AndAlso result
            result = Assert("IsSpecialBuild", False, v.IsSpecialBuild) AndAlso result
            result = Assert("Language", "", v.Language) AndAlso result
            result = Assert("LegalCopyright", "", v.LegalCopyright) AndAlso result
            result = Assert("LegalTrademarks", "", v.LegalTrademarks) AndAlso result
            result = Assert("OriginalFilename", "", v.OriginalFilename) AndAlso result
            result = Assert("PrivateBuild", "", v.PrivateBuild) AndAlso result
            result = Assert("ProductBuildPart", 0, v.ProductBuildPart) AndAlso result
            result = Assert("ProductMajorPart", 0, v.ProductMajorPart) AndAlso result
            result = Assert("ProductMinorPart", 0, v.ProductMinorPart) AndAlso result
            result = Assert("ProductName", "", v.ProductName) AndAlso result
            result = Assert("ProductPrivatePart", 0, v.ProductPrivatePart) AndAlso result
            result = Assert("ProductVersion", "", v.ProductVersion) AndAlso result
            result = Assert("SpecialBuild", "", v.SpecialBuild) AndAlso result

            If result Then
                Return 0
            Else
                Return 1
            End If
        End Function

        Shared Function Assert(ByVal field As String, ByVal expected As String, ByVal actual As String) As Boolean

        End Function
        Shared Function Assert(ByVal field As String, ByVal expected As Boolean, ByVal actual As Boolean) As Boolean

        End Function
        Shared Function Assert(ByVal field As String, ByVal expected As Integer, ByVal actual As Integer) As Boolean

        End Function
    End Class
End Namespace