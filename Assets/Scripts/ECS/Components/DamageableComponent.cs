using Game.Types;
using System.Collections.Generic;

namespace ECS.Components
{
    public struct DamageableComponent
    {
        public Queue<Damage> DamageQueue;
    }
}