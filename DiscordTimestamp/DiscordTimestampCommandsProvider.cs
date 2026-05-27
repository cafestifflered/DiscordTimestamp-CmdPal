using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace DiscordTimestamp;

internal sealed partial class DiscordTimestampCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;
    private readonly DiscordTimestampFallback _fallback = new();

    public DiscordTimestampCommandsProvider()
    {
        DisplayName = "Discord Timestamp";
        Icon = new IconInfo("\uE787");

        _commands = [
            new CommandItem(new DiscordTimestampPage())
            {
                Title = "Discord Timestamp",
                Subtitle = "Generate a Discord timestamp code from a date and time",
            },
        ];
    }

    public override ICommandItem[] TopLevelCommands() => _commands;

    public override IFallbackCommandItem[] FallbackCommands() => [_fallback];
}