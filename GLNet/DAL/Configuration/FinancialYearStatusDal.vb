Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''//'121 Ledger doesn't macthes with Trial Balance Report after Year Closing
'' 22-Oct-2014      M.Shoaib        CR # 332  Data log: Datalog should prepare properly for all configuration of Lightwave
'' 17-Dec-2014      Abdul Jabbar    CR#347   Balance Sheet: Balance sheet is not appropriate during filteration on source 
''/////////////////////////////////////////////////////////////////////////////////////////
Public Class FinancialYearStatusDal


    Public Const G_STATUS_CLOSE As String = "Closed"                        'Use this variable to set status to Open


#Region "Local Functions and Procedures"

    Private Function funGetNextFiniancialYear(ByVal lngCurrentFinancialYearId As Long, ByVal CompanyId As Integer, ByVal trans As SqlTransaction) As DataRow

        Dim strSql As String

        Dim strNextFiscalYear As String
        Dim Dtable As DataTable

        'Check that new fiscal year already exists in data base
        strSql = "select y.year_code,y.financial_year_id,s.status  from tblGLDefFinancialYear y INNER JOIN tblGLDefFinancialYearStatus S ON y.financial_year_id = S.financial_year_id Where (y.financial_year_id=" & lngCurrentFinancialYearId & " AND S.Location_id = " & CompanyId & ")"

        Dtable = UtilityDAL.GetDataTable(strSql, trans).Copy

        If Dtable.Rows.Count > 0 Then

            'Make next fiscal year
            strNextFiscalYear = (Left(Dtable.Rows(0).Item("year_code"), 4) + 1) & "-" & (Right(Dtable.Rows(0).Item("year_code"), 4) + 1)

            'Check that new fiscal year already exists in data base
            strSql = "select y.year_code,y.financial_year_id,s.status  from tblGLDefFinancialYear y INNER JOIN tblGLDefFinancialYearStatus S ON y.financial_year_id = S.financial_year_id Where (y.year_code='" & strNextFiscalYear & "' AND S.Location_id = " & CompanyId & " )"
            Return UtilityDAL.ReturnDataRow(strSql, trans)

        End If

    End Function


    Private Function funDeleteZerothVoucher(ByVal lngNextFiscalYearId As Long, ByVal strNextFiscalYear As String, ByVal iLocationID As Integer, ByVal trans As SqlTransaction)

        Dim strSql As String
        Dim lngVoucherID As Long
        Dim recVoucherID As DataTable
        Try

            If lngNextFiscalYearId > 0 Then

                'If zeeroth voucher for the next year exists then first delete it
                strSql = "select voucher_id from tblGLVoucher where finiancial_year_id = " & lngNextFiscalYearId & " And Location_ID = " & iLocationID & "  And voucher_no = '" & G_VOUCHER_ZERO & "'"
                recVoucherID = UtilityDAL.GetDataTable(strSql)

                If recVoucherID.Rows.Count > 0 Then

                    lngVoucherID = recVoucherID.Rows(0).Item("voucher_id")

                    'Delete ZEEROTH voucher from detail table
                    strSql = "Delete from tblGLVoucherDetail where voucher_id = " & lngVoucherID
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, Nothing)

                    'Delete ZEEROTH voucher from master table
                    strSql = "Delete from tblGLVoucher where voucher_id = " & lngVoucherID
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, Nothing)

                    'Update the Status = Open from Financial Year Table
                    strSql = "Update tblGlDefFinancialYearStatus Set Status = '" & G_OPEN_FYEAR & "' Where financial_year_id = " & lngNextFiscalYearId & " AND Location_id = " & iLocationID
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, Nothing)

                End If

            End If
            Return True

        Catch ex As Exception

            Throw ex
            Return False
        End Try


    End Function

#End Region

