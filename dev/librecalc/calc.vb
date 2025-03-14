REM  *****  BASIC  *****

Sub Main
	Dim sFilePath As String
	dim lCol as long
	dim lRow as long
	GetActiveCell(lCol,lRow)
	'	MsgBox "Zeile: " & lRow & " Spalte: " & lCol
	
	sFilePath = GetFilePathFunction("ods")
	If sFilePath = "" Then
		MsgBox "keine Datei ausgewählt " & sFilePath
		Else
	MsgBox "Ausgewählte Datei: " & sFilePath
	' param: path, sourceSheetname,targetsheetname
	ReadDataFromAnotherFileFlexible(sFilePath ,"Seite1", "Getränkeverkauf",lRow,lCol)
	End If
	
	
	
End Sub
'#######

Function GetFilePathFunction(ByVal sExt As String) As String
    Dim oFilePicker As Object
    Dim sFilePath As String
    Dim aFilterNames(0) As String
    Dim aFilterExtensions(0) As String
    Dim error_s As String
    
    On Error GoTo ErrorHandler ' Fehlerbehandlung aktivieren

    ' Überprüfen, ob das Argument leer ist
    If IsMissing(sExt) OR sExt = ""  Then
        Err.Raise 1005
    End If

    ' FilePicker-Dienst erstellen
    oFilePicker = CreateUnoService("com.sun.star.ui.dialogs.FilePicker")
    
    ' Dialogtyp setzen (Öffnen eines Files)
    oFilePicker.setTitle("Datei auswählen")

    ' Filter basierend auf der übergebenen Dateiendung festlegen
    If sExt <> "" Then
        aFilterNames(0) = "Dateien (*." & sExt & ")"
        aFilterExtensions(0) = "*." & sExt
    End If

    ' Filter hinzufügen
    oFilePicker.appendFilter(aFilterNames(0), aFilterExtensions(0))

    ' Dialog anzeigen
    If oFilePicker.execute() = com.sun.star.ui.dialogs.ExecutableDialogResults.OK Then
        ' Ausgewählten Pfad abrufen
        sFilePath = oFilePicker.getFiles()(0)
        GetFilePathFunction = sFilePath
    Else
        GetFilePathFunction = ""
    End If

    Exit Function

ErrorHandler:
		error_s = error_s & "Error in GetFilePathFunction at line " & Erl() &_
		" : " & Error() & CHR$(10)
		
		Msgbox error_s
		

End Function


'###################################
REM  *****  BASIC  *****

