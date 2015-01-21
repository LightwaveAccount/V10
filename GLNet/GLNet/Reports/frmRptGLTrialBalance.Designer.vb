<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptGLTrialBalance
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
        Me.components = New System.ComponentModel.Container
        Me.lblTitle = New System.Windows.Forms.Label
        Me.chkIncludeOtherVouchers = New System.Windows.Forms.CheckBox
        Me.lblEndDate = New System.Windows.Forms.Label
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker
        Me.lblStartDate = New System.Windows.Forms.Label
        Me.lblAccountNo = New System.Windows.Forms.Label
        Me.cboGroup = New System.Windows.Forms.ComboBox
        Me.lblGroup = New System.Windows.Forms.Label
        Me.chkIncludeUnPostedVouchers = New System.Windows.Forms.CheckBox
        Me.cboFinancialYear = New System.Windows.Forms.ComboBox
        Me.lblCompany = New System.Windows.Forms.Label
        Me.cboCompany = New System.Windows.Forms.ComboBox
        Me.lblFinancialYear = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnPrint = New System.Windows.Forms.Button
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
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cmbSource = New System.Windows.Forms.ComboBox
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.chkHideZeroBalance = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UiCtrlGLAccount1 = New GLNet.uiCtrlGLAccount
        Me.cboCostCenter = New System.Windows.Forms.ComboBox
        Me.lblCostCenter = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
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
        Me.lblTitle.Size = New System.Drawing.Size(200, 24)
        Me.lblTitle.TabIndex = 12
        Me.lblTitle.Text = "Trial Balance Report"
        '
        'chkIncludeOtherVouchers
        '
        Me.chkIncludeOtherVouchers.AutoSize = True
        Me.chkIncludeOtherVouchers.Location = New System.Drawing.Point(18, 72)
        Me.chkIncludeOtherVouchers.Name = "chkIncludeOtherVouchers"
        Me.chkIncludeOtherVouchers.Size = New System.Drawing.Size(138, 17)
        Me.chkIncludeOtherVouchers.TabIndex = 19
        Me.chkIncludeOtherVouchers.Text = "Include Other Vouchers"
        Me.chkIncludeOtherVouchers.UseVisualStyleBackColor = True
        '
        'lblEndDate
        '
        Me.lblEndDate.AutoSize = True
        Me.lblEndDate.Location = New System.Drawing.Point(199, 22)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(52, 13)
        Me.lblEndDate.TabIndex = 18
        Me.lblEndDate.Text = "End Date"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.Location = New System.Drawing.Point(257, 18)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(103, 20)
        Me.dtpEndDate.TabIndex = 17
        '
        'dtpStartDate
        '
        Me.dtpStartDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(71, 18)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(106, 20)
        Me.dtpStartDate.TabIndex = 16
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Location = New System.Drawing.Point(11, 22)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(55, 13)
        Me.lblStartDate.TabIndex = 15
        Me.lblStartDate.Text = "Start Date"
        '
        'lblAccountNo
        '
        Me.lblAccountNo.AutoSize = True
        Me.lblAccountNo.Location = New System.Drawing.Point(24, 158)
        Me.lblAccountNo.Name = "lblAccountNo"
        Me.lblAccountNo.Size = New System.Drawing.Size(43, 13)
        Me.lblAccountNo.TabIndex = 9
        Me.lblAccountNo.Text = "A/C No"
        '
        'cboGroup
        '
        Me.cboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGroup.FormattingEnabled = True
        Me.cboGroup.Items.AddRange(New Object() {"---Select---", "First Level", "Second Level", "Third Level", "Detail Level"})
        Me.cboGroup.Location = New System.Drawing.Point(108, 103)
        Me.cboGroup.Name = "cboGroup"
        Me.cboGroup.Size = New System.Drawing.Size(289, 21)
        Me.cboGroup.TabIndex = 8
        '
        'lblGroup
        '
        Me.lblGroup.AutoSize = True
        Me.lblGroup.Location = New System.Drawing.Point(24, 106)
        Me.lblGroup.Name = "lblGroup"
        Me.lblGroup.Size = New System.Drawing.Size(36, 13)
        Me.lblGroup.TabIndex = 7
        Me.lblGroup.Text = "Group"
        '
        'chkIncludeUnPostedVouchers
        '
        Me.chkIncludeUnPostedVouchers.AutoSize = True
        Me.chkIncludeUnPostedVouchers.Location = New System.Drawing.Point(18, 49)
        Me.chkIncludeUnPostedVouchers.Name = "chkIncludeUnPostedVouchers"
        Me.chkIncludeUnPostedVouchers.Size = New System.Drawing.Size(159, 17)
        Me.chkIncludeUnPostedVouchers.TabIndex = 6
        Me.chkIncludeUnPostedVouchers.Text = "Include UnPosted Vouchers"
        Me.chkIncludeUnPostedVouchers.UseVisualStyleBackColor = True
        '
        'cboFinancialYear
        '
        Me.cboFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFinancialYear.FormattingEnabled = True
        Me.cboFinancialYear.Location = New System.Drawing.Point(108, 17)
        Me.cboFinancialYear.Name = "cboFinancialYear"
        Me.cboFinancialYear.Size = New System.Drawing.Size(146, 21)
        Me.cboFinancialYear.TabIndex = 3
        '
        'lblCompany
        '
        Me.lblCompany.AutoSize = True
        Me.lblCompany.Location = New System.Drawing.Point(24, 50)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(51, 13)
        Me.lblCompany.TabIndex = 2
        Me.lblCompany.Text = "Company"
        '
        'cboCompany
        '
        Me.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(108, 46)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(289, 21)
        Me.cboCompany.TabIndex = 1
        '
        'lblFinancialYear
        '
        Me.lblFinancialYear.AutoSize = True
        Me.lblFinancialYear.Location = New System.Drawing.Point(24, 21)
        Me.lblFinancialYear.Name = "lblFinancialYear"
        Me.lblFinancialYear.Size = New System.Drawing.Size(74, 13)
        Me.lblFinancialYear.TabIndex = 0
        Me.lblFinancialYear.Text = "Financial Year"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnPrint)
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
        Me.Panel1.TabIndex = 14
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(911, 0)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(48, 48)
        Me.btnPrint.TabIndex = 10
        Me.btnPrint.Tag = "HideText"
        Me.btnPrint.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnPrint, "Print (Ctrl+P)")
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(965, 0)
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
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.cboCostCenter)
        Me.Panel2.Controls.Add(Me.lblCostCenter)
        Me.Panel2.Controls.Add(Me.cmbSource)
        Me.Panel2.Controls.Add(Me.GroupBox9)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.UiCtrlGLAccount1)
        Me.Panel2.Controls.Add(Me.cboFinancialYear)
        Me.Panel2.Controls.Add(Me.lblFinancialYear)
        Me.Panel2.Controls.Add(Me.cboCompany)
        Me.Panel2.Controls.Add(Me.lblCompany)
        Me.Panel2.Controls.Add(Me.lblGroup)
        Me.Panel2.Controls.Add(Me.lblAccountNo)
        Me.Panel2.Controls.Add(Me.cboGroup)
        Me.Panel2.Location = New System.Drawing.Point(12, 36)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1001, 570)
        Me.Panel2.TabIndex = 15
        '
        'cmbSource
        '
        Me.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSource.FormattingEnabled = True
        Me.cmbSource.Items.AddRange(New Object() {"--Select Any Value--", "Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbSource.Location = New System.Drawing.Point(107, 74)
        Me.cmbSource.Name = "cmbSource"
        Me.cmbSource.Size = New System.Drawing.Size(290, 21)
        Me.cmbSource.TabIndex = 13
        '
        'GroupBox9
        '
        Me.GroupBox9.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox9.Controls.Add(Me.chkHideZeroBalance)
        Me.GroupBox9.Controls.Add(Me.lblStartDate)
        Me.GroupBox9.Controls.Add(Me.dtpStartDate)
        Me.GroupBox9.Controls.Add(Me.chkIncludeOtherVouchers)
        Me.GroupBox9.Controls.Add(Me.lblEndDate)
        Me.GroupBox9.Controls.Add(Me.dtpEndDate)
        Me.GroupBox9.Controls.Add(Me.chkIncludeUnPostedVouchers)
        Me.GroupBox9.Location = New System.Drawing.Point(108, 195)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(367, 122)
        Me.GroupBox9.TabIndex = 16
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Filter Criteria"
        '
        'chkHideZeroBalance
        '
        Me.chkHideZeroBalance.AutoSize = True
        Me.chkHideZeroBalance.Location = New System.Drawing.Point(18, 95)
        Me.chkHideZeroBalance.Name = "chkHideZeroBalance"
        Me.chkHideZeroBalance.Size = New System.Drawing.Size(169, 17)
        Me.chkHideZeroBalance.TabIndex = 21
        Me.chkHideZeroBalance.Text = "Hide Zero Balance Account(s)"
        Me.chkHideZeroBalance.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(24, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Source"
        '
        'UiCtrlGLAccount1
        '
        Me.UiCtrlGLAccount1.AccountType = GLNet.EnumAccountTypes.None
        Me.UiCtrlGLAccount1.GLAccountCode = Nothing
        Me.UiCtrlGLAccount1.GLAccountHeadType = "1"
        Me.UiCtrlGLAccount1.GLAccountID = 0
        Me.UiCtrlGLAccount1.GLAccountName = Nothing
        Me.UiCtrlGLAccount1.GLFilterAccount = Nothing
        Me.UiCtrlGLAccount1.GLFilterCondition = Nothing
        Me.UiCtrlGLAccount1.Location = New System.Drawing.Point(105, 159)
        Me.UiCtrlGLAccount1.MinimumSize = New System.Drawing.Size(0, 30)
        Me.UiCtrlGLAccount1.Name = "UiCtrlGLAccount1"
        Me.UiCtrlGLAccount1.Size = New System.Drawing.Size(384, 30)
        Me.UiCtrlGLAccount1.TabIndex = 20
        Me.UiCtrlGLAccount1.Tag = Nothing
        '
        'cboCostCenter
        '
        Me.cboCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCostCenter.FormattingEnabled = True
        Me.cboCostCenter.Location = New System.Drawing.Point(108, 132)
        Me.cboCostCenter.Name = "cboCostCenter"
        Me.cboCostCenter.Size = New System.Drawing.Size(289, 21)
        Me.cboCostCenter.TabIndex = 21
        '
        'lblCostCenter
        '
        Me.lblCostCenter.AutoSize = True
        Me.lblCostCenter.Location = New System.Drawing.Point(24, 136)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(62, 13)
        Me.lblCostCenter.TabIndex = 22
        Me.lblCostCenter.Text = "Cost Center"
        '
        'frmRptGLTrialBalance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 680)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblTitle)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRptGLTrialBalance"
        Me.Text = "Trial Balance"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents chkIncludeUnPostedVouchers As System.Windows.Forms.CheckBox
    Friend WithEvents cboFinancialYear As System.Windows.Forms.ComboBox
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents lblFinancialYear As System.Windows.Forms.Label
    Friend WithEvents cboGroup As System.Windows.Forms.ComboBox
    Friend WithEvents lblGroup As System.Windows.Forms.Label
    Friend WithEvents lblAccountNo As System.Windows.Forms.Label
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblStartDate As System.Windows.Forms.Label
    Friend WithEvents lblEndDate As System.Windows.Forms.Label
    Friend WithEvents chkIncludeOtherVouchers As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnPrint As System.Windows.Forms.Button
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
    Friend WithEvents UiCtrlGLAccount1 As GLNet.uiCtrlGLAccount
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chkHideZeroBalance As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbSource As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents lblCostCenter As System.Windows.Forms.Label
End Class
