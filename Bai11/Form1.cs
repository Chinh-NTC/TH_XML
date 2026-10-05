using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient; // Khai báo thư viện MySQL

namespace Bai11
{
    public partial class Form1 : Form
    {
        // 1. Chuỗi kết nối MySQL (Nguồn) - Dùng cấu hình XAMPP mặc định (tài khoản root, không pass)
        string connStringMySQL = "Server=localhost;Database=truonghoc_mysql;Uid=root;Pwd=;";

        // 2. Chuỗi kết nối SQL Server (Đích) - Mật khẩu sa là 25112006
        string connStringSQLServer = "Server=localhost;Database=TruongHoc_Dich;User Id=sa;Password=25112006;Encrypt=True;TrustServerCertificate=True;";

        string xmlFilePath = "DuLieuTrungGian.xml";

        public Form1()
        {
            InitializeComponent();
        }

        // Tự động tải dữ liệu lên 2 bảng khi mở phần mềm
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMySQLDataToGrid(connStringMySQL, dgvNguon);
            LoadSQLDataToGrid(connStringSQLServer, dgvDich);
        }

        // Hàm hỗ trợ: Tải dữ liệu từ MySQL (Bảng bên trái)
        private void LoadMySQLDataToGrid(string connectionString, DataGridView dgv)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MaSV, HoTen, Lop FROM SinhVien";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                InThongBao("Chưa thể tải dữ liệu MySQL: " + ex.Message);
            }
        }

        // Hàm hỗ trợ: Tải dữ liệu từ SQL Server (Bảng bên phải)
        private void LoadSQLDataToGrid(string connectionString, DataGridView dgv)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MaSV, HoTen, Lop FROM SinhVien";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                InThongBao("Chưa thể tải dữ liệu SQL Server: " + ex.Message);
            }
        }

        // Hàm in log xuống màn hình đen
        private void InThongBao(string thongBao)
        {
            txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + " - " + thongBao + Environment.NewLine);
        }

        // BƯỚC 1: Xuất dữ liệu từ MySQL -> XML
        private void btnXuat_Click(object sender, EventArgs e)
        {
            InThongBao("Bắt đầu xuất dữ liệu từ MySQL Nguồn...");

            using (MySqlConnection conn = new MySqlConnection(connStringMySQL))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MaSV, HoTen, Lop FROM SinhVien";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        XDocument doc = new XDocument(
                            new XDeclaration("1.0", "utf-8", "yes"),
                            new XElement("DanhSachSinhVien")
                        );

                        XElement root = doc.Element("DanhSachSinhVien");

                        while (reader.Read())
                        {
                            root.Add(
                                new XElement("SinhVien",
                                    new XElement("MaSV", reader["MaSV"].ToString()),
                                    new XElement("HoTen", reader["HoTen"].ToString()),
                                    new XElement("Lop", reader["Lop"].ToString())
                                )
                            );
                        }

                        doc.Save(xmlFilePath);
                        InThongBao("-> Đã xuất dữ liệu ra file XML thành công!");
                    }
                }
                catch (Exception ex)
                {
                    InThongBao("Lỗi xuất XML từ MySQL: " + ex.Message);
                }
            }
        }

        // BƯỚC 2: Nhập từ XML -> SQL Server
        private void btnNhap_Click(object sender, EventArgs e)
        {
            InThongBao("Bắt đầu đọc file XML và nạp vào SQL Server Đích...");

            try
            {
                XDocument doc = XDocument.Load(xmlFilePath);

                using (SqlConnection conn = new SqlConnection(connStringSQLServer))
                {
                    conn.Open();

                    foreach (XElement sv in doc.Element("DanhSachSinhVien").Elements("SinhVien"))
                    {
                        string maSV = sv.Element("MaSV").Value;
                        string hoTen = sv.Element("HoTen").Value;
                        string lop = sv.Element("Lop").Value;

                        // Kiểm tra trùng lặp mã Sinh Viên trước khi Insert
                        string insertQuery = @"
                            IF NOT EXISTS (SELECT 1 FROM SinhVien WHERE MaSV = @MaSV)
                            BEGIN
                                INSERT INTO SinhVien (MaSV, HoTen, Lop) VALUES (@MaSV, @HoTen, @Lop)
                            END";

                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaSV", maSV);
                            cmd.Parameters.AddWithValue("@HoTen", hoTen);
                            cmd.Parameters.AddWithValue("@Lop", lop);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    InThongBao("-> Đã đồng bộ dữ liệu từ XML vào SQL Server Đích thành công!");

                    // Tự động làm mới bảng lưới bên phải để hiện dữ liệu vừa đồng bộ
                    LoadSQLDataToGrid(connStringSQLServer, dgvDich);
                }
            }
            catch (Exception ex)
            {
                InThongBao("Lỗi nhập SQL Server: " + ex.Message);
            }
        }
    }
}