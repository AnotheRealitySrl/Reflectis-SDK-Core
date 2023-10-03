using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Reflectis.SDK.Transitions
{
    public class MultipleTransitionProvider : AbstractTransitionProvider
    {
        public enum SyncronizationMethod
        {
            Syncronous = 1, Asyncronous = 2,
        }

        [SerializeField, Tooltip("List of all transition providers. If the synchronization method is synchronous it is raccomended to " +
            "put in the last index the provider which is aspected to take the most time. " +
            "If the synchronization method is asynchronous the providers wil be played in order.")]
        private List<AbstractTransitionProvider> providers = new List<AbstractTransitionProvider>();
        [SerializeField]
        private SyncronizationMethod syncronizationMethod = SyncronizationMethod.Syncronous;

        public override async Task EnterTransitionAsync()
        {
            onEnterTransitionStart?.Invoke();

            switch(syncronizationMethod)
            {
                case SyncronizationMethod.Syncronous:
                    await EnterAllTransitionsSyncronously();
                    break;
                case SyncronizationMethod.Asyncronous:
                    await EnterAllTransitionsAsyncronously();
                    break;
            }
            OnEnterTransitionFinish?.Invoke();
        }

        private async Task EnterAllTransitionsAsyncronously()
        {
            foreach(AbstractTransitionProvider provider in providers)
            {
                await provider.DoTransitionAsync(true);
            }
        }

        private async Task EnterAllTransitionsSyncronously()
        {
            IEnumerable<Task> providerEnterTask = providers.Select(async provider =>
            {
                await provider.DoTransitionAsync(true);
            });

            await Task.WhenAll(providerEnterTask);
        }

        public override async Task ExitTransitionAsync()
        {
            OnExitTransitionStart?.Invoke();

            switch (syncronizationMethod)
            {
                case SyncronizationMethod.Syncronous:
                    await ExitAllTransitionsSyncronously();
                    break;
                case SyncronizationMethod.Asyncronous:
                    await ExitAllTransitionsAsyncronously();
                    break;
            }

            onExitTransitionFinish?.Invoke();
        }


        private async Task ExitAllTransitionsAsyncronously()
        {
            for (int i = providers.Count - 1; i >= 0; i--)
            {
                await providers[i].DoTransitionAsync(false);
            }
        }

        private async Task ExitAllTransitionsSyncronously()
        {
            IEnumerable<Task> providerExitTask = providers.Select(async provider =>
            {
                await provider.DoTransitionAsync(false);
            });

            await Task.WhenAll(providerExitTask);
        }

    }
}
