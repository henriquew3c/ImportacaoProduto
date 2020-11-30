namespace _Support
{
    public class PaginationRequest
    {
        public string Order { get; set; } = "ASC";

        public int Start { get; set; } = 0;

        public int PageSize { get; set; } = 10;


        public static explicit operator PaginationRequest(DataTableData data)
        {
            return new PaginationRequest
            {
                Order = data.Order,
                Start = data.Start,
                PageSize = data.PageSize
            };
        }
    }
}
