using System.Collections.Generic;

namespace Shuttle.Esb.Module.PurgeQueues
{
    public class PurgeQueuesOptions
    {
        public const string SectionName = "Shuttle:ServiceBus:Modules:PurgeQueues";

        public List<string> Uris { get; set; } = new List<string>();
    }
}