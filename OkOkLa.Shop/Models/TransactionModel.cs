namespace OkOkLa.Shop.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public string? Akungame_Id { get; set; }
        public string? Game { get; set; }
        public string? Game_Money { get; set; }
        public int? Harga { get; set; }
        public string? Metode_Pembayaran { get; set; }
        public string? Email { get; set; }

    }
}
