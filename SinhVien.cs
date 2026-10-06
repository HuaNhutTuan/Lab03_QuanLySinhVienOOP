using System;

namespace Lab03_QuanLySinhVienOOP
{
    public class SinhVien : Nguoi
    {
        private double _diemTrungBinh;

        // Thuộc tính
        public string MaSinhVien { get; set; } = string.Empty;
        public string MaLop { get; set; } = string.Empty;

        // Property có validation kiểm tra dữ liệu điểm từ 0 đến 10
        public double DiemTrungBinh
        {
            get => _diemTrungBinh;
            set
            {
                if (value < 0 || value > 10)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Điểm trung bình phải nằm trong khoảng từ 0 đến 10.");
                }
                _diemTrungBinh = value;
            }
        }

        // Constructor không tham số
        public SinhVien() : base()
        {
        }

        // Constructor đầy đủ tham số
        public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh, string maLop, double diemTrungBinh)
            : base(hoTen, ngaySinh)
        {
            MaSinhVien = maSinhVien;
            MaLop = maLop;
            DiemTrungBinh = diemTrungBinh; // Sẽ kích hoạt logic kiểm tra dữ liệu
        }

        // Phương thức xếp loại học lực dựa trên điểm trung bình
        public string XepLoai()
        {
            if (DiemTrungBinh >= 8.0)
            {
                return "Giỏi";
            }
            if (DiemTrungBinh >= 6.5)
            {
                return "Khá";
            }
            if (DiemTrungBinh >= 5.0)
            {
                return "Trung bình";
            }
            return "Yếu";
        }

        // Override phương thức LayThongTin() từ class Nguoi
        public override string LayThongTin()
        {
            return $"Mã SV: {MaSinhVien} | {base.LayThongTin()} | Lớp: {MaLop} | ĐTB: {DiemTrungBinh:F1} | Xếp loại: {XepLoai()}";
        }
    }
}