#Region "Public Functions and Procedures"

    Public Function Update(ByVal objFyear As FiniancialYearStatus) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        'start a new transaction
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            Dim lngRecID As Long
            Dim lngNextFiscalYearId As Long


            'build sql to check if new field name or code already exists
            strSQL = "select year_code from tblGLDefFinancialYear Y INNER JOIN tblGLDefFinancialYearStatus S ON y.Financial_year_id = S.Financial_year_id   Where (Y.year_code='" & objFyear.YearCode & "' AND S.Status  = '" & objFyear.Status & "' AND Y.financial_year_id <> " & Val(objFyear.FYearID) & " AND S.Location_ID = " & objFyear.LocationID & " )"
            'UtilityDAL.ReturnDataRow(strSQL)

            'check if no record found in previous query
            If Not UtilityDAL.GetDataTable(strSQL).Rows.Count > 0 Then


                'Get year codes from the Financial year Table whome keeps status = 'Open'
                'Dim recFindCurrentRecords As ADODB.RecordSet
                strSQL = "Select Year_Code from tblGlDefFinancialYear y INNER JOIN tblGLDefFinancialYearStatus S ON y.Financial_year_id = S.Financial_year_id  Where (y.financial_year_id < " & Val(objFyear.FYearID) & ") and (s.Status  = '" & G_OPEN_FYEAR & "' ) AND (S.Location_ID = " & objFyear.LocationID & ") "
                'recFindCurrentRecords = m_ObjExecuteSql.GetRecordset(strSQL)

                'if fyears are available with Status = Open
                If UtilityDAL.GetDataTable(strSQL).Rows.Count > 0 Then

                    Throw New Exception("You must close the previous open Financial Year's") ', vbOKOnly + vbExclamation, gMsgTitle)
                    Exit Function
                End If

                'Dim rs As New ADODB.RecordSet
                strSQL = "select post, * from tblGLvoucher where post=0 and finiancial_year_id=" & objFyear.FYearID & " AND Location_ID = " & objFyear.LocationID
                If UtilityDAL.GetDataTable(strSQL).Rows.Count > 0 Then

                    Throw New Exception("Some vouchers are unposted, Financial year can't b closed") ', vbOKOnly + vbExclamation, gMsgTitle)
                    Exit Function
                End If




                'Get the PK_ID of grid. selected finiancial year id
                lngRecID = objFyear.FYearID

                'build a sql for update of selected tuple
                'strSql = "Update tblGLDefFinancialYear Set start_date ='" & StartDate.Value & "', end_date ='" & EndDate.Value & "', status = '" & cboStatus.Text & "' , year_code ='" & lblfYear.Caption & "' where financial_year_id = " & Me.grdFinancialYear.Columns(GridCol.financial_year_id).Value

                '   25-Jan-2006     Abdul Mannan
                'build a sql for update of selected tuple
                strSQL = "Update tblGLDefFinancialYearStatus Set status = '" & objFyear.Status & "'  where financial_year_id = " & objFyear.FYearID & " AND Location_ID = " & objFyear.LocationID

                'Prompt user for update
                'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                ''Execute SQL 
                SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

                ''SQL Statement Log
                objFyear.ActivityLog.SQLType = "INSERT"
                objFyear.ActivityLog.TableName = "tblGLDefFinancialYearStatus"
                objFyear.ActivityLog.SQL = strSQL
                UtilityDAL.BuildSQLLog(objFyear.ActivityLog, trans, True)

                'If m_ObjExecuteSql.ProUpdateRecord(strSQL) Then

                'set the pointer of recordset to the updated tuple
                'Call proUpdateFormResponseWithXarray(Me, recDefFinancialYear, lngTotalRecords, lngRecID, grdFinancialYear, Xarr)
                'Call proEnableDisableMdiNavigation(Me, lngRecNo, lngTotalRecords)
                'TODO: R@! Insert a new tuple of Activity Log
                ' m_ObjExecuteSql.proEntryIntoActivityLog(Me.Caption, "Update", "", "", "Definition", pbUserId)


                'End If

                ''''''''''''''''''''''''''''''''''''''

                ''''''''''''
                Dim intGridPointer As Integer
                Dim intCounter As Integer
                Dim blnOpenFoundFlag As Boolean
                Dim recFYear As DataRow
                Dim strNextFiscalYear As String
                ''''''''''''




                'Get next financial year code and id
                recFYear = funGetNextFiniancialYear(objFyear.FYearID, objFyear.LocationID, trans)

                'If Not recFYear Then

                lngNextFiscalYearId = recFYear.Item("financial_year_id")
                strNextFiscalYear = recFYear.Item("year_code")

                'Delete zeroth voucher and insert new voucher if no current voucher exists
                Call funDeleteZerothVoucher(lngNextFiscalYearId, strNextFiscalYear, objFyear.LocationID, trans)

                'Exit if Open financial year is encountered
                If objFyear.OldStatus.Trim = G_OPEN_FYEAR Then

                    If objFyear.Status.Trim = G_STATUS_CLOSE Then

                        'Save Opening for next year
                        If Not funSaveClosing(lngNextFiscalYearId, strNextFiscalYear, objFyear, trans) Then
                            trans.Rollback()                 'Cancel the Transaction
                        End If


                    End If

                End If
                ' Else

                'strNextFiscalYear = (Left(grdFinancialYear.Columns(GridCol.Year_Code), 4)) + 1 & "-" & Val(Right(grdFinancialYear.Columns(GridCol.Year_Code), 4) + 1)

                'New fiscal year not exists then Add new fiscal year
                'strSql = "Insert into tblGLDefFinancialYear(start_date, end_date, year_code, status )  Values('" & DateAdd("yyyy", 1, StartDate.Value) & "', '" & DateAdd("yyyy", 1, EndDate.Value) & "','" & strNextFiscalYear & "','" & G_OPEN_FYEAR & "' ) "

                '   25-Jan-2006     Abdul Mannan
                'strSql = "Insert into tblGLDefFinancialYear(start_date, end_date, year_code )  Values('" & DateAdd("yyyy", 1, StartDate.Value) & "', '" & DateAdd("yyyy", 1, EndDate.Value) & "','" & strNextFiscalYear & "' ) "

                'm_ObjExecuteSql.ExecuteSQL (strSql)

                'lngNextFiscalYearId = funGetLatestID("tblGLDefFinancialYear", "financial_year_id")

                'set the pointer of recordset
                'Call proSaveFormResponseWithXarray(Me, recDefFinancialYear, lngTotalRecords, lngRecNo, "financial_year_id", "tblGLDefFinancialYear", grdFinancialYear, Xarr)

                'End If          '' recFYear Record set end


                'If objFyear.Status = G_STATUS_CLOSE Then

                '    'Save Opening for next year
                '    If Not funSaveClosing(lngNextFiscalYearId, strNextFiscalYear) Then
                '        trans.Rollback()                 'Cancel the Transaction
                '    End If

                'End If


                'Commit transaction
                ' trans.Commit()

            End If

            'CR#160

            ' ''Execute SQL 
            'objFyear.FYearID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

            ' ''SQL Statement Log
            'objFyear.ActivityLog.SQLType = "INSERT"
            'objFyear.ActivityLog.TableName = "tblGlDefFinancialYear "
            'objFyear.ActivityLog.SQL = strSQL
            'UtilityDAL.BuildSQLLog(objFyear.ActivityLog, trans)

            ''Activity Log
            objFyear.ActivityLog.FormAction = "Save"
            UtilityDAL.BuildActivityLog(objFyear.ActivityLog, trans)

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

    'This function is used to save opening for next year
    Private Function funSaveClosing(ByVal lngNextFiscalYearId As Long, ByVal strNextFinancialYear As String, ByVal Fyear As FiniancialYearStatus, ByVal trans As SqlTransaction) As Boolean


        Dim strSql As String
        Dim recBalance As DataTable
        Dim strVoucherCode As String
        Dim lngVoucgerID As Long

        Dim dblDebit As Double
        Dim dblCredit As Double
        Dim dblBalance As Double

        Dim lngSelectedFiscalYearId As Long
        Dim strSelectedFiscalYearCode As String
        Dim strLocation_Code As String


        lngSelectedFiscalYearId = Fyear.FYearID
        strSelectedFiscalYearCode = Fyear.YearCode

        ''2005-JV-LS001-May-000073
        strNextFinancialYear = CStr(Right(strNextFinancialYear, 4))
        strLocation_Code = UtilityDAL.ReturnDataRow("SELECT Location_Code FROM tblGLDefLocation WHERE Location_ID = " & Fyear.LocationID & " ").Item(0).ToString
        strVoucherCode = strNextFinancialYear & "-" & "JV" & "-" & strLocation_Code & "-" & "ACC" & "-" & Format(Date.Today.Date, "MMM") & "-" & G_VOUCHER_ZERO

        'CR#121
        'strSql = "Insert Into tblGlVoucher (voucher_code,         finiancial_year_id,      voucher_type_id,       location_id,          voucher_month,                          voucher_no,                 voucher_date,    Post) " & _
        '                    " Values  ('" & strVoucherCode & "', " & lngNextFiscalYearId & ", " & G_VOUCHER_TYPE_JV & ", " & Fyear.LocationID & ", '" & Format(Fyear.EndDate, "MMM") & "', '" & G_VOUCHER_ZERO & "' ,'" & Fyear.EndDate & "' ,  1)" & _
        '                    " Select Ident_Current('tblGlVoucher')"

        'strSql = "Insert Into tblGlVoucher (voucher_code,         finiancial_year_id,      voucher_type_id,       location_id,          voucher_month,                          voucher_no,                 voucher_date,    Post,other_voucher) " & _
        '                      " Values  ('" & strVoucherCode & "', " & lngNextFiscalYearId & ", " & G_VOUCHER_TYPE_JV & ", " & Fyear.LocationID & ", '" & Format(Fyear.EndDate, "MMM") & "', '" & G_VOUCHER_ZERO & "' ,'" & Fyear.EndDate & "' ,  1,0)" & _
        '                      " Select Ident_Current('tblGlVoucher')"

        'CR#347 Source is not saving ,source must be saved as Accounts 
        'strSql = "Insert Into tblGlVoucher (voucher_code,         finiancial_year_id,      voucher_type_id,       location_id,          voucher_month,                          voucher_no,                 voucher_date,    Post,other_voucher) " & _
        '                      " Values  ('" & strVoucherCode & "', " & lngNextFiscalYearId & ", " & G_VOUCHER_TYPE_JV & ", " & Fyear.LocationID & ", '" & Format(Fyear.EndDate, "MMM") & "', '" & G_VOUCHER_ZERO & "' ,convert(datetime,'" & Format(Fyear.EndDate, "dd-MMM-yyyy hh:m tt") & "',108),  1,0)" & _
        '                      " Select Ident_Current('tblGlVoucher')"

        strSql = "Insert Into tblGlVoucher (voucher_code,         finiancial_year_id,      voucher_type_id,       location_id,          voucher_month,                          voucher_no,                 voucher_date,    Post,other_voucher,Source) " & _
                              " Values  ('" & strVoucherCode & "', " & lngNextFiscalYearId & ", " & G_VOUCHER_TYPE_JV & ", " & Fyear.LocationID & ", '" & Format(Fyear.EndDate, "MMM") & "', '" & G_VOUCHER_ZERO & "' ,convert(datetime,'" & Format(Fyear.EndDate, "dd-MMM-yyyy hh:m tt") & "',108),  1,0,'Accounts')" & _
                              " Select Ident_Current('tblGlVoucher')"

        '"convert(datetime,'" & Format(ObjVoucher.VoucherDate, "dd-MMM-yyyy hh:m tt") & "',108) "


        lngVoucgerID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSql, Nothing)

        'Get lastest voucher id
        '//lngVoucgerID = funGetLatestID("tblGlVoucher", "voucher_id")
        ''''''''''''''''''''''''''''''''''''

        strSql = " SELECT vwGlCOADetail.detail_title, vwGlCOADetail.main_code, vwGlCOADetail.DrBS_Note_Title, SUM(tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(tblGlVoucherDetail.credit_amount) AS credit_amount, SUM(tblGlVoucherDetail.debit_amount) - SUM(tblGlVoucherDetail.credit_amount) AS ClosingAmount, tblGlDefFinancialYear.year_code , vwGlCOADetail.coa_detail_id" & _
                 " FROM tblGlVoucherDetail INNER JOIN vwGlCOADetail ON tblGlVoucherDetail.coa_detail_id = vwGlCOADetail.coa_detail_id INNER JOIN tblGlVoucher ON tblGlVoucherDetail.voucher_id = tblGlVoucher.voucher_id AND tblGlVoucherDetail.location_id = tblGlVoucher.location_id INNER JOIN tblGlDefFinancialYear ON tblGlVoucher.finiancial_year_id = tblGlDefFinancialYear.financial_year_id" & _
                 " WHERE (tblGlDefFinancialYear.year_code = '" & strSelectedFiscalYearCode & "') AND (tblGlVoucherDetail.Location_ID = " & Fyear.LocationID & " ) AND (RTRIM(vwGlCOADetail.main_type) IN ('Assets','Capital','Liability') ) AND (tblGlVoucher.post = 1) AND (tblGlVoucher.Other_voucher = 0) " & _
                 " GROUP BY vwGlCOADetail.main_code, vwGlCOADetail.main_title, vwGlCOADetail.DrBS_Note_Title, tblGlDefFinancialYear.year_code, vwGlCOADetail.DrBS_note_id , vwGlCOADetail.note_type, vwGlCOADetail.detail_title, tblGlVoucher.post , vwGlCOADetail.coa_detail_id" & _
                 " HAVING (SUM(tblGlVoucherDetail.debit_amount) - SUM(tblGlVoucherDetail.credit_amount) <> 0) " & _
                 " ORDER BY vwGlCOADetail.coa_detail_id"

        'Execute query, get recordset containing all accounts
        recBalance = UtilityDAL.GetDataTable(strSql, trans)

        If recBalance.Rows.Count > 0 Then

            Dim i As Integer
            i = 0
            'Traverse all accounts for voucher detail entry
            For Each row As DataRow In recBalance.Rows

                i = i + 1

                dblDebit = row.Item("debit_amount")
                dblCredit = row.Item("credit_amount")
                dblBalance = dblDebit - dblCredit

                '                        strSql = "Insert Into TblGlVoucherDetail (voucher_id ,      location_id ,       coa_detail_id ,                               comments  ,             debit_amount   ,    credit_amount ) " & _
                '                                                    " Values (" & lngVoucgerID & " ," & pbLocationID & "," & recBalance.Fields("coa_detail_id") & ", 'Initialization Voucher', " & dblDebit & ", " & dblCredit & ")"

                'Insert in to voucher detail table.
                If dblBalance < 0 Then

                    Dim a As Integer = 1
                End If

                '   21-Jul-2006     Abdul Mannan
                '                   Selected combo location will be inserted in the query
                If dblBalance > 0 Then


                    'If balance is greater then zero then insert in Credit amount
                    strSql = "Insert Into TblGlVoucherDetail (voucher_id ,      location_id ,       coa_detail_id ,                               comments  ,             debit_amount      , credit_amount ) " & _
                                            " Values (" & lngVoucgerID & " ," & Fyear.LocationID & "," & row.Item("coa_detail_id") & ", 'Initialization Voucher',    " & dblBalance & ", 0             )"

                Else


                    'If balance is greater then zero then insert in Credit amount
                    strSql = "Insert Into TblGlVoucherDetail (voucher_id ,      location_id ,       coa_detail_id ,                               comments  ,             debit_amount   ,    credit_amount ) " & _
                                            " Values (" & lngVoucgerID & " ," & Fyear.LocationID & "," & row.Item("coa_detail_id") & ", 'Initialization Voucher',     0            ,    " & dblBalance * -1 & ")"
                End If

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, Nothing)

            Next

        End If


        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''   Profit and Loss Statement


        Dim recProfitLoss As DataTable
        Dim dblBalanceAmount As Long


        strSql = " SELECT IsNull(SUM(tblGlVoucherDetail.credit_amount),0) - Isnull(SUM(tblGlVoucherDetail.debit_amount),0) AS balance_amount" & _
                " FROM tblGlVoucherDetail INNER JOIN vwGlCOADetail ON tblGlVoucherDetail.coa_detail_id = vwGlCOADetail.coa_detail_id INNER JOIN tblGlVoucher ON tblGlVoucherDetail.voucher_id = tblGlVoucher.voucher_id AND tblGlVoucherDetail.location_id = tblGlVoucher.location_id INNER JOIN" & _
                " tblGlDefFinancialYear ON tblGlVoucher.finiancial_year_id = tblGlDefFinancialYear.financial_year_id" & _
                " WHERE (RTRIM(vwGlCOADetail.main_type) IN ('Expense','Income') ) AND (tblGlDefFinancialYear.year_code = '" & strSelectedFiscalYearCode & "') AND (tblGlVoucher.Location_ID = " & Fyear.LocationID & ") AND (tblGlVoucher.Post = 1) AND (tblGlVoucher.Other_voucher = 0)"



        recProfitLoss = UtilityDAL.GetDataTable(strSql, trans)

        If recProfitLoss.Rows.Count > 0 Then
            'If recProfitLoss.RecordCount > 1 Then
            'TOTAL PROFIT
            dblBalanceAmount = recProfitLoss.Rows(0).Item("balance_amount")

            'If balance is greater then zero then insert in Credit amount
            If dblBalanceAmount > 0 Then
                'Insert in to voucher detail table.
                strSql = "Insert Into tblGlVoucherDetail (voucher_id       ,      location_id                                  ,       coa_detail_id        ,               comments  ,             debit_amount   ,        credit_amount    ) " & _
                                            " Values (" & lngVoucgerID & " ," & Fyear.LocationID & "," & G_PROFIT_LOSS_ACC_ID & ", 'Initialization Voucher',               0            , " & dblBalanceAmount & ")"
            Else
                'If balance is greater then zero then insert in DEBIT amount

                'Insert in to voucher detail table.
                strSql = "Insert Into tblGlVoucherDetail (voucher_id       ,        location_id                                ,       coa_detail_id        ,               comments  ,             debit_amount   ,    credit_amount ) " & _
                                            " Values (" & lngVoucgerID & " ," & Fyear.LocationID & "," & G_PROFIT_LOSS_ACC_ID & ", 'Initialization Voucher', " & dblBalanceAmount * -1 & "   ,              0   )"

            End If

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, Nothing)
            'End If
        End If

        funSaveClosing = True

    End Function


    Public Function Deleted(ByVal objFYear As FiniancialYearStatus) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "Delete from tblGLDefFinancialYearStatus where financial_year_id = " & objFYear.FYearID

            ''Execute SQL 
            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

            'SQL Statement Log
            objFYear.ActivityLog.SQLType = "DELETE"
            objFYear.ActivityLog.TableName = "tblGLDefFinancialYearStatus"
            objFYear.ActivityLog.SQL = strSQL
            ' CR # 332
            'UtilityDAL.BuildSQLLog(objFYear.ActivityLog, trans)
            UtilityDAL.BuildSQLLog(objFYear.ActivityLog, trans, True)

            ' delete all fyear status records
            strSQL = "Delete from tblGLDefFinancialYear where financial_year_id = " & objFYear.FYearID
            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))

            ''SQL Statement Log
            objFYear.ActivityLog.SQLType = "DELETE"
            objFYear.ActivityLog.TableName = "tblGLDefFinancialYear"
            objFYear.ActivityLog.SQL = strSQL
            ' CR # 332
            'UtilityDAL.BuildSQLLog(objFYear.ActivityLog, trans)
            UtilityDAL.BuildSQLLog(objFYear.ActivityLog, trans, True)

            ''Activity Log
            objFYear.ActivityLog.FormAction = "Delete"
            UtilityDAL.BuildActivityLog(objFYear.ActivityLog, trans)

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
    Public Function GetAll(ByVal CompanyId As Integer, Optional ByVal strCondition As String = "") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            'strSQL = "SELECT financial_year_id as [FYear ID] , start_date as [Start Date] , end_date as [End Date] , year_code as [FYear Code], status as [Status] From tblGLDefFinancialYear ORDER BY year_code DESC"
            strSQL = "SELECT Y.financial_year_id as [FYear ID], Y.start_date as [Start Date] , Y.end_date  as [End Date] , Y.year_code as [FYear Code], S.status  as [Status]From tblGLDefFinancialYear Y INNER JOIN tblGLDefFinancialYearStatus S ON Y.Financial_year_id = S.Financial_year_id WHERE s.Location_ID = " & CompanyId & " ORDER BY year_code DESC"
            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("FYearList")
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

    Public Function IsLatestYear(ByVal FinancialYearId As Integer) As Boolean

        Dim intTotalcompanies As Integer

        Try

            Dim strSQL As String
            strSQL = "select Max(financial_year_id)as financial_year_id from TblGlDefFinancialYear"

            intTotalcompanies = 0
            If Val(UtilityDAL.ReturnDataRow(strSQL).Item(0).ToString) = FinancialYearId Then Return True Else Return False


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsValidateForUpdate(ByVal ObjFyear As FiniancialYearStatus) As Boolean

        Dim intTotalcompanies As Integer

        Try

            Dim strSQL As String

            'build sql to check if new field name or code already exists
            strSQL = "select year_code from tblGLDefFinancialYear Y INNER JOIN tblGLDefFinancialYearStatus S ON y.Financial_year_id = S.Financial_year_id   Where (Y.year_code='" & ObjFyear.YearCode & "' AND S.Status  = '" & ObjFyear.Status & "' AND Y.financial_year_id <> " & Val(ObjFyear.FYearID) & " AND S.Location_ID = " & ObjFyear.LocationID & " )"
            'UtilityDAL.ReturnDataRow(strSQL)

            'check if no record found in previous query
            If Not UtilityDAL.GetDataTable(strSQL).Rows.Count > 0 Then


                'Get year codes from the Financial year Table whome keeps status = 'Open'
                'Dim recFindCurrentRecords As ADODB.RecordSet
                strSQL = "Select Year_Code from tblGlDefFinancialYear y INNER JOIN tblGLDefFinancialYearStatus S ON y.Financial_year_id = S.Financial_year_id  Where (y.financial_year_id < " & Val(ObjFyear.FYearID) & ") and (s.Status  = '" & G_OPEN_FYEAR & "' ) AND (S.Location_ID = " & ObjFyear.LocationID & ") "
                'recFindCurrentRecords = m_ObjExecuteSql.GetRecordset(strSQL)

                'if fyears are available with Status = Open
                If UtilityDAL.GetDataTable(strSQL).Rows.Count > 0 Then

                    Throw New Exception("You must close the previous open Financial Year's") ', vbOKOnly + vbExclamation, gMsgTitle)
                    Exit Function
                End If

                'Dim rs As New ADODB.RecordSet
                strSQL = "select post, * from tblGLvoucher where post=0 and finiancial_year_id=" & ObjFyear.FYearID & " AND Location_ID = " & ObjFyear.LocationID
                If UtilityDAL.GetDataTable(strSQL).Rows.Count > 0 Then

                    Throw New Exception("Some vouchers are unposted, Financial year can't be closed") ', vbOKOnly + vbExclamation, gMsgTitle)
                    Exit Function
                End If

            End If
            strSQL = "SELECT     coa_detail_id, main_sub_sub_id, detail_code, detail_title, end_date " _
                        & "FROM tblGlCOAMainSubSubDetail " _
                        & "WHERE(coa_detail_id =" & G_PROFIT_LOSS_ACC_ID & " )"

            If Not UtilityDAL.GetDataTable(strSQL).Rows.Count > 0 Then

                Throw New Exception("Configure a proper account for Profit and Loss")
                Exit Function
            End If

            strSQL = "Select location_id from tblGlDefLocation"

            intTotalcompanies = 0
            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then

                    While objDR.Read
                        intTotalcompanies += 1
                    End While

                End If

            End Using

            If intTotalcompanies > 0 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function funOnlyOpenYear() As Boolean
        Dim strSQL As String

        Try

            strSQL = "SELECT     tblOpenYears.location_id, tblOpenYears.NoOfOpenYears, tblTotalYears.NoOfTotalYears " & _
                     " FROM         (SELECT     location_id, COUNT(Status) NoOfOpenYears " & _
                     " From tblGlDefFinancialYearStatus " & _
                     " WHERE      status = 'Open' " & _
                     " GROUP BY location_id) tblOpenYears INNER JOIN " & _
                     " (SELECT     location_id, COUNT(Status) NoOfTotalYears " & _
                     " From tblGlDefFinancialYearStatus " & _
                     " GROUP BY location_id) tblTotalYears ON tblOpenYears.location_id = tblTotalYears.location_id "


            Using objDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read

                        If Val(objDR.Item("NoOfOpenYears")) = 1 And Val(objDR.Item("NoOfTotalYears")) > 1 Then
                            Return True
                        Else
                            Return False
                        End If

                    End While
                End If

            End Using

            Return False

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsValidateForDelete(ByVal ObjFYear As FiniancialYearStatus) As Boolean

        Try

            Dim strSQL As String

            strSQL = "SELECT * FROM tblGLDefFinancialYearStatus where financial_year_id = " & ObjFYear.FYearID & " AND StatuS = '" & G_STATUS_CLOSE & "'"

            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objNameDR.HasRows Then
                    Return False
                End If

            End Using

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region


End Class
