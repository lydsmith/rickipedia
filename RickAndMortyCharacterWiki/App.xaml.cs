using RickAndMortyCharacterWiki.Services;
using RickAndMortyCharacterWiki.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RickAndMortyCharacterWiki
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<ICharacterService, CharacterService>();
            MainPage = new NavigationPage(new Index());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
