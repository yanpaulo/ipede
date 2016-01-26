using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Models
{
    public class MenuItem
    {
        public string Text { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }

        public bool CustomUrl { get; set; }
        public bool Active { get; set; }
    }
}