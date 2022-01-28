using Phoneshop.Domain.Interfaces;
using System;
using System.IO;

namespace Phoneshop.Business.Loggers
{
    public class FileLogger : ILogger
    {
        const string LogPath = @"C:\temp\logging\Logger.txt";

        public void Error(string message)
        {
            using StreamWriter file = new(LogPath, append: true);
            file.WriteLine("Error: " + message);
        }

        public void Error(Exception ex)
        {
            using StreamWriter file = new(LogPath, append: true);
            file.WriteLine("Error: " + ex.Message);
        }

        public void Info(string message)
        {
            using StreamWriter file = new(LogPath, append: true);
            file.WriteLine("INFO: " + message);
        }
    }
}
