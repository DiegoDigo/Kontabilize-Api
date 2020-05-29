namespace Kontabilize.Shared.VOs
{
    public class Document
    {
        public string Cpf { get; private set; }
        public string Cnpj { get; private set; }
        
        public Document()
        {

        }
        public Document(string cpf, string cnpj)
        {
            this.Cpf = cpf;
            this.Cnpj = cnpj;
        }
        public Document DocumentCpf(string cpf)
        {
            this.Cpf = cpf;
            return this;
        }
        public Document DocumentCnpj(string cnpj)
        {
            this.Cnpj = cnpj;
            return this;
        }
    }
}