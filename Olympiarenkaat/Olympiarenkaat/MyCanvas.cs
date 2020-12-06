using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Olympiarenkaat
{
    class MyCanvas : Canvas
    {
        public Pen penni = new Pen(Brushes.Blue, 4);
        double x = 40, y = 40;
        protected override void OnRender(DrawingContext dc)
        {
            dc.DrawEllipse(Brushes.Transparent, penni, new System.Windows.Point(x, y), 40, 40);
        }

        public void SetXY(double xx, double yy)
        {
            x = xx;
            y = yy;
            InvalidateVisual();
        }
    }
}
