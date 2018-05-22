using System;
using System.Collections.Generic;
using System.Text;

namespace Alge.Domain.Models
{
    public class ApplicationModel
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public string Version { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
