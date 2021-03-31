using System.Collections.Generic;


namespace Common.Query
{
    public class PagingResult<TEntity>
    {
        public long TotalCount { get; set; }
        public List<TEntity> Data { get; set; }
        public string NextUrl { get; set; }
        public PagingResult() { }
        public PagingResult(long total, List<TEntity> data)
        {
            TotalCount = total;
            Data = data;
        }

        public PagingResult(long totalCount, List<TEntity> data, string nextUrl)
        {
            TotalCount = totalCount;
            Data = data;
            NextUrl = nextUrl; ;
        }
    }
}
