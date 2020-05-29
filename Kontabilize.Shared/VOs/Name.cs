namespace Kontabilize.Shared.VOs
{
    public class Name
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string GetFullName()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}