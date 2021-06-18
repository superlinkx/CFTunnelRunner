using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace CloudflareTunnelRunner
{
    class CFTunnelRunnerContext : ApplicationContext
    {
        private readonly string cloudflaredLink = "https://bin.equinox.io/c/VdrWdbjqyF/cloudflared-stable-windows-amd64.zip";
        private readonly string cloudflaredPackageName = "cloudflared-stable-windows-amd64.zip";
        private readonly string cloudflaredInstallLocation = "C:\\cloudflared";
        private readonly string cloudflaredCommand;
        private Process tunnelProcess;
        private Process rdpProcess;
        private StringBuilder processOutput;
        private NotifyIcon notifyIcon;
        private SettingsForm settingsForm;
        private UserSettings userSettings;

        public CFTunnelRunnerContext()
        {
            cloudflaredCommand = cloudflaredInstallLocation + "\\cloudflared.exe";
            InitializeTrayIcon();
            InitializeSettings();
            LaunchCommandLineApp();
        }

        private void InitializeTrayIcon()
        {
            // Initialize Tray Icon
            notifyIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.TrayIcon,
                ContextMenuStrip = new ContextMenuStrip(),
                Visible = true
            };
            // Initialize ContextMenu
            notifyIcon.ContextMenuStrip.Items.Add("Start Tunnel", null, StartTunnel);
            notifyIcon.ContextMenuStrip.Items.Add("Stop Tunnel", null, StopTunnel);
            notifyIcon.ContextMenuStrip.Items.Add("Settings", null, OpenSettings);
            notifyIcon.ContextMenuStrip.Items.Add("Exit", null, ExitApp);
        }

        private void InitializeSettings()
        {
            settingsForm = new SettingsForm();
        }

        private void LaunchCommandLineApp()
        {
            // Get fresh User Settings every time we run
            userSettings = new UserSettings();
            if (userSettings.Domain == "")
            {
                ShowSettings();
                // TODO: Handle attempting launch after settings is closed
            }
            else
            {
                if (!File.Exists(cloudflaredCommand))
                {
                    // Should only happen if the command can't be found
                    var client = new WebClient();
                    Directory.CreateDirectory(cloudflaredInstallLocation);
                    client.DownloadFile(cloudflaredLink, cloudflaredPackageName);
                    // Unpack
                    ZipFile.ExtractToDirectory(cloudflaredPackageName, cloudflaredInstallLocation);

                    try
                    {
                        processOutput = new StringBuilder();
                        // Attempt to update to latest before we move on
                        Process p = new Process();
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.FileName = cloudflaredCommand;
                        p.StartInfo.Arguments = "update";
                        p.Start();
                        p.BeginErrorReadLine();
                        p.BeginOutputReadLine();
                        p.WaitForExit();
                    }
                    catch
                    {
                        Debug.WriteLine("Oops");
                    }
                }

                processOutput = new StringBuilder();
                try
                {
                    // Run the tunnel
                    // TODO: Make sure the child process gets cleaned up if this application is killed
                    tunnelProcess = new Process();
                    tunnelProcess.StartInfo.CreateNoWindow = true;
                    tunnelProcess.StartInfo.UseShellExecute = false;
                    tunnelProcess.StartInfo.FileName = cloudflaredCommand;
                    tunnelProcess.StartInfo.Arguments = "access rdp --hostname " + userSettings.Domain + " --url " + userSettings.Endpoint + ":" + userSettings.Port;
                    tunnelProcess.Exited += new EventHandler(TunnelExited);
                    tunnelProcess.Start();
                    tunnelProcess.BeginErrorReadLine();
                    tunnelProcess.BeginOutputReadLine();
                }
                catch
                {
                    Debug.WriteLine("Oops");
                }

                StartRDPSession();
            }
        }

        private void StartRDPSession()
        {
            try
            {
                // Start RDP
                rdpProcess = new Process();
                rdpProcess.StartInfo.CreateNoWindow = true;
                rdpProcess.StartInfo.UseShellExecute = false;
                rdpProcess.StartInfo.FileName = "mstsc.exe";
                rdpProcess.StartInfo.Arguments = "/v:" + userSettings.Endpoint + ":" + userSettings.Port;
                rdpProcess.Start();
                rdpProcess.WaitForExit();
            }
            catch
            {
                Debug.WriteLine("Oops");
            }
        }

        private void ShowSettings()
        {
            if (settingsForm == null || settingsForm.IsDisposed)
                settingsForm = new SettingsForm();
            // Open Settings Form
            settingsForm.Show();
        }

        private void TunnelExited(object sender, EventArgs e)
        {
            Debug.WriteLine("Tunnel Stopped");
        }

        private void StartTunnel(object sender, EventArgs e)
        {
            LaunchCommandLineApp();
        }

        private void StopTunnel(object sender, EventArgs e)
        {
            tunnelProcess.Kill();
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private void ExitApp(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            notifyIcon.Visible = false;
            if (tunnelProcess != null)
                tunnelProcess.Kill();
            Application.Exit();
        }
    }
}
