using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;


namespace SymbTool
{
	class MainClass
	{

		public static void Main(string[] args) {

            //Txt2Symb("out.txt", "newSymb.bin");

            //string[] args = { "symb.txt", "Nsymb.bin"};
            //string[] args = { "Nsymb.bin", "extraThicc.txt" };


            if (args.Length < 2) {
				Console.WriteLine ("Usage: inputFile outputFile");
                System.Environment.Exit(1);
            } else {
                if (args[0].EndsWith(".symb") || args[0].EndsWith(".bin"))
                {
                    Symb2Txt(args[0], args[1]);
                }
                else if (args[0].EndsWith(".txt"))
                {
                    Txt2Symb(args[0], args[1]);
                }
                else {
                    Console.WriteLine("Usage: inputFile outputFile");
                    System.Environment.Exit(1);
                }
			}


            //Symb2Txt("Nsymb.bin", "extraThicc.txt");
            

		}


        //Txt2Symb
        public static void Txt2Symb(string inputS, string outputS) {

            //Load all strings from input
			List<string> txtStream = new List<string>();
			using(StreamReader reader = File.OpenText(inputS))
			{
				while (!reader.EndOfStream)
				{
					txtStream.Add(reader.ReadLine());
				}
			}

			//Covert txt to array
			string[] txt = txtStream.ToArray();


            //SSEQ section
            byte[][] sseqHex = simpleHexDataFromTxt(txt, "SSEQ", ':');

			//Organize that SeqArc.
			List<byte[][][]> seqArcHex = getRecord1DataFromText(txt);


            //BANK section
			byte[][] bankHex = simpleHexDataFromTxt(txt, "BANK", ':');

            //WAVE section
			byte[][] waveHex = simpleHexDataFromTxt(txt, "WAVE", ':');

            //PLAYER section
			byte[][] playerHex = simpleHexDataFromTxt(txt, "PLAYER", ':');

            //GROUP section
            byte[][] groupHex = simpleHexDataFromTxt(txt, "GROUP", ':');

            //PLAYER2 section
            byte[][] player2Hex = simpleHexDataFromTxt(txt, "PLAYER2", ':');

            //STRM section
			byte[][] strmHex = simpleHexDataFromTxt(txt, "STRM", ':');




            //Combine each filename into one byte.
            byte[] allSseq = combineByteArrayToOneByte(sseqHex, 0);
            byte[] allBank = combineByteArrayToOneByte(bankHex, 0);
            byte[] allWave = combineByteArrayToOneByte(waveHex, 0);
            byte[] allPlayer = combineByteArrayToOneByte(playerHex, 0);
            byte[] allGroup = combineByteArrayToOneByte(groupHex, 0);
            byte[] allPlayer2 = combineByteArrayToOneByte(player2Hex, 0);
            byte[] allStrm = combineByteArrayToOneByte(strmHex, 0);


            //Combine the SeqArc into one byte
            byte[][][][] seqHex = seqArcHex.ToArray();
            List<byte[]> seqHexList = new List<byte[]>();

            for (int k = 0; k < seqHex[0].Length; k++) {

                seqHexList.Add(seqHex[0][0][k]);

                for (int j = 0; j < seqHex[1][k].Length; j++) {

                    seqHexList.Add(seqHex[1][k][j]);

                }
            }

            //Get data from that list.
            byte[][] allSeqArcTemp = seqHexList.ToArray();
            byte[] allSeqArc = combineByteArrayToOneByte(allSeqArcTemp, 0);



            //Combine all the bytes, to create filename table.
            byte[][] fileNameArrays = new byte[8][];
            fileNameArrays[0] = allSseq;
            fileNameArrays[1] = allSeqArc;
            fileNameArrays[2] = allBank;
            fileNameArrays[3] = allWave;
            fileNameArrays[4] = allPlayer;
            fileNameArrays[5] = allGroup;
            fileNameArrays[6] = allPlayer2;
            fileNameArrays[7] = allStrm;
            byte[] fileNames = Combine(fileNameArrays);



            //Find out length of filesize, in bytes.
            int numberOfSseq = sseqHex.Length;

            int numberOfSecArcSubRecords = seqArcHex[0].Length;
            int numberOfSecArcRecords = 0;

            for (int i = 0; i < numberOfSecArcSubRecords; i++) {
                for (int j = 0; j < seqArcHex[1][i].Length; j++) {
                    numberOfSecArcRecords += 1;
                }
            }

            int numberOfFilesInRecord1 = numberOfSecArcSubRecords + numberOfSecArcRecords;

            int numberOfBank = bankHex.Length;
            int numberOfWave = waveHex.Length;
            int numberOfPlayer = playerHex.Length;
            int numberOfGroup = groupHex.Length;
            int numberOfPlayer2 = player2Hex.Length;
            int numberOfStrm = strmHex.Length;

            //Calculate Total size, in bytes.
            int headerSize = 64;

            int seqArcOffsetSize = numberOfSecArcSubRecords * 8 + 4 + numberOfSecArcRecords * 4 + numberOfSecArcSubRecords*4;

            int fileOffsetSizeWithoutSeqArc = numberOfSseq * 4 + 4 + numberOfBank * 4 + 4 + numberOfWave * 4 + 4 + numberOfPlayer * 4 + 4 + numberOfGroup * 4 + 4 + numberOfPlayer2 * 4 + 4 + numberOfStrm * 4 + 4;
            int fileNamesSize = fileNames.Length;
			int fileNamesSizeWithoutSeqArc = fileNamesSize - allSeqArc.Length;

            int fileSize = headerSize + seqArcOffsetSize + fileOffsetSizeWithoutSeqArc + fileNamesSize;


            //Get addresses from string destinations.
			int[] sseqAddresses = getAddressesFromRecord(sseqHex, headerSize, fileOffsetSizeWithoutSeqArc + seqArcOffsetSize, 0);

			int[] seqArcAddresses = getSeqArcAddresses (seqArcHex, headerSize, fileOffsetSizeWithoutSeqArc + seqArcOffsetSize, allSseq.Length, sseqAddresses);
				
			int[] bankAddresses = getAddressesFromRecord(bankHex, headerSize, fileOffsetSizeWithoutSeqArc + seqArcOffsetSize, allSseq.Length + allSeqArc.Length);
			int[] waveAddresses = getAddressesFromRecord(waveHex, headerSize, fileOffsetSizeWithoutSeqArc + seqArcOffsetSize, allSseq.Length + allBank.Length + allSeqArc.Length);
			int[] playerAddresses = getAddressesFromRecord(playerHex, headerSize, fileOffsetSizeWithoutSeqArc + seqArcOffsetSize, allSseq.Length + allBank.Length + allWave.Length + allSeqArc.Length);
			int[] groupAddresses = getAddressesFromRecord(groupHex, headerSize, fileOffsetSizeWithoutSeqArc + seqArcOffsetSize,allSseq.Length + allBank.Length + allWave.Length + allPlayer.Length + allSeqArc.Length);
			int[] player2Addresses = getAddressesFromRecord(player2Hex, headerSize, fileOffsetSizeWithoutSeqArc + seqArcOffsetSize, allSseq.Length + allBank.Length + allWave.Length + allPlayer.Length + allGroup.Length + allSeqArc.Length);
			int[] strmAddresses = getAddressesFromRecord(strmHex, headerSize, fileOffsetSizeWithoutSeqArc + seqArcOffsetSize, allSseq.Length + allBank.Length + allWave.Length + allPlayer.Length + allGroup.Length + allPlayer2.Length + allSeqArc.Length);


			//Get lengths of each thing.
			byte[] strmLength = BitConverter.GetBytes(numberOfStrm);
			byte[] player2Length = BitConverter.GetBytes(numberOfPlayer2);
			byte[] groupLength = BitConverter.GetBytes(numberOfGroup);
			byte[] playerLength = BitConverter.GetBytes(numberOfPlayer);
			byte[] waveLength = BitConverter.GetBytes(numberOfWave);
			byte[] bankLength = BitConverter.GetBytes(numberOfBank);
			byte[] seqArcLength = BitConverter.GetBytes (numberOfSecArcSubRecords);
			byte[] sseqLength = BitConverter.GetBytes(numberOfSseq);


			//Console.WriteLine (seqArcOffsetSize);
			//Console.WriteLine (seqArcAddresses.Length);




			//Get final symb.


			//Header stuff

			//Magic
			byte[] magic = stringToBytes("SYMB");




			//Offsets
			byte[] sseqOffsetH = BitConverter.GetBytes(headerSize);
			byte[] sseqArcOffsetH = BitConverter.GetBytes(headerSize + numberOfSseq*4 + 4);
			byte[] bankOffsetH = BitConverter.GetBytes(headerSize + numberOfSseq*4 + 4 + seqArcOffsetSize);
			byte[] waveOffsetH = BitConverter.GetBytes(headerSize + numberOfSseq*4 + 4 + seqArcOffsetSize + numberOfBank*4 + 4);
			byte[] playerOffsetH = BitConverter.GetBytes(headerSize + numberOfSseq*4 + 4 + seqArcOffsetSize + numberOfBank*4 + 4 + numberOfWave*4 + 4);
			byte[] groupOffsetH = BitConverter.GetBytes(headerSize + numberOfSseq*4 + 4 + seqArcOffsetSize + numberOfBank*4 + 4 + numberOfWave*4 + 4 + numberOfPlayer*4 + 4);
			byte[] player2OffsetH = BitConverter.GetBytes(headerSize + numberOfSseq*4 + 4 + seqArcOffsetSize + numberOfBank*4 + 4 + numberOfWave*4 + 4 + numberOfPlayer*4 + 4 + numberOfGroup*4 + 4);
			byte[] strmOffsetH = BitConverter.GetBytes(headerSize + numberOfSseq*4 + 4 + seqArcOffsetSize + numberOfBank*4 + 4 + numberOfWave*4 + 4 + numberOfPlayer*4 + 4 + numberOfGroup*4 + 4 + numberOfPlayer2*4 + 4);


			byte[][] offsetsDataH = { sseqOffsetH, sseqArcOffsetH, bankOffsetH, waveOffsetH, playerOffsetH, groupOffsetH, player2OffsetH, strmOffsetH };
			byte[] offsets = Combine (offsetsDataH);


			//Padding Zeros
			byte[] padding = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};


