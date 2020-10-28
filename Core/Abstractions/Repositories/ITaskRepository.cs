using Domain.DataModels;
using System;
using System.Collections.Generic;

namespace Core.Abstractions.Repositories
{
    public interface ITaskRepository : IBaseRepository<Guid, Task, ITaskRepository>
    {
        System.Threading.Tasks.Task<IEnumerable<Task>> GetByMemberId(Guid assignedMemberId);
        System.Threading.Tasks.Task<IEnumerable<Task>> GetAll();
    }
}
