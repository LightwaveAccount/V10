''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Lumensoft GL
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : BankReconcilationDal.vb           				                            
''// Programmer	     : R@! Shahid
''// Creation Date	 : 16-Jul-2009
''// Description     :                          
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
'//  22 Jul,2011       Abdul Jabbar        CR#145.Problem while Reconciling Banks like if two BPV presented one of 20 jul, and second 23 jul. When user present first in 20 jul update and then second on 23 jul press update, first one of 20 jul date also change to 23 jul.
''//169     20 Dec,2011      Abdul Jabbar       CR#169:Bank Ledger in Bank Position and Bank Reconcilation doesn't match with Ledger report (change in DAL)
''//304     17-apr-2014      Fatima               Bank Statement report
''//319     15 Jul,2014      Abdul Jabbar        Bank Position, Bank Statement Bank Balance balance is not appropriate
''/////////////////////////////////////////////////////////////////////////////////////////

Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility

Public Class BankReconcilationDal

    Private Enum EnumActivity

        colChqPaidChq = 0
        VoucherNo = 1
        VoucherDate = 2
        voucherId = 3
        ChequeNo = 4
        ChequeDate = 5
        ChequePaid = 6
        ChequeCredited = 7
        PaidDate = 8
        DrAmount = 9
        CrAmount = 10
        VoucherType = 11

    End Enum

