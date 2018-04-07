using CommandLine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
// ReSharper disable RedundantNameQualifier

//ChangeDT.exe --path C:\Users\Administrator\OneDrive\PT --day 1 --month 1 --year 2000

namespace ChangeDT
{
    class Program
    {
        private static ParsedOptions _options;

        // Run as Administrator
        // Install-Package CommandLineParser
        private static void Main(string[] args)
        {
            //Parser.Default.ParseArguments<Options>(args)
            //    .WithParsed(RunOptionsAndReturnExitCode)
            //    .WithNotParsed(HandleParseError);

            var errors = new List<CommandLine.Error>();

            Parser.Default.ParseArguments<ParsedOptions>(args)
                .WithParsed(x => _options = x)
                .WithNotParsed(x => errors = x.ToList());

            if (errors.Any())
            {
                errors.ForEach(x => Console.WriteLine(x.ToString()));
                WaitForKey(ConsoleKey.Q);
                return;
            }

            //string dir = @"C:\TMP";
            string dir = _options.Path;
            //var dtime = new DateTime(2000, 1, 1);
            var dtime = new DateTime(_options.Year, _options.Month, _options.Day);

            //var folders = Directory.GetDirectories(dir, "*", SearchOption.AllDirectories);
            //foreach (var item in folders)
            //{
            //    try
            //    {
            //        Console.WriteLine(item);
            //        Directory.SetCreationTimeUtc(item, dtime);
            //        Directory.SetLastAccessTimeUtc(item, dtime);
            //        Directory.SetLastWriteTimeUtc(item, dtime);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Exception in " + MethodBase.GetCurrentMethod().Name + " - " + ex);
            //    }
            //}

            //var files = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories);
            //foreach (var item in files)
            //{
            //    try
            //    {
            //        var info = new FileInfo(item);
            //        Console.WriteLine(info.Name + " - " + info.Length);

            //        Console.WriteLine(item);
            //        Directory.SetCreationTimeUtc(item, dtime);
            //        Directory.SetLastAccessTimeUtc(item, dtime);
            //        Directory.SetLastWriteTimeUtc(item, dtime);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Exception in " + MethodBase.GetCurrentMethod().Name + " - " + ex);
            //    }
            //}

            var fileSystemEntries = Directory.GetFileSystemEntries(dir, "*.*", SearchOption.AllDirectories);
            foreach (var item in fileSystemEntries)
            {
                try
                {
                    if (_options.Verbose)
                    {
                        Console.WriteLine(item);
                    }

                    Directory.SetCreationTimeUtc(item, dtime);
                    Directory.SetLastAccessTimeUtc(item, dtime);
                    Directory.SetLastWriteTimeUtc(item, dtime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception in " + MethodBase.GetCurrentMethod().Name + " - " + ex);
                    break;
                }
            }

            WaitForKey(ConsoleKey.Q);
        }

        static void WaitForKey(ConsoleKey key)
        {
            //Console.ReadKey(); //close when someone presses any key
            //Console.ReadLine(); //the user types something and presses enter
            Console.Write($"Press '{key}' to exit.\n");
            while (Console.ReadKey(true).Key != key)
            {
                Debug.WriteLine(key);
            }
        }

        //static void RunOptionsAndReturnExitCode(ParsedOptions o) => _options = o;

        //static void HandleParseError(IEnumerable<Error> errs)
        //{
        //    Console.WriteLine("HandleParseError: " + errs);
        //}
    }
}
