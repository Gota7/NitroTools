using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syroot.BinaryData;
using LibNitro.Hardware.Sound;

namespace SoundNStream
{
    /// <summary>
    /// SWAV file.
    /// </summary>
    public class swav
    {

        public char[] magic; //SWAV
        public UInt32 identifier; //Identifier. 0x0100feff
        public UInt32 fileSize; //File size.
        public UInt16 headerSize; //Header size, 0x10.
        public UInt16 nBlocks; //Number of blocks.

        public dataBlock data; //Data block.

        /// <summary>
        /// Info.
        /// </summary>
        public struct infoStuff
        {

            public byte waveType; // 0 = PCM8, 1 = PCM16, 2 = (IMA-)ADPCM
            public byte loopFlag; //Loop.
            public UInt16 nSampleRate; //Sampling Rate.
            public UInt16 nTime; //(ARM7_CLOCK / nSampleRate) [ARM7_CLOCK: 33.513982MHz / 2 = 1.6756991 E +7] 16756991 is value, divide by sample rate, round down.
            public UInt16 nloopOffset; //Loop offset expressed in 32 bits.
            public UInt32 nNonLoopLength; //Non loop length in 32 bits.

        }


        /// <summary>
        /// Data block.
        /// </summary>
        public struct dataBlock
        {

            public char[] magic; //DATA.
            public UInt32 size; //Size of block.

            public infoStuff info; //Info.

            public byte[] pcm8; //PCM8.
            public short[] pcm16; //PCM16.
            public byte[] imaAdpcm; //IMA-ADPCM.

        }



        /// <summary>
        /// Load the file.
        /// </summary>
        /// <param name="b"></param>
        public void load(byte[] b)
        {

            //Stream.
            MemoryStream src = new MemoryStream(b);
            BinaryDataReader br = new BinaryDataReader(src);
            br.ByteOrder = ByteOrder.LittleEndian;

            //Stuff.
            magic = br.ReadChars(4);
            identifier = br.ReadUInt32();
            fileSize = br.ReadUInt32();
            headerSize = br.ReadUInt16();
            nBlocks = br.ReadUInt16();

            data = new dataBlock();
            data.magic = br.ReadChars(4);
            data.size = br.ReadUInt32();
            data.info = new infoStuff();

            data.info.waveType = br.ReadByte();
            data.info.loopFlag = br.ReadByte();
            data.info.nSampleRate = br.ReadUInt16();
            data.info.nTime = br.ReadUInt16();
            data.info.nloopOffset = br.ReadUInt16();
            data.info.nNonLoopLength = br.ReadUInt32();

            switch (data.info.waveType)
            {

                case 0:
                    data.pcm8 = br.ReadBytes((int)data.size - 0x14);
                    break;

                case 1:
                    data.pcm16 = br.ReadInt16s(((int)data.size - 0x14) / 2);
                    break;

                case 2:
                    data.imaAdpcm = br.ReadBytes((int)data.size - 0x14);
                    break;

            }

        }



        /// <summary>
        /// Convert to RIFF.
        /// </summary>
        /// <returns></returns>
        public RIFF toRIFF()
        {

            RIFF r = new RIFF();

            r.data = new RIFF.dataBlock();
            r.fmt = new RIFF.fmtBlock();
            r.fmt.bitsPerSample = 8;
            r.fmt.sampleRate = (UInt32)data.info.nSampleRate;
            r.fmt.restOfData = new byte[0];
            r.fmt.chunkFormat = 1;
            r.fmt.numChannels = 1;

            switch (data.info.waveType)
            {

                case 0:
                    r.data.data = new byte[data.pcm8.Length];
                    for (int i = 0; i < r.data.data.Length; i++)
                    {
                        r.data.data[i] = (byte)((data.pcm8[i] + 0x80));
                    }
                    break;

                case 1:
                    r.fmt.bitsPerSample = 16;
                    MemoryStream o = new MemoryStream();
                    BinaryWriter bw = new BinaryWriter(o);
                    foreach (short s in data.pcm16)
                    {
                        bw.Write(s);
                    }
                    r.data.data = o.ToArray();
                    break;

                case 2:
                    MemoryStream o2 = new MemoryStream();
                    BinaryWriter bw2 = new BinaryWriter(o2);
                    r.fmt.bitsPerSample = 16;
                    List<short> std = new List<short>();
                    IMAADPCMDecoder d = new IMAADPCMDecoder(data.imaAdpcm, 0);
                    foreach (byte b in data.imaAdpcm)
                    {
                        try { std.Add(d.GetSample()); } catch { }
                        try { std.Add(d.GetSample()); } catch { }
                    }
                    foreach (short s in std)
                    {
                        bw2.Write(s);
                    }
                    r.data.data = o2.ToArray();
                    break;

            }

            //Make looping RIFF.
            if (data.info.loopFlag == 1)
            {

                r.smpl = new RIFF.smplBlock();

                switch (data.info.waveType)
                {

                    case 0:
                        r.smpl.loopStart = (UInt32)data.info.nloopOffset * 4;
                        r.smpl.loopEnd = (UInt32)r.data.data.Length;
                        break;

                    case 1:
                        r.smpl.loopStart = (UInt32)data.info.nloopOffset * 2;
                        r.smpl.loopEnd = (UInt32)r.data.data.Length / 2;
                        break;

                    case 2:
                        r.smpl.loopStart = (UInt32)data.info.nloopOffset * 8;
                        r.smpl.loopEnd = (UInt32)r.data.data.Length / 2;
                        break;

                }

            }

            r.update();

            return r;

        }


