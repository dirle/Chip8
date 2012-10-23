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
        Me.SuspendLayout()
        '
        'cmdLoad
        '
        Me.cmdLoad.Location = New System.Drawing.Point(33, 39)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(75, 23)
        Me.cmdLoad.TabIndex = 0
        Me.cmdLoad.Text = "Load ROM"
        Me.cmdLoad.UseVisualStyleBackColor = True
        '
        'cmbRom
        '
        Me.cmbRom.FormattingEnabled = True
        Me.cmbRom.Location = New System.Drawing.Point(12, 12)
        Me.cmbRom.Name = "cmbRom"
        Me.cmbRom.Size = New System.Drawing.Size(121, 21)
        Me.cmbRom.TabIndex = 1
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(146, 69)
        Me.Controls.Add(Me.cmbRom)
        Me.Controls.Add(Me.cmdLoad)
        Me.Name = "frmMain"
        Me.Text = "Chip8"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdLoad As System.Windows.Forms.Button
    Friend WithEvents cmbRom As System.Windows.Forms.ComboBox

End Class
