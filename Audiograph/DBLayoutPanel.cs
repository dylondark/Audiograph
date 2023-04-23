using System.Windows.Forms;

namespace Audiograph
{

    public class DBLayoutPanel : TableLayoutPanel
    {

        public DBLayoutPanel() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}