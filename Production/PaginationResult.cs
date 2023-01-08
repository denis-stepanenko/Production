namespace Production
{
    public class PaginationResult<T> where T : class
    {
        public int TotalCount { get; set; }

        public List<T> Data { get; set; }
    }
}
