Attribute VB_Name = "AutoEmailModule"
Option Explicit
Public Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long
Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal lpFileName As String) As Long

Public RsCompany As ADODB.Recordset
Public PubDBCn As ADODB.Connection
Public StrConn As String
Public MasterNo As Variant
Public MasterDate As Date
Public AcName As String
Public AcName1 As String
Public myMenu As String
Public RunDate As Date
Public PubPAYYEAR As String
Public PubCurrDate As Date
Public PubUserID As String
Public PubUserEMPCode As String
Public PubAllowGrant As String
Public PubAllowPermission As String
Public PubAllowRunDateChange As String
Public PubATHUSER As Boolean
Public PubUserLevel As String
Public PubSuperUser As String
Public PubUserPWD As String
Public PubHO As String
Public PubRun_IN As String
Public mDOSPRINTING As Boolean
Public STRRptConn As String
Public mLocalPath  As String
Public PubCompanyCode As Long

Public Const ConSaleBook = "S"
Public Const ConPurchaseBook = "P"
Public Const ConPurchaseGenBook = "PGen"
Public Const ConGRBook = "G"
Public Const ConCashBook = "C"
Public Const ConBankBook = "B"
Public Const ConContraBook = "H"
Public Const ConPDCBook = "F"
Public Const ConJournalBook = "J"
Public Const ConDebitNoteBook = "E"
Public Const ConCreditNoteBook = "R"
Public Const ConOpeningBook = "O"
Public Const ConLedger = "LEDG"

