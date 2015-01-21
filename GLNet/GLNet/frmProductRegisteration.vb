Imports System.Windows.Forms
Imports Utility.Utility
Imports DAL
Imports Model
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// 02 Feb,2012       Abdul Jabbar         CR#173 -GL Release should varify License expiry date before installation
Public Class frmProductRegisteration

    'Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    '    'Me.ProgressBar1.Visible = True
    '    'Me.Label9.Text = "Connecting to server please wait..."
    '    'Me.DialogResult = System.Windows.Forms.DialogResult.OK
    '    'Me.Close()
    '    Me.CtrlRegisteration1.RegisterProduct()
    'End Sub

    'Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    '    'Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    '    Me.CtrlRegisteration1.CancelRegisteration()
    '    Me.Close()
    'End Sub

    'Private Sub frmProductRegisteration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    'End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim objRegisterProductDAL As New RegisterProductDAL

        Dim strFingerPrint As String
        strFingerPrint = New RegisterProductDAL().GetFingerPrint()

        With Me.CtrlRegisteration1
            If .RegisterProduct(strFingerPrint) = True Then

                Me.txtType.Text = IIf(.LicenseType.ToString = "Personal", "Single Company", "Multi Company")
                ' Me.txtPos.Text = .PosAllowed.ToString
                Me.txtShops.Text = .ShopLicense
                ' Me.txtRShops.Text = .RShopAllowed
                Me.grpLiceseInfo.Visible = True

                If .KeyStatus = LsUserControls.CtrlRegisteration.EnumKeyStatus.Active.ToString Or .KeyStatus = LsUserControls.CtrlRegisteration.EnumKeyStatus.Activated.ToString Then
                    Dim strLicenseType As String = String.Empty
                    If .LicenseType = "Personal" Then
                        strLicenseType = "False"

                    ElseIf .LicenseType = "Professional" Then
                        strLicenseType = "True"

                    ElseIf .LicenseType = "Enterprise" Then
                        strLicenseType = "True"
                    End If
                    Dim blnLicensesSet As Boolean = objRegisterProductDAL.SetNoOfLicenses(strLicenseType.ToString)
                    'objRegisterProductDAL.RegisterProduct(strFingerPrint, .FingerPrintSent)
                    objRegisterProductDAL.RegisterProduct(strFingerPrint, .FingerPrintSent, .key.ToString(), .ExpiryDate.ToString("yyyy-MM-dd"))
                End If



                'MsgBox("Registeration complete")
                'Me.Close()
            End If
        End With
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.CtrlRegisteration1.CancelRegisteration()
    End Sub

    Private Sub frmProductRegisteration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
