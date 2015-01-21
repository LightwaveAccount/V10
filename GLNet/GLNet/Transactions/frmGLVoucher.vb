Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
''/////////////////////////////////////////////////////////////////////////////////////////
''//                     GL Transactions
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmGLVoucher.vb           				                            
''// Programmer	     : Abdul Jabbar
''// Creation Date	 : July 21,2009
''// Description     : Voucher & Temporary Voucher Entry screen.
''//-------------------------------------------------------------------------------------
''// CR#     Date Modified        Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// 1       21 oct,2009         Abdul Jabbar        GL Voucher > On saving Cash Payment voucher (sometime) existing Cash Receipt Voucher disturbed and complete Cash Payment voucher merged in Cash Receipt Voucher. 
''// 3       22 oct,2009         Abdul Jabbar        An issue occur while saving Master/Detail Entry,After saving Master entry when we get Current Transaction Id to save in Detail table ,it return wrong ID. (SQL statemnet include SELECT @@Identity to get Current ID). 
''// 85      04 Oct,2010         Abdul Jabbar        Error while entering Cash Receipt Voucher
''// 86      04 Oct,2010         Abdul Jabbar        In GL Voucher there should be option for Voucher Type based rights
''// 88      13 Oct,2010         Abdul Jabbar        Voucher Date should be Right based
''// 98      04 Nov,2010         Abdul Jabbar        Temporary voucher Posting Issue (VoucherDAL.vb)
''// 99      05 Nov,2010         Abdul Jabbar        Two or more Cash Account entry should be allowed in a voucher.
''// 102     08 Nov,2010         Abdul Jabbar        Unexpetected voucher # while moving Temp voucher to Actual
''// 115     12 Jan,2011/20 Jan,2010         Hammad/Abdul Jabbar              Allow Duplicate GL Account Entry
''// 118     19 Jan,2010         Abdul Jabbar       On selection of Cash Bank Info Account, only relevant tuple in Voucher grid should be replaced.
''// 119     08 April,2010       Abdul Jabbar    Voucher Print must be same as Voucher as entered by the user. i.e. if sorting flage is Off voucher detail entry sequence should remain same as entered by the user.  
''// 160     08 Dec,2011         Abdul Jabbar     Service Broker relevant changes in GL , when SSB will be On identity of both voucher master and detail will be off, now we will use shop id in where clause with shop_id<=0 so pick next voucher id in head office 
''// 165     08 Dec,2011         Asif Kamal       Voucher print replica, which will contain each account detail description
''// 172     06 Jan,2012         Asif Kamal       Newly added voucher report that shows description of voucher require formate settings
''// 214     26 Jul,2012         Abdul Jabbar     Voucher entry: Account repeatition should be allowed for all voucher other than type BP,BR
''// 228     24 jan,2013         Abdul Jabbar     Voucher Update: Error on Voucher update deleted Row information can't be updated
'//  254        18 june,2013        farooq-h      SMS Implmentation in Lightwave
''// 268     20 aug,2013         Rana Saeed       Invoice and PO print from GL
''// 210     12-sep-2013         Fatima Tajammal  Voucher Entry: 'Object reference' error occurs on clicking 'Delete' field of Search criteria
''// 260     12-sep-2013         Fatima Tajammal  Voucher Entry: System removes the information on saving the voucher if don't press ENTER 
''// 310     29 May,2014         Abdul Jabbar     Voucher screen; Arthematic OverFlow Error occured on saving voucher
''// 300     09 Jun,2014         Abdul Jabbar     Voucher: Company Wise & shop wise voucher entry option should be available.
''// 321     17 Jul,2014         Abdul Jabbar     Voucher Screen: Voucher type should be fix for next voucher
''// 323     23 Jul,2014          farooq-H        Cheque Printing: add cheque printing on voucher screen for bank payment voucher
''// 328     30 Sep,2014         M. Shoaib        GL Voucher: In Bank and Cash payment vouchers, Amount field should show amount paid.
''// 334     07-nov-2014         Fatima           Sale Tax Invoice: Sale Tax invoice printing option should be available in Lightwave
''// 337     14-Nov-2014         M. SHoaib        Standard Cheque Printing: Add a new cheque printing option which will print All banks standard cheque recently provided by State Bank
''// 343     11-dec-2014         Fatima           Bank Payment Voucher: Multiple banks should allow in bank payment voucher
''// 346     15-dec-2014         Fatima           Changes are required in Sale Tax Invoice
''// 349     29-dec-2014         Fatima           Sale Tax Invoice print: Few changes are required in Printing Sale Tax invoice
''// 351     30-dec-2014         M.Shoaib         Voucher print: A new Land scap report is required which will show cost center along with existing fields
''// 356     02-jan-2015         Fatima           Fix bugs on Voucher screen
''/////////////////////////////////////////////////////////////////////////////////////////

Public Class frmGLVoucher

    Implements IGeneral

#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
    ''This is the model object which will be set with data values and refered for Save|Update|Delete|Loading Record in Edit Mode
    Private mobjModel As GLVoucher
    'CR#300
    'Private pbLocationID As Integer = gobjLocationInfo.CompanyID
    Private pbLocationCode As String = gobjLocationInfo.CompanyCode
    Private pbLocationName As String = gobjLocationInfo.CompanyName
    Private pbFYearID As Integer = gObjFinancialYearInfo.FYearID
    Private pbFYearCode As String = gObjFinancialYearInfo.YearCode
    Private pbFYearStartDate As Date = gObjFinancialYearInfo.StartDate
    Private pbFYearEndDate As Date = gObjFinancialYearInfo.EndDate
    Private G_blnTrialVersion As Boolean = gblnTrialVersion
    Private pbIsCallFromSearchForm As Boolean = False
    Private pbVoucherID As Integer
    Private LngCashBankAccID As Long
    Private intPkId As Integer
    Private intLocId As Integer
    Private strGLAccCode As String = String.Empty
    Private strGLAccTitle As String = String.Empty
    Private intGLAccID As Integer
    Public Const G_STATUS_CLOSE As String = "Closed"                        'Use this variable to set status to Close
    Private BlnPostedVoucher As Boolean = False
    Private strSourceOfVoucher As String = "Accounts"
    Private ListOfDeletedIDs As String
    Private intTempVoucherID As Integer = 0
    Private BlnIsEnterKeyPressed As Boolean = False
    Private _IsTempVoucher As Boolean
    Private _IsCallFromSearchPostScreen As Boolean
    Private _IsCashBankRowExist As Boolean = False
    Private _StrVoucherIDs As String
    Private _BlnSaveV As Boolean = False
    Private _blnOOFFTEMPV As Boolean = False
    Private _VMonth As String
    Private _IsOtherVoucher As Boolean = False
    Private m_VNo As String
    Private _CurrMonth As String
    Private _CurrYear As String
    Dim strAccountID As String
    Dim strAmmountHead As String
    Dim strCashBankAcc As String


    Public Property IsTempVoucher() As Boolean
        Get
            Return _IsTempVoucher
        End Get
        Set(ByVal value As Boolean)
            _IsTempVoucher = value
        End Set
    End Property
    Public Property IsCallFromSearchPostScreen() As Boolean
        Get
            Return _IsCallFromSearchPostScreen
        End Get
        Set(ByVal value As Boolean)
            _IsCallFromSearchPostScreen = value
        End Set
    End Property
    Public Property StrVoucherIDs() As String
        Get
            Return _StrVoucherIDs
        End Get
        Set(ByVal value As String)
            _StrVoucherIDs = value
        End Set
    End Property

    '   Farooq-H            CR#254
    Dim mObjSMS As New SMSConfigurationDAL

#End Region

#Region "Enumerations"

    ''This is the representation of grid columns, and will be used while referring grid values, 
    ''instead of giving hard coded column's indexes
    Private Enum EnumGridVoucher
        VoucherDtlID = 0
        COADtlID = 1
        DtlCode = 2
        DtlTitle = 3
        Comments = 4
        CostCenter = 5
        DebitAmount = 6
        CreditAmount = 7
        Delete = 8
        Action = 9
        CashBank = 10
    End Enum
    Private Enum EnumMasterGrid

        VoucherID = 0
        Location = 1
        LocID = 2
        VoucherCode = 3
        FYearID = 4
        FYearCode = 5
        VoucherTypeID = 6
        VoucherType = 7
        VoucherMonth = 8
        VoucherNo = 9
        VoucherDate = 10
        PaidTo = 11
        COADtlID = 12
        COADtlCode = 13
        COADtlTitle = 14
        ChequeNo = 15
        ChequeDate = 16
        DueDate = 17
        Post = 18
        OtherVoucher = 19
        Source = 20
        TempVoucherID = 21

    End Enum
    Private Enum EnumMode

        NewMode = 0
        UpdateMode = 1

    End Enum

    Private Enum EnumPostUnpost

        POSTED = 0
        UNPOSTED = 1

    End Enum

    Private Enum EnumVType

        JV = 0
        CPV = 1
        CRV = 2
        BPV = 3
        BRV = 4
        SV = 5
        PV = 6

    End Enum

    Private Enum EnumSMSAction

        Save
        Update
        Delete
        Post
        Unpost


    End Enum



#End Region