        /// <summary>
        /// Update the file.
        /// </summary>
        public void update()
        {

            magic = "SWAV".ToCharArray();
            identifier = 0x0100feff;
            fileSize = 0xFFFFFFFF;
            headerSize = 0x10;
            nBlocks = 1;

            data.magic = "DATA".ToCharArray();
            data.size = 0x14;
            switch (data.info.waveType)
            {

                case 0:
                    data.size += (UInt32)data.pcm8.Length;
                    break;

                case 1:
                    data.size += (UInt32)data.pcm16.Length * 2;
                    break;

                case 2:
                    data.size += (UInt32)data.imaAdpcm.Length;
                    break;

            }
            data.info.nTime = (UInt16)Math.Floor((decimal)16756991 / (decimal)data.info.nSampleRate);
            data.info.nNonLoopLength = (UInt32)(((data.size - 0x14) - data.info.nloopOffset * 4) / 4);
            if (data.info.loopFlag == 0) { data.info.nloopOffset = 1; }

            fileSize = data.size + 0x10;

        }


        /// <summary>
        /// Return this file as bytes.
        /// </summary>
        /// <returns></returns>
        public byte[] toBytes()
        {

            //Update.
            update();

            //Writer.
            MemoryStream o = new MemoryStream();
            BinaryDataWriter bw = new BinaryDataWriter(o);

            //Stuff.
            bw.Write(magic);
            bw.Write(identifier);
            bw.Write(fileSize);
            bw.Write(headerSize);
            bw.Write(nBlocks);

            bw.Write(data.magic);
            bw.Write(data.size);

            bw.Write(data.info.waveType);
            bw.Write(data.info.loopFlag);
            bw.Write(data.info.nSampleRate);
            bw.Write(data.info.nTime);
            bw.Write(data.info.nloopOffset);
            bw.Write(data.info.nNonLoopLength);

            switch (data.info.waveType)
            {

                case 0:
                    bw.Write(data.pcm8);
                    break;

                case 1:
                    bw.Write(data.pcm16);
                    break;

                case 2:
                    bw.Write(data.imaAdpcm);
                    break;

            }

            return o.ToArray();

        }

    }



    /// <summary>
    /// STRM stream.
    /// </summary>
    public class strm
    {

        public char[] magic; //STRM
        public UInt32 identifier; //Identifier. 0x0100feff
        public UInt32 fileSize; //File size.
        public UInt16 headerSize; //Header size, 0x10.
        public UInt16 nBlocks; //Number of blocks, 2.

        public headBlock head; //Head.
        public dataBlock data; //Data.

        /// <summary>
        /// Head Block.
        /// </summary>
        public struct headBlock
        {

            public char[] magic; //HEAD.
            public UInt32 size; //Size of block.

