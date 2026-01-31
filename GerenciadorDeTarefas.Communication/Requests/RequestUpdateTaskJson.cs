using GerenciadorDeTarefas.Communication.Enums;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeTarefas.Communication.Requests;

public class RequestUpdateTaskJson
{
    [Required(ErrorMessage = "O campo Name é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Name deve ter no máximo 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "O campo Description deve ter no máximo 500 caracteres.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "O campo Priority é obrigatório.")]
    public Priority Priority { get; set; }

    [Required(ErrorMessage = "O campo DueDate é obrigatório.")]
    public DateTime DueDate { get; set; }

    [Required(ErrorMessage = "O campo Status é obrigatório.")]
    public Enums.TaskStatus Status { get; set; }
}
