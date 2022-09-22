# Worms 3D Clone Assignment

- Can be played with just humans, just AI's, or humans and AI's
- Can be played with both Keyboard and Controller

## Features Implemented

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
- Up to 8 players supported
	- No engine limit, just that the map only has 8 spawn points
- Simple AI Opponent

### Terrain

- Basic Unity Terrain

### Misc

- High score that is kept through game sessions
	- It records most turns played in a single game

### Player

- A player only controls one worm at a time, but can switch between all the alive worms on their team
- Has hit points
- Custom character controller
- Worm can only move a limited range (time limited)

### Camera

- Camera focuses and follows active player
- Camera movement using mouse or right stick on gamepad

### Weapon System

- 3 Weapons. You spawn with all of them from the start.
	- Pistol: fairly standard pistol. Deals 1 damage.
	- Orange Rifle: fires rolling oranges. Deals 10 damage.
	- Paper Planes: a paper plane that doesn't reach very far but deals 99 damage.
	

## Controls

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
