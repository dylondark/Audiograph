using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// custom label, sets usemnemonic to false by default

namespace Audiograph
{
    public partial class Label : System.Windows.Forms.Label
    {
        public Label() : base()
        {
            this.UseMnemonic = false;
        }
    }
}
