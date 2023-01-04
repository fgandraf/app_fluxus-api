
namespace FluxusApi.Entities
{

    public class Profissional
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Nomeid { get; set; }
        public string Cpf { get; set; }
        public string Nascimento { get; set; }
        public string Profissao { get; set; }
        public string Carteira { get; set; }
        public string Entidade { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Email { get; set; }
        public bool ResponsavelTecnico { get; set; }
        public bool ResponsavelLegal { get; set; }
        public bool Usr_ativo { get; set; }
        public string UsuarioNome { get; set; }
        public string UsuarioSenha { get; set; }
    }


}
