<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProductRegisteration
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.CtrlRegisteration1 = New LsUserControls.CtrlRegisteration
        Me.grpLiceseInfo = New System.Windows.Forms.GroupBox
        Me.txtShops = New System.Windows.Forms.TextBox
        Me.txtType = New System.Windows.Forms.TextBox
        Me.lblShop = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        Me.grpLiceseInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(381, 78)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(170, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(78, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Activate"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(88, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(78, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'CtrlRegisteration1
        '
        Me.CtrlRegisteration1.Location = New System.Drawing.Point(14, 12)
        Me.CtrlRegisteration1.Name = "CtrlRegisteration1"
        Me.CtrlRegisteration1.Size = New System.Drawing.Size(538, 57)
        Me.CtrlRegisteration1.TabIndex = 1
        '
        'grpLiceseInfo
        '
        Me.grpLiceseInfo.Controls.Add(Me.txtShops)
        Me.grpLiceseInfo.Controls.Add(Me.txtType)
        Me.grpLiceseInfo.Controls.Add(Me.lblShop)
        Me.grpLiceseInfo.Controls.Add(Me.Label1)
        Me.grpLiceseInfo.Location = New System.Drawing.Point(14, 71)
        Me.grpLiceseInfo.Name = "grpLiceseInfo"
        Me.grpLiceseInfo.Size = New System.Drawing.Size(538, 40)
        Me.grpLiceseInfo.TabIndex = 2
        Me.grpLiceseInfo.TabStop = False
        Me.grpLiceseInfo.Text = "License Informations"
        Me.grpLiceseInfo.Visible = False
        '
        'txtShops
        '
        Me.txtShops.Location = New System.Drawing.Point(420, 16)
        Me.txtShops.Name = "txtShops"
        Me.txtShops.ReadOnly = True
        Me.txtShops.Size = New System.Drawing.Size(112, 20)
        Me.txtShops.TabIndex = 1
        Me.txtShops.Tag = "ReadOnly"
        Me.txtShops.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtType
        '
        Me.txtType.Location = New System.Drawing.Point(116, 16)
        Me.txtType.Name = "txtType"
        Me.txtType.ReadOnly = True
        Me.txtType.Size = New System.Drawing.Size(197, 20)
        Me.txtType.TabIndex = 1
        Me.txtType.Tag = "ReadOnly"
        Me.txtType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblShop
        '
        Me.lblShop.AutoSize = True
        Me.lblShop.Location = New System.Drawing.Point(332, 20)
        Me.lblShop.Name = "lblShop"
        Me.lblShop.Size = New System.Drawing.Size(65, 13)
        Me.lblShop.TabIndex = 0
        Me.lblShop.Text = "Terminals:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "License Type:"
        '
        'frmProductRegisteration
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(566, 119)
        Me.Controls.Add(Me.grpLiceseInfo)
        Me.Controls.Add(Me.CtrlRegisteration1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProductRegisteration"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Activate product online"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.grpLiceseInfo.ResumeLayout(False)
        Me.grpLiceseInfo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents CtrlRegisteration1 As LsUserControls.CtrlRegisteration
    Friend WithEvents grpLiceseInfo As System.Windows.Forms.GroupBox
    Friend WithEvents txtType As System.Windows.Forms.TextBox
    Friend WithEvents lblShop As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtShops As System.Windows.Forms.TextBox

End Class
