using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.International_Driving_Licenses
{
    public partial class frmIntDLApplicationDetails : Form
    {
        public frmIntDLApplicationDetails(int IntLicenseID)
        {
            InitializeComponent();
            ctrlInternationalLicenseInfo1.LoadData(IntLicenseID);   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIntDLApplicationDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
