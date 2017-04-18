using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

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

    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public string DocTypeAliases { get; set; }
        public string FieldPropertyAliases { get; set; }
        public int PageSize { get; set; }
        public int PagingGroupSize { get; set; }
        public string Category { get; set; }
        public List<SearchGroup> SearchGroups { get; set; }
        public SearchResultsModel SearchResults { get; set; }
    }

    public class SearchGroup
    {
        public string[] FieldsToSearchIn { get; set; }
        public string[] SearchTerms { get; set; }

        public SearchGroup(string[] fieldsToSearchIn, string[] searchTerms)
        {
            FieldsToSearchIn = fieldsToSearchIn;
            SearchTerms = searchTerms;
        }
    }

    public class SearchResultsModel
    {
        public string SearchTerm { get; set; }
        public IEnumerable<IPublishedContent> Results { get; set; }

        public bool HasResults
        {
            get { return Results != null && Results.Count() > 0; }
        }

        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public int TotalItemCount { get; set; }
        public PagingBoundsModel PagingBounds { get; set; }
    }

    public class PagingBoundsModel
    {
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public bool ShowFirstButton { get; set; }
        public bool ShowLastButton { get; set; }

        public PagingBoundsModel(int startPage, int endPage, bool showFirstButton, bool showLastButton)
        {
            StartPage = startPage;
            EndPage = endPage;
            ShowFirstButton = showFirstButton;
            ShowLastButton = showLastButton;
        }
    }
}
