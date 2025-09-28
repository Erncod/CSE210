using System;

class Program
{
    static void Main(string[] args)
    {
        // First customer & order
        Address addr1 = new Address("123 Main St", "New York", "NY", "USA");
        Customer cust1 = new Customer("John Doe", addr1);
        Order order1 = new Order(cust1);

        order1.AddProduct(new Product("Laptop", "L123", 800.50, 1));
        order1.AddProduct(new Product("Mouse", "M456", 25.75, 2));

        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice():0.00}");
        Console.WriteLine();

        // Second customer & order
        Address addr2 = new Address("45 Queen Street", "Toronto", "ON", "Canada");
        Customer cust2 = new Customer("Jane Smith", addr2);
        Order order2 = new Order(cust2);

        order2.AddProduct(new Product("Phone", "P789", 600.00, 1));
        order2.AddProduct(new Product("Charger", "C321", 20.00, 3));

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice():0.00}");
    }
}
