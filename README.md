# Android APK Installer (WPF) 📱🖥️

> **Status:** ✅ In Development  
> The app now supports native ADB detection and dynamic device listing via a modern WPF GUI.

A Windows desktop tool for installing Android APKs using ADB, built with a clean WPF interface and MVVM architecture.  
Designed for developers, testers, educators, and anyone managing multiple Android installations — without needing a command line.

---

## ✨ Current Features

- 🔌 Automatically detects connected Android devices on startup
- 🧠 Shows device **model** and **device ID** in a unified list
- 🧱 Uses native C# ADB integration (no CLI wrapper needed)
- ⚠️ Displays warning if ADB is not found, disables UI controls
- 🧰 Modern MVVM architecture with clean ViewModel binding

---

## 📂 Project Structure

```
android-apk-installer/
├── gui/  # WPF GUI application (main focus)
│   └── ApkInstaller.GUI/
├── README.md
└── .gitignore
```

---

## 🚀 Getting Started

> **Requirements:**
> - Windows 10 or later
> - [ADB (Android Debug Bridge)](https://developer.android.com/studio/releases/platform-tools)

### 🔧 Setup Instructions

1. Download and extract [ADB Platform Tools](https://developer.android.com/studio/releases/platform-tools)
2. Add `adb.exe` to your system `PATH`  
   **OR** place it in:
   - `C:\Android\platform-tools\adb.exe`
   - `%LOCALAPPDATA%\Android\Sdk\platform-tools\adb.exe`
3. Plug in your Android device (with USB debugging enabled)
4. Run the app — your device will be auto-detected
5. (Coming soon) Select a folder of APKs and install them

---

## 📅 Roadmap

- 📦 APK installation from folder
- 📜 ADB install output logs (per device)
- 🌓 Light/Dark mode toggle
- 💾 Remember last used ADB path (optional setting)
- 🧪 Drag-and-drop APK support
- 🔧 Device selector for multiple connections

---

## 📝 Notes

- This version no longer uses the CLI tool — ADB is invoked directly via C#.
- If ADB is not found, the app shows a warning and disables interaction.
- The original batch installer can still be found here:  
  👉 [android-apk-installer-cli](https://github.com/DeanAviv/android-apk-installer-cli)

---

## 🙌 Credits

Created and maintained by [Dean Aviv](https://github.com/DeanAviv).  
Follow the project for future updates and public release notes.
