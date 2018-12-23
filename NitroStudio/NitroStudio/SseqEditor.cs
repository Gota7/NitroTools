using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibNitro;

namespace NitroStudio
{
    public partial class SseqEditor : Form
    {

        //SSEQ.
        LibNitro.SND.SSEQ file;

        public SseqEditor()
        {
            InitializeComponent();
        }
    }
}
