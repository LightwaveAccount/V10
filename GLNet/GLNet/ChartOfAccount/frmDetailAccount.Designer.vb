<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDetailAccount
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
        Me.txtDetailTitle = New System.Windows.Forms.TextBox
        Me.txtDetailCode = New System.Windows.Forms.TextBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.CboChequeType = New System.Windows.Forms.ComboBox
        Me.grpAccountDetailSearch = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.chkStatus = New System.Windows.Forms.CheckBox
        Me.lblPhone = New System.Windows.Forms.Label
        Me.lblURL = New System.Windows.Forms.Label
        Me.lblName = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnFirst = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnPrevious = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnNext = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnLast = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.grdMainAccounts = New Janus.Windows.GridEX.GridEX
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UiCtrlGridBar1 = New GLNet.uiCtrlGridBar
        Me.txtDetailAccount = New GLNet.uiCtrlGLAccount
        Me.txtAccountSubSub = New GLNet.uiCtrlGLAccount
        Me.Panel3.SuspendLayout()
        Me.grpAccountDetailSearch.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.grdMainAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtDetailTitle
        '
        Me.txtDetailTitle.Location = New System.Drawing.Point(188, 74)
        Me.txtDetailTitle.MaxLength = 50
        Me.txtDetailTitle.Name = "txtDetailTitle"
        Me.txtDetailTitle.Size = New System.Drawing.Size(369, 20)
        Me.txtDetailTitle.TabIndex = 2
        Me.txtDetailTitle.Tag = "IsRequired"
        '
        'txtDetailCode
        '
        Me.txtDetailCode.Location = New System.Drawing.Point(188, 48)
        Me.txtDetailCode.MaxLength = 5
        Me.txtDetailCode.Name = "txtDetailCode"
        Me.txtDetailCode.Size = New System.Drawing.Size(369, 20)
        Me.txtDetailCode.TabIndex = 1
        Me.txtDetailCode.Tag = "IsRequired"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.CboChequeType)
        Me.Panel3.Controls.Add(Me.grpAccountDetailSearch)
        Me.Panel3.Controls.Add(Me.chkStatus)
        Me.Panel3.Controls.Add(Me.txtDetailTitle)
        Me.Panel3.Controls.Add(Me.txtDetailCode)
        Me.Panel3.Controls.Add(Me.txtAccountSubSub)
        Me.Panel3.Controls.Add(Me.lblPhone)
        Me.Panel3.Controls.Add(Me.lblURL)
        Me.Panel3.Controls.Add(Me.lblName)
        Me.Panel3.Location = New System.Drawing.Point(14, 39)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(973, 139)
        Me.Panel3.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(36, 106)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(135, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Select Cheque Type"
        '
        'CboChequeType
        '
        Me.CboChequeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboChequeType.FormattingEnabled = True
        Me.CboChequeType.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.CboChequeType.Location = New System.Drawing.Point(188, 102)
        Me.CboChequeType.Name = "CboChequeType"
        Me.CboChequeType.Size = New System.Drawing.Size(209, 21)
        Me.CboChequeType.TabIndex = 11
        '
        'grpAccountDetailSearch
        '
        Me.grpAccountDetailSearch.Controls.Add(Me.txtDetailAccount)
        Me.grpAccountDetailSearch.Controls.Add(Me.Label3)
        Me.grpAccountDetailSearch.Location = New System.Drawing.Point(563, 3)
        Me.grpAccountDetailSearch.Name = "grpAccountDetailSearch"
        Me.grpAccountDetailSearch.Size = New System.Drawing.Size(405, 94)
        Me.grpAccountDetailSearch.TabIndex = 10
        Me.grpAccountDetailSearch.TabStop = False
        Me.grpAccountDetailSearch.Text = "Account Detail Search"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Detail Account"
        '
        'chkStatus
        '
        Me.chkStatus.AutoSize = True
        Me.chkStatus.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStatus.Location = New System.Drawing.Point(430, 105)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.Size = New System.Drawing.Size(96, 17)
        Me.chkStatus.TabIndex = 10
        Me.chkStatus.Text = "Deactivate"
        Me.chkStatus.UseVisualStyleBackColor = True
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone.Location = New System.Drawing.Point(36, 77)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(107, 13)
        Me.lblPhone.TabIndex = 8
        Me.lblPhone.Text = "A/C Detail Title"
        '
        'lblURL
        '
        Me.lblURL.AutoSize = True
        Me.lblURL.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblURL.Location = New System.Drawing.Point(36, 51)
        Me.lblURL.Name = "lblURL"
        Me.lblURL.Size = New System.Drawing.Size(110, 13)
        Me.lblURL.TabIndex = 6
        Me.lblURL.Text = "A/C Detail Code"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(35, 24)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(124, 13)
        Me.lblName.TabIndex = 5
        Me.lblName.Text = "A/C Sub Sub Code"
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
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(565, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(48, 48)
        Me.btnCancel.TabIndex = 9
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
        Me.Panel2.TabIndex = 3
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(511, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(48, 48)
        Me.btnDelete.TabIndex = 8
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
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(457, 0)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(48, 48)
        Me.btnUpdate.TabIndex = 7
        Me.btnUpdate.Tag = "HideText"
        Me.btnUpdate.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnUpdate, "Update (Ctrl+U)")
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.Location = New System.Drawing.Point(172, 0)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(48, 48)
        Me.btnLast.TabIndex = 4
        Me.btnLast.Tag = "HideText"
        Me.btnLast.Text = " "
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(403, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(48, 48)
        Me.btnSave.TabIndex = 6
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
        Me.btnNew.TabIndex = 5
        Me.btnNew.Tag = "HideText"
        Me.btnNew.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnNew, "New (Ctrl+N)")
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'grdMainAccounts
        '
        Me.grdMainAccounts.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdMainAccounts.Location = New System.Drawing.Point(14, 43)
        Me.grdMainAccounts.Name = "grdMainAccounts"
        Me.grdMainAccounts.Size = New System.Drawing.Size(942, 351)
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
        Me.Panel1.Size = New System.Drawing.Size(1000, 596)
        Me.Panel1.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AccessibleDescription = "Title"
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label5.Location = New System.Drawing.Point(16, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(145, 24)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Detail Account"
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.UiCtrlGridBar1)
        Me.Panel5.Controls.Add(Me.grdMainAccounts)
        Me.Panel5.Location = New System.Drawing.Point(14, 184)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(973, 404)
        Me.Panel5.TabIndex = 1
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
        'txtDetailAccount
        '
        Me.txtDetailAccount.AccountType = GLNet.EnumAccountTypes.None
        Me.txtDetailAccount.GLAccountCode = Nothing
        Me.txtDetailAccount.GLAccountHeadType = "4"
        Me.txtDetailAccount.GLAccountID = 0
        Me.txtDetailAccount.GLAccountName = Nothing
        Me.txtDetailAccount.GLFilterAccount = Nothing
        Me.txtDetailAccount.GLFilterCondition = Nothing
        Me.txtDetailAccount.Location = New System.Drawing.Point(89, 15)
        Me.txtDetailAccount.MinimumSize = New System.Drawing.Size(0, 30)
        Me.txtDetailAccount.Name = "txtDetailAccount"
        Me.txtDetailAccount.Size = New System.Drawing.Size(316, 30)
        Me.txtDetailAccount.TabIndex = 12
        Me.txtDetailAccount.Tag = Nothing
        '
        'txtAccountSubSub
        '
        Me.txtAccountSubSub.AccountType = GLNet.EnumAccountTypes.None
        Me.txtAccountSubSub.GLAccountCode = Nothing
        Me.txtAccountSubSub.GLAccountHeadType = "3"
        Me.txtAccountSubSub.GLAccountID = 0
        Me.txtAccountSubSub.GLAccountName = Nothing
        Me.txtAccountSubSub.GLFilterAccount = Nothing
        Me.txtAccountSubSub.GLFilterCondition = Nothing
        Me.txtAccountSubSub.Location = New System.Drawing.Point(185, 18)
        Me.txtAccountSubSub.MinimumSize = New System.Drawing.Size(0, 30)
        Me.txtAccountSubSub.Name = "txtAccountSubSub"
        Me.txtAccountSubSub.Size = New System.Drawing.Size(372, 30)
        Me.txtAccountSubSub.TabIndex = 0
        Me.txtAccountSubSub.Tag = Nothing
        '
        'frmDetailAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1022, 740)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "frmDetailAccount"
        Me.Text = "Detail Account"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.grpAccountDetailSearch.ResumeLayout(False)
        Me.grpAccountDetailSearch.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.grdMainAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtDetailTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtDetailCode As System.Windows.Forms.TextBox
    Friend WithEvents txtAccountSubSub As GLNet.uiCtrlGLAccount
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblPhone As System.Windows.Forms.Label
    Friend WithEvents lblURL As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents grdMainAccounts As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents UiCtrlGridBar1 As GLNet.uiCtrlGridBar
    Friend WithEvents chkStatus As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents grpAccountDetailSearch As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDetailAccount As GLNet.uiCtrlGLAccount
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CboChequeType As System.Windows.Forms.ComboBox
End Class
