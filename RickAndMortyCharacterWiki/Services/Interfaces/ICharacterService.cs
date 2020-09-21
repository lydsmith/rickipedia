using RickAndMortyCharacterWiki.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMortyCharacterWiki.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<CharactersResponse> GetCharacters(int page=0, string gender="", string status = "");
        Task<Dictionary<string, ObservableCollection<string>>> GetFilterValues();
        Task<ObservableCollection<Episode>> GetMultipleEpisodes(string ids);
    }
}
