# Nitro File Loader
Tools that allow you to load info and symb files to more editable formats, and convert them back correctly.

## Files
NitroStructures includes the structures, and a class for storing more editable data with fully working conversion.
NitroFileLoader includes the tools to read an symb or info file to the structure and write it back. It also includes SWAR tools.

## Features
* Accurate output.
* Should work with any DS game. (Tested fully with SM64DS and NSMBDS)
* Great for program makers.
* Loading SDAT compatibility.

## Usage
First, you must load a file to a byte array. You can then declare a NitroStructure.infoFile or NitroStructures.symbFile.
You could also convert that to the class with only data using symbData.load(NitroStructures.symb).
Then when converted, you could edit as you please and use File.Write("newSymb.bin", NitroFileLoader.symbToBytes(symbData.toSymbFile())); to convert back everything for you!

This tool can also be used to load SWARs, extract them to a folder, compress those folders, and convert back to a SWAR.
Can also be used to support SDAT files.

To load an SDAT, you would make a new sdatFile, and load the file. You can also extract it to a folder, compile a folder, or write the SDAT back to bytes.
This same method is also for SWARs.

## Credits
* Gota7 - Coder of everything.
* RoadRunnerWMC - SDAT Research.
* Mibts - SDAT Research.
* Crystal - SDAT Research.
* loveemu - SDAT Research.
* Nintendon - SDAT Research.
* DJ Bouche - SDAT Research.
* VGMTrans - SDAT Research.