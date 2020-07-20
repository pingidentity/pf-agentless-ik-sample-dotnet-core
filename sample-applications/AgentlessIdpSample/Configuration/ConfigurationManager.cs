using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AgentlessIdpSample.Configuration
{
    // basic Configuration Manager to update appsettings.json as not supported
    public static class ConfigurationManager
    {
        public static void saveConfigurations(string rootDirectory, HttpRequest request)
        {
            foreach(string key in request.Form.Keys)
            {
                if (key != "submit")
                {
                    if (isValidConfiguration(key, request.Form[key]))
                    {
                        updateAppSetting(rootDirectory, IdpConstants.ADAPTER_CONFIG_SECTION + ":" + key, request.Form[key]);
                    }
                    else
                    {
                        throw new Exception("Please check configuration of " + key);
                    }                   
                }
            }
        }
        
        private static void updateAppSetting(string rootDirectory, string key, string value) 
        {
            try 
            {   
                string filePath = Path.Combine(rootDirectory, "appsettings.json");

                string json = File.ReadAllText(filePath);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);

                string sectionPath = key.Split(":")[0];
                string keyPath = key.Split(":")[1];
                
                jsonObj[sectionPath][keyPath] = value;

                string output = JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);

            }
            catch (Exception)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        private static Boolean isValidConfiguration(string key, string value)
        {
            switch(key)
            {
                case IdpConstants.BASE_PF_URL:
                    return !string.IsNullOrEmpty(value) && Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute);
                case IdpConstants.ADAPTER_USERNAME:
                    return !string.IsNullOrEmpty(value);
                case IdpConstants.ADAPTER_PASSWORD:
                    return !string.IsNullOrEmpty(value);
                case IdpConstants.ADAPTER_ID:
                    return !string.IsNullOrEmpty(value);
                case IdpConstants.TARGET_URL:
                    return !string.IsNullOrEmpty(value) && Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute);
                case IdpConstants.PARTNER_ENTITY_ID:
                    return !string.IsNullOrEmpty(value);
            }
            return false;
        }
    }
}