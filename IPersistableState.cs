using Microsoft.AspNetCore.Components;

namespace BlazorWasmApp1;

public interface IPersistableState
{
    void PersistAsJson(PersistentComponentState persistent);
}