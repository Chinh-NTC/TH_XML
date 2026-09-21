using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting; // Để lấy đường dẫn thư mục wwwroot
using System.Xml.Linq;
using System.Linq;
using System.IO;

namespace Bai8
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;

        // Tiêm IWebHostEnvironment để truy cập thư mục wwwroot
        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Endpoint nhận AJAX request
        public IActionResult LiveSearch(string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return Content("");
            }

            // Đường dẫn tới file links.xml trong wwwroot
            string path = Path.Combine(_env.WebRootPath, "links.xml");

            if (!System.IO.File.Exists(path))
            {
                return Content("Không tìm thấy file XML.");
            }

            XDocument doc = XDocument.Load(path);

            // Dùng LINQ tìm kiếm chuỗi q trong các thẻ <title> (Không phân biệt hoa thường)
            var matches = doc.Descendants("link")
                             .Where(x => x.Element("title").Value.ToLower().Contains(q.ToLower()))
                             .ToList();

            if (matches.Count == 0)
            {
                return Content("no suggestion");
            }

            string hint = "";
            foreach (var item in matches)
            {
                string title = item.Element("title").Value;
                string url = item.Element("url").Value;

                // Nối kết quả thành chuỗi HTML giống cấu trúc của PHP
                if (hint == "")
                {
                    hint = $"<a href='{url}' target='_blank'>{title}</a>";
                }
                else
                {
                    hint += $"<br /><a href='{url}' target='_blank'>{title}</a>";
                }
            }

            // Trả về chuỗi HTML
            return Content(hint, "text/html");
        }
    }
}