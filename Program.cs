using System;
using System.Collections;
using System.Text.Json.Nodes;
using AED_Paradigmas.Models;
using Newtonsoft;
using Newtonsoft.Json;


try{
    StreamReader sr = new StreamReader(@"Files\transactions.json");
    StreamReader sr2 = new StreamReader(@"Files\clients.json");
    StreamReader sr3 = new StreamReader(@"Files\accounts.json");
    String transactionsString = sr.ReadToEnd();
    String clientsString = sr2.ReadToEnd();
    String accountsString = sr3.ReadToEnd();
    sr.Close();
    sr2.Close();
    sr3.Close();
    List<TransacaoModel> transactions = JsonConvert.DeserializeObject<List<TransacaoModel>>(transactionsString);
    List<PessoaModel> clients = JsonConvert.DeserializeObject<List<PessoaModel>>(clientsString);
    List<ContaModel> accounts = JsonConvert.DeserializeObject<List<ContaModel>>(accountsString);
    BancoModel banco = new BancoModel(accounts, clients, transactions);
    Console.WriteLine("Seja Bem Vindo ao Sistema");
    banco.open();
}
catch (Exception ex)
{
    Console.WriteLine("Erro ao Iniciar Banco.");
}
