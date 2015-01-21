''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Gl Account Head Contorl .. 
''// Programmer	     : Tariq Majeed
''// Creation Date	 : 16-July-2009
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////


Imports System.Data.SqlClient
Imports DAL
Imports Utility.Utility

Public Enum EnumAccountTypes

    None = 0
    Bank = 1
    General = 2
    Cash = 3
    Vendor = 4
    Customer = 5

End Enum


Public Class uiCtrlGLAccount

#Region "Custom Properties"


    Private _AccountType As EnumAccountTypes
    Public Property AccountType() As EnumAccountTypes

        Get
            Return _AccountType
        End Get

        Set(ByVal value As EnumAccountTypes)
            _AccountType = value
        End Set

    End Property



    Private _Text As String
    Public Overrides Property Text() As String

        Get
            Return _Text
        End Get

        Set(ByVal value As String)
            _Text = value
            Me.txtACCode.Text = value
        End Set

    End Property


    Private _Tag As String
    Public Overloads Property Tag() As String

        Get
            Return _Tag
        End Get

        Set(ByVal value As String)
            _Tag = value
            Me.txtACCode.Tag = value
        End Set

    End Property


    Private _GLAccountCode As String
    Public Property GLAccountCode() As String

        Get
            Return _GLAccountCode

        End Get

        Set(ByVal value As String)
            _GLAccountCode = value
        End Set


    End Property


    Private _GLAccountName As String
    Public Property GLAccountName() As String

        Get
            Return _GLAccountName

        End Get

        Set(ByVal value As String)
            _GLAccountName = value
        End Set


    End Property



    Private _GLAccountID As Integer
    Public Property GLAccountID() As Integer

        Get
            Return _GLAccountID

        End Get

        Set(ByVal value As Integer)
            _GLAccountID = value
        End Set


    End Property

    Private _GLFilterCondition As String
    Public Property GLFilterCondition() As String

        Get
            Return _GLFilterCondition
        End Get

        Set(ByVal value As String)
            _GLFilterCondition = value
        End Set

    End Property



    Private _GLAccountHeadType As String
    Public Property GLAccountHeadType() As String

        Get
            Return _GLAccountHeadType
        End Get

        Set(ByVal value As String)
            If Not IsNumeric(value) Then
                _GLAccountHeadType = 1

            ElseIf value <= 1 Or value > 5 Then
                _GLAccountHeadType = 1

            Else
                _GLAccountHeadType = value

            End If

        End Set

    End Property


    Private _GLFilterAccount As String
    Public Property GLFilterAccount() As String

        Get
            Return _GLFilterAccount
        End Get

        Set(ByVal value As String)
            _GLFilterAccount = value

        End Set

    End Property



#End Region

#Region "Local Functions"

    Private Sub ShowHelp()

        Dim frmAccount As New frmAccountHelp
        frmAccount.ShowInTaskbar = False

        ' Call ApplyStyleSheet(frmAccount, EnumProjectForms.ForAllForms)


        frmAccount.FilterCondition = Me.GLFilterCondition
        frmAccount.AccountHeadType = Me.GLAccountHeadType
        frmAccount.AccountType = Me.AccountType.ToString
        frmAccount.GLFilterAccount = Me.GLFilterAccount

        frmAccount.ShowDialog()


        If Not frmAccount.Tag Is Nothing Then
            Me.txtACCode.Text = CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(1).Value ' Account Code ..
            Me.txtAccountName.Text = CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(2).Value ' Account Name ..

            Me.GLAccountID = CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(0).Value ' Account ID .. 
            Me.GLAccountName = CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(2).Value ' Account Name ..
            Me.GLAccountCode = CType(frmAccount.Tag, Janus.Windows.GridEX.GridEXRow).Cells(1).Value ' Account Code ..

            RaiseEvent GetGLAccount(Me)

        End If

    End Sub


#End Region


#Region "Events"

    Public Delegate Sub GLAccount(ByVal sender As uiCtrlGLAccount)
    Public Event GetGLAccount As GLAccount

    Private Sub txtACName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtACCode.KeyDown
        If e.KeyCode = Keys.F1 Then
            ShowHelp()

        End If

    End Sub


    Private Sub txtACName_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtACCode.Resize
        Me.Height = Me.txtACCode.Size.Height

    End Sub

    

    Friend Sub txtACName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtACCode.Validating
        Try

        
            Dim ObjDataTable As New System.Data.DataTable
            Dim ObjDataView As New DataView

            If IsDBNull(txtACCode.Text) Then Exit Sub

            If txtACCode.Text.ToString = "" Then Me.txtAccountName.Text = ""

            If txtACCode.Text.ToString.Replace("-", "").Trim = "" Then Exit Sub


            Dim ObjGLAccountDAL As New GLAccountDAL
            ObjDataTable = ObjGLAccountDAL.GetAll(Me.GLFilterCondition, Me.GLAccountHeadType, Me.AccountType.ToString, Me.GLFilterAccount)

            ObjDataView = GetFilterDataFromDataTable(ObjDataTable, String.Format("[AccountCode]= '{0}' ", Trim(txtACCode.Text)))

            If ObjDataView.Count <= 0 Then
                txtACCode.Text = ""
                txtAccountName.Text = ""
                txtACCode.Focus()
                Me.GLAccountID = 0
                'e.Cancel = True
                RaiseEvent GetGLAccount(Me)

            Else
                Me.txtAccountName.Text = ObjDataView.Item(0).Item(2).ToString  ' Account Name .. 
                Me.GLAccountID = ObjDataView.Item(0).Item(0).ToString ' Account ID .. 
                Me.GLAccountCode = ObjDataView.Item(0).Item(1).ToString ' Account Code .. 
                Me.GLAccountName = ObjDataView.Item(0).Item(2).ToString ' Account Name .. 

                RaiseEvent GetGLAccount(Me)

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click

        Try
            Me.txtACName_KeyDown(sender, New System.Windows.Forms.KeyEventArgs(Keys.F1))

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub

#End Region



End Class
