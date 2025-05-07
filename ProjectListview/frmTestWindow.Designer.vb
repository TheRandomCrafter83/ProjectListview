<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTestWindow
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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("test1")
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("test2")
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("test3")
        Me.cboView = New System.Windows.Forms.ComboBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.chkVista = New System.Windows.Forms.CheckBox()
        Me.chkLedger = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.picDark = New System.Windows.Forms.PictureBox()
        Me.picLight = New System.Windows.Forms.PictureBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ListView1 = New ProjectListview.cListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picDark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboView
        '
        Me.cboView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboView.FormattingEnabled = True
        Me.cboView.Items.AddRange(New Object() {"Details", "LargeIcon", "List", "SmallIcon", "Tile"})
        Me.cboView.Location = New System.Drawing.Point(8, 8)
        Me.cboView.Name = "cboView"
        Me.cboView.Size = New System.Drawing.Size(121, 21)
        Me.cboView.TabIndex = 1
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Black
        Me.PictureBox1.Location = New System.Drawing.Point(136, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(1, 24)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(144, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Load Image"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TrackBar1
        '
        Me.TrackBar1.AutoSize = False
        Me.TrackBar1.Location = New System.Drawing.Point(336, 8)
        Me.TrackBar1.Maximum = 255
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(176, 24)
        Me.TrackBar1.TabIndex = 5
        Me.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.None
        Me.TrackBar1.Value = 200
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(232, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Image Transparency"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Black
        Me.PictureBox2.Location = New System.Drawing.Point(512, 8)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(1, 24)
        Me.PictureBox2.TabIndex = 7
        Me.PictureBox2.TabStop = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(520, 8)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Readme"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'chkVista
        '
        Me.chkVista.AutoSize = True
        Me.chkVista.Location = New System.Drawing.Point(608, 16)
        Me.chkVista.Name = "chkVista"
        Me.chkVista.Size = New System.Drawing.Size(121, 17)
        Me.chkVista.TabIndex = 9
        Me.chkVista.Text = "Use Vista Themeing"
        Me.chkVista.UseVisualStyleBackColor = True
        '
        'chkLedger
        '
        Me.chkLedger.AutoSize = True
        Me.chkLedger.Location = New System.Drawing.Point(776, 8)
        Me.chkLedger.Name = "chkLedger"
        Me.chkLedger.Size = New System.Drawing.Size(107, 17)
        Me.chkLedger.TabIndex = 10
        Me.chkLedger.Text = "Use Ledger Style"
        Me.chkLedger.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(800, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "LedgerStyleDarkColor"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(944, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "LedgerStyleLightColor"
        '
        'picDark
        '
        Me.picDark.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picDark.Location = New System.Drawing.Point(776, 32)
        Me.picDark.Name = "picDark"
        Me.picDark.Size = New System.Drawing.Size(24, 24)
        Me.picDark.TabIndex = 13
        Me.picDark.TabStop = False
        '
        'picLight
        '
        Me.picLight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picLight.Location = New System.Drawing.Point(920, 32)
        Me.picLight.Name = "picLight"
        Me.picLight.Size = New System.Drawing.Size(24, 24)
        Me.picLight.TabIndex = 14
        Me.picLight.TabStop = False
        '
        'PictureBox5
        '
        Me.PictureBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox5.Location = New System.Drawing.Point(775, 31)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(137, 26)
        Me.PictureBox5.TabIndex = 15
        Me.PictureBox5.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox6.Location = New System.Drawing.Point(919, 31)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(137, 26)
        Me.PictureBox6.TabIndex = 16
        Me.PictureBox6.TabStop = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(144, 32)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(80, 23)
        Me.Button3.TabIndex = 17
        Me.Button3.Text = "Clear Image"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView1.ColumnSortIndex = 0
        Me.ListView1.ColumnSorting = True
        Me.ListView1.ColumnSortOrder = System.Windows.Forms.SortOrder.Ascending
        Me.ListView1.ColumnSortStyle = ProjectListview.SvenSo.ListViewColumnSorter.SortModifiers.SortByImage
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3})
        Me.ListView1.LedgerStyleDarkColor = System.Drawing.Color.Silver
        Me.ListView1.LedgerStyleLightColor = System.Drawing.Color.White
        Me.ListView1.Location = New System.Drawing.Point(8, 64)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.OwnerDraw = True
        Me.ListView1.ShowFocusRectangle = False
        Me.ListView1.Size = New System.Drawing.Size(1084, 494)
        Me.ListView1.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListView1.TabIndex = 2
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.UseLedgerStyle = True
        Me.ListView1.UseVistaThemeing = True
        Me.ListView1.View = System.Windows.Forms.View.Details
        Me.ListView1.WatermarkAlpha = 200
        Me.ListView1.WatermarkImage = Global.ProjectListview.My.Resources.Resources.Avatar
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Width = 187
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Width = 168
        '
        'frmTestWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1104, 570)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.picLight)
        Me.Controls.Add(Me.picDark)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.chkLedger)
        Me.Controls.Add(Me.chkVista)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.cboView)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.PictureBox6)
        Me.Name = "frmTestWindow"
        Me.Text = "Testing Environment for ListView Watermark"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picDark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboView As System.Windows.Forms.ComboBox
    Friend WithEvents ListView1 As ProjectListview.cListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents chkVista As System.Windows.Forms.CheckBox
    Friend WithEvents chkLedger As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents picDark As System.Windows.Forms.PictureBox
    Friend WithEvents picLight As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents Button3 As System.Windows.Forms.Button

End Class
