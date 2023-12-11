using System.Security.Principal;

namespace AED_Paradigmas.Models
{
    public class PessoaModel
    {
        public String name {get;set;}
        public String lastName {get;set;}
        public String cpf {get;set;}
       
       public PessoaModel()
       {
        
       }
       public PessoaModel(String name, String lastName, String cpf)
       {
            this.name=name;
            this.lastName=lastName;
            this.cpf=cpf;
       }
    }
}