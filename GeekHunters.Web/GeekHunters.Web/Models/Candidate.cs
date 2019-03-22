using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeekHunters.Web.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Skills { get; set; }
        public string Error { get; set; }
    }
}