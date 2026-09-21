using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bai6
{
    public partial class Form1 : Form
    {
        // Điều chỉnh lại đường dẫn trỏ vào thư mục data nếu bạn để file xml trong bin/debug/data
        String pathMaster = "data/master.xml";
        String pathDetail = "data/detail.xml";
        String pathItem = "data/item.xml";
        String pathCustomer = "data/customer.xml";

        XDocument xMaster, xDetail, xItem, xCustomer;

        String today = DateTime.Now.ToString("d/M/yyyy");

        public Form1()
        {
            InitializeComponent();
            try
            {
                InitCboOrderNo(); // Đọc file master.xml
                InitCboCCode();   // Đọc file customer.xml
                lblDate.Text = today; // Cập nhật ngày hiện tại nằm ở đây!
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message);
            }
        }

        void InitCboOrderNo()
        {
            if (!System.IO.File.Exists(pathMaster)) return;
            xMaster = XDocument.Load(pathMaster);
            var qr = (from XElement e in xMaster.Descendants("order")
                      orderby e.Attribute("orderno").Value descending
                      select e.Attribute("orderno").Value).Distinct();
            foreach (String str in qr)
            {
                cboOrder.Items.Add(str);
            }
        }

        void InitCboCCode()
        {
            if (!System.IO.File.Exists(pathCustomer)) return;
            xCustomer = XDocument.Load(pathCustomer);
            var qr = (from XElement e in xCustomer.Descendants("customer")
                      orderby e.Attribute("ccode").Value
                      select e.Attribute("ccode").Value).Distinct();
            foreach (String str in qr)
            {
                cboCCode.Items.Add(str);
            }
        }

        void InitGrid(String header, String width)
        {
            /* Header: các tên cột, Width: các độ rộng cột tương ứng */
            String[] hd = header.Split(',');
            String[] wd = width.Split(',');
            for (int i = 0; i < dgvData.ColumnCount && i < hd.Length; i++)
            {
                dgvData.Columns[i].HeaderText = hd[i].Trim();
                dgvData.Columns[i].Width = int.Parse(wd[i].Trim());
            }
        }

        void listCustomer()
        {
            /* -- Hiển thị danh sách khách hàng vào grid --*/
            if (!System.IO.File.Exists(pathCustomer)) return;
            xCustomer = XDocument.Load(pathCustomer);
            var qr = from XElement e in xCustomer.Descendants("customer")
                     select new
                     {
                         code = e.Attribute("ccode").Value,
                         cname = e.Attribute("cname").Value,
                         add = e.Attribute("address").Value,
                     };

            lblTotal.Text = qr.Count().ToString() + " khách hàng";
            dgvData.DataSource = qr.ToList();

            String hd = "Ccode, Cname, Address";
            String wd = "100, 215, 300";
            InitGrid(hd, wd);
        }

        private void listCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* -- Khi click lên item List Customer trên menu -- */
            listCustomer();
        }

        void listItem()
        {
            /* -- Hiển thị danh sách hàng hóa lên grid --- */
            if (!System.IO.File.Exists(pathItem)) return;
            xItem = XDocument.Load(pathItem);
            var qr = from XElement e in xItem.Descendants("item")
                     orderby e.Attribute("icode").Value
                     select new
                     {
                         code = e.Attribute("icode").Value,
                         name = e.Attribute("iname").Value,
                         price = e.Attribute("rate").Value
                     };
            dgvData.DataSource = qr.ToList();

            String hd = "Icode, Iname, Price";
            String wd = "100, 305, 210";
            InitGrid(hd, wd);
            lblTotal.Text = qr.Count().ToString() + " mặt hàng";
        }

        void agreItemOrdered(String ccode)
        {
            /* -- Tổng hợp hàng đã đặt mua bởi khách có mã ccode --*/
            if (!System.IO.File.Exists(pathDetail) || !System.IO.File.Exists(pathItem) || !System.IO.File.Exists(pathMaster)) return;

            xDetail = XDocument.Load(pathDetail);
            xItem = XDocument.Load(pathItem);
            xMaster = XDocument.Load(pathMaster);

            var qr1 = from em in xMaster.Descendants("order")
                      join ed in xDetail.Descendants("item")
                      on em.Attribute("orderno").Value equals ed.Attribute("orderno").Value
                      join ei in xItem.Descendants("item")
                      on ed.Attribute("icode").Value equals ei.Attribute("icode").Value
                      where em.Attribute("ccode").Value == ccode
                      group int.Parse(ed.Attribute("qty").Value) * int.Parse(ed.Attribute("price").Value)
                      by new
                      {
                          icode = ed.Attribute("icode").Value,
                          iname = ei.Attribute("iname").Value
                      }
                      into g
                      select new
                      {
                          icode = g.Key.icode,
                          iname = g.Key.iname,
                          amount = g.Sum(),
                          note = ""
                      };

            var qr2 = from XElement ed in xDetail.Descendants("item")
                      group int.Parse(ed.Attribute("qty").Value)
                      by ed.Attribute("icode").Value into g2
                      select new
                      {
                          icode = g2.Key,
                          qty = g2.Sum()
                      };

            var qr = from e1 in qr1
                     join e2 in qr2 on e1.icode equals e2.icode
                     select new
                     {
                         icode = e1.icode,
                         iname = e1.iname,
                         Qty = e2.qty,
                         Amount = e1.amount
                     };

            dgvData.DataSource = qr.ToList();
            InitGrid("Icode, Iname, Qty, Amount", "100, 215, 100, 200");

            int total = 0;
            foreach (var obj in qr1)
            {
                total += obj.amount;
            }
            lblTotal.Text = total.ToString();
            lblCustomer.Text = isCustomer(cboCCode.Text);
        }

        private void listItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listItem();
        }

        void viewOrderDetail(String orderno)
        {
            /* -- Hiển thị chi tiết đơn hàng có số orderno -- */
            if (!System.IO.File.Exists(pathCustomer) || !System.IO.File.Exists(pathMaster) || !System.IO.File.Exists(pathDetail) || !System.IO.File.Exists(pathItem)) return;

            xCustomer = XDocument.Load(pathCustomer);
            xMaster = XDocument.Load(pathMaster);
            xDetail = XDocument.Load(pathDetail);
            xItem = XDocument.Load(pathItem);

            // Xác định họ tên khách hàng
            var qr1 = from XElement ec in xCustomer.Descendants("customer")
                      join XElement em in xMaster.Descendants("order")
                      on ec.Attribute("ccode").Value equals em.Attribute("ccode").Value
                      where em.Attribute("orderno").Value == orderno
                      select new
                      {
                          cname = ec.Attribute("cname").Value
                      };

            if (qr1.Count() > 0)
                lblCustomer.Text = qr1.First().cname;

            // Hiển thị chi tiết đơn hàng orderno
            var qr2 = from XElement ed in xDetail.Descendants("item")
                      join XElement ei in xItem.Descendants("item")
                      on ed.Attribute("icode").Value equals ei.Attribute("icode").Value
                      where ed.Attribute("orderno").Value == orderno
                      orderby ed.Attribute("icode").Value
                      select new
                      {
                          icode = ed.Attribute("icode").Value,
                          iname = ei.Attribute("iname").Value,
                          qty = ed.Attribute("qty").Value,
                          price = ed.Attribute("price").Value,
                          amount = (int.Parse(ed.Attribute("qty").Value) * int.Parse(ed.Attribute("price").Value)).ToString()
                      };

            dgvData.DataSource = qr2.ToList();
            String hd = "Icode, Iname, Qty, Price, Amount";
            String wd = "90, 240, 90, 90, 105";
            InitGrid(hd, wd);

            // Tính tổng giá trị đơn hàng
            int total = 0;
            foreach (var obj in qr2)
            {
                total += int.Parse(obj.amount);
            }
            lblTotal.Text = total.ToString() + " triệu đồng";
        }

        private void cboOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOrder.SelectedItem != null)
            {
                viewOrderDetail(cboOrder.Text);
            }
        }

        void viewCcodeItem(String ccode)
        {
            MessageBox.Show(ccode);
        }

        private void cboCCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCCode.SelectedItem != null)
            {
                agreItemOrdered(cboCCode.Text);
            }
        }

        String isCustomer(String ccode)
        {
            String cname = "";
            if (!System.IO.File.Exists(pathCustomer)) return cname;
            xCustomer = XDocument.Load(pathCustomer);
            var qr = from XElement e in xCustomer.Descendants("customer")
                     where e.Attribute("ccode").Value == ccode
                     select new
                     {
                         cname = e.Attribute("cname").Value
                     };
            if (qr.Count() > 0) cname = qr.First().cname;
            return cname;
        }
    }
}