using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Mercaderia",
                keyColumn: "MercaderiaId",
                keyValue: 4,
                column: "Imagen",
                value: "https://elmundoenrecetas.s3.amazonaws.com/uploads/recipe/main_image/89/IMG_4520_1200px_.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Mercaderia",
                keyColumn: "MercaderiaId",
                keyValue: 4,
                column: "Imagen",
                value: "https://hoycomemossano.com/wp-content/uploads/2019/02/Bocadillo-salmon-queso-y-avo-I.jpg");
        }
    }
}
