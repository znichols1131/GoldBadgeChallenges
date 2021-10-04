# Komodo Green Plan

Assignment: **Komodo Green Deal** requires a console app that allows the users to view, add, update, and remove cars of various types (electric, gas-powered, hybrid). To accomplish this, I created the following assemblies:

- [6_ChallengeOne_Console](../6_ChallengeOne_Console)

- [6_ChallengeOne_Repository](../6_ChallengeOne_Repository)

- [6_ChallengeOne_UnitTests](../6_ChallengeOne_UnitTests)

The Console assembly contains a `ConsoleUI` class that houses the framework for the user interface. Users are asked to input what they wish to do (i.e. view existing menu items) with basic error handling and feedback to keep them on track.

The Repository assembly contains a `CarRepository` that houses the basic CRUD methods for the `ElectricCar`, `GasCar`, and `HybridCar` classes. All three classes are children of `Car`, with the only differences being properties that hold the values of miles per fuel and fuel capacity.

Finally, the UnitTests assembly contains the various tests needed to check that the MenuItemRepository works as expected.

Some neat features that I added:

- I focused on using inheritance so that I only needed a respository and CRUD methods for the `Car` class instead of tripling this work for each class.

- I spent some extra time formatting strings to maintain table-like spacing and appropriate decimal points.

- I built out the `Car` class and its subclasses so that they have methods to return the average cost per mile and the total fuel range. This could aid future developers in completing the car comparison calculations mentioned in the assignment prompt.

- The update method ensure that the car is of the same fuel type.

## Resources

- [GitHub Repository](https://github.com/znichols1131/GoldBadgeChallenges)
- [Challenge Requirements](https://elevenfifty.instructure.com/courses/745/pages/challenge-6-green-plan?module_item_id=64745)
- [Grading Rubric](https://elevenfifty.instructure.com/courses/745/assignments/15250?module_item_id=64739)

---

[Back to home](../README.md)