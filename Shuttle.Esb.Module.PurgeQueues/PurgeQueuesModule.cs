using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.PurgeQueues
{
    public class PurgeQueuesModule
    {
        private readonly PurgeQueuesObserver _purgeQueuesObserver;
        private readonly string _startupPipelineName = typeof (StartupPipeline).FullName;

        public PurgeQueuesModule(IPipelineFactory pipelineFactory, PurgeQueuesObserver purgeQueuesObserver)
        {
            Guard.AgainstNull(pipelineFactory, "pipelineFactory");
            Guard.AgainstNull(purgeQueuesObserver, "purgeQueuesObserver");

            _purgeQueuesObserver = purgeQueuesObserver;

            pipelineFactory.PipelineCreated += PipelineCreated;
        }

        private void PipelineCreated(object sender, PipelineEventArgs e)
        {
            if (!e.Pipeline.GetType().FullName.Equals(_startupPipelineName, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            e.Pipeline.RegisterObserver(_purgeQueuesObserver);
        }
    }
}