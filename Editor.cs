using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KwicFrontend
{
    public partial class editor : Form
    {
        public hub Hub;
        //public Process Browser;
        public editor()
        {
            InitializeComponent();
        }

        private void editorClosed(object sender, FormClosedEventArgs e)
        {
            Hub.Show();
        }
    }
}
