using System;
using Syroot.BinaryData;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NitroFileLoader
{

	/// <summary>
	/// Nitro file loader.
	/// </summary>
	public static class NitroFileLoader
	{

        //Info Tools
        #region infoTools

        //Load info file.
        public static NitroStructures.infoFile loadInfoFile(byte[] b) {

			//Make reader.
			MemoryStream src = new MemoryStream (b);
			BinaryDataReader br = new BinaryDataReader (src);

			//Make new file.
			NitroStructures.infoFile f = new NitroStructures.infoFile();

			//Load basic stuff.
			f.magic = br.ReadChars(4);
			f.fileSize = br.ReadUInt32 ();
			f.sseqOffset = br.ReadUInt32 ();
			f.seqArcOffset = br.ReadUInt32 ();
			f.bankOffset = br.ReadUInt32 ();
			f.waveOffset = br.ReadUInt32 ();
			f.playerOffset = br.ReadUInt32 ();
			f.groupOffset = br.ReadUInt32 ();
			f.player2Offset = br.ReadUInt32 ();
			f.strmOffset = br.ReadUInt32 ();
			f.reserved = br.ReadBytes (24);

			//Sseq record.
			br.Position = (int) f.sseqOffset;
			f.sseqRecord.count = br.ReadUInt32 ();
			f.sseqRecord.offsets = br.ReadUInt32s ((int)f.sseqRecord.count);
			f.sseqInfo = new NitroStructures.sseqInfo[(int)f.sseqRecord.count];
			for (int i = 0; i < f.sseqRecord.count; i++) {
			
				//Set position.
				br.Position = (int)f.sseqRecord.offsets[i];

				f.sseqInfo [i].fileId = br.ReadUInt32 ();
				f.sseqInfo [i].bank = br.ReadUInt16 ();
				f.sseqInfo [i].volume = br.ReadByte ();
				f.sseqInfo [i].channelPriority = br.ReadByte ();
				f.sseqInfo [i].playerPriority = br.ReadByte ();
				f.sseqInfo [i].playerNumber = br.ReadByte ();
				f.sseqInfo [i].unknown1 = br.ReadByte ();
				f.sseqInfo [i].unknown2 = br.ReadByte ();
				f.sseqInfo [i].isPlaceHolder = false;

				//If not null
				if (f.sseqRecord.offsets [i] == 0) {

					f.sseqInfo [i].isPlaceHolder = true;

				}
			
			}


			//SeqArc
			br.Position = (int) f.seqArcOffset;
			f.seqArcRecord.count = br.ReadUInt32 ();
			f.seqArcRecord.offsets = br.ReadUInt32s ((int)f.seqArcRecord.count);
			f.seqArcInfo = new NitroStructures.seqArcInfo[(int)f.seqArcRecord.count];
			for (int i = 0; i < f.seqArcRecord.count; i++) {

				//Set position.
				br.Position = (int)f.seqArcRecord.offsets[i];

				f.seqArcInfo [i].fileId = br.ReadUInt32 ();
				f.seqArcInfo [i].isPlaceHolder = false;

				//If not null
				if (f.seqArcRecord.offsets [i] == 0) {

					f.seqArcInfo [i].isPlaceHolder = true;

				}

			}


			//Bank
			br.Position = (int) f.bankOffset;
			f.bankRecord.count = br.ReadUInt32 ();
			f.bankRecord.offsets = br.ReadUInt32s ((int)f.bankRecord.count);
			f.bankInfo = new NitroStructures.bankInfo[(int)f.bankRecord.count];
			for (int i = 0; i < f.bankRecord.count; i++) {

				//Set position.
				br.Position = (int)f.bankRecord.offsets[i];

				f.bankInfo [i].fileId = br.ReadUInt32 ();
				f.bankInfo [i].wave0 = br.ReadUInt16 ();
				f.bankInfo [i].wave1 = br.ReadUInt16 ();
				f.bankInfo [i].wave2 = br.ReadUInt16 ();
				f.bankInfo [i].wave3 = br.ReadUInt16 ();
				f.bankInfo [i].isPlaceHolder = false;

				//If not null
				if (f.bankRecord.offsets [i] == 0) {

					f.bankInfo [i].isPlaceHolder = true;

				}

			}


			//Wave
			br.Position = (int) f.waveOffset;
			f.waveRecord.count = br.ReadUInt32 ();
			f.waveRecord.offsets = br.ReadUInt32s ((int)f.waveRecord.count);
			f.waveInfo = new NitroStructures.waveInfo[(int)f.waveRecord.count];
			for (int i = 0; i < f.waveRecord.count; i++) {

				//Set position.
				br.Position = (int)f.waveRecord.offsets[i];

				f.waveInfo [i].fileId = br.ReadUInt32 ();
				f.waveInfo [i].isPlaceHolder = false;

				//If not null
				if (f.waveRecord.offsets [i] == 0) {

					f.waveInfo [i].isPlaceHolder = true;

				}

			}


			//Player
			br.Position = (int) f.playerOffset;
			f.playerRecord.count = br.ReadUInt32 ();
			f.playerRecord.offsets = br.ReadUInt32s ((int)f.playerRecord.count);
			f.playerInfo = new NitroStructures.playerInfo[(int)f.playerRecord.count];
			for (int i = 0; i < f.playerRecord.count; i++) {

				//Set position.
				br.Position = (int)f.playerRecord.offsets[i];

				f.playerInfo [i].seqMax = br.ReadUInt16();
				f.playerInfo [i].channelFlag = br.ReadUInt16 ();
				f.playerInfo [i].heapSize = br.ReadUInt32 ();
				f.playerInfo [i].isPlaceHolder = false;

				//If not null.
				if (f.playerRecord.offsets [i] == 0) {

					f.playerInfo [i].isPlaceHolder = true;

				}

			}


			//Group
			br.Position = (int) f.groupOffset;
			f.groupRecord.count = br.ReadUInt32 ();
			f.groupRecord.offsets = br.ReadUInt32s ((int)f.groupRecord.count);
			f.groupInfo = new NitroStructures.groupInfo[(int)f.groupRecord.count];
			for (int i = 0; i < f.groupRecord.count; i++) {

				//Set position.
				br.Position = (int)f.groupRecord.offsets[i];

				//Read count.
				f.groupInfo[i].count = br.ReadUInt32();
				f.groupInfo [i].isPlaceHolder = false;

				//Get sub info.
				f.groupInfo[i].subInfo = new NitroStructures.groupSubInfo[(int)f.groupInfo[i].count];
				for (int j = 0; j < f.groupInfo[i].count; j++) {

					//Now get the data.
					f.groupInfo[i].subInfo[j].type = br.ReadByte();
					f.groupInfo[i].subInfo[j].loadFlag = br.ReadByte();
					f.groupInfo [i].subInfo [j].padding = br.ReadUInt16 ();
					f.groupInfo [i].subInfo [j].nEntry = br.ReadUInt32 ();

					//Subgroups can't be placeholders, since there is not offset to it.

				}


				//Check if placeholder.
				if (f.groupRecord.offsets[i] == 0) {
					f.groupInfo [i].isPlaceHolder = true;
				}

			}


			//Player2
			br.Position = (int) f.player2Offset;
			f.player2Record.count = br.ReadUInt32 ();
			f.player2Record.offsets = br.ReadUInt32s ((int)f.player2Record.count);
			f.player2Info = new NitroStructures.player2Info[(int)f.player2Record.count];
			for (int i = 0; i < f.player2Record.count; i++) {

				//Set position.
				br.Position = (int)f.player2Record.offsets[i];

				f.player2Info [i].count = br.ReadByte ();
				f.player2Info [i].v0 = br.ReadByte ();
				f.player2Info [i].v1 = br.ReadByte ();
				f.player2Info [i].v2 = br.ReadByte ();
				f.player2Info [i].v3 = br.ReadByte ();
				f.player2Info [i].v4 = br.ReadByte ();
				f.player2Info [i].v5 = br.ReadByte ();
				f.player2Info [i].v6 = br.ReadByte ();
				f.player2Info [i].v7 = br.ReadByte ();
				f.player2Info [i].v8 = br.ReadByte ();
				f.player2Info [i].v9 = br.ReadByte ();
				f.player2Info [i].v10 = br.ReadByte ();
				f.player2Info [i].v11 = br.ReadByte ();
				f.player2Info [i].v12 = br.ReadByte ();
				f.player2Info [i].v13 = br.ReadByte ();
				f.player2Info [i].v14 = br.ReadByte ();
				f.player2Info [i].v15 = br.ReadByte ();
				f.player2Info [i].reserved = br.ReadBytes (7);
				f.player2Info [i].isPlaceHolder = false;

				//If not null
				if (f.player2Record.offsets [i] == 0) {

					f.player2Info [i].isPlaceHolder = true;

				}

			}



			//Strm
			br.Position = (int) f.strmOffset;
			f.strmRecord.count = br.ReadUInt32 ();
			f.strmRecord.offsets = br.ReadUInt32s ((int)f.strmRecord.count);
			f.strmInfo = new NitroStructures.strmInfo[(int)f.strmRecord.count];
			for (int i = 0; i < f.strmRecord.count; i++) {

				//Set position.
				br.Position = (int)f.waveRecord.offsets[i];

				f.strmInfo [i].fileId = br.ReadUInt32 ();
				f.strmInfo [i].volume = br.ReadByte ();
				f.strmInfo [i].priority = br.ReadByte ();
				f.strmInfo [i].player = br.ReadByte ();
				f.strmInfo [i].reserved = br.ReadBytes (5);
				f.strmInfo [i].isPlaceHolder = false;

				//If not null
				if (f.strmRecord.offsets [i] == 0) {

					f.strmInfo [i].isPlaceHolder = true;

				}

			}


			//Return final.
			return f;

		}



		/// <summary>
		/// Info file to bytes.
		/// </summary>
		/// <returns>The to bytes.</returns>
		/// <param name="f">F.</param>
		public static byte[] infoToBytes(NitroStructures.infoFile f) {
		
			//Update offsets.
			f.updateOffsets();

			//Make new stream.
			MemoryStream o = new MemoryStream();
			BinaryWriter bw = new BinaryWriter (o);

			//Start writing simple data.
			bw.Write (f.magic);
			bw.Write (f.fileSize);
			bw.Write (f.sseqOffset);
			bw.Write (f.seqArcOffset);
			bw.Write (f.bankOffset);
			bw.Write (f.waveOffset);
			bw.Write (f.playerOffset);
			bw.Write (f.groupOffset);
			bw.Write (f.player2Offset);
			bw.Write (f.strmOffset);
			bw.Write (f.reserved);

			//Write sseq.
			bw.Write (f.sseqRecord.count);
			for (int i = 0; i < f.sseqRecord.offsets.Length; i++) {
			
				if (f.sseqInfo [i].isPlaceHolder) {
					bw.Write (0x00000000);				
				} else {
					bw.Write (f.sseqRecord.offsets [i]);
				}
			
			}
			foreach (NitroStructures.sseqInfo i in f.sseqInfo) {

				if (!i.isPlaceHolder) {
				
					bw.Write (i.fileId);
					bw.Write (i.bank);
					bw.Write (i.volume);
					bw.Write (i.channelPriority);
					bw.Write (i.playerPriority);
					bw.Write (i.playerNumber);
					bw.Write (i.unknown1);
					bw.Write (i.unknown2);
				
				}

			}


			//Write SeqArc
			bw.Write (f.seqArcRecord.count);
			for (int i = 0; i < f.seqArcRecord.offsets.Length; i++) {

				if (f.seqArcInfo [i].isPlaceHolder) {
					bw.Write (0x00000000);				
				} else {
					bw.Write (f.seqArcRecord.offsets [i]);
				}

			}
			foreach (NitroStructures.seqArcInfo i in f.seqArcInfo) {

				if (!i.isPlaceHolder) {

					bw.Write (i.fileId);

				}

			}


			//Write Bank
			bw.Write (f.bankRecord.count);
			for (int i = 0; i < f.bankRecord.offsets.Length; i++) {

				if (f.bankInfo [i].isPlaceHolder) {
					bw.Write (0x00000000);				
				} else {
					bw.Write (f.bankRecord.offsets [i]);
				}

			}
			foreach (NitroStructures.bankInfo i in f.bankInfo) {

				if (!i.isPlaceHolder) {

					bw.Write (i.fileId);
					bw.Write (i.wave0);
					bw.Write (i.wave1);
					bw.Write (i.wave2);
					bw.Write (i.wave3);

				}

			}


			//Write Wave
			bw.Write (f.waveRecord.count);
			for (int i = 0; i < f.waveRecord.offsets.Length; i++) {

				if (f.waveInfo [i].isPlaceHolder) {
					bw.Write (0x00000000);				
				} else {
					bw.Write (f.waveRecord.offsets [i]);
				}

			}
			foreach (NitroStructures.waveInfo i in f.waveInfo) {

				if (!i.isPlaceHolder) {

					bw.Write (i.fileId);

				}

			}


			//Write Player
			bw.Write (f.playerRecord.count);
			for (int i = 0; i < f.playerRecord.offsets.Length; i++) {

				if (f.playerInfo [i].isPlaceHolder) {
					bw.Write (0x00000000);				
				} else {
					bw.Write (f.playerRecord.offsets [i]);
				}

			}
			foreach (NitroStructures.playerInfo i in f.playerInfo) {

				if (!i.isPlaceHolder) {

					bw.Write (i.seqMax);
					bw.Write (i.channelFlag);
					bw.Write (i.heapSize);

				}

			}


			//Write group
			bw.Write (f.groupRecord.count);
			for (int i = 0; i < f.groupRecord.offsets.Length; i++) {

				if (f.groupInfo [i].isPlaceHolder) {
					bw.Write (0x00000000);				
				} else {
					bw.Write (f.groupRecord.offsets [i]);
				}

			}
			foreach (NitroStructures.groupInfo i in f.groupInfo) {
			
				bw.Write (i.count);
				foreach (NitroStructures.groupSubInfo j in i.subInfo) {
				
					bw.Write (j.type);
					bw.Write (j.loadFlag);
					bw.Write (j.padding);
					bw.Write (j.nEntry);
				
				}
			
			}

			//Write Player2
			bw.Write (f.player2Record.count);
			for (int i = 0; i < f.player2Record.offsets.Length; i++) {

				if (f.player2Info [i].isPlaceHolder) {
					bw.Write (0x00000000);				
				} else {
					bw.Write (f.player2Record.offsets [i]);
				}

			}
			foreach (NitroStructures.player2Info i in f.player2Info) {

				if (!i.isPlaceHolder) {

					bw.Write (i.count);
					bw.Write (i.v0);
					bw.Write (i.v1);
					bw.Write (i.v2);
					bw.Write (i.v3);
					bw.Write (i.v4);
					bw.Write (i.v5);
					bw.Write (i.v6);
					bw.Write (i.v7);
					bw.Write (i.v8);
					bw.Write (i.v9);
					bw.Write (i.v10);
					bw.Write (i.v11);
					bw.Write (i.v12);
					bw.Write (i.v13);
					bw.Write (i.v14);
					bw.Write (i.v15);
					bw.Write (i.reserved);

				}

			}


			//Write Strm
			bw.Write (f.strmRecord.count);
			for (int i = 0; i < f.strmRecord.offsets.Length; i++) {

				if (f.strmInfo [i].isPlaceHolder) {
					bw.Write (0x00000000);				
				} else {
					bw.Write (f.strmRecord.offsets [i]);
				}

			}
			foreach (NitroStructures.strmInfo i in f.strmInfo) {

				if (!i.isPlaceHolder) {

					bw.Write (i.fileId);
					bw.Write (i.volume);
					bw.Write (i.priority);
					bw.Write (i.player);
					bw.Write (i.reserved);

				}

			}


			//Return bytes.
			return o.ToArray();
		
		}

        #endregion


        //Symb Tools
        #region symbTools

        /// <summary>
        /// Load an symb file.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static NitroStructures.symbFile loadSymbFile(byte[] b) {

            //New reader.
            MemoryStream src = new MemoryStream(b);
            BinaryDataReader br = new BinaryDataReader(src);

            //New File.
            NitroStructures.symbFile s = new NitroStructures.symbFile();

			//Read lame stuff.
			s.magic = br.ReadChars(4);
			s.fileSize = br.ReadUInt32 ();
			s.sseqOffset = br.ReadUInt32 ();
			s.seqArcOffset = br.ReadUInt32 ();
			s.bankOffset = br.ReadUInt32 ();
			s.waveOffset = br.ReadUInt32 ();
			s.playerOffset = br.ReadUInt32 ();
			s.groupOffset = br.ReadUInt32 ();
			s.player2Offset = br.ReadUInt32 ();
			s.strmOffset = br.ReadUInt32 ();
			s.reserved = br.ReadBytes (24);


			//Read records.
			br.Position = (int) s.sseqOffset;
			s.sseqRecord.count = br.ReadUInt32 ();
			s.sseqRecord.offsets = br.ReadUInt32s ((int)s.sseqRecord.count);

			br.Position = (int) s.seqArcOffset;
			s.seqArcRecord.count = br.ReadUInt32 ();
			s.seqArcRecord.subNames = new NitroStructures.seqArcSubName[(int)s.seqArcRecord.count];
			for (int i = 0; i < s.seqArcRecord.count; i++) {
			
				s.seqArcRecord.subNames[i].seqArcNameOffset = br.ReadUInt32 ();
				s.seqArcRecord.subNames[i].seqArcSubOffset = br.ReadUInt32 ();

			}

			//Get sub records.
			s.seqArcSubRecord = new NitroStructures.standardSymbName[(int)s.seqArcRecord.count];
			for (int i = 0; i < s.seqArcRecord.count; i++) {

				//Set position to sub records.
				br.Position = s.seqArcRecord.subNames[i].seqArcSubOffset;

				//Read offsets.
				s.seqArcSubRecord[i].count = br.ReadUInt32();
				s.seqArcSubRecord [i].offsets = br.ReadUInt32s ((int)s.seqArcSubRecord[i].count);

			}

			br.Position = (int) s.bankOffset;
			s.bankRecord.count = br.ReadUInt32 ();
			s.bankRecord.offsets = br.ReadUInt32s ((int)s.bankRecord.count);

			br.Position = (int) s.waveOffset;
			s.waveRecord.count = br.ReadUInt32 ();
			s.waveRecord.offsets = br.ReadUInt32s ((int)s.waveRecord.count);

			br.Position = (int) s.playerOffset;
			s.playerRecord.count = br.ReadUInt32 ();
			s.playerRecord.offsets = br.ReadUInt32s ((int)s.playerRecord.count);

			br.Position = (int) s.groupOffset;
			s.groupRecord.count = br.ReadUInt32 ();
			s.groupRecord.offsets = br.ReadUInt32s ((int)s.groupRecord.count);

			br.Position = (int) s.player2Offset;
			s.player2Record.count = br.ReadUInt32 ();
			s.player2Record.offsets = br.ReadUInt32s ((int)s.player2Record.count);

			br.Position = (int) s.strmOffset;
			s.strmRecord.count = br.ReadUInt32 ();
			s.strmRecord.offsets = br.ReadUInt32s ((int)s.strmRecord.count);

			//Now time to get the strings.
			s.strings.sseqStrings = new NitroStructures.symbStringEntry[(int)s.sseqRecord.offsets.Length];
			for (int i = 0; i < s.sseqRecord.offsets.Length; i++) {

				//Read strings.
				s.strings.sseqStrings[i].name = getStringFromOffset(br, s.sseqRecord.offsets[i]);
				s.strings.sseqStrings [i].seperator = (byte)0;

				s.strings.sseqStrings [i].isPlaceHolder = false;
				if (s.sseqRecord.offsets[i] == 0) {s.strings.sseqStrings [i].isPlaceHolder = true;}

			}


			//SeqArc
			s.strings.seqArcStrings = new NitroStructures.symbStringEntry[(int)s.seqArcRecord.count];
			for (int i = 0; i < s.seqArcRecord.count; i++) {

				//Read strings.
				s.strings.seqArcStrings[i].name = getStringFromOffset(br, s.seqArcRecord.subNames[i].seqArcNameOffset);
				s.strings.seqArcStrings [i].seperator = (byte)0;

				s.strings.seqArcStrings [i].isPlaceHolder = false;
				if (s.seqArcRecord.subNames[i].seqArcNameOffset == 0) {s.strings.seqArcStrings [i].isPlaceHolder = true;}

			}


			//Subsections
			s.strings.seqArcSubStrings = new NitroStructures.symbStringEntry[(int)s.seqArcRecord.count][];
			for (int i = 0; i < s.seqArcRecord.count; i++) {

				//Read strings.
				s.strings.seqArcSubStrings[i] = new NitroStructures.symbStringEntry[s.seqArcSubRecord[i].count];
				for (int j = 0; j < s.seqArcSubRecord[i].count; j++) {

					s.strings.seqArcSubStrings[i][j].name = getStringFromOffset(br, s.seqArcSubRecord[i].offsets[j]);
					s.strings.seqArcSubStrings [i] [j].seperator = 0;

					s.strings.seqArcSubStrings [i][j].isPlaceHolder = false;
					if (s.seqArcSubRecord[i].offsets[j] == 0) {s.strings.seqArcSubStrings [i][j].isPlaceHolder = true;}

				}

			}


			//Bank
			s.strings.bankStrings = new NitroStructures.symbStringEntry[(int)s.bankRecord.offsets.Length];
			for (int i = 0; i < s.bankRecord.offsets.Length; i++) {

				//Read strings.
				s.strings.bankStrings[i].name = getStringFromOffset(br, s.bankRecord.offsets[i]);
				s.strings.bankStrings [i].seperator = (byte)0;

				s.strings.bankStrings [i].isPlaceHolder = false;
				if (s.bankRecord.offsets[i] == 0) {s.strings.bankStrings [i].isPlaceHolder = true;}

			}


			//Wave
			s.strings.waveStrings = new NitroStructures.symbStringEntry[(int)s.waveRecord.offsets.Length];
			for (int i = 0; i < s.waveRecord.offsets.Length; i++) {

				//Read strings.
				s.strings.waveStrings[i].name = getStringFromOffset(br, s.waveRecord.offsets[i]);
				s.strings.waveStrings [i].seperator = (byte)0;

				s.strings.waveStrings [i].isPlaceHolder = false;
				if (s.waveRecord.offsets[i] == 0) {s.strings.waveStrings [i].isPlaceHolder = true;}

			}


			//Player
			s.strings.playerStrings = new NitroStructures.symbStringEntry[(int)s.playerRecord.offsets.Length];
			for (int i = 0; i < s.playerRecord.offsets.Length; i++) {

				//Read strings.
				s.strings.playerStrings[i].name = getStringFromOffset(br, s.playerRecord.offsets[i]);
				s.strings.playerStrings [i].seperator = (byte)0;

				s.strings.playerStrings [i].isPlaceHolder = false;
				if (s.playerRecord.offsets[i] == 0) {s.strings.playerStrings [i].isPlaceHolder = true;}

			}


			//Group
			s.strings.groupStrings = new NitroStructures.symbStringEntry[(int)s.groupRecord.offsets.Length];
			for (int i = 0; i < s.groupRecord.offsets.Length; i++) {

				//Read strings.
				s.strings.groupStrings[i].name = getStringFromOffset(br, s.groupRecord.offsets[i]);
				s.strings.groupStrings [i].seperator = (byte)0;

				s.strings.groupStrings [i].isPlaceHolder = false;
				if (s.groupRecord.offsets[i] == 0) {s.strings.groupStrings [i].isPlaceHolder = true;}

			}


			//Player2
			s.strings.player2Strings = new NitroStructures.symbStringEntry[(int)s.player2Record.offsets.Length];
			for (int i = 0; i < s.player2Record.offsets.Length; i++) {

				//Read strings.
				s.strings.player2Strings[i].name = getStringFromOffset(br, s.player2Record.offsets[i]);
				s.strings.player2Strings [i].seperator = (byte)0;

				s.strings.player2Strings [i].isPlaceHolder = false;
				if (s.player2Record.offsets[i] == 0) {s.strings.player2Strings [i].isPlaceHolder = true;}

			}


			//Strm
			s.strings.strmStrings = new NitroStructures.symbStringEntry[(int)s.strmRecord.offsets.Length];
			for (int i = 0; i < s.strmRecord.offsets.Length; i++) {

				//Read strings.
				s.strings.strmStrings[i].name = getStringFromOffset(br, s.strmRecord.offsets[i]);
				s.strings.strmStrings [i].seperator = (byte)0;

				s.strings.strmStrings [i].isPlaceHolder = false;
				if (s.strmRecord.offsets[i] == 0) {s.strings.strmStrings [i].isPlaceHolder = true;}

			}


            //Return file.
            return s;

        }


		/// <summary>
		/// Gets the string from offset.
		/// </summary>
		/// <returns>The string from offset.</returns>
		/// <param name="br">Br.</param>
		/// <param name="offset">Offset.</param>
		public static string getStringFromOffset(BinaryDataReader br, UInt32 offset) {
		
			br.Position = (int)offset;

			List<char> stringChars = new List<char> ();
			bool read = false;
			while (!read) {
				byte b = br.ReadByte ();
				if (b == 0) {
					read = true;
				} else {
					br.Position = br.Position-1;
					stringChars.Add (br.ReadChars(1)[0]);
				}
			}
				
			return string.Join ("", stringChars);
		
		}


		public static string ByteArrayToString(byte[] ba)
		{
			StringBuilder hex = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
				hex.AppendFormat("{0:x2}", b);
			return hex.ToString();
		}

        /// <summary>
        /// Symb file to bytes.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] symbToBytes(NitroStructures.symbFile s)
        {

            //New reader.
            MemoryStream o = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(o);

			//Update offsets.
			s.fixOffsets();

			//Write basic data.
			bw.Write (s.magic);
			bw.Write (s.fileSize);
			bw.Write (s.sseqOffset);
			bw.Write (s.seqArcOffset);
			bw.Write (s.bankOffset);
			bw.Write (s.waveOffset);
			bw.Write (s.playerOffset);
			bw.Write (s.groupOffset);
			bw.Write (s.player2Offset);
			bw.Write (s.strmOffset);
			bw.Write (s.reserved);

			//Write stuff.
			bw.Write(s.sseqRecord.count);
			for (int i = 0; i < s.sseqRecord.count; i++) {
			
				//If placeholder write it, if not, write offset.
				if (s.strings.sseqStrings [i].isPlaceHolder) {
					bw.Write (0x00000000);
				} else {
					bw.Write (s.sseqRecord.offsets [i]);
				}
			
			}

			bw.Write(s.seqArcRecord.count);
			for (int i = 0; i < s.seqArcRecord.count; i++) {
			
				if (s.strings.seqArcStrings [i].isPlaceHolder) {
					bw.Write (0x00000000);
					bw.Write (0x00000000);
				} else {
					bw.Write (s.seqArcRecord.subNames [i].seqArcNameOffset);
					bw.Write (s.seqArcRecord.subNames [i].seqArcSubOffset);
				}

			}
			for (int i = 0; i < s.seqArcRecord.count; i++) {

				if (!s.strings.seqArcStrings [i].isPlaceHolder) {

					bw.Write (s.seqArcSubRecord [i].count);
					for (int j = 0; j < s.seqArcSubRecord [i].offsets.Length; j++) {

						if (s.strings.seqArcSubStrings [i] [j].isPlaceHolder) {
							bw.Write (0x00000000);
						} else {
							bw.Write (s.seqArcSubRecord [i].offsets [j]);
						}
					}

				}

			}
			bw.Write(s.bankRecord.count);
			for (int i = 0; i < s.bankRecord.count; i++) {

				//If placeholder write it, if not, write offset.
				if (s.strings.bankStrings [i].isPlaceHolder) {
					bw.Write (0x00000000);
				} else {
					bw.Write (s.bankRecord.offsets [i]);
				}

			}
			bw.Write(s.waveRecord.count);
			for (int i = 0; i < s.waveRecord.count; i++) {

				//If placeholder write it, if not, write offset.
				if (s.strings.waveStrings [i].isPlaceHolder) {
					bw.Write (0x00000000);
				} else {
					bw.Write (s.waveRecord.offsets [i]);
				}

			}
			bw.Write(s.playerRecord.count);
			for (int i = 0; i < s.playerRecord.count; i++) {

				//If placeholder write it, if not, write offset.
				if (s.strings.playerStrings [i].isPlaceHolder) {
					bw.Write (0x00000000);
				} else {
					bw.Write (s.playerRecord.offsets [i]);
				}

			}
			bw.Write(s.groupRecord.count);
			for (int i = 0; i < s.groupRecord.count; i++) {

				//If placeholder write it, if not, write offset.
				if (s.strings.groupStrings [i].isPlaceHolder) {
					bw.Write (0x00000000);
				} else {
					bw.Write (s.groupRecord.offsets [i]);
				}

			}
			bw.Write(s.player2Record.count);
			for (int i = 0; i < s.player2Record.count; i++) {

				//If placeholder write it, if not, write offset.
				if (s.strings.player2Strings [i].isPlaceHolder) {
					bw.Write (0x00000000);
				} else {
					bw.Write (s.player2Record.offsets [i]);
				}

			}
			bw.Write(s.strmRecord.count);
			for (int i = 0; i < s.strmRecord.count; i++) {

				//If placeholder write it, if not, write offset.
				if (s.strings.strmStrings [i].isPlaceHolder) {
					bw.Write (0x00000000);
				} else {
					bw.Write (s.strmRecord.offsets [i]);
				}

			}


			//Now time to write the strings.
			foreach (NitroStructures.symbStringEntry name in s.strings.sseqStrings) {

				if (!name.isPlaceHolder) {
					bw.Write (name.name.ToCharArray ());
					bw.Write (name.seperator);
				}

			}
			//Seq arc.
			for (int i = 0; i < s.seqArcRecord.count; i++) {

				if (!s.strings.seqArcStrings [i].isPlaceHolder) {
					bw.Write (s.strings.seqArcStrings [i].name.ToCharArray ());
					bw.Write (s.strings.seqArcStrings [i].seperator);
				}

				for (int j = 0; j < s.strings.seqArcSubStrings[i].Length; j++) {

					if (!s.strings.seqArcSubStrings [i] [j].isPlaceHolder) {
						bw.Write (s.strings.seqArcSubStrings [i] [j].name.ToCharArray ());
						bw.Write (s.strings.seqArcSubStrings [i] [j].seperator);
					}

				}

			}
			//More.
			foreach (NitroStructures.symbStringEntry name in s.strings.bankStrings) {

				if (!name.isPlaceHolder) {
					bw.Write (name.name.ToCharArray ());
					bw.Write (name.seperator);
				}

			}
			foreach (NitroStructures.symbStringEntry name in s.strings.waveStrings) {

				if (!name.isPlaceHolder) {
					bw.Write (name.name.ToCharArray ());
					bw.Write (name.seperator);
				}

			}
			foreach (NitroStructures.symbStringEntry name in s.strings.playerStrings) {

				if (!name.isPlaceHolder) {
					bw.Write (name.name.ToCharArray ());
					bw.Write (name.seperator);
				}

			}
			foreach (NitroStructures.symbStringEntry name in s.strings.groupStrings) {

				if (!name.isPlaceHolder) {
					bw.Write (name.name.ToCharArray ());
					bw.Write (name.seperator);
				}

			}
			foreach (NitroStructures.symbStringEntry name in s.strings.player2Strings) {

				if (!name.isPlaceHolder) {
					bw.Write (name.name.ToCharArray ());
					bw.Write (name.seperator);
				}

			}
			foreach (NitroStructures.symbStringEntry name in s.strings.strmStrings) {

				if (!name.isPlaceHolder) {
					bw.Write (name.name.ToCharArray ());
					bw.Write (name.seperator);
				}

			}


            //Return file.
            return o.ToArray();

        }

        #endregion

    }




}

