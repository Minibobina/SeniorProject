using RCLIB2014.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCLIB2014.Build.Tasks
{
    class BuildStartTracks : Task
    {
        public bool Run(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke)
        {
            CommandHandeler commandHandeler = new CommandHandeler();
            List<Command> commands = new List<Command>();

            //Stright
            for (int i = 0; i < (8 * 3); i++)
            {
                commands.Add(new Command(true, TrackType.Stright, new Orientation(0, 0, 0)));
            }

            //Left
            for (int i = 0; i < (4 * 3); i++)
            {
                commands.Add(new Command(true, TrackType.Left, new Orientation(0, 0, 0)));
            }

            //Stright
            for (int i = 0; i < (28); i++)
            {
                commands.Add(new Command(true, TrackType.Stright, new Orientation(0, 0, 0)));
            }

            return commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);
        }
        public override string ToString()
        {
            return "BuildStartTracks";
        }

    }
}
