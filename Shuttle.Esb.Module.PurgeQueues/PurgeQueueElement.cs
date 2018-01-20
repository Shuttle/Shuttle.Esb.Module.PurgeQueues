using System.Configuration;

namespace Shuttle.Esb.Module.PurgeQueues
{
    public class PurgeQueueElement : ConfigurationElement
    {
        [ConfigurationProperty("uri", IsRequired = true)]
        public string Uri => (string) this["uri"];
    }
}