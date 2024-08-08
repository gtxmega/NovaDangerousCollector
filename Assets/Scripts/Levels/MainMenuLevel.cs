using Game;
using UnityEngine;

namespace Levels
{
    public class MainMenuLevel : LevelInstance
    {
        [SerializeField] private MainMenuServices _menuServices;

        public override void Init(GameInstance gameInstance)
        {
            _menuServices.Init(gameInstance);
            _menuServices.InjectToWorldObject();
        }
    }
}