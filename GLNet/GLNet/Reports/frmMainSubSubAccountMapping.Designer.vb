<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainSubSubAccountMapping
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.grdBar = New GLNet.uiCtrlGridBar
        Me.grdAccountMapping = New Janus.Windows.GridEX.GridEX
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        CType(Me.grdAccountMapping, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.grdBar)
        Me.Panel1.Controls.Add(Me.grdAccountMapping)
        Me.Panel1.Location = New System.Drawing.Point(9, 31)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1004, 701)
        Me.Panel1.TabIndex = 0
        '
        'grdBar
        '
        Me.grdBar.Location = New System.Drawing.Point(14, 14)
        Me.grdBar.MyGrid = Me.grdAccountMapping
        Me.grdBar.Name = "grdBar"
        Me.grdBar.Size = New System.Drawing.Size(977, 25)
        Me.grdBar.TabIndex = 6
        '
        'grdAccountMapping
        '
        Me.grdAccountMapping.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAccountMapping.EmptyRows = True
        Me.grdAccountMapping.Location = New System.Drawing.Point(14, 45)
        Me.grdAccountMapping.Name = "grdAccountMapping"
        Me.grdAccountMapping.Size = New System.Drawing.Size(977, 639)
        Me.grdAccountMapping.TabIndex = 5
        Me.grdAccountMapping.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAccountMapping.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label5.Location = New System.Drawing.Point(11, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(379, 24)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Main Sub Sub Account Mapping Report"
        '
        'frmMainSubSubAccountMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1022, 740)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmMainSubSubAccountMapping"
        Me.Text = "Main Sub Sub Account Mapping .."
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdAccountMapping, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grdBar As GLNet.uiCtrlGridBar
    Friend WithEvents grdAccountMapping As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
