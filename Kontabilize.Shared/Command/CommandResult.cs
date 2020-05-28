namespace Kontabilize.Shared.Command
{
    public class CommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Content { get; set; }

        public CommandResult()
        {
        }

        public CommandResult(bool success, string message, object content)
        {
            Success = success;
            Message = message;
            Content = content;
        }
    }
}