			//Offset table
			byte[] sseqOffssetTable = byteFromLengthAndOffsets(sseqLength, sseqAddresses);
			byte[] seqArcOffsetTable = byteFromSeqArcOffsets (seqArcAddresses);
			byte[] bankOffssetTable = byteFromLengthAndOffsets(bankLength, bankAddresses);
			byte[] waveOffssetTable = byteFromLengthAndOffsets(waveLength, waveAddresses);
			byte[] playerOffssetTable = byteFromLengthAndOffsets(playerLength, playerAddresses);
			byte[] groupOffssetTable = byteFromLengthAndOffsets(groupLength, groupAddresses);
			byte[] player2OffssetTable = byteFromLengthAndOffsets(player2Length, player2Addresses);
			byte[] strmOffssetTable = byteFromLengthAndOffsets(strmLength, strmAddresses);



			//Write hexdata
			byte[][] hexdataTemp = { allSseq, allSeqArc, allBank, allWave, allPlayer, allGroup, allPlayer2, allStrm };
			byte[] hexData = Combine (hexdataTemp);

            //Size
            int newFileSize = magic.Length + 4 + offsets.Length + padding.Length + sseqOffssetTable.Length + seqArcOffsetTable.Length + bankOffssetTable.Length + waveOffssetTable.Length + playerOffssetTable.Length + groupOffssetTable.Length + player2OffssetTable.Length + strmOffssetTable.Length + hexData.Length; 
            byte[] size = BitConverter.GetBytes(newFileSize);

