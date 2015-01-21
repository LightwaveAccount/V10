''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : GL Voucher .. 
''// Programmer	     : Tariq Majeed
''// Creation Date	 : 17-July-2009
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// CR#    Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// 29      14 April,2010     Abdul Jabbar        Profit & Loss notes printer shiould be cost center wise.
''// 40      19 Oct,2010       Abdul Jabbar        Filtering on detail level account in Profit & Loss notes report
''// 105     26 Nov,2010       Abdul Jabbar        Balance Sheet/Profit & Loss Issue.Balance Sheet doesn't show the previous year data.Same is case with Profit & Loss report.
''// 146     22 Jul,2011       Abdul Jabbar        Shop wise filter on Profit & Loss report 
''// 175     19 Jan,2012       Abdul Jabbar        Profit & Loss Notes detail report should show net Profit or loss.
''// 244     19 sep,2013       Fatima Tajammal     Proft and Loss Report: Wrong Spelling of 'Statement' in crystal report
''// 320     17 Jul,2014       Abdul Jabbar        Include source filter in Trial Balance, Balance Sheet and P&L Report
''/////////////////////////////////////////////////////////////////////////////////////////

Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic

Public Class frmRptProfitandLoss
    Implements IGeneral, IReportsInterface

    Dim dblCashBankOpening As Double
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection

    Private Sub frmRptProfitandLoss_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            If e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            ElseIf e.Control And e.KeyCode = Keys.P Then
                If Me.btnPrint.Enabled = True Then btnGenerateButton_Click(Nothing, Nothing)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGLVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ''Getting all available controls list to the user on this screen; in a collection 
        mobjControlList = GetFormSecurityControls(Me.Name)

        Me.ApplySecurity(EnumDataMode.Disabled)
        Me.SetButtonImages()

        dtFromDate.Value = DateAdd(DateInterval.Day, -7, Now)
        dtToDate.Value = Now

        ' Fill Combos Of Financial Year, Company nd Voucher Type ..   
        FillCombos()

        SetConfigurationBaseSetting()



    End Sub

