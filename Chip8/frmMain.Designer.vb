<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdLoad = New System.Windows.Forms.Button
        Me.cmbRom = New System.Windows.Forms.ComboBox
        Me.mnuMenu = New System.Windows.Forms.MenuStrip
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuFileExit = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuROM = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRomOpen = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRomClose = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRomSavestate = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRomLoadstate = New System.Windows.Forms.ToolStripMenuItem
        Me.ofd = New System.Windows.Forms.OpenFileDialog
        Me.graphicsDevice = New System.Windows.Forms.PictureBox
        Me.mnuRomRun = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuMenu.SuspendLayout()
        CType(Me.graphicsDevice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdLoad
        '
        Me.cmdLoad.Location = New System.Drawing.Point(134, 122)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(75, 23)
        Me.cmdLoad.TabIndex = 0
        Me.cmdLoad.Text = "Load ROM"
        Me.cmdLoad.UseVisualStyleBackColor = True
        Me.cmdLoad.Visible = False
        '
        'cmbRom
        '
        Me.cmbRom.FormattingEnabled = True
        Me.cmbRom.Location = New System.Drawing.Point(114, 84)
        Me.cmbRom.Name = "cmbRom"
        Me.cmbRom.Size = New System.Drawing.Size(121, 21)
        Me.cmbRom.TabIndex = 1
        '
        'mnuMenu
        '
        Me.mnuMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuROM})
        Me.mnuMenu.Location = New System.Drawing.Point(0, 0)
        Me.mnuMenu.Name = "mnuMenu"
        Me.mnuMenu.Size = New System.Drawing.Size(664, 24)
        Me.mnuMenu.TabIndex = 2
        Me.mnuMenu.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFileExit})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(35, 20)
        Me.mnuFile.Text = "File"
        '
        'mnuFileExit
        '
        Me.mnuFileExit.Name = "mnuFileExit"
        Me.mnuFileExit.Size = New System.Drawing.Size(103, 22)
        Me.mnuFileExit.Text = "Exit"
        '
        'mnuROM
        '
        Me.mnuROM.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRomOpen, Me.mnuRomRun, Me.mnuRomClose, Me.ToolStripSeparator1, Me.mnuRomSavestate, Me.mnuRomLoadstate})
        Me.mnuROM.Name = "mnuROM"
        Me.mnuROM.Size = New System.Drawing.Size(42, 20)
        Me.mnuROM.Text = "ROM"
        '
        'mnuRomOpen
        '
        Me.mnuRomOpen.Name = "mnuRomOpen"
        Me.mnuRomOpen.Size = New System.Drawing.Size(152, 22)
        Me.mnuRomOpen.Text = "Open"
        '
        'mnuRomClose
        '
        Me.mnuRomClose.Name = "mnuRomClose"
        Me.mnuRomClose.Size = New System.Drawing.Size(152, 22)
        Me.mnuRomClose.Text = "Close"
        '
        'mnuRomSavestate
        '
        Me.mnuRomSavestate.Name = "mnuRomSavestate"
        Me.mnuRomSavestate.Size = New System.Drawing.Size(152, 22)
        Me.mnuRomSavestate.Text = "Save State"
        '
        'mnuRomLoadstate
        '
        Me.mnuRomLoadstate.Name = "mnuRomLoadstate"
        Me.mnuRomLoadstate.Size = New System.Drawing.Size(152, 22)
        Me.mnuRomLoadstate.Text = "Load State"
        '
        'ofd
        '
        Me.ofd.FileName = "OpenFileDialog1"
        '
        'graphicsDevice
        '
        Me.graphicsDevice.BackColor = System.Drawing.SystemColors.Control
        Me.graphicsDevice.InitialImage = Nothing
        Me.graphicsDevice.Location = New System.Drawing.Point(12, 27)
        Me.graphicsDevice.Name = "graphicsDevice"
        Me.graphicsDevice.Size = New System.Drawing.Size(640, 320)
        Me.graphicsDevice.TabIndex = 3
        Me.graphicsDevice.TabStop = False
        '
        'mnuRomRun
        '
        Me.mnuRomRun.Name = "mnuRomRun"
        Me.mnuRomRun.Size = New System.Drawing.Size(152, 22)
        Me.mnuRomRun.Text = "Run"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(149, 6)
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(664, 354)
        Me.Controls.Add(Me.graphicsDevice)
        Me.Controls.Add(Me.cmbRom)
        Me.Controls.Add(Me.cmdLoad)
        Me.Controls.Add(Me.mnuMenu)
        Me.MainMenuStrip = Me.mnuMenu
        Me.Name = "frmMain"
        Me.Text = "Chip8"
        Me.TopMost = True
        Me.mnuMenu.ResumeLayout(False)
        Me.mnuMenu.PerformLayout()
        CType(Me.graphicsDevice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdLoad As System.Windows.Forms.Button
    Friend WithEvents cmbRom As System.Windows.Forms.ComboBox
    Friend WithEvents mnuMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuROM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRomOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRomClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRomSavestate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRomLoadstate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents graphicsDevice As System.Windows.Forms.PictureBox
    Friend WithEvents mnuRomRun As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator

End Class
