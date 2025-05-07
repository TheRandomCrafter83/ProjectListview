Public Class frmTestWindow

    Private Sub frmTestWindow_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        chkVista.Checked = ListView1.UseVistaThemeing
        cboView.Text = "Details"
        'Put some data into the listview, so we can test the scrolling with the watermark
        Dim i As Int32
        For i = 0 To 300
            Dim it As ListViewItem = ListView1.Items.Add("Item" & i)
            'it.BackColor = Color.FromArgb(255, Rnd() * 255, Rnd() * 255, Rnd() * 255)
            it.SubItems.Add("Hello SubItem + " & i.ToString)
            'If i Mod 2 = 0 Then
            'it.BackColor = Color.LightGray
            'End If
        Next
        chkLedger.Checked = ListView1.UseLedgerStyle
        picDark.BackColor = ListView1.LedgerStyleDarkColor
        picLight.BackColor = ListView1.LedgerStyleLightColor
        'ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub

    Private Function FixPath(ByVal spath As String)
        If Strings.Right(spath, 1) <> "\" Then
            Return spath & "\"
        Else
            Return spath
        End If
    End Function

    Public Sub ColorStripeListViewRowsBackColor(ByRef lvwView As ListView, ByVal LightColor As Color, ByVal DarkColor As Color)
        Dim Item As ListViewItem
        With lvwView
            .BeginUpdate()
            For Each Item In .Items
                If Item.Index Mod 2 Then
                    Item.BackColor = LightColor ' Even row
                Else
                    Item.BackColor = DarkColor ' Odd row
                End If
            Next
            .EndUpdate()
        End With
    End Sub

    Private Sub cboView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboView.SelectedIndexChanged
        Select Case cboView.Text
            Case "Details"
                ListView1.View = View.Details
            Case "LargeIcon"
                ListView1.View = View.LargeIcon
            Case "List"
                ListView1.View = View.List
            Case "SmallIcon"
                ListView1.View = View.SmallIcon
            Case "Tile"
                ListView1.View = View.Tile
        End Select
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim fd As New OpenFileDialog
        fd.Filter = "Image Files|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png|All Files|*.*"
        If fd.ShowDialog = Windows.Forms.DialogResult.OK Then
            ListView1.WatermarkImage = Image.FromFile(fd.FileName)
        End If
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        ListView1.WatermarkAlpha = TrackBar1.Value
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        frmReadme.ShowDialog()
    End Sub

    Private Sub chkVista_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVista.CheckedChanged
        ListView1.UseVistaThemeing = chkVista.Checked
    End Sub


    Private Sub chkLedger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLedger.CheckedChanged
        ListView1.UseLedgerStyle = chkLedger.Checked
    End Sub

    Private Sub picDark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picDark.Click, picLight.Click

        Dim cd As New ColorDialog
        Select Case LCase(sender.name)
            Case "picdark"
                cd.Color = picDark.BackColor
                If cd.ShowDialog = Windows.Forms.DialogResult.OK Then
                    picDark.BackColor = cd.Color
                    ListView1.LedgerStyleDarkColor = cd.Color
                End If
            Case "piclight"
                cd.Color = picLight.BackColor
                If cd.ShowDialog = Windows.Forms.DialogResult.OK Then
                    picLight.BackColor = cd.Color
                    ListView1.LedgerStyleLightColor = cd.Color
                End If
        End Select
        cd.Dispose()
    End Sub


    Private Sub ListView1_SubItemClick(ByVal sender As Object, ByVal e As SubItemClickEventArgs) Handles ListView1.SubItemClick
        'MsgBox(e.SubItem.Text)
    End Sub

    Private Sub ListView1_SubItemDoubleClick(ByVal sender As Object, ByVal e As SubItemClickEventArgs) Handles ListView1.SubItemDoubleClick
        MsgBox(e.SubItem.Text)
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        ListView1.WatermarkImage = Nothing
    End Sub
End Class
