using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AppSettings
    {
        public string LanguageAndRegion { get; set; }
        public WorldCupGender WorldCupGender { get; set; }
    }
}
