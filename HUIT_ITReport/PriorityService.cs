using HUIT_PriorityQueue.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HUIT_PriorityQueue.Services
{
    public class PriorityService
    {
        private readonly string[] _tuKhoaKhanCap = {
            "bao mat", "mat khau", "he thong", "ddos", "lo hong", "xac thuc",
            "khoa tai khoan", "mat ket noi", "loi web", "bi hack", "khong the truy cap",
            "sap web", "loi he thong", "he thong co loi", "khong thi duoc", "hoan thi",
            "khan cap", "gap", "cam thi", "huy", "khong the", "khong hoat dong",
            "khong chay", "khong thanh toan duoc", "khong truy cap", "loi nang"
        };

        private readonly string[] _tuKhoaQuanTrong = {
            "gia han", "nop bai", "cap nhat", "thuc tap", "xac nhan", "ho so",
            "ket qua", "doi lich", "loi nang", "mat ket noi", "trung lich",
            "can giai quyet", "thac mac", "hoi ve", "gap ve hoc phi", "ho tro"
        };

        public int TinhMucDoUuTien(string noiDung)
        {
            if (string.IsNullOrEmpty(noiDung))
                return 0;

            string noiDungLower = noiDung.ToLower();

            foreach (string tuKhoa in _tuKhoaKhanCap)
            {
                if (noiDungLower.Contains(tuKhoa))
                    return 2;
            }

            foreach (string tuKhoa in _tuKhoaQuanTrong)
            {
                if (noiDungLower.Contains(tuKhoa))
                    return 1;
            }

            return 0;
        }

        public string LayTenMucDoUuTien(int mucDo)
        {
            switch (mucDo)
            {
                case 2:
                    return "CAO";
                case 1:
                    return "TRUNG BÌNH";
                case 0:
                    return "THẤP";
                default:
                    return "KHÔNG XÁC ĐỊNH";
            }
        }
    }
}