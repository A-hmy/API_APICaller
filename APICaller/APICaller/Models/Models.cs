using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICaller.Models
{
    public class AuthorModel
    {
        public string name { get; set; }
    }
    public class BookModel
    {
        public string title { get; set; }
        public AuthorModel author { get; set; }
    }

}
