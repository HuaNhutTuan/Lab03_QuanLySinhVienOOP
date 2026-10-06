using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab03_QuanLySinhVienOOP
{
    public class QuanLySinhVien
    {
       
        private readonly List<SinhVien> _danhSach;

        public QuanLySinhVien()
        {
            _danhSach = new List<SinhVien>();
        }

        
        public List<SinhVien> LayDanhSach()
        {
            return _danhSach.ToList();
        }

        
        public bool KiemTraTonTai(string maSinhVien)
        {
            return _danhSach.Any(sv => sv.MaSinhVien.Equals(maSinhVien.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        
        public bool Them(SinhVien sv)
        {
            if (sv == null || KiemTraTonTai(sv.MaSinhVien))
            {
                return false;
            }
            _danhSach.Add(sv);
            return true;
        }

        
        public SinhVien? TimTheoMa(string maSinhVien)
        {
            return _danhSach.FirstOrDefault(sv => sv.MaSinhVien.Equals(maSinhVien.Trim(), StringComparison.OrdinalIgnoreCase));
        }

       
        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                return new List<SinhVien>();
            }
            return _danhSach
                .Where(sv => sv.HoTen.Contains(tuKhoa.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

      
        public bool SuaDiem(string maSinhVien, double diemMoi)
        {
            var sv = TimTheoMa(maSinhVien);
            if (sv == null)
            {
                return false;
            }
            sv.DiemTrungBinh = diemMoi;
            return true;
        }

       
        public bool Xoa(string maSinhVien)
        {
            var sv = TimTheoMa(maSinhVien);
            if (sv == null)
            {
                return false;
            }
            return _danhSach.Remove(sv);
        }

        
        public List<SinhVien> SapXepTheoDiemGiamDan()
        {
            return _danhSach.OrderByDescending(sv => sv.DiemTrungBinh).ToList();
        }

       
        public List<SinhVien> LocSinhVienDat()
        {
            return _danhSach.Where(sv => sv.DiemTrungBinh >= 5.0).ToList();
        }
    }
}
