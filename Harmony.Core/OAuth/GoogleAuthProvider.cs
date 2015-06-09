using System;
using System.Reflection;
using DotNetOpenAuth.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Calendar.v3;
using Google.Apis.Util;
using log4net;

namespace Harmony.Core.OAuth
{
    public class GoogleAuthProvider : IAuthProvider
    {
        private readonly IGoogleConfiguration _configuration;

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public GoogleAuthProvider(IGoogleConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IAuthorizationState Authorise(NativeApplicationClient nativeApplicationClient)
        {
            var authState = CreateCalendarAuthorisationState();

            if (_configuration.GoogleRefreshToken.IsNullOrEmpty())
            {
                Log.Warn("No refresh token available for OAuth, cannot establish authorisation.");

                var authUri = nativeApplicationClient.RequestUserAuthorization(authState);
                throw new GoogleAuthorizationRequiredException(authUri.ToString());
            }

            Log.Info("Requesting authorisation using configured refresh token.");

            authState.RefreshToken = _configuration.GoogleRefreshToken;
            nativeApplicationClient.RefreshToken(authState, null);

            return authState;
        }

        private static IAuthorizationState CreateCalendarAuthorisationState()
        {
            IAuthorizationState authState = new AuthorizationState(new[] {CalendarService.Scopes.Calendar.GetStringValue()});
            authState.Callback = new Uri(NativeApplicationClient.OutOfBandCallbackUrl);
            return authState;
        }

        public IAuthorizationState Authorise(NativeApplicationClient nativeApplicationClient, string accessToken)
        {
            var authState = CreateCalendarAuthorisationState();

            Log.Info("Requesting authorisation using supplied access token.");

            var authResult = nativeApplicationClient.ProcessUserAuthorization(accessToken, authState);
            _configuration.GoogleRefreshToken = authResult.RefreshToken;
            nativeApplicationClient.RefreshToken(authState, null);

            return authState;
        }
    }
}