using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;

namespace ZRTSMapEditor.UI
{
    class RenderMapViewGameworldVisitor : NoOpModelComponentVisitor
    {
        private MapView view;

        internal void SetMapView(MapView mapView)
        {
            view = mapView;
        }

        public override void Visit(Gameworld gameworld)
        {
            view.render();
            base.Visit(gameworld);
        }
    }
}
