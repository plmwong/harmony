using DotNetOpenAuth.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;

namespace Harmony.Core.OAuth
{
	public interface IAuthProvider
	{
		IAuthorizationState Authorise(NativeApplicationClient nativeApplicationClient);
        IAuthorizationState Authorise(NativeApplicationClient nativeApplicationClient, string accessToken);
	}
}

