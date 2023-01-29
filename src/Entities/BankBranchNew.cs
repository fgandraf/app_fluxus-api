
namespace FluxusApi.Entities
{
    public class BankBranchNew
    {
        public long id { get; set; }
        public string branch_number { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string complement { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string state { get; set; }
        public string contactName { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string email { get; set; }
    }
}
