using Shuttle.Core.Container;
using Shuttle.Core.Contract;

namespace Shuttle.Esb.Module.PurgeQueues
{
	public static class ComponentRegistryExtensions
	{
		public static void RegisterPurgeQueues(this IComponentRegistry registry)
		{
			Guard.AgainstNull(registry, nameof(registry));

			registry.AttemptRegister<PurgeQueuesModule>();
			registry.AttemptRegister<PurgeQueuesObserver>();
		}
	}
}