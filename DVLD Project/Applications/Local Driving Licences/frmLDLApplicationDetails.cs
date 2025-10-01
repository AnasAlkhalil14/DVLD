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
    public partial class frmLDLApplicationDetails : Form
    {
        public frmLDLApplicationDetails(int LDLAppID)
        {
            InitializeComponent();
            ctrlDrivingLicenseApplicationInfo1.LoadDataInfo(LDLAppID);
        }
        public event Action OnEditPerson;
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLDLApplicationDetails_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.OnEditPerson += CtrlDrivingLicenseApplicationInfo1_OnEditPerson;   
        }

        private void CtrlDrivingLicenseApplicationInfo1_OnEditPerson()
        {
            OnEditPerson?.Invoke();
        }
    }
}
