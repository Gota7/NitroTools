using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syroot.BinaryData;

namespace NitroFileLoader
{
    /// <summary>
    /// Some structures for the nitro sound data.
    /// </summary>
	public class NitroStructures
    {

        //Symb stuff.
        #region symbStructures

        /// <summary>
        /// Basic SYMB file structure.
        /// </summary>
        public struct symbFile {

			public char[] magic; //SYMB magic.
			public UInt32 fileSize; //Filesize.
			public UInt32 sseqOffset; //Sseq Offset.
			public UInt32 seqArcOffset; //SeqArc Offset.
			public UInt32 bankOffset; //Bank Offset.
			public UInt32 waveOffset; //Wave Offset.
			public UInt32 playerOffset; //Player Offset.
			public UInt32 groupOffset; //Group Offset.
			public UInt32 player2Offset; //Player2 Offset.
			public UInt32 strmOffset; //Stream Offset.
			public byte[] reserved; //Zereos (24).

			public standardSymbName sseqRecord; //sseqNames.
			public seqArcName seqArcRecord; //SeqArc names.
			public standardSymbName[] seqArcSubRecord; //Seq Arc Sub Names.
			public standardSymbName bankRecord; //Bank names.
			public standardSymbName waveRecord; //Wave names.
			public standardSymbName playerRecord; //Player names.
			public standardSymbName groupRecord; //Group names.
			public standardSymbName player2Record; //Player2 names.
			public standardSymbName strmRecord; //Strm names.

			public symbStrings strings; //Strings in symb.


            /// <summary>
            /// Fix the offsets.
            /// </summary>
            public void fixOffsets() {

				//Make offset.
				UInt32 o = 0;

				//Add header stuff.
				o += (UInt32)(magic.Length + 9*4 + reserved.Length);


				//Sseq Offset is here.
				sseqOffset = o;

				//Add length of sseq.
				o+=(UInt32) 4;

				//Fix the length of the sseq.
				sseqRecord.count = (UInt32)strings.sseqStrings.Length;
				sseqRecord.offsets = new UInt32[(int)sseqRecord.count];

				//Count the sseq offsets.
				o+=(UInt32)(4*sseqRecord.count);


				//SeqArc Offset is here.
				seqArcOffset = o;

				//Add length of seqArc.
				o+=(UInt32) 4;

				//Fix the length of the seqArc.
				seqArcRecord.count = (UInt32)strings.seqArcStrings.Length;
				seqArcRecord.subNames = new seqArcSubName[(int)seqArcRecord.count];

				UInt32 seqArcSubPosition = (UInt32)(o+(8*seqArcRecord.count));

				seqArcSubRecord = new standardSymbName[strings.seqArcSubStrings.Count()];
				for (int i = 0; i < seqArcRecord.count; i++) {
				
					seqArcSubRecord [i].count = (UInt32) strings.seqArcSubStrings [i].Length;
					seqArcSubRecord [i].offsets = new UInt32[seqArcSubRecord [i].count];

					seqArcRecord.subNames [i].seqArcSubOffset = seqArcSubPosition;
					seqArcSubPosition += (UInt32)(seqArcSubRecord[i].count*4 + 4);

					o+= (UInt32)(seqArcSubRecord[i].count*4 + 4);
				
				}

				//Count the seqArc offsets.
				o+=(UInt32)(8*seqArcRecord.count);

				//New Offset.
				bankOffset = o;

				//Add length.
				o+=(UInt32) 4;

				//Fix the length.
				bankRecord.count = (UInt32)strings.bankStrings.Length;
				bankRecord.offsets = new UInt32[(int)bankRecord.count];

				//Count the offsets.
				o+=(UInt32)(4*bankRecord.count);


				//New Offset.
				waveOffset = o;

				//Add length.
				o+=(UInt32) 4;

				//Fix the length.
				waveRecord.count = (UInt32)strings.waveStrings.Length;
				waveRecord.offsets = new UInt32[(int)waveRecord.count];

				//Count the offsets.
				o+=(UInt32)(4*waveRecord.count);


				//New Offset.
				playerOffset = o;

				//Add length.
				o+=(UInt32) 4;

				//Fix the length.
				playerRecord.count = (UInt32)strings.playerStrings.Length;
				playerRecord.offsets = new UInt32[(int)playerRecord.count];

				//Count the offsets.
				o+=(UInt32)(4*playerRecord.count);


				//New Offset.
				groupOffset = o;

				//Add length.
				o+=(UInt32) 4;

				//Fix the length.
				groupRecord.count = (UInt32)strings.groupStrings.Length;
				groupRecord.offsets = new UInt32[(int)groupRecord.count];

				//Count the offsets.
				o+=(UInt32)(4*groupRecord.count);


				//New Offset.
				player2Offset = o;

				//Add length.
				o+=(UInt32) 4;

				//Fix the length.
				player2Record.count = (UInt32)strings.player2Strings.Length;
				player2Record.offsets = new UInt32[(int)player2Record.count];

				//Count the offsets.
				o+=(UInt32)(4*player2Record.count);


				//New Offset.
				strmOffset = o;

				//Add length.
				o+=(UInt32) 4;

				//Fix the length.
				strmRecord.count = (UInt32)strings.strmStrings.Length;
				strmRecord.offsets = new UInt32[(int)strmRecord.count];

				//Count the offsets.
				o+=(UInt32)(4*strmRecord.count);


				//Get sseq record offsets.
				for (int i = 0; i < sseqRecord.count; i++) {

					sseqRecord.offsets [i] = o;

					if (!strings.sseqStrings [i].isPlaceHolder) {
						o += (UInt32)(strings.sseqStrings [i].name.ToCharArray().Length + 1);
					}

				}

				//Get seqArc offsets.
				for (int i = 0; i < seqArcRecord.count; i++) {

					seqArcRecord.subNames [i].seqArcNameOffset = o;

					if (!strings.seqArcStrings [i].isPlaceHolder) {
						o += (UInt32)(strings.seqArcStrings [i].name.ToCharArray ().Length + 1);
					}

						for (int j = 0; j < strings.seqArcSubStrings[i].Length; j++) {

							seqArcSubRecord [i].offsets [j] = o;

							if (!strings.seqArcSubStrings [i] [j].isPlaceHolder) {
								o += (UInt32)(strings.seqArcSubStrings [i] [j].name.ToCharArray ().Length + 1);
							}

						}

				}


				//Get bank record offsets.
				for (int i = 0; i < bankRecord.count; i++) {

					bankRecord.offsets [i] = o;

					if (!strings.bankStrings [i].isPlaceHolder) {
						o += (UInt32)(strings.bankStrings [i].name.ToCharArray().Length + 1);
					}

				}

				//Get wave record offsets.
				for (int i = 0; i < waveRecord.count; i++) {

					waveRecord.offsets [i] = o;

					if (!strings.waveStrings [i].isPlaceHolder) {
						o += (UInt32)(strings.waveStrings [i].name.ToCharArray().Length + 1);
					}

				}

				//Get player record offsets.
				for (int i = 0; i < playerRecord.count; i++) {

					playerRecord.offsets [i] = o;

					if (!strings.playerStrings [i].isPlaceHolder) {
						o += (UInt32)(strings.playerStrings [i].name.ToCharArray().Length + 1);
					}

				}

				//Get group record offsets.
				for (int i = 0; i < groupRecord.count; i++) {

					groupRecord.offsets [i] = o;

					if (!strings.groupStrings [i].isPlaceHolder) {
						o += (UInt32)(strings.groupStrings [i].name.ToCharArray().Length + 1);
					}

				}

				//Get player2 record offsets.
				for (int i = 0; i < player2Record.count; i++) {

					player2Record.offsets [i] = o;

					if (!strings.player2Strings [i].isPlaceHolder) {
						o += (UInt32)(strings.player2Strings [i].name.ToCharArray().Length + 1);
					}

				}

				//Get strm record offsets.
				for (int i = 0; i < strmRecord.count; i++) {

					strmRecord.offsets [i] = o;

					if (!strings.strmStrings [i].isPlaceHolder) {
						o += (UInt32)(strings.strmStrings [i].name.ToCharArray().Length + 1);
					}

				}

				//Filesize is here.
				fileSize = o;


            }

        }

        /// <summary>
        /// Standard symb name.
        /// </summary>
        public struct standardSymbName {

			public UInt32 count; //Amount of entries.
			public UInt32[] offsets; //Offsets to symb strings.

        }


        /// <summary>
        /// SeqArcNames.
        /// </summary>
        public struct seqArcName {

			public UInt32 count; //Number of seqArcSubNames.
			public seqArcSubName[] subNames; //Sub-Names.

        }

        /// <summary>
        /// Seq Arc Sub Name.
        /// </summary>
        public struct seqArcSubName {

			public UInt32 seqArcNameOffset; //Offset to seqArcName.
			public UInt32 seqArcSubOffset; //Offset to seqArc sub-record.

        }


        /// <summary>
        /// Symb strings.
        /// </summary>
        public struct symbStrings {

			public symbStringEntry[] sseqStrings; //Sseq Strings.
			public symbStringEntry[] seqArcStrings; //SeqArc header strings.
			public symbStringEntry[][] seqArcSubStrings; //SeqArcSubStrings
			public symbStringEntry[] bankStrings; //Bank Strings.
			public symbStringEntry[] waveStrings; //Wave Strings.
			public symbStringEntry[] playerStrings; //Player Strings.
			public symbStringEntry[] groupStrings; //Group Strings.
			public symbStringEntry[] player2Strings; //Player2 Strings.
			public symbStringEntry[] strmStrings; //Strm Strings.

        }


