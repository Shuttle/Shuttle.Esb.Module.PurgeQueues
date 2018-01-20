using System;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Esb.Module.PurgeQueues
{
    public class PurgeQueuesModule
    {
        private readonly PurgeQueuesObserver _purgeQueuesObserver;
        private readonly string _startupPipelineName = typeof (StartupPipeline).FullName;

        public PurgeQueuesModule(IPipelineFactory pipelineFactory, PurgeQueuesObserver purgeQueuesObserver)
        {
            Guard.AgainstNull(pipelineFactory, nameof(pipelineFactory));
            Guard.AgainstNull(purgeQueuesObserver, nameof(purgeQueuesObserver));

            _purgeQueuesObserver = purgeQueuesObserver;

            pipelineFactory.PipelineCreated += PipelineCreated;
        }

        private void PipelineCreated(object sender, PipelineEventArgs e)
        {
            if (!(e.Pipeline.GetType().FullName ?? string.Empty)
                .Equals(_startupPipelineName, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            e.Pipeline.RegisterObserver(_purgeQueuesObserver);
        }
    }
}