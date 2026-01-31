using GerenciadorDeTarefas.Application.Services;
using GerenciadorDeTarefas.Communication.Models;
using GerenciadorDeTarefas.Communication.Requests;
using GerenciadorDeTarefas.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using TaskEntity = GerenciadorDeTarefas.Communication.Models.TaskItem;

namespace GerenciadorDeTarefas.API.Controllers;

/// <summary>
/// Controller da API de tarefas (camada de comunicação).
/// </summary>
[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    /// <summary>
    /// Cria uma nova tarefa.
    /// </summary>
    /// <param name="request">Dados da tarefa.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <response code="201">Tarefa criada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(TaskEntity), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] RequestTaskJson request,
        CancellationToken cancellationToken)
    {
        if (request == null)
            return BadRequest(new ResponseErrorsJson { Erros = ["O corpo da requisição não pode ser nulo."] });

        if (!ModelState.IsValid)
            return BadRequest(ObterResponseErros(ModelState));

        var task = await _taskService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = task!.Id }, task);
    }

    /// <summary>
    /// Lista todas as tarefas.
    /// </summary>
    /// <response code="200">Lista de tarefas retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TaskEntity>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tasks = await _taskService.GetAllAsync(cancellationToken);
        return Ok(tasks);
    }

    /// <summary>
    /// Busca uma tarefa pelo ID.
    /// </summary>
    /// <param name="id">Identificador da tarefa (GUID).</param>
    /// <response code="200">Tarefa encontrada.</response>
    /// <response code="404">Tarefa não encontrada.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TaskEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskService.GetByIdAsync(id, cancellationToken);
        if (task == null)
            return NotFound(new ResponseErrorsJson { Erros = ["Tarefa não encontrada."] });

        return Ok(task);
    }

    /// <summary>
    /// Atualiza uma tarefa existente.
    /// </summary>
    /// <param name="id">Identificador da tarefa (GUID).</param>
    /// <param name="request">Novos dados da tarefa.</param>
    /// <response code="200">Tarefa atualizada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Tarefa não encontrada.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(TaskEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] RequestUpdateTaskJson request,
        CancellationToken cancellationToken)
    {
        if (request == null)
            return BadRequest(new ResponseErrorsJson { Erros = ["O corpo da requisição não pode ser nulo."] });

        if (!ModelState.IsValid)
            return BadRequest(ObterResponseErros(ModelState));

        var task = await _taskService.UpdateAsync(id, request, cancellationToken);
        if (task == null)
            return NotFound(new ResponseErrorsJson { Erros = ["Tarefa não encontrada."] });

        return Ok(task);
    }

    /// <summary>
    /// Exclui uma tarefa pelo ID.
    /// </summary>
    /// <param name="id">Identificador da tarefa (GUID).</param>
    /// <response code="204">Tarefa excluída com sucesso.</response>
    /// <response code="404">Tarefa não encontrada.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _taskService.DeleteAsync(id, cancellationToken);
        if (!deleted)
            return NotFound(new ResponseErrorsJson { Erros = ["Tarefa não encontrada."] });

        return NoContent();
    }

    private static ResponseErrorsJson ObterResponseErros(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
    {
        var erros = new List<string>();
        foreach (var state in modelState.Values)
        {
            if (state.Errors == null) continue;
            foreach (var error in state.Errors)
                erros.Add(error.ErrorMessage ?? error.Exception?.Message ?? "Erro de validação.");
        }
        return new ResponseErrorsJson { Erros = erros };
    }
}
