using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Bai10 // Đã điều chỉnh namespace khớp với project của bạn
{
    public partial class Form1 : Form
    {
        // 1. Chuỗi kết nối CSDL (Nhớ thay MẬT KHẨU CỦA BẠN)
        string connectionString = @"Server=localhost;Database=udn;User Id=sa;Password=25112006;TrustServerCertificate=True;";

        // 2. Đường dẫn file XML
        string pathXML = "sinhvien.xml";

        public Form1()
        {
            InitializeComponent();

            // GỌI HÀM ĐỌC XML NGAY KHI VỪA MỞ PHẦN MỀM
            HienThiXML();
        }

        // Hàm phụ: Đọc file XML và đưa lên bảng lưới
        private void HienThiXML()
        {
            if (File.Exists(pathXML))
            {
                XDocument doc = XDocument.Load(pathXML);

                var listSV = doc.Descendants("sinhvien").Select(x => new {
                    MaSV = x.Attribute("masv")?.Value.Trim(),
                    HoTen = x.Attribute("hoten")?.Value.Trim(),
                    Lop = x.Attribute("lop")?.Value.Trim(),
                    DiaChi = x.Attribute("diachi")?.Value.Trim()
                }).ToList();

                dgvData.DataSource = listSV;

                // Đổi tiêu đề cột sang tiếng Việt cho đẹp
                if (dgvData.Columns.Count >= 4)
                {
                    dgvData.Columns[0].HeaderText = "Mã Sinh Viên";
                    dgvData.Columns[1].HeaderText = "Họ Tên";
                    dgvData.Columns[2].HeaderText = "Lớp";
                    dgvData.Columns[3].HeaderText = "Địa Chỉ";
                }
            }
        }

        // Nút bấm: Đẩy toàn bộ dữ liệu đang có xuống SQL Server
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (!File.Exists(pathXML))
            {
                MessageBox.Show($"Không tìm thấy tệp '{pathXML}'!", "Thiếu File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                XDocument doc = XDocument.Load(pathXML);
                int successCount = 0;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    foreach (XElement sv in doc.Descendants("sinhvien"))
                    {
                        string masv = sv.Attribute("masv")?.Value.Trim();
                        string hoten = sv.Attribute("hoten")?.Value.Trim();
                        string lop = sv.Attribute("lop")?.Value.Trim();
                        string diachi = sv.Attribute("diachi")?.Value.Trim();

                        if (string.IsNullOrEmpty(masv)) continue;

                        string query = @"
                            IF NOT EXISTS (SELECT 1 FROM sinhvien WHERE masv = @masv)
                            BEGIN
                                INSERT INTO sinhvien (masv, hoten, lop, diachi, matkhau) 
                                VALUES (@masv, @hoten, @lop, @diachi, '1234')
                            END
                            ELSE
                            BEGIN
                                UPDATE sinhvien 
                                SET hoten = @hoten, lop = @lop, diachi = @diachi 
                                WHERE masv = @masv
                            END";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@masv", masv);
                            cmd.Parameters.AddWithValue("@hoten", hoten);
                            cmd.Parameters.AddWithValue("@lop", lop);
                            cmd.Parameters.AddWithValue("@diachi", diachi);

                            cmd.ExecuteNonQuery();
                            successCount++;
                        }
                    }
                }

                lblStatus.Text = $"Trạng thái: Đã cập nhật thành công {successCount} sinh viên vào cơ sở dữ liệu.";
                MessageBox.Show($"Hoàn tất quá trình lưu {successCount} sinh viên vào SQL Server!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}