Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility
'Modication 
'CR#160 (Service Broker relavant Changes) by Abdul Jabbar ,shop id check included plus when SSB On identity of voucher table will be off
'CR#164                                      Fatima Tajammal Lightwave Logviewer form is not working properly, need to find out and fix issues
'CR#217                                   by Abdul Jabbar  (Temp. Voucher: voucher detail doesn't shows on loading temp. voucher)
'Cr#245  27-may-2013                      by Fatima Tajammal  Make a new report for Voucher History
''// 268     20 aug,2013         Rana Saeed      Invoice and PO print from GL
'CR#300      09 Jun,2014         by Abdul Jabbar  (Voucher: Company Wise & shop wise voucher entry option should be available.)
''''// 323     23 Jul,2014          farooq-H   Cheque Printing: add cheque printing on voucher screen for bank payment voucher
''''// 337     12 Nov 2014          M. Shoaib    When Auto sorting is ON or flag key is missing then BPV cheque printing doesn't shows amount in print and popup.
''''// 348     19 Dec 2014          M.Shoaib     Cash Payment Voucher: Error in Cash payment voucher on reload

Public Class VoucherDAL
    Public gstrComboZeroIndexString As String = "---Select---"
    '#To Do
    Public MaxLocId As Integer
    Public G_intTrialVerTrans As Integer = 200
    Private Enum EnumMode

        NewMode = 0
        UpdateMode = 1

    End Enum
#Region "Local Functions and Procedures"


#End Region

#Region "Public Functions and Procedures"

    Public Function Add(ByVal ObjVoucher As GLVoucher, Optional ByVal TempVoucher As Boolean = False) As Boolean


        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Dim VoucherID As Integer
        Dim VNo As String
        Dim LocCode As String
        Dim strVoucherType As String
        Dim Voucher_Detail_ID As Integer

        Try

            'CR#83

            VNo = GetNewVoucherNo(ObjVoucher.LocationID, ObjVoucher.VoucherTypeID, Format(ObjVoucher.VoucherDate, "MMM"), Format(ObjVoucher.VoucherDate, "yyyy"), ObjVoucher.OtherVoucher, TempVoucher)
            'replace the voucher no from '1' to '000001'
            VNo = funDoPadding(VNo, ObjVoucher.VNoMaxLength)

            LocCode = GetLocationCode(ObjVoucher.LocationID)
            strVoucherType = GetGLVType(ObjVoucher.VoucherTypeID)
            'Making Voucher Code
            ObjVoucher.VoucherCode = Format(ObjVoucher.VoucherDate, "yyyy") & "-" & strVoucherType & "-" & LocCode & "-" & "ACC" & "-" & Format(ObjVoucher.VoucherDate, "MMM") & "-" & VNo
            ObjVoucher.VoucherNo = VNo

            Dim strSQL As String
            'strSQL = "Select config_value from tblRCMSConfiguration where config_Name='DataLogEntery'"
            'Dim dr As DataRow = New DALReports().ReturnDataRow(strSQL)
            'Dim isLogTrue As Boolean

            'If Not dr Is Nothing Then
            '    isLogTrue = dr.Item(0)
            'End If

            If TempVoucher = False Then
                'Normal Voucher INSERTION



                strSQL = String.Empty
                'CR#160 (Service Broker relavant Changes)
                If G_IsSSBOn = True Then

                    'strSQL = "Select isnull(Max(voucher_id),0) + 1 as MaxID from tblGLVoucher where shop_id <= 0"
                    'ObjVoucher.voucherID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

                    'strSQL = "INSERT INTO tblGlVoucher (voucher_id,location_id, voucher_code, finiancial_year_id, voucher_type_id, voucher_month, voucher_no, "
                    'strSQL = strSQL & " voucher_date, paid_to, coa_detail_id, cheque_no, cheque_date, due_date, post, other_voucher,Source)"
                    'strSQL = strSQL & " VALUES (" & ObjVoucher.voucherID & ", " & ObjVoucher.LocationID & ", '" & ObjVoucher.VoucherCode & "', " & ObjVoucher.FiniancialYearID & ", " & ObjVoucher.VoucherTypeID & ", '" & ObjVoucher.VoucherMonth & "', '" & ObjVoucher.VoucherNo & "', "
                    'strSQL = strSQL & " '" & Format(ObjVoucher.VoucherDate, pbDateFormat) & "', '" & IIf(ObjVoucher.CashBankAccID = 0, "", ObjVoucher.VoucherNarration) & "', " & IIf(ObjVoucher.CashBankAccID = 0, "Null", ObjVoucher.CashBankAccID) & ", '" & ObjVoucher.ChequeNo & "', " & IIf(ObjVoucher.ChequeDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(ObjVoucher.ChequeDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", " & IIf(ObjVoucher.DueDate = Date.MinValue, "convert(datetime,'" & Format(ObjVoucher.VoucherDate, "dd-MMM-yyyy hh:m tt") & "',108) ", " convert(datetime,'" & Format(ObjVoucher.DueDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", 0," & IIf(ObjVoucher.OtherVoucher = True, 1, 0) & ",'" & ObjVoucher.Source & "')"
                    'CR#300 added a new column IsLightwaveVoucher
                    strSQL = "INSERT INTO tblGlVoucher (location_id, voucher_code, finiancial_year_id, voucher_type_id, voucher_month, voucher_no, "
                    strSQL = strSQL & " voucher_date, paid_to, coa_detail_id, cheque_no, cheque_date, due_date, post, other_voucher,Source,IsLightwaveVoucher)"
                    strSQL = strSQL & " VALUES (" & ObjVoucher.LocationID & ", '" & ObjVoucher.VoucherCode & "', " & ObjVoucher.FiniancialYearID & ", " & ObjVoucher.VoucherTypeID & ", '" & ObjVoucher.VoucherMonth & "', '" & ObjVoucher.VoucherNo & "', "
                    strSQL = strSQL & " '" & Format(ObjVoucher.VoucherDate, pbDateFormat) & "', '" & IIf(ObjVoucher.CashBankAccID = 0, "", ObjVoucher.VoucherNarration) & "', " & IIf(ObjVoucher.CashBankAccID = 0, "Null", ObjVoucher.CashBankAccID) & ", '" & ObjVoucher.ChequeNo & "', " & IIf(ObjVoucher.ChequeDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(ObjVoucher.ChequeDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", " & IIf(ObjVoucher.DueDate = Date.MinValue, "convert(datetime,'" & Format(ObjVoucher.VoucherDate, "dd-MMM-yyyy hh:m tt") & "',108) ", " convert(datetime,'" & Format(ObjVoucher.DueDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", 0," & IIf(ObjVoucher.OtherVoucher = True, 1, 0) & ",'" & ObjVoucher.Source & "','True')"
                    strSQL = strSQL & " Select Ident_Current('tblGlVoucher')"


                    ''Execute SQL 
                    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                    VoucherID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)
                    ObjVoucher.voucherID = VoucherID

                Else
                    'CR#300 added a new column IsLightwaveVoucher
                    strSQL = "INSERT INTO tblGlVoucher (location_id, voucher_code, finiancial_year_id, voucher_type_id, voucher_month, voucher_no, "
                    strSQL = strSQL & " voucher_date, paid_to, coa_detail_id, cheque_no, cheque_date, due_date, post, other_voucher,Source,IsLightwaveVoucher)"
                    strSQL = strSQL & " VALUES (" & ObjVoucher.LocationID & ", '" & ObjVoucher.VoucherCode & "', " & ObjVoucher.FiniancialYearID & ", " & ObjVoucher.VoucherTypeID & ", '" & ObjVoucher.VoucherMonth & "', '" & ObjVoucher.VoucherNo & "', "
                    strSQL = strSQL & " '" & Format(ObjVoucher.VoucherDate, pbDateFormat) & "', '" & IIf(ObjVoucher.CashBankAccID = 0, "", ObjVoucher.VoucherNarration) & "', " & IIf(ObjVoucher.CashBankAccID = 0, "Null", ObjVoucher.CashBankAccID) & ", '" & ObjVoucher.ChequeNo & "', " & IIf(ObjVoucher.ChequeDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(ObjVoucher.ChequeDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", " & IIf(ObjVoucher.DueDate = Date.MinValue, "convert(datetime,'" & Format(ObjVoucher.VoucherDate, "dd-MMM-yyyy hh:m tt") & "',108) ", " convert(datetime,'" & Format(ObjVoucher.DueDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", 0," & IIf(ObjVoucher.OtherVoucher = True, 1, 0) & ",'" & ObjVoucher.Source & "','True')"
                    strSQL = strSQL & " Select Ident_Current('tblGlVoucher')"

                    VoucherID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)
                    ObjVoucher.voucherID = VoucherID
                End If
                'Build the SQL of Insert Master record
                'strSQL = "INSERT INTO tblGlVoucher (location_id, voucher_code, finiancial_year_id, voucher_type_id, voucher_month, voucher_no, "
                'strSQL = strSQL & " voucher_date, paid_to, coa_detail_id, cheque_no, cheque_date, due_date, post, other_voucher,Source)"
                'strSQL = strSQL & " VALUES (" & ObjVoucher.LocationID & ", '" & ObjVoucher.VoucherCode & "', " & ObjVoucher.FiniancialYearID & ", " & ObjVoucher.VoucherTypeID & ", '" & ObjVoucher.VoucherMonth & "', '" & ObjVoucher.VoucherNo & "', "
                'strSQL = strSQL & " '" & Format(ObjVoucher.VoucherDate, pbDateFormat) & "', '" & IIf(ObjVoucher.CashBankAccID = 0, "", ObjVoucher.VoucherNarration) & "', " & IIf(ObjVoucher.CashBankAccID = 0, "Null", ObjVoucher.CashBankAccID) & ", '" & ObjVoucher.ChequeNo & "', " & IIf(ObjVoucher.ChequeDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(ObjVoucher.ChequeDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", " & IIf(ObjVoucher.DueDate = Date.MinValue, "convert(datetime,'" & Format(ObjVoucher.VoucherDate, "dd-MMM-yyyy hh:m tt") & "',108) ", " convert(datetime,'" & Format(ObjVoucher.DueDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", 0," & IIf(ObjVoucher.OtherVoucher = True, 1, 0) & ",'" & ObjVoucher.Source & "')"
                'strSQL = strSQL & " Select @@Identity"

                ' ''SQL Statement Log
                ObjVoucher.ActivityLog.SQLType = "INSERT"
                ObjVoucher.ActivityLog.TableName = "tblGlVoucher"
                ObjVoucher.ActivityLog.SQL = strSQL
                UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)


                For Each _objVoucherItem As VoucherDetailItem In ObjVoucher.ListofVouchers

                    'insert the Detail data in Database
                    strSQL = String.Empty

                    If G_IsSSBOn = True Then 'CR#160 (Service Broker relavant Changes)
                        'strSQL = "select isnull(max(voucher_detail_id),0)+1 from tblGlVoucherDetail where shop_id <= 0"
                        'Voucher_Detail_ID = Convert.ToInt64(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))
                        'strSQL = "INSERT INTO tblGlVoucherDetail (voucher_detail_id,voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                        'strSQL = strSQL & " VALUES (" & Voucher_Detail_ID & "," & ObjVoucher.voucherID & ", " & ObjVoucher.LocationID & ", " & _objVoucherItem.COADetailID & ", '" & _objVoucherItem.Comments & "', " & _objVoucherItem.DebitAmount & ", " & _objVoucherItem.CreditAmount & ", " & IIf(_objVoucherItem.CostCenterID = 0, "Null", _objVoucherItem.CostCenterID) & ")"
                        strSQL = "INSERT INTO tblGlVoucherDetail (voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                        strSQL = strSQL & " VALUES (" & VoucherID & ", " & ObjVoucher.LocationID & ", " & _objVoucherItem.COADetailID & ", '" & _objVoucherItem.Comments & "', " & _objVoucherItem.DebitAmount & ", " & _objVoucherItem.CreditAmount & ", " & IIf(_objVoucherItem.CostCenterID = 0, "Null", _objVoucherItem.CostCenterID) & ")"
                    Else
                        strSQL = "INSERT INTO tblGlVoucherDetail (voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                        strSQL = strSQL & " VALUES (" & VoucherID & ", " & ObjVoucher.LocationID & ", " & _objVoucherItem.COADetailID & ", '" & _objVoucherItem.Comments & "', " & _objVoucherItem.DebitAmount & ", " & _objVoucherItem.CreditAmount & ", " & IIf(_objVoucherItem.CostCenterID = 0, "Null", _objVoucherItem.CostCenterID) & ")"
                    End If

                    SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                    ' ''SQL Statement Log
                    _objVoucherItem.ActivityLog.SQLType = "INSERT"
                    _objVoucherItem.ActivityLog.TableName = "tblGlVoucherDetail"
                    _objVoucherItem.ActivityLog.SQL = strSQL
                    UtilityDAL.BuildSQLLog(_objVoucherItem.ActivityLog, trans)

                Next

            Else
                ''Temp Voucher INSERTION

                If G_IsSSBOn = True Then 'CR#160 (Service Broker relavant Changes)

                    'strSQL = "Select isnull(Max(voucher_id),0) + 1 as MaxID from tblGlVoucherTemp where shop_id <= 0"
                    'ObjVoucher.voucherID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

                    'strSQL = "INSERT INTO tblGlVoucherTemp (voucher_id,location_id, voucher_code, finiancial_year_id, voucher_type_id, voucher_month, voucher_no, "
                    'strSQL = strSQL & " voucher_date, paid_to, coa_detail_id, cheque_no, cheque_date, due_date, post, other_voucher,Source)"
                    'strSQL = strSQL & " VALUES (" & ObjVoucher.voucherID & "," & ObjVoucher.LocationID & ", '" & ObjVoucher.VoucherCode & "', " & ObjVoucher.FiniancialYearID & ", " & ObjVoucher.VoucherTypeID & ", '" & ObjVoucher.VoucherMonth & "', '" & ObjVoucher.VoucherNo & "', "
                    'strSQL = strSQL & " '" & Format(ObjVoucher.VoucherDate, pbDateFormat) & "', '" & IIf(ObjVoucher.CashBankAccID = 0, "", ObjVoucher.VoucherNarration) & "', " & IIf(ObjVoucher.CashBankAccID = 0, "Null", ObjVoucher.CashBankAccID) & ", '" & ObjVoucher.ChequeNo & "', " & IIf(ObjVoucher.ChequeDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(ObjVoucher.ChequeDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", " & IIf(ObjVoucher.DueDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(ObjVoucher.DueDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", 0," & IIf(ObjVoucher.OtherVoucher = True, 1, 0) & ",'" & ObjVoucher.Source & "')"

                    strSQL = "INSERT INTO tblGlVoucherTemp (location_id, voucher_code, finiancial_year_id, voucher_type_id, voucher_month, voucher_no, "
                    strSQL = strSQL & " voucher_date, paid_to, coa_detail_id, cheque_no, cheque_date, due_date, post, other_voucher,Source,IsLightwaveVoucher)" 'CR#300 new column IsLightwaveVoucher
                    strSQL = strSQL & " VALUES (" & ObjVoucher.LocationID & ", '" & ObjVoucher.VoucherCode & "', " & ObjVoucher.FiniancialYearID & ", " & ObjVoucher.VoucherTypeID & ", '" & ObjVoucher.VoucherMonth & "', '" & ObjVoucher.VoucherNo & "', "
                    strSQL = strSQL & " '" & Format(ObjVoucher.VoucherDate, pbDateFormat) & "', '" & IIf(ObjVoucher.CashBankAccID = 0, "", ObjVoucher.VoucherNarration) & "', " & IIf(ObjVoucher.CashBankAccID = 0, "Null", ObjVoucher.CashBankAccID) & ", '" & ObjVoucher.ChequeNo & "', " & IIf(ObjVoucher.ChequeDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(ObjVoucher.ChequeDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", " & IIf(ObjVoucher.DueDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(ObjVoucher.DueDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", 0," & IIf(ObjVoucher.OtherVoucher = True, 1, 0) & ",'" & ObjVoucher.Source & "','True')"
                    strSQL = strSQL & " Select Ident_Current('tblGlVoucherTemp')"

                    ''Execute SQL 
                    VoucherID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                Else

                    'Build the SQL of Insert Master record
                    strSQL = "INSERT INTO tblGlVoucherTemp (location_id, voucher_code, finiancial_year_id, voucher_type_id, voucher_month, voucher_no, "
                    strSQL = strSQL & " voucher_date, paid_to, coa_detail_id, cheque_no, cheque_date, due_date, post, other_voucher,Source,IsLightwaveVoucher)" 'CR#300 new column IsLightwaveVoucher
                    strSQL = strSQL & " VALUES (" & ObjVoucher.LocationID & ", '" & ObjVoucher.VoucherCode & "', " & ObjVoucher.FiniancialYearID & ", " & ObjVoucher.VoucherTypeID & ", '" & ObjVoucher.VoucherMonth & "', '" & ObjVoucher.VoucherNo & "', "
                    strSQL = strSQL & " '" & Format(ObjVoucher.VoucherDate, pbDateFormat) & "', '" & IIf(ObjVoucher.CashBankAccID = 0, "", ObjVoucher.VoucherNarration) & "', " & IIf(ObjVoucher.CashBankAccID = 0, "Null", ObjVoucher.CashBankAccID) & ", '" & ObjVoucher.ChequeNo & "', " & IIf(ObjVoucher.ChequeDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(ObjVoucher.ChequeDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", " & IIf(ObjVoucher.DueDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(ObjVoucher.DueDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", 0," & IIf(ObjVoucher.OtherVoucher = True, 1, 0) & ",'" & ObjVoucher.Source & "','True')"
                    strSQL = strSQL & " Select Ident_Current('tblGlVoucherTemp')"

                    ''Execute SQL 
                    VoucherID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                End If


                ''Build the SQL of Insert Master record
                'strSQL = "INSERT INTO tblGlVoucherTemp (location_id, voucher_code, finiancial_year_id, voucher_type_id, voucher_month, voucher_no, "
                'strSQL = strSQL & " voucher_date, paid_to, coa_detail_id, cheque_no, cheque_date, due_date, post, other_voucher,Source)"
                'strSQL = strSQL & " VALUES (" & ObjVoucher.LocationID & ", '" & ObjVoucher.VoucherCode & "', " & ObjVoucher.FiniancialYearID & ", " & ObjVoucher.VoucherTypeID & ", '" & ObjVoucher.VoucherMonth & "', '" & ObjVoucher.VoucherNo & "', "
                'strSQL = strSQL & " '" & Format(ObjVoucher.VoucherDate, pbDateFormat) & "', '" & IIf(ObjVoucher.CashBankAccID = 0, "", ObjVoucher.VoucherNarration) & "', " & IIf(ObjVoucher.CashBankAccID = 0, "Null", ObjVoucher.CashBankAccID) & ", '" & ObjVoucher.ChequeNo & "', " & IIf(ObjVoucher.ChequeDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(ObjVoucher.ChequeDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", " & IIf(ObjVoucher.DueDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(ObjVoucher.DueDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", 0," & IIf(ObjVoucher.OtherVoucher = True, 1, 0) & ",'" & ObjVoucher.Source & "')"
                'strSQL = strSQL & " Select Ident_Current('tblGlVoucherTemp')"

                

                ' ''SQL Statement Log
                ObjVoucher.ActivityLog.SQLType = "INSERT"
                ObjVoucher.ActivityLog.TableName = "tblGlVoucherTemp"
                ObjVoucher.ActivityLog.SQL = strSQL
                UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)


                For Each _objVoucherItem As VoucherDetailItem In ObjVoucher.ListofVouchers


                    strSQL = String.Empty

                    'If G_IsSSBOn = True Then 'CR#160 (Service Broker relavant Changes)

                    'strSQL = "select isnull(max(voucher_detail_id),0)+1 from tblGlVoucherTempDetail  where shop_id <= 0"
                    'Voucher_Detail_ID = Convert.ToInt64(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

                    ''strSQL = "INSERT INTO tblGlVoucherTempDetail (voucher_detail_id,voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                    ''strSQL = strSQL & " VALUES (" & Voucher_Detail_ID & "," & ObjVoucher.voucherID & ", " & ObjVoucher.LocationID & ", " & _objVoucherItem.COADetailID & ", '" & _objVoucherItem.Comments & "', " & _objVoucherItem.DebitAmount & ", " & _objVoucherItem.CreditAmount & ", " & IIf(_objVoucherItem.CostCenterID = 0, "Null", _objVoucherItem.CostCenterID) & ")"

                    'strSQL = "INSERT INTO tblGlVoucherTempDetail (voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                    'strSQL = strSQL & " VALUES (" & VoucherID & ", " & ObjVoucher.LocationID & ", " & _objVoucherItem.COADetailID & ", '" & _objVoucherItem.Comments & "', " & _objVoucherItem.DebitAmount & ", " & _objVoucherItem.CreditAmount & ", " & IIf(_objVoucherItem.CostCenterID = 0, "Null", _objVoucherItem.CostCenterID) & ")"

                    'Else

                    'insert the Detail data in Database
                    strSQL = "INSERT INTO tblGlVoucherTempDetail (voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                    strSQL = strSQL & " VALUES (" & VoucherID & ", " & ObjVoucher.LocationID & ", " & _objVoucherItem.COADetailID & ", '" & _objVoucherItem.Comments & "', " & _objVoucherItem.DebitAmount & ", " & _objVoucherItem.CreditAmount & ", " & IIf(_objVoucherItem.CostCenterID = 0, "Null", _objVoucherItem.CostCenterID) & ")"

                    'End If

                    SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)


                    ''insert the Detail data in Database
                    'strSQL = "INSERT INTO tblGlVoucherTempDetail (voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                    'strSQL = strSQL & " VALUES (" & VoucherID & ", " & ObjVoucher.LocationID & ", " & _objVoucherItem.COADetailID & ", '" & _objVoucherItem.Comments & "', " & _objVoucherItem.DebitAmount & ", " & _objVoucherItem.CreditAmount & ", " & IIf(_objVoucherItem.CostCenterID = 0, "Null", _objVoucherItem.CostCenterID) & ")"

                    'SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                    ' ''SQL Statement Log
                    _objVoucherItem.ActivityLog.SQLType = "INSERT"
                    _objVoucherItem.ActivityLog.TableName = "tblGlVoucherTempDetail"
                    _objVoucherItem.ActivityLog.SQL = strSQL
                    UtilityDAL.BuildSQLLog(_objVoucherItem.ActivityLog, trans)

                Next

            End If

            ' ''Activity Log
            ObjVoucher.ActivityLog.FormAction = "Save"
            ObjVoucher.ActivityLog.LogRef = VoucherID
            ObjVoucher.ActivityLog.LogGroup = "Transactions"
            ObjVoucher.ActivityLog.ScreenTitle = "Voucher"
            UtilityDAL.BuildActivityLog(ObjVoucher.ActivityLog, trans)

            ''Commit Traction
            trans.Commit()

            Return True


        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function
    Private Function funRightsUserLocation(ByVal ObjVoucher As Company, ByVal trans As SqlTransaction) As Boolean

        Dim strSQL As String

        Try

            strSQL = "SELECT ISNULL(MAX(location_id),0) as MaxLoc FROM tblGlDefLocation"

            MaxLocId = 0

            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL, trans)
            'Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            If dt.Rows.Count > 0 Then
                MaxLocId = CInt(dt.Rows(0).Item("MaxLoc").ToString)
            Else
                MaxLocId = 0
            End If

            'End Using
            'Give rights of this new location to the lumensoft default user.

            strSQL = "INSERT INTO tblGLSecurityUserLocation ([user_id], location_id) VALUES(1," & MaxLocId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

            ''SQL Statement Log
            ObjVoucher.ActivityLog.SQLType = "INSERT"
            ObjVoucher.ActivityLog.TableName = "tblGLSecurityUserLocation "
            ObjVoucher.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function funAssociateYear(ByVal ObjVoucher As Company, ByVal trans As SqlTransaction) As Boolean
        Dim strSQL As String
        Dim objDA As SqlDataAdapter
        Dim DT As New DataTable

        Try

            strSQL = "SELECT tblGlDefFinancialYear.financial_year_id [FyearID] FROM tblGLDefFinancialyear " & _
                     "  Inner Join tblGlDefFinancialYearStatus " & _
                     "   on tblGlDefFinancialYear.financial_year_id =tblGlDefFinancialYearStatus.financial_year_id " & _
                     "   Where tblGlDefFinancialYearStatus.status='Open'"

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            objDA.Fill(DT)

            If DT.Rows.Count > 0 Then
                For Each Row As DataRow In DT.Rows

                    strSQL = "INSERT INTO tblGlDefFinancialYearStatus (financial_year_id, status, location_id) VALUES  ( " & Row.Item("FyearID").ToString & ",'Open'," & MaxLocId & ")"

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

                    ''SQL Statement Log
                    ObjVoucher.ActivityLog.SQLType = "INSERT"
                    ObjVoucher.ActivityLog.TableName = "tblGlDefFinancialYearStatus "
                    ObjVoucher.ActivityLog.SQL = strSQL
                    UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)

                Next
            End If

            Return True


        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Deleted(ByVal ObjVoucher As GLVoucher, Optional ByVal TempVoucher As Boolean = False) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            If TempVoucher = False Then
                'Cr # 245
                AddVoucherHistory(ObjVoucher, trans)
                'Deletion of Normal Voucher
                'Delete From tblShopOrderDetail
                strSQL = "DELETE FROM tblGlVoucherDetail Where (voucher_id = " & ObjVoucher.voucherID & ") And (location_id = " & ObjVoucher.LocationID & ") and shop_id <= 0 "

                ''Execute SQL 
                SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                'Maintaining SQL Log
                ObjVoucher.ActivityLog.SQLType = "DELETE"
                ObjVoucher.ActivityLog.TableName = "tblGlVoucherDetail"
                ObjVoucher.ActivityLog.SQL = strSQL
                UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)

                'Delete From tblShopOrderAlteration
                strSQL = "DELETE FROM tblGlVoucher Where (voucher_id = " & ObjVoucher.voucherID & ") And (location_id = " & ObjVoucher.LocationID & ") and shop_id <= 0"

                SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                'Maintaining SQL Log
                ObjVoucher.ActivityLog.SQLType = "DELETE"
                ObjVoucher.ActivityLog.TableName = "tblGlVoucher"
                ObjVoucher.ActivityLog.SQL = strSQL
                UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)

                '   Unpost the Temporary voucher if exists
                If ObjVoucher.TempVoucherID > 0 Then

                    'build a new to Un-Post
                    strSQL = "UPDATE tbLGLVoucherTemp SET Post = 0 WHERE Voucher_ID = " & ObjVoucher.TempVoucherID & " and shop_id <= 0"

                    SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)
                    'Maintaining SQL Log
                    ObjVoucher.ActivityLog.SQLType = "UPDATE"
                    ObjVoucher.ActivityLog.TableName = "tbLGLVoucherTemp"
                    ObjVoucher.ActivityLog.SQL = strSQL
                    UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)

                End If

            Else
                'Deletion of Temp Voucher

                'Delete From tblShopOrderDetail
                strSQL = "DELETE FROM tblGlVoucherTempDetail Where (voucher_id = " & ObjVoucher.voucherID & ") And (location_id = " & ObjVoucher.LocationID & ") and shop_id <= 0"

                ''Execute SQL 
                SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                'Maintaining SQL Log
                ObjVoucher.ActivityLog.SQLType = "DELETE"
                ObjVoucher.ActivityLog.TableName = "tblGlVoucherTempDetail"
                ObjVoucher.ActivityLog.SQL = strSQL
                UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)

                'Delete From tblShopOrderAlteration
                strSQL = "DELETE FROM tblGlVoucherTemp Where (voucher_id = " & ObjVoucher.voucherID & ") And (location_id = " & ObjVoucher.LocationID & ") and shop_id <= 0"

                SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                'Maintaining SQL Log
                ObjVoucher.ActivityLog.SQLType = "DELETE"
                ObjVoucher.ActivityLog.TableName = "tblGlVoucherTemp"
                ObjVoucher.ActivityLog.SQL = strSQL
                UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)

            End If



            ' ''Activity Log

            'ObjVoucher.ActivityLog.FormAction = "Delete"
            ObjVoucher.ActivityLog.SQL = strSQL
            ObjVoucher.ActivityLog.FormAction = "Delete"
            'ObjVoucher.ActivityLog.ScreenTitle = "Voucher"
            'ObjVoucher.ActivityLog.LogGroup = "Transactions"
            'ObjVoucher.ActivityLog.UserID = ObjVoucher.voucherID
            UtilityDAL.BuildActivityLog(ObjVoucher.ActivityLog, trans)

            ''Commit Traction
            trans.Commit()

            ''Return
            Return True

        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function
    Public Function GetAll(ByVal FyearID As Integer, ByVal FYearStartDate As Date, ByVal FYearEndDate As Date, Optional ByVal strCondition As String = "", Optional ByVal TempVoucher As Boolean = False) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter

        Try


            Dim strSQL As String

            If TempVoucher = False Then

                'Build SQL for selected location combo
                strSQL = "SELECT tblGlVoucher.voucher_id, '[' + tblGlDefLocation.location_code + '] ' + tblGlDefLocation.location_name AS Location, tblGlVoucher.location_id, "
                strSQL = strSQL & " tblGlVoucher.voucher_code, tblGlVoucher.finiancial_year_id, tblGlDefFinancialYear.year_code, tblGlVoucher.voucher_type_id,"
                strSQL = strSQL & " tblGlDefVoucherType.voucher_type, tblGlVoucher.voucher_month, tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.paid_to,"
                strSQL = strSQL & " tblGlVoucher.coa_detail_id, tblGlCOAMainSubSubDetail.detail_code, tblGlCOAMainSubSubDetail.detail_title, tblGlVoucher.cheque_no,"
                strSQL = strSQL & " tblGlVoucher.cheque_date, tblGlVoucher.due_date, tblGlVoucher.post,tblGlVoucher.other_voucher, tblGlVoucher.source, ISNULL(Temp_Voucher_id,0) AS Temp_Voucher_id  "
                strSQL = strSQL & " FROM tblGlVoucher LEFT OUTER JOIN tblGlDefLocation ON tblGlVoucher.location_id = tblGlDefLocation.location_id LEFT OUTER JOIN"
                strSQL = strSQL & " tblGlDefFinancialYear ON tblGlVoucher.finiancial_year_id = tblGlDefFinancialYear.financial_year_id LEFT OUTER JOIN"
                strSQL = strSQL & " tblGlDefVoucherType ON tblGlVoucher.voucher_type_id = tblGlDefVoucherType.voucher_type_id LEFT OUTER JOIN"
                strSQL = strSQL & " tblGlCOAMainSubSubDetail ON tblGlVoucher.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id"
                strSQL = strSQL & " Where (tblGlVoucher.finiancial_year_id = " & FyearID & ")"
                strSQL = strSQL & " AND (tblGlVoucher.Voucher_date between '" & Format(FYearStartDate, "yyyy/MM/dd") & "' AND '" & Format(FYearEndDate, "yyyy/MM/dd") & "'  ) AND Voucher_no <> '000000'  "
                strSQL = strSQL & strCondition
                strSQL = strSQL & " ORDER BY tblGlVoucher.voucher_date ASC, tblGlVoucher.voucher_no ASC, tblGlVoucher.voucher_code"

                objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

                Dim MyCollectionList As New DataTable("Voucher")
                objDA.Fill(MyCollectionList)

                Return MyCollectionList

            Else

                'Build SQL for selected location combo
                strSQL = "SELECT tblGlVoucherTemp.voucher_id, '[' + tblGlDefLocation.location_code + '] ' + tblGlDefLocation.location_name AS Location, tblGlVoucherTemp.location_id, "
                strSQL = strSQL & " tblGlVoucherTemp.voucher_code, tblGlVoucherTemp.finiancial_year_id, tblGlDefFinancialYear.year_code, tblGlVoucherTemp.voucher_type_id,"
                strSQL = strSQL & " tblGlDefVoucherType.voucher_type, tblGlVoucherTemp.voucher_month, tblGlVoucherTemp.voucher_no, tblGlVoucherTemp.voucher_date, tblGlVoucherTemp.paid_to,"
                strSQL = strSQL & " tblGlVoucherTemp.coa_detail_id, tblGlCOAMainSubSubDetail.detail_code, tblGlCOAMainSubSubDetail.detail_title, tblGlVoucherTemp.cheque_no,"
                strSQL = strSQL & " tblGlVoucherTemp.cheque_date, tblGlVoucherTemp.due_date, tblGlVoucherTemp.post,tblGlVoucherTemp.other_voucher, tblGlVoucherTemp.source, -1 AS Temp_Voucher_id  "
                strSQL = strSQL & " FROM tblGlVoucherTemp LEFT OUTER JOIN tblGlDefLocation ON tblGlVoucherTemp.location_id = tblGlDefLocation.location_id LEFT OUTER JOIN"
                strSQL = strSQL & " tblGlDefFinancialYear ON tblGlVoucherTemp.finiancial_year_id = tblGlDefFinancialYear.financial_year_id LEFT OUTER JOIN"
                strSQL = strSQL & " tblGlDefVoucherType ON tblGlVoucherTemp.voucher_type_id = tblGlDefVoucherType.voucher_type_id LEFT OUTER JOIN"
                strSQL = strSQL & " tblGlCOAMainSubSubDetail ON tblGlVoucherTemp.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id"
                strSQL = strSQL & " Where (tblGlVoucherTemp.finiancial_year_id = " & FyearID & ")"
                strSQL = strSQL & " AND (tblGlVoucherTemp.Voucher_date between '" & Format(FYearStartDate, "yyyy/MM/dd") & "' AND '" & Format(FYearEndDate, "yyyy/MM/dd") & "'  )"
                strSQL = strSQL & strCondition
                strSQL = strSQL & " ORDER BY tblGlVoucherTemp.voucher_date ASC, tblGlVoucherTemp.voucher_no ASC, tblGlVoucherTemp.voucher_code"

                objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

                Dim MyCollectionList As New DataTable("VoucherTemp")
                objDA.Fill(MyCollectionList)

                Return MyCollectionList


            End If



        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function
    Public Function GetDetailofVoucher(ByVal VoucherID As Integer, ByVal LocId As Integer, Optional ByVal Condition As String = "", Optional ByVal TempVoucher As Boolean = False) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter

        Try


            Dim strSQL As String

            If TempVoucher = False Then
                'After change of Cost Center
                'strSQL = "SELECT tblGlVoucherDetail.voucher_detail_id [VoucherDtl ID], tblGlVoucherDetail.coa_detail_id [COADtl ID], tblGlCOAMainSubSubDetail.detail_code [A/C Code], "
                'strSQL = strSQL & " tblGlCOAMainSubSubDetail.detail_title [Title], tblGlVoucherDetail.comments [Description],'---Select---' AS [Cost Center], tblGlVoucherDetail.debit_amount [Debit], tblGlVoucherDetail.credit_amount [Credit], '' As [Delete], '' As Action, '' As [CashBank]"
                'strSQL = strSQL & " FROM tblGlVoucherDetail LEFT OUTER JOIN tblGlDefGLCostCenter ON tblGlVoucherDetail.cost_center_id = tblGlDefGLCostCenter.cost_center_id LEFT OUTER JOIN"
                'strSQL = strSQL & " tblGlCOAMainSubSubDetail ON tblGlVoucherDetail.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id"
                'strSQL = strSQL & " WHERE (tblGlVoucherDetail.voucher_id = " & VoucherID & ") AND (tblGlVoucherDetail.location_id = " & LocId & ")"
                strSQL = "SELECT tblGlVoucherDetail.voucher_detail_id [VoucherDtl ID], tblGlVoucherDetail.coa_detail_id [COADtl ID], tblGlCOAMainSubSubDetail.detail_code [A/C Code], "
                strSQL = strSQL & " tblGlCOAMainSubSubDetail.detail_title [Title], tblGlVoucherDetail.comments [Description], "
                'strSQL = strSQL & " ISNULL(tblGlDefGLCostCenter.cost_center_id ,0) AS [Cost Center],tblGlVoucherDetail.debit_amount [Debit], tblGlVoucherDetail.credit_amount [Credit], '' As [Delete], '' As Action, '' As [CashBank] "
                strSQL = strSQL & " tblGlDefGLCostCenter.cost_center_id AS [Cost Center],tblGlVoucherDetail.debit_amount [Debit], tblGlVoucherDetail.credit_amount [Credit], '' As [Delete], '' As Action, '' As [CashBank] "
                strSQL = strSQL & " FROM tblGlVoucherDetail LEFT OUTER JOIN tblGlDefGLCostCenter ON tblGlVoucherDetail.cost_center_id = tblGlDefGLCostCenter.cost_center_id LEFT OUTER JOIN"
                strSQL = strSQL & " tblGlCOAMainSubSubDetail ON tblGlVoucherDetail.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id"
                strSQL = strSQL & " WHERE (tblGlVoucherDetail.voucher_id = " & VoucherID & ") AND (tblGlVoucherDetail.location_id = " & LocId & ")"

                strSQL = strSQL & Condition

                objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

                Dim MyCollectionList As New DataTable("VoucherDetail")
                objDA.Fill(MyCollectionList)

                Return MyCollectionList


            Else

                'After change of Cost Center
                strSQL = "SELECT tblGlVoucherTempDetail.voucher_detail_id [VoucherDtl ID], tblGlVoucherTempDetail.coa_detail_id [COADtl ID], tblGlCOAMainSubSubDetail.detail_code [A/C Code], "
                strSQL = strSQL & " tblGlCOAMainSubSubDetail.detail_title [Title], tblGlVoucherTempDetail.comments [Description],ISNULL(tblGlDefGLCostCenter.cost_center_id ,0) AS [Cost Center], tblGlVoucherTempDetail.debit_amount [Debit], tblGlVoucherTempDetail.credit_amount [Credit], '' As [Delete], '' As Action, '' As [CashBank]"
                strSQL = strSQL & " FROM tblGlVoucherTempDetail LEFT OUTER JOIN tblGlDefGLCostCenter ON tblGlVoucherTempDetail.cost_center_id = tblGlDefGLCostCenter.cost_center_id LEFT OUTER JOIN"
                strSQL = strSQL & " tblGlCOAMainSubSubDetail ON tblGlVoucherTempDetail.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id"
                strSQL = strSQL & " WHERE (tblGlVoucherTempDetail.voucher_id = " & VoucherID & ") AND (tblGlVoucherTempDetail.location_id = " & LocId & ")"
                strSQL = strSQL & Condition

                objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

                Dim MyCollectionList As New DataTable("VoucherDetail")
                objDA.Fill(MyCollectionList)

                Return MyCollectionList


            End If

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function
    Public Function GetVoucherType(Optional ByVal ISTempVoucher As Boolean = False) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String

            If ISTempVoucher = False Then
                strSQL = "SELECT voucher_type [VType], voucher_type_id [VType ID] From tblGlDefVoucherType WHERE Voucher_type <> 'TV' ORDER BY sort_order, voucher_type"
            Else
                strSQL = "SELECT voucher_type [VType], voucher_type_id [VType ID]  From tblGlDefVoucherType WHERE Voucher_Type IN ('CRV', 'CPV') AND Voucher_Type <> 'TV' ORDER BY sort_order, voucher_type"
            End If

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("VType")

            objDA.Fill(MyCollectionList)

            Dim dr As DataRow = MyCollectionList.NewRow
            dr.Item("VType ID") = 0
            dr.Item("VType") = gstrComboZeroIndexString
            MyCollectionList.Rows.InsertAt(dr, 0)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function
    Public Function GetAutoCompletedata() As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String


            strSQL = " SELECT coa_detail_id AS AccountID, detail_code AS AccountCode, detail_title AS AccountName " & _
                    " FROM tblGlCOAMainSubSub INNER JOIN  tblGlCOAMainSubSubDetail ON " & _
                    " tblGlCOAMainSubSub.main_sub_sub_id = tblGlCOAMainSubSubDetail.main_sub_sub_id " & _
                    " Where end_date Is NULL  ORDER BY AccountCode "

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("MGLAcc")

            objDA.Fill(MyCollectionList)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function
    ' CR # 337      To get credit amount of bank type voucher
    Public Function GetAmountForBankVoucher(ByVal invoice_Id As Integer) As Double
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String

            ' CR # 348
            'strSQL = " SELECT   tblGlVoucherDetail.credit_amount " & _
            '         " FROM    tblGlCOAMainSubSub INNER JOIN " & _
            '         " tblGlCOAMainSubSubDetail ON tblGlCOAMainSubSub.main_sub_sub_id = tblGlCOAMainSubSubDetail.main_sub_sub_id INNER JOIN " & _
            '         " tblGlVoucherDetail ON tblGlCOAMainSubSubDetail.coa_detail_id = tblGlVoucherDetail.coa_detail_id " & _
            '         " Where voucher_id = " & invoice_Id & " AND account_type = 'Bank'"
            strSQL = " SELECT   tblGlVoucherDetail.credit_amount " & _
                     " FROM    tblGlCOAMainSubSub INNER JOIN " & _
                     " tblGlCOAMainSubSubDetail ON tblGlCOAMainSubSub.main_sub_sub_id = tblGlCOAMainSubSubDetail.main_sub_sub_id INNER JOIN " & _
                     " tblGlVoucherDetail ON tblGlCOAMainSubSubDetail.coa_detail_id = tblGlVoucherDetail.coa_detail_id " & _
                     " Where voucher_id = " & invoice_Id & " AND (account_type = 'Bank' OR account_type ='Cash')"


            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("Credit_Amount")

            objDA.Fill(MyCollectionList)

            ' CR # 348
            'Return Convert.ToDouble(MyCollectionList.Rows(0)(0).ToString())
            If (Not MyCollectionList Is Nothing) AndAlso (MyCollectionList.Rows.Count > 0) Then
                Return Convert.ToDouble(MyCollectionList.Rows(0)(0).ToString())
            Else
                Return 0
            End If
            ' CR # 348 End
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function


    Public Function GetLocation(ByVal pbLocationID As Integer) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            If pbLocationID <> 0 Then
                strSQL = "SELECT '[' + location_code + '] ' + location_name AS Location, location_id as LocID From tblGlDefLocation WHERE (location_id = " & pbLocationID & ") ORDER BY sort_order, location_name"
            Else
                strSQL = "SELECT '[' + location_code + '] ' + location_name AS Location, location_id as LocID  From tblGlDefLocation ORDER BY sort_order, location_name"
            End If

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("Location")
            objDA.Fill(MyCollectionList)

            Dim dr As DataRow = MyCollectionList.NewRow
            dr.Item("LocID") = 0
            dr.Item("Location") = gstrComboZeroIndexString
            MyCollectionList.Rows.InsertAt(dr, 0)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function
    'CR#300 this function will be used to fetch locations/companies against loggedIn user
    Public Function GetLocationOfLoggedInUser(ByVal intUserId As Integer) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            strSQL = "SELECT '['+dbo.tblGlDefLocation.location_code+'] ' + dbo.tblGlDefLocation.location_name as Location, dbo.tblGlDefLocation.location_id LocID  " & _
                     " FROM dbo.tblGLSecurityUserLocation INNER JOIN " & _
                     " dbo.tblGlDefLocation ON dbo.tblGLSecurityUserLocation.location_id = dbo.tblGlDefLocation.location_id" & _
                     " WHERE dbo.tblGLSecurityUserLocation.user_id = " & intUserId & ""

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("Location")
            objDA.Fill(MyCollectionList)

            Dim dr As DataRow = MyCollectionList.NewRow
            dr.Item("LocID") = 0
            dr.Item("Location") = gstrComboZeroIndexString
            MyCollectionList.Rows.InsertAt(dr, 0)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function

    Public Function IsValidateForSave() As Boolean
    End Function
    Public Function IsAmountReceived(ByVal objVoucher As GLVoucher) As Boolean

        Dim strSQL As String
        Dim intTotalRec As Integer

        Try

            intTotalRec = 0

            strSQL = "select Count(*) as AmtRec from tblGLCustomer_Receipts_Detail Where voucher_id=" & objVoucher.voucherID & " and location_id=" & objVoucher.LocationID & ""

            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then

                    intTotalRec += 1
                    While objDR.Read
                        intTotalRec = CInt(objDR.Item("AmtRec"))
                    End While

                End If

            End Using

            If intTotalRec > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsAmountPaid(ByVal objVoucher As GLVoucher) As Boolean

        Dim strSQL As String
        Dim intTotalPaid As Integer

        Try

            intTotalPaid = 0

            strSQL = "select Count(*) as AmtPaid from tblGLSupplier_Payments_Detail Where voucher_id=" & objVoucher.voucherID & " and location_id=" & objVoucher.LocationID & ""

            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then

                    intTotalPaid += 1
                    While objDR.Read
                        intTotalPaid = CInt(objDR.Item("AmtPaid"))
                    End While

                End If

            End Using

            If intTotalPaid > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDBCurrentDate() As Date
        Dim strSQL As String
        Dim CurDate As Date

        Try

            strSQL = "select LEFT(CONVERT(varchar, getdate(), 120), 10) as CurrentDate"

            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read

                        CurDate = CDate(objDR.Item("CurrentDate").ToString)

                    End While
                End If

            End Using

            Return CurDate

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function FYearExist() As Boolean
        Dim strSQL As String
        Dim intFyear As Integer

        Try

            strSQL = "select count(*) as FYears from tblGlDefFinancialYear"

            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then

                    While objDR.Read
                        intFyear = CInt(objDR.Item("FYears"))
                    End While

                End If

            End Using

            If intFyear = 0 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Update(ByVal ObjVoucher As GLVoucher, Optional ByVal TempVoucher As Boolean = False) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            If TempVoucher = False Then
                'Normal Voucher Updation

                'CR # 245

                Dim Command As SqlCommand
                Command = New SqlCommand()
                Command.CommandText = "SpVoucherHistoryUpdate"
                Command.CommandType = CommandType.StoredProcedure
                Command.Parameters.Add(New SqlParameter("@VoucherID", SqlDbType.Int))
                Command.Parameters("@VoucherID").Value = ObjVoucher.voucherID
                Command.Parameters.Add(New SqlParameter("@Condition", SqlDbType.Int))
                Command.Parameters("@Condition").Value = 1
                Command.Connection = trans.Connection

                Command.Transaction = trans
                Command.ExecuteNonQuery()

                strSQL = "UPDATE tblGlVoucher"
                strSQL = strSQL & " SET  voucher_code = '" & ObjVoucher.VoucherCode & "', finiancial_year_id = " & ObjVoucher.FiniancialYearID & ", voucher_type_id = " & ObjVoucher.VoucherTypeID & ", voucher_month = '" & ObjVoucher.VoucherMonth & "', voucher_no = '" & ObjVoucher.VoucherNo & "',"
                strSQL = strSQL & " voucher_date = '" & Format(ObjVoucher.VoucherDate, pbDateFormat) & "', paid_to = '" & IIf(ObjVoucher.CashBankAccID = 0, "", ObjVoucher.VoucherNarration) & "', coa_detail_id = " & IIf(ObjVoucher.CashBankAccID = 0, "Null", ObjVoucher.CashBankAccID) & ", cheque_no = '" & ObjVoucher.ChequeNo & "',"
                strSQL = strSQL & " cheque_date = " & IIf(ObjVoucher.ChequeDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(ObjVoucher.ChequeDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", "
                strSQL = strSQL & " due_date = " & IIf(ObjVoucher.DueDate = Date.MinValue, "convert(datetime,'" & Format(ObjVoucher.VoucherDate, "dd-MMM-yyyy hh:m tt") & "',108) ", " convert(datetime,'" & Format(ObjVoucher.DueDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ""
                'CR#300
                strSQL = strSQL & ", location_id=" & ObjVoucher.LocationID

                'check if voucher is unbalance then set the sql to unpost the voucher
                If ObjVoucher.IsBalancedVoucher = False Then
                    strSQL = strSQL & " , post = 0 "
                End If

                If ObjVoucher.OtherVoucher = True Then
                    strSQL = strSQL & ", Other_Voucher = 1"
                Else
                    strSQL = strSQL & ", Other_Voucher = 0"
                End If
                'CR#300 Start
                strSQL = strSQL & ",Source='" & ObjVoucher.Source & "'"
                strSQL = strSQL & ",IsLightwaveVoucher ='True'"
                'CR#300 End 

                'CR#300 No need for check of Location Id as Voucher Id will always be unqiue
                'strSQL = strSQL & " WHERE (voucher_id = " & ObjVoucher.voucherID & ") AND (location_id = " & ObjVoucher.LocationID & ")"
                strSQL = strSQL & " WHERE (voucher_id = " & ObjVoucher.voucherID & ")"
                strSQL = strSQL & " and shop_id <= 0"

                ''Execute SQL 
                SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                ' ''SQL Statement Log
                ObjVoucher.ActivityLog.SQLType = "UPDATE"
                ObjVoucher.ActivityLog.TableName = "tblGlVoucher"
                ObjVoucher.ActivityLog.SQL = strSQL
                UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)


                ''''Delete records from tblGlVoucherDetail for those tuples which have been deleted from grid, but exist in the table.
                If ObjVoucher.ListOfDeletedIDs.Length > 0 Then

                    Dim arrDeletedAccountsIDs As String() = Split(ObjVoucher.ListOfDeletedIDs, ",")

                    For intCounter As Integer = 1 To arrDeletedAccountsIDs.Length - 1

                        strSQL = "DELETE FROM tblGlVoucherDetail " _
                             & " WHERE voucher_detail_id=" & arrDeletedAccountsIDs(intCounter) & " AND (voucher_id = " & ObjVoucher.voucherID & ") AND (location_id = " & ObjVoucher.LocationID & ")  and shop_id <= 0"

                        'Execute SQL 
                        SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                        ' ''SQL Statement Log
                        ObjVoucher.ActivityLog.SQLType = "DELETE"
                        ObjVoucher.ActivityLog.TableName = "tblGlVoucherDetail"
                        ObjVoucher.ActivityLog.SQL = strSQL
                        UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)

                    Next

                End If

                For Each _objAccountTransaction As VoucherDetailItem In ObjVoucher.ListofVouchers

                    If _objAccountTransaction.VoucherDetailID > 0 Then

                        'Build the Update SQL
                        strSQL = "UPDATE tblGlVoucherDetail SET coa_detail_id = " & _objAccountTransaction.COADetailID & ", comments = '" & _objAccountTransaction.Comments & "', "
                        strSQL = strSQL & " debit_amount = " & _objAccountTransaction.DebitAmount & ", credit_amount = " & _objAccountTransaction.CreditAmount & ", cost_center_id = " & IIf(_objAccountTransaction.CostCenterID = 0, 0, _objAccountTransaction.CostCenterID) & ""
                        'CR#300 Location Id can't be edited now
                        strSQL = strSQL & ",location_id=" & ObjVoucher.LocationID & ""
                        'CR#300 droping location Id from where class as it is not required
                        'strSQL = strSQL & " WHERE (voucher_detail_id = " & _objAccountTransaction.VoucherDetailID & ") AND (voucher_id = " & ObjVoucher.voucherID & ") AND (location_id = " & ObjVoucher.LocationID & ") and shop_id <= 0"
                        strSQL = strSQL & " WHERE (voucher_detail_id = " & _objAccountTransaction.VoucherDetailID & ") AND (voucher_id = " & ObjVoucher.voucherID & ")  and shop_id <= 0"

                        SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                        ' ''SQL Statement Log
                        _objAccountTransaction.ActivityLog.SQLType = "UPDATE"
                        _objAccountTransaction.ActivityLog.TableName = "tblGlVoucherDetail"
                        _objAccountTransaction.ActivityLog.SQL = strSQL
                        UtilityDAL.BuildSQLLog(_objAccountTransaction.ActivityLog, trans)

                    Else

                        'CR#160 (Service Broker relavant Changes)

                        'Build the Insert SQL
                        'strSQL = "INSERT INTO tblGlVoucherDetail (voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                        'strSQL = strSQL & " VALUES (" & ObjVoucher.voucherID & ", " & ObjVoucher.LocationID & ", " & _objAccountTransaction.COADetailID & ", '" & _objAccountTransaction.Comments & "', " & _objAccountTransaction.DebitAmount & ", " & _objAccountTransaction.CreditAmount & ", " & IIf(_objAccountTransaction.CostCenterID = 0, 0, _objAccountTransaction.CostCenterID) & ")"


                        If G_IsSSBOn = True Then 'CR#160 (Service Broker relavant Changes)
                            'strSQL = "select isnull(max(voucher_detail_id),0)+1 from tblGlVoucherDetail where shop_id <= 0"
                            'Voucher_Detail_ID = Convert.ToInt64(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))
                            'strSQL = "INSERT INTO tblGlVoucherDetail (voucher_detail_id,voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                            'strSQL = strSQL & " VALUES (" & Voucher_Detail_ID & "," & ObjVoucher.voucherID & ", " & ObjVoucher.LocationID & ", " & _objVoucherItem.COADetailID & ", '" & _objVoucherItem.Comments & "', " & _objVoucherItem.DebitAmount & ", " & _objVoucherItem.CreditAmount & ", " & IIf(_objVoucherItem.CostCenterID = 0, "Null", _objVoucherItem.CostCenterID) & ")"
                            strSQL = "INSERT INTO tblGlVoucherDetail (voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                            strSQL = strSQL & " VALUES (" & ObjVoucher.voucherID & ", " & ObjVoucher.LocationID & ", " & _objAccountTransaction.COADetailID & ", '" & _objAccountTransaction.Comments & "', " & _objAccountTransaction.DebitAmount & ", " & _objAccountTransaction.CreditAmount & ", " & IIf(_objAccountTransaction.CostCenterID = 0, 0, _objAccountTransaction.CostCenterID) & ")"

                        Else
                            strSQL = "INSERT INTO tblGlVoucherDetail (voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                            strSQL = strSQL & " VALUES (" & ObjVoucher.voucherID & ", " & ObjVoucher.LocationID & ", " & _objAccountTransaction.COADetailID & ", '" & _objAccountTransaction.Comments & "', " & _objAccountTransaction.DebitAmount & ", " & _objAccountTransaction.CreditAmount & ", " & IIf(_objAccountTransaction.CostCenterID = 0, 0, _objAccountTransaction.CostCenterID) & ")"
                        End If

 
                        SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                        ' ''SQL Statement Log
                        _objAccountTransaction.ActivityLog.SQLType = "INSERT"
                        _objAccountTransaction.ActivityLog.TableName = "tblGlVoucherDetail"
                        _objAccountTransaction.ActivityLog.SQL = strSQL
                        UtilityDAL.BuildSQLLog(_objAccountTransaction.ActivityLog, trans)

                    End If

                Next
                'CR # 245

                Dim comm As SqlCommand
                comm = New SqlCommand()
                comm.CommandText = "SpVoucherHistoryUpdate"
                comm.CommandType = CommandType.StoredProcedure
                comm.Parameters.Add(New SqlParameter("@VoucherID", SqlDbType.Int))
                comm.Parameters("@VoucherID").Value = ObjVoucher.voucherID
                comm.Parameters.Add(New SqlParameter("@Condition", SqlDbType.Int))
                comm.Parameters("@Condition").Value = 0
                comm.Connection = trans.Connection
                comm.Transaction = trans
                comm.ExecuteNonQuery()
            Else
                'Temp Voucher Updation

                strSQL = "UPDATE tblGlVoucherTemp"
                strSQL = strSQL & " SET  voucher_code = '" & ObjVoucher.VoucherCode & "', finiancial_year_id = " & ObjVoucher.FiniancialYearID & ", voucher_type_id = " & ObjVoucher.VoucherTypeID & ", voucher_month = '" & ObjVoucher.VoucherMonth & "', voucher_no = '" & ObjVoucher.VoucherNo & "',"
                strSQL = strSQL & " voucher_date = '" & Format(ObjVoucher.VoucherDate, pbDateFormat) & "', paid_to = '" & IIf(ObjVoucher.CashBankAccID = 0, "", ObjVoucher.VoucherNarration) & "', coa_detail_id = " & IIf(ObjVoucher.CashBankAccID = 0, "Null", ObjVoucher.CashBankAccID) & ", cheque_no = '" & ObjVoucher.ChequeNo & "',"
                strSQL = strSQL & " cheque_date = " & IIf(ObjVoucher.ChequeDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(ObjVoucher.ChequeDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", "
                strSQL = strSQL & " due_date = " & IIf(ObjVoucher.DueDate = Date.MinValue, "convert(datetime,'" & Format(ObjVoucher.VoucherDate, "dd-MMM-yyyy hh:m tt") & "',108) ", " convert(datetime,'" & Format(ObjVoucher.DueDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ""
                'CR#300
                strSQL = strSQL & ", location_id=" & ObjVoucher.LocationID

                'check if voucher is unbalance then set the sql to unpost the voucher
                If ObjVoucher.IsBalancedVoucher = False Then
                    strSQL = strSQL & " , post = 0 "
                End If

                If ObjVoucher.OtherVoucher = False Then
                    strSQL = strSQL & ", Other_Voucher = 1"
                Else
                    strSQL = strSQL & ", Other_Voucher = 0"
                End If

                'CR#300 No need for check of Location Id as Voucher Id will always be unqiue
                'strSQL = strSQL & " WHERE (voucher_id = " & ObjVoucher.voucherID & ") AND (location_id = " & ObjVoucher.LocationID & ") and shop_id <= 0"
                strSQL = strSQL & " WHERE (voucher_id = " & ObjVoucher.voucherID & ")  and shop_id <= 0"


                ''Execute SQL 
                SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                ' ''SQL Statement Log
                ObjVoucher.ActivityLog.SQLType = "UPDATE"
                ObjVoucher.ActivityLog.TableName = "tblGlVoucherTemp"
                ObjVoucher.ActivityLog.SQL = strSQL
                UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)


                ''''Delete records from tblGlVoucherDetail for those tuples which have been deleted from grid, but exist in the table.
                If ObjVoucher.ListOfDeletedIDs.Length > 0 Then

                    Dim arrDeletedAccountsIDs As String() = Split(ObjVoucher.ListOfDeletedIDs, ",")

                    For intCounter As Integer = 1 To arrDeletedAccountsIDs.Length - 1

                        strSQL = "DELETE FROM tblGlVoucherTempDetail " _
                             & " WHERE voucher_detail_id=" & arrDeletedAccountsIDs(intCounter) & " AND (voucher_id = " & ObjVoucher.voucherID & ") AND (location_id = " & ObjVoucher.LocationID & ") and shop_id <= 0"

                        'Execute SQL 
                        SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                        ' ''SQL Statement Log
                        ObjVoucher.ActivityLog.SQLType = "DELETE"
                        ObjVoucher.ActivityLog.TableName = "tblGlVoucherTempDetail"
                        ObjVoucher.ActivityLog.SQL = strSQL
                        UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)

                    Next


                    
                End If

                For Each _objAccountTransaction As VoucherDetailItem In ObjVoucher.ListofVouchers

                    If _objAccountTransaction.VoucherDetailID > 0 Then

                        'Build the Update SQL
                        strSQL = "UPDATE tblGlVoucherTempDetail SET coa_detail_id = " & _objAccountTransaction.COADetailID & ", comments = '" & _objAccountTransaction.Comments & "', "
                        strSQL = strSQL & " debit_amount = " & _objAccountTransaction.DebitAmount & ", credit_amount = " & _objAccountTransaction.CreditAmount & ", cost_center_id = " & IIf(_objAccountTransaction.CostCenterID = 0, 0, _objAccountTransaction.CostCenterID) & ""
                        'CR#300 Location Id can't be edited now
                        strSQL = strSQL & ",location_id=" & ObjVoucher.LocationID & ""
                        'CR#300 droping location Id from where class as it is not required
                        'strSQL = strSQL & " WHERE (voucher_detail_id = " & _objAccountTransaction.VoucherDetailID & ") AND (voucher_id = " & ObjVoucher.voucherID & ") AND (location_id = " & ObjVoucher.LocationID & ") and shop_id <= 0"
                        strSQL = strSQL & " WHERE (voucher_detail_id = " & _objAccountTransaction.VoucherDetailID & ") AND (voucher_id = " & ObjVoucher.voucherID & ")  and shop_id <= 0"



                        SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                        ' ''SQL Statement Log
                        _objAccountTransaction.ActivityLog.SQLType = "UPDATE"
                        _objAccountTransaction.ActivityLog.TableName = "tblGlVoucherTempDetail"
                        _objAccountTransaction.ActivityLog.SQL = strSQL
                        UtilityDAL.BuildSQLLog(_objAccountTransaction.ActivityLog, trans)

                    Else

                        'Build the Insert SQL
                        'strSQL = "INSERT INTO tblGlVoucherTempDetail (voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                        'strSQL = strSQL & " VALUES (" & ObjVoucher.voucherID & ", " & ObjVoucher.LocationID & ", " & _objAccountTransaction.COADetailID & ", '" & _objAccountTransaction.Comments & "', " & _objAccountTransaction.DebitAmount & ", " & _objAccountTransaction.CreditAmount & ", " & IIf(_objAccountTransaction.CostCenterID = 0, 0, _objAccountTransaction.CostCenterID) & ")"


                        If G_IsSSBOn = True Then 'CR#160 (Service Broker relavant Changes)
                            'strSQL = "DECLARE @Id AS INT set @Id=(select max(voucher_detail_id)+1 from tblGlVoucherTempDetail where shop_id <= 0)   "
                            'strSQL = strSQL & "INSERT INTO tblGlVoucherTempDetail (voucher_detail_id,voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                            'strSQL = strSQL & " VALUES (@id," & ObjVoucher.voucherID & ", " & ObjVoucher.LocationID & ", " & _objAccountTransaction.COADetailID & ", '" & _objAccountTransaction.Comments & "', " & _objAccountTransaction.DebitAmount & ", " & _objAccountTransaction.CreditAmount & ", " & IIf(_objAccountTransaction.CostCenterID = 0, 0, _objAccountTransaction.CostCenterID) & ")"

                            strSQL = "DECLARE @Id AS INT set @Id=(select max(voucher_detail_id)+1 from tblGlVoucherTempDetail where shop_id <= 0)   "
                            strSQL = strSQL & "INSERT INTO tblGlVoucherTempDetail (voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                            strSQL = strSQL & " VALUES (" & ObjVoucher.voucherID & ", " & ObjVoucher.LocationID & ", " & _objAccountTransaction.COADetailID & ", '" & _objAccountTransaction.Comments & "', " & _objAccountTransaction.DebitAmount & ", " & _objAccountTransaction.CreditAmount & ", " & IIf(_objAccountTransaction.CostCenterID = 0, 0, _objAccountTransaction.CostCenterID) & ")"

                        Else

                            'Build the Insert SQL
                            strSQL = "INSERT INTO tblGlVoucherTempDetail (voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount, cost_center_id)"
                            strSQL = strSQL & " VALUES (" & ObjVoucher.voucherID & ", " & ObjVoucher.LocationID & ", " & _objAccountTransaction.COADetailID & ", '" & _objAccountTransaction.Comments & "', " & _objAccountTransaction.DebitAmount & ", " & _objAccountTransaction.CreditAmount & ", " & IIf(_objAccountTransaction.CostCenterID = 0, 0, _objAccountTransaction.CostCenterID) & ")"

                        End If


                        SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                        ' ''SQL Statement Log
                        _objAccountTransaction.ActivityLog.SQLType = "INSERT"
                        _objAccountTransaction.ActivityLog.TableName = "tblGlVoucherTempDetail"
                        _objAccountTransaction.ActivityLog.SQL = strSQL
                        UtilityDAL.BuildSQLLog(_objAccountTransaction.ActivityLog, trans)

                    End If

                Next

            End If


            ' ''Activity Log
            ObjVoucher.ActivityLog.FormAction = "Update"
            ObjVoucher.ActivityLog.SQL = strSQL
            'Cr # 164
            'ObjVoucher.ActivityLog.UserID = "1"
            ObjVoucher.ActivityLog.ScreenTitle = "Voucher"
            ObjVoucher.ActivityLog.LogGroup = "Transactions"
            ObjVoucher.ActivityLog.SQL = strSQL
            UtilityDAL.BuildActivityLog(ObjVoucher.ActivityLog, trans)

            ''Commit Traction
            trans.Commit()

            ''Return
            Return True

        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try


    End Function
    Public Function GetNewVoucherNo(ByVal LocationId As Long, ByVal VoucherTypeId As Long, _
        ByVal FMonth As String, ByVal FYear As String, ByVal blnOtherVoucher As Integer, Optional ByVal TempVoucher As Boolean = False) As String

        Dim strSQL As String
        Dim NewVNo As String = ""

        Try

            If TempVoucher = False Then

                strSQL = " SELECT isNULL(Max(voucher_no) + 1,  " & IIf(blnOtherVoucher = -1, 500001, 1) & ") as NewVoucherNo From dbo.tblGlVoucher" & _
                             " Where (location_id = " & LocationId & ") And (voucher_type_id = " & VoucherTypeId & ")" & _
                             " And (voucher_month = '" & FMonth & "') And (DATENAME(Year, voucher_date) = '" & FYear & "') And (LEFT(DATENAME(month, voucher_date),3) = '" & FMonth & "') and other_voucher= " & IIf(blnOtherVoucher = -1, 1, 0) & IIf(blnOtherVoucher = -1, " and voucher_no > '500000'", " and voucher_no < '500000'")

            Else

                strSQL = " SELECT isNULL(Max(voucher_no) + 1,  " & IIf(blnOtherVoucher = -1, 500001, 1) & ") as NewVoucherNo From dbo.tblGlVoucherTemp" & _
                    " Where (location_id = " & LocationId & ") And (voucher_type_id = " & VoucherTypeId & ")" & _
                    " And (voucher_month = '" & FMonth & "') And (DATENAME(Year, voucher_date) = '" & FYear & "') And (LEFT(DATENAME(month, voucher_date),3) = '" & FMonth & "')"

            End If


            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read

                        NewVNo = objDR.Item("NewVoucherNo").ToString

                    End While
                End If

            End Using

            Return NewVNo

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetGLVType(ByVal VTypeID As Integer) As String

        Dim strSQL As String
        Dim NewVNo As String = ""

        Try

            strSQL = "Select gl_Type as [GLVType] from tblGLDefVoucherType where Voucher_Type_Id = " & VTypeID

            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read

                        NewVNo = objDR.Item("GLVType").ToString

                    End While
                End If

            End Using

            Return NewVNo

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetLastClosingDate(ByVal VType As String) As Date

        Dim strSQL As String
        Dim LastClosingDate As Date

        Try


            ''' This query get max physical audit date of specific status
            strSQL = "SELECT ISnull(MAX(Closing_Date),Convert(datetime,'01-01-1800',104)) AS Closing_Date From tblCashBankClosing WHERE (Tr_Type = '" & VType & "') "

            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read

                        LastClosingDate = CDate(objDR.Item("Closing_Date").ToString)

                    End While
                End If

            End Using

            Return LastClosingDate

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ISTrialVersionExpired(ByVal FYearID As Integer) As Boolean

        Dim strSQL As String
        Dim intTransactions As Integer

        Try

            strSQL = "SELECT COUNT(*) AS TransCount FROM tblGLVoucher where finiancial_year_id=" & FYearID & ""

            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read

                        intTransactions = Val(objDR.Item("TransCount").ToString)

                    End While
                End If

            End Using

            If intTransactions > G_intTrialVerTrans Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetLocationCode(ByVal LocationId As Long) As String

        Dim strSQL As String
        Dim LocCode As String = ""

        Try

            strSQL = "Select location_code [LocCode] from tblGLDefLocation where location_Id = " & LocationId

            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read

                        LocCode = objDR.Item("LocCode").ToString

                    End While
                End If

            End Using

            Return LocCode

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCostCenter() As DataTable
        Dim objDA As SqlDataAdapter

        Try

            Dim strSQL As String = ""

            strSQL = "SELECT cost_center_id as [Cost Center ID], cost_center_title as [Cost Center Title] FROM tblGlDefGLCostCenter"

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("CostCenter")
            objDA.Fill(MyCollectionList)

            'Dim dr As DataRow = MyCollectionList.NewRow
            'dr.Item("Cost Center ID") = 0
            'dr.Item("Cost Center Title") = gstrComboZeroIndexString
            'MyCollectionList.Rows.InsertAt(dr, 0)

            Return MyCollectionList


        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function PostVoucher(ByVal ObjVoucher As GLVoucher, Optional ByVal TempVoucher As Boolean = False) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Dim strVoucherNo As String
        Dim VoucherID As Integer
        Dim VNo As String
        Dim LocCode As String
        Dim strVoucherType As String
        Dim strSQL As String
        Try

            strVoucherNo = GetNewVNoOfActualGLTable(ObjVoucher.LocationID, ObjVoucher.VoucherTypeID, ObjVoucher.VoucherMonth, Format(ObjVoucher.VoucherDate, "yyyy"), ObjVoucher.OtherVoucher, TempVoucher)


            If TempVoucher = False Then

                'Cr # 245

                Dim Command As SqlCommand
                Command = New SqlCommand()
                Command.CommandText = "SpVoucherPostUnPost"
                Command.CommandType = CommandType.StoredProcedure
                Command.Parameters.Add(New SqlParameter("@VoucherID", SqlDbType.Int))
                Command.Parameters("@VoucherID").Value = ObjVoucher.voucherID
                Command.Parameters.Add(New SqlParameter("@Status", SqlDbType.Text))
                Command.Parameters.Add(New SqlParameter("@Condition", SqlDbType.Int))
                Command.Parameters("@Condition").Value = 0

                Command.Parameters("@Status").Value = "Post"
            
                Command.Connection = trans.Connection

                Command.Transaction = trans
                Command.ExecuteNonQuery()

                strSQL = "UPDATE tblGlVoucher Set post = 1 WHERE (voucher_id = " & ObjVoucher.voucherID & ") AND (location_id = " & ObjVoucher.LocationID & ")"

                ''Execute SQL 
                SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                ' ''SQL Statement Log
                ObjVoucher.ActivityLog.SQLType = "UPDATE"
                ObjVoucher.ActivityLog.TableName = "tblGlVoucher"
                ObjVoucher.ActivityLog.SQL = strSQL
                UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)
                'CR # 245

                Dim comm As SqlCommand
                comm = New SqlCommand()
                comm.CommandText = "SpVoucherPostUnPost"
                comm.CommandType = CommandType.StoredProcedure
                comm.Parameters.Add(New SqlParameter("@VoucherID", SqlDbType.Int))
                comm.Parameters("@VoucherID").Value = ObjVoucher.voucherID
                comm.Parameters.Add(New SqlParameter("@Status", SqlDbType.Text))
                comm.Parameters.Add(New SqlParameter("@Condition", SqlDbType.Int))
                comm.Parameters("@Condition").Value = 1
                comm.Parameters("@Status").Value = "Post"
                comm.Connection = trans.Connection
                comm.Transaction = trans
                comm.ExecuteNonQuery()

            Else





                strSQL = "UPDATE tblGlVoucherTemp Set post = 1 WHERE (voucher_id = " & ObjVoucher.voucherID & ") AND (location_id = " & ObjVoucher.LocationID & ")"

                ''Execute SQL 
                SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                ' ''SQL Statement Log
                ObjVoucher.ActivityLog.SQLType = "UPDATE"
                ObjVoucher.ActivityLog.TableName = "tblGlVoucherTemp"
                ObjVoucher.ActivityLog.SQL = strSQL
                UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)

                'CR#98
                VNo = strVoucherNo
                'replace the voucher no from '1' to '000001'
                VNo = funDoPadding(VNo, ObjVoucher.VNoMaxLength)

                LocCode = GetLocationCode(ObjVoucher.LocationID)
                strVoucherType = GetGLVType(ObjVoucher.VoucherTypeID)
                'Making Voucher Code
                ObjVoucher.VoucherCode = Format(ObjVoucher.VoucherDate, "yyyy") & "-" & strVoucherType & "-" & LocCode & "-" & "ACC" & "-" & Format(ObjVoucher.VoucherDate, "MMM") & "-" & VNo
                ObjVoucher.VoucherNo = VNo

                '--------------------------------25 Nov Changes by AJ................Inserting Voucher into GL Origional Voucher tables
                '   insert a new tuple in Master GLVoucher Table (original)

                If ObjVoucher.BlnSaveVInActualTables Then

                    Dim lngNewVoucherID As Long

                    strVoucherNo = funDoPadding(strVoucherNo, ObjVoucher.VNoMaxLength)
                    '=========================================
                    'Making Voucher Code
                    ObjVoucher.VoucherCode = Format(ObjVoucher.VoucherDate, "yyyy") & "-" & GetGLVType(ObjVoucher.VoucherTypeID) & "-" & GetLocationCode(ObjVoucher.LocationID) & "-" & "ACC" & "-" & Format(ObjVoucher.VoucherDate, "MMM") & "-" & ObjVoucher.VoucherNo

                    Dim strOther_voucher As String

                    strOther_voucher = 0

                    'Explicitly Setting Other voucher =0

                    'strSQL = "INSERT INTO tblGlVoucher (location_id, voucher_code, finiancial_year_id, voucher_type_id, voucher_month, voucher_no, " & _
                    '         " voucher_date, paid_to, coa_detail_id, cheque_no, cheque_date, post, other_voucher,Source, temp_voucher_id) " & _
                    '         " SELECT location_id, '" & ObjVoucher.VoucherCode & "' AS Voucher_Code, finiancial_year_id, voucher_type_id, voucher_month, '" & strVoucherNo & "' AS voucher_no,  " & _
                    '         " voucher_date, paid_to, coa_detail_id, cheque_no, cheque_date, 0, other_voucher,Source, voucher_id " & _
                    '         " FROM tblGLVoucherTemp WHERE voucher_id = " & ObjVoucher.voucherID & " "
                    'strSQL = strSQL & " Select Ident_Current('tblGlVoucher')"


                    'CR#98
                    'strSQL = "INSERT INTO tblGlVoucher (location_id, voucher_code, finiancial_year_id, voucher_type_id, voucher_month, voucher_no, " & _
                    '         " voucher_date, paid_to, coa_detail_id, cheque_no, cheque_date, post, other_voucher,Source, temp_voucher_id) " & _
                    '         " SELECT location_id, '" & ObjVoucher.VoucherCode & "' AS Voucher_Code, finiancial_year_id, voucher_type_id, voucher_month, '" & strVoucherNo & "' AS voucher_no,  " & _
                    '         " voucher_date, paid_to, coa_detail_id, cheque_no, cheque_date, 0, 0,Source, voucher_id " & _
                    '         " FROM tblGLVoucherTemp WHERE voucher_id = " & ObjVoucher.voucherID & " "

                    strSQL = "INSERT INTO tblGlVoucher (location_id, voucher_code, finiancial_year_id, voucher_type_id, voucher_month, voucher_no, " & _
                             " voucher_date, paid_to, coa_detail_id, cheque_no, cheque_date, post, other_voucher,Source, temp_voucher_id) " & _
                             " SELECT location_id, '" & ObjVoucher.VoucherCode & "' AS Voucher_Code, finiancial_year_id, voucher_type_id, voucher_month, '" & strVoucherNo & "' AS voucher_no,  " & _
                             " voucher_date, paid_to, coa_detail_id, cheque_no, cheque_date, 0, 0,Source, voucher_id " & _
                             " FROM tblGLVoucherTemp WHERE voucher_id = " & ObjVoucher.voucherID & " "

                    strSQL = strSQL & " Select Ident_Current('tblGlVoucher')"

                    ''Execute SQL 
                    lngNewVoucherID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                    ' ''SQL Statement Log
                    ObjVoucher.ActivityLog.SQLType = "INSERT"
                    ObjVoucher.ActivityLog.TableName = "tblGlVoucher"
                    ObjVoucher.ActivityLog.SQL = strSQL
                    UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)

                    strSQL = " INSERT INTO tblGlVoucherDetail " & _
                             " (voucher_id, location_id, coa_detail_id, comments,  debit_amount, credit_amount ) " & _
                             " SELECT " & lngNewVoucherID & ", location_id, coa_detail_id, '" & Trim(ObjVoucher.VoucherNarration) & "', SUM(ISNULL(debit_amount,0)) , SUM(ISNULL(credit_amount,0)) " & _
                             " From tblGlVoucherTempDetail " & _
                             " Where voucher_id = " & ObjVoucher.voucherID & " " & _
                             " GROUP BY voucher_id, location_id, coa_detail_id "

                    SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                    ' ''SQL Statement Log
                    ObjVoucher.ActivityLog.SQLType = "INSERT"
                    ObjVoucher.ActivityLog.TableName = "tblGlVoucherDetail"
                    ObjVoucher.ActivityLog.SQL = strSQL
                    UtilityDAL.BuildSQLLog(ObjVoucher.ActivityLog, trans)


                End If

            End If


            ' ''Activity Log
            ObjVoucher.ActivityLog.FormAction = "Post"
            ObjVoucher.ActivityLog.SQL = strSQL
            ObjVoucher.ActivityLog.LogRef = VoucherID
            ObjVoucher.ActivityLog.LogGroup = "Transactions"
            ObjVoucher.ActivityLog.ScreenTitle = "Voucher Posting"
            UtilityDAL.BuildActivityLog(ObjVoucher.ActivityLog, trans)

            ''Commit Traction
            trans.Commit()

            ''Return
            Return True

        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function
    Public Function AlterViewForReport(ByVal VoucherId As Integer, ByVal LocID As Integer, Optional ByVal TempVoucher As Boolean = False) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            If TempVoucher = False Then

                strSQL = "alter view vwGlvouchersingle as " & _
                        "SELECT     dbo.tblGlVoucher.voucher_code, dbo.tblGlVoucher.voucher_type_id, dbo.tblGlVoucher.voucher_month, dbo.tblGlVoucher.voucher_date, " & _
                      "dbo.tblGlVoucherDetail.comments, dbo.tblGlVoucherDetail.debit_amount, dbo.tblGlVoucherDetail.credit_amount, dbo.tblGlVoucher.voucher_no," & _
                      "dbo.tblGlDefFinancialYear.year_code, dbo.tblGlDefVoucherType.voucher_type, dbo.tblGlDefLocation.location_name," & _
                      "dbo.tblGlCOAMainSubSubDetail.detail_code, dbo.tblGlCOAMainSubSubDetail.detail_title, dbo.tblGlVoucher.location_id, " & _
                      "dbo.tblGlVoucher.voucher_id AS voucherid, dbo.tblGlDefGLCostCenter.cost_center_title, dbo.tblGlVoucher.cheque_no, dbo.tblGlVoucher.cheque_date," & _
                      "dbo.tblGlVoucher.cheque_paid , dbo.tblGlVoucher.cheque_paid_date " & _
                       "FROM         dbo.tblGlVoucher INNER JOIN " & _
                      "dbo.tblGlVoucherDetail ON dbo.tblGlVoucher.voucher_id = dbo.tblGlVoucherDetail.voucher_id AND " & _
                      "dbo.tblGlVoucher.location_id = dbo.tblGlVoucherDetail.location_id INNER JOIN " & _
                      "dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN " & _
                      "dbo.tblGlDefVoucherType ON dbo.tblGlVoucher.voucher_type_id = dbo.tblGlDefVoucherType.voucher_type_id INNER JOIN " & _
                      "dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id INNER JOIN " & _
                      "dbo.tblGlCOAMainSubSubDetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.tblGlCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN " & _
                      "dbo.tblGlDefGLCostCenter ON dbo.tblGlVoucherDetail.cost_center_id = dbo.tblGlDefGLCostCenter.cost_center_id " & _
                       "Where (dbo.tblGlVoucher.voucher_id = " & VoucherId & ") And (dbo.tblGlVoucher.location_id = " & LocID & ")  "

            Else

                strSQL = "alter view vwGlvouchersingle as " & _
                        "SELECT     dbo.tblGlVoucherTemp.voucher_code, dbo.tblGlVoucherTemp.voucher_type_id, dbo.tblGlVoucherTemp.voucher_month, dbo.tblGlVoucherTemp.voucher_date, " & _
                      "dbo.tblGlVoucherTempDetail.comments, dbo.tblGlVoucherTempDetail.debit_amount, dbo.tblGlVoucherTempDetail.credit_amount, dbo.tblGlVoucherTemp.voucher_no," & _
                      "dbo.tblGlDefFinancialYear.year_code, dbo.tblGlDefVoucherType.voucher_type, dbo.tblGlDefLocation.location_name," & _
                      "dbo.tblGlCOAMainSubSubDetail.detail_code, dbo.tblGlCOAMainSubSubDetail.detail_title, dbo.tblGlVoucherTemp.location_id, " & _
                      "dbo.tblGlVoucherTemp.voucher_id AS voucherid, dbo.tblGlDefGLCostCenter.cost_center_title, dbo.tblGlVoucherTemp.cheque_no, dbo.tblGlVoucherTemp.cheque_date," & _
                      "dbo.tblGlVoucherTemp.cheque_paid , dbo.tblGlVoucherTemp.cheque_paid_date " & _
                       "FROM         dbo.tblGlVoucherTemp INNER JOIN " & _
                      "dbo.tblGlVoucherTempDetail ON dbo.tblGlVoucherTemp.voucher_id = dbo.tblGlVoucherTempDetail.voucher_id AND " & _
                      "dbo.tblGlVoucherTemp.location_id = dbo.tblGlVoucherTempDetail.location_id INNER JOIN " & _
                      "dbo.tblGlDefFinancialYear ON dbo.tblGlVoucherTemp.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN " & _
                      "dbo.tblGlDefVoucherType ON dbo.tblGlVoucherTemp.voucher_type_id = dbo.tblGlDefVoucherType.voucher_type_id INNER JOIN " & _
                      "dbo.tblGlDefLocation ON dbo.tblGlVoucherTemp.location_id = dbo.tblGlDefLocation.location_id INNER JOIN " & _
                      "dbo.tblGlCOAMainSubSubDetail ON dbo.tblGlVoucherTempDetail.coa_detail_id = dbo.tblGlCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN " & _
                      "dbo.tblGlDefGLCostCenter ON dbo.tblGlVoucherTempDetail.cost_center_id = dbo.tblGlDefGLCostCenter.cost_center_id " & _
                       "Where (dbo.tblGlVoucherTemp.voucher_id = " & VoucherId & ") And (dbo.tblGlVoucherTemp.location_id = " & LocID & ")  "


            End If
            ''Execute SQL 
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

            trans.Commit()

            Return True


        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try

    End Function
    Public Function GetDetailofAccCode(ByVal DtlCode As String) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter

        Try


            Dim strSQL As String


            'Build SQL for selected location combo
            strSQL = "SELECT COA_Detail_id [COA Dtl ID],detail_title [Dtl Title], detail_code  [Dtl Code] From tblGlCOAMainSubSubDetail  WHERE (detail_code = '" & Trim(DtlCode) & "')"
            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("AccDtl")
            objDA.Fill(MyCollectionList)

            Return MyCollectionList


        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function
    Public Function IsVoucherBalanced(ByVal VoucherId As Integer, ByVal LocId As Integer, ByVal IsTempVoucher As Boolean) As Boolean
        Dim strSQL As String
        Dim DrAmount As Double
        Dim CrAmount As Double

        Try

            If IsTempVoucher = False Then
                strSQL = "SELECT Sum(Debit_Amount) as Debit,sum(Credit_amount)  as Credit " _
                                     & " FROM tblGlVoucherDetail WHERE (tblGlVoucherDetail.voucher_id = " & VoucherId & ") " _
                                     & " AND (tblGlVoucherDetail.location_id = " & LocId & ")"

            Else

                strSQL = "SELECT Sum(Debit_Amount) as Debit,sum(Credit_amount)  as Credit " _
                     & " FROM tblGlVoucherTempDetail WHERE (tblGlVoucherTempDetail.voucher_id = " & VoucherId & ") " _
                     & " AND (tblGlVoucherTempDetail.location_id = " & LocId & ")"

            End If

            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then

                    While objDR.Read
                        DrAmount = CDbl(objDR.Item("Debit"))
                        CrAmount = CDbl(objDR.Item("Credit"))
                    End While

                End If

            End Using

            If DrAmount <> CrAmount Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function
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
    Public Function GetNewVNoOfActualGLTable(ByVal LocationId As Long, ByVal VoucherTypeId As Long, _
       ByVal FMonth As String, ByVal FYear As String, ByVal blnOtherVoucher As Integer, Optional ByVal TempVoucher As Boolean = False) As String

        Dim strSQL As String
        Dim NewVNo As String = ""

        Try


            'CR#102
            'strSQL = " SELECT isNULL(Max(voucher_no) + 1,  " & IIf(blnOtherVoucher = -1, 500001, 1) & ") as NewVoucherNo From dbo.tblGlVoucher" & _
            '             " Where (location_id = " & LocationId & ") And (voucher_type_id = " & VoucherTypeId & ")" & _
            '             " And (voucher_month = '" & FMonth & "') And (DATENAME(Year, voucher_date) = '" & FYear & "') And (LEFT(DATENAME(month, voucher_date),3) = '" & FMonth & "') and other_voucher= " & IIf(blnOtherVoucher = -1, 1, 0) & IIf(blnOtherVoucher = -1, " and voucher_no > '500000'", " and voucher_no < '500000'")
            If TempVoucher = True Then
                strSQL = "SELECT isNULL(Max(voucher_no) + 1,1) as NewVoucherNo From dbo.tblGlVoucher" & _
                                         " Where (location_id = " & LocationId & ") And (voucher_type_id = " & VoucherTypeId & ")" & _
                                         " And (voucher_month = '" & FMonth & "') And (DATENAME(Year, voucher_date) = '" & FYear & "') And (LEFT(DATENAME(month, voucher_date),3) = '" & FMonth & "') and other_voucher= 0  and voucher_no < '500000'"

            Else

                If gblnShowOtherVoucher = True Then

                    strSQL = "SELECT isNULL(Max(voucher_no) + 1,  " & IIf(blnOtherVoucher = -1, 500001, 1) & ") as NewVoucherNo From dbo.tblGlVoucher" & _
                                         " Where (location_id = " & LocationId & ") And (voucher_type_id = " & VoucherTypeId & ")" & _
                                         " And (voucher_month = '" & FMonth & "') And (DATENAME(Year, voucher_date) = '" & FYear & "') And (LEFT(DATENAME(month, voucher_date),3) = '" & FMonth & "') and other_voucher= " & IIf(blnOtherVoucher = -1, 1, 0) & IIf(blnOtherVoucher = -1, " and voucher_no > '500000'", " and voucher_no < '500000'")

                Else

                    strSQL = "SELECT isNULL(Max(voucher_no) + 1,1) as NewVoucherNo From dbo.tblGlVoucher " & _
                             " Where(location_id = " & LocationId & ") And (voucher_type_id = " & VoucherTypeId & ") " & _
                             " And (voucher_month = '" & FMonth & "') And (DATENAME(Year, voucher_date) = '" & FYear & "') " & _
                             " And (LEFT(DATENAME(month, voucher_date),3) = '" & FMonth & "') and " & _
                             " other_voucher= 0  and voucher_no < 500000"


                End If


            End If



            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read

                        NewVNo = objDR.Item("NewVoucherNo").ToString

                    End While
                End If

            End Using

            Return NewVNo

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function TrialPeriodExpired(ByVal FYearID As Integer, ByVal MaxTrans As Integer) As Boolean

        Dim strSQL As String
        Dim intTotalTrans As Integer

        Try

            intTotalTrans = 0
            strSQL = "SELECT COUNT(*) AS TransCount FROM tblGLVoucher where finiancial_year_id=" & FYearID & ""

            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read

                        intTotalTrans = Convert.ToInt32(objDR.Item("TransCount").ToString)

                    End While
                End If

            End Using

            If intTotalTrans > MaxTrans Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAccountType(ByVal AccId As Integer) As Boolean
        Dim STR_AccTYPE As String = String.Empty
        Dim StrSQL As String = String.Empty


        Try

            StrSQL = "SELECT  ISNULL(dbo.tblGlCOAMainSubSub.account_type,'')  as account_type " & _
                    " FROM    dbo.tblGlCOAMainSubSub INNER JOIN   dbo.tblGlCOAMainSubSubDetail ON " & _
                    " dbo.tblGlCOAMainSubSub.main_sub_sub_id = dbo.tblGlCOAMainSubSubDetail.main_sub_sub_id " & _
                    " where dbo.tblGlCOAMainSubSubDetail.coa_detail_id = " & AccId & ""

            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, StrSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read

                        STR_AccTYPE = objDR.Item("account_type").ToString

                    End While
                End If

            End Using

            If UCase(STR_AccTYPE.Trim) = "BANK" Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetAccountTypeName(ByVal AccId As Integer) As Boolean
        Dim STR_AccTYPE As String = String.Empty
        Dim StrSQL As String = String.Empty


        Try

            StrSQL = "SELECT  ISNULL(dbo.tblGlCOAMainSubSub.account_type,'')  as account_type " & _
                    " FROM    dbo.tblGlCOAMainSubSub INNER JOIN   dbo.tblGlCOAMainSubSubDetail ON " & _
                    " dbo.tblGlCOAMainSubSub.main_sub_sub_id = dbo.tblGlCOAMainSubSubDetail.main_sub_sub_id " & _
                    " where dbo.tblGlCOAMainSubSubDetail.coa_detail_id = " & AccId & ""

            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, StrSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read

                        STR_AccTYPE = objDR.Item("account_type").ToString

                    End While
                End If

            End Using

            If UCase(STR_AccTYPE.Trim) = "CUSTOMER" Or UCase(STR_AccTYPE.Trim) = "VENDOR" Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function


    'Cr # 245
    Public Sub AddVoucherHistory(ByVal ObjVoucher As GLVoucher, ByVal trans As SqlTransaction)
        Try
            Dim id As Integer
            Dim strSQL As String = String.Empty
            strSQL = "insert into tblGlVoucherHistory (voucher_id ,  location_id ,voucher_code,finiancial_year_id, " _
                    & " voucher_type_id,voucher_month,voucher_no,voucher_date,paid_to, " _
                    & " coa_detail_id, cheque_no, cheque_date, cheque_paid, cheque_paid_date, " _
                    & " post, other_voucher, source, cheque_credited, temp_voucher_id, " _
                    & " due_date,shop_id,shop_code,Action,Action_date) " _
                    & "Select voucher_id ,  location_id ,voucher_code,finiancial_year_id, " _
                    & " voucher_type_id,voucher_month,voucher_no,voucher_date,paid_to, " _
                    & " coa_detail_id, cheque_no, cheque_date, cheque_paid, cheque_paid_date, " _
                    & " post, other_voucher, source, cheque_credited, temp_voucher_id, " _
                    & " due_date,shop_id,shop_code,'Delete',getdate()  from tblGlVoucher Where (voucher_id = " & ObjVoucher.voucherID & ") And (location_id = " & ObjVoucher.LocationID & ") and shop_id <= 0  select ident_current('tblGlVoucherHistory')"
            id = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

            strSQL = "insert into tblGLVoucherDetailHistory(id,voucher_detail_id,voucher_id,location_id,coa_detail_id,comments,debit_amount, " _
            & " credit_amount,cost_center_id,sp_refrence,direction,shop_id,Action,Action_date)  " _
            & " Select  " & id & ",voucher_detail_id,voucher_id,location_id,coa_detail_id,comments,debit_amount, " _
            & " credit_amount,cost_center_id,sp_refrence,direction,shop_id,'Delete',getdate() from tblGLVoucherDetail Where (voucher_id = " & ObjVoucher.voucherID & ") And (location_id = " & ObjVoucher.LocationID & ") and shop_id <= 0"

            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    '************ Start of CR # 268 Rana Saeed
    Public Function ViewInvFor_Sales_Purchase(ByVal VoucherId As Integer, ByVal LocID As Integer, Optional ByVal TempVoucher As Boolean = False) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String
            strSQL = "drop view vwGLInv_PO_Sales "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

            strSQL = "create view vwGLInv_PO_Sales as "
            If TempVoucher = False Then
                strSQL = strSQL & "SELECT     dbo.tblGlVoucher.voucher_code, dbo.tblGlVoucher.voucher_type_id, dbo.tblGlVoucher.voucher_month, dbo.tblGlVoucher.voucher_date, " & _
                      "dbo.tblGlVoucherDetail.comments, dbo.tblGlVoucherDetail.debit_amount, dbo.tblGlVoucherDetail.credit_amount, dbo.tblGlVoucher.voucher_no," & _
                      "dbo.tblGlDefFinancialYear.year_code, dbo.tblGlDefVoucherType.voucher_type, dbo.tblGlDefLocation.location_name," & _
                      "dbo.tblGlCOAMainSubSubDetail.detail_code, dbo.tblGlCOAMainSubSubDetail.detail_title, dbo.tblGlVoucher.location_id, " & _
                      "dbo.tblGlVoucher.voucher_id AS voucherid, dbo.tblGlDefGLCostCenter.cost_center_title, dbo.tblGlVoucher.cheque_no, dbo.tblGlVoucher.cheque_date," & _
                      "dbo.tblGlVoucher.cheque_paid , dbo.tblGlVoucher.cheque_paid_date, tblGLContactDirectory.Address " & _
                       "FROM         dbo.tblGlVoucher INNER JOIN " & _
                      "dbo.tblGlVoucherDetail ON dbo.tblGlVoucher.voucher_id = dbo.tblGlVoucherDetail.voucher_id AND " & _
                      "dbo.tblGlVoucher.location_id = dbo.tblGlVoucherDetail.location_id INNER JOIN " & _
                      "dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN " & _
                      "dbo.tblGlDefVoucherType ON dbo.tblGlVoucher.voucher_type_id = dbo.tblGlDefVoucherType.voucher_type_id INNER JOIN " & _
                      "dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id INNER JOIN " & _
                      "dbo.tblGlCOAMainSubSubDetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.tblGlCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN " & _
                      "dbo.tblGlDefGLCostCenter ON dbo.tblGlVoucherDetail.cost_center_id = dbo.tblGlDefGLCostCenter.cost_center_id " & _
                      "LEFT OUTER JOIN tblGLContactDirectory ON tblGlVoucherDetail.coa_detail_id = tblGLContactDirectory.Account_id " & _
                       "Where (dbo.tblGlVoucher.voucher_id = " & VoucherId & ") And (dbo.tblGlVoucher.location_id = " & LocID & ")  "
            Else
                strSQL = strSQL & "SELECT     dbo.tblGlVoucherTemp.voucher_code, dbo.tblGlVoucherTemp.voucher_type_id, dbo.tblGlVoucherTemp.voucher_month, dbo.tblGlVoucherTemp.voucher_date, " & _
                      "dbo.tblGlVoucherTempDetail.comments, dbo.tblGlVoucherTempDetail.debit_amount, dbo.tblGlVoucherTempDetail.credit_amount, dbo.tblGlVoucherTemp.voucher_no," & _
                      "dbo.tblGlDefFinancialYear.year_code, dbo.tblGlDefVoucherType.voucher_type, dbo.tblGlDefLocation.location_name," & _
                      "dbo.tblGlCOAMainSubSubDetail.detail_code, dbo.tblGlCOAMainSubSubDetail.detail_title, dbo.tblGlVoucherTemp.location_id, " & _
                      "dbo.tblGlVoucherTemp.voucher_id AS voucherid, dbo.tblGlDefGLCostCenter.cost_center_title, dbo.tblGlVoucherTemp.cheque_no, dbo.tblGlVoucherTemp.cheque_date," & _
                      "dbo.tblGlVoucherTemp.cheque_paid , dbo.tblGlVoucherTemp.cheque_paid_date, tblGLContactDirectory.Address " & _
                       "FROM         dbo.tblGlVoucherTemp INNER JOIN " & _
                      "dbo.tblGlVoucherTempDetail ON dbo.tblGlVoucherTemp.voucher_id = dbo.tblGlVoucherTempDetail.voucher_id AND " & _
                      "dbo.tblGlVoucherTemp.location_id = dbo.tblGlVoucherTempDetail.location_id INNER JOIN " & _
                      "dbo.tblGlDefFinancialYear ON dbo.tblGlVoucherTemp.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN " & _
                      "dbo.tblGlDefVoucherType ON dbo.tblGlVoucherTemp.voucher_type_id = dbo.tblGlDefVoucherType.voucher_type_id INNER JOIN " & _
                      "dbo.tblGlDefLocation ON dbo.tblGlVoucherTemp.location_id = dbo.tblGlDefLocation.location_id INNER JOIN " & _
                      "dbo.tblGlCOAMainSubSubDetail ON dbo.tblGlVoucherTempDetail.coa_detail_id = dbo.tblGlCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN " & _
                      "dbo.tblGlDefGLCostCenter ON dbo.tblGlVoucherTempDetail.cost_center_id = dbo.tblGlDefGLCostCenter.cost_center_id " & _
                      "LEFT OUTER JOIN tblGLContactDirectory ON tblGlVoucherTempDetail.coa_detail_id = tblGLContactDirectory.Account_id " & _
                       "Where (dbo.tblGlVoucherTemp.voucher_id = " & VoucherId & ") And (dbo.tblGlVoucherTemp.location_id = " & LocID & ")  "
            End If
            ''Execute SQL 
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

            trans.Commit()
            Return True


        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    '********** End of CR # 268 Rana Saeed
