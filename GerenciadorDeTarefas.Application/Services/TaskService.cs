using GerenciadorDeTarefas.Communication.Models;
using GerenciadorDeTarefas.Communication.Requests;
using TaskEntity = GerenciadorDeTarefas.Communication.Models.TaskItem;

namespace GerenciadorDeTarefas.Application.Services;

/// <summary>
/// Implementação do serviço de tarefas com armazenamento em memória.
/// </summary>
public class TaskService : ITaskService
{
    private readonly Dictionary<Guid, TaskEntity> _tasks = new();

    public Task<TaskEntity?> CreateAsync(RequestTaskJson request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var task = new TaskEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Description = request.Description?.Trim(),
            Priority = request.Priority,
            DueDate = request.DueDate,
            Status = request.Status
        };

        _tasks[task.Id] = task;
        return Task.FromResult<TaskEntity?>(task);
    }

    public Task<IReadOnlyList<TaskEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var list = _tasks.Values.ToList().AsReadOnly();
        return Task.FromResult<IReadOnlyList<TaskEntity>>(list);
    }

    public Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _tasks.TryGetValue(id, out var task);
        return Task.FromResult(task);
    }

    public Task<TaskEntity?> UpdateAsync(Guid id, RequestUpdateTaskJson request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!_tasks.TryGetValue(id, out var existing))
            return Task.FromResult<TaskEntity?>(null);

        existing.Name = request.Name.Trim();
        existing.Description = request.Description?.Trim();
        existing.Priority = request.Priority;
        existing.DueDate = request.DueDate;
        existing.Status = request.Status;

        return Task.FromResult<TaskEntity?>(existing);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_tasks.Remove(id));
    }
}
