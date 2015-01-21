<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGLChequePopUp
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
        Me.txtDate = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtPAY = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtRupees = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPKR = New System.Windows.Forms.TextBox
        Me.ChkCross = New System.Windows.Forms.CheckBox
        Me.LblBankType = New System.Windows.Forms.Label
        Me.btnOK = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'txtDate
        '
        Me.txtDate.Enabled = False
        Me.txtDate.Location = New System.Drawing.Point(564, 34)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.ReadOnly = True
        Me.txtDate.Size = New System.Drawing.Size(131, 20)
        Me.txtDate.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(513, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "DATE"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "PAY"
        '
        'txtPAY
        '
        Me.txtPAY.Location = New System.Drawing.Point(76, 62)
        Me.txtPAY.MaxLength = 450
        Me.txtPAY.Multiline = True
        Me.txtPAY.Name = "txtPAY"
        Me.txtPAY.Size = New System.Drawing.Size(421, 20)
        Me.txtPAY.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "RUPEES"
        '
        'txtRupees
        '
        Me.txtRupees.Enabled = False
        Me.txtRupees.Location = New System.Drawing.Point(76, 97)
        Me.txtRupees.Name = "txtRupees"
        Me.txtRupees.ReadOnly = True
        Me.txtRupees.Size = New System.Drawing.Size(421, 20)
        Me.txtRupees.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(514, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "PKR"
        '
        'txtPKR
        '
        Me.txtPKR.Enabled = False
        Me.txtPKR.Location = New System.Drawing.Point(563, 98)
        Me.txtPKR.Name = "txtPKR"
        Me.txtPKR.ReadOnly = True
        Me.txtPKR.Size = New System.Drawing.Size(132, 20)
        Me.txtPKR.TabIndex = 6
        '
        'ChkCross
        '
        Me.ChkCross.AutoSize = True
        Me.ChkCross.Location = New System.Drawing.Point(23, 14)
        Me.ChkCross.Name = "ChkCross"
        Me.ChkCross.Size = New System.Drawing.Size(63, 17)
        Me.ChkCross.TabIndex = 2
        Me.ChkCross.Text = "CROSS"
        Me.ChkCross.UseVisualStyleBackColor = True
        '
        'LblBankType
        '
        Me.LblBankType.AutoSize = True
        Me.LblBankType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBankType.Location = New System.Drawing.Point(111, 9)
        Me.LblBankType.Name = "LblBankType"
        Me.LblBankType.Size = New System.Drawing.Size(100, 16)
        Me.LblBankType.TabIndex = 9
        Me.LblBankType.Text = "LblBankType"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(564, 126)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(57, 38)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(637, 126)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(55, 38)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmGLChequePopUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(706, 175)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.LblBankType)
        Me.Controls.Add(Me.ChkCross)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtPKR)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtRupees)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPAY)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDate)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGLChequePopUp"
        Me.Text = "Cheque"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtDate As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPAY As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtRupees As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPKR As System.Windows.Forms.TextBox
    Friend WithEvents ChkCross As System.Windows.Forms.CheckBox
    Friend WithEvents LblBankType As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
