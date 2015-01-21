If Not Exists(select Cheque_Name from tblChequeTemplates where Cheque_Name = 'Standard Chartered New')
Begin
Insert into tblChequeTemplates (Cheque_name, Cheque_template, Cheque_Report) 
Values ('Standard Chartered New','SC','crptChequeStandardNew.rpt')
End

If Not Exists(select Cheque_Name from tblChequeTemplates where Cheque_Name = 'Faysal Bank New')
Begin
Insert into tblChequeTemplates (Cheque_name, Cheque_template, Cheque_Report) 
Values ('Faysal Bank New','FBLN','crptChequeBankFaysalNew.rpt')
End

If Not Exists(select Cheque_Name from tblChequeTemplates where Cheque_Name = 'MCB Bank New')
Begin
Insert into tblChequeTemplates (Cheque_name, Cheque_template, Cheque_Report) 
Values ('MCB Bank New','MCBN','crptChequeBankMCBNew.rpt')
End