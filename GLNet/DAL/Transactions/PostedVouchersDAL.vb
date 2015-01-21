Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// 06 May,2013       Abdul Jabbar       CR#225: Lightwave Voucher Searching Posting screen should show unbalanced voucher.
''// 04-jun-2013       Fatima Tajammal    CR#245  Make a new report for Voucher History
''// 03-Jan-2014       Waqas Anwar        CR#290  Non replicated shop should also be included in source at Voucher Search/post screen and Ledger
''// 10 Jun,2014       Abdul Jabbar       CR#300: Voucher: Company Wise & shop wise voucher entry option should be available.
''// 18 Jul,2014       Abdul Jabbar       CR#322: Voucher search required against voucher amount, invoice no. & cheque number
Public Class PostedVouchersDAL

    'Public Function Save(ByVal PostingVouchers As Model.PostingVouchersModel) As Boolean

    '    Dim conn As New SqlConnection(SQLHelper.CON_STR)
    '    conn.Open()

    '    Dim trans As SqlTransaction = conn.BeginTransaction
    '    Dim intCounter As Integer

    '    Dim ObjModelTemp As PostingVouchersModel

    '    Try

    '        Dim strSQL As String
    '        intCounter = 0

    '        For Each ObjModelTemp In PostingVouchers.SELECTEDRECORD_ARRAYLIST
    '            strSQL = " Update tblglvoucher set post = " & ObjModelTemp.Post _
    '                   & " Where location_id = " & ObjModelTemp.LocationID & " and voucher_id = " & ObjModelTemp.VoucherID

    '            ' Execute SQL 
    '            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


    '        Next

    '        ' Commit Transaction .. 
    '        trans.Commit()

    '        ''Return
    '        Return True


    '    Catch ex As SqlException
    '        trans.Rollback()
    '        Throw ex

    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex

    '    Finally
    '        conn.Close()

    '    End Try
    'End Function
    Public Function Save(ByVal PostingVouchers As GLVoucher) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String = ""

            For Each objVList As VoucherDetailItem In PostingVouchers.ListofVouchers

                'CR#160
                'strSQL = " Update tblglvoucher set post = '" & objVList.MPost & "'" & _
                ' " Where location_id = " & objVList.MLocation & " and voucher_id = " & objVList.MVoucherID

                'Cr # 245

                Dim Command As SqlCommand
                Command = New SqlCommand()
                Command.CommandText = "SpVoucherPostUnPost"
                Command.CommandType = CommandType.StoredProcedure
                Command.Parameters.Add(New SqlParameter("@VoucherID", SqlDbType.Int))
                Command.Parameters("@VoucherID").Value = objVList.MVoucherID
                Command.Parameters.Add(New SqlParameter("@Status", SqlDbType.Text))
                Command.Parameters.Add(New SqlParameter("@Condition", SqlDbType.Int))
                Command.Parameters("@Condition").Value = 0
                If objVList.MPost = True Then
                    Command.Parameters("@Status").Value = "Post"
                Else
                    Command.Parameters("@Status").Value = "UnPost"
                End If

                Command.Connection = trans.Connection

                Command.Transaction = trans
                Command.ExecuteNonQuery()

                strSQL = " Update tblglvoucher set post = '" & objVList.MPost & "'" & _
                 " Where location_id = " & objVList.MLocation & " and voucher_id = " & objVList.MVoucherID & " and shop_id <= 0"


                ' Execute SQL 
                Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))

                'CR#61' ''SQL Statement Log
                PostingVouchers.ActivityLog.SQLType = "UPDATE"
                PostingVouchers.ActivityLog.TableName = "tblglvoucher"
                PostingVouchers.ActivityLog.SQL = strSQL
                UtilityDAL.BuildSQLLog(PostingVouchers.ActivityLog, trans)

                'CR # 245

                Dim comm As SqlCommand
                comm = New SqlCommand()
                comm.CommandText = "SpVoucherPostUnPost"
                comm.CommandType = CommandType.StoredProcedure
                comm.Parameters.Add(New SqlParameter("@VoucherID", SqlDbType.Int))
                comm.Parameters("@VoucherID").Value = objVList.MVoucherID
                comm.Parameters.Add(New SqlParameter("@Status", SqlDbType.Text))
                comm.Parameters.Add(New SqlParameter("@Condition", SqlDbType.Int))
                comm.Parameters("@Condition").Value = 1
                If objVList.MPost = True Then
                    comm.Parameters("@Status").Value = "Post"
                Else
                    comm.Parameters("@Status").Value = "UnPost"
                End If
                comm.Connection = trans.Connection
                comm.Transaction = trans
                comm.ExecuteNonQuery()

            Next

            'CR#61''SQL Statement Log
            PostingVouchers.ActivityLog.SQLType = "UPDATE"
            PostingVouchers.ActivityLog.TableName = "tblglvoucher"
            PostingVouchers.ActivityLog.SQL = strSQL
            UtilityDAL.BuildActivityLog(PostingVouchers.ActivityLog, trans)

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

    Public Function GetAll(ByVal ObjModel As Model.PostingVouchersModel, ByVal PostUnPostStatus As String, ByVal BalancedUnBalancedStatus As String) As DataTable

        Dim ObjDA As SqlClient.SqlDataAdapter

        Try

            Dim strSQL As String = ""
            'CR#300 New drop down for company has been added on  screen, when no company will be selected the system should retrun all comapnies vouchers
            Dim strSelectedCompany As String = String.Empty
            If ObjModel.LocationID <> 0 Then
                strSelectedCompany = " and m.location_id = " & ObjModel.LocationID
            End If


            If ObjModel.TempVouchers = False Then
                If BalancedUnBalancedStatus = "ALL" Then
                    'CR#300 Added new column m.IsLightwaveVoucher
                    strSQL = " Select post as checkboxvalue, m.voucher_code, convert(datetime, left(m.voucher_date,11)) voucher_date, yrs.year_code,loc.location_name, " _
                           & " sum(d.debit_amount) debit_amount , sum(d.credit_amount) credit_amount, m.voucher_id, m.location_id, m.source,m.IsLightwaveVoucher " _
                           & " from tblglvoucher as m, tblglvoucherdetail as d,tblgldeflocation loc, " _
                           & " tblgldeffinancialyear As yrs " _
                           & " Where m.voucher_id = d.voucher_id And m.location_id = d.location_id " _
                           & " and loc.location_id = m.location_id  " _
                           & " and yrs.financial_year_id = m.finiancial_year_id  " _
                           & " and yrs.year_code = '" & ObjModel.FinancialYearCode & "'" _
                           & " " & strSelectedCompany & "" 'CR#300
                    '& " and m.location_id = " & ObjModel.LocationID

                ElseIf BalancedUnBalancedStatus = "Other" Then
                    'CR#300 Added new column m.IsLightwaveVoucher
                    strSQL = " Select post as checkboxvalue, m.voucher_code, convert(datetime, left(m.voucher_date,11)) voucher_date,yrs.year_code,loc.location_name, " _
                           & " sum(d.debit_amount) debit_amount , sum(d.credit_amount) credit_amount, m.voucher_id, m.location_id, m.source,m.IsLightwaveVoucher   " _
                           & " from tblglvoucher as m, tblglvoucherdetail as d,tblgldeflocation loc, " _
                           & " tblgldeffinancialyear As yrs " _
                           & " Where m.voucher_id = d.voucher_id And m.location_id = d.location_id " _
                           & " and loc.location_id = m.location_id  " _
                           & " and yrs.financial_year_id=m.finiancial_year_id  " _
                           & " and other_voucher = 1 and yrs.year_code = '" & ObjModel.FinancialYearCode & "'" _
                           & " " & strSelectedCompany & "" 'CR#300
                    '& " and m.location_id= " & ObjModel.LocationID


                Else

                    'CR#225
                    'strSQL = " Select post as checkboxvalue, m.voucher_code, convert(datetime, left(m.voucher_date,11)) voucher_date,yrs.year_code,loc.location_name, " _
                    '       & " sum(d.debit_amount) debit_amount , sum(d.credit_amount) credit_amount, m.voucher_id, m.location_id, m.source  " _
                    '       & " from tblglvoucher as m, tblglvoucherdetail as d,tblgldeflocation loc, " _
                    '       & " tblgldeffinancialyear As yrs " _
                    '       & " Where m.voucher_id = d.voucher_id And m.location_id = d.location_id " _
                    '       & " and loc.location_id=m.location_id  " _
                    '       & " and yrs.financial_year_id = m.finiancial_year_id  " _
                    '       & " and post = " & PostUnPostStatus & " and yrs.year_code= '" & ObjModel.FinancialYearCode & "'" _
                    '       & " and m.location_id = " & ObjModel.LocationID

                    If BalancedUnBalancedStatus = "UnBalanced" Then
                        'CR#300 Added new column m.IsLightwaveVoucher
                        strSQL = " Select post as checkboxvalue, m.voucher_code, convert(datetime, left(m.voucher_date,11)) voucher_date,yrs.year_code,loc.location_name, " _
                                                   & " sum(d.debit_amount) debit_amount , sum(d.credit_amount) credit_amount, m.voucher_id, m.location_id, m.source,m.IsLightwaveVoucher   " _
                                                   & " from tblglvoucher as m, tblglvoucherdetail as d,tblgldeflocation loc, " _
                                                   & " tblgldeffinancialyear As yrs " _
                                                   & " Where m.voucher_id = d.voucher_id And m.location_id = d.location_id " _
                                                   & " and loc.location_id=m.location_id  " _
                                                   & " and yrs.financial_year_id = m.finiancial_year_id  " _
                                                   & " and yrs.year_code= '" & ObjModel.FinancialYearCode & "'" _
                                                   & " " & strSelectedCompany & "" 'CR#300
                        '& " and m.location_id = " & ObjModel.LocationID

                    Else
                        'CR#300 Added new column m.IsLightwaveVoucher
                        strSQL = " Select post as checkboxvalue, m.voucher_code, convert(datetime, left(m.voucher_date,11)) voucher_date,yrs.year_code,loc.location_name, " _
                           & " sum(d.debit_amount) debit_amount , sum(d.credit_amount) credit_amount, m.voucher_id, m.location_id, m.source,m.IsLightwaveVoucher   " _
                           & " from tblglvoucher as m, tblglvoucherdetail as d,tblgldeflocation loc, " _
                           & " tblgldeffinancialyear As yrs " _
                           & " Where m.voucher_id = d.voucher_id And m.location_id = d.location_id " _
                           & " and loc.location_id=m.location_id  " _
                           & " and yrs.financial_year_id = m.finiancial_year_id  " _
                           & " and post = " & PostUnPostStatus & " and yrs.year_code= '" & ObjModel.FinancialYearCode & "'" _
                           & " " & strSelectedCompany & "" 'CR#300
                        '& " and m.location_id = " & ObjModel.LocationID


                    End If

                End If

                '  Don't pick Zeroth Voucher ..
                strSQL = strSQL & " AND Voucher_no <> '000000' "


                ' Adding A Filter To Query Based On The Voucher Type ..
                If ObjModel.VoucherType <> "" Then strSQL = strSQL & " and m.voucher_type_id = " & ObjModel.VoucherType



                ' Adding A Filter To Query Based On The Voucher Month ..
                If ObjModel.VoucherMonth <> "" Then strSQL = strSQL & " and m.voucher_month = '" & ObjModel.VoucherMonth & "'"



                ' Adding A Filter To Query Based On The Voucher Source ..
                If ObjModel.VoucherSource <> "" Then strSQL = strSQL + " and m.source = '" & ObjModel.VoucherSource & "'"



                ' Adding A Filter To Query Based On Voucher # Or DateWise Or ALL ..
                If ObjModel.VoucherNoWiseFlag = 1 Then
                    strSQL = strSQL & " and m.voucher_no >= '" & ObjModel.VoucherNoFrom & "' and m.voucher_no <= '" & ObjModel.VoucherNoTO & "' "

                ElseIf ObjModel.VoucherDateWiseFlag = 1 Then
                    strSQL = strSQL + " and m.voucher_date between '" & ObjModel.VoucherStartDate & "' and '" & ObjModel.VoucherEndDate & "' "

                End If

                'CR#322 Adding filter for Cheque No, Invoice Amount and Remarks
                If ObjModel.ChequeNo <> String.Empty Then
                    strSQL = strSQL + " and m.cheque_no='" & ObjModel.ChequeNo & "'"
                End If

                If ObjModel.Remarks <> String.Empty Then
                    'strSQL = strSQL + " and d.comments like '%" & ObjModel.Remarks & "%'" 
                    strSQL = strSQL + " and m.voucher_id in (select voucher_id from tblglvoucherdetail where comments like '%" & ObjModel.Remarks & "%')"
                End If
                'CR#322 End

                'strSQL = strSQL + " and m.voucher_date between '" & ObjModel.VoucherStartDate & "' and '" & ObjModel.VoucherEndDate & "' "


                ' Adding Group By Conditon In SQL Query .. 
                strSQL = strSQL & " Group by post, " _
                       & " m.voucher_code,convert(varchar(11),m.voucher_date,13), " _
                       & " voucher_date,yrs.year_code,loc.location_name,  m.voucher_id, " _
                       & " m.location_id, m.source,m.IsLightwaveVoucher  "



                If BalancedUnBalancedStatus = "Balanced" Then
                    'CR#322 adding an extra conditin when User entered Amount to filter then search voucher of that amount
                    'strSQL = strSQL & " having sum(debit_amount) - sum(credit_amount) = 0 "
                    If ObjModel.InvAmount <> 0 Then
                        strSQL = strSQL & " having (sum(debit_amount) - sum(credit_amount) = 0 AND SUM(d.debit_amount)=" & ObjModel.InvAmount & ")"
                    Else
                        strSQL = strSQL & " having sum(debit_amount) - sum(credit_amount) = 0 "
                    End If

                ElseIf BalancedUnBalancedStatus = "UnBalanced" Then
                    'CR#322 adding an extra conditin when User entered Amount to filter then search voucher of that amount
                    'strSQL = strSQL & " having sum(debit_amount) - sum(credit_amount) <> 0 "
                    If ObjModel.InvAmount <> 0 Then
                        strSQL = strSQL & " having (sum(debit_amount) - sum(credit_amount) <> 0 AND SUM(d.debit_amount)=" & ObjModel.InvAmount & ")"
                    Else
                        strSQL = strSQL & " having sum(debit_amount) - sum(credit_amount) <> 0 "
                    End If
                    'CR#322 If non of above condition is true then 
                Else
                    If ObjModel.InvAmount <> 0 Then
                        strSQL = strSQL & " having SUM(d.debit_amount)=" & ObjModel.InvAmount & ""
                    End If
                End If

                strSQL = strSQL & " ORDER BY convert(datetime, left(m.voucher_date,11)) ASC, voucher_code "

                End If




            If ObjModel.TempVouchers = True Then
                If BalancedUnBalancedStatus = "ALL" Then
                    strSQL = " Select Convert(bit," & Val(PostUnPostStatus) & ") as checkboxvalue, m.voucher_code,convert(varchar(11), m.voucher_date,13) voucher_date, yrs.year_code,loc.location_name, " _
                           & " sum(d.debit_amount) debit_amount , sum(d.credit_amount) credit_amount, m.voucher_id, m.location_id, m.source,m.IsLightwaveVoucher     " _
                           & " from tblGLVoucherTemp as m, tblGLVoucherTempdetail as d,tblgldeflocation loc, " _
                           & " tblgldeffinancialyear As yrs " _
                           & " Where m.voucher_id = d.voucher_id And m.location_id = d.location_id " _
                           & " and loc.location_id = m.location_id  " _
                           & " and yrs.financial_year_id = m.finiancial_year_id  " _
                           & " and yrs.year_code = '" & ObjModel.FinancialYearCode & "'" _
                           & " " & strSelectedCompany & "" 'CR#300
                    '& " and m.location_id = " & ObjModel.LocationID

                ElseIf BalancedUnBalancedStatus = "Other" Then

                    strSQL = " Select Convert(bit," & Val(PostUnPostStatus) & ") as checkboxvalue, m.voucher_code,convert(varchar(11),m.voucher_date,13) voucher_date,yrs.year_code,loc.location_name, " _
                           & " sum(d.debit_amount) debit_amount , sum(d.credit_amount) credit_amount, m.voucher_id, m.location_id, m.source ,m.IsLightwaveVoucher     " _
                           & " from tblGLVoucherTemp as m, tblGLVoucherTempdetail as d,tblgldeflocation loc, " _
                           & " tblgldeffinancialyear As yrs" _
                           & " Where m.voucher_id = d.voucher_id And m.location_id = d.location_id " _
                           & " and loc.location_id = m.location_id  " _
                           & " and yrs.financial_year_id=m.finiancial_year_id  " _
                           & " and other_voucher = 1 and yrs.year_code = '" & ObjModel.FinancialYearCode & "'" _
                           & " " & strSelectedCompany & "" 'CR#300
                    '& " and m.location_id= " & ObjModel.LocationID


                Else

                    strSQL = " Select Convert(bit," & Val(PostUnPostStatus) & ") as checkboxvalue, m.voucher_code,convert(varchar(11),m.voucher_date,13) voucher_date,yrs.year_code,loc.location_name, " _
                           & " sum(d.debit_amount) debit_amount , sum(d.credit_amount) credit_amount, m.voucher_id, m.location_id, m.source,m.IsLightwaveVoucher      " _
                           & " from tblGLVoucherTemp as m, tblGLVoucherTempdetail as d,tblgldeflocation loc, " _
                           & " tblgldeffinancialyear As yrs " _
                           & " Where m.voucher_id = d.voucher_id And m.location_id = d.location_id " _
                           & " and loc.location_id= m.location_id  " _
                           & " and yrs.financial_year_id = m.finiancial_year_id  " _
                           & " and post = " & PostUnPostStatus & " and yrs.year_code= '" & ObjModel.FinancialYearCode & "'" _
                           & " " & strSelectedCompany & "" 'CR#300
                    '& " and m.location_id = " & ObjModel.LocationID

                End If

                '  Don't pick Zeroth Voucher ..
                strSQL = strSQL & " AND Voucher_no <> '000000' "


                ' Adding A Filter To Query Based On The Voucher Type ..
                If ObjModel.VoucherType <> "" Then strSQL = strSQL & " and m.voucher_type_id = " & ObjModel.VoucherType



                ' Adding A Filter To Query Based On The Voucher Month ..
                If ObjModel.VoucherMonth <> "" Then strSQL = strSQL & " and m.voucher_month = '" & ObjModel.VoucherMonth & "'"



                ' Adding A Filter To Query Based On The Voucher Source ..
                If ObjModel.VoucherSource <> "" Then strSQL = strSQL + " and m.source = '" & ObjModel.VoucherSource & "'"


                ' Adding A Filter To Query Based On Voucher # Or DateWise Or ALL ..
                If ObjModel.VoucherNoWiseFlag = 1 Then
                    strSQL = strSQL & " and m.voucher_no >= '" & ObjModel.VoucherNoFrom & "' and m.voucher_no <= '" & ObjModel.VoucherNoTO & "' "

                ElseIf ObjModel.VoucherDateWiseFlag = 1 Then
                    strSQL = strSQL + " and m.voucher_date between '" & ObjModel.VoucherStartDate & "' and '" & ObjModel.VoucherEndDate & "' "

                End If

                'CR#322 Adding filter for Cheque No, Invoice Amount and Remarks
                If ObjModel.ChequeNo <> String.Empty Then
                    strSQL = strSQL + " and m.cheque_no='" & ObjModel.ChequeNo & "'"
                End If

                If ObjModel.Remarks <> String.Empty Then
                    'strSQL = strSQL + " and d.comments like '%" & ObjModel.Remarks & "%'"
                    strSQL = strSQL + " and m.voucher_id in (select voucher_id from tblglvoucherdetail where comments like '%" & ObjModel.Remarks & "%')"
                End If
                'CR#322 End

                ' Adding Group By Conditon In SQL Query .. 
                strSQL = strSQL & " Group by post, " _
                       & " m.voucher_code,convert(varchar(11),m.voucher_date,13), " _
                       & " voucher_date,yrs.year_code,loc.location_name,  m.voucher_id, " _
                       & " m.location_id, m.source,m.IsLightwaveVoucher "


                If BalancedUnBalancedStatus = "Balanced" Then
                    'CR#322 start adding an extra conditin when User entered Amount to filter then search voucher of that amount
                    'strSQL = strSQL & " having sum(debit_amount) - sum(credit_amount) = 0 "
                    If ObjModel.InvAmount <> 0 Then
                        strSQL = strSQL & " having (sum(debit_amount) - sum(credit_amount) = 0 AND SUM(d.debit_amount)=" & ObjModel.InvAmount & ")"
                    Else
                        strSQL = strSQL & " having sum(debit_amount) - sum(credit_amount) = 0 "
                    End If
                    'CR#322 End

                ElseIf BalancedUnBalancedStatus = "UnBalanced" Then
                    'CR#322 start adding an extra conditin when User entered Amount to filter then search voucher of that amount
                    'strSQL = strSQL & " having sum(debit_amount) - sum(credit_amount) <> 0 "
                    If ObjModel.InvAmount <> 0 Then
                        strSQL = strSQL & " having (sum(debit_amount) - sum(credit_amount) <> 0 AND SUM(d.debit_amount)=" & ObjModel.InvAmount & ")"
                    Else
                        strSQL = strSQL & " having sum(debit_amount) - sum(credit_amount) <> 0 "
                    End If
                    'CR#322 End
                    'CR#322 If non of above condition is true then 
                Else
                    If ObjModel.InvAmount <> 0 Then
                        strSQL = strSQL & " having SUM(d.debit_amount)=" & ObjModel.InvAmount & ""
                    End If
                End If


                strSQL = strSQL & " ORDER BY convert(varchar(11),m.voucher_date,102) ASC, voucher_code "

            End If


            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            ' By Asif Kamal
            ObjDA.SelectCommand.CommandTimeout = 5000

            Dim myDataTable As New DataTable("Vouchers")
            ObjDA.Fill(myDataTable)

            Return myDataTable

        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDA = Nothing

        End Try

    End Function
    'CR#5
    Public Function GetTblshopConfigValues() As DataTable

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction
        Dim IsIntegrated As Boolean = False
        Dim DTShopConfigList As New DataTable
        Dim ObjDA As SqlClient.SqlDataAdapter


        Try

            Dim strSQL As String

            'Check if Table Exist
            strSQL = "IF Not OBJECT_ID (N'dbo.tblShopConfiguration') IS NULL " & _
                     " Select 	1 as Exist " & _
                     " Else " & _
                     " Select 0 as Exist"

            Dim DR As SqlDataReader = SQLHelper.ExecuteReader(conn.ConnectionString, CommandType.Text, strSQL, Nothing)

            While DR.Read

                IsIntegrated = CBool(DR.Item("Exist"))

            End While

            DR.Close()

            If IsIntegrated Then
                'if Table Exist then Get config values
                ''Start of CR # 290   Waqas Anwar   03-Jan-2014
                'strSQL = "select config_value from tblShopConfiguration where config_name='gl_shop_code' and Config_value<>'' and Config_value is not null " & _
                '         " and shop_id not in (select shop_id from tbldefshops where IsReplecatedShop=0)"

                strSQL = " select config_value from tblShopConfiguration where config_name='gl_shop_code' and Config_value<>'' and Config_value is not null "
                ''End CR # 290

                ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

                ObjDA.Fill(DTShopConfigList)


            End If


            Return DTShopConfigList


        Catch ex As Exception

            Throw ex

        Finally
            conn.Close()

        End Try
    End Function
End Class