#Region "Interface Methods"

    ''This will set the images of the buttons at runtime
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

            Me.btnSave.ImageList = gobjMyImageListForOperationBar
            Me.btnSave.ImageKey = "Save"

            Me.btnUpdate.ImageList = gobjMyImageListForOperationBar
            Me.btnUpdate.ImageKey = "Update"

            Me.btnCancel.ImageList = gobjMyImageListForOperationBar
            Me.btnCancel.ImageKey = "Cancel"

            Me.btnDelete.ImageList = gobjMyImageListForOperationBar
            Me.btnDelete.ImageKey = "Delete"
            ' CR # 351
            Me.btnPrintLandScape.ImageList = gobjMyImageListForOperationBar
            Me.btnPrintLandScape.ImageKey = "Print"

            Me.btnprint.ImageList = gobjMyImageListForOperationBar
            Me.btnprint.ImageKey = "Print"
            
            Me.btnExit.ImageList = gobjMyImageListForOperationBar
            Me.btnExit.ImageKey = "Exit"

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''Here will will use this function to fill-up all Combos and Listboxes on the form
    ''Optional condition would be used to fill-up combo or Listbox; which based on the selection of some other combo.
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        'CR#300
        Dim strUserLocations As String = String.Empty

        Try


            Dim DTVType As DataTable

            'dvChargeToList = GetFilterDataFromDataTable(CType(gObjMyAppHashTable(EnumHashTableKeyConstants.GetChargeToList.ToString), DataTable), "")
            DTVType = New VoucherDAL().GetVoucherType(IsTempVoucher)
            Me.cboVoucherType.ValueMember = "VType ID"
            Me.cboVoucherType.DisplayMember = "VType"
            Me.cboVoucherType.DataSource = DTVType
            Me.cboVoucherType.SelectedIndex = 0

            Dim DTLocation As DataTable
            'CR#300
            'strUserLocations = GetUserCompanies(gObjUserInfo.UserID)
            'DTLocation = New VoucherDAL().GetLocation(pbLocationID)
            DTLocation = New VoucherDAL().GetLocationOfLoggedInUser(gObjUserInfo.UserID)

            Me.cboCompany.ValueMember = "LocID"
            Me.cboCompany.DisplayMember = "Location"
            Me.cboCompany.DataSource = DTLocation
            'CR#300
            'Me.cboCompany.SelectedIndex = 0
            'If pbLocationID = 0 Then
            '    pbLocationID = gobjLocationInfo.CompanyID
            'End If
            Me.cboCompany.SelectedValue = gobjLocationInfo.CompanyID


            'CR#300 Populating Source drop down
            Dim ObjDataRow As DataRow
            cboSource.Items.Clear()

            Dim ObjDAL As New DAL.PostedVouchersDAL
            Dim DTConfigList As New DataTable

            DTConfigList = ObjDAL.GetTblshopConfigValues()

            If DTConfigList.Rows.Count > 0 Then

                ObjDataRow = DTConfigList.NewRow
                ObjDataRow.Item("config_value") = gstrComboZeroIndexString
                DTConfigList.Rows.InsertAt(ObjDataRow, 0)

                ObjDataRow = DTConfigList.NewRow
                ObjDataRow.Item("config_value") = "Accounts"
                DTConfigList.Rows.InsertAt(ObjDataRow, 1)

                cboSource.DataSource = DTConfigList
                cboSource.SelectedIndex = 1

                cboSource.DisplayMember = "config_value"
                cboSource.ValueMember = "config_value"

            Else

                cboSource.Items.Add(gstrComboZeroIndexString)
                cboSource.Items.Add("Accounts")
                cboSource.SelectedIndex = 1

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''Here we will use this procedure to load all master records; respective to the screen.

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Condition = String.Empty
            ''Getting Datasource for Grid from DAL
            'If Screen has called from Voucher Search Form then include voucher Id in condition
            'Include voucher id in condition only when multiple voucher ids,otherwise for single id (double click on search post grid) just position the voucher 
            If IsCallFromSearchPostScreen = True AndAlso Len(StrVoucherIDs) > 0 Then
                Condition = Condition & " AND Voucher_Id in (" & StrVoucherIDs & ")"
            End If

            'If company is selected then include it in Condition
            'CR#300
            If Me.cboCompany.SelectedIndex = 0 Then
                MessageBox.Show("Please select a valid company")
                Exit Sub
                Me.cboCompany.Focus()
            End If


            If Me.cboCompany.SelectedIndex <> 0 Then

                If IsTempVoucher = False Then
                    'CR#300
                    'Condition = Condition & "  AND (tblGlVoucher.location_id = " & pbLocationID & ") "
                    Condition = Condition & "  AND (tblGlVoucher.location_id = " & Me.cboCompany.SelectedValue & ") "
                Else
                    'CR#300
                    'Condition = Condition & "  AND (tblGlVoucherTemp.location_id = " & pbLocationID & ") "
                    Condition = Condition & "  AND (tblGlVoucherTemp.location_id = " & Me.cboCompany.SelectedValue & ") "
                End If

            End If

            'CR#310
            'Dim ArrFYear() As String = gObjFinancialYearInfo.YearCode.Split("-")

            'Dim CurrYear As Integer
            'If Not (gObjFinancialYearInfo.StartDate.Month = 7) Then 'If Fyear is from 01 Jan to 31 Dec
            '    CurrYear = Convert.ToInt64(ArrFYear(0))
            'Else 'If Fyear is from 01 Jul to 30 jun

            '    If (DateTime.Now.Month < 7) Then
            '        CurrYear = Convert.ToInt64(ArrFYear(1))
            '    Else
            '        CurrYear = Convert.ToInt64(ArrFYear(0))
            '    End If
            'End If

            'Dim DefStartDate As DateTime
            'Dim DefEndDate As DateTime
            'DefEndDate = New Date(CurrYear, Date.Now.Month, Date.Now.Day)    '---end date will be current date
            'DefStartDate = DefEndDate.AddMonths(-1) 'Date.Now.AddMonths(-1)   '--start date will be current date -30 days

            ''if start date is less then Financial Year start date then set StartDate=FYearStart Date
            'If DefStartDate < gObjFinancialYearInfo.StartDate Then
            '    DefStartDate = gObjFinancialYearInfo.StartDate
            'End If
            'End......CR#310
            'Dim dt As DataTable = New VoucherDAL().GetAll(pbFYearID, Format(pbFYearStartDate, "dd/MM/yyyy"), Format(pbFYearEndDate, "dd/MM/yyyy"), Condition, IsTempVoucher)
            'CR#310
            Dim dt As DataTable = New VoucherDAL().GetAll(pbFYearID, Format(pbFYearStartDate, "dd/MMM/yyyy"), Format(pbFYearEndDate, "dd/MMM/yyyy"), Condition, IsTempVoucher)
            'Dim dt As DataTable = New VoucherDAL().GetAll(pbFYearID, Format(DefStartDate, "dd/MMM/yyyy"), Format(DefEndDate, "dd/MMM/yyyy"), Condition, IsTempVoucher)

            'Binding grid with data table
            Me.grdVoucher.DataSource = dt
            'property will happen to implement datatable structure on grid
            Me.grdVoucher.RetrieveStructure()

            If Not StrVoucherIDs Is Nothing AndAlso StrVoucherIDs.Trim.Length > 0 AndAlso StrVoucherIDs.Contains(",") = False Then

                ''to select Voucher ID only in case of single voucher selected (double click on search post grid) just position the voucher 
                'CR#310
                'For Rind As Int16 = 0 To (grdVoucher.RowCount - 1)
                For Rind As Integer = 0 To (grdVoucher.RowCount - 1)
                    If Me.grdVoucher.GetRow(Rind).Cells(EnumMasterGrid.VoucherID).Value = StrVoucherIDs Then
                        Me.grdVoucher.Row = Rind
                        Exit For
                    End If
                Next

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''This procedure will be used to set the formatting of the grid on that form. For Example, Grid's columns show/Hide,
    '' Caption setting, columns' width etc.
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try

            ''Columns In-visible
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.VoucherDtlID).Visible = False
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.COADtlID).Visible = False
            'Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.CostCenterID).Visible = False
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.CashBank).Visible = False
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.Action).Visible = False

            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DebitAmount).FormatString = "n"
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.CreditAmount).FormatString = "n"
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DebitAmount).TotalFormatString = "n"
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.CreditAmount).TotalFormatString = "n"
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.Delete).FormatString = ""
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DebitAmount).MaxLength = 12
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.CreditAmount).MaxLength = 12
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.Comments).MaxLength = 500

            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DtlCode).Width = 100
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DtlTitle).Width = 180
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.Comments).Width = 300
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.CostCenter).Width = 100
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DebitAmount).Width = 70
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DebitAmount).Width = 70


            Me.grdDetailEntry.TotalRowFormatStyle.BackColor = gobjRequiredFieldtBKColor

            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DtlCode).InputMask = "99-999-999-99999"

            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DebitAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.CreditAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.Delete).ColumnType = Janus.Windows.GridEX.ColumnType.Link

            Me.grdDetailEntry.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True

            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.CostCenter).EditType = Janus.Windows.GridEX.EditType.DropDownList
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.CostCenter).HasValueList = True

            'Populating Cost Center List 
            Dim dtCostCenterList As DataTable

            dtCostCenterList = New VoucherDAL().GetCostCenter()
            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.CostCenter).ValueList.PopulateValueList(dtCostCenterList.DefaultView, "Cost Center ID", "Cost Center Title")

            Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DtlTitle).EditType = Janus.Windows.GridEX.EditType.NoEdit

            'CR#119
            Me.grdDetailEntry.RootTable.SortKeys.Clear()

            If Not DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") Is Nothing AndAlso DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting").ToString <> "" Then
                If Convert.ToBoolean(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = True Then

                    '  If Me.grdDetailEntry.RecordCount > 0 Then
                    Me.grdDetailEntry.RootTable.SortKeys.Add(New Janus.Windows.GridEX.GridEXSortKey(Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DebitAmount), Janus.Windows.GridEX.SortOrder.Descending))
                    ' End If

                End If
            Else
                Me.grdDetailEntry.RootTable.SortKeys.Add(New Janus.Windows.GridEX.GridEXSortKey(Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DebitAmount), Janus.Windows.GridEX.SortOrder.Descending))
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''This procedure will be used (if applicable) to set Active/Deactive or Visible/Invisible some controls on form,
    ''which are based on System level configuration
    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting
        Try

            If IsTempVoucher = False Then
                chkOtherVoucher.Visible = gblnShowOtherVoucher
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''This procedure will be used to set the navigation buttons as per Mode
    Public Sub SetNavigationButtons(ByVal mode As EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
        Try

            If mode = EnumDataMode.[New] Then
                ''if New Mode then Set Disable all Navigation Buttons
                btnFirst.Enabled = False ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnPrevious.Enabled = False ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnNext.Enabled = False ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnLast.Enabled = False ': btnLast.VisualStyle = C1.Win.C1Input.VisualStyle.System

            ElseIf mode = EnumDataMode.Edit Then
                ''if New Mode then Set Enable all Navigation Buttons
                btnFirst.Enabled = True ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnPrevious.Enabled = True ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnNext.Enabled = True ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnLast.Enabled = True ': btnLast.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''here we will clear all the contols of the screen for New Mode
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

        Try

            StrVoucherIDs = String.Empty
            Me.cboVoucherType.Enabled = True
            'CR#300
            Me.cboCompany.Enabled = True
            Me.cboSource.SelectedValue = strSourceOfVoucher
            'CR#300 End

            BlnPostedVoucher = False
            strSourceOfVoucher = "Accounts"
            ListOfDeletedIDs = String.Empty
            intTempVoucherID = 0

            Me.cboVoucherType.Enabled = True
            'CR#300
            Me.cboCompany.Enabled = True

            'CR#321 Voucher Type should remain same, it should not clear.. 
            'If Me.cboVoucherType.Items.Count > 0 Then Me.cboVoucherType.SelectedIndex = 0

            Me.txtFYearCode.Text = pbFYearCode

            'CR#300
            If Me.cboCompany.SelectedIndex = 0 Then
                MessageBox.Show("Please select a valid company")
                Exit Sub
                Me.cboCompany.Focus()
            End If


            If Me.cboCompany.SelectedIndex > 1 Then
                'CR#300
                ' Me.cboCompany.Text = "[" & pbLocationID & "]" & pbLocationID
            End If

            Me.dtpVoucherDate.Value = New VoucherDAL().GetDBCurrentDate()
            Me.dtpDueDate.Value = New VoucherDAL().GetDBCurrentDate()
            Me.dtpChequeDate.Value = New VoucherDAL().GetDBCurrentDate

            ''CR#65
            _CurrMonth = Me.dtpVoucherDate.Value.Month
            _CurrYear = Me.dtpVoucherDate.Value.Year

            Me.txtVNo.Text = String.Empty
            Me.txtChequeNo.Text = String.Empty
            Me.dtpChequeDate.Checked = False
            Me.dtpDueDate.Checked = False
            Me.txtVoucherNarration.Text = String.Empty
            Me.txtAmount.Text = String.Empty
            Me.chkOtherVoucher.Checked = False

            Me.grdDetailEntry.DataSource = Nothing
            Dim DtVoucherDetail As DataTable
            DtVoucherDetail = New VoucherDAL().GetDetailofVoucher(0, 0, " And 1=0", IsTempVoucher)
            Me.grdDetailEntry.DataSource = DtVoucherDetail
            Me.grdDetailEntry.RetrieveStructure()
            ApplyGridSettings()

            Me.ctrlGLAccounts.txtACCode.Text = String.Empty
            Me.ctrlGLAccounts.txtAccountName.Text = String.Empty
            Me.lblPosted.Text = String.Empty
            Me.lblVoucherString.Text = String.Empty

            Me.intPkId = 0
            Me.intLocId = gobjLocationInfo.CompanyID

            If gObjFinancialYearInfo.Status = G_STATUS_CLOSE Then
                Call ApplySecurity(EnumDataMode.Disabled)
            Else
                ''Set New Mode and Applying Security Setting
                Call ApplySecurity(EnumDataMode.[New])
            End If

            Me.cboVoucherType.Focus()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''Here we will pass an argument MODE (New|Edit|Disabled), which will be overwritten according to the rights 
    ''available to user on that screen
    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try

            'CR#88
            If mobjControlList.Item("dtpVoucherDate") Is Nothing Then
                Me.dtpVoucherDate.Enabled = True
            Else
                Me.dtpVoucherDate.Enabled = False
            End If

            'CR#300 start.. Company and source drop down are right based now
            If Not mobjControlList.Item("cboCompany") Is Nothing Then
                Me.cboCompany.Enabled = True
            Else
                Me.cboCompany.Enabled = False
            End If

            If Not mobjControlList.Item("cboSource") Is Nothing Then
                Me.cboSource.Enabled = True
            Else
                Me.cboSource.Enabled = False
            End If
            'CR#300 End

            
            If Mode.ToString = EnumDataMode.Disabled.ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System           
                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(EnumDataMode.Edit)
                Me.grdDetailEntry.Enabled = False
                'change buy farooq - H  ''// 323     23 Jul,2014          farooq-H  

                Me.BtnCheckPrint.Enabled = True
            ElseIf Mode.ToString = EnumDataMode.[New].ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System

                If mobjControlList.Item("btnSave") Is Nothing Then
                    btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
                Else
                    btnSave.Enabled = True ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                End If

                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = True ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(Mode)

                Me.grdDetailEntry.Enabled = True
                'change buy farooq - H  ''// 323     23 Jul,2014          farooq-H  

                Me.BtnCheckPrint.Enabled = False
                ' Me.grdDetailEntry.RootTable.SortKeys.Clear()
                ' Me.grdDetailEntry.RootTable.SortKeys.Add(New Janus.Windows.GridEX.GridEXSortKey(Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DebitAmount), Janus.Windows.GridEX.SortOrder.Descending))

            ElseIf Mode.ToString = EnumDataMode.Edit.ToString Then

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System

                If mobjControlList.Item("btnUpdate") Is Nothing Then
                    btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                Else
                    btnUpdate.Enabled = True ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                End If

                If mobjControlList.Item("btnDelete") Is Nothing Then
                    btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                Else
                    btnDelete.Enabled = True ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                End If
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

                SetNavigationButtons(Mode)

                Me.grdDetailEntry.Enabled = True

                Me.grdDetailEntry.Focus()

                'change buy farooq - H  ''// 323     23 Jul,2014          farooq-H  

                Me.BtnCheckPrint.Enabled = True




            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

                SetNavigationButtons(Mode)

                Me.grdDetailEntry.Enabled = True

                Me.grdDetailEntry.Focus()
                'change buy farooq - H  ''// 323     23 Jul,2014          farooq-H  

                Me.BtnCheckPrint.Enabled = False

            End If


            '' Disabl/Enable the Button that converts Grid data in Excel Sheet According to Login User rights
            If mobjControlList.Item("btnExport") Is Nothing Then
                Me.UiCtrlGridBar1.btnExport.Enabled = False
            End If


            '' Disabl/Enable the Button that Prints Grid data According to Login User rights
            If mobjControlList.Item("btnPrint") Is Nothing Then
                Me.UiCtrlGridBar1.btnPrint.Enabled = False
            End If

            If mobjControlList.Item("btnPrintInvoice") Is Nothing Then
                Me.btnPrintInvoice.Enabled = False                      '268    Rana Saeed (rights to print PO or SI)
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''Here we will apply Front End Validations.
    Public Function IsValidate(Optional ByVal Mode As EnumDataMode = EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        Try

            If Mode = EnumDataMode.[New] Or Mode = EnumDataMode.Edit Then
                'There must be entry of eit her Debit ot Credit
                If GridContainValidEntries() = False Then
                    Return False
                End If

                If Me.cboCompany.SelectedIndex = 0 Then
                    ShowErrorMessage("Please select a company")
                    Me.cboCompany.Focus()
                    Return False
                End If

                If Me.cboSource.SelectedIndex = 0 Then
                    ShowErrorMessage("Please select a source")
                    Me.cboSource.Focus()
                    Return False
                End If

            End If

            Me.FillModel()

            If Condition = "BackEndDeleteValidation" Then

                If FunAllowUpdateDelete() = False Then
                    Return False
                End If

            End If

            If Mode = EnumDataMode.[New] Or Mode = EnumDataMode.Edit Then

                If FrontEndValidation() = False Then
                    Return False
                End If

                'Check Trial Version 
                If G_blnTrialVersion Then

                    If New VoucherDAL().ISTrialVersionExpired(pbFYearID) Then
                        ShowErrorMessage("LS-Financials Trial period is expired; please contact LumenSoft Technologies for Full Version")
                        Return False
                    End If

                End If

                'Validation will apply for only Update Mode
                If Mode = EnumDataMode.Edit Then

                    If FunAllowUpdateDelete() = False Then
                        Return False
                    End If

                End If


                'Prompt if voucher is unbalanced
                Dim DebitAmt As Double
                Dim CreditAmt As Double

                DebitAmt = DebitTotal()
                CreditAmt = CreditTotal()

                If DebitAmt <> CreditAmt Then

                    If MessageBox.Show("Voucher is un-balanced. Do you want to proceed", "Un-balanced Voucher", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                        Return False
                    End If

                End If

                If Me.cboVoucherType.Text = "BRV" Or Me.cboVoucherType.Text = "BPV" Or Me.cboVoucherType.Text = "CRV" Or Me.cboVoucherType.Text = "CPV" Then

                    ' Me.ctrlGLAccounts.AccountType = EnumAccountTypes.Cash

                    Dim strSQL As String
                    'CR#310
                    'Dim intBankCounter, intCashCounter As Int16
                    Dim intBankCounter, intCashCounter As Integer
                    Dim ObjDataRow As DataRow



                    Dim ObjGridDataTable As DataTable = CType(Me.grdDetailEntry.DataSource, DataTable)
                    'CR#228
                    ObjGridDataTable.AcceptChanges()

                    intBankCounter = 0
                    intCashCounter = 0

                    For Each r As DataRow In ObjGridDataTable.Rows

                        Dim mObjDtlModal As New VoucherDetailItem

                        With mObjDtlModal
                            .COADetailID = r.Item(EnumGridVoucher.COADtlID).ToString
                            strSQL = " SELECT dbo.tblGlCOAMainSubSub.account_type FROM dbo.tblGlCOAMainSubSubDetail INNER JOIN " _
                                   & " dbo.tblGlCOAMainSubSub ON dbo.tblGlCOAMainSubSubDetail.main_sub_sub_id = dbo.tblGlCOAMainSubSub.main_sub_sub_id " _
                                   & " WHERE dbo.tblGlCOAMainSubSubDetail.coa_detail_id = " & .COADetailID & ""

                            ObjDataRow = UtilityDAL.ReturnDataRow(strSQL)

                            If ObjDataRow.Item("account_type").ToString = "Bank" Then
                                intBankCounter = intBankCounter + 1

                            End If

                            If ObjDataRow.Item("account_type").ToString = "Cash" Then
                                intCashCounter = intCashCounter + 1

                            End If

                        End With

                    Next

                    'If intBankCounter >= 2 Or intCashCounter >= 2 Then
                    'COde commented against CR # 343
                    'System should allow Multiple banks in BPV and BRV
                    'If intBankCounter >= 2 Then
                    '    'ShowValidationMessage("Two or more Cash/Bank accounts not allowed")
                    '    ShowValidationMessage("Two or more Bank accounts not allowed")
                    '    Return False
                    'End If

                End If

            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''Here we will create an instance of the class, according to the form, and will set the properties of the object
    ''Later this object will be refered in Save|Update|Delete function.

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

        Try
            ''Create Model object
            mobjModel = New GLVoucher

            Dim objVoucher As VoucherDAL
            Dim strVoucherType As String
            Dim VNo As String
            Dim VCode As String
            Dim LocCode As String
            Dim DebitAmt As Double
            Dim CreditAmt As Double

            With mobjModel

                objVoucher = New VoucherDAL()

                'CR#300
                If Me.cboCompany.SelectedIndex = 0 Then
                    MessageBox.Show("Please select a valid company")
                    Exit Sub
                    Me.cboCompany.Focus()
                End If

                strVoucherType = objVoucher.GetGLVType(Me.cboVoucherType.SelectedValue)
                'CR98: Temp Voucher Posting Code
                If Condition = "POSTING" AndAlso IsTempVoucher = True Then
                    'CR#300
                    'VNo = objVoucher.GetNewVoucherNo(pbLocationID, Me.cboVoucherType.SelectedValue.ToString, Format(Me.dtpVoucherDate.Value, "MMM"), Format(Me.dtpVoucherDate.Value, "yyyy"), Me.chkOtherVoucher.Checked, False)
                    VNo = objVoucher.GetNewVoucherNo(Me.cboVoucherType.SelectedValue, Me.cboVoucherType.SelectedValue.ToString, Format(Me.dtpVoucherDate.Value, "MMM"), Format(Me.dtpVoucherDate.Value, "yyyy"), Me.chkOtherVoucher.Checked, False)
                Else
                    'CR#300
                    'VNo = objVoucher.GetNewVoucherNo(pbLocationID, Me.cboVoucherType.SelectedValue.ToString, Format(Me.dtpVoucherDate.Value, "MMM"), Format(Me.dtpVoucherDate.Value, "yyyy"), Me.chkOtherVoucher.Checked, IsTempVoucher)
                    VNo = objVoucher.GetNewVoucherNo(Me.cboVoucherType.SelectedValue, Me.cboVoucherType.SelectedValue.ToString, Format(Me.dtpVoucherDate.Value, "MMM"), Format(Me.dtpVoucherDate.Value, "yyyy"), Me.chkOtherVoucher.Checked, IsTempVoucher)
                End If

                'replace the voucher no from '1' to '000001'
                VNo = funDoPadding(Me.txtVNo.Text, Me.txtVNo.MaxLength)
                'CR#300
                'LocCode = objVoucher.GetLocationCode(pbLocationID)
                LocCode = objVoucher.GetLocationCode(Me.cboCompany.SelectedValue)
                'Making Voucher Code
                VCode = Format(Me.dtpVoucherDate.Value, "yyyy") & "-" & strVoucherType & "-" & LocCode & "-" & "ACC" & "-" & Format(Me.dtpVoucherDate.Value, "MMM") & "-" & VNo
                .voucherID = intPkId
                .LocationCode = funFilterReserveText(LocCode)
                'CR#300
                '.LocationID = pbLocationID
                .LocationID = Me.cboCompany.SelectedValue
                .VoucherCode = VCode
                .FiniancialYearID = pbFYearID
                .VoucherTypeID = Me.cboVoucherType.SelectedValue
                .VoucherMonth = funFilterReserveText(Format(Me.dtpVoucherDate.Value, "MMM"))
                .VoucherNo = VNo
                .VoucherDate = Me.dtpVoucherDate.Value
                .VoucherNarration = funFilterReserveText(Me.txtVoucherNarration.Text)
                .CashBankAccID = Me.ctrlGLAccounts.GLAccountID
                .ChequeNo = Me.txtChequeNo.Text
                .ChequeDate = IIf(Me.dtpChequeDate.Checked = False, Date.MinValue, Me.dtpChequeDate.Value)
                .DueDate = IIf(Me.dtpDueDate.Checked = False, Date.MinValue, Me.dtpDueDate.Value)
                .OtherVoucher = IIf(Me.chkOtherVoucher.Checked = True, True, False)
                'CR#300
                '.Source = strSourceOfVoucher
                .Source = Me.cboSource.Text

                .ListOfDeletedIDs = ListOfDeletedIDs
                .TempVoucherID = intTempVoucherID
                .VNoMaxLength = Me.txtVNo.MaxLength

                '''Filling Activity Log for Lod Viewer

                .ActivityLog.UserID = gObjUserInfo.UserID
                .ActivityLog.LogGroup = gObjUserInfo.GroupInfo.GroupName
                .ActivityLog.LogRef = VNo
                If IsTempVoucher Then
                    .ActivityLog.ScreenTitle = "Temporary Voucher"
                Else
                    .ActivityLog.ScreenTitle = "Voucher"
                End If

                .ActivityLog.LogGroup = "Transactions"
                .ActivityLog.RefType = "Voucher Number"
                '--------------------------------------

                'Checking Voucher is Balanced or unBalanced
                DebitAmt = DebitTotal()
                CreditAmt = CreditTotal()

                If DebitAmt <> CreditAmt Then
                    .IsBalancedVoucher = False
                Else
                    .IsBalancedVoucher = True
                End If

                If _BlnSaveV = True Then
                    .BlnSaveVInActualTables = True
                Else
                    .BlnSaveVInActualTables = False
                End If

                Dim DtItems As DataTable = CType(Me.grdDetailEntry.DataSource, DataTable)

                'CR#119
                If Not DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") Is Nothing AndAlso DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting").ToString <> "" Then
                    If Convert.ToBoolean(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = True Then

                        Dim dv As DataView = DtItems.DefaultView
                        dv.Sort = "Debit desc"

                        'Dim dtSorted As DataTable = dv.ToTable()
                        DtItems = dv.ToTable()

                    End If
                End If


                DtItems.AcceptChanges()

                For Each r As DataRow In DtItems.Rows

                    Dim mObjDtlModal As New VoucherDetailItem

                    With mObjDtlModal

                        .COADetailID = r.Item(EnumGridVoucher.COADtlID).ToString
                        .VoucherDetailID = IIf(r.Item(EnumGridVoucher.VoucherDtlID).ToString = String.Empty, 0, Val(r.Item(EnumGridVoucher.VoucherDtlID).ToString))
                        .Comments = funFilterReserveText(r.Item(EnumGridVoucher.Comments).ToString)
                        .DebitAmount = IIf(Val(r.Item(EnumGridVoucher.DebitAmount).ToString) <= 0, 0, r.Item(EnumGridVoucher.DebitAmount).ToString)
                        .CreditAmount = IIf(Val(r.Item(EnumGridVoucher.CreditAmount).ToString) <= 0, 0, r.Item(EnumGridVoucher.CreditAmount).ToString)
                        '.CostCenterID = r.Item(EnumGridVoucher.CostCenterID).ToString
                        .CostCenterID = IIf(r.Item(EnumGridVoucher.CostCenter).ToString <> "" AndAlso r.Item(EnumGridVoucher.CostCenter).ToString <> "---Select---", r.Item(EnumGridVoucher.CostCenter).ToString, 0) 'Me.grdProducts.GetRow(rowIndex).Cells(5).Value.ToString

                    End With


                    .ListofVouchers.Add(mObjDtlModal)

                Next

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''Here we will call DAL Function for SAVE, and if the function successfully Saves the records
    ''then the function will return True, otherwise returns False
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            'Trial Version check
            'if it is trial version then check no of trans, if greater than or equat to 25 then exit
            If gblnTrialVersion Then
                If New VoucherDAL().TrialPeriodExpired(pbFYearID, _gintMaxTrialTransactions) = True Then

                    Throw New Exception("LS-Financials Trial period is expired; please contact LumenSoft Technologies for Full Version")
                    Exit Function

                End If
            End If

            ''Applying Validation Checks
            If Me.IsValidate(EnumDataMode.[New]) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes

                'Confirmation from user for Creation of New FYear
                If gblnShowSaveConfirmationMessages Then
                    ''Getting Save Confirmation from User
                    result = ShowConfirmationMessage(gstrMsgSave, MessageBoxDefaultButton.Button1)
                End If

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Add Method by passing Model Object
                    If New VoucherDAL().Add(Me.mobjModel, IsTempVoucher) Then
                        'change by Farooq-H  CR# 254 
                        SMS(Me.mobjModel, EnumSMSAction.Save.ToString)
                        'Dim mObjDtlModal As New VoucherDetailItem
                        'Dim ListCount As Integer = mobjModel.ListofVouchers.Count
                        'Dim LoopCount As Integer = 0
                        'Dim TotalDabit As Double = 0.0
                        'Dim AccountID As Integer = 0
                        'For LoopCount = 0 To ListCount - 1
                        '    mObjDtlModal = mobjModel.ListofVouchers(LoopCount)
                        '    TotalDabit = TotalDabit + mObjDtlModal.DebitAmount
                        '    If New VoucherDAL().GetAccountTypeName(mObjDtlModal.COADetailID) Then
                        '        AccountID = mObjDtlModal.COADetailID
                        '    End If
                        'Next
                        'If Me.cboVoucherType.Text = EnumVType.BPV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BPV_03.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.BRV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BRV_03.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.CPV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CPV_03.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.CRV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CRV_03.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.PV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_PV_03.ToString & ";" & EnumSMSCodes.SMS_VE_PV_06.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ", AccountID)
                        'ElseIf Me.cboVoucherType.Text = EnumVType.SV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_SV_03.ToString & ";" & EnumSMSCodes.SMS_VE_SV_06.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ", AccountID)
                        'ElseIf Me.cboVoucherType.Text = EnumVType.JV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_JV_03.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'End If

                        If Not New CompanyDAL().FYearExist() Then

                            ShowErrorMessage("Now define Financial Year")
                            frmDefFiniancialYear.MdiParent = MDIParent1
                            ' Call Me.ShowForm(frmDefFiniancialYear)
                        End If

                        StrVoucherIDs = String.Empty
                        'Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()
                        Me.cboVoucherType.Enabled = False
                        'CR#300
                        Me.cboCompany.Enabled = False
                        ''to select the last updated record
                        'CR#310
                        'For Rind As Int16 = 0 To (grdVoucher.RowCount - 1)
                        For Rind As Integer = 0 To (grdVoucher.RowCount - 1)
                            If Me.grdVoucher.GetRow(Rind).Cells(EnumMasterGrid.VoucherID).Value = mobjModel.voucherID Then
                                Me.grdVoucher.Row = Rind
                                Exit For
                            End If
                        Next

                        Me.ApplySecurity(EnumDataMode.Edit)

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

    ''Here we will call DAL Function for Update the selected record, and if the function successfully Updates the records
    ''then the function will return True, otherwise returns False
    Public Function Update(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

        Try

            'Trial Version check
            'if it is trial version then check no of trans, if greater than or equat to 25 then exit
            If gblnTrialVersion Then
                If New VoucherDAL().TrialPeriodExpired(pbFYearID, _gintMaxTrialTransactions) = True Then

                    Throw New Exception("LS-Financials Trial period is expired; please contact LumenSoft Technologies for Full Version")
                    Exit Function

                End If
            End If


            ''Applying Front End Validation Checks
            If Me.IsValidate(EnumDataMode.Edit) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes
                ''Getting Save Confirmation from User
                result = ShowConfirmationMessage(gstrMsgUpdate, MessageBoxDefaultButton.Button1)

                If result = Windows.Forms.DialogResult.Yes Then

                    ''Create a DAL Object and calls its Update Method by passing Model Object
                    If New VoucherDAL().Update(Me.mobjModel, IsTempVoucher) Then
                        'change by Farooq-H  CR# 254 
                        SMS(Me.mobjModel, EnumSMSAction.Update.ToString)
                        'Dim mObjDtlModal As New VoucherDetailItem
                        'Dim ListCount As Integer = mobjModel.ListofVouchers.Count
                        'Dim LoopCount As Integer = 0
                        'Dim TotalDabit As Double = 0.0
                        'Dim AccountID As Integer = 0
                        'For LoopCount = 0 To ListCount - 1
                        '    mObjDtlModal = mobjModel.ListofVouchers(LoopCount)
                        '    TotalDabit = TotalDabit + mObjDtlModal.DebitAmount
                        '    If New VoucherDAL().GetAccountTypeName(mObjDtlModal.COADetailID) Then
                        '        AccountID = mObjDtlModal.COADetailID
                        '    End If
                        'Next
                        'If Me.cboVoucherType.Text = EnumVType.BPV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BPV_04.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.BRV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BRV_04.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.CPV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CPV_04.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.CRV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CRV_04.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.PV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_PV_04.ToString & ";" & EnumSMSCodes.SMS_VE_PV_07.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ", AccountID)
                        'ElseIf Me.cboVoucherType.Text = EnumVType.SV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_SV_04.ToString & ";" & EnumSMSCodes.SMS_VE_SV_07.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ", AccountID)
                        'ElseIf Me.cboVoucherType.Text = EnumVType.JV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_JV_04.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'End If
                        ' StrVoucherIDs = String.Empty

                        ' Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()
                        Me.cboVoucherType.Enabled = False
                        'CR#300
                        Me.cboCompany.Enabled = False

                        'to select the last updated record
                        'CR#310
                        'For Rind As Int16 = 0 To (Me.grdVoucher.RowCount - 1)
                        For Rind As Integer = 0 To (Me.grdVoucher.RowCount - 1)
                            If Me.grdVoucher.GetRow(Rind).Cells(EnumMasterGrid.VoucherID).Value = mobjModel.voucherID Then
                                Me.grdVoucher.Row = Rind
                                Exit For
                            End If
                        Next

                        grdVoucher_SelectionChanged(Nothing, Nothing)

                        If gblnShowAfterUpdateMessages Then
                            ''Getting Save Confirmation from User
                            ShowInformationMessage(gstrMsgAfterUpdate)
                        End If

                        Me.cboVoucherType.Focus()

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

    ''Here we will call DAL Function for Delete the selected record, and if the function successfully Deletes the records
    ''then the function will return True, otherwise returns False
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

            ''Applying Front End Validation Checks
            If Me.IsValidate(, "BackEndDeleteValidation") Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes
                ''Getting Save Confirmation from User
                result = ShowConfirmationMessage(gstrMsgDelete, MessageBoxDefaultButton.Button2)

                Me.Cursor = Cursors.WaitCursor

                If result = Windows.Forms.DialogResult.Yes Then

                    If New VoucherDAL().Deleted(Me.mobjModel, IsTempVoucher) Then
                        'change by Farooq-H  CR# 254 
                        SMS(Me.mobjModel, EnumSMSAction.Delete.ToString)
                        'Dim mObjDtlModal As New VoucherDetailItem
                        'Dim ListCount As Integer = mobjModel.ListofVouchers.Count
                        'Dim LoopCount As Integer = 0
                        'Dim TotalDabit As Double = 0.0
                        'Dim AccountID As Integer = 0
                        'For LoopCount = 0 To ListCount - 1
                        '    mObjDtlModal = mobjModel.ListofVouchers(LoopCount)
                        '    TotalDabit = TotalDabit + mObjDtlModal.DebitAmount
                        '    If New VoucherDAL().GetAccountTypeName(mObjDtlModal.COADetailID) Then
                        '        AccountID = mObjDtlModal.COADetailID
                        '    End If
                        'Next
                        'If Me.cboVoucherType.Text = EnumVType.BPV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BPV_05.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.BRV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BRV_05.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.CPV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CPV_05.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.CRV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CRV_05.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.PV.ToString Then

                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_PV_05.ToString & ";" & EnumSMSCodes.SMS_VE_PV_08.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ", AccountID)
                        'ElseIf Me.cboVoucherType.Text = EnumVType.SV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_SV_05.ToString & ";" & EnumSMSCodes.SMS_VE_SV_08.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ", AccountID)
                        'ElseIf Me.cboVoucherType.Text = EnumVType.JV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_JV_05.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'End If
                        ''This will hold row index of the selected row 
                        Dim intGridRowIndex As Integer
                        intGridRowIndex = Me.grdDetailEntry.Row

                        ''Query to Database and get fressh modifications in the Grid
                        Me.GetAllRecords()
                        Me.cboVoucherType.Enabled = False
                        'CR#300
                        Me.cboCompany.Enabled = False

                        ''Call RowColumn Change Event
                        If Me.grdVoucher.RecordCount > 0 Then

                            grdVoucher_SelectionChanged(Nothing, Nothing)

                            ''Reset the row index to the grid
                            If intGridRowIndex > (Me.grdVoucher.RowCount - 1) Then intGridRowIndex = (Me.grdVoucher.RowCount - 1)
                            If Not intGridRowIndex < 0 Then Me.grdVoucher.Row = intGridRowIndex

                        Else

                            ReSetControls()

                        End If

                    End If
                End If

            End If


        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Function

#End Region

#Region "Local Functions and Procedures"
    '/////////Getting Voucher Detail against Voucher Id & Location ID//////////////
    Private Sub GetVoucherDetail()
        Dim DtVoucherDetail As DataTable

        Try

            DtVoucherDetail = New VoucherDAL().GetDetailofVoucher(intPkId, intLocId, , IsTempVoucher)

            'Abdul Jabbar--Issue found during testing of cheque printing, when bank account is not on first line then unappropriate amount and bank info selected in master info fields plus cheque printing amount box
            Try

            
                If Me.grpCashBankInfo.Enabled = True AndAlso Me.cboVoucherType.Text = "BPV" Then
                    Dim dtSortedVouchersDtl As DataTable
                    dtSortedVouchersDtl = New DataTable
                    DtVoucherDetail = SortDataTable(DtVoucherDetail, "Credit", False)

                    Me.ctrlGLAccounts.txtACCode.Text = DtVoucherDetail.Rows(0).Item(EnumGridVoucher.DtlCode).ToString()
                    Me.ctrlGLAccounts.GLAccountID = IIf(DtVoucherDetail.Rows(0).Item(EnumGridVoucher.COADtlID).ToString() <> "", DtVoucherDetail.Rows(0).Item(EnumGridVoucher.COADtlID).ToString(), 0)
                    Me.ctrlGLAccounts.txtAccountName.Text = DtVoucherDetail.Rows(0).Item(EnumGridVoucher.DtlTitle).ToString()

                End If

            Catch ex As Exception

            End Try

            'Binding grid with data table
            Me.grdDetailEntry.DataSource = DtVoucherDetail

            'property will happen to implement datatable structure on grid
            Me.grdDetailEntry.RetrieveStructure()
            'Applying grid formating
            Me.ApplyGridSettings()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ctrlGLAccounts_GetGLAccount(ByVal sender As uiCtrlGLAccount)
        Try

            'Me.txtTitle.Text = Me.ctrlGLAccounts.GLAccountName
            LngCashBankAccID = Me.ctrlGLAccounts.GLAccountID
          

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub NewButtonClick()
        Try

            Me.ReSetControls()
            Me.ApplySecurity(EnumDataMode.[New])

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveButtonClick()
        Try


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateButtonClick()
        Try


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CanelButtonClick()
        Try



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function funDoPadding(ByVal strSource As String, ByVal intLength As Integer) As String
        Try

            If strSource <> "" Then
                funDoPadding = New String("0", (intLength - Len(strSource))) & strSource
            Else
                funDoPadding = New String("0", intLength)
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function funFilterReserveText(ByVal Txt As String) As String
        Try

            funFilterReserveText = Replace(Txt, "'", "''", , , vbTextCompare)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function CreditTotal() As Double

        Try

            Dim dblCredit As Double = 0
            If Me.grdDetailEntry.RowCount > 0 Then
                dblCredit = Math.Round(Val(Me.grdDetailEntry.GetTotal(Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.CreditAmount), Janus.Windows.GridEX.AggregateFunction.Sum)))
            End If

            Return dblCredit

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function DebitTotal() As Double

        Try

            Dim dblDebit As Double = 0
            If Me.grdDetailEntry.RowCount > 0 Then
                dblDebit = Math.Round(Val(Me.grdDetailEntry.GetTotal(Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DebitAmount), Janus.Windows.GridEX.AggregateFunction.Sum)))
            End If

            Return dblDebit

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub ControlSettingS()

        Try


            Dim FontStyle As Font
            FontStyle = New Font(New FontFamily("Microsoft Sans Serif"), 8, Drawing.FontStyle.Bold)
            Me.lblTotalRecord.Font = FontStyle
            Me.lblDrCr.Font = FontStyle
            'CR#300
            'Me.txtCompanyName.Text = pbLocationName
            Me.lblPosted.Font = FontStyle
            Me.lblPosted.BackColor = Color.White
            Me.chkOtherVoucher.Font = FontStyle
            Me.lblVoucherString.Font = FontStyle
            Me.lblVoucherString.BackColor = Color.White
            Me.cmdSearchVoucher.Font = FontStyle
            Me.cmdPostVoucher.Font = FontStyle

            'If IsTempVoucher = True Then
            '    Me.txtVoucherOfType.Text = "Temporary Voucher"
            'Else
            '    Me.txtVoucherOfType.Text = "Journal Voucher"
            'End If


            Dim VTStyle As Font
            VTStyle = New Font(New FontFamily("Microsoft Sans Serif"), 18, Drawing.FontStyle.Bold)
            'Me.txtVoucherOfType.Font = VTStyle

            Dim VTScreentext As Font
            VTScreentext = New Font(New FontFamily("Microsoft Sans Serif"), 14, Drawing.FontStyle.Bold)
            Me.lblScreentext.Font = VTScreentext

            If _TempVouchers = False Then
                Me.lblScreentext.Text = "Voucher Entry"
            Else
                Me.lblScreentext.Text = "Temporary Voucher Entry"
            End If



            Dim VTgrpVoucher As Font
            VTgrpVoucher = New Font(New FontFamily("Microsoft Sans Serif"), 8, Drawing.FontStyle.Bold)
            '     Me.grpVNoDetail.Font = VTgrpVoucher
            Me.grpVoucherDtl.Font = VTgrpVoucher

            Me.grpCashBankInfo.BackColor = System.Drawing.Color.LightSteelBlue
            Me.grpCashBankInfo.Font = VTgrpVoucher
            lblFYear.Font = VTgrpVoucher
            txtFYearCode.Font = VTgrpVoucher
            lblVType.Font = VTgrpVoucher
            cboVoucherType.Font = VTgrpVoucher
            lblCompany.Font = VTgrpVoucher
            lblVNo.Font = VTgrpVoucher
            'CR#300
            'txtCompanyName.Font = VTgrpVoucher
            txtVNo.Font = VTgrpVoucher
            lblVDate.Font = VTgrpVoucher
            dtpVoucherDate.Font = VTgrpVoucher
            lblDueDate.Font = VTgrpVoucher
            dtpDueDate.Font = VTgrpVoucher
            chkOtherVoucher.Font = VTgrpVoucher

            Dim VTNormal As Font
            VTNormal = New Font(New FontFamily("Microsoft Sans Serif"), 8, Drawing.FontStyle.Regular)
            Me.grpVNoDetail.Font = VTNormal

            Me.ctrlGLAccounts.Font = VTNormal
            Me.txtVoucherNarration.Font = VTNormal
            Me.txtChequeNo.Font = VTNormal
            Me.dtpChequeDate.Font = VTNormal
            Me.lblDrCr.Font = VTgrpVoucher
            Me.lblAmount.Font = VTNormal
            Me.lblBankCashAc.Font = VTNormal
            Me.lblVNarration.Font = VTNormal
            Me.lblChequeNo.Font = VTNormal
            Me.lblAmount.Font = VTNormal

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function GridContainValidEntries() As Boolean
        Dim intCount As Integer

        Try

            intCount = 0
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdDetailEntry.GetRows

                If IsDBNull(Row.Cells(EnumGridVoucher.DebitAmount).Value) AndAlso IsDBNull(Row.Cells(EnumGridVoucher.CreditAmount).Value) Then
                    ShowErrorMessage("There must be entry of either Debit ot Credit")
                    Me.grdDetailEntry.Row = intCount
                    Me.grdDetailEntry.Col = EnumGridVoucher.DebitAmount
                    Return False
                End If

                intCount += 1

            Next


            If Me.grdDetailEntry.RecordCount < 2 Then
                ShowErrorMessage("Invalid Voucher to Save,there must be at least two transactions in voucher detail")
                Return False
            End If

            '115 
            If ValidateBankVoucher() = False Then Return False

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function FrontEndValidation() As Boolean

        Try

            If Me.cboVoucherType.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select Voucher Type")
                Me.cboVoucherType.Focus()
                Return False
            End If

            'check if entery is between the Open financial year
            'If CDate(Me.dtpVoucherDate.Value) < gObjFinancialYearInfo.StartDate Or CDate(Me.dtpVoucherDate.Value) > gObjFinancialYearInfo.EndDate Then
            If CDate(Me.dtpVoucherDate.Value) < gObjFinancialYearInfo.StartDate.Date Or CDate(Me.dtpVoucherDate.Value) > gObjFinancialYearInfo.EndDate.Date Then
                ShowErrorMessage("Voucher Date can not be greater than Financial Year End Date or less than Financial Year Start Date")
                Me.dtpVoucherDate.Focus()
                Return False
            End If

            If Me.grpCashBankInfo.Enabled = True Then

                If Me.ctrlGLAccounts.txtACCode.Text.Trim.Length = 0 Then
                    ShowErrorMessage("Cash/Bank A/C is required for Non-JV Vouchers")
                    Me.ctrlGLAccounts.txtACCode.Focus()
                    Return False

                    'ElseIf Val(Me.txtAmount.Text) = 0 Then
                    '    ShowErrorMessage("Cheque amount is missing")
                    '    If Me.txtAmount.Enabled Then Me.txtAmount.Focus()
                    '    Return False

                End If

                If Me.txtChequeNo.Text.Trim.Length = 0 AndAlso Me.dtpChequeDate.Checked Then
                    ShowErrorMessage("Cheque No. is missing")
                    Me.txtChequeNo.Focus()
                    Return False

                ElseIf Me.dtpChequeDate.Checked = False AndAlso Not Me.txtChequeNo.Text.Trim.Length = 0 Then
                    ShowErrorMessage("Cheque Date is missing")
                    Me.dtpChequeDate.Focus()
                    Return False

                End If

            End If

            If Me.grdDetailEntry.RecordCount = 0 Then
                ShowErrorMessage("No voucher transaction found to save")
                Me.grdDetailEntry.Focus()
                Return False
            End If

            '============================================================================================
            '   Last Closing Check
            '============================================================================================
            Dim strTrType As String
            Dim dtLasClosingDate As Date
            Dim strVoucherType As String = ""

            Dim objVoucher As VoucherDAL

            objVoucher = New VoucherDAL()
            strVoucherType = objVoucher.GetGLVType(Me.cboVoucherType.SelectedValue)
            dtLasClosingDate = objVoucher.GetLastClosingDate(strVoucherType)

            'Empty date 01-01-1800

            If strVoucherType = "CP" Or strVoucherType = "CR" Then

                strTrType = "Cash"
            ElseIf strVoucherType = "BP" Or strVoucherType = "BR" Then

                strTrType = "Bank"
            End If

            'If Closing date fun. returned Null then it will return date 01-01-1800
            If dtLasClosingDate < Now.Date.AddYears(-100) Then

                If Me.dtpVoucherDate.Value <= dtLasClosingDate Then

                    ShowErrorMessage("Vocher Date must be greater then Last Closing Date - [ " & dtLasClosingDate & " ]")
                    Me.dtpVoucherDate.Focus()
                    Return False
                End If

            End If

            'For Cash/Bank Voucher Type ,Cash bank info must be provided

            If Me.cboVoucherType.Text = "BRV" Or Me.cboVoucherType.Text = "CRV" Or Me.cboVoucherType.Text = "BPV" Or Me.cboVoucherType.Text = "CPV" Then
                If Me.ctrlGLAccounts.txtACCode.Text.Trim.Length <= 0 Then

                    ShowErrorMessage("Cash/Bank Account is missing")
                    Me.ctrlGLAccounts.txtACCode.Focus()
                    Return False

                End If
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub SetSelectedAccDetailinGrid()

        Try

            If Not Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode) Is Nothing Then
                If Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode).ToString <> "" Then

                    Dim DTAcc As New DataTable
                    DTAcc = New VoucherDAL().GetDetailofAccCode(Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode).ToString)

                    If DTAcc.Rows.Count > 0 Then
                        Me.grdDetailEntry.SetValue(EnumGridVoucher.COADtlID, DTAcc.Rows(0).Item("COA Dtl ID").ToString) ' Account ID ..
                        Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlCode, DTAcc.Rows(0).Item("Dtl Code").ToString) ' Account Code ..
                        Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlTitle, DTAcc.Rows(0).Item("Dtl Title").ToString) ' Account Title .
                    End If

                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FunAddReportPramaters()
        Try

            Dim objHashTableParamter As New Hashtable

            '''Report Name

            'CR#119

            'If DAL.SystemConfigurationDAL.GetSystemConfigurationValue("voucher_report_size") = "Long" Then
            '    objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle.rpt")
            'Else
            '    objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_Short.rpt")
            'End If

            'CR# 165        Asif Kamal      Voucher print replica, which will contain each account detail description
            If DAL.SystemConfigurationDAL.GetSystemConfigurationValue("voucher_report_size") = "Long" Then


                If (Not DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") Is Nothing AndAlso DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting").ToString <> "") Or (Not DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") Is Nothing AndAlso DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers").ToString <> "") Then

                    If (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = False AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = False) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_WithoutSorting.rpt")
                    ElseIf (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = False AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = True) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_WithoutSorting_withDesc.rpt")
                    ElseIf (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = True AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = False) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle.rpt")
                    ElseIf (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = True AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = True) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_withDesc.rpt")

                    End If
                Else
                    objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle.rpt")
                End If

            Else

                'CR# 165        Asif Kamal      Voucher print replica, which will contain each account detail description
                'CR# 172        Asif Kamal      Newly added voucher report that shows description of voucher require formate settings
                If (Not DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") Is Nothing AndAlso DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting").ToString <> "") Or (Not DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") Is Nothing AndAlso DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers").ToString <> "") Then
                    If (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = False AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = False) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_Short_WithoutSorting.rpt")

                    ElseIf (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = False AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = True) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_Short_WithoutSorting_withDesc.rpt")

                    ElseIf (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = True AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = False) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_Short.rpt")

                    ElseIf (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = True AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = True) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_Short_withDesc.rpt")

                    End If
                Else
                    objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_Short.rpt")
                End If

            End If

            Dim ObjCompanyData As DataTable
            ObjCompanyData = UtilityDAL.setCompanyInfo(gobjLocationInfo.CompanyID)

            ' Passing Parameters .. (Report Parameters .. )
            objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
            objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))

            If Me.cboVoucherType.Text = "CPV" Or Me.cboVoucherType.Text = "BPV" Then
                objHashTableParamter.Add("ShowReceivedBy", True)
            Else
                objHashTableParamter.Add("ShowReceivedBy", False)
            End If

            'objHashTableParamter.Add("ShowReceivedBy", False)
            'End If

            '#To Do...Err 
            'Adding value to ShowHeader Parameter
            'If Convert.ToBoolean(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Show_Report_Header").ToString) = True Then
            '    objHashTableParamter.Add("ShowHeader", True)
            'Else
            '    objHashTableParamter.Add("ShowHeader", False)
            'End If

            ''Adding value to ShowAddress Parameter
            'If Convert.ToBoolean(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Show_Report_Address").ToString) = True Then
            '    objHashTableParamter.Add("ShowAddress", True)
            'Else
            '    objHashTableParamter.Add("ShowAddress", False)
            'End If

            If mobjControlList.Item("btnPrint") Is Nothing Then
                objHashTableParamter.Add("PrintRights", "False")
            Else
                objHashTableParamter.Add("PrintRights", "True")
            End If

            If mobjControlList.Item("btnExport") Is Nothing Then
                objHashTableParamter.Add("ExportRights", "False")
            Else
                objHashTableParamter.Add("ExportRights", "True")
            End If

            gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ' CR # 351
    Private Sub FunAddReportPramatersLandScape()
        Try

            Dim objHashTableParamter As New Hashtable

            
            If DAL.SystemConfigurationDAL.GetSystemConfigurationValue("voucher_report_size") = "Long" Then


                If (Not DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") Is Nothing AndAlso DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting").ToString <> "") Or (Not DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") Is Nothing AndAlso DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers").ToString <> "") Then

                    If (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = False AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = False) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_WithoutSorting_LandScape.rpt")
                    ElseIf (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = False AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = True) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_WithoutSorting_withDesc_LandScape.rpt")
                    ElseIf (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = True AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = False) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_LandScape.rpt")
                    ElseIf (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = True AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = True) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_withDesc_LandScape.rpt")

                    End If
                Else
                    objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_LandScape.rpt")
                End If

            Else

                
                If (Not DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") Is Nothing AndAlso DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting").ToString <> "") Or (Not DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") Is Nothing AndAlso DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers").ToString <> "") Then
                    If (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = False AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = False) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_Short_WithoutSorting_LandScape.rpt")

                    ElseIf (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = False AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = True) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_Short_WithoutSorting_withDesc_LandScape.rpt")

                    ElseIf (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = True AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = False) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_Short_LandScape.rpt")

                    ElseIf (IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("VoucherAutoSorting")) = True AndAlso IIf(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers") = "", False, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Desc_in_Vouchers")) = True) Then

                        objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_Short_withDesc_LandScape.rpt")

                    End If
                Else
                    objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_Short_LandScape.rpt")
                End If

            End If

            Dim ObjCompanyData As DataTable
            ObjCompanyData = UtilityDAL.setCompanyInfo(gobjLocationInfo.CompanyID)

            ' Passing Parameters .. (Report Parameters .. )
            objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
            objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))

            If Me.cboVoucherType.Text = "CPV" Or Me.cboVoucherType.Text = "BPV" Then
                objHashTableParamter.Add("ShowReceivedBy", True)
            Else
                objHashTableParamter.Add("ShowReceivedBy", False)
            End If

            'objHashTableParamter.Add("ShowReceivedBy", False)
            'End If

            '#To Do...Err 
            'Adding value to ShowHeader Parameter
            'If Convert.ToBoolean(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Show_Report_Header").ToString) = True Then
            '    objHashTableParamter.Add("ShowHeader", True)
            'Else
            '    objHashTableParamter.Add("ShowHeader", False)
            'End If

            ''Adding value to ShowAddress Parameter
            'If Convert.ToBoolean(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Show_Report_Address").ToString) = True Then
            '    objHashTableParamter.Add("ShowAddress", True)
            'Else
            '    objHashTableParamter.Add("ShowAddress", False)
            'End If

            If mobjControlList.Item("btnPrint") Is Nothing Then
                objHashTableParamter.Add("PrintRights", "False")
            Else
                objHashTableParamter.Add("PrintRights", "True")
            End If

            If mobjControlList.Item("btnExport") Is Nothing Then
                objHashTableParamter.Add("ExportRights", "False")
            Else
                objHashTableParamter.Add("ExportRights", "True")
            End If

            gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Function ValidateGrid(ByVal CurrentRow As Janus.Windows.GridEX.GridEXRow) As String

        Try

            CurrentRow.BeginEdit()


            If CStr(CurrentRow.Cells(EnumGridVoucher.DtlCode).Value).Trim = String.Empty Then
                ShowErrorMessage("Please Select Account of Transaction")
                Me.grdVoucher.Row = CurrentRow.RowIndex
                Me.grdVoucher.Col = EnumGridVoucher.DtlCode

            ElseIf CStr(CurrentRow.Cells(EnumGridVoucher.CostCenter).Value).Trim = String.Empty Then
                ShowErrorMessage("Please Select Cost Center")
                Me.grdVoucher.Row = CurrentRow.RowIndex
                Me.grdVoucher.Col = EnumGridVoucher.CostCenter

            End If

            CurrentRow.EndEdit()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function FunAllowUpdateDelete() As Boolean

        Try

            '   check if voucher is posted then this voucher can not be Updated
            If BlnPostedVoucher Then
                '   Prompt the user with the Message
                ShowErrorMessage("Posted Voucher can not be Updated")
                Return False
                Exit Function
            End If

            'Don't Allow to Delete External Vouchers
            'CR#300
            'If strSourceOfVoucher <> "Accounts" Then
            '    '   Prompt the user with the Message
            '    ShowErrorMessage("External or Payment/Receipts Vouchers Can not be Affected.")
            '    Return False
            '    Exit Function
            'End If

            'Receipt shouldn't exist against Voucher
            If Me.cboVoucherType.Text.Trim = "SV" AndAlso New VoucherDAL().IsAmountReceived(mobjModel) Then
                ShowErrorMessage("Receiving exists against this voucher, cannot update or delete")
                Return False
                Exit Function
            End If

            'Payment shouldn't exist against this voucher
            If Me.cboVoucherType.Text.Trim = "PV" And New VoucherDAL().IsAmountPaid(mobjModel) Then
                ShowErrorMessage("Payment exists against this voucher, cannot update or delete")
                Return False
                Exit Function
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub PrintReport()

        Try

            If Me.intPkId > 0 Then

                If New VoucherDAL().AlterViewForReport(intPkId, intLocId, IsTempVoucher) Then
                    Call FunAddReportPramaters()
                    'ShowInformationMessage("View Created")
                    Dim rptViewer As New rptViewer
                    rptViewer.Text = Me.Text
                    rptViewer.Show()
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ' CR # 351
    Private Sub PrintReportLandScape()

        Try

            If Me.intPkId > 0 Then

                If New VoucherDAL().AlterViewForReport(intPkId, intLocId, IsTempVoucher) Then
                    Call FunAddReportPramatersLandScape()
                    'ShowInformationMessage("View Created")
                    Dim rptViewer As New rptViewer
                    rptViewer.Text = Me.Text
                    rptViewer.Show()
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Form Controls Events"

    Private Sub frmGLVoucher_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Reseting Global variables ....
        IsCallFromSearchPostScreen = False
        _gstrVoucherIDs = String.Empty
        _blnOOFFTEMPV = True

        If _TempVouchers = True Then 'bcz If Search/Post of Temp Voucher screen is on then don't set _TempVoucher=False

            For Each frm As System.Windows.Forms.Form In Application.OpenForms
                If frm.Name = "frmGLPostingVoucher" Then
                    _blnOOFFTEMPV = False
                End If
            Next

        End If

        _TempVouchers = Not _blnOOFFTEMPV

    End Sub


    ''This event will prevent the user to change the system language.
    Private Sub frmGLVoucher_InputLanguageChanging(ByVal sender As System.Object, ByVal e As System.Windows.Forms.InputLanguageChangingEventArgs) Handles MyBase.InputLanguageChanging
        e.Cancel = True
    End Sub

    Private Sub btnPrintLandScape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintLandScape.Click
        ' CR # 351 Start
        Try

            PrintReportLandScape()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

        ' CR # 351 End
    End Sub


    Private Sub frmGLVoucher_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            IsTempVoucher = _TempVouchers

            If gObjFinancialYearInfo.YearCode.Trim = "" Then
                ShowErrorMessage("No Financial Year selected. Please define a Financial Year and than select Financial Year while loging In to program")
                Me.Dispose()
                Exit Sub
            End If

            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)

            ''Assing Images to Buttons
            Me.SetButtonImages()

            'Config based values settings
            SetConfigurationBaseSetting()

            'Populating Company & Voucher Type drop down
            Me.FillCombos()

            ''Get all available record for the respective screen and fill the grid
            Call GetAllRecords()

            If Me.grdVoucher.RecordCount > 0 Then
                Me.grdVoucher_SelectionChanged(Nothing, Nothing)
            Else
                NewButtonClick()
            End If

            'Controls settings
            ControlSettingS()
            Me.cboVoucherType.Focus()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub

    Private Sub frmGLVoucher_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ''To avoid implecit call of rowcol chang event , We are assinging event handler at runtime.
        'AddHandler grdDetailEntry.SelectionChanged, AddressOf Me.grdVoucher_SelectionChanged
        Try


            If _TempVouchers = False Then
                Me.lblScreentext.Text = "Voucher Entry"
            Else
                Me.lblScreentext.Text = "Temporary Voucher Entry"
            End If

            'IsTempVoucher = _blnTempVouchers
            IsTempVoucher = _TempVouchers
            IsCallFromSearchPostScreen = _blnCallFromSearchPost
            StrVoucherIDs = _gstrVoucherIDs

            If IsCallFromSearchPostScreen = True Then

                GetAllRecords()

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click, btnNew.Click, btnUpdate.Click, btnDelete.Click, btnCancel.Click, btnExit.Click

        Try

            'Dim btn As C1.Win.C1Input.C1Button = CType(sender, C1.Win.C1Input.C1Button)
            Dim btn As Windows.Forms.Button = CType(sender, Windows.Forms.Button)

            ''If New Button is Clicked
            If btn.Name = btnNew.Name Then
                ''Refresh the controls for new mode
                'Me.ReSetControls()
                NewButtonClick()
                _IsCashBankRowExist = False
                strAccountID = ""
                strAmmountHead = ""
                'CR#321 fetch next Voucher Number on base of Voucher Type
                cboVoucherType_SelectedIndexChanged(Nothing, Nothing)
            ElseIf btn.Name = btnSave.Name Then
                '' Call Save method to save the record
                Me.Save()
            ElseIf btn.Name = btnUpdate.Name Then
                '' Call Update method to update the record
                Me.Update()
            ElseIf btn.Name = btnDelete.Name Then
                '' Call Delete method to delete the record
                Me.Delete()
            ElseIf btn.Name = btnCancel.Name Then
                ''Load Selected record in Edit Mode
                grdVoucher_SelectionChanged(Nothing, Nothing)
            ElseIf btn.Name = btnExit.Name Then
                Me.Close()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            mobjModel = Nothing
        End Try

    End Sub


    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click, btnPrevious.Click, btnNext.Click, btnLast.Click

        Try

            Dim btn As Button = CType(sender, Button)

            ''If Move First is clicked, then navigate to first record of the grid
            If btn.Name = Me.btnFirst.Name Then
                'Me.grdVoucher.Row = 0
                'Me.grdDetailEntry.MoveFirst()
                Me.grdVoucher.MoveFirst()

                ''If Move Previous is clicked, then navigate to Previous record of the grid
            ElseIf btn.Name = Me.btnPrevious.Name Then
                'If Me.grdVoucher.Row > 0 Then Me.grdVoucher.Row = (Me.grdVoucher.Row - 1)
                'Me.grdDetailEntry.MovePrevious()
                Me.grdVoucher.MovePrevious()

                ''If Move Next is clicked, then navigate to next record of the grid
            ElseIf btn.Name = Me.btnNext.Name Then
                'If Me.grdVoucher.Row >= 0 Then Me.grdVoucher.Row = (Me.grdVoucher.Row + 1)
                Me.grdVoucher.MoveNext()


                ''If Move Last is clicked, then navigate to Last record of the grid
            ElseIf btn.Name = Me.btnLast.Name Then
                'Me.grdVoucher.Row = (Me.grdVoucher.RowCount - 1)
                'Me.grdDetailEntry.MoveLast()
                Me.grdVoucher.MoveLast()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub frmGLVoucher_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try

            If e.Control And e.KeyCode = Keys.S Then
                'CR # 260
                Me.btnSave.Focus()
                If Me.btnSave.Enabled = True Then Me.Save()
            ElseIf e.Control And e.KeyCode = Keys.U Then
                'CR # 260
                Me.btnUpdate.Focus()
                If Me.btnUpdate.Enabled = True Then Me.Update()
            ElseIf e.Control And e.KeyCode = Keys.D Then
                If Me.btnDelete.Enabled = True Then Me.Delete()
            ElseIf e.Control And e.KeyCode = Keys.N Then
                If Me.btnNew.Enabled = True Then
                    Me.ReSetControls()
                    'CR#85
                    _IsCashBankRowExist = False
                    strAccountID = ""
                    strAmmountHead = ""
                    'CR#321 fetch next Voucher Number on base of Voucher Type
                    cboVoucherType_SelectedIndexChanged(Nothing, Nothing)
                End If
            ElseIf e.Control And e.KeyCode = Keys.E Then
                If Me.btnCancel.Enabled = True Then Me.grdVoucher_SelectionChanged(Nothing, Nothing)
            ElseIf e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            ElseIf e.Control And e.KeyCode = Keys.P Then
                PrintReport()
            ElseIf e.Control And e.KeyCode = Keys.R Then
                cmdSearchVoucher_Click(Nothing, Nothing)
            ElseIf e.Control And e.KeyCode = Keys.T Then
                If Me.cmdPostVoucher.Enabled = True Then cmdPostVoucher_Click(Nothing, Nothing)
                'On pressing Ctr+F Navigate to Next Record
            ElseIf e.Control And e.KeyCode = Keys.F Then
                If Me.btnNext.Enabled = True Then
                    If Me.grdVoucher.Row >= 0 Then Me.grdVoucher.Row = (Me.grdVoucher.Row + 1)
                End If
                'On pressing Ctr+B Navigate to Previous Record
            ElseIf e.Control And e.KeyCode = Keys.B Then
                If Me.btnPrevious.Enabled = True Then
                    If Me.grdVoucher.Row > 0 Then Me.grdVoucher.Row = (Me.grdVoucher.Row - 1)
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdVoucher_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdVoucher.SelectionChanged
        Try

            If Me.grdVoucher.RootTable Is Nothing Then
                Exit Sub
            End If

            
            If Me.grdVoucher.RootTable.Columns.Count = 0 Then
                Exit Sub
            End If
            'If there is no record found in grid then load the screen in new mode
            'code commented against CR # 356
            'If Me.grdVoucher.RowCount = 0 Then
            'CR # 356
            If Me.grdVoucher.RowCount = 1 Then
                Me.ReSetControls()
                Exit Sub
            End If

            Me.cboVoucherType.Enabled = False
            'CR#300
            Me.cboCompany.Enabled = False

            If Me.grdVoucher.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                Me.grdVoucher.Row = (Me.grdVoucher.Row - 1)
                Exit Sub
            End If


            'Primary Key values
            intPkId = Me.grdVoucher.GetValue(EnumMasterGrid.VoucherID).ToString
            Me.intLocId = Me.grdVoucher.GetValue(EnumMasterGrid.LocID).ToString

            Me.cboVoucherType.SelectedValue = IIf(IsDBNull(Me.grdVoucher.GetValue(EnumMasterGrid.VoucherTypeID).ToString), gstrComboZeroIndexString, Me.grdVoucher.GetValue(EnumMasterGrid.VoucherTypeID).ToString)

            If Not IsDate(Me.grdVoucher.GetValue(EnumMasterGrid.VoucherDate).ToString) Then
                Me.dtpVoucherDate.Value = Now.Date
            Else
                Me.dtpVoucherDate.Value = CDate(Me.grdVoucher.GetValue(EnumMasterGrid.VoucherDate).ToString)
            End If

            _VMonth = Me.dtpVoucherDate.Value.Month

            ''CR#65
            _CurrMonth = Me.dtpVoucherDate.Value.Month
            _CurrYear = Me.dtpVoucherDate.Value.Year

            Me.lblVoucherString.Text = Me.grdVoucher.GetValue(EnumMasterGrid.VoucherCode).ToString

            If CBool(Me.grdVoucher.GetValue(EnumMasterGrid.Post).ToString) = True Then

                Me.lblPosted.Text = "Posted"
                Me.lblPosted.ForeColor = Color.Blue
                Me.cmdPostVoucher.Enabled = False
                BlnPostedVoucher = True

            Else

                Me.lblPosted.Text = "Un-Posted"
                Me.lblPosted.ForeColor = Color.Red
                'If user have Voucher posting rights,then enable posting button
                If mobjControlList.Item("btnPost") Is Nothing Then
                    Me.cmdPostVoucher.Enabled = False
                Else
                    Me.cmdPostVoucher.Enabled = True
                End If
                BlnPostedVoucher = False

            End If


            Me.txtVNo.Text = Me.grdVoucher.GetValue(EnumMasterGrid.VoucherNo).ToString
            'CR#300
            'Me.txtCompanyName.Text = Me.grdVoucher.GetValue(EnumMasterGrid.Location).ToString
            Me.cboCompany.SelectedValue = Me.grdVoucher.GetValue(EnumMasterGrid.LocID).ToString
            Me.txtFYearCode.Text = Me.grdVoucher.GetValue(EnumMasterGrid.FYearCode).ToString

            m_VNo = Me.txtVNo.Text

            If Not IsDBNull(Me.grdVoucher.GetValue(EnumMasterGrid.COADtlCode).ToString) = True AndAlso Me.grpCashBankInfo.Enabled = True Then

                Me.ctrlGLAccounts.txtACCode.Text = Me.grdVoucher.GetValue(EnumMasterGrid.COADtlCode).ToString
                Me.ctrlGLAccounts.GLAccountID = IIf(Me.grdVoucher.GetValue(EnumMasterGrid.COADtlID).ToString <> "", Me.grdVoucher.GetValue(EnumMasterGrid.COADtlID).ToString, 0)
                Me.ctrlGLAccounts.txtAccountName.Text = Me.grdVoucher.GetValue(EnumMasterGrid.COADtlTitle).ToString
                Me.txtVoucherNarration.Text = Me.grdVoucher.GetValue(EnumMasterGrid.PaidTo).ToString

            Else

                Me.ctrlGLAccounts.txtACCode.Text = ""
                Me.ctrlGLAccounts.GLAccountID = 0
                Me.ctrlGLAccounts.txtAccountName.Text = ""
                Me.txtVoucherNarration.Text = ""

            End If

            Me.txtChequeNo.Text = Me.grdVoucher.GetValue(EnumMasterGrid.ChequeNo).ToString
            'CR#300
            'strSourceOfVoucher = Me.grdVoucher.GetValue(EnumMasterGrid.Source).ToString
            Me.cboSource.SelectedValue = Me.grdVoucher.GetValue(EnumMasterGrid.Source).ToString

            intTempVoucherID = Me.grdVoucher.GetValue(EnumMasterGrid.TempVoucherID).ToString

            If Not IsDate(Me.grdVoucher.GetValue(EnumMasterGrid.ChequeDate).ToString) Then
                Me.dtpChequeDate.Value = Now.Date
                Me.dtpChequeDate.Checked = False
            Else
                Me.dtpChequeDate.Value = CDate(Me.grdVoucher.GetValue(EnumMasterGrid.ChequeDate).ToString)
                Me.dtpChequeDate.Checked = True
            End If

            If Not IsDate(Me.grdVoucher.GetValue(EnumMasterGrid.DueDate).ToString) Then
                Me.dtpDueDate.Value = Now.Date
                Me.dtpDueDate.Checked = False
            Else
                Me.dtpDueDate.Value = CDate(Me.grdVoucher.GetValue(EnumMasterGrid.DueDate).ToString)
                Me.dtpDueDate.Checked = True
            End If

            If CBool(Me.grdVoucher.GetValue(EnumMasterGrid.OtherVoucher).ToString) = True Then
                Me.chkOtherVoucher.Checked = True
                _IsOtherVoucher = True
            Else
                Me.chkOtherVoucher.Checked = False
                _IsOtherVoucher = False
            End If

            ListOfDeletedIDs = String.Empty

            GetVoucherDetail()

            Call ApplySecurity(EnumDataMode.Edit)

            Me.lblTotalRecord.Text = "Record " & (CInt(Me.grdVoucher.Row.ToString) + 1) & " Of " & Me.grdVoucher.RecordCount

            Dim objVoucher As VoucherDAL
            Dim strVoucherType As String = ""

            objVoucher = New VoucherDAL()
            strVoucherType = objVoucher.GetGLVType(Me.cboVoucherType.SelectedValue)
            If Not (strVoucherType = "JV" Or strVoucherType = "SV" Or strVoucherType = "PV") Then

                If strVoucherType = "BP" Or strVoucherType = "CP" Then
                    'Me.txtAmount.Text = Math.Round(Val(Me.grdDetailEntry.GetTotal(Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.CreditAmount), Janus.Windows.GridEX.AggregateFunction.Sum)))
                    ' CR # 328
                    'Me.txtAmount.Text = Me.grdDetailEntry.GetRow(0).Cells(EnumGridVoucher.CreditAmount).Value.ToString()
                    ' CR # 337
                    ' setting credit amount of bank voucher
                    Me.txtAmount.Text = New VoucherDAL().GetAmountForBankVoucher(intPkId)
                    Me.lblDrCr.Text = "Cr."

                ElseIf strVoucherType = "BR" Or strVoucherType = "CR" Then

                    Me.txtAmount.Text = Math.Round(Val(Me.grdDetailEntry.GetTotal(Me.grdDetailEntry.RootTable.Columns(EnumGridVoucher.DebitAmount), Janus.Windows.GridEX.AggregateFunction.Sum)))
                    Me.lblDrCr.Text = "Dr."

                End If
            End If


            Me.dtpVoucherDate.Focus()

            'CR#86
            ApplyVTypesRights()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function SortDataTable(ByVal dTable As DataTable, ByVal ColumnName As String, Optional ByVal OrderByAsc As Boolean = True) As DataTable
        Try


            Dim dView As New DataView(dTable)

            If OrderByAsc Then
                dView.Sort = ColumnName & " ASC"
            Else
                dView.Sort = ColumnName & " DESC"
            End If

            Return dView.ToTable

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Private Sub UiCtrlGLAccHidden_GetGLAccount(ByVal sender As uiCtrlGLAccount)
        Try

            intGLAccID = Me.ctrlGLAccounts.GLAccountID
            strGLAccCode = Me.ctrlGLAccounts.GLAccountCode
            strGLAccTitle = Me.ctrlGLAccounts.GLAccountName

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cboVoucherType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboVoucherType.SelectedIndexChanged

        Dim objVoucher As VoucherDAL
        Dim strVoucherType As String = ""

        Try



            'Reset controls
            Me.grdDetailEntry.DataSource = Nothing
            Dim DtVoucherDetail As DataTable
            DtVoucherDetail = New VoucherDAL().GetDetailofVoucher(0, 0, " And 1=0", IsTempVoucher)
            Me.grdDetailEntry.DataSource = DtVoucherDetail
            Me.grdDetailEntry.RetrieveStructure()
            ApplyGridSettings()

            Me.ctrlGLAccounts.txtACCode.Text = String.Empty
            Me.ctrlGLAccounts.txtAccountName.Text = String.Empty
            Me.lblPosted.Text = String.Empty
            Me.lblVoucherString.Text = String.Empty

            If Me.cboVoucherType.SelectedIndex > 0 Then



                objVoucher = New VoucherDAL()

                strVoucherType = objVoucher.GetGLVType(Me.cboVoucherType.SelectedValue)

                'Enable Disable of Cash Bank Info group
                Me.grpCashBankInfo.Enabled = IIf(strVoucherType = "BP" Or strVoucherType = "CP" Or strVoucherType = "BR" Or strVoucherType = "CR", True, False)
                'Enable Disable of Cheque No
                Me.txtChequeNo.Enabled = IIf(strVoucherType = "BP" Or strVoucherType = "BR", True, False)
                'Enable Disable of Cheque Date
                Me.dtpChequeDate.Enabled = IIf(strVoucherType = "BP" Or strVoucherType = "BR", True, False)

                'CR#300
                'Me.txtVNo.Text = objVoucher.GetNewVoucherNo(pbLocationID, Me.cboVoucherType.SelectedValue.ToString, Format(Me.dtpVoucherDate.Value, "MMM"), Format(Me.dtpVoucherDate.Value, "yyyy"), Me.chkOtherVoucher.Checked, IsTempVoucher)
                Me.txtVNo.Text = objVoucher.GetNewVoucherNo(Me.cboCompany.SelectedValue, Me.cboVoucherType.SelectedValue.ToString, Format(Me.dtpVoucherDate.Value, "MMM"), Format(Me.dtpVoucherDate.Value, "yyyy"), Me.chkOtherVoucher.Checked, IsTempVoucher)
                'replace the voucher no from '1' to '000001'
                Me.txtVNo.Text = funDoPadding(Me.txtVNo.Text, Me.txtVNo.MaxLength)

            Else

                Me.txtVNo.Text = String.Empty

            End If

            'For "BP" and "CP" V. Type only Debit entry will accepted and Credit entry will be Auto
            If strVoucherType = "BP" Or strVoucherType = "CP" Then
                Me.lblDrCr.Text = "Cr."
                strAmmountHead = "Cr."

            ElseIf strVoucherType = "BR" Or strVoucherType = "CR" Then
                'For "BR" and "CR" V. Type only Credit entry will accepted and Debit entry will be Auto
                Me.lblDrCr.Text = "Dr."
                strAmmountHead = "Dr."

            ElseIf strVoucherType = "JV" Or strVoucherType = "PV" Or strVoucherType = "SV" Then
                Me.lblDrCr.Text = String.Empty

            End If

            'Enable / Disable Cheque Print Button
            If strVoucherType = "BP" Then
                'Me.btnPrintCheque.Enabled = True
            Else
                ' Me.btnPrintCheque.Enabled = False
            End If

            'Make SQL to be shown in help grid.
            If Me.cboVoucherType.Text = "BRV" Or Me.cboVoucherType.Text = "BPV" Then
                Me.ctrlGLAccounts.AccountType = EnumAccountTypes.Bank
            ElseIf Me.cboVoucherType.Text = "CRV" Or Me.cboVoucherType.Text = "CPV" Then
                Me.ctrlGLAccounts.AccountType = EnumAccountTypes.Cash
            ElseIf Me.cboVoucherType.Text = "JV" Or Me.cboVoucherType.Text = "PV" Or Me.cboVoucherType.Text = "SV" Then
                Me.ctrlGLAccounts.AccountType = EnumAccountTypes.General
            End If

           


            'CR#86
            SetActionControlsON()
            ApplyVTypesRights()

            If Me.cboVoucherType.Text = EnumVType.PV.ToString Then              '268        Rana Saeed  (enable/disable invoice print button on voucher type selection)
                Me.btnPrintInvoice.Text = "Print PO"
                Me.btnPrintInvoice.Enabled = True
            ElseIf Me.cboVoucherType.Text = EnumVType.SV.ToString Then
                Me.btnPrintInvoice.Text = "Print Sale Invoice"
                Me.btnPrintInvoice.Enabled = True
            Else
                Me.btnPrintInvoice.Text = "Print PO"
                Me.btnPrintInvoice.Enabled = False
            End If
            ' changae by farooq -H    ''// 323     23 Jul,2014          farooq-H  
            If Me.cboVoucherType.Text = EnumVType.BPV.ToString Then
                Me.BtnCheckPrint.Enabled = True
            Else
                Me.BtnCheckPrint.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub grpCashBankInfo_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grpCashBankInfo.EnabledChanged
        Try

            If Me.grpCashBankInfo.Enabled = False Then

                Me.ctrlGLAccounts.txtACCode.Clear()
                Me.txtVoucherNarration.Text = String.Empty
                Me.txtChequeNo.Text = String.Empty
                Me.txtAmount.Text = String.Empty
                Me.dtpChequeDate.Checked = False

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdDetailEntry_AddingRecord(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdDetailEntry.AddingRecord
        Dim blnDr As Boolean = False
        Dim blncr As Boolean = False

        Try


            ' ''Don't Allow to Add Record if Dtl Acc Code is not valid/empty
            If (Not Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode) Is Nothing) AndAlso (Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode).ToString.Trim = "") Then
                'e.Cancel = True
                Me.grdDetailEntry.Delete()
                Me.grdDetailEntry.Update()
                Me.grdDetailEntry.Refetch()
            End If

            'Allow Entry of wither Debit or credit for each detail Transaction

            'Don't Allow Debit entry when credit entry exist
            If (Not Me.grdDetailEntry.GetValue(EnumGridVoucher.CreditAmount) Is Nothing) AndAlso Val(Me.grdDetailEntry.GetValue(EnumGridVoucher.CreditAmount).ToString) > 0 Then
                'Me.grdDetailEntry.SetValue(EnumGridVoucher.DebitAmount, 0)
                blncr = True
            End If
            If (Not Me.grdDetailEntry.GetValue(EnumGridVoucher.DebitAmount) Is Nothing) AndAlso Val(Me.grdDetailEntry.GetValue(EnumGridVoucher.DebitAmount).ToString) > 0 Then
                ' Me.grdDetailEntry.SetValue(EnumGridVoucher.CreditAmount, 0)
                blnDr = True
            End If

            If blnDr = True AndAlso blncr = True Then
                ShowErrorMessage("Either Debit or Crdit side entry is allowed")
                e.Cancel = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetailEntry_CellEdited(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetailEntry.CellEdited

    End Sub

    Private Sub grdDetailEntry_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetailEntry.CellUpdated

        Try



            ' ''Don't Allow to enter invalid Acc Dtl code in added row
            'If Not ((Me.grdDetailEntry.RecordCount > 0 AndAlso Me.grdDetailEntry.Row = 0) Or (Me.grdDetailEntry.RecordCount = 0 AndAlso Me.grdDetailEntry.Row = -1)) Then

            '    'When cell is Act Dtl Code
            '    If (Not e.Column Is Nothing) AndAlso (e.Column.Index = EnumGridVoucher.DtlCode) Then
            '        If Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode).ToString = "" Then
            '            ShowErrorMessage("You must have to enter a valid Acc Detail Code")
            '            Exit Sub

            '        End If
            '    End If

            'End If

            'CR#115
            If Me.cboVoucherType.Text = "BRV" Or Me.cboVoucherType.Text = "BPV" Then


            End If

            'Just setting var as true in case of Cash/Bank acc entry
            If Me.cboVoucherType.Text = "BRV" Or Me.cboVoucherType.Text = "CRV" Or Me.cboVoucherType.Text = "BPV" Or Me.cboVoucherType.Text = "CPV" Then

                'if Cash/bank Acc Row
                If ((Me.grdDetailEntry.RecordCount > 0 AndAlso Me.grdDetailEntry.Row = 0) Or (Me.grdDetailEntry.RecordCount = 0 AndAlso Me.grdDetailEntry.Row = -1)) Then

                    ' If (Not e.Column Is Nothing) AndAlso (e.Column.Index = EnumGridVoucher.DtlCode) Then

                    _IsCashBankRowExist = True

                    'End If

                End If

            End If

            'Case of Payment Type BP,CP,BR & CR 
            If Me.cboVoucherType.Text = "BRV" Or Me.cboVoucherType.Text = "CRV" Or Me.cboVoucherType.Text = "BPV" Or Me.cboVoucherType.Text = "CPV" Then

                If Not e.Column Is Nothing AndAlso (e.Column.Index = EnumGridVoucher.CreditAmount Or e.Column.Index = EnumGridVoucher.DebitAmount) Then

                    Dim RowIndex As Integer = Me.grdDetailEntry.Row
                    'Don't Allow Entry in Debit if BP/CP vType selected also don't allow Credit entry for BR/CR Vtype selected

                    'If Me.cboVoucherType.Text = "BRV" Or Me.cboVoucherType.Text = "CRV" Then
                    ''If 1st Row
                    'If (Me.grdDetailEntry.RecordCount > 0 AndAlso RowIndex = 0) Or (Me.grdDetailEntry.RecordCount = 0 AndAlso RowIndex = -1) Then

                    '    'Don't Allow Credit Side Entry in Bank.Cash (First) ACC row
                    '    If e.Column.Index = EnumGridVoucher.CreditAmount Then 'AndAlso IsTempVoucher = False Then
                    '        Me.grdDetailEntry.SetValue(EnumGridVoucher.CreditAmount, 0)
                    '    ElseIf e.Column.Index = EnumGridVoucher.DebitAmount Then
                    '        Me.txtAmount.Text = Me.grdDetailEntry.GetValue(EnumGridVoucher.DebitAmount).ToString
                    '    End If

                    '    'Else

                    '    '    'Don't Allow Debit Side Entry for all records other than bank/cash
                    '    '    If e.Column.Index = EnumGridVoucher.DebitAmount Then 'AndAlso IsTempVoucher = False Then
                    '    '        Me.grdDetailEntry.SetValue(EnumGridVoucher.DebitAmount, 0)
                    '    '    End If
                    '    Exit Sub

                    'End If

                    If grdDetailEntry.GetRow().Cells(EnumGridVoucher.COADtlID).Text = strAccountID Then

                        If e.Column.Index = EnumGridVoucher.CreditAmount Then
                            If Not strAmmountHead = "Cr." Then
                                Me.grdDetailEntry.SetValue(EnumGridVoucher.CreditAmount, 0)

                            End If

                        ElseIf e.Column.Index = EnumGridVoucher.DebitAmount Then
                            If Not strAmmountHead = "Dr." Then
                                Me.grdDetailEntry.SetValue(EnumGridVoucher.DebitAmount, 0)

                            End If

                        End If






                    End If

                    'ElseIf Me.cboVoucherType.Text = "BPV" Or Me.cboVoucherType.Text = "CPV" Then
                    '    'If 1st Row
                    '    If (Me.grdDetailEntry.RecordCount > 0 AndAlso RowIndex = 0) Or (Me.grdDetailEntry.RecordCount = 0 AndAlso RowIndex = -1) Then

                    '        'Don't Allow Debit Side Entry for all records other than bank/cash
                    '        If e.Column.Index = EnumGridVoucher.DebitAmount Then 'AndAlso IsTempVoucher = False Then
                    '            Me.grdDetailEntry.SetValue(EnumGridVoucher.DebitAmount, 0)
                    '        ElseIf e.Column.Index = EnumGridVoucher.CreditAmount Then
                    '            Me.txtAmount.Text = Me.grdDetailEntry.GetValue(EnumGridVoucher.CreditAmount).ToString
                    '        End If

                    '        'Else

                    '        '    'Don't Allow Credit Side Entry in Bank.Cash (First) ACC row
                    '        '    If e.Column.Index = EnumGridVoucher.CreditAmount Then 'AndAlso IsTempVoucher = False Then
                    '        '        Me.grdDetailEntry.SetValue(EnumGridVoucher.CreditAmount, 0)
                    '        '    End If

                    '        Exit Sub
                    '    End If

                    'End If

                End If

            End If


            'For All Voucher Types
            'When user manually enter Acc Code
            If Not e.Column Is Nothing AndAlso e.Column.Index = EnumGridVoucher.DtlCode AndAlso Not Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode) Is Nothing Then


                If Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode).ToString <> "" Then

                    If IsTempVoucher = False Then
                        'Don't Allow duplicate enteries of Accounts
                        If Me.grdDetailEntry.RecordCount > 0 Then

                            Dim DT As DataTable
                            DT = CType(Me.grdDetailEntry.DataSource, DataTable)

                            If DT.Columns(EnumGridVoucher.DtlCode).Unique = False Then
                                Dim UK As New UniqueConstraint("UK", DT.Columns(EnumGridVoucher.DtlCode), True)
                                DT.Constraints.Clear()
                                DT.Constraints.Add(UK)
                            End If

                            Dim drFind As DataRow
                            drFind = DT.Rows.Find(Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode).ToString)

                            If drFind Is Nothing Then
                                Dim DTAcc As New DataTable
                                DTAcc = New VoucherDAL().GetDetailofAccCode(Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode).ToString)

                                If DTAcc.Rows.Count > 0 Then
                                    '[COA Dtl ID],detail_title [Dtl Title], detail_code  [Dtl Code] 
                                    Me.grdDetailEntry.SetValue(EnumGridVoucher.COADtlID, DTAcc.Rows(0).Item("COA Dtl ID").ToString) ' Account ID ..
                                    Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlCode, DTAcc.Rows(0).Item("Dtl Code").ToString) ' Account Code ..
                                    Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlTitle, DTAcc.Rows(0).Item("Dtl Title").ToString) ' Account Title .
                                Else
                                    Me.grdDetailEntry.SetValue(EnumGridVoucher.COADtlID, String.Empty) ' Account ID ..
                                    Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlCode, String.Empty) ' Account Code ..
                                    Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlTitle, String.Empty) ' Account Title 
                                End If

                            Else 'If Already Exist then

                                ' ShowErrorMessage("A/c Alreadt exist,Uplicate A/C Entry is not allowed")
                                Me.grdDetailEntry.SetValue(EnumGridVoucher.COADtlID, String.Empty) ' Account ID ..
                                Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlCode, String.Empty) ' Account Code ..
                                Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlTitle, String.Empty) ' Account Title 

                            End If

                            DT.Constraints.Clear()

                        Else    ''If Entry of Fisrt tuple in grid

                            Dim DTAcc As New DataTable
                            DTAcc = New VoucherDAL().GetDetailofAccCode(Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode).ToString)

                            If DTAcc.Rows.Count > 0 Then
                                '[COA Dtl ID],detail_title [Dtl Title], detail_code  [Dtl Code] 
                                Me.grdDetailEntry.SetValue(EnumGridVoucher.COADtlID, DTAcc.Rows(0).Item("COA Dtl ID").ToString) ' Account ID ..
                                Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlCode, DTAcc.Rows(0).Item("Dtl Code").ToString) ' Account Code ..
                                Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlTitle, DTAcc.Rows(0).Item("Dtl Title").ToString) ' Account Title .
                            Else
                                Me.grdDetailEntry.SetValue(EnumGridVoucher.COADtlID, String.Empty) ' Account ID ..
                                Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlCode, String.Empty) ' Account Code ..
                                Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlTitle, String.Empty) ' Account Title 
                            End If

                        End If

                    ElseIf IsTempVoucher = True Then
                        'For Temp Voucher Allow duplicate Entries of Accounts

                        Dim DTAcc As New DataTable
                        DTAcc = New VoucherDAL().GetDetailofAccCode(Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode).ToString)

                        If DTAcc.Rows.Count > 0 Then
                            '[COA Dtl ID],detail_title [Dtl Title], detail_code  [Dtl Code] 
                            Me.grdDetailEntry.SetValue(EnumGridVoucher.COADtlID, DTAcc.Rows(0).Item("COA Dtl ID").ToString) ' Account ID ..
                            Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlCode, DTAcc.Rows(0).Item("Dtl Code").ToString) ' Account Code ..
                            Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlTitle, DTAcc.Rows(0).Item("Dtl Title").ToString) ' Account Title .
                        Else
                            Me.grdDetailEntry.SetValue(EnumGridVoucher.COADtlID, String.Empty) ' Account ID ..
                            Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlCode, String.Empty) ' Account Code ..
                            Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlTitle, String.Empty) ' Account Title 
                        End If

                    End If



                End If

            End If

            'Case Of Voucher Type JV,CV,PV
            'If Me.cboVoucherType.Text = "JV" Or Me.cboVoucherType.Text = "CV" Or Me.cboVoucherType.Text = "PV" Or Me.cboVoucherType.Text = "CPV" Then

            'Allow Entry of either Debit or credit for each detail Transaction
            If Not e.Column Is Nothing AndAlso (e.Column.Index = EnumGridVoucher.CreditAmount Or e.Column.Index = EnumGridVoucher.DebitAmount) Then
                'Don't Allow Debit entry when credit entry exist
                If e.Column.Index = EnumGridVoucher.DebitAmount Then
                    If (Not Me.grdDetailEntry.GetValue(EnumGridVoucher.CreditAmount) Is Nothing) AndAlso Val(Me.grdDetailEntry.GetValue(EnumGridVoucher.CreditAmount).ToString) > 0 Then
                        Me.grdDetailEntry.SetValue(EnumGridVoucher.DebitAmount, 0)
                    End If
                ElseIf e.Column.Index = EnumGridVoucher.CreditAmount Then
                    If (Not Me.grdDetailEntry.GetValue(EnumGridVoucher.DebitAmount) Is Nothing) AndAlso Val(Me.grdDetailEntry.GetValue(EnumGridVoucher.DebitAmount).ToString) > 0 Then
                        Me.grdDetailEntry.SetValue(EnumGridVoucher.CreditAmount, 0)
                    End If
                End If
            End If




        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetailEntry_ColumnHeaderClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetailEntry.ColumnHeaderClick
        Dim strDesc As String = String.Empty
        Dim RowIndex As Integer

        Try

            'There must be Records in grid for this operation
            If Not Me.grdDetailEntry.RecordCount > 0 Then Exit Sub

            'Grid column should not be nothing
            If e.Column Is Nothing Then Exit Sub

            'Clicked Headder must be of comments column
            If Not e.Column.Index = EnumGridVoucher.Comments Then Exit Sub

            'Copy current Row position of grid
            RowIndex = Me.grdDetailEntry.Row

            'Move grid cursor to 1st entered Row position
            Me.grdDetailEntry.MoveFirst()

            'Copy Description of comments at 1st entered row
            strDesc = Me.grdDetailEntry.GetValue(EnumGridVoucher.Comments).ToString

            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdDetailEntry.GetRows()
                Row.BeginEdit()
                Row.Cells(EnumGridVoucher.Comments).Value = strDesc
                Row.EndEdit()
            Next

            Me.grdDetailEntry.Update()
            Me.grdDetailEntry.Refresh()


            Me.grdDetailEntry.Row = RowIndex

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetailEntry_DeletingRecords(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdDetailEntry.DeletingRecords
        Try

            '   ListOfDeletedIDs = ListOfDeletedIDs & "," & Me.grdDetailEntry.GetValue(EnumGridVoucher.VoucherDtlID).ToString

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetailEntry_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDetailEntry.DoubleClick
        Dim DR As Janus.Windows.GridEX.GridEXRow


        Try



        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdDetailEntry_EditingCell(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.EditingCellEventArgs) Handles grdDetailEntry.EditingCell
        Try


            '1) 'For Cash/bank Row in grid
            'Don't Allow to change Dtl Acc Code in Detail Grid when VType is BP,CP,BR,CR
            If Me.cboVoucherType.Text = "BRV" Or Me.cboVoucherType.Text = "CRV" Or Me.cboVoucherType.Text = "BPV" Or Me.cboVoucherType.Text = "CPV" Then

                If Me.grdDetailEntry.Col = EnumGridVoucher.DtlCode Then

                    'For only Fisrt Row/Cash Bank Row
                    If (Me.grdDetailEntry.RecordCount > 0 AndAlso Me.grdDetailEntry.Row = 0) Or (Me.grdDetailEntry.RecordCount = 0 AndAlso Me.grdDetailEntry.Row = -1) Then

                        e.Cancel = True

                    End If

                End If

            End If


            '2) 'For Non Cash/bank Row in grid
            'Don't Allow to enter invalid Acc Dtl code in added row
            If Not Me.grdDetailEntry.Row = -1 Then

                If Not ((Me.grdDetailEntry.RecordCount > 0 AndAlso Me.grdDetailEntry.Row = 0) Or (Me.grdDetailEntry.RecordCount = 0 AndAlso Me.grdDetailEntry.Row = -1)) Then

                    'When cell is Act Dtl Code
                    If (Not e.Column Is Nothing) AndAlso (e.Column.Index = EnumGridVoucher.DtlCode) Then
                        If Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode).ToString = "" Then
                            'ShowErrorMessage("You must have to enter a valid Acc Detail Code")
                            e.Cancel = True

                        End If
                    End If

                End If

            End If
            
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetailEntry_FormattingRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdDetailEntry.FormattingRow
        Try

            If Me.grdDetailEntry.RowCount > 0 Then

                If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then e.Row.Cells(EnumGridVoucher.Delete).Text = "Delete"

            End If

        Catch ex1 As Exception
            ShowErrorMessage(ex1.Message)
        End Try
    End Sub

    Private Sub grdDetailEntry_GetNewRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.GetNewRowEventArgs) 'Handles grdDetailEntry.GetNewRow
        Dim strCOADtlID As Integer
        Dim strCOADtlCode As String
        Dim Dr As Double
        Dim Cr As Double

        Try


            strCOADtlID = CInt(IIf(Me.grdDetailEntry.GetValue(EnumGridVoucher.COADtlID).ToString = String.Empty, 0, Me.grdDetailEntry.GetValue(EnumGridVoucher.COADtlID).ToString))
            strCOADtlCode = Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode).ToString
            Dr = CDbl(IIf(Me.grdDetailEntry.GetValue(EnumGridVoucher.DebitAmount).ToString = String.Empty, 0, Me.grdDetailEntry.GetValue(EnumGridVoucher.DebitAmount).ToString))
            Cr = CDbl(IIf(Me.grdDetailEntry.GetValue(EnumGridVoucher.CreditAmount).ToString = String.Empty, 0, Me.grdDetailEntry.GetValue(EnumGridVoucher.CreditAmount).ToString))

            If strCOADtlID = 0 Then
                ShowErrorMessage("Invalid enter a Detail A/C")
                e.NewRow = Nothing
                Exit Sub
            End If

            If strCOADtlCode = String.Empty Then
                ShowErrorMessage("Invalid enter a Detail A/C")
                e.NewRow = Nothing
                Exit Sub
            End If

            If Dr < 1 AndAlso Cr < 1 Then
                ShowErrorMessage("Invalid enter a Debit/Credit Amount")
                e.NewRow = Nothing
                Exit Sub
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetailEntry_GetNewRow1(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.GetNewRowEventArgs) Handles grdDetailEntry.GetNewRow

    End Sub

    'Private Sub grdDetailEntry_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDetailEntry.GotFocus
    '    Try

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub grdDetailEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdDetailEntry.KeyDown
        Try

            'If e.KeyValue = Keys.Enter Then

            '    ''Don't Allow to Add Record if Dtl Acc Code is not valid/empty
            '    If (Not Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode) Is Nothing) AndAlso (Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode).ToString.Trim = "") Then
            '        e.Handled = True
            '    End If

            'End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub grdDetailEntry_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdDetailEntry.KeyPress
    '    Try


    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub grdDetailEntry_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdDetailEntry.KeyUp
        Dim objHelp As uiCtrlGLAccount

        Try


            If e.KeyData = Keys.F1 Then

                'For Total Row stop F1 process
                If Me.grdDetailEntry.GetRow.RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                    Exit Sub
                End If

                'If user is on column other than Acc Dtl Code than Stop F1 process
                If Not (Me.grdDetailEntry.Col = EnumGridVoucher.DtlCode) Then
                    Exit Sub
                End If

                'Don't Allow to change Dtl Acc Code in Detail Grid when VType is BP,CP,BR,CR
                If Me.cboVoucherType.Text = "BRV" Or Me.cboVoucherType.Text = "CRV" Or Me.cboVoucherType.Text = "BPV" Or Me.cboVoucherType.Text = "CPV" Then

                    If (Me.grdDetailEntry.RecordCount > 0 AndAlso Me.grdDetailEntry.Row = 0) Or (Me.grdDetailEntry.RecordCount = 0 AndAlso Me.grdDetailEntry.Row = -1) Then

                        Exit Sub

                    End If

                End If

            End If

            'objHelp = New uiCtrlGLAccount
            'if e.KeyChar = Chr(Keys.F1) AndAlso Me.grdDetailEntry.Col = EnumGridVoucher.DtlCode Then
            objHelp = New uiCtrlGLAccount
            If e.KeyData = Keys.F1 Then
                Dim frmAccount As New frmAccountHelp

                frmAccount.FilterCondition = objHelp.GLFilterCondition
                frmAccount.AccountHeadType = 4 'objHelp.GLAccountHeadType
                frmAccount.AccountType = "None"


                frmAccount.ShowDialog()


                If Not frmAccount.Tag Is Nothing Then

                    If IsTempVoucher = False Then
                        'Don't Allow duplicate enteries of Accounts
                        If Me.grdDetailEntry.RecordCount > 0 Then

                            'Dim DT As DataTable
                            'DT = CType(Me.grdDetailEntry.DataSource, DataTable)

                            'If DT.Columns(EnumGridVoucher.COADtlID).Unique = False Then
                            '    Dim UK As New UniqueConstraint("UK", DT.Columns(EnumGridVoucher.COADtlID), True)
                            '    DT.Constraints.Clear()
                            '    DT.Constraints.Add(UK)
                            'End If

                            'Dim drFind As DataRow
                            'drFind = DT.Rows.Find(CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(0).Value)

                            'If drFind Is Nothing Then
                            '    Me.grdDetailEntry.SetValue(EnumGridVoucher.COADtlID, CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(0).Value) ' Account ID ..
                            '    Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlCode, CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(1).Value) ' Account Code ..
                            '    Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlTitle, CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(2).Value) ' Account Title ..
                            'Else
                            '    ShowErrorMessage("A/c Already exist,Uplicate A/C Entry is not allowed")
                            'End If

                            'CR115: Allow Duplicate GL A/C Entry
                            If Me.cboVoucherType.Text = "BRV" Or Me.cboVoucherType.Text = "BPV" Then

                                If CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(3).Value = "Bank" Then

                                    Dim strGL As String = CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(1).Value

                                    If strGL.ToString = Me.ctrlGLAccounts.txtACCode.Text Then
                                        'Code commented against CR # 343 Multiple banks should be allowes in BPV and BRV
                                        'ShowErrorMessage("Bank A/c Already exist,Duplicate A/C Entry is not allowed")
                                        'Exit Sub
                                    Else

                                        Dim ROWNUMBER As Integer
                                        Dim intCOUNTER As Integer

                                        ROWNUMBER = Me.grdDetailEntry.GetRow().RowIndex

                                        'intCOUNTER = 0
                                        'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdDetailEntry.GetRows

                                        '    If IsDBNull(Row.Cells(EnumGridVoucher.DebitAmount).Value) AndAlso IsDBNull(Row.Cells(EnumGridVoucher.CreditAmount).Value) Then
                                        '        ShowErrorMessage("There must be entry of either Debit ot Credit")
                                        '        Me.grdDetailEntry.Row = intCount
                                        '        Me.grdDetailEntry.Col = EnumGridVoucher.DebitAmount
                                        '        Return False
                                        '    End If

                                        '    intCount += 1

                                        'Next
                                        'Code commented against CR # 343 multiple banks should be allowed in BPV and BRV
                                        'If ROWNUMBER = -1 Then 'If it's a New Row then
                                        '    ShowErrorMessage("One Bank A/C already exist, Multiple Bank A/C is not allowed")
                                        'End If

                                        'Exit Sub

                                    End If

                                End If

                            End If

                            Me.grdDetailEntry.SetValue(EnumGridVoucher.COADtlID, CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(0).Value) ' Account ID ..
                            Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlCode, CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(1).Value) ' Account Code ..
                            Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlTitle, CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(2).Value) ' Account Title ..

                        Else
                            'If Entry of Fisrt tuple in grid
                            Me.grdDetailEntry.SetValue(EnumGridVoucher.COADtlID, CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(0).Value) ' Account ID ..
                            Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlCode, CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(1).Value) ' Account Code ..
                            Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlTitle, CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(2).Value) ' Account Title ..

                        End If

                    ElseIf IsTempVoucher = True Then
                        'For Temp Voucher Allow duplicate Entries of Accounts

                        Me.grdDetailEntry.SetValue(EnumGridVoucher.COADtlID, CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(0).Value) ' Account ID ..
                        Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlCode, CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(1).Value) ' Account Code ..
                        Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlTitle, CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(2).Value) ' Account Title ..

                    End If


                End If

                frmAccount = Nothing

            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetailEntry_LinkClicked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetailEntry.LinkClicked
        Dim RowIndex As Integer

        Try
            'Cr # 210
            If Me.grdDetailEntry.GetRow().RowType <> Janus.Windows.GridEX.RowType.NewRecord Then


                ListOfDeletedIDs = ListOfDeletedIDs & "," & Me.grdDetailEntry.GetValue(EnumGridVoucher.VoucherDtlID).ToString

                ''Just setting var as true in case of Cash/Bank acc entry
                'If Me.cboVoucherType.Text = "BRV" Or Me.cboVoucherType.Text = "CRV" Or Me.cboVoucherType.Text = "BPV" Or Me.cboVoucherType.Text = "CPV" Then

                '    'if Cash/bank Acc Row
                '    If ((Me.grdDetailEntry.RecordCount > 0 AndAlso Me.grdDetailEntry.Row = 0) Or (Me.grdDetailEntry.RecordCount = 0 AndAlso Me.grdDetailEntry.Row = -1)) Then

                '        _IsCashBankRowExist = False

                '    End If

                'End If


                'Allow Delete bcz we don't want to delete record by pressing delete key

                Me.grdDetailEntry.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True

                RowIndex = Me.grdDetailEntry.Row

                If Me.cboVoucherType.Text = "BRV" Or Me.cboVoucherType.Text = "CRV" Or Me.cboVoucherType.Text = "BPV" Or Me.cboVoucherType.Text = "CPV" Then

                    'If (Me.grdDetailEntry.RecordCount > 0 AndAlso RowIndex = 0) Or (Me.grdDetailEntry.RecordCount = 0 AndAlso RowIndex = -1) Then

                    '    Me.ctrlGLAccounts.txtACCode.Text = String.Empty
                    '    Me.ctrlGLAccounts.txtAccountName.Text = String.Empty

                    'End If

                    If Me.grdDetailEntry.GetRow().Cells(EnumGridVoucher.COADtlID).Text = strAccountID Then
                        Me.ctrlGLAccounts.txtACCode.Text = String.Empty
                        Me.ctrlGLAccounts.txtAccountName.Text = String.Empty


                        strAccountID = ""
                        strAmmountHead = ""


                        _IsCashBankRowExist = False

                        Me.dtpChequeDate.Value = New VoucherDAL().GetDBCurrentDate
                        Me.txtChequeNo.Text = ""
                        Me.ctrlGLAccounts.Focus()


                    End If

                End If

                Me.grdDetailEntry.Delete()
                Me.grdDetailEntry.Update()
                Me.grdDetailEntry.Refetch()

                'False Allow delete bcz we don't want to delete record by pressing delete key
                Me.grdDetailEntry.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetailEntry_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdDetailEntry.MouseMove
        If grdDetailEntry.RowCount > 0 Then
            grdDetailEntry.GetRow().Cells(EnumGridVoucher.Comments).ToolTipText = grdDetailEntry.GetRow().Cells(EnumGridVoucher.Comments).Text
        End If

    End Sub

    Private Sub grdDetailEntry_RecordAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDetailEntry.RecordAdded
        Try

            Me.grdDetailEntry.Col = 0
            Me.grdDetailEntry.Row = -1

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmdPostVoucher_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPostVoucher.Click
        Dim DebitAmt As Double
        Dim CreditAmt As Double
        Dim intCurrentRow As Integer

        Try

            If Me.grdDetailEntry.RecordCount = 0 Then
                Throw New Exception("No Transaction found")
                Exit Sub
            End If

            'Checking Voucher is Balanced than post it Else show msg of unBalanced voucher to user
            DebitAmt = DebitTotal()
            CreditAmt = CreditTotal()

            'CR#300
            'If New VoucherDAL().IsVoucherBalanced(intPkId, pbLocationID, IsTempVoucher) = False Then
            If New VoucherDAL().IsVoucherBalanced(intPkId, Me.cboCompany.SelectedValue, IsTempVoucher) = False Then

                Throw New Exception("Voucher is Un-Balanced, it can not be Posted")

                Exit Sub

            Else

                If MessageBox.Show("Are you sure you want to Post this Voucher?", "Voucher Posting", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then

                    'Will be used when Temp Voucher will be posted for insertion of voucher in GL actual tables.
                    'If MessageBox.Show("Do you want to save this record?", "Voucher Save", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    _BlnSaveV = True
                    'Else
                    ' _BlnSaveV = False
                    'End If

                    FillModel("POSTING")

                    '11 Feb,2010 change for posting Navigation

                    If New VoucherDAL().PostVoucher(Me.mobjModel, IsTempVoucher) Then

                        'change by Farooq-H  CR# 254 
                        SMS(Me.mobjModel, EnumSMSAction.Post.ToString)
                        'Dim mObjDtlModal As New VoucherDetailItem
                        'Dim ListCount As Integer = mobjModel.ListofVouchers.Count
                        'Dim LoopCount As Integer = 0
                        'Dim TotalDabit As Double = 0.0
                        'For LoopCount = 0 To ListCount - 1
                        '    mObjDtlModal = mobjModel.ListofVouchers(LoopCount)
                        '    TotalDabit = TotalDabit + mObjDtlModal.DebitAmount
                        'Next
                        'If Me.cboVoucherType.Text = EnumVType.BPV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BPV_01.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.BRV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BRV_01.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.CPV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CPV_01.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.CRV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CRV_01.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.PV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_PV_01.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.SV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_SV_01.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'ElseIf Me.cboVoucherType.Text = EnumVType.JV.ToString Then
                        '    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_JV_01.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                        'End If

                        intCurrentRow = Me.grdVoucher.Row

                        ShowInformationMessage("Voucher has been Posted")

                        If InStr(StrVoucherIDs, ",") > 0 Then
                            If StrVoucherIDs.Length = InStr(StrVoucherIDs, intPkId.ToString) + (intPkId.ToString.Length - 1) Then
                                StrVoucherIDs = StrVoucherIDs.Remove(InStr(StrVoucherIDs, intPkId.ToString) - 2, Len(intPkId.ToString) + 1)
                            Else
                                StrVoucherIDs = StrVoucherIDs.Remove(InStr(StrVoucherIDs, intPkId.ToString) - 1, Len(intPkId.ToString) + 1)
                            End If
                        Else
                            StrVoucherIDs = ""
                            Me.lblTotalRecord.Text = ""
                        End If


                        'If InStr(StrVoucherIDs, ",") > 0 Then
                        '    If StrVoucherIDs.Length = InStr(StrVoucherIDs, intPkId.ToString) + (intPkId.ToString.Length - 1) Then
                        '        StrVoucherIDs = StrVoucherIDs.Remove(InStr(StrVoucherIDs, intPkId.ToString) - 2, Len(intPkId.ToString) + 1)
                        '    Else
                        '        StrVoucherIDs = StrVoucherIDs.Remove(InStr(StrVoucherIDs, intPkId.ToString) - 1, Len(intPkId.ToString) + 1)
                        '    End If
                        'Else
                        '    StrVoucherIDs = ""
                        'End If

                        'If gblnShowAfterUpdateMessages Then
                        ''Getting Save Confirmation from User

                        'End If

                        If Not StrVoucherIDs.Length > 0 Then

                            StrVoucherIDs = "0"
                            ''Query to Database and get fressh modifications in the Grid
                            Me.GetAllRecords()
                            '  Me.grdVoucher.Row = intCurrentRow
                            'If Me.grdVoucher.Row >= 0 Then Me.grdVoucher.Row = (Me.grdVoucher.Row + 1)
                            'Me.grdVoucher.Row = -1
                            Me.grdVoucher.DataSource = Nothing
                            'grdVoucher_SelectionChanged(Nothing, Nothing)

                            ' ''to select the last updated record
                            'For Rind As Int16 = 0 To (grdDetailEntry.RowCount - 1)
                            '    If Me.grdDetailEntry.GetRow(Rind).Cells(EnumGridVoucher.VoucherDtlID).Value = mobjModel.voucherID Then
                            '        Me.grdDetailEntry.Row = Rind
                            '        Exit For
                            '    End If
                            'Next
                            Me.ReSetControls()
                            'CR#85
                            _IsCashBankRowExist = False
                            strAccountID = ""
                            strAmmountHead = ""
                            Exit Sub

                        Else

                            Me.GetAllRecords()
                            Me.grdVoucher.Row = intCurrentRow
                            'If Me.grdVoucher.Row >= 0 Then Me.grdVoucher.Row = (Me.grdVoucher.Row + 1)

                            grdVoucher_SelectionChanged(Nothing, Nothing)

                            ''to select the last updated record
                            'CR#310
                            'For Rind As Int16 = 0 To (grdDetailEntry.RowCount - 1)
                            For Rind As Integer = 0 To (grdDetailEntry.RowCount - 1)
                                If Me.grdDetailEntry.GetRow(Rind).Cells(EnumGridVoucher.VoucherDtlID).Value = mobjModel.voucherID Then
                                    Me.grdDetailEntry.Row = Rind + 1
                                    Exit For
                                End If
                            Next

                        End If

                    End If


                    'InStr(StrVoucherIDs, ",")

                    '   Dim arrVoucherIDs As String() = Split(StrVoucherIDs,",")

                    'If StrVoucherIDs.Length = InStr(StrVoucherIDs, intPkId.ToString) + (intPkId.ToString.Length - 1) Then
                    '    PKeyPOS = InStr(StrVoucherIDs, intPkId.ToString) - 2
                    'Else
                    '    PKeyPOS = InStr(StrVoucherIDs, intPkId.ToString) - 1


                    'End If

                    ''If New VoucherDAL().PostVoucher(Me.mobjModel, IsTempVoucher) Then

                    ''    Dim IDsSkip As String
                    ''    Dim PKeyStartIndex As Integer
                    ''    Dim intLen As Integer

                    ''    PKeyStartIndex = InStr(StrVoucherIDs, intPkId.ToString) + intPkId.ToString.Length
                    ''    intLen = StrVoucherIDs.Length - PKeyStartIndex
                    ''    IDsSkip = StrVoucherIDs.Substring(PKeyStartIndex, intLen)
                    ''    IDsSkip = Microsoft.VisualBasic.Left(IDsSkip, InStr(IDsSkip.ToString, ",") - 1)

                    ''    If InStr(StrVoucherIDs, ",") > 0 Then
                    ''        If StrVoucherIDs.Length = InStr(StrVoucherIDs, intPkId.ToString) + (intPkId.ToString.Length - 1) Then
                    ''            StrVoucherIDs = StrVoucherIDs.Remove(InStr(StrVoucherIDs, intPkId.ToString) - 2, Len(intPkId.ToString) + 1)
                    ''        Else
                    ''            StrVoucherIDs = StrVoucherIDs.Remove(InStr(StrVoucherIDs, intPkId.ToString) - 1, Len(intPkId.ToString) + 1)
                    ''        End If
                    ''    Else
                    ''        StrVoucherIDs = ""
                    ''        Me.lblTotalRecord.Text = ""
                    ''    End If


                    ''    'If InStr(StrVoucherIDs, ",") > 0 Then
                    ''    '    If StrVoucherIDs.Length = InStr(StrVoucherIDs, intPkId.ToString) + (intPkId.ToString.Length - 1) Then
                    ''    '        StrVoucherIDs = StrVoucherIDs.Remove(InStr(StrVoucherIDs, intPkId.ToString) - 2, Len(intPkId.ToString) + 1)
                    ''    '    Else
                    ''    '        StrVoucherIDs = StrVoucherIDs.Remove(InStr(StrVoucherIDs, intPkId.ToString) - 1, Len(intPkId.ToString) + 1)
                    ''    '    End If
                    ''    'Else
                    ''    '    StrVoucherIDs = ""
                    ''    'End If

                    ''    'If gblnShowAfterUpdateMessages Then
                    ''    ''Getting Save Confirmation from User
                    ''    ShowInformationMessage("Voucher has been Posted")
                    ''    'End If

                    ''    If Not StrVoucherIDs.Length > 0 Then

                    ''        StrVoucherIDs = "0"
                    ''        ''Query to Database and get fressh modifications in the Grid
                    ''        Me.GetAllRecords()

                    ''        If Me.grdVoucher.Row >= 0 Then Me.grdVoucher.Row = (Me.grdVoucher.Row + 1)

                    ''        grdVoucher_SelectionChanged(Nothing, Nothing)

                    ''        ''to select the last updated record
                    ''        For Rind As Int16 = 0 To (grdDetailEntry.RowCount - 1)
                    ''            'If Me.grdDetailEntry.GetRow(Rind).Cells(EnumGridVoucher.VoucherDtlID).Value = mobjModel.voucherID Then
                    ''            If Me.grdDetailEntry.GetRow(Rind).Cells(EnumGridVoucher.VoucherDtlID).Value = IDsSkip Then
                    ''                Me.grdDetailEntry.Row = Rind
                    ''                Exit For
                    ''            End If
                    ''        Next

                    ''    Else

                    ''        Me.GetAllRecords()
                    ''        If Me.grdVoucher.Row >= 0 Then Me.grdVoucher.Row = (Me.grdVoucher.Row + 1)

                    ''        grdVoucher_SelectionChanged(Nothing, Nothing)

                    ''        ''to select the last updated record
                    ''        For Rind As Int16 = 0 To (grdDetailEntry.RowCount - 1)
                    ''            'If Me.grdDetailEntry.GetRow(Rind).Cells(EnumGridVoucher.VoucherDtlID).Value = mobjModel.voucherID Then
                    ''            If Me.grdDetailEntry.GetRow(Rind).Cells(EnumGridVoucher.VoucherDtlID).Value = IDsSkip Then
                    ''                Me.grdDetailEntry.Row = Rind + 1
                    ''                Exit For
                    ''            End If
                    ''        Next

                    ''    End If

                    ''End If


                End If

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            _BlnSaveV = False
        End Try
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Try

            PrintReport()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal blnIsTempVoucher As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ctrlGLAccounts_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlGLAccounts.Enter
        strCashBankAcc = Me.ctrlGLAccounts.txtACCode.Text.ToString
    End Sub

    Private Sub ctrlGLAccounts_GetGLAccount1(ByVal sender As uiCtrlGLAccount) Handles ctrlGLAccounts.GetGLAccount

        Try

            ' If strCashBankAcc = Me.ctrlGLAccounts.txtACCode.Text.ToString Then Exit Sub

            If Me.cboVoucherType.Text = "BRV" Or Me.cboVoucherType.Text = "BPV" Or Me.cboVoucherType.Text = "CRV" Or Me.cboVoucherType.Text = "CPV" Then

                If strAccountID = "" Then
                    'Me.grdDetailEntry.Row = 0
                    'Else
                    Me.grdDetailEntry.Row = -1
                Else
                    '  Exit Sub

                End If

                'When Total Row is selected and user change Cash/Bank info account than Error msg appears
                If Me.grdDetailEntry.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow AndAlso Me.grdDetailEntry.RowCount > 0 Then
                    Me.grdDetailEntry.Row = (Me.grdDetailEntry.Row - 1)
                    ' Exit Sub
                End If

                'CR#118

                If Me.grdDetailEntry.RecordCount > 0 Then

                    Dim ROWNUMBER As Integer
                    ROWNUMBER = Me.grdDetailEntry.GetRow().RowIndex
                    If ROWNUMBER <> 0 Then
                        Me.grdDetailEntry.Row = 0   'Change Cash/Bank Info Account   
                    End If

                End If


                Me.grdDetailEntry.Col = EnumGridVoucher.DtlCode
                Me.grdDetailEntry.SetValue(EnumGridVoucher.DtlCode, Me.ctrlGLAccounts.txtACCode.Text.ToString)
                SetSelectedAccDetailinGrid()

                Me.txtVoucherNarration.Focus()

                strAccountID = Me.ctrlGLAccounts.GLAccountID
                strAmmountHead = Me.lblDrCr.Text


                ' ctrlGLAccounts.txtACCode.Focus()
                ' ctrlGLAccounts.txtAccountName.TabStop = False



            End If

            LngCashBankAccID = Me.ctrlGLAccounts.GLAccountID

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmount.TextChanged
        Try

            If Not IsNumeric(Me.txtAmount.Text) Then Me.txtAmount.Text = ""
            If Val(Me.txtAmount.Text) > 0 Then
                Me.txtAmount.Text = Format(CDbl(Me.txtAmount.Text), IIf(gintAmountRound = 0, "###,###,##0", "###,###,##0." & New String("0", gintAmountRound)))
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAmount_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount.Validated
        Try

            If Val(Me.txtAmount.Text) < -999999999.99 Then
                ShowErrorMessage("Value can't be less then -999999999.99")
            ElseIf Val(Me.txtAmount.Text) > 999999999.99 Then
                ShowErrorMessage("Value can't be greater then 999999999.99")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmdSearchVoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearchVoucher.Click
        Try

            _TempVouchers = IsTempVoucher

            CType(Me.MdiParent, MDIParent1).MnuCustomerAddressBlockage_Click(Nothing, Nothing)

            For Each exfrm As Form In Application.OpenForms
                If exfrm.Name = frmGLPostingVoucher.Name Then
                    Dim ObjForm As frmGLPostingVoucher = CType(exfrm, frmGLPostingVoucher)
                    exfrm.BringToFront()
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

#End Region

    Private Sub grdDetailEntry_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDetailEntry.SelectionChanged
        ' grdDetailEntry.GetRow().Cells(EnumGridVoucher.Comments).ToolTipText = grdDetailEntry.GetRow().Cells(EnumGridVoucher.Comments).Text

    End Sub

 
    Private Sub grdDetailEntry_UpdatingRecord(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdDetailEntry.UpdatingRecord
        Try

            ''Don't Allow to enter invalid Acc Dtl code in added row
            If Not Me.grdDetailEntry.Row = -1 Then

                'For Non Cash/Bank row
                If Not ((Me.grdDetailEntry.RecordCount > 0 AndAlso Me.grdDetailEntry.Row = 0) Or (Me.grdDetailEntry.RecordCount = 0 AndAlso Me.grdDetailEntry.Row = -1)) Then

                    ' ''Don't Allow to Add Record if Dtl Acc Code is not valid/empty
                    If (Not Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode) Is Nothing) AndAlso (Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode).ToString.Trim = "") Then

                        ShowErrorMessage("You must have to enter a valid Acc Detail Code")
                        Me.grdDetailEntry.Col = EnumGridVoucher.DtlCode
                        e.Cancel = True

                    End If

                End If

            End If
            


            '' ''Don't Allow to Add Record if Dtl Acc Code is not valid/empty
            'If (Not Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode) Is Nothing) AndAlso (Me.grdDetailEntry.GetValue(EnumGridVoucher.DtlCode).ToString.Trim = "") Then
            '    'e.Cancel = True
            '    Me.grdDetailEntry.Delete()
            '    Me.grdDetailEntry.Update()
            '    Me.grdDetailEntry.Refetch()
            '    e.Cancel = True
            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

   
    Private Sub ctrlGLAccounts_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlGLAccounts.Validated
        Try

            '_IsCashBankRowExist = False

            'If Me.ctrlGLAccounts.txtACCode.Text.Trim.Length > 0 Then
            '    _IsCashBankRowExist = True
            'Else
            '    _IsCashBankRowExist = False
            'End If
            If strCashBankAcc = Me.ctrlGLAccounts.txtACCode.Text.ToString Then Exit Sub

            'If _IsCashBankRowExist Then 'AndAlso Me.grdDetailEntry.RowCount > 0 Then
            '    Me.grdDetailEntry.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            '    Me.grdDetailEntry.Row = 0
            '    Me.grdDetailEntry.Delete()
            '    Me.grdDetailEntry.Update()
            '    Me.grdDetailEntry.Refetch()
            '    Me.grdDetailEntry.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            'End If

            _IsCashBankRowExist = False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkOtherVoucher_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOtherVoucher.CheckedChanged

        If Me.grdVoucher.RootTable Is Nothing Then
            Exit Sub
        End If

        If Me.grdVoucher.RowCount = 0 Then
            Exit Sub
        End If

        If chkOtherVoucher.Checked = False Then
            Me.txtVNo.Text = Me.grdVoucher.GetValue(EnumMasterGrid.VoucherNo).ToString

        End If

        Dim objVoucher As VoucherDAL
        objVoucher = New VoucherDAL()
        'CR#300
        'Me.txtVNo.Text = objVoucher.GetNewVoucherNo(pbLocationID, Me.cboVoucherType.SelectedValue.ToString, Format(Me.dtpVoucherDate.Value, "MMM"), Format(Me.dtpVoucherDate.Value, "yyyy"), Me.chkOtherVoucher.Checked, IsTempVoucher)
        Me.txtVNo.Text = objVoucher.GetNewVoucherNo(Me.cboCompany.SelectedValue, Me.cboVoucherType.SelectedValue.ToString, Format(Me.dtpVoucherDate.Value, "MMM"), Format(Me.dtpVoucherDate.Value, "yyyy"), Me.chkOtherVoucher.Checked, IsTempVoucher)
        'replace the voucher no from '1' to '000001'
        Me.txtVNo.Text = funDoPadding(Me.txtVNo.Text, Me.txtVNo.MaxLength)

    End Sub

    Private Sub dtpVoucherDate_LostFocus1(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpVoucherDate.LostFocus
        Try


            Dim objVoucher As VoucherDAL
            objVoucher = New VoucherDAL()

            'CR# 45
            If Me.dtpVoucherDate.Value.Month <> _CurrMonth Or Me.dtpVoucherDate.Value.Year <> _CurrYear Then
                'CR#300
                'CR#300
                If Me.cboCompany.SelectedIndex = 0 Then
                    MessageBox.Show("Please select a valid company")
                    Exit Sub
                    Me.cboCompany.Focus()
                End If

                'Me.txtVNo.Text = objVoucher.GetNewVoucherNo(pbLocationID, Me.cboVoucherType.SelectedValue.ToString, Format(Me.dtpVoucherDate.Value, "MMM"), Format(Me.dtpVoucherDate.Value, "yyyy"), Me.chkOtherVoucher.Checked, IsTempVoucher)
                Me.txtVNo.Text = objVoucher.GetNewVoucherNo(Me.cboCompany.SelectedValue, Me.cboVoucherType.SelectedValue.ToString, Format(Me.dtpVoucherDate.Value, "MMM"), Format(Me.dtpVoucherDate.Value, "yyyy"), Me.chkOtherVoucher.Checked, IsTempVoucher)
                Me.txtVNo.Text = funDoPadding(Me.txtVNo.Text, Me.txtVNo.MaxLength)

            End If

            If Me.dtpVoucherDate.Value.Month <> _VMonth Or Me.chkOtherVoucher.Checked <> _IsOtherVoucher Then

                'Me.txtVNo.Text = objVoucher.GetNewVoucherNo(pbLocationID, Me.cboVoucherType.SelectedValue.ToString, Format(Me.dtpVoucherDate.Value, "MMM"), Format(Me.dtpVoucherDate.Value, "yyyy"), Me.chkOtherVoucher.Checked, IsTempVoucher)
                Me.txtVNo.Text = objVoucher.GetNewVoucherNo(Me.cboCompany.SelectedValue, Me.cboVoucherType.SelectedValue.ToString, Format(Me.dtpVoucherDate.Value, "MMM"), Format(Me.dtpVoucherDate.Value, "yyyy"), Me.chkOtherVoucher.Checked, IsTempVoucher)
                Me.txtVNo.Text = funDoPadding(Me.txtVNo.Text, Me.txtVNo.MaxLength)

            End If

            'If Me.dtpVoucherDate.Value.Month = _VMonth AndAlso Me.chkOtherVoucher.Checked = _IsOtherVoucher Then
            If Me.dtpVoucherDate.Value.Month = _VMonth AndAlso Me.chkOtherVoucher.Checked = True Then
                Me.txtVNo.Text = m_VNo
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub SetActionControlsOFF() 'CR#86
        Try

            Me.btnSave.Enabled = False
            Me.btnUpdate.Enabled = False
            Me.btnDelete.Enabled = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SetActionControlsON() 'CR#86
        Try

            If intPkId = 0 Then

                Me.btnSave.Enabled = True
                Me.btnCancel.Enabled = True

            ElseIf intPkId > 0 Then

                Me.btnUpdate.Enabled = True
                Me.btnDelete.Enabled = True

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ApplyVTypesRights() ''//'CR#86
        Try



            If Me.cboVoucherType.Text = EnumVType.JV.ToString Then

                If Not mobjControlList.Item("JV") Is Nothing Then
                    SetActionControlsOFF()
                End If

            ElseIf Me.cboVoucherType.Text = EnumVType.CPV.ToString Then

                If Not mobjControlList.Item("CP") Is Nothing Then
                    SetActionControlsOFF()
                End If

            ElseIf Me.cboVoucherType.Text = EnumVType.CRV.ToString Then

                If Not mobjControlList.Item("CR") Is Nothing Then
                    SetActionControlsOFF()
                End If

            ElseIf Me.cboVoucherType.Text = EnumVType.BPV.ToString Then

                If Not mobjControlList.Item("BP") Is Nothing Then
                    SetActionControlsOFF()
                End If

            ElseIf Me.cboVoucherType.Text = EnumVType.BRV.ToString Then

                If Not mobjControlList.Item("BR") Is Nothing Then
                    SetActionControlsOFF()
                End If

            ElseIf Me.cboVoucherType.Text = EnumVType.SV.ToString Then

                If Not mobjControlList.Item("SV") Is Nothing Then
                    SetActionControlsOFF()
                End If

            ElseIf Me.cboVoucherType.Text = EnumVType.PV.ToString Then

                If Not mobjControlList.Item("PV") Is Nothing Then
                    SetActionControlsOFF()
                End If

            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'New Function Multiple Bank Account should not be allowed in a BPV/BRV also same Bank Account should not duplicate
    Private Function ValidateBankVoucher() As Boolean
        Dim intBAccount As Integer

        Try

           

            If Me.cboVoucherType.Text = "BRV" Or Me.cboVoucherType.Text = "BPV" Then

                intBAccount = 0
                Dim BlnBankAcc As Boolean

                Dim DtItems As DataTable = CType(Me.grdDetailEntry.DataSource, DataTable)
                DtItems.AcceptChanges()

                For Each r As DataRow In DtItems.Rows

                    BlnBankAcc = New VoucherDAL().GetAccountType(r.Item(EnumGridVoucher.COADtlID).ToString)

                    If BlnBankAcc = True Then
                        intBAccount += 1    'Will let us know Total Bank Account in a Voucher
                    End If

                Next

                'Code commented against CR # 343 System should allow multiple banks in BPV and BRV
                'If intBAccount > 1 Then
                '    ShowErrorMessage("Only single Bank Account is allowed in BPV/BRV")
                '    Return False
                'ElseIf intBAccount = 0 Then
                '    ShowErrorMessage("Atleast one Bank Account must be selected")
                '    Return False
                'Else
                '    Return True
                'End If

                'CR # 343
                If intBAccount = 0 Then
                    ShowErrorMessage("Atleast one Bank Account must be selected")
                    Return False
                Else
                    Return True
                End If

            Else

                Return True

            End If

            
        Catch ex As Exception

        End Try
    End Function

    Private Sub SMS(ByVal ObjVoucher As GLVoucher, ByVal Condition As String)
        Try
            If DAL.SystemConfigurationDAL.GetSystemConfigurationValue("SendSMS").ToString.ToUpper <> "TRUE" Then
                Exit Sub
            End If
            Dim mObjDtlModal As New VoucherDetailItem
            Dim ListCount As Integer = mobjModel.ListofVouchers.Count
            Dim LoopCount As Integer = 0
            Dim TotalDabit As Double = 0.0
            Dim AccountID As Integer = 0
            For LoopCount = 0 To ListCount - 1
                mObjDtlModal = mobjModel.ListofVouchers(LoopCount)
                TotalDabit = TotalDabit + mObjDtlModal.DebitAmount
                If New VoucherDAL().GetAccountTypeName(mObjDtlModal.COADetailID) Then
                    AccountID = mObjDtlModal.COADetailID
                End If
            Next
            
            If Condition.ToString = EnumSMSAction.Delete.ToString Then

                If Me.cboVoucherType.Text = EnumVType.BPV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BPV_05.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.BRV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BRV_05.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.CPV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CPV_05.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.CRV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CRV_05.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.PV.ToString Then

                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_PV_05.ToString & ";" & EnumSMSCodes.SMS_VE_PV_08.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ", AccountID)
                ElseIf Me.cboVoucherType.Text = EnumVType.SV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_SV_05.ToString & ";" & EnumSMSCodes.SMS_VE_SV_08.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ", AccountID)
                ElseIf Me.cboVoucherType.Text = EnumVType.JV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_JV_05.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                End If

            ElseIf Condition.ToString = EnumSMSAction.Update.ToString Then

                If Me.cboVoucherType.Text = EnumVType.BPV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BPV_04.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.BRV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BRV_04.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.CPV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CPV_04.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.CRV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CRV_04.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.PV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_PV_04.ToString & ";" & EnumSMSCodes.SMS_VE_PV_07.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ", AccountID)
                ElseIf Me.cboVoucherType.Text = EnumVType.SV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_SV_04.ToString & ";" & EnumSMSCodes.SMS_VE_SV_07.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ", AccountID)
                ElseIf Me.cboVoucherType.Text = EnumVType.JV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_JV_04.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                End If

            ElseIf Condition.ToString = EnumSMSAction.Save.ToString Then

                If Me.cboVoucherType.Text = EnumVType.BPV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BPV_03.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.BRV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BRV_03.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.CPV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CPV_03.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.CRV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CRV_03.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.PV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_PV_03.ToString & ";" & EnumSMSCodes.SMS_VE_PV_06.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ", AccountID)
                ElseIf Me.cboVoucherType.Text = EnumVType.SV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_SV_03.ToString & ";" & EnumSMSCodes.SMS_VE_SV_06.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ", AccountID)
                ElseIf Me.cboVoucherType.Text = EnumVType.JV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_JV_03.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                End If

            ElseIf Condition.ToString = EnumSMSAction.Post.ToString Then

              
                If Me.cboVoucherType.Text = EnumVType.BPV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BPV_01.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.BRV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_BRV_01.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.CPV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CPV_01.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.CRV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_CRV_01.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.PV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_PV_01.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.SV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_SV_01.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                ElseIf Me.cboVoucherType.Text = EnumVType.JV.ToString Then
                    mObjSMS.SendSMS(EnumSMSCodes.SMS_VE_JV_01.ToString, "&VocuherCode&=" & Me.mobjModel.VoucherCode.ToString & ";&ParamVoucherdate&=" & Me.mobjModel.VoucherDate.ToString("d") & ";&ParamVAmount&=" & TotalDabit.ToString & " ")
                End If

             
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    '***************** Start CR # 268   Rana Saeed
    Private Sub btnPrintSVorPV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintInvoice.Click
        Try
            'CR # 334
            'if selected company name contains Lumensoft then print lumensoft invoice
            If Me.cboCompany.Text.ToUpper.Contains("LUMENSOFT") AndAlso Me.cboVoucherType.Text = EnumVType.SV.ToString Then
                Dim frmSaleTax As New frmSalesTaxInvoiceInfo
                Dim dt As DataTable = grdDetailEntry.DataSource
                For Each dr As DataRow In dt.Rows
                    If Val(dr(EnumGridVoucher.DebitAmount).ToString) <> 0.0 Then
                        If frmSaleTax.VoucherDes = String.Empty Then
                            frmSaleTax.VoucherDes = dr(EnumGridVoucher.Comments).ToString
                            frmSaleTax.VDesc = dr(EnumGridVoucher.Comments).ToString
                        End If
                        'Start of CR # 346
                        If frmSaleTax.VoucherNO = 0 Then
                            frmSaleTax.VoucherNO = Me.txtVNo.Text
                        End If
                        'End of CR # 346
                        If frmSaleTax.VoucherID = 0 Then
                            frmSaleTax.VoucherID = intPkId
                        End If
                        If frmSaleTax.LocationID = 0 Then
                            frmSaleTax.LocationID = intLocId
                        End If
                        If frmSaleTax.CustomerName = String.Empty Then
                            frmSaleTax.CustomerName = dr(EnumGridVoucher.DtlTitle).ToString
                        Else
                            frmSaleTax.CustomerName = frmSaleTax.CustomerName & "," & dr(EnumGridVoucher.DtlTitle).ToString
                        End If
                        'CR # 349
                        frmSaleTax.VoucherDate = Me.dtpVoucherDate.Value.Date
                        'Exit For
                    End If
                Next
                frmSaleTax.Allowexport = IIf(mobjControlList.Item("btnExport") Is Nothing, False, True)
                frmSaleTax.AllowPrint = IIf(mobjControlList.Item("btnPrintInvoice") Is Nothing, False, True)
                frmSaleTax.ShowDialog()
                Exit Sub
            Else
                Call Me.ShowInv_Sales_Purchase()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ShowInv_Sales_Purchase()
        Try
            If Me.intPkId > 0 Then

                If New VoucherDAL().ViewInvFor_Sales_Purchase(intPkId, intLocId, IsTempVoucher) Then            '(create view for the PO/SI report)

                    Call Me.AddRptParamsFor_Sales_Purchase()                        '(fill report parameters)

                    'ShowInformationMessage("View Created")
                    Dim rptViewer As New rptViewer
                    rptViewer.Text = Me.Text
                    rptViewer.Show()
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddRptParamsFor_Sales_Purchase()
        Try
            Dim objHashTableParamter As New Hashtable

            If Me.cboVoucherType.Text = EnumVType.PV.ToString Then
                objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_PV.rpt")
            ElseIf Me.cboVoucherType.Text = EnumVType.SV.ToString Then
                
                objHashTableParamter.Add("ReportPath", "\rptGlVouchersingle_SV.rpt")
            End If

            Dim ObjCompanyData As DataTable
            ObjCompanyData = UtilityDAL.setCompanyInfo(gobjLocationInfo.CompanyID)

            ' Passing Parameters .. (Report Parameters .. )
            objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
            objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))
            objHashTableParamter.Add("ShowReceivedBy", False)

            If mobjControlList.Item("btnPrintInvoice") Is Nothing Then
                objHashTableParamter.Add("PrintRights", "False")
            Else
                objHashTableParamter.Add("PrintRights", "True")
            End If

            If mobjControlList.Item("btnExport") Is Nothing Then
                objHashTableParamter.Add("ExportRights", "False")
            Else
                objHashTableParamter.Add("ExportRights", "True")
            End If

            gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '************** End of CR # 268 Rana Saeed
    'CR#300
    Private Sub cboCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCompany.SelectedIndexChanged
        Try


            If Me.cboCompany.SelectedIndex > 0 Then

                Dim objVoucher As VoucherDAL
                objVoucher = New VoucherDAL()

                Me.txtVNo.Text = objVoucher.GetNewVoucherNo(Me.cboCompany.SelectedValue, Me.cboVoucherType.SelectedValue.ToString, Format(Me.dtpVoucherDate.Value, "MMM"), Format(Me.dtpVoucherDate.Value, "yyyy"), Me.chkOtherVoucher.Checked, IsTempVoucher)
                'replace the voucher no from '1' to '000001'
                Me.txtVNo.Text = funDoPadding(Me.txtVNo.Text, Me.txtVNo.MaxLength)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    ''// 323     23 Jul,2014          farooq-H  
    
    Private Sub BtnCheckPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCheckPrint.Click
        Try
            Dim frm As New frmGLChequePopUp
            frm.PKR = Val(txtAmount.Text.ToString.Replace(",", ""))
            frm.ChequeDate = dtpChequeDate.Value.Date
            Dim AmountInWords As String = ""
            Dim Cross As Boolean
            Dim dtChequeDetails As DataTable

            If Me.intPkId > 0 Then
                If txtAmount.Text.ToString.Equals(String.Empty) Then
                    Exit Sub
                End If
                Dim Name As String = ""
                Dim dt As DataTable = grdDetailEntry.DataSource
                For Each dr As DataRow In dt.Rows
                    If Val(dr(EnumGridVoucher.DebitAmount).ToString) <> 0.0 Then
                        Name = dr(EnumGridVoucher.DtlTitle).ToString
                        Exit For
                    End If
                Next
                frm.Pay = Name.ToString
                dtChequeDetails = New VoucherDAL().GetAccountChequeType(Me.ctrlGLAccounts.GLAccountID)
                If dtChequeDetails.Rows.Count < 1 Then
                    Throw New Exception("Please map cheque type first!")
                End If
                frm.ChequeType = dtChequeDetails.Rows(0).Item(1).ToString
                If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim objVoucher As New VoucherDAL()
                    Name = frm.Pay

                    objVoucher.ViewPrintBankChecks(Name, dtpChequeDate.Value.Date, Val(txtAmount.Text.ToString.Replace(",", "")))
                    AmountInWords = frm.AmountInWords
                    Cross = frm.CrossCheque
                    frm.Close()
                Else
                    frm.Close()
                    Exit Try
                End If

                '(create view for the PO/SI report)

                Call Me.AddRptParamsFor_PrintBankChecks(AmountInWords, Cross, dtChequeDetails.Rows(0).Item(2).ToString)                        '(fill report parameters)

                Dim rptViewer As New rptViewer
                rptViewer.Text = Me.Text
                rptViewer.Show()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    ''// 323     23 Jul,2014          farooq-H  
    Public Sub AddRptParamsFor_PrintBankChecks(ByVal AmountWords As String, ByVal IsCross As Boolean, ByVal ReportName As String)
        Try
            Dim objHashTableParamter As New Hashtable

            'If Me.cboVoucherType.Text = EnumVType.PV.ToString Then
            objHashTableParamter.Add("ReportPath", "\Cheques\" & ReportName.ToString)
            'objHashTableParamter.Add("ReportPath", "\Cheques\crptChequeBankAlfalah.rpt")
           

            '' Passing Parameters .. (Report Parameters .. )
            objHashTableParamter.Add("AmountWords", AmountWords)
            If IsCross Then
                objHashTableParamter.Add("IsCross", "True")
            Else
                objHashTableParamter.Add("IsCross", "False")
            End If


            If mobjControlList.Item("ChequePrint") Is Nothing Then
                objHashTableParamter.Add("PrintRights", "False")
            Else
                objHashTableParamter.Add("PrintRights", "True")
            End If

            If mobjControlList.Item("btnExport") Is Nothing Then
                objHashTableParamter.Add("ExportRights", "False")
            Else
                objHashTableParamter.Add("ExportRights", "True")
            End If

            gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

   
End Class
