using Game.Types;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Constants/GameConstants", fileName = "GameConstants")]
    public class GameConstants : ScriptableObject
    {
        [field: SerializeField] public int FirstSceneIndex { get; private set; }
        [field: SerializeField] public int FrameRate { get; private set; } = 120;
        [field: SerializeField] public ETeam EnemyTeam { get; private set; }
        [field: SerializeField] public int EnemyBufferSize { get; private set; }
        [field: SerializeField] public int PoolSizeUI { get; private set; }
    }
}