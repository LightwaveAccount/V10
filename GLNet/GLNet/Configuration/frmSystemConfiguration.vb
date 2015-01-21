'// 30-May-2011  Rizwan Asif CR # 141 Add Report Email Functionality
'' CR# 148  27-July-2011    Asif Kamal      Transaction > New form need of Customer information
'' CR # 164 08-dec-2011     Fatima Tajammal Lightwave Logviewer form is not working properly, need to find out and fix issues
'' CR# 165  08-Dec-2011     Asif Kamal      Voucher print replica, which will contain each account detail description
'' CR# 166  13-Dec-2011     Asif Kamal      Lightwave Issues need to be fixed.
'' CR# 167  14-Dec-2011     Fatima Tajammal Date Format for GL should be configuration based.
'' CR# 189  10-Feb-2012     Asif Kamal      Email Password should be in Encrypted form in DB
'' CR # 196  20-March-2012  Fatima Tajammal System Configuration: Shortcut keys are not working
'' CR # 252  13-june-2013   Farooq -H       GL System Configuration:Add configuration of SMS 
'' CR # 254  18-june-2013   Farooq -H       SMS implementation  
'' CR # 299  27-Mar-2013    Abdul Jabbar    Configuration of New Report: Sale Tax
Imports DAL
Imports DAL.SystemConfigurationDAL
Imports System.Collections.Specialized
Imports Utility.Utility

Public Class frmSystemConfiguration
    Implements IGeneral
    Dim SystemConfigurationModel As Model.SystemConfiguration
    Dim SystemconfigurationArray As New ArrayList
    Private mobjControlList As NameValueCollection

    Private Sub btnPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPath.Click
        Dim strCurrentPath As String = ""
        strCurrentPath = txtPath.Text

        BrowseFolder.ShowDialog()
        txtPath.Text = BrowseFolder.SelectedPath

        If txtPath.Text.Trim = "" Then txtPath.Text = strCurrentPath


    End Sub

    Private Sub frmSystemConfiguration_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'CR # 196 System Configuration: Shortcut keys are not working
        If e.Control And e.KeyCode = Keys.S Then
            If Me.Button1.Enabled = True Then btnSave_Click(Nothing, Nothing)
        ElseIf e.Control And e.KeyCode = Keys.X Then
            If Me.btnExit.Enabled = True Then Me.Close()
        End If
    End Sub

    Private Sub frmSystemConfiguration_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)

            ''Assing Images to Buttons
            Me.SetButtonImages()

            GetAllRecords()

            Call ApplySecurity(EnumDataMode.[New])

        Catch ex As Exception

        End Try


    End Sub

