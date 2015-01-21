Imports System.Data.SqlClient

Public Class SQLHelper


    Public Shared CON_STR As String = System.Configuration.ConfigurationManager.ConnectionStrings("CON_STR").ConnectionString


#Region "Without Transactions "

    ''Public Shared Function ExecuteNonQuery(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters As SqlParameter()) As Boolean

    ''    Dim cmd As New SqlCommand()
    ''    Using conn As SqlConnection = New SqlConnection(connectionString)

    ''        Try


    ''            PrepareCommand(cmd, conn, Nothing, commandType, commandText, commandParameters)
    ''            Dim Val As Integer = cmd.ExecuteNonQuery()
    ''            cmd.Parameters.Clear()
    ''            conn.Dispose()
    ''            Return Val

    ''        Catch ex As Exception
    ''            Throw ex
    ''        End Try
    ''    End Using


    ''End Function

    ''Public Shared Function ExecuteScaler(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters() As SqlParameter) As Object

    ''    Dim cmd As New SqlCommand()

    ''    Using conn As SqlConnection = New SqlConnection(connectionString)


    ''        PrepareCommand(cmd, conn, Nothing, commandType, commandText, commandParameters)
    ''        Dim Val As Integer = cmd.ExecuteScalar
    ''        cmd.Parameters.Clear()
    ''        conn.Dispose()
    ''        Return Val

    ''    End Using

    ''End Function

#End Region


#Region "With Transactions "


    Private Shared Sub PrepareCommand(ByVal cmd As SqlCommand, ByVal conn As SqlConnection, ByVal trans As SqlTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms() As SqlParameter)
        Try


            If (conn.State <> ConnectionState.Open) Then conn.Open()

            cmd.Connection = conn
            cmd.CommandText = cmdText
            cmd.CommandTimeout = 500

            If Not (trans Is Nothing) Then cmd.Transaction = trans

            cmd.CommandType = cmdType

            If Not (cmdParms Is Nothing) Then
                For Each parm As SqlParameter In cmdParms
                    cmd.Parameters.Add(parm)
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function ExecuteNonQuery(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters As SqlParameter()) As Boolean

        Dim cmd As New SqlCommand()
        Using conn As SqlConnection = New SqlConnection(connectionString)

            Try
                PrepareCommand(cmd, conn, Nothing, commandType, commandText, commandParameters)
                Dim Val As Integer = cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                conn.Dispose()
                Return Val
            Catch ex As Exception
                Throw ex
            End Try
        End Using


    End Function

    Public Shared Function ExecuteNonQuery(ByVal trans As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters As SqlParameter(), Optional ByRef RecrodAffected As Long = 0) As Boolean
        Try
            Dim cmd As New SqlCommand()
            PrepareCommand(cmd, trans.Connection, trans, commandType, commandText, commandParameters)
            Dim Val As Integer = cmd.ExecuteNonQuery()
            RecrodAffected = Val
            cmd.Parameters.Clear()
            Return Val
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function ExecuteScaler(ByVal trans As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters() As SqlParameter) As Object
        Try
            Dim cmd As New SqlCommand()
            PrepareCommand(cmd, trans.Connection, trans, commandType, commandText, commandParameters)
            Dim Val As Object = cmd.ExecuteScalar
            cmd.Parameters.Clear()
            If IsDBNull(Val) Then Val = Nothing
            Return Val
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function ExecuteScaler(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters() As SqlParameter) As Object

        Dim cmd As New SqlCommand()

        Using conn As SqlConnection = New SqlConnection(connectionString)


            PrepareCommand(cmd, conn, Nothing, commandType, commandText, commandParameters)
            Dim Val As Object = cmd.ExecuteScalar
            cmd.Parameters.Clear()
            conn.Dispose()
            Return Val

        End Using

    End Function


    Public Shared Function ExecuteReader(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters() As SqlParameter) As SqlDataReader

        Dim cmd As SqlCommand = New SqlCommand
        Dim conn As SqlConnection = New SqlConnection(connectionString)

        Try

            PrepareCommand(cmd, conn, Nothing, commandType, commandText, commandParameters)
            Dim rdr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            cmd.Parameters.Clear()
            Return rdr

        Catch ex As Exception
            conn.Close()
            Throw ex

        End Try

    End Function

    Public Shared Function ExecuteReader(ByVal Trans As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters() As SqlParameter) As SqlDataReader

        Dim cmd As SqlCommand = New SqlCommand
        cmd.Connection = Trans.Connection ' New SqlConnection(SQLHelper.CON_STR)
        cmd.Transaction = Trans
        'Dim conn As SqlConnection = New SqlConnection(connectionString)

        Try

            PrepareCommand(cmd, cmd.Connection, Trans, commandType, commandText, commandParameters)
            Dim rdr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            cmd.Parameters.Clear()
            Return rdr

        Catch ex As Exception
            'conn.Close()
            Throw ex

        End Try

    End Function

    Public Shared Function CreateParameter(ByVal name As String, ByVal dbType As SqlDbType, ByVal value As Object) As SqlParameter
        Try
            Dim prm As SqlParameter = New SqlParameter(name, dbType)
            prm.Value = IIf(value Is Nothing, DBNull.Value, value)
            Return prm
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region



End Class
