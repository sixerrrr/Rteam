<img src="./logo.png" width="100" align="right"/>

## Rteam

> Simple account manager for Roblox with multi-instancing support

### Getting started

1. Download [Rteam.msi](https://example.com)

> [!NOTE]
> An [MSIX installer](https://example.com) is available, but you will need to turn on developer mode to install it.

> [!IMPORTANT]
> SmartScreen will (probably) nag you about how it "protected your PC". This is due to the code being unsigned.
>
> I am **NOT** paying to get a certificate, you will have to click More Info and Run anways.

> [!IMPORTANT]
> This program requires .NET Core 8. The installer should install it for you.

2. Open it
3. Click <kbd>Next</kbd>
4. Click <kbd>Next</kbd> (again)
5. Click <kbd>Next</kbd> (again) (again)
6. Accept the UAC prompt.
7. Click <kbd>Close</kbd>
8. Open Rteam from the start menu

### How does multi-instancing work?

The `RobloxPlayerBeta.exe` process has a Mutex called 
`ROBLOX_singletonEvent` to ensure that there is only one instance of roblox open.

Rteam creates this Mutex before roblox and closes it if it already exists.

### Is this a virus?

No. But SmartScreen will probably annoy you because this application is unsigned.

If you are still skeptical, you can compile it yourself using [Visual Studio Community](https://visualstudio.microsoft.com/vs/).

### Is this bannable?

No.

### Why can I only use 20 accounts?

The Microsoft Windows Credential Locker sets a [limit of 20 credentials for applications](https://learn.microsoft.com/en-us/windows/apps/develop/security/credential-locker#overview-of-the-sample-scenario).

### How do I login?

1. Click "Manage Accounts"
2. Click the <kbd>+</kbd> icon.
3. Open Roblox and:

	- If you are on mobile, click the hamburger menu <kbd>☰</kbd> and then scroll down to "Quick Sign-in" and type in your code there and then accept
	- If you are on desktop, go to [roblox.com](https://roblox.com/home), click on the settings icon <kbd>⚙️</kbd>, Quick Sign In, type in the code and then accept

4. Done! Your account should show up in the account switcher now.

![MIT License](https://img.shields.io/badge/MIT-license?style=plastic&label=License)

Built by @sixerrrr. I **hate** Visual Studio Installer.