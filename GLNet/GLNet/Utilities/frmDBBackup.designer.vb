<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDBBackup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDBBackup))
        Me.pnlHeader = New System.Windows.Forms.Panel
        Me.lblDescription = New System.Windows.Forms.Label
        Me.lblTitle = New System.Windows.Forms.Label
        Me.pbDBBackup = New System.Windows.Forms.PictureBox
        Me.TabDBBackup = New System.Windows.Forms.TabControl
        Me.TabPgDBBackup = New System.Windows.Forms.TabPage
        Me.grpBoxBackup = New System.Windows.Forms.GroupBox
        Me.btnVerify = New System.Windows.Forms.Button
        Me.btnSchedule = New System.Windows.Forms.Button
        Me.btnBackup = New System.Windows.Forms.Button
        Me.lblMessage = New System.Windows.Forms.Label
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.txtBackupName = New System.Windows.Forms.TextBox
        Me.txtBackupLocation = New System.Windows.Forms.TextBox
        Me.txtBackupFileName = New System.Windows.Forms.TextBox
        Me.lblBackupFileName = New System.Windows.Forms.Label
        Me.lblBackupName = New System.Windows.Forms.Label
        Me.lblLocation = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnConnect = New System.Windows.Forms.Button
        Me.txtServerName = New System.Windows.Forms.TextBox
        Me.grpBoxOperations = New System.Windows.Forms.GroupBox
        Me.optRestore = New System.Windows.Forms.RadioButton
        Me.optBackup = New System.Windows.Forms.RadioButton
        Me.cboDatabase = New System.Windows.Forms.ComboBox
        Me.lblServerName = New System.Windows.Forms.Label
        Me.lblDBName = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnFirst = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnPrevious = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnNext = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnLast = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.dlgBrowse = New System.Windows.Forms.FolderBrowserDialog
        Me.pnlHeader.SuspendLayout()
        CType(Me.pbDBBackup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabDBBackup.SuspendLayout()
        Me.TabPgDBBackup.SuspendLayout()
        Me.grpBoxBackup.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpBoxOperations.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.White
        Me.pnlHeader.Controls.Add(Me.lblDescription)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.pbDBBackup)
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1028, 70)
        Me.pnlHeader.TabIndex = 0
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Location = New System.Drawing.Point(64, 26)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(707, 13)
        Me.lblDescription.TabIndex = 2
        Me.lblDescription.Text = "Use this utility to manually perform backup or restore of your database. Select y" & _
            "our database type and fill in the required fields to perform your backup."
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblTitle.Location = New System.Drawing.Point(64, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(134, 16)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "Backup Database"
        '
        'pbDBBackup
        '
        Me.pbDBBackup.Image = CType(resources.GetObject("pbDBBackup.Image"), System.Drawing.Image)
        Me.pbDBBackup.Location = New System.Drawing.Point(12, 12)
        Me.pbDBBackup.Name = "pbDBBackup"
        Me.pbDBBackup.Size = New System.Drawing.Size(49, 50)
        Me.pbDBBackup.TabIndex = 0
        Me.pbDBBackup.TabStop = False
        '
        'TabDBBackup
        '
        Me.TabDBBackup.Controls.Add(Me.TabPgDBBackup)
        Me.TabDBBackup.Location = New System.Drawing.Point(0, 70)
        Me.TabDBBackup.Margin = New System.Windows.Forms.Padding(0)
        Me.TabDBBackup.Name = "TabDBBackup"
        Me.TabDBBackup.Padding = New System.Drawing.Point(0, 0)
        Me.TabDBBackup.SelectedIndex = 0
        Me.TabDBBackup.Size = New System.Drawing.Size(1028, 387)
        Me.TabDBBackup.TabIndex = 6
        '
        'TabPgDBBackup
        '
        Me.TabPgDBBackup.Controls.Add(Me.grpBoxBackup)
        Me.TabPgDBBackup.Controls.Add(Me.GroupBox1)
        Me.TabPgDBBackup.Location = New System.Drawing.Point(4, 22)
        Me.TabPgDBBackup.Margin = New System.Windows.Forms.Padding(0)
        Me.TabPgDBBackup.Name = "TabPgDBBackup"
        Me.TabPgDBBackup.Size = New System.Drawing.Size(1020, 361)
        Me.TabPgDBBackup.TabIndex = 0
        Me.TabPgDBBackup.Text = "Microsoft® SQL Server™"
        Me.TabPgDBBackup.UseVisualStyleBackColor = True
        '
        'grpBoxBackup
        '
        Me.grpBoxBackup.Controls.Add(Me.btnVerify)
        Me.grpBoxBackup.Controls.Add(Me.btnSchedule)
        Me.grpBoxBackup.Controls.Add(Me.btnBackup)
        Me.grpBoxBackup.Controls.Add(Me.lblMessage)
        Me.grpBoxBackup.Controls.Add(Me.btnBrowse)
        Me.grpBoxBackup.Controls.Add(Me.txtBackupName)
        Me.grpBoxBackup.Controls.Add(Me.txtBackupLocation)
        Me.grpBoxBackup.Controls.Add(Me.txtBackupFileName)
        Me.grpBoxBackup.Controls.Add(Me.lblBackupFileName)
        Me.grpBoxBackup.Controls.Add(Me.lblBackupName)
        Me.grpBoxBackup.Controls.Add(Me.lblLocation)
        Me.grpBoxBackup.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.grpBoxBackup.Location = New System.Drawing.Point(9, 140)
        Me.grpBoxBackup.Name = "grpBoxBackup"
        Me.grpBoxBackup.Size = New System.Drawing.Size(996, 216)
        Me.grpBoxBackup.TabIndex = 9
        Me.grpBoxBackup.TabStop = False
        Me.grpBoxBackup.Text = "Complete Backup of Database"
        '
        'btnVerify
        '
        Me.btnVerify.Image = CType(resources.GetObject("btnVerify.Image"), System.Drawing.Image)
        Me.btnVerify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnVerify.Location = New System.Drawing.Point(587, 177)
        Me.btnVerify.Name = "btnVerify"
        Me.btnVerify.Size = New System.Drawing.Size(116, 31)
        Me.btnVerify.TabIndex = 14
        Me.btnVerify.Text = "Verify"
        Me.btnVerify.UseVisualStyleBackColor = True
        Me.btnVerify.Visible = False
        '
        'btnSchedule
        '
        Me.btnSchedule.Image = CType(resources.GetObject("btnSchedule.Image"), System.Drawing.Image)
        Me.btnSchedule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSchedule.Location = New System.Drawing.Point(465, 177)
        Me.btnSchedule.Name = "btnSchedule"
        Me.btnSchedule.Size = New System.Drawing.Size(116, 31)
        Me.btnSchedule.TabIndex = 13
        Me.btnSchedule.Text = "Schedule"
        Me.btnSchedule.UseVisualStyleBackColor = True
        Me.btnSchedule.Visible = False
        '
        'btnBackup
        '
        Me.btnBackup.Image = CType(resources.GetObject("btnBackup.Image"), System.Drawing.Image)
        Me.btnBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBackup.Location = New System.Drawing.Point(339, 177)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(116, 31)
        Me.btnBackup.TabIndex = 12
        Me.btnBackup.Text = "Backup Now!"
        Me.btnBackup.UseVisualStyleBackColor = True
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.Location = New System.Drawing.Point(10, 127)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(769, 32)
        Me.lblMessage.TabIndex = 11
        Me.lblMessage.Text = resources.GetString("lblMessage.Text")
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(588, 55)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(34, 31)
        Me.btnBrowse.TabIndex = 10
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'txtBackupName
        '
        Me.txtBackupName.Location = New System.Drawing.Point(114, 26)
        Me.txtBackupName.MaxLength = 50
        Me.txtBackupName.Name = "txtBackupName"
        Me.txtBackupName.Size = New System.Drawing.Size(467, 20)
        Me.txtBackupName.TabIndex = 3
        Me.txtBackupName.Tag = "IsRequired"
        '
        'txtBackupLocation
        '
        Me.txtBackupLocation.Location = New System.Drawing.Point(114, 60)
        Me.txtBackupLocation.MaxLength = 500
        Me.txtBackupLocation.Name = "txtBackupLocation"
        Me.txtBackupLocation.Size = New System.Drawing.Size(467, 20)
        Me.txtBackupLocation.TabIndex = 4
        '
        'txtBackupFileName
        '
        Me.txtBackupFileName.Location = New System.Drawing.Point(114, 93)
        Me.txtBackupFileName.MaxLength = 250
        Me.txtBackupFileName.Name = "txtBackupFileName"
        Me.txtBackupFileName.Size = New System.Drawing.Size(467, 20)
        Me.txtBackupFileName.TabIndex = 5
        '
        'lblBackupFileName
        '
        Me.lblBackupFileName.Location = New System.Drawing.Point(8, 96)
        Me.lblBackupFileName.Name = "lblBackupFileName"
        Me.lblBackupFileName.Size = New System.Drawing.Size(100, 15)
        Me.lblBackupFileName.TabIndex = 8
        Me.lblBackupFileName.Text = "Backup File Name"
        '
        'lblBackupName
        '
        Me.lblBackupName.Location = New System.Drawing.Point(8, 29)
        Me.lblBackupName.Name = "lblBackupName"
        Me.lblBackupName.Size = New System.Drawing.Size(100, 15)
        Me.lblBackupName.TabIndex = 6
        Me.lblBackupName.Text = "Backup Name"
        '
        'lblLocation
        '
        Me.lblLocation.Location = New System.Drawing.Point(8, 63)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(100, 15)
        Me.lblLocation.TabIndex = 7
        Me.lblLocation.Text = "Location"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnConnect)
        Me.GroupBox1.Controls.Add(Me.txtServerName)
        Me.GroupBox1.Controls.Add(Me.grpBoxOperations)
        Me.GroupBox1.Controls.Add(Me.cboDatabase)
        Me.GroupBox1.Controls.Add(Me.lblServerName)
        Me.GroupBox1.Controls.Add(Me.lblDBName)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox1.Location = New System.Drawing.Point(9, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(996, 128)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(253, 17)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 23)
        Me.btnConnect.TabIndex = 13
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'txtServerName
        '
        Me.txtServerName.Location = New System.Drawing.Point(114, 18)
        Me.txtServerName.MaxLength = 50
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(132, 20)
        Me.txtServerName.TabIndex = 12
        Me.txtServerName.Tag = "IsRequired"
        '
        'grpBoxOperations
        '
        Me.grpBoxOperations.Controls.Add(Me.optRestore)
        Me.grpBoxOperations.Controls.Add(Me.optBackup)
        Me.grpBoxOperations.Location = New System.Drawing.Point(11, 74)
        Me.grpBoxOperations.Name = "grpBoxOperations"
        Me.grpBoxOperations.Size = New System.Drawing.Size(315, 42)
        Me.grpBoxOperations.TabIndex = 11
        Me.grpBoxOperations.TabStop = False
        Me.grpBoxOperations.Text = "Operations"
        Me.grpBoxOperations.Visible = False
        '
        'optRestore
        '
        Me.optRestore.AutoSize = True
        Me.optRestore.Location = New System.Drawing.Point(152, 17)
        Me.optRestore.Name = "optRestore"
        Me.optRestore.Size = New System.Drawing.Size(62, 17)
        Me.optRestore.TabIndex = 1
        Me.optRestore.Text = "Restore"
        Me.optRestore.UseVisualStyleBackColor = True
        '
        'optBackup
        '
        Me.optBackup.AutoSize = True
        Me.optBackup.Checked = True
        Me.optBackup.Location = New System.Drawing.Point(41, 17)
        Me.optBackup.Name = "optBackup"
        Me.optBackup.Size = New System.Drawing.Size(62, 17)
        Me.optBackup.TabIndex = 0
        Me.optBackup.TabStop = True
        Me.optBackup.Text = "Backup"
        Me.optBackup.UseVisualStyleBackColor = True
        '
        'cboDatabase
        '
        Me.cboDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDatabase.FormattingEnabled = True
        Me.cboDatabase.Location = New System.Drawing.Point(114, 47)
        Me.cboDatabase.Name = "cboDatabase"
        Me.cboDatabase.Size = New System.Drawing.Size(212, 21)
        Me.cboDatabase.TabIndex = 10
        '
        'lblServerName
        '
        Me.lblServerName.Location = New System.Drawing.Point(8, 21)
        Me.lblServerName.Name = "lblServerName"
        Me.lblServerName.Size = New System.Drawing.Size(100, 15)
        Me.lblServerName.TabIndex = 9
        Me.lblServerName.Text = "Server"
        '
        'lblDBName
        '
        Me.lblDBName.Location = New System.Drawing.Point(8, 50)
        Me.lblDBName.Name = "lblDBName"
        Me.lblDBName.Size = New System.Drawing.Size(100, 15)
        Me.lblDBName.TabIndex = 0
        Me.lblDBName.Text = "Database"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.btnFirst)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.btnPrevious)
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Controls.Add(Me.btnNext)
        Me.Panel1.Controls.Add(Me.btnUpdate)
        Me.Panel1.Controls.Add(Me.btnLast)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnNew)
        Me.Panel1.Location = New System.Drawing.Point(0, 556)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1030, 50)
        Me.Panel1.TabIndex = 7
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(965, 0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 48)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Tag = "HideText"
        Me.btnExit.Text = " "
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnFirst
        '
        Me.btnFirst.Location = New System.Drawing.Point(12, 0)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(48, 48)
        Me.btnFirst.TabIndex = 0
        Me.btnFirst.Tag = "HideText"
        Me.btnFirst.Text = " "
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(565, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(48, 48)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Tag = "HideText"
        Me.btnCancel.Text = " "
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.Location = New System.Drawing.Point(66, 0)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(48, 48)
        Me.btnPrevious.TabIndex = 1
        Me.btnPrevious.Tag = "HideText"
        Me.btnPrevious.Text = " "
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(511, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(48, 48)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Tag = "HideText"
        Me.btnDelete.Text = " "
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(120, 0)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(48, 48)
        Me.btnNext.TabIndex = 2
        Me.btnNext.Tag = "HideText"
        Me.btnNext.Text = " "
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(457, 0)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(48, 48)
        Me.btnUpdate.TabIndex = 6
        Me.btnUpdate.Tag = "HideText"
        Me.btnUpdate.Text = " "
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.Location = New System.Drawing.Point(174, 0)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(48, 48)
        Me.btnLast.TabIndex = 3
        Me.btnLast.Tag = "HideText"
        Me.btnLast.Text = " "
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(403, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(48, 48)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Tag = "HideText"
        Me.btnSave.Text = " "
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(349, 0)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(48, 48)
        Me.btnNew.TabIndex = 4
        Me.btnNew.Tag = "HideText"
        Me.btnNew.Text = " "
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'frmDBBackup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 742)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabDBBackup)
        Me.Controls.Add(Me.pnlHeader)
        Me.KeyPreview = True
        Me.Name = "frmDBBackup"
        Me.Text = "Backup Database"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.pbDBBackup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabDBBackup.ResumeLayout(False)
        Me.TabPgDBBackup.ResumeLayout(False)
        Me.grpBoxBackup.ResumeLayout(False)
        Me.grpBoxBackup.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpBoxOperations.ResumeLayout(False)
        Me.grpBoxOperations.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pbDBBackup As System.Windows.Forms.PictureBox
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents TabDBBackup As System.Windows.Forms.TabControl
    Friend WithEvents TabPgDBBackup As System.Windows.Forms.TabPage
    Friend WithEvents grpBoxBackup As System.Windows.Forms.GroupBox
    Friend WithEvents txtBackupName As System.Windows.Forms.TextBox
    Friend WithEvents txtBackupLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtBackupFileName As System.Windows.Forms.TextBox
    Friend WithEvents lblBackupFileName As System.Windows.Forms.Label
    Friend WithEvents lblBackupName As System.Windows.Forms.Label
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpBoxOperations As System.Windows.Forms.GroupBox
    Friend WithEvents optRestore As System.Windows.Forms.RadioButton
    Friend WithEvents optBackup As System.Windows.Forms.RadioButton
    Friend WithEvents cboDatabase As System.Windows.Forms.ComboBox
    Friend WithEvents lblServerName As System.Windows.Forms.Label
    Friend WithEvents lblDBName As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnVerify As System.Windows.Forms.Button
    Friend WithEvents btnSchedule As System.Windows.Forms.Button
    Friend WithEvents btnBackup As System.Windows.Forms.Button
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents txtServerName As System.Windows.Forms.TextBox
    Friend WithEvents dlgBrowse As System.Windows.Forms.FolderBrowserDialog
End Class
