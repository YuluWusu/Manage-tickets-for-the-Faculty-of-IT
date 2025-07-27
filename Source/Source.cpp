#include <conio.h>
#include <stdio.h>
#include <string.h>
#include <windows.h>
struct NgayThangNam
{
	int ngay, thang, nam;
};
struct PhieuGiaiQuyet
{
	char maSV[11];
	char tenSV[51];
	char lop[21];
	char noiDung[201];
	NgayThangNam ntn;
};
typedef PhieuGiaiQuyet Itemtype;
struct QNode {
	Itemtype info;
	int priority;
	QNode* next;
};
struct PriorityQueue
{
	QNode* Head;
};
QNode* taoNode(Itemtype& x, int prio)
{
	QNode* p = new QNode;
	if (p == NULL)
	{
		printf("Khong du bo nho!\n");
		getch();
		return NULL;
	}
	p->info = x;
	p->priority = prio;
	p->next = NULL;
	return p;
}
void khoiTaoPQ(PriorityQueue& pq)
{
	pq.Head = NULL;
}
int kiemTraRong(PriorityQueue& pq)
{
	if (pq.Head == NULL)
		return 1;
	return 0;
}
int laNamNhuan(int nam) {
	return (nam % 4 == 0 && nam % 100 != 0) || (nam % 400 == 0);
}
int soNgayTrongThang(int thang, int nam) {
	int ngayTrongThang[] = { 31,28,31,30,31,30,31,31,30,31,30,31 };
	if (thang == 2 && laNamNhuan(nam)) return 29;
	return ngayTrongThang[thang - 1];
}
void nhapNTN(NgayThangNam& ntn)
{
	do {
		printf("Nhap vao ngay/ thang/ nam: ");
		scanf("%d%d%d", &ntn.ngay, &ntn.thang, &ntn.nam);
		if (ntn.thang < 1 || ntn.thang > 12 || ntn.ngay < 1 || ntn.ngay > soNgayTrongThang(ntn.thang, ntn.nam)) {
			printf("Ngay thang nam khong hop le! Vui long nhap lai.\n");
		}
	} while (ntn.thang < 1 || ntn.thang > 12 || ntn.ngay < 1 || ntn.ngay > soNgayTrongThang(ntn.thang, ntn.nam));
}
void nhapPhieuGiaiQuyet(Itemtype& ptq)
{
	printf("Nhap vao ma sinh vien: ");
	getchar();
	gets_s(ptq.maSV, sizeof(ptq.maSV));
	printf("Nhap vao ten sinh vien: ");
	fflush(stdin);
	gets_s(ptq.tenSV, sizeof(ptq.tenSV));
	printf("Nhap vao ten lop: ");
	fflush(stdin);
	gets_s(ptq.lop, sizeof(ptq.lop));
	printf("Nhap vao noi dung can xu ly: ");
	fflush(stdin);
	gets_s(ptq.noiDung, sizeof(ptq.noiDung));
	nhapNTN(ptq.ntn);
}
void xuatNTN(NgayThangNam ntn)
{
	printf("(%02d/%02d/%04d)", ntn.ngay, ntn.thang, ntn.nam);
}
void xuatPhieuGiaiQuyet(Itemtype ptq)
{
	printf("%-20s   %-20s   %-30s   %-40s   ", ptq.maSV, ptq.tenSV, ptq.lop, ptq.noiDung);
	xuatNTN(ptq.ntn);
	printf("\n");
}
int xetMucDoUuTien(const char* noiDung)
{
	const char* khanCap[] = { "bao mat", "mat khau", "he thong", "DDOS", "lo hong", "xac thuc", "khoa tai khoan", "mat ket noi","loi web","bi hack","khong the truy cap","sap web","loi he thong","he thong co loi","khong thi duoc","hoan thi", "khan cap","gap","cam thi","huy","khong the","khong hoat dong","khong chay","khong thanh toan duoc","khong truy cap","loi nang" };
	const char* quanTrong[] = { "gia han", "nop bai", "cap nhat", "thuc tap", "xac nhan", "ho so", "ket qua","doi lich","loi nang","mat ket noi","trung lich", "can giai quyet","thac mac","hoi ve","gap ve hoc phi","ho tro" };

	for (int i = 0; i < sizeof(khanCap) / sizeof(khanCap[0]); i++) {
		if (strstr(noiDung, khanCap[i]) != NULL) {
			return 2;
		}
	}
	for (int i = 0; i < sizeof(quanTrong) / sizeof(quanTrong[0]); i++) {
		if (strstr(noiDung, quanTrong[i]) != NULL) {
			return 1;
		}
	}
	return 0;
}
int tinhDoUuTien(Itemtype x) {
	return xetMucDoUuTien(x.noiDung);
}
void docNgay(FILE* f, NgayThangNam& d)
{
	fscanf(f, "%d/%d/%d%*c", &d.ngay, &d.thang, &d.nam);
}
void doc1Phieu(FILE* f, Itemtype& x)
{
	fscanf(f, "%[^|]|%[^|]|%[^|]|%[^|]|", x.maSV, x.tenSV, x.lop, x.noiDung);
	docNgay(f, x.ntn);
}
void chenTheoUuTien(PriorityQueue& q, Itemtype x) {
	int prio = tinhDoUuTien(x);
	QNode* node = taoNode(x, prio);
	if (q.Head == NULL || prio < q.Head->priority)
	{
		node->next = q.Head;
		q.Head = node;
		return;
	}

	QNode* prev = q.Head;
	while (prev->next != NULL && prio >= prev->next->priority) {
		prev = prev->next;
	}
	node->next = prev->next;
	prev->next = node;
}
void docDanhSachTuFile(const char* tenFile, PriorityQueue& q) {
	FILE* f = fopen(tenFile, "r");
	if (!f) {
		printf("Khong mo duoc file.\n");
		return;
	}
	int n;
	fscanf(f, "%d\n", &n);
	for (int i = 0; i < n; ++i) {
		Itemtype p;
		doc1Phieu(f, p);
		chenTheoUuTien(q, p);
	}
	printf("Doc file thanh cong!!\n");
	fclose(f);
}
void xuatTatCaPhieu(PriorityQueue q) {
	if (kiemTraRong(q)) {
		printf("Hang doi dang rong.\n");
		return;
	}

	printf("\n%-20s   %-20s   %-30s   %-20s   %30s\n",
		"Ma SV", "Ten SV", "Lop", "Noi Dung", "Ngay Gui");
	printf("-----------------------------------------------------------------------------------------------------------------------------------\n");

	QNode* p = q.Head;
	while (p != NULL) {
		xuatPhieuGiaiQuyet(p->info);
		p = p->next;
	}
}
void nhapDanhSach(PriorityQueue& q)
{
	HANDLE Hconsole = GetStdHandle(STD_OUTPUT_HANDLE);
	int n;
	do
	{
		printf("Nhap vao so luong phieu: ");
		scanf("%d", &n);
	} while (n <= 0);
	Itemtype x;
	for (int i = 0; i < n; i++)
	{
		SetConsoleTextAttribute(Hconsole, 8);
		printf("Phieu %d:\n", i + 1);
		SetConsoleTextAttribute(Hconsole, 11);
		nhapPhieuGiaiQuyet(x);
		chenTheoUuTien(q, x);
	}
}
void timKiemPhieu(PriorityQueue pq, const char* maSVTK)
{
	int found = 0;
	QNode* p = pq.Head;
	while (p != NULL) {
		if (strcmp(p->info.maSV, maSVTK) == 0) {
			printf("Ma sinh vien: %s\n", p->info.maSV);
			printf("Ten sinh vien: %s\n", p->info.tenSV);
			printf("Lop: %s\n", p->info.lop);
			printf("Noi dung cong viec: %s\n", p->info.noiDung);
			printf("Ngay thang nam: ");
			xuatNTN(p->info.ntn);
			printf("\n");
			found = 1;
			break;
		}
		p = p->next;
	}
	if (!found) {
		printf("Khong tim thay phieu voi ma %s\n", maSVTK);
	}
}
void xoaPhieu(PriorityQueue& pq)
{
	if (kiemTraRong(pq)) {
		printf("Hang doi rong, khong co gi de xoa!\n");
		return;
	}
	QNode* temp = pq.Head;
	pq.Head = pq.Head->next;
	delete temp;
	printf("Da xoa mot phieu ra khoi hang doi!\n");
}
void capNhatPhieu(PriorityQueue& pq, const char* maSVCu)
{
	HANDLE Hconsole = GetStdHandle(STD_OUTPUT_HANDLE);
	SetConsoleTextAttribute(Hconsole, 11);
	QNode* prev = NULL;
	QNode* curr = pq.Head;

	while (curr != NULL)
	{
		if (strcmp(curr->info.maSV, maSVCu) == 0)
		{
			SetConsoleTextAttribute(Hconsole, 14);
			printf("--- Thong tin cu ---\n");
			SetConsoleTextAttribute(Hconsole, 11);
			xuatPhieuGiaiQuyet(curr->info);
			SetConsoleTextAttribute(Hconsole, 14);
			printf("--- Nhap thong tin moi ---\n");
			SetConsoleTextAttribute(Hconsole, 11);
			Itemtype moi;
			nhapPhieuGiaiQuyet(moi);
			if (prev == NULL) pq.Head = curr->next;
			else prev->next = curr->next;
			delete curr;
			chenTheoUuTien(pq, moi);
			SetConsoleTextAttribute(Hconsole, 14);
			printf("Cap nhat va sap xep lai thanh cong!\n");
			return;
		}
		prev = curr;
		curr = curr->next;
	}

	printf("Khong tim thay ma sinh vien trong hang doi!\n");
}

