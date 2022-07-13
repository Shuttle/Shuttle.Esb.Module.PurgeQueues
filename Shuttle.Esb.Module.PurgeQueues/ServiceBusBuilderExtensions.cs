using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.Module.PurgeQueues
{
    public static class ServiceBusBuilderExtensions
    {
        public static ServiceBusBuilder AddPurgeQueuesModule(this ServiceBusBuilder serviceBusBuilder,
            Action<PurgeQueuesBuilder> builder = null)
        {
            Guard.AgainstNull(serviceBusBuilder, nameof(serviceBusBuilder));

            var purgeQueuesBuilder = new PurgeQueuesBuilder(serviceBusBuilder.Services);

            builder?.Invoke(purgeQueuesBuilder);

            serviceBusBuilder.Services.TryAddSingleton<PurgeQueuesModule, PurgeQueuesModule>();
            serviceBusBuilder.Services.TryAddSingleton<PurgeQueuesObserver, PurgeQueuesObserver>();

            serviceBusBuilder.Services.AddOptions<PurgeQueuesOptions>().Configure(options =>
            {
                options.Uris = new List<string>(purgeQueuesBuilder.Options.Uris ?? Enumerable.Empty<string>());
            });

            serviceBusBuilder.AddModule<PurgeQueuesModule>();

            return serviceBusBuilder;
        }
    }
}