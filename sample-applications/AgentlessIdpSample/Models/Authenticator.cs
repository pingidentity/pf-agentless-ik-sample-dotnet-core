using Newtonsoft.Json.Linq;

namespace AgentlessIdpSample.Models
{
    public static class Authenticator
    {
        public static JObject authenticate(string rootDirectory, string username, string password)
        {
            IdpSampleUserLoader.loadUsers(rootDirectory);

            return IdpSampleUserLoader.getUser(username, password); 
        }
    }
}