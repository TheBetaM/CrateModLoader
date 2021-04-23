Crate Mod Loader v1.4.0
The all-in-one Randomizer and Mod Loader for console games.  
It detects, extracts, modifies and rebuilds disc image files and directories in an easy to setup way.  
In addition, it allows for standalone "Mod Crates" to be installed seamlessly into games.

Website: https://github.com/TheBetaM/CrateModLoader/

Contributors:

-BetaM (https://www.youtube.com/BetaM) (Creator) (Support development: https://ko-fi.com/thebetam)
-ManDude (https://github.com/ManDude) 

Mod Crates

Place Mod Crates in the "Mods" folder for them to appear in the list.
An example mod can be found in the "Mods" folder.
Mod Crates can be folders or standard .zip files with these contents: (only ModCrateInfo.txt is mandatory for them to function, the rest is optional)
- ModCrateInfo.txt with the details of the mod
- ModCrateSettings.txt game-specific settings that can't be changed with modded files
- ModCrateIcon.png icon of the mod displayed in the program
- "layer0" folder - any files and folders inside will replace or add to the base extracted contents of the game ROM
- "layer1" "layer2" etc. folders - supported games allow for replacement and/or addition of files in different game archives (check compatibility in Games.txt)

Tools Credits

DiscUtils (https://github.com/DiscUtils/DiscUtils) (MIT license) 
PS2ImageMaker (https://github.com/Smartkin/PS2ImageMaker) (GPL-3.0 licensee)  
WQSG_UMD_kkMod (https://github.com/KyousukeKyaa/WQSG_UMD_kkMod) (GPL-2.0 license)  
extract-xiso (https://github.com/XboxDev/extract-xiso) (license included)  
xextool (http://xorloser.com/blog/?p=395) (freeware)  
Wiims ISO Tools (https://wit.wiimm.de/) (GPL-2.0 license)  
Gamecube ISO Tool (http://www.wiibackupmanager.co.uk/gcit.html) (freeware)

License Information (for more information check /Tools/License.txt)

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see https://www.gnu.org/licenses/.