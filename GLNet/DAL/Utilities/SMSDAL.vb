Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.VisualBasic

Public Class SMSDAL

    Public Function GetShopList(Optional ByVal Condition As String = "") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try
            Dim strSQL As String = "SELECT shop_id ,shop_name FROM tblDefShops  "
            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable(EnumHashTableKeyConstants.GetCityList.ToString)
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
End Class