Public pBARCODEFORMAT1  As String           '''Hero Honda Barcode
Public pBARCODEFORMAT2  As String           '''TVS Barcode
Public pBARCODEFORMAT3  As String           '''HEMA Unit Barcode
Public pBARCODEFORMAT4 As String            ''Munjal Showa
Public pBARCODEFORMAT5 As String            ''Omax Group
Public pBARCODEPRINTER  As String

Public pBARCODEPort  As Long
Public pBARCODEDarkNess  As String
    
Public DBConUID As String
Public DBConPWD As String
Public DBConDSN As String
Public DBConSERVICENAME As String

Public DBConTimeDSN As String
Public DBConTimeServer As String
Public DBConTimeDatabase As String

Public mEDTrfPath  As String
Public PubSourceData  As String
Public Const ConAccess = "ACCESS"

Public PubUSCn As ADODB.Connection
Public StrConnGrid As String
Public Const ConBlankDate = ""
Public Const ConcmdmodifyCaption = "&Modify"
Public Const ConCmdCancelCaption = "Ca&ncel"
Public Const ConCmdClearCaption = "C&lear"
Public Const ConCmdSaveCaption = "&Save"
Public Const ConCmdSaveCaption1 = "Sav&e"
Public Const ConCmdDeleteCaption = "&Delete"
Public Const ConCmdAddCaption = "&Add"
Public Const ConCmdGridViewCaption = "List &View"
Public Const ConCmdViewCaption = "Clear &View"
Public Const ConCmdSavePrintCaption = "&Save && Print"
'Private WithEvents poSendMail As vbSendMail.clsSendMail
Public poSendMail As New vbSendMail.clsSendMail

' misc local vars
Global bAuthLogin      As Boolean
Global bPopLogin       As Boolean
Global bHtml           As Boolean
Global MyEncodeType    As ENCODE_METHOD
Global etPriority      As MAIL_PRIORITY
Global bReceipt        As Boolean
Global strServerPop3 As String
Global strServerSmtp As String
Global strAccount As String
Global strPassword As String


Global emailAdd As String
Global outSourec As String
Global fieldArr(1 To 76, 1 To 2) As String
Global fText As String
Global fRTF As String
Global fonttbl() As String
Global colortbl() As String
Global mDEL As Boolean
Global mSAll As Boolean
Const boundary = "\plain"
Const mfont = "\f"
Const mfontsize = "\fs"
Const bold = "\b"
Const italic = "\i"
Const underline = "\ul"
Const para = "\par "
Public Const start = "\deflang1033\pard\plain"
Const finish = "\par }"

Public Type AlterItemArray
    mAlterCode As String
End Type
Public mAlterItemData() As AlterItemArray
Public Function GetScheduleDetail(mCompanyCode As Long, mItemCode As String, mDate As String, mFYEAR As Long, pPubDBCn As ADODB.Connection, mScheduleNo As String, mAmendNo As Long, mScheduleQty As String) As Boolean
On Error GoTo ErrPart
Dim mSqlStr As String
Dim RsTemp As ADODB.Recordset

    mSqlStr = " SELECT IH.AUTO_KEY_DELV, IH.DELV_AMEND_NO, ID.TOTAL_QTY " & vbCrLf _
            & " FROM PUR_DELV_SCHLD_HDR IH, PUR_DELV_SCHLD_DET ID " & vbCrLf _
            & " WHERE IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
            & " AND ID.ITEM_CODE='" & AllowSingleQuote(mItemCode) & "'" & vbCrLf _
            & " AND TO_CHAR(IH.SCHLD_DATE,'MON-YYYY')='" & UCase(Format(mDate, "MMM-YYYY")) & "'"
  
  ''& " AND SUBSTR(IH.AUTO_KEY_DELV,LENGTH(IH.AUTO_KEY_DELV)-5,4)=" & mFYEAR & "" & vbCrLf
  
    UOpenRecordSet mSqlStr, pPubDBCn, adOpenStatic, RsTemp, adLockReadOnly
    If RsTemp.EOF = False Then
        mScheduleNo = IIf(IsNull(RsTemp!AUTO_KEY_DELV), "", RsTemp!AUTO_KEY_DELV)
        mAmendNo = IIf(IsNull(RsTemp!DELV_AMEND_NO), 0, RsTemp!DELV_AMEND_NO)
        mScheduleQty = IIf(IsNull(RsTemp!TOTAL_QTY), "", RsTemp!TOTAL_QTY)
    End If
    GetScheduleDetail = True
Exit Function
ErrPart:
    GetScheduleDetail = False
End Function

Public Function GetTillDateScheduleDetail(mCompanyCode As Long, mItemCode As String, mDate As String, pPubDBCn As ADODB.Connection, mTillDateScheduleQty As String) As Boolean
On Error GoTo ErrPart
Dim mSqlStr As String
Dim RsTemp As ADODB.Recordset

    mSqlStr = " SELECT SUM(ID.PLANNED_QTY) AS PLANNED_QTY " & vbCrLf _
            & " FROM PUR_DELV_SCHLD_HDR IH, PUR_DAILY_SCHLD_DET ID " & vbCrLf _
            & " WHERE IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
            & " AND ID.ITEM_CODE='" & AllowSingleQuote(mItemCode) & "'" & vbCrLf _
            & " AND ID.SERIAL_DATE<='" & Format(mDate, "DD-MMM-YYYY") & "'" & vbCrLf _
            & " AND TO_CHAR(IH.SCHLD_DATE,'MON-YYYY')='" & UCase(Format(mDate, "MMM-YYYY")) & "'"
  
  ''& " AND SUBSTR(IH.AUTO_KEY_DELV,LENGTH(IH.AUTO_KEY_DELV)-5,4)=" & mFYEAR & "" & vbCrLf
  
    UOpenRecordSet mSqlStr, pPubDBCn, adOpenStatic, RsTemp, adLockReadOnly
    If RsTemp.EOF = False Then
        mTillDateScheduleQty = IIf(IsNull(RsTemp!PLANNED_QTY), "", RsTemp!PLANNED_QTY)
    End If
    GetTillDateScheduleDetail = True
Exit Function
ErrPart:
    GetTillDateScheduleDetail = False
End Function
Public Function GetMRRDetail(mCompanyCode As Long, mItemCode As String, mDate As String, mFYEAR As Long, pPubDBCn As ADODB.Connection) As Double
On Error GoTo ErrPart
Dim mSqlStr As String
Dim RsTemp As ADODB.Recordset
Dim mFromDate As String

    mFromDate = "01/" & Format(mDate, "MM/YYYY")
    GetMRRDetail = 0
    mSqlStr = " SELECT SUM(RECEIVED_QTY) AS RECEIVED_QTY " & vbCrLf _
            & " FROM INV_GATE_HDR IH, INV_GATE_DET ID " & vbCrLf _
            & " WHERE IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
            & " AND SUBSTR(IH.AUTO_KEY_MRR,LENGTH(IH.AUTO_KEY_MRR)-5,4)=" & mFYEAR & "" & vbCrLf _
            & " AND ID.ITEM_CODE='" & AllowSingleQuote(mItemCode) & "'" & vbCrLf _
            & " AND IH.REF_TYPE IN ('F','P') " & vbCrLf _
            & " AND IH.MRR_DATE>='" & Format(mFromDate, "DD/MMM/YYYY") & "'" & vbCrLf _
            & " AND IH.MRR_DATE<='" & Format(mDate, "DD/MMM/YYYY") & "'"
            
    UOpenRecordSet mSqlStr, pPubDBCn, adOpenStatic, RsTemp, adLockReadOnly
    If RsTemp.EOF = False Then
       GetMRRDetail = IIf(IsNull(RsTemp!RECEIVED_QTY), 0, RsTemp!RECEIVED_QTY)
    End If
    
Exit Function
ErrPart:
    GetMRRDetail = 0
End Function

Public Function GetDetail(mCompanyCode As Long, mItemCode As String, mDate As String, mFYEAR As Long, mOPQty As Double, mCLQty As Double, mTodayPurQty As Double, mPurQty As Double, mINHouseQty As Double, mTodayDespQty As Double, mDespQty As Double, pPubDBCn As ADODB.Connection, pAlterCode As String) As Boolean
On Error GoTo ErrPart
Dim mSqlStr As String
Dim RsTemp As ADODB.Recordset
Dim mFromDate As String
Dim xSqlStr As String
Dim mUpperBound As Long
Dim mAlterItemCodeStr As String
Dim I As Long
Dim xDespSqlQry As String

'Dim mOPQty As Double,mCLQty As Double,mPurQty As Double,mINHouseQty As Double,mDespQty As Double
'Dim mCLQty As Double
'Dim mPurQty As Double
'Dim mDespQty As Double
'Dim mINHouseQty As Double
Dim mItemUOM As String

    mFromDate = "01/" & Format(mDate, "MM/YYYY")
    GetDetail = False
    
    If ValidateWithMasterTable(mItemCode, "ITEM_CODE", "PURCHASE_UOM", "INV_ITEM_MST", pPubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
        mItemUOM = MasterNo
    End If
            
    xSqlStr = GetQueryForAlterItem(mItemCode)
    UOpenRecordSet xSqlStr, pPubDBCn, adOpenStatic, RsTemp, adLockReadOnly
    mUpperBound = 0
    mAlterItemCodeStr = ""
    If RsTemp.EOF = False Then
         Do While RsTemp.EOF = False
            RsTemp.MoveNext
            If RsTemp.EOF = False Then
                mUpperBound = mUpperBound + 1
            End If
        Loop
        ReDim mAlterItemData(mUpperBound)
        RsTemp.MoveFirst
        I = 0
        Do While RsTemp.EOF = False
            mAlterItemData(I).mAlterCode = Trim(IIf(IsNull(RsTemp!ALTER_RM_CODE), "", RsTemp!ALTER_RM_CODE))
            mAlterItemCodeStr = mAlterItemCodeStr & "/" & Trim(IIf(IsNull(RsTemp!ALTER_RM_CODE), "", RsTemp!ALTER_RM_CODE))
            RsTemp.MoveNext
            I = I + 1
        Loop
    Else
        ReDim mAlterItemData(0)
        mAlterItemData(0).mAlterCode = ""
    End If
    
    mOPQty = GetStockQty(mCompanyCode, mFYEAR, mItemCode, mItemUOM, "STR", "QC", ConWH, "OP", mFromDate, mDate, pPubDBCn)
    mPurQty = GetNetPurchase(mItemCode, mFromDate, mDate, pPubDBCn)    '' GetStockQty(mRMCode, mItemUOM, "STR", "", ConWH, "", "'" & ConStockRefType_MRR & "'")
    mTodayPurQty = GetNetPurchase(mItemCode, mDate, mDate, pPubDBCn)
    mINHouseQty = GetStockQty(mCompanyCode, mFYEAR, mItemCode, mItemUOM, "STR", "", ConWH, "", mFromDate, mDate, pPubDBCn, "'" & ConStockRefType_PISS & "'")
    mCLQty = GetStockQty(mCompanyCode, mFYEAR, mItemCode, mItemUOM, "STR", "QC", ConWH, "CL", mFromDate, mDate, pPubDBCn)
    
    xDespSqlQry = DespatchSqlQry((mItemCode))
    
    For I = 0 To mUpperBound
        If mAlterItemData(I).mAlterCode <> "" Then
            mOPQty = mOPQty + GetStockQty(mCompanyCode, mFYEAR, mAlterItemData(I).mAlterCode, mItemUOM, "STR", "QC", ConWH, "OP", mFromDate, mDate, pPubDBCn)
            mPurQty = mPurQty + GetNetPurchase(mAlterItemData(I).mAlterCode, mFromDate, mDate, pPubDBCn)
            mTodayPurQty = mTodayPurQty + GetNetPurchase(mAlterItemData(I).mAlterCode, mDate, mDate, pPubDBCn)
            mINHouseQty = mINHouseQty + GetStockQty(mCompanyCode, mFYEAR, mAlterItemData(I).mAlterCode, mItemUOM, "STR", "", ConWH, "", mFromDate, mDate, pPubDBCn, "'" & ConStockRefType_PISS & "'")
            mCLQty = mCLQty + GetStockQty(mCompanyCode, mFYEAR, mAlterItemData(I).mAlterCode, mItemUOM, "STR", "QC", ConWH, "CL", mFromDate, mDate, pPubDBCn)
'                    mDespQty = mDespQty + GetDespatchQty(mAlterItemData(I).mAlterCode)
            xDespSqlQry = xDespSqlQry & vbCrLf & " UNION " & vbCrLf & DespatchSqlQry((mAlterItemData(I).mAlterCode))
        End If
    Next
'
    mDespQty = GetDespatchQty(xDespSqlQry, mFromDate, mDate, pPubDBCn)
    mTodayDespQty = GetDespatchQty(xDespSqlQry, mDate, mDate, pPubDBCn)
    GetDetail = True
Exit Function
ErrPart:
    GetDetail = False
End Function
Private Function GetNetPurchase(pItemCode As String, pFromDate As String, pToDate As String, pPubDBCn As ADODB.Connection) As Double

On Error GoTo ErrPart
Dim SqlStr As String
Dim RsTemp As ADODB.Recordset
Dim mPurchaseReturn As Double

    
    SqlStr = ""
   
    
    SqlStr = "SELECT SUM(ID.BILL_QTY * DECODE(TRIM(ID.ITEM_UOM),TRIM(INVMST.PURCHASE_UOM),INVMST.UOM_FACTOR,1)) AS INQTY"
    SqlStr = SqlStr & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, INV_ITEM_MST INVMST"
    
    SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _
            & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND INVMST.ITEM_CODE='" & AllowSingleQuote(pItemCode) & "'"
   

    SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE>=TO_DATE('" & Format(pFromDate, "DD-MMM-YYYY") & "')"
    SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE<=TO_DATE('" & Format(pToDate, "DD-MMM-YYYY") & "')"

    UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RsTemp, adLockReadOnly
    
    If RsTemp.EOF = False Then
        If IsNull(RsTemp.Fields(0).Value) Then
            GetNetPurchase = 0
        Else
            GetNetPurchase = RsTemp.Fields(0).Value
        End If
    Else
        GetNetPurchase = 0
    End If
    
    SqlStr = ""
    
    SqlStr = "SELECT ABS(SUM(ID.PACKED_QTY * DECODE(TRIM(ID.ITEM_UOM),TRIM(INVMST.PURCHASE_UOM),INVMST.UOM_FACTOR,1))) AS INQTY"
    SqlStr = SqlStr & vbCrLf & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID, INV_ITEM_MST INVMST"
    
    SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE AND IH.DESP_TYPE<>'U'" & vbCrLf _
            & " AND INVMST.ITEM_CODE='" & AllowSingleQuote(pItemCode) & "'"
   

    SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE>=TO_DATE('" & Format(pFromDate, "DD-MMM-YYYY") & "')"
    SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE<=TO_DATE('" & Format(pToDate, "DD-MMM-YYYY") & "')"

    UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RsTemp, adLockReadOnly
    
    If RsTemp.EOF = False Then
        If IsNull(RsTemp.Fields(0).Value) Then
            mPurchaseReturn = 0
        Else
            mPurchaseReturn = RsTemp.Fields(0).Value
        End If
    Else
        mPurchaseReturn = 0
    End If
    
    GetNetPurchase = GetNetPurchase - mPurchaseReturn
    Set RsTemp = Nothing
 
Exit Function
ErrPart:
    GetNetPurchase = 0
End Function


Private Function GetStockQty(pCompanyCode As Long, pFYear As Long, pItemCode As String, pPackUnit As String, pDeptCode As String, _
pStockType As String, pStock_ID As String, xShowType As String, pFromDate As String, pToDate As String, pPubDBCn As ADODB.Connection, Optional pRefType As String) As Double

On Error GoTo ErrPart
Dim SqlStr As String
Dim RsDept As ADODB.Recordset
Dim RsBalStock As ADODB.Recordset
Dim mBalQty As Double

Dim RsTemp As ADODB.Recordset
Dim mIssueUOM As String
Dim mPurchaseUOM As String
Dim mFactor As Double
Dim mTableName As String
Dim mDeptCode As String

    mDeptCode = ""
    
    SqlStr = ""
   
    
    SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY"
    
    If pCompanyCode = 1 Then
        mTableName = "INV_STOCK_REC_TRN" & pFYear
    ElseIf pCompanyCode = 3 Or pCompanyCode = 10 Or pCompanyCode = 12 Then
        mTableName = "INV_STOCK_REC_TRN" & Format(pCompanyCode, "00") & pFYear
    Else
        mTableName = "INV_STOCK_REC_TRN"
    End If
    
    SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " "
    
    SqlStr = SqlStr & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND FYEAR=" & pFYear & ""
            
    SqlStr = SqlStr & vbCrLf & "AND STOCK_ID='" & pStock_ID & "'"
    
    SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"
    
    SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & AllowSingleQuote(pItemCode) & "'"
    
    If pDeptCode <> "" And pStock_ID = ConPH Then
        SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & pDeptCode & "'"
    ElseIf pDeptCode = "PAD" And pStock_ID = ConWH And pStockType = "FG" Then
                                ''02-08-2006
                                'SqlStr = SqlStr & vbCrLf & "AND (DEPT_CODE_FROM='" & pDeptCode & "' OR DEPT_CODE_TO='" & pDeptCode & "')"
    End If

    If pRefType <> "" Then
        SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN (" & pRefType & ")"
    End If
        
    If pStockType = "QC" Then
        If xShowType = "OP" Or xShowType = "CL" Then
        
        Else
            SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE IN ('ST','" & pStockType & "')"
        End If
    Else
        If pStockType = "" Then
'            SqlStr = SqlStr & vbCrLf & " AND E_DATE<=TO_DATE('" & Format(pDateTo, "dd-mmm-yyyy") & "')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='" & pStockType & "'"       '' AND E_DATE<=TO_DATE('" & Format(pDateTo, "dd-mmm-yyyy") & "')"
        End If
    End If
    
'    SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & Format((pDateTo), "DD-MMM-YYYY") & "')"
    
    If xShowType = "OP" Then
        SqlStr = SqlStr & vbCrLf & " AND REF_DATE<TO_DATE('" & Format(pFromDate, "DD-MMM-YYYY") & "')"
    ElseIf xShowType = "CL" Then
        SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & Format(pToDate, "DD-MMM-YYYY") & "')"
    Else
        SqlStr = SqlStr & vbCrLf & " AND REF_DATE>=TO_DATE('" & Format(pFromDate, "DD-MMM-YYYY") & "')"
        SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & Format(pToDate, "DD-MMM-YYYY") & "')"
    End If

    UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RsBalStock, adLockReadOnly
    
    If RsBalStock.EOF = False Then
        If IsNull(RsBalStock.Fields(0).Value) Then
            mBalQty = 0
        Else
            mBalQty = RsBalStock.Fields(0).Value
        End If
    Else
        mBalQty = 0
    End If
    
    Set RsBalStock = Nothing
    
    If mBalQty <> 0 Then
        Set RsTemp = Nothing

        SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR FROM INV_ITEM_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & pCompanyCode & " AND ITEM_CODE='" & AllowSingleQuote(pItemCode) & "'"
        UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RsTemp, adLockReadOnly

        If RsTemp.EOF = False Then
            mIssueUOM = IIf(IsNull(RsTemp!ISSUE_UOM), "", RsTemp!ISSUE_UOM)
            mPurchaseUOM = IIf(IsNull(RsTemp!PURCHASE_UOM), "", RsTemp!PURCHASE_UOM)
            mFactor = IIf(IsNull(RsTemp!UOM_FACTOR) Or RsTemp!UOM_FACTOR = 0, 1, RsTemp!UOM_FACTOR)

            If pPackUnit = mPurchaseUOM Then
                mBalQty = mBalQty / mFactor
            End If

            Set RsTemp = Nothing
'            RsTemp.Close
        End If
    End If
    
    GetStockQty = mBalQty

Exit Function
ErrPart:
    GetStockQty = 0
End Function
Private Function DespatchSqlQry(pItemCode As String) As String
On Error GoTo ErrPart
Dim SqlStr As String

    ''TRN.RM_CODE,
    DespatchSqlQry = ""
    SqlStr = " SELECT DISTINCT " & vbCrLf _
            & " PRODUCT_CODE, STD_QTY+  GROSS_WT_SCRAP AS STD_QTY, DEPT_CODE, LEVEL" & vbCrLf _
            & " FROM VW_PRD_BOM_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany!COMPANY_CODE & " AND STATUS='O'"

    SqlStr = SqlStr & vbCrLf _
            & " START WITH  COMPANY_CODE=" & RsCompany!COMPANY_CODE & " AND RM_CODE='" & AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE || ' ')=TRIM(RM_CODE) || COMPANY_CODE || ' '"
                
    DespatchSqlQry = SqlStr
  
Exit Function
ErrPart:
    DespatchSqlQry = ""
End Function



Private Function GetDespatchQty(pQry As String, pFromDate As String, pToDate As String, pPubDBCn As ADODB.Connection) As Double
''GetDespatchQty(pItemCode As String) As Double
On Error GoTo ErrPart
Dim SqlStr As String
Dim RsTemp As ADODB.Recordset
Dim mParentcode As String
Dim mChildCode As String
Dim mStdQty As Double
Dim mLevel As Long
Dim mDespQty As Double
Dim pItemUOM As String
Dim mSqlStrRel As String
Dim RsRel As ADODB.Recordset
Dim xProductRelCode As String

    GetDespatchQty = 0
    UOpenRecordSet pQry, pPubDBCn, adOpenStatic, RsTemp, adLockReadOnly
    
    mStdQty = 1
    If RsTemp.EOF = False Then
        Do While Not RsTemp.EOF
            mLevel = Val(IIf(IsNull(RsTemp!Level), 1, RsTemp!Level))
            If mLevel = 1 Then
                mStdQty = Format(IIf(IsNull(RsTemp!STD_QTY), "", RsTemp!STD_QTY), "0.0000")
            Else
                mStdQty = mStdQty * Format(IIf(IsNull(RsTemp!STD_QTY), "", RsTemp!STD_QTY), "0.0000")
            End If
            
            mParentcode = Trim(IIf(IsNull(RsTemp!PRODUCT_CODE), "", RsTemp!PRODUCT_CODE))
            
            If ValidateWithMasterTable(mParentcode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", pPubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany!COMPANY_CODE & "") = True Then
                pItemUOM = Trim(MasterNo)
            End If
        
            
            mDespQty = GetNetDespatch(mParentcode, pFromDate, pToDate, pPubDBCn)    '' Abs(GetStockQty(mParentcode, pItemUOM, "STR", "FG", ConWH, "", "'" & ConStockRefType_DSP & "'"))
            mDespQty = mDespQty * mStdQty
            GetDespatchQty = GetDespatchQty + mDespQty
            
             mSqlStrRel = GetRelationItem(mParentcode)
            If mSqlStrRel <> "" Then
                UOpenRecordSet mSqlStrRel, pPubDBCn, adOpenStatic, RsRel, adLockReadOnly
                If RsRel.EOF = False Then
                    Do While RsRel.EOF = False
                        xProductRelCode = Trim(IIf(IsNull(RsRel!REF_ITEM_CODE), "", RsRel!REF_ITEM_CODE))
                        mDespQty = GetNetDespatch(xProductRelCode, pFromDate, pToDate, pPubDBCn) '' Abs(GetStockQty(mParentcode, pItemUOM, "STR", "FG", ConWH, "", "'" & ConStockRefType_DSP & "'"))
                        mDespQty = mDespQty * mStdQty
                        GetDespatchQty = GetDespatchQty + mDespQty
                        RsRel.MoveNext
                    Loop
                End If
            End If
                
            mDespQty = 0
            RsTemp.MoveNext
        Loop
    End If
    
  
Exit Function
ErrPart:
    GetDespatchQty = 0
End Function
Private Function GetRelationItem(mProductCode As String) As String
On Error GoTo ErrPart

    
    GetRelationItem = " SELECT REF_ITEM_CODE , ITEM_UOM " & vbCrLf _
            & " FROM INV_ITEM_RELATIONSHIP_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _
            & " AND ITEM_CODE='" & AllowSingleQuote(mProductCode) & "'"
                
  
Exit Function
ErrPart:
    GetRelationItem = ""
    ErrorMsg Err.Description, Err.Number, vbCritical
End Function

Private Function GetNetDespatch(pItemCode As String, pFromDate As String, pToDate As String, pPubDBCn As ADODB.Connection) As Double

On Error GoTo ErrPart
Dim SqlStr As String
Dim RsTemp As ADODB.Recordset
Dim mSaleReturn As Double
    
    SqlStr = ""
    
    SqlStr = "SELECT ABS(SUM(ID.PACKED_QTY * DECODE(TRIM(ID.ITEM_UOM),TRIM(INVMST.PURCHASE_UOM),INVMST.UOM_FACTOR,1))) AS INQTY"
    SqlStr = SqlStr & vbCrLf & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID, INV_ITEM_MST INVMST"
    
    SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE AND IH.DESP_TYPE<>'U'" & vbCrLf _
            & " AND INVMST.ITEM_CODE='" & AllowSingleQuote(pItemCode) & "' AND ID.STOCK_TYPE<>'CR'"
   

    SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE>=TO_DATE('" & Format(pFromDate, "DD-MMM-YYYY") & "')"
    SqlStr = SqlStr & vbCrLf & " AND IH.DESP_DATE<=TO_DATE('" & Format(pToDate, "DD-MMM-YYYY") & "')"

    UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RsTemp, adLockReadOnly
    
    If RsTemp.EOF = False Then
        If IsNull(RsTemp.Fields(0).Value) Then
            GetNetDespatch = 0
        Else
            GetNetDespatch = RsTemp.Fields(0).Value
        End If
    Else
        GetNetDespatch = 0
    End If
    
    
    SqlStr = "SELECT SUM(ID.RECEIVED_QTY * DECODE(TRIM(ID.ITEM_UOM),TRIM(INVMST.PURCHASE_UOM),INVMST.UOM_FACTOR,1)) AS INQTY"
    SqlStr = SqlStr & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, INV_ITEM_MST INVMST"
    
    SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _
            & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND INVMST.ITEM_CODE='" & AllowSingleQuote(pItemCode) & "' AND ID.STOCK_TYPE<>'CR' AND IH.REF_TYPE='I'"
   

    SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE>=TO_DATE('" & Format(pFromDate, "DD-MMM-YYYY") & "')"
    SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE<=TO_DATE('" & Format(pToDate, "DD-MMM-YYYY") & "')"

    UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RsTemp, adLockReadOnly
    
    If RsTemp.EOF = False Then
        If IsNull(RsTemp.Fields(0).Value) Then
            mSaleReturn = 0
        Else
            mSaleReturn = RsTemp.Fields(0).Value
        End If
    Else
        mSaleReturn = 0
    End If
    
    GetNetDespatch = GetNetDespatch - mSaleReturn
    Set RsTemp = Nothing
 
Exit Function
ErrPart:
    GetNetDespatch = 0
End Function

Private Function GetQueryForAlterItem(pItemCode As String) As String
On Error GoTo ErrPart
Dim SqlStr As String
    
    SqlStr = ""
    
    SqlStr = " SELECT DISTINCT TRIM(ALTER_RM_CODE) AS ALTER_RM_CODE" & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.STATUS='O'" & vbCrLf _
            & " AND ID.MAINITEM_CODE='" & AllowSingleQuote(pItemCode) & "'"
   
    SqlStr = SqlStr & vbCrLf & " UNION "
    
'    SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT MAINITEM_CODE " & vbCrLf _
'            & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID" & vbCrLf _
'            & " WHERE IH.COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _
'            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
'            & " AND IH.STATUS='O'" & vbCrLf _
'            & " AND ID.ALTER_RM_CODE='" & AllowSingleQuote(pItemCode) & "'"
'
'    SqlStr = SqlStr & vbCrLf & " UNION "

    SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT TRIM(MAINITEM_CODE) AS ALTER_RM_CODE" & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.STATUS='O'" & vbCrLf _
            & " AND ID.ALTER_RM_CODE IN (" & vbCrLf _
            & " SELECT DISTINCT ALTER_RM_CODE " & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.STATUS='O'" & vbCrLf _
            & " AND ID.MAINITEM_CODE='" & AllowSingleQuote(pItemCode) & "')"
            
    SqlStr = SqlStr & vbCrLf & " MINUS SELECT '" & Trim(pItemCode) & "' AS ALTER_RM_CODE FROM DUAL "
    GetQueryForAlterItem = SqlStr
 
Exit Function
ErrPart:
    GetQueryForAlterItem = ""
End Function

Public Function ValidateWithMasterTable(mFieldData As Variant, mFieldName As String, mGetFieldDataName As String, mTableName As String, mDBCn As ADODB.Connection, ByRef StoreRetval As Variant, Optional pErrMsg As String, Optional mSqlCond As String) As Boolean
On Error GoTo CheckTheAccountNameErr

    Dim mSql As String
    Dim RsValidate As ADODB.Recordset       ''ADODB.Recordset
    Dim xStr As String
    Dim MasterNo As Variant

    MasterNo = ""
    If CStr(mFieldData) <> "" And mTableName <> "" Then
        Select Case VarType(mFieldData)
            Case 2, 3, 4, 5, 14
                mSql = "Select " + mGetFieldDataName + " From " + mTableName + " Where " + mFieldName + " =" & RTrim(LTrim(mFieldData)) & ""
            Case 7
                Dim mDate As Date
                mDate = ToMMDD(CVar(mFieldData))
                mSql = "Select " + mGetFieldDataName + " From " + mTableName + " Where " + mFieldName + " ='" & RTrim(LTrim(mDate)) & "'"
            Case 8
                xStr = mFieldData
                xStr = AllowSingleQuote(xStr)
                mSql = "Select " + mGetFieldDataName + " From " + mTableName + " Where " + mFieldName + " ='" + RTrim(LTrim(xStr)) + "'"
        End Select
        
        If mSqlCond <> "" Then
            mSql = mSql & vbCrLf & " AND " & mSqlCond
        End If
        
        Set RsValidate = Nothing
        If UOpenRecordSet(mSql, mDBCn, adOpenStatic, RsValidate, adLockReadOnly) = False Then GoTo CheckTheAccountNameErr
        
        If RsValidate.EOF = False Then
               ValidateWithMasterTable = True
               MasterNo = RsValidate.Fields(0).Value
               Select Case VarType(MasterNo)
                    Case 0       'vbNull
                         ErrorMsg Err.Description, Err.Number, vbCritical
                    Case 2     'vbInteger
                         MasterNo = CInt(MasterNo)
                    Case 3, 14    'vbLong
                         MasterNo = CDbl(MasterNo)
                        'MasterNo = CLng(MasterNo)
                    Case 4     'vbSingle
                         MasterNo = CSng(MasterNo)
                    Case 5     'vbDouble
                         MasterNo = CDbl(MasterNo)
                    Case 7      'vbDate
                         MasterDate = ToDDMM(CStr(MasterNo))
                    Case 8     'vbString
                         MasterNo = CStr(MasterNo)
                    Case 10      'vbError
                         MsgBox MasterNo
               End Select
        ElseIf RsValidate.EOF = True Then
               ValidateWithMasterTable = False
               If pErrMsg <> "" Then
                    MsgInformation pErrMsg
               End If
               StoreRetval = MasterNo
               Exit Function
        End If
    Else
        ValidateWithMasterTable = False
        Exit Function
    End If
    StoreRetval = IIf(IsNull(MasterNo), " ", MasterNo)
    
    RsValidate.Close
    Set RsValidate = Nothing
    
    Exit Function
CheckTheAccountNameErr:
''Resume
    ErrorMsg Err.Description, Err.Number, vbCritical
    ValidateWithMasterTable = False
'    If RsValidate.State = adStateOpen Then
'        RsValidate.Close
'        Set RsValidate = Nothing
'    End If
End Function
Public Function GetMaxRecord(mTableName As String, mDBCn As ADODB.Connection, Optional mSqlCond As String) As Double
On Error GoTo ErrPart

Dim mSql As String
Dim RsRecordCount As ADODB.Recordset       ''ADODB.Recordset


    GetMaxRecord = 0
    mSql = " SELECT COUNT(1) AS MAXRECD FROM " & mTableName & ""        ''''& vbCrLf _
            & " Where " & mSqlCond
    
    If mSqlCond <> "" Then
        mSql = mSql & vbCrLf & " Where " & mSqlCond
    End If
    
    Set RsRecordCount = Nothing
    UOpenRecordSet mSql, mDBCn, adOpenStatic, RsRecordCount, adLockReadOnly
        
    If RsRecordCount.EOF = False Then
        GetMaxRecord = RsRecordCount.Fields(0).Value
    ElseIf RsRecordCount.EOF = True Then
        GetMaxRecord = 0
    End If
    
    RsRecordCount.Close
    Set RsRecordCount = Nothing
    
    Exit Function
