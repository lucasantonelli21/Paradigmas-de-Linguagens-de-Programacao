using System.ComponentModel;
using System.Linq.Expressions;
using System.Net.WebSockets;
using Newtonsoft.Json;
namespace AED_Paradigmas.Models
{
    public class BancoModel
    {
        public List<ContaModel> accounts {get; set;}
        public List<PessoaModel> clients {get; set;}
        public List<TransacaoModel> transactions {get; set;}

        public BancoModel()
        {    }
        public BancoModel(List<ContaModel> accounts, List<PessoaModel> clients)
        {
            this.accounts=accounts;
            this.clients=clients;
            this.transactions = new List<TransacaoModel>();
        }
        public BancoModel(List<ContaModel> accounts, List<PessoaModel> clients, List<TransacaoModel> transactions)
        {
            this.accounts=accounts;
            this.clients=clients;
            this.transactions=transactions;
        }



         public void cadastrarConta(){
            Console.WriteLine("Pessoa existente no sistema? 0 - Nao  1 - Sim");
            int option = Convert.ToInt16(Console.ReadLine());
            if(option == 0){
                 Console.WriteLine("Digite Nome da Pessoa:");
                 String name = Console.ReadLine();
                 Console.WriteLine("Digite Sobrenome da Pessoa:");
                 String lastName = Console.ReadLine();
                 Console.WriteLine("Digite CPF da Pessoa:");
                 String cpf = Console.ReadLine();
                 PessoaModel client = new PessoaModel(name,lastName,cpf);
                 int contas = Convert.ToInt16(this.accounts.Count) +1;
                 ContaModel account = new ContaModel(contas,client);
                 this.clients.Add(client);
                 this.accounts.Add(account);
                 Console.WriteLine($"Seu numero de conta e {account.number}");
            }
            else{
                Console.WriteLine("Digite o nome do cliente");
                String name = Console.ReadLine();
                if(option == 1 ){
                    foreach(var i in clients)
                        if(i.name.Equals(name)){
                            int contas = Convert.ToInt16(this.accounts.Count) +1;
                            ContaModel account = new ContaModel(contas,i);
                            Console.WriteLine($"Seu numero de conta e {account.number}");
                        }
                    }
                }
            
         }



        public ContaModel selecionarConta(int numeroConta){
            foreach(var conta in accounts){
                if(conta.number == numeroConta){
                    return conta;
                }
            }
            return null;
         }


        public void realizarTransferencia(ContaModel sender , ContaModel receiver)
        {   
            Console.WriteLine("Informe o valor que deseja transferir: ");
            decimal amount = Convert.ToDecimal( Console.ReadLine());
             if(sender.balance>=amount){
                sender.balance=sender.balance-amount;
                receiver.balance=receiver.balance+amount;
                Console.WriteLine($"Voce enviou R${amount} para {receiver.client.name}, seu novo saldo e R${sender.balance}");
                TransacaoModel transaction = new TransacaoModel(receiver.client.name,"Transferencia",sender.client.name,amount);
                this.transactions.Add(transaction);
    
            }
            else{
                Console.WriteLine($"Saldo insuficiente para Transferencia. Seu saldo e {sender.balance}");
    
            }
        }




        public void realizarTransacao(){
            Console.WriteLine("Informe o numero da sua Conta");
            int numeroConta = Convert.ToInt16(Console.ReadLine());
            ContaModel account = selecionarConta(numeroConta);
            if(account!=null){
                 Console.WriteLine("Escolha sua transacao: 1- sacar  2- depositar 3- para transferencia");
                int option = Convert.ToInt16(Console.ReadLine());
                if(option==1){
                    Console.WriteLine("Informe o valor em R$ que deseja sacar");
                    decimal valor = Convert.ToDecimal(Console.ReadLine());
                    TransacaoModel transaction = account.sacar(valor);
                    if(transaction!=null){
                        this.transactions.Add(transaction);
                    }
                    
                }
                else{
                        if(option==2){
                            Console.WriteLine("Informe o valor em R$ que deseja depositar");
                            decimal valor = Convert.ToDecimal(Console.ReadLine());
                            TransacaoModel transaction = account.depositar(valor);
                            if(transaction!=null){
                                this.transactions.Add(transaction);
                            }
                         }
                         else{
                               if(option==3){
                                    Console.WriteLine("Informe o numero da conta que deseja transferir: ");
                                    int accountNumber = Convert.ToInt16(Console.ReadLine());
                                    ContaModel receiver = selecionarConta(accountNumber);
                                    if(receiver!=null){
                                        realizarTransferencia(account,receiver);
                                    }

                               }
                               
                                else{
                                     Console.WriteLine("Operacao Invalida.");
                                 }
                         }
                     }
             }
        }

        public void inspecionarConta(){
             Console.WriteLine("Informe o numero da sua Conta");
            int numeroConta = Convert.ToInt16(Console.ReadLine());
            ContaModel account = selecionarConta(numeroConta);
            if(account!=null){
                account.checarConta();
            }
            else{
                Console.WriteLine("Conta Inexistente.");
            }
        }


        public void open(){
            bool loop = true;
            while(loop){

                Console.WriteLine(" 1 - para Cadastrar Conta\n 2 - para Realizar uma Transacao\n 3 - para Checar sua conta\n 4 para sair");
                int option = Convert.ToInt16(Console.ReadLine());
                switch(option){
                    case 1:
                        cadastrarConta();
                        break;
                    case 2:
                        realizarTransacao();
                        break;
                    case 3:
                        inspecionarConta();
                        break;
                    case 4:
                        String transactions = JsonConvert.SerializeObject(this.transactions, Formatting.Indented);
                        String clients = JsonConvert.SerializeObject(this.clients, Formatting.Indented);
                        String accounts = JsonConvert.SerializeObject(this.accounts, Formatting.Indented);
                        try{
                            StreamWriter sw  = new StreamWriter(@"Files\transactions.json");
                            StreamWriter sw2  = new StreamWriter(@"Files\clients.json");
                            StreamWriter sw3 = new StreamWriter(@"Files\accounts.json");
                            sw.Write(transactions);  
                            sw2.Write(clients);
                            sw3.Write(accounts);
                            sw.Close();
                            sw2.Close();
                            sw3.Close();
                            loop = false;
                        }
                        catch(Exception ex){
                            Console.WriteLine("Erro ao salvar.");
                            loop = false;
                        }

                        break;
                    default:
                        transactions = JsonConvert.SerializeObject(this.transactions, Formatting.Indented);
                        clients = JsonConvert.SerializeObject(this.clients, Formatting.Indented);
                        accounts = JsonConvert.SerializeObject(this.accounts, Formatting.Indented);
                        try{
                            StreamWriter sw  = new StreamWriter(@"Files\transactions.json");
                            StreamWriter sw2  = new StreamWriter(@"Files\clients.json");
                            StreamWriter sw3 = new StreamWriter(@"Files\accounts.json");
                            sw.Write(transactions);  
                            sw2.Write(clients);
                            sw3.Write(accounts);
                            sw.Close();
                            sw2.Close();
                            sw3.Close();
                            loop = false;
                        }
                        catch(Exception ex){
                            Console.WriteLine("Erro ao salvar.");
                            loop = false;
                        }
                        
                        break;  
                    
                }      
                
            }
            
        }
    }
}
