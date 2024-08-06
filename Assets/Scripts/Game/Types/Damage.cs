using Leopotam.Ecs;

namespace Game.Types
{
    public struct Damage
    {
        public float Amount;
        public EDamageType Type;
        public EcsEntity Instigator;
    }
}