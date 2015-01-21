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
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
'    Nov 26,2010       Abdul Jabbar        CR#105,Balance Sheet  Issue, Balance Sheet doesn't show the previous year data. 
''// Jul 17,2014       Abdul Jabbar        CR#320.Include source filter in Trial Balance, Balance Sheet and P&L Report
''/////////////////////////////////////////////////////////////////////////////////////////


Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic

Public Class frmRptBalanceSheet
    Implements IGeneral, IReportsInterface

    Dim dblCashBankOpening As Double

    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection

    Private Sub frmRptBalanceSheet_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

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

        dtFromDate.Value = Date.Now.Date
       
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

        ''Dim ObjGLCostCenterDal As New GLCostCenterDal
        ''ObjDataTable = ObjGLCostCenterDal.GetAll()

        ''ObjDataRow = ObjDataTable.NewRow
        ''ObjDataRow.Item(1) = gstrComboZeroIndexString
        ''ObjDataRow.Item(0) = 0
        ''ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


        ''cmbCostCenter.DisplayMember = ObjDataTable.Columns(1).ColumnName
        ''cmbCostCenter.DataSource = ObjDataTable.Copy

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

        'CR#320, Populating source (replicated shops) drop down
        cmbSource.Items.Clear()
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

            cmbSource.DataSource = DTConfigList
            cmbSource.SelectedIndex = 0

            cmbSource.DisplayMember = "config_value"
            cmbSource.ValueMember = "config_value"

        Else

            cmbSource.Items.Add(gstrComboZeroIndexString)
            cmbSource.Items.Add("Accounts")
            cmbSource.SelectedIndex = 0

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
            Dim strLocationCriteria As String
            Dim strOtherVoucherCriteria As String

            If cmbFinancialYear.SelectedIndex <= 0 Then

                MsgBox("Select valid Financial year", vbInformation)
                cmbFinancialYear.Focus()
                Return False
                Exit Function

            ElseIf cmbCompany.SelectedIndex > 0 Then

                strLocationCriteria = " AND (dbo.tblGlVoucher.location_id = " & cmbCompany.SelectedValue & ")  "
                'strLocationCriteria = " (dbo.tblGlDefLocation.location_id = 2) AND   "
            Else
                strLocationCriteria = " "
            End If


            If chkOtherVoucher.Checked = False Then
                strOtherVoucherCriteria = " AND (tblGlVoucher.other_voucher = 0)  "
            Else
                strOtherVoucherCriteria = " "

            End If

            strSql = " Alter View vwGLBSNotesCurrent as SELECT     dbo.vwGlCOADetail.Sub_Sub_Code, dbo.vwGlCOADetail.sub_sub_title, note_title = CASE WHEN DrBS_Note_Title IS NULL " & _
                     "          THEN CrBS_Note_Title ELSE DrBS_Note_Title END, note_id = CASE WHEN dbo.vwGlCOADetail.DrBS_Note_ID IS NULL" & _
                     "          THEN dbo.vwGlCOADetail.CrBS_Note_ID ELSE dbo.vwGlCOADetail.DrBS_Note_ID END, SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount," & _
                     "          SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount, SUM(dbo.tblGlVoucherDetail.debit_amount) - SUM(dbo.tblGlVoucherDetail.credit_amount)" & _
                     "          AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code" & _
                     " FROM         dbo.tblGlVoucherDetail INNER JOIN" & _
                     "          dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN" & _
                     "          dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND" & _
                     "          dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
                     "          dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id" & _
                     " WHERE     (dbo.tblGlVoucher.post = 1) " & strLocationCriteria & "  AND (dbo.tblGlDefFinancialYear.year_code = '" & cmbFinancialYear.Text & "') AND" & _
                     "          (dbo.vwGlCOADetail.DrBS_note_id > 0 OR" & _
                     "          dbo.vwGlCOADetail.CrBS_note_id > 0) AND (dbo.vwGlCOADetail.note_type = 'BS')  " & strOtherVoucherCriteria & " AND CONVERT(varchar(12), voucher_date, 112) <= '" & Format(dtFromDate.Value.Date, "yyyyMMdd") & "' " & _
                     " GROUP BY dbo.vwGlCOADetail.Sub_Sub_Code, dbo.vwGlCOADetail.sub_sub_title, dbo.vwGlCOADetail.DrBS_Note_Title, dbo.vwGlCOADetail.CrBS_Note_Title," & _
                     " dbo.tblGlDefFinancialYear.year_code , dbo.vwGlCOADetail.DrBS_note_id, dbo.vwGlCOADetail.note_type, dbo.vwGlCOADetail.CrBS_note_id"


            UtilityDAL.ExecuteQuery(strSql)


            '   Get Previous Year Param values
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

            dtPrevParamMaxDate = Me.dtFromDate.Value.Date.AddYears(-1)
            
            strSql = " Alter View vwGLBSNotesPrev as SELECT     dbo.vwGlCOADetail.Sub_Sub_Code, dbo.vwGlCOADetail.sub_sub_title, note_title = CASE WHEN DrBS_Note_Title IS NULL " & _
                     "          THEN CrBS_Note_Title ELSE DrBS_Note_Title END, note_id = CASE WHEN dbo.vwGlCOADetail.DrBS_Note_ID IS NULL" & _
                     "          THEN dbo.vwGlCOADetail.CrBS_Note_ID ELSE dbo.vwGlCOADetail.DrBS_Note_ID END, SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount," & _
                     "          SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount, SUM(dbo.tblGlVoucherDetail.debit_amount) - SUM(dbo.tblGlVoucherDetail.credit_amount)" & _
                     "          AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code" & _
                     " FROM         dbo.tblGlVoucherDetail INNER JOIN" & _
                     "          dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN" & _
                     "          dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND" & _
                     "          dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
                     "          dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id" & _
                     " WHERE     (dbo.tblGlVoucher.post = 1) " & strLocationCriteria & "  AND (dbo.tblGlDefFinancialYear.year_code = '" & strPrevParamFinancialYear & "') AND" & _
                     "          (dbo.vwGlCOADetail.DrBS_note_id > 0 OR" & _
                     "          dbo.vwGlCOADetail.CrBS_note_id > 0) AND (dbo.vwGlCOADetail.note_type = 'BS')  " & strOtherVoucherCriteria & " AND CONVERT(varchar(12), voucher_date, 112) <= '" & Format(dtPrevParamMaxDate, "yyyyMMdd") & "' " & _
                     " GROUP BY dbo.vwGlCOADetail.Sub_Sub_Code, dbo.vwGlCOADetail.sub_sub_title, dbo.vwGlCOADetail.DrBS_Note_Title, dbo.vwGlCOADetail.CrBS_Note_Title," & _
                     " dbo.tblGlDefFinancialYear.year_code , dbo.vwGlCOADetail.DrBS_note_id, dbo.vwGlCOADetail.note_type, dbo.vwGlCOADetail.CrBS_note_id"


            UtilityDAL.ExecuteQuery(strSql)

            Dim ObjDAL As New DAL.RptBalanceSheetDal
            ObjDAL.proSaveDummyTable(Me.cmbFinancialYear.Text)
            If ObjDAL.InsertDataForNotes() Then
            Else
            End If

            Return ""

        Catch ex As Exception
            Throw ex
        End Try

    End Function



    Public Function FunAddReportCriteria() As String Implements IReportsInterface.FunAddReportCriteria

        Try
            Dim strSql As String
            Dim strCond As String
            Dim strLocationCriteria As String

            Dim strPrevParamFinancialYear As String
            Dim dtPrevParamMaxDate As Date
            Dim strOtherVoucherCriteria As String


            If cmbFinancialYear.SelectedIndex <= 0 Then

                MsgBox("Select valid Financial year", vbInformation)
                cmbFinancialYear.Focus()
                Return False
                Exit Function
            End If

            If chkOtherVoucher.Checked = False Then
                strOtherVoucherCriteria = " AND (tblGlVoucher.other_voucher = 0)  "
            Else
                strOtherVoucherCriteria = " "

            End If

            If cmbCompany.SelectedIndex > 0 Then

                strLocationCriteria = " AND (dbo.tblGlVoucher.location_id = " & cmbCompany.SelectedValue & ")  "
            Else

                strLocationCriteria = " "
            End If

            'CR#320 Source Filter criteria 
            Dim strSource As String = String.Empty
            If Me.cmbSource.SelectedIndex > 0 Then
                strSource = strSource + " and tblGlVoucher.source = '" & Me.cmbSource.Text & "'"
            Else
                strSource = ""
            End If

            'sql query prepartion
            strSql = "alter view  vwGLBalanceSheet as " & _
                      " SELECT dbo.vwGlCOADetail.DrBS_Note_Title, dbo.vwGlCOADetail.CrBS_Note_Title, note_title = CASE  WHEN  DrBS_Note_Title IS NULL THEN CrBS_Note_Title ELSE DrBS_Note_Title END , dbo.vwGlCOADetail.DrBS_Note_ID, dbo.vwGlCOADetail.CrBS_Note_id , note_id = CASE WHEN dbo.vwGlCOADetail.DrBS_Note_ID IS NULL THEN  dbo.vwGlCOADetail.CrBS_Note_ID ELSE dbo.vwGlCOADetail.DrBS_Note_ID END  , SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount, SUM(dbo.tblGlVoucherDetail.debit_amount) - SUM(dbo.tblGlVoucherDetail.credit_amount) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code" & _
                      " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id" & _
                      " WHERE   (dbo.tblGlVoucher.post = 1) " & strLocationCriteria & "  AND    (dbo.tblGlDefFinancialYear.year_code = '" & cmbFinancialYear.Text & " ') AND (dbo.vwGlCOADetail.DrBS_note_id > 0 OR dbo.vwGlCOADetail.CrBS_note_id > 0) AND (dbo.vwGlCOADetail.note_type = 'BS') " & strOtherVoucherCriteria & " "

            'CR#320 Adding Source filter criteria in where clause
            strSql = strSql & strSource
            'date conditions
            strCond = " AND Convert(varchar(12),voucher_date,112) <= '" & Format(dtFromDate.Value.Date, "yyyyMMdd") & "'  " & _
                      " GROUP BY dbo.vwGlCOADetail.DrBS_Note_Title, dbo.vwGlCOADetail.CrBS_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.DrBS_note_id , dbo.vwGlCOADetail.note_type , dbo.vwGlCOADetail.CrBS_note_id"

            '" ORDER BY dbo.vwGlCOADetail.DrBS_Note_Title"

            UtilityDAL.ExecuteQuery(strSql & strCond)

            '   Get Previous Year Param values
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


            dtPrevParamMaxDate = Me.dtFromDate.Value.Date.AddYears(-1)

            '   Previous Year Sql Query preparation
            strSql = "alter view  vwGLBalanceSheetPrevious as " & _
                      " SELECT dbo.vwGlCOADetail.DrBS_Note_Title, dbo.vwGlCOADetail.CrBS_Note_Title, note_title = CASE  WHEN  DrBS_Note_Title IS NULL THEN CrBS_Note_Title ELSE DrBS_Note_Title END , dbo.vwGlCOADetail.DrBS_Note_ID, dbo.vwGlCOADetail.CrBS_Note_id , note_id = CASE WHEN dbo.vwGlCOADetail.DrBS_Note_ID IS NULL THEN  dbo.vwGlCOADetail.CrBS_Note_ID ELSE dbo.vwGlCOADetail.DrBS_Note_ID END  , SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) AS credit_amount, SUM(dbo.tblGlVoucherDetail.debit_amount) - SUM(dbo.tblGlVoucherDetail.credit_amount) AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code" & _
                      " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id" & _
                      " WHERE     (dbo.tblGlVoucher.post = 1)  " & strLocationCriteria & "  AND   (dbo.tblGlDefFinancialYear.year_code = '" & strPrevParamFinancialYear & " ') AND (dbo.vwGlCOADetail.DrBS_note_id > 0 OR dbo.vwGlCOADetail.CrBS_note_id > 0) AND (dbo.vwGlCOADetail.note_type = 'BS')   " & strOtherVoucherCriteria & "  "

            'CR#320 Adding Source filter criteria in where clause
            strSql = strSql & strSource

            'date conditions
            strCond = " AND Convert(varchar(12),voucher_date,112) <= '" & Format(dtPrevParamMaxDate, "yyyyMMdd") & "'  " & _
                      " GROUP BY dbo.vwGlCOADetail.DrBS_Note_Title, dbo.vwGlCOADetail.CrBS_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.DrBS_note_id , dbo.vwGlCOADetail.note_type , dbo.vwGlCOADetail.CrBS_note_id"

            '" ORDER BY dbo.vwGlCOADetail.DrBS_Note_Title"

            UtilityDAL.ExecuteQuery(strSql & strCond)

            Dim ObjDAL As New DAL.RptBalanceSheetDal
            ObjDAL.proSaveDummyTable(Me.cmbFinancialYear.Text)
            If ObjDAL.InsertDataForReport() Then
            Else
            End If

            Return ""




        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters

        Try

            Dim objHashTableParamter As New Hashtable

            ' Giving Report Name .. 
            If optProfitLoss.Checked = True Then

                ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
                FunAddReportCriteria()

                '   connectivity with Profit and Loss Report
                objHashTableParamter.Add("ReportPath", "\rptBalanceSheet_Formatted.rpt")
              
            ElseIf optProfitLossNotes.Checked = True Then

                ' Calling A Function Which Will Created View And Insert Data In Attached Table Of Grid ..
                FunAddReportCriteria_Notes()

                '   connectivity with Profit and Loss Notes Report
                objHashTableParamter.Add("ReportPath", "\rptGLBsNotes.rpt")
                ' objHashTableParamter.Add("Current_Year", " " & cmbFinancialYear.Text & " ")

            End If



            Dim ObjCompanyData As DataTable

            ObjCompanyData = UtilityDAL.setCompanyInfo(gobjLocationInfo.CompanyID)



            ' Passing Parameters .. (Report Parameters .. )
            objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
            objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))

            objHashTableParamter.Add("ReportDate", Format(Me.dtFromDate.Value.Date, "dd-MMM-yyyy"))
            
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
            ' =======================================================


            gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)

        Catch ex As Exception
            Throw ex

        End Try

    End Sub

