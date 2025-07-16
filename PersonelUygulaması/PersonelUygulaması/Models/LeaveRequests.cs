namespace PersonelUygulaması.Models
{
    public class LeaveRequests
    {
        public int id { get; set; }

        // foreign key for the user
        public int UserId { get; set; }

        // foreign key for leave types
        public int LeaveTypeId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string Aciklama { get; set; }
        public string Durum { get; set; }

        //  navigation property for leave types
        public LeaveTypes LeaveType { get; set; }

        // navigation property for the user
        public Users User { get; set; }
    }
}
