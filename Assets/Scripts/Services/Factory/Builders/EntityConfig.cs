using Game.Types;
using Logics.Views;
using UnityEngine;

namespace Services.Factory.Builders
{
    public class EntityConfig : ScriptableObject
    {
        [field: Header("Health")]
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float CurrentHealth { get; private set; }
        [field: SerializeField] public float RegenerationHealth { get; private set; }

        [field: Header("View")]
        [field: SerializeField] public ActorView ActorView { get; private set; }

        [field: Header("Team")]
        [field: SerializeField] public ETeam Team { get; private set; }

        [field: Header("Enemy")]
        [field: SerializeField] public ETeam[] EnemyTeams { get; private set; }
        [field: SerializeField] public LayerMask EnemyLayers { get; private set; }

        public virtual EntityBuilder GetBuilder()
        {
            return new EntityBuilder(this);
        }
    }
}