using DVLD.Applications;
using DVLD.Applications.Local_Driving_Licences;
using DVLD.Applications.Manage_Test_Types;
using DVLD.Log_in;
using DVLD.People;
using DVLD.Properties;
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

namespace DVLD
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmPeopleList frm=new frmPeopleList();  
           
            
            frm.ShowDialog();
            
             
            
        }

        private void frmMain_Load_1(object sender, EventArgs e)
        {
             
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmLoginScreen frm=new frmLoginScreen( );
            
            frm.Show();
            this.Close();

        }

        private void peopleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmPeopleList frm= new frmPeopleList();
            frm.ShowDialog();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frmUsersList frm= new frmUsersList();
            frm.ShowDialog();

        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            frmUserDetails frm=new frmUserDetails(clsGlobal.User.UserID);
            frm.ShowDialog();
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            frmChangePassword frm=new frmChangePassword(clsGlobal.User.UserID);
            frm.OnSave += Frm_OnSave; ;
            frm.ShowDialog();
        }

        private void Frm_OnSave(string obj)
        {
           clsGlobal.User.Password= obj;
            if(Properties.Settings.Default.RememberMe)
            {

                Properties.Settings.Default.Password = obj;
                Properties.Settings.Default.Save();
            }

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationsType frm=new frmManageApplicationsType();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frm= new frmManageTestTypes();   
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalDriving_Application frm=new frmNewLocalDriving_Application();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenceApplicationList frm=new frmLocalDrivingLicenceApplicationList();
            frm.ShowDialog();
        }

     

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }
    }
}
