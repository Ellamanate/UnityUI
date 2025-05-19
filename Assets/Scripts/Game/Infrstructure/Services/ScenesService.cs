using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityUI.Utils;

namespace UnityUI.Game
{
    public class ScenesService
    {
        public string CurrentScene { get; private set; }
        
        public ScenesService()
        {
            CurrentScene = SceneManager.GetActiveScene().path;
        }
        
        public async UniTask LoadScene(string scenePath, CancellationToken token)
        {
            ConsoleLogger.Log($"Loading scene {scenePath}");
            
            await SceneManager
                .LoadSceneAsync(scenePath, LoadSceneMode.Single)
                .ToUniTask(cancellationToken: token);
            
            CurrentScene = scenePath;
        }
    }
}