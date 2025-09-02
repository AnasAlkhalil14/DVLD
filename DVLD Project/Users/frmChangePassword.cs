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

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            User = clsUser.Find(UserID);
            ctrlUserInfo1.LoadUserInfo(UserID);

        }
        clsUser User;
        public event Action<string > OnSave;

        private void frmChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {
                errorProvider1.SetError(txtCurrentPassword, "Can not be empty");
            }
            if(txtCurrentPassword.Text!=User.Password)
            {
                errorProvider1.SetError(txtCurrentPassword, "Wrong Password");
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, "");
            }

        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                errorProvider1.SetError(txtConfirmPassword, "Can not be empty");
                return;
            }
            
            if (txtConfirmPassword.Text != txtNewPassword.Text)
            {
                errorProvider1.SetError(txtConfirmPassword, "Wrong Confirmed Password");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, "");
            }


        }

        bool CheckPassword()
        {
            int Right = 0;
            if(string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                {
                errorProvider1.SetError(txtConfirmPassword, "Can not be empty");

            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, "");
                Right++;

            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                errorProvider1.SetError(txtNewPassword, "Can not be empty");

            }
            else
            {
                errorProvider1.SetError(txtNewPassword, "");

                Right++;
            }
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {
                errorProvider1.SetError(txtCurrentPassword, "Can not be empty");

            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, "");

                Right++;
            }
            if (txtCurrentPassword.Text != User.Password)
            {
                errorProvider1.SetError(txtCurrentPassword, "Wrong Password");

            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, "");
                Right++;
            }

                
            if (txtConfirmPassword.Text != txtNewPassword.Text)
            {
                errorProvider1.SetError(txtConfirmPassword, "Wrong Confirmed Password");

            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, "");
                Right++;
            }


                return Right == 5;




            }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to update your password", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                if (CheckPassword())
                {
                    if (clsUser.UpdatePassword(User.UserID, txtNewPassword.Text))
                    {
                        MessageBox.Show("Password Updated Successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OnSave?.Invoke(txtNewPassword.Text);
                    }
                    else
                        MessageBox.Show("Password Updated Failed", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);



                }
                else
                {
                    MessageBox.Show("Password Updated Failed but the mouse over the red icon to see the error", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }


            }




        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlUserInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
