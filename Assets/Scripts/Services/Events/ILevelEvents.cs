using Game.Types;
using Logics.Artifacts;
using Logics.Views;
using System;

namespace Services.Events
{
    public interface ILevelEvents
    {
        event Action LevelStart;
        event Action<PlayerView> PlayerSpawnActor;
        event Action<ELevelEndType> LevelEnd;
        event Action<Artifact> PlayerReceivesArtifact;
        event Action LevelLoaded;
    }
}