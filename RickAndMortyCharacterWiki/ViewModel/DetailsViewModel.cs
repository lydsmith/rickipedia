using RickAndMortyCharacterWiki.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RickAndMortyCharacterWiki.ViewModel
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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
                DisplayType = !string.IsNullOrEmpty(Character.type);
            }
        }

        private bool dislayType;
        public bool DisplayType {
            get { return dislayType; }
            set
            {
                dislayType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisplayType"));
            }
        }

    }
}
