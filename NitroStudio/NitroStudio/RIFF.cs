using System;
using System.IO;
using Syroot.BinaryData;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibNitro.Hardware.Sound;

namespace SoundNStream
{
    //RIFF Wave.
    public class RIFF
    {

        //Loop.
        public bool loop = false;

        //Path.
        string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public char[] magic; //RIFF.
        public UInt32 chunkSize; //Size of chunks.
        public char[] identifier; //WAVE.

        public fmtBlock fmt; //FMT
        public dataBlock data; //DATA
        public smplBlock smpl; //SMPL


        //FMT block data.
        public struct fmtBlock
        {

            public char[] magic; //fmt .
            public UInt32 chunkSize; //Size of chunk.
            public UInt16 chunkFormat; //1 = PCM.
            public UInt16 numChannels; //1 - Mono, 2 - Stereo, etc.
            public UInt32 sampleRate; //Sample rate.
            public UInt32 byteRate; //== sampleRate * numChannels * bitsPerSample/8
            public UInt16 blockAlign; //==numChannels * bitsPerSample/8
            public UInt16 bitsPerSample; //8=8 bit, 16=16 bit, etc.

            public byte[] restOfData; //Misc. Data that I don't care about.

        }

        //DATA block data.
        public struct dataBlock
        {

            public char[] magic; //data.
            public UInt32 chunkSize; //==sampleRate * numChannels * bitsPerSample/8

            public byte[] data; //Raw sound data, divided by channels

            //Optional channel containers.
            public byte[][] pcm8; //PCM8 Data per channel. Always multiple of 0x4.
            public short[][] pcm16; //PCM16 Data per channel.

        }

        //SMPL block data.
        public struct smplBlock
        {

            public char[] magic; //smpl.
            public UInt32 chunkSize; //Size of chunk after this.
            public UInt32[] uselessData; //3 long.
            public UInt32 midiNote; //0x3C.
            public UInt32[] moreUselessData; //3 long.
            public UInt32 numLoops; //Number of loops, but really, I don't care.
            public UInt32[] loopDumb; //Stuff I don't care about, 3 long.
            public UInt32 loopStart; //Loopstart in samples.
            public UInt32 loopEnd; //Loopend in samples.
            public UInt32[] moreLoopDumb; //Stuff I don't care about, 2 long.

        }


        //Load PCM Channels.
        public void loadPCMChannels()
        {

            MemoryStream src = new MemoryStream(data.data);
            BinaryReader br = new BinaryReader(src);

            switch (fmt.bitsPerSample)
            {

                case 8:
                    data.pcm8 = new byte[(int)fmt.numChannels][];
                    for (int i = 0; i < data.pcm8.Length; i++)
                    {
                        data.pcm8[i] = new byte[data.data.Length / fmt.numChannels];
                    }
                    int counter = 0;
                    while (counter < data.chunkSize / fmt.numChannels)
                    {
                        for (int i = 0; i < fmt.numChannels; i++)
                        {
                            data.pcm8[i][counter] = br.ReadByte();
                        }
                        counter += 1;
                    }
                    break;

                case 16:
                    data.pcm16 = new short[(int)fmt.numChannels][];
                    for (int i = 0; i < data.pcm16.Length; i++)
                    {
                        data.pcm16[i] = new short[data.data.Length / fmt.numChannels / 2];
                    }
                    int counter2 = 0;
                    while (counter2 < data.chunkSize / fmt.numChannels / 2)
                    {
                        for (int i = 0; i < fmt.numChannels; i++)
                        {
                            data.pcm16[i][counter2] = br.ReadInt16();
                        }
                        counter2 += 1;
                    }
                    break;

            }

        }


        //Unpack PCM Channels.
        public void unpackPCMChannels()
        {

            MemoryStream o = new MemoryStream();
            BinaryDataWriter bw = new BinaryDataWriter(o);

            switch (fmt.bitsPerSample)
            {

                case 8:
                    for (int i = 0; i < data.pcm8[0].Length; i++)
                    {
                        for (int j = 0; j < data.pcm8.Length; j++)
                        {
                            bw.Write(data.pcm8[j][i]);
                        }
                    }
                    break;

                case 16:
                    for (int i = 0; i < data.pcm16[0].Length; i++)
                    {
                        for (int j = 0; j < data.pcm16.Length; j++)
                        {
                            bw.Write(data.pcm16[j][i]);
                        }
                    }
                    break;

            }

            data.data = o.ToArray();
            update();

        }


