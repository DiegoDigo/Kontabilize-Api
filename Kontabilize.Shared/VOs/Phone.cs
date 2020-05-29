namespace Kontabilize.Shared.VOs
{
    public class Phone
    {
        public string FixNumber { get; private set; }
        public string MobileNumber { get; private set; }

        public Phone()
        {

        }
        public Phone(string fixNumber, string mobileNumber)
        {
            FixNumber = fixNumber;
            MobileNumber = mobileNumber;
        }

        public Phone PhoneFix(string fixNumber)
        {
            this.FixNumber = fixNumber;
            return this;
        }

        public Phone PhoneMobile(string mobileNumber)
        {
            this.MobileNumber = mobileNumber;
            return this;
        }
    }
}