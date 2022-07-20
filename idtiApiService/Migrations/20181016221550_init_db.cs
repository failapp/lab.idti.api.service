using Microsoft.EntityFrameworkCore.Migrations;

namespace idtiApiService.Migrations
{
    public partial class init_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eventlogs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    eventDateTime = table.Column<string>(nullable: true),
                    eventTime = table.Column<string>(nullable: true),
                    eventDate = table.Column<string>(nullable: true),
                    eventCode = table.Column<string>(nullable: true),
                    userId = table.Column<string>(nullable: true),
                    deviceId = table.Column<int>(nullable: false),
                    systemDateTime = table.Column<string>(nullable: true),
                    funcCode = table.Column<int>(nullable: false),
                    doorStatus = table.Column<int>(nullable: false),
                    moduleAddr = table.Column<int>(nullable: false),
                    operationMode = table.Column<int>(nullable: false),
                    readerAddr = table.Column<int>(nullable: false),
                    sync = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventlogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "terminals",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    ipAddr = table.Column<string>(nullable: true),
                    tcpPort = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    controllerModel = table.Column<int>(nullable: false),
                    commType = table.Column<int>(nullable: false),
                    commReadTimeOut = table.Column<int>(nullable: false),
                    commSerialPort = table.Column<string>(nullable: true),
                    commSerialBPS = table.Column<int>(nullable: false),
                    protocolVersion = table.Column<int>(nullable: false),
                    polling = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_terminals", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eventlogs");

            migrationBuilder.DropTable(
                name: "terminals");
        }
    }
}
