﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecursosHumanos_AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class TablasFinales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "capacitacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCapacitacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duracion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinPersonas = table.Column<int>(type: "int", nullable: false),
                    MaxPersonas = table.Column<int>(type: "int", nullable: false),
                    Modalidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenUrlCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoCapacitacion = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_capacitacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreDepartamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionDepartamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoDepartamento = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "evaluacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoEvaluacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoEvaluacion = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "instituciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreInstitucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionInstitucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenUrlInstitucion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoInstitucion = table.Column<bool>(type: "bit", nullable: false),
                    Eliminada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instituciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "puesto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePuesto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionPuesto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoPuesto = table.Column<bool>(type: "bit", nullable: false),
                    Eliminada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_puesto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "respuesta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPregunta = table.Column<int>(type: "int", nullable: false),
                    ColaboradorEvaluado = table.Column<int>(type: "int", nullable: false),
                    ColaboradorEvaluador = table.Column<int>(type: "int", nullable: false),
                    ValorRespuesta = table.Column<int>(type: "int", nullable: false),
                    EstadoRespuesta = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respuesta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "evaluacionColaborador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColaboradorEvaluado = table.Column<int>(type: "int", nullable: false),
                    ColaboradorEvaluador = table.Column<int>(type: "int", nullable: false),
                    EvaluacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluacionColaborador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_evaluacionColaborador_evaluacion_EvaluacionId",
                        column: x => x.EvaluacionId,
                        principalTable: "evaluacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pregunta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCapacitacion = table.Column<int>(type: "int", nullable: false),
                    IdEvaluacion = table.Column<int>(type: "int", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InicioEscala = table.Column<int>(type: "int", nullable: false),
                    FinalEscala = table.Column<int>(type: "int", nullable: false),
                    EstadoPregunta = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pregunta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pregunta_capacitacion_IdCapacitacion",
                        column: x => x.IdCapacitacion,
                        principalTable: "capacitacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pregunta_evaluacion_IdEvaluacion",
                        column: x => x.IdEvaluacion,
                        principalTable: "evaluacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "colaborador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CedulaColaborador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreColaborador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoColaborador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenUrlCol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoColaborador = table.Column<bool>(type: "bit", nullable: true),
                    IdPuesto = table.Column<int>(type: "int", nullable: false),
                    PuestoId = table.Column<int>(type: "int", nullable: true),
                    IdInstitucion = table.Column<int>(type: "int", nullable: false),
                    InstitucionId = table.Column<int>(type: "int", nullable: true),
                    IdDepartamento = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colaborador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_colaborador_departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "departamentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_colaborador_instituciones_InstitucionId",
                        column: x => x.InstitucionId,
                        principalTable: "instituciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_colaborador_puesto_PuestoId",
                        column: x => x.PuestoId,
                        principalTable: "puesto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_colaborador_DepartamentoId",
                table: "colaborador",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_colaborador_InstitucionId",
                table: "colaborador",
                column: "InstitucionId");

            migrationBuilder.CreateIndex(
                name: "IX_colaborador_PuestoId",
                table: "colaborador",
                column: "PuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_evaluacionColaborador_EvaluacionId",
                table: "evaluacionColaborador",
                column: "EvaluacionId");

            migrationBuilder.CreateIndex(
                name: "IX_pregunta_IdCapacitacion",
                table: "pregunta",
                column: "IdCapacitacion");

            migrationBuilder.CreateIndex(
                name: "IX_pregunta_IdEvaluacion",
                table: "pregunta",
                column: "IdEvaluacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "colaborador");

            migrationBuilder.DropTable(
                name: "evaluacionColaborador");

            migrationBuilder.DropTable(
                name: "pregunta");

            migrationBuilder.DropTable(
                name: "respuesta");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "departamentos");

            migrationBuilder.DropTable(
                name: "instituciones");

            migrationBuilder.DropTable(
                name: "puesto");

            migrationBuilder.DropTable(
                name: "capacitacion");

            migrationBuilder.DropTable(
                name: "evaluacion");
        }
    }
}
