using System;

namespace Phoneshop.Domain.Interfaces
{
    public interface ILogger
    {
        void Info(string message);

        void Error(string message);
        
        void Error(Exception ex);
    }
}
