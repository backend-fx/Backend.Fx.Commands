using JetBrains.Annotations;

namespace Backend.Fx.Commands;

[PublicAPI]
public interface ICommand
{
    Func<IServiceProvider, CancellationToken, Task> AsyncInvocation { get; }
}