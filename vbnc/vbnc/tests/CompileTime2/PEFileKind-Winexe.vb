Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.IO

Public Class PEFile
    Public Structure IMAGE_DOS_HEADER
        Public e_magic As UInt16
        Public e_cblp As UInt16
        Public e_cp As UInt16
        Public e_crlc As UInt16
        Public e_cparhdr As UInt16
        Public e_minalloc As UInt16
        Public e_maxalloc As UInt16
        Public e_ss As UInt16
        Public e_sp As UInt16
        Public e_csum As UInt16
        Public e_ip As UInt16
        Public e_cs As UInt16
        Public e_lfarlc As UInt16
        Public e_ovno As UInt16
        '<MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Public e_res1 As UInt16()
        Public e_oemid As UInt16
        Public e_oeminfo As UInt16
        '<MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> _
        Public e_res2 As UInt16()
        Public e_lfanew As Int32

        Sub Read(ByVal str As BinaryReader)
            e_magic = str.ReadUInt16
            e_cblp = str.ReadUInt16
            e_cp = str.ReadUInt16
            e_crlc = str.ReadUInt16
            e_cparhdr = str.ReadUInt16
            e_minalloc = str.ReadUInt16
            e_maxalloc = str.ReadUInt16
            e_ss = str.ReadUInt16
            e_sp = str.ReadUInt16
            e_csum = str.ReadUInt16
            e_ip = str.ReadUInt16
            e_cs = str.ReadUInt16
            e_lfarlc = str.ReadUInt16
            e_ovno = str.ReadUInt16
            e_res1 = New UInt16() {str.ReadUInt16, str.ReadUInt16, str.ReadUInt16, str.ReadUInt16}
            e_oemid = str.ReadUInt16
            e_oeminfo = str.ReadUInt16
            e_res2 = New UInt16() {str.ReadUInt16, str.ReadUInt16, str.ReadUInt16, str.ReadUInt16, str.ReadUInt16, str.ReadUInt16, str.ReadUInt16, str.ReadUInt16, str.ReadUInt16, str.ReadUInt16}
            e_lfanew = str.ReadInt32
        End Sub
    End Structure

    Public Enum SubSystem As UShort
        WINDOWS_GUI = 2
        WINDOWS_CUI = 3
    End Enum

    Public Structure IMAGE_FILE_HEADER
        Public Machine As UInt16
        Public NumberOfSections As UInt16
        Public TimeDateStamp As UInt32
        Public PointerToSymbolTable As UInt32
        Public NumberOfSymbols As UInt32
        Public SizeOfOptionalHeader As UInt16
        Public Characteristics As UInt16

        Public Sub Read(ByVal str As BinaryReader)
            Machine = str.ReadUInt16
            NumberOfSections = str.ReadUInt16
            TimeDateStamp = str.ReadUInt32
            PointerToSymbolTable = str.ReadUInt32
            NumberOfSymbols = str.ReadUInt32
            SizeOfOptionalHeader = str.ReadUInt16
            Characteristics = str.ReadUInt16
        End Sub
    End Structure

    Public Structure IMAGE_OPTIONAL_HEADER32
        Public Magic As UInt16
        Public MajorLinkerVersion As Byte
        Public MinorLinkerVersion As Byte
        Public SizeOfCode As UInt32
        Public SizeOfInitializedData As UInt32
        Public SizeOfUninitializedData As UInt32
        Public AddressOfEntryPoint As UInt32
        Public BaseOfCode As UInt32
        Public BaseOfData As UInt32
        Public ImageBase As UInt32
        Public SectionAlignment As UInt32
        Public FileAlignment As UInt32
        Public MajorOperatingSystemVersion As UInt16
        Public MinorOperatingSystemVersion As UInt16
        Public MajorImageVersion As UInt16
        Public MinorImageVersion As UInt16
        Public MajorSubsystemVersion As UInt16
        Public MinorSubsystemVersion As UInt16
        Public Win32VersionValue As UInt32
        Public SizeOfImage As UInt32
        Public SizeOfHeaders As UInt32
        Public CheckSum As UInt32
        Public SubSystem As SubSystem
        Public DllCharacteristics As UInt16
        Public SizeOfStackReserve As UInt32
        Public SizeOfStackCommit As UInt32
        Public SizeOfHeapReserve As UInt32
        Public SizeOfHeapCommit As UInt32
        Public LoaderFlags As UInt32
        Public NumberOfRvaAndSizes As UInt32
        'Public DataDirectory As IMAGE_DATA_DIRECTORY ()

        Public Sub Read(ByVal str As BinaryReader)
            Magic = str.ReadUInt16
            MajorLinkerVersion = str.ReadByte
            MinorLinkerVersion = str.ReadByte
            SizeOfCode = str.ReadUInt32
            SizeOfInitializedData = str.ReadUInt32
            SizeOfUninitializedData = str.ReadUInt32
            AddressOfEntryPoint = str.ReadUInt32
            BaseOfCode = str.ReadUInt32
            BaseOfData = str.ReadUInt32
            ImageBase = str.ReadUInt32
            SectionAlignment = str.ReadUInt32
            FileAlignment = str.ReadUInt32
            MajorOperatingSystemVersion = str.ReadUInt16
            MinorOperatingSystemVersion = str.ReadUInt16
            MajorImageVersion = str.ReadUInt16
            MinorImageVersion = str.ReadUInt16
            MajorSubsystemVersion = str.ReadUInt16
            MinorSubsystemVersion = str.ReadUInt16
            Win32VersionValue = str.ReadUInt32
            SizeOfImage = str.ReadUInt32
            SizeOfHeaders = str.ReadUInt32
            CheckSum = str.ReadUInt32
            SubSystem = CType(str.ReadUInt16, SubSystem)
            DllCharacteristics = str.ReadUInt16
            SizeOfStackReserve = str.ReadUInt32
            SizeOfStackCommit = str.ReadUInt32
            SizeOfHeapReserve = str.ReadUInt32
            SizeOfHeapCommit = str.ReadUInt32
            LoaderFlags = str.ReadUInt32
            NumberOfRvaAndSizes = str.ReadUInt32
        End Sub
    End Structure

    Public Structure IMAGE_NT_HEADERS
        Public Signature As UInt32
        Public FileHeader As IMAGE_FILE_HEADER
        Public OptionalHeader As IMAGE_OPTIONAL_HEADER32

        Public Sub Read(ByVal str As BinaryReader)
            Signature = str.ReadUInt32
            If Signature <> 17744 Then Throw New Exception("Not a nt executable")
            FileHeader.Read(str)
            OptionalHeader.Read(str)
        End Sub
    End Structure

    Shared Function GetPEFileKind() As SubSystem
        Using fs As New System.IO.FileStream(System.Reflection.Assembly.GetExecutingAssembly().Location, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)
            Using reader As New System.IO.BinaryReader(fs)
                Dim dos As New IMAGE_DOS_HEADER
                Dim nt As New IMAGE_NT_HEADERS

                dos.Read(reader)

                If dos.e_magic <> &H5A4D Then Throw New Exception("Not a pe executable")

                fs.Seek(dos.e_lfanew, SeekOrigin.Begin)

                nt.Read(reader)

                Return nt.OptionalHeader.SubSystem
            End Using
        End Using
    End Function
End Class


Class PEFileKind_WinExe
    Shared Function Main() As Integer
        If PEFile.GetPEFileKind <> PEFile.SubSystem.WINDOWS_GUI Then
            Console.WriteLine("Expected subsystem to be: {0}, but it was: {1}", PEFile.SubSystem.WINDOWS_GUI, PEFile.GetPEFileKind)
            Return 1
        Else
            Return 0
        End If
    End Function

    Shared Function GetPEFileKind() As Integer

    End Function
End Class