            //Final file
            byte[][] finalFileTemp = {magic, size, offsets, padding, sseqOffssetTable, seqArcOffsetTable, bankOffssetTable, waveOffssetTable, playerOffssetTable, groupOffssetTable, player2OffssetTable, strmOffssetTable, hexData};
			byte[] finalFile = Combine (finalFileTemp);

			File.WriteAllBytes (outputS, finalFile);



        }



		//Symb2Txt
		public static void Symb2Txt (string inputS, string outputS)
		{

			//Load SYMB File
			byte[] symb = System.IO.File.ReadAllBytes(inputS);

			//Get magic header
			char[] headerChars = {Convert.ToChar(symb[0]), Convert.ToChar(symb[1]), Convert.ToChar(symb[2]), Convert.ToChar(symb[3])};
			string header = new String (headerChars);

			//Check magic header, if bad, exit
			if (!header.Equals("SYMB")) {Console.WriteLine("INVALID SYMB FILE!");}

			//Get record offsets by taking the the 4 byte values.
			int recordOffset0 = BitConverter.ToInt32 (symb, 8);
			int recordOffset1 = BitConverter.ToInt32 (symb, 12);
			int recordOffset2 = BitConverter.ToInt32 (symb, 16);
			int recordOffset3 = BitConverter.ToInt32 (symb, 20);
			int recordOffset4 = BitConverter.ToInt32 (symb, 24);
			int recordOffset5 = BitConverter.ToInt32 (symb, 28);
			int recordOffset6 = BitConverter.ToInt32 (symb, 32);
			int recordOffset7 = BitConverter.ToInt32 (symb, 36);


			//Get byteSize for each record, using the filecount times the bytes per file.
			int record0byteSize = BitConverter.ToInt32(symb, recordOffset0) * 4;
			int record1byteSize = BitConverter.ToInt32(symb, recordOffset1) * 4;
			int record2byteSize = BitConverter.ToInt32(symb, recordOffset2) * 4;
			int record3byteSize = BitConverter.ToInt32(symb, recordOffset3) * 4;
			int record4byteSize = BitConverter.ToInt32(symb, recordOffset4) * 4;
			int record5byteSize = BitConverter.ToInt32(symb, recordOffset5) * 4;
			int record6byteSize = BitConverter.ToInt32(symb, recordOffset6) * 4;
			int record7byteSize = BitConverter.ToInt32(symb, recordOffset7) * 4;
	

			//Get data for each record, by splitting symb into their offsets not including file count, and size.
			byte[] record0 = PartOfByteArray (symb, recordOffset0 + 4, record0byteSize);
			byte[] record1 = PartOfByteArray (symb, recordOffset1 + 4, record1byteSize);
			byte[] record2 = PartOfByteArray (symb, recordOffset2 + 4, record2byteSize);
			byte[] record3 = PartOfByteArray (symb, recordOffset3 + 4, record3byteSize);
			byte[] record4 = PartOfByteArray (symb, recordOffset4 + 4, record4byteSize);
			byte[] record5 = PartOfByteArray (symb, recordOffset5 + 4, record5byteSize);
			byte[] record6 = PartOfByteArray (symb, recordOffset6 + 4, record6byteSize);
			byte[] record7 = PartOfByteArray (symb, recordOffset7 + 4, record7byteSize);


			//Record Addresses for each record, except for 1, which is different. It needs number of files, so divide by 4.
			int[] record0Adresses = DivideByteArrayToIntAddresses(record0, record0byteSize / 4);
			int[] record2Adresses = DivideByteArrayToIntAddresses(record2, record2byteSize / 4);
			int[] record3Adresses = DivideByteArrayToIntAddresses(record3, record3byteSize / 4);
			int[] record4Adresses = DivideByteArrayToIntAddresses(record4, record4byteSize / 4);
			int[] record5Adresses = DivideByteArrayToIntAddresses(record5, record5byteSize / 4);
			int[] record6Adresses = DivideByteArrayToIntAddresses(record6, record6byteSize / 4);
			int[] record7Adresses = DivideByteArrayToIntAddresses(record7, record7byteSize / 4);


			//Get strings from each record address, except for 1, which is different.
			string[] record0Strings = getStringsFromRecordAddress(record0Adresses, symb);
			string[] record2Strings = getStringsFromRecordAddress(record2Adresses, symb);
			string[] record3Strings = getStringsFromRecordAddress(record3Adresses, symb);
			string[] record4Strings = getStringsFromRecordAddress(record4Adresses, symb);
			string[] record5Strings = getStringsFromRecordAddress(record5Adresses, symb);
			string[] record6Strings = getStringsFromRecordAddress(record6Adresses, symb);
			string[] record7Strings = getStringsFromRecordAddress(record7Adresses, symb);


			//Get sub records from record 1.
			int[] subRecordSymbolAddresses = new int[record1byteSize / 4];
			int[] subRecordRecordAddresses = new int[record1byteSize / 4];

			for (int i = 0; i < record1byteSize / 4 * 8; i += 8) {
			
				subRecordSymbolAddresses [i / 8] = BitConverter.ToInt32 (symb, i + recordOffset1 + 4);
				subRecordRecordAddresses [i / 8] = BitConverter.ToInt32 (symb, i + 4 + recordOffset1 + 4);
			
			}


			//Get seqarc names from record 1.
			string[] record1Strings = getStringsFromRecordAddress(subRecordSymbolAddresses, symb);


			//Get the start and end for each subrecord
			int amountOfSubRecords = record1Strings.Length;

			List<string[]> subStringsList = new List<string[]> ();

			for (int i = 0; i < amountOfSubRecords; i++) {
			
				int[] addresses = new int[BitConverter.ToInt32(symb, subRecordRecordAddresses[i])];
				byte[] array = new byte[BitConverter.ToInt32(symb, subRecordRecordAddresses[i])*4];

				for (int j = 0; j < array.Length; j++) {
					array[j] = symb[subRecordRecordAddresses[i]+j+4];
				}

			
				addresses = DivideByteArrayToIntAddresses (array, addresses.Length);


				subStringsList.Add (getStringsFromRecordAddress (addresses, symb));

			}

			string[][] subStrings = subStringsList.ToArray ();




			//Save final text file.
			List<string> output = new List<string>();

			//SSEQ
			output.Add(":SSEQ");
			for (int i = 0; i < record0Strings.Length; i++) {
				output.Add (record0Strings [i]);
			}




			//SeqArc
			output.Add(":SEQARC");
			for (int i = 0; i < record1Strings.Length; i++) {
				output.Add("~" + record1Strings[i]);
				for (int j = 0; j < subStrings[i].Length; j++) {
					output.Add(subStrings[i][j]);
				}
			}





			//BANK
			output.Add(":BANK");
			for (int i = 0; i < record2Strings.Length; i++) {
				output.Add (record2Strings [i]);
			}

			//WAVE
			output.Add(":WAVE");
			for (int i = 0; i < record3Strings.Length; i++) {
				output.Add (record3Strings [i]);
			}

			//PLAYER
			output.Add(":PLAYER");
			for (int i = 0; i < record4Strings.Length; i++) {
				output.Add (record4Strings [i]);
			}

			//GROUP
			output.Add(":GROUP");
			for (int i = 0; i < record5Strings.Length; i++) {
				output.Add (record5Strings [i]);
			}

			//PLAYER2
			output.Add(":PLAYER2");
			for (int i = 0; i < record6Strings.Length; i++) {
				output.Add (record6Strings [i]);
			}

			//STRM
			output.Add(":STRM");
			for (int i = 0; i < record7Strings.Length; i++) {
				output.Add (record7Strings [i]);
			}


			//Search for symb bytes, and remove them.
			//output.RemoveAll(DoDelete);
			output.RemoveAll (DeleteBlank);


			//Finally Write the output.
			string[] final = output.ToArray ();
			System.IO.File.WriteAllLines (outputS, final);



		}


