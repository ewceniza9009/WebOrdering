namespace AccountMateWebOrder.Models.Page
{
    public class PageNumber
    {
        private PageNumber()
        {

        }
        public static PageNumber Instance
        {
            get
            {
                return _Instance;
            }
        }

        public int InventoryCurrentPage { get; set; } = 1;

        private static PageNumber _Instance = new PageNumber();

    }
}