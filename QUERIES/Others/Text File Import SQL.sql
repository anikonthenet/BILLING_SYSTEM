
DELETE FROM RIPAN

SELECT * FROM RIPAN



//------------------------------------------------------------
//-- SQL SERVER
//-- IMPORT SQL FROM TEXT FILE
//------------------------------------------------------------

BULK
INSERT RIPAN
FROM 'C:\Documents and Settings\Admin\Desktop\ascii\X.TXT'
WITH
(
	FIELDTERMINATOR = ',',
	ROWTERMINATOR   = '\n'
)

//------------------------------------------------------------



DoCmd.TransferText acImportFixed, _
                        "Your import spec", _
                        "your table name", _
                        "temp.txt", _
                        True



Dim cmd As String      ' Holds DDEExecute command.
On Error GoTo DDEerr   ' Error trap.

' Make sure there isn't an existing DDE conversation:
text1.LinkMode = 0

' Set up the DDE link:
text1.LinkTopic = "MSACCESS|C:\ACCESS\NWIND.MDB"
text1.LinkItem = "All"
text1.LinkMode = 2

' DDEExecute command to import text file into Microsoft Access Table:
cmd = "[TransferText ,,'Shippers','C:\ACCESS\SHIPPERS.TXT']"

' Execute the TransferText command and close the DDE link:
text1.LinkExecute cmd
text1.LinkMode = 0
MsgBox "Transfer OK"
Exit Sub





INSERT INTO RIPAN (ICODE, INAME, RANK, PARENT_ID, TOP_ID, SELFCOLL, GRPCOLL, ASCVAL)
SELECT F1 AS EXPR1, F2 AS EXPR2, F3 AS EXPR3, F4 AS EXPR4, F5 AS EXPR5, F6 AS EXPR6, F7 AS EXPR7, F8 AS EXPR8
FROM [TEXT;DATABASE=D:;].X.TXT;



INSERT INTO RIPAN (ICODE, INAME, RANK, PARENT_ID, TOP_ID, SELFCOLL, GRPCOLL, ASCVAL)
SELECT C1 AS EXPR1, 
       C2 AS EXPR2, 
       C3 AS EXPR3, 
       C4 AS EXPR4, 
       C5 AS EXPR5, 
       C6 AS EXPR6, 
       C7 AS EXPR7, 
       C8 AS EXPR8
FROM [TEXT;DATABASE=C:\Documents and Settings\Admin\Desktop\ascii;].X.TXT;









COPY CSV into an Access Table

INSERT INTO TABLE1 (Fld1, fld2, fld3, fld4)  SELECT fld1, fld2, fld3,
fld4 from mycsv.csv

COPY XML into an Access Table

INSERT INTO TABLE1 (Fld1, fld2, fld3, fld4)  SELECT fld1, fld2, fld3,
fld4 from myxml.xml

Using OleDbCommand.executeNonQuery





Export excel sheet to SQL Database(Remote Server)

INSERT INTO TBL_EXCEL
SELECT *
FROM OPENROWSET(‘Microsoft.Jet.OLEDB.4.0′,
‘Excel 8.0;Database=\\pc31\C\Testexcel.xls’,
‘SELECT * FROM [Sheet1$]‘)





INSERT INTO TXT_STATE(STATE_ID, STATE_CODE, STATE_NAME, DEFAULT_FLAG, ASCVAL)
SELECT F1, F2, F3, F4, F5
FROM   [TEXT;HDR=NO;DATABASE=C:\Documents and Settings\Admin\Desktop\RIPAN\BST-EXP-20101029-104450;].[STATE.TXT];





INSERT INTO RIPAN (ICODE, INAME, RANK, PARENT_ID, TOP_ID, SELFCOLL, GRPCOLL, ASCVAL) SELECT C1 AS EXPR1,       C2 AS EXPR2,       C3 AS EXPR3,       C4 AS EXPR4,       C5 AS EXPR5,       C6 AS EXPR6,       C7 AS EXPR7,       C8 AS EXPR8 FROM  [TEXT;DATABASE=C:\Documents and Settings\Admin\Desktop\ascii;].RIPAN PAUL.TXT;
INSERT INTO RIPAN (ICODE, INAME, RANK, PARENT_ID, TOP_ID, SELFCOLL, GRPCOLL, ASCVAL) SELECT C1 AS EXPR1,       C2 AS EXPR2,       C3 AS EXPR3,       C4 AS EXPR4,       C5 AS EXPR5,       C6 AS EXPR6,       C7 AS EXPR7,       C8 AS EXPR8 FROM  [TEXT;DATABASE=C:\Documents and Settings\Admin\Desktop\ascii;].X.TXT;







