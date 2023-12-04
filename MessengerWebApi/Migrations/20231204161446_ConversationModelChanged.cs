using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessengerWebApi.Migrations
{
    /// <inheritdoc />
    public partial class ConversationModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Users_UserFirstName",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Users_UserFirstName",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationUser_Users_ParticipantsFirstName",
                table: "ConversationUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConversationUser",
                table: "ConversationUser");

            migrationBuilder.DropIndex(
                name: "IX_ConversationUser_ParticipantsFirstName",
                table: "ConversationUser");

            migrationBuilder.DropIndex(
                name: "IX_Contact_UserFirstName",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Connections_UserFirstName",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "ParticipantsFirstName",
                table: "ConversationUser");

            migrationBuilder.DropColumn(
                name: "UserFirstName",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "UserFirstName",
                table: "Connections");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipantsId",
                table: "ConversationUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ChannelId",
                table: "Conversations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Contact",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Connections",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConversationUser",
                table: "ConversationUser",
                columns: new[] { "ConversationsId", "ParticipantsId" });

            migrationBuilder.CreateIndex(
                name: "IX_ConversationUser_ParticipantsId",
                table: "ConversationUser",
                column: "ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UserId",
                table: "Contact",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_UserId",
                table: "Connections",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Users_UserId",
                table: "Connections",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Users_UserId",
                table: "Contact",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationUser_Users_ParticipantsId",
                table: "ConversationUser",
                column: "ParticipantsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Users_UserId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Users_UserId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationUser_Users_ParticipantsId",
                table: "ConversationUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConversationUser",
                table: "ConversationUser");

            migrationBuilder.DropIndex(
                name: "IX_ConversationUser_ParticipantsId",
                table: "ConversationUser");

            migrationBuilder.DropIndex(
                name: "IX_Contact_UserId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Connections_UserId",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "ParticipantsId",
                table: "ConversationUser");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Connections");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantsFirstName",
                table: "ConversationUser",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "Conversations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "UserFirstName",
                table: "Contact",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserFirstName",
                table: "Connections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "FirstName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConversationUser",
                table: "ConversationUser",
                columns: new[] { "ConversationsId", "ParticipantsFirstName" });

            migrationBuilder.CreateIndex(
                name: "IX_ConversationUser_ParticipantsFirstName",
                table: "ConversationUser",
                column: "ParticipantsFirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UserFirstName",
                table: "Contact",
                column: "UserFirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_UserFirstName",
                table: "Connections",
                column: "UserFirstName");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Users_UserFirstName",
                table: "Connections",
                column: "UserFirstName",
                principalTable: "Users",
                principalColumn: "FirstName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Users_UserFirstName",
                table: "Contact",
                column: "UserFirstName",
                principalTable: "Users",
                principalColumn: "FirstName");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationUser_Users_ParticipantsFirstName",
                table: "ConversationUser",
                column: "ParticipantsFirstName",
                principalTable: "Users",
                principalColumn: "FirstName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
