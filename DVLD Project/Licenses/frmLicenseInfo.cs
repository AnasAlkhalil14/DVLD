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
    public partial class frmLicenseInfo : Form
    {
        public frmLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            ctrlLicenseInfo1.LoadData(LicenseID);
        }

        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
