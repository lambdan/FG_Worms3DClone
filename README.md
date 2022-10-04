# Worms 3D Clone Assignment

- Can be played with just humans, just AI's, or humans and AI's
- Can be played with both Keyboard and Controller

## Features Implemented

Everything.... I think? See the comments, especially on the ones marked with ⚠️.

Aiming for grade: VG

## General

| Feature                                                      | Implemented | Comment                                                                                                            |
|:------------------------------------------------------------:|:-----------:|:------------------------------------------------------------------------------------------------------------------:|
| (G) Only play scene is required                              | ✅           |                                                                                                                    |
| (VG, small) Add main menu (start) scene and game over scene  | ⚠️           | There is a Menu scene and PlayScene. Game Over is displayed in the PlayScene before going back to Main Menu scene. |
| (VG, medium) Implement Pause menu and settings menu          | ⚠️           | There is a pause menu. Settings are done on the main menu before starting a game.                                  |

## Turn based game

| Feature                                                                        | Implemented | Comment                                                                                                                                                     |
|:--------------------------------------------------------------------------------:|:-------------:|:-------------------------------------------------------------------------------------------------------------------------------------------------------------:|
| (G) You can have two players using the same input device taking turns.         | ✅           | Keyboard, Keyboard/Mouse and Gamepads supported                                                                                                             |
| (VG, large) Support up to 4 players (using the same input device taking turns) | ✅           | Up to *n* players supported. Limited by how many home bases the map has.                                                                                    |
| (VG, large) Implement a simple AI opponent.                                    | ✅           | It is very simple. It finds the nearest enemy, looks at it, and starts walking straight towards it until its in firing range, and then it starts blasting. It tries to avoid obstacles and grabs ammo when needed. |

## Terrain

|                                               Feature                                               | Implemented | Comment |
|:---------------------------------------------------------------------------------------------------:|:-----------:|:-------:|
| (G) Basic Unity terrain or primitives will suffice for a level                                      | ✅           |         |
| (VG, large) Destructible terrain (You can use Unity's built in terrain or your own custom solution) | ⚠️           |Nothing fancy, it just shrinks when you fire on them until they're so small that I destroy them|

## Player

|                                          Feature                                         | Implemented |                                          Comment                                         |
|:----------------------------------------------------------------------------------------:|:-----------:|:----------------------------------------------------------------------------------------:|
| (G) A player only controls one worm                                                      | ✅           | One worm is controlled at a time. You can switch between the worms in your team however. |
| (G) Use the built in Character Controller. Add jumping.                                  | ❌           | I made my own for the VG - see below                                                     |
| (G) Has hit points                                                                       | ✅           |                                                                                          |
| (VG, small) Implement a custom character controller to control the movement of the worm. | ✅           | Moving, rotation and jumping supported                                                   |
| (VG, small) A worm can only move a certain range                                         | ⚠️           | Limited by time                                                                          |
| (VG, medium) A player controls a team of (multiple worms)                                | ✅           | Up to *n* worms supported. I've capped it at 20 for now.                                 |

## Camera

|              Feature              | Implemented |                 Comment                |
|:---------------------------------:|:-----------:|:--------------------------------------:|
| (G) Focus camera on active player | ✅           |                                        |
| (VG, small) Camera movement       | ✅           | You can spin it around the active worm |

## Weapon system

|                                                                                                           Feature                                                                                                          | Implemented |                                         Comment                                        |
|:--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:-----------:|:--------------------------------------------------------------------------------------:|
| (G) Minimum of two different weapons/attacks, can be of similar functionality, can be bound to an individual button, like weapon 1 is left mouse button and weapon 2 is right mouse button                                 | ✅           | 4 different weapons. You swap between them.                                            |
| (VG, small) a weapon can have ammo and needs to reload                                                                                                                                                                     | ✅           |                                                                                        |
| (VG, medium) The two types of weapons/attacks must function differently, I.E a pistol and a hand grenade. The player can switch between the different weapons and using the active weapon on for example left mouse button | ✅           | I have 2 weapons firing "physical" projectiles, 1 throwable and 1 that uses raycasting |

## (VG, medium) Pickups

|                                           Feature                                           | Implemented | Comment |
|:-------------------------------------------------------------------------------------------:|:-----------:|:-------:|
| Spawning randomly on the map during the play session                                        | ✅           |         |
| Gives something to the player picking it up, I.E health, extra ammo, new weapon, armour etc | ✅           |         |

## (VG, medium) Cheat functionalities

|                             Feature                            | Implemented |                                           Comment                                          |
|:--------------------------------------------------------------:|:-----------:|:------------------------------------------------------------------------------------------:|
| Two different cheats, I.E Invincible, all weapons on start etc | ✅           | F1 = Make current worm invincible. F2 = Give all weapons and lots of ammo to current worm. |

## Miscellaneous

|                                             Feature                                            | Implemented |                 Comment                |
|:----------------------------------------------------------------------------------------------:|:-----------:|:--------------------------------------:|
| (VG, medium) Battle royal, danger zones that move around on the map after a set amount of time | ✅           |                                        |
| (VG, medium) High score that is persistent across game sessions                                | ✅           | Also keeps track of team name and date |

## Also implemented even though no one asked for it:

|       Feature      | Implemented |                                                                Comment                                                               |
|:------------------:|:-----------:|:------------------------------------------------------------------------------------------------------------------------------------:|
| Sound Effects      | ✅           | Menu selections, pickups, taking damage, gun fire                                                                                    |
| Controller Hints   | ✅           | Shown in the top right during gameplay. Updates accordingly with what controller you're using (KB/M, Playstation and Xbox supported) |
| 2 different levels | ✅           |                                                                                                                                      |
| HUD                | ✅           | Team alive/score, controller hints, team name, worm name, time limit, ammo, and turns played is shown                                |

## Controls

Controls are listed in the top-right of the screen during gameplay. They update according to what device you are using (tested with keyboard/mouse, Playstation controller and Xbox controller). 

|      Action      |      Keyboard      |       Mouse       | Playstation Controller |     Xbox Controller    |
|:----------------:|:------------------:|:-----------------:|:----------------------:|:----------------------:|
| Movement         | WASD or arrow keys |                   | Left Stick             | Left Stick             |
| Camera Movement  |                    | Move the mouse    | Right Stick            | Right Stick            |
| Camera Re-center | C                  |                   | R3 (Right Stick Click) | RS (Right Stick Click) |
| Jump             | Space              |                   | X                      | A                      |
| Fire/Attack      | CTRL               | Left Mouse Button | Square                 | X                      |
| Reload           | R                  |                   | Circle                 | B                      |
| Switch Weapon    | Q                  |                   | Triangle               | Y                      |
| Switch Worm      | E                  |                   | R1                     | RB                     |
| Pause Menu       | ESC                |                   | Start (Options)        | Start                  |


## Screenshots

![2022-09-21_08 57 10 428_Worms3DClone](https://user-images.githubusercontent.com/1690265/191438325-918786e9-23dd-4fdd-960d-b7bf7bcd8369.png)

![2022-09-21_08 56 47 718_Worms3DClone](https://user-images.githubusercontent.com/1690265/191438344-afebabe2-6f61-42cd-bed9-3b23ff7ea2c8.png)