Sub ReadDataFromAnotherFileFlexible(ByVal sSourceFilePath As String, ByVal sSourceSheetName As String, ByVal sTargetSheetName As String,ByVal lActiveRow as Long,ByVal lActiveCol As Long)
    Dim oDoc As Object
    Dim oSourceDoc As Object
    Dim oSourceSheet As Object
    Dim oTargetSheet As Object
    Dim oSourceCell As Object
    Dim oTargetCell As Object
    Dim iRow As Integer
    Dim iCol As Integer
    'Dim sourceFilePath As String
    'Dim sSourceSheetName As String
    'Dim targetSheetName As String
    Dim fileNameFromCell As String
    Dim oTargetCellForFileName As Object

    ' Aktuelles Dokument abrufen
    oDoc = ThisComponent
    On Error GoTo ErrorHandler ' Fehlerbehandlung aktivieren


    If IsMissing(stargetSheetName) OR stargetSheetName = ""  Then
        Err.Raise 1005
    End If
    ' Ziel-Tabelle und Zelle für den Dateinamen abrufen
    'sTargetSheetName = "Tabelle1" ' Name der Tabelle, die die Quelldatei angibt
    oTargetSheet = oDoc.Sheets.getByName(sTargetSheetName)
    oTargetCellForFileName = oTargetSheet.getCellByPosition(0, 0) ' Zelle A1

    ' Quelldateiname aus der Zelle lesen
    fileNameFromCell = oTargetCellForFileName.String

    If IsMissing(sSourceFilePath) OR sSourceFilePath = ""  Then
        Err.Raise 1005
    End If

    ' Prüfen, ob der Dateiname in der Zelle vorhanden ist
    'If fileNameFromCell = "" Then
        ' Interaktive Abfrage, wenn kein Dateiname in der Zelle steht
     '   sSourceFilePath = InputBox("Geben Sie den vollständigen Pfad zur Quelldatei ein:", "Quelldatei auswählen")
        'If sSourceFilePath = "" Then
         '   MsgBox "Kein Dateiname angegeben. Vorgang abgebrochen.", 16, "Abbruch"
          '  Exit Sub
        'End If
    'Else
        ' Pfad aus der Zelle verwenden
       ' sSourceFilePath = ConvertToURL(fileNameFromCell)
    'End If
     If IsMissing(sSourceFilePath) OR sSourceFilePath = ""  Then
        Err.Raise 1005
        Else
       ' sSourceFilePath = ConvertToURL(fileNameFromCell)
    End If
    If IsMissing(sSourceSheetName) OR sSourceSheetName = ""  Then
        Err.Raise 1005
    End If
    ' Name des Blatts in der Quelldatei
    'sSourceSheetName = "Sheet1" ' Anpassen, falls nötig

    ' Ziel-Tabelle abrufen
    oTargetSheet = oDoc.Sheets.getByName(sTargetSheetName)

    ' Quell-Dokument öffnen
 
    oSourceDoc = StarDesktop.loadComponentFromURL(sSourceFilePath, "_blank", 0, Array())
    oSourceSheet = oSourceDoc.Sheets.getByName(sSourceSheetName)

    ' Datenbereich festlegen (z.B. Zeilen 0-9 und Spalten 0-4)
   For iRow = 0 To 1 ' Zeilen 0 bis 9
       ' For iCol = 0 To 4 ' Spalten 0 bis 4
            ' Zellen auslesen
            oSourceCell = oSourceSheet.getCellByPosition(3, iRow)
            oTargetCell = oTargetSheet.getCellByPosition(lActiveCol+iRow, lActiveRow)
            ' Wert kopieren
            oTargetCell.String = oSourceCell.String
       ' Next iCol
    Next iRow
      For iRow = 0 To 1 ' Zeilen 0 bis 9
       ' For iCol = 0 To 4 ' Spalten 0 bis 4
            ' Zellen auslesen Umsatz
            oSourceCell = oSourceSheet.getCellByPosition(15, 36+iRow)
            oTargetCell = oTargetSheet.getCellByPosition(lActiveCol+3+iRow, lActiveRow)
            ' Wert kopieren
            oTargetCell.Value = oSourceCell.Value
       ' Next iCol
    Next iRow
    ' Zellen auslesen Start
            oSourceCell = oSourceSheet.getCellByPosition(15, 18)
            oTargetCell = oTargetSheet.getCellByPosition(lActiveCol+2, lActiveRow)
             oTargetCell.Value = oSourceCell.Value
    ' Zellen auslesen Endstand
            oSourceCell = oSourceSheet.getCellByPosition(20, 35)
            oTargetCell = oTargetSheet.getCellByPosition(lActiveCol+10, lActiveRow)
              oTargetCell.Value = oSourceCell.Value
' Zellen auslesen Bank
            oSourceCell = oSourceSheet.getCellByPosition(20, 34)
            oTargetCell = oTargetSheet.getCellByPosition(lActiveCol+7, lActiveRow)
              oTargetCell.Value = oSourceCell.Value
   ' Zellen auslesen Sumup
            oSourceCell = oSourceSheet.getCellByPosition(20, 30)
            oTargetCell = oTargetSheet.getCellByPosition(lActiveCol+6, lActiveRow)
             oTargetCell.Value = oSourceCell.Value

    ' Quell-Dokument schließen
    oSourceDoc.close(True)

    MsgBox "Daten aus der Datei wurden erfolgreich importiert!"
    Exit Sub


    ErrorHandler:
		error_s = error_s & "Error in ReadDataFromAnotherFile at line " & Erl() &_
		" : " & Error() & CHR$(10)
		
		Msgbox error_s
End Sub


'###################################
Sub GetActiveCell(lCol as Long,  lRow as Long)
    Dim oDoc As Object
    Dim oSheet As Object
    Dim oCell As Object
    Dim sAddress As String
    'Dim lCol as Long
    'Dim lRow as Long
    
    ' Aktuelles Dokument und Auswahl abrufen
    oDoc = ThisComponent
    oCell = oDoc.CurrentSelection

    ' Überprüfen, ob die Auswahl eine einzelne Zelle ist
    If oCell.supportsService("com.sun.star.sheet.SheetCell") Then
        ' Adresse der aktiven Zelle abrufen
        lCol= oCell.CellAddress.Column
        lRow = oCell.CellAddress.Row
    Else
        MsgBox "Bitte wähle eine einzelne Zelle aus."
    End If
End Sub