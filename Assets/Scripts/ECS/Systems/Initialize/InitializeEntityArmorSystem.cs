using ECS.Components.Attributes;
using ECS.Mark;
using Leopotam.Ecs;

namespace ECS.Systems.Initialize
{
    public class InitializeEntityArmorSystem : IEcsRunSystem
    {
        private EcsFilter<ArmorComponent, GeneralAttributes, SpawnMark> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {

            }
        }
    }
}