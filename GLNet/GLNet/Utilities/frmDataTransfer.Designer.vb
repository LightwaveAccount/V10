<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDataTransfer
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
        Me.lblTitle = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnTransfer = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkSecurity = New System.Windows.Forms.CheckBox
        Me.chkCOA = New System.Windows.Forms.CheckBox
        Me.chkDefinition = New System.Windows.Forms.CheckBox
        Me.chkDCS = New System.Windows.Forms.CheckBox
        Me.chkVoucher = New System.Windows.Forms.CheckBox
        Me.lblDatabase = New System.Windows.Forms.Label
        Me.rdoCmplt = New System.Windows.Forms.RadioButton
        Me.cboDatabases = New System.Windows.Forms.ComboBox
        Me.rdoPartial = New System.Windows.Forms.RadioButton
        Me.Bar = New System.Windows.Forms.ProgressBar
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
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.grpTransaction = New System.Windows.Forms.GroupBox
        Me.Panel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.grpTransaction.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AccessibleDescription = "Title"
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(166, 24)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "Transfer GL Data"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Location = New System.Drawing.Point(12, 36)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1001, 570)
        Me.Panel2.TabIndex = 16
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox4)
        Me.GroupBox2.Controls.Add(Me.grpTransaction)
        Me.GroupBox2.Controls.Add(Me.btnTransfer)
        Me.GroupBox2.Controls.Add(Me.GroupBox1)
        Me.GroupBox2.Controls.Add(Me.lblDatabase)
        Me.GroupBox2.Controls.Add(Me.cboDatabases)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(993, 562)
        Me.GroupBox2.TabIndex = 22
        Me.GroupBox2.TabStop = False
        '
        'btnTransfer
        '
        Me.btnTransfer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransfer.Location = New System.Drawing.Point(844, 513)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(137, 35)
        Me.btnTransfer.TabIndex = 8
        Me.btnTransfer.Tag = "HideText"
        Me.btnTransfer.Text = "Export Data"
        Me.btnTransfer.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkSecurity)
        Me.GroupBox1.Controls.Add(Me.chkDCS)
        Me.GroupBox1.Controls.Add(Me.chkCOA)
        Me.GroupBox1.Controls.Add(Me.chkDefinition)
        Me.GroupBox1.Location = New System.Drawing.Point(134, 110)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(228, 105)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "        Configurations"
        '
        'chkSecurity
        '
        Me.chkSecurity.AutoSize = True
        Me.chkSecurity.Location = New System.Drawing.Point(28, 80)
        Me.chkSecurity.Name = "chkSecurity"
        Me.chkSecurity.Size = New System.Drawing.Size(64, 17)
        Me.chkSecurity.TabIndex = 9
        Me.chkSecurity.Text = "Security"
        Me.chkSecurity.UseVisualStyleBackColor = True
        '
        'chkCOA
        '
        Me.chkCOA.AutoSize = True
        Me.chkCOA.Location = New System.Drawing.Point(28, 57)
        Me.chkCOA.Name = "chkCOA"
        Me.chkCOA.Size = New System.Drawing.Size(48, 17)
        Me.chkCOA.TabIndex = 8
        Me.chkCOA.Text = "COA"
        Me.chkCOA.UseVisualStyleBackColor = True
        '
        'chkDefinition
        '
        Me.chkDefinition.AutoSize = True
        Me.chkDefinition.Location = New System.Drawing.Point(28, 33)
        Me.chkDefinition.Name = "chkDefinition"
        Me.chkDefinition.Size = New System.Drawing.Size(147, 17)
        Me.chkDefinition.TabIndex = 7
        Me.chkDefinition.Text = "Definitions/Configurations"
        Me.chkDefinition.UseVisualStyleBackColor = True
        '
        'chkDCS
        '
        Me.chkDCS.AutoSize = True
        Me.chkDCS.Location = New System.Drawing.Point(13, 0)
        Me.chkDCS.Name = "chkDCS"
        Me.chkDCS.Size = New System.Drawing.Size(15, 14)
        Me.chkDCS.TabIndex = 23
        Me.chkDCS.UseVisualStyleBackColor = True
        '
        'chkVoucher
        '
        Me.chkVoucher.AutoSize = True
        Me.chkVoucher.Location = New System.Drawing.Point(28, 22)
        Me.chkVoucher.Name = "chkVoucher"
        Me.chkVoucher.Size = New System.Drawing.Size(66, 17)
        Me.chkVoucher.TabIndex = 7
        Me.chkVoucher.Text = "Voucher"
        Me.chkVoucher.UseVisualStyleBackColor = True
        '
        'lblDatabase
        '
        Me.lblDatabase.AutoSize = True
        Me.lblDatabase.Location = New System.Drawing.Point(13, 23)
        Me.lblDatabase.Name = "lblDatabase"
        Me.lblDatabase.Size = New System.Drawing.Size(115, 13)
        Me.lblDatabase.TabIndex = 9
        Me.lblDatabase.Text = "Destination Database :"
        '
        'rdoCmplt
        '
        Me.rdoCmplt.AutoSize = True
        Me.rdoCmplt.Location = New System.Drawing.Point(6, 19)
        Me.rdoCmplt.Name = "rdoCmplt"
        Me.rdoCmplt.Size = New System.Drawing.Size(69, 17)
        Me.rdoCmplt.TabIndex = 2
        Me.rdoCmplt.TabStop = True
        Me.rdoCmplt.Text = "Complete"
        Me.rdoCmplt.UseVisualStyleBackColor = True
        '
        'cboDatabases
        '
        Me.cboDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDatabases.FormattingEnabled = True
        Me.cboDatabases.Location = New System.Drawing.Point(134, 19)
        Me.cboDatabases.Name = "cboDatabases"
        Me.cboDatabases.Size = New System.Drawing.Size(228, 21)
        Me.cboDatabases.TabIndex = 1
        '
        'rdoPartial
        '
        Me.rdoPartial.AutoSize = True
        Me.rdoPartial.Location = New System.Drawing.Point(153, 19)
        Me.rdoPartial.Name = "rdoPartial"
        Me.rdoPartial.Size = New System.Drawing.Size(54, 17)
        Me.rdoPartial.TabIndex = 3
        Me.rdoPartial.TabStop = True
        Me.rdoPartial.Text = "Partial"
        Me.rdoPartial.UseVisualStyleBackColor = True
        '
        'Bar
        '
        Me.Bar.Location = New System.Drawing.Point(9, 144)
        Me.Bar.Name = "Bar"
        Me.Bar.Size = New System.Drawing.Size(407, 23)
        Me.Bar.TabIndex = 13
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
        Me.Panel1.Location = New System.Drawing.Point(0, 612)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1030, 50)
        Me.Panel1.TabIndex = 17
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
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rdoCmplt)
        Me.GroupBox4.Controls.Add(Me.rdoPartial)
        Me.GroupBox4.Location = New System.Drawing.Point(134, 52)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(228, 42)
        Me.GroupBox4.TabIndex = 22
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Data Transfer Mode"
        '
        'grpTransaction
        '
        Me.grpTransaction.Controls.Add(Me.chkVoucher)
        Me.grpTransaction.Location = New System.Drawing.Point(134, 220)
        Me.grpTransaction.Name = "grpTransaction"
        Me.grpTransaction.Size = New System.Drawing.Size(228, 50)
        Me.grpTransaction.TabIndex = 24
        Me.grpTransaction.TabStop = False
        Me.grpTransaction.Text = "Transactions"
        '
        'frmDataTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 680)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.lblTitle)
        Me.KeyPreview = True
        Me.Name = "frmDataTransfer"
        Me.Text = "Transfer GL Data"
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.grpTransaction.ResumeLayout(False)
        Me.grpTransaction.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblDatabase As System.Windows.Forms.Label
    Friend WithEvents cboDatabases As System.Windows.Forms.ComboBox
    Friend WithEvents Bar As System.Windows.Forms.ProgressBar
    Friend WithEvents chkVoucher As System.Windows.Forms.CheckBox
    Friend WithEvents rdoPartial As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCmplt As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnTransfer As System.Windows.Forms.Button
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
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkDCS As System.Windows.Forms.CheckBox
    Friend WithEvents chkSecurity As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOA As System.Windows.Forms.CheckBox
    Friend WithEvents chkDefinition As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents grpTransaction As System.Windows.Forms.GroupBox
End Class
