# Discord Timestamp — PowerToys Command Palette Extension

A [PowerToys Command Palette](https://learn.microsoft.com/en-us/windows/powertoys/command-palette/overview) extension that generates [Discord timestamp codes](https://discord.com/developers/docs/reference#message-formatting-timestamp-styles) from any date and time you type — similar to [hammertime.cyou](https://hammertime.cyou), but without leaving your keyboard.

---

## What it does

Type a date and time, get all Discord timestamp format codes instantly. Click any format to copy it to your clipboard, then paste directly into Discord.

Discord timestamps render in every user's local timezone automatically — so `<t:1750006800:F>` shows as `Sunday, June 15, 2025 3:00 PM` for someone in New York and `Monday, June 16, 2025 4:00 AM` for someone in Tokyo.

---

## Supported formats

| Code | Name | Example |
|------|------|---------|
| `<t:…:t>` | Short Time | 3:00 PM |
| `<t:…:T>` | Long Time | 3:00:00 PM |
| `<t:…:d>` | Short Date | 6/15/2025 |
| `<t:…:D>` | Long Date | June 15, 2025 |
| `<t:…:f>` | Short Date/Time | June 15, 2025 3:00 PM |
| `<t:…:F>` | Long Date/Time | Sunday, June 15, 2025 3:00 PM |
| `<t:…:s>` | Short Date/Time (alt) | 6/15/2025 3:00 PM |
| `<t:…:S>` | Long Date/Time (alt) | 6/15/2025 3:00:00 PM |
| `<t:…:R>` | Relative | in 3 days |
| `1750006800` | Raw Unix Timestamp | — |

---

## Requirements

- Windows 11
- [Microsoft PowerToys](https://github.com/microsoft/PowerToys) v0.90 or later (Command Palette must be enabled)

---

## Installation

### Option A — Build from source

1. Clone this repository
2. Open `DiscordTimestamp.sln` in Visual Studio 2022
3. Ensure you have the **.NET desktop development** and **Windows application development** workloads installed
4. Go to **Build → Deploy DiscordTimestamp**
5. Open Command Palette (`Win + Alt + Space`), type `Reload`, and select **Reload Command Palette Extensions**

### Option B — Microsoft Store

*Maybe one day....*

---

## Usage

There are two ways to use the extension:

### Method 1 — Type directly in Command Palette search

Open Command Palette (`Win + Alt + Space`) and start typing a date. The **Discord Timestamp** fallback entry will appear in the results and update as you type. Press **Enter** to open the format selection page, then click a format to copy it.

### Method 2 — Open the extension directly

1. Open Command Palette (`Win + Alt + Space`)
2. Type `Discord Timestamp` and press **Enter**
3. Type your date and time in the search box
4. Click any format row to copy the code to your clipboard

### Accepted date/time formats

The extension uses .NET's `DateTime.TryParse`, so it accepts a wide range of natural inputs:

| Input | Parsed as |
|-------|-----------|
| `June 15 2025 3:00 PM` | June 15, 2025 at 3:00 PM |
| `2025-06-15 15:00` | June 15, 2025 at 3:00 PM |
| `6/15/2025 3pm` | June 15, 2025 at 3:00 PM |
| `3:00 PM` | Today at 3:00 PM |
| `tomorrow` | ❌ Not supported |

All times are interpreted in your **local timezone**.

---

## Development

### Project structure

```
DiscordTimestamp/
├── DiscordTimestamp.cs                  # IExtension entry point
├── DiscordTimestampCommandsProvider.cs  # Registers top-level commands and fallback
├── Program.cs                           # COM server host (scaffold-generated)
├── Pages/
│   ├── DiscordTimestampPage.cs          # ListPage showing all timestamp formats
│   └── DiscordTimestampFallback.cs      # Fallback handler for live search
└── Assets/
    └── ...                              # Extension icons
```

### Making changes

After editing any file:

1. **Build → Deploy DiscordTimestamp** in Visual Studio
2. Open Command Palette → type `Reload` → select **Reload Command Palette Extensions**
3. Test your changes

### SDK version

This extension was built against `Microsoft.CommandPalette.Extensions` version `0.9.260303001`. If you scaffold a new project and get a different version, the `FallbackCommandItem` constructor signature may differ — use **F12 (Go To Definition)** in Visual Studio to inspect the actual API of your installed version.

---

## Contributing

Pull requests are welcome. If you find a date format that fails to parse, or a Discord timestamp format that's missing, please open an issue.

---

## License

MIT
