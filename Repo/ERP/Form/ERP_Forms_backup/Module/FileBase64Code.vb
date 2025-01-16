Option Strict Off
Option Explicit On
Imports System.Collections.Generic
Imports System.IO
Imports System.Stream

'Imports GemBox.Pdf.Forms
'Imports GemBox.Pdf.Security
'Imports GemBox.Pdf

Imports System
Imports System.Linq


Imports Net.Pkcs11Interop
'Imports Pkcs11Interop.PDF
Imports Net.Pkcs11Interop.PDF
Imports System.Security.Cryptography.X509Certificates
Imports Org.BouncyCastle.Security
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.security
'Imports Pkcs11Explorer

'Imports iTextSharp.text
'Imports iTextSharp.text.pdf
'Imports iTextSharp.text.pdf.security
'Imports NUnit.Framework





Public Module FileBase64Code
    Public Function EncodeFileBase64(ByVal fileName As String) As String
        Return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName))
    End Function
    Public Function DecodeBase64(fileName As String) As String
        'Return Convert.FromBase64String(System.IO.File.ReadAllBytes(fileName))
        Return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(fileName))
    End Function
    Public Function SignPdf(pPDFFileName As String, pPDFOutFileName As String, mFindAuthority As String) As Boolean
        On Error GoTo Err_PW
        Dim AsmxUrl As String
        Dim SoapActionUrl As String = ""
        Dim XmlBody As String, XmlBody2 As String
        Dim strSoapAction As String
        Dim mEncodeFileString As String
        Dim mUserName As String
        Dim mPassword As String
        'Dim mSignerName As String
        Dim mTopLeft As Long
        Dim mBottemLeft As Long
        Dim mTopRight As Long
        Dim mBottomRight As Long
        Dim mBMPFileName As String
        Dim mAuthorizedSignatory As String
        Dim pIsTesting As String
        Dim mSignerName As String
        Dim mDSCertidficateNo As String
        Dim mDSCertificateType As String
        Dim mDSCSignerName As String = ""
        Dim mFontSize As Long = 24
        Dim mFindLocation As Long = 0
        SignPdf = False

        If mDSCertificateType = "T" Then
            mDSCSignerName = GetDigitalSignName(PubUserID)

            'If MainClass.ValidateWithMasterTable(PubUserID, "USER_ID", "DS_USERID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIGITAL_SIGN='Y'") = True Then
            '    mDSCSignerName = MasterNo
            'End If

            If mDSCSignerName = "" Then
                MsgInformation("User has Not Rights for Digital Sign")
                SignPdf = False
                Exit Function
            End If

            mFindAuthority = "For " & RsCompany.Fields("PRINT_COMPANY_NAME").Value  ''"Authorised Signatory"
            mFindLocation = 1
        Else
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                mFindLocation = 0
            Else

                mDSCSignerName = GetDigitalSignName(PubUserID)

                'If MainClass.ValidateWithMasterTable(PubUserID, "USER_ID", "DS_USERID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIGITAL_SIGN='Y'") = True Then
                '    mDSCSignerName = MasterNo
                'End If

                If mDSCSignerName = "" Then
                    MsgInformation("User has Not Rights for Digital Sign")
                    SignPdf = False
                    Exit Function
                End If

                mFindAuthority = "For " & RsCompany.Fields("PRINT_COMPANY_NAME").Value  ''"Authorised Signatory"
                mFindLocation = 1
            End If
        End If


        ''Authorised Signatory

        'MsgBox("Check Token Type")
        If GetDigitalSignSetupContents(AsmxUrl, mUserName, mPassword, mAuthorizedSignatory, mTopLeft, mBottemLeft, mTopRight, mBottomRight, mSignerName, pIsTesting, mDSCertidficateNo, mDSCertificateType, mFontSize, mFindAuthority, mFindLocation) = False Then GoTo Err_PW

        'MsgBox(mDSCertificateType)
        If mDSCertificateType = "T" Then
            If IndividualSignPdf(pPDFFileName, pPDFOutFileName, mAuthorizedSignatory, mTopLeft, mBottemLeft, mTopRight, mBottomRight, mSignerName) = False Then GoTo Err_PW
            SignPdf = True
            Exit Function
        End If

        If pIsTesting = "Y" Then
            'AsmxUrl = http : //ip.webtel.in/webesignapi/service.asmx
            '/
            'AsmxUrl = "http://103.178.248.99:83/Esignapi/service.asmx" '' "http://ip.webtel.in/webesignapi/service.asmx"       ''"http://192.168.0.191/service.asmx" '' "http://ip.webtel.in/webesignapi/service.asmx"  "http://192.168.0.191:82/service.asmx" ''
            'strSoapAction = "http://tempuri.org/SignPDF_Base64String"

            'mEncodeFileString = EncodeFileBase64(pPDFFileName)  ' EncodeFileBase64(pPDFFileName)

            'mUserName = "rR482Xeoilw"  '' "admin" ''
            'mPassword = "Rqsie103pd"  '' "admin@123" ''
            'mSignerName = "ASHOK SHARMA"      '' "Susheel Sharma"
            'mTopLeft = 100
            'mBottemLeft = 290
            'mTopRight = 190
            'mBottomRight = 340
            'mAuthorizedSignatory = "KAY JAY FORGINGS PVT LTD" ''TestNew

        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value <> 101 Then
            mSignerName = mDSCSignerName
        End If

        strSoapAction = "http://tempuri.org/SignPDF_Base64String"

        mEncodeFileString = EncodeFileBase64(pPDFFileName)  ' EncodeFileBase64(pPDFFileName)



        XmlBody = "<?xml version=""1.0"" encoding=""utf-8"" ?>" &
                    "<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">" &
                    "<soap:Header>" &
                    "<AuthHeader xmlns=""http://tempuri.org/"">" &
                    "<Username>" & mUserName & "</Username>" &
                    "<Password>" & mPassword & "</Password>" &
                    "</AuthHeader>" &
                    "</soap:Header>" &
                    "<soap:Body>" &
                    "<SignPDF_Base64String xmlns=""http://tempuri.org/"">" &
                    "<pdfByte1>" & mEncodeFileString & "</pdfByte1>" &
                    "<AuthorizedSignatory>" & mAuthorizedSignatory & "</AuthorizedSignatory>" &
                    "<SignerName>" & mSignerName & "</SignerName> " &
                    "<TopLeft>" & mTopLeft & "</TopLeft>" &
                    "<BottomLeft>" & mBottemLeft & "</BottomLeft>" &
                    "<TopRight>" & mTopRight & "</TopRight>" &
                    "<BottomRight>" & mBottomRight & "</BottomRight>" &
                    "<ExcludePageNo />" &
                    "<InvoiceNumber />" &
                    "<pageNo>-1</pageNo>" &
                    "<PrintDateTime />" &
                    "<FindAuth>" & mFindAuthority & "</FindAuth>"

        ''pageNo -1 for all pages

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then

        Else
            XmlBody = XmlBody &
                "<fontsize>" & mFontSize & "</fontsize>" &
                "<adjustCoordinates>0</adjustCoordinates>" &
                "<signOnlySearchTextPage>0</signOnlySearchTextPage>"

        End If

        XmlBody2 = "<FindAuthLocation>" & mFindLocation & "</FindAuthLocation>" &
                   "</SignPDF_Base64String>" &
                   "</soap:Body>" &
                   "</soap:Envelope>"



        '        <fontsize>23</fontsize>
        '        <adjustCoordinates>0</adjustCoordinates>
        '        <signOnlySearchTextPage>0</signOnlySearchTextPage>



        XmlBody = XmlBody & XmlBody2

        Dim objDom As Object
        Dim objXmlHttp As Object
        Dim strRet As String
        Dim intPos1 As Long
        Dim intPos2 As Long
        On Error GoTo Err_PW
        objDom = CreateObject("MSXML2.DOMDocument")
        objXmlHttp = CreateObject("MSXML2.XMLHTTP")
        objDom.async = False
        objDom.loadXML(XmlBody)
        objXmlHttp.Open("POST", AsmxUrl, False)
        objXmlHttp.setRequestHeader("Content-Type", "text/xml; charset=utf-8")
        objXmlHttp.setRequestHeader("SOAPAction", SoapActionUrl)
        objXmlHttp.Send(objDom.xml)
        strRet = objXmlHttp.responseText

        objXmlHttp = Nothing
        intPos1 = InStr(strRet, "Result>") + 7
        intPos2 = InStr(strRet, "</")
        If intPos1 > 7 And intPos2 > 0 Then
            strRet = Mid(strRet, intPos1, intPos2 - intPos1)
            strRet = Replace(strRet, "<OutputFile>", "")
        Else
            strRet = ""
        End If

        IO.File.WriteAllBytes(pPDFOutFileName, Convert.FromBase64String(strRet))

        SignPdf = True

        Exit Function
