using AutoMapper;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.Queries;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TaskService : ITaskService
    {
        #region Properties / Fields
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public TaskService(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        #endregion

        #region Operations / Commands
        public async Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command)
        {
            var task = _mapper.Map<Domain.DataModels.Task>(command);
            var persistedTask = await _taskRepository.CreateRecordAsync(task);

            var vm = _mapper.Map<TaskVm>(persistedTask);

            return new CreateTaskCommandResult()
            {
                Payload = vm
            };
        }

        public async Task<GetAllTasksQueryResult> GetAllTasksQueryHandler()
        {
            IEnumerable<TaskVm> vm = new List<TaskVm>();

            var tasks = await _taskRepository.GetAll();

            if (tasks != null && tasks.Any())
                vm = _mapper.Map<IEnumerable<TaskVm>>(tasks);

            return new GetAllTasksQueryResult()
            {
                Payload = vm
            };
        }

        public async Task<GetTasksByMemberQueryResult> GetTasksByMemberQueryHandker(Guid assignedMemberId)
        {
            IEnumerable<TaskVm> vm = new List<TaskVm>();
            _taskRepository.Reset();
            var tasks = await _taskRepository.GetByMemberId(assignedMemberId);

            if (tasks != null && tasks.Any())
                vm = _mapper.Map<IEnumerable<TaskVm>>(tasks);

            return new GetTasksByMemberQueryResult()
            {
                Payload = vm
            };
        }

        public async Task<UpdateTaskCommandResult> UpdateTaskCommandHandler(UpdateTaskCommand command)
        {
            var isSucceed = true;
            var task = await _taskRepository.ByIdAsync(command.Id);

            _mapper.Map<UpdateTaskCommand, Domain.DataModels.Task>(command, task);

            var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);

            if (affectedRecordsCount < 1)
                isSucceed = false;

            return new UpdateTaskCommandResult()
            {
                Succeed = isSucceed
            };
        }

        public async Task<AssignTaskCommandResult> AssignTaskCommandHandler(AssignTaskCommand command)
        {
            var isSucceed = true;
            var task = await _taskRepository.ByIdAsync(command.TaskId);

            task.AssignedMemberId = command.MemberId;

            var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);

            if (affectedRecordsCount < 1)
                isSucceed = false;

            return new AssignTaskCommandResult()
            {
                Succeed = isSucceed
            };
        }

        public async Task<CompleteTaskCommandResult> CompleteTaskCommandHandler(Guid taskId)
        {
            var isSucceed = true;
            var task = await _taskRepository.ByIdAsync(taskId);

            task.IsComplete = true;

            var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);

            if (affectedRecordsCount < 1)
                isSucceed = false;

            return new CompleteTaskCommandResult()
            {
                Succeed = isSucceed
            };
        }

        public async Task<DeleteTaskCommandResult> DeleteTaskCommandHandler(Guid taskId)
        {
            var isSucceed = true;
            var task = await _taskRepository.ByIdAsync(taskId);

            task.IsComplete = true;

            var affectedRecordsCount = await _taskRepository.DeleteRecordAsync(taskId);

            if (affectedRecordsCount < 1)
                isSucceed = false;

            return new DeleteTaskCommandResult()
            {
                Succeed = isSucceed
            };
        }

        #endregion
    }
}
