using System;

namespace Harmony.Core.OAuth
{
    public class GoogleAuthorizationRequiredException : Exception
    {
        private readonly string _authorisationUri;

        public string AuthorisationUri
        {
            get { return _authorisationUri; }
        }

        public GoogleAuthorizationRequiredException(string authorisationUri)
            : base(string.Format("AccessToken is required, please visit '{0}' and add the AccessToken to the app.config", authorisationUri))
        {
            _authorisationUri = authorisationUri;
        }
    }
}