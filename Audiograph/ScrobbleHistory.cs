using System;

namespace Audiograph
{
    public partial class frmScrobbleHistory
    {
        public frmScrobbleHistory()
        {
            InitializeComponent();
        }
        private void ResizeOps(object sender, EventArgs e)
        {
            ltvHistory.Width = Width - 40;
            ltvHistory.Height = Height - 92;
        }

        private void ExitForm(object sender, EventArgs e)
        {
            Close();
        }

        // updates list view ~60 times per second, i hate that i have to do this but vb sucks
        public void AddListView(object sender, EventArgs e)
        {
            // compare count of list and count of listview
            if (Utilities.scrobblehistory.Count > ltvHistory.Items.Count)
            {
                // add every item that is not already added to the listview
                for (int count = ltvHistory.Items.Count, loopTo = Utilities.scrobblehistory.Count - 1; count <= loopTo; count++)
                    ltvHistory.Items.Insert(0, Utilities.scrobblehistory[count][0]).SubItems.AddRange(new[] { Utilities.scrobblehistory[count][1], Utilities.scrobblehistory[count][2], Utilities.scrobblehistory[count][3], Utilities.scrobblehistory[count][4], Utilities.scrobblehistory[count][5], Utilities.scrobblehistory[count][6] });
            }

            // check for max items
            if (ltvHistory.Items.Count > 100000)
            {
                ltvHistory.Items.Clear();
            }
        }

        private void ClearHistory(object sender, EventArgs e)
        {
            Utilities.scrobblehistory.Clear();
            ltvHistory.Items.Clear();
        }
    }
}