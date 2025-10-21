using HUIT_PriorityQueue.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace HUIT_PriorityQueue.Services
{
    public class FileService : IFileService
    {
        public List<PhieuGiaiQuyet> DocDanhSachTuFile(string tenFile)
        {
            List<PhieuGiaiQuyet> danhSach = new List<PhieuGiaiQuyet>();

            try
            {
                string[] lines = File.ReadAllLines(tenFile);

                if (lines.Length == 0) return danhSach;

                int n = int.Parse(lines[0]);

                for (int i = 1; i <= n && i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split('|');
                    if (parts.Length >= 5)
                    {
                        PhieuGiaiQuyet phieu = new PhieuGiaiQuyet
                        {
                            MaSV = parts[0],
                            TenSV = parts[1],
                            Lop = parts[2],
                            NoiDung = parts[3]
                        };

                        string[] dateParts = parts[4].Split('/');
                        if (dateParts.Length == 3)
                        {
                            phieu.NgayGui = new NgayThangNam
                            {
                                Ngay = int.Parse(dateParts[0]),
                                Thang = int.Parse(dateParts[1]),
                                Nam = int.Parse(dateParts[2])
                            };
                        }

                        phieu.MucDoUuTien = TinhMucDoUuTien(phieu.NoiDung);
                        danhSach.Add(phieu);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đọc file: {ex.Message}");
            }

            return danhSach;
        }

        public void GhiDanhSachRaFile(string tenFile, List<PhieuGiaiQuyet> danhSach)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(tenFile))
                {
                    writer.WriteLine(danhSach.Count);

                    foreach (var phieu in danhSach)
                    {
                        writer.WriteLine($"{phieu.MaSV}|{phieu.TenSV}|{phieu.Lop}|{phieu.NoiDung}|{phieu.NgayGui}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi ghi file: {ex.Message}");
            }
        }

        public void GhiDanhSachRaFile(string tenFile, PriorityQueue queue)
        {
            GhiDanhSachRaFile(tenFile, queue.GetAllItems());
        }

        private int TinhMucDoUuTien(string noiDung)
        {
            if (string.IsNullOrEmpty(noiDung))
                return 0;

            string noiDungLower = noiDung.ToLower();

            string[] khanCap = { "bao mat", "mat khau", "he thong", "ddos", "lo hong", "xac thuc", "khoa tai khoan" };
            string[] quanTrong = { "gia han", "nop bai", "cap nhat", "thuc tap", "xac nhan", "ho so", "ket qua" };

            foreach (string tuKhoa in khanCap)
            {
                if (noiDungLower.Contains(tuKhoa))
                    return 2;
            }

            foreach (string tuKhoa in quanTrong)
            {
                if (noiDungLower.Contains(tuKhoa))
                    return 1;
            }

            return 0;
        }
    }
}