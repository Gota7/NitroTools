# InfoTool
An extractor and packer for info.bin track info.

## Usage
InfoTool.exe inputFile outputFile

inputFile - INFO bin to convert to text, or text to convert to a INFO bin.
outputFile - The name of the output file.

## Features
* Perfect bytesize calculation.
* Should work with any* INFO from any SDAT.   _*See compatibility notes_
* Automatically can tell if input is text or info (based on extension).
* Can tell if input file is valid.
* Ability to add filenames to the list (must also match in symb.bin).

## Compatibility Notes
* DO NOT use the characters *, ~, ;, or : in your text file. Those are used as markers for the compiler.
* Exported files should work with any ds game, tested with SM64DS and NSMBDS.
* InfoTool CAN'T read placeholders in the groups! If there is a group in your info.bin, this tool is useless!

## Credits
* Gota7 - Coder of everything.
* Crystal - SDAT Research.
* loveemu - SDAT Research.
* Nintendon - SDAT Research.
* DJ Bouche - SDAT Research.
* VGMTrans - SDAT Research.
