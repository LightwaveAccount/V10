
''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Cash Flow Report .. 
''// Programmer	     : R@! Shahid
''// Creation Date	 : 20-July-2009
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


Public Class RptCashFlowDal

    Function InsertDataForReport(Optional ByVal Condition As String = "") As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction

        Dim strSQL As String = ""

        Try

            If Condition = "Stander" Then

                strSQL = " Delete from TblrptGLCashFlowStander "
                Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


                strSQL = "Insert Into TblrptGLCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post ) " & _
                              "Select Tr_Type ,Tr_Type + 1, coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post  from vwGLCashFlowPeriodRPT"

                Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


                ' Commit Transaction .. 
                trans.Commit()

            Else

                strSQL = " Delete from TblrptGLCashFlow "
                Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


                strSQL = "Insert Into TblrptGLCashFlow (Tr_Type ,coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post ) " & _
                              "Select Tr_Type ,coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post  from vwGLCashFlowPeriodRPT"

                Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


                ' Commit Transaction .. 
                trans.Commit()

            End If
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

    Function InsertOpenings() As Boolean
        Dim Dt As DataTable
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction

        Dim strSQL As String = ""

        Try
            strSQL = "SELECT     tblGlCOAMainSubSubDetail.coa_detail_id, tblGlCOAMainSubSubDetail.detail_title, dbo.tblGlCOAMainSubSub.account_type " & _
                        "FROM         dbo.tblGlCOAMainSubSubDetail AS tblGlCOAMainSubSubDetail INNER JOIN " & _
                        "dbo.tblGlCOAMainSubSub ON tblGlCOAMainSubSubDetail.main_sub_sub_id = dbo.tblGlCOAMainSubSub.main_sub_sub_id " & _
                        "WHERE     (dbo.tblGlCOAMainSubSub.account_type IN ('Cash', 'Bank'))"

            Dt = UtilityDAL.GetDataTable(strSQL).Copy
            strSQL = String.Empty

            If Dt.Rows.Count > 0 Then
      

                strSQL = "Insert Into TblrptGLCashFlowStander (Tr_Type ,Sort ,coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post ) " & _
                              "Select Tr_Type ,1, coa_detail_id ,Voucher_Date ,Cheque_no ,Cheque_date ,Comments ,detail_title ,debit_amount ,credit_amount ,post  from vwGLCashFlowPeriodRPT"

                Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


                ' Commit Transaction .. 
                trans.Commit()
            End If

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
        Return True
    End Function
End Class

