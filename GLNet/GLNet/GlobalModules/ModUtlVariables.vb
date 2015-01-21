
Imports Model


Module ModUtlVariables


#Region "Public Variables"

    Public gobjMyImageListForOperationBar As System.Windows.Forms.ImageList
    Public gObjUserInfo As SecurityUser
    Public gObjFinancialYearInfo As FiniancialYear
    Public gobjBusinessStartDate As Date
    Public gobjLocationInfo As Company
    Public G_START_DATE As String
    Public G_END_DATE As String
    Public pbIsCallFromSearchForm As Boolean = False


    Public gObjToolTip As New ToolTip

    Public gEnumIsRightToLeft As Windows.Forms.RightToLeft


    Public gintProductHelpLeftLocation As Integer = 0
    Public gintProductHelpTopLocation As Integer = 0
    Public gstrReportPath As String = Application.StartupPath & "\Reports\" '"D:\OfficeWork\GLNet\GLNet\bin\Debug\" ' Tariq .. 

  



#End Region

#Region "Global Variables For UI"

    '  Public _blnTempVouchers As Boolean = False
    Public _blnCallFromSearchPost As Boolean = False
    Public _gstrVoucherIDs As String = String.Empty

    Public _TempVouchers As Boolean = False

    Public gstrMsgTitle As String = "LW Accounting System"

    ''DML Confirmation Messages
    Public gblnShowSaveConfirmationMessages As Boolean = True
    Public gblnShowAfterUpdateMessages As Boolean = True

    Public gstrComboZeroIndexString As String = "---Select---"

    Public gstrMsgSave As String = "Are you sure you want to add this record?"
    Public gstrMsgUpdate As String = "Are you sure you want to update this record?"
    Public gstrMsgAfterUpdate As String = "Record has been updated successfully"
    Public gstrMsgDelete As String = "Are you sure you want to delete this record?"
    Public gstrMsgDeleteByEndDate As String

    ' Validation Messages
    Public gstrMsgNameRequired As String
    Public gstrMsgCodeRequired As String
    Public gstrMsgWrongInput As String

    ' After Export to excel or to CSV Messages
    Public gstrMsgAfterExport As String

    ''Security Rights Messages
    Public gstrMsgRightToViewOption As String

    ' '' ''Visual Effect Variables
    Public gobjRequiredFieldtBKColor As Color = System.Drawing.Color.FromArgb(255, 255, 192)
    Public gobjDisabledFieldtBKColor As Color = System.Drawing.Color.FromArgb(255, 201, 222, 250)  ''System.Windows.Forms.Button.DefaultBackColor ''System.Drawing.Color.FromArgb(255, 255, 192)
    Public gobjDefaultForColorForInputField As Color = System.Windows.Forms.Button.DefaultForeColor ''System.Drawing.Color.FromArgb(255, 255, 192)
    Public gobjDefaultFontSettingForLables As Font
    Public gobjDefaultFontSettingForInput As Font
    Public gobjDefaultFontSettingForTabs As Font
    Public gobjDefaultFontSettingForMenu As Font
    Public _gintMaxTrialTransactions As Integer = 50
    Public gblnTrialVersion As Boolean





#End Region

End Module

