GameCube Rebuilder version 1.1 by BSV (bsv798@gmail.com)
Compiled: September 2016.
---------------------------------------------------------

GameCube Rebuilder tool (GCR next) allows you to edit Nintendo GameCube images.
It was witten with Visual C# v5.0 and thus REQUIRES a Microsoft .NET Framework v4.5.2 or newer installed.
You can get it for free at https://www.microsoft.com/ru-ru/download/details.aspx?id=42642


Main features:


- extact (export) files and folders from image;
- replace (import) files in image;
- make (re/build) a new working image from previously extracted image's files;
- wipe garbage from image;
- change banner information;

and also:
- export banner to a Windows BMP file (and edit it in you favourite editing program) 
- import it back to image (edited or not).


---------------------------------------------------------
"Image" menu commands:
---------------------------------------------------------


Image -> Open...
----------------

- Opens a Nintendo GC image. You can choose *.iso, *.gcm file formats or "All files".
  (note, that .iso and .gcm are the same type of GameCube images. 
  Difference is in file extention only). 
  While opening, GCR reads all files in image regarding to info in "game.TOC" file.
  If it can't find some nessesary file or there are errors in game.toc file, 
  the image won't be opened.

Image -> Close
----------------

- Closes current image and clears all fields in GUI, 
  allowing you to open another one via "Image" or "Root" menu.

Image -> Wipe garbage 
----------------

