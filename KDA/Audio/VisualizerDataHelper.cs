using System;
using System.Linq;
using System.Numerics;

namespace KDA.Audio
{
    public class VisualizerDataHelper
    {
        private DateTime lastTime;
        private readonly SecondOrderDynamicsForArray dynamics;

        /// <summary>
        /// 采样数据
        /// </summary>
        public double[] SampleData { get; }

        public VisualizerDataHelper(int waveDataSize)
        {
            if (!Get2Flag(waveDataSize))
            {
                throw new ArgumentException("长度必须是 2 的 n 次幂");
            }

            //_m = (int)Math.Log2(waveDataSize);
            lastTime = DateTime.Now;
            SampleData = new double[waveDataSize];
            dynamics = new SecondOrderDynamicsForArray(1, 1, 1, 0, waveDataSize / 2);
        }

        /// <summary>
        /// 判断是否是 2 的整数次幂
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static bool Get2Flag(int num)
        {
            if (num < 1)
            {
                return false;
            }

            return (num & (num - 1)) == 0;
        }

        public void PushSampleData(double[] waveData)
        {
            if (waveData.Length > SampleData.Length)
            {
                Array.Copy(waveData, waveData.Length - SampleData.Length, SampleData, 0, SampleData.Length);
            }
            else
            {
                Array.Copy(SampleData, waveData.Length, SampleData, 0, SampleData.Length - waveData.Length);
                Array.Copy(waveData, 0, SampleData, SampleData.Length - waveData.Length, waveData.Length);
            }
        }

        /// <summary>
        /// 获取频谱数据 (数据已经删去共轭部分)
        /// </summary>
        /// <returns></returns>
        public double[] GetSpectrumData()
        {
            DateTime now = DateTime.Now;
            double deltaTime = (now - lastTime).TotalSeconds;
            lastTime = now;

            int len = SampleData.Length;
            Complex[] data = new Complex[len];

            for (int i = 0; i < len; i++)
            {
                data[i] = new Complex(SampleData[i], 0);
            }


            FftSharp.FFT.Forward(data);
            int halfLen = len / 2;
            double[] spectrum = new double[halfLen];           // 傅里叶变换结果左右对称, 只需要取一半
            for (int i = 0; i < halfLen; i++)
            {
                spectrum[i] = data[i].Magnitude / len;
            }

            var window = new FftSharp.Windows.Bartlett();
            window.Create(halfLen);
            window.ApplyInPlace(spectrum, false);

            //return spectrum;
            return dynamics.Update(deltaTime, spectrum);
        }

        /// <summary>
        /// 取指定频率内的频谱数据
        /// </summary>
        /// <param name="spectrum">源频谱数据</param>
        /// <param name="sampleRate">采样率</param>
        /// <param name="frequency">目标频率</param>
        /// <returns></returns>
        public static double[] TakeSpectrumOfFrequency(double[] spectrum, double sampleRate, double frequency)
        {
            double frequencyPerSampe = sampleRate / spectrum.Length;

            int lengthInNeed = (int)Math.Min(frequency / frequencyPerSampe, spectrum.Length);
            double[] result = new double[lengthInNeed];
            Array.Copy(spectrum, 0, result, 0, lengthInNeed);
            return result;
        }

        static double[] GetWeights(int radius)
        {
            double Gaussian(double x) => Math.Pow(Math.E, -4 * x * x);        // 憨批高斯函数

            int len = 1 + (radius * 2);                         // 长度
            int end = len - 1;                                // 最后的索引
            double radiusF = radius;                    // 半径浮点数
            double[] weights = new double[len];                 // 权重

            for (int i = 0; i <= radius; i++)                 // 先把右边的权重算出来
                weights[radius + i] = Gaussian(i / radiusF);
            for (int i = 0; i < radius; i++)                  // 把右边的权重拷贝到左边
                weights[i] = weights[end - i];

            double total = weights.Sum();
            for (int i = 0; i < len; i++)                  // 使权重合为 0
                weights[i] = weights[i] / total;

            return weights;
        }

        static void ApplyWeights(double[] buffer, double[] weights)
        {
            int len = buffer.Length;
            for (int i = 0; i < len; i++)
                buffer[i] = buffer[i] * weights[i];
        }

        /// <summary>
        /// 简单的数据模糊
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="radius">模糊半径</param>
        /// <returns>结果</returns>
        public static double[] MakeSmooth(double[] data, int radius)
        {
            double[] weights = GetWeights(radius);
            double[] buffer = new double[1 + (radius * 2)];

            double[] result = new double[data.Length];
            if (data.Length < radius)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    result[i] = data.Average();
                }
                //Array.Fill(result, data.Average());
                return result;
            }


            for (int i = 0; i < radius; i++)
            {
                for (int n = 0; n < radius + 1; n++)
                {
                    buffer[n] = data[i];
                }
                //Array.Fill(buffer, data[i], 0, radius + 1);      // 填充缺省
                for (int j = 0; j < radius; j++)                 // 
                {
                    buffer[radius + 1 + j] = data[i + j];
                }

                ApplyWeights(buffer, weights);
                result[i] = buffer.Sum();
            }

            for (int i = radius; i < data.Length - radius; i++)
            {
                for (int j = 0; j < radius; j++)                 // 
                {
                    buffer[j] = data[i - j];
                }

                buffer[radius] = data[i];

                for (int j = 0; j < radius; j++)                 // 
                {
                    buffer[radius + j + 1] = data[i + j];
                }

                ApplyWeights(buffer, weights);
                result[i] = buffer.Sum();
            }

            for (int i = data.Length - radius; i < data.Length; i++)
            {
                for (int n = 0; n < radius + 1; n++)
                {
                    buffer[n] = data[i];
                }
                //Array.Fill(buffer, data[i], 0, radius + 1);      // 填充缺省
                for (int j = 0; j < radius; j++)                 // 
                {
                    buffer[radius + 1 + j] = data[i - j];
                }

                ApplyWeights(buffer, weights);
                result[i] = buffer.Sum();
            }

            return result;
        }
    }
}


