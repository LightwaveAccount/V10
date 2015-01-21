Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports MySql.Data
Imports MySql.Data.MySqlClient

''/////////////////////////////////////////////////////////////////////////////////////////
''//                     GL CONFIGURATION
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmCustomerInfo.vb           				                            
''// Programmer	     : Asif Kamal
''// Creation Date	 : July 27, 2011
''// Description     : Customer Information Screen
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by                    Brief Description			                
''// 06-Sep-2011        Asif Kamal                    Implement the Short Keys.
''// 08-dec-2011       Fatima Tajammal                Lightwave Logviewer form is not working properly, need to find out and fix issues
''// 28-Dec-2011        Asif kamal        CR#171      Issues on Updating Record on Customer Information Form
''// 30-May-2012        Abdul Jabbar      CR#202      Customer Information Screen: Records are not filtering on Product Type (Enterprise,Lightwave)
''// 27-Jul-2012        Abdul Jabbar      CR#212      Customer Information: Only relevant records should be displayed in 'Records' screen
''\\ 30-05-2013        farooq-H           CR#242     SMS: Send sms from customer information screen. 
''// 03-dec-2013        Fatima Tajammal   Cr#286    Connection string for online portal must be encrypted
''// 28-JAN-2014        Farooq-H          Cr#292    SMS changes are required on customer screen.
''// 29-Apr-2014        Jabbar            CR#306    Changes are required on Customer information screen.
''// 08-May-2014        Jabbar            CR#307    SMS functionality issues needs to be fix
''// 12-dec-2014        Fatima            CR#346    Changes are required in Sale Tax Invoice
''//------------------------------------------------------------------------------------



Public Class frmCustomerInfo
    Implements IGeneral

#Region "Variables"
    Private mobjControlList As NameValueCollection
    Private mobjModel As CustomerInfo
    Private intPkId As Integer
    Dim timerTicks As Integer = 0
    Dim StrMobile As String = String.Empty  ' 30-05-2013       farooq-H           CR# 242
    Dim StrMobileText As String = String.Empty

#End Region

#Region "Enumerations"

    Private Enum EnumGridCustomerInfo

        CustomerInfoId
        AccountID
        AccountCode
        AccountName
        CreationDate
        MaintStartDate
        MaintEndDate
        PaymentRcvdDate
        Product
        Status
        SlaType
        MonthlyAmount
        ContactPerson
        PhoneOffice
        Mobile
        Fax
        Email
        Address
        Remarks
        Cust_NTNNumber 'CR # 346
        Cust_STRNumber 'CR # 346
        Check   ' 30-05-2013       farooq-H           CR# 242
        InvalidRecord  ''// 28-JAN-2014        Farooq-H          Cr#292 
       
    End Enum
  

#End Region