		// Search predicate returns true if a string ends in "saurus".
		private static bool DoDelete(String s)
		{
			return s.Equals ("DELETE_ME");
		}
		private static bool DeleteBlank(String s)
		{
			return s.Equals ("");
		}



		/// <summary>
		/// Returns part of a byte array
		/// </summary>
		/// <returns>The of byte array.</returns>
		/// <param name="array">Array.</param>
		/// <param name="startIndex">Start index.</param>
		/// <param name="endIndex">End index.</param>
		public static byte[] PartOfByteArray(byte[] array, int startIndex, int length) {
			
			byte[] newArray = new byte[length];
			byte[] zero = { 0, 0, 0, 0};

			for (int i = 0; i < length; i++) {
				newArray [i] = array [i + startIndex];
			}


			if (length > 0) {
				return newArray;
			} else {
				return zero;
			}

		}


		/// <summary>
		/// Divides the byte array to int addresses.
		/// </summary>
		/// <returns>The byte array to int addresses.</returns>
		/// <param name="array">Array.</param>
		/// <param name="size">Size.</param>
		public static int[] DivideByteArrayToIntAddresses(byte[] array, int size) {

			int[] newArray = new int[size];

			//Get addresses for every 4 bytes
			for (int i = 0; i < size * 4; i+=4) {
				newArray[i/4] = BitConverter.ToInt32(array, i);
			}

			return newArray;

		}



