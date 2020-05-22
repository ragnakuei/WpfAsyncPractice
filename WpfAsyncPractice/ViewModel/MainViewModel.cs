﻿using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using WpfAsyncPractice.Service;

namespace WpfAsyncPractice.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            LoadedCommand           = new RelayCommand(LoadedExecute);
            ClickRunAllAsyncCommand = new RelayCommand(async () => await ClickRunAllAsyncExecuteAsync());
            ClickRunAsync1Command   = new RelayCommand(async () => await ClickRunAsync1ExecuteAsync());
            ClickRunAsync2Command   = new RelayCommand(async () => await ClickRunAsync2ExecuteAsync());

            _run1Service               =  new RunService1();
            _run1Service.UpdateNoEvent += UpdateAsync1ProcessNo;

            _run2Service               =  new RunService2();
            _run2Service.UpdateNoEvent += UpdateAsync2ProcessNo;
        }

        public ICommand LoadedCommand { get; }

        private void LoadedExecute()
        {
            ButtonRunAsync1Content   = RunAsync1;
            ButtonRunAsync2Content   = RunAsync2;
            ButtonRunAllAsyncContent = RunAllAsync;
        }

        #region Button

        #region 1

        private const string RunAsync1  = "Run Async1";
        private const string StopAsync1 = "Stop Async1";

        public ICommand ClickRunAsync1Command { get; }

        private async Task ClickRunAsync1ExecuteAsync()
        {
            if (_run1Service.IsRunning)
            {
                ButtonRunAsync1Content = RunAsync1;
            }
            else
            {
                ButtonRunAsync1Content = StopAsync1;
                await _run1Service.RunAsync(0);
                ButtonRunAsync1Content = RunAsync1;
            }
        }

        private string _buttonRunAsync1Content;

        public string ButtonRunAsync1Content
        {
            get => _buttonRunAsync1Content;
            set => Set(() => ButtonRunAsync1Content, ref _buttonRunAsync1Content, value, true);
        }

        #endregion

        #region 2

        private const string RunAsync2  = "Run Async2";
        private const string StopAsync2 = "Stop Async2";

        public ICommand ClickRunAsync2Command { get; }

        private async Task ClickRunAsync2ExecuteAsync()
        {
            if (_run2Service.IsRunning)
            {
                ButtonRunAsync2Content = RunAsync2;
            }
            else
            {
                ButtonRunAsync2Content = StopAsync2;
                await _run2Service.RunAsync(0);
                ButtonRunAsync2Content = RunAsync2;
            }
        }

        private string _buttonRunAsync2Content;

        public string ButtonRunAsync2Content
        {
            get => _buttonRunAsync2Content;
            set => Set(() => ButtonRunAsync2Content, ref _buttonRunAsync2Content, value, true);
        }

        #endregion

        #region All

        private const string RunAllAsync  = "Run All Async";
        private const string StopAllAsync = "Stop All Async";

        private string _buttonRunAllAsyncContent;

        public string ButtonRunAllAsyncContent
        {
            get => _buttonRunAllAsyncContent;
            set => Set(() => ButtonRunAllAsyncContent, ref _buttonRunAllAsyncContent, value, true);
        }

        public ICommand ClickRunAllAsyncCommand { get; }

        private async Task ClickRunAllAsyncExecuteAsync()
        {
            ButtonRunAllAsyncContent = StopAllAsync;
            var t1 = ClickRunAsync1ExecuteAsync();
            var t2 = ClickRunAsync2ExecuteAsync();
            await Task.WhenAll(t1, t2);
            ButtonRunAllAsyncContent = RunAllAsync;
        }

        #endregion

        #endregion

        #region ProcessNo

        private double _async1ProcessNo;

        public double Async1ProcessNo
        {
            get => _async1ProcessNo;
            set => Set(() => Async1ProcessNo, ref _async1ProcessNo, value, true);
        }

        private double _async1ProcessPercentage;

        public double Async1ProcessPercentage
        {
            get => _async1ProcessPercentage;
            set => Set(() => Async1ProcessPercentage, ref _async1ProcessPercentage, value, true);
        }

        private void UpdateAsync1ProcessNo(object sender, ProcessDto processDto)
        {
            Async1ProcessNo         = processDto.ProcessNo;
            Async1ProcessPercentage = processDto.ProcessPercentage;
        }

        private double _async2ProcessPercentage;
        private double _async2ProcessNo;

        public double Async2ProcessNo
        {
            get => _async2ProcessNo;
            set => Set(() => Async2ProcessNo, ref _async2ProcessNo, value, true);
        }

        public double Async2ProcessPercentage
        {
            get => _async2ProcessPercentage;
            set => Set(() => Async2ProcessPercentage, ref _async2ProcessPercentage, value, true);
        }

        private void UpdateAsync2ProcessNo(object sender, ProcessDto processDto)
        {
            Async2ProcessNo         = processDto.ProcessNo;
            Async2ProcessPercentage = processDto.ProcessPercentage;
        }

        #endregion

        private readonly ARunService _run1Service;
        private readonly ARunService _run2Service;
    }
}