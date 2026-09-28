using System;
using System.Data.SqlClient; // Thư viện kết nối SQL Server
using System.Xml.Linq;       // Thư viện xử lý XML

namespace Bai9_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Hỗ trợ in tiếng Việt có dấu ra màn hình Console
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== CHƯƠNG TRÌNH CHUYỂN ĐỔI SQL SANG XML ===");
            Console.WriteLine("Đang kết nối đến cơ sở dữ liệu...");

            // 1. Chuỗi kết nối đến SQL Server (Nhớ thay MẬT KHẨU của bạn)
            string connectionString = @"Server=localhost;Database=udn;User Id=sa;Password=25112006;TrustServerCertificate=True;";

            // 2. Tên file XML sẽ được tạo ra (mặc định lưu cùng thư mục chạy của file .exe)
            string pathXML = "sinhvien.xml";

            try
            {
                // Khởi tạo thẻ gốc <dct>
                XElement root = new XElement("dct");

                // Kết nối CSDL
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("[Thành công] Đã kết nối đến SQL Server.");

                    // Lấy dữ liệu từ bảng sinhvien
                    string query = "SELECT masv, hoten, lop, diachi FROM sinhvien";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    int count = 0; // Biến đếm số sinh viên

                    while (reader.Read())
                    {
                        // 3. Tạo thẻ <sinhvien> chứa các thuộc tính
                        // Dùng .ToString().Trim() để lỡ CSDL có khoảng trắng dư thừa thì sẽ bị cắt đi
                        XElement sv = new XElement("sinhvien",
                            new XAttribute("masv", reader["masv"].ToString().Trim()),
                            new XAttribute("hoten", reader["hoten"].ToString().Trim()),
                            new XAttribute("lop", reader["lop"].ToString().Trim()),
                            new XAttribute("diachi", reader["diachi"].ToString().Trim())
                        );

                        root.Add(sv); // Đẩy vào thẻ gốc
                        count++;
                    }

                    // 4. Lưu thành file XML
                    XDocument doc = new XDocument(root);
                    doc.Save(pathXML);

                    Console.WriteLine($"[Thành công] Đã chuyển đổi {count} sinh viên.");
                    Console.WriteLine($"[Hoàn tất] File được lưu tại: {AppDomain.CurrentDomain.BaseDirectory}{pathXML}");
                }
            }
            catch (Exception ex)
            {
                // In ra lỗi nếu mật khẩu sai, chưa bật SQL, v.v...
                Console.WriteLine("\n[LỖI] CÓ LỖI XẢY RA:");
                Console.WriteLine(ex.Message);
            }

            // Dừng màn hình để xem kết quả
            Console.WriteLine("\nNhấn phím bất kỳ để thoát chương trình...");
            Console.ReadKey();
        }
    }
}