        /// <summary>
        /// Symb string entry.
        /// </summary>
        public struct symbStringEntry {

			public string name; //Name of string.
			public byte seperator; //Seperator byte.
			public bool isPlaceHolder; //If placeholder.

        }

        #endregion


        //Info stuff.
        #region infoStructures
        
        /// <summary>
        /// Info structures.
        /// </summary>
        public struct infoFile {

			public char[] magic; //INFO magic.
			public UInt32 fileSize; //File Size.
			public UInt32 sseqOffset; //Sseq Offset.
			public UInt32 seqArcOffset; //SeqArc Offset.
			public UInt32 bankOffset; //Bank Offset.
			public UInt32 waveOffset; //Wave Offset.
			public UInt32 playerOffset; //Player Offset.
			public UInt32 groupOffset; //Group Offset.
			public UInt32 player2Offset; //Player2 Offset.
			public UInt32 strmOffset; //Stream Offset.
			public byte[] reserved; //Zereos (24).

			public infoRecord sseqRecord; //Sseq Record.
			public sseqInfo[] sseqInfo; //Sseq Info.
			public infoRecord seqArcRecord; //SeqArc Record.
			public seqArcInfo[] seqArcInfo; //SeqArc Info.
			public infoRecord bankRecord; //Bank Record.
			public bankInfo[] bankInfo; //Bank Info.
			public infoRecord waveRecord; //WaveRecord.
			public waveInfo[] waveInfo; //Wave Info.
			public infoRecord playerRecord; //Player Record.
			public playerInfo[] playerInfo; //Player Info.
			public infoRecord groupRecord; //Group Record.
			public groupInfo[] groupInfo; //Group Info.
			public infoRecord player2Record; //Player2 Record.
			public player2Info[] player2Info; //Player2 Info.
			public infoRecord strmRecord; //Strm Record.
			public strmInfo[] strmInfo; //Strm Info.



			/// <summary>
			/// Updates the offsets.
			/// </summary>
			public void updateOffsets () {

				//Offset.
				UInt32 o = 0;

				//Add header values.
				o+=(UInt32) (magic.Length + 4*9 + reserved.Length);

				//Sseq Offset is here.
				sseqOffset = o;

				//Add length of sseq.
				o+=(UInt32) 4;

				//Fix the length of the sseq.
				sseqRecord.count = (UInt32)sseqInfo.Length;
				sseqRecord.offsets = new UInt32[(int)sseqRecord.count];

				//Fix the sseq offsets.
				o+=(UInt32)(4*sseqRecord.count);
				for (int i = 0; i < sseqRecord.count; i++) {

					//Set offsets.
					sseqRecord.offsets [i] = o;

					//Increment offset.
					if (!sseqInfo[i].isPlaceHolder) {o+=(UInt32)12;}

				}


				//Next thing is here.
				seqArcOffset = o;

				//Add length.
				o+=(UInt32) 4;

				//Fix the length.
				seqArcRecord.count = (UInt32)seqArcInfo.Length;
				seqArcRecord.offsets = new UInt32[(int)seqArcRecord.count];

				//Fix the offsets.
				o+=(UInt32)(4*seqArcRecord.count);
				for (int i = 0; i < seqArcRecord.count; i++) {

					//Set offsets.
					seqArcRecord.offsets [i] = o;

					//Increment offset.
					if (!seqArcInfo[i].isPlaceHolder) {o+=(UInt32)4;}

				}


                //Next thing is here.
                bankOffset = o;

                //Add length.
                o += (UInt32)4;

                //Fix the length.
                bankRecord.count = (UInt32)bankInfo.Length;
                bankRecord.offsets = new UInt32[(int)bankRecord.count];

                //Fix the offsets.
                o += (UInt32)(4 * bankRecord.count);
                for (int i = 0; i < bankRecord.count; i++)
                {

                    //Set offsets.
                    bankRecord.offsets[i] = o;

                    //Increment offset.
                    if (!bankInfo[i].isPlaceHolder) { o += (UInt32)12; }

                }


                //Next thing is here.
                waveOffset = o;

                //Add length.
                o += (UInt32)4;

                //Fix the length.
                waveRecord.count = (UInt32)waveInfo.Length;
                waveRecord.offsets = new UInt32[(int)waveRecord.count];

                //Fix the offsets.
                o += (UInt32)(4 * waveRecord.count);
                for (int i = 0; i < waveRecord.count; i++)
                {

                    //Set offsets.
                    waveRecord.offsets[i] = o;

                    //Increment offset.
                    if (!waveInfo[i].isPlaceHolder) { o += (UInt32)4; }

                }


                //Next thing is here.
                playerOffset = o;

                //Add length.
                o += (UInt32)4;

                //Fix the length.
                playerRecord.count = (UInt32)playerInfo.Length;
                playerRecord.offsets = new UInt32[(int)playerRecord.count];

                //Fix the offsets.
                o += (UInt32)(4 * playerRecord.count);
                for (int i = 0; i < playerRecord.count; i++)
                {

                    //Set offsets.
                    playerRecord.offsets[i] = o;

                    //Increment offset.
                    if (!playerInfo[i].isPlaceHolder) { o += (UInt32)8; }

                }


                //Here comes the big one...
                groupOffset = o;

                //Add length.
                o += (UInt32)4;

                //Fix length
                groupRecord.count = (UInt32)groupInfo.Length;
                groupRecord.offsets = new UInt32[(int)groupRecord.count];

                //Fix the offsets.
                o += (UInt32)(4 * groupRecord.count);
                for (int i = 0; i < groupRecord.count; i++)
                {

                    //Set offsets.
                    groupRecord.offsets[i] = o;      


                    //Increment offset.
                    if (!groupInfo[i].isPlaceHolder) {

                        //Increment offset for subgroups.
                        foreach (groupSubInfo s in groupInfo[i].subInfo)
                        {
                            o += (UInt32)8;
                        }

                        o += (UInt32)4;
                    }

                }


                //Next thing is here.
                player2Offset = o;

                //Add length.
                o += (UInt32)4;

                //Fix the length.
                player2Record.count = (UInt32)player2Info.Length;
                player2Record.offsets = new UInt32[(int)player2Record.count];

                //Fix the offsets.
                o += (UInt32)(4 * player2Record.count);
                for (int i = 0; i < player2Record.count; i++)
                {

                    //Set offsets.
                    player2Record.offsets[i] = o;

                    //Increment offset.
                    if (!player2Info[i].isPlaceHolder) { o += (UInt32)24; }

                }


                //Next thing is here.
                strmOffset = o;

                //Add length.
                o += (UInt32)4;

                //Fix the length.
                strmRecord.count = (UInt32)strmInfo.Length;
                strmRecord.offsets = new UInt32[(int)strmRecord.count];

                //Fix the offsets.
                o += (UInt32)(4 * strmRecord.count);
                for (int i = 0; i < strmRecord.count; i++)
                {

                    //Set offsets.
                    strmRecord.offsets[i] = o;

                    //Increment offset.
                    if (!strmInfo[i].isPlaceHolder) { o += (UInt32)12; }

                }

				//Filesize is here.
				fileSize = o;


            }

        }

        /// <summary>
        /// Info record.
        /// </summary>
        public struct infoRecord {

			public UInt32 count; //Count
			public UInt32[] offsets; //Offsets.

        }


        /// <summary>
        /// Sseq Info.
        /// </summary>
        public struct sseqInfo {

			public UInt32 fileId; //File ID.
			public UInt16 bank; //Associated Bank.
			public byte volume; //Volume.
			public byte channelPriority; //Channel Pri.
			public byte playerPriority; //Player Pri.
			public byte playerNumber; //Player Number.
			public byte unknown1; //Unused. (Source: Nitro Soundmaker)
			public byte unknown2; //Also unused. (Source: Nitro Soundmaker)
			public bool isPlaceHolder; //Offset is 0 if it is.

        }

        /// <summary>
        /// Seq Arc Info.
        /// </summary>
        public struct seqArcInfo {

			public UInt32 fileId; //File ID.
			public bool isPlaceHolder; //Offset is 0 if it is.

        }

        /// <summary>
        /// Bank Info.
        /// </summary>
        public struct bankInfo {

			public UInt32 fileId; //File ID.
			public UInt16 wave0; //Wave 0.
			public UInt16 wave1; //Wave 1.
			public UInt16 wave2; //Wave 2.
			public UInt16 wave3; //Wave 3.
			public bool isPlaceHolder; //Offset is 0 if it is.

        }

        /// <summary>
        /// Wave Info.
        /// </summary>
        public struct waveInfo {

			public UInt32 fileId; //File ID.
			public bool isPlaceHolder; //Offset is 0 if it is.

        }

        /// <summary>
        /// Player Info.
        /// </summary>
        public struct playerInfo {

			public UInt16 seqMax; //SeqMax.
			public UInt16 channelFlag; //Channel Flag.
			public UInt32 heapSize; //Heap Size.
			public bool isPlaceHolder; //Offset is 0 if it is.

        }

        /// <summary>
        /// Group info.
        /// </summary>
        public struct groupInfo {

			public UInt32 count; //Count.
			public groupSubInfo[] subInfo; //Sub info.
			public bool isPlaceHolder; //Offset is 0 if it is.

        }

        /// <summary>
        /// Group sub info.
        /// </summary>
        public struct groupSubInfo {

