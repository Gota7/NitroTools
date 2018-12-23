# Symb Dumped Text File Specifications
Here you can find how the dumped text file is organized.

## Section Headers
Each section header begins with a :SECTIONNAME

In a file, you must have these section names in this order:

* SSEQ
* SEQARC
* BANK
* WAVE
* PLAYER
* GROUP
* PLAYER2
* STRM

These mark the beginnings of a table of the filenames per the section.

## Subsections
Each subsection is marked by a ~SUBSECTIONNAME

SUBSECTIONNAME will be the name of the subsection.
The ONLY section with subsections is SEQARC.
__Filenames in the SeqArc must always be in a subsection!__

## Place Holders
Place holders can be used anywhere, except in the subgroup names.
they are marked by PLACEHOLDER.

## Example
Here is a text file dumped from SM64DS's symb.bin:

[Example](../NitroTools/exampleSymbText.txt)

