<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChartOfAccount
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
        Me.Button1 = New System.Windows.Forms.Button
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.grdGlNotes = New Janus.Windows.GridEX.GridEX
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdGlNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(605, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Chart Of Account .. "
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GridEX1
        '
        Me.GridEX1.Location = New System.Drawing.Point(0, 0)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(400, 376)
        Me.GridEX1.TabIndex = 0
        '
        'grdGlNotes
        '
        Me.grdGlNotes.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdGlNotes.EmptyRows = True
        Me.grdGlNotes.Location = New System.Drawing.Point(15, 86)
        Me.grdGlNotes.Name = "grdGlNotes"
        Me.grdGlNotes.Size = New System.Drawing.Size(602, 488)
        Me.grdGlNotes.TabIndex = 1
        Me.grdGlNotes.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdGlNotes.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'frmChartOfAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(629, 586)
        Me.Controls.Add(Me.grdGlNotes)
        Me.Controls.Add(Me.Button1)
        Me.Name = "frmChartOfAccount"
        Me.Text = "GL Notes .. "
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdGlNotes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdGlNotes As Janus.Windows.GridEX.GridEX
End Class
