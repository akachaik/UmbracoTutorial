using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class Testimonials
    {

        public string Title { get; set; }
        public string Introduction { get; set; }

        public List<TestimonialQuote> Quotes { get; set; }

        public string ColumnClass
        {
            get
            {
                if (Quotes == null)
                {
                    return "col-md-12";
                }
                switch (Quotes.Count)
                {
                    case 1:
                        return "col-md-12";
                    case 2:
                        return "col-md-6";
                    case 3:
                        return "col-md-4";
                    case 4:
                        return "col-md-3";
                    default:
                        return "col-md-12";
                }
            }
        }
    }

    public class TestimonialQuote
    {
        public string Name { get; set; }
        public string Quote { get; set; }
    }
}
