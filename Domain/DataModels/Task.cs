using System;

namespace Domain.DataModels
{
    public class Task
    {
        public Guid? AssignedMemberId { get; set; }
        public Guid Id { get; set; }
        public bool IsComplete { get; set; }
        public string Subject { get; set; }

        public Member AssignMember { get; set; }
    }
}
