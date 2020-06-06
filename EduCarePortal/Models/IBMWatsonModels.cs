using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduCarePortal.Models
{
    public class LanguageModel
    {
        public Language[] languages { get; set; }
    }

    public class Language
    {
        public string language { get; set; }
        public string name { get; set; }
    }

    public class TranslationModel
    {
        public Translation[] translations { get; set; }
        public int word_count { get; set; }
        public int character_count { get; set; }
    }

    public class Translation
    {
        public string translation { get; set; }
    }



    public class TranslateBody
    {
        public string[] text { get; set; }
        public string source { get; set; }
        public string target { get; set; }
    }

}