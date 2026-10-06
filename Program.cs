using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lab03_QuanLySinhVienOOP
{
    internal class Program
    {
        private static readonly QuanLySinhVien quanLy = new QuanLySinhVien();

        static void Main(string[] args)
        {
            // Thiết lập font hiển thị tiếng Việt trên Console
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // Khởi tạo một vài dữ liệu mẫu để người dùng dễ thử nghiệm ngay (tuỳ chọn)
            KhoiTaoDuLieuMau();

            bool tiepTuc = true;
            while (tiepTuc)
            {
                HienThiMenu();
                Console.Write("Chon chuc nang: ");
                string? input = Console.ReadLine();

                if (input == null)
                {
                    break;
                }

                if (!int.TryParse(input, out int chon))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lựa chọn không hợp lệ! Vui lòng nhập số tương ứng từ 0 đến 8.");
                    Console.ResetColor();
                    DungManHinh();
                    continue;
                }

                Console.WriteLine();
                switch (chon)
                {
                    case 1:
                        ChucNangThemSinhVien();
                        break;
                    case 2:
                        ChucNangXuatDanhSach();
                        break;
                    case 3:
                        ChucNangTimTheoMa();
                        break;
                    case 4:
                        ChucNangTimTheoTen();
                        break;
                    case 5:
                        ChucNangSuaDiem();
                        break;
                    case 6:
                        ChucNangXoaSinhVien();
                        break;
                    case 7:
                        ChucNangSapXepTheoDiemGiamDan();
                        break;
                    case 8:
                        ChucNangLocSinhVienDat();
                        break;
                    case 0:
                        tiepTuc = false;
                        Console.WriteLine("Đã thoát chương trình. Tạm biệt!");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Chức năng không tồn tại! Vui lòng chọn lại từ 0 đến 8.");
                        Console.ResetColor();
                        break;
                }

                if (tiepTuc)
                {
                    DungManHinh();
                }
            }
        }

        // ========================== CÁC CHỨC NĂNG ==========================

        private static void HienThiMenu()
        {
            Console.WriteLine("===== QUAN LY SINH VIEN =====");
            Console.WriteLine("1. Them sinh vien");
            Console.WriteLine("2. Xuat danh sach");
            Console.WriteLine("3. Tim sinh vien theo ma");
            Console.WriteLine("4. Tim sinh vien theo ten");
            Console.WriteLine("5. Sua diem trung binh");
            Console.WriteLine("6. Xoa sinh vien");
            Console.WriteLine("7. Sap xep theo diem giam dan");
            Console.WriteLine("8. Loc sinh vien dat");
            Console.WriteLine("0. Thoat");
        }

        // 1. Thêm sinh viên
        private static void ChucNangThemSinhVien()
        {
            Console.WriteLine("--- 1. THÊM SINH VIÊN ---");

            string maSV;
            while (true)
            {
                maSV = NhapChuoi("Nhập mã sinh viên: ");
                if (quanLy.KiemTraTonTai(maSV))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Mã sinh viên '{maSV}' đã tồn tại! Vui lòng nhập mã khác.");
                    Console.ResetColor();
                }
                else
                {
                    break;
                }
            }

            string hoTen = NhapChuoi("Nhập họ tên: ");
            DateTime ngaySinh = NhapNgaySinh("Nhập ngày sinh (dd/MM/yyyy): ");
            string maLop = NhapChuoi("Nhập mã lớp: ");
            double diemTB = NhapDiem("Nhập điểm trung bình (0 - 10): ");

            try
            {
                SinhVien sv = new SinhVien(maSV, hoTen, ngaySinh, maLop, diemTB);
                if (quanLy.Them(sv))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("=> Thêm sinh viên thành công!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("=> Thêm thất bại: Mã sinh viên đã tồn tại.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"=> Lỗi khi thêm sinh viên: {ex.Message}");
                Console.ResetColor();
            }
        }

        // 2. Xuất danh sách sinh viên
        private static void ChucNangXuatDanhSach()
        {
            Console.WriteLine("--- 2. DANH SÁCH TẤT CẢ SINH VIÊN ---");
            var ds = quanLy.LayDanhSach();
            InDanhSachSinhVien(ds);
        }

        // 3. Tìm sinh viên theo mã
        private static void ChucNangTimTheoMa()
        {
            Console.WriteLine("--- 3. TÌM SINH VIÊN THEO MÃ ---");
            string maSV = NhapChuoi("Nhập mã sinh viên cần tìm: ");
            var sv = quanLy.TimTheoMa(maSV);

            if (sv != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=> Kết quả tìm thấy:");
                Console.ResetColor();
                InDanhSachSinhVien(new List<SinhVien> { sv });
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Không tìm thấy sinh viên có mã '{maSV}'.");
                Console.ResetColor();
            }
        }

        // 4. Tìm sinh viên theo tên
        private static void ChucNangTimTheoTen()
        {
            Console.WriteLine("--- 4. TÌM SINH VIÊN THEO TÊN ---");
            string tuKhoa = NhapChuoi("Nhập từ khóa họ tên cần tìm: ");
            var ketQua = quanLy.TimTheoTen(tuKhoa);

            if (ketQua.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"=> Tìm thấy {ketQua.Count} sinh viên chứa từ khóa '{tuKhoa}':");
                Console.ResetColor();
                InDanhSachSinhVien(ketQua);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Không tìm thấy sinh viên nào có tên chứa từ khóa '{tuKhoa}'.");
                Console.ResetColor();
            }
        }

        // 5. Sửa điểm trung bình
        private static void ChucNangSuaDiem()
        {
            Console.WriteLine("--- 5. SỬA ĐIỂM TRUNG BÌNH ---");
            string maSV = NhapChuoi("Nhập mã sinh viên cần sửa điểm: ");
            var sv = quanLy.TimTheoMa(maSV);

            if (sv == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Không tìm thấy sinh viên có mã '{maSV}' để cập nhật điểm.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"Sinh viên: {sv.HoTen} (Lớp: {sv.MaLop}) - Điểm hiện tại: {sv.DiemTrungBinh:F1}");
            double diemMoi = NhapDiem("Nhập điểm trung bình mới (0 - 10): ");

            if (quanLy.SuaDiem(maSV, diemMoi))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=> Cập nhật điểm thành công!");
                Console.ResetColor();
                Console.WriteLine($"Thông tin sau cập nhật: {sv.LayThongTin()}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=> Cập nhật điểm thất bại!");
                Console.ResetColor();
            }
        }

        // 6. Xóa sinh viên
        private static void ChucNangXoaSinhVien()
        {
            Console.WriteLine("--- 6. XÓA SINH VIÊN ---");
            string maSV = NhapChuoi("Nhập mã sinh viên cần xóa: ");

            if (quanLy.Xoa(maSV))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"=> Xóa sinh viên có mã '{maSV}' thành công!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Chương trình thông báo không tìm thấy sinh viên có mã '{maSV}'.");
                Console.ResetColor();
            }
        }

        // 7. Sắp xếp theo điểm giảm dần
        private static void ChucNangSapXepTheoDiemGiamDan()
        {
            Console.WriteLine("--- 7. DANH SÁCH SINH VIÊN THEO ĐIỂM GIẢM DẦN ---");
            var dsSapXep = quanLy.SapXepTheoDiemGiamDan();
            InDanhSachSinhVien(dsSapXep);
        }

        // 8. Lọc sinh viên đạt
        private static void ChucNangLocSinhVienDat()
        {
            Console.WriteLine("--- 8. DANH SÁCH SINH VIÊN ĐẠT (ĐIỂM >= 5.0) ---");
            var dsDat = quanLy.LocSinhVienDat();
            InDanhSachSinhVien(dsDat);
        }

        // ========================== HÀM NHẬP DỮ LIỆU & TIỆN ÍCH ==========================

        private static string NhapChuoi(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Dữ liệu không được để trống. Vui lòng nhập lại!");
                Console.ResetColor();
            }
        }

        private static DateTime NhapNgaySinh(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string? input = Console.ReadLine();
                if (DateTime.TryParseExact(input?.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngaySinh))
                {
                    return ngaySinh;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ngày sinh không đúng định dạng (dd/MM/yyyy, ví dụ: 25/08/2004). Vui lòng nhập lại!");
                Console.ResetColor();
            }
        }

        private static double NhapDiem(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string? input = Console.ReadLine();

                // Chuẩn hóa dấu chấm và phẩy
                string normalizedInput = (input ?? "").Trim().Replace(',', '.');

                if (double.TryParse(normalizedInput, NumberStyles.Float, CultureInfo.InvariantCulture, out double diem))
                {
                    if (diem >= 0.0 && diem <= 10.0)
                    {
                        return diem;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Chương trình báo điểm không hợp lệ và yêu cầu nhập lại! (Điểm phải là số thực từ 0 đến 10)");
                Console.ResetColor();
            }
        }

        private static void InDanhSachSinhVien(List<SinhVien> danhSach)
        {
            if (danhSach == null || danhSach.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Danh sách trống!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine(new string('-', 85));
            Console.WriteLine($"| {"STT",-4} | {"Mã SV",-10} | {"Họ và tên",-22} | {"Ngày sinh",-11} | {"Lớp",-8} | {"ĐTB",-5} | {"Xếp loại",-10} |");
            Console.WriteLine(new string('-', 85));

            for (int i = 0; i < danhSach.Count; i++)
            {
                var sv = danhSach[i];
                Console.WriteLine($"| {i + 1,-4} | {sv.MaSinhVien,-10} | {sv.HoTen,-22} | {sv.NgaySinh,-11:dd/MM/yyyy} | {sv.MaLop,-8} | {sv.DiemTrungBinh,5:F1} | {sv.XepLoai(),-10} |");
            }
            Console.WriteLine(new string('-', 85));
            Console.WriteLine($"Tổng số sinh viên: {danhSach.Count}");
        }

        private static void DungManHinh()
        {
            Console.WriteLine();
            Console.WriteLine("Nhấn Enter để tiếp tục...");
            Console.ReadLine();
            try
            {
                Console.Clear();
            }
            catch (Exception)
            {
                // Bỏ qua lỗi khi console bị redirect hoặc không hỗ trợ clear
            }
        }

        private static void KhoiTaoDuLieuMau()
        {
            quanLy.Them(new SinhVien("SV001", "Nguyễn Văn A", new DateTime(2004, 5, 12), "CNTT01", 8.2));
            quanLy.Them(new SinhVien("SV002", "Trần Thị B", new DateTime(2004, 11, 20), "CNTT01", 6.8));
            quanLy.Them(new SinhVien("SV003", "Lê Văn C", new DateTime(2003, 2, 15), "CNTT02", 4.5));
            quanLy.Them(new SinhVien("SV004", "Phạm Hoàng Nguyễn", new DateTime(2004, 9, 8), "CNTT02", 9.0));
        }
    }
}
