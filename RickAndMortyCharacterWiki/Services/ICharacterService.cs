using RickAndMortyCharacterWiki.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMortyCharacterWiki.Services
{
    public interface ICharacterService
    {
        Task<CharactersResponse> GetCharacters(int page=0, string gender="");
        Task<ObservableCollection<string>> GetAllGenders();
    }
}
