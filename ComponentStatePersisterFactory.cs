using Microsoft.AspNetCore.Components;

namespace BlazorWasmApp1;

public class ComponentStatePersisterFactory(PersistentComponentState persistent)
{
    public IComponentStatePersister Create() => new ComponentStatePersister(persistent);
}
