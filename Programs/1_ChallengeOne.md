# Komodo Cafe

Assignment: **Komodo Cafe** requires a console app that allows the manager to view, add, update, and remove menu items. To accomplish this, I created the following assemblies:

- [1_ChallengeOne_Console](../1_ChallengeOne_Console)

- [1_ChallengeOne_Repository](../1_ChallengeOne_Repository)

- [1_ChallengeOne_UnitTests](../1_ChallengeOne_UnitTests)

The Console assembly contains a `ConsoleUI` class that houses the framework for the user interface. Users are asked to input what they wish to do (i.e. view existing menu items) with basic error handling and feedback to keep them on track.

The Repository assembly contains a `MenuItemRepository` that houses the basic CRUD methods for a `MenuItem` class.

Finally, the UnitTests assembly contains the various tests needed to check that the MenuItemRepository works as expected.

Some neat features that I added:

- Users can enter in multiple ingredients separated by commas. All ingredients are stored as lowercase strings for conformity.

- If a user tries to add an ingredient that already exists, the program will now allow a duplicate ingredient.

- If the user tries to delete an ingredient that does not exist, they receive an error message.

## Resources

- [GitHub Repository](https://github.com/znichols1131/GoldBadgeChallenges)
- [Challenge Requirements](https://elevenfifty.instructure.com/courses/745/pages/challenge-1-cafe?module_item_id=64740)
- [Grading Rubric](https://elevenfifty.instructure.com/courses/745/assignments/15250?module_item_id=64739)

---

[Back to home](../README.md)