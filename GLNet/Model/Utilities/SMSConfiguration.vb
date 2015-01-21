''/////////////////////////////////////////////////////////////////////////////////////////
''//                        SMS Configuration utility 
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : SMSConfiguration.vb           				                            
''// Programmer	     : Farooq ul hassan 
''// Creation Date	 : 09-July-2012
''// Description     :  CR#241  
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by           CR#        Brief Description		
''//
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////
Public Class SMSConfiguration
    Private _SMSConfig_ID As Integer
    Private _SMSCode As String
    Private _ScreenName As String
    Private _Action As String
    Private _Mode As String
    Private _SMSRecipient As String
    Private _SendSMS As Boolean
    Private _SMS As String
    Private _PhoneNumber As String
    Private _ActivityLog As ActivityLog

    Public Sub New()
        Me._ActivityLog = New ActivityLog()
    End Sub

    Public Property SMSConfig_id() As Integer
        Get
            Return _SMSConfig_ID
        End Get
        Set(ByVal value As Integer)
            _SMSConfig_ID = value
        End Set
    End Property

    Public Property SMSCode() As String
        Get
            Return _SMSCode
        End Get
        Set(ByVal value As String)
            _SMSCode = value
        End Set
    End Property

    Public Property ScreenName() As String
        Get
            Return _ScreenName
        End Get
        Set(ByVal value As String)
            _ScreenName = value
        End Set
    End Property


    Public Property Action() As String
        Get
            Return _Action
        End Get
        Set(ByVal value As String)
            _Action = value
        End Set
    End Property

    Public Property Mode() As String
        Get
            Return _Mode
        End Get
        Set(ByVal value As String)
            _Mode = value
        End Set
    End Property

    Public Property SMSRecipient() As String
        Get
            Return _SMSRecipient
        End Get
        Set(ByVal value As String)
            _SMSRecipient = value
        End Set
    End Property

    Public Property SendSMS() As Boolean
        Get
            Return _SendSMS
        End Get
        Set(ByVal value As Boolean)
            _SendSMS = value
        End Set
    End Property

    Public Property SMS() As String
        Get
            Return _SMS
        End Get
        Set(ByVal value As String)
            _SMS = value
        End Set
    End Property

    Public Property PhoneNumber() As String
        Get
            Return _PhoneNumber
        End Get
        Set(ByVal value As String)
            _PhoneNumber = value
        End Set
    End Property

    Public Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property
End Class

Public Class SMSLog
    Private _SMS_ID As Integer
    Private _SMS_Number As String
    Private _Sms_Text As String
    Private _Send_Status As Boolean
    Private _ActivityLog As ActivityLog

    Public Property SMS_ID() As Integer
        Get
            Return _SMS_ID
        End Get
        Set(ByVal value As Integer)
            _SMS_ID = value
        End Set
    End Property
    Public Property SMS_Number() As String
        Get
            Return _SMS_Number
        End Get
        Set(ByVal value As String)
            _SMS_Number = value
        End Set
    End Property

    Public Property SMS_Text() As String
        Get
            Return _Sms_Text
        End Get
        Set(ByVal value As String)
            _Sms_Text = value
        End Set
    End Property

    Public Property Send_Status() As Boolean
        Get
            Return _Send_Status
        End Get
        Set(ByVal value As Boolean)
            _Send_Status = value
        End Set
    End Property

    Public Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property

End Class
