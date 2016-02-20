using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BillingSystem.FormRpt
{
    public partial class RptPreview : Form
    {
        public RptPreview()
        {
            InitializeComponent();
        }

        private void RptPreview_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

        }
    }
}