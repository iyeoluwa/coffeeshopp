This is a Readme file / documentation of the the webstars project called 

**<h1>Webstars Coffee</h1>**


     ( (  
      ) )  
    ........
    |      ||]
    \\      /
     `----' 


The program primarily works on the command line interface (CLI) so the design is pretty basic, but we made sure the interface is very clean and easy to maneuver.


## Table of Contents

1. [How To Maneuver The Interface](#how-to-maneuver-the-interface)
    - [The Signup Page](#the-signup-page)
    - [The Login Page](#the-login-page)
    - [Post-Login Options](#post-login-options)
2. [The Menu Page](#the-menu-page)
3. [The Order Page](#the-order-page)
    - [Creating Orders](#creating-orders)
    - [Making a Single Order](#making-a-single-order)
    - [Making Multiple Orders](#making-multiple-orders)
    - [Editing Orders](#editing-orders)
    - [Deleting Orders](#deleting-orders)


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
    \\      /
     `----' 
-----------------------------------------------
--------------------------------------------
----------------------------------------


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
    \\      /
     `----' 
-----------------------------------------------
--------------------------------------------
----------------------------------------


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
    \\      /
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
    \\      /
     `----' 
-----------------------------------------------
--------------------------------------------
----------------------------------------

1. Show Menu
2. Order Page
3. Logout
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

```
1. Make An Order
2. See what you Ordered
3. Edit your Order
4. Delete your order your Order
5. Back to Previous Menu
Please choose any valid option: 
```

###### Creating Orders
This page possess two major functionalities, and they include creating one order or multiple orders 
```

Please choose any of the options ;) :2
1. Make a single Order
2. Make Multiple Orders
Please choose an option (1/2) to place your order: 

```
###### Making a single Order

To make things less complex for the customer we deciced tomake a dedicated page for single and double orders


````
+---------------+---------------+--------------------+----------+
|Product Id     |Product Code   |Item Name           |     Price|
+---------------+---------------+--------------------+----------+
|1              |cap123         |Cappuccino          |     $4.50|
|2              |hc11           |Hot Chocolate       |     $2.50|
|3              |mk221          |Milkshake           |     $5.50|
|4              |pj123          |Pinapple Juice      |     $7.50|
|5              |aj1234         |Apple juice         |     $7.50|
+---------------+---------------+--------------------+----------+

 

Please note that in this page you can only make a single order and Our item menu is place above for your convinience:
Kindly type the item code to place your order

````

###### Making multiple Orders

```
+---------------+---------------+--------------------+----------+
|Product Id     |Product Code   |Item Name           |     Price|
+---------------+---------------+--------------------+----------+
|1              |cap123         |Cappuccino          |     $4.50|
|2              |hc11           |Hot Chocolate       |     $2.50|
|3              |mk221          |Milkshake           |     $5.50|
|4              |pj123          |Pinapple Juice      |     $7.50|
|5              |aj1234         |Apple juice         |     $7.50|
+---------------+---------------+--------------------+----------+

 

You can make multiple orders on this page and Our item menu is place above for your convinience:
Kindly type the item code to place your order
Please enter the product codes for the orders (separated by spaces):


```

###### Editing Orders

```
+---------------+---------------+--------------------+----------+
|Order Id       |Product Code   |Item Name           |     Price|
+---------------+---------------+--------------------+----------+
|3              |mk221          |Milkshake           |     $5.50|
+---------------+---------------+--------------------+----------+
|Total Amount   |               |                    |     $5.50|
+---------------+---------------+--------------------+----------+


Enter the Order ID you want to edit:


```
To make an edit to the order page you need to type in the Order Id 


###### Delete Orders

```
+---------------+---------------+--------------------+----------+
|Order Id       |Product Code   |Item Name           |     Price|
+---------------+---------------+--------------------+----------+
|3              |mk221          |Milkshake           |     $5.50|
+---------------+---------------+--------------------+----------+
|Total Amount   |               |                    |     $5.50|
+---------------+---------------+--------------------+----------+


Enter the Order ID you want to delete:


```
To make an edit to the order page you need to type in the Order Id 


In General we have a fully functioning console that is extremely easy to navigate
