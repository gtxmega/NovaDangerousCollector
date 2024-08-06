using Game.Types;

namespace Services.Events
{
    public interface IArtifactCastEventsUIExec
    {
        void OnArtifactSlotClicked(EArtifactSlotUI artifactSlot);
    }
}