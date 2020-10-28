using System;

namespace Domain.Commands
{
    public class AssignTaskCommand
    {
        public Guid TaskId { get; set; }
        public Guid MemberId { get; set; }
    }
}
