# CreditWorks

To run this you will need Visual Studio and a Microsoft SQL Server Management

To setup database first run the "CreditWorksDB.sql" file in your microsoft sql server modifiying the "FILENAME" fields to your required folder. Then run the "CreditWorksDB - tables.sql" file inside the newly created database. Finaly, run the "Manufacturer and Category Values.sql" file to populate the manufacturer and weight category tables to ensure vehicles can be created right away.

Once the database is created modify the CreditWorksContext connection string in the "appsettings.json" file.

Run the program from Visual Studio by either pressing "F5" or going to "Debug > Starrt Debugging".

Tests can be run through "Test > Run all tests"


NOTES:

This program is built using .NET 10.0 written in C# using MVC as a frontend framework. 

It's database Consists of three tables: 
	- Vehicles: Which contains all the vehicle objects including their weight and a rreference to Manufacturer
	- Manufacturers: Which is the list of all manufacturers
	- WeightCategories: Which has the names, icon references and the start and end weights for each category

Weight categories are calculated when vehicles are fetched and displayed. To calculate this I simply compare the weight of the vehicle to the start and end weights of the category. If it is equal to or greater than the start weight and less than the end weight - unless the end weight is null in which case it is always considered less than the end weight - then the vehicle is considered a part of that category.

Weight categories however, could potentially have gaps. That is why we ensure there are no no gaps whenever a change to the weights happen. We compare any chages to start and end weights with their closest category in each direction and if there is a gap or any overlap, the old or unedited categories get their start or end weight changed to match with the new value.

To add a new icon for weight categories, you must first place the icon image into the "wwwroot/Content/Img" folder.Then, when setting the icon for the category type in "/[image name].[extension]".