Err_PW:
        'Resume
        MsgBox(Err.Description)
        SignPdf = False
    End Function
    Public Function IndividualSignPdf(ByVal xPDFFileName As String, ByVal xPDFOutFileName As String,
                                      ByVal mAuthorizedSignatory As String,
                                      ByVal mTopLeft As Long, ByVal mBottemLeft As Long, ByVal mTopRight As Long, ByVal mBottomRight As Long,
                                      ByVal mSignerName As String) As Boolean
        On Error GoTo ErrPart

        Dim mUserName As String = ""
        Dim mPassword As String = ""
        Dim mDSCertidficateNo As String = ""
        Dim mDLLPathName As String = ""
        Dim mDLLFileName As String = ""
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim libinfo As String = ""


        'Dim pkcs11SoftwareModuleDirectory = If(IntPtr.Size = 8, "C:\Windows\sysWOW64", "C:\Windows\System32")
        'libinfo = Path.Combine(pkcs11SoftwareModuleDirectory, "CryptoIDA_pkcs11.dll")   ''"C:\Windows\System32\CryptoIDA_pkcs11.dll"
        'MsgBox("Start")

        'If GetDigitalSignTokenDetails(mUserName, mPassword, mDSCertidficateNo, mDLLPathName, mDLLFileName) = True Then GoTo ErrPart

        SqlStr = "SELECT * " & vbCrLf _
            & " FROM ATH_PASSWORD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
        If RsTemp.EOF = False Then
            mUserName = IIf(IsDBNull(RsTemp.Fields("DS_USERID").Value), "", RsTemp.Fields("DS_USERID").Value)
            mPassword = IIf(IsDBNull(RsTemp.Fields("DS_PASSWORD").Value), "", RsTemp.Fields("DS_PASSWORD").Value)
            mDSCertidficateNo = IIf(IsDBNull(RsTemp.Fields("DS_CERTIFICATE_SNO").Value), "", RsTemp.Fields("DS_CERTIFICATE_SNO").Value)
            mDLLPathName = IIf(IsDBNull(RsTemp.Fields("DS_DLL_PATH").Value), "", RsTemp.Fields("DS_DLL_PATH").Value)
            mDLLFileName = IIf(IsDBNull(RsTemp.Fields("DS_DLL_FILENAME").Value), "", RsTemp.Fields("DS_DLL_FILENAME").Value)  '', 
        End If

        If mUserName = "" Then
            MsgBox("DSC Config. Not Defined")
            IndividualSignPdf = True
            Exit Function
        End If
        'MsgBox(mUserName & mPassword & mDSCertidficateNo & mDLLPathName & mDLLFileName)

        libinfo = mDLLPathName & "\" & mDLLFileName

        'MsgBox(libinfo)

        Dim tokeninfo As New Pkcs11Explorer(libinfo)       ''libinfo)
        tokeninfo.GetTokens()

        'MsgBox("Get Token")

        Dim privateKeys As List(Of PrivateKey) = Nothing
        Dim certificates As List(Of Certificate) = Nothing
        Dim ckalabel = Nothing '"BINU JOHN"
        Dim ckaserial = Nothing '"c4bedc2cd9289235bb220c8530e01115e05e8e677395f4f896d01161613851f0"
        Dim tokensr As Object = Nothing
        Dim tokenlbl As Object = Nothing
        Dim tokenpass As Object = Nothing
        Dim certsr As Object = Nothing
        Dim pvindex As Double = 0


        For Each token In tokeninfo.GetTokens
            'If token.SerialNumber = "IN19060100011382" Then
            'MsgBox("Start Token")
            tokenpass = mPassword        '' "1234567890"
            tokeninfo.GetTokenObjects(token, True, tokenpass, privateKeys, certificates)

            'MsgBox("Start token info")
            tokensr = token.SerialNumber
            tokenlbl = token.Label

            Console.WriteLine("token.SerialNumber " & token.SerialNumber)
            Console.WriteLine("token.Label " & token.Label)

            'MsgBox("token.SerialNumber " & token.SerialNumber)
            'MsgBox("token.Label " & token.Label)
        Next



        certsr = mDSCertidficateNo   '' "030224ae"
        pvindex = 0


        Dim certsDir As String = Nothing
        '"C:\Program Files x86\CryptoID\CryptoIDA_pkcs11.dll"
        'Using pkcs11RsaSignature As Pkcs11RsaSignature = New Pkcs11RsaSignature(libinfo, tokensr, tokenlbl, tokenpass, privateKeys(pvindex).Label, privateKeys(pvindex).Id, HashAlgorithm.SHA256)
        Using pkcs11RsaSignature As Pkcs11RsaSignature = New Pkcs11RsaSignature(libinfo, tokensr, tokenlbl, tokenpass, privateKeys(pvindex).Label, privateKeys(pvindex).Id, HashAlgorithm.SHA256)
            Dim certClient As X509Certificate2 = Nothing

            Dim st As X509Store = New X509Store(StoreName.My, StoreLocation.CurrentUser)
            st.Open(OpenFlags.MaxAllowed)
            For i As Integer = 0 To st.Certificates.Count - 1

                If String.Compare(st.Certificates(i).SerialNumber, certsr, True) = 0 Then
                    certClient = st.Certificates(i)
                    Console.WriteLine(certClient.SerialNumber)
                    Console.WriteLine(certClient.Subject)

                    'MsgBox(certClient.SerialNumber)
                    'MsgBox(certClient.Subject)

                    Exit For
                End If
            Next

            Dim chain As IList(Of Org.BouncyCastle.X509.X509Certificate) = New List(Of Org.BouncyCastle.X509.X509Certificate)()
            Dim x509Chain As X509Chain = New X509Chain()
            x509Chain.Build(certClient)
            Dim X509Certificate2 = Nothing
            For Each x509ChainElement As X509ChainElement In x509Chain.ChainElements
                chain.Add(DotNetUtilities.FromX509Certificate(x509ChainElement.Certificate))
                X509Certificate2 = x509ChainElement.Certificate
            Next

            Dim mPage As Integer

            'Using PdfReader As PdfReader = New PdfReader(xPDFFileName)
            '    Using outputStream As FileStream = New FileStream(xPDFOutFileName, FileMode.Create)
            '        Using pdfStamper As PdfStamper = PdfStamper.CreateSignature(PdfReader, outputStream, vbNullChar, Path.GetTempFileName, True)

            Dim PdfReader As PdfReader = New PdfReader(xPDFFileName)
            Dim outputStream As FileStream = New FileStream(xPDFOutFileName, FileMode.Create)
            Dim pdfStamper As PdfStamper = PdfStamper.CreateSignature(PdfReader, outputStream, vbNullChar, Path.GetTempFileName, True)

            Dim signatureAppearance As PdfSignatureAppearance = pdfStamper.SignatureAppearance

            For mPage = 1 To PdfReader.NumberOfPages

                signatureAppearance.Acro6Layers = False
                signatureAppearance.Layer4Text = PdfSignatureAppearance.questionMark
                signatureAppearance.SetVisibleSignature(New iTextSharp.text.Rectangle(mTopLeft, mBottemLeft, mTopRight, mBottomRight), mPage, "Authorised Signatory" & mPage.ToString()) ''"Authorised Signatory"
                signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.DESCRIPTION
            Next
            MakeSignature.SignDetached(pdfStamper.SignatureAppearance, pkcs11RsaSignature, chain, Nothing, Nothing, Nothing, 0, CryptoStandard.CADES)
            'MakeSignature.SignDetached(pdfStamper.SignatureAppearance, pkcs11RsaSignature, chain, Nothing, Nothing, Nothing, 0, CryptoStandard.CMS)
            '        End Using
            '    End Using
            'End Using

            pdfStamper.Close()
            outputStream.Close()
            PdfReader.Close()

        End Using

        IndividualSignPdf = True
        Exit Function
