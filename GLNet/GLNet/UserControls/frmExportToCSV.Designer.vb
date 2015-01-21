<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExportToCSV
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
        Me.btnOk = New System.Windows.Forms.Button
        Me.optSelected = New System.Windows.Forms.RadioButton
        Me.chk = New System.Windows.Forms.CheckBox
        Me.optAll = New System.Windows.Forms.RadioButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lstColumns = New System.Windows.Forms.CheckedListBox
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(41, 72)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(64, 26)
        Me.btnOk.TabIndex = 7
        Me.btnOk.Text = "Ok"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'optSelected
        '
        Me.optSelected.AutoSize = True
        Me.optSelected.Location = New System.Drawing.Point(10, 36)
        Me.optSelected.Name = "optSelected"
        Me.optSelected.Size = New System.Drawing.Size(143, 17)
        Me.optSelected.TabIndex = 6
        Me.optSelected.TabStop = True
        Me.optSelected.Text = "Export Selected Columns"
        Me.optSelected.UseVisualStyleBackColor = True
        '
        'chk
        '
        Me.chk.AutoSize = True
        Me.chk.Location = New System.Drawing.Point(13, 4)
        Me.chk.Name = "chk"
        Me.chk.Size = New System.Drawing.Size(107, 17)
        Me.chk.TabIndex = 6
        Me.chk.Text = "Check/UnCheck"
        Me.chk.UseVisualStyleBackColor = True
        '
        'optAll
        '
        Me.optAll.AutoSize = True
        Me.optAll.Location = New System.Drawing.Point(10, 14)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(95, 17)
        Me.optAll.TabIndex = 5
        Me.optAll.TabStop = True
        Me.optAll.Text = "Export All Data"
        Me.optAll.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.chk)
        Me.Panel1.Controls.Add(Me.lstColumns)
        Me.Panel1.Location = New System.Drawing.Point(165, 10)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(199, 305)
        Me.Panel1.TabIndex = 4
        '
        'lstColumns
        '
        Me.lstColumns.CheckOnClick = True
        Me.lstColumns.FormattingEnabled = True
        Me.lstColumns.Location = New System.Drawing.Point(11, 25)
        Me.lstColumns.Name = "lstColumns"
        Me.lstColumns.Size = New System.Drawing.Size(174, 274)
        Me.lstColumns.TabIndex = 5
        '
        'frmExportToCSV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 372)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.optSelected)
        Me.Controls.Add(Me.optAll)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmExportToCSV"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Customize Export To CSV"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents optSelected As System.Windows.Forms.RadioButton
    Friend WithEvents chk As System.Windows.Forms.CheckBox
    Friend WithEvents optAll As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lstColumns As System.Windows.Forms.CheckedListBox
End Class
