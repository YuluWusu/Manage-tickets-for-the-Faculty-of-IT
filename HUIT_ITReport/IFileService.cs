using HUIT_PriorityQueue.Models;
using System.Collections.Generic;

namespace HUIT_PriorityQueue.Services
{
    public interface IFileService
    {
        List<PhieuGiaiQuyet> DocDanhSachTuFile(string tenFile);
        void GhiDanhSachRaFile(string tenFile, List<PhieuGiaiQuyet> danhSach);
        void GhiDanhSachRaFile(string tenFile, PriorityQueue queue);
    }
}