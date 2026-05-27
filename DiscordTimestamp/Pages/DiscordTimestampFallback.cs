using Microsoft.CommandPalette.Extensions.Toolkit;
using System;

namespace DiscordTimestamp;

internal sealed partial class DiscordTimestampFallback : FallbackCommandItem
{
    private readonly DiscordTimestampPage _page = new();

    public DiscordTimestampFallback()
        : base(new DiscordTimestampPage(), "Discord Timestamp", "discord-timestamp-fallback")
    {
        Icon = new IconInfo("\uE787");
        Title = "Discord Timestamp";
        Subtitle = "Type a date/time to generate a Discord timestamp";
    }

    public override void UpdateQuery(string query)
    {
        if (Command is DiscordTimestampPage page)
        {
            page.SearchText = query;
            page.RefreshItems();
        }

        if (string.IsNullOrWhiteSpace(query))
        {
            Title = "Discord Timestamp";
            Subtitle = "Type a date/time to generate a Discord timestamp";
        }
        else if (DateTime.TryParse(query, out _))
        {
            Title = $"Discord Timestamp: {query}";
            Subtitle = "Press Enter to see format options";
        }
        else
        {
            Title = "Discord Timestamp";
            Subtitle = $"Can't parse '{query}' — try: June 15 2025 3:00 PM";
        }
    }
}