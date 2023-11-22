﻿namespace TestApplication.Services
{
    public class ValidationException : Exception
    {
        public string Key { get; }

        public ValidationException(string key, string message) : base(message)
        {
            Key = key;
        }
    }
}
