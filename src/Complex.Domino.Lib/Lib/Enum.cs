﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public enum UserRoleType : int
    {
        Unknown = -1,
        Admin = 1,
        Teacher = 2,
        Student = 3
    }

    public enum GradeType : int
    {
        Unknown = -1,
        Signature = 1,
        Grade = 2,
        Points = 3
    }

    [Flags]
    public enum DateTimeFilter : int
    {
        None = 0,

        Active = 0x01,
        Expired = 0x02,
        Future = 0x08,

        All = Active | Expired | Future
    }
}