An original GameCube images have a random garbage data between files added
by Nintendo for some strange reason. Such data doesn't affect to gameplay in any way
(we don't know this for sure, but all "wiped" GC images works just fine).
The "wipe garbage" command replaces this random data by zero bytes and let you
achieve a much more powerful compression rates when archiving 
(e.g. Rar'ing, Zip'ing) a "wiped" image.

- This process can be cancelled. (The "Wipe garbage" menu switches to "Cancel" while wiping.)
- Your new compiled GC image will not conatain such garbage. It will be wiped while compiling.


---------------------------------------------------------
"Root" menu commands:
---------------------------------------------------------

Root -> Open...
----------------

- Opens previously extracted image.

// You can extract the whole image to your HDD by opening it in GCR (Image->Open), 
// right clicking on "Root" folder in "Structure" field and choosing "Export..." command.


While opening, GCR looks for a "&&systemdata" folder 
with "iso.hdr", "apploader.ldr", "start.dol" and "game.toc" files in it.
If it can't find this, it won't load your extracted image.
  
  *See "Options -> "Do not use game.toc" command below

Then it reads game.TOC file and if it can't find nesessary files, listed in game.toc, 
your extracted image won't be opened. If there are some additional files in your folder, 
containing extracted image, GCR will just ignore them.

Root -> Save...
----------------

- Allows you to choose a file name for you new image before re/building it.
  You can choose *.iso or *.gcm file extension as well. 
  This is just an extension, so it doesn't matter, what you will choose.
  Your new image will be fully functional in any case.

Root -> Close
----------------

- Closes current extracted image and clears all fields in GUI, 
  allows you to open another one with "Image" or "Root" menu.

Root -> Rebuild
----------------

- Builds a new GameCube image to a file, named before via "Save..." command.
  While rebuilding, position of all files in image aligns by 2048 bytes.
  This MAY later allow you to import files a little bit bigger in size than original ones, 
  to your new rebuilded image (without rebuilding it again).

  After rebuilding you'll recieve a fully working GameCube image file 
  with file size of 1.35 Gb (1459978240 bytes). 
  Also, this new image will be already wiped from garbage.

---------------------------------------------------------
"Options" menu commands:
---------------------------------------------------------

// Note - this options only works BEFORE opening an extracted image (ROOT -> Open)
// When a usual GC image (Image -> Open) is opened, these options are disabled.


Options -> Modify system files
----------------

- Modifies "iso.hdr" and "game.toc" files in "&&systemdata" folder while building a new image.
  Use this option if you want these files to be identical in the "Root" folder and your new image.

Options -> Do not use 'game.toc'
----------------

- With this option enabled, GCR ignores information from "game.toc" file.
  It means that you can theorically build a new working GC image with ANY files in "Root" folder 
  (the "&&systemdata" folder must not contain additional files or folders). 
  The only limitation - is the file size of your new image. 
  It should be exactly 1.35 Gb (1459978240 bytes).
  All you'll need - is a correct "apploader.ldr" and "start.dol" files in "&&systemdata" folder
  to make such image work.

  // Use this options only IF YOU REALLY KNOW, what you are doing!!!
  // If you don't sure - than just left them disabled.

---------------------------------------------------------
"Help" menu:
---------------------------------------------------------

- Shows a short info about GCR.

- Shows command line interface parameters.

---------------------------------------------------------
"Image details" field:
---------------------------------------------------------

- Shows image information. This can NOT be changed with GCR.

---------------------------------------------------------
"Banner details" field:
---------------------------------------------------------

- Shows game's banner ("opening.bnr" file) info 
  (game title, developer, comments and a game's banner picture);
- allows you to edit all text info, but note, that each text field 
  has a limited space for typing;
- allows you to export/import banner picture to/from Windows bitmap (BMP) file 
  (note, that GCR supports only 16-bit Windows RGB5A1 BMP files with image size 
  of 96x32 pixels. While export/import alpha-channels will be ignored). 

All changes will be saved to image only after pressing "Save changes" button.

---------------------------------------------------------
"Structure" field:
---------------------------------------------------------

- Shows all directory/file structure in your image.

You can choose from one of the following viewing modes:

- "File names table", to see files in a Windows Explorer's style.
- "Addresses table", to see how all files physically located inside the image.

If you open a compiled (or a "usual"/"downloaded somewhere") Gamecube image (Image->Open), 
you'll be able to extract and replace files and folders in it:

 - To extract the whole image on your hard drive, right click on the "Root" folder, 
   choose "Export..." command and save it to your hard drive.

 - To export (extract) single file of folder, right click on it, 
   choose "Export" and save it to your hard drive. 
   You can NOT choose more than ONE file or folder at a time.
   The extraction of folder can be cancelled. To cancel it, right-click on "Structure" fild
   and choose "Cancel".

 - To replace (import) single file, right click on it, 
   choose "Import..." and select desired file on your disk. 
   You can NOT import a folder (yet). Only a single files are allowed to replace.

   // You can try to import files a little bit bigger in size, than original ones. 
   // If GCR finds enough space between file you want to replace and a file, 
   // which follows it in image, you can do such import.

To replace files or folders in extracted image, do this
in your favourite file manager like Explorer, etc.
and THEN load your extracted image via Root->Open.

---------------------------------------------------------
Some useful advices:
---------------------------------------------------------

- Drag-and-drop a GameCube image or a "Root" folder of an extracted GameCube image 
  to a program's icon and it opens it in a proper mode 
  (as "Root" or as "Image"), depending on what are you want to open.

- Save you new rebuilded image to another physical drive than your "Root" is, 
  if you want to make things faster.

- To pause/cancel any cancelable operation, just press ESC.

- Use command line interface to automate your work.


*********************************************************************

You can find the latest versions of GCR at following websites:

- http://shedevr.org.ru/zelda64rus/downloads.html#romhacking_gc
(this site is in russian, but you'll find the tool easily).
- http://www.romhacking.net/?page=utilities&platform=33&author=1147

*********************************************************************


---------------------------------------------------------
Thanks to:
---------------------------------------------------------

- Nintendo for making such excellent games that makes us spend whole nights long
  playing and translating them;

- Anton from "zelda64rus" for his support and testing the tool.


---------------------------------------------------------
Version history:
---------------------------------------------------------

Version 1.1 - September, 19, 2016
Command line interface support.

Version 1.0 - July, 12, 2009.
First public release.


**************************** DISCLAIMER ****************************

This program is freeware. It was written by BSV and is provided as is. 
There is no warranty that it will work and no warranty that it will not cause any damage.
Use this program at your own risk! If you don't agree with this - don't use it :)

You can NOT sell this program, or use it in any other commercial way.
You can NOT modify its code in any way.
You can NOT distribute it WITHOUT this text file.
In such cases, you MUST ask it's author (BSV - bsv798@gmail.com) first.




(c) 2016 BSV, All rights reserved.
