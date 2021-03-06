'Option Strict On
Imports system.IO
'Commented out code was for debugging purposes

Public Class frmMain

#Region "Declarations"
    'Registers
    Dim V(16 - 1) As Byte

    'Memory
    Dim memory(4096 - 1) As Byte

    'Stack, stack pointer
    Dim stack(16 - 1) As UInt16
    Dim sp As UInt16 = 0

    'Timers, should count down at 60HZ
    Dim timerDelay As Int32
    Dim timerSound As Int32

    'Keys for input
    Dim key(16 - 1) As Boolean
    Dim keyMap() As Keys = { _
    Keys.D1, Keys.D2, Keys.D3, Keys.D4, _
    Keys.Q, Keys.W, Keys.E, Keys.R, _
    Keys.A, Keys.S, Keys.D, Keys.F, _
    Keys.Z, Keys.X, Keys.C, Keys.V _
    }

    'Current opcode
    Dim opcode As UInt32 = 0

    'Address register
    Dim I As UInt32 = 0

    'Program counter
    Dim pc As UInt16 = &H200

    'Screen stuff
    Dim screenMultiplier As Int16 = 4
    Dim screen(64 * 32 - 1) As Boolean '????
    Dim bitmap As New Bitmap(64 * screenMultiplier, 32 * screenMultiplier)
    Dim screenG As Graphics = Me.CreateGraphics
    Dim g As Graphics
    Dim bmpG As Graphics = Graphics.FromImage(bitmap)
    Dim pixel As New Rectangle(0, 0, screenMultiplier, screenMultiplier)
    Dim draw As Boolean
    Dim colorFore As Color = Color.Black
    Dim colorBack As Color = Color.White

    'Error handling
    Dim maxPc As UInt16

    'Character map
    '0-F, in 5 rows of on/off
    'Example:
    '0xF0, 0x90, 0x90, 0x90, 0xF0:
    '0xF0 = 1111 0000
    '0x90 = 1001 0000
    'discard last 4 nibbles:
    '0xF = 1111
    '0x9 = 1001
    '0x9 = 1001
    '0x9 = 1001
    '0xF = 1111
    'and you get a 0
    Dim fontSet() As Byte = _
{ _
 &HF0, &H90, &H90, &H90, &HF0, _
 &H20, &H60, &H20, &H20, &H70, _
 &HF0, &H10, &HF0, &H80, &HF0, _
 &HF0, &H10, &HF0, &H10, &HF0, _
 &H90, &H90, &HF0, &H10, &H10, _
 &HF0, &H80, &HF0, &H10, &HF0, _
 &HF0, &H80, &HF0, &H90, &HF0, _
 &HF0, &H10, &H20, &H40, &H40, _
 &HF0, &H90, &HF0, &H90, &HF0, _
 &HF0, &H90, &HF0, &H10, &HF0, _
 &HF0, &H90, &HF0, &H90, &H90, _
 &HE0, &H90, &HE0, &H90, &HE0, _
 &HF0, &H80, &H80, &H80, &HF0, _
 &HE0, &H90, &H90, &H90, &HE0, _
 &HF0, &H80, &HF0, &H80, &HF0, _
 &HF0, &H80, &HF0, &H80, &H80 _
 }



