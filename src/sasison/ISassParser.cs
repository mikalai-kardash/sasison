﻿using System.Collections.Generic;
using sasison.Expressions;

namespace sasison
{
    public interface ISassParser
    {
        IExpression Parse(string input);
    }
}