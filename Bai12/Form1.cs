using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.IO;

namespace Bai12
{
    public partial class Form1 : Form
    {
        // Chuỗi kết nối SQL Server của bạn
        string connString = "Server=localhost;Database=QuanLyPhongLab;User Id=sa;Password=25112006;Encrypt=True;TrustServerCertificate=True;";

        // Biến toàn cục để lưu dữ liệu tải từ SQL
        DataTable dtData;

        public Form1()
        {
            InitializeComponent();
        }

        // NÚT 1: TẢI DỮ LIỆU TỪ SQL LÊN DATAGRIDVIEW
        private void btnTaiDuLieu_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    // Lấy dữ liệu phòng lab, thiết bị và tự tính luôn cột Thành Tiền
                    string query = @"
                        SELECT p.MaPhong, p.TenPhong, p.QuanLy, 
                               t.MaTB, t.TenTB, t.SoLuong, t.DonGia,
                               (t.SoLuong * t.DonGia) AS ThanhTien
                        FROM PhongLab p
                        LEFT JOIN ThietBi t ON p.MaPhong = t.MaPhong";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        dtData = new DataTable();
                        da.Fill(dtData);
                        dgvDuLieu.DataSource = dtData;
                    }
                }
                MessageBox.Show("Đã tải dữ liệu từ SQL Server thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // NÚT 2: XUẤT TỪ DATATABLE RA FILE XML
        private void btnXuatXML_Click(object sender, EventArgs e)
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng 'Tải dữ liệu SQL' trước khi xuất báo cáo!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                XElement root = new XElement("BaoCaoThietBi",
                    new XAttribute("NgayXuat", DateTime.Now.ToString("dd/MM/yyyy HH:mm")),
                    new XAttribute("DonVi", "Trường ĐH Sư phạm Kỹ thuật")
                );

                // Nhóm dữ liệu theo Phòng Lab
                var groupedData = dtData.AsEnumerable().GroupBy(row => new {
                    MaPhong = row.Field<string>("MaPhong"),
                    TenPhong = row.Field<string>("TenPhong"),
                    QuanLy = row.Field<string>("QuanLy")
                });

                foreach (var group in groupedData)
                {
                    double tongGiaTriPhong = 0;
                    XElement dsThietBi = new XElement("DanhSachThietBi");

                    foreach (var row in group)
                    {
                        if (row["MaTB"] != DBNull.Value) // Nếu phòng có thiết bị
                        {
                            int soLuong = Convert.ToInt32(row["SoLuong"]);
                            double donGia = Convert.ToDouble(row["DonGia"]);
                            double thanhTien = Convert.ToDouble(row["ThanhTien"]);
                            tongGiaTriPhong += thanhTien;

                            dsThietBi.Add(new XElement("ThietBi",
                                new XAttribute("MaTB", row.Field<string>("MaTB")),
                                new XElement("TenThietBi", row.Field<string>("TenTB")),
                                new XElement("SoLuong", soLuong),
                                new XElement("DonGia", donGia),
                                new XElement("ThanhTien", thanhTien)
                            ));
                        }
                    }

                    XElement phongLab = new XElement("PhongLab",
                        new XAttribute("MaPhong", group.Key.MaPhong),
                        new XElement("TenPhong", group.Key.TenPhong),
                        new XElement("QuanLy", group.Key.QuanLy),
                        dsThietBi,
                        new XElement("TongGiaTriPhong", tongGiaTriPhong)
                    );
                    root.Add(phongLab);
                }

                XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
                xmlDoc.Save("BaoCaoThietBi.xml");
                MessageBox.Show("Đã xuất file BaoCaoThietBi.xml thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất XML: " + ex.Message, "Lỗi");
            }
        }

        // NÚT 3: CHUYỂN XML SANG HTML QUA XSLT
        private void btnXuatHTML_Click(object sender, EventArgs e)
        {
            if (!File.Exists("BaoCaoThietBi.xml"))
            {
                MessageBox.Show("Không tìm thấy file XML! Vui lòng 'Xuất XML' trước.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load("BaoCaoTemplate.xslt");
                xslt.Transform("BaoCaoThietBi.xml", "BaoCaoThietBi.html");

                MessageBox.Show("Đã tạo file BaoCaoThietBi.html thành công!\nBạn có thể vào thư mục bin/Debug để xem file HTML.", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất HTML (Kiểm tra lại file BaoCaoTemplate.xslt đã set Copy always chưa): \n" + ex.Message, "Lỗi");
            }
        }
    }
}