#End Region

    ''// 323     23 Jul,2014          farooq-H  
    Public Sub ViewPrintBankChecks(ByVal Name As String, ByVal CheckDate As Date, ByVal CheckAmount As Double)
        Try
            Dim strSQL As String
            strSQL = "select * from dbo.sysobjects where id = object_id(N'[dbo].[tblChequeDetailForPrint]') and OBJECTPROPERTY(id, N'IsUserTable') = 1"

            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    strSQL = ""
                    strSQL = " DELETE FROM tblChequeDetailForPrint "
                    SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)
                Else

                    strSQL = ""
                    strSQL = " CREATE TABLE [dbo].[tblChequeDetailForPrint] ("
                    strSQL = strSQL + "     [Name] [nvarchar] (500) NULL ,"
                    strSQL = strSQL + "     [CheckDate] [datetime] NULL ,"
                    strSQL = strSQL + "     [CheckAmount] [float] NULL"
                    strSQL = strSQL + "      ) ON [PRIMARY]"

                    SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                End If

            End Using

            strSQL = ""
            strSQL = "insert into tblChequeDetailForPrint ( Name ,CheckDate ,CheckAmount ) values( '" & Name.ToString.Replace("'", "''") & "' , '" & CheckDate & "' , " & CheckAmount & "  )  "
            SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''// 323     23 Jul,2014          farooq-H  
    Public Function GetAccountChequeType(ByVal AccId As Integer) As DataTable
        Dim StrSQL As String = String.Empty
        Dim objDA As SqlClient.SqlDataAdapter

        Try

            StrSQL = " SELECT     tblGlCOAMainSubSubDetail.cheque_id, tblChequeTemplates.Cheque_name, tblChequeTemplates.Cheque_Report, tblChequeTemplates.Cheque_template " & _
                     " FROM         tblGlCOAMainSubSub INNER JOIN  " & _
                     " tblGlCOAMainSubSubDetail ON tblGlCOAMainSubSub.main_sub_sub_id = tblGlCOAMainSubSubDetail.main_sub_sub_id INNER JOIN  " & _
                     " tblChequeTemplates ON tblGlCOAMainSubSubDetail.cheque_id = tblChequeTemplates.Cheque_ID   " & _
                     " where dbo.tblGlCOAMainSubSubDetail.coa_detail_id = " & AccId & ""

            objDA = New SqlClient.SqlDataAdapter(StrSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("Voucher")
            objDA.Fill(MyCollectionList)

            Return MyCollectionList

        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class


