Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility

Public Class ActivityReportDAL

    Function InsertDataForReport() As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction

        Dim strSQL As String = ""

        Try


            strSQL = " Delete from TblrptDailyActivity "
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            strSQL = " Insert Into TblrptDailyActivity (voucher_code, voucher_type, debit_amount, credit_amount, voucher_month, year_code, voucher_date, VDescription, detail_title, detail_code, coa_detail_id, Status, location_code, location_name) " _
                   & " Select voucher_code, voucher_type, debit_amount, credit_amount, voucher_month, year_code, voucher_date, VDescription, detail_title, detail_code, coa_detail_id, Status, location_code, location_name from vwDailyActivityReport "
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            ' Commit Transaction .. 
            trans.Commit()

            Return True
        Catch ex As SqlException
            trans.Rollback()
            Return False
            Throw ex

        Catch ex As Exception
            trans.Rollback()
            Return False
            Throw ex

        Finally
            conn.Close()

        End Try

    End Function


End Class