			public byte type; //Type of info.
			public byte loadFlag; //Load Flag.
			public UInt16 padding; //Padding 0s.
			public UInt32 nEntry; //nEntry.

        }

        /// <summary>
        /// Player2 info.
        /// </summary>
        public struct player2Info {

			public byte count; //Count.
			public byte v0; //V0
			public byte v1; //V1
			public byte v2; //V2
			public byte v3; //V3
			public byte v4; //V4
			public byte v5; //V5
			public byte v6; //V6
			public byte v7; //V7
			public byte v8; //V8
			public byte v9; //V9
			public byte v10; //V10
			public byte v11; //V11
			public byte v12; //V12
			public byte v13; //V13
			public byte v14; //V14
			public byte v15; //V15
			public byte[] reserved; //7 bytes long.
			public bool isPlaceHolder; //Offset is 0 if it is.

        }

        /// <summary>
        /// Strm Info.
        /// </summary>
        public struct strmInfo {

			public UInt32 fileId; //File ID.
			public byte volume; //Vol.
			public byte priority; //Pri.
			public byte player; //Player number.
			public byte[] reserved; //Size of 5.
			public bool isPlaceHolder; //Offset is 0 if it is.

        }

        #endregion

    }






	#region infoAndSymbStructures



	/// <summary>
	/// Info data.
	/// </summary>
	public class infoData {

		public List<SseqData> sseqData;
		public List<SeqArcData> seqArcData;
		public List<BankData> bankData;
		public List<WaveData> waveData;
		public List<PlayerData> playerData;
		public List<GroupData> groupData;
		public List<Player2Data> player2Data;
		public List<StrmData> strmData;


		/// <summary>
		/// Load from InfoFile.
		/// </summary>
		public void load(NitroStructures.infoFile f) {

			this.sseqData = new List<SseqData> ();
			for (int i = 0; i < f.sseqInfo.Length; i++) {
				SseqData t = new SseqData ();
				t.fileId = f.sseqInfo [i].fileId;
				t.bank = f.sseqInfo [i].bank;
				t.volume = f.sseqInfo [i].volume;
				t.channelPriority = f.sseqInfo [i].channelPriority;
				t.playerPriority = f.sseqInfo [i].playerPriority;
				t.playerNumber = f.sseqInfo [i].playerNumber;
				t.unknown1 = f.sseqInfo [i].unknown1;
				t.unknown2 = f.sseqInfo [i].unknown2;
				t.isPlaceHolder = f.sseqInfo [i].isPlaceHolder;
				sseqData.Add (t);
			}

			this.seqArcData = new List<SeqArcData> ();
			for (int i = 0; i < f.seqArcInfo.Length; i++) {
				SeqArcData t = new SeqArcData ();
				t.fileId = f.seqArcInfo [i].fileId;
				t.isPlaceHolder = f.seqArcInfo [i].isPlaceHolder;
				seqArcData.Add (t);
			}

			this.bankData = new List<BankData> ();
			for (int i = 0; i < f.bankInfo.Length; i++) {
				BankData t = new BankData ();
				t.fileId = f.bankInfo [i].fileId;
				t.wave0 = f.bankInfo [i].wave0;
				t.wave1 = f.bankInfo [i].wave1;
				t.wave2 = f.bankInfo [i].wave2;
				t.wave3 = f.bankInfo [i].wave3;
				t.isPlaceHolder = f.bankInfo [i].isPlaceHolder;
				bankData.Add (t);
			}

			this.waveData = new List<WaveData> ();
			for (int i = 0; i < f.waveInfo.Length; i++) {
				WaveData t = new WaveData ();
				t.fileId = f.waveInfo [i].fileId;
				t.isPlaceHolder = f.waveInfo [i].isPlaceHolder;
				waveData.Add (t);
			}

			this.playerData = new List<PlayerData> ();
			for (int i = 0; i < f.playerInfo.Length; i++) {
				PlayerData t = new PlayerData ();
				t.seqMax = f.playerInfo [i].seqMax;
				t.channelFlag = f.playerInfo [i].channelFlag;
				t.heapSize = f.playerInfo [i].heapSize;
				t.isPlaceHolder = f.playerInfo [i].isPlaceHolder;
				playerData.Add (t);
			}

			this.groupData = new List<GroupData> ();
			for (int i = 0; i < f.groupInfo.Length; i++) {
				GroupData t = new GroupData ();
				t.count = f.groupInfo [i].count;
				t.subInfo = new List<GroupSubData> ();
				for (int j = 0; j < f.groupInfo [i].count; j++) {
					GroupSubData h = new GroupSubData ();
					h.type = f.groupInfo [i].subInfo [j].type;
					h.loadFlag = f.groupInfo [i].subInfo [j].loadFlag;
					h.padding = f.groupInfo [i].subInfo [j].padding;
					h.nEntry = f.groupInfo [i].subInfo [j].nEntry;
					t.subInfo.Add (h);
				}
				t.isPlaceHolder = f.groupInfo [i].isPlaceHolder;
				groupData.Add (t);
			}

			this.player2Data = new List<Player2Data> ();
			for (int i = 0; i < f.player2Info.Length; i++) {
				Player2Data t = new Player2Data ();
				t.count = f.player2Info [i].count;
				t.v0 = f.player2Info [i].v0;
				t.v1 = f.player2Info [i].v1;
				t.v2 = f.player2Info [i].v2;
				t.v3 = f.player2Info [i].v3;
				t.v4 = f.player2Info [i].v4;
				t.v5 = f.player2Info [i].v5;
				t.v6 = f.player2Info [i].v6;
				t.v7 = f.player2Info [i].v7;
				t.v8 = f.player2Info [i].v8;
				t.v9 = f.player2Info [i].v9;
				t.v10 = f.player2Info [i].v10;
				t.v11 = f.player2Info [i].v11;
				t.v12 = f.player2Info [i].v12;
				t.v13 = f.player2Info [i].v13;
				t.v14 = f.player2Info [i].v14;
				t.v15 = f.player2Info [i].v15;
				t.reserved = f.player2Info [i].reserved;
				t.isPlaceHolder = f.player2Info [i].isPlaceHolder;
				player2Data.Add (t);
			}

			this.strmData = new List<StrmData> ();
			for (int i = 0; i < f.strmInfo.Length; i++) {
				StrmData t = new StrmData ();
				t.fileId = f.strmInfo [i].fileId;
				t.volume = f.strmInfo [i].volume;
				t.priority = f.strmInfo [i].priority;
				t.player = f.strmInfo [i].player;
				t.reserved = f.strmInfo [i].reserved;
				t.isPlaceHolder = f.strmInfo [i].isPlaceHolder;
				strmData.Add (t);
			}
				
		}


