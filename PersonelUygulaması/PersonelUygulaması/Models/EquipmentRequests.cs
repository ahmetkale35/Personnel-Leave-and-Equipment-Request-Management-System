namespace PersonelUygulaması.Models
{
    public class EquipmentRequests
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EquipmentItemId { get; set; }

        public int  Adet { get; set; }
        public string Açıklama { get; set; }
        public string Durum { get; set; }

        // foreign key for Onaylayan
        public int? OnaylayanId { get; set; }

        // navigation property for Onaylayan
        public Users? Onaylayan { get; set; }

        // navigation property for User
        public Users User { get; set; }


        public DateTime TalepTarihi { get; set; }

        // navigation property for EquipmentItem
        public EquipmentItems EquipmentItem { get; set; }

    }
}
