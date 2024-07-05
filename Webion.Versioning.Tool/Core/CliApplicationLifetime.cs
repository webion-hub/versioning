using System.Runtime.InteropServices;

namespace Webion.Versioning.Tool.Core;

public sealed class CliApplicationLifetime : ICliApplicationLifetime, IDisposable
{
    private readonly CancellationTokenSource _cts = new();
    private readonly PosixSignalRegistration _sigInt;
    private readonly PosixSignalRegistration _sigQuit;
    private readonly PosixSignalRegistration _sigTerm;

    public CancellationToken CancellationToken => _cts.Token;


    public CliApplicationLifetime()
    {
        _sigInt = PosixSignalRegistration.Create(PosixSignal.SIGINT, OnSignal);
        _sigQuit = PosixSignalRegistration.Create(PosixSignal.SIGQUIT, OnSignal);
        _sigTerm = PosixSignalRegistration.Create(PosixSignal.SIGTERM, OnSignal);
    }
    
    private void OnSignal(PosixSignalContext context)
    {
        context.Cancel = true;
        _cts.Cancel();
    }

    public void Dispose()
    {
        _sigInt.Dispose();
        _sigQuit.Dispose();
        _sigTerm.Dispose();
        _cts.Dispose();
    }
}