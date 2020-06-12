namespace Kontabilize.Domain.UserContext.Command.Output
{
    public class ResetPasswordCommandResponse
    {
        public string Id { get; set; }
        public string Url { get; set; }

        public ResetPasswordCommandResponse(string id, string url)
        {
            Id = id;
            Url = url;
        }
    }
}