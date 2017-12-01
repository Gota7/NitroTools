using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

}
