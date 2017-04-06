using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RCLIB2014.Build
{
    public interface Rule
    {
        bool Pass(List<Track> track, Track newTrack, List<int> chunks);

    }
}
