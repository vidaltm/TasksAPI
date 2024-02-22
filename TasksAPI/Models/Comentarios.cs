﻿using System.Text.Json.Serialization;

namespace TasksAPI.Models
{
    public class Comentarios
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Comentario { get; set; }
        [JsonIgnore]
        public int TarefaId { get; set; }
    }
}