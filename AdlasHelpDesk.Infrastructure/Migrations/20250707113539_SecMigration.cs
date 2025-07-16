using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdlasHelpDesk.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ticket_CustomerId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Customer",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CustomerId",
                table: "Ticket",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ticket_CustomerId",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customer",
                newName: "UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CustomerId",
                table: "Ticket",
                column: "CustomerId",
                unique: true);
        }
    }
}
