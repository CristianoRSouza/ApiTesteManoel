using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LojaManoelApi.Migrations
{
    /// <inheritdoc />
    public partial class f : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dimensoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Altura = table.Column<double>(type: "float", nullable: false),
                    Largura = table.Column<double>(type: "float", nullable: false),
                    Comprimento = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Papeis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PapelToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Papeis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.PedidoId);
                });

            migrationBuilder.CreateTable(
                name: "Caixas",
                columns: table => new
                {
                    CaixaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DimensoesId = table.Column<int>(type: "int", nullable: false),
                    VolumeDisponivel = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixas", x => x.CaixaId);
                    table.ForeignKey(
                        name: "FK_Caixas_Dimensoes_DimensoesId",
                        column: x => x.DimensoesId,
                        principalTable: "Dimensoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PapelId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Papeis_PapelId",
                        column: x => x.PapelId,
                        principalTable: "Papeis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    ProdutoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DimensaoId = table.Column<int>(type: "int", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    CaixaId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_Produto_Caixas_CaixaId",
                        column: x => x.CaixaId,
                        principalTable: "Caixas",
                        principalColumn: "CaixaId");
                    table.ForeignKey(
                        name: "FK_Produto_Dimensoes_DimensaoId",
                        column: x => x.DimensaoId,
                        principalTable: "Dimensoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produto_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Papeis",
                columns: new[] { "Id", "PapelToken" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Manager" },
                    { 3, "Client" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caixas_DimensoesId",
                table: "Caixas",
                column: "DimensoesId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CaixaId",
                table: "Produto",
                column: "CaixaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_DimensaoId",
                table: "Produto",
                column: "DimensaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_PedidoId",
                table: "Produto",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PapelId",
                table: "Usuarios",
                column: "PapelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Caixas");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Papeis");

            migrationBuilder.DropTable(
                name: "Dimensoes");
        }
    }
}
