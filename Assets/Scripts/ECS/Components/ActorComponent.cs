using Logics.Views;
using UnityEngine;

namespace ECS.Components
{
    public struct ActorComponent
    {
        public Vector3 SpawnPosition;
        public ActorView View;
    }
}