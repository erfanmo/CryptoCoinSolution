using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceDllTest
{
    public partial class frm_ShowResult : Form
    {
        public frm_ShowResult()
        {
            InitializeComponent();
        }

        public frm_ShowResult(string result)
        {
            InitializeComponent();
            txt_Result.Text = result;
        }
    }
}