void duaPhieuLenDau(PriorityQueue& pq, const char* maSV)
{
	if (pq.Head == NULL)
	{
		printf("Hang doi dang rong!\n");
		return;
	}
	HANDLE Hconsole = GetStdHandle(STD_OUTPUT_HANDLE);
	SetConsoleTextAttribute(Hconsole, 14);
	QNode* prev = NULL;
	QNode* curr = pq.Head;

	while (curr != NULL && strcmp(curr->info.maSV, maSV) != 0)
	{
		prev = curr;
		curr = curr->next;
	}

	if (curr == NULL)
	{
		printf("Khong tim thay phieu voi ma SV nay!\n");
		return;
	}

	if (prev == NULL)
	{
		printf("Phieu da o dau danh sach!\n");
		return;
	}

	prev->next = curr->next;
	curr->next = pq.Head;
	pq.Head = curr;

	printf("Da dua phieu len dau danh sach!\n");
}
void thongKeLopNhieuPhieuNhat(PriorityQueue pq)
{
	if (kiemTraRong(pq)) {
		printf("Hang doi rong!\n");
		return;
	}

	char dsLop[100][21];
	int demLop[100] = { 0 };
	int soLop = 0;

	QNode* p = pq.Head;
	while (p != NULL) {
		bool daCo = false;
		for (int i = 0; i < soLop; i++) {
			if (strcmp(dsLop[i], p->info.lop) == 0) {
				demLop[i]++;
				daCo = true;
				break;
			}
		}
		if (!daCo) {
			strcpy(dsLop[soLop], p->info.lop);
			demLop[soLop] = 1;
			soLop++;
		}
		p = p->next;
	}
	int maxIndex = 0;
	for (int i = 1; i < soLop; i++) {
		if (demLop[i] > demLop[maxIndex]) {
			maxIndex = i;
		}
	}
	printf("Lop co nhieu viec can xu ly nhat la: %s (%d phieu)\n", dsLop[maxIndex], demLop[maxIndex]);
}
void in10PhieuDauTien(PriorityQueue pq)
{
	if (kiemTraRong(pq)) {
		printf("Hang doi rong!\n");
		return;
	}
	printf("\n%-20s   %-20s   %-30s   %-20s   %30s\n",
		"Ma SV", "Ten SV", "Lop", "Noi Dung", "Ngay Gui");
	printf("-----------------------------------------------------------------------------------------------------------------------------------\n");

	int dem = 0;
	QNode* p = pq.Head;
	while (p != NULL && dem < 10) {
		xuatPhieuGiaiQuyet(p->info);
		p = p->next;
		dem++;
	}
}
void ghiDanhSachRaFile(const char* tenFile, PriorityQueue pq)
{
	FILE* f = fopen(tenFile, "w");
	if (!f) {
		printf("Khong the mo file de ghi.\n");
		return;
	}

	QNode* p = pq.Head;
	int dem = 0;
	while (p != NULL) {
		dem++;
		p = p->next;
	}
	fprintf(f, "%d\n", dem);

	p = pq.Head;
	while (p != NULL) {
		fprintf(f, "%s|%s|%s|%s|%02d/%02d/%04d\n",
			p->info.maSV, p->info.tenSV, p->info.lop, p->info.noiDung,
			p->info.ntn.ngay, p->info.ntn.thang, p->info.ntn.nam);
		p = p->next;
	}

	fclose(f);
	printf("Da ghi danh sach ra file '%s' thanh cong!\n", tenFile);
}
void menu()
{
	printf("--------------------------------------------------------------------------\n");
	printf("-----CHUONG TRINH GIAI QUYET THAC MAC CUA SINH VIEN HUIT KHOA CNTT--------\n");
	printf("--------------------------------------------------------------------------\n");
	printf("|1. Tao danh sach hang doi chua thong tin cac phieu xu ly (Nhap ban phim)\n");
	printf("|2. Tao danh sach hang doi chua thong tin cac phieu xu ly (Nhap tu FILE)\n");
	printf("|3. Them mot phieu giai quyet cong viec vao hang doi\n");
	printf("|4. Xoa mot phan tu ra khoi hang doi\n");
	printf("|5. Tim kiem mot phieu xu ly trong hang doi\n");
	printf("|6. In danh sach tat ca cac phieu trong hang doi\n");
	printf("|7. Cap nhat thong tin cua mot phieu xu ly trong hang doi\n");
	printf("|8. Uu tien dua mot phieu xu ly len dau danh sach\n");
	printf("|9. Cho biet sinh vien lop nao co nhieu viec can khoa xu ly nhat\n");
	printf("|10.Cho biet danh sach cua 10 viec sap sua duoc xu ly\n");
	printf("|11.Ghi danh sach hien tai ra FILE\n");
	printf("|0. Thoat chuong trinh\n");
	printf("--------------------------------------------------------------------------\n");
}
void thaoTacMenu()
{
	HANDLE Hconsole = GetStdHandle(STD_OUTPUT_HANDLE);
	SetConsoleTextAttribute(Hconsole, 11);
	int chon;
	PriorityQueue q;
	khoiTaoPQ(q);
	int stop = 0;
	menu();
	do
	{
		SetConsoleTextAttribute(Hconsole, 12);
		printf("Nhap vao chuc nang: ");
		scanf("%d", &chon);
		switch (chon)
		{
		case 1:
		{
			SetConsoleTextAttribute(Hconsole, 14);
			printf("Ban chon Tao danh sach hang doi (Nhap ban phim)\n");
			nhapDanhSach(q);
			xuatTatCaPhieu(q);
			break;
		}
		case 2:
		{
			SetConsoleTextAttribute(Hconsole, 14);
			printf("Ban chon Tao danh sach hang doi (Nhap tu FILE)\n");
			docDanhSachTuFile("ds_PhieuGiaiQuyet.txt", q);
			break;
		}
		case 3:
		{
			SetConsoleTextAttribute(Hconsole, 14);
			printf("Ban chon Them mot phieu giai quyet cong viec\n");
			Itemtype x;
			nhapPhieuGiaiQuyet(x);
			chenTheoUuTien(q, x);
			SetConsoleTextAttribute(Hconsole, 11);
			printf("Them phieu thanh cong!\n");
			break;
		}
		case 4:
		{
			SetConsoleTextAttribute(Hconsole, 14);
			printf("Ban chon Xoa mot phieu xu ly\n");
			xoaPhieu(q);
			break;
		}
		case 5:
		{
			SetConsoleTextAttribute(Hconsole, 14);
			char maSVTK[11];
			printf("Ban chon Tim kiem mot phieu giai quyet cong viec\n");
			printf("Nhap ma so sinh vien de tim kiem phieu: ");
			getchar();
			gets_s(maSVTK, sizeof(maSVTK));
			SetConsoleTextAttribute(Hconsole, 11);
			timKiemPhieu(q, maSVTK);
			break;
		}
		case 6:
		{
			SetConsoleTextAttribute(Hconsole, 14);
			printf("Ban chon In tat ca cac phieu trong hang doi\n");
			SetConsoleTextAttribute(Hconsole, 11);
			xuatTatCaPhieu(q);
			break;
		}
		case 7:
		{
			SetConsoleTextAttribute(Hconsole, 14);
			printf("Ban chon Cap nhat thong tin cua mot phieu trong hang doi\n");
			char maCapNhat[11];
			printf("Nhap ma sinh vien can cap nhat: ");
			scanf(" %[^\n]", maCapNhat);
			SetConsoleTextAttribute(Hconsole, 11);
			capNhatPhieu(q, maCapNhat);
			break;
			break;
		}
		case 8:
		{
			SetConsoleTextAttribute(Hconsole, 14);
			printf("Ban chon Uu tien dua mot phieu xu ly len dau danh sach\n");
			char maUuTien[11];
			printf("Nhap ma sinh vien can uu tien: ");
			getchar();
			gets_s(maUuTien, sizeof(maUuTien));
			duaPhieuLenDau(q, maUuTien);
			break;
		}
		case 9:
		{
			SetConsoleTextAttribute(Hconsole, 14);
			printf("Ban chon Cho biet sinh vien lop nao co nhieu viec can khoa xu ly nhat\n");
			SetConsoleTextAttribute(Hconsole, 11);
			thongKeLopNhieuPhieuNhat(q);
			break;
		}
		case 10:
		{
			SetConsoleTextAttribute(Hconsole, 14);
			printf("Ban chon Cho biet danh sach cua 10 viec sap sua duoc xu ly\n");
			SetConsoleTextAttribute(Hconsole, 11);
			in10PhieuDauTien(q);
			break;
		}
		case 11:
		{
			SetConsoleTextAttribute(Hconsole, 14);
			printf("Ban chon Ghi danh sach ra FILE\n");
			SetConsoleTextAttribute(Hconsole, 11);
			ghiDanhSachRaFile("ds_CapNhatPhieu.txt", q);
			break;
		}
		case 0:
		{
			SetConsoleTextAttribute(Hconsole, 7);
			printf("THOAT CHUONG TRINH");
			stop = 1;
			break;
		}
		default:
		{
			SetConsoleTextAttribute(Hconsole, 7);
			printf("Chuc nang khong hop le, ban vui long nhap lai!!\n");
		}
		}
	} while (stop == 0);
}
void main()
{
	thaoTacMenu();
	getch();
}