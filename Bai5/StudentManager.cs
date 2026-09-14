using System;
using System.Collections;           // Dùng cho hashtable
using System.Collections.Generic;   // Dùng cho list, dictionary
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;              // Dùng cho lớp XDocument

namespace Bai5
{
    public partial class StudentManager : Form
    {
        public string path = "data/student.xml";
        public XDocument doc;
        public int current = 0; // dòng hiện tại của DataGridView
        public int maxRow = 0;  // số dòng của DataGridView = số phần tử của tệp XML
        public string gr = "All";

        public StudentManager()
        {
            InitializeComponent();
        }

        public void initCombo()
        {
            doc = XDocument.Load(path);
            var list = doc.Descendants("student");

            string tmp = "All";
            this.cboClass.Items.Clear();
            this.cboClass.Items.Add("All");
            string group;

            ArrayList myClass = new ArrayList();

            foreach (XElement node in list)
            {
                group = node.Attribute("class").Value;
                if (!tmp.Contains(group))
                {
                    tmp = tmp + "," + group;
                    myClass.Add(group);
                }
            }

            myClass.Sort();
            for (int i = 0; i < myClass.Count; i++)
            {
                this.cboClass.Items.Add(myClass[i]);
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Sự kiện Load form
        private void Form1_Load(object sender, EventArgs e)
        {
            initGrid(gr);
            grid2textbox(0);
        }

        public void grid2textbox(int cur)
        {
            // đưa dữ liệu dòng thứ cur lên textbox
            if (this.dgvStudents.Rows.Count > 0 && cur < this.dgvStudents.Rows.Count)
            {
                this.txtCode.Text = this.dgvStudents.Rows[cur].Cells[0].Value?.ToString() ?? "";
                this.txtName.Text = this.dgvStudents.Rows[cur].Cells[1].Value?.ToString() ?? "";
                this.txtClass.Text = this.dgvStudents.Rows[cur].Cells[2].Value?.ToString() ?? "";
                this.txtAddress.Text = this.dgvStudents.Rows[cur].Cells[3].Value?.ToString() ?? "";
            }
        }

        public void initGrid(string gr)
        {
            this.dgvStudents.ColumnCount = 4;
            this.dgvStudents.Columns[0].Name = "Code";
            this.dgvStudents.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvStudents.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvStudents.Columns[0].Width = 85;

            this.dgvStudents.Columns[1].Name = "Name";
            this.dgvStudents.Columns[1].Width = 180;

            this.dgvStudents.Columns[2].Name = "Class";
            this.dgvStudents.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvStudents.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvStudents.Columns[2].Width = 85;

            this.dgvStudents.Columns[3].Name = "Address";
            this.dgvStudents.Columns[3].Width = 180;

            Library lb = new Library();
            doc = lb.open(path);

            if (doc != null)
            {
                var list = doc.Descendants("student");
                string code, name, group, add = "";

                this.dgvStudents.Rows.Clear();
                foreach (XElement node in list)
                {
                    group = node.Attribute("class").Value;
                    if (gr == group || gr == "All")
                    {
                        code = node.Attribute("code").Value;
                        name = node.Attribute("name").Value;
                        add = node.Attribute("address").Value;
                        this.dgvStudents.Rows.Add(code, name, group, add);
                    }
                }
                maxRow = this.dgvStudents.RowCount > 0 ? this.dgvStudents.RowCount - 1 : 0;
                initCombo();
            }
            else
            {
                MessageBox.Show("Không tìm thấy file student.xml tại đường dẫn: " + path, "Lỗi đọc file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Library lb = new Library();
            string code = this.txtCode.Text;
            XElement node = lb.find(code, path);
            emptyTextBox(true);

            if (node == null)
            {
                MessageBox.Show("Node not found or Student Code is empty");
            }
            else
            {
                this.txtCode.Text = code;
                this.txtName.Text = node.Attribute("name").Value;
                this.txtClass.Text = node.Attribute("class").Value;
                this.txtAddress.Text = node.Attribute("address").Value;
            }
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            this.txtName.Text = "";
            this.txtClass.Text = "";
            this.txtAddress.Text = "";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (this.btnNew.Text == "New")
            {
                this.btnNew.Text = "Save";
                this.btnNew.ForeColor = Color.Red;
                emptyTextBox(true);
                this.txtCode.Focus();
            }
            else
            {
                if (txtCode.Text != "" && txtName.Text != "" && txtClass.Text != "" && txtAddress.Text != "")
                {
                    this.btnNew.Text = "New";
                    this.btnNew.ForeColor = Color.Black;
                    Library lb = new Library();
                    lb.insert(this.txtCode.Text, this.txtName.Text, this.txtClass.Text, this.txtAddress.Text, path);
                    initGrid(gr);
                }
                else
                {
                    MessageBox.Show("Không để trống các trường (fields)!");
                    this.txtCode.Focus();
                }
            }
        }

        public void emptyTextBox(bool ok)
        {
            if (ok)
            {
                this.txtCode.Text = "";
            }
            this.txtName.Text = "";
            this.txtClass.Text = "";
            this.txtAddress.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Library lb = new Library();
            bool ok = false;
            string code = this.txtCode.Text;
            DialogResult result = MessageBox.Show("You are sure to delete this node?",
                               "Important Question",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question);

            if (result == DialogResult.Yes && code.Trim() != "")
            {
                ok = lb.delete(code, path);
            }

            emptyTextBox(true);
            initGrid(gr);
            MessageBox.Show((ok == true ? "Removed!" : "Don't remove or Node not found"));
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (current < maxRow - 1) { current++; }
            else { current = 0; }
            grid2textbox(current);
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (current > 0) { current--; }
            else { current = maxRow > 0 ? maxRow - 1 : 0; }
            grid2textbox(current);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Library lb = new Library();
            if (this.btnUpdate.Text == "Update")
            {
                this.txtName.Focus();
                this.btnUpdate.Text = "Save";
                this.btnUpdate.ForeColor = Color.Red;
                this.txtCode.Enabled = false;
            }
            else if (this.btnUpdate.Text == "Save" &&
                     this.txtName.Text != "" &&
                     this.txtClass.Text != "" &&
                     this.txtAddress.Text != "")
            {
                lb.update(this.txtCode.Text,
                    this.txtName.Text,
                    this.txtClass.Text,
                    this.txtAddress.Text,
                    path);
                this.txtCode.Enabled = true;
                this.btnUpdate.ForeColor = Color.Black;
                initGrid(gr);
                grid2textbox(0);
                this.btnUpdate.Text = "Update";
            }
            else
            {
                MessageBox.Show("Không để trống các field",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.txtName.Focus();
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            SortedDictionary<string, sv> student = new SortedDictionary<string, sv>();
            XDocument xdoc = XDocument.Load(path);
            var list = xdoc.Root.Nodes();

            foreach (XElement el in list)
            {
                sv s = new sv();
                string code = el.Attribute("code").Value;
                string name = el.Attribute("name").Value;
                string group = el.Attribute("class").Value;
                string address = el.Attribute("address").Value;
                s.name = name;
                s.group = group;
                s.address = address;
                student.Add(code, s);
            }

            MessageBox.Show("Sorted Dictionary of Object - Đã sắp xếp thành công!",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            XDocument doc = new XDocument(new XElement("ute"));
            foreach (var p in student)
            {
                XElement node = new XElement("student",
                    new XAttribute("code", p.Key),
                    new XAttribute("name", p.Value.name),
                    new XAttribute("class", p.Value.group),
                    new XAttribute("address", p.Value.address)
                    );
                doc.Root.Add(node);
            }
            doc.Save(path);

            initGrid(gr);
            grid2textbox(0);
        }

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvStudents.CurrentRow != null)
            {
                this.txtCode.Text = this.dgvStudents.CurrentRow.Cells[0].Value?.ToString() ?? "";
                this.txtName.Text = this.dgvStudents.CurrentRow.Cells[1].Value?.ToString() ?? "";
                this.txtClass.Text = this.dgvStudents.CurrentRow.Cells[2].Value?.ToString() ?? "";
                this.txtAddress.Text = this.dgvStudents.CurrentRow.Cells[3].Value?.ToString() ?? "";
            }
        }

        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            gr = this.cboClass.Text;
            initGrid(gr);
            grid2textbox(0);
        }
    }

    // Lớp phụ trợ dùng cho chức năng Sắp xếp (Sort)
    public class sv
    {
        public string name { get; set; }
        public string group { get; set; }
        public string address { get; set; }
    }
}