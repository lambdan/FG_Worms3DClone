# Worms 3D Clone Assignment

- Can be played with just humans, just AI's, or humans and AI's
- Can be played with both Keyboard and Controller

## Features Implemented

Everything.

Aiming for grade: VG

### General

- Play Scene
- Main Menu and Game Over Scene
	- Technically not a Game Over scene, but the Play Scene says Game Over for a while before going back to the main menu
- Pause Menu and Settings Screen
	- Pause Menu can restart the match or quit to the main menu
	- In the main menu you can adjust settings

### Turn Based Game

- Turn based local multiplayer using the same input device (keyboard/mouse/gamepad)
- Up to n players supported
	- No engine limit. It's limited by how many spawn points the level has (Flatland has 9, Forest has 4).
- Simple AI Opponent

### Pickups

- 4 different pickups related to worm health:
	- Health pickup (the red cross)
	- Max HP Increase (the green cross)
	- Temporary Invincibility (the star)
- Pickups for each weapon
	

### Terrain

- Basic Unity Terrain
- Simple destructible terrain (shrinks when you shoot them until they're so small they get destroyed)

### Misc

- High score that is kept through game sessions, keeping track of score, player name and date

### Player

- A player only controls one worm at a time, but can switch between all the alive worms on their team
- Has hit points
- Custom character controller 
- Worm can only move a limited range (time limited)

### Camera

- Camera focuses and follows active player
- Camera movement using mouse or right stick on gamepad

### Weapon System

- 4 weapons. 
	- 3 of them use objects that get instantiated
	- 1 of them use raycasting
- Weapons have ammo and needs to reload

## Controls

Controls are listed in the top-right of the screen during gameplay. They update according to what device you are using.

### Keyboard/Mouse:

- WASD/Arrow Keys: movement
- Q: switch weapon
- E: switch worm
- CTRL: fire weapon
- Space: jump
- C: recenter camera

- Mouse: camera movement
- Left Mouse Button: fire weapon

### Gamepad:

- Left Stick: movement
- Right Stick: camera controls
- Y: switch weapon
- X: attack
- A: jump
- RB: switch worm
- Right Stick Click: recenter camera


## Screenshots

![2022-09-21_08 57 10 428_Worms3DClone](https://user-images.githubusercontent.com/1690265/191438325-918786e9-23dd-4fdd-960d-b7bf7bcd8369.png)

![2022-09-21_08 56 47 718_Worms3DClone](https://user-images.githubusercontent.com/1690265/191438344-afebabe2-6f61-42cd-bed9-3b23ff7ea2c8.png)
