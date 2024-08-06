using Game.Types;
using Leopotam.Ecs;
using Logics.Views;
using Services.Events;
using Services.locator;
using System.Collections.Generic;
using UnityEngine;

namespace Logics.Artifacts
{
    public class ArtifactsActivator : MonoBehaviour, InjectDependency
    {
        private EcsEntity _playerEntity;
        private List<Artifact> _artifacts = new();

        private ILevelEvents _levelEvents;
        private IArtifactCastEventsUI _artifactCastEvent;

        public void Inject(IServicesLocator locator)
        {
            _levelEvents = locator.GetServices<ILevelEvents>();
            _artifactCastEvent = locator.GetServices<IArtifactCastEventsUI>();

            _levelEvents.PlayerSpawnActor += OnPlayerSpawnActor;
            _levelEvents.PlayerReceivesArtifact += OnPlayerReceivesArtifact;

            _artifactCastEvent.ArtifactSlotClicked += OnArtifactSlotClicked;
        }

        private void OnPlayerSpawnActor(PlayerView playerView) => _playerEntity = playerView.GetEntity();

        private void OnPlayerReceivesArtifact(Artifact artifact) => _artifacts.Add(artifact);

        private void OnArtifactSlotClicked(EArtifactSlotUI artifactSlotUI)
        {
            for (int i = 0; i < _artifacts.Count; ++i)
            {
                if (_artifacts[i].UISlotType == artifactSlotUI)
                {
                    _artifacts[i].ApplyActiveBonusTo(_playerEntity);
                    return;
                }
            }
        }

        private void OnDestroy()
        {
            if (_levelEvents != null)
            {
                _levelEvents.PlayerReceivesArtifact -= OnPlayerReceivesArtifact;
                _levelEvents.PlayerSpawnActor -= OnPlayerSpawnActor;
            }

            if (_artifactCastEvent != null)
            {
                _artifactCastEvent.ArtifactSlotClicked -= OnArtifactSlotClicked;
            }
        }
    }
}