            public byte waveType;             // 0 = PCM8, 1 = PCM16, 2 = (IMA-)ADPCM
            public byte loop;                 // Loop flag = TRUE|FALSE
            public byte numChannel;           // Channels
            public byte unknown;              // Always 0
            public UInt16 nSampleRate;        // Sampling Rate (perhaps resampled from the original) 
            public UInt16 nTime;              // (1.0 / rate * ARM7_CLOCK / 32) [ARM7_CLOCK: 33.513982MHz / 2 = 1.6756991e7] 16756991
            public UInt32 nLoopOffset;        // Loop Offset (samples) 
            public UInt32 nSample;            // Number of Samples 
            public UInt32 nDataOffset;        // Data Offset (always 0x68)
            public UInt32 nBlock;             // Number of Blocks 
            public UInt32 nBlockLen;          // Block Length (Per Channel) 
            public UInt32 nBlockSample;       // Samples Per Block (Per Channel)
            public UInt32 nLastBlockLen;      // Last Block Length (Per Channel)
            public UInt32 nLastBlockSample;   // Samples Per Last Block (Per Channel)
            public byte[] reserved;           // 32 long with 0s.

        }


        /// <summary>
        /// Data block.
        /// </summary>
        public struct dataBlock
        {

            public char[] magic; //DATA.
            public UInt32 size; //Size of block.

            public byte[][] pcm8; //PCM8.
            public short[][] pcm16; //PCM16.
            public imaAdpcmBlock[][] imaAdpcm; //IMA-ADPCM.


            /// <summary>
            /// IMA-ADPCM block.
            /// </summary>
            public struct imaAdpcmBlock
            {

                public byte[] data; //Data.

            }

        }


        /// <summary>
        /// Load the file.
        /// </summary>
        /// <param name="b"></param>
        public void load(byte[] b)
        {

            //Reader.
            MemoryStream src = new MemoryStream(b);
            BinaryDataReader br = new BinaryDataReader(src);
            br.ByteOrder = ByteOrder.LittleEndian;

            //Read stuff.
            magic = br.ReadChars(4);
            identifier = br.ReadUInt32();
            fileSize = br.ReadUInt32();
            headerSize = br.ReadUInt16();
            nBlocks = br.ReadUInt16();
            head = new headBlock();
            data = new dataBlock();

            //Head block.
            head.magic = br.ReadChars(4);
            head.size = br.ReadUInt32();
            head.waveType = br.ReadByte();
            head.loop = br.ReadByte();
            head.numChannel = br.ReadByte();
            head.unknown = br.ReadByte();
            head.nSampleRate = br.ReadUInt16();
            head.nTime = br.ReadUInt16();
            head.nLoopOffset = br.ReadUInt32();
            head.nSample = br.ReadUInt32();
            head.nDataOffset = br.ReadUInt32();
            head.nBlock = br.ReadUInt32();
            head.nBlockLen = br.ReadUInt32();
            head.nBlockSample = br.ReadUInt32();
            head.nLastBlockLen = br.ReadUInt32();
            head.nLastBlockSample = br.ReadUInt32();
            head.reserved = br.ReadBytes(32);

            //Data block.
            data.magic = br.ReadChars(4);
            data.size = br.ReadUInt32();

            switch (head.waveType)
            {

                case 0:
                    data.pcm8 = new byte[head.numChannel][];
                    if (head.nBlock != 1) { throw new Exception("BAD PCM8 BLOCK!"); }
                    for (int i = 0; i < head.numChannel; i++)
                    {
                        data.pcm8[i] = br.ReadBytes((int)head.nLastBlockLen);
                    }
                    break;

                case 1:
                    data.pcm16 = new short[head.numChannel][];
                    if (head.nBlock != 1) { throw new Exception("BAD PCM16 BLOCK!"); }
                    for (int i = 0; i < head.numChannel; i++)
                    {
                        data.pcm16[i] = br.ReadInt16s((int)head.nLastBlockLen / 2);
                    }
                    break;

                //The big one!
                case 2:
                    data.imaAdpcm = new dataBlock.imaAdpcmBlock[head.numChannel][];
                    for (int i = 0; i < head.numChannel; i++)
                    {
                        data.imaAdpcm[i] = new dataBlock.imaAdpcmBlock[head.nBlock];
                    }
                    for (int i = 0; i < head.nBlock - 1; i++)
                    {
                        for (int j = 0; j < head.numChannel; j++)
                        {
                            data.imaAdpcm[j][i] = new dataBlock.imaAdpcmBlock();
                            //data.imaAdpcm[j][i].identifier = br.ReadUInt32();
                            data.imaAdpcm[j][i].data = br.ReadBytes((int)head.nBlockLen);
                        }
                    }
                    for (int j = 0; j < head.numChannel; j++)
                    {
                        data.imaAdpcm[j][head.nBlock - 1] = new dataBlock.imaAdpcmBlock();
                        //data.imaAdpcm[j][head.nBlock - 1].identifier = br.ReadUInt32();
                        data.imaAdpcm[j][head.nBlock - 1].data = br.ReadBytes((int)head.nLastBlockLen);
                    }
                    break;

            }

        }


