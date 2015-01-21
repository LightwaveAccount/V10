Public Class Form1

    Private Sub UiCtrlGLAccount1_GetGLAccount(ByVal sender As uiCtrlGLAccount) Handles UiCtrlGLAccount1.GetGLAccount
        UiCtrlGLAccount2.GLFilterCondition = UiCtrlGLAccount1.GLAccountID
    End Sub
End Class