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
* Ability to read placeholders.

## Compatibility Notes
* DO NOT use the characters ~ or : in your text file. Those are used as markers.
* Exported files should work with any ds game, tested with SM64DS.
* If there is a placeholder in the list of seqArcs, then this will garble the filenames.

## Credits
* Gota7 - Coder of everything.
* Crystal - SDAT Research.
* loveemu - SDAT Research.
* Nintendon - SDAT Research.
* DJ Bouche - SDAT Research.
* VGMTrans - SDAT Research.
