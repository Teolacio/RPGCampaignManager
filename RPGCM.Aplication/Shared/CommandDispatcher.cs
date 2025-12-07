using RPGCM.Aplication.Interfaces;

namespace RPGCM.Aplication.Shared
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));

            var handler = _serviceProvider.GetService(handlerType);
            if(handler == null)
                throw new InvalidOperationException($"Handler não encontrado para {command.GetType().Name}");

            var method = handlerType.GetMethod("HandleAsync");
            if(method == null)
                throw new InvalidOperationException("Handler não implementa HandleAsync");

            var task = (Task<TResult>)method.Invoke(handler, new object[] { command, cancellationToken })!;
            return await task;
        }
    }
}
