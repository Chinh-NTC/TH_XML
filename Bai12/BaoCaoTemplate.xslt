<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" indent="yes" encoding="utf-8"/>

	<!-- Khai báo định dạng số kiểu Việt Nam: dấu chấm phân cách hàng nghìn, dấu phẩy cho số thập phân -->
	<xsl:decimal-format name="vi" decimal-separator="," grouping-separator="."/>

	<xsl:template match="/">
		<html>
			<head>
				<title>Báo Cáo Thiết Bị Phòng Lab</title>
				<style>
					body { font-family: 'Segoe UI', Arial, sans-serif; background-color: #f4f7f6; padding: 20px; }
					.header { text-align: center; margin-bottom: 30px; }
					.header h1 { color: #2c3e50; }

					/* Thiết kế dạng Card */
					.card { background: white; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.1); margin-bottom: 30px; padding: 20px; border-top: 5px solid #0078D7; }
					.card h2 { color: #0078D7; margin-top: 0; }
					.info { color: #555; margin-bottom: 15px; }

					/* Thiết kế Bảng */
					table { width: 100%; border-collapse: collapse; margin-top: 15px; }
					th, td { border: 1px solid #ddd; padding: 12px; text-align: left; }
					th { background-color: #0078D7; color: white; }
					tr:nth-child(even) { background-color: #f9f9f9; }

					.total-row { font-weight: bold; background-color: #ffeaa7 !important; }
					.total-row td { text-align: right; color: #d63031; font-size: 1.1em; }
					.text-right { text-align: right; } /* Class căn phải cho cột tiền */
				</style>
			</head>
			<body>
				<div class="header">
					<h1>BÁO CÁO THIẾT BỊ PHÒNG LAB</h1>
					<p>
						<strong>Đơn vị: </strong>
						<xsl:value-of select="BaoCaoThietBi/@DonVi"/>
					</p>
					<p>
						<strong>Ngày xuất: </strong>
						<xsl:value-of select="BaoCaoThietBi/@NgayXuat"/>
					</p>
				</div>

				<xsl:for-each select="BaoCaoThietBi/PhongLab">
					<div class="card">
						<h2>
							Phòng: <xsl:value-of select="TenPhong"/> (Mã: <xsl:value-of select="@MaPhong"/>)
						</h2>
						<div class="info">
							<strong>Người quản lý:</strong>
							<xsl:value-of select="QuanLy"/>
						</div>

						<table>
							<tr>
								<th>Mã TB</th>
								<th>Tên Thiết Bị</th>
								<th class="text-right">Số Lượng</th>
								<th class="text-right">Đơn Giá (VNĐ)</th>
								<th class="text-right">Thành Tiền (VNĐ)</th>
							</tr>

							<!-- Duyệt danh sách thiết bị trong phòng -->
							<xsl:for-each select="DanhSachThietBi/ThietBi">
								<tr>
									<td>
										<xsl:value-of select="@MaTB"/>
									</td>
									<td>
										<xsl:value-of select="TenThietBi"/>
									</td>
									<td class="text-right">
										<xsl:value-of select="SoLuong"/>
									</td>
									<!-- Áp dụng format-number cho Đơn Giá và Thành Tiền -->
									<td class="text-right">
										<xsl:value-of select="format-number(DonGia, '#.##0', 'vi')"/>
									</td>
									<td class="text-right">
										<xsl:value-of select="format-number(ThanhTien, '#.##0', 'vi')"/>
									</td>
								</tr>
							</xsl:for-each>

							<!-- Dòng tính tổng -->
							<tr class="total-row">
								<td colspan="4">TỔNG GIÁ TRỊ TRANG THIẾT BỊ PHÒNG NÀY:</td>
								<!-- Áp dụng format-number cho Tổng giá trị -->
								<td class="text-right">
									<xsl:value-of select="format-number(TongGiaTriPhong, '#.##0', 'vi')"/> VNĐ
								</td>
							</tr>
						</table>
					</div>
				</xsl:for-each>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>