﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public class UpdateTaskCommand
    {
        public Guid AssignedMemberId { get; set; }
        public Guid Id { get; set; }
        public bool IsComplete { get; set; }
        public string Subject { get; set; }
    }
}