#Region "Report Interface Metholds .. "

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try


            If Mode.ToString = EnumDataMode.Disabled.ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System           
                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(EnumDataMode.[New])
                ''Me.grdAllRecords.Enabled = True

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

                ''Me.grdAllRecords.Enabled = False

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

                ''Me.grdAllRecords.Enabled = True

                ''Me.grdAllRecords.Focus()

            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System


                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

                SetNavigationButtons(Mode)

                ''Me.grdAllRecords.Enabled = True

                ''Me.grdAllRecords.Focus()

            End If


            '' Disabl/Enable the Button that converts Grid data in Excel Sheet According to Login User rights
            ''If mobjControlList.Item("btnExport") Is Nothing Then
            ''Me.UiCtrlGridBar1.btnExport.Enabled = False
            ''End If


            '' Disabl/Enable the Button that Prints Grid data According to Login User rights
            ''If mobjControlList.Item("btnPrint") Is Nothing Then
            ''Me.UiCtrlGridBar1.btnPrint.Enabled = False
            ''End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

        Dim strSQL As String = ""
        Dim ObjDataTable As DataTable
        Dim ObjDataRow As DataRow


        ' Binding Financial Year Combo .. 
        ' =========================================================================================
        ' =========================================================================================
        Dim ObjDalFinancialYear As New FiniancialYearDefDAL
        ObjDataTable = ObjDalFinancialYear.GetAll()


        ObjDataRow = ObjDataTable.NewRow
        ObjDataRow.Item("FYear Code") = gstrComboZeroIndexString
        ObjDataRow.Item("FYear ID") = 0
        ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


        cmbFinancialYear.DataSource = ObjDataTable.Copy


        cmbFinancialYear.DisplayMember = "FYear Code"
        cmbFinancialYear.ValueMember = "FYear ID"

        ObjDalFinancialYear = Nothing

        If Me.cmbFinancialYear.Items.Count > 1 Then
            Me.cmbFinancialYear.SelectedValue = gObjFinancialYearInfo.FYearID

        End If
        ' =========================================================================================
        ' =========================================================================================



        ' Binding Voucher Type .. 
        ' =========================================================================================
        ' =========================================================================================
        'strSQL = " SELECT voucher_type VoucherType, voucher_type_id TypeID FROM tblGlDefVoucherType "
        Dim ObjGLCostCenterDal As New GLCostCenterDal
        ObjDataTable = ObjGLCostCenterDal.GetAll()

        ObjDataRow = ObjDataTable.NewRow
        ObjDataRow.Item(1) = gstrComboZeroIndexString
        ObjDataRow.Item(0) = 0
        ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


        cmbCostCenter.DisplayMember = ObjDataTable.Columns(1).ColumnName
        cmbCostCenter.ValueMember = ObjDataTable.Columns(0).ColumnName
        cmbCostCenter.DataSource = ObjDataTable.Copy

        'cmbCostCenter.ValueMember = "TypeID"
        '=========================================================================================
        '=========================================================================================




        ' Binding Company .. 
        ' =========================================================================================
        ' =========================================================================================
        Dim ObjDalCompany As New CompanyDAL
        ObjDataTable = ObjDalCompany.GetAll(gObjUserInfo.UserID)


        ObjDataRow = ObjDataTable.NewRow
        ObjDataRow.Item("Company Name") = gstrComboZeroIndexString
        ObjDataRow.Item("Company ID") = 0
        ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


        cmbCompany.DataSource = ObjDataTable.Copy


        cmbCompany.DisplayMember = "Company Name"
        cmbCompany.ValueMember = "Company ID"

        ObjDalCompany = Nothing

        If Me.cmbCompany.Items.Count > 1 Then
            Me.cmbCompany.SelectedValue = gobjLocationInfo.CompanyID

        End If
        ' =========================================================================================
        ' =========================================================================================

        'CR#146
        'New filter for Shop 
        'CR#320
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

            cboShop.DataSource = DTConfigList
            cboShop.SelectedIndex = 0

            cboShop.DisplayMember = "config_value"
            cboShop.ValueMember = "config_value"

        Else

            cboShop.Items.Add(gstrComboZeroIndexString)
            cboShop.Items.Add("Accounts")
            cboShop.SelectedIndex = 0

        End If


    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
    End Sub

    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
    End Function

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

            Me.btnPrint.ImageList = gobjMyImageListForOperationBar
            Me.btnPrint.ImageKey = "Print"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting
        Try
            chkOtherVoucher.Visible = gblnShowOtherVoucher
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
        Try
            If Mode = EnumDataMode.[New] Then
                ''if New Mode then Set Disable all Navigation Buttons
                btnFirst.Enabled = False ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnPrevious.Enabled = False ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnNext.Enabled = False ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnLast.Enabled = False ': btnLast.VisualStyle = C1.Win.C1Input.VisualStyle.System

            ElseIf Mode = EnumDataMode.Edit Then
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

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
    End Function

#End Region

