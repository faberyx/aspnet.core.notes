using System.Collections.Generic;
 
namespace notes_manager.Models
{
    /// <summary>
    /// Pagination class used to pass and retrieve all pagination and sort information
    /// SortBy: name of the database field to sort
    /// SortDirection: sort direction  -> true:ASC  false:DESC
    /// PageNumber: current page numebr
    /// PageSize: total rows for a page
    /// Results: list of paginated notes
    /// </summary>
    /// <typeparam name="T">Entity to paginate</typeparam>
    public class ResultPage<T>
    {
        public int TotalCount { get; set; }
        public string SortBy { get; set; }
        public bool SortDirection { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
