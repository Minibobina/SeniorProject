using System;
using System.Collections.Generic;
using System.Linq;

namespace RCLIB2014.Build.Rules
{
    class MaxY : Rule
    {
        public bool Pass(List<Track> track, Track NewTrack, List<int> chunks)
        {
            if (NewTrack.Position.Y > Globals.BUILD_AREA_SIZE_Y)
                return false;
            else
                return true;
        }

        public override string ToString()
        {
            return "MaxY";
        }
    }
}
