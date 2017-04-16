using System;
using System.IO;
using System.Linq;
using System.Text;

public class ArgumentsParser
{
    private string[] _args;

    public string WebAppsFolderPath { get; private set; }
    public string NginxConfigFolderPath { get; private set; }
    public bool IsValid { get; private set; } = false;
    public string ValidationMessage { get; private set; }

    public ArgumentsParser(string[] args)
    {
        _args = args;
    }

    public void Parse()
    {
        WebAppsFolderPath = _args.FirstOrDefault(a => String.Equals(a, ArgumentsConfiguration.WebAppsWatchFolderPath_ParameterName, StringComparison.OrdinalIgnoreCase));
        NginxConfigFolderPath = _args.FirstOrDefault(a => String.Equals(a, ArgumentsConfiguration.NginxConfigFolderPath_ParameterName, StringComparison.OrdinalIgnoreCase));

        Validate();
    }

    private void Validate()
    {
        IsValid = false;

        StringBuilder valMsgBuilder = new StringBuilder();

        if (String.IsNullOrWhiteSpace(WebAppsFolderPath))
        {
            valMsgBuilder.AppendLine($"Web apps watch folder is not specified. Please provide '{ArgumentsConfiguration.WebAppsWatchFolderPath_ParameterName}' switch with the appropriate value");
        }
        else if (!Directory.Exists(WebAppsFolderPath))
        {
            valMsgBuilder.AppendLine($"Web apps watch folder specified by '{ArgumentsConfiguration.WebAppsWatchFolderPath_ParameterName}' switch does not exist: '{WebAppsFolderPath}'");
        }

        if (String.IsNullOrWhiteSpace(NginxConfigFolderPath))
        {
            valMsgBuilder.AppendLine($"Nginx configuration file storage folder is not specified. Please provide '{ArgumentsConfiguration.NginxConfigFolderPath_ParameterName}' switch with the appropriate value");
        }
        else if (!Directory.Exists(NginxConfigFolderPath))
        {
            valMsgBuilder.AppendLine($"Nginx configuraiton file storage folder specified by '{ArgumentsConfiguration.NginxConfigFolderPath_ParameterName}' switch does not exist: '{NginxConfigFolderPath}'");
        }

        IsValid = valMsgBuilder.Length > 0;
        ValidationMessage = valMsgBuilder.ToString();
    }
}