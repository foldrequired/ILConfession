using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Settings.JwtSettings
{
    public class JwtOptions
    {
        public string TokenSecret { get; set; }
        public TimeSpan TokenLifecycle { get; set; }
    }
}
