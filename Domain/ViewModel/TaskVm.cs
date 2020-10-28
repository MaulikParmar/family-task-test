using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModel
{
    public class TaskVm
    {
        public Guid AssignedMemberId { get; set; }
        public Guid Id { get; set; }
        public bool IsComplete { get; set; }
        public string Subject { get; set; }
        public string Avatar { get; set; }
    }
}
