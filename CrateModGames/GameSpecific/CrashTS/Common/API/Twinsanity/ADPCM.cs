using System;
using System.Collections.Generic;

namespace Twinsanity
{
    // Based on code by bITmASTER and nextvolume
    // https://github.com/simias/psxsdk/blob/master/tools/vag2wav.c
    // https://github.com/simias/psxsdk/blob/master/tools/wav2vag.c

    [Flags]
    public enum SampleLineFlags : byte
    {
        None = 0,
        LoopEnd = 1,
        Unknown = 2,
        LoopStart = 4
    }

    /// <summary>
    /// Represents Adaptive Differential Pulse-Code Modulation
    /// </summary>
    public static class ADPCM
    {
        private static readonly int BUFFER_SIZE = 128 * 28;
        private static readonly double[][] f = new double[][]{
            new double [2]{ 0.0, 0.0 },
            new double [2]{   60.0 / 64.0,  0.0 },
            new double [2]{  115.0 / 64.0, -52.0 / 64.0 },
            new double [2]{   98.0 / 64.0, -55.0 / 64.0 },
            new double [2]{ 122.0 / 64.0, -60.0 / 64.0 },
            new double [2]{ 0.0, 0.0 },
            new double [2]{ 0.0, 0.0 },
            new double [2]{ 0.0, 0.0 },
            new double [2]{ 0.0, 0.0 },
            new double [2]{ 0.0, 0.0 },
            new double [2]{ 0.0, 0.0 },
            new double [2]{ 0.0, 0.0 },
            new double [2]{ 0.0, 0.0 },
            new double [2]{ 0.0, 0.0 },
            new double [2]{ 0.0, 0.0 },
            new double [2]{ 0.0, 0.0 } };
        private static double _s_1 = 0.0, _s_2 = 0.0;

        //Convert a single ADPCM sample (4-bit value) into a PCM sample
        private static short SampleToPCM(int sample, int factor, int predict, ref double s0, ref double s1)
        {
            sample <<= 12;
            sample = (short)sample; //sign extend
            sample >>= factor;
            double value = sample;
            value += s0 * f[predict][0];
            value += s1 * f[predict][1];
            s1 = s0;
            s0 = value;
            return (short)Math.Round(value);
        }

        //Convert a single sample-line (28 samples, 16 bytes) from memory into an array of PCM samples
        private static byte[] LineToPCM(byte[] input, ref double s0, ref double s1)
        {
            if (input.Length != 16)
                throw new ArgumentException("input");
            byte[] o = new byte[28 * 2];
            int factor = input[0] & 0xF;
            int predict = (input[0] >> 4) & 0xF;
            for (int i = 0; i < 14; i++)
            {
                int adl = input[i+2] & 0xF;
                int adh = (input[i+2] & 0xF0) >> 4;
                short l = SampleToPCM(adl, factor, predict, ref s0, ref s1);
                short h = SampleToPCM(adh, factor, predict, ref s0, ref s1);
                BitConv.ToInt16(o, i * 4 + 0, l);
                BitConv.ToInt16(o, i * 4 + 2, h);
            }
            return o;
        }

        public static byte[] ToPCMMono(byte[] data, int size)
        {
            if ((size % 16) != 0)
                throw new ArgumentException("Sample size is not a multiple of 16.");
            double s0 = 0, s1 = 0;
            List<byte> pcm_data = new List<byte>();
            for (int i = 0; i < size; i+=16)
            {
                byte[] line = new byte[16];
                Array.Copy(data, i, line, 0, 16);
                if (line[1] == 7)
                    break;
                pcm_data.AddRange(LineToPCM(line, ref s0, ref s1));
                if (((SampleLineFlags)line[1] & SampleLineFlags.LoopEnd) != 0)
                    break;
            }
            return pcm_data.ToArray();
        }

        public static byte[] ToPCMStereo(byte[] data, int size, int interleave)
        {
            if ((size % 32) != 0)
                throw new ArgumentException("Stereo sample size is not a multiple of 32.");
            if ((interleave % 16) != 0)
                throw new ArgumentException("Stereo interleave is not a multiple of 16.");
            if (interleave <= 0)
                throw new ArgumentOutOfRangeException("interleave");
            size /= 32;
            interleave /= 16;
            double s0_l = 0, s1_l = 0;
            double s0_r = 0, s1_r = 0;
            List<byte> pcm_data = new List<byte>();
            int interleave_adv = 0;
            for (int i = 0; i < size; ++i)
            {
                if ((i % interleave) == 0)
                    ++interleave_adv;
                byte[] line_l = new byte[16];
                byte[] line_r = new byte[16];
                Array.Copy(data, (i + interleave * (interleave_adv-1)) * 16, line_l, 0, 16);
                Array.Copy(data, (i + interleave * interleave_adv) * 16, line_r, 0, 16);
                if (line_l[1] == 7 || line_r[1] == 7)
                    break;
                var l = LineToPCM(line_l, ref s0_l, ref s1_l);
                var r = LineToPCM(line_r, ref s0_r, ref s1_r);
                for (int j = 0; j < 28; ++j)
                {
                    pcm_data.Add(l[0 + j * 2]);
                    pcm_data.Add(l[1 + j * 2]);
                    pcm_data.Add(r[0 + j * 2]);
                    pcm_data.Add(r[1 + j * 2]);
                }
                if (line_l[1] == 1 || line_r[1] == 1)
                    break;
            }
            return pcm_data.ToArray();
        }

