''/////////////////////////////////////////////////////////////////////////////////////////
''//                     GL Transactions
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmSalesTaxInvoiceInfo.vb           				                            
''// Programmer	     : Fatima Tajammal
''// Creation Date	 : Oct 30,2014
''// Description     : 
''//-------------------------------------------------------------------------------------
''// CR#     Date Modified        Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// 334     30-oct-2014          Fatima Tajammal      Sale Tax Invoice: Sale Tax invoice printing option should be available in Lightwave
''// 346     15-dec-2014          Fatima Tajammal      Changes are required in Sale Tax Invoice
''// 349     29-dec-2014          Fatima Tajammal      Sale Tax Invoice print: Few changes are required in Printing Sale Tax invoice
Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Public Class frmSalesTaxInvoiceInfo
    Implements IGeneral

    Private _VoucherID As Integer
    Public Property VoucherID() As Integer
        Get
            Return _VoucherID
        End Get
        Set(ByVal value As Integer)
            _VoucherID = value
        End Set
    End Property

    Private _LocationID As Integer
    Public Property LocationID() As Integer
        Get
            Return _LocationID
        End Get
        Set(ByVal value As Integer)
            _LocationID = value
        End Set
    End Property

    Private _VoucherDes As String
    Public Property VoucherDes() As String
        Get
            Return _VoucherDes
        End Get
        Set(ByVal value As String)
            _VoucherDes = value
        End Set
    End Property

    Private _CustomerName As String
    Public Property CustomerName() As String
        Get
            Return _CustomerName
        End Get
        Set(ByVal value As String)
            _CustomerName = value
        End Set
    End Property

    Private _NationalTaxNo As String
    Public Property NationalTaxNo() As String
        Get
            Return _NationalTaxNo
        End Get
        Set(ByVal value As String)
            _NationalTaxNo = value
        End Set
    End Property

    Private _VDesc As String
    Public Property VDesc() As String
        Get
            Return _VDesc
        End Get
        Set(ByVal value As String)
            _VDesc = value
        End Set
    End Property

    Private _AllowPrint As Boolean
    Public Property AllowPrint() As Boolean
        Get
            Return _AllowPrint
        End Get
        Set(ByVal value As Boolean)
            _AllowPrint = value
        End Set
    End Property

    Private _AllowExport As Boolean
    Public Property Allowexport() As Boolean
        Get
            Return _AllowExport
        End Get
        Set(ByVal value As Boolean)
            _AllowExport = value
        End Set
    End Property

    Private _VoucherNO As String
    Public Property VoucherNO() As String
        Get
            Return _VoucherNO
        End Get
        Set(ByVal value As String)
            _VoucherNO = value
        End Set
    End Property

    'CR # 349
    Private _VoucherDate As Date
    Public Property VoucherDate() As Date
        Get
            Return _VoucherDate
        End Get
        Set(ByVal value As Date)
            _VoucherDate = value
        End Set
    End Property


    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub frmSalesTaxInvoiceInfo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmSalesTaxInvoiceInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillControls()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Cr # 334
    Public Sub fillControls()
        Try
            Me.txtVoucherdes.Text = Me._VoucherDes
            Me.txtInvoiceNo.Text = Me._VoucherNO
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            If Me.txtInvoiceNo.Text = String.Empty Then
                ShowInformationMessage("Please enter invoice number.")
                Me.txtInvoiceNo.Focus()
                Exit Sub
            End If
            If Me.txtUnit.Text = String.Empty Then
                ShowInformationMessage("Please enter unit.")
                Me.txtUnit.Focus()
                Exit Sub
            End If
            Dim objHashTableParamter As New Hashtable
            objHashTableParamter.Add("ReportPath", "\rptSalesTaxInvoice.rpt")
            objHashTableParamter.Add("@Voucher_id", Me._VoucherID)
            objHashTableParamter.Add("@Location_id", Me._LocationID)
            objHashTableParamter.Add("InvoiceNo", Me.txtInvoiceNo.Text)
            'COde commented against CR # 346
            'objHashTableParamter.Add("SalesTaxRegNo", Me.txtSalesTaxRegNo.Text)
            objHashTableParamter.Add("voucherDescription", Me.txtVoucherdes.Text)
            objHashTableParamter.Add("Unit", Me.txtUnit.Text)
            objHashTableParamter.Add("Customer_name", Me.CustomerName)
            'Code commented against CR # 346
            'objHashTableParamter.Add("National_Tax_No", Me.txtNationalTaxNO.Text)
            'Cr # 349
            objHashTableParamter.Add("VoucherDate", Me.VoucherDate)
            objHashTableParamter.Add("VDescrp", Me.VDesc)
            If Me.AllowPrint = False Then
                objHashTableParamter.Add("PrintRights", "False")
            Else
                objHashTableParamter.Add("PrintRights", "True")
            End If

            If Me.Allowexport = False Then
                objHashTableParamter.Add("ExportRights", "False")
            Else
                objHashTableParamter.Add("ExportRights", "True")
            End If

            gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)

            Dim rptViewer As New rptViewer
            rptViewer.Text = Me.Text
            rptViewer.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    'CR # 349
    'Allow only digit value in unit textbox
    Private Sub txtUnit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUnit.KeyPress
        Dim txtbx As TextBox = CType(sender, TextBox)
        If e.KeyChar = "." And txtbx.Text.Contains(".") Then
            e.Handled = True
            Exit Sub
        ElseIf e.KeyChar = "." And txtbx.Text = "" Then
            txtbx.Text = "0."
            txtbx.Select(txtbx.Text.Length, 0)
            e.Handled = True
            Exit Sub
        End If
        
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(8) Or e.KeyChar = Chr(46) Then
            e.Handled = False
        End If
    End Sub
End Class