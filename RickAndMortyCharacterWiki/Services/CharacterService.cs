﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RickAndMortyCharacterWiki.Model;
using RickAndMortyCharacterWiki.Services.Interfaces;
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
        const string AllGendersText = "All genders";
        const string AllStatusText = "All status";
        private readonly NLog.ILogger Logger = NLog.LogManager.GetCurrentClassLogger();

        public async Task<CharactersResponse> GetCharacters(int page = 0, string gender = "", string status = "")
        {
            if (gender == AllGendersText) { gender = ""; }
            if (status == AllStatusText) { status = ""; }
            string url = $"{_baseUrl}character/?page={page}&gender={gender}&status={status}";
            HttpClient client = new HttpClient();

            try
            {
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
            }
            catch(Exception e)
            {
                Logger.Error(e, e.Message);
            }
            return null;
        }

        public async Task<Dictionary<string, ObservableCollection<string>>> GetFilterValues()
        {
            var genders = new List<string> { AllGendersText };
            var status = new List<string> { AllStatusText };
            HttpClient client = new HttpClient();
            var nextUrl = "";
            var page = 1;
            do
            {
                string url = $"{_baseUrl}character/?page={page}";
                try
                {
                    await client.GetAsync(url)
                    .ContinueWith(async (filterResponse) =>
                    {
                        var response = await filterResponse;
                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            var jsonResult = JsonConvert.DeserializeObject<CharactersResponse>(result);
                            if (jsonResult != null)
                            {
                                genders.AddRange(jsonResult.results.Select(x => x.gender).Distinct());
                                genders = genders.Distinct().ToList();
                                status.AddRange(jsonResult.results.Select(x => x.status).Distinct());
                                status = status.Distinct().ToList();
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
                }
                catch(Exception e)
                {
                    Logger.Error(e, e.Message);
                }

            } while (nextUrl != null);

            var filterValues = new Dictionary<string, ObservableCollection<string>>();
            filterValues.Add("genders", new ObservableCollection<string>(genders.OrderBy(x => x).ToList()));
            filterValues.Add("status", new ObservableCollection<string>(status.OrderBy(x => x).ToList()));
            return filterValues;
        }

        public async Task<ObservableCollection<Episode>> GetMultipleEpisodes(string ids)
        {
            if (ids != null && ids.Any())
            {
                try
                {
                    string url = $"{_baseUrl}episode/{ids}";
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var token = JToken.Parse(result);

                        if (token is JArray)
                        {
                            return JsonConvert.DeserializeObject<ObservableCollection<Episode>>(result);
                        }
                        else if (token is JObject)
                        {
                          return new ObservableCollection<Episode>(new List<Episode> { JsonConvert.DeserializeObject<Episode>(result) });
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e, e.Message);
                }
            }
            return null;
        }
    }
}
