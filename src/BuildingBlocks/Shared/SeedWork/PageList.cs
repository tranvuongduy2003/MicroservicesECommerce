namespace Shared.SeedWork
{
    public class PageList<T> : List<T>
    {
        public PageList(IEnumerable<T> items, long totalItems, int pageNumber, int pageSize)
        {
            _metadata = new Metadata
            {
                TotalItems = totalItems,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
            };
        }

        private Metadata _metadata { get; }

        public Metadata GetMetaData()
        {
            return _metadata;
        }
    }
}
