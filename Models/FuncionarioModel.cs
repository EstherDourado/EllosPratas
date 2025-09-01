using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EllosPratas.Models
{
    public class FuncionarioModel
    {
        [Key]
        public int id_funcionario { get; set; }
        public required string nome_funcionario { get; set; }
        public string telefone { get; set; } = string.Empty;
        public int id_loja { get; set; }
        public int id_nivel_acesso { get; set; }
        public required string cpf { get; set; }
    }
}