        //Load.
        public void load(byte[] b)
        {

            //New stream stuff.
            MemoryStream src = new MemoryStream(b);
            BinaryDataReader br = new BinaryDataReader(src);

            //Read stuff.
            magic = br.ReadChars(4);
            chunkSize = br.ReadUInt32();
            identifier = br.ReadChars(4);

            //FMT
            fmt.magic = br.ReadChars(4);
            fmt.chunkSize = br.ReadUInt32();
            fmt.chunkFormat = br.ReadUInt16();
            fmt.numChannels = br.ReadUInt16();
            fmt.sampleRate = br.ReadUInt32();
            fmt.byteRate = br.ReadUInt32();
            fmt.blockAlign = br.ReadUInt16();
            fmt.bitsPerSample = br.ReadUInt16();
            fmt.restOfData = br.ReadBytes((int)fmt.chunkSize - 16);

            //DATA
            data.magic = br.ReadChars(4);
            data.chunkSize = br.ReadUInt32();
            data.data = br.ReadBytes((int)data.chunkSize);

            //SMPL
            smpl = new smplBlock();
            smpl.magic = "NULL".ToCharArray();
            loop = false;
            try
            {
                smpl.magic = br.ReadChars(4);
            }
            catch { }
            if (new string(smpl.magic) == "smpl")
            {

                loop = true;
                smpl.chunkSize = br.ReadUInt32();
                smpl.uselessData = br.ReadUInt32s(3);
                smpl.midiNote = br.ReadUInt32();
                smpl.moreUselessData = br.ReadUInt32s(3);
                smpl.numLoops = br.ReadUInt32();
                smpl.loopDumb = br.ReadUInt32s(3);
                smpl.loopStart = br.ReadUInt32();
                smpl.loopEnd = br.ReadUInt32();
                smpl.moreLoopDumb = br.ReadUInt32s(2);
            }

        }


        //To bytes.
        public byte[] toBytes(bool fix = true, bool loop = false)
        {

            if (fix) { update(loop); }
            MemoryStream o = new MemoryStream();
            BinaryDataWriter bw = new BinaryDataWriter(o);

            bw.Write(magic);
            bw.Write(chunkSize);
            bw.Write(identifier);

            //FMT.
            bw.Write(fmt.magic);
            bw.Write(fmt.chunkSize);
            bw.Write(fmt.chunkFormat);
            bw.Write(fmt.numChannels);
            bw.Write(fmt.sampleRate);
            bw.Write(fmt.byteRate);
            bw.Write(fmt.blockAlign);
            bw.Write(fmt.bitsPerSample);
            bw.Write(fmt.restOfData);

            //DATA.
            bw.Write(data.magic);
            bw.Write(data.chunkSize);
            bw.Write(data.data);

            //SMPL.
            if (loop)
            {

                bw.Write(smpl.magic);
                bw.Write(smpl.chunkSize);
                bw.Write(smpl.uselessData);
                bw.Write(smpl.midiNote);
                bw.Write(smpl.moreUselessData);
                bw.Write(smpl.numLoops);
                bw.Write(smpl.loopDumb);
                bw.Write(smpl.loopStart);
                bw.Write(smpl.loopEnd);
                bw.Write(smpl.moreLoopDumb);

            }

            return o.ToArray();

        }


