using Newtonsoft.Json;
using RickAndMortyCharacterWiki.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMortyCharacterWiki.Services
{
    public class CharacterService : ICharacterService
    {
        readonly string _baseUrl = "https://rickandmortyapi.com/api/";
        public async Task<CharactersResponse> GetCharacters(int page=0)
        {
            string url = $"{_baseUrl}character/?page={page}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                var jsonResult = JsonConvert.DeserializeObject<CharactersResponse>(result);
                if (jsonResult != null)
                {
                    return jsonResult;
                }
            }
            return null;
        }
    }
}
