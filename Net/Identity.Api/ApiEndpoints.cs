namespace Identity.Api;

public class ApiEndpoints
{
        private const string ApiBase = "/auth";
        
        public const string Register = $"{ApiBase}/register";
        public const string Login = $"{ApiBase}/login";
}