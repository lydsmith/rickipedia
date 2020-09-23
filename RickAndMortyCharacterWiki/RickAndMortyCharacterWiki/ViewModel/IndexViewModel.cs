using RickAndMortyCharacterWiki.Model;
using RickAndMortyCharacterWiki.Services.Interfaces;
using RickAndMortyCharacterWiki.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RickAndMortyCharacterWiki.ViewModel
{
    public class IndexViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ICharacterService service = DependencyService.Get<ICharacterService>();
        private readonly NLog.ILogger Logger = NLog.LogManager.GetCurrentClassLogger();
        public IndexViewModel()
        {
            SetUpFilters();
            GetCharacters();
            GetPreviousPage = new Command(previousPage);
            GetNextPage = new Command(nextPage);
    }

        private ObservableCollection<Character> characters;
        public ObservableCollection<Character> Characters
        {
            get { return characters; }
            set
            {
                characters = value;
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PagesLabel"));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PagesLabel"));
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
        private ObservableCollection<string> genders;
        public ObservableCollection<string> Genders
        {
            get { return genders; }
            set
            {
                genders = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Genders"));
            }
        }
        private ObservableCollection<string> status;
        public ObservableCollection<string> Status
        {
            get { return status; }
            set
            {
                status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));

            }
        }
        private string selectedGender;
        public string SelectedGender
        {
            get { return selectedGender; }
            set
            {
                selectedGender = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedGender"));
                //call api to filter by gender
                GetCharacters(true);
            }
        }
        private string selectedStatus;
        public string SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedStatus"));
                //call api to filter by status
                GetCharacters(true);
            }
        }
        private Character selectedCharacter;
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedCharacter"));

                if (selectedCharacter == null)
                    return;

                //Show Details page for selected character
                App.Current.MainPage.Navigation.PushAsync(new Details(SelectedCharacter));
                SelectedCharacter = null;
            }
        }
        private string searchTerm;
        public string SearchTerm
        {
            get { return searchTerm; }
            set
            {
                searchTerm = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SearchTerm"));
                GetCharacters(true);
            }
        }
        private string noResultsMessage;
        public string NoResultsMessage
        {
            get { return noResultsMessage; }
            set
            {
                noResultsMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NoResultsMessage"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NoResults"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShowCharacterList"));
            }
        }
        public bool NoResults => !String.IsNullOrEmpty(noResultsMessage);
        public bool ShowCharacterList => !NoResults;
        public string PagesLabel => $"Page {PageNumber} of {TotalPages}";

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

        public async void SetUpFilters()
        {
            try
            {
                //setup filter values
                var filterValues = await service.GetFilterValues();
                Genders = filterValues["genders"];
                Status = filterValues["status"];
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
            }
        }
        public async void GetCharacters(bool filterUpdated = false)
        {
            try
            {
                //goto page 1 if filtering or if first call
                if (filterUpdated || PageNumber == 0) { PageNumber = 1; }
                var response = await service.GetCharacters(PageNumber, SelectedGender, SelectedStatus, SearchTerm);
                if (response == null)
                {
                    NoResultsMessage = "No results were found.";
                    PageNumber = 0;
                    TotalPages = 0;
                    HasNext = false;
                    HasPrevious = false;
                }
                else
                {
                    NoResultsMessage = String.Empty;
                    TotalPages = response.info.pages;
                    HasNext = !string.IsNullOrEmpty(response.info.next);
                    HasPrevious = !string.IsNullOrEmpty(response.info.prev);
                    Characters = response.results;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
            }
        }
    }
}
