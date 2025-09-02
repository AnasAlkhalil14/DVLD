using DVLD.Properties;
using DVLD_Business_Tier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class ctrlPersonInfo : UserControl
    {
        public ctrlPersonInfo()
        {
            InitializeComponent();
       
        }
        clsPerson Person;
        void SetDefaultImage()
        {
           
                if (Person.Gendor== "Female")
                    pbPersonImage.Image = Resources.Female_512;
                else pbPersonImage.Image = Resources.Male_512;

        }
        public event Action OnSave;
        void SetImageIconGendor()
        {
            if (Person.Gendor == "Female")
                pbGendor.Image = Resources.Woman_32;
            else pbGendor.Image = Resources.Man_32;

        }
        void FillFormWithPersonInfo()
        {
            lblPersonID.Text=Person.PersonID.ToString();
            lblName.Text = Person.FullName();
            lblGendor.Text = Person.Gendor;
        lblCountry.Text=Person.Nationality;
            if (!string.IsNullOrWhiteSpace(Person.Email))
                lblEmail.Text = Person.Email;
            else
                lblEmail.Text = "N/A";
                lblPhone.Text = Person.Phone;
            lblNationalNo.Text = Person.NationalNo;
            lblDateOfBirth.Text=Person.DateOfBirth.ToShortDateString();
            lblAddress.Text = Person.Address;
            if (!string.IsNullOrWhiteSpace(Person.ImagePath))
            {
                if (File.Exists(Person.ImagePath))
                {
                    pbPersonImage.Image = Image.FromFile(Person.ImagePath);
                }
                else SetDefaultImage(); 


            }
            else
            {
                SetDefaultImage();
            }

            SetImageIconGendor();





        }

        void RefreshForm()
        {
            Person=clsPerson.Find(Person.PersonID);
            FillFormWithPersonInfo();
        }

        private void gbPersonInfo_Enter(object sender, EventArgs e)
        {

        }

        public void LoadPersonInfo(int PersonID)
        {
            Person = clsPerson.Find(PersonID);
            if (Person != null)
                FillFormWithPersonInfo();

        }
    

        private void ctrlPersonInfo_Load(object sender, EventArgs e)
        {
        
        }

        private void lnkEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson FrmAddEditPerson=new frmAddEditPerson(Person.PersonID);
            FrmAddEditPerson.OnSave += FrmAddEditPerson_OnSave;
            FrmAddEditPerson.OnDeleteImageForShowDetails += FrmAddEditPerson_OnDeleteImageForShowDetails;
            FrmAddEditPerson.ShowDialog();
        }

        public void FrmAddEditPerson_OnDeleteImageForShowDetails()
        {
           pbPersonImage.Image.Dispose();
        }

        private void FrmAddEditPerson_OnSave()
        {

           RefreshForm();
            OnSave?.Invoke();

        }
    }
}
