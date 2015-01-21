Imports Model
Imports Utility
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Management
Imports System.Management.Instrumentation
Imports System.Net

Public Class RegisterProductDAL
    ''//-------------------------------------------------------------------------------------
    ''// Date Modified     Modified by         Brief Description			                
    ''//------------------------------------------------------------------------------------
    ''// 05 July,2010      Abdul Jabbar         CR#67  -GL Application is not Runinng on Windows 7,Windows server 2K8 
    ''// 25 Aug,2010       Abdul Jabbar         CR#75  -GL License Process needs to update
    ''// 08 Nov,2010       Abdul Jabbar         CR#100 -Problem in Online Registration of GL
    ''// 02 Feb,2012       Abdul Jabbar         CR#173 -GL Release should varify License expiry date before installation
    ''// 16 Dec,2013       Abdul Jabbar         CR#287 -GL Lightwave registration failed.
    ''// 18 Feb,2014       Rana Saeed           CR#293 -Lightwave Registation: varification of registration process must not use auto generated Ids while varifying keys values
    ''/////////////////////////////////////////////////////////////////////////////////////////
#Region "Declarations"
    Private Const NO_ERROR = 0

    Private Declare Function inet_addr Lib "wsock32.dll" _
      (ByVal s As String) As Long

    Private Declare Function SendARP Lib "iphlpapi.dll" _
          (ByVal DestIP As Long, _
           ByVal SrcIP As Long, _
            ByVal pMacAddr As Long, _
            ByVal PhyAddrLen As Long) As Long

    Private Declare Sub CopyMemory Lib "kernel32" _
           Alias "RtlMoveMemory" _
          (ByVal dst As Object, _
            ByVal src As Object, _
           ByVal bcount As Long)

    Private Declare Function getHostByName _
                                Lib "wsock32" _
                                (ByVal hostname As String) _
                                As Long

    Private Declare Sub RtlMoveMemory _
                                    Lib "kernel32" _
                                    (ByVal hpvDest As Object, _
                                    ByVal hpvSource As Long, _
                                    ByVal cbCopy As Long)

    Public Structure SystemInfo
        Dim dwOemId As Long
        Dim dwPageSize As Long
        Dim lpMinimumApplicationAddress As Long
        Dim lpMaximumApplicationAddress As Long
        Dim dwActiveProcessorMask As Long
        Dim dwNumberOfProcessors As Long
        Dim dwProcessorType As Long
        Dim dwAllocationGranularity As Long
        Dim dwReserved As Long
    End Structure

    Declare Sub GetSystemInfo Lib "kernel32" (ByVal lpSystemInfo As SystemInfo)
    Declare Function apiGetComputerName Lib "kernel32" Alias _
        "GetComputerNameA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long

#End Region

