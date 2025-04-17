using ContactManager.WPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.WPF.Services
{
    public class ContactService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7001/api/contacts"; // coloque sua URL;

        public ContactService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ContactDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl + "?page=1&pageSize=1000"); // pega tudo para simplificar
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            // Aqui deserializa usando o envelope correto (PaginatedResult<ContactDto>)
            var paginated = JsonConvert.DeserializeObject<PaginatedResult<ContactDto>>(json);
            return new List<ContactDto>(paginated.Items);
        }

        public async Task<ContactDto> CreateAsync(CreateContactDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ContactDto>(result);
        }

        public async Task UpdateAsync(int id, UpdateContactDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}