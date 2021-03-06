﻿# AND Digital -  Challenge 5 - "Easy Does It" 

https://roguedungeondev.itch.io/and-droid

Problem:
Your task is write a miniature game :video_game:. The player is in charge of a prototype robot AND-Roid, which is sent into an active minefield. The player's task is to direct the robot with up/down/left/right commands safely through the minefield to the exit, without striking any mines.
The game map is provided here.

```
Map key:
R Robot start position
+ Wall (impassable)
# Empty space (passable)
^ Mine
E Exit
```

It's expected that your game will display the map and exit to the player, but obviously not the locations of the mines.

## Screenshots (Animated)

![Success](/screenshots/success.gif)
![Game Over](/screenshots/gameover.gif)

## Screenshots

![Room](/screenshots/room.png)
![Success](/screenshots/success.png)
![Game Over](/screenshots/gameover.png)

## Resources
'Pithazard' Font from: 
http://www.thealmightyguru.com/GameFonts/Series-ResidentEvil.html

Particle engine sourced (and modified) from the following MonoGame tutuorial:
http://rbwhitaker.wikidot.com/2d-particle-engine-1


## Building and running

To build, install dotnet core 3+ and run: `dotnet build` and `dotnet run`

If you cannot build locally, you can download the latest version build via my GitHub Actions or Releases tab


## Notes

Image assets are compiled as part of the build process but fonts have been compiled and committed to git 
because fonts need to be installed on the build agents or users machine
