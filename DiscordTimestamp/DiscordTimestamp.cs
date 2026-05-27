using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace DiscordTimestamp;

[Guid("cd4dc595-9607-414e-9c6e-549279165e7b")]
public sealed partial class DiscordTimestamp : IExtension, IDisposable
{
    private readonly ManualResetEvent _extensionDisposedEvent;

    public DiscordTimestamp(ManualResetEvent extensionDisposedEvent)
    {
        _extensionDisposedEvent = extensionDisposedEvent;
    }

    public object? GetProvider(ProviderType providerType)
    {
        return providerType switch
        {
            ProviderType.Commands => new DiscordTimestampCommandsProvider(),
            _ => null,
        };
    }

    public void Dispose()
    {
        _extensionDisposedEvent.Set();
    }
}