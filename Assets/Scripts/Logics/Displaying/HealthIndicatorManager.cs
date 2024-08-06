using Services.Events;
using Services.locator;
using System.Collections.Generic;
using UnityEngine;

namespace Logics.Displaying
{
    public class HealthIndicatorManager : MonoBehaviour, InjectDependency
    {
        [SerializeField] private RectTransform _widgetContainer;
        [SerializeField] private HealthIndicator _widgetPrefab;

        private const int WIDGET_POOL_SIZE = 20;
        private Queue<HealthIndicator> _widgetsPool = new();

        private ILevelEvents _levelEvents;

        public void Inject(IServicesLocator locator)
        {
            _levelEvents = locator.GetServices<ILevelEvents>();
            _levelEvents.LevelStart += OnLevelStart;
        }

        private void OnLevelStart()
        {
            CreateWidgetPool();
        }

        public HealthIndicator GetWidget()
        {
            if (_widgetsPool.Count == 0)
            {
                AddWidgetToPool();
            }

            return _widgetsPool.Dequeue();
        }

        public void ReturnWidgetToPool(HealthIndicator widget)
        {
            widget.Hide();
            _widgetsPool.Enqueue(widget);
        }

        private void CreateWidgetPool()
        {
            for (int i = 0; i < WIDGET_POOL_SIZE; ++i)
            {
                AddWidgetToPool();
            }
        }

        private void AddWidgetToPool()
        {
            HealthIndicator widgetInstance = Instantiate(_widgetPrefab, _widgetContainer);
            widgetInstance.Hide();

            _widgetsPool.Enqueue(widgetInstance);
        }

        private void OnDestroy()
        {
            if (_levelEvents != null)
            {
                _levelEvents.LevelStart -= OnLevelStart;
            }
        }
    }
}