#End Region

    Private Sub DrawPixel(ByVal x As Integer, ByVal y As Integer)
        If x > 63 Or y > 31 Then
            Exit Sub
        Else
            If screenMultiplier = 1 Then
                bitmap.SetPixel(x, y, colorFore)
                screenG.DrawImage(bitmap, 0, 0)
            Else
                Dim i, j As Int16
                For i = 0 To screenMultiplier - 1
                    For j = 0 To screenMultiplier - 1
                        bitmap.SetPixel(x * screenMultiplier + i, y * screenMultiplier + j, colorFore)

                    Next j
                Next i
                screenG.DrawImage(bitmap, 0, 0)

            End If
        End If
    End Sub 'Good for now...

    Private Sub LoadResourceMemory()
        For pc = 0 To fontSet.Length - 1
            memory(pc) = fontSet(pc)
        Next
        For pc = 0 To My.Resources.UFO.Length - 1
            memory(pc + &H200) = My.Resources.UFO(pc)
            Console.WriteLine(memory(pc + &H200) & ", " & (pc + &H200))
        Next
        maxPc = pc + &H200
    End Sub 'Complete, Use this one

    Private Sub LoadMemory()
        Reset()
        For pc = 0 To fontSet.Length - 1
            memory(pc) = fontSet(pc)
        Next pc
        Using reader As New BinaryReader(File.Open("C:\Chip8\GAMES\UFO", FileMode.Open))
            For pc = 0 To reader.BaseStream.Length - 1
                memory(pc + &H200) = reader.ReadByte()
                ''console.writeLine(memory(pc + &H200) & ", " & (pc + &H200))
            Next
            maxPc = pc + &H200
            'pc = &H200
        End Using
    End Sub 'COMPLETE, Obsolete

    Private Sub LoadFileMemory(ByVal filename As String)
        Reset()
        Dim loadedProgramBytes() As Byte
        Try
            loadedProgramBytes = File.ReadAllBytes(filename)
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
            loadedProgramBytes = Nothing
            Exit Sub
        End Try

        If Not (loadedProgramBytes Is Nothing) Then
            For pc = 0 To loadedProgramBytes.Length - 1
                memory(pc + &H200) = loadedProgramBytes(pc)
            Next
            maxPc = pc + &H200
        End If
        'Emulate()

    End Sub

    Private Sub Reset()
        Dim j As Int32
        For j = 0 To V.Length - 1
            V(j) = 0
        Next
        For j = 0 To memory.Length - 1
            memory(j) = 0
        Next
        For j = 0 To stack.Length - 1
            stack(j) = 0
        Next
        sp = 0
        timerDelay = 0
        timerSound = 0
        For j = 0 To key.Length - 1
            key(j) = 0
        Next
        opcode = 0
        I = 0
        pc = &H200
        'For j = 0 To (64 * 32)
        'Screen(j) = 0
        'Next
        maxPc = 0
    End Sub 'COMPLETE

    Private Sub cmdLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoad.Click

        Randomize()

        Reset()

        'LoadMemory()
        LoadResourceMemory()

        pc = &H200


        cmdLoad.Visible = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Width = 64 * screenMultiplier
        Me.Height = 32 * screenMultiplier
        Me.BackColor = colorBack
        Me.Refresh()

        Me.KeyPreview = True

        Emulate()

        'lstDebug.Items.Add("All done")


    End Sub

    Private Sub UpdateDisplay()
        g.Clear(colorBack)
        g.DrawImage(GFXToBitmap, New Rectangle(0, 0, 320, 160))
        graphicsDevice.Invalidate()
    End Sub

    Private Function GFXToBitmap() As Bitmap
        Dim bmp As New Bitmap(64, 32)
        Dim y, x As Integer
        For y = 0 To 31
            For x = 0 To 63
                If screen((y * 64) + x) Then
                    bmp.SetPixel(x, y, colorFore)
                Else
                    bmp.SetPixel(x, y, colorBack)
                End If
            Next
        Next
        Return bmp
    End Function

    Private Sub Emulate()

        While pc < maxPc 'should always be true
            opcode = memory(pc)
            opcode <<= 8
            opcode = opcode Or memory(pc + 1)
            'lstDebug.Items.Add(opcode)
            OpcodeSwitch(opcode)
            If draw Then
                UpdateDisplay()
            End If
        End While
    End Sub

    Private Sub OpcodeSwitch(ByVal opcode As UInt16)
        Dim nibble(4 - 1) As Int32 'nibble of opcode, big endian
        nibble(0) = (opcode And &HF000) >> 12
        nibble(1) = (opcode And &HF00) >> 8
        nibble(2) = (opcode And &HF0) >> 4
        nibble(3) = opcode And &HF

        If nibble(0) = &H0 Then 'Multiple, FIX FOR BAD OPCODE
            If nibble(3) Then 'RET
                sp -= 1
                pc = stack(sp)
                Console.WriteLine()
                Console.WriteLine(opcode)
                Console.WriteLine("RET")
                pc += 2
            Else 'CLS
                Dim counter As Int16
                For counter = 0 To screen.Length - 1
                    screen(counter) = 0
                Next
                draw = 1
                Console.WriteLine()
                Console.WriteLine(opcode)
                Console.WriteLine("CLS")
                pc += 2
            End If
        ElseIf nibble(0) = &H1 Then 'JP addr 
            pc = opcode And &HFFF
            Console.WriteLine()
            Console.WriteLine(opcode)
            Console.WriteLine("JP " & (opcode And &HFFF))
        ElseIf nibble(0) = &H2 Then 'CALL addr
            stack(sp) = pc
            sp += 1
            pc = opcode And &HFFF
            Console.WriteLine()
            Console.WriteLine(opcode)
            Console.WriteLine("PC = " & pc)
            Console.WriteLine("CALL " & (opcode And &HFFF))
        ElseIf nibble(0) = &H3 Then 'SE Vx, byte
            If V(nibble(1)) = (opcode And &HFF) Then
                pc += 4
            Else
                pc += 2
            End If
            Console.WriteLine()
            Console.WriteLine(opcode)
            Console.WriteLine("SE V" & nibble(1) & ", " & (opcode And &HFF))
        ElseIf nibble(0) = &H4 Then 'SNE Vx, byte
            If V(nibble(1)) = (opcode And &HFF) Then
                pc += 2
            Else
                pc += 4
            End If
            Console.WriteLine()
            Console.WriteLine(opcode)
            Console.WriteLine("SNE V" & nibble(1) & ", " & (opcode And &HFF))
        ElseIf nibble(0) = &H5 Then 'SE Vx, Vy
            If V(nibble(1)) = V(nibble(2)) Then
                pc += 4
            Else
                pc += 2
            End If
            Console.WriteLine()
            Console.WriteLine(opcode)
            Console.WriteLine("CLS")
        ElseIf nibble(0) = &H6 Then 'LD Vx, byte
            V(nibble(1)) = opcode And &HFF
            pc += 2
            Console.WriteLine()
            Console.WriteLine(opcode)
            Console.WriteLine("LD " & nibble(1) & ", " & (opcode And &HFF))
        ElseIf nibble(0) = &H7 Then 'ADD Vx, byte
            V(nibble(1)) = (V(nibble(1)) + opcode And &HFF) Mod 2 ^ 8
            pc += 2
            Console.WriteLine()
            Console.WriteLine(opcode)
            Console.WriteLine("ADD " & nibble(1) & (opcode And &HFF))
        ElseIf nibble(0) = &H8 Then 'Multiple
            If nibble(3) = 0 Then 'LD Vx, Vy
                V(nibble(1)) = V(nibble(2))
                pc += 2
                Console.WriteLine()
                Console.WriteLine(opcode)
                Console.WriteLine("CLS")
            ElseIf nibble(3) = 1 Then 'OR Vx, Vy
                V(nibble(1)) = V(nibble(1)) Or V(nibble(2))
                pc += 2
                Console.WriteLine()
                Console.WriteLine(opcode)
                Console.WriteLine("CLS")
            ElseIf nibble(3) = 2 Then 'AND Vx, Vy
                V(nibble(1)) = V(nibble(1)) And V(nibble(2))
                pc += 2
                Console.WriteLine()
                Console.WriteLine(opcode)
                Console.WriteLine("CLS")
            ElseIf nibble(3) = 3 Then 'XOR Vx, Vy
                V(nibble(1)) = V(nibble(1)) Xor V(nibble(2))
                pc += 2
                Console.WriteLine()
                Console.WriteLine(opcode)
                Console.WriteLine("CLS")
            ElseIf nibble(3) = 4 Then 'ADC Vx, Vy
                If (V(nibble(2)) > (&HFF - V(nibble(1)))) Then
                    V(&HF) = 1
                Else
                    V(&HF) = 0
                End If
                V(nibble(1)) = (V(nibble(1)) + V(nibble(2))) Mod (2 ^ 8 - 1)
                pc += 2
                Console.WriteLine()
                Console.WriteLine(opcode)
                Console.WriteLine("CLS")
            ElseIf nibble(3) = 5 Then 'SBC Vx, Vy
                V(nibble(1)) -= V(nibble(2))
                If V(nibble(1)) > V(nibble(2)) Then
                    V(&HF) = 1
                Else
                    V(&HF) = 0
                End If
                pc += 2
                Console.WriteLine()
                Console.WriteLine(opcode)
                Console.WriteLine("CLS")
            ElseIf nibble(3) = 6 Then 'SHR
                V(&HF) = (V(nibble(1)) >> 8) And 1
                V(nibble(1)) >>= 1
                pc += 2
                Console.WriteLine()
                Console.WriteLine(opcode)
                Console.WriteLine("CLS")
            ElseIf nibble(3) = 7 Then 'SUBN
                V(nibble(1)) = V(nibble(2)) - V(nibble(1))
                If V(nibble(2)) > V(nibble(1)) Then
                    V(&HF) = 1
                Else
                    V(&HF) = 0
                End If
                pc += 2
                Console.WriteLine()
                Console.WriteLine(opcode)
                Console.WriteLine("CLS")
            ElseIf nibble(3) = &HE Then 'SHL
                V(&HF) = V(nibble(1)) And &H7F
                V(nibble(1)) <<= 1
                pc += 2
                Console.WriteLine()
                Console.WriteLine(opcode)
                Console.WriteLine("CLS")
            Else
                Console.WriteLine()
                Console.WriteLine(opcode)
                Console.WriteLine("Opcode Error: Undefined")
                Console.WriteLine("Memory:" & memory(pc))
                Console.WriteLine("PC:" & pc)
            End If
        ElseIf nibble(0) = &H9 Then 'SNE Vx, Vy
            If V(nibble(1)) = V(nibble(2)) Then
                pc += 2
            Else
                pc += 4
            End If
            Console.WriteLine()
            Console.WriteLine(opcode)
            Console.WriteLine("SNE " & nibble(1) & ", " & nibble(2))
        ElseIf nibble(0) = &HA Then 'LD I, addr
            I = opcode And &HFFF
            pc += 2
            Console.WriteLine()
            Console.WriteLine(opcode)
            Console.WriteLine("LD I, " & (opcode And &HFFF))
        ElseIf nibble(0) = &HB Then 'JP V0, addr
            pc = (opcode And &HFFF) + V(0)
            Console.WriteLine()
            Console.WriteLine(opcode)
            Console.WriteLine("JP V0, " & (opcode And &HFFF))
        ElseIf nibble(0) = &HC Then 'RND Vx, byte
            V(nibble(1)) = (Rnd() * &HFF) And (opcode And &HFF)
            pc += 2
            Console.WriteLine()
            Console.WriteLine(opcode)
            Console.WriteLine("RND " & nibble(1) & (opcode And &HFF))
        ElseIf nibble(0) = &HD Then 'DRW Vx, Vy, nibble
            Dim x, y, height, pixel As UShort
            x = nibble(1)
            y = nibble(2)
            height = nibble(3)
            V(&HF) = 0
            Dim k, j As Int16
            For j = 0 To height
                pixel = memory(I + j)
                For k = 0 To 8
                    If pixel And (&H80 >> k) Then
                        If (screen((x + k + ((y + j) * 64)))) Then
                            V(&HF) = 1
                        End If
                        screen(x + k + ((y + j) * 64)) = (screen(x + k + ((y + j) * 64)) Xor 1)
                    End If
                Next
            Next
            draw = 1
            pc += 2
            Console.WriteLine()
            Console.WriteLine(opcode)
            Console.WriteLine("DRW " & nibble(1) & ", " & nibble(2) & ", " & nibble(3))
        ElseIf nibble(0) = &HE Then 'Multiple
            If nibble(2) = 9 Then 'SKP
                If key(nibble(1)) <> 0 Then
                    pc += 4
                Else
                    pc += 2
                End If
            ElseIf nibble(2) = &HA Then 'SKNP
                If key(nibble(1)) <> 0 Then
                    pc += 2
                Else
                    pc += 4
                End If
            End If
            Console.WriteLine()
            Console.WriteLine(opcode)
            Console.WriteLine("CLS")
        ElseIf nibble(0) = &HF Then 'Multiple
            If nibble(2) = 0 Then
                If nibble(3) = 7 Then 'LD Vx, DT
                    V(nibble(1)) = timerDelay
                    pc += 2
                ElseIf nibble(3) = &HA Then 'LD Vx, k
                    Dim break As Boolean
                    Dim counter As Int16
                    While Not break
                        For counter = 0 To key.Length - 1
                            If key(counter) Then
                                break = True
                                V(nibble(1)) = counter
                                Exit For
                            End If
                        Next
                    End While
                End If
                pc += 2
                Console.WriteLine()
                Console.WriteLine(opcode)
                Console.WriteLine("CLS")
            ElseIf nibble(2) = 1 Then
                If nibble(3) = 5 Then 'LD DT, Vx
                    timerDelay = V(nibble(1))
                    pc += 2
                    Console.WriteLine()
                    Console.WriteLine(opcode)
                    Console.WriteLine("CLS")
                ElseIf nibble(3) = 8 Then 'LD ST, Vx
                    timerSound = V(nibble(1))
                    pc += 2
                    Console.WriteLine()
                    Console.WriteLine(opcode)
                    Console.WriteLine("CLS")
                ElseIf nibble(3) = &HE Then 'ADD I, Vx
                    If I + V(nibble(1)) > &HFFF Then
                        V(&HF) = 1
                    Else
                        V(&HF) = 0
                    End If
                    I += V(nibble(1))
                    pc += 2
                End If
                Console.WriteLine()
                Console.WriteLine(opcode)
                Console.WriteLine("CLS")
            ElseIf nibble(2) = 2 Then 'LD F, Vx
                I = V(nibble(1)) * 5
                pc += 2
            ElseIf nibble(2) = 3 Then 'LD B, Vx
                memory(I) = V(nibble(1)) / 100
                memory(I + 1) = (V(nibble(1)) / 10) Mod 10
                memory(I + 2) = (V(nibble(1)) Mod 100) Mod 10
                pc += 2
            ElseIf nibble(3) = 5 Then
                If nibble(2) = 5 Then 'LD [I], Vx
                    Dim counter As Int16
                    For counter = 0 To nibble(1)
                        memory(I + counter) = V(counter)
                    Next
                    I += (nibble(1) + 1)
                    pc += 2
                ElseIf nibble(2) = 6 Then 'LD Vx, [I]
                    Dim counter As Int16
                    For counter = 0 To nibble(1)
                        V(counter) = memory(I + counter)
                    Next
                    I += (nibble(1) + 1)
                    pc += 2
                End If
            End If
        Else
            Console.WriteLine()
            Console.WriteLine(opcode)
            Console.WriteLine("Opcode Error: Undefined")
        End If
        If (timerDelay > 0) Then
            timerDelay -= 1
        End If
        If (timerSound > 0) Then
            'If timerSound = 1 Then
            Console.WriteLine("Beep")
            'End If
            timerSound -= 1
        End If
        'lstDebug.Items.Add(nibble(0))
    End Sub 'COMPLETE, needs comments, major debugging

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim counter As Int16
        Dim currentKey As Keys = e.KeyData
        For counter = 0 To &HF
            If currentKey = keyMap(counter) Then
                key(counter) = 1
                'Me.Text = (currentKey)
                Exit Sub
            Else
                key(counter) = 0
            End If
        Next counter
    End Sub 'Sets keypress flags, COMPLETE

    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        Dim counter As Int16
        For counter = 0 To &HF
            key(counter) = 0
        Next
    End Sub 'Releases keypress flags, COMPLETE

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmbRom.Visible = False
        'Dim romList() As String = { _
        '"15Puzzle", "Blinky", "Blitz", "Breakout", _
        '"Brix", "Connect4", "Guess", "Hidden", _
        '"Invaders", "Kaleid", "Maze", "Merlin", _
        '"Missile", "Pong", "Pong2", "Puzzle", _
        '"Squash", "Syzygy", "Tank", "Tetris", _
        '"Tetris 2", "TicTac", "UFO", "Vbrix", _
        '"Vers", "Wall", "Wipeoff" _
        '}
        'cmbRom.Items.AddRange(romList)
        If graphicsDevice.Image Is Nothing Then
            graphicsDevice.Image = New Bitmap(graphicsDevice.Width, graphicsDevice.Height)
        End If
        g = Graphics.FromImage(graphicsDevice.Image)
    End Sub

    Private Sub mnuRomOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRomOpen.Click
        ofd.Title = "Choose a ROM..."
        ofd.InitialDirectory = "E:\GAMES"

        ofd.ShowDialog()
    End Sub

    Private Sub ofd_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ofd.FileOk
        LoadFileMemory(sender.filename)
    End Sub

    Private Sub mnuFileExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileExit.Click
        End
    End Sub

    Private Sub mnuRomRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRomRun.Click
        Emulate()
    End Sub
End Class
