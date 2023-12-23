using System;
using System.Windows.Forms;

namespace SampleProgram
{
    public partial class frmEmployeeForm : Form
    {

        private int cellValue = 0;//5
        public frmEmployeeForm()
        {
            InitializeComponent();
        }

        public frmEmployeeForm(int cellValue)//5
        {
            InitializeComponent();
            DataConnection db = new DataConnection();
            Employee employee = new Employee();
            employee = db.loadEmployee(cellValue);//5

            this.txtFullName.Text = employee.FullName;
            this.cmbGender.Text = employee.Gender;
            this.cmbStatus.Text = employee.Status;
            this.txtAddress.Text = employee.Address;
            this.cellValue = cellValue;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.FullName = txtFullName.Text;
            employee.Status = cmbStatus.Text;
            employee.Gender = cmbGender.Text;
            employee.Address = txtAddress.Text;
            employee.Id = this.cellValue;


            if (this.cellValue.Equals(0))
            {
                DataConnection db = new DataConnection();
                if (db.save(employee))
                {
                    MessageBox.Show("Save Successfully");
                    this.Close();
                }
            }
            else
            {
                DataConnection db = new DataConnection();
                if (db.update(employee))//true
                {
                    MessageBox.Show("Updated Successfully");
                    this.Close();
                }
            }

        }
    }
}