#Region "Public Functions"
    Public Function GetFingerPrint() As String
        Try
            Dim strFingerPrint As String

            Dim strProductKey1 As String = String.Empty
            Dim strProductKey2 As String = String.Empty
            Dim strProductKey3 As String = String.Empty

            Dim strComputerName As String = Environment.MachineName.ToString
            Dim strMACAddresses() As String = GetMACAddress()

            For i As Integer = 0 To strMACAddresses.GetUpperBound(0) '- 1
                If i > 1 Then Exit For

                If i = 0 Then
                    strProductKey1 = Replace(strMACAddresses(i), ":", "-")
                ElseIf i = 1 Then
                    strProductKey2 = Replace(strMACAddresses(i), ":", "-")
                End If
            Next

            If strProductKey1 = "" Then
                Dim strRemoteMacAddress As String
                ''This function return nothing in every case so dont need to call this.
                'If GetRemoteMACAddress(NameToAddress(strComputerName), strRemoteMacAddress, "-") Then


                '    strProductKey1 = strRemoteMacAddress
                'Else
                strProductKey1 = "(SendARP Call Failed)"
                'End If
            End If

            Dim YourSystem As SystemInfo
            'GetSystemInfo(YourSystem)
            Try
                strProductKey3 = GetMotherboardId().Trim  'GetProcessorId() 'YourSystem.dwReserved
                If strProductKey3 = "" Then strProductKey3 = GetMotherboardIdFromBIOS()
            Catch ex As Exception 'CR#75
                strProductKey3 = GetHardDiskID()
            End Try

            ''

            ''motherboard id return empty string on some machines, so we get hard disk id in that case
            ''------------- 'CR#75
            'CR#100
            '' ''motherboard id return empty string on some machines, so we get hard disk id in that case
            ''If strProductKey3 = "" Or strProductKey3 Is Nothing Then strProductKey3 = GetHardDiskID()
            '' ''------------

            If strProductKey3.Trim = "" Or strProductKey3 Is Nothing Then
                strProductKey3 = GetHardDiskID()
            End If


            Dim strProductKey4 As String = strProductKey3

            strFingerPrint = strProductKey1 & "," & strProductKey2 & "," & strProductKey3 & ", " & strProductKey4

            ''Return Utility.Utility.EncryptWithCSP(strFingerPrint, "f")        
            Return Utility.Utility.SymmetricEncryption.Encrypt(strFingerPrint, "f")

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetMACAddress(Optional ByVal strServerName As String = "") As String()
        Try
            'CR#67

            'Dim strMACAddresses As New List(Of String)
            'Dim i As Integer = 0

            'If strServerName.Trim = "" Then
            '    Dim mc As New ManagementClass("Win32_NetworkAdapterConfiguration")
            '    Dim moc As ManagementObjectCollection = mc.GetInstances()            

            '    For Each mo As ManagementObject In moc
            '        ''If strMACAddresses(i) = String.Empty Then '// only return MAC Address from first card
            '        If CBool(mo("IPEnabled")) = True Then
            '            strMACAddresses.Add(mo("MacAddress").ToString)
            '            'Exit For
            '            ''i += 1
            '        End If
            '        ''End If
            '        'mo.Dispose()
            '    Next

            'Else
            '    Dim objMgmtScope As New ManagementScope("\\" & strServerName & "\root\cimv2")
            '    Dim strQueryString As String = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 1"
            '    Dim objObjectQuery As New ObjectQuery(strQueryString)

            '    ''Dim objSearcher As New ManagementObjectSearcher(objMgmtScope, objObjectQuery)
            '    Dim objSearcher As ManagementObjectSearcher = New ManagementObjectSearcher(strQueryString)
            '    Dim objResultCollection As ManagementObjectCollection
            '    objResultCollection = objSearcher.Get()

            '    For Each objResult As ManagementObject In objResultCollection
            '        strMACAddresses.Add(objResult("MACAddress").ToString)
            '        i += 1
            '    Next
            'End If

            ''strMACAddress = strMACAddress.Replace(":", "")
            'strMACAddresses.TrimExcess()
            'Return strMACAddresses.ToArray

            Dim strMACAddresses As New List(Of String)
            Dim i As Integer = 0

            If strServerName.Trim = "" Then

                Dim mc As New ManagementClass("Win32_NetworkAdapterConfiguration")
                Dim moc As ManagementObjectCollection = mc.GetInstances()

                For Each mo As ManagementObject In moc
                    ''If strMACAddresses(i) = String.Empty Then '// only return MAC Address from first card
                    If CBool(mo("IPEnabled")) = True Then
                        strMACAddresses.Add(mo("MacAddress").ToString)
                    End If
                Next

                If strMACAddresses.Count = 0 Then
                    Dim nic As NetworkInformation.NetworkInterface = Nothing
                    Dim mac_Address As String
                    For Each nic In NetworkInformation.NetworkInterface.GetAllNetworkInterfaces
                        strMACAddresses.Add(nic.GetPhysicalAddress().ToString)
                        If strMACAddresses.Count > 0 Then Exit For
                    Next
                End If

            Else

                'CR#101
                '    Dim nic As NetworkInformation.NetworkInterface = Nothing
                '    Dim mac_Address As String
                '    For Each nic In NetworkInformation.NetworkInterface.GetAllNetworkInterfaces
                '        If Len(nic.GetPhysicalAddress().ToString) > 0 Then
                '            strMACAddresses.Add(nic.GetPhysicalAddress().ToString)
                '        End If
                '    Next

                'End If

                'strMACAddresses.TrimExcess()

                'Return strMACAddresses.ToArray

                Dim objMgmtScope As New ManagementScope("\\" & strServerName & "\root\cimv2")
                Dim strQueryString As String = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 1"
                Dim objObjectQuery As New ObjectQuery(strQueryString)

                ''Dim objSearcher As New ManagementObjectSearcher(objMgmtScope, objObjectQuery)
                Dim objSearcher As ManagementObjectSearcher = New ManagementObjectSearcher(strQueryString)
                Dim objResultCollection As ManagementObjectCollection
                objResultCollection = objSearcher.Get()

                For Each objResult As ManagementObject In objResultCollection
                    strMACAddresses.Add(objResult("MACAddress").ToString)
                    i += 1


                Next

                If strMACAddresses.Count = 0 Then
                    Dim nic As NetworkInformation.NetworkInterface = Nothing
                    Dim mac_Address As String
                    For Each nic In NetworkInformation.NetworkInterface.GetAllNetworkInterfaces
                        strMACAddresses.Add(nic.GetPhysicalAddress().ToString)
                        If strMACAddresses.Count > 0 Then Exit For
                    Next
                End If


            End If

            strMACAddresses.TrimExcess()

            Return strMACAddresses.ToArray


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetRemoteMACAddress(ByVal sRemoteIP As String, ByVal sRemoteMacAddress As String, _
                                        ByVal sDelimiter As String) As Boolean
        Try
            ''Dim strHostName As String = Dns.GetHostName()
            ''Dim strIPAddress As String = Dns.GetHostEntry(strHostName).AddressList(0).ToString

            Dim dwRemoteIP As Long
            Dim pMacAddr As Long
            Dim bpMacAddr() As Byte
            Dim PhyAddrLen As Long

            'convert the string IP into
            'an unsigned long value containing
            'a suitable binary representation
            'of the Internet address given
            dwRemoteIP = inet_addr(sRemoteIP) 'ConvertIPtoLong(sRemoteIP)

            If dwRemoteIP <> 0 Then

                'must set this up first!
                PhyAddrLen = 6

                'assume failure
                GetRemoteMACAddress = False

                'retrieve the remote MAC address
                If SendARP(dwRemoteIP, 0&, pMacAddr, PhyAddrLen) = NO_ERROR Then

                    If (pMacAddr <> 0) And (PhyAddrLen <> 0) Then

                        'returned value is a long pointer
                        'to the MAC address, so copy data
                        'to a byte array
                        ReDim bpMacAddr(0 To PhyAddrLen - 1)
                        CopyMemory(bpMacAddr(0), pMacAddr, PhyAddrLen)

                        'convert the byte array to a string
                        'and return success
                        sRemoteMacAddress = MakeMacAddress(bpMacAddr, sDelimiter)
                        GetRemoteMACAddress = True

                    End If 'pMacAddr

                End If  'SendARP

            End If  'dwRemoteIP

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetProcessorId() As String
        Try

            Dim strProcessorId As String = ""

            Dim strQueryString As String = "SELECT * FROM Win32_Processor"
            Dim objObjectQuery As New ObjectQuery(strQueryString)

            ''Dim objSearcher As New ManagementObjectSearcher(objMgmtScope, objObjectQuery)
            Dim objSearcher As ManagementObjectSearcher = New ManagementObjectSearcher(strQueryString)
            Dim objResultCollection As ManagementObjectCollection
            objResultCollection = objSearcher.Get()

            For Each objResult As ManagementObject In objResultCollection
                strProcessorId = objResult("ProcessorID").ToString
                'i += 1
            Next

            Return strProcessorId
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetMotherboardId() As String
        Try

            'CR#67


            Dim wmi As Object = GetObject("WinMgmts:")

            ' Get the "base boards" (mother boards).
            Dim serial_numbers As String = ""
            Dim mother_boards As Object = _
                wmi.InstancesOf("Win32_BaseBoard")
            For Each board As Object In mother_boards
                serial_numbers &= ", " & board.SerialNumber
            Next board
            If serial_numbers.Length > 0 Then serial_numbers = _
                serial_numbers.Substring(2)
            'CR#287
            If serial_numbers.Replace("0", String.Empty).Length = 0 Then Return String.Empty

            Return serial_numbers

            ''Dim strMotherboardId As String = ""

            ''Dim strQueryString As String = "SELECT * FROM Win32_BaseBoard"
            ''Dim objObjectQuery As New ObjectQuery(strQueryString)

            '' ''Dim objSearcher As New ManagementObjectSearcher(objMgmtScope, objObjectQuery)
            ''Dim objSearcher As ManagementObjectSearcher = New ManagementObjectSearcher(strQueryString)
            ''Dim objResultCollection As ManagementObjectCollection
            ''objResultCollection = objSearcher.Get()

            ''For Each objResult As ManagementObject In objResultCollection
            ''    strMotherboardId = objResult("SerialNumber").ToString
            ''Next

            ''Return strMotherboardId

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetMotherboardIdFromBIOS() As String
        Try
            Dim strMotherboardId As String = ""

            Dim strQueryString As String = "SELECT * FROM Win32_BIOS"
            Dim objObjectQuery As New ObjectQuery(strQueryString)

            ''Dim objSearcher As New ManagementObjectSearcher(objMgmtScope, objObjectQuery)
            Dim objSearcher As ManagementObjectSearcher = New ManagementObjectSearcher(strQueryString)
            Dim objResultCollection As ManagementObjectCollection
            objResultCollection = objSearcher.Get()

            For Each objResult As ManagementObject In objResultCollection
                strMotherboardId = objResult("SerialNumber").ToString
            Next

            'CR#287
            If strMotherboardId.Replace("0", String.Empty).Length = 0 Then Return String.Empty

            Return strMotherboardId

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'this function will combine the MAC address values
    Private Function MakeMacAddress(ByVal b() As Byte, ByVal sDelim As String) As String

        Dim cnt As Long
        Dim buff As String

        On Error GoTo MakeMac_error

        'so far, MAC addresses are
        'exactly 6 segments in size (0-5)
        If UBound(b) = 5 Then

            'concatenate the first five values
            'together and separate with the
            'delimiter char
            For cnt = 0 To 4
                buff = buff & Right$("00" & Hex(b(cnt)), 2) & sDelim
            Next

            'and append the last value
            buff = buff & Right$("00" & Hex(b(5)), 2)

        End If  'UBound(b)

        MakeMacAddress = buff

