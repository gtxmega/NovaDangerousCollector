using Game.Types;
using Logics.Views;
using System.Text;
using UnityEngine;

namespace Services.Factory.Builders
{
    [CreateAssetMenu(menuName = "Game/Weapons/Weapon", fileName = "WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [field: Header("Description")]
        [field: SerializeField] public string WeaponName { get; private set; }
        [field: SerializeField] public string PlanetName { get; private set; }
        [field: SerializeField] public Sprite DisplayImage { get; private set; }
        [field: SerializeField] public EArtifactRare WeaponRare { get; private set; }

        [field: Header("Main attributes")]
        [field: SerializeField] public EAttackType AttackType { get; private set; }
        [field: SerializeField] public EDamageType DamageType { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField][field: Range(0.0f, 1.0f)] public float Vampirism { get; private set; }
        [field: SerializeField][field: Range(0.0f, 1.0f)] public float CriticalChance { get; private set; }
        [field: SerializeField] public float CriticalDamage { get; private set; }
        [field: SerializeField] public float Reloads { get; private set; }
        [field: SerializeField] public float RadiusOrder { get; private set; }
        [field: SerializeField] public float ReloadsSpeed { get; private set; }

        [field: Header("Mesh")]
        [field: SerializeField] public ProjectileConfig ProjectileConfig { get; private set; }
        [field: SerializeField] public ProjectileConfig CriticalProjectileConfig { get; private set; }
        [field: SerializeField] public WeaponView WeaponPrefab { get; private set; }

        private string _weaponAttributeTextCached = string.Empty;

        public virtual string GetAttributeString()
        {
            if (_weaponAttributeTextCached == string.Empty)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("  Attack type: " + AttackType.ToString());
                sb.AppendLine("  Damage type: " + DamageType.ToString());
                sb.AppendLine("  Damage: " + Damage);
                sb.AppendLine("  Vampirism: " + (Vampirism * 100.0f) + "%");
                sb.AppendLine("  Critical chance: " + (CriticalChance * 100.0f) + "%");
                sb.AppendLine("  Critical damage: " + CriticalDamage + "x");
                sb.AppendLine("  Reloads: " + Reloads);
                sb.AppendLine("  Reloads speed: " + ReloadsSpeed);
                sb.AppendLine("  Radius order: " + RadiusOrder);

                _weaponAttributeTextCached = sb.ToString();
            }

            return _weaponAttributeTextCached;
        }

        public virtual string GetUniqueAttributeString()
        {
            return string.Empty;
        }

        public virtual WeaponBuilder GetBuilder() => new WeaponBuilder(this);
    }
}