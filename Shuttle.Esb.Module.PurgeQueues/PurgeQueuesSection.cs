using System.Configuration;

namespace Shuttle.Esb.Module.PurgeQueues
{
    public class PurgeQueuesSection : ConfigurationSection
    {
        [ConfigurationProperty("queues", IsRequired = true, DefaultValue = null)]
        public PurgeQueueElementCollection Queues => (PurgeQueueElementCollection) this["queues"];
    }
}