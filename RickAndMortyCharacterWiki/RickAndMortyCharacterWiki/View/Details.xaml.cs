using RickAndMortyCharacterWiki.Model;
using RickAndMortyCharacterWiki.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RickAndMortyCharacterWiki.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Details : ContentPage
    {
        public Details(Character character)
        {
            InitializeComponent();

            //NavigationPage.SetHasNavigationBar(this, false);
            DetailsViewModel vm = new DetailsViewModel { };
            this.BindingContext = vm;
            vm.Character = character;
        }
    }
}