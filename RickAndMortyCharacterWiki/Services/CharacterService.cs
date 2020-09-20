using Newtonsoft.Json;
using RickAndMortyCharacterWiki.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMortyCharacterWiki.Services
{
    public class CharacterService : ICharacterService
    {
        readonly string _baseUrl = "https://rickandmortyapi.com/api/";

        public async Task<ObservableCollection<string>> GetAllGenders()
        {
            var genders = new List<string> { "All Genders" };
            HttpClient client = new HttpClient();
            var nextUrl = "";
            var page = 1;
            do
            {
                string url = $"{_baseUrl}character/?page={page}";
                await client.GetAsync(url)
                    .ContinueWith(async (genderResponse) =>
                    {
                        var response = await genderResponse;
                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            var jsonResult = JsonConvert.DeserializeObject<CharactersResponse>(result);
                            if (jsonResult != null)
                            {
                                genders.AddRange(jsonResult.results.Select(x => x.gender).Distinct());
                                genders = genders.Distinct().ToList();
                                // Get the URL for the next page
                                nextUrl = jsonResult.info.next;
                                page++;
                            }
                        }
                        else
                        {
                            // End loop
                            nextUrl = null;
                        }
                    });

            } while (nextUrl!=null);
            return new ObservableCollection<string>(genders.OrderBy(x=>x).ToList());
        }

        public async Task<CharactersResponse> GetCharacters(int page=0, string gender="")
        {
            if (gender == "All Genders") { gender = ""; }
            string url = $"{_baseUrl}character/?page={page}&gender={gender}";
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
