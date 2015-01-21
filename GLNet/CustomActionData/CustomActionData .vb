Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.IO
Imports System.Reflection
Imports System
Imports System.Management
Imports System.Text
Imports Microsoft.Win32

Public Class CustomActionData

    Dim sqlConnection1 As New SqlClient.SqlConnection("Integrated Security=SSPI;Persist Security Info=False;User ID=sa;Initial Catalog=master;Data Source=.\sqlExpress")

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent

    End Sub

    Private Function GetSql(ByVal Name As String) As String
        Try

            ' Gets the current assembly.
            Dim Asm As [Assembly] = [Assembly].GetExecutingAssembly()

            ' Resources are named using a fully qualified name.
            Dim strm As Stream = Asm.GetManifestResourceStream(Asm.GetName().Name + "." + Name)

            ' Reads the contents of the embedded file.
            Dim reader As StreamReader = New StreamReader(strm)
            Return reader.ReadToEnd()
        Catch ex As Exception
            MsgBox("In GetSQL: " & ex.Message)
            Throw ex
        End Try

    End Function

    Private Function WriteConnectionString(ByVal InstanceName As String) As String

        Dim strBuilder As New StringBuilder
        'Dim strConn As String = Utility.Utility.SymmetricEncryption.Encrypt("Password=lumensoft2003;Persist Security Info=True;User ID=sa;Initial Catalog=" & Me.Context.Parameters.Item("dbname") & ";Data Source=" & System.Environment.MachineName.ToString & "\" & InstanceName & "", "f")
        Dim strConn As String = "Password=lumensoft2003;Persist Security Info=True;User ID=sa;Initial Catalog=" & Me.Context.Parameters.Item("dbname") & ";Data Source=" & System.Environment.MachineName.ToString & "\" & InstanceName & ""
        strBuilder.AppendLine("<?xml version=""1.0"" encoding=""utf-8"" ?>")
        strBuilder.AppendLine("<configuration>")
        strBuilder.AppendLine("	<connectionStrings>")
        ' strBuilder.AppendLine("    <add connectionString=""Password=lumensoft2003;Persist Security Info=True;User ID=sa;Initial Catalog=" & Me.Context.Parameters.Item("dbname") & ";Data Source=" & System.Environment.MachineName.ToString & "\" & InstanceName & """ name=""CON_STR""/>")
        strBuilder.AppendLine("    <add connectionString=""" & strConn & """ name=""CON_STR""/>")
        strBuilder.AppendLine("  </connectionStrings>")
        strBuilder.AppendLine("  <appSettings>")
        strBuilder.AppendLine("    <add key=""StrUserName""  value=""sa""/>")
        strBuilder.AppendLine("    <add key=""StrPassword"" value=""lumensoft2003""/>")
        strBuilder.AppendLine("    <add key=""StrDBServerName"" value=""" & System.Environment.MachineName & "\" & InstanceName & """/>")
        strBuilder.AppendLine("    <add key=""StrDBName"" value=""" & Me.Context.Parameters.Item("dbname") & """/>")
        strBuilder.AppendLine("    <add key=""ReportPath"" value=""\Candela New\Candela New\Candela\Reports""/>")
        strBuilder.AppendLine("")
        strBuilder.AppendLine("  </appSettings>	")
        strBuilder.AppendLine("    <system.diagnostics>")
        strBuilder.AppendLine("        <sources>")
        strBuilder.AppendLine("            <!-- This section defines the logging configuration for My.Application.Log -->")
        strBuilder.AppendLine("            <source name=""DefaultSource"" switchName=""DefaultSwitch"">")
        strBuilder.AppendLine("                <listeners>")
        strBuilder.AppendLine("                    <add name=""FileLog""/>")
        strBuilder.AppendLine("                    <!-- Uncomment the below section to write to the Application Event Log -->")
        strBuilder.AppendLine("                    <!--<add name=""EventLog""/>-->")
        strBuilder.AppendLine("                </listeners>")
        strBuilder.AppendLine("            </source>")
        strBuilder.AppendLine("        </sources>")
        strBuilder.AppendLine("        <switches>")
        strBuilder.AppendLine("            <add name=""DefaultSwitch"" value=""Information"" />")
        strBuilder.AppendLine("        </switches>")
        strBuilder.AppendLine("        <sharedListeners>")
        strBuilder.AppendLine("            <add name=""FileLog""")
        strBuilder.AppendLine("        type = ""Microsoft.VisualBasic.Logging.FileLogTraceListener, Microsoft.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL""")
        strBuilder.AppendLine("                 initializeData=""FileLogWriter""/>")
        strBuilder.AppendLine("            <!-- Uncomment the below section and replace APPLICATION_NAME with the name of your application to write to the Application Event Log -->")
        strBuilder.AppendLine("            <!--<add name=""EventLog"" type=""System.Diagnostics.EventLogTraceListener"" initializeData=""APPLICATION_NAME""/> -->")
        strBuilder.AppendLine("        </sharedListeners>")
        strBuilder.AppendLine("    </system.diagnostics>")
        strBuilder.AppendLine("</configuration>")

        Try

            Dim objWriter As StreamWriter = New StreamWriter(Me.Context.Parameters.Item("targetdir") & "GLNet.exe.config")
            'Dim objWriter As StreamWriter = New StreamWriter("c:\Candela.exe.config")
            objWriter.Write(strBuilder.ToString) '"Provider=SQLOLEDB.1;Password=lumensoft2003;Persist Security Info=True;UserID=sa;Initial Catalog=" & Me.Context.Parameters.Item("dbname") & ";Data Source=.\" & InstanceName)
            objWriter.AutoFlush = True
            objWriter.Close()
            SetLoginModeInRegistery(InstanceName)

        Catch ex As Exception
            MsgBox("In Writing connection string: " & ex.Message)
            Throw ex
        End Try

        'Try

        '    'Dim objWriter As StreamWriter = New StreamWriter(Me.Context.Parameters.Item("targetdir") & "SvrName.txt")
        '    'objWriter.Write("Provider=SQLOLEDB.1;Password=lumensoft2003;Persist Security Info=True;UserID=sa;Initial Catalog=" & Me.Context.Parameters.Item("dbname") & ";Data Source=.\" & InstanceName)
        '    'objWriter.AutoFlush = True
        '    'objWriter.Close()

        'Catch ex As Exception
        '    MsgBox("In Writing connection string: " & ex.Message)
        '    Throw ex
        'End Try

    End Function

    Private Sub ExecuteSql(ByVal DatabaseName As String, ByVal Sql As String)
        Dim Command As New SqlClient.SqlCommand(Sql, sqlConnection1)

        Command.Connection.Open()
        Command.Connection.ChangeDatabase(DatabaseName)
        Try
            Command.ExecuteNonQuery()
        Finally
            ' Finally, blocks are a great way to ensure that the connection 
            ' is always closed.
            Command.Connection.Close()
        End Try
    End Sub

    Protected Sub AddDBTable(ByVal strDBName As String)
        Try

            ' Creates the database.
            'ExecuteSql("master", "CREATE DATABASE " + strDBName)

            ExecuteSql("master", "alter login sa enable; alter login sa with password = 'lumensoft2003';")

            If Not Me.Context.Parameters.Item("companyvalue") = 6 Then
                ' insert computer name
                Dim strSql As String = "INSERT INTO tblComputerList " _
                            & "(computer_name, POS_code, ISHeadOfficeComputer, computer_Type) " _
                            & " VALUES     ('" & System.Environment.MachineName.ToString & "', '001', 1,null)"
                ' ExecuteSql(strDBName, strSql)
            End If
        Catch ex As Exception
            ' Reports any errors and abort.
            MsgBox("In exception handler: " & ex.Message)
            Throw ex
        End Try
    End Sub

    Protected Function SetConnectionString() As String

        Const edition As String = "Express Edition"

        Dim fCheckEdition As Boolean = False

        Dim fCheckSpLevel As Boolean = False

        Try

            Dim getSqlExpress As ManagementObjectSearcher = New ManagementObjectSearcher("root\Microsoft\SqlServer\ComputerManagement", "select * from SqlServiceAdvancedProperty where SQLServiceType = 1 and (PropertyName = 'SKUNAME')")

            If getSqlExpress.Get.Count = 0 Then

                Return False

            End If

            For Each sqlEngine As ManagementObject In getSqlExpress.Get

                'dr.Item(2) = isConnected("Integrated Security=SSPI;Persist Security Info=False;User ID=sa;Initial Catalog=master;Data Source=.\" & Mid(sqlEngine("ServiceName").ToString, 7))
                'If sqlEngine("ServiceName").ToString.Equals(instance) Then

                Select Case sqlEngine("PropertyName").ToString

                    Case "SKUNAME"

                        fCheckEdition = sqlEngine("PropertyStrValue").ToString.Contains(edition)

                End Select

                If fCheckEdition = True Then
                    sqlConnection1.ConnectionString = "Integrated Security=SSPI;Persist Security Info=False;User ID=sa;Initial Catalog=master;Data Source=" & System.Environment.MachineName.ToString & "\" & Mid(sqlEngine("ServiceName").ToString, 7)
                    If isConnected(sqlConnection1.ConnectionString) = True Then
                        WriteConnectionString(Mid(sqlEngine("ServiceName").ToString, 7))
                        Return True
                    End If
                End If

            Next



            Return False

        Catch e As ManagementException

            Throw New Exception("Error occured while getting server info: " + e.ErrorCode + ", " + e.Message)

            Return False

        End Try
    End Function

    Public Function isConnected(ByVal ConnectionString As String) As Boolean
        Try
            Dim cn As New SqlClient.SqlConnection
            cn.ConnectionString = ConnectionString
            cn.Open()
            cn.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Overrides Sub Install(ByVal stateSaver As System.Collections.IDictionary)

        MyBase.Install(stateSaver)
        '  WritePath(Me.Context.Parameters.Item("targetdir"))

        Dim FilePath As String

        If Me.Context.Parameters.Item("companyvalue") = 1 Then
            FilePath = Me.Context.Parameters.Item("targetdir") & "\DataBases\candelageneral"
        ElseIf Me.Context.Parameters.Item("companyvalue") = 2 Then
            FilePath = Me.Context.Parameters.Item("targetdir") & "\DataBases\candelagarments"
        ElseIf Me.Context.Parameters.Item("companyvalue") = 3 Then
            FilePath = Me.Context.Parameters.Item("targetdir") & "\DataBases\candelashoes"
        ElseIf Me.Context.Parameters.Item("companyvalue") = 4 Then
            FilePath = Me.Context.Parameters.Item("targetdir") & "\DataBases\CandelaMedicine"
        ElseIf Me.Context.Parameters.Item("companyvalue") = 5 Then
            FilePath = Me.Context.Parameters.Item("targetdir") & "\DataBases\candelaBlank"
        ElseIf Me.Context.Parameters.Item("companyvalue") = 6 Then
            FilePath = Me.Context.Parameters.Item("targetdir") & "\DataBases\CandelaReplShopDB_New"
        End If

        If Me.Context.Parameters.Item("companyvalue") > 6 Then
            Shell(Me.Context.Parameters.Item("targetdir") & "\Utilities\Connection Writer\Candela Connection Information Writer.exe")
        Else
            SetConnectionString()
            RestoreDB(sqlConnection1.DataSource, sqlConnection1.ConnectionString, FilePath, Me.Context.Parameters.Item("targetdir") & "\DataBases", Me.Context.Parameters.Item("dbname"))
            AddDBTable(Me.Context.Parameters.Item("dbname"))
        End If

    End Sub

    Private Function RestoreDB(ByVal ServerName As String, ByVal ConnectionString As String, ByVal FilePath As String, ByVal RestorePath As String, ByVal DBName As String) As Boolean

        ''Find Server Exist
        'Dim MyPing As New Net.NetworkInformation.Ping
        'Dim MyPingReply As Net.NetworkInformation.PingReply = MyPing.Send(ServerName, 10)

        'If MyPingReply.Status.ToString = "Success" Then

        ''Restore
        Dim conn As SqlClient.SqlConnection = New SqlClient.SqlConnection(ConnectionString)
        Try
            conn.Open()

            Try

                Dim strSQL As String

                Dim cmd As New SqlClient.SqlCommand
                cmd.CommandType = CommandType.Text
                cmd.Connection = conn

                ShareFolder(RestorePath)

                strSQL = " RESTORE FILELISTONLY FROM DISK = '" & FilePath & "'"
                cmd.CommandText = strSQL


                Dim mdfFileName As String = ""
                Dim logFileName As String = ""

                Dim DR As SqlClient.SqlDataReader
                DR = cmd.ExecuteReader

                If DR.HasRows Then
                    DR.Read()
                    mdfFileName = DR.Item(0)

                    DR.Read()
                    logFileName = DR.Item(0)

                End If

                DR.Close()
                DR = Nothing

                strSQL = "IF Not Exists (SELECT * FROM sysdatabases WHERE [name]='" & DBName.Trim & "')" & vbCrLf
                strSQL = strSQL & " Begin " & vbCrLf

                strSQL = strSQL & "         RESTORE DATABASE " & DBName.Trim & vbCrLf
                strSQL = strSQL & "         FROM DISK = '" & FilePath & "'" & vbCrLf
                strSQL = strSQL & "         WITH MOVE '" & mdfFileName & "' TO '" & RestorePath & "\" & DBName.Trim & ".mdf'," & vbCrLf
                strSQL = strSQL & "         MOVE '" & logFileName & "' TO '" & RestorePath & "\" & DBName.Trim & "_log.ldf'" & vbCrLf
                strSQL = strSQL & " End " & vbCrLf


                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()

                If conn.State = ConnectionState.Open Then conn.Close()
                conn = Nothing

            Catch ex As Exception

                If conn.State = ConnectionState.Open Then conn.Close()
                conn = Nothing
                Throw New Exception("Fail to Restore DB Backup: " & ex.Message)
                Return False

            End Try

        Catch ex As Exception

            'If ex.Message = "Fail to Restore DB Backup: " & ex.Message Then
            Throw ex
            'Else

            'If conn.State = ConnectionState.Open Then conn.Close()
            'conn = Nothing
            'Throw New Exception("Connection to Master DB Failed.")
            Return False

            'End If
        End Try

        Return True
        'End If
    End Function

    Sub ShareFolder(ByVal FolderName As String)
        Try

            Dim managementClass As New ManagementClass("Win32_Share")
            Dim inParams As ManagementBaseObject = managementClass.GetMethodParameters("Create")

            inParams("Description") = "My Description"

            inParams("Name") = "Share Name"
            If Not System.IO.Directory.Exists(FolderName) Then
                System.IO.Directory.CreateDirectory(FolderName)
            End If
            inParams("Path") = FolderName

            inParams("Type") = &H0
            Dim outParams As ManagementBaseObject = managementClass.InvokeMethod("Create", inParams, Nothing)
            If Convert.ToUInt32(outParams.Properties("ReturnValue").Value) <> 0 Then

                ' Throw New Exception("Unable to share directory.")
            Else
                'MessageBox.Show("Shared folder successfully!")
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Protected Sub SetLoginModeInRegistery(ByVal InstanceName As String)
        Try

            Dim keyName As String = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL"
            Dim valueName As String = InstanceName
            Dim defaultValue As Object = 1
            Dim returnValue As Object

            returnValue = Registry.GetValue(keyName, valueName, defaultValue).ToString
            '    Me.Label1.Text = returnValue
            keyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\" & returnValue.ToString & "\MSSQLServer"
            valueName = "LoginMode"
            returnValue = Registry.GetValue(keyName, valueName, defaultValue).ToString
            If Not returnValue = 2 Then Registry.SetValue(keyName, valueName, 2)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class
