using Game.Types;
using Services.Events;
using Services.locator;
using UnityEngine;

namespace Levels.Screens
{
    public class LevelEndScreen : MonoBehaviour, InjectDependency
    {
        [SerializeField] private Canvas _screenCanvas;
        [SerializeField] private ELevelEndType _levelEndType;

        private ILevelEvents _levelEvents;

        public void Inject(IServicesLocator locator)
        {
            _levelEvents = locator.GetServices<ILevelEvents>();

            _levelEvents.LevelEnd += OnLevelEnd;
        }

        private void OnLevelEnd(ELevelEndType endType)
        {
            if (_levelEndType == endType)
            {
                OnEventExecute();
            }
        }

        protected virtual void OnEventExecute()
        {

        }

        private void OnDestroy()
        {
            if (_levelEvents != null)
            {
                _levelEvents.LevelEnd -= OnLevelEnd;
            }
        }
    }
}