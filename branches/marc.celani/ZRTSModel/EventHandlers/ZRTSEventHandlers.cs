using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZRTSModel.EventHandlers
{
    #region ZRTS Delegates

    public delegate void ScenarioChangedHandler(Object sender, ScenarioChangedEventArgs args);
    public delegate void TileChangedHandler(Object sender, TileChangedEventArgs args);
    public delegate void PlayerListChangedHandler(Object sender, PlayerListChangedEventArgs args);
    public delegate void UnitAddedToPlayerListHandler(Object sender, UnitAddedEventArgs args);
    public delegate void UnitRemovedFromPlayerListHandler(Object sender, UnitRemovedEventArgs args);

    #endregion
}