        /// <summary>
        /// Convert file to RIFF.
        /// </summary>
        /// <returns></returns>
        public RIFF toRIFF()
        {

            RIFF r = new RIFF();
            r.fmt.bitsPerSample = 8;
            r.fmt.chunkFormat = 1;
            r.fmt.numChannels = head.numChannel;
            r.fmt.restOfData = new byte[0];
            r.fmt.sampleRate = head.nSampleRate;

            switch (head.waveType)
            {

                case 0:
                    r.data.pcm8 = new byte[r.fmt.numChannels][];
                    for (int i = 0; i < data.pcm8.Length; i++)
                    {
                        r.data.pcm8[i] = new byte[data.pcm8[i].Length];
                        for (int j = 0; j < data.pcm8[i].Length; j++)
                        {
                            r.data.pcm8[i][j] = (byte)(data.pcm8[i][j] + 0x80);
                        }
                    }
                    r.unpackPCMChannels();
                    break;

                case 1:
                    r.fmt.bitsPerSample = 16;
                    r.data.pcm16 = new short[r.fmt.numChannels][];
                    for (int i = 0; i < data.pcm16.Length; i++)
                    {
                        r.data.pcm16[i] = new short[data.pcm16[i].Length];
                        for (int j = 0; j < data.pcm16[i].Length; j++)
                        {
                            r.data.pcm16[i][j] = data.pcm16[i][j];
                        }
                    }
                    r.unpackPCMChannels();
                    break;

                case 2:
                    r.fmt.bitsPerSample = 16;
                    r.data.pcm16 = new short[r.fmt.numChannels][];

                    //Convert IMA-ADPCM to PCM16.
                    short[][] newSamples = new short[r.fmt.numChannels][];
                    for (int i = 0; i < r.fmt.numChannels; i++)
                    {

                        List<short> samples = new List<short>();
                        int blockCount = 0;
                        foreach (dataBlock.imaAdpcmBlock a in data.imaAdpcm[i])
                        {
                            IMAADPCMDecoder d = new IMAADPCMDecoder(a.data, 0);
                            int sampleCount = 0;
                            if (blockCount != head.nBlock - 1)
                            {
                                foreach (byte b in a.data)
                                {
                                    if (sampleCount < head.nBlockSample) { try { samples.Add(d.GetSample()); } catch { } }
                                    sampleCount += 1;
                                    if (sampleCount < head.nBlockSample) { try { samples.Add(d.GetSample()); } catch { } }
                                    sampleCount += 1;
                                }
                            }
                            else
                            {
                                foreach (byte b in a.data)
                                {
                                    if (sampleCount < head.nLastBlockSample) { try { samples.Add(d.GetSample()); } catch { } }
                                    sampleCount += 1;
                                    if (sampleCount < head.nLastBlockSample) { try { samples.Add(d.GetSample()); } catch { } }
                                    sampleCount += 1;
                                }
                            }
                            blockCount += 1;
                        }

                        newSamples[i] = samples.ToArray();

                    }

                    r.data.pcm16 = newSamples;
                    r.unpackPCMChannels();
                    break;

            }

            //Make loop.
            if (head.loop == 1)
            {

                r.smpl = new RIFF.smplBlock();
                r.smpl.loopStart = head.nLoopOffset;
                r.smpl.loopEnd = head.nSample;

            }

            r.update();
            return r;

        }


