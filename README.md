# MovieRental Exercise

This is a dummy representation of a movie rental system.
Can you help us fix some issues and implement missing features?

 * The app is throwing an error when we start, please help us. Also, tell us what caused the issue.
    - The app is throwing an error on Program.cs due to the DI service for RentalFeatures being defined as Singleton.
		- The service can't be a Singleton because it depends on MovieRentalDbContext that is a scoped service.
  		- The service shouldn't be Transient since inside the same request it will create new objects.
    	- The service should be Scoped as the dbContext is also scoped and we want it to create an object that will last until the request is done.

 * The rental class has a method to save, but it is not async, can you make it async and explain to us what is the difference?
    - The method being async allows multiple requests to be made without having to wait for the data of one to be saved before it can run another one.

 * Please finish the method to filter rentals by customer name, and add the new endpoint.
    - A method GetRentalsByCustomerName was created in RentalFeatures and a get enpoint in RentalController was implemented and receives a customerName to search rentals for.
 
 * We noticed we do not have a table for customers, it is not good to have just the customer name in the rental.
   Can you help us add a new entity for this? Don't forget to change the customer name field to a foreign key, and fix your previous method!
	- Added a Customer entity
   		- I wanted to add Migrations as I've used Migrations in my last project but I was having some issues getting the correct updates working. As of this moment I'm focusing on delivering a working solution but in a setting with a bit more time I would 100% implement it and keep it updated for every database update.
 
 * In the MovieFeatures class, there is a method to list all movies, tell us your opinion about it.
    - The method will crash in case there's an error connecting to the database or fetching the data.
   		- To avoid this issue we should try to catch that and throw an exception.
    - The lack of pagination in a large dataset can cause performance issues.
    	- To solve this issue we can allow the user to specify the ammount of items per page and what page number they want to get.
    - Not an issue but I also took the oppurtunity to include a filter by availability that is useful to show only available movies to customers but still allow admins to see a list in a management area. 

 * No exceptions are being caught in this api, how would you deal with these exceptions?
   	- Adding differents checks in the methods and make sure these exceptions are propagated upwards.


	## Challenge (Nice to have)
We need to implement a new feature in the system that supports automatic payment processing. Given the advancements in technology, it is essential to integrate multiple payment providers into our system.

Here are the specific instructions for this implementation:

* Payment Provider Classes:
    * In the "PaymentProvider" folder, you will find two classes that contain basic (dummy) implementations of payment providers. These can be used as a starting point for your work.
* RentalFeatures Class:
    * Within the RentalFeatures class, you are required to implement the payment processing functionality.
* Payment Provider Designation:
    * The specific payment provider to be used in a rental is specified in the Rental model under the attribute named "PaymentMethod".
* Extensibility:
    * The system should be designed to allow the addition of more payment providers in the future, ensuring flexibility and scalability.
* Payment Failure Handling:
    * If the payment method fails during the transaction, the system should prevent the creation of the rental record. In such cases, no rental should be saved to the database.

