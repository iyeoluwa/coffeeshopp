using System;
using System.Data.SqlClient;
namespace CoffeeShop;

class Program
{
    //This is a group project that runs a mini coffee shop program 

    //This string is a makeshift connection string.. This string helps us input the b=neccssary values in the database.
    static string connectionString = @"Data Source=localhost,1433;Initial Catalog=coffeeShop;User ID=SA;Password=123IsaGoodnumber#;";
    //Create a flag to track if the user is logged in 
    private static bool isLoggedIn = false;
    
    //store the users/ customer id ( it can be used in the future...
    private static string LoggedInUser = null;
    
   
    private static DateTime currentDate = DateTime.Now; // Current date and time

    static void Main(string[] args)
    {
       
        while (true)
        {
            if (!isLoggedIn)
            {
                Console.WriteLine("welcome to Our console coffee shop");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("-----------------------------------------------");
                DrawCoffeeMug();
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1. Signup");
                Console.WriteLine("2. Login");
                Console.Write("Please choose an option (1/2): ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Signup();
                        break;
                    case "2":
                        Login();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("welcome back to your favourite console coffee shop");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("-----------------------------------------------");
                DrawCoffeeMug();
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1. Show Menu");
                Console.WriteLine("2. Place Order");
                Console.WriteLine("3.Logout");
                Console.Write("Please choose any of the options ;) :");
                switch (Console.ReadLine())
                {
                    case "1":
                        ShowMenu();
                        break;
                    case "2":
                        PlaceOrders();
                        break;
                    case "3":
                        Login();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            
        }
        
    }


    //This method is used to extract all the available products that the coffeeshop sells 

    static void ShowMenu()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            //write the sql statement to get the list of items in the database.

            string SelectQuery = "Select * From Products";
            using (SqlCommand command = new SqlCommand(SelectQuery, connection))
            {

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Define column widths
                    int codeWidth = 15;
                    int nameWidth = 20;
                    int priceWidth = 10;

                    // Print table header
                    PrintLine(codeWidth, nameWidth, priceWidth);
                    PrintRow("Product Code", "Item Name", "Price", codeWidth, nameWidth, priceWidth);
                    PrintLine(codeWidth, nameWidth, priceWidth);
                    while (reader.Read())
                    {
                        string productCode = reader.GetString(4);
                        string itemName = reader.GetString(1);
                        double price = reader.GetDouble(2);
                        // Display each row with consistent column widths
                        PrintRow(productCode, itemName, price.ToString("C"), codeWidth, nameWidth, priceWidth);
                    }
                }
            }
        }
    }

