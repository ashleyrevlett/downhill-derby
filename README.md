## Downhill Derby

####Controls
* Hit space repeatedly at the starting line to push off
* Use the arrow keys or WASD to steer
* Escape to pause and restart

####Dev Notes
* The main menu is a scene containing the GameController and main menu UI.
* The GameController (GC) maintains overall game state and level settings.
* Each racing level is a different scene. Each racing level must contain a LevelController.
* The LevelController (LC) maintains in-level state and UI (countdown, timer, etc).
* Racing levels require a GameController in the scene, but a GC is loaded by the LevelController if one is not already present. Do not include a GC in the race level itself.

####To do's
* See [Trello board](https://trello.com/b/I6nFUSMJ/boxcar-derby) for the current to do list