ErrPart:
    ErrorMsg Err.Description, Err.Number, vbCritical
End Function
Public Function GetMonthInString(TextMonth As String) As String
    TextMonth = Format(TextMonth, "00")
    If TextMonth = "" Then
        GetMonthInString = ConBlankDate
    ElseIf TextMonth = "01" Then
        GetMonthInString = "January"
    ElseIf TextMonth = "02" Then
        GetMonthInString = "February"
    ElseIf TextMonth = "03" Then
        GetMonthInString = "March"
    ElseIf TextMonth = "04" Then
        GetMonthInString = "April"
    ElseIf TextMonth = "05" Then
        GetMonthInString = "May"
    ElseIf TextMonth = "06" Then
        GetMonthInString = "June"
    ElseIf TextMonth = "07" Then
        GetMonthInString = "July"
    ElseIf TextMonth = "08" Then
        GetMonthInString = "August"
    ElseIf TextMonth = "09" Then
        GetMonthInString = "September"
    ElseIf TextMonth = "10" Then
        GetMonthInString = "October"
    ElseIf TextMonth = "11" Then
        GetMonthInString = "November"
    ElseIf TextMonth = "12" Then
        GetMonthInString = "December"
    End If
End Function
Public Function ToDDMM(FldDate As Date)
    If Not IsDate(FldDate) Then
        ToDDMM = ""
    Else
        ToDDMM = Format(FldDate, "DD/MM/YYYY")
    End If
End Function

Public Function ToMMDD(TextDate As String)
    If TextDate = "" Then
        ToMMDD = ConBlankDate
    Else
        ToMMDD = Format(TextDate, "MM-DD-YYYY")
    End If
End Function
Public Sub ProtectCell(sprd As Object, Row As Long, Row2 As Long, Col As Long, col2 As Long)
    sprd.Row = Row
    sprd.Row2 = Row2
    sprd.Col = Col
    sprd.col2 = col2
    sprd.BlockMode = True
    sprd.Lock = True
    sprd.Protect = True
    sprd.BlockMode = False
        
End Sub
Public Sub UnLockCell(sprd As Object, Row As Long, Row2 As Long, Col As Long, col2 As Long)
    sprd.Row = Row
    sprd.Row2 = Row2
    sprd.Col = Col
    sprd.col2 = col2
    sprd.BlockMode = True
    sprd.Lock = False
    sprd.BlockMode = False
        
End Sub
Public Sub LockCell(sprd As Object, Row As Long, Row2 As Long, Col As Long, col2 As Long)
    sprd.Row = Row
    sprd.Row2 = Row2
    sprd.Col = Col
    sprd.col2 = col2
    sprd.BlockMode = True
    sprd.Lock = True
    sprd.BlockMode = False
        
End Sub
Public Sub CellColor(sprd As Object, Row As Long, Row2 As Long, Col As Long, col2 As Long)
 sprd.Row = Row
    sprd.Row2 = Row2
    sprd.Col = Col
    sprd.col2 = col2
    sprd.BlockMode = True
    sprd.BackColor = &HFFFF00
    sprd.GridColor = &HC00000
    sprd.BlockMode = False
    sprd.ShadowText = &HFF&
    sprd.ShadowColor = &H80FFFF
    sprd.SelBackColor = &HC0FFFF
    sprd.SelForeColor = &H800000
End Sub
Public Sub BlockCellColor(sprd As Object, Row As Long, Row2 As Long, Col As Long, col2 As Long)
 sprd.Row = Row
    sprd.Row2 = Row2
    sprd.Col = Col
    sprd.col2 = col2
    sprd.BlockMode = True
    sprd.BackColor = &HC0C0C0       ''&HFFFF00
    sprd.GridColor = &HC00000
    sprd.Lock = True
    sprd.Protect = True
'    sprd.ShadowText = &HFF&
'    sprd.ShadowColor = &H80FFFF
'    sprd.SelBackColor = &HC0FFFF
'    sprd.SelForeColor = &H800000
    sprd.BlockMode = False
End Sub
Public Sub UnProtectCell(ByRef sprd As Object, Row As Long, Row2 As Long, Col As Long, col2 As Long)
    sprd.Row = Row
    sprd.Row2 = Row2
    sprd.Col = Col
    sprd.col2 = col2
    sprd.BlockMode = True
    sprd.Lock = False
    sprd.Protect = False
    sprd.BlockMode = False
End Sub

Public Sub SprdAction(sprd As Object, mAction)
    sprd.Col = -1
    sprd.Row = -1
    sprd.BlockMode = True
    sprd.Action = mAction
    sprd.BlockMode = False
End Sub

Public Sub SaveStatus(MyForm As Form, ADDMode As Boolean, MODIFYMode As Boolean, Optional ActivateSavebutton As Boolean)
    If ADDMode = True Or MODIFYMode = True Or ActivateSavebutton = True Then
        MyForm.cmdSave.Enabled = True
'        MyForm.cmdSavePrint.Enabled = True
    End If
End Sub
                                                   

