using System;
using UnityEngine;
using UnityEngine.UI;

namespace RH.Utilities.FPSCounting
{
    public class FpsDisplay : MonoBehaviour
    {
        [SerializeField] private Text _highestFpsLabel;
        [SerializeField] private Text _averageFpsLabel;
        [SerializeField] private Text _lowestFpsLabel;

        [Tooltip("Количество созданных строк для показателя FPS (для оптимизации).")]
        [Space, SerializeField] private int _createdValuesCount = 100;
        [SerializeField] private int _maxShowingFps = MAX_SHOWING_FPS_DEFAULT_VALUE;

        [Space, SerializeField] private FpsColor[] _colors;

        [Space, SerializeField] private int _fpsBufferLenght = 60;

        private string[] _valuesArray;
        private FpsCounter _counter;

        private bool _isInitialized = false;

        private const int MAX_SHOWING_FPS_DEFAULT_VALUE = -1;

        public void ShowFps() => _counter.StartCounting();
        public void HideFps() => _counter.StopCounting();

        private void Update()
        {
            if (_counter.isCounting)
            {
                _counter.Calculate();
                Display();
            }
        }

        public void Init()
        {
            if (_isInitialized)
                return;
            _isInitialized = true;

            _counter = new FpsCounter(_fpsBufferLenght);
            CreateValuesArray();
        }

        private void Display()
        {
            TrySetValue(_averageFpsLabel, _counter.AverageFPS);
            TrySetValue(_highestFpsLabel, _counter.HighestPFS);
            TrySetValue(_lowestFpsLabel, _counter.LowestFPS);
        }

        private void TrySetValue(Text text, int value)
        {
            value = TryClampValue(value);

            if (text != null)
            {
                SetValue(text, value);
                UpdateValueColor(text, value);
            }
        }

        private int TryClampValue(int value)
        {
            if (_maxShowingFps == MAX_SHOWING_FPS_DEFAULT_VALUE)
                return value;
            else
                return Mathf.Min(value, _maxShowingFps);
        }

        private void SetValue(Text text, int value)
        {
            var stringValue = value < _createdValuesCount ? _valuesArray[value] : value.ToString();
            text.text = stringValue;
        }

        private void CreateValuesArray()
        {
            if (_createdValuesCount <= 0)
                _createdValuesCount = 0;

            _valuesArray = new string[_createdValuesCount];

            for (int i = 0; i < _createdValuesCount; i++)
                _valuesArray[i] = $"{i}";
        }

        private void UpdateValueColor(Text text, int value)
        {
            if (_colors == null)
                return;

            foreach (var fpsColor in _colors)
                if (value <= fpsColor.borderValue)
                    text.color = fpsColor.color;
        }

        [Serializable]
        private class FpsColor
        {
            public Color color;
            public int borderValue;
        }
    }
}