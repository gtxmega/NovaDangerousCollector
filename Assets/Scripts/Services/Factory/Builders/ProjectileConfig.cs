using Logics.Views;
using UnityEngine;

namespace Services.Factory.Builders
{
    [CreateAssetMenu(menuName = "Game/Projectiles/Projectile", fileName = "ProjectileConfig")]
    public class ProjectileConfig : ScriptableObject
    {
        [field: SerializeField] public ProjectileView ProjectilePrefab { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }

        public ProjectileBuilder GetBuilder()
        {
            return new ProjectileBuilder(this);
        }
    }
}