Public Function UOpenRecordSet(SqlStr As String, DbCN As ADODB.Connection, mOpenType As ADODB.CursorTypeEnum, ByRef mRs As ADODB.Recordset, Optional mLockType As ADODB.LockTypeEnum) As Boolean
''Public Function UOpenRecordSet(SqlStr As String, DbCN As ADODB.Connection, mOpenType As ADODB.CursorTypeEnum, ByRef mRs As ADODB.Recordset, Optional mLockType As ADODB.LockTypeEnum) As Boolean
On Error GoTo ERR1
UOpenRecordSet = False
Set mRs = New ADODB.Recordset
mRs.CursorLocation = adUseServer        ''adUseClient          '''

    If mLockType = 0 Then
        mRs.Open SqlStr, DbCN, mOpenType
    Else
        mRs.Open SqlStr, DbCN, mOpenType, mLockType
    End If
   
   ''Set mRs = DbCN.CreateDynaset(SqlStr, 0&)
   UOpenRecordSet = True
Exit Function
ERR1:
    ErrorMsg Err.Description, Err.Number, vbCritical
    UOpenRecordSet = False
'Resume
End Function

Public Function AdjNum(ByVal N As Double) As String
    AdjNum = Space(9 - Len(Trim(Format(N, "0.00")))) + Format(Trim(N), "0.00")
End Function

Public Function AllowSingleQuote(txt As String) As String
    txt = Trim(txt)
    AllowSingleQuote = Trim(Replace(txt, "'", "''"))
End Function
Public Function AllowVBNewLine(txt As String) As String
    AllowVBNewLine = Trim(Replace(txt, vbNewLine, " "))
End Function
Public Sub MovementAction(rsName As ADODB.Recordset, CmdMovement As Object, Index As Integer)
    On Error GoTo ErrPart
    CmdMovement(0).Enabled = True
    CmdMovement(1).Enabled = True
    CmdMovement(2).Enabled = True
    CmdMovement(3).Enabled = True
    If Index = 0 Then
        rsName.MoveFirst
    ElseIf Index = 1 Then
        rsName.MovePrevious
    ElseIf Index = 2 Then
        rsName.MoveNext
    ElseIf Index = 3 Then
        rsName.MoveLast
    End If
    If rsName.EOF Then
        rsName.MoveLast
        CmdMovement(3).Enabled = False
        CmdMovement(2).Enabled = False
    ElseIf rsName.BOF Then
        rsName.MoveFirst
        CmdMovement(0).Enabled = False
        CmdMovement(1).Enabled = False
    End If
ErrPart:
End Sub

Public Sub NameFill(SqlStr As String, mListObj As Object, CN As ADODB.Connection)
On Error GoTo ERR1
Static tmpStr As String
Static FieldsSent     As Integer
Static I As Integer
Static RS As ADODB.Recordset
Set RS = Nothing
UOpenRecordSet SqlStr, CN, adOpenStatic, RS
    
mListObj.Clear
If RS.EOF = False Then
    RS.MoveFirst
    Screen.MousePointer = 11
    FieldsSent = RS.Fields.Count
    Do While Not RS.EOF
        tmpStr = ""
        For I = 0 To FieldsSent - 1
            tmpStr = tmpStr + IIf(VarType(RS.Fields(I) <> 8), Format(RS.Fields(I), "0"), RS.Fields(I)) + IIf(IsNumeric(RS.Fields(I)), Space(8), Space(RS.Fields(I).DefinedSize - Len(RS.Fields(I)))) + "  "
        Next I
        mListObj.AddItem tmpStr
        RS.MoveNext
    Loop
    Screen.MousePointer = 1
End If
Exit Sub
ERR1:
ErrorMsg Err.Description, Err.Number, vbCritical
End Sub

Public Sub SearchName(Control As ListBox, txt As TextBox)
    Dim I As Integer, J As Integer
    Dim b As Integer, llct As Integer, prevb As Integer
    Dim idarri(50) As Integer
     b = Len(Trim$(txt.Text))
     llct = Control.ListCount
     If b < 1 Then
         Control.Text = Control.List(0)
         I = 0
         prevb = 0
         For J = 0 To 50
             idarri(J) = 0
         Next
         J = 0
     Else
         If b > prevb Then
          If J >= 0 Then
             idarri(J) = I
          End If
          J = J + 1
     '        i = idarri(j)-1
         Else
             J = J - 1
             If J >= 0 Then
                  I = idarri(J)
             End If
         End If
         Do While I < llct
             If UCase(Left(Control.List(I), b)) = UCase(Trim$(txt.Text)) Then
                 Control.Text = Control.List(I)
                 prevb = b
                 Exit Sub
             End If
             I = I + 1
         Loop
     End If
End Sub

'Public Sub ReportWindow(Rept1 As CrystalReport)
'    Rept1.WindowMaxButton = True
'    Rept1.WindowMinButton = True
'    Rept1.WindowShowGroupTree = True
'    Rept1.WindowShowNavigationCtls = True
'    Rept1.WindowAllowDrillDown = True
'    Rept1.WindowShowPrintSetupBtn = True
'    Rept1.WindowShowProgressCtls = True
'    Rept1.WindowShowSearchBtn = True
'    Rept1.WindowShowZoomCtl = True
'    Rept1.WindowState = crptMaximized
'    Rept1.WindowBorderStyle = crptSizable
'End Sub

Public Function STRMenuRight(mUser As String, mModuleID As Integer, mMenu As String, DbCN As ADODB.Connection) As String
On Error GoTo ErrSTRMenuRight
Dim RS As ADODB.Recordset      'ADODB.Recordset
Dim SqlStr As String

  
    STRMenuRight = ""
    If UCase(mMenu) = "MNUWINDOWS" Or UCase(mMenu) = "MNUABOUT" Or UCase(mMenu) = "MNULOGOUT" Then
        STRMenuRight = "AMDV"
        Exit Function
    End If
    
    If mUser <> "" And mMenu <> "" Then
        ''14-06-2006
'        If mUser = "SUPER" Then
        
        If PubATHUSER = True Then
            STRMenuRight = "AMDVS"
            Exit Function
        Else
            SqlStr = " Select Rights " & vbCrLf _
                    & " From FIN_Rights_MST " & vbCrLf _
                    & " Where UserID='" & UCase(mUser) & "'" & vbCrLf _
                    & " And COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND MODULEID=" & mModuleID & " And MenuHead='" & UCase(mMenu) & "'"
            
            UOpenRecordSet SqlStr, DbCN, adOpenStatic, RS
            If RS.EOF = False Then
                STRMenuRight = RS.Fields("Rights").Value
            Else
                STRMenuRight = ""
            End If
        End If
    End If
'    If Rs.State = adStateOpen Then
'        Rs.Close
'        Set Rs = Nothing
'    End If
'
Exit Function
ErrSTRMenuRight:
'Resume
    MsgBox Err.Description
'    If Rs.State = adStateOpen Then
'        Rs.Close
'        Set Rs = Nothing
'    End If
End Function
'Public Sub RightsToButton(MyForm As Form, RightsSTR As String)
'On Error GoTo ERR1
'    Dim mControl As Control
'    Set mControl = Nothing
'
'    Call SetStatusBar
'
''    Call FormOpened
'
'
'    For Each mControl In MyForm.Controls
'        'Making All Std. Buttons(Add, Save, Modify, Delete) Enabled=False
'        If TypeOf mControl Is CommandButton Then
'            If mControl.Caption = ConCmdAddCaption Then
'                mControl.Enabled = False
'            End If
'            If mControl.Caption = ConCmdSaveCaption Then
'                mControl.Enabled = False
'            End If
'            If mControl.Caption = ConcmdmodifyCaption Then
'                mControl.Enabled = False
'            End If
'            If mControl.Caption = ConCmdDeleteCaption Then
'                mControl.Enabled = False
'            End If
'            MiscButtonRights mControl, False
'        End If
''
''        'Making Std. Buttons(Add, Save, Modify, Delete) Enabled=True, Based on the RightsSTR
'        If TypeOf mControl Is CommandButton Then
'            If InStr(1, RightsSTR, "A", vbTextCompare) <> 0 Then
'                If mControl.Caption = ConCmdAddCaption Then
'                    mControl.Enabled = True
'                End If
'                If mControl.Caption = ConCmdSaveCaption Then
'                    mControl.Enabled = True
'                End If
'                MiscButtonRights mControl, False
'            End If
'
'            If InStr(1, RightsSTR, "M", vbTextCompare) <> 0 Then
'                If mControl.Caption = ConCmdSaveCaption Then
'                    mControl.Enabled = True
'                End If
'                If mControl.Caption = ConcmdmodifyCaption Then
'                    mControl.Enabled = True
'                End If
'                MiscButtonRights mControl, False
'            End If
'
'            If InStr(1, RightsSTR, "D", vbTextCompare) <> 0 Then
'                If mControl.Caption = ConCmdDeleteCaption Then
'                    mControl.Enabled = True
'                End If
'                MiscButtonRights mControl, False
'            End If
'            If InStr(1, RightsSTR, "V", vbTextCompare) <> 0 Then
'                MiscButtonRights mControl, True
'            End If
'        End If
'    Next mControl
'    Exit Sub
'ERR1:
'    ErrorMsg Err.Description, Err.Number, vbCritical
'End Sub

Sub MiscButtonRights(mControl As Control, RightFlag As Boolean)
On Error GoTo ERR1
            If mControl.Caption = "&Begin" Then
                mControl.Enabled = RightFlag
            End If
            If mControl.Caption = "&End" Then
                mControl.Enabled = RightFlag
            End If
            If mControl.Caption = "&Open" Then
                mControl.Enabled = RightFlag
            End If
            If mControl.Caption = "&Show" Then
                mControl.Enabled = RightFlag
            End If
            If mControl.Caption = "Show" Then
                mControl.Enabled = RightFlag
            End If
            If UCase(mControl.Caption) = "OK" Or UCase(mControl.Caption) = "&OK" Then
                mControl.Enabled = RightFlag
            End If
            If mControl.Caption = "&Print" Then
                mControl.Enabled = RightFlag
            End If
            If mControl.Caption = "Pre&view" Then
                mControl.Enabled = RightFlag
            End If
    Exit Sub
ERR1:
    ErrorMsg Err.Description, Err.Number, vbCritical
'    Resume
End Sub
Public Function MsgQuestion(Msg As String) As String
    MsgQuestion = MsgBox(Msg, vbQuestion + vbYesNo + vbDefaultButton2 + vbApplicationModal, App.CompanyName + " " + App.Title)
End Function

Public Function MsgExclamation(Msg As String) As String
    MsgExclamation = MsgBox(Msg, vbExclamation + vbApplicationModal, App.CompanyName + " " + App.Title)
End Function

Public Function LastDay(GiveMonth As Integer, GiveYear As Integer) As Integer
On Error GoTo LastDayErr:
    Dim mmm As Integer
    mmm = GiveMonth
    Select Case mmm
        Case 1, 3, 5, 7, 8, 10, 12
            LastDay = 31
        Case 2
            If GiveYear Mod 4 = 0 Then
                LastDay = 29
            Else
                LastDay = 28
            End If
        Case 4, 6, 9, 11
            LastDay = 30
    End Select
    Exit Function
LastDayErr:
    MsgBox Err.Description
    Exit Function
End Function

Public Function ClearFields(MyForm As Form)
On Error GoTo ErrPart
Dim mControl As Control
    Set mControl = Nothing
    For Each mControl In MyForm.Controls
        If TypeOf mControl Is TextBox Then
            mControl.Text = ""
        ElseIf TypeOf mControl Is ComboBox Then
            If mControl.Style = 0 Then mControl.Text = ""
            If mControl.Style = 1 Then mControl.Text = ""
            If mControl.Style = 2 Then mControl.ListIndex = -1
        ElseIf TypeOf mControl Is CheckBox Then
            mControl.Value = 0
        End If
    Next
    Exit Function
ErrPart:
    MsgBox Err.Description
End Function
Public Function SetQryParm(cmd As Command, CmdText As String, CmdType As CommandTypeEnum, Parm As Parameter, ParmType As DataTypeEnum, ParmSize As Long, ParmDirection As ParameterDirectionEnum, ParmValue As Variant, DbCN As ADODB.Connection, ParmIndex As Integer, Optional PassParm As Boolean) As Boolean
On Error GoTo ERR1
'If Cmd.CommandText = "" Then
'     Cmd.ActiveConnection = DbCN
'     With Cmd
'         .CommandText = CmdText
'         .CommandType = CmdType
'     End With
'
'     If PassParm = True Then
'        Set Parm = New Parameter
'        With Parm
'            .Type = ParmType
'            .Size = ParmSize
'            .Direction = ParmDirection
'            .Value = ParmValue
'        End With
'        Cmd.Parameters.Append ParmValue
'     End If
'Else
'   If PassParm = True Then
'        Cmd.Parameters(ParmIndex).Value = ParmValue
'   End If
'End If
SetQryParm = True
Exit Function
ERR1:
ErrorMsg Err.Description, Err.Number, vbCritical
'Resume
End Function

Public Function PadC(mText As String, mLength As Integer, Optional FillChar As String) As String
On Error GoTo ERR1
Static I As Integer
If FillChar = "" Then
    FillChar = " "
End If
I = (mLength - Len(mText)) / 2
PadC = String(I, FillChar) & mText & String(I, FillChar)
Exit Function
ERR1:
ErrorMsg Err.Description, Err.Number, vbCritical
End Function

Public Function PadL(mText As String, mLength As Integer, Optional FillChar As String) As String
On Error GoTo ERR1
Static I As Integer
If FillChar = "" Then
    FillChar = " "
End If
I = (mLength - Len(mText))
PadL = String(I, FillChar) & mText
Exit Function
ERR1:
ErrorMsg Err.Description, Err.Number, vbCritical
End Function

Public Function MLCount(txtString As String, LineWidth As Integer) As Integer
    If Int(Len(txtString) / LineWidth) = Len(txtString) / LineWidth Then
        MLCount = Int(Len(txtString) / LineWidth)
    Else
        MLCount = Int(Len(txtString) / LineWidth) + 1
    End If
End Function
Public Function MemoLine(txtString As String, LineNumber As Integer, LineWidth As Integer) As String
    MemoLine = Mid$(txtString, ((LineNumber - 1) * LineWidth) + 1, LineWidth)
End Function

Public Function ValidNameKey(KeyCode As Integer) As Boolean
    ValidNameKey = False
    If KeyCode <> vbKeyTab And KeyCode <> vbKeyLeft And KeyCode <> vbKeyRight _
                            And KeyCode <> vbKeyEnd And KeyCode <> vbKeyHome And KeyCode <> vbKeyReturn Then
        ValidNameKey = True
    End If
End Function

Public Function RemoveReturnKey(tStr As String) As String
On Error GoTo ERR1
Static I As Long
Static J As Long
Static XX As String

J = Len(tStr)
For I = 1 To J
       XX = Mid(tStr, I, 1)
       RemoveReturnKey = RemoveReturnKey + IIf(XX = Chr(vbKeyReturn) Or XX = Chr(10), " ", XX)
Next I
Exit Function
ERR1:
ErrorMsg Err.Description, Err.Number, vbCritical
End Function
Public Function WriteInI(Section As String, ByVal KeyName As String, ByVal DefaultValue As String, BarFileName As String)
On Error GoTo WriteErr
Dim FileName
'FileName = App.Path & "\" & BarFileName
FileName = mLocalPath & "\" & BarFileName
WritePrivateProfileString Section, KeyName, DefaultValue, FileName
Exit Function
WriteErr:
MsgBox Err.Description
End Function

Public Function ReadInI(Section As String, ByVal KeyName As String, BarFileName As String) As String
On Error GoTo ReadIniErr
Dim Default, FileName, ReturnString$, ReturnStr
Dim Valid%
FileName = App.Path & "\" & BarFileName
'FileName = mLocalPath & "\" & BarFileName
Default = "Not Found"
ReturnString$ = Space(100)
Valid% = GetPrivateProfileString(Section, KeyName, Default, ReturnString, Len(ReturnString) + 1, FileName)
ReturnStr = Left$(ReturnString$, Valid%)
ReadInI = ReturnStr
Exit Function
ReadIniErr:
MsgBox Err.Description
End Function
Public Function SequenceVal(SequenceName As String, DbCN As ADODB.Connection) As Long
On Error GoTo ERR1
    Dim SqlStr As String
    Dim RS As New ADODB.Recordset
    SqlStr = "Select " & SequenceName & ".Nextval from dual"
    UOpenRecordSet SqlStr, DbCN, adOpenStatic, RS, adLockReadOnly
    SequenceVal = RS.Fields(0)
Exit Function
ERR1:
    ErrorMsg Err.Description, Err.Number, vbCritical
    MsgInformation "Error occured in generating the rowno from sequence : " & SequenceName
End Function

'Public Function AutoGenRowNo(mTable As String, mMaxField As String, dbcn as adodb.Connection, Optional mCondition As String) As Long
'On Error GoTo Err1
'Static Rs As ADODB.Recordset
'Static SqlStr As String
'    Set Rs = Nothing
'    SqlStr = "Select Max(" & mMaxField & ") from " & mTable & " " & mCondition
'    Set Rs = dbcn.Execute(SqlStr)
'    If Not IsNull(Rs.Fields(0)) Then
'        AutoGenRowNo = Rs.Fields(0) + 1
'    Else
'        AutoGenRowNo = 1
'    End If
'Exit Function
'Err1:
'MsgInformation err.Description
'End Function
'
Sub CenterForm(FrontObject As Object, BackObject As Object)
  FrontObject.Left = (BackObject.Width - FrontObject.Width) / 2
  FrontObject.Top = (BackObject.Height - FrontObject.Height) / 2
End Sub

Public Function TabPrint(Item As Double) As Integer
    If Item >= 0 And Item < 10 Then
        TabPrint = 1
    ElseIf Item >= 10 And Item < 100 Then
        TabPrint = 2
    ElseIf Item >= 100 And Item < 1000 Then
        TabPrint = 3
    ElseIf Item >= 1000 And Item < 10000 Then
        TabPrint = 4
    ElseIf Item >= 10000 And Item < 100000 Then
        TabPrint = 5
    ElseIf Item >= 100000 And Item < 1000000 Then
        TabPrint = 6
    ElseIf Item >= 1000000 And Item < 10000000 Then
        TabPrint = 7
    ElseIf Item >= 10000000 And Item < 100000000 Then
        TabPrint = 8
    ElseIf Item >= 100000000 And Item < 1000000000 Then
        TabPrint = 9
    Else
        TabPrint = 10
    End If
End Function

Public Function ArrayScan(ArrayName As Variant, SearchElmnt As Variant) As Long
Dim ii As Long
For ii = 0 To UBound(ArrayName, 1) Step 1    'Len(ArrayName) Step 1
    If ArrayName(ii, 1) = SearchElmnt Then
        ArrayScan = ii
        Exit Function
    End If
Next ii
ArrayScan = -1    'not found
End Function

Public Function ArrayLen(ArrayName As Variant) As Long
'Dim II As Long
'For II = 0 To UBound(ArrayName, 1) Step 1      'Len(ArrayName) Step 1
'    If ArrayName(II, 1) = SearchElmnt Then
'        ArrayLen = II
'        Exit Function
'    End If
'Next II
ArrayLen = UBound(ArrayName)
End Function


Public Sub ButtonStatus(MyForm As Form, XRIGHT As String, MyRS As ADODB.Recordset, ADDMode As Boolean, MODIFYMode As Boolean, Optional NoNavigation As Boolean, Optional KeepEnabled As Boolean, Optional Authorised As Boolean)
On Error GoTo ErrPart
    NoNavigation = Not NoNavigation
    
'    If MyRS = Nothing Then
'        Exit Sub
'    End If
    
    With MyForm
        .cmdSave.Enabled = False
        If ADDMode = True Then
            .CmdAdd.Caption = ConCmdCancelCaption
            .CmdAdd.ToolTipText = "Cancel Add Operation"
            .cmdClose.Enabled = False
            .CmdModify.Caption = ConcmdmodifyCaption
            .CmdModify.Enabled = False
            .CmdDelete.Enabled = False
            If NoNavigation = True Then
                .CmdMovement(0).Enabled = False
                .CmdMovement(1).Enabled = False
                .CmdMovement(2).Enabled = False
                .CmdMovement(3).Enabled = False
            Else
                .cmdSavePrint.Enabled = False
                .cmdPrint.Enabled = False
                .CmdPreview.Enabled = False
            End If
            
            If Authorised = True Then
                .cmdAuthorised.Enabled = False
            End If
            
            .CmdView.Enabled = False
       ElseIf MODIFYMode = True Then
            .CmdModify.Caption = ConCmdCancelCaption
            .CmdModify.ToolTipText = "Cancel Modify Operation"
            .cmdClose.Enabled = False
            .CmdAdd.Caption = ConCmdAddCaption
            .CmdAdd.Enabled = False
            .CmdDelete.Enabled = False
            If NoNavigation = True Then
                .CmdMovement(0).Enabled = False
                .CmdMovement(1).Enabled = False
                .CmdMovement(2).Enabled = False
                .CmdMovement(3).Enabled = False
            Else
                .cmdSavePrint.Enabled = False
                .cmdPrint.Enabled = False
                .CmdPreview.Enabled = False
            End If
            .CmdView.Enabled = False
            If Authorised = True Then
                .cmdAuthorised.Enabled = False
            End If
            
        ElseIf MyRS.EOF = True Then
            If NoNavigation = True Then
                .CmdMovement(0).Enabled = False
                .CmdMovement(1).Enabled = False
                .CmdMovement(2).Enabled = False
                .CmdMovement(3).Enabled = False
            Else
                .cmdSavePrint.Enabled = False
                .cmdPrint.Enabled = False
                .CmdPreview.Enabled = False
            End If
            If .CmdView.Caption = ConCmdViewCaption Then
                .CmdAdd.Enabled = False
            Else
                .CmdAdd.Enabled = IIf(InStr(1, XRIGHT, "A") > 0, True, False)
                .CmdModify.Enabled = False
                .CmdDelete.Enabled = False
                .cmdClose.Enabled = True
                .CmdAdd.Caption = ConCmdAddCaption
                .CmdModify.Caption = ConcmdmodifyCaption
                .CmdAdd.ToolTipText = "Add New Record"
                .CmdModify.ToolTipText = "Modify Record"
                .CmdView.Enabled = True
            End If
            If Authorised = True Then
                .cmdAuthorised.Enabled = False
            End If
        ElseIf MyRS.EOF = False And .CmdView.Caption = ConCmdViewCaption Then
            If NoNavigation = True Then
                .CmdMovement(0).Enabled = IIf(KeepEnabled = True, True, False)
                .CmdMovement(1).Enabled = IIf(KeepEnabled = True, True, False)
                .CmdMovement(2).Enabled = IIf(KeepEnabled = True, True, False)
                .CmdMovement(3).Enabled = IIf(KeepEnabled = True, True, False)
            End If
            .CmdAdd.Enabled = IIf(InStr(1, XRIGHT, "A") = 0, False, IIf(KeepEnabled = True, True, IIf(.CmdView.Caption = ConCmdViewCaption, False, True)))
            .CmdModify.Enabled = IIf(InStr(1, XRIGHT, "A") = 0, False, IIf(KeepEnabled = True, True, IIf(.CmdView.Caption = ConCmdViewCaption, False, True)))
            .CmdDelete.Enabled = IIf(InStr(1, XRIGHT, "A") = 0, False, IIf(KeepEnabled = True, True, IIf(.CmdView.Caption = ConCmdViewCaption, False, True)))
            .cmdClose.Enabled = True
            .CmdAdd.Caption = ConCmdAddCaption
            .CmdModify.Caption = ConcmdmodifyCaption
            .CmdAdd.ToolTipText = "Add New Record"
            .CmdModify.ToolTipText = "Modify Record"
            .CmdView.Enabled = True
            If Authorised = True Then
                .cmdAuthorised.Enabled = IIf(InStr(1, XRIGHT, "S") = 0, False, IIf(KeepEnabled = True, True, IIf(.CmdView.Caption = ConCmdViewCaption, False, True)))
            End If
        ElseIf MyRS.EOF = False Then
            If NoNavigation = True Then
                .CmdMovement(0).Enabled = True
                .CmdMovement(1).Enabled = True
                .CmdMovement(2).Enabled = True
                .CmdMovement(3).Enabled = True
            Else
                .cmdPrint.Enabled = True
                .CmdPreview.Enabled = True
            End If
            .CmdView.Enabled = True
            .CmdDelete.Enabled = IIf(InStr(1, XRIGHT, "D") > 0, True, False)
            .CmdModify.Enabled = IIf(InStr(1, XRIGHT, "M") > 0, True, False)
            .CmdAdd.Enabled = IIf(InStr(1, XRIGHT, "A") > 0, True, False)
            .cmdClose.Enabled = True
            .CmdAdd.Caption = ConCmdAddCaption
            .CmdModify.Caption = ConcmdmodifyCaption
            .CmdAdd.ToolTipText = "Add New Record"
            .CmdModify.ToolTipText = "Modify Record"
            If Authorised = True Then
                .cmdAuthorised.Enabled = IIf(InStr(1, XRIGHT, "S") > 0, True, False)
            End If
        End If
    End With
Exit Sub
ErrPart:
    ErrorMsg Err.Description, Err.Number, vbCritical
'    Resume
End Sub


Public Sub GridButtonStatus(MyForm As Form, XRIGHT As String, mRows As Long, ADDMode As Boolean, MODIFYMode As Boolean, Optional NoNavigation As Boolean, Optional KeepEnabled As Boolean)
NoNavigation = Not NoNavigation
With MyForm
    .cmdSave.Enabled = False
    If ADDMode = True Then
        .CmdAdd.Caption = ConCmdCancelCaption
        .CmdAdd.ToolTipText = "Cancel Add Operation"
        .cmdClose.Enabled = False
        .CmdModify.Caption = ConcmdmodifyCaption
        .CmdModify.Enabled = False
        .CmdDelete.Enabled = False
        If NoNavigation = True Then
            .CmdMovement(0).Enabled = False
            .CmdMovement(1).Enabled = False
            .CmdMovement(2).Enabled = False
            .CmdMovement(3).Enabled = False
        Else
            .cmdSavePrint.Enabled = False
            .cmdPrint.Enabled = False
            .CmdPreview.Enabled = False
        End If
        .CmdView.Enabled = False
   ElseIf MODIFYMode = True Then
        .CmdModify.Caption = ConCmdCancelCaption
        .CmdModify.ToolTipText = "Cancel Modify Operation"
        .cmdClose.Enabled = False
        .CmdAdd.Caption = ConCmdAddCaption
        .CmdAdd.Enabled = False
        .CmdDelete.Enabled = False
        If NoNavigation = True Then
            .CmdMovement(0).Enabled = False
            .CmdMovement(1).Enabled = False
            .CmdMovement(2).Enabled = False
            .CmdMovement(3).Enabled = False
        Else
            .cmdSavePrint.Enabled = False
            .cmdPrint.Enabled = False
            .CmdPreview.Enabled = False
        End If
        .CmdView.Enabled = False
    ElseIf mRows <= 1 Then
        If NoNavigation = True Then
            .CmdMovement(0).Enabled = False
            .CmdMovement(1).Enabled = False
            .CmdMovement(2).Enabled = False
            .CmdMovement(3).Enabled = False
        Else
            .cmdSavePrint.Enabled = False
            .cmdPrint.Enabled = False
            .CmdPreview.Enabled = False
        End If
        If .CmdView.Caption = ConCmdViewCaption Then
        .CmdAdd.Enabled = False
        Else
        .CmdAdd.Enabled = IIf(InStr(1, XRIGHT, "A") > 0, True, False)
        .CmdModify.Enabled = IIf(InStr(1, XRIGHT, "M") > 0, True, False)
        .CmdDelete.Enabled = False
        .cmdClose.Enabled = True
        .CmdAdd.Caption = ConCmdAddCaption
        .CmdModify.Caption = ConcmdmodifyCaption
        .CmdAdd.ToolTipText = "Add New Record"
        .CmdModify.ToolTipText = "Modify Record"
        .CmdView.Enabled = True
        End If
    ElseIf mRows > 1 And .CmdView.Caption = ConCmdViewCaption Then
        If NoNavigation = True Then
            .CmdMovement(0).Enabled = IIf(KeepEnabled = True, True, False)
            .CmdMovement(1).Enabled = IIf(KeepEnabled = True, True, False)
            .CmdMovement(2).Enabled = IIf(KeepEnabled = True, True, False)
            .CmdMovement(3).Enabled = IIf(KeepEnabled = True, True, False)
        End If
        .CmdAdd.Enabled = IIf(InStr(1, XRIGHT, "A") = 0, False, IIf(KeepEnabled = True, True, IIf(.CmdView.Caption = ConCmdViewCaption, False, True)))
        .CmdModify.Enabled = IIf(InStr(1, XRIGHT, "A") = 0, False, IIf(KeepEnabled = True, True, IIf(.CmdView.Caption = ConCmdViewCaption, False, True)))
        .CmdDelete.Enabled = IIf(InStr(1, XRIGHT, "A") = 0, False, IIf(KeepEnabled = True, True, IIf(.CmdView.Caption = ConCmdViewCaption, False, True)))
        .cmdClose.Enabled = True
        .CmdAdd.Caption = ConCmdAddCaption
        .CmdModify.Caption = ConcmdmodifyCaption
        .CmdAdd.ToolTipText = "Add New Record"
        .CmdModify.ToolTipText = "Modify Record"
        .CmdView.Enabled = True
    ElseIf mRows > 1 Then
        If NoNavigation = True Then
            .CmdMovement(0).Enabled = True
            .CmdMovement(1).Enabled = True
            .CmdMovement(2).Enabled = True
            .CmdMovement(3).Enabled = True
        Else
            .cmdPrint.Enabled = True
            .CmdPreview.Enabled = True
        End If
        .CmdView.Enabled = True
        .CmdDelete.Enabled = IIf(InStr(1, XRIGHT, "D") > 0, True, False)
        .CmdModify.Enabled = IIf(InStr(1, XRIGHT, "M") > 0, True, False)
        .CmdAdd.Enabled = IIf(InStr(1, XRIGHT, "A") > 0, True, False)
        .cmdClose.Enabled = True
        .CmdAdd.Caption = ConCmdAddCaption
        .CmdModify.Caption = ConcmdmodifyCaption
        .CmdAdd.ToolTipText = "Add New Record"
        .CmdModify.ToolTipText = "Modify Record"
    End If
End With
End Sub
Public Sub DoFunctionKey(mFORM As Form, mkeyCode As Integer)
If mkeyCode = vbKeyF2 And mFORM.CmdAdd.Enabled = True Then mFORM.CmdAdd = True
'If mkeyCode = vbKeyF3 And mFORM.cmdModify.Enabled = True Then mFORM.cmdModify = True
If mkeyCode = vbKeyF4 And mFORM.cmdSave.Enabled = True Then mFORM.cmdSave = True
If mkeyCode = vbKeyF5 And mFORM.cmdSavePrint.Enabled = True Then mFORM.cmdSavePrint = True
If mkeyCode = vbKeyF6 And mFORM.CmdDelete.Enabled = True Then mFORM.CmdDelete = True
If mkeyCode = vbKeyF7 And mFORM.cmdPrint.Enabled = True Then mFORM.cmdPrint = True
If mkeyCode = vbKeyF8 And mFORM.CmdPreview.Enabled = True Then mFORM.CmdPreview = True
If mkeyCode = vbKeyF9 And mFORM.CmdView.Enabled = True Then mFORM.CmdView = True
If mkeyCode = vbKeyF10 And mFORM.cmdClose.Enabled = True Then mFORM.cmdClose = True
End Sub

Public Function SetNumericField(mKeyAscii As Integer) As Integer
mKeyAscii = Asc(UCase(Chr(mKeyAscii)))
If (mKeyAscii >= 48 And mKeyAscii <= 57) Or mKeyAscii = 8 Or mKeyAscii = 46 Or mKeyAscii = 45 Then
    SetNumericField = mKeyAscii
Else
    SetNumericField = 0
End If
End Function
Public Function TitleCase(mKeyAscii As Integer, TxtStr As String) As Integer
Static mI As Integer
    If mI = 1 Then
        mKeyAscii = Asc(UCase(Chr(mKeyAscii)))
        mI = 0
    ElseIf mI = 0 And mKeyAscii = vbBack Then
        mKeyAscii = Asc(UCase(Chr(mKeyAscii)))
        mI = 0
    End If
    If Len(TxtStr) < 1 Then
        mKeyAscii = Asc(UCase(Chr(mKeyAscii)))
    End If
    If mKeyAscii = vbKeySpace Or mKeyAscii = vbKeyTab Then
        mI = 1
    End If
    TitleCase = mKeyAscii
End Function
Public Function UpperCase(mKeyAscii As Integer, TxtStr As String, Optional SpeacialCharAllow As String) As Integer
Dim mI As Integer

    If SpeacialCharAllow = "N" Then
        If (mKeyAscii >= 48 And mKeyAscii <= 57) Or (mKeyAscii >= 97 And mKeyAscii <= 122) Or (mKeyAscii >= 65 And mKeyAscii <= 90) Or mKeyAscii = 8 Or mKeyAscii = 45 Then
            mKeyAscii = Asc(UCase(Chr(mKeyAscii)))
        Else
            mKeyAscii = 0
        End If
    End If
    mKeyAscii = Asc(UCase(Chr(mKeyAscii)))
    UpperCase = mKeyAscii
End Function

Public Function CheckDateKey(mKeyAscii As Integer) As Integer
Dim strvalid
    strvalid = "0123456789/-"
     If mKeyAscii > 26 Then
       If InStr(strvalid, Chr(mKeyAscii)) = 0 Then
          mKeyAscii = 0
       End If
     End If
     CheckDateKey = mKeyAscii
End Function
Public Function SetMaxLength(mFieldName As String, mTable As String, mConn As ADODB.Connection) As Long
Dim RS As ADODB.Recordset
Dim SqlStr As String
Dim mDataType As Long

    SqlStr = "Select " & mFieldName & " From " & mTable & " WHERE 1=2"
    UOpenRecordSet SqlStr, mConn, adOpenStatic, RS, adLockReadOnly
    
'    mDataType = Rs.Fields(0).OraIDataType
    Select Case RS.Fields(0).Type           ''mDataType           ''
            Case 131                        ''ORATYPE_NUMBER         ''
                SetMaxLength = RS.Fields(0).Precision           ''.Precision     '' - 2
            Case 135                        ''ORATYPE_DATE           ''
                SetMaxLength = 10   ''Rs.Fields(0).DefinedSize - 6
            Case Else
                SetMaxLength = RS.Fields(0).DefinedSize             '''.DefinedSize           ''
    End Select
    RS.Close
    Set RS = Nothing
