using Game.Types;
using Logics.Artifacts;
using Logics.Views;

namespace Services.Events
{
    public interface ILevelEventsExec
    {
        void OnActorDie();
        void OnActorSpawn(ActorView view);
        void OnLevelEnd(ELevelEndType type);
        void OnLevelLoaded();
        void OnLevelStart();
        void OnPlayerDie();
        void OnPlayerReceivesArtifact(Artifact artifact);
        void OnPlayerSpawnActor(PlayerView view);
    }
}