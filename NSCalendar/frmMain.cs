using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSCalendar
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            ListViewItem aItem = new ListViewItem();
            aItem.Text = "이름";
            listView1.Items.Add(aItem);
        }
    }
}
