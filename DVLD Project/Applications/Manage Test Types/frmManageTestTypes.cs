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
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        void RefreshForm()
        {
            dgvTestTypes.DataSource = clsTestTypes.AllTestType();
            lblRecordsCount.Text=dgvTestTypes.Rows.Count.ToString();

        }
        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID=Convert.ToInt32(dgvTestTypes.CurrentRow.Cells["TestTypeID"].Value);
            
            frmEditTestType frm = new frmEditTestType(ID);
            frm.OnSave += Frm_OnSave;
            frm.ShowDialog();
            
        }

        private void Frm_OnSave()
        {
            RefreshForm();
        }
    }
}
