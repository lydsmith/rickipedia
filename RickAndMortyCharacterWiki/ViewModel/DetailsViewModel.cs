using RickAndMortyCharacterWiki.Model;
using RickAndMortyCharacterWiki.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace RickAndMortyCharacterWiki.ViewModel
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ICharacterService service = DependencyService.Get<ICharacterService>();
        public DetailsViewModel()
        {
        }

        private Character character;
        public Character Character
        {
            get { return character; }
            set
            {
                character = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Character"));
                GetEpisodes();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EpisodeIdsString"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisplayType"));
            }
        }

        private ObservableCollection<Episode> episodes;
        public ObservableCollection<Episode> Episodes
        {
            get { return episodes; }
            set
            {
                episodes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Episodes"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EpisodesString"));
            }
        }
        public bool DisplayType => Character != null && !string.IsNullOrEmpty(Character.type);
        public string EpisodeIdsString => Character != null ? Character.episode.Select(x=> x.Split('/')[5]).ToList()
            .Aggregate((i, j) => i + "," + j) : "";
        public string EpisodesString => Episodes != null ? Episodes.Select(x => x.episode).ToList()
            .Aggregate((i, j) => i + ", " + j) : "";

        public async void GetEpisodes()
        {
            var response = await service.GetMultipleEpisodes(EpisodeIdsString);
            Episodes = response;
        }
    }
}
