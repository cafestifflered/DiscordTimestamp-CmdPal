using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Globalization;

namespace DiscordTimestamp;

internal sealed partial class DiscordTimestampPage : ListPage
{
    public DiscordTimestampPage()
    {
        Icon = new IconInfo("\uE787");
        Title = "Discord Timestamp";
        Name = "Open";
        ShowDetails = false;

        PropChanged += (sender, args) =>
        {
            if (args.PropertyName == "SearchText")
            {
                RefreshItems();
            }
        };
    }

    public void RefreshItems()
    {
        RaiseItemsChanged(0);
    }

    public override IListItem[] GetItems()
    {
        string query = (SearchText ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(query))
        {
            return [
                new ListItem(new NoOpCommand())
                {
                    Title = "Type a date and time above",
                    Subtitle = "Example: June 15 2025 3:00 PM  or  2025-06-15 15:00",
                }
            ];
        }

        if (!DateTime.TryParse(query, out DateTime parsedDate))
        {
            return [
                new ListItem(new NoOpCommand())
                {
                    Title = $"Couldn't parse: '{query}'",
                    Subtitle = "Try: 'June 15 2025 3:00 PM' or '2025-06-15 15:00'",
                }
            ];
        }

        long unix = ((DateTimeOffset)parsedDate).ToUnixTimeSeconds();
        var inv = CultureInfo.InvariantCulture;

        return [
            MakeItem(unix, "t", "Short Time",            parsedDate.ToString("h:mm tt",                    inv)),
            MakeItem(unix, "T", "Long Time",             parsedDate.ToString("h:mm:ss tt",                 inv)),
            MakeItem(unix, "d", "Short Date",            parsedDate.ToString("M/d/yyyy",                   inv)),
            MakeItem(unix, "D", "Long Date",             parsedDate.ToString("MMMM d, yyyy",               inv)),
            MakeItem(unix, "f", "Short Date/Time",       parsedDate.ToString("MMMM d, yyyy h:mm tt",       inv)),
            MakeItem(unix, "F", "Long Date/Time",        parsedDate.ToString("dddd, MMMM d, yyyy h:mm tt", inv)),
            MakeItem(unix, "s", "Short Date/Time (alt)", parsedDate.ToString("M/d/yyyy h:mm tt",           inv)),
            MakeItem(unix, "S", "Long Date/Time (alt)",  parsedDate.ToString("M/d/yyyy h:mm:ss tt",        inv)),
            MakeItem(unix, "R", "Relative",              GetRelativeTime(parsedDate)),
            new ListItem(new CopyTextCommand(unix.ToString(inv)))
            {
                Title = unix.ToString(inv),
                Subtitle = "Raw Unix Timestamp",
            },
        ];
    }

    private static ListItem MakeItem(long unix, string flag, string label, string preview)
    {
        string code = $"<t:{unix}:{flag}>";
        return new ListItem(new CopyTextCommand(code))
        {
            Title = code,
            Subtitle = $"{label}  →  {preview}",
        };
    }

    private static string GetRelativeTime(DateTime dt)
    {
        var span = (dt - DateTime.Now).Duration();
        string dir = dt > DateTime.Now ? "from now" : "ago";
        if (span.TotalSeconds < 60) return $"{(int)span.TotalSeconds} seconds {dir}";
        if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes} minutes {dir}";
        if (span.TotalHours < 24) return $"{(int)span.TotalHours} hours {dir}";
        if (span.TotalDays < 30) return $"{(int)span.TotalDays} days {dir}";
        if (span.TotalDays < 365) return $"{(int)(span.TotalDays / 30)} months {dir}";
        return $"{(int)(span.TotalDays / 365)} years {dir}";
    }
}