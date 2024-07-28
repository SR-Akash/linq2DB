using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BMS.DBMigration.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Configuration");

            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.CreateTable(
                name: "DeviceOption",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FontOption",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FontOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FontType",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FontType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenderOption",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomePageImageOption",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePageImageOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageType",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictApplications",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientStatus = table.Column<string>(type: "character varying(56)", maxLength: 56, nullable: true),
                    ClientType = table.Column<int>(type: "integer", nullable: false),
                    ClientRoleId = table.Column<int>(type: "integer", nullable: false),
                    ClientTenantId = table.Column<int>(type: "integer", nullable: true),
                    Remarks = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IsRequiredUserCredential = table.Column<bool>(type: "boolean", nullable: false),
                    IsAuthorizationRequired = table.Column<bool>(type: "boolean", nullable: false),
                    AllowedIps = table.Column<string>(type: "text", maxLength: 2147483647, nullable: true),
                    AllowedEndPoints = table.Column<string>(type: "text", maxLength: 2147483647, nullable: true),
                    HourlyRequestLimit = table.Column<long>(type: "bigint", nullable: true),
                    CreatedByClient = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedByClient = table.Column<int>(type: "integer", nullable: true),
                    ClientId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ClientSecret = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyToken = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ConsentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    DisplayNames = table.Column<string>(type: "text", nullable: true),
                    Permissions = table.Column<string>(type: "text", nullable: true),
                    PostLogoutRedirectUris = table.Column<string>(type: "text", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    RedirectUris = table.Column<string>(type: "text", nullable: true),
                    Requirements = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictScopes",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConcurrencyToken = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Descriptions = table.Column<string>(type: "text", nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    DisplayNames = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    Resources = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotoInstructionOption",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoInstructionOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrioritizationOption",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrioritizationOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductRecommendationEngineOption",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRecommendationEngineOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResultsLayoutOption",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultsLayoutOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResultsOutputOption",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultsOutputOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkinConcern",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinConcern", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkinCondition",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinCondition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SunExposureOption",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SunExposureOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    IsTrial = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(56)", maxLength: 56, nullable: true),
                    CreatedByClient = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedByClient = table.Column<int>(type: "integer", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserActivities",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ActivityAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BrowserInfo = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    IpAddress = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Activity = table.Column<string>(type: "text", nullable: true),
                    ActivityType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ReferenceType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ReferenceId = table.Column<int>(type: "integer", nullable: true),
                    ReferenceUrl = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ActivityDuration = table.Column<int>(type: "integer", nullable: true),
                    RequestedData = table.Column<string>(type: "text", nullable: true),
                    ResponseData = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    CreatedByClient = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictAuthorizations",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<long>(type: "bigint", nullable: true),
                    ConcurrencyToken = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    Scopes = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictAuthorizations_OpenIddictApplications_Application~",
                        column: x => x.ApplicationId,
                        principalSchema: "security",
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientConfiguration",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LogoUrl = table.Column<string>(type: "text", nullable: true),
                    PrimaryColor = table.Column<string>(type: "text", nullable: true),
                    SecondaryColor = table.Column<string>(type: "text", nullable: true),
                    BackgroundUrl = table.Column<string>(type: "text", nullable: true),
                    UseDefaultTermsAndCondition = table.Column<bool>(type: "boolean", nullable: true),
                    TermsAndCondition = table.Column<string>(type: "text", nullable: true),
                    HomePageImageOptionId = table.Column<int>(type: "integer", nullable: true),
                    PhotoInstructionOptionId = table.Column<int>(type: "integer", nullable: true),
                    ImageTypeId = table.Column<int>(type: "integer", nullable: true),
                    ResultsLayoutId = table.Column<int>(type: "integer", nullable: true),
                    ProductRecommendationEngineId = table.Column<int>(type: "integer", nullable: true),
                    PrioritizationOptionId = table.Column<int>(type: "integer", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    CreatedByClient = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedByClient = table.Column<int>(type: "integer", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientConfiguration_HomePageImageOption_HomePageImageOption~",
                        column: x => x.HomePageImageOptionId,
                        principalSchema: "Configuration",
                        principalTable: "HomePageImageOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientConfiguration_ImageType_ImageTypeId",
                        column: x => x.ImageTypeId,
                        principalSchema: "Configuration",
                        principalTable: "ImageType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientConfiguration_PhotoInstructionOption_PhotoInstruction~",
                        column: x => x.PhotoInstructionOptionId,
                        principalSchema: "Configuration",
                        principalTable: "PhotoInstructionOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientConfiguration_PrioritizationOption_PrioritizationOpti~",
                        column: x => x.PrioritizationOptionId,
                        principalSchema: "Configuration",
                        principalTable: "PrioritizationOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientConfiguration_ProductRecommendationEngineOption_Produ~",
                        column: x => x.ProductRecommendationEngineId,
                        principalSchema: "Configuration",
                        principalTable: "ProductRecommendationEngineOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientConfiguration_ResultsLayoutOption_ResultsLayoutId",
                        column: x => x.ResultsLayoutId,
                        principalSchema: "Configuration",
                        principalTable: "ResultsLayoutOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientRoles",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    IsSystemRequired = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(56)", maxLength: 56, nullable: true),
                    CreatedByClient = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedByClient = table.Column<int>(type: "integer", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientRoles_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "security",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Grade = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    IsSystemRequired = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(56)", maxLength: 56, nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    CreatedByClient = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedByClient = table.Column<int>(type: "integer", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "security",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictTokens",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<long>(type: "bigint", nullable: true),
                    AuthorizationId = table.Column<long>(type: "bigint", nullable: true),
                    ConcurrencyToken = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Payload = table.Column<string>(type: "text", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    RedemptionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReferenceId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "security",
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalSchema: "security",
                        principalTable: "OpenIddictAuthorizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientConfigurationDetails",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    TypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientConfigurationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientConfigurationDetails_ClientConfiguration_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "Configuration",
                        principalTable: "ClientConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Font",
                schema: "Configuration",
                columns: table => new
                {
                    FontId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: true),
                    FontTypeId = table.Column<int>(type: "integer", nullable: true),
                    Size = table.Column<int>(type: "integer", nullable: true),
                    IsBold = table.Column<bool>(type: "boolean", nullable: true),
                    IsItalic = table.Column<bool>(type: "boolean", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Font", x => x.FontId);
                    table.ForeignKey(
                        name: "FK_Font_ClientConfiguration_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "Configuration",
                        principalTable: "ClientConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Font_FontType_FontTypeId",
                        column: x => x.FontTypeId,
                        principalSchema: "Configuration",
                        principalTable: "FontType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomePageImage",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePageImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomePageImage_ClientConfiguration_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "Configuration",
                        principalTable: "ClientConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhotoInstruction",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Instruction = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoInstruction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoInstruction_ClientConfiguration_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "Configuration",
                        principalTable: "ClientConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkinType",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkinType_ClientConfiguration_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "Configuration",
                        principalTable: "ClientConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    LastName = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    FullName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    DisplayId = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    UserName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Password = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CountryCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    Mobile = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    UserType = table.Column<string>(type: "character varying(56)", maxLength: 56, nullable: true),
                    AccountType = table.Column<string>(type: "character varying(56)", maxLength: 56, nullable: true),
                    ReferrerId = table.Column<int>(type: "integer", nullable: true),
                    ReferralCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    MobileVerifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EmailVerifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AccountVerifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ProfileImageUrl = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    BannerImageUrl = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    CompanyId = table.Column<int>(type: "integer", nullable: true),
                    CurrentAddressId = table.Column<int>(type: "integer", nullable: true),
                    SocialAddressId = table.Column<int>(type: "integer", nullable: true),
                    DOB = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Gender = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Tag = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    IsCompanyAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    IsTenantAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystemAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(56)", maxLength: 56, nullable: true),
                    RoleId1 = table.Column<int>(type: "integer", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: true),
                    CreatedByClient = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedByClient = table.Column<int>(type: "integer", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId1",
                        column: x => x.RoleId1,
                        principalSchema: "security",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "security",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "DeviceOption",
                columns: new[] { "Id", "Name", "Sequence" },
                values: new object[,]
                {
                    { 1, "Mobile", 1 },
                    { 2, "iPad", 2 },
                    { 3, "Laptop", 3 }
                });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "FontOption",
                columns: new[] { "Id", "Name", "Sequence" },
                values: new object[,]
                {
                    { 1, "Montserrat Bold", 1 },
                    { 2, "Montserrat SemiBold", 2 },
                    { 3, "Montserrat Regular", 3 }
                });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "FontType",
                columns: new[] { "Id", "Name", "Sequence" },
                values: new object[,]
                {
                    { 1, "Heading", 1 },
                    { 2, "Heading", 1 },
                    { 3, "Subheading", 2 }
                });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "GenderOption",
                columns: new[] { "Id", "Name", "Sequence" },
                values: new object[] { 1, "Include Gender question in survey", 1 });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "HomePageImageOption",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[,]
                {
                    { 4, "4 Photo", 4 },
                    { 3, "3 Photo", 3 },
                    { 5, "5 Photo", 5 },
                    { 1, "1 Photo", 1 },
                    { 2, "2 Photo", 2 }
                });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "ImageType",
                columns: new[] { "Id", "Name", "Sequence" },
                values: new object[,]
                {
                    { 1, "Frontal", 1 },
                    { 2, "Frontal/ Left Oblique/ Right Oblique", 2 }
                });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "PhotoInstructionOption",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[,]
                {
                    { 4, "4 Step", 4 },
                    { 3, "3 Step", 3 },
                    { 5, "5 Step", 5 },
                    { 1, "1 Step", 1 },
                    { 2, "2 Step", 2 }
                });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "PrioritizationOption",
                columns: new[] { "Id", "Name", "Sequence" },
                values: new object[,]
                {
                    { 1, "Require customers to rank all concerns", 1 },
                    { 2, "Require customers to select top 3 concerns", 2 },
                    { 3, "Require customers to categorize concerns as Low/Medium/High", 3 }
                });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "ProductRecommendationEngineOption",
                columns: new[] { "Id", "Name", "Sequence" },
                values: new object[,]
                {
                    { 1, "On", 1 },
                    { 2, "Off", 2 }
                });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "ResultsLayoutOption",
                columns: new[] { "Id", "Name", "Sequence" },
                values: new object[,]
                {
                    { 2, "Toggle between photo results", 2 },
                    { 1, "Show all conditions on the same page", 1 }
                });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "ResultsOutputOption",
                columns: new[] { "Id", "Name", "Sequence" },
                values: new object[,]
                {
                    { 3, "Export to EHR", 3 },
                    { 2, "Export to CRM", 2 },
                    { 1, "Save to PDF", 1 }
                });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "SkinConcern",
                columns: new[] { "Id", "Name", "Sequence" },
                values: new object[,]
                {
                    { 1, "Acne/Blemishes/Oiliness", 1 },
                    { 2, "Under Eye Circles/Puffiness", 2 },
                    { 3, "Loss of Volume / Skin Sagging", 3 },
                    { 4, "Pigmentation/Uneven Skin Tone", 4 },
                    { 5, "Dullness", 5 },
                    { 6, "Crow’s Feet", 6 },
                    { 7, "Dryness", 7 },
                    { 8, "Pore Size", 8 },
                    { 9, "Texture", 9 },
                    { 10, "Fine Lines & Wrinkles", 10 },
                    { 11, "Sensitivity/Redness", 11 }
                });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "SkinCondition",
                columns: new[] { "Id", "Name", "Sequence" },
                values: new object[,]
                {
                    { 3, "Pigmentation", 3 },
                    { 4, "Under Eye Circles", 4 },
                    { 1, "Acne", 1 },
                    { 2, "Wrinkles", 2 }
                });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "SkinType",
                columns: new[] { "Id", "ClientId", "ImageUrl", "Name", "Notes", "Sequence", "Title" },
                values: new object[,]
                {
                    { 1, null, "", "Normal", "‘Normal’ is a term widely used to refer to well-balanced skin.", 1, "Normal" },
                    { 2, null, "", "Oily", "Associated with overproduction of oil or sebum", 2, "Oily" },
                    { 3, null, "", "Dry", "Produces less sebum than normal skin.  Can appear dull or lifeless due to loss of moisture", 3, "Dry" },
                    { 4, null, "", "Combination", "Oily forehead, nose, and chin and relatively dry cheeks", 4, "Combination" },
                    { 5, null, "", "Sensitive", "Sensitive skin is easily irritated", 5, "Sensitive" }
                });

            migrationBuilder.InsertData(
                schema: "Configuration",
                table: "SunExposureOption",
                columns: new[] { "Id", "Name", "Sequence" },
                values: new object[] { 1, "Include Sun Exposure question in survey", 1 });

            migrationBuilder.InsertData(
                schema: "security",
                table: "ClientRoles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByClient", "Description", "IsSystemRequired", "Name", "Status", "TenantId", "UpdatedAt", "UpdatedBy", "UpdatedByClient" },
                values: new object[,]
                {
                    { 3, new DateTime(2022, 2, 5, 22, 7, 42, 907, DateTimeKind.Utc).AddTicks(7272), null, null, "App Client", true, "App Client", "Active", null, null, null, null },
                    { 2, new DateTime(2022, 2, 5, 22, 7, 42, 907, DateTimeKind.Utc).AddTicks(7267), null, null, "Web Client", true, "Web Client", "Active", null, null, null, null },
                    { 1, new DateTime(2022, 2, 5, 22, 7, 42, 907, DateTimeKind.Utc).AddTicks(6912), null, null, "Master Client", true, "Master Client", "Active", null, null, null, null }
                });

            migrationBuilder.InsertData(
                schema: "security",
                table: "OpenIddictApplications",
                columns: new[] { "Id", "AllowedEndPoints", "AllowedIps", "ClientId", "ClientRoleId", "ClientSecret", "ClientStatus", "ClientTenantId", "ClientType", "ConcurrencyToken", "ConsentType", "CreatedAt", "CreatedBy", "CreatedByClient", "DisplayName", "DisplayNames", "HourlyRequestLimit", "IsAuthorizationRequired", "IsRequiredUserCredential", "Permissions", "PostLogoutRedirectUris", "Properties", "RedirectUris", "Remarks", "Requirements", "Type", "UpdatedAt", "UpdatedBy", "UpdatedByClient" },
                values: new object[,]
                {
                    { 1L, null, null, "MASTER_CLIENT", 1, "AQAAAAEAACcQAAAAEHlgdSPOPiWHPohwR5mqo+51bkn1cQUkf07HBzhTSIykTq6mpeoSF59Hlx4jGutd3A==", "Active", 1, 0, "3e37cec0-6dd8-4023-be89-dcdd01789b4b", null, new DateTime(2022, 2, 5, 22, 7, 43, 4, DateTimeKind.Utc).AddTicks(6287), 0, null, "MASTER_CLIENT", null, null, false, false, "[\"ept:token\",\"gt:client_credentials\",\"gt:password\",\"gt:refresh_token\"]", null, null, null, null, null, null, null, null, null },
                    { 2L, null, null, "WEB_CLIENT", 0, "AQAAAAEAACcQAAAAECATAnattR4ZOwUehNYFkYPErwgOday8FjG8IgNy6/xJcXL2Hviy9cTZ9m5KzAKVuw==", "Active", 1, 0, "99659ca4-f7fd-438a-acf6-bc1c20ec7774", null, new DateTime(2022, 2, 5, 22, 7, 43, 12, DateTimeKind.Utc).AddTicks(7597), 0, null, "WEB_CLIENT", null, null, true, true, "[\"ept:token\",\"gt:client_credentials\",\"gt:password\",\"gt:refresh_token\"]", null, null, null, null, null, null, null, null, null },
                    { 3L, null, null, "APP_CLIENT", 0, "AQAAAAEAACcQAAAAEPowzHIejHkH+/84qdmpJ+AEtlxcBPO9WWCzJ3gcUogmczti+yx5fkzJhdQ6dCg/3Q==", "Active", 1, 0, "2c5164ee-0c29-4e6e-9187-a13c5126d5be", null, new DateTime(2022, 2, 5, 22, 7, 43, 20, DateTimeKind.Utc).AddTicks(5507), 0, null, "APP_CLIENT", null, null, true, true, "[\"ept:token\",\"gt:client_credentials\",\"gt:password\",\"gt:refresh_token\"]", null, null, null, null, null, null, null, null, null }
                });

            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByClient", "Description", "Grade", "IsSystemRequired", "Name", "Status", "TenantId", "UpdatedAt", "UpdatedBy", "UpdatedByClient" },
                values: new object[] { 1, new DateTime(2022, 2, 5, 22, 7, 43, 20, DateTimeKind.Utc).AddTicks(9040), null, null, "System Admin", 0, true, "System Admin", "Active", null, null, null, null });

            migrationBuilder.InsertData(
                schema: "security",
                table: "Tenants",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByClient", "Description", "IsTrial", "Name", "Status", "UpdatedAt", "UpdatedBy", "UpdatedByClient" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 2, 5, 22, 7, 42, 906, DateTimeKind.Utc).AddTicks(3321), null, null, "SkinIntel Tenant", false, "SkinIntel", "Active", null, null, null },
                    { 2, new DateTime(2022, 2, 5, 22, 7, 42, 906, DateTimeKind.Utc).AddTicks(3696), null, null, "ABC Tenant", false, "ABC", "Active", null, null, null }
                });

            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByClient", "Description", "Grade", "IsSystemRequired", "Name", "Status", "TenantId", "UpdatedAt", "UpdatedBy", "UpdatedByClient" },
                values: new object[,]
                {
                    { 2, new DateTime(2022, 2, 5, 22, 7, 43, 20, DateTimeKind.Utc).AddTicks(9401), null, null, "Tenant Admin", 0, true, "Tenant Admin", "Active", 1, null, null, null },
                    { 3, new DateTime(2022, 2, 5, 22, 7, 43, 20, DateTimeKind.Utc).AddTicks(9721), null, null, "Company Admin", 0, true, "Company Admin", "Active", 1, null, null, null },
                    { 4, new DateTime(2022, 2, 5, 22, 7, 43, 20, DateTimeKind.Utc).AddTicks(9725), null, null, "Customer", 0, true, "Customer", "Active", 1, null, null, null }
                });

            migrationBuilder.InsertData(
                schema: "security",
                table: "Users",
                columns: new[] { "Id", "AccountType", "AccountVerifiedAt", "BannerImageUrl", "CompanyId", "CountryCode", "CreatedAt", "CreatedBy", "CreatedByClient", "CurrentAddressId", "DOB", "DisplayId", "Email", "EmailVerifiedAt", "FirstName", "FullName", "Gender", "IsCompanyAdmin", "IsSystemAdmin", "IsTenantAdmin", "IsVerified", "LastName", "Mobile", "MobileVerifiedAt", "Password", "ProfileImageUrl", "ReferralCode", "ReferrerId", "RoleId", "RoleId1", "SocialAddressId", "Status", "Tag", "TenantId", "UpdatedAt", "UpdatedBy", "UpdatedByClient", "UserName", "UserType" },
                values: new object[] { 1, null, new DateTime(2022, 2, 5, 22, 7, 43, 29, DateTimeKind.Utc).AddTicks(4445), null, null, null, new DateTime(2022, 2, 5, 22, 7, 43, 29, DateTimeKind.Utc).AddTicks(5836), null, null, null, null, null, "systemadmin@example.com", new DateTime(2022, 2, 5, 22, 7, 43, 29, DateTimeKind.Utc).AddTicks(3568), "System", "System Admin", null, true, true, true, false, "Admin", null, null, "AQAAAAEAACcQAAAAEEt6URzrAzdSLUf6MfCONnwTGveCCzF2213pJ2siykbQ7+1es2m3wY4FwDC6ka+f1w==", null, null, null, 1, null, null, "Active", null, null, null, null, null, "systemadmin@example.com", "BackOffice" });

            migrationBuilder.InsertData(
                schema: "security",
                table: "Users",
                columns: new[] { "Id", "AccountType", "AccountVerifiedAt", "BannerImageUrl", "CompanyId", "CountryCode", "CreatedAt", "CreatedBy", "CreatedByClient", "CurrentAddressId", "DOB", "DisplayId", "Email", "EmailVerifiedAt", "FirstName", "FullName", "Gender", "IsCompanyAdmin", "IsSystemAdmin", "IsTenantAdmin", "IsVerified", "LastName", "Mobile", "MobileVerifiedAt", "Password", "ProfileImageUrl", "ReferralCode", "ReferrerId", "RoleId", "RoleId1", "SocialAddressId", "Status", "Tag", "TenantId", "UpdatedAt", "UpdatedBy", "UpdatedByClient", "UserName", "UserType" },
                values: new object[,]
                {
                    { 2, null, new DateTime(2022, 2, 5, 22, 7, 43, 37, DateTimeKind.Utc).AddTicks(5689), null, null, null, new DateTime(2022, 2, 5, 22, 7, 43, 37, DateTimeKind.Utc).AddTicks(5701), null, null, null, null, null, "tenantadmin@example.com", new DateTime(2022, 2, 5, 22, 7, 43, 37, DateTimeKind.Utc).AddTicks(5678), "Tenant", "Tenant Admin", null, true, false, true, false, "Admin", null, null, "AQAAAAEAACcQAAAAEPfx+yPwU6yASxR0blMZaaqaLZ09VkwkZ7AdwrSiT+EGPwz/2CE3gJ9KvvRO9RJLsw==", null, null, null, 2, null, null, "Active", null, 1, null, null, null, "tenantadmin@example.com", "BackOffice" },
                    { 3, null, new DateTime(2022, 2, 5, 22, 7, 43, 45, DateTimeKind.Utc).AddTicks(4682), null, null, null, new DateTime(2022, 2, 5, 22, 7, 43, 45, DateTimeKind.Utc).AddTicks(4689), null, null, null, null, null, "companyadmin@example.com", new DateTime(2022, 2, 5, 22, 7, 43, 45, DateTimeKind.Utc).AddTicks(4676), "Company", "Company Admin", null, true, false, false, false, "Admin", null, null, "AQAAAAEAACcQAAAAEEp1uDU46HZzyZr7KslSWo2VWBt/75lupK/e3ThKsH/JVF5qxNPIrTZTtF2OTmA0sQ==", null, null, null, 3, null, null, "Active", null, 1, null, null, null, "companyadmin@example.com", "BackOffice" },
                    { 4, null, new DateTime(2022, 2, 5, 22, 7, 43, 55, DateTimeKind.Utc).AddTicks(4367), null, null, null, new DateTime(2022, 2, 5, 22, 7, 43, 55, DateTimeKind.Utc).AddTicks(4378), null, null, null, null, null, "customer01@example.com", new DateTime(2022, 2, 5, 22, 7, 43, 55, DateTimeKind.Utc).AddTicks(4351), "Customer", "Customer 01", null, false, false, false, false, "01", null, null, "AQAAAAEAACcQAAAAEKu+86xHcpqc2lTsxd7zBB7tcPGIkp0NO8H8qUqCrAAfBXLGotzhuDIdrl8oTWESJA==", null, null, null, 4, null, null, "Active", null, 1, null, null, null, "customer01@example.com", "Customer" },
                    { 5, null, new DateTime(2022, 2, 5, 22, 7, 43, 65, DateTimeKind.Utc).AddTicks(1495), null, null, null, new DateTime(2022, 2, 5, 22, 7, 43, 65, DateTimeKind.Utc).AddTicks(1514), null, null, null, null, null, "customer02@example.com", new DateTime(2022, 2, 5, 22, 7, 43, 65, DateTimeKind.Utc).AddTicks(1478), "Customer", "Customer 02", null, false, false, false, false, "02", null, null, "AQAAAAEAACcQAAAAEAeowycjtAEyUjO2qF/lg5wftQpHC1jKlV/MluHPi+w14ydeYCL+yH2TrBbRj4gHiw==", null, null, null, 4, null, null, "Active", null, 1, null, null, null, "customer02@example.com", "Customer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientConfiguration_HomePageImageOptionId",
                schema: "Configuration",
                table: "ClientConfiguration",
                column: "HomePageImageOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientConfiguration_ImageTypeId",
                schema: "Configuration",
                table: "ClientConfiguration",
                column: "ImageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientConfiguration_PhotoInstructionOptionId",
                schema: "Configuration",
                table: "ClientConfiguration",
                column: "PhotoInstructionOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientConfiguration_PrioritizationOptionId",
                schema: "Configuration",
                table: "ClientConfiguration",
                column: "PrioritizationOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientConfiguration_ProductRecommendationEngineId",
                schema: "Configuration",
                table: "ClientConfiguration",
                column: "ProductRecommendationEngineId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientConfiguration_ResultsLayoutId",
                schema: "Configuration",
                table: "ClientConfiguration",
                column: "ResultsLayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientConfigurationDetails_ClientId",
                schema: "Configuration",
                table: "ClientConfigurationDetails",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRoles_TenantId",
                schema: "security",
                table: "ClientRoles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Font_ClientId",
                schema: "Configuration",
                table: "Font",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Font_FontTypeId",
                schema: "Configuration",
                table: "Font",
                column: "FontTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HomePageImage_ClientId",
                schema: "Configuration",
                table: "HomePageImage",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictApplications_ClientId",
                schema: "security",
                table: "OpenIddictApplications",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type",
                schema: "security",
                table: "OpenIddictAuthorizations",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictScopes_Name",
                schema: "security",
                table: "OpenIddictScopes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type",
                schema: "security",
                table: "OpenIddictTokens",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_AuthorizationId",
                schema: "security",
                table: "OpenIddictTokens",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ReferenceId",
                schema: "security",
                table: "OpenIddictTokens",
                column: "ReferenceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoInstruction_ClientId",
                schema: "Configuration",
                table: "PhotoInstruction",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_TenantId",
                schema: "security",
                table: "Roles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_SkinType_ClientId",
                schema: "Configuration",
                table: "SkinType",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "security",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId1",
                schema: "security",
                table: "Users",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId",
                schema: "security",
                table: "Users",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientConfigurationDetails",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "ClientRoles",
                schema: "security");

            migrationBuilder.DropTable(
                name: "DeviceOption",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "Font",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "FontOption",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "GenderOption",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "HomePageImage",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "OpenIddictScopes",
                schema: "security");

            migrationBuilder.DropTable(
                name: "OpenIddictTokens",
                schema: "security");

            migrationBuilder.DropTable(
                name: "PhotoInstruction",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "ResultsOutputOption",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "SkinConcern",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "SkinCondition",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "SkinType",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "SunExposureOption",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "UserActivities",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "security");

            migrationBuilder.DropTable(
                name: "FontType",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "OpenIddictAuthorizations",
                schema: "security");

            migrationBuilder.DropTable(
                name: "ClientConfiguration",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "security");

            migrationBuilder.DropTable(
                name: "OpenIddictApplications",
                schema: "security");

            migrationBuilder.DropTable(
                name: "HomePageImageOption",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "ImageType",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "PhotoInstructionOption",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "PrioritizationOption",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "ProductRecommendationEngineOption",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "ResultsLayoutOption",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "security");
        }
    }
}
