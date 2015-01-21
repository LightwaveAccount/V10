Imports Utility.Utility
Imports Microsoft.VisualBasic
Public NotInheritable Class AboutBox1

    ' Reg Key Security Options...
    Const READ_CONTROL = &H20000
    Const KEY_QUERY_VALUE = &H1
    Const KEY_SET_VALUE = &H2
    Const KEY_CREATE_SUB_KEY = &H4
    Const KEY_ENUMERATE_SUB_KEYS = &H8
    Const KEY_NOTIFY = &H10
    Const KEY_CREATE_LINK = &H20
    Const KEY_ALL_ACCESS = KEY_QUERY_VALUE + KEY_SET_VALUE + _
                           KEY_CREATE_SUB_KEY + KEY_ENUMERATE_SUB_KEYS + _
                           KEY_NOTIFY + KEY_CREATE_LINK + READ_CONTROL

    ' Reg Key ROOT Types...
    Const HKEY_LOCAL_MACHINE = &H80000002
    Const ERROR_SUCCESS = 0
    Const REG_SZ = 1                         ' Unicode nul terminated string
    Const REG_DWORD = 4                      ' 32-bit number

    Const gREGKEYSYSINFOLOC = "SOFTWARE\Microsoft\Shared Tools Location"
    Const gREGVALSYSINFOLOC = "MSINFO"
    Const gREGKEYSYSINFO = "SOFTWARE\Microsoft\Shared Tools\MSINFO"
    Const gREGVALSYSINFO = "PATH"

    Private Declare Function RegOpenKeyEx Lib "advapi32" Alias "RegOpenKeyExA" (ByVal hKey As Long, ByVal lpSubKey As String, ByVal ulOptions As Long, ByVal samDesired As Long, ByRef phkResult As Long) As Long
    Private Declare Function RegQueryValueEx Lib "advapi32" Alias "RegQueryValueExA" (ByVal hKey As Long, ByVal lpValueName As String, ByVal lpReserved As Long, ByRef lpType As Long, ByVal lpData As String, ByRef lpcbData As Long) As Long
    Private Declare Function RegCloseKey Lib "advapi32" (ByVal hKey As Long) As Long


    Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).

        Me.lblVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString) ' My.Application.Info.CompanyName
        Me.lblSchemaVersion.Text = "Schema Version  " & DAL.SystemConfigurationDAL.GetSystemConfigurationValue(("Schema_Version"))
        Me.lblTitle.Text = System.Configuration.ConfigurationManager.AppSettings("StrDBName").ToString
        Me.lblCopyRight.Text = My.Application.Info.Copyright
        Me.lblURL.Text = "http://www.lumensoft.biz"

        If DecryptWithALP(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Version")) = "2" Then
            Me.lblVersionType.Text = "Professional Edition"

        ElseIf DecryptWithALP(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Version")) = "3" Then
            Me.lblVersionType.Text = "Enterprise Edition"

        ElseIf DecryptWithALP(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Version")) = "1" Then
            Me.lblVersionType.Text = "Personal Edition"
        End If

        'Trial Version check
        'if it is trial version then check no of trans, if greater than or equat to 25 then exit
        If gblnTrialVersion Then
            Me.lblVersionType.Text = "Trial Version"
        Else
            Me.lblVersionType.Text = "Enterprise Edition"
        End If

        'Me.TextBoxDescription.Text = My.Application.Info.Description
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Public Sub StartSysInfo()
        Try

            Dim rc As Long
            Dim SysInfoPath As String

            ' Try To Get System Info Program Path\Name From Registry...
            If GetKeyValueNew(HKEY_LOCAL_MACHINE, gREGKEYSYSINFO, gREGVALSYSINFO, SysInfoPath) Then
                ' Try To Get System Info Program Path Only From Registry...
            ElseIf GetKeyValueNew(HKEY_LOCAL_MACHINE, gREGKEYSYSINFOLOC, gREGVALSYSINFOLOC, SysInfoPath) Then
                ' Validate Existance Of Known 32 Bit File Version
                If (Dir(SysInfoPath & "\MSINFO32.EXE") <> "") Then
                    SysInfoPath = SysInfoPath & "\MSINFO32.EXE"

                    ' Error - File Can Not Be Found...
                Else
                    Throw New Exception
                End If
                ' Error - Registry Entry Can Not Be Found...
            Else
                Throw New Exception
            End If

            Call Shell(SysInfoPath, vbNormalFocus)

            Exit Sub
        Catch ex As Exception
            ShowErrorMessage("System Information Is Unavailable At This Time")
        End Try

    End Sub

    Public Function GetKeyValueNew(ByVal KeyRoot As Long, ByVal KeyName As String, ByVal SubKeyRef As String, ByRef KeyVal As String) As Boolean
        Dim hKey As Long                                        ' Handle To An Open Registry Key
        Dim rc As Long
        Try
            Dim i As Long                                           ' Loop Counter
            Dim hDepth As Long                                      '
            Dim KeyValType As Long                                  ' Data Type Of A Registry Key
            Dim tmpVal As String                                    ' Tempory Storage For A Registry Key Value
            Dim KeyValSize As Long                                  ' Size Of Registry Key Variable
            '------------------------------------------------------------
            ' Open RegKey Under KeyRoot {HKEY_LOCAL_MACHINE...}
            '------------------------------------------------------------
            rc = RegOpenKeyEx(KeyRoot, KeyName, 0, KEY_ALL_ACCESS, hKey) ' Open Registry Key

            If (rc <> ERROR_SUCCESS) Then Throw New Exception ' Handle Error...

            tmpVal = Space(1024)                         ' Allocate Variable Space
            KeyValSize = 1024                                       ' Mark Variable Size

            '------------------------------------------------------------
            ' Retrieve Registry Key Value...
            '------------------------------------------------------------
            rc = RegQueryValueEx(hKey, SubKeyRef, 0, _
                                 KeyValType, tmpVal, KeyValSize)    ' Get/Create Key Value

            If (rc <> ERROR_SUCCESS) Then Throw New Exception ' Handle Errors

            If (Asc(Mid(tmpVal, KeyValSize, 1)) = 0) Then           ' Win95 Adds Null Terminated String...
                tmpVal = Microsoft.VisualBasic.Left(tmpVal, KeyValSize - 1)               ' Null Found, Extract From String
            Else                                                    ' WinNT Does NOT Null Terminate String...
                tmpVal = Microsoft.VisualBasic.Left(tmpVal, KeyValSize)                   ' Null Not Found, Extract String Only
            End If
            '------------------------------------------------------------
            ' Determine Key Value Type For Conversion...
            '------------------------------------------------------------
            Select Case KeyValType                                  ' Search Data Types...
                Case REG_SZ                                             ' String Registry Key Data Type
                    KeyVal = tmpVal                                     ' Copy String Value
                Case REG_DWORD                                          ' Double Word Registry Key Data Type
                    For i = Len(tmpVal) To 1 Step -1                    ' Convert Each Bit
                        KeyVal = KeyVal + Hex(Asc(Mid(tmpVal, i, 1)))   ' Build Value Char. By Char.
                    Next
                    KeyVal = Format$("&h" + KeyVal)                     ' Convert Double Word To String
            End Select

            GetKeyValueNew = True                                      ' Return Success
            rc = RegCloseKey(hKey)                                  ' Close Registry Key
            Exit Function                                           ' Exit

        Catch ex As Exception
            KeyVal = ""                                             ' Set Return Val To Empty String
            GetKeyValueNew = False                                     ' Return Failure
            rc = RegCloseKey(hKey)                                  ' Close Registry Key
        End Try
        
    End Function

    Private Sub btnSystemInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSystemInfo.Click
        Me.StartSysInfo()
    End Sub

    Private Sub lblURL_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblURL.LinkClicked
        System.Diagnostics.Process.Start("http://www.lumensoft.biz")
        Me.lblURL.LinkVisited = True
    End Sub
End Class
