using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;

namespace ZRTSMapEditor.UI
{
    public class RenderMapViewGameworldVisitor : NoOpModelComponentVisitor
    {
        private MapView mapView;

        private RenderMapViewGameworldVisitor()
        { }

        public RenderMapViewGameworldVisitor(MapView mapView)
        {
            this.mapView = mapView;
        }

        override public void Visit(Gameworld gameworld)
        {
            mapView.Refresh();
        }
    }
}
