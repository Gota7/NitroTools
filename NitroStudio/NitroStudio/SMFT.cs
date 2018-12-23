using LibNitro.SND;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitroStudio
{

    public class SMFT : LibNitro.SND.SMFT {

        private static readonly string[] Notes = new string[12]
    {
      "cn",
      "cs",
      "dn",
      "ds",
      "en",
      "fn",
      "fs",
      "gn",
      "gs",
      "an",
      "as",
      "bn"
    };

        public static string ToSMFT(SSEQ Seq, List<int> existingLabels = null)
        {
            int[] labels = SMFT.GetLabels(Seq, existingLabels);
            int Offset = 0;
            StringBuilder b = new StringBuilder();
            while (Offset < Seq.Data.Length)
            {
                if (((IEnumerable<int>)labels).Contains<int>(Offset))
                    b.AppendFormat("\nLabel_0x{0:X8}:\n", (object)Offset);
                string str = "";
                bool flag = false;
                byte num1 = Seq.Data[Offset++];
                if (num1 == (byte)162)
                {
                    num1 = Seq.Data[Offset++];
                    str = "_if" + str;
                }
                byte num2 = 0;
                if (num1 == (byte)160)
                {
                    num1 = Seq.Data[Offset++];
                    str = "_r" + str;
                    num2 = (byte)3;
                    flag = true;
                }
                if (num1 == (byte)161)
                {
                    num1 = Seq.Data[Offset++];
                    str = "_v" + str;
                    num2 = (byte)4;
                    flag = true;
                }
                if (num1 < (byte)128)
                {
                    if (num1 != (byte)0 || Offset < Seq.Data.Length - 4)
                    {
                        byte num3 = Seq.Data[Offset++];
                        int index = (int)num1 % 12;
                        int num4 = (int)num1 / 12 - 1;
                        b.AppendFormat("    {0}{1} {2},", (object)SMFT.Notes[index], (object)((num4 < 0 ? "m1" : string.Concat((object)num4)) + str), (object)num3);
                        if (flag)
                            SMFT.WriteArgOverride(b, Seq.Data, ref Offset, (int)num2);
                        else
                            b.AppendFormat(" {0}\n", (object)SMFT.ReadArg(Seq.Data, ref Offset, 2));
                    }
                    else
                        break;
                }
                else
                {
                    switch ((int)num1 & 240)
                    {
                        case 128:
                            short num5 = -1;
                            if (!flag)
                                num5 = (short)SMFT.ReadArg(Seq.Data, ref Offset, 2);
                            switch (num1)
                            {
                                case 128:
                                    b.Append("    wait" + str);
                                    break;
                                case 129:
                                    b.Append("    prg" + str);
                                    break;
                            }
                            if (flag)
                            {
                                SMFT.WriteArgOverride(b, Seq.Data, ref Offset, (int)num2);
                                continue;
                            }
                            b.AppendFormat(" {0}\n", (object)num5);
                            continue;
                        case 144:
                            switch (num1)
                            {
                                case 147:
                                    int num6 = (int)Seq.Data[Offset++];
                                    int num7 = (int)Seq.Data[Offset++] | (int)Seq.Data[Offset++] << 8 | (int)Seq.Data[Offset++] << 16;
                                    b.AppendFormat("    opentrack{0} {1}, Label_0x{2:X8}\n", (object)str, (object)num6, (object)num7);
                                    continue;
                                case 148:
                                    int num8 = (int)Seq.Data[Offset++] | (int)Seq.Data[Offset++] << 8 | (int)Seq.Data[Offset++] << 16;
                                    b.AppendFormat("    jump{0} Label_0x{1:X8}\n", (object)str, (object)num8);
                                    continue;
                                case 149:
                                    int num9 = (int)Seq.Data[Offset++] | (int)Seq.Data[Offset++] << 8 | (int)Seq.Data[Offset++] << 16;
                                    b.AppendFormat("    call{0} Label_0x{1:X8}\n", (object)str, (object)num9);
                                    continue;
                                default:
                                    continue;
                            }
                        case 176:
                            switch (num1)
                            {
                                case 176:
                                    b.Append("    setvar" + str);
                                    break;
                                case 177:
                                    b.Append("    addvar" + str);
                                    break;
                                case 178:
                                    b.Append("    subvar" + str);
                                    break;
                                case 179:
                                    b.Append("    mulvar" + str);
                                    break;
                                case 180:
                                    b.Append("    divvar" + str);
                                    break;
                                case 181:
                                    b.Append("    shiftvar" + str);
                                    break;
                                case 182:
                                    b.Append("    randvar" + str);
                                    break;
                                case 184:
                                    b.Append("    cmp_eq" + str);
                                    break;
                                case 185:
                                    b.Append("    cmp_ge" + str);
                                    break;
                                case 186:
                                    b.Append("    cmp_gt" + str);
                                    break;
                                case 187:
                                    b.Append("    cmp_le" + str);
                                    break;
                                case 188:
                                    b.Append("    cmp_lt" + str);
                                    break;
                                case 189:
                                    b.Append("    cmp_ne" + str);
                                    break;
                            }
                            int num10 = (int)Seq.Data[Offset++];
                            b.AppendFormat(" {0},", (object)num10);
                            if (flag)
                            {
                                SMFT.WriteArgOverride(b, Seq.Data, ref Offset, (int)num2);
                                continue;
                            }
                            b.AppendFormat(" {0}\n", (object)(short)SMFT.ReadArg(Seq.Data, ref Offset, 1));
                            continue;
                        case 192:
                        case 208:
                            int num11 = 0;
                            if (!flag)
                                num11 = SMFT.ReadArg(Seq.Data, ref Offset, 0);
                            switch (num1)
                            {
                                case 192:
                                    b.Append("    pan" + str);
                                    break;
                                case 193:
                                    b.Append("    volume" + str);
                                    break;
                                case 194:
                                    b.Append("    main_volume" + str);
                                    break;
                                case 195:
                                    b.Append("    transpose" + str);
                                    break;
                                case 196:
                                    b.Append("    pitchbend" + str);
                                    break;
                                case 197:
                                    b.Append("    bendrange" + str);
                                    break;
                                case 198:
                                    b.Append("    prio" + str);
                                    break;
                                case 199:
                                    b.AppendFormat("    notewait_{0}", (object)(((num11 & 1) == 1 ? "on" : "off") + str));
                                    break;
                                case 200:
                                    b.AppendFormat("    tie{0}", (object)(((num11 & 1) == 1 ? "on" : "off") + str));
                                    break;
                                case 201:
                                    b.Append("    porta" + str);
                                    break;
                                case 202:
                                    b.Append("    mod_depth" + str);
                                    break;
                                case 203:
                                    b.Append("    mod_speed" + str);
                                    break;
                                case 204:
                                    b.Append("    mod_type" + str);
                                    break;
                                case 205:
                                    b.Append("    mod_range" + str);
                                    break;
                                case 206:
                                    b.AppendFormat("    porta_{0}", (num11 & 1) == 1 ? (object)"on" : (object)"off");
                                    break;
                                case 207:
                                    b.Append("    porta_time" + str);
                                    break;
                                case 208:
                                    b.Append("    attack" + str);
                                    break;
                                case 209:
                                    b.Append("    decay" + str);
                                    break;
                                case 210:
                                    b.Append("    sustain" + str);
                                    break;
                                case 211:
                                    b.Append("    release" + str);
                                    break;
                                case 212:
                                    b.Append("    loop_start" + str);
                                    break;
                                case 213:
                                    b.Append("    volume2" + str);
                                    break;
                                case 214:
                                    b.Append("    printvar" + str);
                                    break;
                                case 215:
                                    b.Append("    mute" + str);
                                    break;
                            }
                            if (flag)
                            {
                                SMFT.WriteArgOverride(b, Seq.Data, ref Offset, (int)num2);
                                continue;
                            }
                            switch (num1)
                            {
                                case 195:
                                case 196:
                                    b.AppendFormat(" {0}\n", (object)(sbyte)num11);
                                    continue;
                                case 199:
                                case 200:
                                case 206:
                                    b.AppendLine();
                                    continue;
                                default:
                                    b.AppendFormat(" {0}\n", (object)num11);
                                    continue;
                            }
                        case 224:
                            switch (num1)
                            {
                                case 224:
                                    b.Append("    mod_delay" + str);
                                    break;
                                case 225:
                                    b.Append("    tempo" + str);
                                    break;
                                case 227:
                                    b.Append("    sweep_pitch" + str);
                                    break;
                            }
                            if (flag)
                            {
                                SMFT.WriteArgOverride(b, Seq.Data, ref Offset, (int)num2);
                                continue;
                            }
                            if (num1 == (byte)227)
                            {
                                b.AppendFormat(" {0}\n", (object)(short)SMFT.ReadArg(Seq.Data, ref Offset, 1));
                                continue;
                            }
                            b.AppendFormat(" {0}\n", (object)(ushort)SMFT.ReadArg(Seq.Data, ref Offset, 1));
                            continue;
                        case 240:
                            switch (num1)
                            {
                                case 252:
                                    b.AppendLine("    loop_end" + str);
                                    continue;
                                case 253:
                                    b.AppendLine("    ret" + str);
                                    continue;
                                case 254:
                                    b.AppendFormat("    alloctrack{0} 0x{1:X4}\n", (object)str, (object)SMFT.ReadArg(Seq.Data, ref Offset, 1));
                                    continue;
                                case byte.MaxValue:
                                    b.AppendLine("    fin" + str);
                                    continue;
                                default:
                                    continue;
                            }
                        default:
                            continue;
                    }
                }
            }
            return b.ToString();
        }

        private static int[] GetLabels(SSEQ Seq, List<int> existingLabels)
        {
            int Offset = 0;
            List<int> intList = new List<int>();
            if (existingLabels != null) intList.AddRange(existingLabels);
            while (Offset < Seq.Data.Length)
            {
                bool flag = false;
                byte num1 = Seq.Data[Offset++];
                if (num1 == (byte)162)
                    num1 = Seq.Data[Offset++];
                byte num2 = 0;
                if (num1 == (byte)160)
                {
                    num1 = Seq.Data[Offset++];
                    num2 = (byte)3;
                    flag = true;
                }
                if (num1 == (byte)161)
                {
                    num1 = Seq.Data[Offset++];
                    num2 = (byte)4;
                    flag = true;
                }
                if (num1 < (byte)128)
                {
                    if (num1 != (byte)0 || Offset < Seq.Data.Length - 4)
                    {
                        byte num3 = Seq.Data[Offset++];
                        byte num4 = 2;
                        if (flag)
                            num4 = num2;
                        SMFT.ReadArg(Seq.Data, ref Offset, (int)num4);
                    }
                    else
                        break;
                }
                else
                {
                    switch ((int)num1 & 240)
                    {
                        case 128:
                            int type1 = 2;
                            if (flag)
                                type1 = (int)num2;
                            int num5 = (int)(short)SMFT.ReadArg(Seq.Data, ref Offset, type1);
                            continue;
                        case 144:
                            switch (num1)
                            {
                                case 147:
                                    byte[] data1 = Seq.Data;
                                    int index1 = Offset;
                                    int num6 = index1 + 1;
                                    int num7 = (int)data1[index1];
                                    byte[] data2 = Seq.Data;
                                    int index2 = num6;
                                    int num8 = index2 + 1;
                                    int num9 = (int)data2[index2];
                                    byte[] data3 = Seq.Data;
                                    int index3 = num8;
                                    int num10 = index3 + 1;
                                    int num11 = (int)data3[index3] << 8;
                                    int num12 = num9 | num11;
                                    byte[] data4 = Seq.Data;
                                    int index4 = num10;
                                    Offset = index4 + 1;
                                    int num13 = (int)data4[index4] << 16;
                                    int num14 = num12 | num13;
                                    if (!intList.Contains(num14))
                                    {
                                        intList.Add(num14);
                                        continue;
                                    }
                                    continue;
                                case 148:
                                    byte[] data5 = Seq.Data;
                                    int index5 = Offset;
                                    int num15 = index5 + 1;
                                    int num16 = (int)data5[index5];
                                    byte[] data6 = Seq.Data;
                                    int index6 = num15;
                                    int num17 = index6 + 1;
                                    int num18 = (int)data6[index6] << 8;
                                    int num19 = num16 | num18;
                                    byte[] data7 = Seq.Data;
                                    int index7 = num17;
                                    Offset = index7 + 1;
                                    int num20 = (int)data7[index7] << 16;
                                    int num21 = num19 | num20;
                                    if (!intList.Contains(num21))
                                    {
                                        intList.Add(num21);
                                        continue;
                                    }
                                    continue;
                                case 149:
                                    byte[] data8 = Seq.Data;
                                    int index8 = Offset;
                                    int num22 = index8 + 1;
                                    int num23 = (int)data8[index8];
                                    byte[] data9 = Seq.Data;
                                    int index9 = num22;
                                    int num24 = index9 + 1;
                                    int num25 = (int)data9[index9] << 8;
                                    int num26 = num23 | num25;
                                    byte[] data10 = Seq.Data;
                                    int index10 = num24;
                                    Offset = index10 + 1;
                                    int num27 = (int)data10[index10] << 16;
                                    int num28 = num26 | num27;
                                    if (!intList.Contains(num28))
                                    {
                                        intList.Add(num28);
                                        continue;
                                    }
                                    continue;
                                default:
                                    continue;
                            }
                        case 176:
                            int num29 = (int)Seq.Data[Offset++];
                            int type2 = 1;
                            if (flag)
                                type2 = (int)num2;
                            short num30 = (short)SMFT.ReadArg(Seq.Data, ref Offset, type2);
                            continue;
                        case 192:
                        case 208:
                            int type3 = 0;
                            if (flag)
                                type3 = (int)num2;
                            SMFT.ReadArg(Seq.Data, ref Offset, type3);
                            continue;
                        case 224:
                            int type4 = 1;
                            if (flag)
                                type4 = (int)num2;
                            short num31 = (short)SMFT.ReadArg(Seq.Data, ref Offset, type4);
                            continue;
                        case 240:
                            switch (num1)
                            {
                                case 254:
                                    Offset += 2;
                                    continue;
                                default:
                                    continue;
                            }
                        default:
                            continue;
                    }
                }
            }
            return intList.ToArray();
        }

        private static int ReadArg(byte[] Data, ref int Offset, int type)
        {
            switch (type)
            {
                case 0:
                    return (int)Data[Offset++];
                case 1:
                    return (int)Data[Offset++] | (int)Data[Offset++] << 8;
                case 2:
                    int num1 = 0;
                    byte num2;
                    do
                    {
                        num2 = Data[Offset++];
                        num1 = num1 << 7 | (int)num2 & (int)sbyte.MaxValue;
                    }
                    while (((int)num2 & 128) != 0);
                    return num1;
                case 3:
                    short num3 = (short)((int)Data[Offset++] | (int)Data[Offset++] << 8);
                    ushort num4 = (ushort)((int)Data[Offset++] | (int)Data[Offset++] << 8);
                    return 0;
                case 4:
                    byte num5 = Data[Offset++];
                    return 0;
                default:
                    return 0;
            }
        }

        private static void WriteArgOverride(StringBuilder b, byte[] Data, ref int Offset, int type)
        {
            if (type != 3)
            {
                if (type != 4)
                    return;
                b.AppendFormat(" {0}\n", (object)Data[Offset++]);
            }
            else
            {
                short num1 = (short)((int)Data[Offset++] | (int)Data[Offset++] << 8);
                ushort num2 = (ushort)((int)Data[Offset++] | (int)Data[Offset++] << 8);
                b.AppendFormat(" {0}, {1}\n", (object)num1, (object)num2);
            }
        }

    }

}
