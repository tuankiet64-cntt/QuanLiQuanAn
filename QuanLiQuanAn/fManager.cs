using QuanLiQuanAn.DAO;
using QuanLiQuanAn.DTO;
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
            tableLoad();
        }
        #region event
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            FInfo fi = new FInfo();
            fi.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fAdmin fAdmin = new fAdmin();
            fAdmin.ShowDialog();
        }
        #endregion
        #region Method
        void tableLoad()
        {
            List<table> tableList = TableDAO.Intance.LoadTableList();

            foreach(table item in tableList)
            {   
                Button btn = new Button() { Width=TableDAO.TableWidth ,Height =TableDAO.TableWidth };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                switch (item.Status) {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                      
                };
               
            flpTable.Controls.Add(btn);
            }
        }
        #endregion

    }
}
