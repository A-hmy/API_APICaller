using APICaller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace APICaller
{
    internal class HttpRequest
    {
        private static readonly object lockObj = new object();
        public static async Task Post(string baseUrl)
        {
            using var httpClient = new HttpClient();
            string title;
            string authorName;
            lock (lockObj)
            {
                Console.WriteLine("Add new book:");
                Console.WriteLine("Enter book title:");
                title = Console.ReadLine();
                Console.WriteLine("Enter book author name:");
                authorName = Console.ReadLine();
                Console.WriteLine("___________________________________________________________________________________________________");
            }
            BookModel book = new BookModel { title = title, author = new AuthorModel { name = authorName } };
            string json = JsonSerializer.Serialize(book);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.BaseAddress = new Uri(baseUrl);
            HttpResponseMessage response = await httpClient.PostAsync("book", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Book Added");
            }
            else
            {
                Console.WriteLine("Book does not Added");
            }
        }

        public static async Task Get(string baseUrl)
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            HttpResponseMessage response = await httpClient.GetAsync("book");
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                List<BookModel> listBook = JsonSerializer.Deserialize<List<BookModel>>(res);
                lock (lockObj)
                {
                    Console.WriteLine("All Books:");
                    foreach (BookModel book in listBook) { Console.WriteLine($"Title:{book.title}{"\t"}Author:{book.author.name}{"\n"}"); }
                    Console.WriteLine("___________________________________________________________________________________________________");
                }
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
        public static async Task GetByTitle(string baseUrl)
        {
            using var httpClient = new HttpClient();
            string title;
            lock (lockObj)
            {
                Console.WriteLine("Enter a title to search for books.");
                title = Console.ReadLine();
                Console.WriteLine("___________________________________________________________________________________________________");
            }
            httpClient.BaseAddress = new Uri(baseUrl);
            HttpResponseMessage response = await httpClient.GetAsync($"book/{title}");
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                List<BookModel> listBook = JsonSerializer.Deserialize<List<BookModel>>(res);
                lock (lockObj)
                {
                    Console.WriteLine($"Results for your search: \"{title}\":");
                    foreach (BookModel book in listBook) { Console.WriteLine($"Title:{book.title}{"\t"}Author:{book.author.name}{"\n"}"); }
                    Console.WriteLine("___________________________________________________________________________________________________");
                }
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
    }
}
