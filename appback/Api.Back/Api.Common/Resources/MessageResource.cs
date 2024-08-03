using Api.Common.Utils;
using System.Globalization;
using System.Resources;

namespace Api.Common.Resources
{
    public class MessageResource
    {
        public string GetMessage(string folder, string language, string valueMessage, bool throwing = false, params string[] args)
        {
            string baseName = $"{folder}Message_{language}";
            string resourcesPath = $"Api.Common.Dictionary.{folder}.{language}";
            string fullResourceName = $"{resourcesPath}.{baseName}";
            ResourceManager resourceManager = new ResourceManager(fullResourceName, typeof(ReferenceMessage).Assembly);
            string? message = resourceManager.GetString(valueMessage);
            message = message ?? "";
            if (args.Length > 0)
            {
                object[] formattedArgs = new object[CountPlaceholders(message)];
                Array.Copy(args, formattedArgs, Math.Min(args.Length, formattedArgs.Length));

                message = string.Format(CultureInfo.CurrentCulture, message, formattedArgs);
            }
            if (throwing) throw new BusinessException(message);
            return message;
        }

        private int CountPlaceholders(string message)
        {
            int count = 0;
            int index = -1;
            while ((index = message.IndexOf("{", index + 1)) != -1)
            {
                count++;
            }
            return count;
        }
    }
}