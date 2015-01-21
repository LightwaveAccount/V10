Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility

Public Class CBOCollectionDAL
#Region "Public Functions and Procedures"

    Public Shared Function GetServerDateTime() As Date
        Try
            Dim strSQL As String = "Select GetDate()"
            Using dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)
                dr.Read()
                Return dr.GetDateTime(0)
            End Using
        Catch ex As SqlClient.SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetMushroomCommonList(Optional ByVal strCondition As String = "") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            strSQL = " SELECT [mushroom_common_id] [Mushroom Common ID],[expiry_service] [Expiry Service]," _
            & " [membership_fee] [MemberShip Fee],[renewal_fee] [Renewal Fee],[membership_card_expiry] " _
            & " [Member Ship Card Expiry] ,[psr_expiry] [PSR Expiry],[shop_priority_date]" _
            & " [Shop Priority Date],[shop_priority_validity] [Shop Priority Validity]," _
            & " [membership_duration] [Member Ship Duration],[Membership_Signature] [Member Ship Signature]," _
            & " LM ,TM ,[label_type] [Label Type],[production_product_detail] [Production Product Detail]," _
            & " [STR_product_name_report] [STR Product Name Report],[replication_status]" _
            & " [Replication Status] " _
            & " FROM tblMushroomCommon" _
            & " ORDER BY [Mushroom Common ID] DESC"

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable(EnumHashTableKeyConstants.GetMushroomCommonList.ToString())
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
