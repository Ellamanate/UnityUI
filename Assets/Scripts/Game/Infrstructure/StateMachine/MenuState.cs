using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityUI.Utils;

namespace UnityUI.Game
{
    public class MenuState : IGameState, IEnteringState, IDisposable
    {
        private readonly ScenesService _scenesService;
        private readonly BootstrapConfig _bootstrapConfig;
        private readonly CancellationTokenSource _tokenSource;
        
        private GameStateMachine _stateMachine;
        private MenuInstance _menuInstance;

        public MenuState(ScenesService scenesService, BootstrapConfig bootstrapConfig)
        {
            _scenesService = scenesService;
            _bootstrapConfig = bootstrapConfig;
            _tokenSource = new CancellationTokenSource();
        }
        
        public void Initialize(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Dispose()
        {
            _tokenSource.Dispose();
        }
        
        public void Enter()
        {
            _ = EnterMenu(_tokenSource.Token);
        }
        
        public void SetMenuInstance(MenuInstance menuInstance)
        {
            _menuInstance = menuInstance;
        }

        private async UniTask EnterMenu(CancellationToken cancellationToken)
        {
            if (_scenesService.CurrentScene != _bootstrapConfig.MenuScene.ScenePath)
            {
                await _scenesService.LoadScene(_bootstrapConfig.MenuScene.ScenePath, _tokenSource.Token);
            }
            
            await UniTask.WaitWhile(() => _menuInstance == null, cancellationToken: cancellationToken);
            
            _menuInstance.StartScene();
        }
    }
}