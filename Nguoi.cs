using System;

namespace Lab03_QuanLySinhVienOOP
{
    public class Nguoi
    {
        // Thuộc tính
        public string HoTen { get; set; } = string.Empty;
        public DateTime NgaySinh { get; set; }

        // Constructor không tham số
        public Nguoi()
        {
        }

        // Constructor có tham số
        public Nguoi(string hoTen, DateTime ngaySinh)
        {
            HoTen = hoTen;
            NgaySinh = ngaySinh;
        }

        // Phương thức ảo LayThongTin() có thể override
        public virtual string LayThongTin()
        {
            return $"Họ tên: {HoTen} | Ngày sinh: {NgaySinh:dd/MM/yyyy}";
        }
    }
}
