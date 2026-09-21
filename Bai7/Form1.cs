using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics; // Thư viện dùng để mở trình duyệt web

namespace Bai7
{
    public partial class Form1 : Form
    {
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
                InitCboOrderNo();
                InitCboCCode();
                lblDate.Text = today; // Cập nhật ngày hiện tại lên nhãn
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message);
            }
        }

        // ================= CÁC HÀM KHỞI TẠO =================
        void InitCboOrderNo()
        {
            if (!System.IO.File.Exists(pathMaster)) return;
            xMaster = XDocument.Load(pathMaster);
            var qr = (from XElement e in xMaster.Descendants("order")
                      orderby e.Attribute("orderno").Value descending
                      select e.Attribute("orderno").Value).Distinct();
            foreach (String str in qr) cboOrder.Items.Add(str);
        }

        void InitCboCCode()
        {
            if (!System.IO.File.Exists(pathCustomer)) return;
            xCustomer = XDocument.Load(pathCustomer);
            var qr = (from XElement e in xCustomer.Descendants("customer")
                      orderby e.Attribute("ccode").Value
                      select e.Attribute("ccode").Value).Distinct();
            foreach (String str in qr) cboCCode.Items.Add(str);
        }

        void InitGrid(String header, String width)
        {
            String[] hd = header.Split(',');
            String[] wd = width.Split(',');
            for (int i = 0; i < dgvData.ColumnCount && i < hd.Length; i++)
            {
                dgvData.Columns[i].HeaderText = hd[i].Trim();
                dgvData.Columns[i].Width = int.Parse(wd[i].Trim());
            }
        }

        // ================= XỬ LÝ KHÁCH HÀNG =================
        void listCustomer()
        {
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
            InitGrid("Mã KH, Tên khách hàng, Địa chỉ", "100, 215, 340");
        }

        private void listCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listCustomer();
        }

        // ================= XỬ LÝ MẶT HÀNG =================
        void listItem()
        {
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
            InitGrid("Mã hàng, Tên mặt hàng, Đơn giá", "100, 350, 205");
            lblTotal.Text = qr.Count().ToString() + " mặt hàng";
        }

        private void listItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listItem();
        }

        // ================= XỬ LÝ ĐƠN HÀNG CHI TIẾT =================
        void agreItemOrdered(String ccode)
        {
            if (!System.IO.File.Exists(pathDetail) || !System.IO.File.Exists(pathItem) || !System.IO.File.Exists(pathMaster)) return;

            xDetail = XDocument.Load(pathDetail);
            xItem = XDocument.Load(pathItem);
            xMaster = XDocument.Load(pathMaster);

            var qr1 = from em in xMaster.Descendants("order")
                      join ed in xDetail.Descendants("item") on em.Attribute("orderno").Value equals ed.Attribute("orderno").Value
                      join ei in xItem.Descendants("item") on ed.Attribute("icode").Value equals ei.Attribute("icode").Value
                      where em.Attribute("ccode").Value == ccode
                      group int.Parse(ed.Attribute("qty").Value) * int.Parse(ed.Attribute("price").Value)
                      by new { icode = ed.Attribute("icode").Value, iname = ei.Attribute("iname").Value } into g
                      select new { icode = g.Key.icode, iname = g.Key.iname, amount = g.Sum(), note = "" };

            var qr2 = from XElement ed in xDetail.Descendants("item")
                      group int.Parse(ed.Attribute("qty").Value) by ed.Attribute("icode").Value into g2
                      select new { icode = g2.Key, qty = g2.Sum() };

            var qr = from e1 in qr1
                     join e2 in qr2 on e1.icode equals e2.icode
                     select new { icode = e1.icode, iname = e1.iname, Qty = e2.qty, Amount = e1.amount };

            dgvData.DataSource = qr.ToList();
            InitGrid("Mã hàng, Tên hàng, Số lượng, Thành tiền", "100, 235, 100, 220");

            int total = 0;
            foreach (var obj in qr1) total += obj.amount;
            lblTotal.Text = total.ToString() + " VNĐ";
            lblCustomer.Text = isCustomer(cboCCode.Text);
        }

        void viewOrderDetail(String orderno)
        {
            if (!System.IO.File.Exists(pathCustomer) || !System.IO.File.Exists(pathMaster) || !System.IO.File.Exists(pathDetail) || !System.IO.File.Exists(pathItem)) return;

            xCustomer = XDocument.Load(pathCustomer);
            xMaster = XDocument.Load(pathMaster);
            xDetail = XDocument.Load(pathDetail);
            xItem = XDocument.Load(pathItem);

            var qr1 = from XElement ec in xCustomer.Descendants("customer")
                      join XElement em in xMaster.Descendants("order") on ec.Attribute("ccode").Value equals em.Attribute("ccode").Value
                      where em.Attribute("orderno").Value == orderno
                      select new { cname = ec.Attribute("cname").Value };

            if (qr1.Count() > 0) lblCustomer.Text = qr1.First().cname;

            var qr2 = from XElement ed in xDetail.Descendants("item")
                      join XElement ei in xItem.Descendants("item") on ed.Attribute("icode").Value equals ei.Attribute("icode").Value
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
            InitGrid("Mã hàng, Tên hàng, Số lượng, Đơn giá, Thành tiền", "90, 215, 80, 110, 160");

            int total = 0;
            foreach (var obj in qr2) total += int.Parse(obj.amount);
            lblTotal.Text = total.ToString() + " VNĐ";
        }

        private void cboOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOrder.SelectedItem != null) viewOrderDetail(cboOrder.Text);
        }

        private void cboCCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCCode.SelectedItem != null) agreItemOrdered(cboCCode.Text);
        }

        String isCustomer(String ccode)
        {
            if (!System.IO.File.Exists(pathCustomer)) return "";
            xCustomer = XDocument.Load(pathCustomer);
            var qr = from XElement e in xCustomer.Descendants("customer")
                     where e.Attribute("ccode").Value == ccode
                     select new { cname = e.Attribute("cname").Value };
            return qr.Count() > 0 ? qr.First().cname : "";
        }

        // ================= BÀI TẬP 7: CHUYỂN ĐỔI XML SANG HTML =================

        // A. Chuyển đổi Mặt hàng (Items)
        void chuyendoiItem()
        {
            string pathHTML = "item.html";
            if (!System.IO.File.Exists(pathItem)) return;

            xItem = XDocument.Load(pathItem);
            var xI = xItem.Descendants("item");

            var html = new XElement("html",
                new XElement("head",
                    new XElement("style",
                        "body { font-family: 'Times New Roman'; padding: 20px; }" +
                        "h2 { text-align: center; color: #b30000; text-transform: uppercase; font-size: 28px; margin-bottom: 25px; }" + // Căn giữa, làm nổi bật tiêu đề
                        "table { border-collapse: collapse; width: 60%; margin: 0 auto; }" + // Thêm margin: 0 auto để căn giữa bảng
                        "th, td { border: solid 1px silver; padding: 8px; text-align: left; }" +
                        "th { background-color: #ffcccc; color: maroon; }" +
                        "tr:nth-child(even) { background-color: #f9f9f9; }"
                    )
                ),
                new XElement("body",
                    new XElement("h2", "Danh sách Mặt hàng"),
                    new XElement("table",
                        new XElement("tr",
                            new XElement("th", "Icode"),
                            new XElement("th", "IName"),
                            new XElement("th", "Rate")
                        ),
                        from el in xI
                        select new XElement("tr",
                            new XElement("td", el.Attribute("icode")?.Value),
                            new XElement("td", el.Attribute("iname")?.Value),
                            new XElement("td", new XAttribute("style", "text-align:right; color: red;"), el.Attribute("rate")?.Value)
                        )
                    )
                )
            );

            html.Save(pathHTML);
            Process.Start(pathHTML);
        }

        private void exportItemHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chuyendoiItem();
        }

        // B. Chuyển đổi Khách hàng (Customers)
        void chuyendoiCustomer()
        {
            string pathHTML = "customer.html";
            if (!System.IO.File.Exists(pathCustomer)) return;

            xCustomer = XDocument.Load(pathCustomer);
            var xC = xCustomer.Descendants("customer");

            var html = new XElement("html",
                new XElement("head",
                    new XElement("style",
                        "body { font-family: 'Times New Roman'; padding: 20px; }" +
                        "h2 { text-align: center; color: #003366; text-transform: uppercase; font-size: 28px; margin-bottom: 25px; }" + // Căn giữa, làm nổi bật tiêu đề
                        "table { border-collapse: collapse; width: 80%; margin: 0 auto; }" + // Thêm margin: 0 auto để căn giữa bảng
                        "th, td { border: solid 1px silver; padding: 10px; text-align: left; }" +
                        "th { background-color: #cceeff; color: navy; }"
                    )
                ),
                new XElement("body",
                    new XElement("h2", "Danh sách Khách hàng"),
                    new XElement("table",
                        new XElement("tr",
                            new XElement("th", "Mã KH"),
                            new XElement("th", "Tên Khách Hàng"),
                            new XElement("th", "Địa Chỉ")
                        ),
                        from el in xC
                        select new XElement("tr",
                            new XElement("td", el.Attribute("ccode")?.Value),
                            new XElement("td", el.Attribute("cname")?.Value),
                            new XElement("td", el.Attribute("address")?.Value)
                        )
                    )
                )
            );

            html.Save(pathHTML);
            Process.Start(pathHTML);
        }

        private void exportCustomerHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chuyendoiCustomer();
        }
    }
    }