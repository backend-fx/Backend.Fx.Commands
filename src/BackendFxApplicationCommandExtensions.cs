using System.Security.Principal;
using Backend.Fx.Execution;
using Backend.Fx.Execution.Pipeline;
using JetBrains.Annotations;

namespace Backend.Fx.Commands;

[PublicAPI]
public static class BackendFxApplicationCommandExtensions
{
    public static async Task Execute(
        this IBackendFxApplication application,
        ICommand command,
        IIdentity? identity = null,
        CancellationToken cancellation = default)
    {
        await application.Invoker.InvokeAsync(
            async (sp, ct) =>
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                if (command is IInitializableCommand initializableCommand)
                {
                    await initializableCommand.InitializeAsync(sp, ct).ConfigureAwait(false);
                }

                // ReSharper disable once SuspiciousTypeConversion.Global
                if (command is IAuthorizedCommand authorizedCommand)
                {
                    await authorizedCommand.AuthorizeAsync(sp, ct).ConfigureAwait(false);
                }

                await command.AsyncInvocation.Invoke(sp, ct).ConfigureAwait(false);
            },
            identity ?? new AnonymousIdentity(), cancellation).ConfigureAwait(false);
    }
}