using RickAndMortyCharacterWiki.Model;
using RickAndMortyCharacterWiki.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace RickAndMortyCharacterWiki.ViewModel
{
    public class IndexViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ICharacterService service = DependencyService.Get<ICharacterService>();
        public IndexViewModel()
        {
            GetCharacters();
            GetPreviousPage = new Command(previousPage);
            GetNextPage = new Command(nextPage);
        }

        private ObservableCollection<Character> characters;

        public ObservableCollection<Character> Characters
        {
            get { return characters; }
            set { characters = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Characters"));
            }
        }
        private int pageNumber;
        public int PageNumber
        {
            get { return pageNumber; }
            set
            {
                pageNumber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PageNumber"));
            }
        }
        private int totalPages;
        public int TotalPages
        {
            get { return totalPages; }
            set
            {
                totalPages = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPages"));
            }
        }
        private bool hasPrevious;
        public bool HasPrevious
        {
            get { return hasPrevious; }
            set
            {
                hasPrevious = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HasPrevious"));
            }
        }
        private bool hasNext;
        public bool HasNext
        {
            get { return hasNext; }
            set
            {
                hasNext = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HasNext"));
            }
        }


        public Command GetPreviousPage { private set; get; }
        public Command GetNextPage { private set; get; }
        private void previousPage(object obj)
        {
            if (HasPrevious)
            {
                PageNumber = PageNumber - 1;
                GetCharacters();
            }
        }
        private void nextPage(object obj)
        {
            if (HasNext)
            {
                PageNumber = PageNumber + 1;
                GetCharacters();
            }
        }

        public async void GetCharacters() {
            try
            {
                var response = await service.GetCharacters(PageNumber);
                TotalPages = response.info.pages;
                HasNext = !string.IsNullOrEmpty(response.info.next);
                HasPrevious = !string.IsNullOrEmpty(response.info.prev);
                Characters = response.results;
            }
            catch (Exception e) { 
                //log errors
            }
        }
    }
}
