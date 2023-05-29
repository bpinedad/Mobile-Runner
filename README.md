# G-Run
#### Video Demo:  https://youtu.be/s2xvge7A8ec
#### Description:
G-Run is a scrolling game that consists of the player moving forward to achieve the highest score/distance possible without falling or dying. Accross the levels, there are platfmors on the floor and on the ceiling, the player must modify its own gravity to avoid falling to the void.
Along with this, random blocks with turrets will appear continously shooting, move and get as far as possible while avoiding falling or being hit by the turrets.

The initial screen shows instructions and 2 buttons. One to quit the application and one to play the game.

The second state is the actual game, on which player is able to move left, right or change gravity. During game floating blocks can be pushed and they will move around as if no gravity, this can be useful to move turrets when they are blocking the player.
The player has a idle, running, floating, pushing and dead animations called accordingly to the actions happening at the moment.
The score increases only when the playe rmoves forward, it is displayed on the top left corner.

The last screen is the game over, here the player can go back to the main menu or go back to the game. The final score is displayed here.

# Controls
* Arrow keys to move left or right
* Space to change gravity (Can change gravity only when touching a surface with your feet)

# Scripts
* Block.cs: Contains logic to create blocks floating in space, including their randomized turrets they may have.
* Feet.cs: Logic related to player feet, which is used to allow player to change gravity only when touching a surface.
* FinalScore.cs: Used to display score on game over screen.
* GameManager.cs: Using singleton to handle audio accross different scenes and also score.
* GameOver.cs: Handles conditions that would trigger the game over, such as falling or being shoot by a turret.
* GameOverAnimation.cs: To roate character on last game over screen.
* LightBlink.cs: Blinking lights on player shoulders.
* Menu.cs: Logic for menu buttons.
* PlayerMove.cs: Logic related to player moving with arrow keys and space bar.
* Score.cs: Logic to calculate score. Calculated depending on distance travelled.
* SelfDestruct.cs: Used to destroy objects out of current window, forcing player to move forward.
* TurretShooting.cs: Handles turrets shooting.
* WorldGenerator.cs: Generates turrets and platforms as the player moves forward. 

# Assets
* Futuristic Planel Textures: For floor and block textures.
* Robo's turret: For turrets in game
* Sci-fi Music Pack 1: For audio.
* Space Robot Kyle: For player model.
* StarField Skybox: For background.
* Used Mixamo for animations.

# World generation algorithm
The algorithm for world generation works on steps of 5 units. First the tiles are checked, there is a probability of 65% to add a tile, if not a space is left as an obstacle. To avoid having impossible levels, if a space was already added, next tile is forced to appear, avoiding impossible gaps.

Then, there is a probability of 50% to add a block which could contain turrets. This turret will appear at the cente +-2 units on Y axis. Once the block is created, this iterates over each side of the block and has a 40% of adding a turret on a random direction (left or right)

# Challenges
The first challenge was deciding the mechanics, while experimenting i liked changing the direction of the player so that helped to determine the direction of the game early on.

The next challenge was the world generation, I had to experiment with many combinations and probabilities until I found what i believed made the game compliated enough without being also impossible.

The last challenge was randomizing the turrets, at first i wasn't sure how to do this and my first thought was to generate a prefab of all possible combinations, but this would have been a very bad design, at the end I was able to randomly add a prefab into another prefab, creating all possible combinations of block with turrets only with code and 2 saved prefabs.

