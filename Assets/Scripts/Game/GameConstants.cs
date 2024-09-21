using Game.Types;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Constants/GameConstants", fileName = "GameConstants")]
    public class GameConstants : ScriptableObject
    {
        [field: SerializeField] public int FirstSceneIndex { get; private set; }
        [field: SerializeField] public int FrameRate { get; private set; } = 120;

        [field: Header("Enemy")]
        [field: SerializeField] public ETeam EnemyTeam { get; private set; }
        [field: SerializeField] public int EnemyBufferSize { get; private set; }
        [field: SerializeField] public int PoolSizeUI { get; private set; }

        [field: Header("UI")]
        [field: SerializeField] public Sprite[] RareSprites { get; private set; }
        [field: SerializeField] public Color[] RareColors { get; private set; }

    }
}