#Region "Report Interface Metholds .. "

       Public Function FunAddReportCriteria_Notes() As String

        Try
            Dim strSql As String
            Dim strPrevParamFinancialYear As String
            Dim dtPrevParamMaxDate As Date

            Dim dtPrevParamFromDate As Date

            Dim strLocationCriteria As String
            Dim strOtherVoucherCriteria As String
            Dim strCostCenterCriteria As String
            Dim strCtr As String = ""
            Dim strShop As String


            If Me.cmbFinancialYear.SelectedIndex <= 0 Then

                MsgBox("Select valid Financial year", vbInformation)
                cmbFinancialYear.Focus()
                Return False
                Exit Function
            End If

            'CR# 29 
            If chkOtherVoucher.Checked = False Then
                'strOtherVoucherCriteria = " AND (tblGlVoucher.other_voucher = 0)  "
                strOtherVoucherCriteria = "  (tblGlVoucher.other_voucher = 0)   "
            Else
                strOtherVoucherCriteria = ""

            End If

            If Me.cmbCompany.SelectedIndex > 0 Then

                strLocationCriteria = " (dbo.tblGlDefLocation.location_id = " & Me.cmbCompany.SelectedValue & ")  "
                'strLocationCriteria = " (dbo.tblGlDefLocation.location_id = 2) AND   "
            Else
                strLocationCriteria = ""
            End If

            'CR#29
            If Me.cmbCostCenter.SelectedIndex > 0 Then

                strCostCenterCriteria = " ((dbo.tblGlVoucherDetail.cost_center_id = " & cmbCostCenter.SelectedValue & ") "
            Else

                strCostCenterCriteria = ""
            End If


            'CR146
            'If Me.cboShop.SelectedIndex > 0 Then

            '    strShop = "  (dbo.tblGlVoucher.source= '" & cboShop.SelectedValue & "') "
            'Else

            '    strShop = ""
            'End If

            'CR#146
            If (Me.cboShop.SelectedIndex > 0 And Me.cmbCostCenter.SelectedIndex > 0) Then
                strShop = " OR   (dbo.tblGlVoucher.source= '" & cboShop.SelectedValue & "')  "
            ElseIf Me.cboShop.SelectedIndex > 0 Then

                strShop = " AND   (dbo.tblGlVoucher.source= '" & cboShop.SelectedValue & "')  "
            Else

                strShop = ""
            End If


            If Me.cmbCostCenter.SelectedIndex > 0 Then
                strShop += " )"
            End If


            If strOtherVoucherCriteria.Trim.Length > 0 Then
                strCtr = "AND" & strOtherVoucherCriteria
            End If

            'If strLocationCriteria.Trim.Length > 0 Then
            If strLocationCriteria.Trim.Length > 0 AndAlso strOtherVoucherCriteria.Trim.Length Then
                strCtr = strCtr & "AND" & strOtherVoucherCriteria
            End If

            If strCostCenterCriteria.Trim.Length > 0 Then
                strCtr = strCtr & "AND" & strCostCenterCriteria
            End If

            If strShop.Trim.Length > 0 Then
                strCtr = strCtr & strShop
            End If

            'CR# 29 
            'Query to Alter the view for Profit and Loss assessment
            'strSql = "Alter view vwGLPLNotesCurrent as SELECT dbo.vwGlCOADetail.PL_Note_Title,dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title, SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code , dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id " & _
            '         " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
            '         " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
            '         " WHERE (dbo.tblGlVoucher.post = 1)  " & strOtherVoucherCriteria & " AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(Me.dtToDate.Value.Date, "yyyyMMdd") & "' AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(dtFromDate.Value.Date, "yyyyMMdd") & "'" & _
            '         " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id,dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id,dbo.vwGlCOADetail.sub_sub_code, dbo.vwGl5COADetail.sub_sub_title " & _
            '         " HAVING " & strLocationCriteria & "  (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & Me.cmbFinancialYear.Text & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )"

            If strLocationCriteria = "" Then
                strSql = "Alter view vwGLPLNotesCurrent as SELECT dbo.vwGlCOADetail.PL_Note_Title,dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title, SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code , dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id " & _
                                     " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
                                     " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
                                     " WHERE (dbo.tblGlVoucher.post = 1)  " & strCtr & " AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(Me.dtToDate.Value.Date, "yyyyMMdd") & "' AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(dtFromDate.Value.Date, "yyyyMMdd") & "'" & _
                                     " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id,dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id,dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title " & _
                                     " HAVING (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & Me.cmbFinancialYear.Text & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )"
            Else
                strSql = "Alter view vwGLPLNotesCurrent as SELECT dbo.vwGlCOADetail.PL_Note_Title,dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title, SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code , dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id " & _
                         " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
                         " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
                         " WHERE (dbo.tblGlVoucher.post = 1)  " & strCtr & " AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(Me.dtToDate.Value.Date, "yyyyMMdd") & "' AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(dtFromDate.Value.Date, "yyyyMMdd") & "'" & _
                         " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id,dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id,dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title " & _
                         " HAVING " & strLocationCriteria & " and (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & Me.cmbFinancialYear.Text & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )"
            End If

            UtilityDAL.ExecuteQuery(strSql)

            'CR#105
            'strPrevParamFinancialYear = Val(Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)) - 1 & "-" & Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)
            Dim strInitYearCode As String = String.Empty
            Dim strLastYearCode As String = String.Empty

            strInitYearCode = Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)
            strLastYearCode = Microsoft.VisualBasic.Right(cmbFinancialYear.Text, 4)

            If strInitYearCode <> strLastYearCode Then
                strPrevParamFinancialYear = Val(Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)) - 1 & "-" & Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)
            Else
                strPrevParamFinancialYear = Val(Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)) - 1 & "-" & Val(Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)) - 1
            End If

            dtPrevParamMaxDate = Me.dtToDate.Value.Date.AddYears(-1) ' DateAdd("yyyy", -1, dtParamMaxDate)

            dtPrevParamFromDate = Me.dtFromDate.Value.Date.AddYears(-1) 'DateAdd("yyyy", -1, dtParamFromDate)

            'CR# 29 
            'Query to Alter the view for Profit and Loss assessment
            'strSql = " Alter View vwGLPLNotesPrev as SELECT dbo.vwGlCOADetail.PL_Note_Title, dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title, SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code , dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id " & _
            '         " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
            '         " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
            '         " WHERE (dbo.tblGlVoucher.post = 1)  " & strOtherVoucherCriteria & "  AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(dtPrevParamMaxDate, "yyyyMMdd") & "' AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(dtPrevParamFromDate, "yyyyMMdd") & "'" & _
            '         " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id,dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id, dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title " & _
            '         " HAVING " & strLocationCriteria & " (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & strPrevParamFinancialYear & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )"
            If strLocationCriteria = "" Then
                strSql = "Alter view vwGLPLNotesCurrent as SELECT dbo.vwGlCOADetail.PL_Note_Title,dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title, SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code , dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id " & _
                                    " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
                                    " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
                                    " WHERE (dbo.tblGlVoucher.post = 1)  " & strCtr & " AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(Me.dtToDate.Value.Date, "yyyyMMdd") & "' AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(dtFromDate.Value.Date, "yyyyMMdd") & "'" & _
                                    " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id,dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id,dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title " & _
                                    " HAVING (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & Me.cmbFinancialYear.Text & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )"
            Else

                strSql = " Alter View vwGLPLNotesPrev as SELECT dbo.vwGlCOADetail.PL_Note_Title, dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title, SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code , dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id " & _
                         " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
                         " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
                         " WHERE (dbo.tblGlVoucher.post = 1)  " & strCtr & "  AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(dtPrevParamMaxDate, "yyyyMMdd") & "' AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(dtPrevParamFromDate, "yyyyMMdd") & "'" & _
                         " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id,dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id, dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title " & _
                         " HAVING " & strLocationCriteria & " and (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & strPrevParamFinancialYear & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )"

            End If

            UtilityDAL.ExecuteQuery(strSql)

            Dim ObjDAL As New DAL.RptProfitAndLossDal
            If ObjDAL.InsertDataForNotes() Then
            Else
            End If

            Return ""

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function FunAddReportCriteria_Notes_Detail() As String   'CR#40

        Try
            Dim strSql As String
            Dim strPrevParamFinancialYear As String
            Dim dtPrevParamMaxDate As Date

            Dim dtPrevParamFromDate As Date

            Dim strLocationCriteria As String
            Dim strOtherVoucherCriteria As String
            Dim strCostCenterCriteria As String
            Dim strCtr As String = ""
            Dim strShop As String


            If Me.cmbFinancialYear.SelectedIndex <= 0 Then

                MsgBox("Select valid Financial year", vbInformation)
                cmbFinancialYear.Focus()
                Return False
                Exit Function
            End If

            'CR# 29 
            If chkOtherVoucher.Checked = False Then
                'strOtherVoucherCriteria = " AND (tblGlVoucher.other_voucher = 0)  "
                strOtherVoucherCriteria = "  (tblGlVoucher.other_voucher = 0)   "
            Else
                strOtherVoucherCriteria = ""

            End If

            'CR146

            'If Me.cboShop.SelectedIndex > 0 Then

            '    strShop = "  (dbo.tblGlVoucher.source= '" & cboShop.SelectedValue & "') "
            'Else

            '    strShop = ""
            'End If

            If Me.cmbCompany.SelectedIndex > 0 Then

                strLocationCriteria = " (dbo.tblGlDefLocation.location_id = " & Me.cmbCompany.SelectedValue & ")  "
                'strLocationCriteria = " (dbo.tblGlDefLocation.location_id = 2) AND   "
            Else
                strLocationCriteria = ""
            End If

            'CR#29
            If Me.cmbCostCenter.SelectedIndex > 0 Then

                strCostCenterCriteria = " ((dbo.tblGlVoucherDetail.cost_center_id = " & cmbCostCenter.SelectedValue & ") "
            Else

                strCostCenterCriteria = ""
            End If

            'CR#146
            If (Me.cboShop.SelectedIndex > 0 And Me.cmbCostCenter.SelectedIndex > 0) Then
                strShop = " OR  (dbo.tblGlVoucher.source= '" & cboShop.SelectedValue & "') "
            ElseIf Me.cboShop.SelectedIndex > 0 Then

                strShop = " AND  (dbo.tblGlVoucher.source= '" & cboShop.SelectedValue & "')  "
            Else

                strShop = ""
            End If

            If Me.cmbCostCenter.SelectedIndex > 0 Then
                strShop += " )"
            End If

            If strOtherVoucherCriteria.Trim.Length > 0 Then
                strCtr = "AND" & strOtherVoucherCriteria
            End If

            'If strLocationCriteria.Trim.Length > 0 Then
            If strLocationCriteria.Trim.Length > 0 AndAlso strOtherVoucherCriteria.Trim.Length > 0 Then
                strCtr = strCtr & "AND" & strOtherVoucherCriteria
            End If

            If strCostCenterCriteria.Trim.Length > 0 Then
                strCtr = strCtr & "AND" & strCostCenterCriteria
            End If

            If strShop.Trim.Length > 0 Then
                strCtr = strCtr & strShop
            End If

            'CR#113: Change Date Criteria on all companies selection
            If strLocationCriteria = "" Then
                strSql = "Alter view vwGLPLNotesCurrentDtl as SELECT dbo.vwGlCOADetail.PL_Note_Title,dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title,dbo.vwGlCOADetail.Detail_Code,dbo.vwGlCOADetail.detail_Title, SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code , dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id " & _
                                     " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
                                     " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
                                     " WHERE (dbo.tblGlVoucher.post = 1)  " & strCtr & " AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(Me.dtToDate.Value.Date, "yyyyMMdd") & "' AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(dtFromDate.Value.Date, "yyyyMMdd") & "'" & _
                                     " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id,dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id,dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title,dbo.vwGlCOADetail.Detail_Code,dbo.vwGlCOADetail.detail_Title " & _
                                     " HAVING (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & Me.cmbFinancialYear.Text & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )"
            Else
                strSql = "Alter view vwGLPLNotesCurrentDtl as SELECT dbo.vwGlCOADetail.PL_Note_Title,dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title, dbo.vwGlCOADetail.Detail_Code,dbo.vwGlCOADetail.detail_Title,SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code , dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id " & _
                         " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
                         " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
                         " WHERE (dbo.tblGlVoucher.post = 1)  " & strCtr & " AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(Me.dtToDate.Value.Date, "yyyyMMdd") & "' AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(dtFromDate.Value.Date, "yyyyMMdd") & "'" & _
                         " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id,dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id,dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title,dbo.vwGlCOADetail.Detail_Code,dbo.vwGlCOADetail.detail_Title " & _
                         " HAVING " & strLocationCriteria & " and (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & Me.cmbFinancialYear.Text & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )"
            End If

            UtilityDAL.ExecuteQuery(strSql)

            'CR#105
            'strPrevParamFinancialYear = Val(Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)) - 1 & "-" & Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)
            Dim strInitYearCode As String = String.Empty
            Dim strLastYearCode As String = String.Empty

            strInitYearCode = Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)
            strLastYearCode = Microsoft.VisualBasic.Right(cmbFinancialYear.Text, 4)

            If strInitYearCode <> strLastYearCode Then
                strPrevParamFinancialYear = Val(Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)) - 1 & "-" & Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)
            Else
                strPrevParamFinancialYear = Val(Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)) - 1 & "-" & Val(Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)) - 1
            End If


            dtPrevParamMaxDate = Me.dtToDate.Value.Date.AddYears(-1) ' DateAdd("yyyy", -1, dtParamMaxDate)

            dtPrevParamFromDate = Me.dtFromDate.Value.Date.AddYears(-1) 'DateAdd("yyyy", -1, dtParamFromDate)

            If strLocationCriteria = "" Then
                strSql = "Alter view vwGLPLNotesPrevDtl as SELECT dbo.vwGlCOADetail.PL_Note_Title,dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title, dbo.vwGlCOADetail.Detail_Code,dbo.vwGlCOADetail.detail_Title,SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code , dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id " & _
                                    " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
                                    " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
                                    " WHERE (dbo.tblGlVoucher.post = 1)  " & strCtr & " AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(dtPrevParamMaxDate, "yyyyMMdd") & "' AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(dtPrevParamFromDate, "yyyyMMdd") & "'" & _
                                    " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id,dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id,dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title,dbo.vwGlCOADetail.Detail_Code,dbo.vwGlCOADetail.detail_Title " & _
                                    " HAVING (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & strPrevParamFinancialYear & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )"
            Else

                strSql = " Alter View vwGLPLNotesPrevDtl as SELECT dbo.vwGlCOADetail.PL_Note_Title, dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title,dbo.vwGlCOADetail.Detail_Code,dbo.vwGlCOADetail.detail_Title, SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code , dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id " & _
                         " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
                         " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
                         " WHERE (dbo.tblGlVoucher.post = 1)  " & strCtr & "  AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(dtPrevParamMaxDate, "yyyyMMdd") & "' AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(dtPrevParamFromDate, "yyyyMMdd") & "'" & _
                         " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id,dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id, dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title,dbo.vwGlCOADetail.Detail_Code,dbo.vwGlCOADetail.detail_Title " & _
                         " HAVING " & strLocationCriteria & " and (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & strPrevParamFinancialYear & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )"

            End If

            UtilityDAL.ExecuteQuery(strSql)

            Dim ObjDAL As New DAL.RptProfitAndLossDal
            If ObjDAL.InsertDataForNotesDetail() Then
            Else
            End If

            Return ""

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function FunAddReportCriteria() As String Implements IReportsInterface.FunAddReportCriteria

        Dim strSql As String
        Dim strCondAccount As String
        Dim strYearCriteria As String
        Dim strLocationCriteria As String

        Dim strOtherVoucherCriteria As String
        Dim strPrevParamFinancialYear As String
        Dim dtPrevParamMaxDate As Date
        Dim dtPrevParamFromDate As Date
        Dim strCostCenterCriteria As String
        Dim strShop As String

        Dim strPostCriteria As String
        Dim strOther_Voucher_Criteria As String
        Dim intlocation_id As Integer

        If Me.cmbCompany.SelectedIndex > 0 Then

            strLocationCriteria = " AND(dbo.tblGlDefLocation.location_id = " & Me.cmbCompany.SelectedValue & ") "
        Else
            strLocationCriteria = " "
        End If

        'CR#29
        If Me.cmbCostCenter.SelectedIndex > 0 Then

            strCostCenterCriteria = " AND ((dbo.tblGlVoucherDetail.cost_center_id = " & cmbCostCenter.SelectedValue & ") "
        Else

            strCostCenterCriteria = ""
        End If

        'CR146
        If (Me.cboShop.SelectedIndex > 0 And Me.cmbCostCenter.SelectedIndex > 0) Then
            strShop = " OR (dbo.tblGlVoucher.source= '" & cboShop.SelectedValue & "') "
        ElseIf Me.cboShop.SelectedIndex > 0 Then

            strShop = " AND (dbo.tblGlVoucher.source= '" & cboShop.SelectedValue & "') "
        Else

            strShop = ""
        End If



        If Me.cmbCostCenter.SelectedIndex > 0 Then
            strShop += " )"
        End If



        '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
        '' posted vouchers so we add the check
        If Me.chkOtherVoucher.Checked = False Then
            strOtherVoucherCriteria = " AND (tblGlVoucher.other_voucher = 0) "
        Else

            strOther_Voucher_Criteria = ""
        End If

        '   get the location id
        If Me.cmbCompany.SelectedIndex > 0 Then

            intlocation_id = Me.cmbCompany.SelectedValue
        Else

            intlocation_id = 0
        End If


        'CR#146
        'strSql = " ALTER VIEW vwGLProfitLoss as " & _
        '       " SELECT dbo.vwGlCOADetail.PL_Note_Title, SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code  " & _
        '       " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
        '       " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
        '       " WHERE (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(Me.dtFromDate.Value.Date, "yyyyMMdd") & "'  AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(Me.dtToDate.Value.Date, "yyyyMMdd") & "' " & strOtherVoucherCriteria & " " & strLocationCriteria & " " & strCostCenterCriteria & "  " & _
        '       " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id " & _
        '       " HAVING (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & Me.cmbFinancialYear.Text & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )"

        strSql = " ALTER VIEW vwGLProfitLoss as " & _
               " SELECT dbo.vwGlCOADetail.PL_Note_Title, SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code  " & _
               " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
               " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
               " WHERE (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(Me.dtFromDate.Value.Date, "yyyyMMdd") & "'  AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(Me.dtToDate.Value.Date, "yyyyMMdd") & "' " & strOtherVoucherCriteria & " " & strLocationCriteria & " " & strCostCenterCriteria & " " & strShop & " " & _
               " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id " & _
               " HAVING (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & Me.cmbFinancialYear.Text & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )"


        UtilityDAL.ExecuteQuery(strSql)

        dtPrevParamMaxDate = Me.dtToDate.Value.Date.AddYears(-1)
        dtPrevParamFromDate = Me.dtFromDate.Value.Date.AddYears(-1)


        'CR#105
        'strPrevParamFinancialYear = Val(Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)) - 1 & "-" & Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)
        Dim strInitYearCode As String = String.Empty
        Dim strLastYearCode As String = String.Empty

        strInitYearCode = Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)
        strLastYearCode = Microsoft.VisualBasic.Right(cmbFinancialYear.Text, 4)

        If strInitYearCode <> strLastYearCode Then
            strPrevParamFinancialYear = Val(Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)) - 1 & "-" & Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)
        Else
            strPrevParamFinancialYear = Val(Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)) - 1 & "-" & Val(Microsoft.VisualBasic.Left(cmbFinancialYear.Text, 4)) - 1
        End If

        'strSql = " ALTER VIEW vwGLProfitLossPrevious as " & _
        '          " SELECT dbo.vwGlCOADetail.PL_Note_Title, SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code  " & _
        '          " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
        '          " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
        '          " WHERE (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(dtPrevParamFromDate, "yyyyMMdd") & "'  AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(dtPrevParamMaxDate, "yyyyMMdd") & "' " & strOtherVoucherCriteria & " " & strLocationCriteria & " " & strCostCenterCriteria & " " & _
        '          " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id  " & _
        '          " HAVING (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & strPrevParamFinancialYear & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )"

        strSql = " ALTER VIEW vwGLProfitLossPrevious as " & _
                 " SELECT dbo.vwGlCOADetail.PL_Note_Title, SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code  " & _
                 " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
                 " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
                 " WHERE (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(dtPrevParamFromDate, "yyyyMMdd") & "'  AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(dtPrevParamMaxDate, "yyyyMMdd") & "' " & strOtherVoucherCriteria & " " & strLocationCriteria & " " & strCostCenterCriteria & " " & strShop & " " & _
                 " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id  " & _
                 " HAVING (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & strPrevParamFinancialYear & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )"

        UtilityDAL.ExecuteQuery(strSql)

        Dim ObjDAL As New DAL.RptProfitAndLossDal

        ObjDAL.proSaveDummyTable(Me.cmbFinancialYear.Text)

        If ObjDAL.InsertDataForReport() Then
        Else
        End If

        Return ""

    End Function

    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters

        Try

            Dim objHashTableParamter As New Hashtable

            ' Giving Report Name .. 
            If optProfitLoss.Checked = True Then

                ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
                FunAddReportCriteria()

                '   connectivity with Profit and Loss Report
                objHashTableParamter.Add("ReportPath", "\rptProftAndLossStatement_Formated.rpt")
                objHashTableParamter.Add("CostCenter", IIf(cmbCostCenter.SelectedIndex = 0, "ALL", cmbCostCenter.Text))

            ElseIf optProfitLossNotes.Checked = True Then

                'CR#40
                ' '' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
                ''FunAddReportCriteria_Notes()

                ' ''   connectivity with Profit and Loss Notes Report
                ''objHashTableParamter.Add("ReportPath", "\rptGLPLNotes.rpt")

                If Me.chkShowNotesDetail.Checked = True Then
                    ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
                    FunAddReportCriteria_Notes_Detail()

                    '   connectivity with Profit and Loss Notes Report
                    objHashTableParamter.Add("ReportPath", "\rptGLPLNotesDetail.rpt")

                Else
                    ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
                    FunAddReportCriteria_Notes()

                    '   connectivity with Profit and Loss Notes Report
                    objHashTableParamter.Add("ReportPath", "\rptGLPLNotes.rpt")
                End If
                
                objHashTableParamter.Add("Current_Year", " " & cmbFinancialYear.Text & " ")
                objHashTableParamter.Add("CostCenter", IIf(cmbCostCenter.SelectedIndex = 0, "ALL", cmbCostCenter.Text))

            End If



            Dim ObjCompanyData As DataTable

            ObjCompanyData = UtilityDAL.setCompanyInfo(cmbCompany.SelectedValue)



            ' Passing Parameters .. (Report Parameters .. )
            If Me.cmbCompany.SelectedIndex > 0 Then
                objHashTableParamter.Add("companyname", IIf(ObjCompanyData.Rows(0).Item("CompanyName") = "---Select---", "All", ObjCompanyData.Rows(0).Item("CompanyName")))
                objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))
            Else
                objHashTableParamter.Add("companyname", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CompanyName").ToString)
                objHashTableParamter.Add("address", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CompanyAddress").ToString)
            End If
            'objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
            'objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))

            objHashTableParamter.Add("fromdate", Format(Me.dtFromDate.Value.Date, "dd/MMM/yyyy"))
            objHashTableParamter.Add("todate", Format(Me.dtToDate.Value.Date, "dd/MMM/yyyy"))


            ' Adding Location Parameter .. 
            If cmbCompany.SelectedIndex > 0 Then
                objHashTableParamter.Add("Location", cmbCompany.Text)
            Else
                objHashTableParamter.Add("Location", "ALL")

            End If
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
            ' =======================================================
            ' =======================================   ================


            gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)

        Catch ex As Exception
            Throw ex

        End Try

    End Sub

#End Region


    Private Sub btnGenerateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Try

            If dtFromDate.Value.Date > dtToDate.Value.Date Then
                ShowValidationMessage("FromDate should be less than ToDate")
                dtFromDate.Focus()
                Exit Sub
            End If


            'check that either financial year is selected or not
            If Me.cmbFinancialYear.SelectedIndex <= 0 Then

                ShowValidationMessage("Select valid Financial year")
                Me.cmbFinancialYear.Focus()
                Exit Sub

            End If

            ' Implemented Interface Method .. 
            ' Used To Add Report Parameters .. (Also Report Name Is Given In This Function .. )
            Call FunAddReportPramaters()


            ' Create A Object Of Report Viewer .. And Calls His Show Method, To Show The Report .. 
            ' ------------------------------------------------------------------------------------
            Dim rptViewer As New rptViewer
            rptViewer.Text = Me.Text
            rptViewer.Show()
            ' ------------------------------------------------------------------------------------

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub cmbFinancialYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFinancialYear.SelectedIndexChanged


        If cmbFinancialYear.SelectedIndex > 0 Then
            Dim dtRow As DataRowView = CType(cmbFinancialYear.SelectedItem, DataRowView)

            dtFromDate.MinDate = dtRow("Start Date")
            dtToDate.MaxDate = dtRow("End Date")

            dtFromDate.Value = dtRow("Start Date")
            dtToDate.Value = dtRow("End Date")

        Else

            dtFromDate.MinDate = CDate("01/07/1980")
            dtToDate.MaxDate = CDate("01/01/2099")

            Me.dtFromDate.Value = gobjBusinessStartDate
            Me.dtToDate.Value = Now

        End If

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class