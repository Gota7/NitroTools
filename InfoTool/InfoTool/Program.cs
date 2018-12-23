using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace InfoTool
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            if (args.Length < 2) { Console.WriteLine("Usage: InfoTool.exe inputFile outputFile"); Environment.Exit(1); }


            if (args[0].EndsWith(".txt"))
            {
                TextToInfo(args[0], args[1]);
            }
            else if (args[0].EndsWith(".bin"))
            {
                InfoToText(args[0], args[1]);
            }
            else {
                Console.WriteLine("Incompatible File!");
                Environment.Exit(1);
            }

            //TextToInfo("info.txt", "out.bin");
            //InfoToText("out.bin", "test.txt");


        }






        /// <summary>
        /// Convert text file to info.bin
        /// </summary>
        /// <param name="inputS">Input text file name.</param>
        /// <param name="outputS">Output bin file name.</param>
        public static void TextToInfo(string inputS, string outputS) {


            string[] textFile = File.ReadAllLines(inputS);


            //Get Sseq Strings.
            string[] sseqStrings = getSectionOfText(textFile, "*SSEQ", "*");

            //Get SeqArc Strings.
            string[] seqArcStrings = getSectionOfText(textFile, "*SEQARC", "*");

            //Get Bank Strings.
            string[] bankStrings = getSectionOfText(textFile, "*BANK", "*");

            //Get Wave Strings.
            string[] waveStrings = getSectionOfText(textFile, "*WAVE", "*");

            //Get Player Strings.
            string[] playerStrings = getSectionOfText(textFile, "*PLAYER", "*");

            //Get Group Strings.
            string[][] groupStrings = getGroupStrings(textFile, "*Group", "*", "~");

            //Get Player2 Strings.
            string[] player2Strings = getSectionOfText(textFile, "*PLAYER2", "*");

            //Get Strm Strings.
            string[] strmStrings = getSectionOfText(textFile, "*STRM", "*");


            //Header size
            int headerSize = 64;

            //FileSize Stuff
            int dataSizeWithoutGroup = sseqStrings.Length * 12 + seqArcStrings.Length * 4 + bankStrings.Length * 12 + waveStrings.Length * 4 + playerStrings.Length * 8 + player2Strings.Length * 24 + strmStrings.Length * 12;
            int offsetSizeWithoutGroup = sseqStrings.Length + 4 + seqArcStrings.Length + 4 + bankStrings.Length + 4 + waveStrings.Length + 4 + playerStrings.Length + 4 + playerStrings.Length + 4 + strmStrings.Length + 4;
            int fileSizeWithoutGroup = headerSize + dataSizeWithoutGroup + offsetSizeWithoutGroup;



            //Create sseq data.
            int[] sseqStructure = { 2, 2, 2, 1, 1, 1, 1, 1, 1 };
            byte[] sseqData = getStringData(sseqStrings, sseqStructure);

            //Create SeqArc data.
            int[] seqArcStructure = { 2, 2 };
            byte[] seqArcData = getStringData(seqArcStrings, seqArcStructure);

            //Create Bank data.
            int[] bankStructure = { 2, 2, 2, 2, 2, 2 };
            byte[] bankData = getStringData(bankStrings, bankStructure);

            //Create Wave data.
            int[] waveStructure = { 2, 2 };
            byte[] waveData = getStringData(waveStrings, waveStructure);

            //Create Player data.
            int[] playerStructure = { 1, 0, 0, 0, 4 };
            byte[] playerData = getStringData(playerStrings, playerStructure);

			//Create Group data.
			byte[][] groupData = getGroupStringData(groupStrings);

			//Create Player2 data.
			int[] player2Structure = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0};
			byte[] player2Data = getStringData (player2Strings, player2Structure);

			//Create Strm data.
			int[] strmStructure = {2, 2, 1, 1, 1, 0, 0, 0, 0, 0};
			byte[] strmData = getStringData (strmStrings, strmStructure);

			//Make group data with lengths
			byte[] groupDataWithLengths = getGroupDataWithLengths(groupData, groupStrings);


			//Get length of offsets for sseq.
			byte[] sseqOffsetArea = getOffsetArea(12, sseqStrings, headerSize);

			//SeqArc
			byte[] seqArcOffsetArea = getOffsetArea(4, seqArcStrings, headerSize + sseqData.Length + sseqOffsetArea.Length);

			//Bank
			byte[] bankOffsetArea = getOffsetArea(12, bankStrings, headerSize + sseqData.Length + sseqOffsetArea.Length + seqArcData.Length + seqArcOffsetArea.Length);

			//Wave
			byte[] waveOffsetArea = getOffsetArea(4, waveStrings, headerSize + sseqData.Length + sseqOffsetArea.Length + seqArcData.Length + seqArcOffsetArea.Length + bankData.Length + bankOffsetArea.Length);

            //Player
            byte[] playerOffsetArea = getOffsetArea(8, playerStrings, headerSize + sseqData.Length + sseqOffsetArea.Length + seqArcData.Length + seqArcOffsetArea.Length + bankData.Length + bankOffsetArea.Length + waveData.Length + waveOffsetArea.Length);

            //Group
            byte[] groupOffsetArea = getGroupOffsetArea(groupDataWithLengths, groupStrings, headerSize + sseqData.Length + sseqOffsetArea.Length + seqArcData.Length + seqArcOffsetArea.Length + bankData.Length + bankOffsetArea.Length + waveData.Length + waveOffsetArea.Length + playerData.Length + playerOffsetArea.Length);

            //Player2
            byte[] player2OffsetArea = getOffsetArea(24, player2Strings, headerSize + sseqData.Length + sseqOffsetArea.Length + seqArcData.Length + seqArcOffsetArea.Length + bankData.Length + bankOffsetArea.Length + waveData.Length + waveOffsetArea.Length + playerData.Length + playerOffsetArea.Length + groupOffsetArea.Length + groupDataWithLengths.Length);

            //Strm
            byte[] strmOffsetArea = getOffsetArea(12, strmStrings, headerSize + sseqData.Length + sseqOffsetArea.Length + seqArcData.Length + seqArcOffsetArea.Length + bankData.Length + bankOffsetArea.Length + waveData.Length + waveOffsetArea.Length + playerData.Length + playerOffsetArea.Length + groupOffsetArea.Length + groupDataWithLengths.Length + player2Data.Length + player2OffsetArea.Length);



            //Create final file.
            byte[] magic = Encoding.UTF8.GetBytes("INFO");

			byte[] fileSize = BitConverter.GetBytes(headerSize + sseqData.Length + sseqOffsetArea.Length + seqArcData.Length + seqArcOffsetArea.Length + bankData.Length + bankOffsetArea.Length + waveData.Length + waveOffsetArea.Length + playerData.Length + playerOffsetArea.Length + groupOffsetArea.Length + groupDataWithLengths.Length + player2Data.Length + player2OffsetArea.Length + strmData.Length + strmOffsetArea.Length);

			byte[] sseqOffset = BitConverter.GetBytes (headerSize);
			byte[] seqArcOffset = BitConverter.GetBytes (headerSize + sseqData.Length + sseqOffsetArea.Length);
			byte[] bankOffset = BitConverter.GetBytes (headerSize + sseqData.Length + sseqOffsetArea.Length + seqArcData.Length + seqArcOffsetArea.Length);
			byte[] waveOffset = BitConverter.GetBytes (headerSize + sseqData.Length + sseqOffsetArea.Length + seqArcData.Length + seqArcOffsetArea.Length + bankData.Length + bankOffsetArea.Length);
			byte[] playerOffset = BitConverter.GetBytes(headerSize + sseqData.Length + sseqOffsetArea.Length + seqArcData.Length + seqArcOffsetArea.Length + bankData.Length + bankOffsetArea.Length + waveData.Length + waveOffsetArea.Length);
            byte[] groupOffset = BitConverter.GetBytes(headerSize + sseqData.Length + sseqOffsetArea.Length + seqArcData.Length + seqArcOffsetArea.Length + bankData.Length + bankOffsetArea.Length + waveData.Length + waveOffsetArea.Length + playerData.Length + playerOffsetArea.Length);
            byte[] player2Offset = BitConverter.GetBytes(headerSize + sseqData.Length + sseqOffsetArea.Length + seqArcData.Length + seqArcOffsetArea.Length + bankData.Length + bankOffsetArea.Length + waveData.Length + waveOffsetArea.Length + playerData.Length + playerOffsetArea.Length + groupOffsetArea.Length + groupDataWithLengths.Length);
            byte[] strmOffset = BitConverter.GetBytes(headerSize + sseqData.Length + sseqOffsetArea.Length + seqArcData.Length + seqArcOffsetArea.Length + bankData.Length + bankOffsetArea.Length + waveData.Length + waveOffsetArea.Length + playerData.Length + playerOffsetArea.Length + groupOffsetArea.Length + groupDataWithLengths.Length + player2Data.Length + player2OffsetArea.Length);

            byte[][] offsetsTemp = { sseqOffset, seqArcOffset, bankOffset, waveOffset, playerOffset, groupOffset, player2Offset, strmOffset };
			byte[] offsets = Combine (offsetsTemp);

			byte[] reserved = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

			byte[][] headerTemp = { magic, fileSize, offsets, reserved };
			byte[] header = Combine (headerTemp);

			byte[][] outputTemp = { header, sseqOffsetArea, sseqData, seqArcOffsetArea, seqArcData, bankOffsetArea, bankData, waveOffsetArea, waveData, playerOffsetArea, playerData, groupOffsetArea, groupDataWithLengths, player2OffsetArea, player2Data, strmOffsetArea, strmData };
			byte[] output = Combine (outputTemp);

			File.WriteAllBytes (outputS, output);

        }







        /// <summary>
        /// Convert info.bin to text.
        /// </summary>
        /// <param name="inputS"></param>
        /// <param name="outputS"></param>
        public static void InfoToText(string inputS, string outputS) {


            //Load info file.
            byte[] infoFile = File.ReadAllBytes(inputS);


            //Check if info is valid.
            byte[] magicHeader = PartOfByteArray(infoFile, 0, 4);
            if (!Encoding.UTF8.GetString(magicHeader).Equals("INFO")) { Console.WriteLine("ERROR! INVALID INFO FILE!"); Environment.Exit(1); }


            //Get offsets for each thing.
            int recordOffset0 = BitConverter.ToInt32(infoFile, 8);
            int recordOffset1 = BitConverter.ToInt32(infoFile, 12);
            int recordOffset2 = BitConverter.ToInt32(infoFile, 16);
            int recordOffset3 = BitConverter.ToInt32(infoFile, 20);
            int recordOffset4 = BitConverter.ToInt32(infoFile, 24);
            int recordOffset5 = BitConverter.ToInt32(infoFile, 28);
            int recordOffset6 = BitConverter.ToInt32(infoFile, 32);
            int recordOffset7 = BitConverter.ToInt32(infoFile, 36);


            //Get byte size for each record.
            int record0byteSize = BitConverter.ToInt32(infoFile, recordOffset0) * 4;
            int record1byteSize = BitConverter.ToInt32(infoFile, recordOffset1) * 4;
            int record2byteSize = BitConverter.ToInt32(infoFile, recordOffset2) * 4;
            int record3byteSize = BitConverter.ToInt32(infoFile, recordOffset3) * 4;
            int record4byteSize = BitConverter.ToInt32(infoFile, recordOffset4) * 4;
            int record5byteSize = BitConverter.ToInt32(infoFile, recordOffset5) * 4;
            int record6byteSize = BitConverter.ToInt32(infoFile, recordOffset6) * 4;
            int record7byteSize = BitConverter.ToInt32(infoFile, recordOffset7) * 4;


            //Get record addresses bytes.
            byte[] record0 = PartOfByteArray(infoFile, recordOffset0 + 4, record0byteSize);
            byte[] record1 = PartOfByteArray(infoFile, recordOffset1 + 4, record1byteSize);
            byte[] record2 = PartOfByteArray(infoFile, recordOffset2 + 4, record2byteSize);
            byte[] record3 = PartOfByteArray(infoFile, recordOffset3 + 4, record3byteSize);
            byte[] record4 = PartOfByteArray(infoFile, recordOffset4 + 4, record4byteSize);
            byte[] record5 = PartOfByteArray(infoFile, recordOffset5 + 4, record5byteSize);
            byte[] record6 = PartOfByteArray(infoFile, recordOffset6 + 4, record6byteSize);
            byte[] record7 = PartOfByteArray(infoFile, recordOffset7 + 4, record7byteSize);


            //Divide record addresses bytes into actual addresses.
            int[] record0Adresses = DivideByteArrayToIntAddresses(record0, record0byteSize / 4);
            int[] record1Adresses = DivideByteArrayToIntAddresses(record1, record1byteSize / 4);
            int[] record2Adresses = DivideByteArrayToIntAddresses(record2, record2byteSize / 4);
            int[] record3Adresses = DivideByteArrayToIntAddresses(record3, record3byteSize / 4);
            int[] record4Adresses = DivideByteArrayToIntAddresses(record4, record4byteSize / 4);
            int[] record5Adresses = DivideByteArrayToIntAddresses(record5, record5byteSize / 4);
            int[] record6Adresses = DivideByteArrayToIntAddresses(record6, record6byteSize / 4);
            int[] record7Adresses = DivideByteArrayToIntAddresses(record7, record7byteSize / 4);


            //Get data for each sseq info.
            List<int[]> sseqInfo = getSseqInfo(infoFile, record0Adresses);

            //Get data for each seqArc info.
            List<int[]> seqArcInfo = getSeqArcInfo(infoFile, record1Adresses);

            //Get data for each bank info.
            List<int[]> bankInfo = getBankInfo(infoFile, record2Adresses);

            //Get data for each wave info.
            List<int[]> waveInfo = getSeqArcInfo(infoFile, record3Adresses);

            //Get data for each player info.
            List<int[]> playerInfo = getPlayerInfo(infoFile, record4Adresses);

            //Get Group data.
            List<int[][]> groupSubRecords = getSubRecordsFromGroup(infoFile, record5Adresses);


            /* Group Sub Record Structure:
             * [X] [Y] [Z]
             *
             * X: Group number.
             * Y: 0 for type, 1 for nCount.
             * Z: Number on group sublist.
             */


            //Get data for each player2.
            List<int[]> player2Info = getPlayer2Info(infoFile, record6Adresses);

            //Get data for strm.
            List<int[]> strmInfo = getStrmInfo(infoFile, record7Adresses);




            //Write data to text file.
            List<string> output = new List<string>();


            //SSEQ
            output.Add("*SSEQ");
            for (int i = 0; i < sseqInfo.Count; i++)
            {

                int[] zero = { 0, 255, 40, 0 };
                if (Enumerable.SequenceEqual(sseqInfo[i], zero)) { output.Add("PLACEHOLDER"); } else {

                    string temp = "";

                    temp += "FileID: " + sseqInfo[i][0].ToString() + "; ";
                    temp += "Unknown1: " + sseqInfo[i][1].ToString() + "; ";
                    temp += "BankID: " + sseqInfo[i][2].ToString() + "; ";
                    temp += "Volume: " + sseqInfo[i][3].ToString() + "; ";
                    temp += "ChannelPriority: " + sseqInfo[i][4].ToString() + "; ";
                    temp += "PlayerPriority: " + sseqInfo[i][5].ToString() + "; ";
                    temp += "PlayerNumber: " + sseqInfo[i][6].ToString() + "; ";
                    temp += "Unknown2: " + sseqInfo[i][7].ToString() + "; ";
                    temp += "Unknown3: " + sseqInfo[i][8].ToString() + ";";

                    output.Add(temp);

                }

            }


            //SEQARC
            output.Add("*SEQARC");
            for (int i = 0; i < seqArcInfo.Count; i++)
            {

                int[] zero = { 0, 255, 40, 0 };
                if (Enumerable.SequenceEqual(seqArcInfo[i], zero)) { output.Add("PLACEHOLDER"); }
                else
                {

                    string temp = "";

                    temp += "FileID: " + seqArcInfo[i][0].ToString() + "; ";
                    temp += "Unknown: " + seqArcInfo[i][1].ToString() + ";";

                    output.Add(temp);
                }
            }


            //BANK
            output.Add("*BANK");
            for (int i = 0; i < bankInfo.Count; i++)
            {

                int[] zero = { 0, 255, 40, 0 };
                if (Enumerable.SequenceEqual(bankInfo[i], zero)) { output.Add("PLACEHOLDER"); }
                else
                {

                    string temp = "";

                    temp += "FileID: " + bankInfo[i][0].ToString() + "; ";
                    temp += "Unknown: " + bankInfo[i][1].ToString() + "; ";
                    temp += "Wave1: " + bankInfo[i][2].ToString() + "; ";
                    temp += "Wave2: " + bankInfo[i][3].ToString() + "; ";
                    temp += "Wave3: " + bankInfo[i][4].ToString() + "; ";
                    temp += "Wave4: " + bankInfo[i][5].ToString() + ";";

                    output.Add(temp);
                }
            }


            //WAVE
            output.Add("*WAVE");
            for (int i = 0; i < waveInfo.Count; i++)
            {

                int[] zero = { 0, 255, 40, 0 };
                if (Enumerable.SequenceEqual(waveInfo[i], zero)) { output.Add("PLACEHOLDER"); }
                else
                {

                    string temp = "";

                    temp += "FileID: " + waveInfo[i][0].ToString() + "; ";
                    temp += "Flag: " + waveInfo[i][1].ToString() + ";";

                    output.Add(temp);

                }
            }


            //PLAYER
            output.Add("*PLAYER");
            for (int i = 0; i < playerInfo.Count; i++)
            {
                int[] zero = { 0, 255, 40, 0 };
                if (Enumerable.SequenceEqual(playerInfo[i], zero)) { output.Add("PLACEHOLDER"); }
                else
                {

                    string temp = "";

                    temp += "Unknown: " + playerInfo[i][0].ToString() + "; ";
                    temp += "Unknown2: " + playerInfo[i][1].ToString() + ";";

                    output.Add(temp);
                }

            }



            //GROUP
            output.Add("*GROUP");
            for (int i = 0; i < groupSubRecords.Count; i++) {

                output.Add("~G" + i);

                for (int j = 0; j < groupSubRecords[i][0].Length; j++) {

                    string temp = "";

                    temp += "Type: " + groupSubRecords[i][0][j] + "; ";
                    temp += "nEntry: " + groupSubRecords[i][1][j] + ";";

                    output.Add(temp);

                }

            }



            //PLAYER
            output.Add("*PLAYER2");
            for (int i = 0; i < player2Info.Count; i++)
            {

                int[] zero = { 0, 255, 40, 0 };
                if (Enumerable.SequenceEqual(player2Info[i], zero)) { output.Add("PLACEHOLDER"); }
                else
                {

                    string temp = "";

                    temp += "NCount: " + player2Info[i][0].ToString() + "; ";
                    temp += "V1: " + player2Info[i][0].ToString() + "; ";
                    temp += "V2: " + player2Info[i][0].ToString() + "; ";
                    temp += "V3: " + player2Info[i][0].ToString() + "; ";
                    temp += "V4: " + player2Info[i][0].ToString() + "; ";
                    temp += "V5: " + player2Info[i][0].ToString() + "; ";
                    temp += "V6: " + player2Info[i][0].ToString() + "; ";
                    temp += "V7: " + player2Info[i][0].ToString() + "; ";
                    temp += "V8: " + player2Info[i][0].ToString() + "; ";
                    temp += "V9: " + player2Info[i][0].ToString() + "; ";
                    temp += "V10: " + player2Info[i][0].ToString() + "; ";
                    temp += "V11: " + player2Info[i][0].ToString() + "; ";
                    temp += "V12: " + player2Info[i][0].ToString() + "; ";
                    temp += "V13: " + player2Info[i][0].ToString() + "; ";
                    temp += "V14: " + player2Info[i][0].ToString() + "; ";
                    temp += "V15: " + player2Info[i][0].ToString() + "; ";
                    temp += "V16: " + player2Info[i][1].ToString() + ";";

                    output.Add(temp);


                }
            }



            //STRM
            output.Add("*STRM");
            for (int i = 0; i < strmInfo.Count; i++)
            {

                int[] zero = { 0, 255, 40, 0 };
                if (Enumerable.SequenceEqual(strmInfo[i], zero)) { output.Add("PLACEHOLDER"); }
                else
                {

                    string temp = "";

                    temp += "FileID: " + strmInfo[i][0].ToString() + "; ";
                    temp += "Unknown: " + strmInfo[i][1].ToString() + "; ";
                    temp += "Volume: " + strmInfo[i][0].ToString() + "; ";
                    temp += "Priority: " + strmInfo[i][0].ToString() + "; ";
                    temp += "Player2ID: " + strmInfo[i][0].ToString() + ";";

                    output.Add(temp);
                }

            }





            string[] final = output.ToArray();

            File.WriteAllLines(outputS, final);


        }









        /// <summary>
		/// Returns part of a byte array
		/// </summary>
		/// <returns>The of byte array.</returns>
		/// <param name="array">Array.</param>
		/// <param name="startIndex">Start index.</param>
		/// <param name="endIndex">End index.</param>
		public static byte[] PartOfByteArray(byte[] array, int startIndex, int length)
        {

            byte[] newArray = new byte[length];
            byte[] zero = { 0, 0, 0, 0 };

            for (int i = 0; i < length; i++)
            {
                newArray[i] = array[i + startIndex];
            }


            if (length > 0)
            {
                return newArray;
            }
            else
            {
                return zero;
            }

        }



        /// <summary>
		/// Divides the byte array to int addresses.
		/// </summary>
		/// <returns>The byte array to int addresses.</returns>
		/// <param name="array">Array.</param>
		/// <param name="size">Size.</param>
		public static int[] DivideByteArrayToIntAddresses(byte[] array, int size)
        {

            int[] newArray = new int[size];

            //Get addresses for every 4 bytes
            for (int i = 0; i < size * 4; i += 4)
            {
                newArray[i / 4] = BitConverter.ToInt32(array, i);
            }

            return newArray;

        }



        /// <summary>
		/// Strings to bytes, when other function fails.
		/// </summary>
		/// <returns>Byte array.</returns>
		/// <param name="a">String to convert</param>
		public static byte[] stringToBytes(string a)
        {

            byte[] hex = new byte[a.Length];

            for (int i = 0; i < hex.Length; i++)
            {
                hex[i] = Convert.ToByte(a[i]);
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
        public static byte[] combineByteArrayToOneByte(byte[][] b, byte seperator)
        {

            List<byte> allList = new List<byte>();
            for (int i = 0; i < b.Length; i++)
            {
                for (int j = 0; j < b[i].Length; j++)
                {
                    allList.Add(b[i][j]);
                }
                allList.Add(seperator);
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
        /// Get info from sseq.
        /// </summary>
        /// <param name="sseqAddresses">Sseq Addresses.</param>
        /// <returns>A list of ints (Each list is per file). Structure: {File ID, Unknown1, Bank, Volume, CPR, PPR, PLY, Unknown2, Unknown3}</returns>
        public static List<int[]> getSseqInfo(byte[] infoFile, int[] sseqAddresses) {

            List<int[]> data = new List<int[]>();

            //Get data for each address.
            for (int i = 0; i < sseqAddresses.Length; i++) {

                int[] sseqInfoBytes = new int[9];

                sseqInfoBytes[0] = BitConverter.ToInt16(infoFile, sseqAddresses[i]);
                sseqInfoBytes[1] = BitConverter.ToInt16(infoFile, sseqAddresses[i] + 2);
                sseqInfoBytes[2] = BitConverter.ToInt16(infoFile, sseqAddresses[i] + 4);
                sseqInfoBytes[3] = (int)infoFile[sseqAddresses[i] + 6];
                sseqInfoBytes[4] = (int)infoFile[sseqAddresses[i] + 7];
                sseqInfoBytes[5] = (int)infoFile[sseqAddresses[i] + 8];
                sseqInfoBytes[6] = (int)infoFile[sseqAddresses[i] + 9];
                sseqInfoBytes[7] = (int)infoFile[sseqAddresses[i] + 10];
                sseqInfoBytes[8] = (int)infoFile[sseqAddresses[i] + 11];

                if (sseqAddresses[i] != 0) data.Add(sseqInfoBytes);
                int[] zero = { 0, 255, 40, 0 };
                if (sseqAddresses[i] == 0) data.Add(zero);

            }

            return data;

        }



        /// <summary>
        /// Get seqArcInfo from addresses.
        /// </summary>
        /// <param name="infoFile">Info file.</param>
        /// <param name="seqArcAddresses">Addresses.</param>
        /// <returns>{FileID, Unknown}</returns>
        public static List<int[]> getSeqArcInfo(byte[] infoFile, int[] seqArcAddresses) {

            List<int[]> data = new List<int[]>();

            //Get data for each address.
            for (int i = 0; i < seqArcAddresses.Length; i++)
            {

                int[] seqArcInfoBytes = new int[2];

                seqArcInfoBytes[0] = BitConverter.ToInt16(infoFile, seqArcAddresses[i]);
                seqArcInfoBytes[1] = BitConverter.ToInt16(infoFile, seqArcAddresses[i] + 2);

                
                if (seqArcAddresses[i] != 0) data.Add(seqArcInfoBytes);
                int[] zero = { 0, 255, 40, 0 };
                if (seqArcAddresses[i] == 0) data.Add(zero);

            }

            return data;


        }





        /// <summary>
        /// Get info from bank.
        /// </summary>
        /// <param name="bankAddresses">Sseq Addresses.</param>
        /// <returns>{File ID, Unknown1, Bank, Volume, CPR, PPR, PLY, Unknown2, Unknown3}</returns>
        public static List<int[]> getBankInfo(byte[] infoFile, int[] bankAddresses)
        {

            List<int[]> data = new List<int[]>();

            //Get data for each address.
            for (int i = 0; i < bankAddresses.Length; i++)
            {

                int[] bankInfoBytes = new int[6];

                bankInfoBytes[0] = BitConverter.ToInt16(infoFile, bankAddresses[i]);
                bankInfoBytes[1] = BitConverter.ToInt16(infoFile, bankAddresses[i] + 2);
                bankInfoBytes[2] = BitConverter.ToInt16(infoFile, bankAddresses[i] + 4);
                bankInfoBytes[3] = BitConverter.ToInt16(infoFile, bankAddresses[i] + 6);
                bankInfoBytes[4] = BitConverter.ToInt16(infoFile, bankAddresses[i] + 8);
                bankInfoBytes[5] = BitConverter.ToInt16(infoFile, bankAddresses[i] + 10);

                if (bankAddresses[i] != 0) data.Add(bankInfoBytes);
                int[] zero = { 0, 255, 40, 0 };
                if (bankAddresses[i] == 0) data.Add(zero);

            }

            return data;

        }




        /// <summary>
        /// Get info from player.
        /// </summary>
        /// <param name="playerAddresses">Sseq Addresses.</param>
        /// <returns>Structure: {Unknown1, Unknown2}</returns>
        public static List<int[]> getPlayerInfo(byte[] infoFile, int[] playerAddresses)
        {

            List<int[]> data = new List<int[]>();

            //Get data for each address.
            for (int i = 0; i < playerAddresses.Length; i++)
            {

                int[] playerInfoBytes = new int[2];

                playerInfoBytes[0] = (int)infoFile[playerAddresses[i]];
                playerInfoBytes[1] = BitConverter.ToInt32(infoFile, playerAddresses[i] + 4);

                if (playerAddresses[i] != 0) data.Add(playerInfoBytes);
                int[] zero = { 0, 255, 40, 0 };
                if (playerAddresses[i] == 0) data.Add(zero);

            }

            return data;

        }



        /// <summary>
        /// Get info from player2.
        /// </summary>
        /// <param name="player2Addresses">Sseq Addresses.</param>
        /// <returns>Structure: {Ncount, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16}</returns>
        public static List<int[]> getPlayer2Info(byte[] infoFile, int[] player2Addresses)
        {

            List<int[]> data = new List<int[]>();

            //Get data for each address.
            for (int i = 0; i < player2Addresses.Length; i++)
            {

                int[] player2InfoBytes = new int[17];

                player2InfoBytes[0] = (int)infoFile[player2Addresses[i]];
                player2InfoBytes[1] = (int)infoFile[player2Addresses[i] + 1];
                player2InfoBytes[2] = (int)infoFile[player2Addresses[i] + 2];
                player2InfoBytes[3] = (int)infoFile[player2Addresses[i] + 3];
                player2InfoBytes[4] = (int)infoFile[player2Addresses[i] + 4];
                player2InfoBytes[5] = (int)infoFile[player2Addresses[i] + 5];
                player2InfoBytes[6] = (int)infoFile[player2Addresses[i] + 6];
                player2InfoBytes[7] = (int)infoFile[player2Addresses[i] + 7];
                player2InfoBytes[8] = (int)infoFile[player2Addresses[i] + 8];
                player2InfoBytes[9] = (int)infoFile[player2Addresses[i] + 9];
                player2InfoBytes[10] = (int)infoFile[player2Addresses[i] + 10];
                player2InfoBytes[11] = (int)infoFile[player2Addresses[i] + 11];
                player2InfoBytes[12] = (int)infoFile[player2Addresses[i] + 12];
                player2InfoBytes[13] = (int)infoFile[player2Addresses[i] + 13];
                player2InfoBytes[14] = (int)infoFile[player2Addresses[i] + 14];
                player2InfoBytes[15] = (int)infoFile[player2Addresses[i] + 15];
                player2InfoBytes[16] = (int)infoFile[player2Addresses[i] + 16];

                if (player2Addresses[i] != 0) data.Add(player2InfoBytes);
                int[] zero = { 0, 255, 40, 0 };
                if (player2Addresses[i] == 0) data.Add(zero);

            }

            return data;

        }





        /// <summary>
        /// Get info from strm.
        /// </summary>
        /// <param name="strmAddresses">Sseq Addresses.</param>
        /// <returns>{File ID, Unknown1, Volume, PRI, PLY}</returns>
        public static List<int[]> getStrmInfo(byte[] infoFile, int[] strmAddresses)
        {

            List<int[]> data = new List<int[]>();

            //Get data for each address.
            for (int i = 0; i < strmAddresses.Length; i++)
            {

                int[] strmInfoBytes = new int[5];

                strmInfoBytes[0] = BitConverter.ToInt16(infoFile, strmAddresses[i]);
                strmInfoBytes[1] = BitConverter.ToInt16(infoFile, strmAddresses[i] + 2);
                strmInfoBytes[2] = (int)infoFile[strmAddresses[i] + 4];
                strmInfoBytes[3] = (int)infoFile[strmAddresses[i] + 5];
                strmInfoBytes[4] = (int)infoFile[strmAddresses[i] + 6];

                if (strmAddresses[i] != 0) data.Add(strmInfoBytes);
                int[] zero = { 0, 255, 40, 0 };
                if (strmAddresses[i] == 0) data.Add(zero);

            }

            return data;

        }



        /// <summary>
        /// Get sub addresses from group.
        /// </summary>
        /// <param name="infoFile"></param>
        /// <param name="groupAddresses"></param>
        /// <returns></returns>
        public static List<int[][]> getSubRecordsFromGroup(byte[] infoFile, int[] groupAddresses) {

            List<int[][]> data = new List<int[][]>();

            for (int i = 0; i < groupAddresses.Length; i++) {

                List<int> type = new List<int>();
                List<int> nEntry = new List<int>();

                for (int j = 0; j < infoFile[groupAddresses[i]]; j++) {

                    type.Add(BitConverter.ToInt32(infoFile, groupAddresses[i] + j*8 + 4));
                    nEntry.Add(BitConverter.ToInt32(infoFile, groupAddresses[i] + j * 8 + 8));

                }

                int[][] typeAndEntry = { type.ToArray(), nEntry.ToArray()};

                data.Add(typeAndEntry.ToArray());

            }

            return data;

        }



        /// <summary>
        /// Get a section of text.
        /// </summary>
        /// <param name="textFile"></param>
        /// <param name="magicHeader"></param>
        /// <param name="endChar"></param>
        /// <returns></returns>
        public static string[] getSectionOfText(string[] textFile, string magicHeader, string endChar) {


            List<string> data = new List<string>();
            bool writing = false;

            for (int i = 0; i < textFile.Length; i++) {

                if (textFile[i].StartsWith(endChar)) { writing = false; }
                if (writing) { data.Add(textFile[i]); }
                if (textFile[i] == magicHeader) { writing = true; }

            }

            return data.ToArray();

        }

        public static string[] getSectionOfText(string[] textFile, string magicHeader, string endChar, string alternateEndChar)
        {


            List<string> data = new List<string>();
            bool writing = false;

            for (int i = 0; i < textFile.Length; i++)
            {

                if (textFile[i].StartsWith(endChar) || textFile[i].StartsWith(alternateEndChar)) { writing = false; }
                if (writing) { data.Add(textFile[i]); }
                if (textFile[i] == magicHeader) { writing = true; }

            }

            return data.ToArray();

        }



        /// <summary>
        /// Get Group Strings.
        /// </summary>
        /// <param name="textFile"></param>
        /// <param name="magicHeader"></param>
        /// <param name="endChar"></param>
        /// <param name="groupChar"></param>
        /// <returns></returns>
        public static string[][] getGroupStrings(string[] textFile, string magicHeader, string endChar, string groupChar) {

            List<string[]> data = new List<string[]>();

            string[] groupData = getSectionOfText(textFile, "*GROUP", "*");


            for (int i = 0; i < groupData.Length; i++) {

                for (int j = 0; j < groupData.Length; j++)
                {
                    if (groupData[j] == ("~G" + i.ToString()))
                    {
                        data.Add(getSectionOfText(groupData, "~G" + i.ToString(), "~", "*"));
                    }
                }

            }

            return data.ToArray();

        }









        /// <summary>
        /// Get data from strings.
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static byte[] getStringData(string[] strings, int[] dataStructure)
        {

            List<byte[]> data = new List<byte[]>();
            List<string> temp = new List<string>();

            for (int i = 0; i < strings.Length; i++)
            {

                int placeHolderCount = 0;
                if (strings[i] != "PLACEHOLDER")
                {

                    bool writing = false;
                    List<char> temp2 = new List<char>();
                    for (int j = 0; j < strings[i].Length; j++)
                    {
                        if (strings[i][j] == ';') { writing = false; temp2.Add(';'); }
                        if (writing && strings[i][j] != ' ') { temp2.Add(strings[i][j]); }
                        if (strings[i][j] == ':') { writing = true; }

                    }
                    string[] temp3 = ((string.Join(",", temp2.ToArray())).Replace(",", "")).Split(';');

                    //temp[i - placeHolderCount] = temp[i - placeHolderCount].Replace(",", "");

                    //string[] temp3 = temp[i].Split(';');

                    List<byte[]> bytes = new List<byte[]>();

                    //For padding zeroes
                    int paddedZeroes = 0;
                    for (int j = 0; j < dataStructure.Length; j++)
                    {
                        byte[] temp4 = new byte[dataStructure[j]];
                        temp4 = BitConverter.GetBytes(int.Parse(temp3[j - paddedZeroes]));

                        List<byte> temp5 = temp4.ToList();
                        for (int k = 4; k > dataStructure[j]; k--)
                        {
                            temp5.RemoveAt(k - 1);
                        }

                        byte[] blank = { 0 };
                        if (dataStructure[j] != 0) { bytes.Add(temp5.ToArray()); } else { bytes.Add(blank); paddedZeroes++; }
                    }

                    data.Add(Combine(bytes.ToArray()));

                } else { placeHolderCount++; }

            }


            return Combine(data.ToArray());

        }




		/// <summary>
		/// Gets the group string data.
		/// </summary>
		/// <returns>The group string data.</returns>
		/// <param name="groupStrings">Group strings.</param>
		public static byte[][] getGroupStringData(string[][] groupStrings) {

			//The byte list.
			List<byte[]> data = new List<byte[]>();

			for (int i = 0; i < groupStrings.Length; i++) {
				int[] groupDataStructure = { 4, 4 };
				data.Add(getStringData(groupStrings[i], groupDataStructure));
			}

			return data.ToArray ();

		}


		/// <summary>
		/// Gets the group data with lengths.
		/// </summary>
		/// <returns>The group data with lengths.</returns>
		/// <param name="groupData">Group data.</param>//
		public static byte[] getGroupDataWithLengths(byte[][] groupData, string[][] groupStrings) {

			List<byte[]> data = new List<byte[]> ();

			for (int i = 0; i < groupData.Length; i++) {
				byte[] length = BitConverter.GetBytes (groupStrings [i].Length);
				data.Add (length);
				data.Add (groupData [i]);
			}

			return Combine (data.ToArray ());

		}



		/// <summary>
		/// Gets the offset area.
		/// </summary>
		/// <returns>The offset area.</returns>
		/// <param name="infoData">Info data.</param>
		/// <param name="strings">Strings.</param>
		/// <param name="lengthOfDataAbove">Length of data above.</param>
		public static byte[] getOffsetArea (int byteSizeOfEachRecord, string[] strings, int lengthOfDataAbove) {

			List<byte[]> data = new List<byte[]> ();

			//Add number of records
			data.Add(BitConverter.GetBytes(strings.Length));

			int offsetSize = 4 * strings.Length + 4;

			//Get the offsets.
			int temp = 0;
			for (int i = 0; i < strings.Length; i++) {

                
                if (strings[i] != "PLACEHOLDER")
                {
                    data.Add(BitConverter.GetBytes(lengthOfDataAbove + offsetSize + temp));
                    temp += byteSizeOfEachRecord;
                } else { data.Add(BitConverter.GetBytes(0)); }

			}


			return Combine (data.ToArray ());

		}



        /// <summary>
        /// Get group offset areas with lengths.
        /// </summary>
        /// <param name="groupDataWithLengths"></param>
        /// <param name="groupStrings"></param>
        /// <param name="lengthOfRecordsAbove"></param>
        /// <returns></returns>
        public static byte[] getGroupOffsetArea(byte[] groupDataWithLengths, string[][] groupStrings, int lengthOfDataAbove) {

            //Data
            List<byte[]> data = new List<byte[]>();

            //Add length
            data.Add(BitConverter.GetBytes(groupStrings.Length));

            int offsetSize = 4 * groupStrings.Length + 4;

            //Add the arrays.
            int temp = 0;
            for (int i = 0; i < groupStrings.Length; i++) {

                data.Add(BitConverter.GetBytes(lengthOfDataAbove + offsetSize + temp));

                temp += groupStrings[i].Length * 8 + 4;

            }

            return Combine(data.ToArray());

        }


    }

}