        //Convert to swav.
        public swav toSwav(bool encode = true)
        {

            swav s = new swav();
            if (fmt.numChannels < 2)
            {

                s.data = new swav.dataBlock();
                s.data.info.loopFlag = 0;
                s.data.info.nloopOffset = 0;
                s.data.info.nSampleRate = (UInt16)fmt.sampleRate;

                if (fmt.bitsPerSample == 8)
                {

                    //Cut off extra data.
                    loadPCMChannels();
                    if (loop && smpl.loopEnd != 0)
                    {
                        for (int i = 0; i < fmt.numChannels; i++)
                        {

                            List<byte> newSamples = data.pcm8[i].ToList();
                            while (newSamples.Count() > smpl.loopEnd)
                            {
                                newSamples.RemoveAt(newSamples.Count() - 1);
                            }
                            data.pcm8[i] = newSamples.ToArray();
                        }
                    }
                    unpackPCMChannels();

                    s.data.info.waveType = 0;
                    s.data.pcm8 = new byte[data.data.Length];
                    for (int i = 0; i < s.data.pcm8.Length; i++)
                    {
                        s.data.pcm8[i] = (byte)(data.data[i] - 0x80);
                    }
                    List<byte> pcm8Data = s.data.pcm8.ToList();
                    while (pcm8Data.Count % 4 != 0)
                    {
                        pcm8Data.Add(0);
                    }
                    s.data.pcm8 = pcm8Data.ToArray();

                }
                else
                {

                    //Cut off extra data.
                    loadPCMChannels();
                    if (loop && smpl.loopEnd != 0)
                    {
                        for (int i = 0; i < fmt.numChannels; i++)
                        {

                            List<short> newSamples = data.pcm16[i].ToList();
                            while (newSamples.Count() > smpl.loopEnd)
                            {
                                newSamples.RemoveAt(newSamples.Count() - 1);
                            }
                            data.pcm16[i] = newSamples.ToArray();
                        }
                    }
                    unpackPCMChannels();

                    if (encode)
                    {
                        s.data.info.waveType = 2;
                        MemoryStream src2 = new MemoryStream(data.data);
                        BinaryDataReader br2 = new BinaryDataReader(src2);
                        short[] samples = br2.ReadInt16s(data.data.Length / 2);
                        IMAADPCMEncoder e = new IMAADPCMEncoder();
                        List<byte> imaData = e.Encode(samples).ToList();
                        while (imaData.Count % 4 != 0)
                        {
                            imaData.Add(0);
                        }
                        s.data.imaAdpcm = imaData.ToArray();

                    }
                    else
                    {
                        s.data.info.waveType = 1;
                        MemoryStream src = new MemoryStream(data.data);
                        BinaryDataReader br = new BinaryDataReader(src);
                        s.data.pcm16 = br.ReadInt16s(data.data.Length / 2);
                        List<short> pcm16Data = s.data.pcm16.ToList();
                        while (pcm16Data.Count % 2 != 0)
                        {
                            pcm16Data.Add(0);
                        }
                        s.data.pcm16 = pcm16Data.ToArray();
                    }
                }
            }
            else
            {
                throw new Exception("Wave file not mono!");
            }


            //Loop.
            if (loop)
            {

                s.data.info.loopFlag = 1;
                switch (s.data.info.waveType)
                {

                    case 0:
                        s.data.info.nloopOffset = (UInt16)NearestRound(smpl.loopStart / 4, 4);
                        break;

                    case 1:
                        s.data.info.nloopOffset = (UInt16)NearestRound(smpl.loopStart / 2, 4);
                        break;

                    case 2:
                        s.data.info.nloopOffset = (UInt16)NearestRound(smpl.loopStart / 8, 4);
                        break;

                }

            }

            return s;

        }


