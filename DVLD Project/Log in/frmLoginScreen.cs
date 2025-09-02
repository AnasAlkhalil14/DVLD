using DVLD_Business_Tier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD.Log_in
{
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }
       


        void SaveDataUserWhenLoginInSetting()
        {
            if(chbRememberMe.Checked)
            {
                Properties.Settings.Default.UserName=txtUserName.Text;
                Properties.Settings.Default.Password=txtPassword.Text;
                Properties.Settings.Default.RememberMe = chbRememberMe.Checked;
            }
            else
            {
                Properties.Settings.Default.UserName = "";
                Properties.Settings.Default.Password ="";
                Properties.Settings.Default.RememberMe = false;

            }
            Properties.Settings.Default.Save();

        }
        void LoadDataIfRememberMeFromSetting()
        {
            if (clsGlobal.User==null)
            {
                if (Properties.Settings.Default.RememberMe)
                {
                    txtPassword.Text = Properties.Settings.Default.Password;
                    txtUserName.Text = Properties.Settings.Default.UserName;
                    chbRememberMe.Checked = true;

                }
                else
                {
                    txtPassword.Text = "";
                    txtUserName.Text = "";
                    chbRememberMe.Checked = false;


                }
            }
            else
                FillDataBack(clsGlobal.User.UserName, clsGlobal.User.Password);

        }



        void FillDataBack(string UserName, string Password)
        {
            txtUserName.Text = UserName;
            txtPassword.Text = Password;
            chbRememberMe.Checked = clsGlobal.IsRememberMe ;
        }



        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Invalid UserName/Password", "Wrong Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            clsUser User = clsUser.Find(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            if (User != null)
            {
                if (User.IsActive)
                {
                   frmMain frm=new frmMain();  
                    clsGlobal.User = User;
                    clsGlobal.IsRememberMe = chbRememberMe.Checked;
                    SaveDataUserWhenLoginInSetting();
                    frm.Show();
                this.Close();

                }
                else
                {
                    MessageBox.Show("this user is not active , contact your admin", "Not Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            else
            {
                MessageBox.Show("Invalid UserName/Password", "Wrong Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;


            }





        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            LoadDataIfRememberMeFromSetting();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
