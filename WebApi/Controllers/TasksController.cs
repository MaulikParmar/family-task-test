using Core;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _taskService.CreateTaskCommandHandler(command);

            return Created($"/api/members/{result.Payload.Id}", result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid id, UpdateTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _taskService.UpdateTaskCommandHandler(command);

                return Ok(result);
            }
            catch (NotFoundException<Guid>)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetAllMembersQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taskService.GetAllTasksQueryHandler();

            return Ok(result);
        }

        [HttpGet("{assignedMemberId}")]
        [ProducesResponseType(typeof(GetTasksByMemberQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByMemberId(Guid assignedMemberId)
        {
            var result = await _taskService
                .GetTasksByMemberQueryHandker(assignedMemberId);

            return Ok(result);
        }

        [HttpDelete("{taskId}")]
        [ProducesResponseType(typeof(DeleteTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            var result = await _taskService
                .DeleteTaskCommandHandler(taskId);

            return Ok(result);
        }

        [HttpPost("assign-task")]
        [ProducesResponseType(typeof(AssignTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> AssignTask(AssignTaskCommand command)
        {
            var result = await _taskService
                .AssignTaskCommandHandler(command);

            return Ok(result);
        }

        [HttpPost("complete-task/{taskId}")]
        [ProducesResponseType(typeof(CompleteTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CompleteTask(Guid taskId)
        {
            var result = await _taskService
                .CompleteTaskCommandHandler(taskId);

            return Ok(result);
        }
    }
}
