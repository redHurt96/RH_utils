using UnityEngine;

namespace RH.Utilities.FPSCounting
{
    public class FpsCounter
    {
        public int AverageFPS { get; private set; }
        public int HighestPFS { get; private set; }
        public int LowestFPS { get; private set; }

        public bool isCounting { get; private set; }

        private int _bufferLength = 60;
        private int _fpsBufferIndex = 0;
        private int[] _fpsBuffer;
        
        public FpsCounter(int bufferLenght = 60)
        {
            _bufferLength = bufferLenght;

            InitBuffer();
            InitValues();
        }

        public void StartCounting() => isCounting = true;
        public void StopCounting() => isCounting = false;

        public void Calculate()
        {
            if (!isCounting)
                return;

            UpdateBuffer();
            CountFps();
        }

        private void InitValues()
        {
            HighestPFS = 0;
            LowestFPS = int.MaxValue;
        }

        private void InitBuffer()
        {
            if (_bufferLength <= 0)
                _bufferLength = 1;

            _fpsBuffer = new int[_bufferLength];
        }

        private void UpdateBuffer()
        {
            _fpsBuffer[_fpsBufferIndex] = (int)(1f / Time.unscaledDeltaTime);
            _fpsBufferIndex++;

            if (_fpsBufferIndex == _bufferLength)
                _fpsBufferIndex = 0;
        }

        private void CountFps()
        {
            int sum = 0;
            int lowest = int.MaxValue;
            int highest = 0;

            foreach (var fps in _fpsBuffer)
            {
                sum += fps;

                if (fps < lowest)
                    lowest = fps;
                if (fps > highest)
                    highest = fps;
            }

            AverageFPS = sum / _bufferLength;
            LowestFPS = lowest;
            HighestPFS = highest;
        }
    }
}