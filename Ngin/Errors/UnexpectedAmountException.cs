using System;

namespace Ngin.Errors;

public class UnexpectedAmountException : Exception
{
    public UnexpectedAmountException(string errorMessage) : base(errorMessage)
    {
        
    }
}