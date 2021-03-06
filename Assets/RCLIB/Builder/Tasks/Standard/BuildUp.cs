﻿using RCLIB2014.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RCLIB2014.Build.Tasks
{
    class BuildUp : Task
    {
        public bool Run(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke)
        {
            CommandHandeler commandHandeler = new CommandHandeler();

            List<Command> commands = new List<Command>();

            commands.Add(new Command(true, TrackType.Up, new Orientation(0, 0, 0)));
            commands.Add(new Command(true, TrackType.Up, new Orientation(0, 0, 0)));
            commands.Add(new Command(true, TrackType.Up, new Orientation(0, 0, 0)));

            return commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);

        }
        public override string ToString()
        {
            return "BuildUp";
        }
    }
}
