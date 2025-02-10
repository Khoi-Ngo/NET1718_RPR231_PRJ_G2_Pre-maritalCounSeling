using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pre_maritalCounSeling.Common.Util
{
    public static class AppUtil
    {
        //get the deserialized response from the api
        public static async Task<Dictionary<string, string>?> GetDeserializedResponseFromApi(HttpResponseMessage? response)
        => JsonSerializer.Deserialize<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
    }
}
