using System;
using System.IO;

/// <summary>
/// Nitro File Loader by Gota7. Tools to load, edit, and recompile the SYMB and INFO bins.
/// </summary>
namespace NitroFileLoader
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            
			//Some dummy code for messing around with SYMB and INFO files.

			/*
			NitroStructures.infoFile f = NitroFileLoader.loadInfoFile (File.ReadAllBytes ("infoNSMB.bin"));

			NitroStructures.infoFile h = f;

			byte[] infoTest = NitroFileLoader.infoToBytes (f);
			File.WriteAllBytes ("test.bin",infoTest);

			f = NitroFileLoader.loadInfoFile (File.ReadAllBytes ("test.bin"));
			File.WriteAllBytes ("test2.bin", NitroFileLoader.infoToBytes (f));
            
			

            NitroStructures.symbFile s = NitroFileLoader.loadSymbFile(File.ReadAllBytes("symb.bin"));

            NitroStructures.symbFile t = s;

            byte[] symbTest = NitroFileLoader.symbToBytes(s);
            File.WriteAllBytes("test.bin", symbTest);

			symbData sData = new symbData ();
			sData.load (s);

			File.WriteAllBytes ("test2.bin", NitroFileLoader.symbToBytes(sData.toSymb()));
*/

			//Test seqArc loading.
			/*
			byte[] seqArcNintendo = File.ReadAllBytes("sound_data/Sequence Archive/05NCS_SEQARC_NINTENDO.ssar");
			byte[] seqArcVoice = File.ReadAllBytes("sound_data/Sequence Archive/02NCS_SEQARC_VOICE.ssar");
			seqArc sN = new seqArc ();
			sN.load (seqArcNintendo);
			seqArc vN = new seqArc ();
			vN.load (seqArcVoice);
			*/
			/*
			//Wave arcs.
			byte[] waveArcNintendo = File.ReadAllBytes("sound_data/Wave Archive/01NCS_WAVE_SE_NINTENDO.swar");
			swarFile sW = new swarFile ();
			sW.load (waveArcNintendo);
			byte[] waveArcResident = File.ReadAllBytes ("sound_data/Wave Archive/04NCS_WAVE_RESIDENT.swar");
			swarFile sR = new swarFile ();
			sR.load (waveArcResident);

			sW.extract ("NINTENDO_WAVE");
			sR.extract ("RESIDENT_WAVE");

			sR.compress ("RESIDENT_WAVE");
			sW.compress ("NINTENDO_WAVE");
			File.WriteAllBytes ("WWAVE.swar", sW.toBytes ());
			File.WriteAllBytes ("RWAVE.swar", sR.toBytes ());


			//SBNK
			byte[] bankWater = File.ReadAllBytes("sound_data/Bank/070NCS_BANK_BGM_WATER.sbnk");
			sbnkFile water = new sbnkFile ();
			water.load (bankWater);


			//SDAT
			byte[] dataSdatB = File.ReadAllBytes("data.sdat");
			sdatFile dataSdat = new sdatFile ();
			//dataSdat.load (dataSdatB);
			//dataSdat.extract ("extract");
			dataSdat.compress ("extract");
			byte[] newSdat = dataSdat.toBytes ();
			File.WriteAllBytes ("test.sdat", newSdat);

			//Extract other sdat.
			byte[] otherSdatB = File.ReadAllBytes("test.sdat");
			sdatFile otherSdat = new sdatFile ();
			otherSdat.load (otherSdatB);
			otherSdat.extract ("extract2");
			*/
        }
	}
}
