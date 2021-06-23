using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuanLiQuanAn
{
    public partial class fManager : Form
    {
        public fManager()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            FInfo fi = new FInfo();
            fi.ShowDialog();
        }
    }
}
