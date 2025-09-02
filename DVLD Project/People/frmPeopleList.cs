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

namespace DVLD.People
{
    public partial class frmPeopleList : Form
    {
        public frmPeopleList()
        {
            InitializeComponent();
        }
        
    enum enFilter {
     None    ,
     PersonID  ,
     FirsName  ,
     SecondName,
     ThirdName ,
     LastName  ,
     Gendor     ,
     Nationaliy ,
     Phone      ,
     Email
             }

        

        void RefreshForm()
        {
            rbMale.Checked = false;
            rbFemale.Checked = false;
            txtFilter.Clear();
            DataTable dt = clsPerson.AllPeople();
            dgvPeopleList.DataSource = dt;

            lblRecordsCount.Text = dt.Rows.Count.ToString();

        }


        private void frmPeopleList_Load(object sender, EventArgs e)
        {
            this.TopLevel = true;   
            RefreshForm();
            cbFilter.SelectedIndex = 0;
        }

        void DoFilter(enFilter Filter,object ToFilterBy )
        {
            if(string.IsNullOrWhiteSpace(txtFilter.Text)&&txtFilter.Visible)
            {
                RefreshForm();
                return;
            }

            DataTable dt;
            switch (Filter)
            {
               
                    case enFilter.PersonID :
                    {
                        dt = clsPerson.FilterByPersonID(Convert.ToInt32(ToFilterBy));
                        break;
                    }
                    case enFilter.FirsName :
                    {
                        dt = clsPerson.FilterByFirstName(ToFilterBy.ToString());
                        break;

                    }
                case enFilter.Phone:
                    {
                        dt = clsPerson.FilterByPhone(ToFilterBy.ToString());
                        break;

                    }
                case enFilter.ThirdName:
                    {
                        dt = clsPerson.FilterByThirdName(ToFilterBy.ToString());
                        break;

                    }
                case enFilter.Email:
                    {
                        dt = clsPerson.FilterByEmail(ToFilterBy.ToString());
                        break;

                    }

                case enFilter.Gendor:
                    {
                        dt = clsPerson.FilterByGendor(Convert.ToInt32(ToFilterBy));
                        break;

                    }
                case enFilter.LastName:
                    {
                        dt = clsPerson.FilterByLastName(ToFilterBy.ToString());
                        break;

                    }
                case enFilter.Nationaliy:
                    {
                        dt = clsPerson.FilterByNationality(ToFilterBy.ToString());
                        break;

                    }
                case enFilter.SecondName:
                    {
                        dt = clsPerson.FilterBySecondName(ToFilterBy.ToString());
                        break;

                    }
                default:
                    {
                        dt = clsPerson.AllPeople();
                        break;
                    }

            }

            dgvPeopleList.DataSource = dt;
            lblRecordsCount.Text = dt.Rows.Count.ToString();




        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            RefreshForm();
            enFilter filter = (enFilter)cbFilter.SelectedIndex;
            if (filter == enFilter.None)
            {
                rbFemale.Visible = false;
                rbMale.Visible = false;
                txtFilter.Visible = false;

            }

            else if (filter == enFilter.Gendor)
            {
                txtFilter.Visible = false;
                rbMale.Visible=true;
                rbFemale.Visible = true;
            }
            else

            {
                txtFilter.Visible = true;
                rbFemale.Visible = false;
                rbMale.Visible = false;
            }

              



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DoFilter((enFilter)cbFilter.SelectedIndex,txtFilter.Text);

        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            enFilter filter=(enFilter)cbFilter.SelectedIndex;
            if(filter==enFilter.PersonID&& !(char.IsDigit(e.KeyChar)||char.IsControl(e.KeyChar)))
            {
 e.Handled = true;

            }
            else
                { e.Handled = false; }  


        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked)
            {
                DoFilter(enFilter.Gendor, Convert.ToInt32(rbMale.Tag));
            }

        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if(rbFemale.Checked)
            {
                DoFilter(enFilter.Gendor,Convert.ToInt32(rbFemale.Tag));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddEditPerson FrmAddEditPerson=new frmAddEditPerson();
            FrmAddEditPerson.OnSave += FrmAddEditPerson_OnSave;

            FrmAddEditPerson.ShowDialog();


        }

        private void FrmAddEditPerson_OnSave()
        {
            RefreshForm();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson FrmAddEditPerson = new frmAddEditPerson();
            FrmAddEditPerson.OnSave += FrmAddEditPerson_OnSave;

            FrmAddEditPerson.ShowDialog();
        }

        private void edintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvPeopleList.CurrentRow.Cells["PersonID"].Value);
            frmAddEditPerson FrmAddEditPerson = new frmAddEditPerson(PersonID);
            FrmAddEditPerson.OnSave += FrmAddEditPerson_OnSave;

            FrmAddEditPerson.ShowDialog();



        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvPeopleList.CurrentRow.Cells["PersonID"].Value);
            if (MessageBox.Show("Are you sure you want to delete Person with ID="+ PersonID,"Confirm Delete",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation)==DialogResult.Yes)
            {
                if(clsPerson.Delete(PersonID))
                {
                    MessageBox.Show("Person with ID=" + PersonID + " deleted successfully.","Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshForm();  
                }
                else
                {
                    MessageBox.Show("Person with ID=" + PersonID + " Can not be Deleted because having data link to it.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }



        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvPeopleList.CurrentRow.Cells["PersonID"].Value);
         frmPersonDetails frm=new frmPersonDetails(PersonID);
            frm.ctrlPersonInfo1.OnSave += FrmAddEditPerson_OnSave;
            frm.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
