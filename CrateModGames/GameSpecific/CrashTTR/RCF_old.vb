Public Class RCF
    Public Structure RCF_HEADER
        Dim signature As String
        Dim Flags As UInt32
        Dim T1Offset As UInt32
        Dim T1Size As UInt32
        Dim T2Offset As UInt32
        Dim T2Size As UInt32
        Dim Gap1 As UInt32
        Dim Files As UInt32
        Dim T1File() As RCF_TABLE1
        Dim NamesAligment As UInt32
        Dim Gap2 As UInt32
        Dim T2File() As RCF_TABLE2
    End Structure
    Public Structure RCF_TABLE1
        Dim ID As UInt32
        Dim Offset As UInt32
        Dim Size As UInt32
        Dim Pos As UInt32
    End Structure
    Public Structure RCF_TABLE2
        Dim SomeShit1 As UInt32
        Dim Align As UInt32
        Dim Gap1 As UInt32
        Dim NameLen As UInt32
        Dim Name As String
        Dim Gap2 As UInt32
        Dim Ref As UInt32
        Dim External As String
    End Structure

    Private RCF_Path As String
    Public Header As RCF_HEADER
    Public Function OpenRCF(Path As String)
        If Not IO.File.Exists(Path) Then
            Return 1
        End If
        RCF_Path = Path
        Dim RCF As New IO.FileStream(RCF_Path, IO.FileMode.Open, IO.FileAccess.Read)
        Dim RCFReader As New IO.BinaryReader(RCF)
        Header.signature = ""
        For i As Int32 = 1 To 32
            Dim ch As Char = RCFReader.ReadChar
            If Not ch = Chr(0) Then
                Header.signature += ch
            Else
                Exit For
            End If
        Next i
        RCF.Position = 32
        Header.Flags = RCFReader.ReadUInt32
        Header.T1Offset = RCFReader.ReadUInt32
        Header.T1Size = RCFReader.ReadUInt32
        Header.T2Offset = RCFReader.ReadUInt32
        Header.T2Size = RCFReader.ReadUInt32
        Header.Gap1 = RCFReader.ReadUInt32
        Header.Files = RCFReader.ReadUInt32
        Array.Resize(Header.T1File, Header.Files)
        Array.Resize(Header.T2File, Header.Files)
        RCF.Position = Header.T1Offset
        For i As Int32 = 0 To Header.Files - 1
            With Header.T1File(i)
                .ID = RCFReader.ReadUInt32
                .Offset = RCFReader.ReadUInt32
                .Size = RCFReader.ReadUInt32
            End With
        Next i
        RCF.Position = Header.T2Offset
        Header.NamesAligment = RCFReader.ReadUInt32
        Header.Gap2 = RCFReader.ReadUInt32
        For i As Int32 = 0 To Header.Files - 1
            With Header.T2File(i)
                .SomeShit1 = RCFReader.ReadUInt32
                .Align = RCFReader.ReadUInt32
                .Gap1 = RCFReader.ReadUInt32
                .NameLen = RCFReader.ReadUInt32
                .Name = RCFReader.ReadChars(.NameLen - 1)
                .Gap2 = RCFReader.ReadInt32
                .External = ""
            End With
        Next i
        Dim n As Int32 = 1
        While n < Header.Files
            If Header.T1File(n).Offset < Header.T1File(n - 1).Offset Then
                Dim tmp As RCF_TABLE1 = Header.T1File(n)
                Header.T1File(n) = Header.T1File(n - 1)
                Header.T1File(n - 1) = tmp
                If n > 1 Then
                    n -= 2
                End If
            End If
            n += 1
        End While
        For i As Int32 = 0 To Header.Files - 1
            Header.T1File(i).Pos = i
        Next i
        n = 1
        While n < Header.Files
            If Header.T1File(n).ID < Header.T1File(n - 1).ID Then
                Dim tmp As RCF_TABLE1 = Header.T1File(n)
                Header.T1File(n) = Header.T1File(n - 1)
                Header.T1File(n - 1) = tmp
                If n > 1 Then
                    n -= 2
                End If
            End If
            n += 1
        End While
        For i As Int32 = 0 To Header.Files - 1
            For j As Int32 = 0 To Header.Files - 1
                If i = Header.T1File(j).Pos Then
                    Header.T2File(i).Ref = j
                End If
            Next j
        Next i
        RCFReader.Close()
        RCF.Close()
        Return 0
    End Function
    ''' <summary>
    ''' Uses T2 index
    ''' </summary>
    Public Function ExtractItem(ind As UInt32, Path As String)
        If Not IO.File.Exists(RCF_Path) Then
            Return 1
        End If
        If Not Path(Path.Length - 1) = "\" Then
            Path += "\"
        End If
        Dim RCF As New IO.FileStream(RCF_Path, IO.FileMode.Open, IO.FileAccess.Read)
        Dim RCFReader As New IO.BinaryReader(RCF)
        Dim Folders() As String = Header.T2File(ind).Name.Split("\")
        Dim File As New IO.FileStream(Path + Folders(Folders.Length - 1), IO.FileMode.Create, IO.FileAccess.Write)
        Dim FileWriter As New IO.BinaryWriter(File)
        RCF.Position = Header.T1File(Header.T2File(ind).Ref).Offset
        FileWriter.Write(RCFReader.ReadBytes(Header.T1File(Header.T2File(ind).Ref).Size))
        FileWriter.Close()
        File.Close()
        RCFReader.Close()
        RCF.Close()
        Return 0
    End Function
    Public Function ExtractRCF(ByRef feedback As String, Path As String)
        If Not IO.File.Exists(RCF_Path) Then
            Return 1
        End If
        If Not Path(Path.Length - 1) = "\" Then
            Path += "\"
        End If
        Dim RCF As New IO.FileStream(RCF_Path, IO.FileMode.Open, IO.FileAccess.Read)
        Dim RCFReader As New IO.BinaryReader(RCF)
        Dim BaseStr As String = feedback
        For i As Int32 = 0 To Header.Files - 1
            Dim checked As String = Path
            Dim Folders() As String = Header.T2File(i).Name.Split("\")
            For j As Int32 = 0 To Folders.Length - 2
                checked += Folders(j) + "\"
                If Not IO.Directory.Exists(checked) Then IO.Directory.CreateDirectory(checked)
            Next j
            checked += Folders(Folders.Length - 1)
            feedback = BaseStr + " " + Folders(Folders.Length - 1) + " " + (i + 1).ToString + "\" + Header.Files.ToString
            Dim File As New IO.FileStream(checked, IO.FileMode.Create, IO.FileAccess.Write)
            Dim FileWriter As New IO.BinaryWriter(File)
            RCF.Position = Header.T1File(Header.T2File(i).Ref).Offset
            FileWriter.Write(RCFReader.ReadBytes(Header.T1File(Header.T2File(i).Ref).Size))
            FileWriter.Close()
            File.Close()
        Next i
        RCFReader.Close()
        RCF.Close()
        feedback = BaseStr
        Return 0
    End Function
    ''' <summary>
    ''' Uses T1 index
    ''' </summary>
    Public Overloads Function GetStream(ind As UInt32, ByRef MS As IO.MemoryStream)
        MS = New IO.MemoryStream(Header.T1File(ind).Size)
        Dim RCF As New IO.FileStream(RCF_Path, IO.FileMode.Open, IO.FileAccess.Read)
        Dim RCFReader As New IO.BinaryReader(RCF)
        RCF.Position = Header.T1File(ind).Offset
        Dim MSW As New IO.BinaryWriter(MS)
        MSW.Write(RCFReader.ReadBytes(Header.T1File(ind).Size))
        RCFReader.Close()
        RCF.Close()
        Return 0
    End Function
    Public Overloads Function GetStream(ind As UInt32, ByRef MS As IO.FileStream)
        Dim RCF As New IO.FileStream(RCF_Path, IO.FileMode.Open, IO.FileAccess.Read)
        Dim RCFReader As New IO.BinaryReader(RCF)
        RCF.Position = Header.T1File(ind).Offset
        Dim MSW As New IO.BinaryWriter(MS)
        MSW.Write(RCFReader.ReadBytes(Header.T1File(ind).Size))
        RCFReader.Close()
        RCF.Close()
        Return 0
    End Function
    Public Function Pack(NewPath As String, ByRef feedback As String, ByVal Optional Alignment As UInt32 = 2048)
        If NewPath = RCF_Path Then
            Return 1
        End If
        Dim NRCF As New IO.FileStream(NewPath, IO.FileMode.Create, IO.FileAccess.Write)
        Dim NRCFWriter As New IO.BinaryWriter(NRCF)
        Dim BaseStr As String = feedback
        feedback = BaseStr + " Recalculating..."
        Recalculate(Alignment)
        For i As Int32 = 0 To 31
            If i < Header.signature.Length Then
                NRCFWriter.Write(Header.signature(i))
            Else
                NRCFWriter.Write(CByte(0))
            End If
        Next i
        feedback = BaseStr + " Writing header..."
        NRCFWriter.Write(Header.Flags)
        NRCFWriter.Write(Header.T1Offset)
        NRCFWriter.Write(Header.T1Size)
        NRCFWriter.Write(Header.T2Offset)
        NRCFWriter.Write(Header.T2Size)
        NRCFWriter.Write(Header.Gap1)
        NRCFWriter.Write(Header.Files)
        NRCF.Position = Header.T1Offset
        For i As Int32 = 0 To Header.Files - 1
            NRCFWriter.Write(Header.T1File(i).ID)
            NRCFWriter.Write(Header.T1File(i).Offset)
            NRCFWriter.Write(Header.T1File(i).Size)
        Next i
        NRCF.Position = Header.T2Offset
        NRCFWriter.Write(Header.NamesAligment)
        NRCFWriter.Write(Header.Gap2)
        For i As Int32 = 0 To Header.Files - 1
            NRCFWriter.Write(Header.T2File(i).SomeShit1)
            NRCFWriter.Write(Header.T2File(i).Align)
            NRCFWriter.Write(Header.T2File(i).Gap1)
            NRCFWriter.Write(Header.T2File(i).NameLen)
            For j As Int32 = 0 To Header.T2File(i).NameLen - 2
                NRCFWriter.Write(CChar(Header.T2File(i).Name(j)))
            Next j
            NRCFWriter.Write(Header.T2File(i).Gap2)
        Next i
        Dim ORCF As New RCF
        ORCF.OpenRCF(RCF_Path)
        For i As Int32 = 0 To Header.Files - 1
            feedback = BaseStr + " " + Header.T2File(Header.T1File(i).Pos).Name + " " + (i + 1).ToString + "\" + Header.Files.ToString
            NRCF.Position = Header.T1File(i).Offset
            If Header.T2File(Header.T1File(i).Pos).External = "" Then
                ORCF.GetStream(i, NRCF)
            Else
                NRCFWriter.Write(IO.File.ReadAllBytes(Header.T2File(Header.T1File(i).Pos).External))
            End If
        Next i
        NRCFWriter.Close()
        NRCF.Close()
        feedback = BaseStr
        Return 0
    End Function
    Public Function Recalculate(Optional Alignment As UInt32 = 2048)
        Header.NamesAligment = Alignment
        Header.T1Offset = 60
        Header.T1Size = Header.Files * 12
        Header.T2Offset = ((Header.T1Offset + Header.T1Size - 1) \ Alignment + 1) * Alignment
        Header.T2Size = 8 + Header.Files * 20
        For i As Int32 = 0 To Header.Files - 1
            Header.T2Size += Header.T2File(i).NameLen - 1
        Next i
        Dim offset As UInt32 = Header.T2Offset
        Dim size As UInt32 = Header.T2Size
        For i As Int32 = 0 To Header.Files - 1
            Dim ind As UInt32 = Header.T2File(i).Ref
            Header.T1File(ind).Offset = ((offset + size - 1) \ Alignment + 1) * Alignment
            If Not Header.T2File(i).External = "" Then
                Dim FI As New IO.FileInfo(Header.T2File(i).External)
                Header.T1File(ind).Size = FI.Length
            End If
            offset = Header.T1File(ind).Offset
            size = Header.T1File(ind).Size
        Next i
        Return 0
    End Function
End Class
