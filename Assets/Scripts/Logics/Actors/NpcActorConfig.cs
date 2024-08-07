using Services.Factory.Builders;
using UnityEngine;

namespace Logics.Actors
{
    [CreateAssetMenu(menuName = "Game/Actors/Npc", fileName = "NpcActorConfig")]
    public class NpcActorConfig : EntityConfig
    {
        [field: SerializeField] public float Damage { get; internal set; }
        [field: SerializeField][field: Range(0.0f, 1.0f)] public float Vampirism { get; internal set; }
        [field: SerializeField][field: Range(0.0f, 1.0f)] public float CriticalChance { get; internal set; }
        [field: SerializeField] public float CriticalDamage { get; internal set; }
        [field: SerializeField] public float AttackReloadsSpeed { get; internal set; }
        [field: SerializeField] public float Armor { get; private set; }
        [field: SerializeField][field: Range(0.0f, 1.0f)] public float BasePhysicResistance { get; private set; }
        [field: SerializeField][field: Range(0.0f, 1.0f)] public float BaseMagicResistance { get; private set; }

        public override EntityBuilder GetBuilder()
        {
            return new NpcActorBuilder(this);
        }
    }
}