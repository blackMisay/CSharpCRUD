using System;
using System.Windows.Forms;

namespace SampleProgram
{
    public partial class frmMain : Form
    {
        private int cellValue;
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmEmployeeForm employeeForm = new frmEmployeeForm();
            employeeForm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee() { Id = this.cellValue };
            DataConnection db = new DataConnection();
            if (db.delete(employee))
            {
                MessageBox.Show("Deleted Successfully!");
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            load();
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            load();
        }

        private void load()
        {
            DataConnection db = new DataConnection();
            dgvList.DataSource = db.load("employee");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmEmployeeForm employeeForm = new frmEmployeeForm(cellValue);
            employeeForm.ShowDialog();
        }

        private void dgvList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvList.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgvList.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvList.Rows[selectedrowindex];
                this.cellValue = Convert.ToInt32(selectedRow.Cells["Id"].Value);
            }
        }
    }
}
