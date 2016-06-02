using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorIO.Test
{
    class TestCommon
    {
        static readonly string ENV_APIKEY = "CONSTRUCTORIO_KEY";

        public static ConstructorIOAPI MakeAPI()
        {
            
            string key = Environment.GetEnvironmentVariable(ENV_APIKEY, EnvironmentVariableTarget.User);

            if (String.IsNullOrWhiteSpace(key))
            {
                throw new Exception("No API key provided. Set Environment variable '" + ENV_APIKEY + "' to '{API Key}:{Autocomplete Key}'");
            }
            else
            {
                string[] apiParts = key.Split(':');
                if (apiParts.Length != 2) throw new Exception("Invalid API Key format. Use '{API Key}:{Autocomplete Key}' example 'xxxx:yyyy'");

                return new ConstructorIOAPI(apiParts[0], apiParts[1]);
            }
        }
    }
}
