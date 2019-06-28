using System;
namespace ferrilata_devilline.Models.ErrorModels
{
    public class CustomError
    {
        public string Error { get; }

        public CustomError(string message)
        {
            Error = message;
        }
    }
}
