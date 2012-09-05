using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsModel.Cqrs
{
    public interface DocumentBasedAggregate
    {
        void SetId(Guid id);
    }
}
