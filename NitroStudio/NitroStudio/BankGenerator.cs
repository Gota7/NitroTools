using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NitroFileLoader;
using SoundNStream;

namespace NitroStudio
{
    public partial class BankGenerator : Form
    {

        public MainWindow parent;
        List<GenEntry> gens;
        List<instrumentToFix> instrumentsToFix;

        public class GenEntry {

            public int index;
            public int[] instruments;

        }
        

        /// <summary>
        /// New bank creator.
        /// </summary>
        /// <param name="parent"></param>
        public BankGenerator(MainWindow parent)
        {

            InitializeComponent();
            this.parent = parent;
            banks.TabPages[0].Controls.Clear();
            banks.TabPages[0].Controls.Add(bankPanel);

            gens = new List<GenEntry>();
            gens.Add(new GenEntry { index = 0, instruments = new int[0] });

            int count = 0;
            foreach (symbStringName s in parent.sdat.symbFile.bankStrings) {

                if (!s.isPlaceHolder) { rBanks.Items.Add("[" + count + "] " + s.name); }
                else { rBanks.Items.Add("[" + count + "] %PLACEHOLDER% (DON'T USE!)"); }
                count += 1;

            }
            rBanks.SelectedIndex = 0;
            banks.TabPages[0].Text = rBanks.SelectedItem as string;

        }


        /// <summary>
        /// Rbanks index changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void rBanksIndexChanged(object sender, EventArgs e) {

            banks.SelectedTab.Text = rBanks.SelectedItem as string;
            gens[banks.SelectedIndex].index = rBanks.SelectedIndex;

        }


        /// <summary>
        /// Banks index changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void banksChanged(object sender, EventArgs e)
        {

            banks.SelectedTab.Controls.Clear();
            banks.SelectedTab.Controls.Add(bankPanel);

            rBanks.SelectedIndex = gens[banks.SelectedIndex].index;
            int count = 0;
            string newS = "";
            foreach (int i in gens[banks.SelectedIndex].instruments) {

                newS += i;
                if (count != gens[banks.SelectedIndex].instruments.Count() - 1) { newS += ","; }
                count += 1;

            }
            instrumentBox.Text = newS;
            if (banks.TabCount > 1) { removeButton.Enabled = true; } else { removeButton.Enabled = false; }

        }


        /// <summary>
        /// Add new generator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {

