Imports System
Imports Microsoft.EntityFrameworkCore

Module Program
    Sub Main(args As String())
        Dim context As CustomerContext = New CustomerContext()
        context.Database.Migrate()  ' this will create all tables and everything for you if there not already there
        Dim name As String = Guid.NewGuid().ToString()
        Dim customer = New Customer() With {.Name = name, .DateOfBirth = DateTime.Now.AddDays(-5678)}

        context.Customers.Add(customer)
        context.SaveChanges()


        ' Gets
        Dim allCustomers = context.Customers.ToArray()
        Dim customersWithCertainItems = context.Customers.Where(Function(e) e.Name.Contains(name)).ToArray()
        Dim onlyOneCustomerWithCertainItems = context.Customers.FirstOrDefault(Function(e) e.Name.Contains(name))

        ' update a customer
        onlyOneCustomerWithCertainItems.Name = name + " mike 2"
        context.SaveChanges()


        '' add things on via joins
        onlyOneCustomerWithCertainItems.CustomerLogins.Add(New CustomerLogin With {.Success = False})
        context.SaveChanges()

        ' delete a customer

        'foreign key constrait so have to delete there logins aswell
        Dim allLogins = context.CustomerLogins.Where(Function(e) e.Customer.Name.Contains(name)).ToArray()  ' get via join sql statement
        context.CustomerLogins.RemoveRange(allLogins)
        context.SaveChanges()
        onlyOneCustomerWithCertainItems = context.Customers.FirstOrDefault(Function(e) e.Name.Contains(name))
        context.Customers.Remove(onlyOneCustomerWithCertainItems)
        context.SaveChanges()
    End Sub
End Module
