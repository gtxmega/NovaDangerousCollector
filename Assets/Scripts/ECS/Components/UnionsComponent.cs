using Game.Types;
using UnityEngine;

namespace ECS.Components
{
    public struct UnionsComponent
    {
        public ETeam[] EnemyTeams;
        public LayerMask EnemyLayers;
    }
}