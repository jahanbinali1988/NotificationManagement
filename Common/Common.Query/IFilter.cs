namespace Common.Query
{
    public interface IFilter
    {
        public int Offset { get; set; }
        public int Count { get; set; }
    }
}
