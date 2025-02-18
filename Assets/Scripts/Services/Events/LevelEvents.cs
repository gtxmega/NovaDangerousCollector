using Game.Types;
using Logics.Artifacts;
using Logics.Views;
using System;

namespace Services.Events
{
    public class LevelEvents : ILevelEvents, ILevelEventsExec
    {
        #region Level

        public event Action LevelLoaded;
        public void OnLevelLoaded() => LevelLoaded?.Invoke();

        public event Action LevelStart;
        public void OnLevelStart() => LevelStart?.Invoke();


        public event Action<ELevelEndType> LevelEnd;
        public void OnLevelEnd(ELevelEndType type) => LevelEnd?.Invoke(type);

        #endregion

        #region Actors

        public event Action<PlayerView> PlayerSpawnActor;
        public void OnPlayerSpawnActor(PlayerView view) => PlayerSpawnActor?.Invoke(view);

        public event Action<ActorView> ActorSpawn;
        public void OnActorSpawn(ActorView view) => ActorSpawn?.Invoke(view);

        public event Action PlayerDie;
        public void OnPlayerDie() => PlayerDie?.Invoke();

        public event Action ActorDie;
        public void OnActorDie() => ActorDie?.Invoke();

        #endregion

        #region Artifacts

        public event Action<Artifact> PlayerReceivesArtifact;
        public void OnPlayerReceivesArtifact(Artifact artifact) => PlayerReceivesArtifact?.Invoke(artifact);

        #endregion
    }
}