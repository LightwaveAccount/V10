''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Daily Activity Report .. 
''// Programmer	     : Tariq Majeed
''// Creation Date	 : 17-July-2009
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////


Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility


Public Class GLVoucherDAL

    Function InsertDataForReport() As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction

        Dim strSQL As String = ""

        Try


            strSQL = " Delete from TblrptGlVoucher "
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            strSQL = " Insert Into TblrptGlVoucher (voucher_code, finiancial_year_id, voucher_type_id, location_id, voucher_no, voucher_date, location_name, paid_to, cheque_no, cheque_date, cheque_paid, cheque_paid_date, post, main_code, main_title, main_type, sub_code, sub_title, sub_sub_code, sub_sub_title, account_type, cost_center_id, cost_center_title, detail_code, detail_title, Comments, debit_amount, credit_amount) " _
                   & " Select voucher_code, finiancial_year_id, voucher_type_id, location_id, voucher_no, voucher_date, location_name, paid_to, cheque_no, cheque_date, cheque_paid, cheque_paid_date, post, main_code, main_title, main_type, sub_code, sub_title, sub_sub_code, sub_sub_title, account_type, cost_center_id, cost_center_title, detail_code, detail_title, Comments, debit_amount, credit_amount from vwGlVouchers "
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
