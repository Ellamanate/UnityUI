using System;
using UnityEngine;
using Zenject;

namespace UnityUI.Game
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private SelectCharacterView _selectCharacterView;
        
        public override void InstallBindings()
        {
            BindMenuInstance();
            BindUIPages();
        }

        private void BindMenuInstance()
        {
            Container
                .BindInterfacesAndSelfTo<MenuInstance>()
                .AsSingle()
                .NonLazy();
        }

        private void BindUIPages()
        {
            Container
                .Bind<MenuPageRouting>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind(typeof(MainMenuState), typeof(IDisposable))
                .To<MainMenuState>()
                .AsSingle()
                .WithArguments(_mainMenuView);

            Container
                .Bind<CharactersViewsFactory>()
                .AsSingle();
            
            Container
                .Bind(typeof(SelectCharacterState), typeof(IDisposable))
                .To<SelectCharacterState>()
                .AsSingle()
                .WithArguments(_selectCharacterView);
            
            Container
                .Bind<InitializeMenuState>()
                .AsSingle()
                .WithArguments(_mainMenuView, _selectCharacterView);
        }
    }
}