#Region "Interface Methods"

    '' To set the Button Images.
    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

        Try

            Me.btnFirst.ImageList = gobjMyImageListForOperationBar
            Me.btnFirst.ImageKey = "First"

            Me.btnNext.ImageList = gobjMyImageListForOperationBar
            Me.btnNext.ImageKey = "Next"

            Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
            Me.btnPrevious.ImageKey = "Previous"

            Me.btnLast.ImageList = gobjMyImageListForOperationBar
            Me.btnLast.ImageKey = "Last"


            Me.btnNew.ImageList = gobjMyImageListForOperationBar
            Me.btnNew.ImageKey = "New"

            Me.btnSave.ImageList = gobjMyImageListForOperationBar
            Me.btnSave.ImageKey = "Save"

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

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

        Try

            'CR#202
            Dim dtProductsVersion As DataTable
            dtProductsVersion = New DataTable
            'dtProductsVersion.Columns.Add("Id", GetType(System.Int32))
            dtProductsVersion.Columns.Add("Name", GetType(System.String))

            Dim DRProdVer As DataRow

            DRProdVer = dtProductsVersion.NewRow
            DRProdVer("Name") = "--Select--"
            dtProductsVersion.Rows.Add(DRProdVer)

            DRProdVer = dtProductsVersion.NewRow
            DRProdVer("Name") = "LightWave"
            dtProductsVersion.Rows.Add(DRProdVer)

            DRProdVer = dtProductsVersion.NewRow
            DRProdVer("Name") = "Personal"
            dtProductsVersion.Rows.Add(DRProdVer)

            DRProdVer = dtProductsVersion.NewRow
            DRProdVer("Name") = "Professional"
            dtProductsVersion.Rows.Add(DRProdVer)

            DRProdVer = dtProductsVersion.NewRow
            DRProdVer("Name") = "Enterprise"
            dtProductsVersion.Rows.Add(DRProdVer)

            Me.cmbProduct.DataSource = dtProductsVersion
            Me.cmbProduct.ValueMember = "Name"
            Me.cmbProduct.DisplayMember = "Name"
            Me.cmbProduct.SelectedIndex = 0


            Me.cmbProductType_DtlSearch.DataSource = dtProductsVersion.Copy 'Assigning same table copy to search drop down
            Me.cmbProductType_DtlSearch.ValueMember = "Name"
            Me.cmbProductType_DtlSearch.DisplayMember = "Name"
            Me.cmbProductType_DtlSearch.SelectedIndex = 0
            ''          Farooq-H
            Dim ListOfSMS As New Dictionary(Of String, String)
            ListOfSMS.Add("--Select--", "")
            'CR#306
            'ListOfSMS.Add("Before Warrenty Expire", "Dear Customer: Your warranty for ??Candela?? is going to expire on ??MaintEndDate??. Kindly make the payment for product Upgrade and services to enjoy un-interrupted support. Your quarterly payment is Rs.??MonthlyAmount??. For details please contact LumenSoft Accounts Dept.: 042 111 290 290")
            'ListOfSMS.Add("On Warrenty Expire ", "Dear Customer: Your warranty for ??Candela?? has expired on ??MaintEndDate??. Kindly make the payment for product Upgrade and services to enjoy un-interrupted support.Your quarterly payment is Rs.??MonthlyAmount??. For details please contact LumenSoft Accounts Dept.: 042 111 290 290")
            'ListOfSMS.Add("Reminder-services will not be available", "Dear Customer: It is reminder for payment of ??Candela?? product upgrade and services. Support services will not be available from ??MaintEndDate?? onwards. Kindly make payment to enjoy un-interrupted support. Your quarterly payment is Rs.??MonthlyAmount??. For details contact LumenSoft Accounts Dept.: 042 111 290 290")
            'ListOfSMS.Add("Before Maintenance Expire", "Dear Customer: Your Payment for ??Candela?? Upgrade & Services is due from ??MaintEndDate+1??. Kindly make the payment before this ??MaintEndDate?? to enjoy un-interrupted support. Your quarterly payment is Rs.??MonthlyAmount??. For details please contact LumenSoft Accounts Dept.: 042 111 290 290")
            'ListOfSMS.Add("Reminder-Maintenance will not be available", "Dear Customer: It is reminder for payment of ??Candela?? product upgrade and services. Kindly make the payment before this ??MaintEndDate?? to enjoy un-interrupted support. Your quarterly payment is Rs.??MonthlyAmount??. For details please contact LumenSoft Accounts Dept.: 042 111 290 290")
            'ListOfSMS.Add("Service/Maintenance Closure", "Dear Customer: Your services for ??Candela?? has been suspended due to non payment. Kindly make payment to enjoy un-interrupted support. Your quarterly payment is Rs.??MonthlyAmount??. For details contact LumenSoft Accounts Dept.: 042 111 290 290")

            ListOfSMS.Add("1. Before Warrenty Expire", "Dear Customer: Your warranty for ??Candela?? is going to expire on ??MaintEndDate??. Kindly make the payment for product Upgrade and services to enjoy un-interrupted support. For details please contact LumenSoft Accounts Dept.: 042 111 290 290")
            ListOfSMS.Add("2. On Warrenty Expire ", "Dear Customer: Your warranty for ??Candela?? has expired on ??MaintEndDate??. Kindly make the payment for product Upgrade and services to enjoy un-interrupted support. For details please contact LumenSoft Accounts Dept.: 042 111 290 290")
            ListOfSMS.Add("3. Reminder-services will not be available (Warranty)", "Dear Customer: Your warranty for ??Candela?? expired on ??MaintEndDate??. We will not be able to provide you Support services if payment is not received today. Kindly make payment to enjoy un-interrupted support. For details contact LumenSoft Accounts Dept.: 042 111 290 290")
            ListOfSMS.Add("1. Before Maintenance Expire", "Dear Customer: Your Payment for ??Candela?? Upgrade & Services is due from ??MaintEndDate+1??. Kindly make the payment before this date to enjoy un-interrupted support. For details please contact LumenSoft Accounts Dept.: 042 111 290 290")
            ListOfSMS.Add("2. On Maintenance Expire", "Dear Customer: Your support contract for ??Candela?? has expired on ??MaintEndDate??. Kindly make the payment for product Upgrade and services to enjoy un-interrupted support. For details please contact LumenSoft Accounts Dept.: 042 111 290 290")
            ListOfSMS.Add("3. Reminder-Maintenance will not be available", "Dear Customer: It is reminder for payment of ??Candela?? product upgrade and services. We will not be able to provide you Support services if payment is not received today. Kindly make the payment to enjoy un-interrupted support. For details please contact LumenSoft Accounts Dept.: 042 111 290 290")
            ListOfSMS.Add("4. Service/Maintenance Closure", "Dear Customer: Your services for ??Candela?? has been suspended due to non payment. Kindly make payment to enjoy un-interrupted support. For details contact LumenSoft Accounts Dept.: 042 111 290 290")

            Me.CmbSMSTemplates.DisplayMember = "Key"
            Me.CmbSMSTemplates.ValueMember = "Value"
            Me.CmbSMSTemplates.DataSource = New BindingSource(ListOfSMS, Nothing)
            Me.CmbSMSTemplates.SelectedIndex = 0


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

        Dim lngTotalRecords As Long

        Try

            ''Getting Datasource for Grid from DAL
            Dim dt As DataTable = New CustomerInfoDAL().GetAll()
            ' 30-05-2013       farooq-H           CR# 242  SMS: Send sms from customer information screen. 
            dt.Columns.Add("Check", GetType(System.Boolean))
            ''// 28-JAN-2014        Farooq-H          Cr#292  
            dt.Columns.Add("InvalidRecord", GetType(System.String))

            Me.grdAllRecords.DataSource = dt
            Me.grdAllRecords.RetrieveStructure()

            lngTotalRecords = dt.Rows.Count

            If lngTotalRecords <= 0 Then
                Me.ReSetControls()
            Else
                '' Me.grdRecords.Row = 0
            End If
            ''Applying Grid Formatting Setting
            Me.ApplyGridSettings()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try

            ''Columns In-visible

            'Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.CustomerID).Visible = False
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.CustomerInfoId).Visible = False
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.AccountID).Visible = False
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.AccountID).Width = 110
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.AccountName).Width = 150

            ''Set columns widths for visible columns
            
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.CreationDate).Width = 100
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.MaintStartDate).Width = 100
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.MaintEndDate).Width = 100
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.PaymentRcvdDate).Width = 100
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Product).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Status).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.SlaType).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.MonthlyAmount).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.MonthlyAmount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.MonthlyAmount).FormatString = IIf(gintAmountRound = 0, "###,###,##0", "###,###,##0." & New String("0", gintAmountRound))
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.MonthlyAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.ContactPerson).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.PhoneOffice).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Mobile).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Fax).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Email).Width = 80
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Address).Width = 120
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Remarks).Width = 200
            ' 30-05-2013       farooq-H           CR# 242  SMS: Send sms from customer information screen. 

            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.CustomerInfoId).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.AccountID).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.AccountName).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.CreationDate).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.MaintStartDate).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.MaintEndDate).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.PaymentRcvdDate).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Product).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Status).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.SlaType).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.MonthlyAmount).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.ContactPerson).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.PhoneOffice).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Mobile).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Fax).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Email).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Address).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Remarks).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.AccountCode).EditType = Janus.Windows.GridEX.EditType.NoEdit


            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Check).Visible = True
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Check).ColumnType = Janus.Windows.GridEX.ColumnType.CheckBox
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Check).EditType = Janus.Windows.GridEX.EditType.CheckBox
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.Check).ActAsSelector = True
       

            Me.grdAllRecords.TotalRowFormatStyle.BackColor = gobjRequiredFieldtBKColor

            
            'Stop Editing in Grid
            'Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False 
            Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdAllRecords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.MonthlyAmount).TotalFormatString = IIf(gintAmountRound = 0, "###,###,##0", "###,###,##0." & New String("0", gintAmountRound))
            ' Me.grdAllRecords.TotalRow.Format (EnumGridCustomerInfo.MonthlyAmount ,,IIf(gintAmountRound = 0, "###,###,##0", "###,###,##0." & New String("0", gintAmountRound))

            ''// 28-JAN-2014        Farooq-H          Cr#292  
            Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.InvalidRecord).Visible = False

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

        Try

            If Mode = EnumDataMode.Disabled Then
                ''if New Mode then Set Disable all Navigation Buttons
                btnFirst.Enabled = False
                btnPrevious.Enabled = False
                btnNext.Enabled = False
                btnLast.Enabled = False

            ElseIf Mode = EnumDataMode.Edit Then
                ''if New Mode then Set Enable all Navigation Buttons
                btnFirst.Enabled = True
                btnPrevious.Enabled = True
                btnNext.Enabled = True
                btnLast.Enabled = True

            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

        Try
            '' Resetting All Controls
            Me.UiCtrlGLAccount1.txtACCode.Text = String.Empty
            Me.UiCtrlGLAccount1.txtAccountName.Text = String.Empty
            Me.txtContactPerson.Text = String.Empty
            Me.txtAddress.Text = String.Empty
            Me.txtEmail.Text = String.Empty
            Me.txtFax.Text = String.Empty
            Me.txtMobile.Text = String.Empty
            Me.txtMonthlyAmount.Text = String.Empty
            Me.txtPhoneOffice.Text = String.Empty
            Me.txtRemarks.Text = String.Empty
            Me.cmbProduct.SelectedIndex = 0
            Me.cmbStatus.SelectedIndex = 0
            Me.cmbSlaType.SelectedIndex = 0
            'Me.cmbProduct.Text = String.Empty
            'Me.cmbStatus.Text = String.Empty
            'Me.cmbSlaType.Text = String.Empty

            Me.dtCreationDate.Value = Date.Now


            '***Start of CR # 346
            Me.txtSTNNum.Text = String.Empty
            Me.txtNTNNum.Text = String.Empty
            '***End of CR # 346

            ' Detail Search Record Opitions ReSetting Control
            'Me.cmbProductType_DtlSearch.SelectedIndex = 0
            'Me.cmbSLAType_DtlSearch.SelectedIndex = 0
            'Me.cmbStatus_DtlSearch.SelectedIndex = 0
            ' '' Creation Date Time Pickers in Detailed Search Dialogue Box
            'Me.dtpCreationDate_DtlSearch.Checked = False
            'Me.dtpCreationDate2_DtlSearch.Checked = False
            ' '' Paymenet Date Time Pickers in Detailed Search Dialogue Box
            'Me.dtpPaymentDate_DtlSearch.Checked = False
            'Me.dtpPaymentto_DtlSearch.Checked = False
            ' '' Maintainance Date Time Pickers in Detailed Search Dialogue Box
            'Me.dtpMaintEndDate_DtlSearch.Checked = False
            'Me.dtpmaintTo_DtlSearch.Checked = False

            ' '' To Clear the Contact Person Text Box in the Detailed Search Dialogue Box
            'Me.txtContactPerson_dtlSearch.Text = String.Empty
            'Me.dtMaintEndDate.Checked = False
            'Me.dtMaintStartDate.Checked = False
            'Me.dtPaymentRcvdDate.Checked = False
            '' Set Focus to the UI Control of GL-Account
            Me.UiCtrlGLAccount1.Focus()
            Me.ApplySecurity(EnumDataMode.New)

        Catch ex As Exception
            Throw ex

        End Try



    End Sub

    Public Sub ApplySecurity(ByVal Mode As EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

        Try
            '' For the New Mode
            If Mode.ToString = EnumDataMode.[New].ToString Then

                btnNew.Enabled = False

                If mobjControlList.Item("btnSave") Is Nothing Then
                    btnSave.Enabled = False
                Else
                    btnSave.Enabled = True
                End If

                btnUpdate.Enabled = False
                btnDelete.Enabled = False
                btnCancel.Enabled = True
                SetNavigationButtons(EnumDataMode.Disabled)

                '' For the Edit Mode
            ElseIf Mode.ToString = EnumDataMode.Edit.ToString Then

                btnNew.Enabled = True
                btnSave.Enabled = False

                If mobjControlList.Item("btnUpdate") Is Nothing Then
                    btnUpdate.Enabled = False
                Else
                    btnUpdate.Enabled = True
                End If

                If mobjControlList.Item("btnDelete") Is Nothing Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Enabled = True
                End If
                btnCancel.Enabled = False
                SetNavigationButtons(EnumDataMode.Edit)

                '' For the Read Only Mode
            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                btnNew.Enabled = True
                btnSave.Enabled = False
                btnUpdate.Enabled = False
                btnDelete.Enabled = False
                btnCancel.Enabled = False
            End If

            If mobjControlList.Item("btnExport") Is Nothing Then
                Me.UiCtrlGridBar1.btnExport.Enabled = False
            End If


            '' Disabl/Enable the Button that Prints Grid data According to Login User rights
            If mobjControlList.Item("btnPrint") Is Nothing Then
                Me.UiCtrlGridBar1.btnPrint.Enabled = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

        Try
            '' Filling the Model
            mobjModel = New CustomerInfo
            With mobjModel

                Me.UiCtrlGLAccount1.txtACCode.Focus()

                .CustomerInfoID = intPkId
                .AccountID = Me.UiCtrlGLAccount1.GLAccountID
                .ContactPerson = funFilterReserveText(Me.txtContactPerson.Text)
                .PhoneOffice = funFilterReserveText(Me.txtPhoneOffice.Text)
                .Mobile = funFilterReserveText(Me.txtMobile.Text)
                .Fax = funFilterReserveText(Me.txtFax.Text)
                .Email = funFilterReserveText(Me.txtEmail.Text)
                .Address = funFilterReserveText(Me.txtAddress.Text)
                .Remarks = funFilterReserveText(Me.txtRemarks.Text)
                .CreationDate = Me.dtCreationDate.Value.Date
                .MaintStartDate = IIf(dtMaintEndDate.Checked = True, Me.dtMaintStartDate.Value.Date, Date.MinValue)
                .MaintEndDate = IIf(Me.dtMaintEndDate.Checked = True, Me.dtMaintEndDate.Value.Date, Date.MinValue)
                .PaymentRcvdDate = IIf(Me.dtPaymentRcvdDate.Checked = True, Me.dtPaymentRcvdDate.Value.Date, Date.MinValue)
                ' CR#202
                '.Product = IIf(Me.cmbProduct.SelectedIndex = 0, Nothing, Me.cmbProduct.SelectedItem )
                .Product = IIf(Me.cmbProduct.SelectedIndex = 0, Nothing, Me.cmbProduct.SelectedValue)
                .Status = IIf(Me.cmbStatus.SelectedIndex = 0, Nothing, Me.cmbStatus.SelectedItem)
                .SlaType = IIf(Me.cmbSlaType.SelectedIndex = 0, Nothing, Me.cmbSlaType.SelectedItem)
                .MonthlyAmount = IIf(Me.txtMonthlyAmount.Text = String.Empty, 0, Me.txtMonthlyAmount.Text)
                ' CR#202
                '.DtlProduct = IIf(Me.cmbProductType_DtlSearch.Text.Trim = String.Empty, Nothing, Me.cmbProductType_DtlSearch.SelectedItem )
                .DtlProduct = IIf(Me.cmbProductType_DtlSearch.Text.Trim = String.Empty, Nothing, Me.cmbProductType_DtlSearch.SelectedValue)
                .DtlStatus = IIf(Me.cmbStatus_DtlSearch.Text.Trim = String.Empty, Nothing, Me.cmbStatus_DtlSearch.SelectedItem)
                .DtlSlaType = IIf(Me.cmbSLAType_DtlSearch.Text.Trim = String.Empty, Nothing, Me.cmbSLAType_DtlSearch.SelectedItem)
                .DtlContactPerson = IIf(Me.txtContactPerson_dtlSearch.Text.Trim = String.Empty, Nothing, Me.txtContactPerson_dtlSearch.Text)
                .DtlCreationDate = IIf(dtpCreationDate_DtlSearch.Checked = True, Me.dtpCreationDate_DtlSearch.Value.Date, Date.MinValue)
                .DtlCreationDate2 = IIf(dtpCreationDate2_DtlSearch.Checked = True, Me.dtpCreationDate2_DtlSearch.Value.Date, Date.MinValue)
                .DtlMaintEndDate = IIf(dtpMaintEndDate_DtlSearch.Checked = True, Me.dtpMaintEndDate_DtlSearch.Value.Date, Date.MinValue)
                .DtlPaymentRcvdDate = IIf(Me.dtpPaymentDate_DtlSearch.Checked = True, Me.dtpPaymentDate_DtlSearch.Value.Date, Date.MinValue)
                .DtlPaymentRcvdToDate = IIf(Me.dtpPaymentto_DtlSearch.Checked = True, Me.dtpPaymentto_DtlSearch.Value.Date, Date.MinValue)
                .DtlMaintEndToDate = IIf(Me.dtpmaintTo_DtlSearch.Checked = True, Me.dtpmaintTo_DtlSearch.Value.Date, Date.MinValue)
                '*****Start of CR # 346
                .cust_NTNNumber = Me.txtNTNNum.Text
                .Cust_STRNumber = Me.txtSTNNum.Text
                '*****End of CR # 346
                'Cr # 164
                mobjModel.ActivityLog.ShopID = 0 'gObjUserInfo.ShopInfo.ShopID
                mobjModel.ActivityLog.ScreenTitle = Me.Text
                mobjModel.ActivityLog.LogGroup = "Transactions"
                mobjModel.ActivityLog.UserID = gObjUserInfo.UserID

            End With

        Catch ex As Exception
            Throw ex
        End Try



    End Sub

    Private Function funFilterReserveText(ByVal Txt As String) As String
        Try

            funFilterReserveText = Replace(Txt, "'", "''", , , vbTextCompare)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Public Function IsValidate(Optional ByVal Mode As EnumDataMode = EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        '' For the front end validation

        Try

            If Mode = EnumDataMode.[New] Or EnumDataMode.Edit Then
                If Me.UiCtrlGLAccount1.txtACCode.Text = String.Empty Then
                    ShowInformationMessage("Account Code is Missing")
                    Me.UiCtrlGLAccount1.Focus()
                    Return False

                End If

                If Me.dtMaintEndDate.Checked = False Then
                    ShowInformationMessage("Select Maintenance End Date")
                    Me.dtMaintEndDate.Focus()
                    Return False
                End If

                If Me.txtMonthlyAmount.Text.Trim = String.Empty Then
                    ShowInformationMessage("Enter Monthly Amount")
                    Me.txtMonthlyAmount.Focus()
                    Return False
                End If

                If Me.cmbStatus.SelectedIndex = 0 Then
                    ShowInformationMessage("Select Status")
                    Me.cmbStatus.Focus()
                    Return False
                End If
            End If
            'If Me.txtContactPerson.Text.Trim = String.Empty Then
            '    ShowErrorMessage("Contact Person's Name is missing ! ")
            '    Me.txtContactPerson.Focus()
            '    Return False
            'End If
            ' '' Contact Information check
            'If Me.txtMobile.Text.Trim = String.Empty Or Me.txtPhoneOffice.Text.Trim = String.Empty Then
            '    ShowErrorMessage("Contact Information is Missing.")
            '    Me.txtMobile.Focus()
            '    Return False

            'End If
            
            '' Filling the Model
            FillModel()

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

        Try

            If Me.IsValidate(EnumDataMode.[New]) Then
                If ShowConfirmationMessage("Do you want to save?", MessageBoxDefaultButton.Button1) <> Windows.Forms.DialogResult.Yes Then
                    Exit Function
                End If
                If New CustomerInfoDAL().Add(mobjModel) Then
                    If Me.chkUpdateOnline.Checked Then

                        If dtMaintStartDate.Checked = True Or dtMaintEndDate.Checked = True Then

                            If Me.UpdateOnline() Then
                                Application.DoEvents()
                                Me.lblScreentext1.Text = "Record updated successfully"
                            Else
                                Application.DoEvents()
                                Me.lblScreentext1.Text = "Update failed"

                            End If

                        End If

                    End If

                    ShowInformationMessage("Record has been Inserted Successfully")

                End If

                Call ReSetControls()
                'CR#212
                SearchAgainstCriteria()
                'Call GetAllRecords()

            End If

        Catch ex As Exception
            Throw ex

        End Try

    End Function

    Function UpdateOnline() As Boolean

        Me.Timer1.Enabled = False
        Me.lblScreentext1.Visible = True
        Me.pbOnlineActivation.Visible = True
        Me.pbOnlineActivation.Value = 0

        Application.DoEvents()

        'Dim conn As New MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("CON_STR_ONLINE").ConnectionString)
        '------------- start of Cr # 286
        Dim Constr As String = String.Empty
        Try
            Constr = Decrypt(System.Configuration.ConfigurationManager.ConnectionStrings("CON_STR_ONLINE").ConnectionString, "f")
        Catch ex As Exception
            ShowErrorMessage("Please mention valid connection string for online activation")
            Return False
        End Try
        Dim conn As New MySqlConnection(Constr)
        '-----------End of cr # 286
        Dim trans As MySqlTransaction

        Application.DoEvents()
        Me.pbOnlineActivation.Value = 10

        Me.lblScreentext1.Text = "Connecting online database"
        Application.DoEvents()
        Application.DoEvents()
        Try

            Me.pbOnlineActivation.Value = 15
            conn.Open()
            Application.DoEvents()
            Me.pbOnlineActivation.Value = 20
            Application.DoEvents()
            Me.pbOnlineActivation.Value = 25
            Me.lblScreentext1.Text = "Database connected"
            Application.DoEvents()
            Application.DoEvents()
            Me.lblScreentext1.Text = "Preparing to update"
            Me.pbOnlineActivation.Value = 35

            Application.DoEvents()
            Application.DoEvents()
            trans = conn.BeginTransaction
            Dim cm As New MySqlCommand
            Dim strSQL As String

            strSQL = ""
            strSQL = "UPDATE tblproductkeys SET "

            If dtMaintStartDate.Checked Then
                strSQL = strSQL + " MaintenanceStartDate = '" & dtMaintStartDate.Value.Date.ToString("yyyy-MM-dd") & "', "
            End If
            If dtMaintEndDate.Checked Then
                strSQL = strSQL + " MaintenanceEndDate = '" & dtMaintEndDate.Value.Date.ToString("yyyy-MM-dd") & "' "
            End If

            strSQL = strSQL + " where customerid= (select contactid from tbldefcontacts where accountcode='" & Me.UiCtrlGLAccount1.txtACCode.Text.ToString & "') "

            ''Execute SQL 
            cm.CommandText = strSQL
            cm.Connection = conn
            Me.pbOnlineActivation.Value = 50
            Me.lblScreentext1.Text = "Updating information"
            Application.DoEvents()
            Application.DoEvents()

            cm.ExecuteNonQuery()
            Application.DoEvents()
            Application.DoEvents()
            Me.pbOnlineActivation.Value = 70

            Me.lblScreentext1.Text = "Update completed successfully"
            Application.DoEvents()
            Application.DoEvents()

            ''Commit Traction
            trans.Commit()

            Me.pbOnlineActivation.Value = 80
            Application.DoEvents()

            ''Return
            Return True

            Application.DoEvents()
            Application.DoEvents()

        Catch ex As MySqlException
            If Not trans Is Nothing Then trans.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Application.DoEvents()
            Application.DoEvents()
            Me.lblScreentext1.Text = "Update failed"
            Application.DoEvents()
            Application.DoEvents()

        Catch ex As Exception
            If Not trans Is Nothing Then trans.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Application.DoEvents()
            Application.DoEvents()
            Me.lblScreentext1.Text = "Update failed"
            Application.DoEvents()
            Application.DoEvents()

        Finally

            Me.lblScreentext1.Text = "Closing database connection"
            Application.DoEvents()
            Me.pbOnlineActivation.Value = 90
            Application.DoEvents()
            If conn.State = ConnectionState.Open Then conn.Close()
            Application.DoEvents()
            Me.pbOnlineActivation.Value = 100
            Application.DoEvents()
            Me.lblScreentext1.Text = "Connection closed"

            Timer1.Enabled = True

        End Try
    End Function

    Public Function Update(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

        Try

            ''Applying Front End Validation Checks
            If Me.IsValidate(EnumDataMode.Edit) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes
                ''Getting Save Confirmation from User
                result = ShowConfirmationMessage("Do you want to update?", MessageBoxDefaultButton.Button1)

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Update Method by passing Model Object
                    ' CR# 171       Asif Kamal      Issues on updating record on Customer Information Form
                    If New CustomerInfoDAL().Update(Me.mobjModel) Then

                        If gblnShowAfterUpdateMessages Then
                            ''Getting Save Confirmation from User
                            ShowInformationMessage(gstrMsgAfterUpdate)
                        End If

                        If Me.chkUpdateOnline.Checked Then

                            If dtMaintStartDate.Checked = True Or dtMaintEndDate.Checked = True Then

                                If Me.UpdateOnline() Then
                                    Application.DoEvents()
                                    Me.lblScreentext1.Text = "Record updated successfully"

                                Else
                                    Application.DoEvents()
                                    Me.lblScreentext1.Text = "Update failed"

                                End If

                            End If

                        End If

                        ''Query to Database and get fressh modifications in the Grid
                        'CR#212
                        ' Me.GetAllRecords()
                        SearchAgainstCriteria()

                        'to select the last updated record
                        For Rind As Int16 = 0 To (grdAllRecords.RowCount - 1)
                            If Me.grdAllRecords.GetRow(Rind).Cells(EnumGridCustomerInfo.AccountID).Value = mobjModel.AccountID Then
                                Me.grdAllRecords.Row = Rind
                                Exit For
                            End If
                        Next


                    End If
                End If
            End If

        Catch ex As Exception
            If ex.Message = gstrMsgDuplicateName Then
                ShowErrorMessage(ex.Message)
            ElseIf ex.Message = gstrMsgDuplicateCode Then
                ShowErrorMessage(ex.Message)
            Else
                Throw ex
            End If
        End Try
    End Function

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

        Try

            ''Applying Front End Validation Checks
            ' If Me.IsValidate(, "BackEndDeleteValidation") Then
            Dim result As DialogResult = Windows.Forms.DialogResult.Yes
            ''Getting Save Confirmation from User
            result = ShowConfirmationMessage("Do you want to delete?", MessageBoxDefaultButton.Button2)
            If result = Windows.Forms.DialogResult.Yes Then

                ''Create a DAL Object and calls its Delete Method by passing Model Object
                Me.FillModel()
                If New CustomerInfoDAL().Deleted(Me.mobjModel) Then

                    ''This will hold row index of the selected row 
                    Dim intGridRowIndex As Integer
                    intGridRowIndex = Me.grdAllRecords.Row

                    ''Query to Database and get fressh modifications in the Grid
                    'CR#212
                    'Me.GetAllRecords()
                    SearchAgainstCriteria()
                    Call ReSetControls()

                    '        ''Call RowColumn Change Event
                    '        Me.grdAllRecords_SelectionChanged(Nothing, Nothing)

                    '        ''Reset the row index to the grid
                    '        If intGridRowIndex > (Me.grdAllRecords.RowCount - 1) Then intGridRowIndex = (Me.grdAllRecords.RowCount - 1)
                    '        If Not intGridRowIndex < 0 Then Me.grdAllRecords.Row = intGridRowIndex
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function


#End Region

    Private Sub frmCustomerInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load


        Try
            
            mobjControlList = GetFormSecurityControls(Me.Name)
            '' Setting Images to the Buttons
            Me.SetButtonImages()

            Me.FillCombos()

            Call GetAllRecords()
            Call ReSetControls()
            Me.cmbProductType_DtlSearch.SelectedIndex = 0
            Me.cmbSLAType_DtlSearch.SelectedIndex = 0
            Me.cmbStatus_DtlSearch.SelectedIndex = 0
            Me.ActiveControl = Me.UiCtrlGLAccount1
            Me.Timer1.Interval = 1000


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click, btnDelete.Click, btnCancel.Click, btnExit.Click, btnUpdate.Click, btnNew.Click, btnDtlSearch.Click
        Try

            Dim btn As Windows.Forms.Button = CType(sender, Windows.Forms.Button)

            ''If New Button is Clicked
            If btn.Name = btnNew.Name Then
                ''Refresh the controls for new mode
                Me.TabCustomerInfo.SelectedTab = Me.TabCustomerInfo.TabPages(0)
                Me.ReSetControls()
            ElseIf btn.Name = btnSave.Name Then
                '' Call Save method 
                Me.Save()
            ElseIf btn.Name = btnUpdate.Name Then
                '' Call Update method 
                Me.Update()
            ElseIf btn.Name = btnDelete.Name Then
                '' Call Delete method   
                Me.Delete()
            ElseIf btn.Name = btnCancel.Name Then
                grdAllRecords_SelectionChanged(Nothing, Nothing)
            ElseIf btn.Name = btnExit.Name Then
                Me.Close()
            ElseIf btn.Name = btnDtlSearch.Name Then
                'CR#212
                SearchAgainstCriteria()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            mobjModel = Nothing
        End Try
    End Sub
    Private Sub SearchAgainstCriteria()
        '' Call GetAll_Detail Method
        Try
            Dim lngTotalRecords As Long
            FillModel()
            '' [1]. Front End Validations


            Dim dt As DataTable = New CustomerInfoDAL().GetAll_Detail(mobjModel)
            dt.Columns.Add("Check", GetType(System.Boolean))  ' 30-05-2013       farooq-H           CR# 242
            dt.Columns.Add("InvalidRecord", GetType(System.String)) ''// 28-JAN-2014        Farooq-H          Cr#292  
            Me.grdAllRecords.DataSource = dt
            Me.grdAllRecords.RetrieveStructure()

            lngTotalRecords = dt.Rows.Count

            If lngTotalRecords <= 0 Then
                Me.ReSetControls()
            Else
                ''Me.grdAllRecords.MoveLast()
            End If
            Call ApplyGridSettings()
            Me.UiCtrlGLAccount2.txtACCode.Text = String.Empty
            Me.UiCtrlGLAccount2.txtAccountName.Text = String.Empty
            Me.ReSetControls()

        Catch ex As Exception
            Throw ex

        End Try
    End Sub
    Private Sub GridEX1_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdAllRecords.FormattingRow

    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click, btnPrevious.Click, btnNext.Click, btnLast.Click
        Try

            Dim btn As Button = CType(sender, Button)

            ''If Move First is clicked, then navigate to first record of the grid
            If btn.Name = Me.btnFirst.Name Then

                Me.grdAllRecords.MoveFirst()

                ''If Move Previous is clicked,
            ElseIf btn.Name = Me.btnPrevious.Name Then

                Me.grdAllRecords.MovePrevious()

                ''If Move Next is clicked, 
            ElseIf btn.Name = Me.btnNext.Name Then

                Me.grdAllRecords.MoveNext()


                ''If Move Last is clicked, then navigate to Last record of the grid
            ElseIf btn.Name = Me.btnLast.Name Then

                Me.grdAllRecords.MoveLast()

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdAllRecords_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAllRecords.DoubleClick

        Try


            ''If there is no record found in grid then load the screen in new mode
            If grdAllRecords.RowCount = 0 Then
                Me.ReSetControls()
                Exit Sub
            End If

            If Me.grdAllRecords.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                Me.grdAllRecords.Row = (Me.grdAllRecords.Row - 1)
                Exit Sub
            End If


            ''intPkId = Convert.ToInt32()
            Me.UiCtrlGLAccount1.GLAccountID = Val(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountID).ToString)
            Me.UiCtrlGLAccount1.txtACCode.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountCode).ToString
            Me.UiCtrlGLAccount1.txtAccountName.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountName).ToString
            Me.txtContactPerson.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.ContactPerson).ToString
            Me.txtPhoneOffice.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.PhoneOffice).ToString
            Me.txtMobile.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Mobile).ToString
            Me.txtFax.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Fax).ToString
            Me.txtEmail.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Email).ToString
            Me.txtAddress.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Address).ToString
            Me.txtRemarks.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Remarks).ToString
            Me.dtCreationDate.Value = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.CreationDate)

             
            If Not IsDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MaintStartDate).ToString) Then
                Me.dtMaintStartDate.Value = Now.Date
                Me.dtMaintStartDate.Checked = False
            Else
                Me.dtMaintStartDate.Value = CDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MaintStartDate).ToString)
                Me.dtMaintStartDate.Checked = True
            End If

            If Not IsDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MaintEndDate).ToString) Then
                Me.dtMaintEndDate.Value = Now.Date
                Me.dtMaintEndDate.Checked = False
            Else
                Me.dtMaintEndDate.Value = CDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MaintEndDate).ToString)
                Me.dtMaintEndDate.Checked = True
            End If

            If Not IsDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.PaymentRcvdDate).ToString) Then
                Me.dtPaymentRcvdDate.Value = Now.Date
                Me.dtPaymentRcvdDate.Checked = False
            Else
                Me.dtPaymentRcvdDate.Value = CDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.PaymentRcvdDate).ToString)
                Me.dtPaymentRcvdDate.Checked = True
            End If


            Me.cmbProduct.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Product).ToString
            Me.cmbStatus.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Status).ToString
            Me.cmbSlaType.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.SlaType).ToString
            Me.txtMonthlyAmount.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MonthlyAmount).ToString

            '****Start of CR # 346
            Me.txtNTNNum.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Cust_NTNNumber).ToString
            Me.txtSTNNum.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Cust_STRNumber).ToString
            '****End of CR # 346
            Call ApplySecurity(EnumDataMode.Edit)
            Me.TabCustomerInfo.SelectedTab = Me.TabCustomerInfo.TabPages(0)
            Me.txtContactPerson.Focus()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdAllRecords_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAllRecords.SelectionChanged
        Try
            If grdAllRecords.RowCount = 0 Then
                Me.ReSetControls()
                Exit Sub
            End If

            If Me.grdAllRecords.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                Me.grdAllRecords.Row = (Me.grdAllRecords.Row - 1)
                Exit Sub
            End If


            ''intPkId = Convert.ToInt32()
            Me.UiCtrlGLAccount1.GLAccountID = Val(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountID).ToString)
            Me.UiCtrlGLAccount1.txtACCode.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountCode).ToString
            Me.UiCtrlGLAccount1.txtAccountName.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountName).ToString
            Me.txtContactPerson.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.ContactPerson).ToString
            Me.txtPhoneOffice.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.PhoneOffice).ToString
            Me.txtMobile.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Mobile).ToString
            Me.txtFax.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Fax).ToString
            Me.txtEmail.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Email).ToString
            Me.txtAddress.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Address).ToString
            Me.txtRemarks.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Remarks).ToString
            Me.dtCreationDate.Value = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.CreationDate)

            If Not IsDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MaintStartDate).ToString) Then
                Me.dtMaintStartDate.Value = Now.Date
                Me.dtMaintStartDate.Checked = False
            Else
                Me.dtMaintStartDate.Value = CDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MaintStartDate).ToString)
                Me.dtMaintStartDate.Checked = True
            End If

            If Not IsDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MaintEndDate).ToString) Then
                Me.dtMaintEndDate.Value = Now.Date
                Me.dtMaintEndDate.Checked = False
            Else
                Me.dtMaintEndDate.Value = CDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MaintEndDate).ToString)
                Me.dtMaintEndDate.Checked = True
            End If

            If Not IsDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.PaymentRcvdDate).ToString) Then
                Me.dtPaymentRcvdDate.Value = Now.Date
                Me.dtPaymentRcvdDate.Checked = False
            Else
                Me.dtPaymentRcvdDate.Value = CDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.PaymentRcvdDate).ToString)
                Me.dtPaymentRcvdDate.Checked = True
            End If

            Me.cmbProduct.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Product).ToString
            Me.cmbStatus.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Status).ToString
            Me.cmbSlaType.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.SlaType).ToString
            Me.txtMonthlyAmount.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MonthlyAmount).ToString

            Call ApplySecurity(EnumDataMode.Edit)
            Me.txtContactPerson.Focus()

        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub UiCtrlGLAccount1_GetGLAccount(ByVal sender As uiCtrlGLAccount) Handles UiCtrlGLAccount1.GetGLAccount

        Try
            Dim dtGrid As DataTable = CType(Me.grdAllRecords.DataSource, DataTable)
            If dtGrid.Constraints.Count = 0 Then
                Dim uk As New UniqueConstraint("AccID", dtGrid.Columns(EnumGridCustomerInfo.AccountID), True)
                dtGrid.Constraints.Add(uk)
            End If

            Dim drFound As DataRow = dtGrid.Rows.Find(sender.GLAccountID)

            If Not drFound Is Nothing Then

                If grdAllRecords.RowCount = 0 Then
                    Me.ReSetControls()
                    Exit Sub
                End If

                If Me.grdAllRecords.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                    Me.grdAllRecords.Row = (Me.grdAllRecords.Row - 1)
                    Exit Sub
                End If


                ''intPkId = Convert.ToInt32()
                'Me.UiCtrlGLAccount1.GLAccountID = Val(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountID).ToString)

                Me.grdAllRecords.Row = Me.grdAllRecords.GetRow(drFound).RowIndex

                Me.UiCtrlGLAccount1.txtACCode.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountCode).ToString
                Me.UiCtrlGLAccount1.txtAccountName.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.AccountName).ToString
                Me.txtContactPerson.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.ContactPerson).ToString
                Me.txtPhoneOffice.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.PhoneOffice).ToString
                Me.txtMobile.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Mobile).ToString
                Me.txtFax.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Fax).ToString
                Me.txtEmail.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Email).ToString
                Me.txtAddress.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Address).ToString
                Me.txtRemarks.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Remarks).ToString
                Me.dtCreationDate.Value = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.CreationDate)

                If Not IsDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MaintStartDate).ToString) Then
                    Me.dtMaintStartDate.Value = Now.Date
                    Me.dtMaintStartDate.Checked = False
                Else
                    Me.dtMaintStartDate.Value = CDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MaintStartDate).ToString)
                    Me.dtMaintStartDate.Checked = True
                End If

                If Not IsDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MaintEndDate).ToString) Then
                    Me.dtMaintEndDate.Value = Now.Date
                    Me.dtMaintEndDate.Checked = False
                Else
                    Me.dtMaintEndDate.Value = CDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MaintEndDate).ToString)
                    Me.dtMaintEndDate.Checked = True
                End If

                If Not IsDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.PaymentRcvdDate).ToString) Then
                    Me.dtPaymentRcvdDate.Value = Now.Date
                    Me.dtPaymentRcvdDate.Checked = False
                Else
                    Me.dtPaymentRcvdDate.Value = CDate(Me.grdAllRecords.GetValue(EnumGridCustomerInfo.PaymentRcvdDate).ToString)
                    Me.dtPaymentRcvdDate.Checked = True
                End If

                Me.cmbProduct.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Product).ToString
                Me.cmbStatus.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.Status).ToString
                Me.cmbSlaType.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.SlaType).ToString
                Me.txtMonthlyAmount.Text = Me.grdAllRecords.GetValue(EnumGridCustomerInfo.MonthlyAmount).ToString

                Call ApplySecurity(EnumDataMode.Edit)
                Me.txtContactPerson.Focus()
            Else
                Me.txtContactPerson.Text = String.Empty
                Me.txtAddress.Text = String.Empty
                Me.txtEmail.Text = String.Empty
                Me.txtFax.Text = String.Empty
                Me.txtMobile.Text = String.Empty
                Me.txtMonthlyAmount.Text = String.Empty
                Me.txtPhoneOffice.Text = String.Empty
                Me.txtRemarks.Text = String.Empty
                Me.cmbProduct.SelectedIndex = 0
                Me.cmbStatus.SelectedIndex = 0
                Me.cmbSlaType.SelectedIndex = 0
                Me.dtMaintStartDate.Checked = False
                Me.dtMaintEndDate.Checked = False
                Me.dtPaymentRcvdDate.Checked = False
                Me.dtCreationDate.Value = Date.Now
                Me.txtContactPerson.Focus()
                Me.ApplySecurity(EnumDataMode.New)


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub UiCtrlGLAccount2_GetGLAccount(ByVal sender As uiCtrlGLAccount) Handles UiCtrlGLAccount2.GetGLAccount
        Try
            Dim dtGrid As DataTable = CType(Me.grdAllRecords.DataSource, DataTable)
            If dtGrid.Constraints.Count = 0 Then
                Dim uk As New UniqueConstraint("AccID", dtGrid.Columns(EnumGridCustomerInfo.AccountID), True)
                dtGrid.Constraints.Add(uk)
            End If

            Dim drFound As DataRow = dtGrid.Rows.Find(sender.GLAccountID)
            If Not drFound Is Nothing Then
                Me.UiCtrlGLAccount2.txtAccountName.Focus()
                Me.grdAllRecords.Row = Me.grdAllRecords.GetRow(drFound).Position 'Me.grdAllRecords.GetRow(Me.grdAllRecords.GetRow(drFound).Position).Position

            Else
                'Me.ActiveControl = Me.UiCtrlGLAccount2.txtAccountName

                ShowInformationMessage("Record does not exist")
                Exit Sub

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        
        End Try

    End Sub

    Private Sub txtMonthlyAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMonthlyAmount.TextChanged
        Try

            If Not IsNumeric(Me.txtMonthlyAmount.Text.Trim) Then Me.txtMonthlyAmount.Text = ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub frmCustomerInfo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Try
            If e.Control And e.KeyCode = Keys.S Then
                If btnSave.Enabled = True Then Me.Save()
            ElseIf e.Control And e.KeyCode = Keys.U Then
                If btnUpdate.Enabled = True Then Me.Update()
            ElseIf e.Control And e.KeyCode = Keys.N Then
                If btnNew.Enabled = True Then Me.ReSetControls()
            ElseIf e.Control And e.KeyCode = Keys.E Then
                If btnCancel.Enabled = True Then Me.grdAllRecords_SelectionChanged(Nothing, Nothing)
            ElseIf e.Control And e.KeyCode = Keys.D Then
                If btnDelete.Enabled = True Then Me.Delete()
            ElseIf e.Control And e.KeyCode = Keys.X Then
                If btnExit.Enabled = True Then Me.Close()
            ElseIf e.Control And e.KeyCode = Keys.F Then
                If btnNext.Enabled = True Then Me.grdAllRecords.MoveNext()
            ElseIf e.Control And e.KeyCode = Keys.B Then
                If btnPrevious.Enabled = True Then Me.grdAllRecords.MovePrevious()

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try



    End Sub

    Private Sub chkUpdateOnline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUpdateOnline.CheckedChanged
        If Not chkUpdateOnline.Checked = True Then
            Me.lblScreentext1.Visible = False
            Me.pbOnlineActivation.Visible = False
            Me.pbOnlineActivation.Value = 0

        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        timerTicks = timerTicks + 1
        If timerTicks = 10 Then
            timerTicks = 0
            Timer1.Enabled = False
            Me.lblScreentext1.Visible = False
            Me.pbOnlineActivation.Visible = False
            Me.pbOnlineActivation.Value = 0

        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try

            Me.cmbProductType_DtlSearch.SelectedIndex = 0
            Me.cmbSLAType_DtlSearch.SelectedIndex = 0
            Me.cmbStatus_DtlSearch.SelectedIndex = 0
            '' Creation Date Time Pickers in Detailed Search Dialogue Box
            Me.dtpCreationDate_DtlSearch.Checked = False
            Me.dtpCreationDate2_DtlSearch.Checked = False
            '' Paymenet Date Time Pickers in Detailed Search Dialogue Box
            Me.dtpPaymentDate_DtlSearch.Checked = False
            Me.dtpPaymentto_DtlSearch.Checked = False
            '' Maintainance Date Time Pickers in Detailed Search Dialogue Box
            Me.dtpMaintEndDate_DtlSearch.Checked = False
            Me.dtpmaintTo_DtlSearch.Checked = False

            '' To Clear the Contact Person Text Box in the Detailed Search Dialogue Box
            Me.txtContactPerson_dtlSearch.Text = String.Empty
            Me.dtMaintEndDate.Checked = False
            Me.dtMaintStartDate.Checked = False
            Me.dtPaymentRcvdDate.Checked = False
            ' Set Focus to the UI Control of GL-Account
            Me.UiCtrlGLAccount2.txtACCode.Text = String.Empty
            Me.UiCtrlGLAccount2.txtAccountName.Text = String.Empty
            Me.UiCtrlGLAccount1.Focus()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub
    ' 30-05-2013       farooq-H           CR# 242  SMS: Send sms from customer information screen. 

    Public Sub BuildMobile()
        StrMobile = String.Empty
        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdAllRecords.GetCheckedRows
            If r.Cells(EnumGridCustomerInfo.Mobile).Text.Trim.Replace("-", "").Replace(" ", "").Replace(".", "").Trim.Length > 0 Then
                Dim Str As String = r.Cells(EnumGridCustomerInfo.Mobile).Text.ToString.Trim.Replace("-", "").Replace(" ", "").Replace(".", "")
                If Str.Length <> 0 Then ''// 28-JAN-2014        Farooq-H          Cr#292  
                    If Str.Substring(0, 1) = "+" Then
                        Str = Str.Substring(1, Str.Length - 1)
                        If Str.Substring(0, 2) <> "92" Then
                            Str = "92" & Str
                        End If
                    End If
                    If Str.Substring(0, 1) = "0" Then
                        Str = Str.Substring(1, Str.Length - 1)
                        If Str.Substring(0, 2) <> "92" Then
                            Str = "92" & Str
                        End If
                    End If
                    If Str.Substring(0, 3) = "920" Then
                        Str = "92" & Str.Remove(0, 3)
                    End If
                    If Str.Length = 11 And Str.Substring(0, 2) <> "92" Then
                        Str = "92" & Str
                    End If
                    'Str = Replace(Replace(Str, ".", ""), " ", "")
                    StrMobile = StrMobile & Str & ","
                End If
            End If
        Next
    End Sub
    ' 30-05-2013       farooq-H           CR# 242  SMS: Send sms from customer information screen. 


    Private Sub btnSendSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS.Click
        Try
            ''// 28-JAN-2014        Farooq-H          Cr#292  
            If Me.CmbSMSTemplates.SelectedValue.ToString.Trim <> String.Empty Then
                SendSMSTemplate()
            Else
                Me.BuildMobile()
                If Not Me.StrMobile.Trim.Length > 0 Then
                    'TODO: Message box string is hard coded
                    ShowValidationMessage("Please select the mobile number first.")
                    Exit Sub
                End If
                If My.Computer.Network.IsAvailable = False Then
                    MessageBox.Show("Please Connect to the Internet First . ", "Candela")
                    Exit Sub
                End If
                Dim strMessage As String = InputBox("Please enter the SMS text.", "Message text", "Message text")
                If strMessage.ToString.Trim <> String.Empty Then
                    '    ShowValidationMessage("Please enter the message text.")
                    'Else
                    Dim respose As String = SendSMS(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("SMSAPIUserName"), DecryptWithALP(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("SMSAPIPassword")), strMessage.ToString.Trim, StrMobile.Trim.Replace("-", "").Replace(" ", "").Replace(".", ""), DAL.SystemConfigurationDAL.GetSystemConfigurationValue("BrandName"))
                    If respose.Contains("<type>Success</type>") Then
                        ShowInformationMessage("Mobile text message has been sent.")
                    Else
                        ShowInformationMessage("Please check the SMS settings and try again! .")
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''// 28-JAN-2014        Farooq-H          Cr#292  
    Private Sub SendSMSTemplate()
        'CR#307
        Dim arrPhoneNum As String() 'Array for Formating of multiple mobile numbers against one contact against CR#307
        Dim frmPrgBar As Utility.frmDualProgressBar 'Dynamic Progress bar against CR#307
        Dim intSMSProcessingCounter As Integer = 0

        Try
            If System.IO.File.Exists(gstrReportPath & "/.." & "\SMSLOGForCustomers.txt") Then
                System.IO.File.Delete(gstrReportPath & "/.." & "\SMSLOGForCustomers.txt")
            End If

            frmPrgBar = Utility.Utility.ShowDualProgressBar_Dynamic("frmCustomerInfo", Me.grdAllRecords.RecordCount, "Please wait, SMS is in prgress")

            frmPrgBar.Show()

            Dim ShowFirstSMSString As Boolean = False
            Dim CountSentSMS As Integer = 0
            Dim TotalSMS As Integer = 0
            Dim phonenumber As String = ""
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdAllRecords.GetCheckedRows
                If My.Computer.Network.IsAvailable = False Then
                    MessageBox.Show("Please Connect to the Internet First . ", "Candela")
                    Exit Sub
                End If
                'CR#307
                frmPrgBar.PB.Value += 1
                intSMSProcessingCounter += 1
                frmPrgBar.Text = "Processing SMS .... " & intSMSProcessingCounter
                '306
                'If (Me.cmbProductType_DtlSearch.SelectedIndex = 0) Then
                '    ShowInformationMessage("Select Product Type")
                '    Me.cmbProductType_DtlSearch.Focus()
                '    Exit Sub
                'End If
                'CR#307
                'TotalSMS = TotalSMS + 1
                phonenumber = r.Cells(EnumGridCustomerInfo.Mobile).Text.Trim.Replace("-", "").Replace(" ", "").Replace(".", "")

                'If phonenumber.Contains(",") = True Then
                'CR#307 start
                arrPhoneNum = phonenumber.Split(",")
                'End If

                For index As Int32 = 0 To arrPhoneNum.Length - 1

                    phonenumber = arrPhoneNum(index)
                    TotalSMS = TotalSMS + 1
                    'CR#307 end
                    If phonenumber.Length <> 0 Then

                        If phonenumber.Substring(0, 1) = "+" Then
                            phonenumber = phonenumber.Substring(1, phonenumber.Length - 1)
                            If phonenumber.Substring(0, 2) <> "92" Then
                                phonenumber = "92" & phonenumber
                            End If
                        End If
                        If phonenumber.Substring(0, 1) = "0" Then
                            phonenumber = phonenumber.Substring(1, phonenumber.Length - 1)
                            If phonenumber.Substring(0, 2) <> "92" Then
                                phonenumber = "92" & phonenumber
                            End If
                        End If
                        If phonenumber.Substring(0, 3) = "920" Then
                            phonenumber = "92" & phonenumber.Remove(0, 3)
                        End If
                        If phonenumber.Length = 11 And phonenumber.Substring(0, 2) <> "92" Then
                            phonenumber = "92" & phonenumber
                        End If
                    Else
                        WriteStatusFile(r.Cells(EnumGridCustomerInfo.AccountName).Text.ToString, r.Cells(EnumGridCustomerInfo.AccountCode).Text.ToString, "Phone Number Invalid")
                        Continue For
                    End If
                    If phonenumber.Length < 12 Or phonenumber.Substring(0, 3) <> "923" Then
                        WriteStatusFile(r.Cells(EnumGridCustomerInfo.AccountName).Text.ToString, r.Cells(EnumGridCustomerInfo.AccountCode).Text.ToString, "Phone Number Invalid")
                        Continue For
                    End If
                    Dim strtext As String = Me.CmbSMSTemplates.SelectedValue.ToString
                    ''      Parameters in the String 
                    '??Candela??
                    '??MaintEndDate??
                    '??MonthlyAmount?? 
                    '??MaintEndDate+1??

                    If strtext.Trim.Contains("??Candela??") Then
                        'If (Me.cmbProductType_DtlSearch.SelectedIndex = 0) Then
                        '    ShowInformationMessage("Select Product Type")
                        '    Me.cmbProductType_DtlSearch.Focus()
                        '    Exit Sub
                        'End If
                        'CR#306
                        'strtext = strtext.Trim.Replace("??Candela??", Me.cmbProductType_DtlSearch.Text)
                        Dim strProduct As String = "Candela"
                        strtext = strtext.Trim.Replace("??Candela??", IIf(Me.cmbProductType_DtlSearch.Text = "LightWave", "LightWave", "Candela"))
                    End If
                    If strtext.Trim.Contains("??MaintEndDate??") Then
                        If r.Cells(EnumGridCustomerInfo.MaintEndDate).Text.ToString.Trim = String.Empty Then
                            WriteStatusFile(r.Cells(EnumGridCustomerInfo.AccountName).Text.ToString, r.Cells(EnumGridCustomerInfo.AccountCode).Text.ToString, "warranty expiry date not available")
                            Continue For
                        End If
                        strtext = strtext.Trim.Replace("??MaintEndDate??", r.Cells(EnumGridCustomerInfo.MaintEndDate).Text.ToString.Trim)
                    End If
                    If strtext.Trim.Contains("??MonthlyAmount??") Then
                        If r.Cells(EnumGridCustomerInfo.MonthlyAmount).Text.ToString.Trim = String.Empty Or CDbl(r.Cells(EnumGridCustomerInfo.MonthlyAmount).Text) = 0.0 Then
                            WriteStatusFile(r.Cells(EnumGridCustomerInfo.AccountName).Text.ToString, r.Cells(EnumGridCustomerInfo.AccountCode).Text.ToString, "Monthly Amount is not available")
                            Continue For
                        End If
                        strtext = strtext.Trim.Replace("??MonthlyAmount??", CDbl(r.Cells(EnumGridCustomerInfo.MonthlyAmount).Text) * 3)
                    End If
                    If strtext.Trim.Contains("??MaintEndDate+1??") Then
                        If r.Cells(EnumGridCustomerInfo.MaintEndDate).Text.ToString.Trim = String.Empty Then
                            WriteStatusFile(r.Cells(EnumGridCustomerInfo.AccountName).Text.ToString, r.Cells(EnumGridCustomerInfo.AccountCode).Text.ToString, "warranty expiry date not available")
                            Continue For
                        End If
                        strtext = strtext.Trim.Replace("??MaintEndDate+1??", Convert.ToDateTime(r.Cells(EnumGridCustomerInfo.MaintEndDate).Text).AddDays(1))
                    End If
                    If ShowFirstSMSString = False Then
                        Dim result As DialogResult = Windows.Forms.DialogResult.OK
                        ''Getting Save Confirmation from User
                        result = MessageBox.Show(strtext, "Message Text of SMS", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                        If result = Windows.Forms.DialogResult.Cancel Then
                            Exit Sub
                        End If
                        ShowFirstSMSString = True
                    End If
                    If strtext.ToString.Trim <> String.Empty Then
                        Dim respose As String = SendSMS(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("SMSAPIUserName"), DecryptWithALP(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("SMSAPIPassword")), strtext.ToString.Trim, phonenumber.Trim.Replace("-", "").Replace(" ", "").Replace(".", ""), DAL.SystemConfigurationDAL.GetSystemConfigurationValue("BrandName"))
                        'CR#307 Start
                        If respose.ToString() = "Internet connection problem, problem check internet connectivity and try again" Then
                            MessageBox.Show(respose.ToString(), "Internet Problem", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        Else
                            'CR#307 End
                            If respose.Contains("<type>Success</type>") Then
                                CountSentSMS = CountSentSMS + 1
                            ElseIf respose.Contains("Invalid API id/password for the customer") Then
                                Throw New Exception("Invalid API id/password for the customer")
                            End If

                        End If

                    End If

                Next

            Next
            'CR#307
            frmPrgBar.Dispose()
            System.Windows.Forms.Application.DoEvents()
            System.Windows.Forms.Application.DoEvents()

            If TotalSMS = 0 Then
                ShowInformationMessage("Please select the Customer first.")
                Exit Sub
            End If
            If CountSentSMS = TotalSMS Then
                ShowInformationMessage("Mobile text message has been sent.")
            Else
                'CR#307
                Dim intNotSentCount As Integer = 0
                intNotSentCount = TotalSMS - CountSentSMS
                ShowInformationMessage("SMS to " & intNotSentCount & " contact couldn't send,Please check the SMSLOGForCustomers.txt! and Try again .")
                'CR#307 end
            End If
        Catch ex As Exception
            ''// 28-JAN-2014        Farooq-H          Cr#292  
            If ex.Message.Contains("Invalid API id/password for the customer") Then
                ShowInformationMessage("Please check the SMS settings and Try again .")
            Else
                ShowErrorMessage(ex.Message)
            End If

        End Try
    End Sub
    ''// 28-JAN-2014        Farooq-H          Cr#292  
    Private Sub WriteStatusFile(ByVal strCustomername As String, ByVal strCustomerCode As String, ByVal StrValidationParameter As String)
        Try
            Dim sw As System.IO.StreamWriter
            sw = System.IO.File.AppendText(gstrReportPath & "/.." & "\SMSLOGForCustomers.txt")
            sw.WriteLine("[Customer Name] = " & strCustomername.Trim.ToString & "           [Customer Code] = " & strCustomerCode.Trim.ToString & "         " & StrValidationParameter.ToString)
            sw.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'CR # 286
    Public Shared Function Decrypt(ByVal strEncryptedData As String, ByVal strKey As String) As String
        Try
            Dim strDecryptedData As String

            If Not strEncryptedData.Length > 0 Then
                Return String.Empty
            End If

            Dim sym As New EncryptionClassLibrary.Encryption.Symmetric(EncryptionClassLibrary.Encryption.Symmetric.Provider.Rijndael)
            Dim key As New EncryptionClassLibrary.Encryption.Data(strKey)
            Dim encryptedData As New EncryptionClassLibrary.Encryption.Data
            encryptedData.Hex = strEncryptedData
            Dim decryptedData As EncryptionClassLibrary.Encryption.Data
            decryptedData = sym.Decrypt(encryptedData, key)

            strDecryptedData = decryptedData.ToString
            Return strDecryptedData
        Catch Ex As System.IndexOutOfRangeException
            Throw New Exception("License information is incorrect. Please contact LumenSoft Technologies (Pvt) Ltd. +92 42 111 290 290")
        Catch ex As System.FormatException
            Throw New Exception("License information is incorrect. Please contact LumenSoft Technologies (Pvt) Ltd. +92 42 111 290 290")
        Catch ex As System.SystemException
            Throw New Exception("License information is incorrect. Please contact LumenSoft Technologies (Pvt) Ltd. +92 42 111 290 290")
        Catch ex As Exception
            Throw New Exception("License information is incorrect. Please contact LumenSoft Technologies (Pvt) Ltd. +92 42 111 290 290")
        End Try

    End Function

   
    ''// 28-JAN-2014        Farooq-H          Cr#292  
    Private Sub btnValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        Try
            If Me.grdAllRecords.RecordCount = 0 Then
                Exit Sub
            End If
            Dim InvalidRecord As Boolean = False
            'CR#307
            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdAllRecords.GetRows
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdAllRecords.GetCheckedRows
                InvalidRecord = False
                Dim phonenumber As String = r.Cells(EnumGridCustomerInfo.Mobile).Text.Trim.Replace("-", "").Replace(" ", "").Replace(".", "")
                If phonenumber.Length <> 0 Then
                    If phonenumber.Substring(0, 1) = "+" Then
                        phonenumber = phonenumber.Substring(1, phonenumber.Length - 1)
                        If phonenumber.Substring(0, 2) <> "92" Then
                            phonenumber = "92" & phonenumber
                        End If
                    End If
                    If phonenumber.Substring(0, 1) = "0" Then
                        phonenumber = phonenumber.Substring(1, phonenumber.Length - 1)
                        If phonenumber.Substring(0, 2) <> "92" Then
                            phonenumber = "92" & phonenumber
                        End If
                    End If
                    If phonenumber.Substring(0, 3) = "920" Then
                        phonenumber = "92" & phonenumber.Remove(0, 3)
                    End If
                    If phonenumber.Length = 11 And phonenumber.Substring(0, 2) <> "92" Then
                        phonenumber = "92" & phonenumber
                    End If
                    If phonenumber.Length < 12 Or phonenumber.Substring(0, 3) <> "923" Then
                        InvalidRecord = True
                    End If
                Else
                    InvalidRecord = True
                End If

                If r.Cells(EnumGridCustomerInfo.MaintEndDate).Text.ToString = "" Then
                    InvalidRecord = True
                    'CR#306 No need to validate on monthly amount
                    'ElseIf r.Cells(EnumGridCustomerInfo.MonthlyAmount).Text.ToString = "" Or Convert.ToDouble(r.Cells(EnumGridCustomerInfo.MonthlyAmount).Text) = 0.0 Then
                    '    InvalidRecord = True
                End If
                If InvalidRecord Then
                    r.BeginEdit()
                    r.Cells(EnumGridCustomerInfo.InvalidRecord).Value = "A"
                    r.EndEdit()
                Else
                    r.BeginEdit()
                    r.Cells(EnumGridCustomerInfo.InvalidRecord).Value = "B"
                    r.EndEdit()
                End If
            Next
            Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.InvalidRecord) _
                                    , Janus.Windows.GridEX.ConditionOperator.Equal, "A")
            fc.FormatStyle.ForeColor = Color.Red
            Me.grdAllRecords.RootTable.FormatConditions.Add(fc)
            Me.grdAllRecords.RootTable.SortKeys.Clear()
            Dim col As Janus.Windows.GridEX.GridEXColumn = Me.grdAllRecords.RootTable.Columns(EnumGridCustomerInfo.InvalidRecord)
            Dim sortKey As Janus.Windows.GridEX.GridEXSortKey = New Janus.Windows.GridEX.GridEXSortKey(col, SortOrder.Ascending)
            Me.grdAllRecords.RootTable.SortKeys.Add(sortKey)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class