            gens.Add(new GenEntry { index = 0, instruments = new int[0] });
            banks.TabPages.Add(rBanks.Items[0] as string);
            if (banks.TabCount > 1) { removeButton.Enabled = true; } else { removeButton.Enabled = false; }

        }


        /// <summary>
        /// Remove generator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeButton_Click(object sender, EventArgs e)
        {

            gens.RemoveAt(banks.SelectedIndex);
            banks.TabPages.RemoveAt(banks.SelectedIndex);
            if (banks.TabCount > 1) { removeButton.Enabled = true; } else { removeButton.Enabled = false; }

        }


        /// <summary>
        /// Change instruments.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void instrumentBox_TextChanged(object sender, EventArgs e)
        {

            string text = instrumentBox.Text.Replace(" ", "");
            string[] items = text.Split(',');
            List<int> ints = new List<int>();
            try { foreach (string s in items) { ints.Add(int.Parse(s)); } } catch { }
            gens[banks.SelectedIndex].instruments = ints.ToArray();

        }


        /// <summary>
        /// Generate a new bank.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genButton_Click(object sender, EventArgs e)
        {

            swarFile s = new swarFile();
            sbnkFile b = new sbnkFile();
            s.data = new swarFile.swarData[1];
            b.data = new sbnkFile.sbnkData[1];
            s.data[0].files = new byte[0][];
            b.data[0].records = new sbnkFile.sbnkInstrumentRecord[0];

            List<byte[]> files = new List<byte[]>();
            List<sbnkFile.sbnkInstrumentRecord> records = new List<sbnkFile.sbnkInstrumentRecord>();

            UInt16 swavId = 0;
            foreach (GenEntry g in gens)
            {

                int fileStart = files.Count();
                Dictionary<int, Dictionary<int, int>> newSwavs = new Dictionary<int, Dictionary<int, int>>();
                sbnkFile currInstrument = new sbnkFile();
                if (!parent.sdat.infoFile.bankData[g.index].isPlaceHolder)
                {

                    currInstrument.load(parent.sdat.files.files[(int)parent.sdat.infoFile.bankData[g.index].fileId]);

                    foreach (int instrument in g.instruments)
                    {

                        try
                        {

                            sbnkFile.sbnkInstrumentRecord currentRecord = currInstrument.data[0].records[instrument];

                            //Set universal wave data.
                            if (currentRecord.fRecord == 1)
                            {

                                FixInstrumentIndexStuffUniversal(ref currentRecord.instrumentA, ref newSwavs, ref swavId, ref files, parent.sdat.infoFile.bankData[g.index]);

                            }

                            //Set ranged wave data.
                            else if (currentRecord.fRecord == 16)
                            {

                                for (int i = 0; i < currentRecord.instrumentB.stuff.Count; i++)
                                {
                                    sbnkFile.basicInstrumentStuff bcd = currentRecord.instrumentB.stuff[i];
                                    FixInstrumentIndexStuffOther(ref bcd, ref newSwavs, ref swavId, ref files, parent.sdat.infoFile.bankData[g.index]);
                                    currentRecord.instrumentB.stuff[i] = bcd;
                                }

                            }

                            //Set regional wave data.
                            else if (currentRecord.fRecord > 16)
                            {

                                for (int i = 0; i < currentRecord.instrumentC.stuff.Count; i++)
                                {
                                    sbnkFile.basicInstrumentStuff bcd = currentRecord.instrumentC.stuff[i];
                                    FixInstrumentIndexStuffOther(ref bcd, ref newSwavs, ref swavId, ref files, parent.sdat.infoFile.bankData[g.index]);
                                    currentRecord.instrumentC.stuff[i] = bcd;
                                }

                            }

                            //Add instrument.
                            records.Add(currentRecord);

                        }
                        catch {
                            MessageBox.Show("Couldn't add instrument " + instrument + " from bank " + g.index + ".", "Notice:");
                        }

                    }

                }

            }

            s.data[0].files = files.ToArray();
            b.data[0].records = records.ToArray();

            parent.sdat.files.bankFiles.Add(b.toBytes());
            parent.sdat.files.waveFiles.Add(s.toBytes());
            parent.sdat.fixOffsets();

            parent.sdat.infoFile.bankData.Add(new BankData { fileId = (UInt32)(parent.sdat.files.bankFiles.Count + parent.sdat.files.seqArcFiles.Count + parent.sdat.files.sseqFiles.Count - 1), isPlaceHolder = false, wave0 = (UInt16)(parent.sdat.infoFile.waveData.Count), wave1 = 0xFFFF, wave2 = 0xFFFF, wave3 = 0xFFFF });
            parent.sdat.symbFile.bankStrings.Add(new symbStringName { isPlaceHolder = false, name = "GENERATED_BANK" });

            parent.sdat.infoFile.waveData.Add(new WaveData { isPlaceHolder = false, fileId = (UInt32)(parent.sdat.files.waveFiles.Count + parent.sdat.files.bankFiles.Count + parent.sdat.files.seqArcFiles.Count + parent.sdat.files.sseqFiles.Count - 1) });
            parent.sdat.symbFile.waveStrings.Add(new symbStringName { isPlaceHolder = false, name = "GENERATED_WAVE" });

            //Fix file ids.
            for (int i = 0; i < parent.sdat.infoFile.waveData.Count - 1; i++) {
                parent.sdat.infoFile.waveData[i].fileId += 1;
            }
            for (int i = 0; i < parent.sdat.infoFile.strmData.Count; i++)
            {
                parent.sdat.infoFile.strmData[i].fileId += 2;
            }

            parent.sdat.fixOffsets();
            parent.updateNodes();

            MessageBox.Show("Done! It's recommended that you re-open all open banks and streams!", "Notice:");

            this.Close();

        }


        /// <summary>
        /// Get real swar number.
        /// </summary>
        /// <param name="swarNumber"></param>
        /// <param name="bank"></param>
        /// <returns></returns>
        public UInt32 GetRealSwarFileId(int swarNumber, BankData bank) {

            switch (swarNumber) {

                case 0:
                    return parent.sdat.infoFile.waveData[bank.wave0].fileId;

                case 1:
                    return parent.sdat.infoFile.waveData[bank.wave1].fileId;

                case 2:
                    return parent.sdat.infoFile.waveData[bank.wave2].fileId;

                case 3:
                    return parent.sdat.infoFile.waveData[bank.wave3].fileId;

            }

            return 0xFFFFFFFF;

        }


        public void FixInstrumentIndexStuffUniversal(ref sbnkFile.sbnkInstrumentLessThan16 u, ref Dictionary<int, Dictionary<int, int>> newSwavs, ref UInt16 swavId, ref List<byte[]> files, BankData bankData) {

            //See if wave has been loaded.
            try
            {
                int index = newSwavs[(int)GetRealSwarFileId(u.swarNumber, bankData)][u.swavNumber];
                u.swarNumber = 0;
                u.swavNumber = (UInt16)index;
            }

            //Not loaded.
            catch
            {

                //Add to loaded things.
                Dictionary<int, int> swavThing = new Dictionary<int, int>();
                swavThing.Add(u.swavNumber, swavId);

                try
                {

                    newSwavs[(int)GetRealSwarFileId(u.swarNumber, bankData)].Add(u.swavNumber, swavId);

                }

                catch
                {

                    newSwavs.Add((int)GetRealSwarFileId(u.swarNumber, bankData), swavThing);

                }

                //Add that swav file.
                swarFile sf = new swarFile();
                sf.load(parent.sdat.files.files[(int)GetRealSwarFileId(u.swarNumber, bankData)]);
                files.Add(sf.data[0].files[u.swavNumber]);

                u.swarNumber = 0;
                u.swavNumber = swavId;
                swavId += 1;

            }

        }


        public void FixInstrumentIndexStuffOther(ref sbnkFile.basicInstrumentStuff u, ref Dictionary<int, Dictionary<int, int>> newSwavs, ref UInt16 swavId, ref List<byte[]> files, BankData bankData)
        {

            //See if wave has been loaded.
            try
            {
                int index = newSwavs[(int)GetRealSwarFileId(u.swarNumber, bankData)][u.swavNumber];
                u.swarNumber = 0;
                u.swavNumber = (UInt16)index;
            }

            //Not loaded.
            catch
            {

                //Add to loaded things.
                Dictionary<int, int> swavThing = new Dictionary<int, int>();
                swavThing.Add(u.swavNumber, swavId);

                try
                {

                    newSwavs[(int)GetRealSwarFileId(u.swarNumber, bankData)].Add(u.swavNumber, swavId);

                }

                catch
                {

                    newSwavs.Add((int)GetRealSwarFileId(u.swarNumber, bankData), swavThing);

                }

                //Add that swav file.
                swarFile sf = new swarFile();
                sf.load(parent.sdat.files.files[(int)GetRealSwarFileId(u.swarNumber, bankData)]);
                files.Add(sf.data[0].files[u.swavNumber]);

                u.swarNumber = 0;
                u.swavNumber = swavId;
                swavId += 1;

            }

        }

    }


    /// <summary>
    /// Instrument to fix.
    /// </summary>
    public class instrumentToFix {

        public UInt32 swarId;
        public UInt32 swavId;

    }

}
