using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoDisplaySwitch;

public partial class Form1 : Form
{
    // 常量定义
    private const string CONFIG_FILE = "config.json";
    private const string ICON_CONNECTED = "QuickControl1.ico";
    private const string ICON_DISCONNECTED = "QuickControl0.ico";
    private const int FADE_DURATION_DOWN = 1000; // 淡出时间（毫秒）
    private const int FADE_DURATION_UP = 2000;   // 淡入时间（毫秒）
    private const int SWITCH_DELAY = 2000;       // 切换延迟（毫秒）
    private const int FADE_STEPS_DOWN = 25;      // 淡出步骤数
    private const int FADE_STEPS_UP = 50;        // 淡入步骤数

    // 私有字段
    private AppConfig appConfig;
    private ManagementEventWatcher? usbWatcher;
    private readonly HttpClient httpClient = new HttpClient();

    /// <summary>
    /// 构造函数，初始化应用程序
    /// </summary>
    public Form1()
    {
        InitializeComponent();
        appConfig = new AppConfig();
        EnsureConfigExists();
        LoadDevices();
        LoadConfig();
        UpdateTrayIcon();
        StartUsbWatcher();
        this.ShowInTaskbar = false;
        this.WindowState = FormWindowState.Minimized;
        this.Hide();
    }

    /// <summary>
    /// 确保配置文件存在，如果不存在则创建默认配置
    /// </summary>
    private void EnsureConfigExists()
    {
        if (!File.Exists(CONFIG_FILE))
        {
            var defaultConfig = new AppConfig().ToDictionary();
            string json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(CONFIG_FILE, json);
        }
    }

    /// <summary>
    /// 更新托盘图标，根据设备连接状态显示不同图标
    /// </summary>
    private void UpdateTrayIcon()
    {
        bool isConnected = IsDeviceConnected(appConfig.SelectedDeviceId);
        string iconPath = isConnected ? ICON_CONNECTED : ICON_DISCONNECTED;
        if (File.Exists(iconPath))
        {
            notifyIcon1.Icon = new System.Drawing.Icon(iconPath);
        }
    }

    /// <summary>
    /// 检查指定的设备是否已连接
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <returns>设备是否连接</returns>
    private bool IsDeviceConnected(string deviceId)
    {
        if (string.IsNullOrEmpty(deviceId))
        {
            return false;
        }

        try
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_USBHub");
            foreach (ManagementObject device in searcher.Get())
            {
                string currentDeviceId = device["DeviceID"]?.ToString() ?? "";
                if (currentDeviceId.Contains(deviceId))
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            // 记录错误但不显示给用户，因为这是后台检查
            Console.WriteLine($"检查设备连接状态失败: {ex.Message}");
        }

        return false;
    }

