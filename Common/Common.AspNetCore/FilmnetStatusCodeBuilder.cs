using System.Net;

namespace Common.AspNetCore
{
    public static class FilmnetStatusCodeBuilder
    {
        public static FilmnetStatusCode Init(string code)
        {
            return new FilmnetStatusCode(code);
        }

        public static FilmnetStatusCode WithDescription(this FilmnetStatusCode code, string description)
        {
            return code.SetDescription(description);
        }
        
    }
}