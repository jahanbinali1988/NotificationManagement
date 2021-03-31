using Common.Core.Exceptions;
using HashidsNet;

namespace Common.Core.Helpers
{
    /// <summary>
    /// IdHelpers (Hash ID's) creates short, unique, decryptable hashes from unsigned integers.
    /// (NOTE: This is NOT a true cryptographic hash, since it is reversible)
    /// </summary>
    public static class IdHelpers
    {
        private static readonly Hashids Hashids
            = new Hashids(salt: "SJKLwDSFuOfOgTdCgHhAsNdGKHFd", minHashLength: 4);

        private static readonly string Prefix = "sc";

        private static readonly int PrefixLength;
        static IdHelpers()
        {
            PrefixLength = Prefix.Length;
        }

        /// <summary>
        /// Create hashId from long values
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string ToUrlFriendlyIdString(long id)
        {
            return Prefix + Hashids.EncodeLong(id);
        }

        /// <summary>
        /// Convert string of hashIds to original long value
        /// </summary>
        /// <param name="urlFriendlyIdString"></param>
        /// <returns></returns>
        /// <exception cref="InvalidHashIdException"></exception>
        public static long ToLong(string urlFriendlyIdString)
        {
            long[] ids = Hashids.DecodeLong(urlFriendlyIdString.Substring(PrefixLength));

            if (ids.Length == 0)
                throw new InvalidHashIdException();

            return ids[0];
        }

        /// <summary>
        /// Safe method to convert string of hashIds to original long value
        /// </summary>
        /// <param name="urlFriendlyIdString"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool TryParse(string urlFriendlyIdString, out long id)
        {
            long[] ids = Hashids.DecodeLong(urlFriendlyIdString.Substring(PrefixLength));

            if (ids.Length == 0)
            {
                id = 0;
                return false;
            }

            id = ids[0];
            return true;
        }

    }
}