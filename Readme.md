This is a Readme file / documentation of the the webstars project called 

**<h1>Webstars Coffee</h1>**


     ( (  
      ) )  
    ........
    |      ||]
    \\      /
     `----' 


The program primarily works on the command line interface (CLI) so the design is pretty basic, but we made sure the interface is very clean and easy to maneuver.

## How To Maneuver The Interface
The intro page features a coffee emblem, and two options 

````
welcome to Our console coffee shop
----------------------------------------
--------------------------------------------
-----------------------------------------------

      ( (  
       ) )  
    ........
    |      ||]
    \      /
     `----' 

-----------------------------------------------
--------------------------------------------
----------------------------------------
1. Signup
2. Login
Please choose an option (1/2): 

````

The Signup and login as their name implies, are the way that a user can register/ use their details for most of the activities 
like making orders

#### The Signup page 
```
welcome to Our console coffee shop
----------------------------------------
--------------------------------------------
-----------------------------------------------

      ( (  
       ) )  
    ........
    |      ||]
    \      /
     `----' 

-----------------------------------------------
--------------------------------------------
----------------------------------------
1. Signup
2. Login
Please choose an option (1/2): 1

--- Signup ---
Enter Name: 

```

The Login Page 
```
welcome to Our console coffee shop
----------------------------------------
--------------------------------------------
-----------------------------------------------

      ( (  
       ) )  
    ........
    |      ||]
    \      /
     `----' 

-----------------------------------------------
--------------------------------------------
----------------------------------------
1. Signup
2. Login
Please choose an option (1/2): 2

--- Login ---
Enter Your Email Address: 

```

The login / register details are stored in a database (in the latter part of this documentation the database schema will be present in the Readme.)

Once you have have been logged in you will see a screen that looks like this 

```
Login successful! Welcome, fajimiiyeoluwa@yahoo.com.
welcome back to your favourite console coffee shop
----------------------------------------
--------------------------------------------
-----------------------------------------------

      ( (  
       ) )  
    ........
    |      ||]
    \      /
     `----' 

-----------------------------------------------
--------------------------------------------
----------------------------------------
1. Show Menu
2. Order Page
3.Logout
Please choose any of the options ;) :

```

This page gives you 3 options and they include 

##### [The Menu page](#the-menu-page) 

##### The Order page 

##### The Logout Page


##### The Menu Page: 
This page displays the menu available for sale in the cafe, it also shows the 
prices, item name and the ItemCode/ reference number.

```

+---------------+--------------------+----------+
|Product Code   |Item Name           |     Price|
+---------------+--------------------+----------+
|cap123         |Cappuccino          |     $4.50|
|hc11           |Hot Chocolate       |     $2.50|
|mk221          |Milkshake           |     $5.50|
|pj123          |Pinapple Juice      |     $7.50|
|aj1234         |Apple juice         |     $7.50|
+---------------+--------------------+----------+

----------------------------------------
--------------------------------------------
-----------------------------------------------

```

##### The Order Page: 
This Page allows to place orders it contains functionalities like creating, editing and deleting orders



###### Creating Orders
This page possess two major functionalities, and they include creating one order or multiple orders 
```

Please choose any of the options ;) :2
1. Make a single Order
2. Make Multiple Orders
Please choose an option (1/2) to place your order: 

```

