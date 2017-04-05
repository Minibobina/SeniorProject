using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCLIB2014.Build;
using RCLIB2014.Ride;
using System.Reflection;
using System.Diagnostics;

namespace RCLIB2014
{
    public class RollerCoasterMaker
    {
        public Coaster NewCoaster()
        {
            return new Coaster();
        }

        public Builder Builder(Coaster coaster)
        {
            Builder builder = new Builder();
            List<string> userRequests = new List<string>();
            builder.Start(coaster, userRequests);

            return builder;
        }
        public Rider Rider(Coaster coaster)
        {
            return null;
        }

        public List<Coaster> LoadCoasters()
        {
            return null;
        }
        public Coaster LoadCoaster(string id)
        {
            return null;
        }
        public bool SaveCoaster(Coaster coaster)
        {
            return false;
        }

        public string VersionNum()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }
    }
}
