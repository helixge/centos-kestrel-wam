public static class ArgumentsConfiguration
{
    public const string WebAppsWatchFolderPath_ParameterName = "-w";
    public const string WebAppsWatchFolderPath_ParameterDescription = "Watch folder where .net core web applications are stored. Each subfolder in s specified folder will be treated as a separate web application. Watch folder itself will be treated as a contrainer for the web application and will not be considered as a web application folder itself";

    public const string NginxConfigFolderPath_ParameterName = "-nc";
    public const string NginxConfigFolderPath_ParameterDescription = "Target folder where Nginx reverse proxy web application configuration files will be stores. One configuration file will be created for each subfolder in watch folder specified by '" + WebAppsWatchFolderPath_ParameterName + "' switch";
}