		/// <summary>
		/// Convert to the info file.
		/// </summary>
		/// <returns>The info file.</returns>
		public NitroStructures.infoFile toInfoFile() {

			//New info file.
			NitroStructures.infoFile f = new NitroStructures.infoFile();

			//Set magic.
			char[] magic = {'I', 'N', 'F', 'O'};
			f.magic = magic;

			//Set f reserved.
			byte[] reserved = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
		    f.reserved = reserved;


			//Set the data.
			f.sseqInfo = new NitroStructures.sseqInfo[sseqData.Count()];
			for (int i = 0; i < f.sseqInfo.Length; i++) {
			
				f.sseqInfo [i].fileId = sseqData [i].fileId;
				f.sseqInfo [i].bank = sseqData [i].bank;
				f.sseqInfo [i].volume = sseqData [i].volume;
				f.sseqInfo [i].channelPriority = sseqData [i].channelPriority;
				f.sseqInfo [i].playerPriority = sseqData [i].playerPriority;
				f.sseqInfo [i].playerNumber = sseqData [i].playerNumber;
				f.sseqInfo [i].unknown1 = sseqData [i].unknown1;
				f.sseqInfo [i].unknown2 = sseqData [i].unknown2;
				f.sseqInfo [i].isPlaceHolder = sseqData [i].isPlaceHolder;
			
			}

			f.seqArcInfo = new NitroStructures.seqArcInfo[seqArcData.Count()];
			for (int i = 0; i < f.seqArcInfo.Length; i++) {

				f.seqArcInfo [i].fileId = seqArcData [i].fileId;
				f.seqArcInfo [i].isPlaceHolder = seqArcData [i].isPlaceHolder;

			}

			f.bankInfo = new NitroStructures.bankInfo[bankData.Count()];
			for (int i = 0; i < f.bankInfo.Length; i++) {

				f.bankInfo [i].fileId = bankData [i].fileId;
				f.bankInfo [i].wave0 = bankData [i].wave0;
				f.bankInfo [i].wave1 = bankData [i].wave1;
				f.bankInfo [i].wave2 = bankData [i].wave2;
				f.bankInfo [i].wave3 = bankData [i].wave3;
				f.bankInfo [i].isPlaceHolder = bankData [i].isPlaceHolder;

			}

			f.waveInfo = new NitroStructures.waveInfo[waveData.Count()];
			for (int i = 0; i < f.waveInfo.Length; i++) {

				f.waveInfo [i].fileId = waveData [i].fileId;
				f.waveInfo [i].isPlaceHolder = waveData [i].isPlaceHolder;

			}

			f.playerInfo = new NitroStructures.playerInfo[playerData.Count()];
			for (int i = 0; i < f.playerInfo.Length; i++) {

				f.playerInfo [i].seqMax = playerData [i].seqMax;
				f.playerInfo [i].channelFlag = playerData [i].channelFlag;
				f.playerInfo [i].heapSize = playerData [i].heapSize;
				f.playerInfo [i].isPlaceHolder = playerData [i].isPlaceHolder;

			}

			f.groupInfo = new NitroStructures.groupInfo[groupData.Count ()];
			for (int i = 0; i < f.groupInfo.Length; i++) {
			
				f.groupInfo [i].count = groupData [i].count;
				f.groupInfo [i].isPlaceHolder = groupData [i].isPlaceHolder;
				f.groupInfo[i].subInfo = new NitroStructures.groupSubInfo[(int)f.groupInfo[i].count];
				for (int j = 0; j < (int)f.groupInfo [i].count; j++) {
				
					f.groupInfo [i].subInfo [j].type = groupData [i].subInfo [j].type;
					f.groupInfo [i].subInfo [j].loadFlag = groupData [i].subInfo [j].loadFlag;
					f.groupInfo [i].subInfo [j].padding = groupData [i].subInfo [j].padding;
					f.groupInfo [i].subInfo [j].nEntry = groupData [i].subInfo [j].nEntry;
				
				}
			
			}

			f.player2Info = new NitroStructures.player2Info[player2Data.Count()];
			for (int i = 0; i < f.player2Info.Length; i++) {

				f.player2Info [i].count = player2Data [i].count;
				f.player2Info [i].v0 = player2Data [i].v0;
				f.player2Info [i].v1 = player2Data [i].v1;
				f.player2Info [i].v2 = player2Data [i].v2;
				f.player2Info [i].v3 = player2Data [i].v3;
				f.player2Info [i].v4 = player2Data [i].v4;
				f.player2Info [i].v5 = player2Data [i].v5;
				f.player2Info [i].v6 = player2Data [i].v6;
				f.player2Info [i].v7 = player2Data [i].v7;
				f.player2Info [i].v8 = player2Data [i].v8;
				f.player2Info [i].v9 = player2Data [i].v9;
				f.player2Info [i].v10 = player2Data [i].v10;
				f.player2Info [i].v11 = player2Data [i].v11;
				f.player2Info [i].v12 = player2Data [i].v12;
				f.player2Info [i].v13 = player2Data [i].v13;
				f.player2Info [i].v14 = player2Data [i].v14;
				f.player2Info [i].v15 = player2Data [i].v15;
				f.player2Info [i].reserved = player2Data [i].reserved;
				f.player2Info [i].isPlaceHolder = player2Data [i].isPlaceHolder;

			}

			f.strmInfo = new NitroStructures.strmInfo[strmData.Count()];
			for (int i = 0; i < f.strmInfo.Length; i++) {

				f.strmInfo [i].fileId = strmData [i].fileId;
				f.strmInfo [i].volume = strmData [i].volume;
				f.strmInfo [i].priority = strmData [i].priority;
				f.strmInfo [i].player = strmData [i].player;
				f.strmInfo [i].reserved = strmData [i].reserved;
				f.strmInfo [i].isPlaceHolder = strmData [i].isPlaceHolder;

			}


			//Update f info.
			f.updateOffsets();

			//Return f.
			return f;

		}

	}


	public class SseqData {

		public UInt32 fileId; //File ID.
		public UInt16 bank; //Associated Bank.
		public byte volume; //Volume.
		public byte channelPriority; //Channel Pri.
		public byte playerPriority; //Player Pri.
		public byte playerNumber; //Player Number.
		public byte unknown1; //Unused. (Source: Nitro Soundmaker)
		public byte unknown2; //Also unused. (Source: Nitro Soundmaker)
		public bool isPlaceHolder; //Offset is 0 if it is.

	}

	public class SeqArcData {

		public UInt32 fileId; //File ID.
		public bool isPlaceHolder; //Offset is 0 if it is.

	}

	public class BankData {

		public UInt32 fileId; //File ID.
		public UInt16 wave0; //Wave 0.
		public UInt16 wave1; //Wave 1.
		public UInt16 wave2; //Wave 2.
		public UInt16 wave3; //Wave 3.
		public bool isPlaceHolder; //Offset is 0 if it is.

	}

	public class WaveData {

		public UInt32 fileId; //File ID.
		public bool isPlaceHolder; //Offset is 0 if it is.

	}

	public class PlayerData {

		public UInt16 seqMax; //SeqMax.
		public UInt16 channelFlag; //Channel Flag.
		public UInt32 heapSize; //Heap Size.
		public bool isPlaceHolder; //Offset is 0 if it is.

	}

	public class GroupData {

		public UInt32 count; //Count.
		public List<GroupSubData> subInfo; //Sub info.
		public bool isPlaceHolder; //Offset is 0 if it is.

	}

	public class GroupSubData {

		public byte type; //Type of info.
		public byte loadFlag; //Load Flag.
		public UInt16 padding; //Padding 0s.
		public UInt32 nEntry; //nEntry.

	}

	public class Player2Data {

		public byte count; //Count.
		public byte v0; //V0
		public byte v1; //V1
		public byte v2; //V2
		public byte v3; //V3
		public byte v4; //V4
		public byte v5; //V5
		public byte v6; //V6
		public byte v7; //V7
		public byte v8; //V8
		public byte v9; //V9
		public byte v10; //V10
		public byte v11; //V11
		public byte v12; //V12
		public byte v13; //V13
		public byte v14; //V14
		public byte v15; //V15
		public byte[] reserved; //7 bytes long.
		public bool isPlaceHolder; //Offset is 0 if it is.

	}

	public class StrmData {

		public UInt32 fileId; //File ID.
		public byte volume; //Vol.
		public byte priority; //Pri.
		public byte player; //Player number.
		public byte[] reserved; //Size of 5.
		public bool isPlaceHolder; //Offset is 0 if it is.

	}









	/// <summary>
	/// Symb data.
	/// </summary>
	public class symbData {

		public List<symbStringName> sseqStrings; //Sseq Strings.
		public List<symbStringName> seqArcStrings; //SeqArc Strings.
		public List<List<symbStringName>> seqArcSubStrings; //SeqArcSubStrings.
		public List<symbStringName> bankStrings; //Bank Strings.
		public List<symbStringName> waveStrings; //Wave Strings.
		public List<symbStringName> playerStrings; //Player Strings.
		public List<symbStringName> groupStrings; //Group Strings.
		public List<symbStringName> player2Strings; //Player2 Strings.
		public List<symbStringName> strmStrings; //Strm Strings.


		public void load(NitroStructures.symbFile f) {
		
			//Load the strings.
			sseqStrings = new List<symbStringName>();
			foreach (NitroStructures.symbStringEntry s in f.strings.sseqStrings) {
			
				symbStringName t = new symbStringName ();
				t.name = s.name;
				t.isPlaceHolder = s.isPlaceHolder;
				sseqStrings.Add (t);
			
			}
			seqArcStrings = new List<symbStringName> ();
			foreach (NitroStructures.symbStringEntry s in f.strings.seqArcStrings) {

				symbStringName t = new symbStringName ();
				t.name = s.name;
				t.isPlaceHolder = s.isPlaceHolder;
				seqArcStrings.Add (t);

			}
			seqArcSubStrings = new List<List<symbStringName>> ();
			foreach (NitroStructures.symbStringEntry[] h in f.strings.seqArcSubStrings) {

				List<symbStringName> s = new List<symbStringName> ();
				foreach (NitroStructures.symbStringEntry w in h) {
					symbStringName t = new symbStringName ();
					t.name = w.name;
					t.isPlaceHolder = w.isPlaceHolder;
					s.Add (t);
				}

				seqArcSubStrings.Add (s);

			}
			bankStrings = new List<symbStringName> ();
			foreach (NitroStructures.symbStringEntry s in f.strings.bankStrings) {

				symbStringName t = new symbStringName ();
				t.name = s.name;
				t.isPlaceHolder = s.isPlaceHolder;
				bankStrings.Add (t);

			}
			waveStrings = new List<symbStringName> ();
			foreach (NitroStructures.symbStringEntry s in f.strings.waveStrings) {

				symbStringName t = new symbStringName ();
				t.name = s.name;
				t.isPlaceHolder = s.isPlaceHolder;
				waveStrings.Add (t);

			}
			playerStrings = new List<symbStringName> ();
			foreach (NitroStructures.symbStringEntry s in f.strings.playerStrings) {

				symbStringName t = new symbStringName ();
				t.name = s.name;
				t.isPlaceHolder = s.isPlaceHolder;
				playerStrings.Add (t);

			}
			groupStrings = new List<symbStringName> ();
			foreach (NitroStructures.symbStringEntry s in f.strings.groupStrings) {

				symbStringName t = new symbStringName ();
				t.name = s.name;
				t.isPlaceHolder = s.isPlaceHolder;
				groupStrings.Add (t);

			}
			player2Strings = new List<symbStringName> ();
			foreach (NitroStructures.symbStringEntry s in f.strings.player2Strings) {

				symbStringName t = new symbStringName ();
				t.name = s.name;
				t.isPlaceHolder = s.isPlaceHolder;
				player2Strings.Add (t);

			}
			strmStrings = new List<symbStringName> ();
			foreach (NitroStructures.symbStringEntry s in f.strings.strmStrings) {

				symbStringName t = new symbStringName ();
				t.name = s.name;
				t.isPlaceHolder = s.isPlaceHolder;
				strmStrings.Add (t);

			}
		
		}


