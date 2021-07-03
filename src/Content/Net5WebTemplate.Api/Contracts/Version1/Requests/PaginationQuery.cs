namespace Net5WebTemplate.Api.Contracts.Version1.Requests
{
    public class PaginationQuery
    {
        public int Offset { get; set; }
        public int Limit { get; set; }

        public PaginationQuery()
        {
            Offset = 1;
            Limit = 25;
        }

        public PaginationQuery(int offset, int limit)
        {
            Offset = offset < 0 ? 1 : offset;
            Limit = limit > 100 ? 100 : limit;
        }
    }
}
