<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRegisterProduct
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRegisterProduct))
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.lblRegistered = New System.Windows.Forms.Label
        Me.lblLogoCaption2 = New System.Windows.Forms.Label
        Me.lblLogoCaption = New System.Windows.Forms.Label
        Me.lblURL = New System.Windows.Forms.Label
        Me.lblEmail = New System.Windows.Forms.Label
        Me.lblSlogan = New System.Windows.Forms.Label
        Me.lblTitle = New System.Windows.Forms.Label
        Me.pbLogo = New System.Windows.Forms.PictureBox
        Me.lblProductKey = New System.Windows.Forms.Label
        Me.txtProductKey = New System.Windows.Forms.TextBox
        Me.btnGenerateFingerPrint = New System.Windows.Forms.Button
        Me.btnSendViaEmail = New System.Windows.Forms.Button
        Me.lblDesc = New System.Windows.Forms.Label
        Me.txtFileName = New System.Windows.Forms.TextBox
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.lblRegistrationKey = New System.Windows.Forms.Label
        Me.txtRegistrationKey = New System.Windows.Forms.TextBox
        Me.btnRegisterLater = New System.Windows.Forms.Button
        Me.btnRegister = New System.Windows.Forms.Button
        Me.openProductKey = New System.Windows.Forms.OpenFileDialog
        Me.Button1 = New System.Windows.Forms.Button
        Me.pnlTop.SuspendLayout()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.lblRegistered)
        Me.pnlTop.Controls.Add(Me.lblLogoCaption2)
        Me.pnlTop.Controls.Add(Me.lblLogoCaption)
        Me.pnlTop.Controls.Add(Me.lblURL)
        Me.pnlTop.Controls.Add(Me.lblEmail)
        Me.pnlTop.Controls.Add(Me.lblSlogan)
        Me.pnlTop.Controls.Add(Me.lblTitle)
        Me.pnlTop.Controls.Add(Me.pbLogo)
        Me.pnlTop.Location = New System.Drawing.Point(-1, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(535, 109)
        Me.pnlTop.TabIndex = 0
        '
        'lblRegistered
        '
        Me.lblRegistered.AutoSize = True
        Me.lblRegistered.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegistered.ForeColor = System.Drawing.Color.Red
        Me.lblRegistered.Location = New System.Drawing.Point(175, 76)
        Me.lblRegistered.Name = "lblRegistered"
        Me.lblRegistered.Size = New System.Drawing.Size(241, 20)
        Me.lblRegistered.TabIndex = 8
        Me.lblRegistered.Text = "UNREGISTERED PRODUCT"
        '
        'lblLogoCaption2
        '
        Me.lblLogoCaption2.AutoSize = True
        Me.lblLogoCaption2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogoCaption2.Location = New System.Drawing.Point(19, 95)
        Me.lblLogoCaption2.Name = "lblLogoCaption2"
        Me.lblLogoCaption2.Size = New System.Drawing.Size(67, 9)
        Me.lblLogoCaption2.TabIndex = 7
        Me.lblLogoCaption2.Text = "TECHNOLOGIES"
        '
        'lblLogoCaption
        '
        Me.lblLogoCaption.AutoSize = True
        Me.lblLogoCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogoCaption.Location = New System.Drawing.Point(17, 82)
        Me.lblLogoCaption.Name = "lblLogoCaption"
        Me.lblLogoCaption.Size = New System.Drawing.Size(72, 12)
        Me.lblLogoCaption.TabIndex = 6
        Me.lblLogoCaption.Text = "LUMENSOFT"
        '
        'lblURL
        '
        Me.lblURL.AutoSize = True
        Me.lblURL.Location = New System.Drawing.Point(334, 56)
        Me.lblURL.Name = "lblURL"
        Me.lblURL.Size = New System.Drawing.Size(95, 13)
        Me.lblURL.TabIndex = 5
        Me.lblURL.Text = "www.lumensoft.biz"
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(172, 56)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(96, 13)
        Me.lblEmail.TabIndex = 4
        Me.lblEmail.Text = "info@lumensoft.biz"
        '
        'lblSlogan
        '
        Me.lblSlogan.AutoSize = True
        Me.lblSlogan.Font = New System.Drawing.Font("Arial", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSlogan.Location = New System.Drawing.Point(166, 32)
        Me.lblSlogan.Name = "lblSlogan"
        Me.lblSlogan.Size = New System.Drawing.Size(261, 18)
        Me.lblSlogan.TabIndex = 3
        Me.lblSlogan.Text = "Software That Fits Your Business"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(166, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(263, 24)
        Me.lblTitle.TabIndex = 2
        Me.lblTitle.Text = "LumenSoft Technologies"
        '
        'pbLogo
        '
        Me.pbLogo.Image = CType(resources.GetObject("pbLogo.Image"), System.Drawing.Image)
        Me.pbLogo.Location = New System.Drawing.Point(12, 3)
        Me.pbLogo.Name = "pbLogo"
        Me.pbLogo.Size = New System.Drawing.Size(81, 81)
        Me.pbLogo.TabIndex = 1
        Me.pbLogo.TabStop = False
        '
        'lblProductKey
        '
        Me.lblProductKey.AutoSize = True
        Me.lblProductKey.Location = New System.Drawing.Point(9, 123)
        Me.lblProductKey.Name = "lblProductKey"
        Me.lblProductKey.Size = New System.Drawing.Size(60, 13)
        Me.lblProductKey.TabIndex = 1
        Me.lblProductKey.Text = "Finger Print"
        '
        'txtProductKey
        '
        Me.txtProductKey.Location = New System.Drawing.Point(9, 142)
        Me.txtProductKey.Multiline = True
        Me.txtProductKey.Name = "txtProductKey"
        Me.txtProductKey.ReadOnly = True
        Me.txtProductKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtProductKey.Size = New System.Drawing.Size(508, 78)
        Me.txtProductKey.TabIndex = 2
        Me.txtProductKey.Tag = "ReadOnly"
        '
        'btnGenerateFingerPrint
        '
        Me.btnGenerateFingerPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerateFingerPrint.Location = New System.Drawing.Point(285, 222)
        Me.btnGenerateFingerPrint.Name = "btnGenerateFingerPrint"
        Me.btnGenerateFingerPrint.Size = New System.Drawing.Size(137, 23)
        Me.btnGenerateFingerPrint.TabIndex = 3
        Me.btnGenerateFingerPrint.Text = "Generate Finger Print"
        Me.btnGenerateFingerPrint.UseVisualStyleBackColor = True
        '
        'btnSendViaEmail
        '
        Me.btnSendViaEmail.Location = New System.Drawing.Point(423, 222)
        Me.btnSendViaEmail.Name = "btnSendViaEmail"
        Me.btnSendViaEmail.Size = New System.Drawing.Size(94, 23)
        Me.btnSendViaEmail.TabIndex = 4
        Me.btnSendViaEmail.Text = "Send via Email..."
        Me.btnSendViaEmail.UseVisualStyleBackColor = True
        '
        'lblDesc
        '
        Me.lblDesc.AutoSize = True
        Me.lblDesc.Location = New System.Drawing.Point(9, 263)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(266, 26)
        Me.lblDesc.TabIndex = 5
        Me.lblDesc.Text = "To register the Product click Browse button and select " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "the file sent by LumenSo" & _
            "ft Technologies"
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(9, 293)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.ReadOnly = True
        Me.txtFileName.Size = New System.Drawing.Size(435, 20)
        Me.txtFileName.TabIndex = 6
        Me.txtFileName.Tag = "ReadOnly"
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(453, 292)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(64, 23)
        Me.btnBrowse.TabIndex = 7
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'lblRegistrationKey
        '
        Me.lblRegistrationKey.AutoSize = True
        Me.lblRegistrationKey.Location = New System.Drawing.Point(9, 329)
        Me.lblRegistrationKey.Name = "lblRegistrationKey"
        Me.lblRegistrationKey.Size = New System.Drawing.Size(65, 13)
        Me.lblRegistrationKey.TabIndex = 8
        Me.lblRegistrationKey.Text = "Product Key"
        '
        'txtRegistrationKey
        '
        Me.txtRegistrationKey.Location = New System.Drawing.Point(9, 345)
        Me.txtRegistrationKey.Multiline = True
        Me.txtRegistrationKey.Name = "txtRegistrationKey"
        Me.txtRegistrationKey.ReadOnly = True
        Me.txtRegistrationKey.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRegistrationKey.Size = New System.Drawing.Size(508, 78)
        Me.txtRegistrationKey.TabIndex = 9
        Me.txtRegistrationKey.Tag = "ReadOnly"
        '
        'btnRegisterLater
        '
        Me.btnRegisterLater.Location = New System.Drawing.Point(423, 429)
        Me.btnRegisterLater.Name = "btnRegisterLater"
        Me.btnRegisterLater.Size = New System.Drawing.Size(94, 23)
        Me.btnRegisterLater.TabIndex = 11
        Me.btnRegisterLater.Text = "Register Later"
        Me.btnRegisterLater.UseVisualStyleBackColor = True
        '
        'btnRegister
        '
        Me.btnRegister.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRegister.Location = New System.Drawing.Point(202, 429)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(137, 23)
        Me.btnRegister.TabIndex = 10
        Me.btnRegister.Text = "Register Product"
        Me.btnRegister.UseVisualStyleBackColor = True
        '
        'openProductKey
        '
        Me.openProductKey.FileName = "ProductKey"
        Me.openProductKey.Filter = "Text Files|*.txt"
        Me.openProductKey.Title = "Open Product Key"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(341, 429)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(81, 23)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmRegisterProduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 466)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnRegisterLater)
        Me.Controls.Add(Me.btnRegister)
        Me.Controls.Add(Me.txtRegistrationKey)
        Me.Controls.Add(Me.lblRegistrationKey)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.txtFileName)
        Me.Controls.Add(Me.lblDesc)
        Me.Controls.Add(Me.btnSendViaEmail)
        Me.Controls.Add(Me.btnGenerateFingerPrint)
        Me.Controls.Add(Me.txtProductKey)
        Me.Controls.Add(Me.lblProductKey)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "frmRegisterProduct"
        Me.Text = "Register LumenSoft Product"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblSlogan As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pbLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblLogoCaption2 As System.Windows.Forms.Label
    Friend WithEvents lblLogoCaption As System.Windows.Forms.Label
    Friend WithEvents lblURL As System.Windows.Forms.Label
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents lblRegistered As System.Windows.Forms.Label
    Friend WithEvents lblProductKey As System.Windows.Forms.Label
    Friend WithEvents txtProductKey As System.Windows.Forms.TextBox
    Friend WithEvents btnGenerateFingerPrint As System.Windows.Forms.Button
    Friend WithEvents btnSendViaEmail As System.Windows.Forms.Button
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents lblRegistrationKey As System.Windows.Forms.Label
    Friend WithEvents txtRegistrationKey As System.Windows.Forms.TextBox
    Friend WithEvents btnRegisterLater As System.Windows.Forms.Button
    Friend WithEvents btnRegister As System.Windows.Forms.Button
    Friend WithEvents openProductKey As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
