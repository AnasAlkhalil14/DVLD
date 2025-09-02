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

namespace DVLD.Applications
{
    public partial class frmEditApplicationType : Form
    {
        public frmEditApplicationType(int ID)
        {
            InitializeComponent();
           Application=clsApplicationTypes.Find(ID);
        }
        clsApplicationTypes Application;
        public event Action OnSave;

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            if(Application != null)
            {
                lblID.Text=Application.ID.ToString();
                txtTitle.Text=Application.Title;
                numericUpDown1.Value = Convert.ToDecimal(Application.Fees);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorProvider1.SetError(txtTitle, "Can not be Empty");
                return;
            }
            else
            {
                errorProvider1.SetError(txtTitle, "");
            }

            if (MessageBox.Show("Are you sure to update Application type with ID=" + lblID.Text, "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsApplicationTypes.Update(Convert.ToInt32(lblID.Text), txtTitle.Text, Convert.ToDouble(numericUpDown1.Value)))
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