End Function

Public Sub UserUnlock()
On Error GoTo ERR1
    Dim SqlStr As String
    SqlStr = "UPDATE USERS SET DUMKEY='' WHERE USERID='" & PubUserID & "'"
    PubDBCn.Execute SqlStr
    Exit Sub
ERR1:
    ErrorMsg Err.Description, Err.Number, vbCritical
End Sub

Public Function CheckUserLock(KeyFldStr As String, pPubDBCn As ADODB.Connection) As Boolean
On Error GoTo ERR1
    Dim SqlStr As String
    Dim RS As ADODB.Recordset
    SqlStr = "SELECT * FROM USERS WHERE DumKey='" & AllowSingleQuote(KeyFldStr) & "' and UserId<> '" & PubUserID & "' "
    UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RS
    If RS.EOF = False Then
       MsgInformation "USER " & RS.Fields("UserId").Value & "ALREADY USING THE SAME"
       CheckUserLock = False
    Else
       SqlStr = "UPDATE USER SET DUMKEY='" & AllowSingleQuote(KeyFldStr) & "'"
       PubDBCn.Execute SqlStr
       CheckUserLock = True
    End If
    Exit Function
ERR1:
    ErrorMsg Err.Description, Err.Number, vbCritical
End Function
Public Function Distribute(Rep_width As Integer, Pos_Arr() As Integer, WIDTH_ARR() As Integer, Optional Left_Margin As Long) As Boolean
Dim LAST, INT_ERROR, LEFTOVER, NON_ZERO, KOUNT, COL_WIDTH As Integer
On Error GoTo ERR1
    COL_WIDTH = 0
    Left_Margin = IIf(Left_Margin = "", 0, Left_Margin)
    Rep_width = Rep_width - Left_Margin
    NON_ZERO = 0
    For KOUNT = 0 To ArrayLen(WIDTH_ARR)
        COL_WIDTH = COL_WIDTH + WIDTH_ARR(KOUNT)
        NON_ZERO = NON_ZERO + IIf(WIDTH_ARR(KOUNT) <> 0, 1, 0)
        LEFTOVER = Rep_width - COL_WIDTH
     Next
     If LEFTOVER < 0 Then
         Distribute = False
         Exit Function
     Else
         COL_WIDTH = Int(LEFTOVER / NON_ZERO)
         INT_ERROR = Int((LEFTOVER - (COL_WIDTH * NON_ZERO) + COL_WIDTH) / 2)
         '&& NOT TAKING INTO ACCOUNT && THE ERROR OF INTEGER
     End If

     LAST = 0
     For KOUNT = 0 To ArrayLen(WIDTH_ARR)
         If WIDTH_ARR(KOUNT) = 0 Then
            Pos_Arr(KOUNT) = 0
         Else
            Pos_Arr(KOUNT) = IIf(LAST <> 0, WIDTH_ARR(LAST) + COL_WIDTH, INT_ERROR + Left_Margin) + IIf(LAST <> 0, Pos_Arr(LAST), 0)
            LAST = KOUNT
         End If
    Next
    Distribute = True
    Exit Function
ERR1:
    ErrorMsg Err.Description, Err.Number, vbCritical
End Function
Public Function pAge() As Boolean
Dim p_key
     'EJECT
pAge = True
End Function

Public Sub FillCombo(mCbo As ComboBox, mTableName As String, mFieldName As String, Optional InitialValue As String, Optional AdditionalCondition As String)
    On Error GoTo ERR1
    Dim RS As ADODB.Recordset
    Dim SqlStr As String
    SqlStr = "select " & mFieldName & " from " & mTableName
    If AdditionalCondition <> "" Then SqlStr = SqlStr + " Where " & AdditionalCondition
    SqlStr = SqlStr + " ORDER BY " & mFieldName
    UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RS, adLockReadOnly
    mCbo.Clear
    If InitialValue <> "" Then mCbo.AddItem InitialValue
    If RS.EOF = False Then
        Do While RS.EOF = False
            mCbo.AddItem IIf(IsNull(RS.Fields(0)), "", RS.Fields(0))
            RS.MoveNext
        Loop
    End If
    Exit Sub
ERR1:
    ErrorMsg Err.Description, Err.Number, vbCritical
End Sub

Public Function CheckDigit(pBarCode As String) As String
On Error GoTo CheckDigitERR
Dim mSum1 As Double
Dim mSum2 As Double
Dim mSum3 As Double
Dim mNextMultiOf10 As Long
Dim ii As Long
    mSum1 = 0
    mSum2 = 0
    mSum3 = 0

    For ii = 1 To Len(pBarCode) Step 2
        mSum1 = mSum1 + Val(Mid(pBarCode, ii, 1))
    Next
    mSum1 = mSum1 * 3

    For ii = Len(pBarCode) - 1 To 1 Step -2
        mSum2 = mSum2 + Val(Mid(pBarCode, ii, 1))
    Next
    mSum3 = mSum1 + mSum2

