using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class FixSubject2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTeacher_teachers_SubjectId",
                table: "SubjectTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTeacher_subjects_TeacherId",
                table: "SubjectTeacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectTeacher",
                table: "SubjectTeacher");

            migrationBuilder.RenameTable(
                name: "SubjectTeacher",
                newName: "subjects_teachers");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectTeacher_TeacherId",
                table: "subjects_teachers",
                newName: "IX_subjects_teachers_TeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_subjects_teachers",
                table: "subjects_teachers",
                columns: new[] { "SubjectId", "TeacherId" });

            migrationBuilder.AddForeignKey(
                name: "FK_subjects_teachers_subjects_SubjectId",
                table: "subjects_teachers",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subjects_teachers_teachers_TeacherId",
                table: "subjects_teachers",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subjects_teachers_subjects_SubjectId",
                table: "subjects_teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_subjects_teachers_teachers_TeacherId",
                table: "subjects_teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_subjects_teachers",
                table: "subjects_teachers");

            migrationBuilder.RenameTable(
                name: "subjects_teachers",
                newName: "SubjectTeacher");

            migrationBuilder.RenameIndex(
                name: "IX_subjects_teachers_TeacherId",
                table: "SubjectTeacher",
                newName: "IX_SubjectTeacher_TeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectTeacher",
                table: "SubjectTeacher",
                columns: new[] { "SubjectId", "TeacherId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTeacher_teachers_SubjectId",
                table: "SubjectTeacher",
                column: "SubjectId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTeacher_subjects_TeacherId",
                table: "SubjectTeacher",
                column: "TeacherId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
