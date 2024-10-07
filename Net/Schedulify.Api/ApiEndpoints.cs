namespace Schedulify.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Calendars
    {
        private const string Base = $"{ApiBase}/calendars";
        
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetByUser = $"{Base}/user"; //OPTIONAL ID ELSE GET FROM JWS
        public const string Create = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
    
    public static class Categories
    {
        private const string Base = $"{ApiBase}/categories";
        
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetByUser = $"{Base}/user"; //OPTIONAL ID ELSE GET FROM JWS
        public const string Create = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
    
    public static class Schedules
    {
        private const string Base = $"{ApiBase}/schedules";
        
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetByUser = $"{Base}/user"; //OPTIONAL ID ELSE GET FROM JWS
        public const string Create = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }

    public static class Users
    {
        private const string Base = $"{ApiBase}/auth";
        
        public const string Login = $"{Base}/login";
        public const string Register = $"{Base}/regiser";
    }
}