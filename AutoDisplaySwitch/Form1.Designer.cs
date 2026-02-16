namespace AutoDisplaySwitch;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        listBoxDevices = new ListBox();
        buttonSave = new Button();
        labelInstructions = new Label();
        notifyIcon1 = new NotifyIcon(components);
        contextMenuStrip1 = new ContextMenuStrip(components);
        toolStripMenuItemSwitch = new ToolStripMenuItem();
        toolStripMenuItemSwitchToMac = new ToolStripMenuItem();
        toolStripMenuItemSwitchToWin = new ToolStripMenuItem();
        toolStripMenuItemExit = new ToolStripMenuItem();
        labelBDIP = new Label();
        textBoxBDIP = new TextBox();
        labelBDPort = new Label();
        textBoxBDPort = new TextBox();
        labelBDToken = new Label();
        textBoxBDToken = new TextBox();
        checkBoxDisconnectExecute = new CheckBox();
        checkBoxDisconnectSendBD = new CheckBox();
        checkBoxConnectExecute = new CheckBox();
        checkBoxConnectSendBD = new CheckBox();
        checkBoxEnableFadeEffect= new CheckBox();
        contextMenuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // listBoxDevices
        // 
        listBoxDevices.FormattingEnabled = true;
        listBoxDevices.Location = new Point(12, 40);
        listBoxDevices.Name = "listBoxDevices";
        listBoxDevices.Size = new Size(540, 344);
        listBoxDevices.TabIndex = 0;
        // 
        // buttonSave
        // 
        buttonSave.Location = new Point(462, 420);
        buttonSave.Name = "buttonSave";
        buttonSave.Size = new Size(90, 51);
        buttonSave.TabIndex = 1;
        buttonSave.Text = "保存";
        buttonSave.UseVisualStyleBackColor = true;
        buttonSave.Click += buttonSave_Click;
        // 
        // labelInstructions
        // 
        labelInstructions.AutoSize = true;
        labelInstructions.Location = new Point(12, 9);
        labelInstructions.Name = "labelInstructions";
        labelInstructions.Size = new Size(140, 17);
        labelInstructions.TabIndex = 2;
        labelInstructions.Text = "选择要监控的USB设备：";
        // 
        // notifyIcon1
        // 
        notifyIcon1.ContextMenuStrip = contextMenuStrip1;
        notifyIcon1.Text = "Auto Display Switch";
        notifyIcon1.Visible = true;
        // 
        // contextMenuStrip1
        // 
        contextMenuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItemSwitch, toolStripMenuItemSwitchToMac, toolStripMenuItemSwitchToWin, toolStripMenuItemExit });
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new Size(125, 48);
        // 
        // toolStripMenuItemSwitch
        // 
        toolStripMenuItemSwitch.Name = "toolStripMenuItemSwitch";
        toolStripMenuItemSwitch.Size = new Size(124, 22);
        toolStripMenuItemSwitch.Text = "修改配置";
        toolStripMenuItemSwitch.Click += toolStripMenuItemSwitch_Click;
        // 
        // toolStripMenuItemSwitchToMac
        //
        toolStripMenuItemSwitchToMac.Name = "toolStripMenuItemSwitchToMac";
        toolStripMenuItemSwitchToMac.Size = new Size(124, 22);
        toolStripMenuItemSwitchToMac.Text = "切换至MAC";
        toolStripMenuItemSwitchToMac.Click += toolStripMenuItemSwitchToMac_Click;
        //
        // toolStripMenuItemSwitchToWin
        //
        toolStripMenuItemSwitchToWin.Name = "toolStripMenuItemSwitchToWin";
        toolStripMenuItemSwitchToWin.Size = new Size(124, 22);
        toolStripMenuItemSwitchToWin.Text = "切换至WIN";
        toolStripMenuItemSwitchToWin.Click += toolStripMenuItemSwitchToWin_Click;
        //
        // toolStripMenuItemExit
        //
        toolStripMenuItemExit.Name = "toolStripMenuItemExit";
        toolStripMenuItemExit.Size = new Size(124, 22);
        toolStripMenuItemExit.Text = "退出";
        toolStripMenuItemExit.Click += toolStripMenuItemExit_Click;
        // 
        // labelBDIP
        // 
        labelBDIP.AutoSize = true;
        labelBDIP.Location = new Point(12, 390);
        labelBDIP.Name = "labelBDIP";
        labelBDIP.Size = new Size(103, 17);
        labelBDIP.TabIndex = 3;
        labelBDIP.Text = "BetterDisplay IP:";
        // 
        // textBoxBDIP
        // 
        textBoxBDIP.Location = new Point(120, 390);
        textBoxBDIP.Name = "textBoxBDIP";
        textBoxBDIP.Size = new Size(100, 23);
        textBoxBDIP.TabIndex = 4;
        // 
        // labelBDPort
        // 
        labelBDPort.AutoSize = true;
        labelBDPort.Location = new Point(230, 390);
        labelBDPort.Name = "labelBDPort";
        labelBDPort.Size = new Size(35, 17);
        labelBDPort.TabIndex = 5;
        labelBDPort.Text = "端口:";
        // 
        // textBoxBDPort
        // 
        textBoxBDPort.Location = new Point(270, 390);
        textBoxBDPort.Name = "textBoxBDPort";
        textBoxBDPort.Size = new Size(60, 23);
        textBoxBDPort.TabIndex = 6;
        // 
        // labelBDToken
        // 
        labelBDToken.AutoSize = true;
        labelBDToken.Location = new Point(340, 390);
        labelBDToken.Name = "labelBDToken";
        labelBDToken.Size = new Size(47, 17);
        labelBDToken.TabIndex = 7;
        labelBDToken.Text = "Token:";
        // 
        // textBoxBDToken
        // 
        textBoxBDToken.Location = new Point(393, 390);
        textBoxBDToken.Name = "textBoxBDToken";
        textBoxBDToken.Size = new Size(100, 23);
        textBoxBDToken.TabIndex = 8;
        textBoxBDToken.Text = "373137461";
        // 
        // checkBoxDisconnectExecute
        // 
        checkBoxDisconnectExecute.AutoSize = true;
        checkBoxDisconnectExecute.Checked = true;
        checkBoxDisconnectExecute.CheckState = CheckState.Checked;
        checkBoxDisconnectExecute.Location = new Point(12, 420);
        checkBoxDisconnectExecute.Name = "checkBoxDisconnectExecute";
        checkBoxDisconnectExecute.Size = new Size(219, 21);
        checkBoxDisconnectExecute.TabIndex = 9;
        checkBoxDisconnectExecute.Text = "设备断开时执行ControlMyMonitor";
        checkBoxDisconnectExecute.UseVisualStyleBackColor = true;
        // 
        // checkBoxDisconnectSendBD
        // 
        checkBoxDisconnectSendBD.AutoSize = true;
        checkBoxDisconnectSendBD.Checked = true;
        checkBoxDisconnectSendBD.CheckState = CheckState.Checked;
        checkBoxDisconnectSendBD.Location = new Point(12, 447);
        checkBoxDisconnectSendBD.Name = "checkBoxDisconnectSendBD";
        checkBoxDisconnectSendBD.Size = new Size(188, 21);
        checkBoxDisconnectSendBD.TabIndex = 10;
        checkBoxDisconnectSendBD.Text = "设备断开时发送BetterDisplay";
        checkBoxDisconnectSendBD.UseVisualStyleBackColor = true;
        // 
        // checkBoxConnectExecute
        // 
        checkBoxConnectExecute.AutoSize = true;
        checkBoxConnectExecute.Checked = true;
        checkBoxConnectExecute.CheckState = CheckState.Checked;
        checkBoxConnectExecute.Location = new Point(237, 419);
        checkBoxConnectExecute.Name = "checkBoxConnectExecute";
        checkBoxConnectExecute.Size = new Size(219, 21);
        checkBoxConnectExecute.TabIndex = 11;
        checkBoxConnectExecute.Text = "设备连接时执行ControlMyMonitor";
        checkBoxConnectExecute.UseVisualStyleBackColor = true;
        // 
        // checkBoxConnectSendBD
        // 
        checkBoxConnectSendBD.AutoSize = true;
        checkBoxConnectSendBD.Checked = true;
        checkBoxConnectSendBD.CheckState = CheckState.Checked;
        checkBoxConnectSendBD.Location = new Point(237, 446);
        checkBoxConnectSendBD.Name = "checkBoxConnectSendBD";
        checkBoxConnectSendBD.Size = new Size(188, 21);
        checkBoxConnectSendBD.TabIndex = 12;
        checkBoxConnectSendBD.Text = "设备连接时发送BetterDisplay";
        checkBoxConnectSendBD.UseVisualStyleBackColor = true;
        //
        // checkBoxEnableFadeEffect
        //
        checkBoxEnableFadeEffect.AutoSize = true;
        checkBoxEnableFadeEffect.Checked = true;
        checkBoxEnableFadeEffect.CheckState = CheckState.Checked;
        checkBoxEnableFadeEffect.Location = new Point(12, 474);
        checkBoxEnableFadeEffect.Name = "checkBoxEnableFadeEffect";
        checkBoxEnableFadeEffect.Size = new Size(140, 21);
        checkBoxEnableFadeEffect.TabIndex = 13;
        checkBoxEnableFadeEffect.Text = "启用淡入淡出效果";
        checkBoxEnableFadeEffect.UseVisualStyleBackColor = true;
        //
        // Form1
        //
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(564, 510);
        Controls.Add(labelInstructions);
        Controls.Add(buttonSave);
        Controls.Add(listBoxDevices);
        Controls.Add(labelBDIP);
        Controls.Add(textBoxBDIP);
        Controls.Add(labelBDPort);
        Controls.Add(textBoxBDPort);
        Controls.Add(labelBDToken);
        Controls.Add(textBoxBDToken);
        Controls.Add(checkBoxDisconnectExecute);
        Controls.Add(checkBoxDisconnectSendBD);
        Controls.Add(checkBoxConnectExecute);
        Controls.Add(checkBoxConnectSendBD);
        Controls.Add(checkBoxEnableFadeEffect);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "Form1";
        Text = "USB设备监控";
        WindowState = FormWindowState.Minimized;
        FormClosing += Form1_FormClosing;
        contextMenuStrip1.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.ListBox listBoxDevices;
    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.Label labelInstructions;
    private System.Windows.Forms.NotifyIcon notifyIcon1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSwitch;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSwitchToMac;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSwitchToWin;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
    private System.Windows.Forms.Label labelBDIP;
    private System.Windows.Forms.TextBox textBoxBDIP;
    private System.Windows.Forms.Label labelBDPort;
    private System.Windows.Forms.TextBox textBoxBDPort;
    private System.Windows.Forms.Label labelBDToken;
    private System.Windows.Forms.TextBox textBoxBDToken;
    private System.Windows.Forms.CheckBox checkBoxDisconnectExecute;
    private System.Windows.Forms.CheckBox checkBoxDisconnectSendBD;
    private System.Windows.Forms.CheckBox checkBoxConnectExecute;
    private System.Windows.Forms.CheckBox checkBoxConnectSendBD;
    private System.Windows.Forms.CheckBox checkBoxEnableFadeEffect;
}
