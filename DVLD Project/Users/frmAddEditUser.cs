using DVLD_Business_Tier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmAddEditUser : Form
    {
        public frmAddEditUser()
        {
            InitializeComponent();
            Mode = enMode.eAdd;
            User = new clsUser();
        }
        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            Mode = enMode.eUpdate;
            User =  clsUser.Find(UserID);
            ctrlFilterPerson1.ctrlPersonInfo1.LoadPersonInfo(User.Person.PersonID);
            FillUserInfo();
            ctrlFilterPerson1.gbFilters.Enabled = false;
            lblAddEditUser.Text = "Update User With ID=" + User.UserID;

        }
        void FillUserInfo()
        {
            lblPersonID.Text=User.Person.PersonID.ToString();
            lblUserID.Text=User.UserID.ToString();
            txtPassword.Text=User.Password;
            txtUserName.Text=User.UserName;
            txtConfirmPassword.Text = User.Password;

        }



        enum enMode { eAdd,eUpdate}
        enMode Mode;
        private void button1_Click(object sender, EventArgs e)
        {
            
            tbcUserInfo.SelectedIndex = 1;

        }

        public event Action OnSave;
        clsUser User;
        private void ctrlFilterPerson1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            
        if(txtConfirmPassword.Text!=txtPassword.Text)
            {
                errorProvider1.SetError(txtConfirmPassword, "The Confirmed Password is different from the password you choose ,correct it");
            }
        else
            {
                errorProvider1.SetError(txtConfirmPassword,"");

            }


        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {

        }

        bool CheckToSave()
        {
            
            if(int.TryParse(lblPersonID.Text, out int personID))
            {

            }
            else
            {
                MessageBox.Show("Select a Person First", "Wrong Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("UserName/Password/ConfirmPassword can not be empty", "Wrong Data", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            if(txtConfirmPassword.Text!=txtPassword.Text)
            {
                MessageBox.Show("ConfirmPassword must equal Password", "Wrong Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            User.Password = txtPassword.Text;
            User.UserName = txtUserName.Text;
            User.IsActive=chbIsActive.Checked;
            User.Person.PersonID = personID;

            return true;




        }
        private void tbcUserInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ctrlFilterPerson1.PersonID!=-1)
            lblPersonID.Text = ctrlFilterPerson1.PersonID.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckToSave())
            {
               if(User.Save())
                {
                    if(Mode==enMode.eAdd)
                    MessageBox.Show("User added Successfully with ID="+User.UserID,"Resutl",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("User Updated Successfully with ID=" + User.UserID, "Resutl", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    OnSave?.Invoke();



                }




            }





        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
