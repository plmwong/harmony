using System;
using DotNetOpenAuth.OAuth2;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Calendar.v3;
using Harmony.Core.OAuth;

namespace Harmony.Core.Calendars
{
    public class GoogleCalendarFactory : IGoogleCalendarFactory
    {
        private readonly IAuthProvider _authProvider;
        private readonly IGoogleConfiguration _configuration;

        public GoogleCalendarFactory(IGoogleConfiguration configuration, IAuthProvider authProvider)
        {
            _authProvider = authProvider;
            _configuration = configuration;
        }

        public GoogleCalendar Create()
        {
            return CreateCalendar(_authProvider.Authorise);
        }

        private GoogleCalendar CreateCalendar(Func<NativeApplicationClient, IAuthorizationState> authorisationProvider)
        {
            var nativeApplicationClient = new NativeApplicationClient(GoogleAuthenticationServer.Description)
            {
                ClientIdentifier = "704452130573-sls2j55v6o0u3g7tnlu8i4tniascrogf.apps.googleusercontent.com",
                ClientSecret = "k9o5O89-MODDEi91o0jmRGes"
            };

            var calendarService = new CalendarService(
                new OAuth2Authenticator<NativeApplicationClient>(nativeApplicationClient, authorisationProvider));

            return new GoogleCalendar(_configuration.GoogleCalendarId, calendarService);
        }

        public GoogleCalendar Create(string accessToken)
        {
            return CreateCalendar(n => _authProvider.Authorise(n, accessToken));
        }
    }
}