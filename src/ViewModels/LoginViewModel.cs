using System.ComponentModel.DataAnnotations;

namespace FluxusApi.ViewModels;

public class LoginViewModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
}