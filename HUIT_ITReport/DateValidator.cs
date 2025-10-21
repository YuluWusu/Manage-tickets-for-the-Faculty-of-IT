using System;

namespace HUIT_PriorityQueue.Utilities
{
    public static class DateValidator
    {
        public static bool LaNamNhuan(int nam)
        {
            return (nam % 4 == 0 && nam % 100 != 0) || (nam % 400 == 0);
        }

        public static int SoNgayTrongThang(int thang, int nam)
        {
            if (thang < 1 || thang > 12)
                return 0;

            int[] ngayTrongThang = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            if (thang == 2 && LaNamNhuan(nam))
                return 29;

            return ngayTrongThang[thang - 1];
        }

        public static bool LaNgayHopLe(int ngay, int thang, int nam)
        {
            if (thang < 1 || thang > 12)
                return false;

            if (ngay < 1 || ngay > SoNgayTrongThang(thang, nam))
                return false;

            if (nam < 2000 || nam > 2100) // Giới hạn năm hợp lý
                return false;

            return true;
        }

        public static void NhapNgayThangNam(string prompt, out int ngay, out int thang, out int nam)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Ngày tháng không được để trống!");
                    continue;
                }

                string[] parts = input.Split('/');
                if (parts.Length == 3 &&
                    int.TryParse(parts[0], out ngay) &&
                    int.TryParse(parts[1], out thang) &&
                    int.TryParse(parts[2], out nam))
                {
                    if (LaNgayHopLe(ngay, thang, nam))
                        break;
                    else
                        Console.WriteLine("Ngày tháng không hợp lệ! Vui lòng nhập lại.");
                }
                else
                {
                    Console.WriteLine("Định dạng không hợp lệ! Sử dụng dd/mm/yyyy.");
                }
            }
        }
    }
}