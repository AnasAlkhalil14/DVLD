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
    public partial class frmUsersList : Form
    {
        public frmUsersList()
        {
            InitializeComponent();
        }
        clsUser User;
       
        enum enFilter { eNone,eUserID,eUserName,ePersonID,eFullName,eIsActive}
        enFilter Filter;
        enum enActive { eAll,eYes,eNo}
        enActive ActiveState;
        void RefreshForm()
        {
          
            dgvUsersList.DataSource = clsUser.AllUsers(); ;
            lblRecordsCount.Text=dgvUsersList.Rows.Count.ToString();
        }

        private void lblRecordsCount_Click(object sender, EventArgs e)
        {

        }

        private void frmUsersList_Load(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void edintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(dgvUsersList.CurrentRow.Cells["UserID"].Value);
            frmAddEditUser frm=new frmAddEditUser(UserID);
            frm.OnSave += Frm_OnSave;
            frm.ShowDialog();

        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm=new frmAddEditUser();
            frm.OnSave += Frm_OnSave;
            frm.ShowDialog();
        }

        private void Frm_OnSave()
        {
            RefreshForm();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.OnSave += Frm_OnSave;
            frm.ShowDialog();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter = (enFilter)cbFilter.SelectedIndex;
            if(Filter==enFilter.eNone)
            {
                txtFilter.Visible = false;
                cbActiveState.Visible = false;
                RefreshForm();
                return;
            }
            if(Filter==enFilter.eIsActive)
            {
                txtFilter.Visible = false;
                cbActiveState.Visible = true;
                cbActiveState.SelectedIndex = (int)enActive.eAll;
                RefreshForm();

                return;
            }
            else
            {
                txtFilter.Clear();
                txtFilter.Visible = true;
                cbActiveState.Visible = false;
               
            }

            
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(Filter==enFilter.eUserID||Filter==enFilter.ePersonID)
            if(!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }



        }

        void ManageFilterWithTextBox()
        {
            DataTable dtOrginal = dgvUsersList.DataSource as DataTable;
            DataTable dtClone=dtOrginal.Clone() ;
           
             switch (Filter)
            {
                case enFilter.eUserID:
                    {
                        dtOrginal = clsUser.FilterByUserID(Convert.ToInt32(txtFilter.Text));
                        if(dtOrginal != null)
                        {
                            dgvUsersList.DataSource = dtOrginal;
                        }else
                        {
                            dgvUsersList.DataSource = dtClone;
                        }


                            break;
                    }
                    case enFilter.ePersonID:
                    {
                        dtOrginal = clsUser.FilterByPersonID(Convert.ToInt32(txtFilter.Text));
                        if (dtOrginal != null)
                        {
                            dgvUsersList.DataSource = dtOrginal;
                        }
                        else
                        {
                            dgvUsersList.DataSource = dtClone;
                        }


                        break;



                    }

                case enFilter.eUserName:
                    {
                        dtOrginal = clsUser.FilterByUserName(txtFilter.Text);
                        if (dtOrginal != null)
                        {
                            dgvUsersList.DataSource = dtOrginal;
                        }
                        else
                        {
                            dgvUsersList.DataSource = dtClone;
                        }


                        break;

                    }
                case enFilter.eFullName:
                    {
                        dtOrginal = clsUser.FilterByFullName(txtFilter.Text);
                        if (dtOrginal != null)
                        {
                            dgvUsersList.DataSource = dtOrginal;
                        }
                        else
                        {
                            dgvUsersList.DataSource = dtClone;
                        }


                        break;



                    }







            }
            lblRecordsCount.Text=dgvUsersList.Rows.Count.ToString();



        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtFilter.Text)) 
            ManageFilterWithTextBox();
            else
                RefreshForm(); 


        }

        private void cbActiveState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveState=(enActive)cbActiveState.SelectedIndex;
            DataTable dtOrginal = dgvUsersList.DataSource as DataTable;
            DataTable dtClone = dtOrginal.Clone();
            switch (ActiveState)
                {
                case enActive.eAll:
                    {
                        dgvUsersList.DataSource = clsUser.AllUsers();
                        break;
                        
                    }
                case enActive.eYes:
                    {
                        dtOrginal = clsUser.FilterByActiveState(true);
                        if (dtOrginal != null)
                        {
                            dgvUsersList.DataSource = dtOrginal;
                        }
                        else
                        {
                            dgvUsersList.DataSource = dtClone;
                        }


                        break;



                    }
                case enActive.eNo:
                    {
                        dtOrginal = clsUser.FilterByActiveState(false);
                        if (dtOrginal != null)
                        {
                            dgvUsersList.DataSource = dtOrginal;
                        }
                        else
                        {
                            dgvUsersList.DataSource = dtClone;
                        }


                        break;




                    }




            }



        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(dgvUsersList.CurrentRow.Cells["UserID"].Value);

            if (UserID != clsGlobal.User.UserID)
            {
                if (MessageBox.Show("Are You Sure You Want To Delete User With ID=" + UserID + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    if (clsUser.Delete(UserID))
                    {
                        MessageBox.Show("User With ID=" + UserID + " Deleted Successfully.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshForm();
                    }
                    else
                    {
                        MessageBox.Show("User With ID=" + UserID + " Can not be Deleted he has data link to him.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }



                }
            }
            else
            {
                MessageBox.Show("sorry,but User Can Not Delete Himself :-).", "Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }





        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(dgvUsersList.CurrentRow.Cells["UserID"].Value);
            frmUserDetails frm=new frmUserDetails(UserID);
            frm.ShowDialog();

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(dgvUsersList.CurrentRow.Cells["UserID"].Value);
            frmChangePassword frm=new frmChangePassword(UserID);
            frm.ctrlUserInfo1.ctrlPersonInfo1.OnSave += CtrlPersonInfo1_OnSave;
            frm.ctrlUserInfo1.OnSave += CtrlPersonInfo1_OnSave;
            frm.ctrlUserInfo1.OnSavePerson += CtrlPersonInfo1_OnSave;
            frm.ShowDialog();
        }

        private void CtrlPersonInfo1_OnSave()
        {
          RefreshForm();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
