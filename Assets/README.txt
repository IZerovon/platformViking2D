Made by: MrVokan /Admiral Dolphin || Start date: 23.Nov 2017
============================================================
Assets used:

Bayat Games - Free Platform Game Assets @assetstore.unity.com
Kenney Vleugels(www.kenney.nl) - Platformer Pack Redux
------------------------------------------------------------
Assets made:

Music, Scheming Weasel, based on sheetmusic (Original composition by Kevin MacLeod @incompetech.com)
Music, The Cannery, based on sheetmusic (Original composition by Kevin MacLeod @incompetech.com)
Music, Vanilla Dome, based on sheetmusic & MIDI files (original composition by Koji Kondo)

------------------------------------------------------------
To do list:
- Scene transitions
- Make Level 2
- Make 'Secret' Level
- Make transition effect to 'Secret' Level
- Make player 'jump' animation
- Add health-up pickups
- Add system to store -1- health-up pickup if hp full, used by keypress
- Secret Level
- Animate Title Screen
- set up enemy-only collision boxes to prevent enemies from dropping off the world

- Set 'death' by killbox to damage the player in addition to reset their position, aprox 2 - 2,5 dmg should be good
- Fix so if player hp <= 0, return to title, do not update score or coins

--------------------------------------------------------------
Stuff-that-doesn't-work list:
- Fix health slider not going down when hit
- Highscore calculation on level end, highscore on title screen isn't updated either


--------------------------------------------------------------
Ideas:
- Some worldblocks (blocks with many sprites forming complex shapes) use polygon colliders instead of 2DBox colliders.
^ Is 1 polygon collider more performance friendly than several box colliders?
- Make a level with a dynamic light fixed to the camera to shine a cone of light down on the player.
The rest of the level is dark and the player can only see the area immediately around them.
^ Dungeon/underground theme? New enemy type for the area?

- Make a water themed level consisting only of moving wooden platforms.
^ Each platform would require their own animation. Horizontal + vertical platforms.
^ Platforms that move in a circle?