		/// <summary>
		/// To the symb.
		/// </summary>
		/// <returns>The symb.</returns>
		public NitroStructures.symbFile toSymb() {

			//New file.
			NitroStructures.symbFile s = new NitroStructures.symbFile();

			//Set magic.
			char[] magic = {'S', 'Y', 'M', 'B'};
			s.magic = magic;

			//Set f reserved.
			byte[] reserved = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
			s.reserved = reserved;

			s.strings.sseqStrings = new NitroStructures.symbStringEntry[sseqStrings.Count()];
			for (int i = 0; i < sseqStrings.Count (); i++) {
			
				s.strings.sseqStrings [i].name = sseqStrings [i].name;
				s.strings.sseqStrings [i].isPlaceHolder = sseqStrings [i].isPlaceHolder;
				s.strings.sseqStrings [i].seperator = 0;
			
			}
			s.strings.seqArcStrings = new NitroStructures.symbStringEntry[seqArcStrings.Count ()];
			for (int i = 0; i < seqArcStrings.Count (); i++) {

				s.strings.seqArcStrings [i].name = seqArcStrings [i].name;
				s.strings.seqArcStrings [i].isPlaceHolder = seqArcStrings [i].isPlaceHolder;
				s.strings.seqArcStrings [i].seperator = 0;

			}
			s.strings.seqArcSubStrings = new NitroStructures.symbStringEntry[seqArcSubStrings.Count()][];
			for (int i = 0; i < seqArcSubStrings.Count; i++) {

				s.strings.seqArcSubStrings[i] = new NitroStructures.symbStringEntry[seqArcSubStrings[i].Count()];
				for (int j = 0; j < seqArcSubStrings [i].Count (); j++) {
				
					s.strings.seqArcSubStrings [i][j].name = seqArcSubStrings [i][j].name;
					s.strings.seqArcSubStrings [i][j].isPlaceHolder = seqArcSubStrings [i][j].isPlaceHolder;
					s.strings.seqArcSubStrings [i][j].seperator = 0;
				
				}
					
			}
			s.strings.bankStrings = new NitroStructures.symbStringEntry[bankStrings.Count()];
			for (int i = 0; i < bankStrings.Count (); i++) {

				s.strings.bankStrings [i].name = bankStrings [i].name;
				s.strings.bankStrings [i].isPlaceHolder = bankStrings [i].isPlaceHolder;
				s.strings.bankStrings [i].seperator = 0;

			}
			s.strings.waveStrings = new NitroStructures.symbStringEntry[waveStrings.Count()];
			for (int i = 0; i < waveStrings.Count (); i++) {

				s.strings.waveStrings [i].name = waveStrings [i].name;
				s.strings.waveStrings [i].isPlaceHolder = waveStrings [i].isPlaceHolder;
				s.strings.waveStrings [i].seperator = 0;

			}
			s.strings.playerStrings = new NitroStructures.symbStringEntry[playerStrings.Count()];
			for (int i = 0; i < playerStrings.Count (); i++) {

				s.strings.playerStrings [i].name = playerStrings [i].name;
				s.strings.playerStrings [i].isPlaceHolder = playerStrings [i].isPlaceHolder;
				s.strings.playerStrings [i].seperator = 0;

			}
			s.strings.groupStrings = new NitroStructures.symbStringEntry[groupStrings.Count()];
			for (int i = 0; i < groupStrings.Count (); i++) {

				s.strings.groupStrings [i].name = groupStrings [i].name;
				s.strings.groupStrings [i].isPlaceHolder = groupStrings [i].isPlaceHolder;
				s.strings.groupStrings [i].seperator = 0;

			}
			s.strings.player2Strings = new NitroStructures.symbStringEntry[player2Strings.Count()];
			for (int i = 0; i < player2Strings.Count (); i++) {

				s.strings.player2Strings [i].name = player2Strings [i].name;
				s.strings.player2Strings [i].isPlaceHolder = player2Strings [i].isPlaceHolder;
				s.strings.player2Strings [i].seperator = 0;

			}
			s.strings.strmStrings = new NitroStructures.symbStringEntry[strmStrings.Count()];
			for (int i = 0; i < strmStrings.Count (); i++) {

				s.strings.strmStrings [i].name = strmStrings [i].name;
				s.strings.strmStrings [i].isPlaceHolder = strmStrings [i].isPlaceHolder;
				s.strings.strmStrings [i].seperator = 0;

			}




			//Update offsets.
			s.fixOffsets();

			//Return final.
			return s;

		}

	}

	/// <summary>
	/// Symb string entry.
	/// </summary>
	public class symbStringName {

		public string name; //Name of string.
		public bool isPlaceHolder; //If placeholder.

	}

	#endregion



	/// <summary>
	/// Seq arc.
	/// </summary>
	public class seqArc {

		public char[] magic; //SSAR magic.
		public UInt32 identifier; //Identifier - FFFE0001
		public UInt32 fileSize; //Filesize.
		public UInt16 headerSize; //Should be 16.
		public UInt16 nBlocks; //Mostly 1?
		public seqArcData[] data; // Data.

		/// <summary>
		/// Data of seqArc.
		/// </summary>
		public struct seqArcData {

			public char[] magic; //DATA.
			public UInt32 size; //Data size.
			public UInt32 sseqDataStart; //Offset of where raw sseq data stars.
			public UInt32 nCount; //Count of entries.
			public seqArcSseqInfo[] sseqInfo; //Info.
			public byte[][] rawSseq; //Sseq data.

		}

		/// <summary>
		/// Seq arc sseq info.
		/// </summary>
		public struct seqArcSseqInfo {

			public UInt32 nOffset;	//Relative offset of the archived SEQ file, absolute offset = nOffset + SSAR::nDataOffset
			public UInt16 bank; //Bank
			public byte volume; //Volume
			public byte cpr; //Channel Priority.
			public byte ppr; //Player Priority
			public byte ply; //Player
			public byte[] reserved; //[2] zeroes.

		}


		/// <summary>
		/// Load the specified bytes.
		/// </summary>
		/// <param name="b">The bytes..</param>
		public void load(byte[] b) {

			//Make reader.
			MemoryStream src = new MemoryStream(b);
			BinaryDataReader br = new BinaryDataReader (src);

			//Read the data.
			magic = br.ReadChars(4);
			identifier = br.ReadUInt32 ();
			fileSize = br.ReadUInt32 ();
			headerSize = br.ReadUInt16 ();
			nBlocks = br.ReadUInt16 ();

			//Read the blocks.
			data = new seqArcData[(int)nBlocks];
			for (int i = 0; i < (int)nBlocks; i++) {

				data [i].magic = br.ReadChars (4);
				data [i].size = br.ReadUInt32 ();
				data [i].sseqDataStart = br.ReadUInt32 ();
				data [i].nCount = br.ReadUInt32 ();

				//Read the sseq data.
				data[i].sseqInfo = new seqArcSseqInfo[(int)data[i].nCount];
				for (int j = 0; j < (int)data [i].nCount; j++) {
				
					data [i].sseqInfo [j].nOffset = br.ReadUInt32 ();
					data [i].sseqInfo [j].bank = br.ReadUInt16 ();
					data [i].sseqInfo [j].volume = br.ReadByte ();
					data [i].sseqInfo [j].cpr = br.ReadByte ();
					data [i].sseqInfo [j].ppr = br.ReadByte ();
					data [i].sseqInfo [j].ply = br.ReadByte ();
					data [i].sseqInfo [j].reserved = br.ReadBytes (2);
				
				}

				//Read the raw sseq data.
				int totalSseq = 0;
				data[i].rawSseq = new byte[(int)data[i].nCount][];
				for (int j = 0; j < (int)data [i].nCount; j++) {
				
					int length = 0;

					//Set position.
					br.Position = (int)(data[i].sseqInfo[j].nOffset + data[i].sseqDataStart);

					//Get length.
					if (j == (int)(data [i].nCount - 1)) {length = (int)(fileSize - (totalSseq + data[i].sseqInfo[j].nOffset + data[i].sseqDataStart)); } else {
						length = (int)(data[i].sseqInfo[j+1].nOffset - data[i].sseqInfo[j].nOffset);
					}

					//Read raw sseq.
					data[i].rawSseq[j] = br.ReadBytes(length);
					totalSseq += data[i].rawSseq[j].Length;
				
				}

			
			}

		}



	}




	/// <summary>
	/// Swav file.
	/// </summary>
	public class swarFile {

		public char[] magic; //SWAR.
		public UInt32 identifier; //0xFFFE0001
		public UInt32 fileSize; //File size.
		public UInt16 headerSize; //Usually 16.
		public UInt16 nBlocks; //Usually 1.
		public swarData[] data; //Data.

		public struct swarData {

			public char[] magic; //DATA.
			public UInt32 size; //Size of DATA.
			public UInt32[] reserved; //[8] for runtime.
			public UInt32 nCount; //Amount of songs.
			public UInt32[] offsets; //Data offsets.
			public byte[][] files; //Files.

		}


