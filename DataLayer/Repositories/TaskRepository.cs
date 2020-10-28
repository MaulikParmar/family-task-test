using Core.Abstractions.Repositories;
using Domain.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class TaskRepository :  BaseRepository<Guid, Task, TaskRepository>, ITaskRepository
    {
        public TaskRepository(FamilyTaskContext context) : base(context)
        {
            
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetAll()
        {
            return await Context.Set<Task>()
                .Include(x => x.AssignMember)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<IEnumerable<Task>> GetByMemberId(Guid assignedMemberId)
        {
            return await Query.Where(x => x.AssignedMemberId == assignedMemberId).ToListAsync();
        }

        ITaskRepository IBaseRepository<Guid, Task, ITaskRepository>.NoTrack()
        {
            return base.NoTrack();
        }

        ITaskRepository IBaseRepository<Guid, Task, ITaskRepository>.Reset()
        {
            return base.Reset();
        }
    }
}
