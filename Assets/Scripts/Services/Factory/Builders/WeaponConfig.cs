using Game.Types;
using Logics.Views;
using UnityEngine;

namespace Services.Factory.Builders
{
    [CreateAssetMenu(menuName = "Game/Weapons/Weapon", fileName = "WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public EAttackType AttackType { get; private set; }
        [field: SerializeField] public EDamageType DamageType { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Vampirism { get; private set; }
        [field: SerializeField] public float CriticalChance { get; private set; }
        [field: SerializeField] public float CriticalDamage { get; private set; }
        [field: SerializeField] public float Reloads { get; private set; }
        [field: SerializeField] public float RadiusOrder { get; private set; }
        [field: SerializeField] public float ReloadsSpeed { get; private set; }
        [field: SerializeField] public ProjectileConfig ProjectileConfig { get; private set; }
        [field: SerializeField] public ProjectileConfig CriticalProjectileConfig { get; private set; }
        [field: SerializeField] public WeaponView WeaponPrefab { get; private set; }

        public virtual WeaponBuilder GetBuilder() => new WeaponBuilder(this);
    }
}