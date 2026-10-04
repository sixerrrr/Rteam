<img src="./logo.png" width="100" align="right"/>

## Rteam

> Simple account manager for Roblox with multi-instancing support

### Getting started

1. Download [Rteam.msix](https://example.com)
2. Open it
3. Click `Install`
4. Open Rteam from the start menu

### How does multi-instancing work?

The `RobloxPlayerBeta.exe` process has a Mutex called 
`ROBLOX_singletonEvent` to ensure that there is only one instance of roblox open.

Rteam creates this Mutex before roblox and closes it if it already exists.

### Is this a virus?

No. But SmartScreen will probably annoy you because this application is unsigned.

If you are still skeptical, you can compile it yourself using [Visual Studio Community](https://visualstudio.microsoft.com/vs/).

### Is this bannable?

No.

### How do I login?

1. Click "Manage Accounts"
2. Click the + icon.
3. Open Roblox and:

	- If you are on mobile, click the hamburger menu (☰) and then scroll down to "Quick Sign-in" and type in your code there and then accept
	- If you are on desktop, go to [roblox.com](https://roblox.com/home), click on the settings icon (⚙️), Quick Sign In, type in the code and then accept

4. Done! Your account should show up in the account switcher now.

![MIT License](https://img.shields.io/badge/MIT-license?style=plastic&label=License
)

Built by @sixerrrr