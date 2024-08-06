using Game.Types;
using System;

namespace Services.Events
{
    public class ArtifactCastEventsUI : IArtifactCastEventsUI, IArtifactCastEventsUIExec
    {
        public event Action<EArtifactSlotUI> ArtifactSlotClicked;

        public void OnArtifactSlotClicked(EArtifactSlotUI artifactSlot) => ArtifactSlotClicked?.Invoke(artifactSlot);
    }
}