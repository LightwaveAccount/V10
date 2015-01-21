Imports DAL
Imports System.Data.SqlClient
Imports Utility.Utility

Public Class DALReports

    Public Function ExecuteQuery(ByVal strSql As String, Optional ByVal strCondition As String = "") As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            'Dim strSQL As String

            'strSQL = "DELETE FROM TblDefCityAreas " _
            '& " WHERE City_Area_id = " & objArea.CityAreaID

            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


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
    Public Function GetDataTable(ByVal strSql As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim ObjCon As SqlClient.SqlConnection
        Dim objDA As SqlClient.SqlDataAdapter
        Dim Objcmd As SqlClient.SqlCommand

        'ObjCon = New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If trans Is Nothing Then
            ObjCon = New SqlClient.SqlConnection(SQLHelper.CON_STR)
        Else
            ObjCon = trans.Connection
        End If

        Objcmd = New SqlClient.SqlCommand(strSql)

        Try

            Objcmd.CommandTimeout = 9000
            Objcmd.Connection = ObjCon
            Objcmd.Transaction = trans

            objDA = New SqlClient.SqlDataAdapter
            objDA.SelectCommand = Objcmd

            Dim MyCollectionList As New DataTable
            objDA.Fill(MyCollectionList)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
            If trans Is Nothing Then
                If ObjCon.State = ConnectionState.Open Then ObjCon.Close()
                ObjCon.Dispose()
            End If

        End Try
    End Function
    Public Function ReturnDataRow(ByVal strSql As String, Optional ByVal trans As SqlTransaction = Nothing) As DataRow
        Dim Objcmd As SqlClient.SqlCommand
        Dim objDA As New SqlDataAdapter
        Dim objDS As New DataSet
        Dim ObjCon As SqlClient.SqlConnection

        If trans Is Nothing Then
            ObjCon = New SqlClient.SqlConnection(SQLHelper.CON_STR)
        Else
            ObjCon = trans.Connection
        End If
        Objcmd = New SqlClient.SqlCommand(strSql)
        Try

            'Dim trans As SqlTransaction = conn.BeginTransaction
            Objcmd.Connection = ObjCon
            Objcmd.CommandType = CommandType.Text
            Objcmd.CommandText = strSql
            Objcmd.Transaction = trans
            objDA.SelectCommand = Objcmd
            objDA.Fill(objDS) '

            If objDS.Tables(0).Rows.Count > 0 Then
                Return objDS.Tables(0).Rows(0)
            Else
                Return Nothing
            End If
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
            If trans Is Nothing Then
                If ObjCon.State = ConnectionState.Open Then ObjCon.Close()
                ObjCon.Dispose()
            End If
        End Try

    End Function
    '//Commented by Abdul Jabbar ..Below function result to Timeout Expire exception on heavy queries,Also Connect timout is not functioning here
    'Public Function GetDataTable(ByVal strSql As String) As DataTable
    '    Dim objDA As SqlClient.SqlDataAdapter
    '    Try

    '        objDA = New SqlClient.SqlDataAdapter(strSql, SQLHelper.CON_STR)

    '        Dim MyCollectionList As New DataTable
    '        objDA.Fill(MyCollectionList)

    '        Return MyCollectionList

    '    Catch ex As SqlException
    '        Throw ex
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objDA = Nothing
    '    End Try
    'End Function
 


 
End Class

