using System;
using System.Xml;
using APICaller;
using APICaller.Models;
class Program
{
    static async Task Main()
    {
        Console.WriteLine("Enter the URL base:");
        string baseurl = Console.ReadLine();
        Console.WriteLine($"Enter number:{"\n"} Display All books: 1 {"\n"} Display a book by title: 2 {"\n"} Add a book: 3 {"\n"} End: 0");
        List<string> request = new List<string>();
        string input = Console.ReadLine();
        while (input!="0") {
            request.Add(input);
            input = Console.ReadLine();
        }
        List<Task>tasks = new List<Task>();
        for (int i = 0; i < request.Count; i++) {
            if (request[i] == "1")
            {
                tasks.Add(HttpRequest.Get(baseurl));
            }
            else if (request[i] == "2")
            {
                tasks.Add(HttpRequest.GetByTitle(baseurl));
            }
            else if (request[i] == "3")
            {
                tasks.Add(HttpRequest.Post(baseurl));
            }
        }
        await Task.WhenAll(tasks);
    }
}