<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class pImmediate
    Inherits System.Windows.Forms.UserControl

    'UserControl esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.plot1 = New OxyPlot.WindowsForms.PlotView()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TS = New System.Windows.Forms.Label()
        Me.TST0 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'plot1
        '
        Me.plot1.BackColor = System.Drawing.Color.White
        Me.plot1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.plot1.Location = New System.Drawing.Point(3, 3)
        Me.plot1.Name = "plot1"
        Me.plot1.PanCursor = System.Windows.Forms.Cursors.Hand
        Me.plot1.Size = New System.Drawing.Size(557, 327)
        Me.plot1.TabIndex = 2
        Me.plot1.Text = "plot1"
        Me.plot1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE
        Me.plot1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE
        Me.plot1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.plot1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(563, 383)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Controls.Add(Me.TST0)
        Me.Panel1.Controls.Add(Me.TS)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 336)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(557, 44)
        Me.Panel1.TabIndex = 3
        '
        'TS
        '
        Me.TS.AutoSize = True
        Me.TS.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TS.ForeColor = System.Drawing.Color.Yellow
        Me.TS.Location = New System.Drawing.Point(18, 5)
        Me.TS.Name = "TS"
        Me.TS.Size = New System.Drawing.Size(0, 14)
        Me.TS.TabIndex = 0
        '
        'TST0
        '
        Me.TST0.AutoSize = True
        Me.TST0.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TST0.ForeColor = System.Drawing.Color.Yellow
        Me.TST0.Location = New System.Drawing.Point(18, 21)
        Me.TST0.Name = "TST0"
        Me.TST0.Size = New System.Drawing.Size(0, 14)
        Me.TST0.TabIndex = 1
        '
        'pImmediate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "pImmediate"
        Me.Size = New System.Drawing.Size(563, 383)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents plot1 As OxyPlot.WindowsForms.PlotView
    Friend WithEvents Timer1 As Timer
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TST0 As Label
    Friend WithEvents TS As Label
End Class
