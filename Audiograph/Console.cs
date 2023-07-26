using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Audiograph.My;
using Microsoft.VisualBasic;

namespace Audiograph
{
    public partial class frmConsole
    {
        // var for checking whether a command has been entered since the last time this sub was ran
        private ushort _History_checkvalue = 0;
        private Color[] _tmrSecret_Tick_colors = new Color[] { Color.Red, Color.Green, Color.Blue, Color.Yellow };
        private byte _tmrSecret_Tick_count = default;
        private bool authorized = false;
        private List<string> cmdlist = new List<string>();
        private ushort cmdlistcount;
        public bool working = false;

        public frmConsole()
        {
            InitializeComponent();
        }

        // step 1 - interprets commands and sends them to their respective methods
        public void CommandInterpreter(string command)
        {
            // stop if working
            if (working == true)
            {
                return;
            }

            working = true;

            // append command
            txtOutput.AppendText(command);

            // add command to cmdlist
            cmdlist.Add(command);

            // detect for no input
            if (string.IsNullOrEmpty(command))
            {
                DisplayOut("Please enter a command.");
                return;
            }

            // check that spaceholder character is not present
            if (command.Contains("ˍ") == true)
            {
                DisplayOut("ERROR: Char 'ˍ' is an invalid character.");
            }

            // parse for backticks
            bool backtick = false;
            for (uint count = 0U, loopTo = (uint)(command.Count() - 1); count <= loopTo; count++)
            {
                // backtick switch
                if (command[(int)count] == '`')
                {
                    if (backtick == false)
                    {
                        backtick = true;
                    }
                    else
                    {
                        backtick = false;
                    }
                }

                // space replace
                if (command[(int)count] == ' ' && backtick == false)
                {
                    command = command.Remove((int)count, 1);
                    command = command.Insert((int)count, "ˍ");
                }
            }

            // split command into array
            string[] commandarray = command.Trim().Split('ˍ');

            // tolower/null check
            for (byte count = 0, loopTo1 = (byte)(commandarray.Count() - 1); count <= loopTo1; count++)
            {
                // make tolower if no backticks
                if (commandarray[count].Contains("`") == false)
                {
                    commandarray[count] = commandarray[count].ToLower();
                }

                // make null nothing
                if (commandarray[count] == "null")
                {
                    commandarray[count] = string.Empty;
                }
            }

            // get rid of any backticks in array
            for (byte count = 0, loopTo2 = (byte)(commandarray.Count() - 1); count <= loopTo2; count++)
                commandarray[count] = commandarray[count].Replace("`", string.Empty);

            // send command to proper method
            switch (commandarray[0] ?? "")
            {
                case "help":
                {
                    CmdHelp(commandarray);
                    break;
                }
                case "hash":
                {
                    CmdHash(commandarray);
                    break;
                }
                case "lfm":
                {
                    CmdLfm(commandarray);
                    break;
                }
                case "auth":
                case "authorize":
                {
                    CmdAuthorize(commandarray);
                    break;
                }
                case "var":
                {
                    CmdVar(commandarray);
                    break;
                }
                case "createreq":
                {
                    CmdCreatereq(commandarray);
                    break;
                }
                case "createsig":
                {
                    CmdCreatesig(commandarray);
                    break;
                }
                case "frm":
                {
                    CmdFrm(commandarray);
                    break;
                }
                case "setsk":
                {
                    CmdSetsk(commandarray);
                    break;
                }
                case "threads":
                {
                    CmdThreads();
                    break;
                }
                case "resetprog":
                {
                    CmdResetprog();
                    break;
                }
                case "time":
                {
                    CmdTime();
                    break;
                }
                case "viewsk":
                {
                    CmdViewsk();
                    break;
                }
                case "secret":
                {
                    CmdSecret();
                    break;
                }
                case "removeuser":
                {
                    MySettingsProperty.Settings.User = string.Empty;
                    MySettingsProperty.Settings.SessionKey = string.Empty;
                    DisplayOut("User removed.");
                    break;
                }
                case "removesk":
                {
                    MySettingsProperty.Settings.SessionKey = string.Empty;
                    MyProject.Forms.frmMain.AuthenticatedUI(false);
                    DisplayOut("Session key removed.");
                    break;
                }
                case "root":
                {
                    Process.Start(Application.StartupPath);
                    DisplayOut(Application.ExecutablePath);
                    break;
                }
                case "clr":
                case "clear":
                case "cls":
                {
                    txtOutput.Text = "user>";
                    working = false;
                    break;
                }
                case "exit":
                {
                    Application.Exit();
                    break;
                }
                case "restart":
                {
                    Application.Restart();
                    break;
                }
                case "ram":
                {
                    var x = Process.GetCurrentProcess();
                    DisplayOut((x.WorkingSet64 / 1024d / 1024d).ToString("N2") + "MB currently in use with " +
                               x.Threads.Count.ToString() + " threads by the Audiograph process.");
                    break;
                }
                case "apiinfo":
                {
                    // check for authorization
                    if (authorized == true)
                    {
                        DisplayOut("Key".PadRight(8) + Secrets.APIkey + Constants.vbCrLf + "Secret".PadRight(8) +
                                   Secrets.APIsecret);
                    }
                    else
                    {
                        DisplayOut(
                            "ERROR: apiinfo command requires authorization. Please use the 'authorize' command.");
                    }

                    break;
                }
                case "clearindex":
                {
                    MySettingsProperty.Settings.CurrentScrobbleIndex = string.Empty;
                    DisplayOut("Current index cleared.");
                    break;
                }

                default:
                {
                    DisplayOut("ERROR: Command '" + commandarray[0] + "' unrecognized.");
                    break;
                }
            }
        }

        // step 3 - display output
        public void DisplayOut(string output)
        {
            txtOutput.AppendText(Constants.vbCrLf + output + Constants.vbCrLf + Constants.vbCrLf + "user>");
            working = false;
        }

