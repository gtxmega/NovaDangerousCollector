using Game.Types;

namespace Logics.Artifacts
{
    public interface IArtifactDisplaySettings
    {
        void SetDescription(ArtifactDescription description);
        void SetUISlot(EArtifactSlotUI uiSlot);
    }
}