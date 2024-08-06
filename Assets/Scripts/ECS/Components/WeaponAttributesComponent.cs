using Game.Types;

namespace ECS.Components
{
    public struct WeaponAttributesComponent
    {
        public EDamageType DamageType;
        public float Damage;
        public float RadiusOrder;

        public float Vampirism;
        public float CriticalChance;
        public float CriticalDamage;

        public float Reloads;
        public float ReloadsSpeed;
        public float ReloadsTimer;
    }
}