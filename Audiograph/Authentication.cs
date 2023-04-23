using System;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace Audiograph
{
    public partial class frmAuthentication
    {
        public frmAuthentication()
        {
            InitializeComponent();
        }
        private void frmAuthentication_Load(object sender, EventArgs e)
        {
            lblInstruction.Text = "This window will allow you to authenticate your user account." + Constants.vbCrLf + "Click OK to begin the process.";
            lblStatus.Text = "";
        }

        private byte _btnOK_Click_authenticatestep = default;
        private string[] _btnOK_Click_token = new string[1];

        private void btnOK_Click(object sender, EventArgs e)
        {

            switch (_btnOK_Click_authenticatestep)
            {
                case 0:  // step 1 get token
                    {
                        // button stuff
                        btnOK.Enabled = false;
                        btnOK.Text = "&OK";
                        btnCancel.Text = "&Cancel";

                        // set labels
                        lblInstruction.Text = "";
                        lblStatus.Text = "Retrieving token...";

                        // get token
                        string tokenXML = Utilities.CallAPI("auth.getToken");
                        _btnOK_Click_token[0] = "token";
                        Utilities.ParseXML(tokenXML, "/lfm", 0U, ref _btnOK_Click_token);

                        // set labels
                        lblInstruction.Text = "You will now be taken to the Last.fm authentication page." + Constants.vbCrLf + "Once you have authenticated, please come back to this window.";
                        lblStatus.Text = "Click OK to continue.";

                        // enable button
                        btnOK.Enabled = true;
                        btnOK.Select();

                        // go to next step
                        _btnOK_Click_authenticatestep = 1;
                        break;
                    }
                case 1:  // step 2 authenticate with last
                    {
                        // set labels
                        lblInstruction.Text = "Click " + '"' + "Allow Access" + '"' + " on the webpage." + Constants.vbCrLf + "Return here and click OK when finished.";
                        lblStatus.Text = "";

                        // open web browser for user to authenticate
                        Process.Start("http://www.last.fm/api/auth/?api_key=27a6f7ec4ae4cd5bca77c7639a78abc0&token=" + _btnOK_Click_token[0]);

                        // go to next step
                        _btnOK_Click_authenticatestep = 2;
                        break;
                    }
                case 2:  // step 3 get session
                    {
                        // set labels and stuff
                        btnOK.Enabled = false;
                        lblInstruction.Text = "";
                        lblStatus.Text = "Getting session key...";

                        // get session key
                        try
                        {
                            // get session
                            string key = Utilities.CallAPI("auth.getSession", "", "token=" + _btnOK_Click_token[0], "api_sig=" + Utilities.CreateSignature("auth.getSession", "token" + _btnOK_Click_token[0], "", "", "", false));
                            string[] keynodes = new string[] { "key", "name" };
                            Utilities.ParseXML(key, "/lfm/session", 0U, ref keynodes);

                            // check for errors
                            if (keynodes[0].Contains("ERROR: ") == true)
                            {
                                lblInstruction.Text = "Authentication unsuccessful." + Constants.vbCrLf + "Please try again later.";
                                lblStatus.Text = string.Empty;

                                // allow user to retry authentication
                                btnOK.Text = "&Retry";
                                btnOK.Enabled = true;
                                btnOK.Select();
                                _btnOK_Click_authenticatestep = 0;
                                return;
                            }

                            // check for wrong account
                            if ((keynodes[1].ToLower() ?? "") != (My.MySettingsProperty.Settings.User.ToLower() ?? ""))
                            {
                                lblInstruction.Text = "Error: you have validated for the wrong account." + Constants.vbCrLf + "Make sure you are signed in to the correct account.";
                                lblStatus.Text = string.Empty;

                                // allow user to retry authentication
                                btnOK.Text = "&Retry";
                                btnOK.Enabled = true;
                                btnOK.Select();
                                _btnOK_Click_authenticatestep = 0;
                                return;
                            }

                            // save key
                            My.MySettingsProperty.Settings.SessionKey = keynodes[0];
                            My.MySettingsProperty.Settings.Save();

                            // tell main form to allow authentication features
                            My.MyProject.Forms.frmMain.AuthenticatedUI(true);

                            // set labels and buttons
                            lblInstruction.Text = "Authentication successful!" + Constants.vbCrLf + "You can now close this window.";
                            lblStatus.Text = "";
                            btnCancel.Text = "&Close";
                        }
                        catch (Exception ex)   // check for errors
                        {
                            lblInstruction.Text = "Authentication unsuccessful." + Constants.vbCrLf + "Please try again.";
                            lblStatus.Text = "Message: " + ex.Message;

                            // allow user to retry authentication
                            btnOK.Text = "&Retry";
                            btnOK.Enabled = true;
                            btnOK.Select();
                            _btnOK_Click_authenticatestep = 0;
                        }

                        break;
                    }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}