ErrPart:
        'Resume
        MsgBox(Err.Description)
        IndividualSignPdf = False
    End Function
    Public Function IndividualSignPdfGem(pPDFFileName As String, pPDFOutFileName As String) As Boolean
        'On Error GoTo ErrPart
        'Dim pkcs11SoftwareModuleDirectory = If(IntPtr.Size = 8, "C:\Windows\sysWOW64", "C:\Windows\System32") ''        "G:\VBDotNetERP_Working\GemBoxPkcs11SoftwareModule"
        ''If(IntPtr.Size = 8, "softhsm2-x64.dll", "softhsm2.dll"))

        ''System.IO.Compression.ZipFile.ExtractToDirectory("GemBoxPkcs11SoftwareModule.zip", pkcs11SoftwareModuleDirectory)
        ''Environment.SetEnvironmentVariable("SOFTHSM2_CONF", Path.Combine(pkcs11SoftwareModuleDirectory, "softhsm2.conf"))  ''Temp mark sandeep

        '' Specify path to PKCS#11/Cryptoki library, depending on the runtime architecture (64-bit or 32-bit).
        ''Dim libraryPath = Path.Combine(pkcs11SoftwareModuleDirectory, If(IntPtr.Size = 8, "softhsm2-x64.dll", "softhsm2.dll"))
        'Dim libraryPath = Path.Combine(pkcs11SoftwareModuleDirectory, "CryptoIDA_pkcs11.dll")       'If(IntPtr.Size = 8, "softhsm2-x64.dll", "softhsm2.dll"))

        '' If using Professional version, put your serial key below.
        'ComponentInfo.SetLicense("FREE-LIMITED-KEY")

        'Using pkcs11Module = New PdfPkcs11Module(libraryPath)

        '    ' Get a token from PKCS#11/Cryptoki device.
        '    Dim token = pkcs11Module.Tokens.Single(Function(t) t.TokenLabel = "IN19060100011382")      '' "CryptoID_Setup") "GemBoxECDsaToken" ''Capricorn CA 2014 ''sha256RSA

        '    ' Login to the token to get access to protected cryptographic functions.
        '    token.Login("1234567890")       ''"GemBoxECDsaPin")

        '    ' Get a digital ID from PKCS#11/Cryptoki device token.
        '    Dim digitalId = token.DigitalIds.Single(Function(id) id.Certificate.SubjectCommonName = "VINEET BATHLA")        ''"GemBoxECDsa521")

        '    Using document = PdfDocument.Load(pPDFFileName)

        '        ' Add a visible signature field to the first page of the PDF document.
        '        Dim signatureField = document.Form.Fields.AddSignature(document.Pages(0), 300, 500, 250, 50)

        '        ' Create a PDF signer that will create the digital signature.
        '        Dim signer = New PdfSigner(digitalId)

        '        ' Adobe Acrobat Reader currently doesn't download certificate chain
        '        ' so we will also embed certificate of intermediate Certificate Authority in the signature.
        '        ' (see https://community.adobe.com/t5/acrobat/signature-validation-using-aia-extension-Not-enabled-by-default/td-p/10729647)
        '        Dim intermediateCA = token.DigitalIds.Single(Function(id) id.Certificate.SubjectCommonName = "GemBoxECDsa").Certificate
        '        signer.ValidationInfo = New PdfSignatureValidationInfo(New PdfCertificate() {intermediateCA}, Nothing, Nothing)

        '        ' Initiate signing of a PDF file with the specified signer.
        '        signatureField.Sign(signer)

        '        'Finish signing of a PDF file.
        '        document.Save("Digital Signature PKCS#11.pdf")
        '    End Using

        '    token.Logout()
        'End Using

        IndividualSignPdfGem = True
        Exit Function
ErrPart:
        'Resume
        MsgBox(Err.Description)
        IndividualSignPdfGem = False
    End Function
End Module