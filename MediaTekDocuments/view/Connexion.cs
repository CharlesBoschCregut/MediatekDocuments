using MediaTekDocuments.controller;
using System;
using System.Windows.Forms;

namespace MediaTekDocuments.view
{
    public partial class Connexion : Form
    {
        private string login;
        private string password;
        private readonly FrmMediatekController controller;
        public Connexion()
        {
            InitializeComponent();
            this.controller = new FrmMediatekController();
        }

        private void btnSeConnecter_Click(object sender, EventArgs e)
        {
            this.login = txbLogin.Text;
            this.password = txbPwd.Text;

            (bool valid, string service) = IsValidLogin(login, password);
            if (valid)
            {
                Console.WriteLine("service : " + service);
                if (service == "000")
                {
                    MessageBox.Show("Ce service (Culture) n'a pas accès a cette application.");
                    Environment.Exit(0);

                }
                else
                {
                    FrmMediatek main = new FrmMediatek(service);
                    main.FormClosing += (s, args) => this.Close();
                    main.Show();
                    this.Hide();
                }
                
            }
            else
            {
                MessageBox.Show("Combinaison invalide.");
                txbLogin.Clear();
                txbPwd.Clear();
                txbLogin.Focus();
            }
        }

        private (bool, string) IsValidLogin(string username, string password)
        {
            string response = controller.GetUserLogin(username, password);
            if (response == null || response == "failed")
            {
                return (false, null);
            }
            else
            {
                return (true, response);
            }
        }
    }
}