		/// <summary>
		/// Load a swar file.
		/// </summary>
		public void load(byte[] b) {

			//New reader.
			MemoryStream src = new MemoryStream(b);
			BinaryDataReader br = new BinaryDataReader(src);

			//Read data.
			magic = br.ReadChars(4);
			identifier = br.ReadUInt32 ();
			fileSize = br.ReadUInt32 ();
			headerSize = br.ReadUInt16 ();
			nBlocks = br.ReadUInt16 ();

			//Read each block.
			data = new swarData[(int)nBlocks];
			for (int i = 0; i < (int)nBlocks; i++) {
			
				data [i].magic = br.ReadChars (4);
				data [i].size = br.ReadUInt32 ();
				data [i].reserved = br.ReadUInt32s (8);
				data [i].nCount = br.ReadUInt32 ();
				data [i].offsets = br.ReadUInt32s ((int)data [i].nCount);

				//Get the files.
				data[i].files = new byte[(int)data[i].nCount][];
				for (int j = 0; j < (int)data [i].nCount; j++) {
				
					int length = 0;

					if (j == (int)(data [i].nCount - 1)) {
						length = (int)(data [i].size + 16 - data[i].offsets[j]);
					} else {
						length = (int)(data [i].offsets [j + 1] - data [i].offsets [j]);
					}

					br.Position = (int)data [i].offsets [j];
					data [i].files [j] = br.ReadBytes (length);
				
				}
			
			}

			fixSwavFiles ();

		}


		/// <summary>
		/// Fixes the swav files.
		/// </summary>
		public void fixSwavFiles() {

			foreach (swarData s in data) {
			
				for (int i = 0; i < s.files.Length; i++) {
				
					//Make new header byte.
					MemoryStream o = new MemoryStream();
					BinaryWriter bw = new BinaryWriter (o);

					//Write lame info.
					bw.Write("SWAV".ToCharArray());
					bw.Write ((UInt32)0x0100feff);
					bw.Write ((UInt32)(s.files[i].Length + 24));
					bw.Write ((UInt16)16);
					bw.Write ((UInt16)1);
					bw.Write ("DATA".ToArray());
					bw.Write ((UInt32)(s.files[i].Length + 8));
					byte[][] bs = { o.ToArray(), s.files[i]};
					s.files[i] = Combine (bs);
				
				}
			
			}

		}


		/// <summary>
		/// Unfixes the swav files.
		/// </summary>
		public void unfixSwavFiles() {
		
			foreach (swarData s in data) {

				for (int i = 0; i < s.files.Length; i++) {

					//Get the byte.
					List<byte> newBytes = new List<byte>();
					for (int j = 24; j < s.files [i].Length; j++) {
					
						newBytes.Add (s.files[i][j]);
					
					}
					s.files [i] = newBytes.ToArray ();

				}

			}
		
		}


		/// <summary>
		/// Extract the specified path.
		/// </summary>
		/// <param name="path">Path.</param>
		public void extract(string path) {
		
			//Make dir if not existing.
			Directory.CreateDirectory(path);

			//Write each file.
			for (int i = 0; i < data.Length; i++) {

				Directory.CreateDirectory (path + "/Entry_" + i.ToString("D3"));
				for (int j = 0; j < data [i].files.Length; j++) {
				
					File.WriteAllBytes(path + "/Entry_" + i.ToString("D3") + "/Sound_" + j.ToString("D4") + ".swav", data[i].files[j]);
				
				}

			}
		
		}


		/// <summary>
		/// Compress the specified path into a SWAR.
		/// </summary>
		/// <param name="path">Path.</param>
		public void compress(string path) {
		
			string[] directoryNames = Directory.EnumerateDirectories (path).ToArray();

			data = new swarData[directoryNames.Length];
			for (int i = 0; i < directoryNames.Length; i++) {
			
				string[] fileNames = Directory.EnumerateFiles (directoryNames [i]).ToArray();
				List<byte[]> files = new List<byte[]> ();
				for (int j = 0; j < fileNames.Length; j++) {
				
					files.Add (File.ReadAllBytes (fileNames[j]));
				
				}
				data [i].files = files.ToArray ();
			
			}
				
			fixOffsets ();
		
		}


		/// <summary>
		/// Fixes the offsets.
		/// </summary>
		public void fixOffsets() {

			//General info.
			magic = "SWAR".ToCharArray();
			identifier = (UInt32)0x0100feff;
		
			//Fix data.
			nBlocks = (UInt16)data.Length;
			for (int s = 0; s < data.Length; s++) {
			
				UInt32[] reserved = { (UInt32)0x0000, (UInt32)0x0000, (UInt32)0x0000, (UInt32)0x0000, (UInt32)0x0000, (UInt32)0x0000, (UInt32)0x0000, (UInt32)0x0000};
				data[s].reserved = reserved;
				data[s].magic = "DATA".ToCharArray ();
				data[s].nCount = (UInt32)data[s].files.Length;
				data[s].offsets = new UInt32[(int)data[s].nCount];
			
			}

			//Get the offsets.
			UInt32 offset = 16;
			headerSize = (UInt16)offset;
			for (int s = 0; s < data.Length; s++) {
				
				UInt32 dataSize = 0;

				//Basic header stuff.
				offset += (UInt32)8;
				dataSize += (UInt32)8;

				//Reserved.
				offset+= (UInt32)(4*data[s].reserved.Length);
				dataSize+= (UInt32)(4*data[s].reserved.Length);

				//Add the offsets.
				offset += (UInt32)(data[s].offsets.Length*4 + 4);
				dataSize += (UInt32)(data[s].offsets.Length*4 + 4);

				//Set the data offsets.
				for (int i = 0; i < data[s].offsets.Length; i++) {

					data[s].offsets [i] = offset;

					offset += (UInt32)data[s].files [i].Length;
					dataSize += (UInt32)data[s].files [i].Length;

				}


				data[s].size = dataSize;
			
			}
			fileSize = offset;
		
		}


		/// <summary>
		/// Convert to byte array.
		/// </summary>
		public byte[] toBytes() {

			//Fix offsets.
			fixOffsets();

			//Unfix Swavs.
			unfixSwavFiles();

			//Make writer.
			MemoryStream o = new MemoryStream ();
			BinaryWriter bw = new BinaryWriter (o);

			//General info.
			bw.Write (magic);
			bw.Write (identifier);
			bw.Write (fileSize);
			bw.Write (headerSize);
			bw.Write (nBlocks);

			//Write the blocks.
			foreach (swarData d in data) {

				bw.Write (d.magic);
				bw.Write (d.size);
				foreach (UInt32 r in d.reserved) {
					bw.Write (r);
				}
				bw.Write (d.nCount);
				foreach (UInt32 offset in d.offsets) {
					bw.Write (offset);
				}
				foreach (byte[] file in d.files) {
					bw.Write (file);
				}

			}

			//Fix swavs.
			fixSwavFiles();

			//Export output.
			return o.ToArray();

		}


		/// <summary>
		/// Combine the specified arrays.
		/// </summary>
		/// <param name="arrays">Arrays.</param>
		private byte[] Combine(params byte[][] arrays)
		{
			byte[] rv = new byte[arrays.Sum(a => a.Length)];
			int offset = 0;
			foreach (byte[] array in arrays) {
				System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
				offset += array.Length;
			}
			return rv;
		}

	}





	/// <summary>
	/// Bank file.
	/// </summary>
	public class sbnkFile {

		public char[] magic; //SBNK.
		public UInt32 identifier; //Identifier.
		public UInt32 fileSize; //File size.
		public UInt16 headerSize; //Header size.
		public UInt16 nBlocks; //Amount of nBlocks.
		public sbnkData[] data; //Data.

		public struct sbnkData {

			public char[] magic; //DATA.
			public UInt32 size; //Size of data.
			public UInt32[] reserved; //[8] reserved.
			public UInt32 nCount; //nCount.
			public sbnkInstrumentRecord[] records; //Records.

		}

		public struct sbnkInstrumentRecord {

			public byte fRecord; //Type???
			public UInt16 instrumentOffset; //Offset.
			public byte reserved; //Reserved.
			public bool isPlaceholder; //Is placeholder.

		}

		public struct sbnkInstrumentLessThan16
		{
			public UInt16 swavNumber; //Swav number.
			public UInt16 swarNumber; //Swar number, relative to info block.
			public byte noteNumber; //Note number.
			public byte attackRate; //Attack rate.
			public byte decayRate; //Decay rate.
			public byte sustainLevel; //Sustain rate.
			public byte releaseRate; //Release rate.
			public byte pan; //Pan.

		}


		/// <summary>
		/// Load the file.
		/// </summary>
		public void load(byte[] b) {

			//New reader.
			MemoryStream src = new MemoryStream(b);
			BinaryDataReader br = new BinaryDataReader (src);

			//Read basic stuff.
			magic = br.ReadChars(4);
			identifier = br.ReadUInt32 ();
			fileSize = br.ReadUInt32 ();
			headerSize = br.ReadUInt16 ();
			nBlocks = br.ReadUInt16 ();
			data = new sbnkData[(int)nBlocks];

			//Read the data.
			for (int i = 0; i < data.Length; i++) {

				data [i].magic = br.ReadChars (4);
				data [i].size = br.ReadUInt32 ();
				data [i].reserved = br.ReadUInt32s (8);
				data [i].nCount = br.ReadUInt32 ();

				//Read the records.
				data[i].records = new sbnkInstrumentRecord[(int)data[i].nCount];
				for (int j = 0; j < data [i].records.Length; j++) {
				
					data [i].records [j].fRecord = br.ReadByte ();
					data [i].records [j].instrumentOffset = br.ReadUInt16 ();
					data [i].records [j].reserved = br.ReadByte ();
					data [i].records [j].isPlaceholder = false;

					if (data [i].records [j].instrumentOffset == (UInt16)0) {
						data [i].records [j].isPlaceholder = true;
					}
				
				}

			}

		}

	}




