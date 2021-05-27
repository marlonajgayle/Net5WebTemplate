namespace Net5WebTemplate.Api.Routes.Version1
{
    public static class ApiRoutes
    {
        public const string Domain = "api";
        public const string Version = "v{version:apiVersion}";
        public const string Base = Domain + "/" + Version;

        public static class Auth
        {
            public const string ForgotPassword = Base + "/auth/forgot-password";
        }

        public static class Client
        {
            public const string Create = Base + "/clients";
            public const string Get = Base + "/clients/{clientId}";
            public const string GetAll = Base + "/clients";
            public const string Update = Base + "/clients/{clientId}";
            public const string Delete = Base + "/clients/{clientId}";
        }

        public static class Account
        {
            public const string Create = Base + "/register";
            public const string Login = Base + "/login";

        }
    }
}
