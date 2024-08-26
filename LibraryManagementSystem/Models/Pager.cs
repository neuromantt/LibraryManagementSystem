namespace LibraryManagementSystem.Models
{
    public class Pager
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public Pager()
        {
        }

        public Pager(int totalItems, int currentPage, int pageSize = 10)
        {
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);
            StartPage = currentPage - 5;
            EndPage = currentPage + 5;

            if (StartPage <= 0)
            {
                EndPage -= StartPage - 1;
                StartPage = 1;
            }

            if (EndPage > TotalPages)
            {
                EndPage = TotalPages;
                if (EndPage > pageSize)
                {
                    StartPage -= EndPage - TotalPages;
                }
            }
        }
    }
}
