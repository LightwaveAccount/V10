Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.VisualBasic
Imports Microsoft.SqlServer.Replication
Imports SQLMERGXLib

Public Class DBBackupDAL

#Region "Private Variables"
    Private _con As SqlConnection
    Private objSQLClient As Server
    Dim sqlConnectionString As String
#End Region

#Region "Public Functions and Procedures"
    Public Function ConnectToServer(ByVal strServerName As String, ByVal strUsername As String, ByVal strPassword As String) As Boolean
        Try
            sqlConnectionString = "Password=" & strPassword & _
                                  ";Persist Security Info=True;User ID=" & strUsername _
                                  & ";Initial Catalog=master;Data Source=" & strServerName & ""
            Dim connection As New SqlConnection(sqlConnectionString)
            objSQLClient = New Server(New ServerConnection(connection))

            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Public Function GetDBList() As DataTable
        'CR#38
        'Dim objDA As SqlDataAdapter
        'Try
        '    Dim strSQL As String
        '    strSQL = "SELECT [name] FROM master.dbo.sysdatabases WHERE dbid > 6"

        '    Dim dt As New DataTable
        '    objDA = New SqlDataAdapter(strSQL, sqlConnectionString)
        '    objDA.Fill(dt)

        '    Return dt
        'Catch ex As Exception
        '    Throw ex
        'Finally
        '    objDA = Nothing
        'End Try
        ''Dim objDA As SqlDataAdapter
        Try
            ''Dim strSQL As String
            ''strSQL = "SELECT [name] FROM master.dbo.sysdatabases WHERE cmptlevel = 80"

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("name"))
            ''objDA = New SqlDataAdapter(strSQL, sqlConnectionString)
            ''objDA.Fill(dt)

            For Each db As Database In objSQLClient.Databases
                If Not db.IsSystemObject Then
                    Dim dr As DataRow = dt.NewRow
                    dr("name") = db.Name
                    dt.Rows.Add(dr)
                End If
            Next

            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            ''objDA = Nothing
        End Try
    End Function

    Public Function GetDefaultBackupLocation() As String
        Return ""
    End Function

    Public Function BackupDatabase(ByVal strUsername As String, ByVal strPassword As String, ByVal strDatabaseName As String, _
                                   ByVal strDBServerName As String, ByVal strPath As String) As Boolean
        Try
            _con = New SqlConnection("Password=" & strPassword & ";Persist Security Info=True;" _
                                     & "User ID=" & strUsername & ";Initial Catalog=" & strDatabaseName _
                                     & ";Data Source=" & strDBServerName)

            Dim objCommand As New SqlCommand
            objCommand.CommandText = "backup database " & strDatabaseName & " to disk='" & strPath & "'"
            _con.Open()
            objCommand.Connection = _con
            objCommand.ExecuteNonQuery()

            _con.Close()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RestoreDatabase(ByVal strUsername As String, ByVal strPassword As String, ByVal strDatabaseName As String, _
                                      ByVal strDBServerName As String, ByVal strPath As String) As Boolean
        Try
            _con = New SqlConnection("Provider=SQLOLEDB.1;Password=sa;Persist Security Info=True;" _
                                     & "User ID=" & strUsername & ";Initial Catalog=" & strDatabaseName _
                                     & ";Data Source=" & strDBServerName)

            Dim objCommand As New SqlCommand
            objCommand.CommandText = "restore database " & strDatabaseName & " from disk='" & strPath & "'"
            _con.Open()
            objCommand.Connection = _con
            objCommand.ExecuteNonQuery()

            _con.Close()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