    static void PlaceOrders()
    {
        //this method handles the placing of orders
        //collect the order to be bought 
        // check if the the user has an account/ or the user is logged in.
        
        Console.WriteLine("Type the code of the item on the menu to order");
        String code = Console.ReadLine();
        
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
           int productId =  getproductID(code);
            string SelectQuery = "Insert into Orders (ProductId,CustomerId,Date,TotalAmount)VALUES (@productid,@customerid,@currentDate,@amount)";// this is the insert statement that inserts orders 
            using (SqlCommand command = new SqlCommand(SelectQuery, connection))
            {
                command.Parameters.AddWithValue("@productid", productId);
                command.Parameters.AddWithValue("@customerid", getCustomerCredentials(LoggedInUser));
                command.Parameters.AddWithValue("@currentDate", currentDate);
                command.Parameters.AddWithValue("@amount", getSingleDetailFromDb(true,"products","price","Product_code", code));
                command.ExecuteNonQuery();
            }
            
            Console.WriteLine("Order Placed successfully.");
        }
    }
    
     static void Signup()
        {
            Console.WriteLine("\n--- Signup ---");

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            
            Console.Write("Enter Email address: ");
            string email = Console.ReadLine();
            
            Console.Write("Enter Your Phone Number: ");
            string phone = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            // Save user data to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if the username already exists
                    string checkQuery = "SELECT COUNT(*) FROM Customers WHERE email = @email";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@email", email);
                        int userExists = (int)checkCommand.ExecuteScalar();

                        if (userExists > 0)
                        {
                            Console.WriteLine("Email address already exists. Please choose another email address.");
                            return;
                        }
                    }

                    // Insert new user
                    string query = "INSERT INTO Customers (name, Password,phonenumber,email) VALUES (@name, @password,@phone,@email)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@phone", phone);
                        command.Parameters.AddWithValue("@password", password);  // Hashing passwords is recommended in a real-world scenario

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Console.WriteLine("Signup successful! you are now logged in.");
                            isLoggedIn = true;
                            LoggedInUser = email;
                            Console.Clear();
                            
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("SQL Exception: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        static void Login()
        {
            Console.WriteLine("\n--- Login ---");

            Console.Write("Enter Your Email Address: ");
            string email = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            // Verify user credentials
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Customers WHERE email = @email AND Password = @password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password",
                            password); // Hashing and verifying passwords is recommended in a real-world scenario

                        int result = (int)command.ExecuteScalar();

                        if (result > 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Login successful! Welcome, " + email + ".");
                            isLoggedIn = true;
                            LoggedInUser = email;

                        }
                        else
                        {
                            Console.WriteLine("Invalid username or password. Please try again.");
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("SQL Exception: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        static void DrawCoffeeMug()
    {
        Console.WriteLine();
        Console.WriteLine("      ( (  ");
        Console.WriteLine("       ) )  ");
        Console.WriteLine("    ........");
        Console.WriteLine("    |      ||]");
        Console.WriteLine("    \\      /");
        Console.WriteLine("     `----' ");
        Console.WriteLine();
    }

    static void PrintLine(int codeWidth, int nameWidth, int priceWidth)
    {
        Console.WriteLine($"+{new string('-', codeWidth)}+{new string('-', nameWidth)}+{new string('-', priceWidth)}+");
    }

    static void PrintRow(string productCode, string name, string price, int codeWidth, int nameWidth, int priceWidth)
    {
        Console.WriteLine(
            $"|{productCode.PadRight(codeWidth)}|{name.PadRight(nameWidth)}|{price.PadLeft(priceWidth)}|");
    }
    
    
    static int getCustomerCredentials(string email)
    {
        int customerId = 0;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string selectQuery = "SELECT customer_id FROM Customers where email=@email";
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                // Add the customerId parameter to prevent SQL injection
                command.Parameters.AddWithValue("@email", email);

                // Execute the query and fetch the single result
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    customerId = Convert.ToInt32(result);  // Convert the result to a string (customer name)
                }
            }
        }

        return customerId;  // Convert the List to an array before returning
    }
    static int getproductID(string productCode)
    {
        int productId = 0;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string selectQuery = "SELECT Product_id FROM Products where Product_code=@productcode";
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                // Add the customerId parameter to prevent SQL injection
                command.Parameters.AddWithValue("@productcode", productCode);

                // Execute the query and fetch the single result
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    productId = Convert.ToInt32(result);  // Convert the result to a string (customer name)
                }
            }
        }

        return productId;  // Convert the List to an array before returning
    }
    
    /*
     * I just could not be bothered creating new methods for individual queries, so i decided to create a method that can always be used
     * Interchangeably.
     *
     * Please note that when you want to return an int  or double result make sure that the TestReturn is `True` otherwise please make it false.
     */
    
static object? getSingleDetailFromDb(
    bool TestReturn, 
    string TableName, 
    string ColumnName, 
    string ColumnGet, 
    object ResultMatch)
{
    
    object? returnValue = null;

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        // Build the SQL query
        string selectQuery = $"SELECT {ColumnName} FROM {TableName} WHERE {ColumnGet} = @value";
        using (SqlCommand command = new SqlCommand(selectQuery, connection))
        {
            // Add the parameter, ensuring type safety by passing the ResultMatch as an object
            command.Parameters.AddWithValue("@value", ResultMatch);

            // Execute the query and fetch the result
            object? result = command.ExecuteScalar();

            if (result != null)
            {
                // Return the result directly as an object
                returnValue = result;
            }
        }
    }

    // Return the object, which could be a string, int, or double, depending on the query result
    return returnValue;
}
}

