using Phoneshop.Domain.Interfaces;
using Phoneshop.Business.Loggers;

namespace Phoneshop.Tests.FileLoggerTests
{
    public class Error
    {
        private readonly ILogger _logger;

        public Error()
        {
            _logger = new FileLogger();
        }

    }
}
