using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO products (Name, Price, Description, Stock, ImageURL, CategoryId)
                VALUES ('Caderno', 7.55, 'Caderno de 10 matérias', 10, 'caderno.jpg', 1);");

            migrationBuilder.Sql(@"
                INSERT INTO products (Name, Price, Description, Stock, ImageURL, CategoryId)
                VALUES ('Lápis', 3.45, 'Lápis Preto', 20, 'lapis.jpg', 1);");

            migrationBuilder.Sql(@"
                INSERT INTO products (Name, Price, Description, Stock, ImageURL, CategoryId)
                VALUES ('Clips', 5.33, 'Clips para papel', 50, 'clips.jpg', 2);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM products");
        }
    }
}
