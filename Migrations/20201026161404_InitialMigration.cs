using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace ExperimentToolApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    MaterialPhoto = table.Column<string>(nullable: false),
                    AdditionalInformations = table.Column<string>(nullable: false),
                    ChemicalComposition = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompressionTests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MaterialId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Company = table.Column<string>(nullable: false),
                    TestStandard = table.Column<string>(nullable: false),
                    MachineInfo = table.Column<string>(nullable: false),
                    InitialForce = table.Column<decimal>(nullable: false),
                    YoungModuleSpeed = table.Column<decimal>(nullable: false),
                    TestSpeed = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompressionTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompressionTests_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TensileTests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MaterialId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    TestAuthor = table.Column<string>(nullable: false),
                    MachineInfo = table.Column<string>(nullable: false),
                    InitialForce = table.Column<decimal>(nullable: false),
                    CompressionModuleSpeed = table.Column<decimal>(nullable: false),
                    YeldPointSpeed = table.Column<decimal>(nullable: false),
                    TestSpeed = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TensileTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TensileTests_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextureAnalyses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    MaterialId = table.Column<int>(nullable: false),
                    EbsdPhoto = table.Column<string>(nullable: false),
                    EbsdDescription = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextureAnalyses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextureAnalyses_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompressionResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CompressionTestId = table.Column<int>(nullable: false),
                    AttemptNumber = table.Column<int>(nullable: false),
                    RelativeReduction = table.Column<decimal>(nullable: false),
                    StandardForce = table.Column<decimal>(nullable: false),
                    PlasticRelativeReduction = table.Column<decimal>(nullable: false),
                    XCorrectRelativeReduction = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompressionResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompressionResults_CompressionTests_CompressionTestId",
                        column: x => x.CompressionTestId,
                        principalTable: "CompressionTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TensileResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TensileTestId = table.Column<int>(nullable: false),
                    AttemptNumber = table.Column<int>(nullable: false),
                    Elongation = table.Column<decimal>(nullable: false),
                    StandardForce = table.Column<decimal>(nullable: false),
                    TrueStress = table.Column<decimal>(nullable: false),
                    PlasticElongation = table.Column<decimal>(nullable: false),
                    XCorrectElongation = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TensileResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TensileResults_TensileTests_TensileTestId",
                        column: x => x.TensileTestId,
                        principalTable: "TensileTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompressionResults_CompressionTestId",
                table: "CompressionResults",
                column: "CompressionTestId");

            migrationBuilder.CreateIndex(
                name: "IX_CompressionTests_MaterialId",
                table: "CompressionTests",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_TensileResults_TensileTestId",
                table: "TensileResults",
                column: "TensileTestId");

            migrationBuilder.CreateIndex(
                name: "IX_TensileTests_MaterialId",
                table: "TensileTests",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_TextureAnalyses_MaterialId",
                table: "TextureAnalyses",
                column: "MaterialId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompressionResults");

            migrationBuilder.DropTable(
                name: "TensileResults");

            migrationBuilder.DropTable(
                name: "TextureAnalyses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CompressionTests");

            migrationBuilder.DropTable(
                name: "TensileTests");

            migrationBuilder.DropTable(
                name: "Materials");
        }
    }
}
