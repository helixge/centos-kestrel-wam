using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

public class WebAppWatcher
{
    private const int InitialPortNumber = 5000;

    private string _webAppsFolderPath;
    private string _nginxConfigFolderPath;
    private List<string> _folderList = new List<string>();

    public WebAppWatcher(ArgumentsParser argumentsParser)
    {
        _webAppsFolderPath = argumentsParser.WebAppsFolderPath;
        _nginxConfigFolderPath = argumentsParser.NginxConfigFolderPath;
    }

    public void Watch()
    {
        while (true)
        {
            try
            {
                if (FoldersChanged())
                {
                    UpdateConfiguration();
                }
            }
            finally
            {
                Thread.Sleep(1000);
            }
        }
    }

    private bool FoldersChanged()
    {
        List<string> newFolderList = GetFolderList();
        if (FolderListChanged(_folderList, newFolderList))
        {
            _folderList.Clear();
            _folderList.AddRange(newFolderList);
            return true;
        }
        return false;
    }

    private List<string> GetFolderList()
    {
        string[] folders = Directory.GetDirectories(_webAppsFolderPath);
        return folders.ToList();
    }

    private bool FolderListChanged(List<string> folderList, List<string> newFolderList)
    {
        return folderList.SequenceEqual(newFolderList);
    }

    private void UpdateConfiguration()
    {
        DeleteAllKesterlDaemons();
        DeleteAllExistingNginxConfigurationFiles();

        if (_folderList != null)
        {
            for (int folderIndex = 0; folderIndex < _folderList.Count; folderIndex++)
            {
                string folderName = Path.GetDirectoryName(_folderList[folderIndex]);
                int port = GeneratePortNumber(folderIndex);
                CreateNginxConfigurationFileForFolder(folderName, port);
                CreateKesterlDaemon();
            }
        }

        ReloadAllKesterlDaemons();
        ReloadnginxConfiguration();
    }

    private int GeneratePortNumber(int folderIndex)
    {
        return InitialPortNumber + folderIndex;
    }
    private void DeleteAllExistingNginxConfigurationFiles()
    {
        File.Delete(Path.Combine(_nginxConfigFolderPath, "*.conf"));
    }

    private void CreateNginxConfigurationFileForFolder(string folderName, int port)
    {
        string configContent = GenerateNginxConfigurationFileContent(folderName, port);
        string configFileName = Path.Combine(_nginxConfigFolderPath, $"{folderName}.conf");
        using (Stream configFileStream = File.Create(configFileName))
        {
            using (StreamWriter configFileWriter = new StreamWriter(configFileStream))
            {
                configFileWriter.Write(configContent);
            }
        }
    }

    private string GenerateNginxConfigurationFileContent(string folderName, int portNumber)
    {
        string content = @"
            server {
                listen 80;
                listen[::]:80;
                " + folderName + @";

                location / {
                    proxy_pass http://localhost:" + portNumber + @";
                    proxy_http_version 1.1;
                    proxy_set_header Upgrade $http_upgrade;
                    proxy_set_header Connection keep - alive;
                    proxy_set_header Host $host;
                    proxy_cache_bypass $http_upgrade;
                }
            }
        ";

        return content;
    }

    public void DeleteAllKesterlDaemons()
    {
        StopAllKestrelDaemons();

        //TODO: Implement
        throw new NotImplementedException();
    }
    private void StopAllKestrelDaemons()
    {
        //TODO: Implement
        throw new NotImplementedException();
    }
    private void ReloadnginxConfiguration()
    {
        //TODO: Implement
        throw new NotImplementedException();
    }

    private void ReloadAllKesterlDaemons()
    {
        //TODO: Implement
        throw new NotImplementedException();
    }
    private void CreateKesterlDaemon()
    {
        //TODO: create and enable a kestrel daemon
        throw new NotImplementedException();
    }
}