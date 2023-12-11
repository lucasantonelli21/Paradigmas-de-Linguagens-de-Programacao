using System.ComponentModel.DataAnnotations;

namespace AED_Paradigmas.Models
{
    public class ContaModel
    {
        public int number {get;set;}  
        public decimal balance {get; set;} 
        public PessoaModel client {get;set;}
        public List<TransacaoModel> transactions {get; set;}
        public ContaModel()
        {           }
        public ContaModel(int accountNumber ,PessoaModel person, List<TransacaoModel> transactions)
        {
            this.number=accountNumber;
            this.client = person;
            this.transactions = transactions;
        }

        public ContaModel(int accountNumber ,PessoaModel person)
        {
            this.number=accountNumber;
            this.client = person;
            this.transactions = new List<TransacaoModel>();
        }

        public void checarConta(){
            Console.WriteLine($"Cliente: {this.client.name}, Numero da Conta: {this.number} , Saldo: R${this.balance}");
            Console.WriteLine("Transacoes:");
            foreach(var i in transactions){
                Console.WriteLine($"Enviou: {i.sender}");
                Console.WriteLine($"Tipo de Operacao: {i.type} ");
                Console.WriteLine($"Quem recebeu: {i.receiver}");
                Console.WriteLine($"Data: {i.date}");
                Console.WriteLine("");
            }
        }
        public TransacaoModel sacar(decimal valor){
            if(this.balance<=valor){
                this.balance=this.balance-valor;
                Console.WriteLine($"Voce sacou R${valor}, seu novo saldo e R${this.balance}");
                TransacaoModel transaction = new TransacaoModel(this.client.name,"Saque",valor);
                this.transactions.Add(transaction);
                return transaction;
            }
            else{
                Console.WriteLine($"Saldo insuficiente. Seu saldo e {this.balance}");
                return null;
            }
        }
        public TransacaoModel depositar(decimal valor){
            this.balance= this.balance+valor;
            Console.WriteLine($"Seu novo saldo e R${this.balance}");
            TransacaoModel transaction = new TransacaoModel(this.client.name,"Deposito",valor);
            this.transactions.Add(transaction);
            return transaction;
        }

        
    }
}