using Services.Factory.Builders;
using UnityEngine;

namespace Logics.Actors
{
    [CreateAssetMenu(menuName = "Game/Actors/Npc", fileName = "NpcActorConfig")]
    public class NpcActorConfig : EntityConfig
    {
        [field: SerializeField] public float Damage { get; internal set; }
        [field: SerializeField] public float Vampirism { get; internal set; }
        [field: SerializeField] public float CriticalChance { get; internal set; }
        [field: SerializeField] public float CriticalDamage { get; internal set; }
        [field: SerializeField] public float AttackReloadsSpeed { get; internal set; }
        [field: SerializeField] public float Armor { get; private set; }
        [field: SerializeField] public float BasePhysicResistance { get; private set; }
        [field: SerializeField] public float BaseMagicResistance { get; private set; }

        public override EntityBuilder GetBuilder()
        {
            return new NpcActorBuilder(this);
        }
    }
}