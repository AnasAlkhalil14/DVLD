using DVLD.Users;
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
    public partial class ctrlFilterPerson : UserControl
    {
        public ctrlFilterPerson()
        {
            InitializeComponent();
        }
        clsPerson Person;
        public event Action OnFind;
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterValue.Text)) return;

            if (cbFilterBy.SelectedIndex == 0)
            {
                Person = clsPerson.Find(txtFilterValue.Text);
                if (this.FindForm() is frmAddEditUser&& clsUser.IsPersonUser(Person.PersonID))
                {
                    MessageBox.Show("Person With ID=" + txtFilterValue.Text + " is already User", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

             

            }
            else
            {
                if (this.FindForm() is frmAddEditUser &&  clsUser.IsPersonUser(Convert.ToInt32(txtFilterValue.Text)))
                {
                    MessageBox.Show("Person With ID=" + txtFilterValue.Text + " is already User", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                Person = clsPerson.Find(Convert.ToInt32(txtFilterValue.Text));


            }
               
            if(Person == null)
            {
                MessageBox.Show("Person with "+ cbFilterBy.Text+"="+txtFilterValue.Text+ " is not exist.","Result",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                ctrlPersonInfo1.LoadPersonInfo(Person.PersonID);
                OnFind?.Invoke();

            }


        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0) return;
            
            if(!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
            else
            {
                e.Handled= false;
            }



        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {

            frmAddEditPerson frm = new frmAddEditPerson();
            frm.OnAddEditSaveWithPersonID += Frm_OnAddEditSaveWithPersonID;
            frm.OnDeleteImageForShowDetails +=ctrlPersonInfo1.FrmAddEditPerson_OnDeleteImageForShowDetails;
            frm.ShowDialog();
          
         
        
          
        }


       
        private void Frm_OnAddEditSaveWithPersonID(int obj)
        {
            ctrlPersonInfo1.LoadPersonInfo(obj);
            OnFind?.Invoke();
        }

        public int PersonID { get {
                if (Person != null)
                    return Person.PersonID;
                else
                    return -1;
            } }
        private void ctrlFilterPerson_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Clear();
        }
    }
}
