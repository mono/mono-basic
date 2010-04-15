Imports System.Runtime.InteropServices

Namespace MarshalAs
    <StructLayout(LayoutKind.Explicit)> _
    Structure E
        '<MarshalAs(UnmanagedType.I4)> _
        <FieldOffset(0)> _
        Dim i As Integer
        '<MarshalAs(UnmanagedType.I4)> _
        <FieldOffset(4)> _
        Dim h As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Structure S
        Dim i As Boolean
    End Structure
    <StructLayout(LayoutKind.Auto)> _
    Structure A
        Dim i As Boolean
    End Structure
End Namespace