'******* STEP 3
    mNextMultiOf10 = (Int((mSum3 / 10)) + 1) * 10
    CheckDigit = Right(mNextMultiOf10 - mSum3, 1)
    Exit Function
CheckDigitERR:
    MsgBox Err.Description
'    Resume
End Function

Public Function IsOldItemCode(pDBCn As ADODB.Connection, pOldItemCode As String, pRetItemCode As String) As Boolean
On Error GoTo IsOldERR
    ValidateWithMasterTable pOldItemCode, "OldItemCode", "itemCode", "ITEM", pDBCn, MasterNo
    If IsNull(MasterNo) Or MasterNo = "" Then
        IsOldItemCode = False
        pRetItemCode = ""
    Else
        IsOldItemCode = True
        pRetItemCode = MasterNo
    End If
    Exit Function
IsOldERR:
    IsOldItemCode = False
    pRetItemCode = ""
    MsgBox Err.Description
End Function

Public Sub ScanBarCode(pBarCode As String, pRetItemCode As String, pRetBatchNo As String)
On Error GoTo ScanERR
    pRetItemCode = Left(pBarCode, 14)
    pRetBatchNo = Mid(pBarCode, 15, 5)
    Exit Sub
ScanERR:
    MsgBox Err.Description
End Sub
Public Function MakeFirstLot(pDBCn As ADODB.Connection, pItemCode) As String
On Error GoTo ERR1
Dim SqlStr As String
Dim RS As ADODB.Recordset
'    SqlStr = "SELECT BRANCH.BranchShortCode " _
'        & " FROM BRANCH,ITEM " _
'        & " WHERE BRANCH.BRANCHCODE=ITEM.DIVISIONCODE " _
'        & " AND ITEM.ITEMCODE='" & pItemCode & "'"
    ' MARKED TO GET 1ST LOT NO. FROM BARCODE TABLE
    SqlStr = "SELECT MIN(LOTNO) AS BATCHNO" _
        & " FROM BARCODE " _
        & " WHERE ITEMCODE='" & pItemCode & "'"
   UOpenRecordSet SqlStr, pDBCn, adOpenStatic, RS, adLockReadOnly
   If RS.EOF = False Then
      'MakeFirstLot = IIf(IsNull(rs.FIELDS("BranchShortCode").Value), "", rs.FIELDS("BranchShortCode").Value) & "001"
        MakeFirstLot = IIf(IsNull(RS.Fields("BATCHNO").Value), "", RS.Fields("BATCHNO").Value)
   Else
        MakeFirstLot = ""
   End If
   Exit Function
ERR1:
    MakeFirstLot = ""
End Function
Public Function BarCodeValidation(pDBCn As ADODB.Connection, pBarCode As String, Optional pRetItemShortName As String, Optional pRetCost As Double, Optional pRetMRP As Double) As Boolean
On Error GoTo ERR1:
Dim SqlStr As String
Dim RS As ADODB.Recordset
    SqlStr = "SELECT * FROM BARCODE " _
        & " WHERE ITEMCODE='" & Left(pBarCode, 14) & "' " _
        & " AND LOTNO='" & Mid(pBarCode, 15, 5) & "'"
    UOpenRecordSet SqlStr, pDBCn, adOpenStatic, RS, adLockReadOnly
    If RS.EOF = True Then
        BarCodeValidation = False
        pRetItemShortName = ""
        pRetCost = 0
        pRetMRP = 0
    Else
        BarCodeValidation = True
        pRetItemShortName = IIf(IsNull(RS.Fields("ITEMSHORTNAME").Value), "", RS.Fields("ITEMSHORTNAME").Value)
        pRetCost = Val(IIf(IsNull(RS.Fields("COSTPRICE").Value), "", RS.Fields("COSTPRICE").Value))
        pRetMRP = Val(IIf(IsNull(RS.Fields("MRP").Value), "", RS.Fields("MRP").Value))
    End If
    Exit Function
ERR1:
    ErrorMsg Err.Description, Err.Number, vbCritical
    pRetItemShortName = ""
    pRetCost = 0
    pRetMRP = 0
    BarCodeValidation = False
