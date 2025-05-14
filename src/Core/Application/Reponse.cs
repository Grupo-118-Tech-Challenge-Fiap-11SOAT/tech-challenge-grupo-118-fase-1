namespace Application
{
    public class Reponse
    {
        public enum ErrorCode
        {
            // Employee related codes 1 to 99
            NotFound = 1,
            InvalidInput = 2,
        }

        public abstract class Response
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public ErrorCode ErrorCode { get; set; }
        }
    }
}
