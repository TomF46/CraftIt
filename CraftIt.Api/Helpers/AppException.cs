using System;
using System.Globalization;

namespace CraftIt.Api.Helpers{
    /// <summary>Class <c>App Exception</c> A helper class that implements the exception class to return an error message from a service to be returned to the user</summary>
    public class AppException : Exception
    {
        public AppException() : base() {}

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args) 
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}