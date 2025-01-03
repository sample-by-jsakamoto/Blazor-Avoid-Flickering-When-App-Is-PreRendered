using Microsoft.AspNetCore.Components;

namespace BlazorWasmApp1;

public class ComponentStateItem<T>(string key, Func<ValueTask<T?>> getAsync) : IPersistableState
{
    private T? _instance;

    public async ValueTask<T?> GetAsync(PersistentComponentState persistent)
    {
        if (!persistent.TryTakeFromJson<T?>(key, out var restored))
        {
            this._instance = await getAsync();
        }
        else
        {
            this._instance = restored;
        }
        return this._instance;
    }

    public void PersistAsJson(PersistentComponentState persistent)
    {
        persistent.PersistAsJson(key, this._instance);
    }
}
