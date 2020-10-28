using Domain.ViewModel;
using System.Collections.Generic;

namespace Domain.Queries
{
    public class GetTasksByMemberQueryResult
    {
        public IEnumerable<TaskVm> Payload { get; set; }
    }
}
