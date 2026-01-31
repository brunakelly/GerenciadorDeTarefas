using GerenciadorDeTarefas.Communication.Requests;
using TaskEntity = GerenciadorDeTarefas.Communication.Models.TaskItem;

namespace GerenciadorDeTarefas.Application.Services;

public interface ITaskService
{
    Task<TaskEntity?> CreateAsync(RequestTaskJson request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TaskEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TaskEntity?> UpdateAsync(Guid id, RequestUpdateTaskJson request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
