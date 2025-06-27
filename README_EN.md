# SpeakerSwitch - Windows Audio Output Device Switcher

A simple Windows application for switching to the next available audio output device. Can be used with keyboard or mouse macros to achieve one-click audio device switching.

## Features

- 🔄 Automatically cycle through audio output devices
- ⚡ Exits immediately after execution, no GUI

## System Requirements

- Windows 10/11
- .NET 9.0 or higher
- At least 2 available audio output devices

## Usage

### Basic Usage

Simply double-click `SpeakerSwitch.exe` to switch to the next audio device.

### Usage Scenarios

#### Add to System PATH

Add the directory containing `SpeakerSwitch.exe` to the PATH environment variable:

1. Right-click "This PC" → "Properties" → "Advanced system settings"
2. Click "Environment Variables"
3. Find "Path" in "System variables" and click "Edit"
4. Add the directory path where SpeakerSwitch.exe is located
5. Click OK to save

After completion, you can run it from anywhere via command line:
```cmd
SpeakerSwitch
```

## License

This project is open source under the [MIT License](LICENSE).