#End Region


    Private Sub btnGenerateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Try


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
            rptViewer.Text = IIf(Me.optProfitLoss.Checked = True, optProfitLoss.Text, optProfitLossNotes.Text)
            rptViewer.Show()
            ' ------------------------------------------------------------------------------------

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub cmbFinancialYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFinancialYear.SelectedIndexChanged

        dtFromDate.MinDate = CDate("01/07/1980")
        dtFromDate.MaxDate = CDate("01/01/3000")
        Dim dtRow As DataRowView = CType(cmbFinancialYear.SelectedItem, DataRowView)

        If cmbFinancialYear.SelectedIndex > 1 Then

            dtFromDate.MinDate = dtRow("Start Date")
            dtFromDate.MaxDate = dtRow("End Date")

            dtFromDate.Value = dtRow("End Date")

        ElseIf cmbFinancialYear.SelectedIndex = 1 Then

            dtFromDate.MaxDate = dtRow("End Date")

            dtFromDate.Value = dtRow("End Date")

            'set the value of tdate
            If dtFromDate.MaxDate < Date.Now.Date Or dtFromDate.MinDate > Date.Now.Date Then

                dtFromDate.Value = dtRow("End Date")
            Else

                dtFromDate.Value = Date.Now.Date

            End If
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

   
End Class