		public static string[] getStringsFromRecordAddress(int[] addresses, byte[] symbFile) {

			//New strings
			string[] newString = new string [addresses.Length];
			string[] emptyString = { "" };

			//Return empty string if no records.
			if (addresses.Length < 1) {
				return emptyString;
			}


			//Get chars from each string.
			for (int j = 0; j < addresses.Length; j++) {

				//Do the actual chars part.
				List<char> charList = new List<char>();

				bool gotAllChars = false;

				for (int i = 0; i < 1000; i++) {

					if (!gotAllChars) {
					
						if (symbFile [addresses[j] + i] != 0) {
						
							charList.Add (Convert.ToChar ( symbFile[addresses[j] + i]));

						} else {
							gotAllChars = true;
						}
					}

					newString [j] = String.Join ("", charList);

					//Cleanup if address is bad.
					if (addresses [j] == 0) {
						newString [j] = "PLACEHOLDER";
					}

				}



			}


			return newString;

		}


        /// <summary>
        /// Convery a string list into an array of bytes.
        /// </summary>
        /// <param name="str">The string list.</param>
		public static byte[] stringList2ByteArray(List<string> str, string seperator) {

            //Convert List to String.
            string s = String.Join(seperator, str.ToArray());

            //Append byteArray
            byte[] outS = new byte[s.Length];

            for (int i = 0; i < s.Length; i++) {
                outS[i] = Convert.ToByte(s[i]);
            }

            return outS;

        }
			



