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
        private readonly string cloudflaredLink = "https://bin.equinox.io/c/VdrWdbjqyF/cloudflared-stable-windows-386.zip";
        private readonly string cloudflaredPackageName = "cloudflared-stable-windows-386.zip";
        private readonly string cloudflaredInstallLocation = "C:\\cloudflared";
        private readonly string dataDir;
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
            dataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\CTR\\";
            //InitializeTrayIcon();
            ShowSettings();
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

        private void LaunchCommandLineApp()
        {
            // Get fresh User Settings every time we run
            userSettings = new UserSettings();
            if (!File.Exists(cloudflaredCommand))
            {
                // Should only happen if the command can't be found
                var client = new WebClient();
                Directory.CreateDirectory(dataDir);
                Directory.CreateDirectory(cloudflaredInstallLocation);
                client.DownloadFile(cloudflaredLink, dataDir + cloudflaredPackageName);
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
                    MessageBox.Show("Tunnel update failed");
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
            }
            catch
            {
                MessageBox.Show("Tunnel Failed to start");
            }

            StartRDPSession();
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
                MessageBox.Show("RDP Failed to start");
            }

            EndApp();
        }

        private void ShowSettings()
        {
            if (tunnelProcess != null)
                KillTunnel();
            if (settingsForm == null || settingsForm.IsDisposed)
                settingsForm = new SettingsForm();
            if (!settingsForm.Visible) {
                settingsForm.ShowDialog();
            }
            LaunchCommandLineApp();
        }

        private void TunnelExited(object sender, EventArgs e)
        {
            MessageBox.Show("Tunnel Stopped");
        }

        private void StartTunnel(object sender, EventArgs e)
        {
            LaunchCommandLineApp();
        }

        private void StopTunnel(object sender, EventArgs e)
        {
            KillTunnel();
        }

        private void KillTunnel()
        {
            tunnelProcess.Kill();
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private void EndApp() {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            //notifyIcon.Visible = false;
            if (tunnelProcess != null)
                tunnelProcess.Kill();
            Application.Exit();
            Environment.Exit(0);
        }

        private void ExitApp(object sender, EventArgs e)
        {
            EndApp();
        }
    }
}
