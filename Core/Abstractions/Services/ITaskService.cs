using Domain.Commands;
using Domain.Queries;
using System;
using System.Threading.Tasks;

namespace Core.Abstractions.Services
{
    public interface ITaskService
    {
        Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command);
        Task<UpdateTaskCommandResult> UpdateTaskCommandHandler(UpdateTaskCommand command);
        Task<GetAllTasksQueryResult> GetAllTasksQueryHandler();
        Task<GetTasksByMemberQueryResult> GetTasksByMemberQueryHandker(Guid assignedMemberId);
        Task<DeleteTaskCommandResult> DeleteTaskCommandHandler(Guid taskId);
        Task<AssignTaskCommandResult> AssignTaskCommandHandler(AssignTaskCommand command);
        Task<CompleteTaskCommandResult> CompleteTaskCommandHandler(Guid taskId);
    }
}
