namespace FluxusApi.ViewModels;

public class ProfileToPrintViewModel
{
    public string Cnpj { get; set; }
    public string CompanyName { get; set; }
    public string ContractNotice { get; set; }
    public string ContractNumber { get; set; }
    public byte[] Logo { get; set; }
}