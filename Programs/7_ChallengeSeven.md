# Komodo Barbeque

Assignment: Komodo requested a console app to track Barbecue parties (referred to as "parties") and sales of the products as various booths. Each guest gets one ticket for each type of booth (i.e. Hamburger booth, Treats booth). To accomplish this, I created the following assemblies:

- [7_ChallengeSeven_Console](../7_ChallengeSeven_Console)

- [7_ChallengeSeven_Repository](../7_ChallengeSeven_Repository)

- [7_ChallengeSeven_UnitTests](../7_ChallengeSeven_UnitTests)

The Console assembly contains several `ConsoleUI` files:

- `ConsoleUI` contains the main menu as well as a method that populates the program with dummy data for testing.
-  `ConsoleUI_Party`, `ConsoleUI_Booth`, and `ConsoleUI_Product` contain menus dealing with the associated classes in the repository assembly.

The repository holds these objects.

- The `PartyRepository` keeps track of the CRUD methods for the `Party` class. It also contains a method for randomizing the tickets exchanged for each party.
- Each `Party` can have multiple `Booth` objects.
- Each `Booth` can sell multiple `Product` objects.
- Each `Product` is made up of a list of `Ingredient` objects. This class also keeps track of the number of tickets exchanged (i.e. the number of products sold).
- Each ingredient has a name and cost.
- When any Party, Booth, or Product is asked for a `TotalCost`, it sums up the costs of the objects below it.
- Similarly, when any Party, Booth, or Product is asked for how many tickets were exchanged, it sums up the `TicketsExchanged` in the objects below it.

Finally, the UnitTests assembly contains the various tests needed to check that the `PartyRepository` works as expected. There are also unit test files for the `Party`, `Booth`, and `Product` classes since they contained methods for adding, removing, and updating sub-objects.

Some neat features that I added:

- Each page has a navigation bar at the top. It helps keep track of where the user is at throughout the various layers of menus. If the navigation text gets too long, it begins to replace parent "directories" with dots.
- As the Console assembly grew, I moved the common methods to a `ConsoleUI_FormattingHelpers` file to centralize those methods. Most of those methods are for processing user inputs, formatting the outputs, or keeping track of the navigation.
- The main menu has an option to randomize the tickets sold for each product in the dummy data.

## Resources

- [GitHub Repository](https://github.com/znichols1131/GoldBadgeChallenges)
- [Challenge Requirements](https://elevenfifty.instructure.com/courses/745/pages/challenge-7-barbecue?module_item_id=64746)
- [Grading Rubric](https://elevenfifty.instructure.com/courses/745/assignments/15250?module_item_id=64739)

---

[Back to home](../README.md)