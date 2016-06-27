using System.DirectoryServices.AccountManagement;

namespace Business_Logic.Handlers
{
    public class ActiveDirectoryHandler
    {
        private static PrincipalContext ConnectToAd(string domainName)
        {
            return new PrincipalContext(ContextType.Domain, domainName);
        }

        public PrincipalContext ValidateUserInAd(string username, string password, string domainName)
        {
            var adConnection = ConnectToAd(domainName);

            return adConnection.ValidateCredentials(username, password) ? adConnection : null;
        }
    }
}
