using Game.Types;
using Logics.Views;
using Services.Events;
using Services.locator;
using UnityEngine;

namespace Levels
{
    public class LevelConditionEnd : MonoBehaviour, InjectDependency
    {
        private bool _enableTick = true;

        private int _playersCount;
        private int _actorsCount;

        private ILevelEventsExec _levelEventsExec;
        private ILevelEvents _levelEvents;

        public void Inject(IServicesLocator locator)
        {
            _levelEventsExec = locator.GetServices<ILevelEventsExec>();
            _levelEvents = locator.GetServices<ILevelEvents>();

            _levelEvents.ActorSpawn += OnActorSpawn;
            _levelEvents.PlayerSpawnActor += OnPlayerSpawnActor;

            _levelEvents.ActorDie += OnActorDie;
            _levelEvents.PlayerDie += OnPlayerDie;
        }

        private void OnPlayerDie()
        {
            if (_enableTick == false)
                return;

            --_playersCount;
            if (_playersCount <= 0)
            {
                _enableTick = false;
                _levelEventsExec.OnLevelEnd(ELevelEndType.LOSE);
            }
        }

        private void OnActorDie()
        {
            if (_enableTick == false)
                return;

            --_actorsCount;
            if(_actorsCount <= 0)
            {
                _enableTick = false;
                _levelEventsExec.OnLevelEnd(ELevelEndType.WIN);
            }
        }

        private void OnPlayerSpawnActor(PlayerView playerView)
        {
            ++_playersCount;
        }

        private void OnActorSpawn(ActorView actorView)
        {
            ++_actorsCount;
        }

        private void OnDestroy()
        {
            if (_levelEvents != null)
            {
                _levelEvents.ActorSpawn -= OnActorSpawn;
                _levelEvents.PlayerSpawnActor -= OnPlayerSpawnActor;

                _levelEvents.ActorDie -= OnActorDie;
                _levelEvents.PlayerDie -= OnPlayerDie;
            }
        }
    }
}