MakeMac_exit:
        Exit Function

MakeMac_error:
        MakeMacAddress = "Error"
        Resume MakeMac_exit

    End Function

    'resolves host name to IP address
    Private Function NameToAddr(ByVal strHost As String) As String

        Dim ip_list() As Byte
        Dim heEntry As IPHostEntry
        Dim strIPAddr As String
        Dim lp_HostEnt As Long
        Dim lp_HostIP As Long
        Dim iLoop As Integer

        On Error GoTo NameToAddrError

        lp_HostEnt = getHostByName(strHost)

        If lp_HostEnt = 0 Then
            Exit Function
        End If

        ''RtlMoveMemory(heEntry, lp_HostEnt, LenB(heEntry))
        ''RtlMoveMemory(lp_HostIP, heEntry.hAddrList, 4)

        ''ReDim ip_list(1 To heEntry.hLength)

        ''RtlMoveMemory(ip_list(1), lp_HostIP, heEntry.hLength)

        ''For iLoop = 1 To heEntry.hLength
        ''    strIPAddr = strIPAddr & ip_list(iLoop) & "."
        ''Next

        strIPAddr = Mid(strIPAddr, 1, Len(strIPAddr) - 1)

        NameToAddr = strIPAddr
NameToAddrError:

    End Function

    Private Function NameToAddress(ByVal strName As String) As String

        'If mbInitialized Then
        NameToAddress = NameToAddr(strName)
        'End If

    End Function


    Public Function RegisterProduct(ByVal strFingerPrint As String, ByVal strRegistrationKey As String, Optional ByVal strOnlineKey As String = "", Optional ByVal LicenseExpiryDate As String = "") As Boolean
    

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try
            Dim strGLID() As String
            Dim strSQL As String
            Dim intCounter As Integer

            Dim blnFirstPartMatched As Boolean = False
            Dim blnSecondPartMatched As Boolean = False
            Dim blnThirdPartMatched As Boolean = False

            Dim DecryFingerPrint() As String
            ''DecryFingerPrint = Split(Utility.Utility.DecryptWithCSP(strFingerPrint, "f"), ",")
            DecryFingerPrint = Split(Utility.Utility.SymmetricEncryption.Decrypt(strFingerPrint, "f"), ",")

            DecryFingerPrint(0) = DecryFingerPrint(0).ToString.Trim
            DecryFingerPrint(1) = DecryFingerPrint(1).ToString.Trim
            DecryFingerPrint(2) = DecryFingerPrint(2).ToString.Trim
            DecryFingerPrint(3) = DecryFingerPrint(3).ToString.Trim

            Dim DecryRegisterKey() As String
            DecryRegisterKey = Split(strRegistrationKey, vbCrLf)

            ''If Not ((IIf(DecryFingerPrint(0) = "", False, DecryFingerPrint(0) = Utility.Utility.DecryptWithALP(DecryRegisterKey(0)))) Or IIf(DecryFingerPrint(1) = "", False, (DecryFingerPrint(1) = Utility.Utility.DecryptWithALP(DecryRegisterKey(1)))) Or IIf(DecryFingerPrint(2) = "", False, (DecryFingerPrint(2) = Utility.Utility.DecryptWithALP(DecryRegisterKey(2))))) Then
            ''    'Screen.MousePointer = vbNormal
            ''    'MsgBox("Registration Failed, Invalid Product Registration key.", vbInformation)
            ''    'Exit Function
            ''    Return False
            ''End If

            If DecryFingerPrint(0) = "" Then
                blnFirstPartMatched = False
            Else
                If DecryFingerPrint(0) = Utility.Utility.SymmetricEncryption.Decrypt(DecryRegisterKey(0), "f") Then
                    blnFirstPartMatched = True
                Else
                    blnFirstPartMatched = False
                End If
            End If

            If DecryFingerPrint(1) = "" Then
                blnSecondPartMatched = False
            Else
                If DecryFingerPrint(1) = Utility.Utility.SymmetricEncryption.Decrypt(DecryRegisterKey(1), "f") Then
                    blnSecondPartMatched = True
                Else
                    blnSecondPartMatched = False
                End If
            End If

            If DecryFingerPrint(2) = "" Then
                blnThirdPartMatched = False
            Else
                If DecryFingerPrint(2) = Utility.Utility.SymmetricEncryption.Decrypt(DecryRegisterKey(2), "f") Then
                    blnThirdPartMatched = True
                Else
                    blnThirdPartMatched = False
                End If
            End If

            If Not (blnFirstPartMatched Or blnSecondPartMatched Or blnThirdPartMatched) Then
                Return False
            End If


            Dim strDecryptedRegKey As String = Utility.Utility.SymmetricEncryption.Decrypt(DecryRegisterKey(3), "f")

            strGLID = Split(Trim(strRegistrationKey), vbCrLf)

            For intCounter = 0 To strGLID.Length - 1
                Select Case intCounter

                    Case 0

                        'Build a SQL to update the Configuration Value
                        strSQL = "UPDATE tblGLConfiguration SET config_value = '" & strGLID(intCounter) & "' WHERE (config_name = 'GL_ID')"

                    Case 1

                        'Build a SQL to update the Configuration Value
                        strSQL = "UPDATE tblGLConfiguration SET config_value = '" & strGLID(intCounter) & "' WHERE (config_name = 'GL_ID1')"

                    Case 2

                        'Build a SQL to update the Configuration Value
                        strSQL = "UPDATE tblGLConfiguration SET config_value = '" & strGLID(intCounter) & "' WHERE (config_name = 'GL_ID2')"

                    Case 3

                        strSQL = "UPDATE tblGLConfiguration SET config_value = '" & strDecryptedRegKey.Trim & "' WHERE (config_name = 'GL_ID3')"

                End Select

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
            Next


            'CR#173
            If strOnlineKey <> String.Empty Then
                strSQL = "UPDATE tblGLConfiguration SET config_value = '" & Utility.Utility.SymmetricEncryption.Encrypt(LicenseExpiryDate, "f") & "' WHERE (config_name = 'GL_LED')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

                Dim log As New ActivityLog
                log.TableName = "tblGLConfiguration"
                log.SQL = strSQL
                log.SQL = "UPDATE"
                UtilityDAL.BuildSQLLog(log, trans, False) 'no need to maintain ssb log as License info (GL_LED) doesn't requires to update at shop level

            End If

            ''Dim strComputerName As String = Environment.MachineName.ToString
            ''strSQL = "SELECT COUNT(*) AS reccount From tblComputerList WHERE (computer_name = '" & strComputerName & "')"
            ''Dim intRecordCount As Integer = SQLHelper.ExecuteScaler(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            ''If intRecordCount = 0 Then
            ''    strSQL = "INSERT INTO tblComputerList (computer_name, POS_code, ISHeadOfficeComputer, computer_Type)" _
            ''           & " VALUES     ('" & strComputerName & "', '002', 1, 'POS')"
            ''    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
            ''End If

            trans.Commit()
            Return True


        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

    Public Sub GenerateRegistrationKey(ByVal strPath As String, ByVal strFingerPrint As String)
        Try

            Const strFingeprintFileName As String = "Fingerprint.txt"
            Const strFolderName As String = "\Registration"

            If Not Directory.Exists(strPath & strFolderName) Then
                Directory.CreateDirectory(strPath & strFolderName)
            End If

            Dim objStreamWriter As New StreamWriter(strPath & strFolderName & "\" & strFingeprintFileName)
            objStreamWriter.WriteLine(strFingerPrint)
            objStreamWriter.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub RegisterLater()

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        ''Dim trans As SqlTransaction = conn.BeginTransaction

        Try
            Dim strSQL As String
            Dim strComputerName As String = Environment.MachineName.ToString

            ''strSQL = "SELECT COUNT(*) AS reccount From tblComputerList WHERE (computer_name = '" & strComputerName & "')"
            ''Dim intCount As Integer = SQLHelper.ExecuteScaler(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            ''If intCount = 0 Then
            ''    strSQL = "INSERT INTO tblComputerList (computer_name, POS_code, ISHeadOfficeComputer, computer_Type)" _
            ''           & " VALUES     ('" & strComputerName & "', '002', 1, 'POS')"

            ''    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
            ''    trans.Commit()
            ''End If

        Catch ex As Exception
            ''trans.Rollback()
            Throw ex
        End Try
    End Sub

    '*************************************************************************************
    'This method will check the Candela License by doing these steps
    '1. Check if this machine is not Publisher then ignore it
    '2. If Machine is Publisher then Get the Encrypted Value of Candela_ID
    '3. Decrypt the MAC address to readable format
    '4. Get the Server Name from Connection String
    '5. Get the MAC address of Server Name
    '6. Compare the MAC addresses and then send True
    '*************************************************************************************

    Public Function ValidateGLLicense(ByVal strServerName As String) As Boolean
        '************ Block Commented under 293 by   RSaeed
        'Dim objDA As SqlDataAdapter

        'Dim conn As New SqlConnection(SQLHelper.CON_STR)
        'conn.Open()
        'Dim trans As SqlTransaction = conn.BeginTransaction
        '************ Block Commented under 293 by   RSaeed

        Try

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '' 1. Check if this machine is not Publisher then ignore it
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '**********************************************************
            ' Already implemented on form
            '**********************************************************

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '' 2. If Machine is Publisher then Get the Encrypted Value of Candela_ID
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '************ Block Commented under 293 by   RSaeed
            'Dim strSQL As String
            'Dim dtGLIds As New DataTable
            'Dim sRemoteMacAddress As String
            '************ Block Commented under 293 by   RSaeed

            'build sql to get the candela_id from Configuration table
            '************ Block Commented under 293 by   RSaeed
            'strSQL = "SELECT config_name, config_value From tblGLConfiguration WHERE (config_name IN ('GL_ID', 'GL_ID1', 'GL_ID2','GL_ID3'))"
            'objDA = New SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            'objDA.Fill(dtGLIds)
            '************ Block Commented under 293 by   RSaeed

            Dim strGLID As String = ""
            Dim strGLID1 As String = ""
            Dim strGLID2 As String = ""
            Dim strGLID3 As String = ""

            '************ Block Commented under 293 by   RSaeed
            'If dtGLIds.Rows.Count > 0 Then

            '    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '    '' 3. Decrypt the MAC address to readable format
            '    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            '    If dtGLIds.Rows.Count = 1 Then
            '        'If dtGLIds.Rows(0)("config_value").ToString <> "" Then strGLID = Utility.Utility.SymmetricEncryption.Decrypt(dtGLIds.Rows(0)("config_value"), "f")
            '        If strGLID <> "" Then strGLID = Utility.Utility.SymmetricEncryption.Decrypt(dtGLIds.Rows(0)("config_value"), "f")
            '    Else

            '        If dtGLIds.Rows(0)("config_value").ToString <> "" Then strGLID = Utility.Utility.SymmetricEncryption.Decrypt(dtGLIds.Rows(0)("config_value"), "f")
            '        If dtGLIds.Rows(1)("config_value").ToString <> "" Then strGLID1 = Utility.Utility.SymmetricEncryption.Decrypt(dtGLIds.Rows(1)("config_value"), "f")
            '        If dtGLIds.Rows(2)("config_value").ToString <> "" Then strGLID2 = Utility.Utility.SymmetricEncryption.Decrypt(dtGLIds.Rows(2)("config_value"), "f")

            '        If Not dtGLIds.Rows(3) Is Nothing Then
            '            ''strGLID3 = Utility.Utility.DecryptWithALP(dtGLIds.Rows(3)("config_value"))
            '            strGLID3 = dtGLIds.Rows(3)("config_value")
            '        End If
            '    End If
            'Else
            '    'G_blnTrialVersion = True
            '    Return False
            'End If
            '************ Block Commented under 293 by   RSaeed

            '************ 293   RSaeed
            If DAL.SystemConfigurationDAL.GetSystemConfigurationValue("GL_ID").ToString <> "" Then strGLID = Utility.Utility.SymmetricEncryption.Decrypt(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("GL_ID"), "f")
            If DAL.SystemConfigurationDAL.GetSystemConfigurationValue("GL_ID1").ToString <> "" Then strGLID1 = Utility.Utility.SymmetricEncryption.Decrypt(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("GL_ID1"), "f")
            If DAL.SystemConfigurationDAL.GetSystemConfigurationValue("GL_ID2").ToString <> "" Then strGLID2 = Utility.Utility.SymmetricEncryption.Decrypt(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("GL_ID2"), "f")
            If DAL.SystemConfigurationDAL.GetSystemConfigurationValue("GL_ID3").ToString <> "" Then strGLID3 = DAL.SystemConfigurationDAL.GetSystemConfigurationValue("GL_ID3")
            '************ 293   RSaeed


            'CR#287
            'If Not strGLID Is Nothing Then strGLID = strGLID.ToString.Trim
            'If Not strGLID1 Is Nothing Then strGLID1 = strGLID1.ToString.Trim
            'If Not strGLID2 Is Nothing Then strGLID2 = strGLID2.ToString.Trim
            'If Not strGLID3 Is Nothing Then strGLID3 = strGLID3.ToString.Trim

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '' 4. Get the Server Name from Connection String
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '***********************************************************
                ' Already passed to this function as parameter
                '***********************************************************

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '' 5. Get the MAC address of Server Name
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim strMACAddresses() As String = GetMACAddress(strServerName)

                Dim strServerMAC As String
                Dim strServerMAC1 As String
                Dim strServerMAC2 As String

                For intCounter As Integer = 0 To strMACAddresses.Length - 1
                    If intCounter > 1 Then Exit For

                    If intCounter = 0 Then
                        strServerMAC = Replace(strMACAddresses(intCounter), ":", "-")
                    ElseIf intCounter = 1 Then
                        strServerMAC1 = Replace(strMACAddresses(intCounter), ":", "-")
                    End If
                Next

                '=================================================
                'Get the processor id
                '=================================================
                'Intializing a variable to const.
                ''Dim YourSystem As SystemInfo
                'Refer cons. to API system info
            ''GetSystemInfo(YourSystem)

            Try
                strServerMAC2 = GetMotherboardId() 'GetProcessorId() ''YourSystem.dwReserved            
                If strServerMAC2 = "" Then strServerMAC2 = GetMotherboardIdFromBIOS()
            Catch ex As Exception
                strServerMAC2 = GetHardDiskID()
            End Try


            ''-------------CR#75
            ''motherboard id return empty string on some machines, so we get hard disk id in that case
            'If strServerMAC2 = "" Or strServerMAC2 Is Nothing Then strServerMAC2 = GetHardDiskID()
            If strServerMAC2 = "" Or strServerMAC2 Is Nothing Then strServerMAC2 = GetHardDiskID()
            ''-----------------
            strServerMAC2 = strServerMAC2.Trim()

            '=================================================

            If Trim(strServerMAC) = "" Then
                ''This function retuns nothing in every case so dont needto call this.
                ''Call GetRemoteMACAddress(NameToAddress(strServerName), sRemoteMacAddress, "-")

                'store the MAC address of server to a local variable
                strServerMAC = "" 'sRemoteMacAddress
            End If

            If strServerMAC = "Error" And strServerMAC1 = "" And strServerMAC2 = "" Then
                Return False
                Exit Function

            End If

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '' 6. Compare the MAC addresses and then send True
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                If IIf(strGLID <> "", (strServerMAC = strGLID), False) Or IIf(strGLID1 <> "", (strServerMAC1 = strGLID1), False) Or IIf(strGLID2 <> "", (strServerMAC2 = strGLID2), False) Then

                    ''If strGLID3 = "" Then
                    ''    If funIsServer(strServerName) Then

                    ''        strSQL = "UPDATE tblGLConfiguration SET config_value = '" & strGLID2 & "' WHERE (config_name = 'GL_ID3')"
                    ''        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

                    ''    End If
                    ''End If
                    'return True if Licence is valid
                    'G_blnTrialVersion = False
                    Return True
                Exit Function

                Else

                    ''If funIsServer(strServerName) Then
                    ''    strSQL = "UPDATE tblGLConfiguration SET config_value = '' WHERE (config_name = 'GL_ID3')"
                    ''    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

                    ''    'return True if Licence is valid
                    ''    'G_blnTrialVersion = True
                    ''    Return False

                    ''Else

                Dim strComputerName As String = ""
                If strServerName.Contains("\") Then
                    strComputerName = strServerName.Split("\")(0)
                Else
                    strComputerName = strServerName
                End If

                If strComputerName.ToString.Trim.ToUpper <> GetComputerName().ToString.Trim.ToUpper Then

                    If IIf(strGLID2 <> "", (strGLID3 = strGLID2), False) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End If

            'trans.Commit()             '293        RSaeed

            'return True if Licence is valid
            Return True

        Catch ex As Exception
            'trans.Rollback()           '293        RSaeed
            Throw ex
        Finally
            'objDA = Nothing            '293        RSaeed
        End Try
    End Function

    Private Function funIsServer(ByVal strServerName As String) As Boolean
        Dim strComputerName As String = Environment.MachineName.ToString
        If strServerName = strComputerName Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetHardDiskID() As String

        Dim disk As New ManagementObject("Win32_LogicalDisk.DeviceID=""C:""")
        Dim diskPropertyA As PropertyData = disk.Properties("VolumeSerialNumber")

        Return diskPropertyA.Value.ToString()

    End Function
    Public Function GetComputerName() As String
        Try
            Dim strMotherboardId As String = ""

            Dim strQueryString As String = "SELECT * FROM Win32_ComputerSystem"
            Dim objObjectQuery As New ObjectQuery(strQueryString)

            ''Dim objSearcher As New ManagementObjectSearcher(objMgmtScope, objObjectQuery)
            Dim objSearcher As ManagementObjectSearcher = New ManagementObjectSearcher(strQueryString)
            Dim objResultCollection As ManagementObjectCollection
            objResultCollection = objSearcher.Get()

            For Each objResult As ManagementObject In objResultCollection
                strMotherboardId = objResult("name").ToString
            Next

            Return strMotherboardId
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SetNoOfLicenses(ByVal strLicenseType As String) As Boolean

        Dim objDA As SqlDataAdapter

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Dim strSQL As String
        Dim intRecordsAffected As Integer

        Try

            '*****************************************************
            ' Setting type of Licenses in Configuration table
            '*****************************************************
            strSQL = "UPDATE tblGLConfiguration SET config_value = '" & Utility.Utility.SymmetricEncryption.Encrypt(strLicenseType.ToString, "f") _
                                          & "' WHERE config_name = 'Multi_Company'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing, intRecordsAffected)

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
#End Region

End Class
