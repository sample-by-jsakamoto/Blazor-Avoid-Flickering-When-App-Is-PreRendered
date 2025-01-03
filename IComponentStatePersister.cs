namespace BlazorWasmApp1;

public interface IComponentStatePersister : IDisposable
{
    ValueTask<T?> GetAsync<T>(string key, Func<ValueTask<T?>> getAsync);
}