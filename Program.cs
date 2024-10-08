﻿using System;
using System.Data.SqlClient;
using System.Text;

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
    
    static Stack<Action> screenHistory = new Stack<Action>();
    
    

    static void Main(string[] args)
    {
     
        Console.ForegroundColor = ConsoleColor.Red; // i'm just faffing around ;)
        Console.Clear();


        mainMenu();

    }


    static void mainMenu()
    {
        while (true)
        {
            if (!isLoggedIn)
            {
                Console.WriteLine("welcome to Our console coffee shop");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("-----------------------------------------------");
                DrawWebstarsCoffeeLogo();
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1. Signup");
                Console.WriteLine("2. Login");
                Console.Write("Please choose an option (1/2): ");
                switch (Console.ReadLine())
                {
                    case "1":
                        NavigateTo(Signup);
                        break;
                    case "2":
                        NavigateTo(Login);
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
                DrawWebstarsCoffeeLogo();
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1. Show Menu");
                Console.WriteLine("2. Order Page");
                Console.WriteLine("3.Logout");
                Console.Write("Please choose any of the options ;) :");
                switch (Console.ReadLine())
                {
                    case "1":
                        NavigateTo(ShowMenu);
                        break;
                    case "2":
                    
                        NavigateTo(OrderPage);
                        break;
                    case "3":
                       // this is where the logout method will come in.
                       logout();
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
        Console.Clear();
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
                    PrintLine(codeWidth,codeWidth, nameWidth, priceWidth);
                    PrintRow("Product Id","Product Code", "Item Name", "Price", codeWidth, nameWidth, priceWidth);
                    PrintLine(codeWidth,codeWidth, nameWidth, priceWidth);
                    while (reader.Read())
                    {
                        string productCode = reader.GetString(4);
                        string itemName = reader.GetString(1);
                        double price = reader.GetDouble(2);
                        int productId = reader.GetInt32(0);
                        // Display each row with consistent column widths
                        PrintRow(productId.ToString(),productCode, itemName, price.ToString("C"), codeWidth, nameWidth, priceWidth);
                    }
                    PrintLine(codeWidth,codeWidth, nameWidth, priceWidth);
                }
            }
        }
    }

    
    static void DrawWebstarsCoffeeLogo()
    {
        int consoleWidth = Console.WindowWidth; // Get console width
        string horizontalBorder = new string('*', consoleWidth); // Full-width border

        // Helper function to center any given text
        void PrintCentered(string text)
        {
            int padding = (consoleWidth - text.Length) / 2;
            Console.WriteLine(new string(' ', padding) + text);
        }

        Console.WriteLine(horizontalBorder);
        PrintCentered("*                                            *");
        PrintCentered("*               WEBSTARS COFFEE              *");
        PrintCentered("*                                            *");
        Console.WriteLine(horizontalBorder);
        PrintCentered("*                                            *");
        PrintCentered("*                ( (        ( (              *");
        PrintCentered("*                 ) )        ) )             *");
        PrintCentered("*              ........    ........           *");
        PrintCentered("*             |        |  |        ||]        *");
        PrintCentered("*             |        |  |        |          *");
        PrintCentered("*             |        |  |        |          *");
        PrintCentered("*             \\        /  \\        /          *");
        PrintCentered("*              `------'    `------'           *");
        PrintCentered("*                                            *");
        PrintCentered("*          A cup of innovation               *");
        PrintCentered("*                                            *");
        Console.WriteLine(horizontalBorder);
    }

    
    static void PrintLine(int productID, int codeWidth, int nameWidth, int priceWidth)
    {
        Console.WriteLine($"+{new string('-', productID)}+{new string('-', codeWidth)}+{new string('-', nameWidth)}+{new string('-', priceWidth)}+");
    }

    static void PrintRow(string productID, string productCode, string name, string price, int codeWidth, int nameWidth, int priceWidth)
    {
        Console.WriteLine(
            $"|{productID.PadRight(codeWidth)}|{productCode.PadRight(codeWidth)}|{name.PadRight(nameWidth)}|{price.PadLeft(priceWidth)}|");
    }
    
    
    //these are the order methods 
    
    static void OrderPage()
    {
        Console.Clear();
        //this method is the parent method reposnible for the routing of everything that has to do with ordering
        Console.WriteLine("1. Make An Order");
        Console.WriteLine("2. See what you Ordered");
        Console.WriteLine("3. Edit your Order");
        Console.WriteLine("4. Delete your order your Order");
        Console.WriteLine("5. Back to Previous Menu");
        Console.Write("Please choose any valid option: ");
        
        switch (Console.ReadLine())
        {
            case "1":
                 NavigateTo(PlaceOrders);
                break;
            case "2":
                NavigateTo(showOrders);
                Console.WriteLine("\nPress B to go back.");
                if (Console.ReadKey().Key == ConsoleKey.B)
                {
                    NavigateBack();
                }
                break;
            case "3":
                
                // this is where the edit orders will be 
                NavigateTo(editOrders);
                break;
            case "4":
                Console.Clear();
                // this is where the delete orders will be
                NavigateTo(deleteOrders);
                break;
            case "5":
                NavigateTo(mainMenu);
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    static void PlaceOrders()
    {
        //this method handles the placing of orders
        //collect the order to be bought 
        // check if the user has an account/ or the user is logged in.
        Console.Clear();
        Console.WriteLine("1. Make a single Order");
        Console.WriteLine("2. Make Multiple Orders");
        Console.WriteLine("B. Go back to previous Menu");
        Console.Write("Please choose an option (1/2) to place your order: ");
        
        switch (Console.ReadLine())
        {
            case "1":
                Console.Clear();
                ShowMenu();
                Console.WriteLine("\n \n");
                Console.WriteLine("Please note that in this page you can only make a single order and Our item menu is place above for your convinience:");
                Console.WriteLine("Kindly type the item code to place your order");
                String code = Console.ReadLine();
                singleOrder(code);
                
                break;
            case "2":
                Console.Clear();
                ShowMenu();
                Console.WriteLine("\n \n");
                Console.WriteLine("You can make multiple orders on this page and Our item menu is place above for your convinience:");
                Console.WriteLine("Kindly type the item code to place your order");
                Console.WriteLine("Please enter the product codes for the orders (separated by spaces):");
                string input = Console.ReadLine();
    
                // Split the input string by spaces to get an array of product codes
                string[] productCodes = input.Split(' ');

                // Place multiple orders
                multipleOrders(productCodes);
                break;
            case "B":
                NavigateBack();
                
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
       
        
       
    }
    
    static void singleOrder(string productCode)
    {
        
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            int productId =  getproductID(productCode);
            string SelectQuery = "Insert into Orders (ProductId,CustomerId,Date,TotalAmount)VALUES (@productid,@customerid,@currentDate,@amount)";// this is the insert statement that inserts orders 
            using (SqlCommand command = new SqlCommand(SelectQuery, connection))
            {
                command.Parameters.AddWithValue("@productid", productId);
                command.Parameters.AddWithValue("@customerid", getCustomerCredentials(LoggedInUser));
                command.Parameters.AddWithValue("@currentDate", currentDate);
                command.Parameters.AddWithValue("@amount", getSingleDetailFromDb(true,"products","price","Product_code", productCode));
                command.ExecuteNonQuery();
            }
                
            Console.WriteLine("Order Placed successfully.");
        }
       
            NavigateBack();
      
    }

    static void multipleOrders(string[] productCodes)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            foreach (var productCode in productCodes)
            {
                int productId =  getproductID(productCode);
                string SelectQuery = "Insert into Orders (ProductId,CustomerId,Date,TotalAmount)VALUES (@productid,@customerid,@currentDate,@amount)";// this is the insert statement that inserts orders 
                using (SqlCommand command = new SqlCommand(SelectQuery, connection))
                {
                    command.Parameters.AddWithValue("@productid", productId);
                    command.Parameters.AddWithValue("@customerid", getCustomerCredentials(LoggedInUser));
                    command.Parameters.AddWithValue("@currentDate", currentDate);
                    command.Parameters.AddWithValue("@amount", getSingleDetailFromDb(true,"products","price","Product_code", productCode));
                    command.ExecuteNonQuery();
                }
            }
           
                
            Console.WriteLine("All orders placed successfully.");
        }
    }

    static void deleteOrders()
    {
        //this method shows all the orders of a particular user.
        
        Console.Clear();
        showOrders();
        Console.WriteLine("\n");
        Console.WriteLine("\n");
        Console.WriteLine("Enter the Order ID you want to delete:");
        int orderId = int.Parse(Console.ReadLine());

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string deleteQuery = "DELETE FROM Orders WHERE Order_id = @orderId";
            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@orderId", orderId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Order deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Order not found.");
                }
            }
        }

        Console.WriteLine("\nPress B to go back.");
        if (Console.ReadKey().Key == ConsoleKey.B)
        {
            NavigateBack();
        }
    }

    // now since we can add new orders, it's time to delete them, after-all you should be able to cancel order
    
    static void showOrders()
{
    Console.Clear();
    int customerId = getCustomerCredentials(LoggedInUser); // Get the customer ID
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        
        // Query to get ProductId from Orders for a specific customer
        string SelectQuery = "SELECT ProductId,Order_Id FROM Orders WHERE CustomerId = @userid";
        
        using (SqlCommand command = new SqlCommand(SelectQuery, connection))
        {
            command.Parameters.AddWithValue("@userid", customerId);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                // Define column widths
                int codeWidth = 15;
                int nameWidth = 20;
                int priceWidth = 10;
                
                // Print table header
                PrintLine(codeWidth,codeWidth, nameWidth, priceWidth);
                PrintRow("Order Id","Product Code", "Item Name", "Price", codeWidth, nameWidth, priceWidth);
                PrintLine(codeWidth,codeWidth, nameWidth, priceWidth);
                
                double totalAmount = 0;

                while (reader.Read())
                {
                    // Assuming the ProductId is an integer
                    int productId = reader.GetInt32(reader.GetOrdinal("ProductId"));
                    int OrderId = reader.GetInt32(reader.GetOrdinal("Order_Id"));

                    // Fetch the product details for this ProductId
                    string productCode = getSingleDetailFromDb(false, "Products", "Product_Code", "Product_Id", productId) as string;
                    string itemName = getSingleDetailFromDb(false, "Products", "ProductName", "Product_Id",  productId) as string;
                    double price = (double)getSingleDetailFromDb(true, "Products", "Price", "Product_Id",  productId);

                    // Display each row with consistent column widths
                    PrintRow(OrderId.ToString(),productCode, itemName, price.ToString("C"), codeWidth, nameWidth, priceWidth);

                    // Add to the total amount
                    totalAmount += price;
                }

              

                // Print the total order amount
                PrintLine(codeWidth,codeWidth, nameWidth, priceWidth);
                PrintRow("Total Amount","", "", totalAmount.ToString("C"), codeWidth, nameWidth, priceWidth);
                PrintLine(codeWidth,codeWidth, nameWidth, priceWidth);
            }
        }
    }
    
}


    static int countOrders()
    {
        //this method caounts the totak amount of orders that the user has in the database 
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            //type the query string 
            string queryString = "SELECT COUNT(ORDER_ID) FROM ORDERS where CustomerId = @customerid";

            using (SqlCommand command = new SqlCommand(queryString,connection))
            {
                command.Parameters.AddWithValue("@customerid",getCustomerCredentials(LoggedInUser));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int totalOrders = 0;
                    while (reader.Read())
                    {
                        totalOrders ++;
                    }
                    return totalOrders;
                }
            }
        }
    }
    
    static void editOrders()
{
    // this function provides the capacity to edit orders 
    Console.Clear();
    showOrders();
    Console.WriteLine("\n");
    Console.WriteLine("\n");
    Console.WriteLine("Enter the Order ID you want to edit:");
    int orderId = int.Parse(Console.ReadLine());
    Console.WriteLine("\n");
    Console.WriteLine("\n");
    Console.WriteLine("Here is the Item Menu For reference");
    Console.WriteLine("\n");
    ShowMenu();
    Console.WriteLine("Enter the new Item code  ID:");
    String newProductId = Console.ReadLine();
    
    int productId =  getproductID(newProductId);

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        string updateQuery = "UPDATE Orders SET ProductId = @productId WHERE Order_id = @orderId";
        using (SqlCommand command = new SqlCommand(updateQuery, connection))
        {
            command.Parameters.AddWithValue("@productId", productId);
            command.Parameters.AddWithValue("@orderId", orderId);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Order updated successfully.");
            }
            else
            {
                Console.WriteLine("Order not found.");
            }
        }
    }

    Console.WriteLine("\nPress B to go back.");
    if (Console.ReadKey().Key == ConsoleKey.B)
    {
        NavigateBack();
    }
}
    
    

    
    //these are the account operating methods 

    static void Signup()
        {
            Console.Clear();
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
            Console.Clear();
            Console.WriteLine("\n--- Login ---");

            Console.Write("Enter Your Email Address: ");
            string email = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = ReadPassword();

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
                            string name = getSingleDetailFromDb(false, "Customers", "name", "email", email) as string;
                            Console.Clear();
                            Console.WriteLine("Login successful! Welcome, " + name + ".");
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
    
    static void logout()
    {
        //this method helps to the customer logout 
        isLoggedIn = false;
        LoggedInUser = null;
        Console.Clear();
        Console.WriteLine("You are logged out.. Goodbye and have a nice day.");
    }
    
    //text masking method to keep the privacy of the password.
    static string ReadPassword()
    {
        StringBuilder password = new StringBuilder();
        ConsoleKeyInfo keyInfo;

        do
        {
            keyInfo = Console.ReadKey(intercept: true);  // Don't display the key
            if (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Backspace)
            {
                password.Append(keyInfo.KeyChar);  // Append the character to the password
                Console.Write("*");  // Display an asterisk to indicate typing
            }
            else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                // Handle backspace
                password.Remove(password.Length - 1, 1);
                Console.Write("\b \b");  // Remove the asterisk from console
            }
        } while (keyInfo.Key != ConsoleKey.Enter);  // Stop when Enter is pressed

        Console.WriteLine();  // Move to the next line
        return password.ToString();  // Return the password
    }
    
    //navigation methods 
    static void NavigateTo(Action screen)
    {
        screenHistory.Push(screen);
        screen();
    }

    static void NavigateBack()
    {
        // Remove the current screen and go back to the previous one
        if (screenHistory.Count > 1)
        {
            screenHistory.Pop(); // Pop the current screen
            var previousScreen = screenHistory.Peek(); // Get the previous screen
            previousScreen(); // Execute the previous screen
        }
        else
        {
            // If no previous screen, go back to the main menu
            mainMenu();
            
        }
    }


    //General purpose methods 
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
    
    static object? getSingleDetailFromDb(bool TestReturn, string TableName, string ColumnName, string ColumnGet, object ResultMatch)
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

