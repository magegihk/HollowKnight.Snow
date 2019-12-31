using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modding;

namespace HollowKnight.Snow
{
    public class SaveSettings : ModSettings { }

    public class GlobalSettings : ModSettings
    {
        public string Particles
        {
            get => GetString("Particles");
            set => SetString(value);
        }
    }
}