	/// <summary>
	/// Sdat file.
	/// </summary>
	public class sdatFile {

		//Header
		public char[] magic; //SDAT
		public UInt32 identifier; //FFFE0001
		public UInt32 fileSize; //File size
		public UInt16 headerSize; //Size of header.
		public UInt16 nBlock; //Amount of blocks.

		//Offsets
		public UInt32 symbOffset; //Symb offset.
		public UInt32 symbSize; //Size of symb.
		public UInt32 infoOffset; //Info offset.
		public UInt32 infoSize; //Size of info.
		public UInt32 fatOffset; //Offset of fat block.
		public UInt32 fatSize; //Fat size.
		public UInt32 filesOffset; //Files offset.
		public UInt32 filesSize; //Files size.
		public byte[] reserved; //[16] reserved.

		//Blocks
		public symbData symbFile; //Symb file.
		public infoData infoFile; //Info file.
		public fatBlock fat; //Fat.
		public fileBlock files; //Files.


		/// <summary>
		/// Fat block.
		/// </summary>
		public struct fatBlock {

			public char[] magic; //'FAT '
			public UInt32 size; //Size of FAT.
			public UInt32 nCount; //Amount of records.
			public List<fatRecords> records; //Records.

		}


		/// <summary>
		/// Fat records.
		/// </summary>
		public struct fatRecords {

			public UInt32 offset; //Offset.
			public UInt32 nSize; //Size of file.
			public UInt32[] reserved; //[2] Reserved.

		}


		/// <summary>
		/// File block.
		/// </summary>
		public struct fileBlock {

			public char[] magic; //FILE
			public UInt32 nSize; //Size of this block.
			public UInt32 nCount; //Amount of sound files.
			public UInt32 reserved; //Reserved.
			public List<byte[]> files; //Plain files.
			public List<byte[]> sseqFiles; //Files.
			public List<byte[]> seqArcFiles; //Files.
			public List<byte[]> bankFiles; //Files.
			public List<byte[]> waveFiles; //Files.
			public List<byte[]> strmFiles; //Files.

		}



		public void load(byte[] b) {

			//Make reader.
			MemoryStream src = new MemoryStream(b);
			BinaryDataReader br = new BinaryDataReader(src);

			//Start reading.
			magic = br.ReadChars(4);
			identifier = br.ReadUInt32 ();
			fileSize = br.ReadUInt32 ();
			headerSize = br.ReadUInt16 ();
			nBlock = br.ReadUInt16 ();

			//Read offsets and sizes.
			symbOffset = br.ReadUInt32();
			symbSize = br.ReadUInt32 ();
			infoOffset = br.ReadUInt32 ();
			infoSize = br.ReadUInt32 ();
			fatOffset = br.ReadUInt32 ();
			fatSize = br.ReadUInt32 ();
			filesOffset = br.ReadUInt32 ();
			filesSize = br.ReadUInt32 ();
			reserved = br.ReadBytes (16);

			//Get info file.
			br.Position = (int)infoOffset;
			NitroStructures.infoFile info = NitroFileLoader.loadInfoFile (br.ReadBytes ((int)infoSize));
			infoData infoD = new infoData ();
			infoD.load (info);
			infoFile = infoD;

			//Get symb file.
			br.Position = (int)symbOffset;
			if (symbOffset != (UInt32)0) {
				NitroStructures.symbFile symb = NitroFileLoader.loadSymbFile (br.ReadBytes ((int)symbSize));
				symbData symbD = new symbData ();
				symbD.load (symb);
				symbFile = symbD;
			} else {
			
				//Make new file if blank.
				symbFile = new symbData();
				symbFile.sseqStrings = new List<symbStringName> ();
				foreach (SseqData s in infoFile.sseqData) {
					symbStringName t = new symbStringName ();
					t.name = "NO_NAME";
					t.isPlaceHolder = s.isPlaceHolder;
					symbFile.sseqStrings.Add (t);
				}
				symbFile.seqArcStrings = new List<symbStringName> ();
				symbFile.seqArcSubStrings = new List<List<symbStringName>> ();
				foreach (SeqArcData s in infoFile.seqArcData) {
					symbStringName t = new symbStringName ();
					t.name = "NO_NAME";
					t.isPlaceHolder = s.isPlaceHolder;
					symbFile.seqArcStrings.Add (t);

					//Seq Arc subs.
					List<symbStringName> p = new List<symbStringName> ();
					symbStringName q = new symbStringName ();
					q.name = "UNKNOWN_FILES";
					q.isPlaceHolder = false;
					p.Add (q);
					symbFile.seqArcSubStrings.Add (p);

				}
				symbFile.bankStrings = new List<symbStringName> ();
				foreach (BankData s in infoFile.bankData) {
					symbStringName t = new symbStringName ();
					t.name = "NO_NAME";
					t.isPlaceHolder = s.isPlaceHolder;
					symbFile.bankStrings.Add (t);
				}
				symbFile.waveStrings = new List<symbStringName> ();
				foreach (WaveData s in infoFile.waveData) {
					symbStringName t = new symbStringName ();
					t.name = "NO_NAME";
					t.isPlaceHolder = s.isPlaceHolder;
					symbFile.waveStrings.Add (t);
				}
				symbFile.playerStrings = new List<symbStringName> ();
				foreach (PlayerData s in infoFile.playerData) {
					symbStringName t = new symbStringName ();
					t.name = "NO_NAME";
					t.isPlaceHolder = s.isPlaceHolder;
					symbFile.playerStrings.Add (t);
				}
				symbFile.groupStrings = new List<symbStringName> ();
				foreach (GroupData s in infoFile.groupData) {
					symbStringName t = new symbStringName ();
					t.name = "NO_NAME";
					t.isPlaceHolder = s.isPlaceHolder;
					symbFile.groupStrings.Add (t);
				}
				symbFile.player2Strings = new List<symbStringName> ();
				foreach (Player2Data s in infoFile.player2Data) {
					symbStringName t = new symbStringName ();
					t.name = "NO_NAME";
					t.isPlaceHolder = s.isPlaceHolder;
					symbFile.player2Strings.Add (t);
				}
				symbFile.strmStrings = new List<symbStringName> ();
				foreach (StrmData s in infoFile.strmData) {
					symbStringName t = new symbStringName ();
					t.name = "NO_NAME";
					t.isPlaceHolder = s.isPlaceHolder;
					symbFile.strmStrings.Add (t);
				}
			
			}


			//Get FAT.
			br.Position = (int)fatOffset;
			fat.magic = br.ReadChars (4);
			fat.size = br.ReadUInt32 ();
			fat.nCount = br.ReadUInt32 ();

			//Get records.
			fat.records = new List<fatRecords>();
			for (int i = 0; i < (int)fat.nCount; i++) {

				fatRecords f = new fatRecords ();
				f.offset= br.ReadUInt32 ();
				f.nSize= br.ReadUInt32 ();
				f.reserved = br.ReadUInt32s (2);
				fat.records.Add (f);

			}



			//Get files.
			br.Position = (int)filesOffset;
			files.magic = br.ReadChars (4);
			files.nSize = br.ReadUInt32 ();
			files.nCount = br.ReadUInt32 ();
			files.reserved = br.ReadUInt32 ();

			files.files = new List<byte[]>();
			files.sseqFiles = new List<byte[]> ();
			files.seqArcFiles = new List<byte[]> ();
			files.bankFiles = new List<byte[]> ();
			files.waveFiles = new List<byte[]> ();
			files.strmFiles = new List<byte[]> ();
			foreach (fatRecords f in fat.records) {
			
				br.Position = f.offset;
				files.files.Add(br.ReadBytes((int)f.nSize));
			
			}

			//Now sort the files.
			foreach (byte[] f in files.files) {

				//Make new reader.
				MemoryStream src2 = new MemoryStream(f);
				BinaryDataReader br2 = new BinaryDataReader (src2);

				//See what file it is.
				UInt32 magic2 = br2.ReadUInt32();

				if (magic2 == (UInt32)0x51455353) {files.sseqFiles.Add (f);}
				if (magic2 == (UInt32)0x52415353) {files.seqArcFiles.Add (f);}
				if (magic2 == (UInt32)0x4b4e4253) {files.bankFiles.Add (f);}
				if (magic2 == (UInt32)0x52415753) {files.waveFiles.Add (f);}
				if (magic2 == (UInt32)0x4d525453) {files.strmFiles.Add (f);}

			}

		}


