using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using HobZonePOC.Models;
using Newtonsoft.Json;

namespace HobZonePOC.ViewModels
{

    public partial class HobViewModel
    {
        public string Tile { get; set; } = "Test";

        public ZoneInfo GetZoneInfo()
        {
            var assembly = typeof(HobViewModel).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("HobZonePOC.Data.zones2.json");
            using (StreamReader sr = new StreamReader(stream))
            {
                var json = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<ZoneInfo>(json);
            }
        }

    }



}
