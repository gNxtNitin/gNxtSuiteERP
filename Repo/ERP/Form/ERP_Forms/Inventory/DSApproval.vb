Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDSApproval
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColDSNo As Short = 1
    Private Const ColDSDate As Short = 2
    Private Const ColDSAmendNo As Short = 3
    Private Const ColDSAmendDate As Short = 4
    Private Const ColPartyName As Short = 5
    Private Const ColPONo As Short = 6
    Private Const ColPODate As Short = 7
    Private Const ColSchdDate As Short = 8
    Private Const ColPostStatus As Short = 9

    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mDSNo As Double
        Dim mDSAmendNo As Integer
        Dim mDSDate As String
        Dim mDSAmendDate As String
        Dim pMailCount As Integer
        Dim mUpdateCount As Integer
        Dim mApprovalRight As String
        Dim mEMailSend As Boolean
        mApprovalRight = "Y"

        If mApprovalRight = "N" Then
            MsgInformation("You have no right to Approved Deliverly Schedule. Please Contact to Corporate Purchase Team.")
            Exit Sub
        End If

        mEMailSend = False


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            mEMailSend = False
        Else
            If MsgQuestion("Are you also Want to send eMail to Vendor?") = CStr(MsgBoxResult.Yes) Then
                mEMailSend = True
            End If
        End If
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColDSNo
                mDSNo = CDbl(Trim(.Text))

                .Col = ColDSDate
                mDSDate = Trim(.Text)

                .Col = ColDSAmendNo
                mDSAmendNo = CInt(Trim(.Text))

                .Col = ColDSAmendDate
                mDSAmendDate = Trim(.Text)

                .Col = ColPostStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    SqlStr = "UPDATE PUR_DELV_SCHLD_HDR SET POST_FLAG='Y'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_DELV=" & mDSNo & "" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                    PubDBCn.Execute(SqlStr)

                    If mEMailSend = True Then
                        Call DeliveryeMailSend(mDSNo, mDSDate, mDSAmendNo, mDSAmendDate, pMailCount)
                    End If
                    mUpdateCount = mUpdateCount + 1
                End If

            Next
        End With
        PubDBCn.CommitTrans()

        If mEMailSend = True Then
            MsgBox("Total " & mUpdateCount & " DS Posted and " & pMailCount & " mail send to party.", MsgBoxStyle.Information)
        Else
            MsgBox("Total " & mUpdateCount & " DS Posted.", MsgBoxStyle.Information)
        End If

        Exit Sub
