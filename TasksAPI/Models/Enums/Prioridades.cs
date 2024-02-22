using System.ComponentModel;

namespace TasksAPI.Models.Enums
{
    public enum Prioridades
    {
        [Description("Baixa")]
        Baixa = 0,
        [Description("Media")]
        Media = 1,
        [Description("Alta")]
        Alta = 2
    }
}
