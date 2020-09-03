using System;
using System.IO;
using CommandLine;
using Repopulator;
using YamlDotNet.Serialization;

namespace RepopulatorCli
{
    class RepopulatorCliOptions
    {
        
        [Option('y', Required = true, HelpText = "The yaml file describing which directory to process with what csv etc")]
        public string YamlFile { get; set; }
    }
    
    class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<RepopulatorCliOptions>(args).MapResult(
                opts => Run(opts),
                errs => -100);
        }

        private static int Run(RepopulatorCliOptions opts)
        {
            var fi = new FileInfo(opts.YamlFile);

            if (!fi.Exists)
            {
                Console.WriteLine($"File '{fi.FullName}' did not exist");
                return -1;
            }

            try
            {
                var des = new Deserializer();
                var state = des.Deserialize<DicomRepopulatorOptions>(File.ReadAllText(fi.FullName));

                using var populator = new DicomRepopulatorProcessor();
                return populator.Process(state);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return -555;
            }
        }
    }
}
