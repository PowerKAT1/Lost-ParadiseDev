﻿#if LPP_Sponsors
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Content.Server.Database.Migrations.Postgres
{
    /// <inheritdoc />
    public partial class Sponsors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sponsors",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tier = table.Column<int>(type: "integer", nullable: false),
                    ooccolor = table.Column<string>(type: "text", nullable: false),
                    have_priority_join = table.Column<bool>(type: "boolean", nullable: false),
                    allowed_markings = table.Column<string>(type: "text", nullable: false),
                    expire_date = table.Column<DateTime>(type: "text", nullable: false),
                    extra_slots = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sponsors", x => x.user_id);
                });


            migrationBuilder.CreateIndex(
                name: "IX_sponsors_user_id",
                table: "sponsors",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
#endif
