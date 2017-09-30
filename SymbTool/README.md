# SymbTool
An extractor and packer for symb.bin filenames.

## Usage
SymbTool.exe inputFile outputFile

inputFile - SYMB bin to convert to text, or text to convert to a SYMB bin.
outputFile - The name of the output file.

## Features
* Perfect bytesize calculation.
* Should work with any SYMB from any SDAT.
* Automatically can tell if input is text or symb (based on extension).
* Can tell if input file is valid.
* Ability to add filenames to the list (must also match in info.bin).

## Compatibility Notes
* DO NOT use the characters ~ or : in your text file. Those are used as markers.
* Exported SYMB files work with SM64DS, in theory should work with any SYMB bin.
* Rebuilding the sdat with a compiled SYMB works with vgmTrans.
* Rebuilding the sdat with a compiled SYMB does not seem to work with DS Sound Studio for whatever reason. 

## Credits
* Gota7 - Coder of everything.
* Crystal - SDAT Research.
* loveemu - SDAT Research.
* Nintendon - SDAT Research.
* DJ Bouche - SDAT Research.
* VGMTrans - SDAT Research.
