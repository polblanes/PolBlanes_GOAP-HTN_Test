using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ICreatable : IHoldable
    {
        public int BuildPoints { get; set; }
    }
}