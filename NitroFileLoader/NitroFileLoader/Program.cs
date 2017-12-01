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

        }
	}
}