#Region "IGeneral Methods .. "

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try

            btnUpdate.Enabled = False
            btnDelete.Enabled = False
            btnNew.Enabled = False
            btnCancel.Enabled = False

            btnNext.Enabled = False
            btnFirst.Enabled = False
            btnLast.Enabled = False
            btnPrevious.Enabled = False

            If mobjControlList.Item("btnSave") Is Nothing Then
                Button1.Enabled = False
            Else
                Button1.Enabled = True

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            'CR# 167 
            '...................................................
            Dim dtDisplayDate As New DataTable
            dtDisplayDate.Columns.Add("DateFormat", GetType(String))

            Dim dr1 As DataRow = dtDisplayDate.NewRow
            dr1(0) = "MMM/dd/yyyy"

            Dim dr2 As DataRow = dtDisplayDate.NewRow
            dr2(0) = "MM/dd/yyyy"

            Dim dr3 As DataRow = dtDisplayDate.NewRow
            dr3(0) = "dd/MM/yyyy"

            Dim dr4 As DataRow = dtDisplayDate.NewRow
            dr4(0) = "dd/MMM/yyyy"

            dtDisplayDate.Rows.Add(dr1)
            dtDisplayDate.Rows.Add(dr2)
            dtDisplayDate.Rows.Add(dr3)
            dtDisplayDate.Rows.Add(dr4)

            Me.cmbDateDisplay.DisplayMember = "DateFormat"
            Me.cmbDateDisplay.ValueMember = "DateFormat"
            Me.cmbDateDisplay.DataSource = dtDisplayDate
            Me.cmbDateDisplay.SelectedIndex = 0
            '......................................
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

        ' Company Name .. 
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "CompanyName"
        SystemConfigurationModel.Config_Value = txtName.Text.Trim
        SystemconfigurationArray.Add(SystemConfigurationModel)


        ' Company URL ..
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "CompanyURL"
        SystemConfigurationModel.Config_Value = txtURL.Text.Trim
        SystemconfigurationArray.Add(SystemConfigurationModel)


        ' Company Phone ..
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "CompanyPhone"
        SystemConfigurationModel.Config_Value = txtPhone.Text.Trim
        SystemconfigurationArray.Add(SystemConfigurationModel)


        ' Company Fax ..
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "CompanyFax"
        SystemConfigurationModel.Config_Value = txtFax.Text.Trim
        SystemconfigurationArray.Add(SystemConfigurationModel)


        ' Company Address ..
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "CompanyAddress"
        SystemConfigurationModel.Config_Value = txtAddress.Text.Trim
        SystemconfigurationArray.Add(SystemConfigurationModel)


        ' Profit And Loss Account .. 
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "Profit_Loss_Acc_ID"
        SystemConfigurationModel.Config_Value = ctrlGL_ProfitLossAccount.GLAccountID
        SystemconfigurationArray.Add(SystemConfigurationModel)


        ' Tax Deducted By Vendor .. 
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "gl_ac_income_tax_payable"
        SystemConfigurationModel.Config_Value = ctrlGL_TaxDeductedVendor.GLAccountID
        SystemconfigurationArray.Add(SystemConfigurationModel)


        ' Tax Deducted By Customer .. 
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "gl_ac_income_tax_Receivable"
        SystemConfigurationModel.Config_Value = ctrlGL_TaxDeductedCustomer.GLAccountID
        SystemconfigurationArray.Add(SystemConfigurationModel)


        ' Voucher .. 
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "voucher_report_size"
        SystemConfigurationModel.Config_Value = IIf(optShortVoucher.Checked = True, "Short", "Long")
        SystemconfigurationArray.Add(SystemConfigurationModel)


        ' BackUp Location
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "Backup_Location"
        SystemConfigurationModel.Config_Value = txtPath.Text.Trim
        SystemconfigurationArray.Add(SystemConfigurationModel)

        ''GL Integration
        'SystemConfigurationModel = New Model.SystemConfiguration
        'SystemConfigurationModel.Config_Name = "Integration_Of_Sale_Purchase"
        'SystemConfigurationModel.Config_Value = Utility.Utility.SymmetricEncryption.Encrypt(chkIntegration.Checked, "true")
        'SystemconfigurationArray.Add(SystemConfigurationModel)

        'Other Voucher
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "Other_Voucher"
        SystemConfigurationModel.Config_Value = Utility.Utility.SymmetricEncryption.Encrypt(chkOtherVoucher.Checked, "true")
        SystemconfigurationArray.Add(SystemConfigurationModel)

        'CR#119
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "VoucherAutoSorting"
        SystemConfigurationModel.Config_Value = Me.chkAutoSortingV.Checked
        SystemconfigurationArray.Add(SystemConfigurationModel)

        'CR# 141
        ''''''''''''''''''''''''''''''''''
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "Pop3Server"
        SystemConfigurationModel.Config_Value = Me.txtMailServer.Text.Trim
        SystemconfigurationArray.Add(SystemConfigurationModel)

        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "EMailFromAddress"
        SystemConfigurationModel.Config_Value = Me.txtEmail.Text.Trim
        SystemconfigurationArray.Add(SystemConfigurationModel)

        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "MailServerUser"
        SystemConfigurationModel.Config_Value = Me.txtUserName.Text.Trim
        SystemconfigurationArray.Add(SystemConfigurationModel)

        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "MailServerPassword"
        SystemConfigurationModel.Config_Value = EncryptWithALP(Me.txtPassword.Text.Trim)    'CR # 189       Asif Kamal      Email Password should be in Encrypted form in DB
        SystemconfigurationArray.Add(SystemConfigurationModel)

        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "MailServerSSL"
        SystemConfigurationModel.Config_Value = Me.chkSSL.Checked
        SystemconfigurationArray.Add(SystemConfigurationModel)

        ''CR# 148
        '' Transaction > New form need of Customer information

        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "CustomerInfo"
        SystemConfigurationModel.Config_Value = Me.chkCustomerInfo.Checked
        SystemconfigurationArray.Add(SystemConfigurationModel)

        '''''''''''''''''''''''''''''''''''''''''''''''''''
        ''CR# 165   Asif Kamal
        '' Voucher print replica, which will contain each account detail description

        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "Desc_in_Vouchers"
        SystemConfigurationModel.Config_Value = Me.chkShowDescInVouchers.Checked
        SystemconfigurationArray.Add(SystemConfigurationModel)


        '''''''''''''''''''''''''''''''''''''''''''''''''''
        'Cr # 164
        SystemConfigurationModel.ActivityLog.ShopID = 0 'gObjUserInfo.ShopInfo.ShopID
        SystemConfigurationModel.ActivityLog.ScreenTitle = Me.Text
        SystemConfigurationModel.ActivityLog.LogGroup = "Definition"
        SystemConfigurationModel.ActivityLog.UserID = gObjUserInfo.UserID

        'CR#167
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "GL_Date_Formate"
        SystemConfigurationModel.Config_Value = Me.cmbDateDisplay.SelectedIndex
        SystemconfigurationArray.Add(SystemConfigurationModel)


        ' change by farooq-H CR# 252
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "SMSAPIUserName"
        SystemConfigurationModel.Config_Value = Me.txtSMSLogin.Text.Trim
        SystemconfigurationArray.Add(SystemConfigurationModel)

        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "SMSAPIPassword"
        SystemConfigurationModel.Config_Value = EncryptWithALP(Me.txtSMSPassword.Text.ToString)
        SystemconfigurationArray.Add(SystemConfigurationModel)

        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "BrandName"
        SystemConfigurationModel.Config_Value = Me.txtBrandName.Text.Trim
        SystemconfigurationArray.Add(SystemConfigurationModel)
        'farooq-h            CR#254
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "SendSMS"
        SystemConfigurationModel.Config_Value = Me.ChkSendSMS.Checked
        SystemconfigurationArray.Add(SystemConfigurationModel)

        'CR#299
        ' Tax Deducted By Customer .. 
        SystemConfigurationModel = New Model.SystemConfiguration
        SystemConfigurationModel.Config_Name = "gl_ac_Services_Tax"
        SystemConfigurationModel.Config_Value = ctrl_GLTaxOnServices.GLAccountID
        SystemconfigurationArray.Add(SystemConfigurationModel)

        ''Voucher Print Size
        'SystemConfigurationModel = New Model.SystemConfiguration
        'SystemConfigurationModel.Config_Name = "voucher_report_size"

        'SystemconfigurationArray.Add(SystemConfigurationModel)

        ' Adding Array List TO Model .. 
        SystemConfigurationModel.SELECTEDRECORD_ARRAYLIST = SystemconfigurationArray


    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

        Try
            txtName.Text = SystemConfigurationDAL.GetSystemConfigurationValue("CompanyName") ' Company Name .. 
            txtURL.Text = SystemConfigurationDAL.GetSystemConfigurationValue("CompanyURL") ' Company URL .. 
            txtPhone.Text = SystemConfigurationDAL.GetSystemConfigurationValue("CompanyPhone") ' Company Phone .. 
            txtFax.Text = SystemConfigurationDAL.GetSystemConfigurationValue("CompanyFax") ' Company Fax .. 
            txtAddress.Text = SystemConfigurationDAL.GetSystemConfigurationValue("CompanyAddress") ' Company Address .. 
            txtPath.Text = SystemConfigurationDAL.GetSystemConfigurationValue("Backup_Location") ' BackUp DB Path .. 

            ' Voucher Type .. 
            If SystemConfigurationDAL.GetSystemConfigurationValue("voucher_report_size").ToString = "Short" Then
                optShortVoucher.Checked = True
            Else
                optLongVoucher.Checked = True

            End If


            Dim objGLDAL As New GLAccountDAL
            Dim ObjDataTable As DataTable = objGLDAL.GetAll("", 4, "None")

            If SystemConfigurationDAL.GetSystemConfigurationValue("Profit_Loss_Acc_ID") <> "" Then
                ctrlGL_ProfitLossAccount.txtACCode.Text = SystemConfigurationDAL.GetAccountDetail(SystemConfigurationDAL.GetSystemConfigurationValue("Profit_Loss_Acc_ID"), ObjDataTable)
                ctrlGL_ProfitLossAccount.txtACName_Validating(Nothing, Nothing)
            End If

            If SystemConfigurationDAL.GetSystemConfigurationValue("gl_ac_income_tax_Receivable") <> "" Then
                ctrlGL_TaxDeductedCustomer.txtACCode.Text = SystemConfigurationDAL.GetAccountDetail(SystemConfigurationDAL.GetSystemConfigurationValue("gl_ac_income_tax_Receivable"), ObjDataTable)
                ctrlGL_TaxDeductedCustomer.txtACName_Validating(Nothing, Nothing)
            End If

            If SystemConfigurationDAL.GetSystemConfigurationValue("gl_ac_income_tax_payable") <> "" Then
                ctrlGL_TaxDeductedVendor.txtACCode.Text = SystemConfigurationDAL.GetAccountDetail(SystemConfigurationDAL.GetSystemConfigurationValue("gl_ac_income_tax_payable"), ObjDataTable)
                ctrlGL_TaxDeductedVendor.txtACName_Validating(Nothing, Nothing)
            End If

            'Dim strIsIntegrated As String = SystemConfigurationDAL.GetSystemConfigurationValue("Integration_Of_Sale_Purchase")
            'If strIsIntegrated = "" Then
            '    gblnIsIntegratedWithSalesAndPurchase = False                
            'Else
            '    gblnIsIntegratedWithSalesAndPurchase = Utility.Utility.SymmetricEncryption.Decrypt(SystemConfigurationDAL.GetSystemConfigurationValue("Integration_Of_Sale_Purchase"), "true")
            'End If

            'chkIntegration.Checked = gblnIsIntegratedWithSalesAndPurchase

            Dim strOtherVoucher As String = SystemConfigurationDAL.GetSystemConfigurationValue("Other_Voucher")
            If strOtherVoucher = "" Then
                gblnShowOtherVoucher = False
            Else
                gblnShowOtherVoucher = Utility.Utility.SymmetricEncryption.Decrypt(SystemConfigurationDAL.GetSystemConfigurationValue("Other_Voucher"), "true")
            End If

            chkOtherVoucher.Checked = gblnShowOtherVoucher

            'CR#119
            Dim strAutoVouchersort As String = SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")
            If strAutoVouchersort.Length < 1 Then
                Me.chkAutoSortingV.Checked = False
            Else
                Me.chkAutoSortingV.Checked = Convert.ToBoolean(SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting"))
            End If

            'CR#165     Asif Kamal  Voucher print replica, which will contain each account detail description
            Dim strDescInVouchers As String = SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")
            If strDescInVouchers <> "" Then
                If strDescInVouchers.Length < 1 Then
                    Me.chkShowDescInVouchers.Checked = False
                Else
                    Me.chkShowDescInVouchers.Checked = Convert.ToBoolean(SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers"))
                End If
            End If


            'CR # 141
            Try

                Me.txtMailServer.Text = SystemConfigurationDAL.GetSystemConfigurationValue("Pop3Server")
                Me.txtEmail.Text = SystemConfigurationDAL.GetSystemConfigurationValue("EMailFromAddress")
                Me.txtUserName.Text = SystemConfigurationDAL.GetSystemConfigurationValue("MailServerUser")
                Me.txtPassword.Text = DecryptWithALP(SystemConfigurationDAL.GetSystemConfigurationValue("MailServerPassword"))  'CR # 189   Asif Kamal  Email Password should be in Encrypted form in DB
                'CR#166     Lightwave Issues need to be fixed.
                Me.chkSSL.Checked = IIf(SystemConfigurationDAL.GetSystemConfigurationValue("MailServerSSL") = String.Empty, False, SystemConfigurationDAL.GetSystemConfigurationValue("MailServerSSL"))

            Catch ex As Exception

            End Try

            ''CR# 148   Transaction > New form need of Customer information
            Me.chkCustomerInfo.Checked = IIf(SystemConfigurationDAL.GetSystemConfigurationValue("CustomerInfo") = String.Empty, False, SystemConfigurationDAL.GetSystemConfigurationValue("CustomerInfo"))
            'CR # 167 
            '......................................
            ' Me.cmbDateDisplay.SelectedIndex = SystemConfigurationDAL.GetSystemConfigurationValue("GL_Date_Formate")
            '......................................
            ' change by farooq-H CR# 252
            Me.txtSMSLogin.Text = SystemConfigurationDAL.GetSystemConfigurationValue("SMSAPIUserName").ToString
            Me.txtSMSPassword.Text = DecryptWithALP(SystemConfigurationDAL.GetSystemConfigurationValue("SMSAPIPassword").ToString)
            Me.txtBrandName.Text = SystemConfigurationDAL.GetSystemConfigurationValue("BrandName").ToString
            ' change by farooq-H CR# 254
            Me.ChkSendSMS.Checked = IIf(SystemConfigurationDAL.GetSystemConfigurationValue("SendSMS") = String.Empty, False, SystemConfigurationDAL.GetSystemConfigurationValue("SendSMS"))

            'CR#299
            If SystemConfigurationDAL.GetSystemConfigurationValue("gl_ac_Services_Tax") <> "" Then
                ctrl_GLTaxOnServices.txtACCode.Text = SystemConfigurationDAL.GetAccountDetail(SystemConfigurationDAL.GetSystemConfigurationValue("gl_ac_Services_Tax"), ObjDataTable)
                ctrl_GLTaxOnServices.txtACName_Validating(Nothing, Nothing)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        
    End Sub

    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        ' Checking Company Name .. 
        If txtName.Text = "" Then
            ShowValidationMessage("Please enter Company Name")
            txtName.Focus()
            Return False
            Exit Function


            ' Checking Phone # .. 
            'ElseIf txtPhone.Text = "" Then
            '    ShowValidationMessage("Please enter Company Phone")
            '    txtPhone.Focus()
            '    Return False
            '    Exit Function


            '    ' Checking Fax # .. 
            'ElseIf txtFax.Text = "" Then
            '    ShowValidationMessage("Please enter Company Fax")
            '    txtFax.Focus()
            '    Return False
            '    Exit Function


            ' Checking Address .. 
            'ElseIf txtAddress.Text = "" Then
            '    MsgBox("Please enter Company Address")
            '    txtAddress.Focus()
            '    Return False
            '    Exit Function

        End If
        Return True

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

        Try

            If gEnumIsRightToLeft = Windows.Forms.RightToLeft.No Then
                Me.btnFirst.ImageList = gobjMyImageListForOperationBar
                Me.btnFirst.ImageKey = "First"

                Me.btnNext.ImageList = gobjMyImageListForOperationBar
                Me.btnNext.ImageKey = "Next"

                Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
                Me.btnPrevious.ImageKey = "Previous"

                Me.btnLast.ImageList = gobjMyImageListForOperationBar
                Me.btnLast.ImageKey = "Last"

            Else
                Me.btnFirst.ImageList = gobjMyImageListForOperationBar
                Me.btnFirst.ImageKey = "Last"

                Me.btnNext.ImageList = gobjMyImageListForOperationBar
                Me.btnNext.ImageKey = "Previous"

                Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
                Me.btnPrevious.ImageKey = "Next"

                Me.btnLast.ImageList = gobjMyImageListForOperationBar
                Me.btnLast.ImageKey = "First"
            End If


            Me.btnNew.ImageList = gobjMyImageListForOperationBar
            Me.btnNew.ImageKey = "New"

            Me.Button1.ImageList = gobjMyImageListForOperationBar
            Me.Button1.ImageKey = "Save"

            Me.btnUpdate.ImageList = gobjMyImageListForOperationBar
            Me.btnUpdate.ImageKey = "Update"

            Me.btnCancel.ImageList = gobjMyImageListForOperationBar
            Me.btnCancel.ImageKey = "Cancel"

            Me.btnDelete.ImageList = gobjMyImageListForOperationBar
            Me.btnDelete.ImageKey = "Delete"

            Me.btnExit.ImageList = gobjMyImageListForOperationBar
            Me.btnExit.ImageKey = "Exit"

        Catch ex As Exception
            Throw ex

        End Try

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting
    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update


        If IsValidate() Then

            FillModel()
            If New SystemConfigurationDAL().Update(SystemConfigurationModel) Then
                ShowInformationMessage("System Configuration Saved Successfully .. ")
                GetAllRecords()
                Exit Function

            End If

        End If

    End Function

#End Region


    ' Click Event Of Save Button ..
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Update1()

    End Sub

    ' Click Event Of Close Button .. 
    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub chkOtherVoucher_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOtherVoucher.CheckedChanged

    End Sub

    Private Sub chkAutoSortingV_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoSortingV.CheckedChanged

    End Sub
End Class