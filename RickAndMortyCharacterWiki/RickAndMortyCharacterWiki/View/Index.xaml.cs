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
    public partial class Index : ContentPage
    {
        private readonly NLog.ILogger Logger = NLog.LogManager.GetCurrentClassLogger();
        public Index()
        {
            InitializeComponent();
            Logger.Info("Application Start - NLog Initialised");
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}