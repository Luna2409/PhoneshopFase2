using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Enums;
using Phoneshop.Domain.Interfaces;
using System;

namespace Phoneshop.Business.Loggers
{
    public class DBLogger : ILogger
    {
        private readonly IRepository<Log> _logRepository;

        public DBLogger(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        public void Error(string message)
        {
            var level = Level.Error;
            CreateNewLog(message, level);
        }

        public void Error(Exception ex)
        {
            var level = Level.Error;
            CreateNewLog(ex, level);
        }

        public void Info(string message)
        {
            var level = Level.Information;
            CreateNewLog(message, level);
        }

        private void CreateNewLog(string message, Level level)
        {
            var log = new Log
            {
                Level = Enum.GetName(typeof(Level), level),
                Message = message,
                DateTime = DateTime.Now,
            };
            _logRepository.Create(log);
        }

        private void CreateNewLog(Exception ex, Level level)
        {
            var log = new Log
            {
                Level = Enum.GetName(typeof(Level), level),
                Message = ex.Message,
                DateTime = DateTime.Now,
            };
            _logRepository.Create(log);
        }
    }
}
