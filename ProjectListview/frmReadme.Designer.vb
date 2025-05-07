<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReadme
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.rtfReadMe = New System.Windows.Forms.RichTextBox()
		Me.wb = New System.Windows.Forms.WebBrowser()
		Me.SuspendLayout()
		'
		'rtfReadMe
		'
		Me.rtfReadMe.Dock = System.Windows.Forms.DockStyle.Fill
		Me.rtfReadMe.Location = New System.Drawing.Point(0, 0)
		Me.rtfReadMe.Name = "rtfReadMe"
		Me.rtfReadMe.Size = New System.Drawing.Size(854, 525)
		Me.rtfReadMe.TabIndex = 0
		Me.rtfReadMe.Text = ""
		Me.rtfReadMe.Visible = False
		'
		'wb
		'
		Me.wb.Dock = System.Windows.Forms.DockStyle.Fill
		Me.wb.Location = New System.Drawing.Point(0, 0)
		Me.wb.MinimumSize = New System.Drawing.Size(20, 20)
		Me.wb.Name = "wb"
		Me.wb.Size = New System.Drawing.Size(854, 525)
		Me.wb.TabIndex = 1
		'
		'frmReadme
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(854, 525)
		Me.Controls.Add(Me.wb)
		Me.Controls.Add(Me.rtfReadMe)
		Me.Name = "frmReadme"
		Me.Text = "Readme"
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents rtfReadMe As System.Windows.Forms.RichTextBox
	Friend WithEvents wb As System.Windows.Forms.WebBrowser
End Class
