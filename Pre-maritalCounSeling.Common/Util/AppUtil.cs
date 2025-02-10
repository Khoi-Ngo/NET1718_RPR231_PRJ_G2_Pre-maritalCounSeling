﻿using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Pre_maritalCounSeling.Common.Util
{
    public static class AppUtil
    {
        //get the deserialized response from the api
        public static async Task<Dictionary<string, string>?> GetDeserializedResponseFromApi(HttpResponseMessage? response)
        => JsonSerializer.Deserialize<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());

        public static async Task<T> GetDeserializedResponseFromApi<T>(HttpResponseMessage? response)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), options);
        }


        //adding jwt token to the request header
        public static Task AddJwtTokenToRequestHeader(HttpClient httpClient, HttpContext httpContext)
        {
            var tokenString = httpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
            return Task.CompletedTask;
        }
    }
}