    private void LoadDevices()
    {
        listBoxDevices.Items.Clear();
        try
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_USBHub");
            foreach (ManagementObject device in searcher.Get())
            {
                string name = device["Name"]?.ToString() ?? "Unknown";
                string deviceId = device["DeviceID"]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(deviceId))
                {
                    listBoxDevices.Items.Add($"{name} ({deviceId})");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"加载USB设备失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 加载配置文件并更新UI和配置对象
    /// </summary>
    private void LoadConfig()
    {
        if (File.Exists(CONFIG_FILE))
        {
            try
            {
                string json = File.ReadAllText(CONFIG_FILE);
                var configDict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                if (configDict != null)
                {
                    appConfig.LoadFromDictionary(configDict);

                    // 更新UI控件
                    textBoxBDIP.Text = appConfig.BDIP;
                    textBoxBDPort.Text = appConfig.BDPort;
                    textBoxBDToken.Text = appConfig.BDToken;
                    checkBoxDisconnectExecute.Checked = appConfig.DisconnectExecute;
                    checkBoxDisconnectSendBD.Checked = appConfig.DisconnectSendBD;
                    checkBoxConnectExecute.Checked = appConfig.ConnectExecute;
                    checkBoxConnectSendBD.Checked = appConfig.ConnectSendBD;
                    checkBoxEnableFadeEffect.Checked = appConfig.EnableFadeEffect;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载配置文件失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// 保存配置到文件
    /// </summary>
    private void SaveConfig()
    {
        try
        {
            // 从UI更新配置对象
            appConfig.BDIP = textBoxBDIP.Text;
            appConfig.BDPort = textBoxBDPort.Text;
            appConfig.BDToken = textBoxBDToken.Text;
            appConfig.DisconnectExecute = checkBoxDisconnectExecute.Checked;
            appConfig.DisconnectSendBD = checkBoxDisconnectSendBD.Checked;
            appConfig.ConnectExecute = checkBoxConnectExecute.Checked;
            appConfig.ConnectSendBD = checkBoxConnectSendBD.Checked;
            appConfig.EnableFadeEffect = checkBoxEnableFadeEffect.Checked;

            string json = JsonSerializer.Serialize(appConfig.ToDictionary(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(CONFIG_FILE, json);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"保存配置文件失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void StartUsbWatcher()
    {
        try
        {
            var query = new WqlEventQuery("__InstanceOperationEvent", new TimeSpan(0, 0, 1));
            query.QueryString = "SELECT * FROM __InstanceOperationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_USBHub'";
            usbWatcher = new ManagementEventWatcher(query);
            usbWatcher.EventArrived += new EventArrivedEventHandler(OnUsbEvent);
            usbWatcher.Start();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"启动USB监听失败: {ex.Message}");
        }
    }

    private async void OnUsbEvent(object sender, EventArrivedEventArgs e)
    {
        var targetInstance = e.NewEvent["TargetInstance"] as ManagementBaseObject;
        if (targetInstance != null)
        {
            string deviceId = targetInstance["DeviceID"]?.ToString() ?? "";
            string eventType = e.NewEvent.ClassPath.ClassName;

            if (deviceId.Contains(appConfig.SelectedDeviceId) && !string.IsNullOrEmpty(appConfig.SelectedDeviceId))
            {
                if (eventType.Contains("InstanceDeletion"))
                {
                    // 设备断开 - 切换到 Mac
                    if (appConfig.DisconnectExecute)
                    {
                        await ExecuteDisplaySwitchCommand("/SetValue Primary 60 15", "0x0F", true);
                    }
                    if (appConfig.DisconnectSendBD)
                    {
                        await ExecuteDisplaySwitchCommand("", "0x0F", true);
                    }
                    UpdateTrayIcon();
                }
                else if (eventType.Contains("InstanceCreation"))
                {
                    // 设备连接 - 切换到 Windows
                    if (appConfig.ConnectExecute)
                    {
                        await ExecuteDisplaySwitchCommand("/SetValue Primary 60 17", "0x11", true);
                    }
                    if (appConfig.ConnectSendBD)
                    {
                        await ExecuteDisplaySwitchCommand("", "0x11", true);
                    }
                    UpdateTrayIcon();
                }
            }
        }
    }

    /// <summary>
    /// 执行显示器切换命令
    /// </summary>
    /// <param name="controlMyMonitorArgs">ControlMyMonitor 命令参数</param>
    /// <param name="betterDisplayValue">BetterDisplay 输入源值</param>
    /// <param name="forceQuickMode">是否强制快速模式（忽略配置）</param>
    private async Task ExecuteDisplaySwitchCommand(string controlMyMonitorArgs, string betterDisplayValue, bool forceQuickMode = false)
    {
        try
        {
            bool useFadeEffect = appConfig.EnableFadeEffect && !forceQuickMode;

            if (!useFadeEffect)
            {
                // 快速模式：直接执行切换，无淡入淡出
                Process.Start("ControlMyMonitor.exe", controlMyMonitorArgs);
                SendBetterDisplayCommand(betterDisplayValue, "0x60");
                SendBetterDisplayCommand("0x64", "0x10");
                Process.Start("ControlMyMonitor.exe", "/SetValue Primary 10 100");
                return;
            }

            // 完整模式：带淡入淡出效果
            // 亮度从100线性降到0
            await FadeBrightness(100, 0, FADE_DURATION_DOWN, 0, FADE_STEPS_DOWN);

            // 执行切换命令
            Process.Start("ControlMyMonitor.exe", controlMyMonitorArgs);
            SendBetterDisplayCommand(betterDisplayValue, "0x60");

            // 等待切换完成
            await Task.Delay(SWITCH_DELAY);

            // 亮度从0线性升到100
            await FadeBrightness(0, 100, FADE_DURATION_UP, 1, FADE_STEPS_UP);
            SendBetterDisplayCommand("0x64", "0x10");
            Process.Start("ControlMyMonitor.exe", "/SetValue Primary 10 100");
        }
        catch (Exception ex)
        {
            // 记录错误但不阻止程序运行
            Console.WriteLine($"执行显示器切换命令失败: {ex.Message}");
        }
    }
    private async Task FadeBrightness(int startBrightness, int endBrightness, int durationMs, int type=0, int steps=50)
    {
        // const int steps = 50; // 20个步骤
        int stepDuration = durationMs / steps;
        double brightnessStep = (double)(endBrightness - startBrightness) / steps;

        for (int i = 0; i <= steps; i++)
        {
            int currentBrightness = (int)Math.Round(startBrightness + brightnessStep * i);
            try
            {
                if (type == 1)
                {
                    // 使用BetterDisplay
                    SendBetterDisplayCommand($"0x{currentBrightness:X2}","0x10");
                }
                else
                {
                    // 使用ControlMyMonitor
                    Process.Start("ControlMyMonitor.exe", $"/SetValue Primary 10 {currentBrightness}");
                }
                // Process.Start("ControlMyMonitor.exe", $"/SetValue Primary 10 {currentBrightness}");
            }
            catch
            {
                // 忽略单个步骤的错误
            }

            if (i < steps)
            {
                await Task.Delay(stepDuration);
            }
        }
    }

    /// <summary>
    /// 发送命令到 BetterDisplay
    /// </summary>
    /// <param name="value">要设置的值</param>
    /// <param name="vcp">VCP 代码</param>
    private async void SendBetterDisplayCommand(string value, string vcp = "0x60")
    {
        if (!string.IsNullOrEmpty(appConfig.BDIP) && !string.IsNullOrEmpty(appConfig.BDPort))
        {
            string url = $"http://{appConfig.BDIP}:{appConfig.BDPort}/set?feature=ddc&vcp={vcp}&value={value}";
            if (!string.IsNullOrEmpty(appConfig.BDToken))
            {
                url += $"&token={appConfig.BDToken}";
            }

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                // 可选：检查 response.IsSuccessStatusCode
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"BetterDisplay 命令失败: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // 记录错误但不显示给用户，因为这是后台操作
                Console.WriteLine($"发送 BetterDisplay 命令失败: {ex.Message}");
            }
        }
    }

    private void buttonSave_Click(object sender, EventArgs e)
    {
        if (listBoxDevices.SelectedItem != null)
        {
            string? selected = listBoxDevices.SelectedItem.ToString();
            if (selected != null)
            {
                int start = selected.LastIndexOf('(') + 1;
                int end = selected.LastIndexOf(')');
                if (start > 0 && end > start)
                {
                    appConfig.SelectedDeviceId = selected.Substring(start, end - start);
                    SaveConfig();
                    MessageBox.Show("配置已保存！");
                }
                else
                {
                    MessageBox.Show("设备ID格式无效！");
                }
            }
            else
            {
                MessageBox.Show("无法获取设备信息！");
            }
        }
        else
        {
            MessageBox.Show("请先选择一个设备！");
        }
    }

    private void toolStripMenuItemSwitch_Click(object sender, EventArgs e)
    {
        this.Show();
        this.WindowState = FormWindowState.Normal;
        this.ShowInTaskbar = true;
    }

    private void toolStripMenuItemExit_Click(object sender, EventArgs e)
    {
        usbWatcher?.Stop();
        Application.Exit();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            this.Hide();
            this.ShowInTaskbar = false;
        }
    }

    private void toolStripMenuItemSwitchToMac_Click(object sender, EventArgs e)
    {
        // 执行设备断开功能（切换至MAC）
        _ = SwitchToMacAsync();
    }

    private void toolStripMenuItemSwitchToWin_Click(object sender, EventArgs e)
    {
        // 执行设备连接功能（切换至WIN）
        _ = SwitchToWinAsync();
    }

    /// <summary>
    /// 手动切换到 Mac 模式
    /// </summary>
    private async Task SwitchToMacAsync()
    {
        if (appConfig.DisconnectExecute)
        {
            await ExecuteDisplaySwitchCommand("/SetValue Primary 60 15", "0x0F");
        }
        if (appConfig.DisconnectSendBD)
        {
            await ExecuteDisplaySwitchCommand("", "0x0F");
        }
        UpdateTrayIcon();
    }

    /// <summary>
    /// 手动切换到 Windows 模式
    /// </summary>
    private async Task SwitchToWinAsync()
    {
        if (appConfig.ConnectExecute)
        {
            await ExecuteDisplaySwitchCommand("/SetValue Primary 60 17", "0x11");
        }
        if (appConfig.ConnectSendBD)
        {
            await ExecuteDisplaySwitchCommand("", "0x11");
        }
        UpdateTrayIcon();
    }
}

/// <summary>
/// 配置类，用于管理应用程序设置
/// </summary>
public class AppConfig
{
    public string SelectedDeviceId { get; set; } = "";
    public string BDIP { get; set; } = "";
    public string BDPort { get; set; } = "";
    public string BDToken { get; set; } = "";
    public bool DisconnectExecute { get; set; } = true;
    public bool DisconnectSendBD { get; set; } = true;
    public bool ConnectExecute { get; set; } = true;
    public bool ConnectSendBD { get; set; } = true;
    public bool EnableFadeEffect { get; set; } = true; // 是否启用淡入淡出效果

    /// <summary>
    /// 从字典加载配置
    /// </summary>
    public void LoadFromDictionary(Dictionary<string, string> dict)
    {
        SelectedDeviceId = dict.GetValueOrDefault("SelectedDeviceId", "");
        BDIP = dict.GetValueOrDefault("BDIP", "");
        BDPort = dict.GetValueOrDefault("BDPort", "");
        BDToken = dict.GetValueOrDefault("BDToken", "");
        DisconnectExecute = bool.Parse(dict.GetValueOrDefault("DisconnectExecute", "true"));
        DisconnectSendBD = bool.Parse(dict.GetValueOrDefault("DisconnectSendBD", "true"));
        ConnectExecute = bool.Parse(dict.GetValueOrDefault("ConnectExecute", "true"));
        ConnectSendBD = bool.Parse(dict.GetValueOrDefault("ConnectSendBD", "true"));
        EnableFadeEffect = bool.Parse(dict.GetValueOrDefault("EnableFadeEffect", "true"));
    }

    /// <summary>
    /// 转换为字典
    /// </summary>
    public Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string>
        {
            { "SelectedDeviceId", SelectedDeviceId },
            { "BDIP", BDIP },
            { "BDPort", BDPort },
            { "BDToken", BDToken },
            { "DisconnectExecute", DisconnectExecute.ToString() },
            { "DisconnectSendBD", DisconnectSendBD.ToString() },
            { "ConnectExecute", ConnectExecute.ToString() },
            { "ConnectSendBD", ConnectSendBD.ToString() },
            { "EnableFadeEffect", EnableFadeEffect.ToString() }
        };
    }
}
