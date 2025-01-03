using System.Collections.Concurrent;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmApp1;

public class ComponentStatePersister : IComponentStatePersister
{
    private readonly PersistentComponentState _persistentState;

    private readonly PersistingComponentStateSubscription _subscription;

    private readonly ConcurrentDictionary<string, IPersistableState> _stateItems = new();

    public ComponentStatePersister(PersistentComponentState persistentState)
    {
        this._persistentState = persistentState;
        this._subscription = this._persistentState.RegisterOnPersisting(this.OnPersisting);
    }

    public async ValueTask<T?> GetAsync<T>(string key, Func<ValueTask<T?>> getAsync)
    {
        var stateItem = this._stateItems.GetOrAdd(key, _ => new ComponentStateItem<T>(key, getAsync)) as ComponentStateItem<T>;
        return await stateItem!.GetAsync(this._persistentState);
    }

    private Task OnPersisting()
    {
        foreach (var stateItem in this._stateItems.Values)
        {
            stateItem.PersistAsJson(this._persistentState);
        }
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        this._subscription.Dispose();
    }
}
