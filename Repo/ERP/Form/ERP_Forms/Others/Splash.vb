Option Strict Off
Option Explicit On
Imports System.Data.SqlClient   '' System.Data.OleDb
Imports System.Data.OleDb
Imports ADODB
Imports Microsoft.VisualBasic.Compatibility

Friend Class frmSplash
	Inherits System.Windows.Forms.Form
	Dim mFormActive As Boolean
   Private Sub frmSplash_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

      Call ReadLicenseFile()

      lblVersion.Text = "Version " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision

        Dim pCopyRights As String = ""
        'Dim pCompanyAddress As String = ""
        'Dim pClientCompanyAddress As String = ""


        If pERPLogo = "" Then
            pCopyRights = pLicenseTo
            imgHema.Visible = False
            pERPNAME = ""
            ''lblERPName.Visible = False
        Else
            imgHema.Image = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\" & pERPLogo)  ''pLOGOPath
            pCopyRights = "gNxt Systems"
            imgHema.Visible = True
        End If

        lblLic.Text = "M/s " & pLicenseTo
        lblCopyRight.Text = "(c) " & pCopyRights
        ''lblERPName.Text = pERPNAME ''& " - "
        Label1.Text = pCompanyAddressLine1
        Label3.Text = pCompanyAddressLine2
        Label2.Text = pClientCompanyAddressLine1
        Label5.Text = pClientCompanyAddressLine2



        'App.Title = pERPNAME & "-" & mModelCaption ''HEILAdmin - The ERP
        mFormActive = False
   End Sub
	Private Sub frmSplash_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
      'Me.Hide()
	End Sub
	Private Sub frmSplash_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
		On Error GoTo ErrPart
		If mFormActive = True Then Exit Sub
      'FrmLogin.ShowDialog()
		mFormActive = True
ErrPart: 
		
	End Sub
	Private Sub frmSplash_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
      'Me.Hide()
	End Sub
	Private Sub fraMainFrame_MouseMove(ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef y As Single)
      'Me.Hide() ''frmSplash.DefInstance.Close()
   End Sub
   Private Sub ReadLicenseFile()
      On Error GoTo DSMCFGErr
      Dim mConfigFile As String
      Dim MyString As String = ""

        pLOGOName = "Nothing"
        pLOGOPath = "Nothing"   ''"CLogo.jpg"
        pLicenseTo = "Nothing"  ''"Company Name"
        pFormPic = ""
        pERPNAME = "SnapSoft"
        pERPLogo = "Nothing"
        pCompanyAddressLine1 = "Nothing"
        pCompanyAddressLine2 = "Nothing"
        pClientCompanyAddressLine1 = "Nothing"
        pClientCompanyAddressLine2 = "Nothing"

        mConfigFile = App_Path() & "\ERPConfig.CFG"

      If System.IO.File.Exists(mConfigFile) = False Then 'Config FILE DOES NOT EXIST
         MsgInformation("Configuration file not found at " & App_Path())
         GoTo ConnCondition
      End If

      FileOpen(1, mConfigFile, OpenMode.Input)
      Do While Not EOF(1) ' Loop until end of file.
         Input(1, MyString) ', MYNUMBER   ' Read data into two variables.

            If Mid(MyString, 1, 23) = "[COMPANY_ADDRESS_LINE1]" Then
                pCompanyAddressLine1 = Trim(Mid(MyString, 24))
            End If
            If Mid(MyString, 1, 23) = "[COMPANY_ADDRESS_LINE2]" Then
                pCompanyAddressLine2 = Trim(Mid(MyString, 24))
            End If
            If Mid(MyString, 1, 30) = "[CLIENT_COMPANY_ADDRESS_LINE1]" Then
                pClientCompanyAddressLine1 = Trim(Mid(MyString, 31))
            End If
            If Mid(MyString, 1, 30) = "[CLIENT_COMPANY_ADDRESS_LINE2]" Then
                pClientCompanyAddressLine2 = Trim(Mid(MyString, 31))
            End If

            If Mid(MyString, 1, 10) = "[LOGONAME]" Then
            pLOGOName = Trim(Mid(MyString, 12))
         End If

            If Mid(MyString, 1, 9) = "[ERPLOGO]" Then
                pERPLogo = Trim(Mid(MyString, 11))
            End If

            If Mid(MyString, 1, 9) = "[LOGOPIC]" Then
                pLOGOPath = Trim(Mid(MyString, 11))
            End If

            If Mid(MyString, 1, 9) = "[LICENSE]" Then
            pLicenseTo = Trim(Mid(MyString, 11))
         End If

         If Mid(MyString, 1, 10) = "[FORM_PIC]" Then
            pFormPic = Trim(Mid(MyString, 12))
         End If

            If Mid(MyString, 1, 10) = "[ERP_NAME]" Then
                pERPNAME = Trim(Mid(MyString, 12))
            End If

        Loop
      FileClose(1)

      Exit Sub
ConnCondition:
      Exit Sub
DSMCFGErr:
      FileClose(1)
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
   End Sub

    Private Sub imgHema_Click(sender As Object, e As EventArgs) Handles imgHema.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class