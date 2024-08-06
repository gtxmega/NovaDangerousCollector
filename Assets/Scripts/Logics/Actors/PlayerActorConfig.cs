using Services.Factory.Builders;
using UnityEngine;

namespace Logics.Actors
{
    [CreateAssetMenu(menuName = "Game/Actors/PlayerActor", fileName = "PlayerActorConfig")]
    public class PlayerActorConfig : EntityConfig
    {
        [field: Header("Damage")]
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float CriticalChance { get; private set; }
        [field: SerializeField] public float CriticalDamage { get; private set; }
        [field: SerializeField] public float Vampirism { get; private set; }
        [field: SerializeField] public float AttackReloadsSpeed { get; private set; }

        [field: Header("Armor")]
        [field: SerializeField] public int Armor { get; private set; }

        [field: Header("Movement")]
        [field: SerializeField] public float MoveSpeed { get; private set; }

        public override EntityBuilder GetBuilder()
        {
            return new PlayerActorBuilder(this);
        }
    }
}