﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        string Name { get; set; }
       // public DateTime CreatingTime { get; set; }

    }
}