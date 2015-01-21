<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProfitAndLossMonthWise
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
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnGenerateReport = New System.Windows.Forms.Button
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.grpReportView = New System.Windows.Forms.GroupBox
        Me.rdoDetail = New System.Windows.Forms.RadioButton
        Me.rdoSummary = New System.Windows.Forms.RadioButton
        Me.chkOtherVoucher = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboShop = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblCompany = New System.Windows.Forms.Label
        Me.cmbCostCenter = New System.Windows.Forms.ComboBox
        Me.cmbCompany = New System.Windows.Forms.ComboBox
        Me.cmbFinancialYear = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.dtToDate = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtFromDate = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.UiCtrlGridBar1 = New GLNet.uiCtrlGridBar
        Me.grdAllRecords = New Janus.Windows.GridEX.GridEX
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnExp1stLvl = New System.Windows.Forms.Button
        Me.btnExp2ndLvl = New System.Windows.Forms.Button
        Me.btnExp3rdLvl = New System.Windows.Forms.Button
        Me.grpExpClp = New System.Windows.Forms.GroupBox
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.grpReportView.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.grdAllRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpExpClp.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnGenerateReport)
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
        Me.Panel2.Size = New System.Drawing.Size(1013, 50)
        Me.Panel2.TabIndex = 7
        '
        'btnGenerateReport
        '
        Me.btnGenerateReport.Location = New System.Drawing.Point(845, 0)
        Me.btnGenerateReport.Name = "btnGenerateReport"
        Me.btnGenerateReport.Size = New System.Drawing.Size(113, 48)
        Me.btnGenerateReport.TabIndex = 10
        Me.btnGenerateReport.Tag = ""
        Me.btnGenerateReport.Text = " Generate Report"
        Me.ToolTip1.SetToolTip(Me.btnGenerateReport, "Generate Report")
        Me.btnGenerateReport.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(964, 0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 48)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Tag = "HideText"
        Me.btnExit.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnExit, "Exit (Ctrl + X)")
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
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Location = New System.Drawing.Point(12, 11)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1000, 595)
        Me.Panel1.TabIndex = 6
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.grpReportView)
        Me.Panel3.Controls.Add(Me.chkOtherVoucher)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.cboShop)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.lblCompany)
        Me.Panel3.Controls.Add(Me.cmbCostCenter)
        Me.Panel3.Controls.Add(Me.cmbCompany)
        Me.Panel3.Controls.Add(Me.cmbFinancialYear)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.GroupBox9)
        Me.Panel3.Controls.Add(Me.grpExpClp)
        Me.Panel3.Location = New System.Drawing.Point(14, 34)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(973, 151)
        Me.Panel3.TabIndex = 16
        '
        'grpReportView
        '
        Me.grpReportView.Controls.Add(Me.rdoDetail)
        Me.grpReportView.Controls.Add(Me.rdoSummary)
        Me.grpReportView.Location = New System.Drawing.Point(346, 10)
        Me.grpReportView.Name = "grpReportView"
        Me.grpReportView.Size = New System.Drawing.Size(200, 70)
        Me.grpReportView.TabIndex = 22
        Me.grpReportView.TabStop = False
        Me.grpReportView.Text = "Report View"
        '
        'rdoDetail
        '
        Me.rdoDetail.AutoSize = True
        Me.rdoDetail.Location = New System.Drawing.Point(20, 43)
        Me.rdoDetail.Name = "rdoDetail"
        Me.rdoDetail.Size = New System.Drawing.Size(52, 17)
        Me.rdoDetail.TabIndex = 1
        Me.rdoDetail.Text = "Detail"
        Me.rdoDetail.UseVisualStyleBackColor = True
        '
        'rdoSummary
        '
        Me.rdoSummary.AutoSize = True
        Me.rdoSummary.Checked = True
        Me.rdoSummary.Location = New System.Drawing.Point(20, 19)
        Me.rdoSummary.Name = "rdoSummary"
        Me.rdoSummary.Size = New System.Drawing.Size(68, 17)
        Me.rdoSummary.TabIndex = 0
        Me.rdoSummary.TabStop = True
        Me.rdoSummary.Text = "Summary"
        Me.rdoSummary.UseVisualStyleBackColor = True
        '
        'chkOtherVoucher
        '
        Me.chkOtherVoucher.AutoSize = True
        Me.chkOtherVoucher.Location = New System.Drawing.Point(310, 128)
        Me.chkOtherVoucher.Name = "chkOtherVoucher"
        Me.chkOtherVoucher.Size = New System.Drawing.Size(138, 17)
        Me.chkOtherVoucher.TabIndex = 21
        Me.chkOtherVoucher.Text = "Include Other Vouchers"
        Me.ToolTip1.SetToolTip(Me.chkOtherVoucher, "Include Other Vouchers")
        Me.chkOtherVoucher.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 85)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Shop"
        '
        'cboShop
        '
        Me.cboShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShop.FormattingEnabled = True
        Me.cboShop.Location = New System.Drawing.Point(121, 85)
        Me.cboShop.Name = "cboShop"
        Me.cboShop.Size = New System.Drawing.Size(183, 21)
        Me.cboShop.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.cboShop, "Shop")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Cost Center"
        '
        'lblCompany
        '
        Me.lblCompany.AutoSize = True
        Me.lblCompany.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany.Location = New System.Drawing.Point(11, 34)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(67, 13)
        Me.lblCompany.TabIndex = 18
        Me.lblCompany.Text = "Company"
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCostCenter.FormattingEnabled = True
        Me.cmbCostCenter.Location = New System.Drawing.Point(121, 59)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(183, 21)
        Me.cmbCostCenter.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.cmbCostCenter, "Cost Center")
        '
        'cmbCompany
        '
        Me.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Location = New System.Drawing.Point(121, 34)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(183, 21)
        Me.cmbCompany.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.cmbCompany, "Company")
        '
        'cmbFinancialYear
        '
        Me.cmbFinancialYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFinancialYear.FormattingEnabled = True
        Me.cmbFinancialYear.Location = New System.Drawing.Point(121, 10)
        Me.cmbFinancialYear.Name = "cmbFinancialYear"
        Me.cmbFinancialYear.Size = New System.Drawing.Size(183, 21)
        Me.cmbFinancialYear.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.cmbFinancialYear, "Financial Year")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Financial Year"
        '
        'GroupBox9
        '
        Me.GroupBox9.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox9.Controls.Add(Me.dtToDate)
        Me.GroupBox9.Controls.Add(Me.Label2)
        Me.GroupBox9.Controls.Add(Me.dtFromDate)
        Me.GroupBox9.Location = New System.Drawing.Point(14, 104)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(290, 43)
        Me.GroupBox9.TabIndex = 15
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Date Range"
        '
        'dtToDate
        '
        Me.dtToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtToDate.Location = New System.Drawing.Point(155, 17)
        Me.dtToDate.Name = "dtToDate"
        Me.dtToDate.Size = New System.Drawing.Size(118, 20)
        Me.dtToDate.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.dtToDate, "To Date")
        Me.dtToDate.Value = New Date(2009, 1, 6, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(136, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(13, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "_"
        '
        'dtFromDate
        '
        Me.dtFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFromDate.Location = New System.Drawing.Point(6, 17)
        Me.dtFromDate.Name = "dtFromDate"
        Me.dtFromDate.Size = New System.Drawing.Size(124, 20)
        Me.dtFromDate.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.dtFromDate, "From Date")
        Me.dtFromDate.Value = New Date(2009, 1, 6, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AccessibleDescription = "Title"
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label5.Location = New System.Drawing.Point(16, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(361, 24)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Month Wise Profit and Loss Statement"
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.UiCtrlGridBar1)
        Me.Panel5.Controls.Add(Me.grdAllRecords)
        Me.Panel5.Location = New System.Drawing.Point(14, 191)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(973, 395)
        Me.Panel5.TabIndex = 5
        '
        'UiCtrlGridBar1
        '
        Me.UiCtrlGridBar1.Location = New System.Drawing.Point(14, 6)
        Me.UiCtrlGridBar1.MyGrid = Me.grdAllRecords
        Me.UiCtrlGridBar1.Name = "UiCtrlGridBar1"
        Me.UiCtrlGridBar1.Size = New System.Drawing.Size(942, 25)
        Me.UiCtrlGridBar1.TabIndex = 4
        Me.UiCtrlGridBar1.TabStop = False
        '
        'grdAllRecords
        '
        Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAllRecords.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdAllRecords.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAllRecords.Location = New System.Drawing.Point(14, 37)
        Me.grdAllRecords.Name = "grdAllRecords"
        Me.grdAllRecords.Size = New System.Drawing.Size(942, 353)
        Me.grdAllRecords.TabIndex = 0
        '
        'btnExp1stLvl
        '
        Me.btnExp1stLvl.Location = New System.Drawing.Point(6, 19)
        Me.btnExp1stLvl.Name = "btnExp1stLvl"
        Me.btnExp1stLvl.Size = New System.Drawing.Size(96, 38)
        Me.btnExp1stLvl.TabIndex = 23
        Me.btnExp1stLvl.Text = "Expand/Collapse 1st Level"
        Me.btnExp1stLvl.UseVisualStyleBackColor = True
        '
        'btnExp2ndLvl
        '
        Me.btnExp2ndLvl.Location = New System.Drawing.Point(108, 19)
        Me.btnExp2ndLvl.Name = "btnExp2ndLvl"
        Me.btnExp2ndLvl.Size = New System.Drawing.Size(96, 38)
        Me.btnExp2ndLvl.TabIndex = 24
        Me.btnExp2ndLvl.Text = "Expand/Collapse 2nd Level"
        Me.btnExp2ndLvl.UseVisualStyleBackColor = True
        '
        'btnExp3rdLvl
        '
        Me.btnExp3rdLvl.Location = New System.Drawing.Point(6, 62)
        Me.btnExp3rdLvl.Name = "btnExp3rdLvl"
        Me.btnExp3rdLvl.Size = New System.Drawing.Size(96, 38)
        Me.btnExp3rdLvl.TabIndex = 25
        Me.btnExp3rdLvl.Text = "Expand/Collapse 3rd Level"
        Me.btnExp3rdLvl.UseVisualStyleBackColor = True
        '
        'grpExpClp
        '
        Me.grpExpClp.Controls.Add(Me.btnExp3rdLvl)
        Me.grpExpClp.Controls.Add(Me.btnExp1stLvl)
        Me.grpExpClp.Controls.Add(Me.btnExp2ndLvl)
        Me.grpExpClp.Location = New System.Drawing.Point(562, 10)
        Me.grpExpClp.Name = "grpExpClp"
        Me.grpExpClp.Size = New System.Drawing.Size(211, 109)
        Me.grpExpClp.TabIndex = 26
        Me.grpExpClp.TabStop = False
        Me.grpExpClp.Text = "Expand/Collapse"
        Me.grpExpClp.Visible = False
        '
        'frmProfitAndLossMonthWise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1022, 740)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "frmProfitAndLossMonthWise"
        Me.Text = "Profit And Loss Month Wise Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.grpReportView.ResumeLayout(False)
        Me.grpReportView.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        CType(Me.grdAllRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpExpClp.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents UiCtrlGridBar1 As GLNet.uiCtrlGridBar
    Friend WithEvents grdAllRecords As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnGenerateReport As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboShop As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents cmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFinancialYear As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents dtToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkOtherVoucher As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents grpReportView As System.Windows.Forms.GroupBox
    Friend WithEvents rdoDetail As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSummary As System.Windows.Forms.RadioButton
    Friend WithEvents btnExp2ndLvl As System.Windows.Forms.Button
    Friend WithEvents btnExp1stLvl As System.Windows.Forms.Button
    Friend WithEvents btnExp3rdLvl As System.Windows.Forms.Button
    Friend WithEvents grpExpClp As System.Windows.Forms.GroupBox
End Class
