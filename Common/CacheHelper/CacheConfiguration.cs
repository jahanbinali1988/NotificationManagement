using System;

namespace CacheHelper
{
    public class CacheConfiguration
    {
        public string Prefix { get; set; }
        public TimeSpan Ttl { get; set; }
        public TimeSpan ReConstruct { get; set; }
        public TimeSpan Expiration { get; set; }
        public string[] Providers { get; set; }
    }
}