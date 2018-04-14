using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureClient
{
    public partial class LogonPage : Form
    {
        private string username = "";
        private string password = "";

        public string Username
        {
            get
            {
                return username;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
        }
        public LogonPage()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private void LogonButton_Click(object sender, EventArgs e)
        {
            username = UsernameTexbox.Text;
            password = PasswordTextbox.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
