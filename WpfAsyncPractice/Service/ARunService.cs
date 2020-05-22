using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WpfAsyncPractice.Service
{
    public abstract class ARunService
    {
        public abstract Task<int> RunAsync(int no);

        protected abstract double TotalCount { get; set; }

        public event EventHandler<ProcessDto> UpdateNoEvent;

        public bool IsRunning { get; protected set; }

        private int _no;

        protected int No
        {
            get => _no;
            set
            {
                _no = value;

                var processDto = new ProcessDto
                                 {
                                     ProcessNo         = _no,
                                     ProcessPercentage = _no / TotalCount
                                 };

                UpdateNoEvent?.Invoke(this, processDto);
            }
        }

        protected async Task<int> 機板進料(int no)
        {
            Debug.WriteLine("機板進料");
            await Task.Delay(9);
            No = ++no;
            return No;
        }

        protected async Task<int> 產品底座模組_掃描(int no)
        {
            Debug.WriteLine("產品底座模組_掃描");
            await Task.Delay(9);
            No = ++no;
            return No;
        }

        protected async Task<int> 手臂模組(int no)
        {
            Debug.WriteLine("手臂模組");
            await Task.Delay(9);
            No = ++no;
            return No;
        }

        protected async Task<int> AOI(int no)
        {
            Debug.WriteLine("AOI");
            await Task.Delay(9);
            No = ++no;
            return No;
        }

        protected async Task<int> 晶片模組(int no)
        {
            Debug.WriteLine("晶片模組");
            await Task.Delay(9);
            No = ++no;
            return No;
        }

        protected async Task<int> 產品底座模組(int no)
        {
            Debug.WriteLine("產品底座模組");
            await Task.Delay(9);
            No = ++no;
            return No;
        }

        protected async Task<int> 機板退料(int no)
        {
            Debug.WriteLine("機板退料");
            await Task.Delay(9);
            No = ++no;
            return No;
        }
    }
}