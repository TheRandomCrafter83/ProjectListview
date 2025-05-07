Public Class frmReadme

	Private Sub frmReadme_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Dim sPath As String = Application.ExecutablePath()

		wb.Navigate(FixPath(StripPath(sPath)) & "ReadMe.htm")
	End Sub
	Private Function StripPath(ByVal sPath As String) As String
		Dim ret As String = sPath
		Dim nPos As Integer = Strings.InStrRev(sPath, "\")
		ret = Strings.Left(sPath, nPos)
		Return ret
	End Function
	Private Function FixPath(ByVal sPath As String) As String
		Dim ret As String = sPath
		If Strings.Right(ret, 1) <> "\" Then
			ret = sPath & "\"
		End If
		Return ret
	End Function
End Class