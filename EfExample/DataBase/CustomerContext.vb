Imports Microsoft.EntityFrameworkCore
' our db context

Public Class CustomerContext
    Inherits DbContext

    Public Sub New()

    End Sub

    ' connection string builder (should pass in via constructor but its a example in sqlite so cba)
    Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
        optionsBuilder.UseSqlite("Data Source=CustomerDatabase.db")
        MyBase.OnConfiguring(optionsBuilder)
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)

        modelBuilder.Entity(Of Customer)(Sub(builder)
                                             builder.ToTable("Customers")               ' we tell the builder we want customer to go to table customers
                                             builder.Property(Function(e) e.DateOfBirth).HasColumnType("date")      ' only need the date don't need time
                                             builder.Property(Function(e) e.Name).HasMaxLength(150)                 ' has max lenght of 150
                                         End Sub)

        modelBuilder.Entity(Of CustomerLogin)(Sub(builder)

                                                  builder.ToTable("CustomerLogins")
                                                  builder.Property(Function(e) e.CreatedAt).HasColumnType("dateTime").HasDefaultValueSql("datetime()")  ' is a date time and is auto generated in sqlite


                                                  ' below here is how we do joins ...
                                                  ' so customer login links to one customer..
                                                  ' that customer has many logins . and there joined via customerid on customer logins
                                                  ' on delete restrict to force referential integrity 
                                                  builder.HasOne(Function(e) e.Customer).
                                                 WithMany(Function(w) w.CustomerLogins).HasForeignKey(Function(e) e.CustomerId).
                                                 OnDelete(DeleteBehavior.Restrict).
                                                 HasConstraintName("FK_CustomerLogins_Customer")

                                              End Sub)
        MyBase.OnModelCreating(modelBuilder)

    End Sub


    ' our db sets (tables)

    Public Overridable Property Customers() As DbSet(Of Customer)
    Public Overridable Property CustomerLogins() As DbSet(Of CustomerLogin)

End Class

