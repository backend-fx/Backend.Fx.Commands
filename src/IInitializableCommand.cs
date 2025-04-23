using JetBrains.Annotations;

namespace Backend.Fx.Commands;

[PublicAPI]
public interface IInitializableCommand
{
    Func<IServiceProvider, CancellationToken, Task> InitializeAsync { get; }
}
