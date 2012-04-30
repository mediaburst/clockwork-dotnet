<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.sendMessage = New System.Windows.Forms.Button()
        Me.message = New System.Windows.Forms.TextBox()
        Me.messageLabel = New System.Windows.Forms.Label()
        Me.number = New System.Windows.Forms.TextBox()
        Me.toLabel = New System.Windows.Forms.Label()
        Me.key = New System.Windows.Forms.TextBox()
        Me.keyLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'sendMessage
        '
        Me.sendMessage.Location = New System.Drawing.Point(70, 151)
        Me.sendMessage.Name = "sendMessage"
        Me.sendMessage.Size = New System.Drawing.Size(202, 23)
        Me.sendMessage.TabIndex = 13
        Me.sendMessage.Text = "Send Message"
        Me.sendMessage.UseVisualStyleBackColor = True
        '
        'message
        '
        Me.message.Location = New System.Drawing.Point(70, 64)
        Me.message.Multiline = True
        Me.message.Name = "message"
        Me.message.Size = New System.Drawing.Size(202, 80)
        Me.message.TabIndex = 12
        '
        'messageLabel
        '
        Me.messageLabel.AutoSize = True
        Me.messageLabel.Location = New System.Drawing.Point(14, 67)
        Me.messageLabel.Name = "messageLabel"
        Me.messageLabel.Size = New System.Drawing.Size(50, 13)
        Me.messageLabel.TabIndex = 11
        Me.messageLabel.Text = "Message"
        '
        'number
        '
        Me.number.Location = New System.Drawing.Point(70, 38)
        Me.number.Name = "number"
        Me.number.Size = New System.Drawing.Size(93, 20)
        Me.number.TabIndex = 10
        '
        'toLabel
        '
        Me.toLabel.AutoSize = True
        Me.toLabel.Location = New System.Drawing.Point(14, 41)
        Me.toLabel.Name = "toLabel"
        Me.toLabel.Size = New System.Drawing.Size(20, 13)
        Me.toLabel.TabIndex = 9
        Me.toLabel.Text = "To"
        '
        'key
        '
        Me.key.Location = New System.Drawing.Point(70, 12)
        Me.key.Name = "key"
        Me.key.Size = New System.Drawing.Size(202, 20)
        Me.key.TabIndex = 8
        '
        'keyLabel
        '
        Me.keyLabel.AutoSize = True
        Me.keyLabel.Location = New System.Drawing.Point(14, 15)
        Me.keyLabel.Name = "keyLabel"
        Me.keyLabel.Size = New System.Drawing.Size(25, 13)
        Me.keyLabel.TabIndex = 7
        Me.keyLabel.Text = "Key"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.sendMessage)
        Me.Controls.Add(Me.message)
        Me.Controls.Add(Me.messageLabel)
        Me.Controls.Add(Me.number)
        Me.Controls.Add(Me.toLabel)
        Me.Controls.Add(Me.key)
        Me.Controls.Add(Me.keyLabel)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents sendMessage As System.Windows.Forms.Button
    Private WithEvents message As System.Windows.Forms.TextBox
    Private WithEvents messageLabel As System.Windows.Forms.Label
    Private WithEvents number As System.Windows.Forms.TextBox
    Private WithEvents toLabel As System.Windows.Forms.Label
    Private WithEvents key As System.Windows.Forms.TextBox
    Private WithEvents keyLabel As System.Windows.Forms.Label
End Class
