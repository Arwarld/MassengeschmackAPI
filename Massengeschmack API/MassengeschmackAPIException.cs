using System;

namespace Massengeschmack_API
{
    class MassengeschmackAPIException : Exception
    {
        public MassengeschmackAPIException(string message)
            : base(message)
        {
        }
    }
}