using System;
using System.ComponentModel;
using System.Net;

namespace Common.AspNetCore
{
    public sealed class FilmnetStatusCode
    {
        public FilmnetStatusCode(string code)
        {

            Code = code;
        }

        public string Code { get; private set; }

        public string Description { get; private set; }

        public string  DisplayMessage { get; private set; }

        public FilmnetStatusCode SetDescription(string description)
        {

            if (string.IsNullOrWhiteSpace(description))
                throw new InvalidEnumArgumentException(nameof(description));

            Description = description;
            return this;
        }

        public FilmnetStatusCode SetDisplayMessage(string displayMessage)
        {

            if (string.IsNullOrWhiteSpace(displayMessage))
                throw new InvalidEnumArgumentException(nameof(displayMessage));

            DisplayMessage = displayMessage;
            return this;
        }
    }
}