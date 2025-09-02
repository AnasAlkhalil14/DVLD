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

namespace DVLD.Applications.Manage_Test_Types
{
    public partial class frmEditTestType : Form
    {
        public frmEditTestType(int ID)
        {
            InitializeComponent();
       Test=clsTestTypes.Find(ID);
        }
        clsTestTypes Test;
        public event Action OnSave;
        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            if(Test!=null)
            {
                lblID.Text=Test.ID.ToString();
                txtDescription.Text=Test.Description;
                txtTitle.Text=Test.Title;
                nudFees.Value=Convert.ToDecimal(Test.Fees);


            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorProvider1.SetError(txtTitle, "Can not be Empty");
                return;
            }
            else
            {
                errorProvider1.SetError(txtTitle, "");
            }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                errorProvider1.SetError(txtDescription, "Can not be Empty");
                return;
            }
            else
            {
                errorProvider1.SetError(txtDescription, "");
            }


            if (MessageBox.Show("Are you sure to update Test type with ID=" + lblID.Text, "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsTestTypes.Update(Convert.ToInt32(lblID.Text), txtTitle.Text,txtDescription.Text, Convert.ToDouble(nudFees.Value)))
                {
                    MessageBox.Show("Data Updated Successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnSave?.Invoke();
                }
                else
                    MessageBox.Show("Updated Failed", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
