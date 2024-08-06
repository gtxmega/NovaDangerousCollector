using Game.Types;
using System;

namespace Services.Events
{
    public interface IArtifactCastEventsUI
    {
        event Action<EArtifactSlotUI> ArtifactSlotClicked;
    }
}