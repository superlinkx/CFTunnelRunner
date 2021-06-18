# Cloudflare Tunnel Runner

## A simple wrapper around cloudflared for easy RDP access

This project is a quick hack and my first C# application. It's a bit gross and needs some refactoring, as well as some resiliency.

Works well enough for its intended purpose.

### How it works

- Running the application starts a tray icon
- First run will bring up the Settings screen to allow user input of their cloudflared domain
- Right clicking the tray icon allows user to start the tunnel
- Starting the tunnel runs through the process of downloading cloudflared.exe if needed, running the tunnel, and starting up an RDP connection automatically. User will just see RDP start relatively quickly.
- When the user completes their RDP session, they'll be able to stop the tunnel from the tray or exit entirely (which also removes the tunnel)
- Outside of first run, the application will attempt to start the tunnel automatically on launch
