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

namespace DVLD.Licenses
{
    public partial class ctrlLDLicenseWithFilter : UserControl
    {
        public ctrlLDLicenseWithFilter()
        {
            InitializeComponent();
        }

        public event Action<int> OnFind;

        bool ChickForLicenseFind()
        {
            if(string.IsNullOrWhiteSpace(txtLicenseID.Text.Trim()))
            {
                MessageBox.Show("You Should Write a valid License ID Frist.","Wrong input",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }

            int LicenseID = Convert.ToInt32(txtLicenseID.Text.Trim());
            if (clsLicense.IsLicenseExist(LicenseID))
            {

                ctrlLicenseInfo1.LoadData(LicenseID);
                OnFind?.Invoke(LicenseID);
                return true;
            }
            else
            {
                MessageBox.Show("License with id="+txtLicenseID.Text+" is not found.","Wrong Input",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


        }



        private void button1_Click(object sender, EventArgs e)
        {
            ChickForLicenseFind();

        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

        }

        public int DriverID { get { return ctrlLicenseInfo1.DriverID; } }
        public bool IsDetain { get { return ctrlLicenseInfo1.IsDetain; } }

        public bool IsActive { get { return ctrlLicenseInfo1.IsActive; } }
        public DateTime ExpirationDate { get { return ctrlLicenseInfo1.ExpirationDate; } }
        public DateTime IssueDate { get { return ctrlLicenseInfo1.IssueDate; } }
        public string Notes { get { return ctrlLicenseInfo1.Notes; } }
        private void ctrlLDLicenseWithFilter_Load(object sender, EventArgs e)
        {

        }
    }
}
