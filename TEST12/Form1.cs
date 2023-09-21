using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEST12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public const string conn = "Data Source=DESKTOP-14VJT84\\SQLEXPRESS;Initial Catalog=AdminAttendance;Integrated Security=True";
        private void button4_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void Load()
        {
            var db = new dbTestDataContext(conn);

            var login = db.tblLogins.ToList();

            dataGridView1.DataSource = login;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var db = new dbTestDataContext(conn);

            var login = new tblLogin()
            {
                username = txtUname.Text,
                password = txtPass.Text
            };
            db.tblLogins.InsertOnSubmit(login);
            db.SubmitChanges();
            Load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var db = new dbTestDataContext(conn);
            var update = (from a in db.tblLogins
                          where a.Id == Convert.ToInt32(txtId.Text)select a ).FirstOrDefault();
            update.username = txtUname.Text;
            update.password = txtPass.Text;
            db.SubmitChanges();
            Load();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var db = new dbTestDataContext(conn);
            var row = e.RowIndex;
            var id = Convert.ToInt32( dataGridView1.Rows[row].Cells[2].Value);
            var login = (from a in db.tblLogins
                         where a.Id == id select new {
                             USERNAME = a.username,
                             PASSWORD = a.password,
                             ID = a.Id
                         }).FirstOrDefault();
            txtUname.Text = login.USERNAME;
            txtPass.Text = login.PASSWORD;
            txtId.Text = login.ID.ToString();
           
            

        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var db = new dbTestDataContext(conn);
            /*var delete = (from a in db.tblLogins
                          where a.Id == Convert.ToInt32(txtId.Text)
                          select a).FirstOrDefault();*/
            var delete = db.tblLogins.Where(a => a.Id == Convert.ToInt32(txtId.Text)).FirstOrDefault();
            db.tblLogins.DeleteOnSubmit(delete);
            db.SubmitChanges();
            Load();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var db = new dbTestDataContext(conn);
            Load();
            Load();

        }
    }
}
