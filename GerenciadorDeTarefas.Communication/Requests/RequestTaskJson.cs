using GerenciadorDeTarefas.Communication.Enums;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeTarefas.Communication.Requests;

public class RequestTaskJson
{
    [Required(ErrorMessage = "O campo Name é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Name deve ter no máximo 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "O campo Description deve ter no máximo 500 caracteres.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "O campo Priority é obrigatório.")]
    public Priority Priority { get; set; }

    [Required(ErrorMessage = "O campo DueDate é obrigatório.")]
    [CustomValidation(typeof(RequestTaskJson), nameof(ValidateDueDate))]
    public DateTime DueDate { get; set; }

    [Required(ErrorMessage = "O campo Status é obrigatório.")]
    public Enums.TaskStatus Status { get; set; }

    public static ValidationResult? ValidateDueDate(DateTime dueDate, ValidationContext context)
    {
        if (dueDate.Date < DateTime.UtcNow.Date)
            return new ValidationResult("A data limite não pode ser no passado.");
        return ValidationResult.Success;
    }
}
