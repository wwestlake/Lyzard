﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{
    /// <summary>
    /// Connects a TimeSignal between view models
    /// </summary>
    /// <returns>An enumeration of time signals</returns>
    public delegate IEnumerable<Tuple<double, double>>  TimeSignalDelegate();

}
