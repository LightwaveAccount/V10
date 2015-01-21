<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptPostDatedCheques
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
        Me.chkIncludeUnPostedVouchers = New System.Windows.Forms.CheckBox
        Me.grpBoxVoucherTypes = New System.Windows.Forms.GroupBox
        Me.optBoth = New System.Windows.Forms.RadioButton
        Me.optOtherVouchers = New System.Windows.Forms.RadioButton
        Me.optSTDVouchers = New System.Windows.Forms.RadioButton
        Me.grpVouchers = New System.Windows.Forms.GroupBox
        Me.optBRV = New System.Windows.Forms.RadioButton
        Me.optBPV = New System.Windows.Forms.RadioButton
        Me.cboBank = New System.Windows.Forms.ComboBox
        Me.lblBankName = New System.Windows.Forms.Label
        Me.cboCompany = New System.Windows.Forms.ComboBox
        Me.lblCompanyName = New System.Windows.Forms.Label
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
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker
        Me.lblFrom = New System.Windows.Forms.Label
        Me.lblTo = New System.Windows.Forms.Label
        Me.dtpTo = New System.Windows.Forms.DateTimePicker
        Me.grpBoxVoucherTypes.SuspendLayout()
        Me.grpVouchers.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.lblTitle.TabIndex = 11
        Me.lblTitle.Text = "Post Dated Cheques"
        '
        'chkIncludeUnPostedVouchers
        '
        Me.chkIncludeUnPostedVouchers.AutoSize = True
        Me.chkIncludeUnPostedVouchers.Location = New System.Drawing.Point(114, 235)
        Me.chkIncludeUnPostedVouchers.Name = "chkIncludeUnPostedVouchers"
        Me.chkIncludeUnPostedVouchers.Size = New System.Drawing.Size(159, 17)
        Me.chkIncludeUnPostedVouchers.TabIndex = 6
        Me.chkIncludeUnPostedVouchers.Text = "Include UnPosted Vouchers"
        Me.chkIncludeUnPostedVouchers.UseVisualStyleBackColor = True
        '
        'grpBoxVoucherTypes
        '
        Me.grpBoxVoucherTypes.Controls.Add(Me.optBoth)
        Me.grpBoxVoucherTypes.Controls.Add(Me.optOtherVouchers)
        Me.grpBoxVoucherTypes.Controls.Add(Me.optSTDVouchers)
        Me.grpBoxVoucherTypes.Location = New System.Drawing.Point(108, 177)
        Me.grpBoxVoucherTypes.Name = "grpBoxVoucherTypes"
        Me.grpBoxVoucherTypes.Size = New System.Drawing.Size(268, 50)
        Me.grpBoxVoucherTypes.TabIndex = 5
        Me.grpBoxVoucherTypes.TabStop = False
        '
        'optBoth
        '
        Me.optBoth.AutoSize = True
        Me.optBoth.Checked = True
        Me.optBoth.Location = New System.Drawing.Point(214, 19)
        Me.optBoth.Name = "optBoth"
        Me.optBoth.Size = New System.Drawing.Size(47, 17)
        Me.optBoth.TabIndex = 4
        Me.optBoth.TabStop = True
        Me.optBoth.Text = "Both"
        Me.optBoth.UseVisualStyleBackColor = True
        '
        'optOtherVouchers
        '
        Me.optOtherVouchers.AutoSize = True
        Me.optOtherVouchers.Location = New System.Drawing.Point(105, 19)
        Me.optOtherVouchers.Name = "optOtherVouchers"
        Me.optOtherVouchers.Size = New System.Drawing.Size(99, 17)
        Me.optOtherVouchers.TabIndex = 3
        Me.optOtherVouchers.TabStop = True
        Me.optOtherVouchers.Text = "Other Vouchers"
        Me.optOtherVouchers.UseVisualStyleBackColor = True
        '
        'optSTDVouchers
        '
        Me.optSTDVouchers.AutoSize = True
        Me.optSTDVouchers.Location = New System.Drawing.Point(6, 19)
        Me.optSTDVouchers.Name = "optSTDVouchers"
        Me.optSTDVouchers.Size = New System.Drawing.Size(89, 17)
        Me.optSTDVouchers.TabIndex = 2
        Me.optSTDVouchers.TabStop = True
        Me.optSTDVouchers.Text = "Std Vouchers"
        Me.optSTDVouchers.UseVisualStyleBackColor = True
        '
        'grpVouchers
        '
        Me.grpVouchers.Controls.Add(Me.optBRV)
        Me.grpVouchers.Controls.Add(Me.optBPV)
        Me.grpVouchers.Location = New System.Drawing.Point(108, 132)
        Me.grpVouchers.Name = "grpVouchers"
        Me.grpVouchers.Size = New System.Drawing.Size(268, 44)
        Me.grpVouchers.TabIndex = 4
        Me.grpVouchers.TabStop = False
        '
        'optBRV
        '
        Me.optBRV.AutoSize = True
        Me.optBRV.Location = New System.Drawing.Point(74, 15)
        Me.optBRV.Name = "optBRV"
        Me.optBRV.Size = New System.Drawing.Size(47, 17)
        Me.optBRV.TabIndex = 1
        Me.optBRV.TabStop = True
        Me.optBRV.Text = "BRV"
        Me.optBRV.UseVisualStyleBackColor = True
        '
        'optBPV
        '
        Me.optBPV.AutoSize = True
        Me.optBPV.Checked = True
        Me.optBPV.Location = New System.Drawing.Point(11, 15)
        Me.optBPV.Name = "optBPV"
        Me.optBPV.Size = New System.Drawing.Size(46, 17)
        Me.optBPV.TabIndex = 0
        Me.optBPV.TabStop = True
        Me.optBPV.Text = "BPV"
        Me.optBPV.UseVisualStyleBackColor = True
        '
        'cboBank
        '
        Me.cboBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBank.FormattingEnabled = True
        Me.cboBank.Location = New System.Drawing.Point(108, 32)
        Me.cboBank.Name = "cboBank"
        Me.cboBank.Size = New System.Drawing.Size(268, 21)
        Me.cboBank.TabIndex = 3
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = True
        Me.lblBankName.Location = New System.Drawing.Point(16, 36)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(32, 13)
        Me.lblBankName.TabIndex = 2
        Me.lblBankName.Text = "Bank"
        '
        'cboCompany
        '
        Me.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(108, 3)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(268, 21)
        Me.cboCompany.TabIndex = 1
        '
        'lblCompanyName
        '
        Me.lblCompanyName.AutoSize = True
        Me.lblCompanyName.Location = New System.Drawing.Point(16, 7)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(51, 13)
        Me.lblCompanyName.TabIndex = 0
        Me.lblCompanyName.Text = "Company"
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
        Me.Panel1.TabIndex = 13
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
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.chkIncludeUnPostedVouchers)
        Me.Panel2.Controls.Add(Me.cboCompany)
        Me.Panel2.Controls.Add(Me.grpBoxVoucherTypes)
        Me.Panel2.Controls.Add(Me.lblCompanyName)
        Me.Panel2.Controls.Add(Me.grpVouchers)
        Me.Panel2.Controls.Add(Me.lblBankName)
        Me.Panel2.Controls.Add(Me.cboBank)
        Me.Panel2.Location = New System.Drawing.Point(12, 36)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1001, 570)
        Me.Panel2.TabIndex = 14
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtpTo)
        Me.GroupBox1.Controls.Add(Me.lblTo)
        Me.GroupBox1.Controls.Add(Me.lblFrom)
        Me.GroupBox1.Controls.Add(Me.dtpFrom)
        Me.GroupBox1.Location = New System.Drawing.Point(108, 57)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(268, 73)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Range"
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(105, 19)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(121, 20)
        Me.dtpFrom.TabIndex = 0
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(11, 19)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(30, 13)
        Me.lblFrom.TabIndex = 1
        Me.lblFrom.Text = "From"
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(14, 54)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(20, 13)
        Me.lblTo.TabIndex = 2
        Me.lblTo.Text = "To"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(105, 46)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(121, 20)
        Me.dtpTo.TabIndex = 3
        '
        'frmRptPostDatedCheques
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 690)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblTitle)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRptPostDatedCheques"
        Me.Text = "Post Dated Cheques"
        Me.grpBoxVoucherTypes.ResumeLayout(False)
        Me.grpBoxVoucherTypes.PerformLayout()
        Me.grpVouchers.ResumeLayout(False)
        Me.grpVouchers.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents cboBank As System.Windows.Forms.ComboBox
    Friend WithEvents lblBankName As System.Windows.Forms.Label
    Friend WithEvents cboCompany As System.Windows.Forms.ComboBox
    Friend WithEvents lblCompanyName As System.Windows.Forms.Label
    Friend WithEvents grpBoxVoucherTypes As System.Windows.Forms.GroupBox
    Friend WithEvents optBoth As System.Windows.Forms.RadioButton
    Friend WithEvents optOtherVouchers As System.Windows.Forms.RadioButton
    Friend WithEvents optSTDVouchers As System.Windows.Forms.RadioButton
    Friend WithEvents grpVouchers As System.Windows.Forms.GroupBox
    Friend WithEvents optBRV As System.Windows.Forms.RadioButton
    Friend WithEvents optBPV As System.Windows.Forms.RadioButton
    Friend WithEvents chkIncludeUnPostedVouchers As System.Windows.Forms.CheckBox
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
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
End Class
