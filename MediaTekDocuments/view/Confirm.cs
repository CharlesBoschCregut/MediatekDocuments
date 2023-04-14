using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaTekDocuments.view
{
    public partial class Confirm : Form
    {
        Action<string, string> callback;
        string table;
        string id;
        public Confirm(string msg, string table = "", string id = "",  Action<string, string> action = null)
        {
            InitializeComponent();
            message.Text = msg;
            this.table = table;
            this.id = id;
            this.callback = action;

            if (callback == null) 
            {
                YesConfirm.Visible = false;
                YesConfirm.Enabled = false;
                NotConfirm.Visible = false;
                NotConfirm.Enabled = false;
                Ok.Visible = true;
            }
        }

        private void YesConfirm_Click(object sender, EventArgs e)
        {
            callback?.Invoke(this.table, this.id);
            this.Close();
        }

        private void NotConfirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
