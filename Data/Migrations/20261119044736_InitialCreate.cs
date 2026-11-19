using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CuaHang.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMucs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenDanhMuc = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    HoatDong = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    NgayTao = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HoTen = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    MatKhauHash = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    SoDienThoai = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    DiaChi = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    VaiTro = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, defaultValue: "KhachHang"),
                    TrangThai = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    NgayTao = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaVach = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TenSP = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    GiaNhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    GiamGiaPhanTram = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    SoLuongTon = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    DonViTinh = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, defaultValue: "Cái"),
                    HanSuDung = table.Column<DateTime>(type: "TEXT", nullable: true),
                    HinhAnh = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    DanhMucId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrangThai = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    NgayTao = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.Id);
                    table.CheckConstraint("CK_SanPhams_GiaBan", "[GiaBan] >= 0");
                    table.CheckConstraint("CK_SanPhams_GiamGiaPhanTram", "[GiamGiaPhanTram] >= 0 AND [GiamGiaPhanTram] <= 100");
                    table.CheckConstraint("CK_SanPhams_GiaNhap", "[GiaNhap] >= 0");
                    table.CheckConstraint("CK_SanPhams_SoLuongTon", "[SoLuongTon] >= 0");
                    table.ForeignKey(
                        name: "FK_SanPhams_DanhMucs_DanhMucId",
                        column: x => x.DanhMucId,
                        principalTable: "DanhMucs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaDon = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    KenhBan = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, defaultValue: "Online"),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    TienKhachDua = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    TienThua = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    TrangThai = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false, defaultValue: "ChoXacNhan"),
                    PhuongThucTT = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false, defaultValue: "COD"),
                    KhachHangId = table.Column<int>(type: "INTEGER", nullable: true),
                    NhanVienId = table.Column<int>(type: "INTEGER", nullable: true),
                    TenNguoiNhan = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    SoDienThoaiNhan = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    DiaChiGiaoHang = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    GhiChu = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.Id);
                    table.CheckConstraint("CK_DonHangs_TienKhachDua", "[TienKhachDua] >= 0");
                    table.CheckConstraint("CK_DonHangs_TienThua", "[TienThua] >= 0");
                    table.CheckConstraint("CK_DonHangs_TongTien", "[TongTien] >= 0");
                    table.ForeignKey(
                        name: "FK_DonHangs_NguoiDungs_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonHangs_NguoiDungs_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhieuNhapKhos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaPhieu = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    NhaCungCap = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    TongTienNhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    NguoiTaoId = table.Column<int>(type: "INTEGER", nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    GhiChu = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhapKhos", x => x.Id);
                    table.CheckConstraint("CK_PhieuNhapKhos_TongTienNhap", "[TongTienNhap] >= 0");
                    table.ForeignKey(
                        name: "FK_PhieuNhapKhos_NguoiDungs_NguoiTaoId",
                        column: x => x.NguoiTaoId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DonHangId = table.Column<int>(type: "INTEGER", nullable: false),
                    SanPhamId = table.Column<int>(type: "INTEGER", nullable: false),
                    SoLuong = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHangs", x => x.Id);
                    table.CheckConstraint("CK_ChiTietDonHangs_DonGia", "[DonGia] >= 0");
                    table.CheckConstraint("CK_ChiTietDonHangs_SoLuong", "[SoLuong] >= 1");
                    table.CheckConstraint("CK_ChiTietDonHangs_ThanhTien", "[ThanhTien] >= 0");
                    table.ForeignKey(
                        name: "FK_ChiTietDonHangs_DonHangs_DonHangId",
                        column: x => x.DonHangId,
                        principalTable: "DonHangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHangs_SanPhams_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "SanPhams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhieuNhaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PhieuNhapId = table.Column<int>(type: "INTEGER", nullable: false),
                    SanPhamId = table.Column<int>(type: "INTEGER", nullable: false),
                    SoLuong = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    GiaNhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhieuNhaps", x => x.Id);
                    table.CheckConstraint("CK_ChiTietPhieuNhaps_GiaNhap", "[GiaNhap] >= 0");
                    table.CheckConstraint("CK_ChiTietPhieuNhaps_SoLuong", "[SoLuong] >= 1");
                    table.CheckConstraint("CK_ChiTietPhieuNhaps_ThanhTien", "[ThanhTien] >= 0");
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuNhaps_PhieuNhapKhos_PhieuNhapId",
                        column: x => x.PhieuNhapId,
                        principalTable: "PhieuNhapKhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuNhaps_SanPhams_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "SanPhams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHangs_DonHangId",
                table: "ChiTietDonHangs",
                column: "DonHangId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHangs_SanPhamId",
                table: "ChiTietDonHangs",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuNhaps_PhieuNhapId",
                table: "ChiTietPhieuNhaps",
                column: "PhieuNhapId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuNhaps_SanPhamId",
                table: "ChiTietPhieuNhaps",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhMucs_TenDanhMuc",
                table: "DanhMucs",
                column: "TenDanhMuc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_KhachHangId",
                table: "DonHangs",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_MaDon",
                table: "DonHangs",
                column: "MaDon",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_NhanVienId",
                table: "DonHangs",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_Email",
                table: "NguoiDungs",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhapKhos_MaPhieu",
                table: "PhieuNhapKhos",
                column: "MaPhieu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhapKhos_NguoiTaoId",
                table: "PhieuNhapKhos",
                column: "NguoiTaoId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_DanhMucId",
                table: "SanPhams",
                column: "DanhMucId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_MaVach",
                table: "SanPhams",
                column: "MaVach",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonHangs");

            migrationBuilder.DropTable(
                name: "ChiTietPhieuNhaps");

            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.DropTable(
                name: "PhieuNhapKhos");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "NguoiDungs");

            migrationBuilder.DropTable(
                name: "DanhMucs");
        }
    }
}
