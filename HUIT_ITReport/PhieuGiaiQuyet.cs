using System;

namespace HUIT_PriorityQueue.Models
{
    public class NgayThangNam
    {
        public int Ngay { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }

        public override string ToString()
        {
            return $"{Ngay:00}/{Thang:00}/{Nam:0000}";
        }
    }

    public class PhieuGiaiQuyet
    {
        public string MaSV { get; set; }
        public string TenSV { get; set; }
        public string Lop { get; set; }
        public string NoiDung { get; set; }
        public NgayThangNam NgayGui { get; set; }
        public int MucDoUuTien { get; set; }

        public PhieuGiaiQuyet()
        {
            NgayGui = new NgayThangNam();
        }

        public override string ToString()
        {
            return $"{MaSV,-12} {TenSV,-20} {Lop,-15} {NoiDung,-40} {NgayGui}";
        }
    }
}