using DVLD.People;
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
    public partial class ctrlUserInfo : UserControl
    {
        public ctrlUserInfo()
        {
            InitializeComponent();
          
       
        }
        clsUser User;
        public event Action OnSave;
        public event Action OnSavePerson;
      public  void LoadUserInfo(int UserID)
        {
            User = clsUser.Find(UserID);
            ctrlPersonInfo1.LoadPersonInfo(User.Person.PersonID);
            lblIsActive.Text=User.IsActive.ToString();
            lblUserID.Text=User.UserID.ToString();
            lblUserName.Text=User.UserName;
        }


        private void ctrlUserInfo_Load(object sender, EventArgs e)
        {

        }

        private void lnkEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmAddEditUser frm=new frmAddEditUser(User.UserID);
            frm.OnSave += Frm_OnSave;
            frm.ctrlFilterPerson1.ctrlPersonInfo1.OnSave += CtrlPersonInfo1_OnSave;
            frm.ShowDialog();

        }

        private void Frm1_OnDeleteImageForShowDetails()
        {
      
        }

        private void CtrlPersonInfo1_OnSave()
        {
            ctrlPersonInfo1.LoadPersonInfo(User.Person.PersonID);
            OnSavePerson?.Invoke();
        }

        private void Frm_OnSave()
        {

            LoadUserInfo(User.UserID);
            OnSave?.Invoke();

        }
    }
}