		//Extract the sdat.
		public void extract(string path) {

			//Make needed directories.
			if (files.sseqFiles.Count > 1) {Directory.CreateDirectory(path+"/Sequence");}
			if (files.seqArcFiles.Count > 1) {Directory.CreateDirectory(path+"/Sequence Archive");}
			if (files.bankFiles.Count > 1) {Directory.CreateDirectory(path+"/Bank");}
			if (files.waveFiles.Count > 1) {Directory.CreateDirectory(path+"/Wave Archive");}
			if (files.strmFiles.Count > 1) {Directory.CreateDirectory(path+"/Stream");}

			//Extract info and symb bins.
			File.WriteAllBytes(path+"/symb.bin", NitroFileLoader.symbToBytes(symbFile.toSymb()));
			File.WriteAllBytes(path+"/info.bin", NitroFileLoader.infoToBytes(infoFile.toInfoFile()));

			//Extract sseqs.
			for (int i = 0; i < files.sseqFiles.Count(); i++) {
				//Get correct info.
				for (int j = 0; j < infoFile.sseqData.Count(); j++) {
					if ((int)infoFile.sseqData [j].fileId == i) {		
						File.WriteAllBytes (path + "/Sequence/" + i.ToString ("D3") + symbFile.sseqStrings [j].name + ".sseq", files.sseqFiles[i]);
						break;
					}
				}
			}

			//Extract seqArcs.
			for (int i = 0; i < files.seqArcFiles.Count(); i++) {
				//Get correct info.
				for (int j = 0; j < infoFile.seqArcData.Count(); j++) {
					if ((int)infoFile.seqArcData [j].fileId == i + files.sseqFiles.Count()) {		
						File.WriteAllBytes (path + "/Sequence Archive/" + i.ToString ("D3") + symbFile.seqArcStrings [j].name + ".ssar", files.seqArcFiles[i]);
						break;
					}
				}
			}

			//Extract bank.
			for (int i = 0; i < files.bankFiles.Count(); i++) {
				//Get correct info.
				for (int j = 0; j < infoFile.bankData.Count(); j++) {
					if ((int)infoFile.bankData [j].fileId == i + files.sseqFiles.Count() + files.seqArcFiles.Count()) {		
						File.WriteAllBytes (path + "/Bank/" + i.ToString ("D3") + symbFile.bankStrings [j].name + ".sbnk", files.bankFiles[i]);
						break;
					}
				}
			}

			//Extract wave.
			for (int i = 0; i < files.waveFiles.Count(); i++) {
				//Get correct info.
				for (int j = 0; j < infoFile.waveData.Count(); j++) {
					if ((int)infoFile.waveData [j].fileId == i + files.sseqFiles.Count() + files.seqArcFiles.Count() + files.bankFiles.Count()) {		
						File.WriteAllBytes (path + "/Wave Archive/" + i.ToString ("D3") + symbFile.waveStrings [j].name + ".swar", files.waveFiles[i]);
						break;
					}
				}
			}


			//Extract strm.
			for (int i = 0; i < files.strmFiles.Count(); i++) {
				//Get correct info.
				for (int j = 0; j < infoFile.strmData.Count(); j++) {
					if ((int)infoFile.strmData [j].fileId == i + files.sseqFiles.Count() + files.seqArcFiles.Count() + files.bankFiles.Count() + files.waveFiles.Count()) {		
						File.WriteAllBytes (path + "/Stream/" + i.ToString ("D3") + symbFile.strmStrings [j].name + ".strm", files.strmFiles[i]);
						break;
					}
				}
			}


		}



		/// <summary>
		/// Compress the specified path to an sdat.
		/// </summary>
		/// <param name="path">Path.</param>
		public void compress(string path) {
		
			//Load bins.
			symbFile = new symbData ();
			symbFile.load (NitroFileLoader.loadSymbFile (File.ReadAllBytes (path + "\\symb.bin")));
			infoFile = new infoData ();
			infoFile.load (NitroFileLoader.loadInfoFile (File.ReadAllBytes (path + "\\info.bin")));

			//Load sseqs.
			files.sseqFiles = new List<byte[]>();
			if (Directory.Exists (path + "/Sequence")) {
				string[] sseqNames = Directory.EnumerateFiles (path + "/Sequence").ToArray ();
				foreach (string s in sseqNames) {
					files.sseqFiles.Add (File.ReadAllBytes (s));
				}
			}
			//Load seqArcs.
			files.seqArcFiles = new List<byte[]>();
			if (Directory.Exists (path + "/Sequence Archive")) {
				string[] seqArcNames = Directory.EnumerateFiles (path + "/Sequence Archive").ToArray ();
				foreach (string s in seqArcNames) {
					files.seqArcFiles.Add (File.ReadAllBytes (s));
				}
			}
			//Load banks.
			files.bankFiles = new List<byte[]>();
			if (Directory.Exists (path + "/Bank")) {
				string[] bankNames = Directory.EnumerateFiles (path + "/Bank").ToArray ();
				foreach (string s in bankNames) {
					files.bankFiles.Add (File.ReadAllBytes (s));
				}
			}
			//Load waves.
			files.waveFiles = new List<byte[]>();
			if (Directory.Exists (path + "/Wave Archive")) {
				string[] waveNames = Directory.EnumerateFiles (path + "/Wave Archive").ToArray ();
				foreach (string s in waveNames) {
					files.waveFiles.Add (File.ReadAllBytes (s));
				}
			}
			//Load strms.
			files.strmFiles = new List<byte[]>();
			if (Directory.Exists (path + "/Stream")) {
				string[] strmNames = Directory.EnumerateFiles (path + "/Stream").ToArray ();
				foreach (string s in strmNames) {
					files.strmFiles.Add (File.ReadAllBytes (s));
				}
			}

			//Now add everything to files.
			files.files = new List<byte[]>();
			foreach (byte[] b in files.sseqFiles) {
				files.files.Add (b);
			}
			foreach (byte[] b in files.seqArcFiles) {
				files.files.Add (b);
			}
			foreach (byte[] b in files.bankFiles) {
				files.files.Add (b);
			}
			foreach (byte[] b in files.waveFiles) {
				files.files.Add (b);
			}
			foreach (byte[] b in files.strmFiles) {
				files.files.Add (b);
			}
		
		}


		/// <summary>
		/// Convert file to bytes.
		/// </summary>
		/// <returns>The bytes.</returns>
		public byte[] toBytes() {

			//Fix offsets.
			fixOffsets ();

			//Make new stream.
			MemoryStream o = new MemoryStream ();
			BinaryWriter bw = new BinaryWriter (o);

			//Write header.
			bw.Write (magic);
			bw.Write (identifier);
			bw.Write (fileSize);
			bw.Write (headerSize);
			bw.Write (nBlock);

			//Write offsets.
			bw.Write (symbOffset);
			bw.Write (symbSize);
			bw.Write (infoOffset);
			bw.Write (infoSize);
			bw.Write (fatOffset);
			bw.Write (fatSize);
			bw.Write (filesOffset);
			bw.Write (filesSize);
			bw.Write (reserved);

			//Write symb.
			bw.Write(NitroFileLoader.symbToBytes(symbFile.toSymb()));

			//Write info.
			bw.Write(NitroFileLoader.infoToBytes(infoFile.toInfoFile()));

			//Write FAT.
			bw.Write (fat.magic);
			bw.Write (fat.size);
			bw.Write (fat.nCount);
			foreach (fatRecords n in fat.records) {
			
				bw.Write (n.offset);
				bw.Write (n.nSize);
				bw.Write (n.reserved [0]);
				bw.Write (n.reserved [1]);
			
			}


			//Write files.
			bw.Write (files.magic);
			bw.Write (files.nSize);
			bw.Write (files.nCount);
			bw.Write (files.reserved);
			foreach (byte[] file in files.files) {
			
				bw.Write (file);

			}


			//Return o.
			return o.ToArray();

		}


		/// <summary>
		/// Fixes the offsets.
		/// </summary>
		public void fixOffsets() {

			//Generate header crap.
			magic = "SDAT".ToCharArray();
			identifier = (UInt32)0x0100feff;
			headerSize = (UInt16)64;
			nBlock = (UInt16)4;
			byte[] reserved2 = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
			reserved = reserved2;

			//Offset.
			UInt32 offset = headerSize;

			//Symb offset is here.
			symbOffset = offset;
			symbSize = (UInt32) NitroFileLoader.symbToBytes (symbFile.toSymb ()).Length;

			//Increment offset.
			offset+=symbSize;

			//Info offset is here.
			infoOffset = offset;
			infoSize = (UInt32) NitroFileLoader.infoToBytes (infoFile.toInfoFile ()).Length;

			//Increment offse.
			offset+=infoSize;

			//FAT is here.
			fatOffset = offset;

			//Fix files first.
			files.files = new List<byte[]>();
			foreach (byte[] b in files.sseqFiles) {
				files.files.Add (b);
			}
			foreach (byte[] b in files.seqArcFiles) {
				files.files.Add (b);
			}
			foreach (byte[] b in files.bankFiles) {
				files.files.Add (b);
			}
			foreach (byte[] b in files.waveFiles) {
				files.files.Add (b);
			}
			foreach (byte[] b in files.strmFiles) {
				files.files.Add (b);
			}

			//Fix FAT.
			fat.magic = "FAT ".ToArray();
			fat.nCount = (UInt32) files.files.Count ();
			fat.records = new List<fatRecords> ();

			//Get size of FAT.
			fatSize = (UInt32)(12 + (16*fat.nCount));
			fat.size = fatSize;
			offset += fatSize;

			//Files is here.
			filesOffset = offset;

			//Fix files.
			files.magic = "FILE".ToCharArray();
			files.reserved = (UInt32)0;
			files.nCount = (UInt32) files.files.Count ();

			//Now fix the individual FAT records.
			files.nSize = (UInt32) 16;
			filesSize = (UInt32)16;
			offset += (UInt32)16;
			for (int i = 0; i < files.files.Count (); i++) {
			
				fatRecords f = new fatRecords ();
				f.nSize = (UInt32) files.files [i].Length;
				f.offset = offset;
				UInt32[] reserved = { (UInt32)0, (UInt32)0 };
				f.reserved = reserved;
				fat.records.Add (f);

				offset += (UInt32) files.files [i].Length;
				filesSize += (UInt32) files.files [i].Length;
				files.nSize += (UInt32) files.files [i].Length;
			
			}


			//Filesize is here.
			fileSize = offset;



		}


	}

}
