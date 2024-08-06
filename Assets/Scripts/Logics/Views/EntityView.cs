using Game.Types;
using Leopotam.Ecs;
using UnityEngine;

namespace Logics.Views
{
    public class EntityView : MonoBehaviour
    {
        [field: SerializeField] public Transform SelfTransform { get; private set; }
        [field: SerializeField] public ETeam Team { get; private set; }

        protected EcsEntity _entity;
        protected EcsWorld _world;

        public void Init(in EcsEntity entity, EcsWorld world)
        {
            _entity = entity;
            _world = world;
        }

        public void SetTeam(ETeam team) => Team = team;
        public ref EcsEntity GetEntity() => ref _entity;
    }
}