		/// <summary>
		/// Extract the hex data from text between a header and a symbol.
		/// </summary>
		/// <returns>The hex data from text.</returns>
		/// <param name="txt">Text.</param>
		/// <param name="fileType">File type.</param>
		public static byte[][] simpleHexDataFromTxt(string[] txt, string fileType, char magicSymbol) {

            //Get all data between the section header until next section.
            bool writing = false;
            List<string> qList = new List<string>();

            for (int i = 0; i < txt.Length; i++)
            {

				if (txt[i].StartsWith(magicSymbol.ToString())) { writing = false; }
                if (writing && txt[i]!="PLACEHOLDER") qList.Add(txt[i]);
                if (writing && txt[i] == "PLACEHOLDER") qList.Add("");
                if (txt[i].Equals(magicSymbol + fileType)) { writing = true; }
            }

			List<byte[]> bytes = new List<byte[]> ();

			for (int i = 0; i < qList.Count; i ++) {

                if (qList[i] != "") { bytes.Add(stringToBytes(qList[i])); } else { bytes.Add(null); }

			}

            //string qHex = ByteArrayToString(qBytesArray);
            //qHex += "00";
            //qHex = qHex.Replace(".", "{");
            //qHex = qHex.Replace("2e", "{");

            //string[] final = qHex.Split('{');

			//byte[] final = qBytesArray;

			byte[][] final = bytes.ToArray ();

            return final;

        }



