﻿using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows.Automation;
using Winium.Cruciatus.Core;
using Winium.Cruciatus.Extensions;

namespace winium_test
{
    public class DisposeableApplication : IDisposable
    {
        public DisposeableApplication(string executableFilePath)
        {
            this.App = new Winium.Cruciatus.Application(executableFilePath);
            this.App.Start();
        }

        public void Dispose()
        {
            this.App.Close();
        }

        public Winium.Cruciatus.Application App { get; private set; }
    }

    class Program
    {
        private const string calcPath = @"C:\\Windows\\System32\\calc.exe";

        static void Main(string[] args)
        {
            initializeLoggingRule();
            checkAlreadyRunning();

            using (var calc = new DisposeableApplication(calcPath))
            {
                var win = Winium.Cruciatus.CruciatusFactory.Root;

                foreach (var ch in String.Join("", args))
                {
                    win.FindElementByUid(keyIds[ch].ToString()).Click();
                }

                // 計算結果を表示する
                win.FindElementByUid("121").Click();                      // =
                var result = win.FindElementByUid("150").Properties.Name; //結果のテキスト
                Console.Write(result);
            }
        }

        // 電卓のキーとidの対応表
        private static readonly IDictionary<char, int> keyIds = new Dictionary<char, int>()
        {
            {'.', 84},
            {'/', 91},
            {'*', 92},
            {'+', 93},
            {'-', 94},
            {'=', 121},
            {'0', 130},
            {'1', 131},
            {'2', 132},
            {'3', 133},
            {'4', 134},
            {'5', 135},
            {'6', 136},
            {'7', 137},
            {'8', 138},
            {'9', 139},
        };


        // ログをコマンド用に設定する
        // Infoは出力しない
        // Warn以上を標準エラー出力に出す
        static void initializeLoggingRule()
        {
            var fact = Winium.Cruciatus.CruciatusFactory.Logger.Factory;

            var target = new NLog.Targets.ConsoleTarget();
            target.Error = true;
            var rule = new NLog.Config.LoggingRule("*", NLog.LogLevel.Warn, target);

            fact.Configuration.LoggingRules.Clear();
            fact.Configuration.LoggingRules.Add(rule);
            fact.ReconfigExistingLoggers();
        }

        // 対象のアプリが起動してないか？
        static void checkAlreadyRunning()
        {
            var procname = System.IO.Path.GetFileNameWithoutExtension(calcPath);
            if (System.Diagnostics.Process.GetProcessesByName(procname).Any())
            {
                Console.Error.WriteLine("error!, already running calc process");
                Environment.Exit(1);
            }
        }
    }
}
