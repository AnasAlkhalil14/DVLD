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
    public partial class frmManageApplicationsType : Form
    {
        public frmManageApplicationsType()
        {
            InitializeComponent();
        }
        void RefreshForm()
        {
            dgvApplicationTypes.DataSource = clsApplicationTypes.AllAplicationType();
            lblRecordsCount.Text=dgvApplicationTypes.Rows.Count.ToString();
        }
        private void frmManageApplicationsType_Load(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationTypeID = Convert.ToInt32(dgvApplicationTypes.CurrentRow.Cells["ApplicationTypeID"].Value);
            frmEditApplicationType frm = new frmEditApplicationType(ApplicationTypeID);
            frm.OnSave += Frm_OnSave;
            frm.ShowDialog();   
        }

        private void Frm_OnSave()
        {
            RefreshForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