End Function
Public Function GetCostPrice(pDBCn As ADODB.Connection, pItemCode As String, pBatchNo As String) As Double
On Error GoTo GetCostERR
Dim RsTempCostPrice As ADODB.Recordset
Dim SqlStr As String
    SqlStr = ""
    SqlStr = "SELECT CostPrice FROM BarCode " _
        & " WHERE ItemCode='" & AllowSingleQuote(pItemCode) & "' " _
        & " AND LotNo='" & AllowSingleQuote(pBatchNo) & "'"
    UOpenRecordSet SqlStr, pDBCn, adOpenStatic, RsTempCostPrice, adLockReadOnly
    If RsTempCostPrice.EOF = False Then
        GetCostPrice = Val(IIf(IsNull(RsTempCostPrice!COSTPRICE), 0#, RsTempCostPrice!COSTPRICE))
    Else
        GetCostPrice = 0#
    End If
    Exit Function
GetCostERR:
    GetCostPrice = 0#
    MsgBox Err.Description
End Function

Public Function SumQty(SprdMain As Object, ColItemCode As Long, mItemCode As String, ColLotNo As Long, mBatchNo As String, ColQty As Long, mQty As Double, ByRef I As Long) As Double
On Error GoTo ERR1
Dim mItemCode2 As String
Dim mBatchNo2 As String
Dim mFOC2 As String
    
    With SprdMain
        I = I + 1
        .Row = I
        
        
        .Col = ColItemCode
        mItemCode2 = .Text
        
        .Col = ColLotNo
        mBatchNo2 = .Text
        
        
        Do While mItemCode = mItemCode2 And mBatchNo = mBatchNo2
            .Col = ColQty
            mQty = mQty + Val(.Text)
            
            I = I + 1
            .Row = I
            .Col = ColItemCode
            mItemCode2 = .Text
            .Col = ColLotNo
            mBatchNo2 = .Text
        Loop
    End With
    SumQty = mQty
    Exit Function
ERR1:
    SumQty = mQty
End Function

Public Sub FormOpened()
On Error GoTo ErrPart
Dim nForms As Long
    If UCase(App.EXEName) = "REVIVEPOS" Then Exit Sub
    nForms = VB.Forms.Count
   
    If nForms >= 7 Then
        MsgBox "Too many Forms Opened in your System", vbInformation
    End If
    
Exit Sub
ErrPart:
    MsgBox Err.Description
End Sub

Public Function FillPrintDummyDataFromSprd(GridName As Object, ByVal prmStartGridRow As Long, ByVal prmEndGridRow As Long, ByVal prmStartGridCol As Long, ByVal prmEndGridCol As Long, mPvtDBCn As ADODB.Connection) As Boolean
''' This procedure fills the Grid Data into PrintDummy table for printing...
On Error GoTo PrintDummyErr

Dim RSPrintDummy As ADODB.Recordset
Dim FieldCnt As Integer
Dim RowNum As Integer
Dim FieldNum As Integer
Dim GetData As String
Dim SetData As String
Dim SqlStr As String

   
    mPvtDBCn.Errors.Clear
    
    mPvtDBCn.BeginTrans
    
    SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & AllowSingleQuote(UCase(PubUserID)) & "'"
    mPvtDBCn.Execute SqlStr
    
    For RowNum = prmStartGridRow To prmEndGridRow
        FieldCnt = 1
        SetData = ""
        GetData = ""
        GridName.Row = RowNum
        For FieldNum = prmStartGridCol To prmEndGridCol
            GridName.Col = FieldNum
            If FieldNum = prmStartGridCol Then
                SetData = "FIELD" & FieldCnt
                GetData = "'" & AllowSingleQuote(Left(GridName.Text, 255)) & "'"
            Else
                SetData = SetData & ", " & "FIELD" & FieldCnt
                GetData = GetData & ", " & "'" & AllowSingleQuote(Left(GridName.Text, 255)) & "'"
            End If
            FieldCnt = FieldCnt + 1
        Next
        SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf _
                & " " & SetData & ") " & vbCrLf _
                & " VALUES (" & vbCrLf _
                & " '" & AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf _
                & " " & GetData & ") "
        mPvtDBCn.Execute SqlStr
NextRec:
    Next
        
    mPvtDBCn.CommitTrans
    FillPrintDummyDataFromSprd = True
   
    Exit Function
PrintDummyErr:
    ErrorMsg Err.Description, Err.Number, vbCritical
    FillPrintDummyDataFromSprd = False
    mPvtDBCn.RollbackTrans
    Screen.MousePointer = 0
End Function
Public Function FetchFromTempData(mSqlStr As String, mOrderBy As String) As String
    
    mSqlStr = " SELECT * " _
            & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf _
            & " WHERE  " & vbCrLf _
            & " UserID='" & AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf _
    
    If mOrderBy = "" Then
        mSqlStr = mSqlStr & " ORDER BY SUBROW"
    Else
        mSqlStr = mSqlStr & " ORDER BY " & mOrderBy
    End If
    
    FetchFromTempData = mSqlStr
    
End Function


Public Function GetUserCanModify(pVNoDate As String) As Boolean
On Error GoTo ErrPart
Dim mEntryDate As String
Dim SqlStr As String
Dim RsCFYNo As ADODB.Recordset
Dim mCurrFYYear As Long

    SqlStr = "SELECT FYEAR FROM GEN_CMPYRDTL_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _
            & " AND START_DATE<=TO_DATE('" & Format(PubCurrDate, "DD-MMM-YYYY") & "')" & vbCrLf _
            & " AND END_DATE>=TO_DATE('" & Format(PubCurrDate, "DD-MMM-YYYY") & "')"
            
    UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsCFYNo
    If Not RsCFYNo.EOF Then
        mCurrFYYear = Format(CStr(RsCFYNo!FYEAR), "0000")
    End If
    
    GetUserCanModify = False
    
    If PubUserLevel = 1 Then
        GetUserCanModify = True
    ElseIf PubUserLevel = 2 Then
'        If mCurrFYYear = RsCompany!FYEAR Then
'            GetUserCanModify = True
'        End If
        mEntryDate = DateAdd("d", 60, pVNoDate)
        If DateDiff("d", PubCurrDate, mEntryDate) >= 0 Then
            GetUserCanModify = True
        End If
    ElseIf PubUserLevel = 3 Then
        mEntryDate = DateAdd("d", 45, pVNoDate)
        If DateDiff("d", PubCurrDate, mEntryDate) >= 0 Then
            GetUserCanModify = True
        End If
    ElseIf PubUserLevel = 4 Then
        mEntryDate = DateAdd("d", 1, pVNoDate)
        If DateDiff("d", PubCurrDate, mEntryDate) >= 0 Then
            GetUserCanModify = True
        End If
'    ElseIf PubUserLevel = 5 Then
'        mEntryDate = DateAdd("d", 1, pVNoDate)
'        If DateDiff("d", PubCurrDate, mEntryDate) >= 0 Then
'            GetUserCanModify = True
'        End If
    End If

Exit Function
ErrPart:
    GetUserCanModify = False
End Function
Public Function ErrorMsg(mErrDesc As String, Optional mErrNo As String, Optional MsgBoxStyle As VbMsgBoxStyle) As String
On Error GoTo ErrPart
Dim mStartSearch As Long
    
    If Trim(mErrDesc) = "" Then Exit Function
    
    mStartSearch = InStr(1, mErrDesc, ":", vbTextCompare) + 1
    If mStartSearch = 1 Then
        ErrorMsg = mErrDesc
        MsgBox UCase(ErrorMsg), MsgBoxStyle, IIf(mErrNo = "", "", "Error No : " & mErrNo)
        Exit Function
    End If
    ErrorMsg = UCase(Mid(mErrDesc, mStartSearch))
    
    
    MsgBox ErrorMsg, MsgBoxStyle, IIf(mErrNo = "", "", "Error No : " & mErrNo)
Exit Function
ErrPart:
    MsgBox Err.Description
   'Resume
End Function
Public Function MsgInformation(Msg As String)
    MsgInformation = MsgBox(Msg, vbInformation + vbApplicationModal, App.CompanyName + " " + App.Title)
End Function


Public Sub SetMainFormCordinate(MyForm As Form)
    MyForm.Left = (Screen.Width - MyForm.Width) / 2
    MyForm.Top = (Screen.Height - MyForm.Height) / 2
    MyForm.Height = MyForm.Height
    MyForm.Width = MyForm.Width
End Sub
Public Function GetServerDate(pPubDBCn As ADODB.Connection) As String
Dim RS As ADODB.Recordset            ''Recordset
    UOpenRecordSet "SELECT SYSDATE FROM DUAL", pPubDBCn, adOpenKeyset, RS, adLockReadOnly
'    adOpenKeyset , RsCompany, adLockOptimistic
    ''Set Rs = PubDBCn.CreateDynaset("SELECT SYSDATE FROM DUAL", 0&)
    
    GetServerDate = Format(RS.Fields(0).Value, "DD/MM/YYYY")
    
    RS.Close
    Set RS = Nothing
End Function
Public Function AutoGenRowNo(mTable As String, mMaxField As String, DbCN As ADODB.Connection, Optional mCondition As String) As Long
On Error GoTo ERR1
Dim RS As ADODB.Recordset
Dim SqlStr As String
Dim SeqName As String
    Set RS = Nothing
    SeqName = "Seq_" & mTable & "_" & mMaxField
    
    SqlStr = "Select " & SeqName & ".NextVal from Dual"
    ''Set Rs = DbCN.Execute(SqlStr)
    UOpenRecordSet SqlStr, DbCN, adOpenStatic, RS, adLockReadOnly

    
    If Not IsNull(RS.Fields(0)) Then
        AutoGenRowNo = RS.Fields(0)
    Else
        AutoGenRowNo = 1
    End If

    If UCase(mMaxField) = "CODE" Then  '' FOR MASTER ADD COMPANY & BRANCH
        AutoGenRowNo = RsCompany.Fields("Company_Code").Value & Format(AutoGenRowNo, "000000")
    End If
Exit Function
ERR1:
ErrorMsg Err.Description, Err.Number, vbCritical
End Function

Public Sub mAutoEmail(pPubDBCn As ADODB.Connection)
On Error GoTo ErrPart
Dim SqlStr As String
Dim RsTemp As ADODB.Recordset
Dim mTo As String
Dim mCC As String
Dim mFrom As String
Dim mAttachmentFile As String
Dim mDateTime As String
Dim pAccountCode As String
Dim mSubject As String
    ' *****************************************************************************
    ' This is where all of the Components Properties are set / Methods called
    ' *****************************************************************************

    strServerPop3 = ReadInI("InternetInfo", "POP3", "InternetInfo.INI")
    strServerSmtp = ReadInI("InternetInfo", "SMTP", "InternetInfo.INI")
    strAccount = ReadInI("InternetInfo", "Account", "InternetInfo.INI")
    strPassword = ReadInI("InternetInfo", "Password", "InternetInfo.INI")
    mTo = ReadInI("InternetInfo", "TO", "InternetInfo.INI")
    mCC = ReadInI("InternetInfo", "CC", "InternetInfo.INI")
    mFrom = ReadInI("InternetInfo", "FROM", "InternetInfo.INI")
    mAttachmentFile = ReadInI("InternetInfo", "ATTACHMENT", "InternetInfo.INI")
    mAttachmentFile = App.Path & "\Files\" & mAttachmentFile
    pAccountCode = ReadInI("InternetInfo", "ACCOUNTCODE", "InternetInfo.INI")
    If strServerPop3 = "" And strServerSmtp = "" And strAccount = "" And strPassword = "" Then
        MsgBox "Please Check Email Configuration", vbInformation
        Exit Sub
    End If
    Screen.MousePointer = vbHourglass
    
    If CreateXLSFile(mAttachmentFile, mDateTime, pAccountCode, pPubDBCn) = False Then GoTo ErrPart
    mSubject = " Material Consumption Daily Report as on Date : " & mDateTime
'    If GetInternetConnection(strServerPop3, strAccount, strPassword) = False Then
'        MsgBox "Connection could not be establised", vbInformation
'    Else
'        Call SendMail(strServerSmtp, mTo, mTo, mCC, mSubject & mDateTime, "", mAttachmentFile)
'    End If

    
    If Trim(mTo) <> "" Then
        Call SendMailProcess(mFrom, mTo, mCC, "", strAccount, strPassword, mAttachmentFile, mSubject)
    End If
    

    Screen.MousePointer = vbDefault
ErrPart:

End Sub
Public Function CreateXLSFile(mAttachmentFile As String, mDate As String, pAccountCode As String, pPubDBCn As ADODB.Connection) As Boolean
On Error GoTo ErrPart
'Dim mLineCount As Long
'Dim pPageNo As Long
Dim cntRow As Double
'Dim pFileName As String
Dim mItemCode As String
Dim mScheduleQty As String
Dim mTillDateScheduleQty As String
'Dim mStock As Double
'Dim mMRRQty As Double
Dim mHeadingline As Long
Dim mSNO As Long
Dim exlobj As Object
Dim mSqlStr As String
Dim RsTemp As ADODB.Recordset
Dim mScheduleDate As String
'Dim mDate As String
Dim mScheduleNo As String
Dim mAmendNo As Long
Dim mDays As Long
Dim mFYEAR As Long
Dim mItemUOM As String
Dim I As Long
Dim J As Long
Dim mMRRDate As String
Dim mColHeader As String
Dim mColHeader1 As Long
Dim mColHeader2 As Long
Dim mLastDateofMonth As Long
Dim mCompanyCode As Long
Dim mDespQty As Double
Dim pOPQty As Double
Dim pCLQty As Double
Dim pPurQty As Double
Dim pINHouseQty As Double
Dim pDespQty As Double
Dim pTodayPurQty As Double
Dim pTodayDespQty As Double
Dim mRate As Double
Dim mToDayPurchase As Double
Dim mToDayDespatch As Double
Dim pAlterCode As String

    mHeadingline = 1
    mDate = DateAdd("d", -1, Format(PubCurrDate, "DD/MM/YYYY"))     ''Format(PubCurrDate, "DD/MM/YYYY")       ''
    mDays = Day(mDate)
    mFYEAR = GetFYear(pPubDBCn, mDate)
    mLastDateofMonth = LastDay(Month(mDate), Year(mDate))
    
    Set exlobj = CreateObject("excel.application")
    exlobj.Visible = True
    exlobj.Workbooks.Open (mAttachmentFile & ".XLS")
    

    exlobj.ActiveSheet.Cells(2, 1).Value = "AS ON DATE : " & mDate
    exlobj.ActiveSheet.Cells(6, 4).Value = "Stock Qty as on " & "01/" & Format(mDate, "MM/YYYY")
    exlobj.ActiveSheet.Cells(6, 10).Value = "Stock Qty as on " & mDate
    mHeadingline = 8
    mCompanyCode = PubCompanyCode
    
    For I = 1 To 500
        With exlobj.ActiveSheet
            mScheduleNo = ""
            mAmendNo = 0
            mScheduleQty = ""
            mTillDateScheduleQty = ""
            
            
            pOPQty = 0
            pCLQty = 0
            pPurQty = 0
            pINHouseQty = 0
            pDespQty = 0
            
            mItemCode = Trim(.Cells(mHeadingline, 1).Value)
'            mCompanyCode = Val(.Cells(mHeadingline, 15).Value)
          
            If mItemCode = "" Then GoTo NextRec
            
            If ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", pPubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = False Then
                GoTo NextRec
            End If
            
            If ValidateWithMasterTable(mItemCode, "ITEM_CODE", "PURCHASE_UOM", "INV_ITEM_MST", pPubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
                mItemUOM = MasterNo
            End If
            
            mRate = GetCurrentItemRate(mItemCode, Format(mDate, "DD/MM/YYYY"), pPubDBCn)
            
            If GetScheduleDetail(mCompanyCode, mItemCode, mDate, mFYEAR, pPubDBCn, mScheduleNo, mAmendNo, mScheduleQty) = False Then GoTo ErrPart
            If GetTillDateScheduleDetail(mCompanyCode, mItemCode, mDate, pPubDBCn, mTillDateScheduleQty) = False Then GoTo ErrPart
            
'            mStock = GetBalanceStockQty(mCompanyCode, mItemCode, mDate, mItemUOM, "STR", "ST", "", "WH", mFYEAR, pPubDbcn)
'            mStock = mStock + GetBalanceStockQty(mCompanyCode, mItemCode, mDate, mItemUOM, "STR", "QC", "", "WH", mFYEAR, pPubDbcn)
'            mMRRQty = GetMRRDetail(mCompanyCode, mItemCode, mDate, mFYEAR, pPubDbcn)

            If GetDetail(mCompanyCode, mItemCode, mDate, mFYEAR, pOPQty, pCLQty, mToDayPurchase, pPurQty, pINHouseQty, mToDayDespatch, pDespQty, pPubDBCn, pAlterCode) = False Then GoTo ErrPart
            
            .Cells(mHeadingline, 3).Value = mRate
            .Cells(mHeadingline, 4).Value = pOPQty
            .Cells(mHeadingline, 5).Value = mTillDateScheduleQty
            .Cells(mHeadingline, 6).Value = mToDayPurchase
            .Cells(mHeadingline, 7).Value = pPurQty         ''mScheduleQty
            .Cells(mHeadingline, 8).Value = mToDayDespatch
            .Cells(mHeadingline, 9).Value = pDespQty
            .Cells(mHeadingline, 10).Value = pCLQty
            .Cells(mHeadingline, 16).Value = pAlterCode
NextRec:
            mHeadingline = mHeadingline + 1
        
        End With
    Next
'    With exlobj.ActiveSheet
'        For J = 71 To 101
'            If J <= 90 Then
'                mColHeader = Chr(J)
'            Else
'                mColHeader1 = J - 90 + 64
'                mColHeader = "A" & Chr(mColHeader1)
'            End If
'
'            If J > mDays + 70 Then
'                .Columns("" & mColHeader & "").EntireColumn.Hidden = True
'            Else
'                .Columns("" & mColHeader & "").EntireColumn.Hidden = False
'            End If
'        Next
'    End With
    mAttachmentFile = mAttachmentFile & "_" & Format(mDate, "DDMMYYYY") & ".xls"
    With exlobj
        
        .ScreenUpDating = False
        .DisplayAlerts = False
    End With

    exlobj.ActiveWorkbook.SaveAs mAttachmentFile
'    exlobj.Close
    exlobj.Quit
   CreateXLSFile = True
Exit Function
ErrPart:
    MsgBox Err.Description
    CreateXLSFile = False
'    Resume
'    Close #1
End Function



Public Function GetCurrentItemRate(pItemCode As String, xRefDate As String, pPubDBCn As ADODB.Connection) As Double
On Error GoTo ErrPart:
Dim SqlStr As String
Dim RsTemp As ADODB.Recordset
Dim RsTemp1 As ADODB.Recordset
Dim RsTempMain As ADODB.Recordset

Dim mItemRate As Double
Dim mPurchaseUOM As String
Dim xItemUOM As String
Dim pFactor As Double
Dim mRMCost As Double
Dim xPurchaseCost As Double
Dim mPartyCode As String
Dim mTotalQtyPurchase As Double
Dim mTotQty As Double
Dim mTotValuePurchase As Double
'Dim PvtDBCn As ADODB.Connection
'
'    Set PvtDBCn = New ADODB.Connection
'    PvtDBCn.Open StrConn
    
    pFactor = 1
    mTotalQtyPurchase = 0
    
    ''TOP ATTACHMENT (04010008)
    
'    PubDBCn.Close
'    If PubDBCn.State = adStateClosed Then
'        PubDBCn.Open StrConn
'        SqlStr = "SELECT * FROm GEN_COMPANY_MST"
'        UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsCompany, adLockReadOnly
'    End If
    ''
    
    SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR,PURCHASE_COST FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany!COMPANY_CODE & " AND ITEM_CODE='" & AllowSingleQuote(pItemCode) & "'"
    
    UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RsTemp1, adLockReadOnly
    
    If RsTemp1.EOF = False Then
        xItemUOM = IIf(IsNull(RsTemp1!ISSUE_UOM), "", RsTemp1!ISSUE_UOM)
        pFactor = IIf(IsNull(RsTemp1!UOM_FACTOR) Or RsTemp1!UOM_FACTOR = 0, 1, RsTemp1!UOM_FACTOR)
        xPurchaseCost = IIf(IsNull(RsTemp1!PURCHASE_COST), 0, RsTemp1!PURCHASE_COST) / pFactor
    End If
    
    SqlStr = " SELECT DISTINCT SUPP_CUST_CODE  " & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY AND IH.PO_STATUS='Y' AND IH.PUR_TYPE IN ('P','J')"
   
    SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & AllowSingleQuote(pItemCode) & "'"
    
    UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RsTempMain, adLockReadOnly
    
    If RsTempMain.EOF = False Then
        Do While RsTempMain.EOF = False
            mPartyCode = IIf(IsNull(RsTempMain!SUPP_CUST_CODE), "-1", RsTempMain!SUPP_CUST_CODE)
            
            SqlStr = " SELECT (NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,4)) PURCHASE_COST, " & vbCrLf _
                        & " ITEM_UOM,IH.PUR_TYPE,IH.PO_CLOSED, ID.PO_WEF_DATE  " & vbCrLf _
                        & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID " & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _
                        & " AND IH.MKEY=ID.MKEY AND IH.PO_STATUS='Y' AND IH.PUR_TYPE='P'"       '' IN ('P','J')"
            
            SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                        & " AND IH.SUPP_CUST_CODE='" & AllowSingleQuote(mPartyCode) & "'" & vbCrLf _
                        & " AND ID.PO_WEF_DATE = ( " & vbCrLf _
                        & " SELECT MAX(PO_WEF_DATE) " & vbCrLf _
                        & " FROM PUR_PURCHASE_HDR SIH, PUR_PURCHASE_DET SID" & vbCrLf _
                        & " WHERE SIH.COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _
                        & " AND SIH.MKEY=SID.MKEY AND SIH.PO_STATUS='Y' AND SIH.PUR_TYPE ='P'"       '' IN ('P','J')"
             
            SqlStr = SqlStr & vbCrLf & " AND SID.ITEM_CODE='" & AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                        & " AND SIH.SUPP_CUST_CODE='" & AllowSingleQuote(mPartyCode) & "'" & vbCrLf _
                        & " AND SID.PO_WEF_DATE <= '" & Format(xRefDate, "DD-MMM-YYYY") & "')"
             
            SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PO_CLOSED, ID.PO_WEF_DATE DESC"
             
            UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RsTemp, adLockReadOnly
             
            If RsTemp.EOF = False Then
                 mItemRate = IIf(IsNull(RsTemp!PURCHASE_COST), 0, RsTemp!PURCHASE_COST)
                 mPurchaseUOM = IIf(IsNull(RsTemp!ITEM_UOM), 0, RsTemp!ITEM_UOM)
                     
'                 If RsTemp!PUR_TYPE = "J" Then
'                     mRMCost = GetTotalJWRMCost(pItemCode, xRefDate, mPurchaseUOM, pFactor)
'                     mItemRate = mItemRate + mRMCost
'                 End If
                 
                 mTotQty = GetQtyPurchase(mPartyCode, pItemCode, xRefDate, pPubDBCn)
                 mTotValuePurchase = mTotValuePurchase + (mItemRate * mTotQty)
                 mTotalQtyPurchase = mTotalQtyPurchase + mTotQty
            Else
'                 mItemRate = GetTotalJWRMCost(pItemCode, xRefDate, mPurchaseUOM, pFactor)
'                 mTotQty = 1
'                 mTotValuePurchase = mTotValuePurchase + (mItemRate * mTotQty)
                 mTotalQtyPurchase = mTotalQtyPurchase      ''mTotalQtyPurchase + mTotQty
            End If
            RsTempMain.MoveNext
        Loop
        If mTotalQtyPurchase > 0 Then
            mItemRate = Format(mTotValuePurchase / mTotalQtyPurchase, "0.00")
        End If
    End If
    
    
    If mItemRate = 0 Then
        mItemRate = xPurchaseCost
    Else
        If xItemUOM <> mPurchaseUOM Then
            mItemRate = mItemRate / pFactor
        End If
    End If
    
    GetCurrentItemRate = mItemRate
Exit Function
ErrPart:
'    Resume
    ErrorMsg Err.Description, Err.Number, vbCritical
    GetCurrentItemRate = 0
End Function

Public Function GetQtyPurchase(pPartyCode As String, pItemCode As String, xRefDate As String, pPubDBCn As ADODB.Connection) As Double
On Error GoTo ErrPart:
Dim SqlStr As String
Dim RsTemp As ADODB.Recordset
'Dim PvtDBCn As ADODB.Connection
'
'    Set PvtDBCn = New ADODB.Connection
'    PvtDBCn.Open StrConn
    
''INV_GATE_HDR IH, AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR

        SqlStr = " SELECT SUM(NVL(RECEIVED_QTY,0)) RECEIVED_QTY " & vbCrLf _
                    & " FROM INV_GATE_DET ID " & vbCrLf _
                    & " WHERE ID.COMPANY_CODE=" & RsCompany!COMPANY_CODE & " " & vbCrLf _
                    & " AND ID.SUPP_CUST_CODE='" & AllowSingleQuote(pPartyCode) & "' AND ID.ITEM_CODE='" & AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                    & " AND TO_CHAR(ID.MRR_DATE,'YYYYMM') = '" & Format(xRefDate, "YYYYMM") & "'"

        UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RsTemp, adLockReadOnly

        If RsTemp.EOF = False Then
            GetQtyPurchase = IIf(IsNull(RsTemp!RECEIVED_QTY), 0, RsTemp!RECEIVED_QTY)
        End If

Exit Function
ErrPart:
'    Resume
    ErrorMsg Err.Description, Err.Number, vbCritical

End Function

Public Function GetBalanceStockQty(mCompanyCode As Long, pItemCode As String, pDateTo As String, pPackUnit As String, pDeptCode As String, _
pStockType As String, pLotNo As String, pStock_ID As String, mFYEAR As Long, pPubDBCn As ADODB.Connection, _
Optional pRefType As String, Optional pRefNo As Double) As Double
On Error GoTo ErrPart
Dim SqlStr As String
Dim RsBalStock As ADODB.Recordset
Dim mBalQty As Double

Dim RsTemp As ADODB.Recordset
Dim mIssueUOM As String
Dim mPurchaseUOM As String
Dim mFactor As Double
Dim mTableName As String
    SqlStr = ""
    
    ''COMPANY_CODE, FYEAR, STOCK_ID, STOCK_TYPE, ITEM_CODE,REF_DATE

    
    SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY"
    
    If mCompanyCode = 1 Then
        mTableName = "INV_STOCK_REC_TRN" & mFYEAR
    ElseIf mCompanyCode = 3 Or mCompanyCode = 10 Or mCompanyCode = 12 Then
        mTableName = "INV_STOCK_REC_TRN" & Format(mCompanyCode, "00") & mFYEAR
    Else
        mTableName = "INV_STOCK_REC_TRN"
    End If
    
    SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " "
    
    SqlStr = SqlStr & vbCrLf _
            & " WHERE COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
            & " AND FYEAR=" & mFYEAR & ""
            
    SqlStr = SqlStr & vbCrLf & "AND STOCK_ID='" & pStock_ID & "'"
            
    If pStockType = "QC" Then
        SqlStr = SqlStr & vbCrLf & " AND (STOCK_TYPE='" & pStockType & "' OR E_DATE>TO_DATE('" & Format(pDateTo, "dd-mmm-yyyy") & "'))"
    Else
        If pStockType = "" Then
            SqlStr = SqlStr & vbCrLf & " AND E_DATE<=TO_DATE('" & Format(pDateTo, "dd-mmm-yyyy") & "')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND (STOCK_TYPE='" & pStockType & "' AND E_DATE<=TO_DATE('" & Format(pDateTo, "dd-mmm-yyyy") & "'))"
        End If
    End If
    
    If pDeptCode <> "" And pStock_ID = "PH" Then
        SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & pDeptCode & "'"
    ElseIf pDeptCode = "PAD" And pStock_ID = "WH" And pStockType = "FG" Then
        ''02-08-2006
'        SqlStr = SqlStr & vbCrLf & "AND (DEPT_CODE_FROM='" & pDeptCode & "' OR DEPT_CODE_TO='" & pDeptCode & "')"
    End If
    
    If pLotNo <> "" Then
        SqlStr = SqlStr & vbCrLf & " AND BATCH_NO='" & AllowSingleQuote(UCase(pLotNo)) & "'"
    End If
    
    If pRefType <> "" And Val(pRefNo) <> 0 Then
        SqlStr = SqlStr & vbCrLf _
                & " AND REF_TYPE || REF_NO <> '" & pRefType & pRefNo & "'"
    End If
    
    SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & AllowSingleQuote(pItemCode) & "'"
    SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"
    
    SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & Format((pDateTo), "DD-MMM-YYYY") & "')"
    UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RsBalStock, adLockReadOnly
    
    If RsBalStock.EOF = False Then
        If IsNull(RsBalStock.Fields(0).Value) Then
            mBalQty = 0
        Else
            mBalQty = RsBalStock.Fields(0).Value
        End If
    Else
        mBalQty = 0
    End If
    
    Set RsBalStock = Nothing
    
    If mBalQty <> 0 Then
        Set RsTemp = Nothing
        
        SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR FROM INV_ITEM_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & mCompanyCode & " AND ITEM_CODE='" & AllowSingleQuote(pItemCode) & "'"
        UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        
        If RsTemp.EOF = False Then
            mIssueUOM = IIf(IsNull(RsTemp!ISSUE_UOM), "", RsTemp!ISSUE_UOM)
            mPurchaseUOM = IIf(IsNull(RsTemp!PURCHASE_UOM), "", RsTemp!PURCHASE_UOM)
            mFactor = IIf(IsNull(RsTemp!UOM_FACTOR) Or RsTemp!UOM_FACTOR = 0, 1, RsTemp!UOM_FACTOR)
            
            If pPackUnit = mPurchaseUOM Then
                mBalQty = mBalQty / mFactor
            End If
            
            Set RsTemp = Nothing
'            RsTemp.Close
        End If
    End If
    
    GetBalanceStockQty = mBalQty
    
Exit Function
ErrPart:
    GetBalanceStockQty = 0
End Function
Public Function GetFYear(pPubDBCn As ADODB.Connection, mDate As String) As Long
On Error GoTo FillFYErr
Dim SqlStr As String
Dim RsCFYNo As ADODB.Recordset
    SqlStr = "SELECT FYEAR,START_DATE,END_DATE FROM GEN_CMPYRDTL_TRN" _
            & " WHERE COMPANY_CODE=" & RsCompany!COMPANY_CODE & " " _
            & " AND START_DATE<=TO_DATE('" & Format(mDate, "DD-MMM-YYYY") & "') " _
            & " AND END_DATE>=TO_DATE('" & Format(mDate, "DD-MMM-YYYY") & "') "
            
    UOpenRecordSet SqlStr, pPubDBCn, adOpenStatic, RsCFYNo
    If Not RsCFYNo.EOF Then
        GetFYear = Format(CStr(RsCFYNo!FYEAR), "0000")
    End If
    Exit Function
FillFYErr:
    GetFYear = -1
End Function

Public Sub SendMailProcessOld(pFrom As String, pRecipient As String, pCcRecipient As String, pBccRecipient As String, pUserName As String, pUserPassword As String, mAttachmentFile As String, mSubject As String)
'Private WithEvents poSendMail As vbSendMail.clsSendMail

    bAuthLogin = True
    With poSendMail

        ' **************************************************************************
        ' Optional properties for sending email, but these should be set first
        ' if you are going to use them
        ' **************************************************************************

        .SMTPHostValidation = VALIDATE_NONE         ' Optional, default = VALIDATE_HOST_DNS
        .EmailAddressValidation = VALIDATE_SYNTAX   ' Optional, default = VALIDATE_SYNTAX
        .Delimiter = ";"                            ' Optional, default = ";" (semicolon)

        ' **************************************************************************
        ' Basic properties for sending email
        ' **************************************************************************
        .SMTPHost = strServerSmtp   ''txtServer.text                  ' Required the fist time, optional thereafter
        .From = pFrom                      ' Required the fist time, optional thereafter
        .FromDisplayName = pFrom       ' Optional, saved after first use
        .Recipient = pRecipient                     ' Required, separate multiple entries with delimiter character
        .RecipientDisplayName = ""      ''TxtTo.text      ' Optional, separate multiple entries with delimiter character
        .CcRecipient = pCcRecipient                        ' Optional, separate multiple entries with delimiter character
        .CcDisplayName = "" '' txtCcName                  ' Optional, separate multiple entries with delimiter character
        .BccRecipient = pBccRecipient                      ' Optional, separate multiple entries with delimiter character
        .ReplyToAddress = pFrom             ' Optional, used when different than 'From' address
        .Subject = mSubject                  ' Optional
        .Message = "AUTO GENEREATED MAIL."      '' txtMsg.text                      ' Optional
        .Attachment = mAttachmentFile       ''Trim(txtAttachFile.Text)          ' Optional, separate multiple entries with delimiter character

        ' **************************************************************************
        ' Additional Optional properties, use as required by your application / environment
        ' **************************************************************************
        .AsHTML = bHtml                             ' Optional, default = FALSE, send mail as html or plain text
        .ContentBase = ""                           ' Optional, default = Null String, reference base for embedded links
        .EncodeType = MyEncodeType                  ' Optional, default = MIME_ENCODE
        .Priority = etPriority                      ' Optional, default = PRIORITY_NORMAL
        .Receipt = bReceipt                         ' Optional, default = FALSE
        .UseAuthentication = bAuthLogin             ' Optional, default = FALSE
        .UsePopAuthentication = bPopLogin           ' Optional, default = FALSE
        .UserName = pUserName  ''txtUserName                     ' Optional, default = Null String
        .Password = pUserPassword  ''TxtPassword                     ' Optional, default = Null String, value is NOT saved
        .POP3Host = strServerPop3       ''txtPopServer
        .MaxRecipients = 100                        ' Optional, default = 100, recipient count before error is raised
'        .UseAuthentication = True
'        .UsePopAuthentication = True
        ' **************************************************************************
        ' Advanced Properties, change only if you have a good reason to do so.
        ' **************************************************************************
'         .ConnectTimeout = 10                      ' Optional, default = 10
'         .ConnectRetry = 5                         ' Optional, default = 5
'         .MessageTimeout = 60                      ' Optional, default = 60
'         .PersistentSettings = True                ' Optional, default = TRUE
         .SMTPPort = 25                            ' Optional, default = 25
        
        ' **************************************************************************
        ' OK, all of the properties are set, send the email...
        ' **************************************************************************
'         .connect                                  ' Optional, use when sending bulk mail
        .Send                                       ' Required
'         .Disconnect                               ' Optional, use when sending bulk mail
'        txtServer.text = .SMTPHost                  ' Optional, re-populate the Host in case
                                                    ' MX look up was used to find a host    End With
    End With
End Sub


Public Sub SendMailProcess(pFrom As String, pRecipient As String, pCcRecipient As String, pBccRecipient As String, pUserName As String, pUserPassword As String, mAttachmentFile As String, mSubject As String)

'Private Sub SendMail(strServer$, strFrom$, strTo$, strSubject$, strBodyText$)
On Error GoTo SendMailErr
Dim x%, y
Dim SMTP As Object
Dim Msg As String

Dim strToArray() As String
Dim strCCArray() As String
Dim strBCCArray() As String

    Set SMTP = CreateObject("EasyMail.SMTP.5")
    SMTP.LicenseKey = "brain/S1cI500R1AX50C0R0200"
    
'    strServerPop3 = ReadInI("InternetInfo", "POP3", "InternetInfo.INI")
'    strServerSmtp = ReadInI("InternetInfo", "SMTP", "InternetInfo.INI")
'    strAccount = ReadInI("InternetInfo", "Account", "InternetInfo.INI")
'    strPassword = ReadInI("InternetInfo", "Password", "InternetInfo.INI")
    
    
    SMTP.MailServer = strServerSmtp
    SMTP.FromAddr = pFrom
'    SMTP.AddRecipient "", pRecipient, 1
    
    strToArray = Split(pRecipient, ";")
    strCCArray = Split(pCcRecipient, ";")
    strBCCArray = Split(pBccRecipient, ";")

    For y = 0 To UBound(strToArray)
        If Trim(pRecipient) <> "" Then
            SMTP.AddRecipient strToArray(y), strToArray(y), 1
        End If
    Next y
    For y = 0 To UBound(strCCArray)
        If Trim(pCcRecipient) <> "" Then
            SMTP.AddRecipient strCCArray(y), strCCArray(y), 2
        End If
    Next y
    For y = 0 To UBound(strBCCArray)
        If Trim(pBccRecipient) <> "" Then
            SMTP.AddRecipient strBCCArray(y), strBCCArray(y), 3
        End If
    Next y


    SMTP.Subject = mSubject
    SMTP.BodyText = "AUTO GENEREATED MAIL. " & mAttachmentFile
    outSourec = mAttachmentFile
    If outSourec <> "" Then
        y = SMTP.AddAttachment(outSourec, 0)
    End If
    SMTP.BodyEncoding = 1
    SMTP.TimeOut = 3600
    
   
    x% = SMTP.Send
    If x% = 0 Then
       Msg = "Message sent successfully."
    Else
       Msg = "There was an error sending your message.  Error: "
       GoTo SendMailErr
    End If
    If y = 0 Then
    Else
        Msg = "Error with attachment. Error: "
        GoTo SendMailErr
    End If
'    SMTP.Clear (1 + 2)
'    If pCcRecipient <> "" Then
'        SMTP.AddRecipient "", pCcRecipient, 2
'        SMTP.Send
'    End If
'    If pBccRecipient <> "" Then
'        SMTP.AddRecipient "", pBccRecipient, 3
'        SMTP.Send
'    End If
    
    Set SMTP = Nothing
   outSourec = ""
'   CmdSend.Enabled = False
Exit Sub
SendMailErr:
    MsgBox Msg & CStr(x%) & " " & vbCrLf _
         & "" & GetErrorMSG(Int(x%)), vbCritical
'    CmdSend.Enabled = True
'    Resume
'End Sub

End Sub
Public Function GetErrorMSG(mVal As Integer) As String
Select Case mVal
    Case 1
        GetErrorMSG = "An unknown error has occurred"
    Case 2
        GetErrorMSG = "An error has resulted because there was no message specified"
    Case 3
        GetErrorMSG = "The process has run out of memory."
    Case 4
        GetErrorMSG = "An error has occurred due to a problem with the message body or attachments."
    Case 5
        GetErrorMSG = "There was a problem initiating the conversation with the mail server. Ensure the setting of the Domain property is correct."
    Case 6
        GetErrorMSG = "There was an error terminating the conversation with the SMTP mail server."
    Case 7
        GetErrorMSG = "The from address was not formatted correctly or was rejected by the SMTP mail server. Some SMTP mail servers will only accept mail from particular addresses or domains. SMTP mail servers may also reject a from address if the server can not successfully do a reverse lookup on the from address."
    Case 8
        GetErrorMSG = "An error was reported in response to a recipient address. The SMTP server may refuse to handle mail for unknown recipients."
    Case 9
        GetErrorMSG = "There was an error connecting to the SMTP mail server."
    Case 10
        GetErrorMSG = "There was an error opening a file. If you have specified file attachments, ensure that they exist and the you have access to them."
    Case 11
        GetErrorMSG = "There was an error reading a file. If you have specified file attachments, ensure that they exist and the you have access to them."
    Case 12
        GetErrorMSG = "There was an error writing to a file. Ensure that you have sufficient disk space."
    Case 15
        GetErrorMSG = "No mail server specified."
    Case 16
        GetErrorMSG = "There was a problem with the connection and a socket error occured."
    Case 17
        GetErrorMSG = "Could not resolve host."
    Case 18
        GetErrorMSG = "Connected but server sent back bad response."
    Case 19
        GetErrorMSG = "Could not create thread."
    Case 20
        GetErrorMSG = "Cancelled as a result of calling the Cancel() method."
    Case 21
        GetErrorMSG = "The operation timed-out while the host was being resolved."
    Case 22
        GetErrorMSG = "The operation timed-out while connecting."
    Case 24
        GetErrorMSG = "ESMTP Authentication failed."
    Case 25
        GetErrorMSG = "The selected ESMTP Authentication mode is not supported by the server."
    Case 26
        GetErrorMSG = "ESMTP Authentication protocol error."
    Case 27
        GetErrorMSG = "Socket Timeout Error"
End Select
End Function
Public Function GetInternetConnection(strServer$, strAccount$, strPassword$) As Boolean
    Dim POP3 As Object
    Dim x As Variant
    
    Set POP3 = CreateObject("EasyMail.POP3.5")
    POP3.Account = strAccount$
    POP3.Password = strPassword$
    POP3.MailServer = strServer
    POP3.LicenseKey = "brain/S1cI500R1AX50C0R0200"
    x = POP3.Connect()
    If x <> 0 Then
        GetInternetConnection = False
        Exit Function
    Else
        GetInternetConnection = True
    End If
End Function
Public Sub SendMail(strServer As String, strFrom As String, strTo As String, strCC As String, strSubject As String, strBodyText As String, outSourec As String)
On Error GoTo SendMailErr
    Dim x%, y
    Dim SMTP As Object
    Dim Msg As String
    
    Set SMTP = CreateObject("EasyMail.SMTP.5")
    SMTP.LicenseKey = "brain/S1cI500R1AX50C0R0200"
    SMTP.MailServer = strServer
    SMTP.FromAddr = strFrom
    SMTP.AddRecipient "", strTo, 1
    SMTP.Subject = strSubject
    SMTP.BodyText = strBodyText
    If outSourec <> "" Then
        y = SMTP.AddAttachment(outSourec, 0)
    End If
    SMTP.BodyEncoding = 1
    SMTP.TimeOut = 3600
    x% = SMTP.Send
    If x% = 0 Then
       Msg = "Message sent successfully."
    Else
       Msg = "There was an error sending your message.  Error: "
       GoTo SendMailErr
    End If
    If y = 0 Then
    Else
        Msg = "Error with attachment. Error: "
        GoTo SendMailErr
    End If
    'SMTP.Clear (1 + 2)
    If strCC <> "" Then
        SMTP.AddRecipient "", Trim(strCC), 1
        SMTP.Send
    End If
'    If TxtBCC.Text <> "" Then
'        SMTP.AddRecipient "", Trim(TxtBCC.Text), 1
'        SMTP.Send
'    End If
    
    Set SMTP = Nothing
   outSourec = ""
   
Exit Sub
SendMailErr:
    MsgBox Msg & CStr(x%) & " " '''& vbCrLf _
         & "" & GetErrorMSG(Int(x%)), vbCritical
'    CmdSend.Enabled = True
'    Resume
End Sub

