using DVLD.Properties;
using DVLD_Business_Tier;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLD.People
{
    public partial class frmAddEditPerson : Form
    {
        public frmAddEditPerson()
        {
            InitializeComponent();
            Mode = enMode.eAdd;
            lblAddEditPeson.Text = "Add New Person";
            Person=new clsPerson(); 
        }
        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();
            Mode = enMode.eUpdate;
            lblAddEditPeson.Text = "Update Person with ID="+PersonID;
            Person=clsPerson.Find(PersonID);
            FillFormWithPersonInfo();
        }

        public event Action OnSave;
        public event Action OnDeleteImageForShowDetails;
        public event Action<int> OnAddEditSaveWithPersonID;
        void FillFormWithPersonInfo()
        {
       
            lblPersonID.Text=Person.PersonID.ToString();    
            txtFirstName.Text=Person.FirstName;
            txtFirstName.Tag = 1;
            txtSecondName.Text=Person.SecondName; txtSecondName.Tag = 1;
            txtThirdName.Text=Person.ThirdName; txtThirdName.Tag = 1;
            txtLastName.Text=Person.LastName; txtLastName.Tag = 1;
            txtEmail.Text=Person.Email; txtEmail.Tag = 1;
            txtPhone.Text=Person.Phone; txtPhone.Tag = 1;
            txtAddress.Text=Person.Address; txtAddress.Tag = 1;
            cbCountries.Text = Person.Nationality;  
            if (Person.Gendor=="Male")
            {
                rbMale.Checked = true;
                if (string.IsNullOrWhiteSpace(Person.ImagePath))
                {
                  
                    pbPersonImage.Image = Resources.Male_512;
                    IsUserSetImage = false;
                   lnkRemove.Visible = false;
                }
                else
                {
                    if (File.Exists(Person.ImagePath))
                    {
                       
                        pbPersonImage.Image = Image.FromFile(Person.ImagePath);
                        IsUserSetImage = true;

                        lnkRemove.Visible = true;
                    }
                    else
                    {
                       
                        Person.ImagePath = "";
                        lnkRemove.Visible = false;
                        IsUserSetImage = false;
                        SetDefaultImage();

                    }
                }

            }
            else
            {
                rbFemale.Checked = true;
                if (string.IsNullOrWhiteSpace(Person.ImagePath))
                {
                   
                    lnkRemove.Visible = false;
                    pbPersonImage.Image = Resources.Female_512;
                    IsUserSetImage = false;
                }
                else
                {
                    if (File.Exists(Person.ImagePath))
                    {
                       
                        pbPersonImage.Image = Image.FromFile(Person.ImagePath);
                        IsUserSetImage = true;
                        lnkRemove.Visible = true;
                    }
                    else
                    {
                       
                        Person.ImagePath = "";
                        lnkRemove.Visible = false;
                        IsUserSetImage = false;
                        SetDefaultImage();

                    }


                }







            }

            dtpDateOfBirth.Value = Person.DateOfBirth;
            txtNationalNo.Text = Person.NationalNo; txtNationalNo.Tag = 1;
            pbPersonImage.Tag = 1;

        }


        clsPerson Person;
        bool isFail=false;
        enum enMode { eAdd,eUpdate}
        enMode Mode;
        bool IsUserSetImage=false;
       
        void SetDefaultImage()
        {
            if(!IsUserSetImage)
            {
                pbPersonImage.ImageLocation = "";
                if (rbFemale.Checked)
                    pbPersonImage.Image = Resources.Female_512;
                else pbPersonImage.Image = Resources.Male_512;


            }

        }


        private void CheckName(object sender, CancelEventArgs e)
        {
            Guna2TextBox txt =(Guna2TextBox)sender;
            if(string.IsNullOrWhiteSpace(txt.Text)||txt.Text.Length<2)
            {
                errorProvider1.SetError(txt, "Invalid Field Value");
                txt.Tag = 0;
            }
            else
            {
                errorProvider1.SetError(txt, "");
                txt.Tag = 1;
            }



        }

        void AfewFormSetting()
        {
            cbCountries.DataSource = clsPerson.AllCounties();
            cbCountries.DisplayMember = "CountryName";
            cbCountries.ValueMember = "CountryID";
            dtpDateOfBirth.MinDate = DateTime.Today.AddYears(-100);
            dtpDateOfBirth.MaxDate = DateTime.Today.AddYears(-18);
            if (Mode == enMode.eAdd)
            {
                cbCountries.SelectedValue = "169";
                dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            }

        }
        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {

            AfewFormSetting();




        }

        private void lblAddEditPeson_Click(object sender, EventArgs e)
        {

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtNationalNo.Text))
            {
                errorProvider1.SetError(txtNationalNo, "this field is required"); 
                txtNationalNo.Tag = 0;
                return; 
            }



          if( txtNationalNo.Text!=Person.NationalNo)
            {
                if (clsPerson.IsThisNationalNoExist(txtNationalNo.Text))
                {

                    errorProvider1.SetError(txtNationalNo, "this NationalNo is used for another person,the NationalNo must be unique change it ");
                    txtNationalNo.Tag = 0;

                }
                else
                {
                    txtNationalNo.Tag = 1;
                    errorProvider1.SetError(txtNationalNo, "");

                }


            }
           
            else
            {
                txtNationalNo.Tag = 1;
                errorProvider1.SetError(txtNationalNo, "");
               
            }



        }

        private void lnklblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Title = "Select Your Image";
            openFileDialog1.Filter = "Images|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbPersonImage.Tag = 1;
                if (Mode == enMode.eUpdate && !string.IsNullOrWhiteSpace(Person.ImagePath))
                {
                    pbPersonImage.Image.Dispose();
                }


                    IsUserSetImage = true;
                pbPersonImage.Image=Image.FromFile(openFileDialog1.FileName);
                pbPersonImage.ImageLocation = openFileDialog1.FileName;
               lnkRemove.Visible = true;
            }




        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if(rbFemale.Checked&&!IsUserSetImage)
            {
                pbPersonImage.Image = Resources.Female_512;
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked && !IsUserSetImage)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
        }

        private void txtNationalNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
           
        
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                string Patern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
                if(Regex.IsMatch(txtEmail.Text,Patern))
                {
                    txtEmail.Tag = 1;
                    errorProvider1.SetError(txtEmail, "");
                }
                else

                {
                    txtEmail.Tag=0;
                    errorProvider1.SetError(txtEmail, "Invalid Email Address");
                }



            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
                txtEmail.Tag = 1;
            }



        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
                { e.Handled = false; }

        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtPhone.Text)|| txtPhone.Text.Length<7)
            {
                errorProvider1.SetError(txtPhone, "Invalid Phone Number");
                txtPhone.Tag = 0;
            }
            else
            {
                errorProvider1.SetError(txtPhone, "");
                txtPhone.Tag = 1;
            }

        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text) || txtAddress.Text.Length < 2)
            {
                errorProvider1.SetError(txtAddress, "Invalid Address");
                txtAddress.Tag = 0;
            }
            else
            {
                errorProvider1.SetError(txtAddress, "");
                txtAddress.Tag = 1;
            }





        }

        private void lnkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.Image.Dispose();
            lnkRemove.Visible = false;
            IsUserSetImage = false;
         pbPersonImage.Tag = 0;   
            SetDefaultImage();
        }


        bool CheckRightData()
        {

            int Result = Convert.ToInt16(txtFirstName.Tag) + Convert.ToInt16(txtSecondName.Tag) +
                Convert.ToInt16(txtThirdName.Tag) + Convert.ToInt16(txtLastName.Tag) + Convert.ToInt16(txtPhone.Tag) +
                Convert.ToInt16(txtEmail.Tag) + Convert.ToInt16(txtAddress.Tag) + Convert.ToInt16(txtNationalNo.Tag);
            return Result == 8;

        }
        
       string CopyImageToTheSharedFolder()
        {
          
            if (Mode==enMode.eUpdate && string.IsNullOrWhiteSpace(pbPersonImage.ImageLocation))
            {
              if(Convert.ToInt32(pbPersonImage.Tag)!=0)
                return Person.ImagePath;

            }

           if(Mode==enMode.eUpdate&& Person.ImagePath==pbPersonImage.ImageLocation)
            {
                return Person.ImagePath; ;
            }


            if(Mode==enMode.eUpdate&& !string.IsNullOrWhiteSpace(Person.ImagePath))
            {
                if(File.Exists(Person.ImagePath))
                {
                    OnDeleteImageForShowDetails?.Invoke();
             
                    File.Delete(Person.ImagePath);
                  
                   
                }


            }

            

            if (IsUserSetImage)
            {

                string SourcePath;
                string SharedFolderPath = @"C:\DVLD-People-Images";


                SourcePath = pbPersonImage.ImageLocation;

                    string FileName = Guid.NewGuid().ToString("N") + Path.GetExtension(SourcePath);

                    string DestinationPath = Path.Combine(SharedFolderPath, FileName);
                    File.Copy(SourcePath, DestinationPath, true);
                    return DestinationPath;

            }
               

            
            return "";


        }
        void FillPersonObjectToSave()
        {
            Person.FirstName = txtFirstName.Text;
            Person.LastName = txtLastName.Text;
            Person.Phone = txtPhone.Text;
            Person.Email = txtEmail.Text;
            Person.Address = txtAddress.Text;
            Person.SecondName = txtSecondName.Text;
            Person.ThirdName = txtThirdName.Text;
            if(rbFemale.Checked)
            {
                Person.Gendor = "Female";
            }
            else
            {
                Person.Gendor = "Male";
            }
            Person.NationalNo = txtNationalNo.Text;
            Person.DateOfBirth = dtpDateOfBirth.Value;
            Person.Nationality = cbCountries.Text;
            Person.ImagePath = CopyImageToTheSharedFolder();
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
           if(CheckRightData())
            {
                FillPersonObjectToSave();
                if(Person.Save())
                {
                    if (Mode == enMode.eAdd)
                    {
                        MessageBox.Show("Person Added Successfully with id=" + Person.PersonID, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Mode=enMode.eUpdate;
                        lblAddEditPeson.Text = "Update Person with ID=" + Person.PersonID;
                        pbPersonImage.ImageLocation = Person.ImagePath;
                        
                    }
                    else
                        MessageBox.Show("Person Updated Successfully with id=" + Person.PersonID, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lblPersonID.Text = Person.PersonID.ToString();

                    OnSave?.Invoke();
                    OnAddEditSaveWithPersonID?.Invoke(Person.PersonID);


                }
                else
                {
                    MessageBox.Show("Something wrong happen", "Result",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }


            }
           else
            {
                MessageBox.Show("Save Failed,fill the person with right data", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
         
               

        }
    }
}
