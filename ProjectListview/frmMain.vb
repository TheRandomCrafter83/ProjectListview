Public Class frmMain

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        frmTestWindow.ShowDialog()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 1 Then
            frmTestWindow.Visible = Not frmTestWindow.Visible
        End If
    End Sub

    Private Sub DataGridView1_CellValuePushed(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles DataGridView1.CellValuePushed

    End Sub

    Private Sub frmMain_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        frmTestWindow.Show()
    End Sub
End Class