        /// <summary>
        /// Update the data.
        /// </summary>
        public void update()
        {

            magic = "STRM".ToCharArray();
            fileSize = 0xFFFFFFFF;
            identifier = 0x0100feff;
            headerSize = 0x10;
            nBlocks = 2;

            head.magic = "HEAD".ToCharArray();
            head.unknown = 0;
            head.nTime = (UInt16)Math.Floor((decimal)523655.96875 * ((decimal)1 / (decimal)head.nSampleRate));
            head.nDataOffset = 0x68;
            head.reserved = new byte[32];
            head.size = 0x50;

            data.magic = "DATA".ToCharArray();
            data.size = 0xFFFFFFFF;
            switch (head.waveType)
            {

                case 0:
                    data.size = (UInt32)(8 + data.pcm8.Length * data.pcm8[0].Length);
                    break;

                case 1:
                    data.size = (UInt32)(8 + data.pcm16.Length * data.pcm16[0].Length * 2);
                    break;

                case 2:
                    data.size = (UInt32)(8 + (head.nBlock - 1) * head.numChannel * head.nBlockLen + head.nLastBlockLen * head.numChannel);
                    break;

            }
            fileSize = data.size + head.size + 0x10;

        }


        /// <summary>
        /// Convert to bytes.
        /// </summary>
        /// <returns></returns>
        public byte[] toBytes()
        {

            //Update.
            update();

            //Writer
            MemoryStream o = new MemoryStream();
            BinaryDataWriter bw = new BinaryDataWriter(o);
            bw.ByteOrder = ByteOrder.LittleEndian;

            //Padding.
            int paddingCount = 0;
            while (fileSize % 4 != 0)
            {
                paddingCount += 1;
                fileSize += 1;
                data.size += 1;
            }

            //Read stuff.
            bw.Write(magic);
            bw.Write(identifier);
            bw.Write(fileSize);
            bw.Write(headerSize);
            bw.Write(nBlocks);

            //Wave data.
            switch (head.waveType)
            {

                case 0:
                    head.nBlock = 1;
                    head.nBlockSample = (UInt32)data.pcm8[0].Length;
                    head.nBlockLen = (UInt32)data.pcm8[0].Length;
                    head.nSample = (UInt32)data.pcm8[0].Length;
                    head.nLastBlockLen = (UInt32)data.pcm8[0].Length;
                    head.nLastBlockSample = (UInt32)data.pcm8[0].Length;
                    break;

                case 1:
                    head.nBlock = 1;
                    head.nBlockSample = (UInt32)data.pcm16[0].Length;
                    head.nBlockLen = (UInt32)data.pcm16[0].Length * 2;
                    head.nSample = (UInt32)data.pcm16[0].Length;
                    head.nLastBlockLen = (UInt32)data.pcm16[0].Length * 2;
                    head.nLastBlockSample = (UInt32)data.pcm16[0].Length;
                    break;

            }

            //Head block.
            bw.Write(head.magic);
            bw.Write(head.size);
            bw.Write(head.waveType);
            bw.Write(head.loop);
            bw.Write(head.numChannel);
            bw.Write(head.unknown);
            bw.Write(head.nSampleRate);
            bw.Write(head.nTime);
            bw.Write(head.nLoopOffset);
            bw.Write(head.nSample);
            bw.Write(head.nDataOffset);
            bw.Write(head.nBlock);
            bw.Write(head.nBlockLen);
            bw.Write(head.nBlockSample);
            bw.Write(head.nLastBlockLen);
            bw.Write(head.nLastBlockSample);
            bw.Write(head.reserved);

            //Data block.
            bw.Write(data.magic);
            bw.Write(data.size);

            //Wave data.
            switch (head.waveType)
            {

                case 0:
                    foreach (byte[] pcm8Data in data.pcm8)
                    {
                        bw.Write(pcm8Data);
                    }
                    break;

                case 1:
                    foreach (short[] pcm16Data in data.pcm16)
                    {
                        bw.Write(pcm16Data);
                    }
                    break;

                case 2:

                    //Write normal blocks.
                    int[] count = new int[head.numChannel];
                    for (int i = 0; i < count.Length; i++)
                    {
                        count[i] = 0;
                    }
                    for (int i = 0; i < head.nBlock - 1; i++)
                    {

                        for (int j = 0; j < data.imaAdpcm.Length; j++)
                        {
                            //bw.Write(data.imaAdpcm[j][i].identifier);
                            bw.Write(data.imaAdpcm[j][i].data);
                        }

                    }

                    //Last block.
                    for (int j = 0; j < data.imaAdpcm.Length; j++)
                    {
                        //bw.Write(data.imaAdpcm[j][head.nBlock - 1].identifier);
                        bw.Write(data.imaAdpcm[j][head.nBlock - 1].data);
                    }

                    break;

            }

            //Padding.
            bw.Write(new byte[paddingCount]);

            //Return.
            return o.ToArray();

        }

    }
}
