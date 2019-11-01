using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.MagicStringHandlers.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Confessions
        {
            public const string GetConfessions = Base + "/confessions";
            public const string Get = Base + "/confessions/{confessionId}";
            public const string Create = Base + "/confessions";
            public const string Update = Base + "/confessions/{confessionId}";
            public const string Delete = Base + "/confessions/{confessionId}";
        }

        public static class Auth
        {
            public const string Register = Base + "/auth/register";
            public const string Login = Base + "/auth/login";
            public const string Refresh = Base + "/auth/refresh";
        }

        public static class Users
        {
            public const string GetUsers = Base + "/users";
            public const string Get = Base + "/users/{userId}";
            public const string Create = Base + "/users";
        }
    }
}
