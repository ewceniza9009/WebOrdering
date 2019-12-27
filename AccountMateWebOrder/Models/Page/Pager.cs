namespace AccountMateWebOrder.Models.Page
{
    public class Pager
    {
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 8;
        public int PageRange { get; set; } = 5;
        public double PageCount { get; set; }      
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public string Button { get; set; }
        public int Start
        {
            get 
            {
                return (Page - 1) * ItemsPerPage;
            }
        }
    }
}