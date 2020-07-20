using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace AgentlessIdpSample.Models
{
    public static class IdpSampleUserLoader
    {
        private static dynamic users;
        
        public static JObject getUser(string username, string password)
        {
            foreach(dynamic user in users)
            {
                if(user[IdpConstants.USERNAME] == username && user[IdpConstants.PASSWORD] == password)
                {
                    return (JObject) user["attributes"];
                }
            }
            return null;
        }

        public static void loadUsers(string rootDirectory)
        {
            if (users == null)
            {
                try 
                {   
                    string filePath = Path.Combine(rootDirectory, "Configuration/idp-sample-users.json");

                    string json = File.ReadAllText(filePath);
                    users = JsonConvert.DeserializeObject(json);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error loading users");
                }
            }
        }
    }
}