		public static List<byte[][][]> getRecord1DataFromText(string[] txt) {
		
			//List for different groups
			List<string> record1groupsList = new List<string>();

			bool writing = false;
			for (int i = 0; i < txt.Length; i++) {
				if (txt[i].StartsWith(":")) { writing = true; }
				if (writing && txt[i].StartsWith("~")) record1groupsList.Add(txt[i].Remove(0, 1));
				if (txt[i].Equals(":SEQARC")) { writing = true; }
			}
				

			//List for all sub groups.
			List<List<string>> subRecords = new List<List<string>>();
			string[] record1groups = record1groupsList.ToArray ();

			writing = false;
			for (int j = 0; j < record1groups.Length; j++) {
				writing = false;
				List<string> records = new List<string>();
				for (int i = 0; i < txt.Length; i++) {
					if (txt [i].StartsWith ("~") || txt[i].StartsWith(":")) {
						writing = false;
					}
                    if (writing && txt[i] != "PLACEHOLDER")
                    {
                        records.Add(txt[i]);
                    }
                    if (writing && txt[i] == "PLACEHOLDER") {
                        records.Add("");
                    }
					if (txt [i].StartsWith ("~" + record1groups[j])) {
						writing = true;
					}
				}
                subRecords.Add(records);
			}

            //Get data array.
            List<string>[] data = subRecords.ToArray();

            string[][] dataArray = new string[data.Count()][];

            for (int i = 0; i < dataArray.Length; i++) {
                dataArray[i] = data[i].ToArray();
            }

            //Byte for subrecord names.
            byte[][][] recordNames = new byte[record1groups.Length][][];

            byte[][] sanity = new byte[record1groups.Length][];

            for (int i = 0; i < recordNames.Length; i++) {
          
                sanity[i] = stringToBytes(record1groups[i]);

            }

            recordNames[0] = sanity;

            /*Structure of the record names:
             * [0] [X] [Y]
             * First is always 0.
             * Second is the ID of the subrecord list.
             * Third is the name of the subrecord list.
            */

            //Byte for each record name.
            byte[][][] arc = new byte[dataArray.Length][][];

            for (int j = 0; j < arc.Length; j++) {

                byte[][] tempArc = new byte[dataArray[j].Length][];

                for (int i = 0; i < dataArray[j].Length; i++) {
                    if (dataArray[j][i] != "") { tempArc[i] = stringToBytes(dataArray[j][i]); }
                    else { tempArc[i] = null; }
                }

                arc[j] = tempArc;

            }

            /*
             * Structure of sub-records:
             * [X] [Y] [Z]
             * First is the number of the subrecord.
             * Second is the record nothing.
             * Third is the actual byte array.
             */

            List < byte[][][] > final = new List<byte[][][]>();

            //Return final list, first element is the subrecords, second is records.
            final.Add(recordNames);
            final.Add(arc);

            return final;
		
		}



