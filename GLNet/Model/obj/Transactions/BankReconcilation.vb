
''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Lumensoft GL
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : BankReconcilation.vb           				                            
''// Programmer	     : R@! Shahid
''// Creation Date	 : 16-Jul-2009
''// Description     : This form will be used to reconcile banks
''//                 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////

Public Class BankReconcilation

    Private _ActivityLog As ActivityLog

    Public Sub New()
        Me._ActivityLog = New ActivityLog
    End Sub

    Public Property ActivityLog() As ActivityLog
        Get
            Return Me._ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            Me._ActivityLog = value
        End Set
    End Property

End Class
