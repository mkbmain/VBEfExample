Imports Microsoft.EntityFrameworkCore.Migrations

Namespace Global.EfExample.Migrations
    Partial Public Class init
        Inherits Migration

        Protected Overrides Sub Up(migrationBuilder As MigrationBuilder)
            migrationBuilder.CreateTable(
                name:="Customers",
                columns:=Function(table) New With {
                    .Id = table.Column(Of Integer)(type:="INTEGER", nullable:=False).
                        Annotation("Sqlite:Autoincrement", True),
                    .Name = table.Column(Of String)(type:="TEXT", maxLength:=150, nullable:=True),
                    .DateOfBirth = table.Column(Of Date)(type:="date", nullable:=False)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_Customers", Function(x) x.Id)
                End Sub)

            migrationBuilder.CreateTable(
                name:="CustomerLogins",
                columns:=Function(table) New With {
                    .Id = table.Column(Of Integer)(type:="INTEGER", nullable:=False).
                        Annotation("Sqlite:Autoincrement", True),
                    .CustomerId = table.Column(Of Integer)(type:="INTEGER", nullable:=False),
                    .CreatedAt = table.Column(Of Date)(type:="dateTime", nullable:=False, defaultValueSql:="datetime()"),
                    .Success = table.Column(Of Boolean)(type:="INTEGER", nullable:=False)
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_CustomerLogins", Function(x) x.Id)
                    table.ForeignKey(
                        name:="FK_CustomerLogins_Customer",
                        column:=Function(x) x.CustomerId,
                        principalTable:="Customers",
                        principalColumn:="Id",
                        onDelete:=ReferentialAction.Restrict)
                End Sub)

            migrationBuilder.CreateIndex(
                name:="IX_CustomerLogins_CustomerId",
                table:="CustomerLogins",
                column:="CustomerId")
        End Sub

        Protected Overrides Sub Down(migrationBuilder As MigrationBuilder)
            migrationBuilder.DropTable(
                name:="CustomerLogins")

            migrationBuilder.DropTable(
                name:="Customers")
        End Sub
    End Class
End Namespace
