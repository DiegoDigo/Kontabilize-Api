namespace Kontabilize.Domain.UserContext.Command.Output
{
    public class SignInCommandResponse
    {
        public string Token { get; private set; }

        public SignInCommandResponse(string token)
        {
            Token = token;
        }
    }
}