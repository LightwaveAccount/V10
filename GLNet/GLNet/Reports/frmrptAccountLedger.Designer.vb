<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmrptAccountLedger
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
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cmbSource = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblCostCenter = New System.Windows.Forms.Label
        Me.cmbCostCenter = New System.Windows.Forms.ComboBox
        Me.txtAccount = New GLNet.uiCtrlGLAccount
        Me.txtMainAccount = New GLNet.uiCtrlGLAccount
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chkInvertDatainLineChart = New System.Windows.Forms.CheckBox
        Me.chkShowlineChart = New System.Windows.Forms.CheckBox
        Me.chkShowBarChart = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chkIncludeOtherVouchers = New System.Windows.Forms.CheckBox
        Me.chkIncludeUnPostedVouchers = New System.Windows.Forms.CheckBox
        Me.chkVoucherDetail = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.rdbCustomerTax = New System.Windows.Forms.RadioButton
        Me.rdbVendorTax = New System.Windows.Forms.RadioButton
        Me.lblCompany = New System.Windows.Forms.Label
        Me.cmbCompany = New System.Windows.Forms.ComboBox
        Me.cmbFinancialYear = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.dtToDate = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtFromDate = New System.Windows.Forms.DateTimePicker
        Me.Panel2 = New System.Windows.Forms.Panel
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
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnPrintLandScape = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AccessibleDescription = "Title"
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label5.Location = New System.Drawing.Point(12, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(159, 24)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Account Ledger"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.cmbSource)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.lblCostCenter)
        Me.Panel1.Controls.Add(Me.cmbCostCenter)
        Me.Panel1.Controls.Add(Me.txtAccount)
        Me.Panel1.Controls.Add(Me.txtMainAccount)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.lblCompany)
        Me.Panel1.Controls.Add(Me.cmbCompany)
        Me.Panel1.Controls.Add(Me.cmbFinancialYear)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.GroupBox9)
        Me.Panel1.Location = New System.Drawing.Point(12, 36)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1001, 570)
        Me.Panel1.TabIndex = 0
        '
        'cmbSource
        '
        Me.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSource.FormattingEnabled = True
        Me.cmbSource.Items.AddRange(New Object() {"--Select Any Value--", "Assets", "Liability", "Capital", "Income", "Expense"})
        Me.cmbSource.Location = New System.Drawing.Point(125, 65)
        Me.cmbSource.Name = "cmbSource"
        Me.cmbSource.Size = New System.Drawing.Size(204, 21)
        Me.cmbSource.TabIndex = 13
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(17, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Source"
        '
        'lblCostCenter
        '
        Me.lblCostCenter.AutoSize = True
        Me.lblCostCenter.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostCenter.Location = New System.Drawing.Point(17, 96)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(82, 13)
        Me.lblCostCenter.TabIndex = 12
        Me.lblCostCenter.Text = "Cost Center"
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCostCenter.FormattingEnabled = True
        Me.cmbCostCenter.Location = New System.Drawing.Point(125, 92)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(204, 21)
        Me.cmbCostCenter.TabIndex = 11
        '
        'txtAccount
        '
        Me.txtAccount.AccountType = GLNet.EnumAccountTypes.None
        Me.txtAccount.GLAccountCode = Nothing
        Me.txtAccount.GLAccountHeadType = "4"
        Me.txtAccount.GLAccountID = 0
        Me.txtAccount.GLAccountName = Nothing
        Me.txtAccount.GLFilterAccount = Nothing
        Me.txtAccount.GLFilterCondition = Nothing
        Me.txtAccount.Location = New System.Drawing.Point(208, 172)
        Me.txtAccount.MinimumSize = New System.Drawing.Size(0, 30)
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.Size = New System.Drawing.Size(384, 30)
        Me.txtAccount.TabIndex = 3
        Me.txtAccount.Tag = Nothing
        '
        'txtMainAccount
        '
        Me.txtMainAccount.AccountType = GLNet.EnumAccountTypes.None
        Me.txtMainAccount.GLAccountCode = Nothing
        Me.txtMainAccount.GLAccountHeadType = "1"
        Me.txtMainAccount.GLAccountID = 0
        Me.txtMainAccount.GLAccountName = Nothing
        Me.txtMainAccount.GLFilterAccount = Nothing
        Me.txtMainAccount.GLFilterCondition = Nothing
        Me.txtMainAccount.Location = New System.Drawing.Point(208, 137)
        Me.txtMainAccount.MinimumSize = New System.Drawing.Size(0, 30)
        Me.txtMainAccount.Name = "txtMainAccount"
        Me.txtMainAccount.Size = New System.Drawing.Size(384, 30)
        Me.txtMainAccount.TabIndex = 2
        Me.txtMainAccount.Tag = Nothing
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox3.Controls.Add(Me.chkInvertDatainLineChart)
        Me.GroupBox3.Controls.Add(Me.chkShowlineChart)
        Me.GroupBox3.Controls.Add(Me.chkShowBarChart)
        Me.GroupBox3.Location = New System.Drawing.Point(212, 260)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(368, 92)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Chart Options"
        '
        'chkInvertDatainLineChart
        '
        Me.chkInvertDatainLineChart.AutoSize = True
        Me.chkInvertDatainLineChart.Location = New System.Drawing.Point(17, 65)
        Me.chkInvertDatainLineChart.Name = "chkInvertDatainLineChart"
        Me.chkInvertDatainLineChart.Size = New System.Drawing.Size(141, 17)
        Me.chkInvertDatainLineChart.TabIndex = 2
        Me.chkInvertDatainLineChart.Text = "Invert Data in Line Chart"
        Me.chkInvertDatainLineChart.UseVisualStyleBackColor = True
        '
        'chkShowlineChart
        '
        Me.chkShowlineChart.AutoSize = True
        Me.chkShowlineChart.Location = New System.Drawing.Point(17, 42)
        Me.chkShowlineChart.Name = "chkShowlineChart"
        Me.chkShowlineChart.Size = New System.Drawing.Size(104, 17)
        Me.chkShowlineChart.TabIndex = 1
        Me.chkShowlineChart.Text = "Show Line Chart"
        Me.chkShowlineChart.UseVisualStyleBackColor = True
        '
        'chkShowBarChart
        '
        Me.chkShowBarChart.AutoSize = True
        Me.chkShowBarChart.Location = New System.Drawing.Point(17, 19)
        Me.chkShowBarChart.Name = "chkShowBarChart"
        Me.chkShowBarChart.Size = New System.Drawing.Size(100, 17)
        Me.chkShowBarChart.TabIndex = 0
        Me.chkShowBarChart.Text = "Show Bar Chart"
        Me.chkShowBarChart.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.chkIncludeOtherVouchers)
        Me.GroupBox2.Controls.Add(Me.chkIncludeUnPostedVouchers)
        Me.GroupBox2.Controls.Add(Me.chkVoucherDetail)
        Me.GroupBox2.Location = New System.Drawing.Point(17, 260)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(183, 92)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Voucher Options"
        '
        'chkIncludeOtherVouchers
        '
        Me.chkIncludeOtherVouchers.AutoSize = True
        Me.chkIncludeOtherVouchers.Location = New System.Drawing.Point(17, 65)
        Me.chkIncludeOtherVouchers.Name = "chkIncludeOtherVouchers"
        Me.chkIncludeOtherVouchers.Size = New System.Drawing.Size(138, 17)
        Me.chkIncludeOtherVouchers.TabIndex = 2
        Me.chkIncludeOtherVouchers.Text = "Include Other Vouchers"
        Me.chkIncludeOtherVouchers.UseVisualStyleBackColor = True
        '
        'chkIncludeUnPostedVouchers
        '
        Me.chkIncludeUnPostedVouchers.AutoSize = True
        Me.chkIncludeUnPostedVouchers.Location = New System.Drawing.Point(17, 42)
        Me.chkIncludeUnPostedVouchers.Name = "chkIncludeUnPostedVouchers"
        Me.chkIncludeUnPostedVouchers.Size = New System.Drawing.Size(159, 17)
        Me.chkIncludeUnPostedVouchers.TabIndex = 1
        Me.chkIncludeUnPostedVouchers.Text = "Include UnPosted Vouchers"
        Me.chkIncludeUnPostedVouchers.UseVisualStyleBackColor = True
        '
        'chkVoucherDetail
        '
        Me.chkVoucherDetail.AutoSize = True
        Me.chkVoucherDetail.Location = New System.Drawing.Point(17, 19)
        Me.chkVoucherDetail.Name = "chkVoucherDetail"
        Me.chkVoucherDetail.Size = New System.Drawing.Size(96, 17)
        Me.chkVoucherDetail.TabIndex = 0
        Me.chkVoucherDetail.Text = "Voucher Detail"
        Me.chkVoucherDetail.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(141, 179)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Account"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Controls.Add(Me.rdbCustomerTax)
        Me.GroupBox1.Controls.Add(Me.rdbVendorTax)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 125)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(183, 43)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Account Type"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(124, 19)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(49, 17)
        Me.RadioButton1.TabIndex = 2
        Me.RadioButton1.Text = "Third"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'rdbCustomerTax
        '
        Me.rdbCustomerTax.AutoSize = True
        Me.rdbCustomerTax.Checked = True
        Me.rdbCustomerTax.Location = New System.Drawing.Point(6, 19)
        Me.rdbCustomerTax.Name = "rdbCustomerTax"
        Me.rdbCustomerTax.Size = New System.Drawing.Size(44, 17)
        Me.rdbCustomerTax.TabIndex = 0
        Me.rdbCustomerTax.TabStop = True
        Me.rdbCustomerTax.Text = "First"
        Me.rdbCustomerTax.UseVisualStyleBackColor = True
        '
        'rdbVendorTax
        '
        Me.rdbVendorTax.AutoSize = True
        Me.rdbVendorTax.Location = New System.Drawing.Point(56, 19)
        Me.rdbVendorTax.Name = "rdbVendorTax"
        Me.rdbVendorTax.Size = New System.Drawing.Size(62, 17)
        Me.rdbVendorTax.TabIndex = 1
        Me.rdbVendorTax.Text = "Second"
        Me.rdbVendorTax.UseVisualStyleBackColor = True
        '
        'lblCompany
        '
        Me.lblCompany.AutoSize = True
        Me.lblCompany.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany.Location = New System.Drawing.Point(17, 42)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(67, 13)
        Me.lblCompany.TabIndex = 8
        Me.lblCompany.Text = "Company"
        '
        'cmbCompany
        '
        Me.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Location = New System.Drawing.Point(125, 38)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(204, 21)
        Me.cmbCompany.TabIndex = 1
        '
        'cmbFinancialYear
        '
        Me.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFinancialYear.FormattingEnabled = True
        Me.cmbFinancialYear.Location = New System.Drawing.Point(125, 11)
        Me.cmbFinancialYear.Name = "cmbFinancialYear"
        Me.cmbFinancialYear.Size = New System.Drawing.Size(204, 21)
        Me.cmbFinancialYear.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Financial Year"
        '
        'GroupBox9
        '
        Me.GroupBox9.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox9.Controls.Add(Me.dtToDate)
        Me.GroupBox9.Controls.Add(Me.Label2)
        Me.GroupBox9.Controls.Add(Me.dtFromDate)
        Me.GroupBox9.Location = New System.Drawing.Point(17, 204)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(563, 44)
        Me.GroupBox9.TabIndex = 3
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Date Range"
        '
        'dtToDate
        '
        Me.dtToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtToDate.Location = New System.Drawing.Point(142, 17)
        Me.dtToDate.Name = "dtToDate"
        Me.dtToDate.Size = New System.Drawing.Size(98, 20)
        Me.dtToDate.TabIndex = 1
        Me.dtToDate.Value = New Date(2009, 1, 6, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(127, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(13, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "_"
        '
        'dtFromDate
        '
        Me.dtFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFromDate.Location = New System.Drawing.Point(21, 17)
        Me.dtFromDate.Name = "dtFromDate"
        Me.dtFromDate.Size = New System.Drawing.Size(99, 20)
        Me.dtFromDate.TabIndex = 0
        Me.dtFromDate.Value = New Date(2009, 1, 6, 0, 0, 0, 0)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnPrintLandScape)
        Me.Panel2.Controls.Add(Me.btnPrint)
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
        Me.Panel2.Size = New System.Drawing.Size(1030, 50)
        Me.Panel2.TabIndex = 18
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
        'btnPrintLandScape
        '
        Me.btnPrintLandScape.Location = New System.Drawing.Point(857, 0)
        Me.btnPrintLandScape.Name = "btnPrintLandScape"
        Me.btnPrintLandScape.Size = New System.Drawing.Size(48, 48)
        Me.btnPrintLandScape.TabIndex = 14
        Me.btnPrintLandScape.Tag = "HideText"
        Me.ToolTip1.SetToolTip(Me.btnPrintLandScape, "Land Scape Voucher Print")
        Me.btnPrintLandScape.UseVisualStyleBackColor = True
        '
        'frmrptAccountLedger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 693)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.Name = "frmrptAccountLedger"
        Me.Text = "Account Ledger"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbCustomerTax As System.Windows.Forms.RadioButton
    Friend WithEvents rdbVendorTax As System.Windows.Forms.RadioButton
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFinancialYear As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents dtToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkInvertDatainLineChart As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowlineChart As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowBarChart As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkIncludeOtherVouchers As System.Windows.Forms.CheckBox
    Friend WithEvents chkIncludeUnPostedVouchers As System.Windows.Forms.CheckBox
    Friend WithEvents chkVoucherDetail As System.Windows.Forms.CheckBox
    Friend WithEvents txtAccount As GLNet.uiCtrlGLAccount
    Friend WithEvents txtMainAccount As GLNet.uiCtrlGLAccount
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
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
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbSource As System.Windows.Forms.ComboBox
    Friend WithEvents lblCostCenter As System.Windows.Forms.Label
    Friend WithEvents cmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrintLandScape As System.Windows.Forms.Button
End Class
