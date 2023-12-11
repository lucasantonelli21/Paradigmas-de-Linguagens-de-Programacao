namespace AED_Paradigmas.Models
{
    public class TransacaoModel
    {
        public String sender {get; set;}
        public String type {get; set;}
        public String receiver {get; set;}
        public decimal amount {get; set;}
        public String date {get; set;}

        public TransacaoModel()
        {
            
        }
         public TransacaoModel(String sender, String type, String receiver, decimal amount, String date)
        {
            this.sender=sender;
            this.type=type;
            this.receiver=receiver;
            this.amount=amount;
            this.date=date;
        }
        public TransacaoModel(String sender, String type, String receiver, decimal amount)
        {
            this.sender=sender;
            this.type=type;
            this.receiver=receiver;
            this.amount=amount;
            this.date=DateTime.Now.ToString("dddd, dd MMMM yyyy");
        }
         public TransacaoModel(String sender, String type, decimal amount)
        {
            this.sender=sender;
            this.type=type;
            this.receiver=sender;
            this.amount=amount;
            this.date=DateTime.Now.ToString("dddd, dd MMMM yyyy");
        }
    }
}