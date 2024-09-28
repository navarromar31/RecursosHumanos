using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecursosHumanos.Migrations
{
    /// <inheritdoc />
    public partial class Tablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreDepartamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionPuesto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "evaluacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoEvaluacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "institucion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreInstitucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionInstitucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenUrlIns = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_institucion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "puesto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePuesto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionPuesto = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    ValorRespuesta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respuesta", x => x.Id);
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
                        name: "FK_colaborador_departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "departamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_colaborador_institucion_InstitucionId",
                        column: x => x.InstitucionId,
                        principalTable: "institucion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_colaborador_puesto_PuestoId",
                        column: x => x.PuestoId,
                        principalTable: "puesto",
                        principalColumn: "Id");
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
                    ColaboradorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_capacitacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_capacitacion_colaborador_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "colaborador",
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
                    FinalEscala = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_capacitacion_ColaboradorId",
                table: "capacitacion",
                column: "ColaboradorId");

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
                name: "evaluacionColaborador");

            migrationBuilder.DropTable(
                name: "pregunta");

            migrationBuilder.DropTable(
                name: "respuesta");

            migrationBuilder.DropTable(
                name: "capacitacion");

            migrationBuilder.DropTable(
                name: "evaluacion");

            migrationBuilder.DropTable(
                name: "colaborador");

            migrationBuilder.DropTable(
                name: "departamento");

            migrationBuilder.DropTable(
                name: "institucion");

            migrationBuilder.DropTable(
                name: "puesto");
        }
    }
}
