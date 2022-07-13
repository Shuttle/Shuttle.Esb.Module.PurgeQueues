using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.Module.PurgeQueues
{
    public class PurgeQueuesObserver : IPipelineObserver<OnAfterConfigureQueues>
    {
        private readonly PurgeQueuesOptions _purgeQueuesOptions;
        private readonly IQueueService _queueService;

        public PurgeQueuesObserver(IOptions<PurgeQueuesOptions> purgeQueuesOptions, IQueueService queueService)
        {
            Guard.AgainstNull(purgeQueuesOptions, nameof(purgeQueuesOptions));
            Guard.AgainstNull(purgeQueuesOptions.Value, nameof(purgeQueuesOptions.Value));
            Guard.AgainstNull(queueService, nameof(queueService));

            _purgeQueuesOptions = purgeQueuesOptions.Value;
            _queueService = queueService;
        }

        public void Execute(OnAfterConfigureQueues pipelineEvent)
        {
            foreach (var uri in _purgeQueuesOptions.Uris)
            {
                var queue = _queueService.Get(uri);
                var purge = queue as IPurgeQueue;

                if (purge == null)
                {
                    continue;
                }

                purge.Purge();
            }
        }
    }
}