        // switches between command history on up/down arrow press
        private void History(object sender, KeyEventArgs e)
        {
            // check that there is at least one command entered
            if (cmdlist.Count == 0)
            {
                return;
            }

            // up
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                if (_History_checkvalue !=
                    cmdlist.Count) // new command check (if checkvalue is different than cmdlist.count then a new command has been entered)
                {
                    cmdlistcount = (ushort)(cmdlist.Count - 1);
                    txtInput.Text = cmdlist[cmdlistcount];
                    _History_checkvalue = (ushort)cmdlist.Count;
                }
                else
                {
                    if (cmdlistcount > 0)
                    {
                        cmdlistcount = (ushort)(cmdlistcount - 1);
                    }

                    txtInput.Text = cmdlist[cmdlistcount];
                }
            }

            // down
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                if (_History_checkvalue != cmdlist.Count)
                {
                    cmdlistcount = 0;
                    txtInput.Text = cmdlist[cmdlistcount];
                    _History_checkvalue = (ushort)cmdlist.Count;
                }
                else
                {
                    if (cmdlistcount < cmdlist.Count - 1)
                    {
                        cmdlistcount = (ushort)(cmdlistcount + 1);
                    }

                    txtInput.Text = cmdlist[cmdlistcount];
                }
            }
        }

        // sends input
        private void Send(object sender, EventArgs e)
        {
            if (working == false)
            {
                CommandInterpreter(txtInput.Text);
                txtInput.Clear();
            }
        }

        // resize controls
        private void ResizeOps(object sender, EventArgs e)
        {
            txtOutput.Width = Width - 40;
            txtOutput.Height = Height - 90;
            txtInput.Width = Width - 122;
        }

        private void tmrSecret_Tick(object sender, EventArgs e)
        {
            _tmrSecret_Tick_count = (byte)(_tmrSecret_Tick_count + 1);
            if (_tmrSecret_Tick_count > 3)
            {
                _tmrSecret_Tick_count = 0;
            }

            BackColor = _tmrSecret_Tick_colors[_tmrSecret_Tick_count];
        }

        #region Step 2 - command methods

        private void CmdHelp(string[] commands)
        {
            var output = new StringBuilder();
            if (commands.Count() == 1)
            {
                // general help
                output.AppendLine("Type 'help {command}' to display information about a specific command.");
                output.AppendLine("() = required, {} = optional");
                output.AppendLine(
                    "Use backticks `` to pass an argument with spaces/capitalization and 'null' to pass an argument with nothing.");
                output.AppendLine(string.Empty);
                output.AppendLine("help".PadRight(12) +
                                  "Displays list of commands or information about a specific command.");
                output.AppendLine("clr".PadRight(12) + "Clears the screen.");
                output.AppendLine("lfm".PadRight(12) +
                                  "Returns the raw XML from requests made with the CallAPI and CallAPIAuth functions.");
                output.AppendLine("var".PadRight(12) + "Read the value of a variable.");
                output.AppendLine("hash".PadRight(12) + "Returns the MD5 hash of a string.");
                output.AppendLine(
                    "createreq".PadRight(12) + "Returns the url used for a get request to the LFM server.");
                output.AppendLine("createsig".PadRight(12) + "Create an LFM API signature.");
                output.AppendLine("frm".PadRight(12) + "Open, close or display information about a form.");
                output.AppendLine("removeuser".PadRight(12) + "Remove user (can cause errors).");
                output.AppendLine("removesk".PadRight(12) +
                                  "Remove session key (you will have to reauthenticate, can cause errors).");
                output.AppendLine("ram".PadRight(12) + "Returns the current memory usage from the Audiograph process.");
                output.AppendLine("exit".PadRight(12) + "Exits the Audiograph application, including all forms.");
                output.AppendLine("restart".PadRight(12) + "Restarts the Audiograph application, closing all forms.");
                output.AppendLine("root".PadRight(12) +
                                  "Opens a file explorer window with the root folder for this Audiograph application.");
                output.AppendLine("threads".PadRight(12) + "Shows current running threads.");
                output.AppendLine("resetprog".PadRight(12) + "Resets the progress and progressmultiplier variables.");
                output.AppendLine("time".PadRight(12) + "Displays various time variables.");
                output.AppendLine("clearindex".PadRight(12) + "Unloads the currently loaded scrobble index.");
                output.AppendLine("auth".PadRight(12) +
                                  "Allows you to authorize as a developer in order to use restricted commands. Unavailable in release builds.");
                output.AppendLine("viewsk".PadRight(12) +
                                  "Displays the current user session key (requires authorization).");
                output.AppendLine("setsk".PadRight(12) +
                                  "Sets the current user session key to a provided session key (requires authorization).");
                output.Append("apiinfo".PadRight(12) +
                              "Display the Last.fm API key and secret used by this application (requires authorization).");
            }
            else
            {
                // command help
                switch (commands[1] ?? "")
                {
                    case "help":
                    {
                        output.AppendLine("Usage: help {command}");
                        output.Append(
                            "Displays general information about commands (no params) or information about a specific command.");
                        break;
                    }
                    case "clr":
                    {
                        output.AppendLine("Usage: clr");
                        output.Append("Clears the screen.");
                        break;
                    }
                    case "lfm":
                    {
                        output.AppendLine("Usage: lfm (get/post) (method) {user if get} {param1} {param2} {param3}");
                        output.AppendLine(
                            "Uses the CallAPI (get) or CallAPIAuth (post) functions to make a request to the Last.fm API. Returns the output from these functions.");
                        output.Append("'post' argument requires authorization, see 'help auth' for more information.");
                        break;
                    }
                    case "var":
                    {
                        output.AppendLine("Usage: var (variablename)");
                        output.AppendLine("Displays the value of a specifed variable during runtime.");
                        output.Append(
                            "Variables: progress, progressmultiplier, stoploadexecution, userlookup, tracklookup, cmdlist, cmdlistcount, settings.user, settings.sessionkey");
                        break;
                    }
                    case "hash":
                    {
                        output.AppendLine("Usage: hash (string)");
                        output.Append("Returns the MD5 hash of a specified string.");
                        break;
                    }
                    case "createreq":
                    {
                        output.AppendLine("Usage: createreq (method) {user} {param1} {param2} {param3}");
                        output.Append(
                            "Creates a url used for an HTTP GET request to the Last.fm server, can be loaded into an XML viewer.");
                        break;
                    }
                    case "createsig":
                    {
                        output.AppendLine("Usage: createsig (method) {param1} {param2} {param3}");
                        output.AppendLine("Creates a Last.fm API signature used for HTTP POST requests.");
                        output.Append("Command requires authorization, see 'help auth' for more information.");
                        break;
                    }
                    case "frm":
                    {
                        output.AppendLine("Usage: frm (open/close/info) (formname)");
                        output.AppendLine("Opens, closes, or retrieves information about a form.");
                        output.Append(
                            "Forms: main, about, authentication, console, apihistory, trackadvanced, artistadvanced, albumadvanced, addqueue, scrobblehistory, backuptool, scrobbleindexeditor, scrobbleindexaddrow, scrobbleindextrackadvanced, scrobblesearch");
                        break;
                    }
                    case "removeuser":
                    {
                        output.AppendLine("Usage: removeuser");
                        output.Append(
                            "Removes the set user from the application, along with their session key if applicable. Can cause errors, and requires a restart of the application to fully take effect.");
                        break;
                    }
                    case "removesk":
                    {
                        output.AppendLine("Usage: removesk");
                        output.Append(
                            "Removes the user's session key from the application if they have authenticated, effectively taking away their authentication.");
                        break;
                    }
                    case "ram":
                    {
                        output.AppendLine("Usage: ram");
                        output.Append("Displays the current memory usage from the Audiograph process.");
                        break;
                    }
                    case "exit":
                    {
                        output.AppendLine("Usage: exit");
                        output.Append("Exits the Audiograph application, including all forms.");
                        break;
                    }
                    case "restart":
                    {
                        output.AppendLine("Usage: restart");
                        output.Append("Restarts the Audiograph application, closing all forms.");
                        break;
                    }
                    case "root":
                    {
                        output.AppendLine("Usage: root");
                        output.Append(
                            "Opens a file explorer window with the root folder for this Audiograph executable.");
                        break;
                    }
                    case "auth":
                    case "authorize":
                    {
                        output.AppendLine("Usage: auth (password)");
                        output.AppendLine(
                            "Allows you to authorize as a developer using a special password in order to use developer-restricted commands.");
                        output.Append("This feature is not available in release builds.");
                        break;
                    }
                    case "threads":
                    {
                        output.AppendLine("Usage: threads");
                        output.Append("Displays current running active threads/operations.");
                        break;
                    }
                    case "resetprog":
                    {
                        output.AppendLine("Usage: resetprog");
                        output.Append(
                            "Resets the progress and progressmultiplier variables to 0. Useful when the progress bar has become stuck.");
                        break;
                    }
                    case "time":
                    {
                        output.AppendLine("Usage: time");
                        output.Append(
                            "Displays the current time, current UTC time (using GetCurrentUTC()), and the current time zone offset.");
                        break;
                    }
                    case "apiinfo":
                    {
                        output.AppendLine("Usage: apiinfo");
                        output.AppendLine("Display the Last.fm API key and secret used by this application.");
                        output.Append("Command requires authorization, see 'help auth' for more information.");
                        break;
                    }
                    case "viewsk":
                    {
                        output.AppendLine("Usage: viewsk");
                        output.AppendLine("Display the current user session key if the user is authorized.");
                        output.Append("Command requires authorization, see 'help auth' for more information.");
                        break;
                    }
                    case "setsk":
                    {
                        output.AppendLine("Usage: setsk (sessionkey)");
                        output.AppendLine(
                            "Sets the current user session key to a provided session key. Validates that the session key is usable.");
                        output.Append("Command requires authorization, see 'help auth' for more information.");
                        break;
                    }
                    case "clearindex":
                    {
                        output.AppendLine("Usage: clearindex");
                        output.Append(
                            "Unloads the currently loaded scrobble index. Requires a restart to take effect.");
                        break;
                    }

                    default:
                    {
                        output.Append("ERROR: Help command '" + commands[1] + "' not found.");
                        break;
                    }
                }
            }

            // final output
            DisplayOut(output.ToString());
        }

        private void CmdHash(string[] commands)
        {
            // make sure method has been passed required params
            if (commands.Count() > 1)
            {
                DisplayOut(Utilities.Hash(commands[1]));
            }
            else
            {
                DisplayOut("ERROR: Missing required parameter 'string'. Type 'help hash' for proper command usage.");
            }
        }

        private void CmdLfm(string[] commands)
        {
            // make sure method has been passed required params
            if (commands.Count() >= 3) // has required parameters
            {
                // check that get/post is proper
                if (commands[1] == "get")
                {
                    // get start time
                    var starttime = new DateTime();
                    starttime = DateTime.Now;

                    // call api
                    string[] newcommands = Utilities.FillArray(commands, 6);
                    string returnXML = Utilities.CallAPI(newcommands[2], newcommands[3], newcommands[4], newcommands[5],
                        newcommands[6]);

                    // get end time
                    var endtime = new DateTime();
                    endtime = DateTime.Now;

                    // get milliseconds
                    var calltime = new TimeSpan();
                    calltime = endtime.Subtract(starttime);

                    // display data
                    var output = new StringBuilder();
                    output.AppendLine("-----BEGIN RESPONSE-----");
                    output.AppendLine(returnXML);
                    output.AppendLine("------END RESPONSE------");
                    output.AppendLine(string.Empty);
                    // status message
                    if (returnXML.Contains("ERROR: ") == false)
                    {
                        output.Append("Call returned successfully in " + calltime.Milliseconds.ToString() + "ms.");
                    }
                    else
                    {
                        output.Append("Call returned unsuccessfully.");
                    }

                    DisplayOut(output.ToString());
                }
                else if (commands[1] == "post")
                {
                    // check for authorization to use post
                    if (authorized == true)
                    {
                        // call api
                        string[] newcommands = Utilities.FillArray(commands, 5);
                        string returnXML = Utilities.CallAPIAuth(newcommands[2], newcommands[3], newcommands[4],
                            newcommands[5]);

                        // display data
                        var output = new StringBuilder();
                        output.AppendLine("-----BEGIN RESPONSE-----");
                        output.AppendLine(returnXML);
                        output.AppendLine("------END RESPONSE------");
                        output.AppendLine(string.Empty);
                        // status message
                        if (returnXML.Contains("ERROR: ") == false)
                        {
                            output.Append("Post made successfully.");
                        }
                        else
                        {
                            output.Append("Post unsuccessful.");
                        }

                        DisplayOut(output.ToString());
                    }
                    else
                    {
                        DisplayOut("ERROR: 'post' parameter requires authorization. Please use the 'auth' command.");
                    }
                }
                else
                {
                    // if getpost not recognized
                    DisplayOut("ERROR: '" + commands[1] +
                               "' parameter not recognized. Type 'help lfm' for proper command usage.");
                }
            }
            else if (commands.Count() == 2) // missing method parameter
            {
                DisplayOut("ERROR: Missing required parameter 'method'. Type 'help lfm' for proper command usage.");
            }
            else // missing getpost and method parameters
            {
                DisplayOut(
                    "ERROR: Missing required parameters 'get/post' and 'method'. Type 'help lfm' for proper command usage.");
            }
        }

        private void CmdAuthorize(string[] commands)
        {
#if DEBUG
            // make sure method has been passed required params
            if (commands.Count() >= 2)
            {
                // check that password is correct
                if ((commands[1] ?? "") == Secrets.ConsolePass)
                {
                    authorized = true;
                    DisplayOut("Authorization success!");
                }
                else
                {
                    DisplayOut("Password incorrect, authorization unsuccessful.");
                }
            }
            else
            {
                DisplayOut("ERROR: Missing required parameter 'code'. Type 'help authorize' for proper command usage.");
            }
#else
            DisplayOut("ERROR: This function is not present in release builds.");
#endif
        }

        private void CmdVar(string[] commands)
        {
            // make sure method has been passed required params
            if (commands.Count() >= 2)
            {
                // determine variable
                var output = new StringBuilder();
                switch (commands[1] ?? "")
                {
                    case "apihistory":
                    {
                        output.AppendLine("Displaying variable 'apihistory' (frmMain) as List of String with " +
                                          Utilities.apihistory.Count.ToString() + " elements.");
                        output.Append(string.Empty);
                        // display values
                        for (uint count = 0U, loopTo = (uint)(Utilities.apihistory.Count - 1); count <= loopTo; count++)
                            output.Append(Constants.vbCrLf + "apihistory (" + count + ") = " +
                                          Utilities.apihistory[(int)count][0] + ", " +
                                          Utilities.apihistory[(int)count][1] + ", " +
                                          Utilities.apihistory[(int)count][2] + ", " +
                                          Utilities.apihistory[(int)count][3] + ", " +
                                          Utilities.apihistory[(int)count][4] + ", " +
                                          Utilities.apihistory[(int)count][5]);
                        DisplayOut(output.ToString());
                        break;
                    }
                    case "tracklookup":
                    {
                        output.AppendLine("Displaying variable 'tracklookup' (frmMain) as Array of String with " +
                                          Utilities.tracklookup.Count().ToString() + " elements.");
                        output.Append(string.Empty);
                        // display values
                        for (uint count = 0U, loopTo1 = (uint)(Utilities.tracklookup.Count() - 1);
                             count <= loopTo1;
                             count++)
                            output.Append(Constants.vbCrLf + "tracklookup(" + count + ") = " +
                                          Utilities.tracklookup[(int)count]);
                        DisplayOut(output.ToString());
                        break;
                    }
                    case "cmdlist":
                    {
                        output.AppendLine("Displaying variable 'cmdlist' (frmConsole) as List of String with " +
                                          cmdlist.Count.ToString() + " elements.");
                        output.Append(string.Empty);
                        // display values
                        for (uint count = 0U, loopTo2 = (uint)(cmdlist.Count - 1); count <= loopTo2; count++)
                            output.Append(Constants.vbCrLf + "cmdlist(" + count + ") = " + cmdlist[(int)count]);
                        DisplayOut(output.ToString());
                        break;
                    }
                    case "cmdlistcount":
                    {
                        DisplayOut("Displaying variable 'cmdlistcount' (frmConsole) as UShort." + Constants.vbCrLf +
                                   cmdlistcount.ToString());
                        break;
                    }
                    case "progress":
                    {
                        DisplayOut("Displaying variable 'progress' (frmMain) as UShort." + Constants.vbCrLf +
                                   Utilities.progress.ToString());
                        break;
                    }
                    case "progressmultiplier":
                    {
                        DisplayOut("Displaying variable 'progressmultiplier' (frmMain) as Byte." + Constants.vbCrLf +
                                   Utilities.progressmultiplier.ToString());
                        break;
                    }
                    case "stoploadexecution":
                    {
                        DisplayOut("Displaying variable 'stoploadexecution' (frmMain) as Boolean." + Constants.vbCrLf +
                                   Utilities.stoploadexecution.ToString());
                        break;
                    }
                    case "userlookup":
                    {
                        DisplayOut("Displaying variable 'userlookup' (frmMain) as String." + Constants.vbCrLf +
                                   Utilities.userlookup);
                        break;
                    }
                    case "settings.user":
                    {
                        DisplayOut("Displaying settings variable 'user' as String." + Constants.vbCrLf +
                                   MySettingsProperty.Settings.User);
                        break;
                    }
                    case "settings.sessionkey":
                    {
                        // check for authorization
                        if (authorized == true)
                        {
                            DisplayOut("Displaying settings variable 'sessionkey' as String." + Constants.vbCrLf +
                                       MySettingsProperty.Settings.SessionKey);
                        }
                        else
                        {
                            DisplayOut(
                                "ERROR: Variable 'settings.sessionkey' requires authorization. Please use the 'authorize' command.");
                        }

                        break;
                    }

                    default:
                    {
                        DisplayOut("ERROR: Variable '" + commands[1] + "' not found.");
                        break;
                    }
                }
            }
            else
            {
                DisplayOut(
                    "ERROR: Missing required parameter 'variablename'. Type 'help authorize' for proper command usage.");
            }
        }

        private void CmdCreatereq(string[] commands)
        {
            // make sure method has been passed required params
            if (commands.Count() >= 2)
            {
                // check for authorization
                if (authorized == true)
                {
                    string[] newcommands = Utilities.FillArray(commands, 5);

                    // url formatting
                    var urldata = new StringBuilder();
                    urldata.Append("http://ws.audioscrobbler.com/2.0/?method=" + newcommands[1] +
                                   "&"); // url and method
                    if (!string.IsNullOrEmpty(newcommands[2]))
                    {
                        urldata.Append("user=" + newcommands[2] + "&"); // user
                    }

                    if (!string.IsNullOrEmpty(newcommands[3]))
                    {
                        urldata.Append(newcommands[3] + "&"); // param1
                    }

                    if (!string.IsNullOrEmpty(newcommands[4]))
                    {
                        urldata.Append(newcommands[4] + "&"); // param2
                    }

                    if (!string.IsNullOrEmpty(newcommands[5]))
                    {
                        urldata.Append(newcommands[5] + "&"); // param3
                    }

                    urldata.Append("api_key=" + Secrets.APIkey); // api key
                    urldata = urldata.Replace(" ", "+");
                    DisplayOut(urldata.ToString());
                }
                else
                {
                    DisplayOut("ERROR: createreq command requires authorization. Please use the 'auth' command.");
                }
            }
            else
            {
                DisplayOut("ERROR: Missing required parameter 'method'. Type 'help createreq' for proper usage.");
            }
        }

        private void CmdCreatesig(string[] commands)
        {
            // make sure method has been passed required params
            if (commands.Count() >= 2)
            {
                // check auth
                if (authorized == true)
                {
                    string[] newcommands = Utilities.FillArray(commands, 4);

                    DisplayOut(
                        Utilities.CreateSignature(newcommands[1], newcommands[2], newcommands[3], newcommands[4]));
                }
                else
                {
                    DisplayOut("ERROR: createsig command requires authorization. Please use the 'auth' command.");
                }
            }
            else
            {
                DisplayOut("ERROR: Missing required parameter 'method'. Type 'help createsig' for proper usage.");
            }
        }

        private void CmdFrm(string[] commands)
        {
            // make sure method has been passed required params
            if (commands.Count() >= 3)
            {
                // open/close/info
                switch (commands[1] ?? "")
                {
                    case "open":
                    {
                        // form name
                        switch (commands[2].ToLower() ?? "")
                        {
                            case "main":
                            {
                                Show();
                                Activate();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "about":
                            {
                                MyProject.Forms.frmAbout.Show();
                                MyProject.Forms.frmAbout.Activate();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "authentication":
                            {
                                MyProject.Forms.frmAuthentication.Show();
                                MyProject.Forms.frmAuthentication.Activate();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "apihistory":
                            {
                                MyProject.Forms.frmAPIHistory.Show();
                                MyProject.Forms.frmAPIHistory.Activate();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "trackadvanced":
                            {
                                MyProject.Forms.frmTrackAdvanced.Show();
                                MyProject.Forms.frmTrackAdvanced.Activate();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "artistadvanced":
                            {
                                MyProject.Forms.frmArtistAdvanced.Show();
                                MyProject.Forms.frmArtistAdvanced.Activate();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "albumadvanced":
                            {
                                MyProject.Forms.frmAlbumAdvanced.Show();
                                MyProject.Forms.frmAlbumAdvanced.Activate();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "addqueue":
                            {
                                MyProject.Forms.frmAddToQueue.Show();
                                MyProject.Forms.frmAddToQueue.Activate();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "console":
                            {
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "scrobblehistory":
                            {
                                MyProject.Forms.frmScrobbleHistory.Show();
                                MyProject.Forms.frmScrobbleHistory.Activate();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "backuptool":
                            {
                                MyProject.Forms.frmBackupTool.Show();
                                MyProject.Forms.frmBackupTool.Activate();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "scrobbleindexeditor":
                            {
                                MyProject.Forms.frmScrobbleIndexEditor.Show();
                                MyProject.Forms.frmScrobbleIndexEditor.Activate();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "scrobbleindexaddrow":
                            {
                                MyProject.Forms.frmScrobbleIndexAddRow.Show();
                                MyProject.Forms.frmScrobbleIndexAddRow.Activate();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "scrobbleindextrackadvanced":
                            {
                                MyProject.Forms.frmScrobbleIndexTrackAdvanced.Show();
                                MyProject.Forms.frmScrobbleIndexTrackAdvanced.Activate();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "scrobblesearch":
                            {
                                MyProject.Forms.frmScrobbleSearch.Show();
                                MyProject.Forms.frmScrobbleSearch.Activate();
                                DisplayOut(string.Empty);
                                break;
                            }

                            default:
                            {
                                DisplayOut("ERROR: Form '" + commands[2] +
                                           "' not found. Type 'help frm' for proper command usage.");
                                break;
                            }
                        }

                        break;
                    }
                    case "close":
                    {
                        switch (commands[2].ToLower() ?? "")
                        {
                            case "main":
                            {
                                Close();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "about":
                            {
                                MyProject.Forms.frmAbout.Close();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "authentication":
                            {
                                MyProject.Forms.frmAuthentication.Close();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "apihistory":
                            {
                                MyProject.Forms.frmAPIHistory.Close();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "trackadvanced":
                            {
                                MyProject.Forms.frmTrackAdvanced.Close();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "artistadvanced":
                            {
                                MyProject.Forms.frmArtistAdvanced.Close();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "albumadvanced":
                            {
                                MyProject.Forms.frmAlbumAdvanced.Close();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "adddqueue":
                            {
                                MyProject.Forms.frmAddToQueue.Close();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "console":
                            {
                                Close();
                                break;
                            }
                            case "scrobblehistory":
                            {
                                MyProject.Forms.frmScrobbleHistory.Close();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "backuptool":
                            {
                                MyProject.Forms.frmBackupTool.Close();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "scrobbleindexeditor":
                            {
                                MyProject.Forms.frmScrobbleIndexEditor.Close();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "scrobbleindexaddrow":
                            {
                                MyProject.Forms.frmScrobbleIndexAddRow.Close();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "scrobbleindextrackadvanced":
                            {
                                MyProject.Forms.frmScrobbleIndexTrackAdvanced.Close();
                                DisplayOut(string.Empty);
                                break;
                            }
                            case "scrobblesearch":
                            {
                                MyProject.Forms.frmScrobbleSearch.Close();
                                DisplayOut(string.Empty);
                                break;
                            }

                            default:
                            {
                                DisplayOut("ERROR: Form '" + commands[2] +
                                           "' not found. Type 'help frm' for proper command usage.");
                                break;
                            }
                        }

                        break;
                    }
                    case "info":
                    {
                        var output = new StringBuilder();
                        switch (commands[2].ToLower() ?? "")
                        {
                            case "main":
                            {
                                output.AppendLine("Displaying information for form frmMain");
                                output.AppendLine(string.Empty);
                                output.AppendLine("Open".PadRight(12) + MyProject.Forms.frmMain.Visible.ToString());
                                output.AppendLine("Size".PadRight(12) + MyProject.Forms.frmMain.Width.ToString() +
                                                  ", " + Height);
                                output.AppendLine("Position".PadRight(12) + MyProject.Forms.frmMain.Left.ToString() +
                                                  ", " + MyProject.Forms.frmMain.Top.ToString());
                                output.Append("WindowState".PadRight(12) +
                                              MyProject.Forms.frmMain.WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }
                            case "about":
                            {
                                output.AppendLine("Displaying information for form frmAbout.");
                                output.AppendLine(string.Empty);
                                output.AppendLine("Open".PadRight(12) + MyProject.Forms.frmAbout.Visible.ToString());
                                output.AppendLine("Size".PadRight(12) + MyProject.Forms.frmAbout.Width.ToString() +
                                                  ", " + MyProject.Forms.frmAbout.Height);
                                output.AppendLine("Position".PadRight(12) + MyProject.Forms.frmAbout.Left.ToString() +
                                                  ", " + MyProject.Forms.frmAbout.Top.ToString());
                                output.Append("WindowState".PadRight(12) +
                                              MyProject.Forms.frmAbout.WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }
                            case "authentication":
                            {
                                output.AppendLine("Displaying information for form frmAuthentication.");
                                output.AppendLine(string.Empty);
                                output.AppendLine("Open".PadRight(12) +
                                                  MyProject.Forms.frmAuthentication.Visible.ToString());
                                output.AppendLine("Size".PadRight(12) +
                                                  MyProject.Forms.frmAuthentication.Width.ToString() + ", " +
                                                  MyProject.Forms.frmAuthentication.Height);
                                output.AppendLine("Position".PadRight(12) +
                                                  MyProject.Forms.frmAuthentication.Left.ToString() + ", " +
                                                  MyProject.Forms.frmAuthentication.Top.ToString());
                                output.Append("WindowState".PadRight(12) +
                                              MyProject.Forms.frmAuthentication.WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }
                            case "apihistory":
                            {
                                output.AppendLine("Displaying information for form frmAPIHistory.");
                                output.AppendLine(string.Empty);
                                output.AppendLine(
                                    "Open".PadRight(12) + MyProject.Forms.frmAPIHistory.Visible.ToString());
                                output.AppendLine("Size".PadRight(12) + MyProject.Forms.frmAPIHistory.Width.ToString() +
                                                  ", " + MyProject.Forms.frmAPIHistory.Height);
                                output.AppendLine("Position".PadRight(12) +
                                                  MyProject.Forms.frmAPIHistory.Left.ToString() + ", " +
                                                  MyProject.Forms.frmAPIHistory.Top.ToString());
                                output.Append("WindowState".PadRight(12) +
                                              MyProject.Forms.frmAPIHistory.WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }
                            case "trackadvanced":
                            {
                                output.AppendLine("Displaying information for form frmTrackAdvanced.");
                                output.AppendLine(string.Empty);
                                output.AppendLine("Open".PadRight(12) +
                                                  MyProject.Forms.frmTrackAdvanced.Visible.ToString());
                                output.AppendLine("Size".PadRight(12) +
                                                  MyProject.Forms.frmTrackAdvanced.Width.ToString() + ", " +
                                                  MyProject.Forms.frmTrackAdvanced.Height);
                                output.AppendLine("Position".PadRight(12) +
                                                  MyProject.Forms.frmTrackAdvanced.Left.ToString() + ", " +
                                                  MyProject.Forms.frmTrackAdvanced.Top.ToString());
                                output.Append("WindowState".PadRight(12) +
                                              MyProject.Forms.frmTrackAdvanced.WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }
                            case "artistadvanced":
                            {
                                output.AppendLine("Displaying information for form frmArtistAdvanced.");
                                output.AppendLine(string.Empty);
                                output.AppendLine("Open".PadRight(12) +
                                                  MyProject.Forms.frmArtistAdvanced.Visible.ToString());
                                output.AppendLine("Size".PadRight(12) +
                                                  MyProject.Forms.frmArtistAdvanced.Width.ToString() + ", " +
                                                  MyProject.Forms.frmArtistAdvanced.Height);
                                output.AppendLine("Position".PadRight(12) +
                                                  MyProject.Forms.frmArtistAdvanced.Left.ToString() + ", " +
                                                  MyProject.Forms.frmArtistAdvanced.Top.ToString());
                                output.AppendLine("WindowState".PadRight(12) +
                                                  MyProject.Forms.frmArtistAdvanced.WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }
                            case "albumadvanced":
                            {
                                output.AppendLine("Displaying information for form frmAlbumAdvanced.");
                                output.AppendLine(string.Empty);
                                output.AppendLine("Open".PadRight(12) +
                                                  MyProject.Forms.frmAlbumAdvanced.Visible.ToString());
                                output.AppendLine("Size".PadRight(12) +
                                                  MyProject.Forms.frmAlbumAdvanced.Width.ToString() + ", " +
                                                  MyProject.Forms.frmAlbumAdvanced.Height);
                                output.AppendLine("Position".PadRight(12) +
                                                  MyProject.Forms.frmAlbumAdvanced.Left.ToString() + ", " +
                                                  MyProject.Forms.frmAlbumAdvanced.Top.ToString());
                                output.AppendLine("WindowState".PadRight(12) +
                                                  MyProject.Forms.frmAlbumAdvanced.WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }
                            case "addqueue":
                            {
                                output.AppendLine("Displaying information for form frmAddToQueue.");
                                output.AppendLine(string.Empty);
                                output.AppendLine(
                                    "Open".PadRight(12) + MyProject.Forms.frmAddToQueue.Visible.ToString());
                                output.AppendLine("Size".PadRight(12) + MyProject.Forms.frmAddToQueue.Width.ToString() +
                                                  ", " + MyProject.Forms.frmAddToQueue.Height);
                                output.AppendLine("Position".PadRight(12) +
                                                  MyProject.Forms.frmAddToQueue.Left.ToString() + ", " +
                                                  MyProject.Forms.frmAddToQueue.Top.ToString());
                                output.AppendLine("WindowState".PadRight(12) +
                                                  MyProject.Forms.frmAddToQueue.WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }
                            case "console":
                            {
                                output.AppendLine("Displaying information for form frmConsole.");
                                output.AppendLine(string.Empty);
                                output.AppendLine("Open".PadRight(12) + Visible.ToString());
                                output.AppendLine("Size".PadRight(12) + Width.ToString() + ", " + Height);
                                output.AppendLine("Position".PadRight(12) + Left.ToString() + ", " + Top.ToString());
                                output.Append("WindowState".PadRight(12) + WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }
                            case "scrobblehistory":
                            {
                                output.AppendLine("Displaying information for form frmscrobblehistory.");
                                output.AppendLine(string.Empty);
                                output.AppendLine("Open".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleHistory.Visible.ToString());
                                output.AppendLine("Size".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleHistory.Width.ToString() + ", " +
                                                  MyProject.Forms.frmScrobbleHistory.Height);
                                output.AppendLine("Position".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleHistory.Left.ToString() + ", " +
                                                  MyProject.Forms.frmScrobbleHistory.Top.ToString());
                                output.AppendLine("WindowState".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleHistory.WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }
                            case "backuptool":
                            {
                                output.AppendLine("Displaying information for form frmbackuptool.");
                                output.AppendLine(string.Empty);
                                output.AppendLine(
                                    "Open".PadRight(12) + MyProject.Forms.frmBackupTool.Visible.ToString());
                                output.AppendLine("Size".PadRight(12) + MyProject.Forms.frmBackupTool.Width.ToString() +
                                                  ", " + MyProject.Forms.frmBackupTool.Height);
                                output.AppendLine("Position".PadRight(12) +
                                                  MyProject.Forms.frmBackupTool.Left.ToString() + ", " +
                                                  MyProject.Forms.frmBackupTool.Top.ToString());
                                output.AppendLine("WindowState".PadRight(12) +
                                                  MyProject.Forms.frmBackupTool.WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }
                            case "scrobbleindexeditor":
                            {
                                output.AppendLine("Displaying information for form frmscrobbleindexeditor.");
                                output.AppendLine(string.Empty);
                                output.AppendLine("Open".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleIndexEditor.Visible.ToString());
                                output.AppendLine("Size".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleIndexEditor.Width.ToString() + ", " +
                                                  MyProject.Forms.frmScrobbleIndexEditor.Height);
                                output.AppendLine("Position".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleIndexEditor.Left.ToString() + ", " +
                                                  MyProject.Forms.frmScrobbleIndexEditor.Top.ToString());
                                output.AppendLine("WindowState".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleIndexEditor.WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }
                            case "scrobbleindexaddrow":
                            {
                                output.AppendLine("Displaying information for form frmscrobbleindexaddrow.");
                                output.AppendLine(string.Empty);
                                output.AppendLine("Open".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleIndexAddRow.Visible.ToString());
                                output.AppendLine("Size".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleIndexAddRow.Width.ToString() + ", " +
                                                  MyProject.Forms.frmScrobbleIndexAddRow.Height);
                                output.AppendLine("Position".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleIndexAddRow.Left.ToString() + ", " +
                                                  MyProject.Forms.frmScrobbleIndexAddRow.Top.ToString());
                                output.AppendLine("WindowState".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleIndexAddRow.WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }
                            case "scrobbleindextrackadvanced":
                            {
                                output.AppendLine("Displaying information for form frmscrobbleindextrackadvanced.");
                                output.AppendLine(string.Empty);
                                output.AppendLine("Open".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleIndexTrackAdvanced.Visible.ToString());
                                output.AppendLine("Size".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleIndexTrackAdvanced.Width.ToString() +
                                                  ", " + MyProject.Forms.frmScrobbleIndexTrackAdvanced.Height);
                                output.AppendLine("Position".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleIndexTrackAdvanced.Left.ToString() + ", " +
                                                  MyProject.Forms.frmScrobbleIndexTrackAdvanced.Top.ToString());
                                output.AppendLine("WindowState".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleIndexTrackAdvanced.WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }
                            case "scrobblesearch":
                            {
                                output.AppendLine("Displaying information for form frmscrobblesearch.");
                                output.AppendLine(string.Empty);
                                output.AppendLine("Open".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleSearch.Visible.ToString());
                                output.AppendLine("Size".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleSearch.Width.ToString() + ", " +
                                                  MyProject.Forms.frmScrobbleSearch.Height);
                                output.AppendLine("Position".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleSearch.Left.ToString() + ", " +
                                                  MyProject.Forms.frmScrobbleSearch.Top.ToString());
                                output.AppendLine("WindowState".PadRight(12) +
                                                  MyProject.Forms.frmScrobbleSearch.WindowState.ToString());
                                DisplayOut(output.ToString());
                                break;
                            }

                            default:
                            {
                                DisplayOut("ERROR: Form '" + commands[2] +
                                           "' not found. Type 'help frm' for proper command usage.");
                                break;
                            }
                        }

                        break;
                    }

                    default:
                    {
                        DisplayOut("ERROR: '" + commands[1] +
                                   "' parameter not recognized. Type 'help frm' for proper command usage.");
                        break;
                    }
                }
            }
            else if (commands.Count() == 2)
            {
                DisplayOut("ERROR: Missing required parameter 'formname'. Type 'help frm' for proper usage.");
            }
            else
            {
                DisplayOut("ERROR: Missing required parameter 'open/close/info'. Type 'help frm' for proper usage.");
            }
        }

        private void CmdSetsk(string[] commands)
        {
            // check commands
            if (commands.Count() >= 2)
            {
                // check auth
                if (authorized == true)
                {
                    // remember old session key
                    string oldsk = MySettingsProperty.Settings.SessionKey;

                    // set session key
                    MySettingsProperty.Settings.SessionKey = commands[1];

                    // test session key
                    string response1 = Utilities.CallAPIAuth("artist.addTags", "artist=test", "tag=test");

                    if (response1 == "CallAPIAuth ERROR: The remote server returned an error: (403) Forbidden." == true)
                    {
                        MySettingsProperty.Settings.SessionKey = oldsk;
                        DisplayOut("ERROR: Invalid session key provided.");
                    }
                    else if (response1.Contains("ERROR: ") == true)
                    {
                        MySettingsProperty.Settings.SessionKey = oldsk;
                        DisplayOut("ERROR: Session key was unable to complete test.");
                    }
                    else
                    {
                        // second step
                        string response2 = Utilities.CallAPI("artist.getTags", MySettingsProperty.Settings.User,
                            "artist=test");
                        if (response2.Contains("test") == true)
                        {
                            Utilities.CallAPIAuth("artist.removeTag", "artist=test", "tag=test");
                            MyProject.Forms.frmMain.AuthenticatedUI(true);
                            DisplayOut("Session key has been successfully verified and set.");
                        }
                        else
                        {
                            MySettingsProperty.Settings.SessionKey = oldsk;
                            DisplayOut("ERROR: Session key does not match user.");
                        }
                    }
                }
                else
                {
                    DisplayOut("ERROR: setsk command requires authorization. Please use the 'auth' command.");
                }
            }
            else
            {
                DisplayOut("ERROR: Missing required parameter 'sessionkey'. Type 'help setsk' for proper usage.");
            }
        }

        private void CmdThreads()
        {
            var output = new StringBuilder();
            // charts
            if (MyProject.Forms.frmMain.bgwChartUpdater.IsBusy == true)
            {
                output.AppendLine("Chart update thread Is currently active. (bgwChartUpdater)");
            }

            // track
            if (MyProject.Forms.frmMain.bgwTrackUpdater.IsBusy == true)
            {
                output.AppendLine("Track update thread Is currently active. (bgwTrackUpdater)");
            }

            // artist
            if (MyProject.Forms.frmMain.bgwArtistUpdater.IsBusy == true)
            {
                output.AppendLine("Artist update thread Is currently active. (bgwArtistUpdater)");
            }

            // album
            if (MyProject.Forms.frmMain.bgwAlbumUpdater.IsBusy == true)
            {
                output.AppendLine("Album update thread Is currently active. (bgwAlbumUpdater)");
            }

            // tag
            if (MyProject.Forms.frmMain.bgwSearchUpdater.IsBusy == true)
            {
                output.AppendLine("Tag update thread Is currently active. (bgwTagUpdater)");
            }

            // user
            if (MyProject.Forms.frmMain.bgwUserUpdater.IsBusy == true)
            {
                output.AppendLine("User update thread Is currently active. (bgwUserUpdater)");
            }

            // user loved
            if (MyProject.Forms.frmMain.bgwUserLovedUpdater.IsBusy == true)
            {
                output.AppendLine("User loved tracks update thread Is currently active. (bgwUserLovedUpdater)");
            }

            // user chart
            if (MyProject.Forms.frmMain.bgwUserChartUpdater.IsBusy == true)
            {
                output.AppendLine("User charts update thread Is currently active. (bgwUserChartUpdater)");
            }

            // user history
            if (MyProject.Forms.frmMain.bgwUserHistoryUpdater.IsBusy == true)
            {
                output.AppendLine("User history update thread Is currently active. (bgwUserHistoryUpdater)");
            }

            // user lookup
            if (MyProject.Forms.frmMain.bgwUserLookupUpdater.IsBusy == true)
            {
                output.AppendLine("User lookup update thread Is currently active. (bgwUserLookupUpdater)");
            }

            // userl loved
            if (MyProject.Forms.frmMain.bgwUserLLovedUpdater.IsBusy == true)
            {
                output.AppendLine("User lookup loved tracks update thread Is currently active. (bgwUserLLovedUpdater)");
            }

            // userl chart
            if (MyProject.Forms.frmMain.bgwUserLChartUpdater.IsBusy == true)
            {
                output.AppendLine("User lookup charts update thread Is currently active. (bgwUserLChartUpdater)");
            }

            // userl history
            if (MyProject.Forms.frmMain.bgwUserLHistoryUpdater.IsBusy == true)
            {
                output.AppendLine("User lookup history update thread Is currently active. (bgwUserLHistoryUpdater)");
            }

            // check if no threads were active
            if (string.IsNullOrEmpty(output.ToString()))
            {
                output.Append("No threads are currently active.");
            }

            DisplayOut(output.ToString());
        }

        private void CmdResetprog()
        {
            Utilities.progress = 0;
            Utilities.progressmultiplier = 0;
            MyProject.Forms.frmMain.UpdateProgress.Value = 100;
            MyProject.Forms.frmMain.UpdateProgress.Visible = false;
            DisplayOut(string.Empty);
        }

        private void CmdTime()
        {
            var output = new StringBuilder();
            output.AppendLine("Current Time: " + DateTime.Now.ToString("G") + " (" +
                              Utilities.DateToUnix(DateTime.Now).ToString() + ")");
            output.AppendLine("Current Time (UTC): " + Utilities.UnixToDate(Utilities.GetCurrentUTC()).ToString("G") +
                              " (" + Utilities.GetCurrentUTC().ToString() + ")");
            output.Append("Time Zone Offset: " + (Utilities.timezoneoffset / 3600d).ToString() + ":00 (" +
                          Utilities.timezoneoffset.ToString() + ")");
            DisplayOut(output.ToString());
        }

        private void CmdViewsk()
        {
            // check authenticated
            if (string.IsNullOrEmpty(MySettingsProperty.Settings.SessionKey))
            {
                DisplayOut("Current user is not authenticated.");
            }
            // check authorized
            else if (authorized == true)
            {
                DisplayOut("Current session key is '" + MySettingsProperty.Settings.SessionKey + "' for user '" +
                           MySettingsProperty.Settings.User + "'");
            }
            else
            {
                DisplayOut("ERROR: viewsk command requires authorization. Please use the 'auth' command.");
            }
        }

        private void CmdSecret()
        {
            if (tmrSecret.Enabled == false)
            {
                tmrSecret.Start();
            }
            else
            {
                tmrSecret.Stop();
                BackColor = SystemColors.Control;
            }

            DisplayOut(string.Empty);
        }

        #endregion
    }
}