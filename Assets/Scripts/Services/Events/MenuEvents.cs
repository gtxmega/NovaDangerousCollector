using System;

namespace Services.Events
{
    public class MenuEvents : IMenuEvents, IMenuEventsExec
    {
        public event Action<int> PlayerStartWorld;
        public void OnPlayerStartWorld(int levelIndex) => PlayerStartWorld?.Invoke(levelIndex);
    }
}