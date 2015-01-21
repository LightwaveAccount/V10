<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutBox1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutBox1))
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.TextBoxDescription = New System.Windows.Forms.TextBox
        Me.btnSystemInfo = New System.Windows.Forms.Button
        Me.lblURL = New System.Windows.Forms.LinkLabel
        Me.lblVersionType = New System.Windows.Forms.Label
        Me.lblTitle = New System.Windows.Forms.Label
        Me.lblVersion = New System.Windows.Forms.Label
        Me.lblSchemaVersion = New System.Windows.Forms.Label
        Me.lblCopyRight = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.Image = CType(resources.GetObject("LogoPictureBox.Image"), System.Drawing.Image)
        Me.LogoPictureBox.Location = New System.Drawing.Point(3, 3)
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.LogoPictureBox.Size = New System.Drawing.Size(124, 262)
        Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LogoPictureBox.TabIndex = 0
        Me.LogoPictureBox.TabStop = False
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnOK.Location = New System.Drawing.Point(322, 242)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "&OK"
        '
        'TextBoxDescription
        '
        Me.TextBoxDescription.Location = New System.Drawing.Point(140, 146)
        Me.TextBoxDescription.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
        Me.TextBoxDescription.Multiline = True
        Me.TextBoxDescription.Name = "TextBoxDescription"
        Me.TextBoxDescription.ReadOnly = True
        Me.TextBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxDescription.Size = New System.Drawing.Size(257, 92)
        Me.TextBoxDescription.TabIndex = 0
        Me.TextBoxDescription.TabStop = False
        Me.TextBoxDescription.Text = resources.GetString("TextBoxDescription.Text")
        '
        'btnSystemInfo
        '
        Me.btnSystemInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSystemInfo.Location = New System.Drawing.Point(224, 242)
        Me.btnSystemInfo.Name = "btnSystemInfo"
        Me.btnSystemInfo.Size = New System.Drawing.Size(92, 23)
        Me.btnSystemInfo.TabIndex = 2
        Me.btnSystemInfo.Text = "System Info...."
        Me.btnSystemInfo.Visible = False
        '
        'lblURL
        '
        Me.lblURL.AutoSize = True
        Me.lblURL.Location = New System.Drawing.Point(136, 126)
        Me.lblURL.Name = "lblURL"
        Me.lblURL.Size = New System.Drawing.Size(126, 13)
        Me.lblURL.TabIndex = 3
        Me.lblURL.TabStop = True
        Me.lblURL.Text = "http://www.lumensoft.biz"
        '
        'lblVersionType
        '
        Me.lblVersionType.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersionType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblVersionType.Location = New System.Drawing.Point(136, 9)
        Me.lblVersionType.Name = "lblVersionType"
        Me.lblVersionType.Size = New System.Drawing.Size(266, 20)
        Me.lblVersionType.TabIndex = 4
        Me.lblVersionType.Text = "Label1"
        Me.lblVersionType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(136, 33)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(266, 20)
        Me.lblTitle.TabIndex = 5
        Me.lblTitle.Text = "Label2"
        '
        'lblVersion
        '
        Me.lblVersion.Location = New System.Drawing.Point(136, 61)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(266, 20)
        Me.lblVersion.TabIndex = 6
        Me.lblVersion.Text = "Label3"
        '
        'lblSchemaVersion
        '
        Me.lblSchemaVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSchemaVersion.Location = New System.Drawing.Point(137, 81)
        Me.lblSchemaVersion.Name = "lblSchemaVersion"
        Me.lblSchemaVersion.Size = New System.Drawing.Size(266, 20)
        Me.lblSchemaVersion.TabIndex = 7
        Me.lblSchemaVersion.Text = "Label4"
        '
        'lblCopyRight
        '
        Me.lblCopyRight.Location = New System.Drawing.Point(137, 101)
        Me.lblCopyRight.Name = "lblCopyRight"
        Me.lblCopyRight.Size = New System.Drawing.Size(266, 20)
        Me.lblCopyRight.TabIndex = 8
        Me.lblCopyRight.Text = "Label5"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(258, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "bug fixed"
        '
        'AboutBox1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(414, 270)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblCopyRight)
        Me.Controls.Add(Me.lblSchemaVersion)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblVersionType)
        Me.Controls.Add(Me.lblURL)
        Me.Controls.Add(Me.btnSystemInfo)
        Me.Controls.Add(Me.LogoPictureBox)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.TextBoxDescription)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutBox1"
        Me.Padding = New System.Windows.Forms.Padding(9)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "AboutBox1"
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSystemInfo As System.Windows.Forms.Button
    Friend WithEvents lblURL As System.Windows.Forms.LinkLabel
    Friend WithEvents lblVersionType As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblSchemaVersion As System.Windows.Forms.Label
    Friend WithEvents lblCopyRight As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