        //Convert to strm.
        public strm toStrm(bool encode = true)
        {

            strm s = new strm();
            s.head = new strm.headBlock();
            s.data = new strm.dataBlock();
            s.head.loop = 0;
            s.head.nLoopOffset = 0;
            s.head.numChannel = (byte)fmt.numChannels;
            s.head.nSampleRate = (UInt16)fmt.sampleRate;
            s.head.waveType = 0;

            if (fmt.bitsPerSample == 8)
            {

                loadPCMChannels();

                //Cut off extra data.
                if (loop && smpl.loopEnd != 0)
                {
                    for (int i = 0; i < fmt.numChannels; i++)
                    {

                        List<byte> newSamples = data.pcm8[i].ToList();
                        while (newSamples.Count() > smpl.loopEnd)
                        {
                            newSamples.RemoveAt(newSamples.Count() - 1);
                        }
                        data.pcm8[i] = newSamples.ToArray();
                    }
                }


                s.data.pcm8 = new byte[fmt.numChannels][];
                for (int i = 0; i < fmt.numChannels; i++)
                {
                    s.data.pcm8[i] = new byte[data.pcm8[i].Length];
                    for (int j = 0; j < s.data.pcm8[i].Length; j++)
                    {
                        s.data.pcm8[i][j] = (byte)(data.pcm8[i][j] - 0x80);
                    }
                }

            }
            else if (fmt.bitsPerSample == 16)
            {

                loadPCMChannels();

                //Cut off extra data.
                if (loop && smpl.loopEnd != 0)
                {
                    for (int i = 0; i < fmt.numChannels; i++)
                    {

                        List<short> newSamples = data.pcm16[i].ToList();
                        while (newSamples.Count() > smpl.loopEnd)
                        {
                            newSamples.RemoveAt(newSamples.Count() - 1);
                        }
                        data.pcm16[i] = newSamples.ToArray();
                    }
                }

                if (encode)
                {

                    s.head.waveType = 2;

                    //Divide to blocks.
                    List<short>[][] samples = new List<short>[(int)fmt.numChannels][];
                    for (int i = 0; i < fmt.numChannels; i++)
                    {

                        int numBlocks = (int)Math.Ceiling((decimal)data.pcm16[i].Length / (decimal)0x3F8);
                        samples[i] = new List<short>[numBlocks];
                        int blockCount = 0;
                        samples[i][0] = new List<short>();
                        for (int j = 0; j < data.pcm16[i].Length; j++)
                        {
                            if (j % 0x3F8 == 0 && j != 0)
                            {
                                blockCount += 1;
                                samples[i][blockCount] = new List<short>();
                            }
                            samples[i][blockCount].Add(data.pcm16[i][j]);
                        }

                    }


                    //IMA.
                    s.data.imaAdpcm = new strm.dataBlock.imaAdpcmBlock[fmt.numChannels][];
                    for (int i = 0; i < fmt.numChannels; i++)
                    {
                        s.data.imaAdpcm[i] = new strm.dataBlock.imaAdpcmBlock[samples[i].Length];
                    }

                    //Convert blocks.
                    for (int i = 0; i < fmt.numChannels; i++)
                    {

                        for (int j = 0; j < samples[i].Length; j++)
                        {
                            IMAADPCMEncoder e = new IMAADPCMEncoder();
                            byte[] encodedBlock = e.Encode(samples[i][j].ToArray());
                            s.data.imaAdpcm[i][j] = new strm.dataBlock.imaAdpcmBlock();
                            s.data.imaAdpcm[i][j].data = encodedBlock;
                        }

                    }

                    //Head stuff.
                    s.head.nSample = (UInt32)data.pcm16[0].Length;
                    s.head.nBlockLen = 0x200;
                    s.head.nBlock = (UInt32)Math.Ceiling(data.pcm16[0].Length / (decimal)0x3F8);
                    s.head.nBlockSample = 0x3F8;
                    s.head.nLastBlockLen = (UInt32)(s.data.imaAdpcm[0][s.data.imaAdpcm[0].Length - 1].data.Length);
                    s.head.nLastBlockSample = (UInt32)(samples[0][samples[0].Length - 1].Count());

                }
                else
                {

                    s.head.waveType = 1;
                    s.data.pcm16 = new short[fmt.numChannels][];
                    for (int i = 0; i < fmt.numChannels; i++)
                    {
                        s.data.pcm16[i] = new short[data.pcm16[i].Length];
                        for (int j = 0; j < s.data.pcm16[i].Length; j++)
                        {
                            s.data.pcm16[i][j] = data.pcm16[i][j];
                        }
                    }

                }

            }

            //Loop.
            if (loop)
            {

                s.head.loop = 1;
                s.head.nLoopOffset = (UInt32)NearestRound(smpl.loopStart, 0x3F8);

            }

            s.update();
            return s;

        }


        //Fix offsets.
        public void update(bool loop = false)
        {

            //Data.
            data.chunkSize = (UInt32)data.data.Length;
            data.magic = "data".ToCharArray();

            //FMT.
            fmt.magic = "fmt ".ToCharArray();
            fmt.chunkSize = 16 + (UInt32)fmt.restOfData.Length;
            fmt.blockAlign = (UInt16)(fmt.numChannels * fmt.bitsPerSample / 8);
            fmt.byteRate = (UInt32)(fmt.sampleRate * fmt.numChannels * fmt.bitsPerSample / 8);

            //SMPL.
            smpl.magic = "smpl".ToCharArray();
            smpl.chunkSize = 0x3C;
            smpl.midiNote = 0x3C;
            smpl.uselessData = new UInt32[3];
            smpl.moreUselessData = new UInt32[3];
            smpl.numLoops = 1;
            smpl.loopDumb = new UInt32[3];
            smpl.moreLoopDumb = new UInt32[2];

            //Total.
            magic = "RIFF".ToCharArray();
            identifier = "WAVE".ToCharArray();
            chunkSize = fmt.chunkSize + data.chunkSize + 20;
            if (loop) { chunkSize += (smpl.chunkSize + 8); }

        }


        //Round to a number.
        public float NearestRound(float x, float delX)
        {
            if (delX < 1)
            {
                float i = (float)Math.Floor(x);
                float x2 = i;
                while ((x2 += delX) < x) ;
                float x1 = x2 - delX;
                return (Math.Abs(x - x1) < Math.Abs(x - x2)) ? x1 : x2;
            }
            else
            {
                return (float)Math.Round(x / delX, MidpointRounding.AwayFromZero) * delX;
            }
        }

    }
}
