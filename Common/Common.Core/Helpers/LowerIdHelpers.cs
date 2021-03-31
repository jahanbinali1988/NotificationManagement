using Common.Core.Exceptions;
using HashidsNet;

namespace Common.Core.Helpers
{
    /// <summary>
    /// IdHelpers (Hash ID's) creates short, unique, decryptable hashes from unsigned integers.This class only generate lower case string
    /// (NOTE: This is NOT a true cryptographic hash, since it is reversible)
    /// </summary>
    public static class LowerIdHelpers
    {
        private static readonly Hashids Hashids
            = new Hashids(salt: "SJKLwDSFuOfOgTdCgHhAsNdGKHFd", minHashLength: 2,alphabet: "qwertyuioplkjhgfdasxcvbznmghtujahfnvb");

        private static readonly string Prefix = "f";

        private static readonly int PrefixLength;
        static LowerIdHelpers()
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
            return Prefix + Hashids.EncodeLong(id).ToLower();
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
            long[] ids = Hashids.DecodeLong(urlFriendlyIdString.Substring(PrefixLength).ToLower());

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