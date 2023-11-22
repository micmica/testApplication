using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApplication.Migrations
{
    public partial class Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployee_Companies_CompaniesId",
                table: "CompanyEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployee_Employees_EmployeesId",
                table: "CompanyEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyEmployee",
                table: "CompanyEmployee");

            migrationBuilder.RenameTable(
                name: "CompanyEmployee",
                newName: "CompanyEmployees");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyEmployee_EmployeesId",
                table: "CompanyEmployees",
                newName: "IX_CompanyEmployees_EmployeesId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyEmployees",
                table: "CompanyEmployees",
                columns: new[] { "CompaniesId", "EmployeesId" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_Companies_CompaniesId",
                table: "CompanyEmployees",
                column: "CompaniesId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployees_Employees_EmployeesId",
                table: "CompanyEmployees",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_Companies_CompaniesId",
                table: "CompanyEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyEmployees_Employees_EmployeesId",
                table: "CompanyEmployees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Email",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Companies_Name",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyEmployees",
                table: "CompanyEmployees");

            migrationBuilder.RenameTable(
                name: "CompanyEmployees",
                newName: "CompanyEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyEmployees_EmployeesId",
                table: "CompanyEmployee",
                newName: "IX_CompanyEmployee_EmployeesId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyEmployee",
                table: "CompanyEmployee",
                columns: new[] { "CompaniesId", "EmployeesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployee_Companies_CompaniesId",
                table: "CompanyEmployee",
                column: "CompaniesId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyEmployee_Employees_EmployeesId",
                table: "CompanyEmployee",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