        public static byte[] FromPCMMono(byte[] data)
        {
            int off = 0, predict = 0, factor = 0;
            int i;
            short[] wave = new short[BUFFER_SIZE];
            double[] d_samples = new double[28];
            short[] v_samples = new short[28];
            int sample_size = data.Length / 2;
            List<byte> vag = new List<byte>();
            _s_1 = _s_2 = 0.0;
            while (sample_size > 0)
            {
                var size = (sample_size >= BUFFER_SIZE) ? BUFFER_SIZE : sample_size;
                for (i = 0; i < size; ++i, off += 2)
                    wave[i] = BitConverter.ToInt16(data, off);
                i = size / 28;
                if ((size % 28) != 0)
                {
                    for (int j = size % 28; j < 28; ++j)
                        wave[28 * i + j] = 0;
                    ++i;
                }
                for (int j = 0; j < i; ++j)
                {
                    FindPredict(wave, j * 28, d_samples, ref predict, ref factor);
                    PackSamples(d_samples, v_samples, predict, factor);
                    vag.Add((byte)((predict << 4) | factor));
                    vag.Add(0);
                    for (int k = 0; k < 28; k += 2)
                    {
                        vag.Add((byte)(((v_samples[k + 1] >> 8) & 0xF0) | ((v_samples[k] >> 12) & 0xF)));
                    }
                    sample_size -= 28;
                }
            }
            vag.Add((byte)((predict << 4) | factor));
            vag.Add(7);
            for (i = 0; i < 14; ++i)
                vag.Add(0);
            return vag.ToArray();
        }

        /// <summary>
        /// Return VAG data from a 16-bit PCM stream.
        /// </summary>
        /// <param name="data">PCM data to convert from.</param>
        /// <param name="interleave">Left/right interleave, in bytes.</param>
        /// <returns></returns>
        public static byte[] FromPCMStereo(byte[] data, int interleave)
        {
            if ((interleave % 16) != 0)
                throw new ArgumentException("Interleave must be a multiple of 16.");
            byte[] silence = new byte[interleave];
            for (int i = 0; i < interleave*2; ++i)
                silence[i] = 0;
            int sample_size = data.Length / 4;
            byte[] data_l = new byte[sample_size*2];
            byte[] data_r = new byte[sample_size*2];
            List<byte> vag = new List<byte>();
            for (int i = 0; i < sample_size; ++i)
            {
                data_l[i+0] = data[i * 4 + 0];
                data_l[i+1] = data[i * 4 + 1];
                data_r[i+0] = data[i * 4 + 2];
                data_r[i+1] = data[i * 4 + 3];
            }
            data_l = FromPCMMono(data_l);
            data_r = FromPCMMono(data_r);
            for (int i = 0; i < data_l.Length; i += interleave)
            {
                vag.AddRange(silence);
            }
            var vag_data = vag.ToArray();
            var ch_size = data_l.Length;
            int off = 0;
            while (ch_size > 0)
            {
                var size = (ch_size >= interleave) ? interleave : ch_size;
                Array.Copy(data_l, off, vag_data, off*2, size);
                Array.Copy(data_r, off + interleave, vag_data, off * 2 + interleave, size);
                off += interleave;
                ch_size -= interleave;
            }
            return vag_data;
        }

        private static void FindPredict(short[] samples, int sample_off, double[] d_samples, ref int predict, ref int factor)
        {
            double[] max = new double[5];
            double[][] buffer = new double[28][];
            for (int i = 0; i < 28; ++i)
                buffer[i] = new double[5];
            double s_0, s_1 = 0.0, s_2 = 0.0, min = 1e10;
            for (int i = 0; i < 5; ++i)
            {
                max[i] = 0.0;
                s_1 = _s_1;
                s_2 = _s_2;
                for (int j = 0; j < 28; ++j)
                {
                    s_0 = samples[j + sample_off];
                    if (s_0 > 30719.0)
                        s_0 = 30719.0;
                    else if (s_0 < -30719.0)
                        s_0 = -30719.0;
                    double ds = s_0 + s_1 * f[i][0] + s_2 * f[i][1];
                    buffer[j][i] = ds;
                    if (Math.Abs(ds) > max[i])
                    {
                        max[i] = Math.Abs(ds);
                    }
                    s_2 = s_1;
                    s_1 = s_0;
                }
                if (max[i] < min)
                {
                    min = max[i];
                    predict = i;
                }
                if (min <= 7)
                {
                    predict = 0;
                    break;
                }
            }
            _s_1 = s_1;
            _s_2 = s_2;
            for (int i = 0; i < 28; ++i)
            {
                d_samples[i] = buffer[i][predict];
            }
            int min2 = (int)min, mask = 0x4000;
            factor = 0;
            while (factor < 12)
            {
                if ((mask & (min2 + (mask>>3))) != 0)
                {
                    break;
                }
                factor++;
                mask >>= 1;
            }
        }

        private static void PackSamples(double[] d_samples, short[] v_samples, int predict, int factor)
        {
            double s_1 = 0.0, s_2 = 0.0;

            for (int i = 0; i < 28; ++i)
            {
                double s_0 = d_samples[i] + s_1 * f[predict][0] + s_2 * f[predict][1];
                double ds = s_0 * (1 << factor);
                int di = (int)(((int)ds + 0x800) & 0xfffff000);
                if (di > short.MaxValue)
                    di = short.MaxValue;
                else if (di < short.MinValue)
                    di = short.MinValue;
                v_samples[i] = (short)di;
                di >>= factor;
                s_2 = s_1;
                s_1 = di - s_0;
            }
        }
    }
}