#Region "Local Functions and Procedures"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objBankReconcilation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsValidateForSave(ByVal objBankReconcilation As BankReconcilation) As Boolean

        Try
            Dim strSQL As String

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' 
    ''' 
    ''' </summary>
    ''' <param name="objBankReconcilation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsValidateForDelete(ByVal objBankReconcilation As BankReconcilation) As Boolean

        Try
            Dim strSQL As String

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region


#Region "Public Functions and Procedures"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ObjDt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Add(ByVal ObjDt As DataTable, ByVal dtpPaidDate As Date, ByVal mobjModel As BankReconcilation) As Boolean

        Dim objBankReconcilation As New BankReconcilation

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Dim strSQL As String

        Try

            Dim intCounter As Integer
            Dim xarrDetail As DataRowCollection = ObjDt.Rows
            For intCounter = 0 To ObjDt.Rows.Count - 1

                'strSql = "update tblGlVoucher set  cheque_paid = " & IIf(xarrDetail(intCounter, GridCol.colChqPaidChq) = True, 1, 0) & ",cheque_paid_date = '" & IIf(xarrDetail(intCounter, GridCol.colChqPaidChq) = True, dtpPaidDate.Value, xarrDetail(intCounter, GridCol.colChqPaidDate)) & "' where voucher_id =" & xarrDetail(intCounter, GridCol.colVoucherId)
                'strSql = "update tblGlVoucher set  cheque_paid = " & IIf(xarrDetail(intCounter, GridCol.colChqPaidChq) = True, 1, 0) & ",cheque_paid_date = '" & IIf(xarrDetail(intCounter, GridCol.colChqPaidChq) = True, dtpPaidDate.Value, Null) & "' where voucher_id =" & xarrDetail(intCounter, GridCol.colVoucherId)

                strSQL = " update tblGlVoucher set  "
                If xarrDetail(intCounter).Item("colChqPaidChq").ToString = "True" Then

                    ''Modified on 29-May-2006   by Ali Qasim    to update the cheque_credited if debit amount is present and vice versa
                    ''''                                    strSql = strSql & " cheque_paid = 1  "
                    If Val(xarrDetail(intCounter).Item(EnumActivity.CrAmount).ToString) > 0 Then

                        strSQL = strSQL & " cheque_paid = 1  "

                    Else

                        strSQL = strSQL & " cheque_credited = 1  "

                    End If

                    'CR#145
                    'If Not (xarrDetail(intCounter).Item(EnumActivity.PaidDate).ToString = String.Empty And xarrDetail(intCounter).Item(EnumActivity.PaidDate).ToString <> "01/Jan/1900")) Then
                    If Not ((xarrDetail(intCounter).Item(EnumActivity.PaidDate).ToString = String.Empty And xarrDetail(intCounter).Item(EnumActivity.PaidDate).ToString <> "01/Jan/1900") Or (IsDBNull(xarrDetail(intCounter).Item(EnumActivity.PaidDate)))) Then

                        strSQL = strSQL & ", cheque_paid_date = '" & xarrDetail(intCounter).Item(EnumActivity.PaidDate) & "'  where voucher_id =" & xarrDetail(intCounter).Item(EnumActivity.voucherId)
                    Else

                        strSQL = strSQL & ", cheque_paid_date = '" & dtpPaidDate & "'  where voucher_id =" & xarrDetail(intCounter).Item(EnumActivity.voucherId)
                    End If
                Else

                    ''Modified on 17-Jul-2006   by Ali Qasim    to update the cheque_credited if debit amount is present and vice versa (removed the problem of changing record from credited to uncredited)
                    ''=============================
                    'strsql = strsql & " cheque_paid = 0 , cheque_paid_date = NULL where voucher_id =" & xarrDetail(intCounter, GridCol.colVoucherId)
                    If Val(xarrDetail(intCounter).Item(EnumActivity.CrAmount)) > 0 Then

                        strSQL = strSQL & " cheque_paid = 0 , cheque_paid_date = NULL where voucher_id =" & xarrDetail(intCounter).Item(EnumActivity.voucherId)

                    Else

                        strSQL = strSQL & " cheque_credited = 0 , cheque_paid_date = NULL where voucher_id =" & xarrDetail(intCounter).Item(EnumActivity.voucherId)

                    End If
                    ''=============================

                End If

                'CR#160
                strSQL = strSQL & " and shop_id <= 0"

                SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

            Next

            'strSQL = "INSERT INTO TblDefCityAreas ( city_id, field_name, field_code, sort_order, comments, readonly) " _
            '& " VALUES ( " & objBankReconcilation.CityID & ", '" & objBankReconcilation.AreaName.Trim.Replace("'", "''") & "', '" & objBankReconcilation.AreaCode.Trim.Replace("'", "''") & "', '" & objBankReconcilation.SortOrder & "', '" & objBankReconcilation.Comments.Trim.Replace("'", "''") & "', '" & IIf(objBankReconcilation.IsReadOnly, "ReadOnly", "") & "') " _
            '& " Select @@Identity"

            '''Execute SQL 
            'objBankReconcilation.CityAreaID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

            ' ''SQL Statement Log
            'objBankReconcilation.ActivityLog.SQLType = "INSERT"
            'objBankReconcilation.ActivityLog.TableName = "TblDefCities"
            'objBankReconcilation.ActivityLog.SQL = strSQL
            'UtilityDAL.BuildSQLLog(objBankReconcilation.ActivityLog, trans)

            ' ''Activity Log

            mobjModel.ActivityLog.FormAction = "Save"
            mobjModel.ActivityLog.LogRef = EnumActivity.voucherId
            mobjModel.ActivityLog.RefType = "Voucher Id"

            'mobjModel.ActivityLog.ScreenTitle = "Bank ReconcilationDal"
            'mobjModel.ActivityLog.SQL = strSQL
            UtilityDAL.BuildActivityLog(mobjModel.ActivityLog, trans)

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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objBankReconcilation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update(ByVal objBankReconcilation As BankReconcilation) As Boolean

        ''Try
        ''    'Check DB Validation, If Not validate then the function returns an exception
        ''    Me.IsValidateForSave(objCity)


        ''Catch ex As Exception
        ''    'Thorw exception if db validation false
        ''    Throw ex

        ''End Try

        'Dim conn As New SqlConnection(SQLHelper.CON_STR)
        'conn.Open()
        'Dim trans As SqlTransaction = conn.BeginTransaction

        'Try

        '    Dim strSQL As String

        '    strSQL = "UPDATE TblDefCityAreas SET " _
        '    & " city_id = " & objBankReconcilation.CityID & ", " _
        '    & " field_name = '" & objBankReconcilation.AreaName.Trim.Replace("'", "''") & "', " _
        '    & " field_code = '" & objBankReconcilation.AreaCode.Trim.Replace("'", "''") & "', " _
        '    & " sort_order = '" & objBankReconcilation.SortOrder & "',  " _
        '    & " comments = '" & objBankReconcilation.Comments.Trim.Replace("'", "''") & "',  " _
        '    & " readonly = '" & IIf(objBankReconcilation.IsReadOnly, "ReadOnly", "") & "' " _
        '    & " WHERE City_Area_id = " & objBankReconcilation.CityAreaID

        '    ''Execute SQL 
        '    Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


        '    ''SQL Statement Log
        '    objBankReconcilation.ActivityLog.SQLType = "UPDATE"
        '    objBankReconcilation.ActivityLog.TableName = "TblDefCityAreas"
        '    objBankReconcilation.ActivityLog.SQL = strSQL
        '    UtilityDAL.BuildSQLLog(objBankReconcilation.ActivityLog, trans)

        '    ''Activity Log
        '    objBankReconcilation.ActivityLog.FormAction = "Update"
        '    UtilityDAL.BuildActivityLog(objBankReconcilation.ActivityLog, trans)


        '    ''Commit Traction
        '    trans.Commit()

        '    ''Return
        '    Return True


        'Catch ex As SqlException
        '    trans.Rollback()
        '    Throw ex
        'Catch ex As Exception
        '    trans.Rollback()
        '    Throw ex
        'Finally
        '    conn.Close()
        'End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objBankReconcilation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Deleted(ByVal objBankReconcilation As BankReconcilation) As Boolean

        ''Try
        ''    'Check DB Validation, If Not validate then the function returns an exception
        ''    Me.IsValidateForDelete(objCity)

        ''Catch ex As Exception
        ''    'Thorw exception if db validation false
        ''    Throw ex

        ''End Try

        'Dim conn As New SqlConnection(SQLHelper.CON_STR)
        'conn.Open()
        'Dim trans As SqlTransaction = conn.BeginTransaction

        'Try

        '    Dim strSQL As String

        '    strSQL = "DELETE FROM TblDefCityAreas " _
        '    & " WHERE City_Area_id = " & objArea.CityAreaID

        '    ''Execute SQL 
        '    Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))

        '    ''SQL Statement Log
        '    objBankReconcilation.ActivityLog.SQLType = "DELETE"
        '    objBankReconcilation.ActivityLog.TableName = "TblDefCityAreas"
        '    objBankReconcilation.ActivityLog.SQL = strSQL
        '    UtilityDAL.BuildSQLLog(objBankReconcilation.ActivityLog, trans)

        '    ''Activity Log
        '    objBankReconcilation.ActivityLog.FormAction = "Delete"
        '    UtilityDAL.BuildActivityLog(objBankReconcilation.ActivityLog, trans)

        '    ''Commit Traction
        '    trans.Commit()

        '    ''Return
        '    Return True


        'Catch ex As SqlException
        '    trans.Rollback()
        '    Throw ex
        'Catch ex As Exception
        '    trans.Rollback()
        '    Throw ex
        'Finally
        '    conn.Close()

        'End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAll(Optional ByVal strCondition As String = "") As DataTable
        'Dim objDA As SqlClient.SqlDataAdapter
        'Try

        '    Dim strSQL As String
        '    strSQL = "SELECT     city_area_id AS [Area ID], city_id AS [City ID], field_name AS [Area Name], field_code AS [Area Code], sort_order AS [Sort Order],  comments AS Comments, readonly AS [Read Only] " _
        '    & " FROM         tblDefCityAreas  " _
        '    & " ORDER BY [Area Name]"


        '    objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

        '    Dim MyCollectionList As New DataTable(EnumHashTableKeyConstants.GetAreaList.ToString())
        '    objDA.Fill(MyCollectionList)

        '    Return MyCollectionList

        'Catch ex As SqlException
        '    Throw ex
        'Catch ex As Exception
        '    Throw ex
        'Finally
        '    objDA = Nothing
        'End Try

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBanks(Optional ByVal strCondition As String = "") As DataTable

        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String

            ' This Query get all bank accounts.
            strSQL = "SELECT     tblGlCOAMain.main_title, tblGlCOAMainSub.sub_title, tblGlCOAMainSubSub.sub_sub_title, tblGlCOAMainSubSubDetail.detail_title, tblGlCOAMainSubSub.account_type , tblGlCOAMainSubSubDetail.coa_detail_id"
            strSQL = strSQL + " FROM tblGlCOAMain INNER JOIN tblGlCOAMainSub ON tblGlCOAMain.coa_main_id = tblGlCOAMainSub.coa_main_id INNER JOIN tblGlCOAMainSubSub ON tblGlCOAMainSub.main_sub_id = tblGlCOAMainSubSub.main_sub_id INNER JOIN"
            strSQL = strSQL + " tblGlCOAMainSubSubDetail ON tblGlCOAMainSubSub.main_sub_sub_id = tblGlCOAMainSubSubDetail.main_sub_sub_id"
            strSQL = strSQL + " WHERE (tblGlCOAMainSubSub.account_type = 'Bank')  AND (tblGlCOAMainSubSubDetail.end_date IS NULL)"
            strSQL = strSQL + " GROUP BY tblGlCOAMain.main_title, tblGlCOAMainSub.sub_title, tblGlCOAMainSubSub.sub_sub_title, tblGlCOAMainSubSubDetail.detail_title,"
            strSQL = strSQL + " tblGlCOAMainSubSub.account_type , tblGlCOAMainSubSubDetail.coa_detail_id"



            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("DTabBank")
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVoucherDetail(ByVal lngAccId As Integer, ByVal dtpFromDate As DateTime, ByVal dtpToDate As DateTime, _
            Optional ByVal strCondition As String = "", Optional ByVal UnPresented As Boolean = False, Optional ByVal Presented As Boolean = False, _
            Optional ByVal UnCredited As Boolean = False, Optional ByVal Credited As Boolean = False) As DataTable

        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String

            Dim strUnPresented As String
            Dim strPresented As String
            Dim strUnCredited As String
            Dim strCredited As String
            Dim intCounter As Integer

            strSQL = ""
            If UnPresented = True Or Presented = True Or Credited = True Or UnCredited = True Then
                If UnPresented = True Then

                    strUnPresented = " SELECT  case   when (voucher.cheque_paid_date is null) then 'False' else 'True' end AS colChqPaidChq, voucher.voucher_no, voucher.voucher_date, voucher.voucher_id, voucher.cheque_no, voucher.cheque_date, voucher.cheque_paid, voucher.cheque_Credited, voucher.cheque_paid_date , voucher_detail.Dr_amount, voucher_detail.Cr_amount, voucher.voucher_type " & _
                             " FROM (SELECT tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid, tblGlVoucher.cheque_Credited, tblGlVoucher.cheque_paid_date , tblGlVoucher.voucher_id, tblGlDefVoucherType.voucher_type " & _
                             " FROM tblGlVoucher INNER JOIN tblGlDefVoucherType ON tblGlVoucher.voucher_type_id = tblGlDefVoucherType.voucher_type_id " & _
                             " WHERE (tblGlVoucher.post = 1) AND (voucher_no <> '000000') AND (tblGlVoucher.voucher_date BETWEEN '" & Format(dtpFromDate, pbDateFormat) & "' AND '" & Format(dtpToDate, pbDateFormat) & "') AND (Cheque_Paid = 0 )  " & _
                             " GROUP BY tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid_date, tblGlVoucher.voucher_id, tblGlVoucher.cheque_paid, tblGlVoucher.cheque_Credited, tblGlDefVoucherType.voucher_type) voucher INNER JOIN " & _
                             " (SELECT coa_detail_id, voucher_id, SUM(debit_amount) AS Dr_amount, SUM(credit_amount) AS Cr_amount " & _
                             " From tblGlVoucherDetail " & _
                             " Where (coa_detail_id = " & lngAccId & ") and  credit_amount > 0 " & _
                             " GROUP BY coa_detail_id, voucher_id) voucher_detail ON voucher.voucher_id = voucher_detail.voucher_id"

                    strSQL = strUnPresented
                Else
                    strUnPresented = ""
                End If

                '   2.  UnCredited
                If UnCredited = True Then

                    strUnCredited = " SELECT  case   when (voucher.cheque_paid_date is null) then 'False' else 'True' end AS colChqPaidChq, voucher.voucher_no, voucher.voucher_date, voucher.voucher_id, voucher.cheque_no, voucher.cheque_date, voucher.cheque_paid, voucher.cheque_Credited, voucher.cheque_paid_date , voucher_detail.Dr_amount, voucher_detail.Cr_amount, voucher.voucher_type " & _
                         " FROM (SELECT tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid, tblGlVoucher.cheque_Credited, tblGlVoucher.cheque_paid_date , tblGlVoucher.voucher_id, tblGlDefVoucherType.voucher_type " & _
                         " FROM tblGlVoucher INNER JOIN tblGlDefVoucherType ON tblGlVoucher.voucher_type_id = tblGlDefVoucherType.voucher_type_id " & _
                         " WHERE (tblGlVoucher.post = 1) AND (voucher_no <> '000000') AND (tblGlVoucher.voucher_date BETWEEN '" & Format(dtpFromDate, pbDateFormat) & "' AND '" & Format(dtpToDate, pbDateFormat) & "') AND (Cheque_Credited = 0 )  " & _
                         " GROUP BY tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid_date, tblGlVoucher.voucher_id, tblGlVoucher.cheque_paid, tblGlVoucher.cheque_Credited, tblGlDefVoucherType.voucher_type) voucher INNER JOIN " & _
                         " (SELECT coa_detail_id, voucher_id, SUM(debit_amount) AS Dr_amount, SUM(credit_amount) AS Cr_amount " & _
                         " From tblGlVoucherDetail " & _
                         " Where (coa_detail_id = " & lngAccId & ") and  debit_amount > 0 " & _
                         " GROUP BY coa_detail_id, voucher_id) voucher_detail ON voucher.voucher_id = voucher_detail.voucher_id"


                    '========================================================================================================
                    ' make a combined query
                    '========================================================================================================
                    If strSQL <> "" Then

                        strSQL = strSQL & " Union " & strUnCredited
                    Else

                        strSQL = strUnCredited
                    End If

                Else

                    strUnCredited = ""
                End If

                '   3.  Presented
                If Presented = True Then

                    strPresented = " SELECT  case   when (voucher.cheque_paid_date is null) then 'False' else 'True' end AS colChqPaidChq, voucher.voucher_no, voucher.voucher_date, voucher.voucher_id, voucher.cheque_no, voucher.cheque_date, voucher.cheque_paid, voucher.cheque_Credited, voucher.cheque_paid_date , voucher_detail.Dr_amount, voucher_detail.Cr_amount, voucher.voucher_type " & _
                             " FROM (SELECT tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid, tblGlVoucher.cheque_Credited, tblGlVoucher.cheque_paid_date , tblGlVoucher.voucher_id, tblGlDefVoucherType.voucher_type " & _
                             " FROM tblGlVoucher INNER JOIN tblGlDefVoucherType ON tblGlVoucher.voucher_type_id = tblGlDefVoucherType.voucher_type_id " & _
                             " WHERE (tblGlVoucher.post = 1) AND (voucher_no <> '000000') AND (tblGlVoucher.voucher_date BETWEEN '" & Format(dtpFromDate, pbDateFormat) & "' AND '" & Format(dtpToDate, pbDateFormat) & "') AND (Cheque_Paid = 1 )  " & _
                             " GROUP BY tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid_date, tblGlVoucher.voucher_id, tblGlVoucher.cheque_paid, tblGlVoucher.cheque_Credited, tblGlDefVoucherType.voucher_type) voucher INNER JOIN " & _
                             " (SELECT coa_detail_id, voucher_id, SUM(debit_amount) AS Dr_amount, SUM(credit_amount) AS Cr_amount " & _
                             " From tblGlVoucherDetail " & _
                             " Where (coa_detail_id = " & lngAccId & ") and  credit_amount > 0 " & _
                             " GROUP BY coa_detail_id, voucher_id) voucher_detail ON voucher.voucher_id = voucher_detail.voucher_id"

                    '========================================================================================================
                    ' make a combined query
                    '========================================================================================================
                    If strSQL <> "" Then

                        strSQL = strSQL & " Union " & strPresented
                    Else

                        strSQL = strPresented
                    End If

                Else

                    strPresented = ""
                End If

                '   4.  Credited
                If Credited = True Then

                    strCredited = " SELECT  case   when (voucher.cheque_paid_date is null) then 'False' else 'True' end AS colChqPaidChq, voucher.voucher_no, voucher.voucher_date, voucher.voucher_id, voucher.cheque_no, voucher.cheque_date, voucher.cheque_paid, voucher.cheque_Credited, voucher.cheque_paid_date , voucher_detail.Dr_amount, voucher_detail.Cr_amount, voucher.voucher_type " & _
                             " FROM (SELECT tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid, tblGlVoucher.cheque_Credited, tblGlVoucher.cheque_paid_date , tblGlVoucher.voucher_id, tblGlDefVoucherType.voucher_type " & _
                             " FROM tblGlVoucher INNER JOIN tblGlDefVoucherType ON tblGlVoucher.voucher_type_id = tblGlDefVoucherType.voucher_type_id " & _
                             " WHERE (tblGlVoucher.post = 1) AND (voucher_no <> '000000') AND (tblGlVoucher.voucher_date BETWEEN '" & Format(dtpFromDate, pbDateFormat) & "' AND '" & Format(dtpToDate, pbDateFormat) & "') AND (Cheque_Credited = 1 )  " & _
                             " GROUP BY tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid_date, tblGlVoucher.voucher_id, tblGlVoucher.cheque_paid, tblGlVoucher.cheque_Credited, tblGlDefVoucherType.voucher_type) voucher INNER JOIN " & _
                             " (SELECT coa_detail_id, voucher_id, SUM(debit_amount) AS Dr_amount, SUM(credit_amount) AS Cr_amount " & _
                             " From tblGlVoucherDetail " & _
                             " Where (coa_detail_id = " & lngAccId & ") and  debit_amount > 0  " & _
                             " GROUP BY coa_detail_id, voucher_id) voucher_detail ON voucher.voucher_id = voucher_detail.voucher_id"


                    '========================================================================================================
                    ' make a combined query
                    '========================================================================================================
                    If strSQL <> "" Then

                        strSQL = strSQL & " Union " & strCredited
                    Else

                        strSQL = strCredited
                    End If

                Else

                    strCredited = ""
                End If
            Else


                strSQL = " SELECT  case   when (voucher.cheque_paid_date is null) then 'False' else 'True' end AS colChqPaidChq, voucher.voucher_no, voucher.voucher_date, voucher.voucher_id, voucher.cheque_no, voucher.cheque_date, voucher.cheque_paid, voucher.cheque_Credited, voucher.cheque_paid_date , voucher_detail.Dr_amount, voucher_detail.Cr_amount, voucher.voucher_type " & _
                             " FROM (SELECT tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid, tblGlVoucher.cheque_Credited, tblGlVoucher.cheque_paid_date , tblGlVoucher.voucher_id, tblGlDefVoucherType.voucher_type " & _
                             " FROM tblGlVoucher INNER JOIN tblGlDefVoucherType ON tblGlVoucher.voucher_type_id = tblGlDefVoucherType.voucher_type_id " & _
                             " WHERE (tblGlVoucher.post = 1) AND (voucher_no <> '000000') AND (tblGlVoucher.voucher_date BETWEEN '" & Format(dtpFromDate, pbDateFormat) & "' AND '" & Format(dtpToDate, pbDateFormat) & "') " & _
                             " GROUP BY tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid_date, tblGlVoucher.voucher_id, tblGlVoucher.cheque_paid, tblGlVoucher.cheque_Credited, tblGlDefVoucherType.voucher_type) voucher INNER JOIN " & _
                             " (SELECT coa_detail_id, voucher_id, SUM(debit_amount) AS Dr_amount, SUM(credit_amount) AS Cr_amount " & _
                             " From tblGlVoucherDetail " & _
                             " Where (coa_detail_id = " & lngAccId & ")  " & _
                             " GROUP BY coa_detail_id, voucher_id) voucher_detail ON voucher.voucher_id = voucher_detail.voucher_id "
                '"  ORDER BY Voucher_Date,Voucher_type,Voucher_NO  "  'Commented on October 23, 2007, Adeel Iqbal Baloch.

            End If

            'Adeel Iqbal Baloch, October 23, 2007
            If strSQL <> "" Then strSQL = strSQL & " ORDER BY Voucher_Date,Voucher_type,Voucher_NO "



            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("DTabBank")
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVoucherDetail_View(ByVal lngAccId As Integer, _
            ByVal dtpFromDate As DateTime, ByVal dtpToDate As DateTime, _
            Optional ByVal strCondition As String = "") As DataTable

        Dim objDA As SqlClient.SqlDataAdapter

        Try
            Dim m_voucher_type As String = strCondition
            Dim strSql As String = ""

            If m_voucher_type = "" Then


                strSql = " SELECT case   when (voucher.cheque_paid_date is null) then 'False' else 'True' end AS colChqPaidChq, voucher.voucher_no, voucher.voucher_date, voucher.voucher_id, voucher.cheque_no, voucher.cheque_date, voucher.cheque_paid, voucher.cheque_credited, voucher.cheque_paid_date , voucher_detail.Dr_amount, voucher_detail.Cr_amount, voucher.voucher_type " & _
                         " FROM (SELECT tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid, tblGlVoucher.cheque_credited, tblGlVoucher.cheque_paid_date , tblGlVoucher.voucher_id, tblGlDefVoucherType.voucher_type " & _
                         " FROM tblGlVoucher INNER JOIN tblGlDefVoucherType ON tblGlVoucher.voucher_type_id = tblGlDefVoucherType.voucher_type_id " & _
                         " WHERE (tblGlVoucher.post = 1) AND (voucher_no <> '000000') AND (tblGlVoucher.voucher_date BETWEEN '" & Format(dtpFromDate.Date, pbDateFormat) & "' AND '" & Format(dtpToDate.Date, pbDateFormat) & "') AND (Cheque_Paid = 0 OR Cheque_credited = 0 OR tblGlVoucher.cheque_paid_date > '" & Format(dtpToDate.Date, pbDateFormat) & "' )   " & _
                         " GROUP BY tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid_date, tblGlVoucher.voucher_id, tblGlVoucher.cheque_paid,  tblGlVoucher.cheque_credited, tblGlDefVoucherType.voucher_type) voucher INNER JOIN " & _
                         " (SELECT coa_detail_id, voucher_id, SUM(debit_amount) AS Dr_amount, SUM(credit_amount) AS Cr_amount " & _
                         " From tblGlVoucherDetail " & _
                         " Where (coa_detail_id = " & lngAccId & ")  " & _
                         " GROUP BY coa_detail_id, voucher_id) voucher_detail ON voucher.voucher_id = voucher_detail.voucher_id " & _
                         " ORDER BY Voucher_Date,Voucher_type,Voucher_NO"

            Else

                strSql = " SELECT case   when (voucher.cheque_paid_date is null) then 'False' else 'True' end AS colChqPaidChq, voucher.voucher_no, voucher.voucher_date, voucher.voucher_id, voucher.cheque_no, voucher.cheque_date, voucher.cheque_paid, voucher.cheque_paid_date , voucher_detail.Dr_amount, voucher_detail.Cr_amount, voucher.voucher_type " & _
                         " FROM (SELECT tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, " & IIf(m_voucher_type = "Debit_Amount", " tblGlVoucher.cheque_credited  ", " tblGlVoucher.cheque_paid") & " as cheque_paid , tblGlVoucher.cheque_paid_date , tblGlVoucher.voucher_id, tblGlDefVoucherType.voucher_type " & _
                         " FROM tblGlVoucher INNER JOIN tblGlDefVoucherType ON tblGlVoucher.voucher_type_id = tblGlDefVoucherType.voucher_type_id " & _
                         " WHERE (tblGlVoucher.post = 1) AND (voucher_no <> '000000') AND (tblGlVoucher.voucher_date BETWEEN '" & Format(dtpFromDate.Date, pbDateFormat) & "' AND '" & Format(dtpToDate.Date, pbDateFormat) & "') AND (" & IIf(m_voucher_type = "Debit_Amount", "cheque_credited", " cheque_paid ") & "= 0  OR tblGlVoucher.cheque_paid_date > '" & Format(dtpToDate.Date, pbDateFormat) & "' )   " & _
                         " GROUP BY tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid_date, tblGlVoucher.voucher_id, tblGlVoucher.cheque_paid, tblGlVoucher.cheque_credited, tblGlDefVoucherType.voucher_type) voucher INNER JOIN " & _
                         " (SELECT coa_detail_id, voucher_id, SUM(debit_amount) AS Dr_amount, SUM(credit_amount) AS Cr_amount " & _
                         " From tblGlVoucherDetail " & _
                         " Where (coa_detail_id = " & lngAccId & ") and  " & m_voucher_type & " > 0 " & _
                         " GROUP BY coa_detail_id, voucher_id) voucher_detail ON voucher.voucher_id = voucher_detail.voucher_id " & _
                         " ORDER BY Voucher_Date,Voucher_type,Voucher_NO"


            End If




            objDA = New SqlClient.SqlDataAdapter(strSql, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("DTabBank")
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


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLedgerBalance(ByVal lngAccId As Integer, _
            ByVal dtpFromDate As DateTime, ByVal dtpToDate As DateTime, _
            Optional ByVal strCondition As String = "") As Long

        Dim objDA As SqlClient.SqlDataAdapter

        Try

            Dim strSql As String = "SELECT (ISNULL(sum(D.Debit_Amount),0) - ISNULL(sum(D.Credit_Amount),0)) AS [Ledger_Balance] FROM tblglvoucherDetail D INNER JOIN tblglVoucher M ON D.Voucher_ID = M.Voucher_ID WHERE Post = 1 AND Voucher_No <> '000000' AND D.coa_detail_id  = '" & lngAccId & "' AND  M.Voucher_Date <= '" & Format(dtpToDate.Date, pbDateFormat) & "'  "

            strSql = strSql + " and (M.Other_Voucher = 0)" 'CR#169 Exclude Other voucher


            objDA = New SqlClient.SqlDataAdapter(strSql, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("DTabBank")
            objDA.Fill(MyCollectionList)

            If MyCollectionList.Rows.Count > 0 Then
                Return MyCollectionList.Rows(0).Item(0).ToString
            Else
                Return 0
            End If

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function
    'CR#319 this method will return opening balance of a bank i.e. All Cheques which have been paid before selected from date
    Public Function GetBankOpeningBalance(ByVal lngAccId As Integer, _
                ByVal dtpFromDate As DateTime, ByVal dtpToDate As DateTime, _
                Optional ByVal strCondition As String = "") As Long

        Dim objDA As SqlClient.SqlDataAdapter

        Try

            'Dim strSql As String = "SELECT (ISNULL(sum(D.Debit_Amount),0) - ISNULL(sum(D.Credit_Amount),0)) AS [Ledger_Balance] FROM tblglvoucherDetail D INNER JOIN tblglVoucher M ON D.Voucher_ID = M.Voucher_ID WHERE Post = 1 AND Voucher_No <> '000000' AND D.coa_detail_id  = '" & lngAccId & "' AND  M.cheque_Paid_date <= '" & Format(dtpToDate.Date, pbDateFormat) & "'  "
            Dim strSql As String = "SELECT (ISNULL(sum(D.Debit_Amount),0) - ISNULL(sum(D.Credit_Amount),0)) AS [Ledger_Balance] FROM tblglvoucherDetail D INNER JOIN tblglVoucher M ON D.Voucher_ID = M.Voucher_ID WHERE Post = 1 AND Voucher_No <> '000000' AND D.coa_detail_id  = '" & lngAccId & "' AND  M.cheque_Paid_date <= '" & Format(dtpFromDate.Date, pbDateFormat) & "'  "

            strSql = strSql + " and (M.Other_Voucher = 0)" 'CR#169 Exclude Other voucher


            objDA = New SqlClient.SqlDataAdapter(strSql, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("DTabBank")
            objDA.Fill(MyCollectionList)

            If MyCollectionList.Rows.Count > 0 Then
                Return MyCollectionList.Rows(0).Item(0).ToString
            Else
                Return 0
            End If

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUnPresented(ByVal lngAccId As Integer, _
            ByVal dtpFromDate As DateTime, ByVal dtpToDate As DateTime, _
            Optional ByVal strCondition As String = "") As Long

        Dim objDA As SqlClient.SqlDataAdapter

        Try
            'CR#319
            Dim strSql As String = "SELECT ABS(ISNULL(sum(D.Credit_Amount),0))  AS [UnPresented_Amount] FROM tblglvoucherDetail D INNER JOIN tblglVoucher M ON D.Voucher_ID = M.Voucher_ID WHERE Post = 1 AND Voucher_No <> '000000' AND D.coa_detail_id  = '" & lngAccId & "' AND (Cheque_Paid = 0  or cheque_Paid_date > '" & Format(dtpToDate.Date, pbDateFormat) & "' )  AND M.Voucher_Date <= '" & Format(dtpToDate.Date, pbDateFormat) & "'  "
            'Dim strSql As String = "SELECT ABS(ISNULL(sum(D.Credit_Amount),0))  AS [UnPresented_Amount] FROM tblglvoucherDetail D INNER JOIN tblglVoucher M ON D.Voucher_ID = M.Voucher_ID WHERE Post = 1 AND Voucher_No <> '000000' AND D.coa_detail_id  = '" & lngAccId & "' AND (Cheque_Paid = 0  or cheque_Paid_date > '" & Format(dtpToDate.Date, pbDateFormat) & "' ) "
            strSql = strSql + " and (M.Other_Voucher = 0)" 'CR#169 Exclude Other voucher"


            objDA = New SqlClient.SqlDataAdapter(strSql, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("DTabBank")
            objDA.Fill(MyCollectionList)

            If MyCollectionList.Rows.Count > 0 Then
                Return MyCollectionList.Rows(0).Item(0).ToString
            Else
                Return 0
            End If

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUnCredited(ByVal lngAccId As Integer, _
            ByVal dtpFromDate As DateTime, ByVal dtpToDate As DateTime, _
            Optional ByVal strCondition As String = "") As Long

        Dim objDA As SqlClient.SqlDataAdapter

        Try

            'CR#319
            Dim strSql As String = "SELECT ABS(ISNULL(sum(D.Debit_Amount),0))  AS [UnCredited_Amount] FROM tblglvoucherDetail D INNER JOIN tblglVoucher M ON D.Voucher_ID = M.Voucher_ID WHERE Post = 1 AND Voucher_No <> '000000' AND D.coa_detail_id  = '" & lngAccId & "' AND (Cheque_Credited = 0  or cheque_Paid_date > '" & Format(dtpToDate.Date, pbDateFormat) & "' ) AND M.Voucher_Date <= '" & Format(dtpToDate.Date, pbDateFormat) & "'  "
            'Dim strSql As String = "SELECT ABS(ISNULL(sum(D.Debit_Amount),0))  AS [UnCredited_Amount] FROM tblglvoucherDetail D INNER JOIN tblglVoucher M ON D.Voucher_ID = M.Voucher_ID WHERE Post = 1 AND Voucher_No <> '000000' AND D.coa_detail_id  = '" & lngAccId & "' AND (Cheque_Credited = 0  or cheque_Paid_date > '" & Format(dtpToDate.Date, pbDateFormat) & "' )"
            strSql = strSql + " and (M.Other_Voucher = 0)" 'CR#169 Exclude Other voucher


            objDA = New SqlClient.SqlDataAdapter(strSql, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("DTabBank")
            objDA.Fill(MyCollectionList)

            If MyCollectionList.Rows.Count > 0 Then
                Return MyCollectionList.Rows(0).Item(0).ToString
            Else
                Return 0
            End If

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function

    'Cr # 304

    Public Function GetBankStatementReport(ByVal lngAccId As Integer, ByVal dtpFromDate As DateTime, ByVal dtpToDate As DateTime)
        Dim objDA As SqlClient.SqlDataAdapter
        Try
            Dim strSQL As String = String.Empty
            strSQL = " SELECT   voucher.voucher_no, voucher.voucher_date, voucher.voucher_id, voucher.cheque_no, voucher.cheque_date, voucher.cheque_paid, voucher.cheque_Credited, voucher.cheque_paid_date , voucher_detail.Dr_amount, voucher_detail.Cr_amount, voucher.voucher_type " & _
                                         " FROM (SELECT tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid, tblGlVoucher.cheque_Credited, tblGlVoucher.cheque_paid_date , tblGlVoucher.voucher_id, tblGlDefVoucherType.voucher_type " & _
                                         " FROM tblGlVoucher INNER JOIN tblGlDefVoucherType ON tblGlVoucher.voucher_type_id = tblGlDefVoucherType.voucher_type_id " & _
                                         " WHERE (tblGlVoucher.post = 1) AND (voucher_no <> '000000') AND (tblGlVoucher.cheque_paid_date BETWEEN '" & Format(dtpFromDate, pbDateFormat) & "' AND '" & Format(dtpToDate, pbDateFormat) & "') " & _
                                         " GROUP BY tblGlVoucher.voucher_no, tblGlVoucher.voucher_date, tblGlVoucher.cheque_no, tblGlVoucher.cheque_date, tblGlVoucher.cheque_paid_date, tblGlVoucher.voucher_id, tblGlVoucher.cheque_paid, tblGlVoucher.cheque_Credited, tblGlDefVoucherType.voucher_type) voucher INNER JOIN " & _
                                         " (SELECT coa_detail_id, voucher_id, SUM(debit_amount) AS Dr_amount, SUM(credit_amount) AS Cr_amount " & _
                                         " From tblGlVoucherDetail " & _
                                         " Where (coa_detail_id = " & lngAccId & ")  " & _
                                         " GROUP BY coa_detail_id, voucher_id) voucher_detail ON voucher.voucher_id = voucher_detail.voucher_id "
            strSQL = strSQL & " ORDER BY cheque_paid_date,Voucher_type,Voucher_NO "

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("DTabBank")
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

#End Region

End Class
