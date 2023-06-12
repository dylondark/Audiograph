using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;

namespace Audiograph
{

    public partial class frmScrobbleIndexEditor
    {
        public string currentIndexLocation;
        private List<string[]> currentIndexData = new List<string[]>();
        private bool currentSaved = true;
        private bool newState = true;
        private bool searched = false;

        public frmScrobbleIndexEditor()
        {
            InitializeComponent();
        }

        #region UI
        private void ResizeOps(object sender, EventArgs e)
        {
            dgvData.Height = Height - 67;
            dgvData.Width = Width - 16;
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            KeyPreview = true;
        }

        private void ShortcutKeys(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.N)
                {
                    btnNew.PerformClick();
                }
                else if (e.KeyCode == Keys.O)
                {
                    btnOpen.PerformClick();
                }
                else if (e.Shift == false && e.KeyCode == Keys.S)
                {
                    btnSave.PerformClick();
                }
                else if (e.Shift == true && e.KeyCode == Keys.S)
                {
                    btnSaveAs.PerformClick();
                }
                else if (e.Shift == true && e.KeyCode == Keys.A)
                {
                    btnAddRow.PerformClick();
                }
                else if (e.KeyCode == Keys.R)
                {
                    btnReload.PerformClick();
                }
                else if (e.KeyCode == Keys.F)
                {
                    btnFind.PerformClick();
                }
            }
        }

        public void Saved(bool yesno)
        {
            // disable/enable button
            if ((currentIndexLocation ?? "") == (My.MySettingsProperty.Settings.CurrentScrobbleIndex ?? ""))
            {
                btnSetIndex.Enabled = false;
            }
            else
            {
                btnSetIndex.Enabled = true;
            }

            // only run when needed
            if (yesno == currentSaved)
            {
                return;
            }

            if (yesno == true)
            {
                // remove asterisk
                Text = Text.Replace("*", string.Empty);
                btnSave.Enabled = false;
                currentSaved = true;
                newState = false;
            }
            else
            {
                // insert asterisk after "- "
                Text = Text.Insert(Strings.InStr(Text, "-") + 1, "*");
                btnSave.Enabled = true;
                currentSaved = false;
            }
        }

        private void Modified(object sender, DataGridViewCellEventArgs e)
        {
            Saved(false);
        }

        private void Enable(object sender, EventArgs e)
        {
            // check that there is something selected
            if (dgvData.SelectedCells.Count <= 0 && dgvData.SelectedRows.Count <= 0 && dgvData.SelectedColumns.Count <= 0)
            {
                btnEdit.Enabled = false;
                btnVerifyFile.Enabled = false;
            }
            else
            {
                btnEdit.Enabled = true;
                btnVerifyFile.Enabled = true;
                if (dgvData.SelectedRows.Count > 0)
                {
                    btnCopyRow.Enabled = true;
                }
            }

            // check that a row is selected
            if (dgvData.SelectedRows.Count > 0)
            {
                btnCopyRow.Enabled = true;
                btnCutRow.Enabled = true;
                btnDeleteRow.Enabled = true;
                btnVerifyRow.Enabled = true;
            }
            else
            {
                btnCutRow.Enabled = false;
                btnCopyRow.Enabled = false;
                btnDeleteRow.Enabled = false;
                btnVerifyRow.Enabled = false;
            }
        }

        private void DisableFind(object sender, EventArgs e)
        {
            txtFind.Visible = false;
            lblFind.Visible = false;
            searched = false;
        }

        private void VerifyRows()
        {
            var failedRows = new List<int>();
            var rows = new List<int>();
            double progress;
            string[] currentResponse;

            Invoke(new Action(() =>
                {
                    prgVerify.Value = 0;
                    prgVerify.Visible = true;
                    lblVerify.Text = "Verifying";
                    lblVerify.Visible = true;
                }));

            // get rows to verify
            for (int selectedRow = 0, loopTo = dgvData.SelectedRows.Count - 1; selectedRow <= loopTo; selectedRow++)
                rows.Add(dgvData.SelectedRows[selectedRow].Index);

            for (int row = 0, loopTo1 = rows.Count - 2; row <= loopTo1; row++)
            {
                // verify
                currentResponse = Utilities.VerifyTrack(Conversions.ToString(dgvData.Rows[rows[row]].Cells[1].Value), Conversions.ToString(dgvData.Rows[rows[row]].Cells[2].Value));

                // add if failed
                if (currentResponse[0].Contains("ERROR: ") == true)
                {
                    failedRows.Add(rows[row]);
                }

                // update progress
                progress = (row + 1) / (double)rows.Count * 100d;
                Invoke(new Action(() =>
                    {
                        prgVerify.Value = (int)Math.Round(Math.Round(progress));
                        lblVerify.Text = "Verifying: " + (row + 2).ToString() + " of " + rows.Count.ToString();
                    }));
            }

            Invoke(new Action(() => prgVerify.Value = 100));

            // deal with results
            if (failedRows.Count == 0)
            {
                Invoke(new Action(() => MessageBox.Show("All rows were successfully verified!", "Verify Entire File", MessageBoxButtons.OK, MessageBoxIcon.Information)));
            }
            else
            {
                Invoke(new Action(() => MessageBox.Show("Some rows were not able to be verified" + Constants.vbCrLf + "Unverified rows will be highlighted", "Verify Entire File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)));

                Invoke(new Action(() => dgvData.ClearSelection()));
                foreach (var row in failedRows)
                    Invoke(new Action(() => dgvData.Rows[row].Selected = true));
            }

            Invoke(new Action(() =>
                {
                    prgVerify.Visible = false;
                    lblVerify.Visible = false;
                }));
        }

        private void VerifyFile()
        {
            var failedRows = new List<int>();
            int rows = dgvData.Rows.Count;
            double progress;
            string[] currentResponse;

            Invoke(new Action(() =>
                {
                    prgVerify.Value = 0;
                    prgVerify.Visible = true;
                    lblVerify.Text = "Verifying";
                    lblVerify.Visible = true;
                }));

            for (int row = 0, loopTo = rows - 2; row <= loopTo; row++)
            {
                // verify
                currentResponse = Utilities.VerifyTrack(Conversions.ToString(dgvData.Rows[row].Cells[1].Value), Conversions.ToString(dgvData.Rows[row].Cells[2].Value));

                // add if failed
                if (currentResponse[0].Contains("ERROR: ") == true)
                {
                    failedRows.Add(row);
                }

                // update progress
                progress = (row + 1) / (double)rows * 100d;
                Invoke(new Action(() =>
                    {
                        prgVerify.Value = (int)Math.Round(Math.Round(progress));
                        lblVerify.Text = "Verifying: " + (row + 2).ToString() + " of " + rows.ToString();
                    }));
            }

            Invoke(new Action(() => prgVerify.Value = 100));

            // deal with results
            if (failedRows.Count == 0)
            {
                Invoke(new Action(() => MessageBox.Show("All rows were successfully verified!", "Verify Entire File", MessageBoxButtons.OK, MessageBoxIcon.Information)));
            }
            else
            {
                Invoke(new Action(() => MessageBox.Show("Some rows were not able to be verified" + Constants.vbCrLf + "Unverified rows will be highlighted", "Verify Entire File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)));

                Invoke(new Action(() => dgvData.ClearSelection()));
                foreach (var row in failedRows)
                    Invoke(new Action(() => dgvData.Rows[row].Selected = true));
            }

            Invoke(new Action(() =>
                {
                    prgVerify.Visible = false;
                    lblVerify.Visible = false;
                }));
        }
        #endregion

        #region Buttons
        private void ClickNew(object sender, EventArgs e)
        {
            NewFile();
        }

        private void ClickOpen(object sender, EventArgs e)
        {
            if (ofdOpen.ShowDialog() == DialogResult.OK)
            {
                Open(ofdOpen.FileName);
            }
        }

        private void ClickSave(object sender, EventArgs e)
        {
            if (newState == true)
            {
                // open save as instead
                btnSaveAs.PerformClick();
                return;
            }

            Save(currentIndexLocation);
        }

        private void ClickSaveAs(object sender, EventArgs e)
        {
            sfdSaveAs.InitialDirectory = Application.StartupPath;

            if (sfdSaveAs.ShowDialog() == DialogResult.OK)
            {
                Save(sfdSaveAs.FileName);
            }
        }

        private void SetIndexClick(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmMain.LoadScrobbleIndex(currentIndexLocation);
            btnSetIndex.Enabled = false;
        }

        private void ClickReload(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to reload index from file?" + Constants.vbCrLf + "All unsaved changes will be lost", "Confirm Reload", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Open(currentIndexLocation);
            }
        }
        private List<int[]> _ClickFind_cells = new List<int[]>();
        private int _ClickFind_currentCell = default;
        private string _ClickFind_lastSearchTerm = default;

        private void ClickFind(object sender, EventArgs e)
        {

            if (txtFind.Visible == false)
            {
                // step 1 - show text box and label 
                _ClickFind_cells.Clear();
                _ClickFind_currentCell = 0;
                txtFind.Text = string.Empty;
                lblFind.Text = "Find";
                txtFind.Visible = true;
                lblFind.Visible = true;
                txtFind.Select();
            }
            else if (searched == false || (_ClickFind_lastSearchTerm ?? "") != (txtFind.Text ?? "") || string.IsNullOrEmpty(txtFind.Text) && searched == false)
            {
                // step 2 - do initial search and highlight first item
                _ClickFind_cells.Clear();
                _ClickFind_lastSearchTerm = txtFind.Text;
                string searchTerm = _ClickFind_lastSearchTerm.Trim().ToLower();

                // search
                for (int row = 0, loopTo = dgvData.Rows.Count - 2; row <= loopTo; row++)
                {
                    for (int column = 0; column <= 3; column++)
                    {
                        if (Conversions.ToString(dgvData.Rows[row].Cells[column].Value).ToLower().Contains(searchTerm) == true)
                        {
                            _ClickFind_cells.Add(new[] { row, column });
                        }
                    }
                }

                // if nothing was found
                if (_ClickFind_cells.Count == 0)
                {
                    lblFind.Text = "Nothing found";
                    return;
                }

                searched = true;

                // display first
                _ClickFind_currentCell = 1;
                lblFind.Text = _ClickFind_currentCell.ToString() + " of " + _ClickFind_cells.Count.ToString();
                dgvData.CurrentCell = dgvData.Rows[_ClickFind_cells[_ClickFind_currentCell - 1][0]].Cells[_ClickFind_cells[_ClickFind_currentCell - 1][1]];
            }
            else
            {
                // step 3 - loop between rest of items
                if (_ClickFind_currentCell < _ClickFind_cells.Count)
                {
                    _ClickFind_currentCell += 1;
                }
                else
                {
                    _ClickFind_currentCell = 1;
                }

                // display
                lblFind.Text = _ClickFind_currentCell.ToString() + " of " + _ClickFind_cells.Count.ToString();
                dgvData.CurrentCell = dgvData.Rows[_ClickFind_cells[_ClickFind_currentCell - 1][0]].Cells[_ClickFind_cells[_ClickFind_currentCell - 1][1]];
            }
        }

        private void ClickCopyCell(object sender, EventArgs e)
        {
            // if theres only one cell selected dont add quotations and commas and crap
            if (dgvData.SelectedCells.Count <= 1)
            {
                My.MyProject.Computer.Clipboard.SetText(Conversions.ToString(dgvData.SelectedCells[0].Value));
            }
            else
            {
                var str = new StringBuilder();

                for (int selectedCell = dgvData.SelectedCells.Count - 1; selectedCell >= 0; selectedCell -= 1)
                {
                    str.Append('"' + Conversions.ToString(dgvData.SelectedCells[selectedCell].Value) + '"');
                    if (selectedCell > 0)
                    {
                        str.Append(",");
                    }
                }

                My.MyProject.Computer.Clipboard.SetText(str.ToString());
            }
        }

        private void ClickCopyRow(object sender, EventArgs e)
        {
            var str = new StringBuilder();
            int currentRow;

            for (int selectedRow = dgvData.SelectedRows.Count - 1; selectedRow >= 0; selectedRow -= 1)
            {
                currentRow = dgvData.SelectedRows[selectedRow].Index;
                str.Append(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject('"', dgvData.Rows[currentRow].Cells[0].Value), '"'), ","));
                str.Append(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject('"', dgvData.Rows[currentRow].Cells[1].Value), '"'), ","));
                str.Append(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject('"', dgvData.Rows[currentRow].Cells[2].Value), '"'), ","));
                str.Append(Operators.ConcatenateObject(Operators.ConcatenateObject('"', dgvData.Rows[currentRow].Cells[3].Value), '"'));
                if (selectedRow > 0)
                {
                    str.AppendLine();    // next line
                }
            }

            My.MyProject.Computer.Clipboard.SetText(str.ToString());
        }

        private void ClickCutCell(object sender, EventArgs e)
        {
            btnCopyCell.PerformClick();
            btnDeleteCell.PerformClick();
        }

        private void ClickCutRow(object sender, EventArgs e)
        {
            btnCopyRow.PerformClick();
            btnDeleteRow.PerformClick();
        }

        private void ClickPasteCell(object sender, EventArgs e)
        {
            List<string> cells = ParseClipboard(Clipboard.GetText());
            if (cells.Count <= 1)
            {
                // paste the same text into every selected cell
                for (int selectedCell = dgvData.SelectedCells.Count - 1; selectedCell >= 0; selectedCell -= 1)
                    dgvData.SelectedCells[selectedCell].Value = cells[0];
            }
            else
            {
                // copy each cell into each selected cell
                for (int selectedCell = dgvData.SelectedCells.Count - 1; selectedCell >= 0; selectedCell -= 1)
                {
                    if ((dgvData.SelectedCells.Count - 1 - selectedCell) < cells.Count)
                        dgvData.SelectedCells[selectedCell].Value = cells[dgvData.SelectedCells.Count - 1 - selectedCell];
                }
            }
        }

        private void ClickPasteRow(object sender, EventArgs e)
        {
            string clipboard = My.MyProject.Computer.Clipboard.GetText();
            List<string> cells = ParseClipboard(clipboard);
            List<string[]> rows = new List<string[]>();
            List<string> currentRow = new List<string>();
            for (int x = 0; x < cells.Count; x++)
            {
                currentRow.Add(cells[x]);
                if (currentRow.Count >= 4) 
                {
                    rows.Add(currentRow.ToArray());
                    currentRow.Clear();
                }
            }

            if (dgvData.SelectedRows.Count > 0 && dgvData.SelectedRows[dgvData.SelectedRows.Count - 1].Index < dgvData.Rows.Count - 1) // insert at selected row
                dgvData.Rows.Insert(dgvData.SelectedRows[dgvData.SelectedRows.Count - 1].Index + 1, cells.ToArray());
            else if (dgvData.SelectedCells.Count > 0 && dgvData.SelectedCells[dgvData.SelectedCells.Count - 1].RowIndex < dgvData.Rows.Count - 1) // insert at row of last selected cell
                dgvData.Rows.Insert(dgvData.SelectedCells[dgvData.SelectedCells.Count - 1].RowIndex + 1, cells.ToArray());
            else // insert at end of file
                dgvData.Rows.Insert(dgvData.Rows.Count - 1, cells.ToArray());
        }

        private void ClickDeleteCell(object sender, EventArgs e)
        {
            for (int selectedCell = dgvData.SelectedCells.Count - 1; selectedCell >= 0; selectedCell -= 1)
                dgvData.SelectedCells[selectedCell].Value = string.Empty;
            Saved(false);
        }

        private void ClickDeleteRow(object sender, EventArgs e)
        {
            int currentRow;

            for (int selectedRow = dgvData.SelectedRows.Count - 1; selectedRow >= 0; selectedRow -= 1)
            {
                currentRow = dgvData.SelectedRows[selectedRow].Index;
                if (currentRow < dgvData.Rows.Count - 1) // check that we are not trying to delete an uncommitted row
                {
                    dgvData.Rows.RemoveAt(currentRow);
                }
            }
            Saved(false);
        }

        private void ClickAddRow(object sender, EventArgs e)
        {
            My.MyProject.Forms.frmScrobbleIndexAddRow.Show();
            My.MyProject.Forms.frmScrobbleIndexAddRow.Activate();
        }

        private void ClickVerifyRow(object sender, EventArgs e)
        {
            var th = new System.Threading.Thread(VerifyRows);
            th.Name = "VerifyIndex";
            th.Start();
        }

        private void ClickVerifyFile(object sender, EventArgs e)
        {
            var th = new System.Threading.Thread(VerifyFile);
            th.Name = "VerifyIndex";
            th.Start();
        }
        #endregion

        #region Ops
        public void NewFile()
        {
            Saved(true);
            Text = "Scrobble Index Editor - Untitled";
            newState = true;
            btnSetIndex.Enabled = false;
            btnReload.Enabled = false;
            dgvData.Rows.Clear();
        }

        public void Open(string location)
        {
            // init reader
            using (var reader = new TextFieldParser(location))
            {
                reader.TextFieldType = FieldType.Delimited;
                reader.SetDelimiters(",");

                // read file and gather errors if any
                currentIndexData.Clear();
                string[] currentRow;
                var badLines = new List<uint>();
                var currentLine = default(uint);
                bool blank = true; // if this remains true after the loop that means the file is blank
                while (!reader.EndOfData)
                {
                    blank = false;
                    currentLine = (uint)(currentLine + 1L);
                    try
                    {
                        currentRow = reader.ReadFields();
                        // row must have 4 fields
                        if (currentRow.Length != 4)
                        {
                            badLines.Add(currentLine);
                        }
                        else
                        {
                            // add line to contents
                            currentIndexData.Add(currentRow);
                        }
                    }
                    catch (MalformedLineException ex)
                    {
                        badLines.Add(currentLine);
                    }
                }

                // handle errors
                if (blank == false)
                {
                    // if entire file is unusable
                    if (badLines.Count >= currentLine)
                    {
                        MessageBox.Show("Scrobble index was unable to be parsed", "Scrobble Index", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    // if only some lines are usuable
                    if (badLines.Count >= 1)
                    {
                        MessageBox.Show("Scrobble index contains errors on some lines, these lines will be ignored by the editor and overwritten if you save the file", "Scrobble Index", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                // save file location
                currentIndexLocation = location;

                // add file to form title
                Text = "Scrobble Index Editor - " + location;

                // enter data into dgv
                dgvData.Rows.Clear();
                if (blank == false)
                {
                    for (uint count = 0U, loopTo = (uint)(currentIndexData.Count - 1); count <= loopTo; count++)
                        dgvData.Rows.Add(currentIndexData[(int)count]);
                }
            }
            btnReload.Enabled = true;
            newState = false;
            Saved(true);
        }

        public void Save(string location)
        {
            newState = false;

            var fi = new FileInfo(location);

            // delete file if it already exists
            if (fi.Exists && dgvData.Rows.Count > 1)
            {
                fi.Delete();
            }

            // create new file and filestream
            var fs = fi.Create();

            // only attempt to write if there is data to write
            if (dgvData.Rows.Count > 1)
            {
                // compile data to string
                var str = new StringBuilder();
                for (uint row = 0U, loopTo = (uint)(dgvData.Rows.Count - 2); row <= loopTo; row++)
                {
                    str.Append(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject('"', dgvData.Rows[(int)row].Cells[0].Value), '"'), ","));
                    str.Append(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject('"', dgvData.Rows[(int)row].Cells[1].Value), '"'), ","));
                    str.Append(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject('"', dgvData.Rows[(int)row].Cells[2].Value), '"'), ","));
                    str.Append(Operators.ConcatenateObject(Operators.ConcatenateObject('"', dgvData.Rows[(int)row].Cells[3].Value), '"'));
                    str.AppendLine();    // next line
                }

                // convert to bytearray and write
                byte[] bytes = new UTF8Encoding(true).GetBytes(str.ToString());
                fs.Write(bytes, 0, bytes.Length);
            }
            fs.Close();

            // tell main to reload scrobble index if this is the currently loaded scrobble index
            if ((location ?? "") == (My.MySettingsProperty.Settings.CurrentScrobbleIndex ?? ""))
            {
                My.MyProject.Forms.frmMain.LoadScrobbleIndex(location);
            }

            // final stuff
            Saved(true);
            btnReload.Enabled = true;
            currentIndexLocation = location;
            Text = "Scrobble Index Editor - " + location;
        }

        private void FormClose(object sender, CancelEventArgs e)
        {
            if (currentSaved == false)
            {
                var result = MessageBox.Show("You have made unsaved changes" + Constants.vbCrLf + "Are you sure you want to quit without saving?", "Add Row", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                // cancel exit if not yes
                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }

            My.MyProject.Forms.frmScrobbleIndexAddRow.Close();
        }

        // parses cells out of clipboard text
        private List<string> ParseClipboard(string str)
        {
            List<string> cells = new List<string>();
            if (str.Trim().StartsWith("\"") && str.Trim().EndsWith("\""))
            {
                int firstPos = 0, lastPos = 0;
                bool end = false;
                while (!end)
                {
                    firstPos = str.IndexOf("\"", firstPos);
                    lastPos = str.IndexOf("\"", firstPos + 1);
                    cells.Add(str.Substring(firstPos + 1, lastPos - firstPos - 1));
                    if (str.Length > lastPos + 1 && str[lastPos + 1] == ',')
                    {
                        firstPos = lastPos + 1;
                    }
                    else
                    {
                        end = true;
                    }
                }
            }
            else
            {
                cells.Add(str);
            }

            return cells;
        }
        #endregion
    }
}