ErrPart:
        ''Resume
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Sub

    Private Sub DeliveryeMailSend(ByRef pDSNo As Double, ByRef pDSdate As String, ByRef pDSAmendNo As Integer, ByRef pDSAmendDate As String, ByRef pMailCount As Integer)

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTo As String
        Dim mCC As String
        Dim mFrom As String
        Dim mAttachmentFile As String
        Dim mSubject As String

        Dim mSupplierName As String = ""
        Dim mCity As String
        Dim mAddress As String
        Dim mPin As String
        Dim mState As String

        Dim mBodyTextHeader As String
        Dim mBodyText As String
        Dim mBodyTextDetail As String

        Dim mDSNo As String
        Dim mAmendNo As String
        Dim mItemCode As String
        Dim mItemDesc As String

        Dim cntRow As Integer
        Dim cntCol As Integer

        Dim mMailCount As Integer

        ' *****************************************************************************
        ' This is where all of the Components Properties are set / Methods called
        ' *****************************************************************************

        mMailCount = 0



        mFrom = GetEMailID("MAIL_FROM") ''mFrom = GetEMailID("PUR_MAIL_TO")
        mCC = GetEMailID("PUR_MAIL_TO")


        mAttachmentFile = ""

        mBodyTextHeader = "<table width=6500 align=center border=1 cellPadding=2 cellSpacing=1>" & "<tr>" & "<td width=1000 align=center><b>Item Code & Name</b></td>" & "<td width=100 align=center><b>01</b></td>" & "<td width=100 align=center><b>02</b></td>" & "<td width=100 align=center><b>03</b></td>" & "<td width=100 align=center><b>04</b></td>" & "<td width=100 align=center><b>05</b></td>" & "<td width=100 align=center><b>06</b></td>" & "<td width=100 align=center><b>07</b></td>" & "<td width=100 align=center><b>08</b></td>" & "<td width=100 align=center><b>09</b></td>" & "<td width=100 align=center><b>10</b></td>" & "<td width=100 align=center><b>11</b></td>" & "<td width=100 align=center><b>12</b></td>" & "<td width=100 align=center><b>13</b></td>" & "<td width=100 align=center><b>14</b></td>" & "<td width=100 align=center><b>15</b></td>" & "<td width=100 align=center><b>16</b></td>" & "<td width=100 align=center><b>17</b></td>" & "<td width=100 align=center><b>18</b></td>" & "<td width=100 align=center><b>19</b></td>" & "<td width=100 align=center><b>20</b></td>"

        mBodyTextHeader = mBodyTextHeader & "<td width=100 align=center><b>21</b></td>" & "<td width=100 align=center><b>22</b></td>" & "<td width=100 align=center><b>23</b></td>" & "<td width=100 align=center><b>24</b></td>" & "<td width=100 align=center><b>25</b></td>" & "<td width=100 align=center><b>26</b></td>" & "<td width=100 align=center><b>27</b></td>" & "<td width=100 align=center><b>28</b></td>" & "<td width=100 align=center><b>29</b></td>" & "<td width=100 align=center><b>30</b></td>" & "<td width=100 align=center><b>31</b></td>" & "<td width=100 align=center><b>Total</b></td>" & "</tr>"


        mBodyTextDetail = mBodyTextHeader


        SqlStr = GetDSQry(pDSNo, pDSdate, pDSAmendNo, pDSAmendDate)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mSubject = "Delivery Schedule for the month of " & VB6.Format(RsTemp.Fields("SCHLD_DATE").Value, "MMMM , YYYY")
            mSupplierName = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
            mAddress = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
            mCity = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
            mState = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
            mPin = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
            mTo = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_MAILID").Value), "", RsTemp.Fields("SUPP_CUST_MAILID").Value)

            If Len(mTo) <= 5 Then
                mTo = ""
            End If

            If InStr(1, mTo, "@") = 0 Then
                mTo = ""
            End If

            Do While RsTemp.EOF = False
                mItemCode = Trim(RsTemp.Fields("ITEM_CODE").Value)
                mItemDesc = Trim(RsTemp.Fields("Item_Short_Desc").Value)

                mBodyTextDetail = mBodyTextDetail & "<tr>" & "<td align=Left>" & mItemCode & " - " & mItemDesc & "</td>"

                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day1").Value), 0, RsTemp.Fields("Day1").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day2").Value), 0, RsTemp.Fields("Day2").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day3").Value), 0, RsTemp.Fields("Day3").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day4").Value), 0, RsTemp.Fields("Day4").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day5").Value), 0, RsTemp.Fields("Day5").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day6").Value), 0, RsTemp.Fields("Day6").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day7").Value), 0, RsTemp.Fields("Day7").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day8").Value), 0, RsTemp.Fields("Day8").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day9").Value), 0, RsTemp.Fields("Day9").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day10").Value), 0, RsTemp.Fields("Day10").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day11").Value), 0, RsTemp.Fields("Day11").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day12").Value), 0, RsTemp.Fields("Day12").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day13").Value), 0, RsTemp.Fields("Day13").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day14").Value), 0, RsTemp.Fields("Day14").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day15").Value), 0, RsTemp.Fields("Day15").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day16").Value), 0, RsTemp.Fields("Day16").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day17").Value), 0, RsTemp.Fields("Day17").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day18").Value), 0, RsTemp.Fields("Day18").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day19").Value), 0, RsTemp.Fields("Day19").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day20").Value), 0, RsTemp.Fields("Day20").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day21").Value), 0, RsTemp.Fields("Day21").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day22").Value), 0, RsTemp.Fields("Day22").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day23").Value), 0, RsTemp.Fields("Day23").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day24").Value), 0, RsTemp.Fields("Day24").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day25").Value), 0, RsTemp.Fields("Day25").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day26").Value), 0, RsTemp.Fields("Day26").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day27").Value), 0, RsTemp.Fields("Day27").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day28").Value), 0, RsTemp.Fields("Day28").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day29").Value), 0, RsTemp.Fields("Day29").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day30").Value), 0, RsTemp.Fields("Day30").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("Day31").Value), 0, RsTemp.Fields("Day31").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(IIf(IsDbNull(RsTemp.Fields("PLANNED_QTY").Value), 0, RsTemp.Fields("PLANNED_QTY").Value), "0") & "</td>"
                mBodyTextDetail = mBodyTextDetail & "</tr>"

                RsTemp.MoveNext()
            Loop

            mBodyTextDetail = mBodyTextDetail & "</table>"

            mBodyText = "<html><body>To,<br />" & "<b>M/s </b>" & mSupplierName & "<br />" & "" & mAddress & "<br />" & "" & mCity & "<br />" & "" & mState & "<br />" & "" & mPin & "<br />" & "<br />" & "<br />" & "<b>Delivery Schedule No : </b>" & pDSNo & "<br />" & "<br />" & "<br />" & mBodyTextDetail & "<br />" & "<br />" & "<b>Please follow the instruction given as under : </b><br />" & "1. Our item code & description of material must be mention on your bill.<br />" & "2. Material dispatch advice should be send through eMail.<br />" & "3. Ensure material should come in standard packing (Qty. / Material description / Item code of material / Tag).<br />" & "4. Material Inspection report / MTC must be send along with each and every consignment.<br />" & "<br />" & "<br />" & "Your Faithfully<br />" & "for " & RsCompany.Fields("Company_Name").Value & "<br />" & "</body></html>"


            If Trim(mTo) <> "" Then
                If SendMailProcess(mFrom, mTo, mCC, "", mAttachmentFile, mSubject, mBodyText) = False Then GoTo ErrPart
                pMailCount = pMailCount + 1
            End If

        End If

        Exit Sub
        ''MsgInformation "Total " & mMailCount & " Mail/s sucessfully send."

ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Function GetDSQry(ByRef pDSNo As Double, ByRef pDSdate As String, ByRef pDSAmendNo As Integer, ByRef mDSAmendDate As String) As String
        On Error GoTo ERR1
        Dim mSqlStr As String


        '& " TO_CHAR(IH.AUTO_KEY_DELV), TO_CHAR(IH.DELV_AMEND_NO), " & vbCrLf _
        '
        ''SELECT CLAUSE...
        mSqlStr = " SELECT IH.SCHLD_DATE, " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " TRIM(ID.ITEM_CODE) AS ITEM_CODE, INVMST.ITEM_SHORT_DESC, " & vbCrLf & " CMST.SUPP_CUST_MAILID, CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_PIN,"

        mSqlStr = mSqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='01' THEN PLANNED_QTY ELSE 0 END)) AS DAY1," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='02' THEN PLANNED_QTY ELSE 0 END)) AS DAY2," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='03' THEN PLANNED_QTY ELSE 0 END)) AS DAY3," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='04' THEN PLANNED_QTY ELSE 0 END)) AS DAY4," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='05' THEN PLANNED_QTY ELSE 0 END)) AS DAY5," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='06' THEN PLANNED_QTY ELSE 0 END)) AS DAY6," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='07' THEN PLANNED_QTY ELSE 0 END)) AS DAY7,"

        mSqlStr = mSqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='08' THEN PLANNED_QTY ELSE 0 END)) AS DAY8," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='09' THEN PLANNED_QTY ELSE 0 END)) AS DAY9," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='10' THEN PLANNED_QTY ELSE 0 END)) AS DAY10," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='11' THEN PLANNED_QTY ELSE 0 END)) AS DAY11," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='12' THEN PLANNED_QTY ELSE 0 END)) AS DAY12," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='13' THEN PLANNED_QTY ELSE 0 END)) AS DAY13," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='14' THEN PLANNED_QTY ELSE 0 END)) AS DAY14,"

        mSqlStr = mSqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='15' THEN PLANNED_QTY ELSE 0 END)) AS DAY15," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='16' THEN PLANNED_QTY ELSE 0 END)) AS DAY16," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='17' THEN PLANNED_QTY ELSE 0 END)) AS DAY17," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='18' THEN PLANNED_QTY ELSE 0 END)) AS DAY18," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='19' THEN PLANNED_QTY ELSE 0 END)) AS DAY19," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='20' THEN PLANNED_QTY ELSE 0 END)) AS DAY20," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='21' THEN PLANNED_QTY ELSE 0 END)) AS DAY21,"

        mSqlStr = mSqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='22' THEN PLANNED_QTY ELSE 0 END)) AS DAY22," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='23' THEN PLANNED_QTY ELSE 0 END)) AS DAY23," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='24' THEN PLANNED_QTY ELSE 0 END)) AS DAY24," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='25' THEN PLANNED_QTY ELSE 0 END)) AS DAY25," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='26' THEN PLANNED_QTY ELSE 0 END)) AS DAY26," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='27' THEN PLANNED_QTY ELSE 0 END)) AS DAY27," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='28' THEN PLANNED_QTY ELSE 0 END)) AS DAY28,"

        mSqlStr = mSqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='29' THEN PLANNED_QTY ELSE 0 END)) AS DAY29," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='30' THEN PLANNED_QTY ELSE 0 END)) AS DAY30," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='31' THEN PLANNED_QTY ELSE 0 END)) AS DAY31,"



        mSqlStr = mSqlStr & vbCrLf & "TO_CHAR(SUM(PLANNED_QTY)) AS PLANNED_QTY"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM PUR_DELV_SCHLD_HDR IH, PUR_DAILY_SCHLD_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_DELV=" & Val(CStr(pDSNo)) & "" & vbCrLf & " AND IH.DELV_AMEND_NO=" & Val(CStr(pDSAmendNo)) & ""

        ''GROUP BY CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "GROUP BY " & vbCrLf & " IH.SCHLD_DATE, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC, " & vbCrLf & " CMST.SUPP_CUST_MAILID, CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_PIN"

        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC"


        GetDSQry = mSqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        OptSelection(1).Checked = True
        Show1()

        FormatSprdMain()
        cmdShow.Enabled = False
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.SetFocus: Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmDSApproval_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        If lblType.Text = "B" Then
            Me.Text = "Delivery Schedule (BOP/RM) - Approval"
        Else
            Me.Text = "Delivery Schedule (Other Than BOP/RM) - Approval"
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmDSApproval_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Call SetMainFormCordinate(Me)
        Me.Top = 0
        Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        FormatSprdMain()
        cmdShow.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "SELECT DSMain.AUTO_KEY_DELV,DSMain.DELV_SCHLD_DATE,  " & vbCrLf & " DSMain.DELV_AMEND_NO, DSMain.DELV_AMEND_DATE, " & vbCrLf & " ACM.SUPP_CUST_NAME, DSMain.AUTO_KEY_PO," & vbCrLf & " DSMain.PO_DATE,DSMain.SCHLD_DATE,''  " & vbCrLf & " FROM PUR_DELV_SCHLD_HDR DSMain,FIN_SUPP_CUST_MST ACM" & vbCrLf & " WHERE DSMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND DSMain.Company_Code=ACM.Company_Code " & vbCrLf & " AND DSMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DELV,LENGTH(AUTO_KEY_DELV)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND DSMain.POST_FLAG='N' "


        If lblType.Text = "B" Then
            SqlStr = SqlStr & vbCrLf & " AND DSMain.AUTO_KEY_DELV IN ( " & vbCrLf & " SELECT DISTINCT AUTO_KEY_DELV " & vbCrLf & " FROM PUR_DELV_SCHLD_DET ID, INV_ITEM_MST INVMST, INV_GENERAL_MST GMST" & vbCrLf & " WHERE ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(ID.AUTO_KEY_DELV,LENGTH(ID.AUTO_KEY_DELV)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND INVMST.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND INVMST.CATEGORY_CODE=GMST.GEN_CODE" & vbCrLf & " AND GMST.GEN_TYPE='C' AND GMST.PRD_TYPE IN ('P','R','B','I','D','3')" & vbCrLf & " )"
        Else
            SqlStr = SqlStr & vbCrLf & " AND DSMain.AUTO_KEY_DELV IN ( " & vbCrLf & " SELECT DISTINCT AUTO_KEY_DELV " & vbCrLf & " FROM PUR_DELV_SCHLD_DET ID, INV_ITEM_MST INVMST, INV_GENERAL_MST GMST" & vbCrLf & " WHERE ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(ID.AUTO_KEY_DELV,LENGTH(ID.AUTO_KEY_DELV)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND INVMST.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND INVMST.CATEGORY_CODE=GMST.GEN_CODE" & vbCrLf & " AND GMST.GEN_TYPE='C' AND GMST.PRD_TYPE NOT IN ('P','R','B','I','D','3')" & vbCrLf & " )"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY DSMain.DELV_SCHLD_DATE,DSMain.AUTO_KEY_DELV"

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()

        With SprdMain
            .MaxCols = ColPostStatus
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColDSNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColDSNo, 9)

            .Col = ColDSDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColDSDate, 8)

            .Col = ColDSAmendNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColDSAmendNo, 6)

            .Col = ColDSAmendDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColDSAmendDate, 8)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColPartyName, 22.5)

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPONo, 9)

            .Col = ColPODate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColPODate, 8)



            .Col = ColSchdDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColSchdDate, 8)


            .Col = ColPostStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColPostStatus, 6)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColSchdDate)
            '    SprdMain.OperationMode = OperationModeSingle
            '    SprdMain.DAutoCellTypes = True
            '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '    SprdMain.GridColor = &HC00000
        End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColDSNo
            .Text = "DS No."

            .Col = ColDSDate
            .Text = "DS Date"

            .Col = ColDSAmendNo
            .Text = "Amend No."

            .Col = ColDSAmendDate
            .Text = "Amend  Date"

            .Col = ColPartyName
            .Text = "Supplier Name"

            .Col = ColPONo
            .Text = "PO No."

            .Col = ColPODate
            .Text = "PO Date"

            .Col = ColSchdDate
            .Text = "Schd. date"

            .Col = ColPostStatus
            .Text = "Post Status"
        End With
    End Sub
    Private Sub frmDSApproval_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColPostStatus
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
End Class
