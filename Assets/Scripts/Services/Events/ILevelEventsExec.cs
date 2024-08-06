using Game.Types;
using Logics.Artifacts;
using Logics.Views;

namespace Services.Events
{
    public interface ILevelEventsExec
    {
        void OnLevelEnd(ELevelEndType type);
        void OnLevelLoaded();
        void OnLevelStart();
        void OnPlayerReceivesArtifact(Artifact artifact);
        void OnPlayerSpawnActor(PlayerView view);
    }
}