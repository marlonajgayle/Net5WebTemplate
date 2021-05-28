namespace Net5WebTemplate.Api.Routes.Version1
{
    public static class ApiRoutes
    {
        public const string Domain = "api";
        public const string Version = "v{version:apiVersion}";
        public const string Base = Domain + "/" + Version;

        public static class Auth
        {
            public const string Login = Base + "/auth/login";
            public const string ForgotPassword = Base + "/auth/forgot-password";
            public const string ResetPassword = Base + "/auth/reset-password";
        }

        public static class Account
        {
            public const string Create = Base + "/account/register";
        }

        public static class Client
        {
            public const string Create = Base + "/clients";
            public const string Get = Base + "/clients/{clientId}";
            public const string GetAll = Base + "/clients";
            public const string Update = Base + "/clients/{clientId}";
            public const string Delete = Base + "/clients/{clientId}";
        }
    }
}
