using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Api.Migrations
{
    public partial class FixTeachers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teachers_subjects_SubjectId",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_teachers_SubjectId",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "teachers");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "subjects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_subjects_TeacherId",
                table: "subjects",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_subjects_teachers_TeacherId",
                table: "subjects",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subjects_teachers_TeacherId",
                table: "subjects");

            migrationBuilder.DropIndex(
                name: "IX_subjects_TeacherId",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "subjects");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "teachers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_teachers_SubjectId",
                table: "teachers",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_subjects_SubjectId",
                table: "teachers",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
