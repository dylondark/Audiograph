using System;

namespace Audiograph
{
    public partial class frmAPIHistory
    {
        public frmAPIHistory()
        {
            InitializeComponent();
        }
        private void ResizeOps(object sender, EventArgs e)
        {
            ltvStatus.Width = Width - 40;
            ltvStatus.Height = Height - 92;
        }

        private void ExitForm(object sender, EventArgs e)
        {
            Close();
        }

        // updates list view ~60 times per second, i hate that i have to do this but vb sucks
        public void AddListView(object sender, EventArgs e)
        {
            // compare count of list and count of listview
            if (Utilities.apihistory.Count > ltvStatus.Items.Count)
            {
                // add every item that is not already added to the listview
                for (int count = ltvStatus.Items.Count, loopTo = Utilities.apihistory.Count - 1; count <= loopTo; count++)
                    ltvStatus.Items.Insert(0, Utilities.apihistory[count][0]).SubItems.AddRange(new[] { Utilities.apihistory[count][1], Utilities.apihistory[count][2], Utilities.apihistory[count][3], Utilities.apihistory[count][4], Utilities.apihistory[count][5] });
            }

            // check for max items
            if (ltvStatus.Items.Count > 100000)
            {
                ltvStatus.Items.Clear();
            }
        }

        private void ClearHistory(object sender, EventArgs e)
        {
            Utilities.apihistory.Clear();
            ltvStatus.Items.Clear();
        }
    }
}