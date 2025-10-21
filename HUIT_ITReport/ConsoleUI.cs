using HUIT_PriorityQueue.Models;
using HUIT_PriorityQueue.Services;
using HUIT_PriorityQueue.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HUIT_PriorityQueue.UI
{
    public class ConsoleUI
    {
        private PriorityQueue _queue;
        private IFileService _fileService;
        private PriorityService _priorityService;

        public ConsoleUI()
        {
            _queue = new PriorityQueue();
            _fileService = new FileService();
            _priorityService = new PriorityService();
        }

        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                HienThiMenu();
                Console.Write("Chọn chức năng: ");
                string luaChon = Console.ReadLine();

                switch (luaChon)
                {
                    case "1":
                        NhapDanhSach();
                        break;
                    case "2":
                        DocTuFile();
                        break;
                    case "3":
                        ThemPhieuMoi();
                        break;
                    case "4":
                        XoaPhieu();
                        break;
                    case "5":
                        TimKiemPhieu();
                        break;
                    case "6":
                        HienThiTatCaPhieu();
                        break;
                    case "7":
                        CapNhatPhieu();
                        break;
                    case "8":
                        UuTienPhieu();
                        break;
                    case "9":
                        ThongKeLop();
                        break;
                    case "10":
                        HienThi10PhieuDau();
                        break;
                    case "11":
                        GhiRaFile();
                        break;
                    case "12":
                        ThongKeMucDoUuTien();
                        break;
                    case "0":
                        Console.WriteLine("Thoát chương trình!");
                        return;
                    default:
                        Console.WriteLine("Chức năng không hợp lệ!");
                        break;
                }

                Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
                Console.ReadKey();
            }
        }

        private void HienThiMenu()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("  HỆ THỐNG QUẢN LÝ PHIẾU GIẢI QUYẾT");
            Console.WriteLine("         ĐẠI HỌC HUIT - KHOA CNTT");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Nhập danh sách phiếu (bàn phím)");
            Console.WriteLine("2. Đọc danh sách từ file");
            Console.WriteLine("3. Thêm phiếu mới");
            Console.WriteLine("4. Xóa phiếu (đầu queue)");
            Console.WriteLine("5. Tìm kiếm phiếu");
            Console.WriteLine("6. Hiển thị tất cả phiếu");
            Console.WriteLine("7. Cập nhật phiếu");
            Console.WriteLine("8. Ưu tiên phiếu lên đầu");
            Console.WriteLine("9. Thống kê lớp nhiều phiếu nhất");
            Console.WriteLine("10. Hiển thị 10 phiếu sắp xử lý");
            Console.WriteLine("11. Ghi danh sách ra file");
            Console.WriteLine("12. Thống kê mức độ ưu tiên");
            Console.WriteLine("0. Thoát");
            Console.WriteLine("==========================================");
        }

        private void NhapDanhSach()
        {
            Console.Write("Nhập số lượng phiếu: ");
            if (int.TryParse(Console.ReadLine(), out int n) && n > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine($"\n--- Phiếu {i + 1} ---");
                    PhieuGiaiQuyet phieu = NhapThongTinPhieu();
                    int doUuTien = _priorityService.TinhMucDoUuTien(phieu.NoiDung);
                    _queue.Enqueue(phieu, doUuTien);
                    phieu.MucDoUuTien = doUuTien;
                }
                Console.WriteLine($"\n✅ Đã thêm {n} phiếu vào hàng đợi!");
            }
            else
            {
                Console.WriteLine("❌ Số lượng không hợp lệ!");
            }
        }

        private void DocTuFile()
        {
            Console.Write("Nhập tên file (mặc định: ds_PhieuGiaiQuyet.txt): ");
            string tenFile = Console.ReadLine();
            if (string.IsNullOrEmpty(tenFile))
                tenFile = "ds_PhieuGiaiQuyet.txt";

            var danhSach = _fileService.DocDanhSachTuFile(tenFile);
            foreach (var phieu in danhSach)
            {
                _queue.Enqueue(phieu, phieu.MucDoUuTien);
            }
            Console.WriteLine($"✅ Đã đọc {danhSach.Count} phiếu từ file!");
        }

        private void ThemPhieuMoi()
        {
            Console.WriteLine("\n--- Thêm phiếu mới ---");
            PhieuGiaiQuyet phieu = NhapThongTinPhieu();
            int doUuTien = _priorityService.TinhMucDoUuTien(phieu.NoiDung);
            _queue.Enqueue(phieu, doUuTien);
            phieu.MucDoUuTien = doUuTien;
            Console.WriteLine("✅ Đã thêm phiếu thành công!");
        }

        private PhieuGiaiQuyet NhapThongTinPhieu()
        {
            PhieuGiaiQuyet phieu = new PhieuGiaiQuyet();

            Console.Write("Mã SV: ");
            phieu.MaSV = Console.ReadLine();

            Console.Write("Tên SV: ");
            phieu.TenSV = Console.ReadLine();

            Console.Write("Lớp: ");
            phieu.Lop = Console.ReadLine();

            Console.Write("Nội dung: ");
            phieu.NoiDung = Console.ReadLine();

            DateValidator.NhapNgayThangNam("Ngày gửi (dd/mm/yyyy): ",
                out int ngay, out int thang, out int nam);

            phieu.NgayGui = new NgayThangNam { Ngay = ngay, Thang = thang, Nam = nam };

            return phieu;
        }

        private void XoaPhieu()
        {
            if (_queue.IsEmpty())
            {
                Console.WriteLine("❌ Hàng đợi đang trống!");
                return;
            }

            var phieu = _queue.Dequeue();
            Console.WriteLine("✅ Đã xóa phiếu:");
            HienThiPhieu(phieu);
        }

        private void TimKiemPhieu()
        {
            Console.Write("Nhập mã SV cần tìm: ");
            string maSV = Console.ReadLine();

            var danhSach = _queue.GetAllItems();
            var ketQua = danhSach.Where(p => p.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase)).ToList();

            if (ketQua.Count > 0)
            {
                Console.WriteLine($"\n🔍 Tìm thấy {ketQua.Count} phiếu:");
                HienThiDanhSachPhieu(ketQua);
            }
            else
            {
                Console.WriteLine("❌ Không tìm thấy phiếu nào!");
            }
        }

        private void HienThiTatCaPhieu()
        {
            var danhSach = _queue.GetAllItems();
            if (danhSach.Count == 0)
            {
                Console.WriteLine("📭 Hàng đợi đang trống!");
                return;
            }

            HienThiDanhSachPhieu(danhSach);
        }

        private void CapNhatPhieu()
        {
            Console.Write("Nhập mã SV cần cập nhật: ");
            string maSV = Console.ReadLine();

            var danhSach = _queue.GetAllItems();
            var phieuCu = danhSach.FirstOrDefault(p => p.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));

            if (phieuCu == null)
            {
                Console.WriteLine("❌ Không tìm thấy phiếu!");
                return;
            }

            Console.WriteLine("\n--- Thông tin cũ ---");
            HienThiPhieu(phieuCu);

            Console.WriteLine("\n--- Nhập thông tin mới ---");
            PhieuGiaiQuyet phieuMoi = NhapThongTinPhieu();

            // Xóa phiếu cũ và thêm phiếu mới
            _queue = new PriorityQueue(); // Reset queue
            foreach (var phieu in danhSach.Where(p => p.MaSV != maSV))
            {
                _queue.Enqueue(phieu, phieu.MucDoUuTien);
            }

            int doUuTien = _priorityService.TinhMucDoUuTien(phieuMoi.NoiDung);
            _queue.Enqueue(phieuMoi, doUuTien);
            phieuMoi.MucDoUuTien = doUuTien;

            Console.WriteLine("✅ Cập nhật thành công!");
        }

        private void UuTienPhieu()
        {
            Console.Write("Nhập mã SV cần ưu tiên: ");
            string maSV = Console.ReadLine();

            var danhSach = _queue.GetAllItems();
            var phieu = danhSach.FirstOrDefault(p => p.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));

            if (phieu == null)
            {
                Console.WriteLine("❌ Không tìm thấy phiếu!");
                return;
            }

            // Tạo queue mới với phiếu được ưu tiên lên đầu
            var queueMoi = new PriorityQueue();
            queueMoi.Enqueue(phieu, 0); // Ưu tiên cao nhất

            foreach (var p in danhSach.Where(p => p.MaSV != maSV))
            {
                queueMoi.Enqueue(p, p.MucDoUuTien);
            }

            _queue = queueMoi;
            Console.WriteLine("✅ Đã ưu tiên phiếu lên đầu hàng đợi!");
        }

        private void ThongKeLop()
        {
            var danhSach = _queue.GetAllItems();
            if (danhSach.Count == 0)
            {
                Console.WriteLine("📭 Không có dữ liệu để thống kê!");
                return;
            }

            var thongKe = danhSach
                .GroupBy(p => p.Lop)
                .Select(g => new LopThongKe { Lop = g.Key, SoLuong = g.Count() })
                .OrderByDescending(x => x.SoLuong)
                .ToList();

            Console.WriteLine("\n📊 THỐNG KÊ THEO LỚP");
            Console.WriteLine(new string('=', 30));
            foreach (var item in thongKe)
            {
                Console.WriteLine($"{item.Lop,-15} : {item.SoLuong} phiếu");
            }

            var lopNhieuNhat = thongKe.First();
            Console.WriteLine($"\n🏆 Lớp có nhiều phiếu nhất: {lopNhieuNhat.Lop} ({lopNhieuNhat.SoLuong} phiếu)");
        }

        private void HienThi10PhieuDau()
        {
            var danhSach = _queue.GetAllItems();
            var top10 = danhSach.Take(10).ToList();

            Console.WriteLine($"\n📋 10 PHIẾU SẮP ĐƯỢC XỬ LÝ (Tổng: {danhSach.Count} phiếu)");
            HienThiDanhSachPhieu(top10);
        }

        private void GhiRaFile()
        {
            Console.Write("Nhập tên file (mặc định: ds_CapNhatPhieu.txt): ");
            string tenFile = Console.ReadLine();
            if (string.IsNullOrEmpty(tenFile))
                tenFile = "ds_CapNhatPhieu.txt";

            _fileService.GhiDanhSachRaFile(tenFile, _queue);
            Console.WriteLine($"✅ Đã ghi {_queue.Count()} phiếu ra file!");
        }

        private void ThongKeMucDoUuTien()
        {
            var danhSach = _queue.GetAllItems();
            if (danhSach.Count == 0)
            {
                Console.WriteLine("📭 Không có dữ liệu để thống kê!");
                return;
            }

            var thongKe = danhSach
                .GroupBy(p => p.MucDoUuTien)
                .Select(g => new MucDoThongKe
                {
                    MucDo = g.Key,
                    SoLuong = g.Count(),
                    TyLe = (double)g.Count() / danhSach.Count * 100
                })
                .OrderByDescending(x => x.MucDo)
                .ToList();

            Console.WriteLine("\n📈 THỐNG KÊ MỨC ĐỘ ƯU TIÊN");
            Console.WriteLine(new string('=', 50));
            foreach (var item in thongKe)
            {
                string tenMucDo = _priorityService.LayTenMucDoUuTien(item.MucDo);
                Console.WriteLine($"{tenMucDo,-15} : {item.SoLuong,3} phiếu ({item.TyLe:0.0}%)");
            }
        }

        private void HienThiDanhSachPhieu(List<PhieuGiaiQuyet> danhSach)
        {
            Console.WriteLine("\n" + new string('=', 130));
            Console.WriteLine($"{"Mã SV",-12} {"Tên SV",-20} {"Lớp",-15} {"Nội dung",-40} {"Ngày gửi",-12} {"Ưu tien",-10}");
            Console.WriteLine(new string('=', 130));

            foreach (var phieu in danhSach)
            {
                string mucDo = _priorityService.LayTenMucDoUuTien(phieu.MucDoUuTien);
                ConsoleColor color = GetMauTheoMucDoUuTien(phieu.MucDoUuTien);

                Console.ForegroundColor = color;
                Console.WriteLine($"{phieu.MaSV,-12} {phieu.TenSV,-20} {phieu.Lop,-15} {phieu.NoiDung,-40} {phieu.NgayGui,-12} {mucDo,-10}");
                Console.ResetColor();
            }
            Console.WriteLine(new string('=', 130));
            Console.WriteLine($"Tổng số: {danhSach.Count} phiếu");
        }

        private ConsoleColor GetMauTheoMucDoUuTien(int mucDo)
        {
            switch (mucDo)
            {
                case 2:
                    return ConsoleColor.Red;
                case 1:
                    return ConsoleColor.Yellow;
                default:
                    return ConsoleColor.Green;
            }
        }

        private void HienThiPhieu(PhieuGiaiQuyet phieu)
        {
            if (phieu == null) return;

            string mucDo = _priorityService.LayTenMucDoUuTien(phieu.MucDoUuTien);
            Console.WriteLine($"Mã SV     : {phieu.MaSV}");
            Console.WriteLine($"Tên SV    : {phieu.TenSV}");
            Console.WriteLine($"Lớp       : {phieu.Lop}");
            Console.WriteLine($"Nội dung  : {phieu.NoiDung}");
            Console.WriteLine($"Ngày gửi  : {phieu.NgayGui}");
            Console.WriteLine($"Ưu tiên   : {mucDo}");
        }

        // Helper classes for statistics
        private class LopThongKe
        {
            public string Lop { get; set; }
            public int SoLuong { get; set; }
        }

        private class MucDoThongKe
        {
            public int MucDo { get; set; }
            public int SoLuong { get; set; }
            public double TyLe { get; set; }
        }
    }
}