using RickAndMortyCharacterWiki.Services;
using RickAndMortyCharacterWiki.Services.Interfaces;
using RickAndMortyCharacterWiki.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("get_schwifty.ttf", Alias = "GetSchwifty")]
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
