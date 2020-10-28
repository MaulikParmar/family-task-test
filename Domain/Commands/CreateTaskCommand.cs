using System;

namespace Domain.Commands
{
    public class CreateTaskCommand
    {
        public Guid? AssignedMemberId { get; set; }
        public bool IsComplete { get; set; }
        public string Subject { get; set; }
    }
}
