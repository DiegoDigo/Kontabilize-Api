namespace Kontabilize.Domain.UserContext.Command.Output
{
    public class TokenResponseCommand
    {
        public string Token { get; private set; }

        public TokenResponseCommand(string token)
        {
            Token = token;
        }
    }
}