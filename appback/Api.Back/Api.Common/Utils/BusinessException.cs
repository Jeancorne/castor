namespace Api.Common.Utils
{
    public class BusinessException(string message) : Exception(message)
    {
    }
}