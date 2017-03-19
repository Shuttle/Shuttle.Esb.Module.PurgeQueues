using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.PurgeQueues
{
	public class PurgeQueuesObserver : IPipelineObserver<OnAfterConfigureQueueManager>
	{
	    private readonly IQueueManager _queueManager;
	    private readonly ILog _log;

		public PurgeQueuesObserver(IQueueManager queueManager)
		{
            Guard.AgainstNull(queueManager, "queueManager");

		    _queueManager = queueManager;
		    _log = Log.For(this);
		}

	    public void Execute(OnAfterConfigureQueueManager pipelineEvent)
		{
			var section = ConfigurationSectionProvider.Open<PurgeQueuesSection>("shuttle", "purgeQueues");

			if (section == null || section.Queues == null)
			{
				return;
			}

			foreach (PurgeQueueElement queueElement in section.Queues)
			{
				var queue = _queueManager.GetQueue(queueElement.Uri);
				var purge = queue as IPurgeQueue;

				if (purge == null)
				{
					_log.Warning(string.Format(PurgeQueuesResources.IPurgeQueueNotImplemented, queue.GetType().FullName));

					continue;
				}

				purge.Purge();
			}
		}
	}
}