using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Audiograph
{
    public partial class frmAbout
    {
        public frmAbout()
        {
            InitializeComponent();
        }
        private void FormLoad(object sender, EventArgs e)
        {
            txtAbout.Rtf = @"{\rtf1\ansi \b Description\b0 \par Music tracking program that utilizes the Last.fm service \par \par
\b Info\b0 \par .NET Target: 4.7.2 \par Language: C# \par Release Date: 8/15/2023 \par \par
Uses Last.fm - https://www.last.fm \par Copyright" + Strings.Chr(169) + @" 2023 - Dylan Miller \par Some icons created by Riley Schaefer}";
        }

        private void LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void ExitForm(object sender, EventArgs e)
        {
            Close();
        }
    }
}