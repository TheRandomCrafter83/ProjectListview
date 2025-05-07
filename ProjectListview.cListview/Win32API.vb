Public NotInheritable Class Win32API
    <System.Runtime.InteropServices.DllImport("uxtheme", CharSet:=System.Runtime.InteropServices.CharSet.Unicode)> _
    Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal textSubAppName As String, ByVal textSubIdList As String) As Integer
    End Function


    'API Declarations
    Public Declare Sub CoInitialize Lib "ole32.dll" (ByVal pvReserved As Int32)
    Public Declare Sub CoUninitialize Lib "ole32.dll" ()
    Public Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hwnd As Int32, ByVal wMsg As Int32, ByVal wParam As Int32, ByVal lParam As Int32) As Int32


    'Structure needed to set the listviews background watermark image
    Public Structure LVBKIMAGE
        Public ulFlags As Int32
        Public hbm As IntPtr
        Public pszImage As String
        Public cchImageMax As Int32
        Public xOffsetPercent As Int32
        Public yOffsetPercent As Int32
    End Structure

    Public Const WM_CHANGEUISTATE As Integer = &H127
    Public Const UIS_SET As Integer = 1
    Public Const UISF_HIDEFOCUS As Integer = &H1
    Public Const UISF_ACTIVE As Integer = &H4

    'Constant Declarations
    Public Const LVM_FIRST As Int32 = &H1000
    Public Const LVM_SETBKIMAGEW As Int32 = (LVM_FIRST + 138)
    Public Const LVBKIF_TYPE_WATERMARK As Int32 = &H10000000
End Class
