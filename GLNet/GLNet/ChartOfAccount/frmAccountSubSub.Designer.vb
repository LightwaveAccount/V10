<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAccountSubSub
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
        Me.components = New System.ComponentModel.Container
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnNext = New System.Windows.Forms.Button
        Me.lblPhone = New System.Windows.Forms.Label
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.lblURL = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnFirst = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnPrevious = New System.Windows.Forms.Button
        Me.btnLast = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.UiCtrlGridBar1 = New GLNet.uiCtrlGridBar
        Me.grdMainAccounts = New Janus.Windows.GridEX.GridEX
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.cmbPLNote = New System.Windows.Forms.ComboBox
        Me.cmbCrBSNote = New System.Windows.Forms.ComboBox
        Me.cmbDrBSNote = New System.Windows.Forms.ComboBox
        Me.cmbAccountType = New System.Windows.Forms.ComboBox
        Me.txtSubSubAccountTitle = New System.Windows.Forms.TextBox
        Me.txtSubSubAccountCode = New System.Windows.Forms.TextBox
        Me.txtAccountSub = New GLNet.uiCtrlGLAccount
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblName = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2.SuspendLayout()
        CType(Me.grdMainAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(511, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(48, 48)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Tag = "HideText"
        Me.btnDelete.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete (Ctrl+D)")
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(118, 0)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(48, 48)
        Me.btnNext.TabIndex = 2
        Me.btnNext.Tag = "HideText"
        Me.btnNext.Text = " "
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone.Location = New System.Drawing.Point(36, 77)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(121, 13)
        Me.lblPhone.TabIndex = 8
        Me.lblPhone.Text = "A/C Sub Sub Title"
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(457, 0)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(48, 48)
        Me.btnUpdate.TabIndex = 6
        Me.btnUpdate.Tag = "HideText"
        Me.btnUpdate.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnUpdate, "Update (Ctrl+U)")
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'lblURL
        '
        Me.lblURL.AutoSize = True
        Me.lblURL.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblURL.Location = New System.Drawing.Point(36, 44)
        Me.lblURL.Name = "lblURL"
        Me.lblURL.Size = New System.Drawing.Size(124, 13)
        Me.lblURL.TabIndex = 6
        Me.lblURL.Text = "A/C Sub Sub Code"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(962, 0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 48)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Tag = "HideText"
        Me.btnExit.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnExit, "Exit (Ctrl+X)")
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnFirst
        '
        Me.btnFirst.Location = New System.Drawing.Point(10, 0)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(48, 48)
        Me.btnFirst.TabIndex = 0
        Me.btnFirst.Tag = "HideText"
        Me.btnFirst.Text = " "
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnExit)
        Me.Panel2.Controls.Add(Me.btnFirst)
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnPrevious)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.btnNext)
        Me.Panel2.Controls.Add(Me.btnUpdate)
        Me.Panel2.Controls.Add(Me.btnLast)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Controls.Add(Me.btnNew)
        Me.Panel2.Location = New System.Drawing.Point(0, 612)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1051, 50)
        Me.Panel2.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(565, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(48, 48)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Tag = "HideText"
        Me.btnCancel.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnCancel, "Cancel (Ctrl+E)")
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.Location = New System.Drawing.Point(64, 0)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(48, 48)
        Me.btnPrevious.TabIndex = 1
        Me.btnPrevious.Tag = "HideText"
        Me.btnPrevious.Text = " "
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.Location = New System.Drawing.Point(172, 0)
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
        Me.ToolTip1.SetToolTip(Me.btnSave, "Save (Ctrl+S)")
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
        Me.ToolTip1.SetToolTip(Me.btnNew, "New (Ctrl+N)")
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'UiCtrlGridBar1
        '
        Me.UiCtrlGridBar1.Location = New System.Drawing.Point(14, 12)
        Me.UiCtrlGridBar1.MyGrid = Me.grdMainAccounts
        Me.UiCtrlGridBar1.Name = "UiCtrlGridBar1"
        Me.UiCtrlGridBar1.Size = New System.Drawing.Size(942, 25)
        Me.UiCtrlGridBar1.TabIndex = 0
        Me.UiCtrlGridBar1.TabStop = False
        '
        'grdMainAccounts
        '
        Me.grdMainAccounts.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdMainAccounts.Location = New System.Drawing.Point(14, 43)
        Me.grdMainAccounts.Name = "grdMainAccounts"
        Me.grdMainAccounts.Size = New System.Drawing.Size(942, 250)
        Me.grdMainAccounts.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Location = New System.Drawing.Point(10, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1000, 595)
        Me.Panel1.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AccessibleDescription = "Title"
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label5.Location = New System.Drawing.Point(10, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(173, 24)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Sub Sub Account"
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.UiCtrlGridBar1)
        Me.Panel5.Controls.Add(Me.grdMainAccounts)
        Me.Panel5.Location = New System.Drawing.Point(14, 285)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(973, 302)
        Me.Panel5.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.cmbPLNote)
        Me.Panel3.Controls.Add(Me.cmbCrBSNote)
        Me.Panel3.Controls.Add(Me.cmbDrBSNote)
        Me.Panel3.Controls.Add(Me.cmbAccountType)
        Me.Panel3.Controls.Add(Me.txtSubSubAccountTitle)
        Me.Panel3.Controls.Add(Me.txtSubSubAccountCode)
        Me.Panel3.Controls.Add(Me.txtAccountSub)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.lblPhone)
        Me.Panel3.Controls.Add(Me.lblURL)
        Me.Panel3.Controls.Add(Me.lblName)
        Me.Panel3.Location = New System.Drawing.Point(14, 32)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(973, 244)
        Me.Panel3.TabIndex = 0
        '
        'cmbPLNote
        '
        Me.cmbPLNote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPLNote.FormattingEnabled = True
        Me.cmbPLNote.Items.AddRange(New Object() {"--Select Any Value--", "Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbPLNote.Location = New System.Drawing.Point(188, 209)
        Me.cmbPLNote.Name = "cmbPLNote"
        Me.cmbPLNote.Size = New System.Drawing.Size(369, 21)
        Me.cmbPLNote.TabIndex = 6
        '
        'cmbCrBSNote
        '
        Me.cmbCrBSNote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCrBSNote.FormattingEnabled = True
        Me.cmbCrBSNote.Items.AddRange(New Object() {"--Select Any Value--", "Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbCrBSNote.Location = New System.Drawing.Point(188, 176)
        Me.cmbCrBSNote.Name = "cmbCrBSNote"
        Me.cmbCrBSNote.Size = New System.Drawing.Size(369, 21)
        Me.cmbCrBSNote.TabIndex = 5
        '
        'cmbDrBSNote
        '
        Me.cmbDrBSNote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDrBSNote.FormattingEnabled = True
        Me.cmbDrBSNote.Items.AddRange(New Object() {"--Select Any Value--", "Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbDrBSNote.Location = New System.Drawing.Point(188, 143)
        Me.cmbDrBSNote.Name = "cmbDrBSNote"
        Me.cmbDrBSNote.Size = New System.Drawing.Size(369, 21)
        Me.cmbDrBSNote.TabIndex = 4
        '
        'cmbAccountType
        '
        Me.cmbAccountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAccountType.FormattingEnabled = True
        Me.cmbAccountType.Items.AddRange(New Object() {"--Select Any Value--", "Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbAccountType.Location = New System.Drawing.Point(188, 110)
        Me.cmbAccountType.Name = "cmbAccountType"
        Me.cmbAccountType.Size = New System.Drawing.Size(369, 21)
        Me.cmbAccountType.TabIndex = 3
        '
        'txtSubSubAccountTitle
        '
        Me.txtSubSubAccountTitle.Location = New System.Drawing.Point(188, 77)
        Me.txtSubSubAccountTitle.MaxLength = 50
        Me.txtSubSubAccountTitle.Name = "txtSubSubAccountTitle"
        Me.txtSubSubAccountTitle.Size = New System.Drawing.Size(369, 20)
        Me.txtSubSubAccountTitle.TabIndex = 2
        Me.txtSubSubAccountTitle.Tag = "IsRequired"
        '
        'txtSubSubAccountCode
        '
        Me.txtSubSubAccountCode.Location = New System.Drawing.Point(188, 44)
        Me.txtSubSubAccountCode.MaxLength = 3
        Me.txtSubSubAccountCode.Name = "txtSubSubAccountCode"
        Me.txtSubSubAccountCode.Size = New System.Drawing.Size(369, 20)
        Me.txtSubSubAccountCode.TabIndex = 1
        Me.txtSubSubAccountCode.Tag = "IsRequired"
        '
        'txtAccountSub
        '
        Me.txtAccountSub.AccountType = GLNet.EnumAccountTypes.None
        Me.txtAccountSub.GLAccountCode = Nothing
        Me.txtAccountSub.GLAccountHeadType = "2"
        Me.txtAccountSub.GLAccountID = 0
        Me.txtAccountSub.GLAccountName = Nothing
        Me.txtAccountSub.GLFilterAccount = Nothing
        Me.txtAccountSub.GLFilterCondition = Nothing
        Me.txtAccountSub.Location = New System.Drawing.Point(185, 11)
        Me.txtAccountSub.MinimumSize = New System.Drawing.Size(0, 30)
        Me.txtAccountSub.Name = "txtAccountSub"
        Me.txtAccountSub.Size = New System.Drawing.Size(384, 30)
        Me.txtAccountSub.TabIndex = 0
        Me.txtAccountSub.Tag = Nothing
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(36, 209)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "PL Note"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(36, 176)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Cr BS Note"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(36, 143)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Dr BS Note"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(36, 110)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "A/C Type"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(36, 11)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(60, 13)
        Me.lblName.TabIndex = 5
        Me.lblName.Text = "A/C Sub"
        '
        'frmAccountSubSub
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1022, 740)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.Name = "frmAccountSubSub"
        Me.Text = "Sub Sub Account"
        Me.Panel2.ResumeLayout(False)
        CType(Me.grdMainAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents lblPhone As System.Windows.Forms.Label
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents lblURL As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents UiCtrlGridBar1 As GLNet.uiCtrlGridBar
    Friend WithEvents grdMainAccounts As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAccountSub As GLNet.uiCtrlGLAccount
    Friend WithEvents txtSubSubAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSubSubAccountTitle As System.Windows.Forms.TextBox
    Friend WithEvents cmbAccountType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPLNote As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCrBSNote As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDrBSNote As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
