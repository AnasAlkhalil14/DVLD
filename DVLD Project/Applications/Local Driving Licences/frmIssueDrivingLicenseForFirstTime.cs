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

namespace DVLD.Applications.Local_Driving_Licences
{
    public partial class frmIssueDrivingLicenseForFirstTime : Form
    {
        public frmIssueDrivingLicenseForFirstTime(int LDLAppID)
        {
            InitializeComponent();
            ctrlDrivingLicenseApplicationInfo1.LoadDataInfo(LDLAppID);
        }
        clsDriver Driver = new clsDriver();
        clsLicense License = new clsLicense();
        public event Action<int> OnSave;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIssueDrivingLicenseForFirstTime_Load(object sender, EventArgs e)
        {

        }

        bool AddDriver()
        {
           
            Driver.CreatedDate = DateTime.Now;
            Driver.PersonID=ctrlDrivingLicenseApplicationInfo1.PersonID;
            Driver.CreatedByUserID = clsGlobal.User.UserID;
            return Driver.Add();
        }

        bool IssueLicense()
        {
           License.Notes=txtNotes.Text;
            License.ApplicationID = ctrlDrivingLicenseApplicationInfo1.ApplicationID;
            License.LicenseClass = clsLicenseClass.ClassIDByLDLAppID(ctrlDrivingLicenseApplicationInfo1.LDLAppID);
            License.IsActive= true;
            License.IssueReason = 1;
            License.CreatedByUserID= clsGlobal.User.UserID;
            License.IssueDate = DateTime.Now;

            int DriverID = clsDriver.GetDriverIDByPersonID(ctrlDrivingLicenseApplicationInfo1.PersonID);
            if(DriverID==-1)
            {
                if(AddDriver())
                {
                    License.DriverID = Driver.DriverID;
                }
                else
                    return false;

            }
            else
            {
                License.DriverID= DriverID;
            }

            return License.Add();

        }
        private void btnIssue_Click(object sender, EventArgs e)
        {

            if(IssueLicense())
            {
                MessageBox.Show("License issued successfully with id=" + License.LicenseID, "Result", MessageBoxButtons.OK,MessageBoxIcon.Information);
                OnSave?.Invoke(ctrlDrivingLicenseApplicationInfo1.LDLAppID);
                btnIssue.Enabled = false;
                this.Close();
            }
            else
            {
                MessageBox.Show("License issued failed :-(","Result",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
    }
}
