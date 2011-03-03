using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel.Entities;

namespace ZRTSModel.Scenario
{
    /// <summary>
    /// ViewSelectObserver Objects
    /// Observes only the Unit(s) that are currently selected by the User
    /// 
    /// </summary>
    //[Serializable()]
    interface ViewSelectObserver
    {
        /// <summary>
        /// Return List of the selected Units
        /// 
        /// </summary>
        List<Unit> getSelectedUnits
        { get; set; }

    }
}
