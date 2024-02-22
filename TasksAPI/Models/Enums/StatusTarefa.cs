using System.ComponentModel;

namespace TasksAPI.Models.Enums
{
    public enum StatusTarefa
    {
        [Description("Pendente")]
        Pendente = 0,
        [Description("Em Andamento")]
        EmAndamento = 1,
        [Description("Concluida")]
        Concluida = 2
    }
}
