﻿using RL.Properties;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace AssaultCubeHack
{
    class Clsini   // revision 11
    {
        public static Clsini INIConfig = new Clsini("profiles/" + Settings.Default.Profiles + "/config.ini");
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public Clsini(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}

public sealed class IniParser
{
    private static Regex SectionRegex = new Regex(@"\[(?<section>[^\n\[\]]+)\]\n*(?<valuelist>(.(?!\[[^\n\[\]]+\]))*)", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
    private static Regex ValueRegex = new Regex(@"(?<valuename>[^=\n]+)=(?<value>[^\n]*)", RegexOptions.CultureInvariant | RegexOptions.Compiled);

    /// <summary>
    ///     ''' Parses an .ini-file.
    ///     ''' </summary>
    ///     ''' <param name="FileName">The path to the file to parse.</param>
    ///     ''' <remarks></remarks>
    public static Dictionary<string, Dictionary<string, string>> ParseFile(string STRING01)
    {
        return IniParser.Parse(STRING01);
    }

    /// <summary>
    ///     ''' Parses a text of .ini-format.
    ///     ''' </summary>
    ///     ''' <param name="Data">The text to parse.</param>
    ///     ''' <remarks></remarks>
    public static Dictionary<string, Dictionary<string, string>> Parse(string Data)
    {
        Dictionary<string, Dictionary<string, string>> Result = new Dictionary<string, Dictionary<string, string>>(); // (Section, (Value name, Value))
        MatchCollection Sections = SectionRegex.Matches(Data);

        // Iterate each section.
        foreach (Match SectionMatch in Sections)
        {
            Dictionary<string, string> Section = new Dictionary<string, string>();
            string SectionName = SectionMatch.Groups["section"].Value;
            MatchCollection Values = ValueRegex.Matches(SectionMatch.Groups["valuelist"].Value);

            if (Result.ContainsKey(SectionName) == true)
            {
                // A section by this name already exists.
                int i = 1;

                // Append a number to the section name until a unique name is found.
                while (Result.ContainsKey(SectionName + i))
                    i += 1;

                Result.Add(SectionName + i, Section);
            }
            else
                // A section by this name does not exist.
                Result.Add(SectionName, Section);

            // Iterate each value of this section.
            foreach (Match ValueMatch in Values)
            {
                string ValueName = ValueMatch.Groups["valuename"].Value;
                string Value = ValueMatch.Groups["value"].Value;

                if (Section.ContainsKey(ValueName) == true)
                {
                    // A value by this name already exists.
                    int i = 1;

                    // Append a number to the value name until a unique name is found.
                    while (Section.ContainsKey(ValueName + i))
                        i += 1;

                    Section.Add(ValueName + i, Value);
                }
                else
                    // A value by this name does not exist.
                    Section.Add(ValueName, Value);
            }
        }

        return Result;
    }
}