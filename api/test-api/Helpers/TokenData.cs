using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace test_api.Helpers
{   
    public class Tokendata {
        public string UserEmail {get; set;}
        public string UserPassword {get; set;}

        public static string Get (string key, HttpRequest request) 
        {
            var lstHeaders = request.Headers;
            if(!lstHeaders.ContainsKey("Authorization"))
                return null;
            return Get(Convert.ToString(lstHeaders["Authorization"]), key);
        }

        public static string Get(JwtSecurityToken token, string key)
        {
            return token == null || string.IsNullOrEmpty(key) ? null : Convert.ToString(token.Payload[key]);
        }

        public static string Get(string token, string key)
        {
            if (string.IsNullOrEmpty(token))
                return null;
            token = token.Trim();
            if(!token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) || token.Length < 10 || token == "Bearer null")
                return null;
            var tk = new JwtSecurityToken(token.Substring(7));
            return Get(tk, key);
        }

        public static Tokendata Get(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;
            token = token.Trim();
            if (!token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) || token.Length < 10 || token == "Bearer null")
                return null;
            var tk = new JwtSecurityToken(token.Substring(7));
            return Get(tk);
        }

        public static Tokendata Get(HttpRequest request)
        {
            var lstHeaders = request.Headers;
            if(!lstHeaders.ContainsKey("Authorization"))
                return null;
            return Get(Convert.ToString(lstHeaders["Authorization"]));
        }

        public static string GetToken(HttpRequest request)
        {
            var lstHeaders = request.Headers;
            if(!lstHeaders.ContainsKey("Authorizaton"))
                return null;
            var token = Convert.ToString(lstHeaders["Authorization"]);
            if(string.IsNullOrEmpty(token))
                return null;
            token = token.Trim();
            if(!token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) || token.Length < 10 || token == "Bearer null")
                return null;
            return token;
        }

        public static Tokendata Get(JwtSecurityToken token)
        {
            return token == null ? null : new Tokendata()
            {
                UserEmail = Convert.ToString(token.Payload["user-email"]),
                UserPassword = Convert.ToString(token.Payload["user-password"])
            }; 
        } 

    }
}