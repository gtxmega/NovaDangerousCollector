using ECS.Components.Artifacts;
using Game;
using Game.Types;
using Leopotam.Ecs;
using Logics.Artifacts;
using Services.Events;
using Services.locator;
using UnityEngine;

namespace Logics.Displaying
{
    public class ArtifactsPanel : MonoBehaviour, InjectDependency
    {
        [SerializeField] private ArtifactUIWidget[] _artifactsWidget;

        private GameConstants _gameConstants;
        private ILevelEvents _levelEvents;

        public void Inject(IServicesLocator locator)
        {
            _gameConstants = locator.GetServices<GameConstants>();
            _levelEvents = locator.GetServices<ILevelEvents>();

            _levelEvents.PlayerReceivesArtifact += OnAddedArtifact;
        }

        private void OnAddedArtifact(Artifact artifact)
        {
            ArtifactUIWidget emptyWidget = GetEmptyWidget(artifact.ArtifactRare);
            if (emptyWidget != null)
            {
                ref var artifactEntity = ref artifact.GetEntity();
                ref var artifactWidgetComponent = ref artifactEntity.Get<ArtifactWidgetComponent>();

                artifactWidgetComponent.Widget = emptyWidget;
                artifact.SetUISlot(emptyWidget.GetArtifactSlotType());

                ArtifactDescription artifactDescription = artifact.Description;

                emptyWidget.SetRareImage(_gameConstants.RareSprites[(int)artifact.ArtifactRare]);
                emptyWidget.SetDisplayImage(artifactDescription.DisplayImage);
                emptyWidget.SetNotEmptySlot();
                emptyWidget.Show();
            }
        }

        private ArtifactUIWidget GetEmptyWidget(EArtifactRare artifactRare)
        {
            for (int i = 0; i < _artifactsWidget.Length; ++i)
            {
                if (_artifactsWidget[i].IsEmpty)
                {
                    EArtifactRare[] slotRare = _artifactsWidget[i].GetSlotRare();

                    for (int j = 0; j < slotRare.Length; ++j)
                    {
                        if (slotRare[j] == artifactRare)
                        {
                            return _artifactsWidget[i];
                        }
                    }
                }
            }

            return null;
        }

        private void OnDestroy()
        {
            if (_levelEvents != null)
            {
                _levelEvents.PlayerReceivesArtifact -= OnAddedArtifact;
            }
        }
    }
}