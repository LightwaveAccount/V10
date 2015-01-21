Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility

''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// 30 Dec,2014       M.Shoaib          CR#350.     Trial Balance: Implement Cost Center filter on trial balance reports

Public Class TrialBalanceDAL
    Public Shared Sub GenerateTrialBalance(ByVal strProcedure As String, ByVal strAlterViewTrialOpening As String, ByVal strAlterViewTrialPeriod As String, Optional ByVal isCostCenter As Boolean = False) ' CR # 350, Added new parameter isCostCenter

        Dim objConn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction

        objConn.Open()
        trans = objConn.BeginTransaction

        Try
            Dim strSQL As String


            ' Exeucting Procedure .. 
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strProcedure, Nothing)

            ' Altering View GL TrailOpening .. 
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strAlterViewTrialOpening, Nothing)

            ' Altering View GL ViewTrailPeriod .. 
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strAlterViewTrialPeriod, Nothing)

            strSQL = "DELETE FROM TblrptGlTrailBalance"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)


            'strSQL = "SELECT main_code, main_title, main_type, sub_code, sub_title, sub_sub_code ,sub_sub_title ,account_type ,detail_code ,detail_title ,Opening_Debit_Amount ,Opening_Credit_Amount ,OpeningBalance ,Debit_Amount ,Credit_Amount ,Balance ,ClosingBalance ,post  FROM vwGlTrailBalance"

            'Dim objDA As SqlClient.SqlDataAdapter

            'objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            'Dim MyCollectionList As New DataTable("Voucher")
            'objDA.Fill(MyCollectionList)
            'Dim dt As DataTable
            'dt = MyCollectionList

            ' CR # 350,

            If isCostCenter Then
                strSQL = " sp_TrialBalanceCostCenter "
            Else
                strSQL = " INSERT INTO TblrptGlTrailBalance (main_code, main_title, main_type, sub_code, sub_title, sub_sub_code ,sub_sub_title ,account_type ,detail_code ,detail_title ,Opening_Debit_Amount ,Opening_Credit_Amount ,OpeningBalance ,Debit_Amount ,Credit_Amount ,Balance ,ClosingBalance ,post ) " _
                                   & "SELECT main_code, main_title, main_type, sub_code, sub_title, sub_sub_code ,sub_sub_title ,account_type ,detail_code ,detail_title ,Opening_Debit_Amount ,Opening_Credit_Amount ,OpeningBalance ,Debit_Amount ,Credit_Amount ,Balance ,ClosingBalance ,post  FROM vwGlTrailBalance"
            End If
            ' CR # 350 End




            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)


            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex

        End Try
    End Sub
End Class
