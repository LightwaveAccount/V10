<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefCompany
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grdAllRecords = New Janus.Windows.GridEX.GridEX
        Me.UiCtrlGridBar1 = New GLNet.uiCtrlGridBar
        Me.txtSortOrder = New System.Windows.Forms.TextBox
        Me.lblSortOrder = New System.Windows.Forms.Label
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.lblComments = New System.Windows.Forms.Label
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.lblCompanyAddress = New System.Windows.Forms.Label
        Me.txtCompanyURL = New System.Windows.Forms.TextBox
        Me.lblCompanyURL = New System.Windows.Forms.Label
        Me.txtCompanyFax = New System.Windows.Forms.TextBox
        Me.lblCompanyFax = New System.Windows.Forms.Label
        Me.txtCompanyPhone = New System.Windows.Forms.TextBox
        Me.lblCompanyPhone = New System.Windows.Forms.Label
        Me.txtCompanyName = New System.Windows.Forms.TextBox
        Me.lblCompanyName = New System.Windows.Forms.Label
        Me.txtCompanyCode = New System.Windows.Forms.TextBox
        Me.lblCompanyCode = New System.Windows.Forms.Label
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
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lblScreentext = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdAllRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdAllRecords)
        Me.GroupBox2.Controls.Add(Me.UiCtrlGridBar1)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox2.Location = New System.Drawing.Point(3, 184)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1001, 409)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'grdAllRecords
        '
        Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAllRecords.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdAllRecords.EmptyRows = True
        Me.grdAllRecords.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAllRecords.GroupByBoxVisible = False
        Me.grdAllRecords.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdAllRecords.Location = New System.Drawing.Point(6, 40)
        Me.grdAllRecords.Name = "grdAllRecords"
        Me.grdAllRecords.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAllRecords.Size = New System.Drawing.Size(992, 364)
        Me.grdAllRecords.TabIndex = 0
        Me.grdAllRecords.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        '
        'UiCtrlGridBar1
        '
        Me.UiCtrlGridBar1.Location = New System.Drawing.Point(4, 10)
        Me.UiCtrlGridBar1.MyGrid = Me.grdAllRecords
        Me.UiCtrlGridBar1.Name = "UiCtrlGridBar1"
        Me.UiCtrlGridBar1.Size = New System.Drawing.Size(994, 25)
        Me.UiCtrlGridBar1.TabIndex = 3
        Me.UiCtrlGridBar1.TabStop = False
        '
        'txtSortOrder
        '
        Me.txtSortOrder.Location = New System.Drawing.Point(454, 5)
        Me.txtSortOrder.MaxLength = 2
        Me.txtSortOrder.Name = "txtSortOrder"
        Me.txtSortOrder.Size = New System.Drawing.Size(90, 20)
        Me.txtSortOrder.TabIndex = 10
        Me.txtSortOrder.Tag = ""
        '
        'lblSortOrder
        '
        Me.lblSortOrder.Location = New System.Drawing.Point(376, 7)
        Me.lblSortOrder.Name = "lblSortOrder"
        Me.lblSortOrder.Size = New System.Drawing.Size(73, 16)
        Me.lblSortOrder.TabIndex = 9
        Me.lblSortOrder.Text = "Sort Order"
        '
        'txtComments
        '
        Me.txtComments.Location = New System.Drawing.Point(454, 88)
        Me.txtComments.MaxLength = 250
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(356, 49)
        Me.txtComments.TabIndex = 14
        Me.txtComments.Tag = ""
        '
        'lblComments
        '
        Me.lblComments.Location = New System.Drawing.Point(376, 93)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(73, 16)
        Me.lblComments.TabIndex = 13
        Me.lblComments.Text = "Comments"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(454, 34)
        Me.txtAddress.MaxLength = 100
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(356, 46)
        Me.txtAddress.TabIndex = 12
        Me.txtAddress.Tag = ""
        '
        'lblCompanyAddress
        '
        Me.lblCompanyAddress.Location = New System.Drawing.Point(376, 36)
        Me.lblCompanyAddress.Name = "lblCompanyAddress"
        Me.lblCompanyAddress.Size = New System.Drawing.Size(73, 16)
        Me.lblCompanyAddress.TabIndex = 11
        Me.lblCompanyAddress.Text = "Address"
        '
        'txtCompanyURL
        '
        Me.txtCompanyURL.Location = New System.Drawing.Point(118, 112)
        Me.txtCompanyURL.MaxLength = 100
        Me.txtCompanyURL.Name = "txtCompanyURL"
        Me.txtCompanyURL.Size = New System.Drawing.Size(230, 20)
        Me.txtCompanyURL.TabIndex = 8
        Me.txtCompanyURL.Tag = ""
        '
        'lblCompanyURL
        '
        Me.lblCompanyURL.Location = New System.Drawing.Point(16, 114)
        Me.lblCompanyURL.Name = "lblCompanyURL"
        Me.lblCompanyURL.Size = New System.Drawing.Size(94, 15)
        Me.lblCompanyURL.TabIndex = 7
        Me.lblCompanyURL.Text = "URL"
        '
        'txtCompanyFax
        '
        Me.txtCompanyFax.Location = New System.Drawing.Point(118, 86)
        Me.txtCompanyFax.MaxLength = 25
        Me.txtCompanyFax.Name = "txtCompanyFax"
        Me.txtCompanyFax.Size = New System.Drawing.Size(230, 20)
        Me.txtCompanyFax.TabIndex = 7
        Me.txtCompanyFax.Tag = ""
        '
        'lblCompanyFax
        '
        Me.lblCompanyFax.Location = New System.Drawing.Point(16, 88)
        Me.lblCompanyFax.Name = "lblCompanyFax"
        Me.lblCompanyFax.Size = New System.Drawing.Size(94, 15)
        Me.lblCompanyFax.TabIndex = 6
        Me.lblCompanyFax.Text = "Fax"
        '
        'txtCompanyPhone
        '
        Me.txtCompanyPhone.Location = New System.Drawing.Point(118, 60)
        Me.txtCompanyPhone.MaxLength = 25
        Me.txtCompanyPhone.Name = "txtCompanyPhone"
        Me.txtCompanyPhone.Size = New System.Drawing.Size(230, 20)
        Me.txtCompanyPhone.TabIndex = 5
        Me.txtCompanyPhone.Tag = ""
        '
        'lblCompanyPhone
        '
        Me.lblCompanyPhone.Location = New System.Drawing.Point(16, 62)
        Me.lblCompanyPhone.Name = "lblCompanyPhone"
        Me.lblCompanyPhone.Size = New System.Drawing.Size(94, 15)
        Me.lblCompanyPhone.TabIndex = 4
        Me.lblCompanyPhone.Text = "Phone"
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Location = New System.Drawing.Point(118, 32)
        Me.txtCompanyName.MaxLength = 50
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(230, 20)
        Me.txtCompanyName.TabIndex = 3
        Me.txtCompanyName.Tag = "IsRequired"
        '
        'lblCompanyName
        '
        Me.lblCompanyName.Location = New System.Drawing.Point(16, 32)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(94, 15)
        Me.lblCompanyName.TabIndex = 2
        Me.lblCompanyName.Text = "Company Name"
        '
        'txtCompanyCode
        '
        Me.txtCompanyCode.Location = New System.Drawing.Point(118, 5)
        Me.txtCompanyCode.MaxLength = 2
        Me.txtCompanyCode.Name = "txtCompanyCode"
        Me.txtCompanyCode.Size = New System.Drawing.Size(230, 20)
        Me.txtCompanyCode.TabIndex = 1
        Me.txtCompanyCode.Tag = "IsRequired"
        '
        'lblCompanyCode
        '
        Me.lblCompanyCode.Location = New System.Drawing.Point(16, 8)
        Me.lblCompanyCode.Name = "lblCompanyCode"
        Me.lblCompanyCode.Size = New System.Drawing.Size(94, 15)
        Me.lblCompanyCode.TabIndex = 0
        Me.lblCompanyCode.Text = "Company Code"
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
        Me.Panel1.Location = New System.Drawing.Point(-2, 607)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1024, 50)
        Me.Panel1.TabIndex = 6
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(965, 0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 48)
        Me.btnExit.TabIndex = 5
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
        Me.btnFirst.TabStop = False
        Me.btnFirst.Tag = "HideText"
        Me.btnFirst.Text = " "
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(565, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(48, 48)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Tag = "HideText"
        Me.btnCancel.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnCancel, "Cancel (Ctrl+E)")
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.Location = New System.Drawing.Point(66, 0)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(48, 48)
        Me.btnPrevious.TabIndex = 1
        Me.btnPrevious.TabStop = False
        Me.btnPrevious.Tag = "HideText"
        Me.btnPrevious.Text = " "
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(511, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(48, 48)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Tag = "HideText"
        Me.btnDelete.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete (Ctrl+D)")
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(120, 0)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(48, 48)
        Me.btnNext.TabIndex = 2
        Me.btnNext.TabStop = False
        Me.btnNext.Tag = "HideText"
        Me.btnNext.Text = " "
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(457, 0)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(48, 48)
        Me.btnUpdate.TabIndex = 2
        Me.btnUpdate.Tag = "HideText"
        Me.btnUpdate.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnUpdate, "Update (Ctrl+U)")
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.Location = New System.Drawing.Point(174, 0)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(48, 48)
        Me.btnLast.TabIndex = 3
        Me.btnLast.TabStop = False
        Me.btnLast.Tag = "HideText"
        Me.btnLast.Text = " "
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(403, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(48, 48)
        Me.btnSave.TabIndex = 0
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
        Me.btnNew.TabIndex = 1
        Me.btnNew.Tag = "HideText"
        Me.btnNew.Text = " "
        Me.ToolTip1.SetToolTip(Me.btnNew, "New (Ctrl+N)")
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.lblScreentext)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Location = New System.Drawing.Point(6, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1010, 599)
        Me.Panel2.TabIndex = 7
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.txtSortOrder)
        Me.Panel3.Controls.Add(Me.txtCompanyCode)
        Me.Panel3.Controls.Add(Me.lblSortOrder)
        Me.Panel3.Controls.Add(Me.lblCompanyCode)
        Me.Panel3.Controls.Add(Me.txtComments)
        Me.Panel3.Controls.Add(Me.lblCompanyName)
        Me.Panel3.Controls.Add(Me.lblComments)
        Me.Panel3.Controls.Add(Me.txtCompanyName)
        Me.Panel3.Controls.Add(Me.txtAddress)
        Me.Panel3.Controls.Add(Me.lblCompanyAddress)
        Me.Panel3.Controls.Add(Me.lblCompanyPhone)
        Me.Panel3.Controls.Add(Me.txtCompanyPhone)
        Me.Panel3.Controls.Add(Me.txtCompanyURL)
        Me.Panel3.Controls.Add(Me.lblCompanyFax)
        Me.Panel3.Controls.Add(Me.lblCompanyURL)
        Me.Panel3.Controls.Add(Me.txtCompanyFax)
        Me.Panel3.Location = New System.Drawing.Point(5, 30)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(998, 154)
        Me.Panel3.TabIndex = 18
        '
        'lblScreentext
        '
        Me.lblScreentext.AccessibleDescription = "Title"
        Me.lblScreentext.AutoSize = True
        Me.lblScreentext.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblScreentext.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblScreentext.Location = New System.Drawing.Point(5, 3)
        Me.lblScreentext.Name = "lblScreentext"
        Me.lblScreentext.Size = New System.Drawing.Size(191, 24)
        Me.lblScreentext.TabIndex = 17
        Me.lblScreentext.Text = "Company Definition"
        '
        'frmDefCompany
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 746)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.Name = "frmDefCompany"
        Me.Text = "Company Defination"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdAllRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdAllRecords As Janus.Windows.GridEX.GridEX
    Friend WithEvents UiCtrlGridBar1 As GLNet.uiCtrlGridBar
    Friend WithEvents txtCompanyCode As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyCode As System.Windows.Forms.Label
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
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents lblComments As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyAddress As System.Windows.Forms.Label
    Friend WithEvents txtCompanyURL As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyURL As System.Windows.Forms.Label
    Friend WithEvents txtCompanyFax As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyFax As System.Windows.Forms.Label
    Friend WithEvents txtCompanyPhone As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyPhone As System.Windows.Forms.Label
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyName As System.Windows.Forms.Label
    Friend WithEvents txtSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents lblSortOrder As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblScreentext As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
