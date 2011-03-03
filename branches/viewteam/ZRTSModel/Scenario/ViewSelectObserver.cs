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
    /// Based Interface: Observer
    /// </summary>
    //[Serializable()]
    public interface ViewSelectObserver:Observer
    {
        /// <summary>
        /// Return List of the selected Units
        /// 
        /// </summary>
        List<Entity> getSelectedUnits
        { get; set; }


        void removeEverything();
        void addUnit(ZRTSModel.Entities.Entity e);
    }
}