		/// <summary>
		/// Strings to bytes, when other function fails.
		/// </summary>
		/// <returns>Byte array.</returns>
		/// <param name="a">String to convert</param>
		public static byte[] stringToBytes(string a) {
		
			byte[] hex = new byte[a.Length];

			for (int i = 0; i < hex.Length; i++) {
				hex [i] = Convert.ToByte (a [i]);
			}

			return hex;
		
		}


        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }


        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }


        /// <summary>
        /// Convert a 2d byte array to one byte array.
        /// </summary>
        /// <param name="b">The 2d byte array</param>
        /// <param name="seperator">Byte to place in-between each byte section</param>
        /// <returns></returns>
        public static byte[] combineByteArrayToOneByte(byte[][] b, byte seperator) {

            List<byte> allList = new List<byte>();
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i] != null)
                {
                    for (int j = 0; j < b[i].Length; j++)
                    {
                        allList.Add(b[i][j]);
                    }
                    allList.Add(seperator);
                }
            }

            byte[] all = allList.ToArray();

            return all;

        }


        /// <summary>
        /// Combine byte arrays.
        /// </summary>
        /// <param name="arrays">Arrays.</param>
        /// <returns></returns>
         public static byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }



		/// <summary>
		/// Gets the addresses from record.
		/// </summary>
		/// <returns>The addresses from record.</returns>
		/// <param name="lengthsOfStrings">Data array.</param>
		/// <param name="fileSize">File size of symb.</param>
		/// <param name="lengthOfOtherRecordsAbove">Length of other records above.</param>
		public static int[] getAddressesFromRecord(byte[][] hexData, int headerSize, int offsetAreaLength, int lengthOfStringsAbove) {

			//Number of all address is the size of the data array.
			int[] addresses = new int[hexData.Length];


			//Get the addresses
			int temp = 0;
			for (int i=0; i < addresses.Length; i++) {

                if (hexData[i] != null)
                {

                    addresses[i] = headerSize + offsetAreaLength + temp + lengthOfStringsAbove + i;
                    temp += hexData[i].Length;

                }
                else {
                    addresses[i] = 0;
                    temp -= 1;
                }

			}


			//Return final addresses.
			return addresses;

		}


		/// <summary>
		/// Bytes from length and offsets.
		/// </summary>
		/// <param name="length">Length.</param>
		/// <param name="addresses">Addresses.</param>
		public static byte[] byteFromLengthAndOffsets(byte[] length, int[] addresses) {

			byte[] newOffset = { 0, 0, 0, 0 };
			if (length.Length == 0) {return newOffset;}

			byte[][] data = new byte[addresses.Length + 1][];
			data [0] = length;

			for (int i = 1; i < data.Length; i++) {
			
				data [i] = BitConverter.GetBytes (addresses [i-1]);

			}

			return Combine (data);

		}



		public static byte[] byteFromSeqArcOffsets(int[] addresses) {


			byte[][] data = new byte[addresses.Length][];

			for (int i = 0; i < data.Length; i++) {

				data [i] = BitConverter.GetBytes (addresses [i]);

			}

			return Combine (data);

		}




		/// <summary>
		/// Gets the seq arc addresses.
		/// </summary>
		/// <returns>The seq arc addresses.</returns>
		/// <param name="seqArcHex">Seq arc hex.</param>
		/// <param name="headerSize">Header size.</param>
		/// <param name="lengthOfStringsAbove">Length of strings above.</param>
		public static int[] getSeqArcAddresses(List<byte[][][]> seqArcHex, int headerSize, int offsetAreaLength, int lengthOfStringsAbove, int[] sseqAddresses) {

			List<int> seqArcAddressesTemp = new List<int> ();


			//First part is the length of the list of record lists.
			seqArcAddressesTemp.Add(seqArcHex[0][0].Length);


			//Get Addresses for each subrecord list name.
			int temp = 0;
			int temp2 = 0;

			for (int i = 0; i < seqArcHex [0] [0].Length; i++) {

				int aboveData = headerSize + offsetAreaLength + temp + lengthOfStringsAbove;
			
				//Console.WriteLine (seqArcHex [0] [0] [i].Length);
				//Console.WriteLine (aboveData);
				//Console.WriteLine (seqArcHex [1] [i].Length);

				//Get add value to add for temp, so bytes are correct.
				int addValue = 0;
				for (int j = 0; j < seqArcHex [1] [i].Length; j++) {
				
					if (seqArcHex[1][i][j] != null) addValue += seqArcHex [1] [i] [j].Length + 1;

				}

				//Add the byte to array.
				seqArcAddressesTemp.Add(aboveData);

				temp += seqArcHex [0] [0] [i].Length + addValue + 1;


				//Now get the offsets to the record list.


				//Length of the subrecord list.
				int subRecordOffsetsLength = seqArcHex [0] [0].Length * 8 + 4;

				//Get where each record list is.
				int offsetData = headerSize + sseqAddresses.Length*4 + 4 + subRecordOffsetsLength + temp2;

				//Append to array.
				seqArcAddressesTemp.Add(offsetData);

				temp2 += 4 + seqArcHex [1] [i].Length*4;

			}
				

			//Offsets to records.
			int temp3 = 0;
			for (int i = 0; i < seqArcHex[1].Length; i++) {

				//Add lengths of each seqArcSection.
				seqArcAddressesTemp.Add(seqArcHex[1][i].Length);

				temp3 += seqArcHex [0] [0] [i].Length + 1;

				int aboveData = headerSize + offsetAreaLength + lengthOfStringsAbove;
				for (int j = 0; j < seqArcHex [1][i].Length; j++) {

                    if (seqArcHex[1][i][j] != null)
                    {

                        //Add addresses for each record.
                        seqArcAddressesTemp.Add(aboveData + temp3);

                        //Add temp.
                        temp3 += seqArcHex[1][i][j].Length + 1;

                    }
                    else {
                        seqArcAddressesTemp.Add(0);
                    }
				}
			}


			int[] seqArcAddresses = seqArcAddressesTemp.ToArray ();
			return seqArcAddresses;

		}

    }
}
