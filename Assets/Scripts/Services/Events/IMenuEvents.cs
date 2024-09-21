using System;

namespace Services.Events
{
    public interface IMenuEvents
    {
        event Action<int> PlayerStartWorld;
    }
}