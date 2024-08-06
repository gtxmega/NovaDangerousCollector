using Levels;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameInstance : MonoBehaviour
    {
        [field: SerializeField] public GameConstants GameConstants { get; private set; }
        [field: SerializeField] public LevelState LevelState { get; private set; }

        private Coroutine _loadingSceneCoroutine;

        private static GameInstance _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            Application.targetFrameRate = GameConstants.FrameRate;

            LoadScene(GameConstants.FirstSceneIndex);
        }

        private void LoadScene(int sceneIndex)
        {
            if (_loadingSceneCoroutine != null)
                throw new Exception("_loadingSceneCoroutine is not null!");

            _loadingSceneCoroutine = StartCoroutine(LoadSceneAsync(sceneIndex));
        }

        private IEnumerator LoadSceneAsync(int sceneIndex)
        {
            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneIndex);
            while (loadSceneOperation.isDone == false)
            {
                yield return null;
            }

            var levelInstance = FindObjectOfType<LevelInstance>();
            levelInstance?.Init(this);

            _loadingSceneCoroutine = null;
        }
    }
}