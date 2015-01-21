Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility

Public Class PostDatedChequesDAL

    Public Function GetCompanies(ByVal intUserId As Integer) As DataTable
        Dim objDA As SqlDataAdapter
        Try
            Dim strSQL As String

            strSQL = "SELECT '[' + location_code + '] ' + location_name AS Location, tblGlDefLocation.location_id " _
                   & " FROM tblGlDefLocation " _
                   & " INNER JOIN tblGlSecurityUserLocation on tblGlDefLocation.Location_Id= tblGlSecurityUserLocation.Location_Id " _
                   & " WHERE User_Id= " & intUserId

            Dim dt As New DataTable
            objDA = New SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            objDA.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try
    End Function

    Public Function GetBanks() As DataTable
        Dim objDA As SqlDataAdapter
        Try
            Dim strSQL As String

            strSQL = "SELECT detail_title AS [Bank Name], coa_detail_id AS [COA Detail ID]" _
                    & " FROM dbo.tblGlCOAMainSubSubDetail " _
                    & " INNER JOIN dbo.tblGlCOAMainSubSub ON dbo.tblGlCOAMainSubSubDetail.main_sub_sub_id = dbo.tblGlCOAMainSubSub.main_sub_sub_id " _
                    & " WHERE (dbo.tblGlCOAMainSubSub.account_type = 'Bank')"

            Dim dt As New DataTable
            objDA = New SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            objDA.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try
    End Function
End Class
