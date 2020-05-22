﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WpfAsyncPractice.Service
{
    public class RunService2 : ARunService
    {
        protected override double TotalCount { get; set; } = 603.0;

        public override async Task<int> RunAsync(int no)
        {
            IsRunning = true;

            var start = DateTime.Now;

            var result = await 機板進料(no);

            for (int i = 0; i < 100; i++)
            {
                result = await AOI(result);
                result = await 產品底座模組_掃描(result);
            }

            result = await 手臂模組(result);

            for (int i = 0; i < 100; i++)
            {
                result = await AOI(result);

                result = await 晶片模組(result);
                result = await 產品底座模組(result);

                result = await 手臂模組(result);
            }

            result = await 機板退料(result);

            IsRunning = false;

            var end               = DateTime.Now;
            var totalMilliseconds = (end - start).TotalMilliseconds;
            Debug.WriteLine($"totalMilliseconds:{totalMilliseconds}");

            return result;
        }
    }
}