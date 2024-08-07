using ECS.Components;
using ECS.Mark;
using Leopotam.Ecs;
using Logics.Displaying;
using UnityEngine;
using Utility;

namespace ECS.Systems.Indicators
{
    public class HealthIndicatorSystem : IEcsRunSystem
    {
        private EcsFilter<HealthComponent, SpawnMark> _spawnHealthFilter;
        private EcsFilter<HealthComponent, HealthIndicatorComponent, ViewComponent> _healthFilter;

        private readonly CoordinatesUtility _coordinatesUtility;
        private readonly HealthIndicatorManager _healthIndicatorManager;

        public HealthIndicatorSystem(CoordinatesUtility coordinatesUtility, HealthIndicatorManager healthIndicatorManager)
        {
            _coordinatesUtility = coordinatesUtility;
            _healthIndicatorManager = healthIndicatorManager;
        }

        public void Run()
        {
            foreach (var i in _spawnHealthFilter)
            {
                ref var entity = ref _spawnHealthFilter.GetEntity(i);

                ref var healthIndicator = ref entity.Get<HealthIndicatorComponent>();
                healthIndicator.Indicator = _healthIndicatorManager.GetWidget();
                healthIndicator.Indicator.Show();
            }

            foreach (var i in _healthFilter)
            {
                ref var entity = ref _healthFilter.GetEntity(i);
                ref var healthComponent = ref _healthFilter.Get1(i);
                ref var healthIndicatorComponent = ref _healthFilter.Get2(i);
                ref var viewComponent = ref _healthFilter.Get3(i);

                if (entity.Has<DiedMark>())
                {
                    _healthIndicatorManager.ReturnWidgetToPool(healthIndicatorComponent.Indicator);
                    continue;
                }

                healthIndicatorComponent.Indicator.UpdateHealth(healthComponent.Current, healthComponent.Max);

                Vector2 screenPosition = _coordinatesUtility.GetScreenPosition(viewComponent.View.SelfTransform.position);
                healthIndicatorComponent.Indicator.SetPosition(screenPosition + viewComponent.HealthWidgetOffset);

            }
        }
    }
}