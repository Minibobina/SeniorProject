﻿using RCLIB2014.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCLIB2014.Build.Tasks
{
    static public class  BuildToXY
    {
        public static bool Run(List<Track> _tracks, List<int> _chunks, ref bool _tracksStarted, ref bool _tracksFinshed, ref Rule _ruleBroke, float XPosition, float YPosition, float withIn)
        {
            CommandHandeler commandHandeler = new CommandHandeler();

            List<Command> commands = new List<Command>();

            //Copy
            Coaster coaster = new Coaster();
            coaster.SetTracks = _tracks;
            coaster.SetChunks = _chunks;
            coaster.SetTracksStarted = _tracksStarted;
            coaster.SetTracksFinshed = _tracksFinshed;

            List<Track> tracks = coaster.GetCurrentTracks;
            List<int> chunks = coaster.GetCurrentChunks;
            bool tracksStarted = coaster.GetCurrentTracksStarted;
            bool tracksFinshed = coaster.GetCurrentTracksFinshed;
            Rule ruleBroke = _ruleBroke;

            bool passed = false;

            if (GoToXY(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, XPosition, YPosition, withIn))
            {
                passed = GoToXY(_tracks, _chunks, ref _tracksStarted, ref _tracksFinshed, ref _ruleBroke, XPosition, YPosition, withIn);
            }

          
    
            return passed;
        }
        public new static string ToString()
        {
            return "BuildToXY";
        }

        static bool GoToXY(List<Track> tracks, List<int> chunks, ref bool tracksStarted, ref bool tracksFinshed, ref Rule ruleBroke, float XPosition, float YPosition, float withIn)
        {
            CommandHandeler commandHandeler = new CommandHandeler();
            List<Command> commands = new List<Command>();
            bool buildPass = true;

            bool firstStrightTrack = true;
            float last = 0;
            float lastDiffernce = 0;
            float yawGoal = 0;
            buildPass = BuildToPitch.Run(tracks, chunks, ref tracksStarted, ref tracksFinshed, ref ruleBroke, 0);
            if (!buildPass)
                return false;

            while (!((tracks.Last().Position.X < XPosition + (withIn / 2) && tracks.Last().Position.X > XPosition - (withIn / 2)) && (tracks.Last().Position.Y < YPosition + (withIn / 2) && tracks.Last().Position.Y > YPosition - (withIn / 2))) && buildPass)
            {
                float x = XPosition - tracks.Last().Position.X;
                float y = YPosition - tracks.Last().Position.Y;

                //Determine Best Yaw
                yawGoal = Convert.ToSingle(Math.Atan2((double)y, (double)x) * 180 / Math.PI);

                if (yawGoal < 0)
                    yawGoal = yawGoal + 360;

                //Get YawGoal To Nearest Angle Game Can Handle
                int totalAdjustments = (int)(yawGoal / Globals.STANDARD_ANGLE_CHANGE);

                if((yawGoal % 15) > Globals.STANDARD_ANGLE_CHANGE/2)
                    totalAdjustments++;

                yawGoal = totalAdjustments * Globals.STANDARD_ANGLE_CHANGE;

                if (tracks.Last().Orientation.Yaw == yawGoal)
                {
                    commands.Clear();
                    commands.Add(new Command(true, TrackType.Stright, new Orientation(0, 0, 0)));
                    buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);

                    float xDistance = tracks.Last().Position.X - XPosition;
                    float YDistance = tracks.Last().Position.Y - YPosition;

                    float differnce = Math.Abs(xDistance + xDistance);
                    if (!firstStrightTrack)
                    {
                        //This Means You Passed The Goal Point, This could have been done by turning, Or After the Fact. But You Are now going the wrong way.
                        if (differnce > lastDiffernce)
                            return false;
                    }
                    else
                        firstStrightTrack = true;

                    last = tracks.Last().Position.X + tracks.Last().Position.Y;
                    lastDiffernce = differnce;

                }
                else
                {
                    TrackType type = new TrackType();
              
                    if (tracks.Last().Orientation.Yaw - yawGoal > 0)
                    {
                        if (Math.Abs(tracks.Last().Orientation.Yaw - yawGoal) < 180)
                            type = TrackType.Right;
                        else
                            type = TrackType.Left;

                    }
                    else
                    {
                        if (Math.Abs(yawGoal - tracks.Last().Orientation.Yaw) < 180)
                            type = TrackType.Left;
                        else
                            type = TrackType.Right;

                    }

                    commands.Clear();
                    commands.Add(new Command(true, type, new Orientation(0, 0, 0)));
                    buildPass = commandHandeler.Run(commands, tracks, chunks, tracksStarted, tracksFinshed, ref ruleBroke);


                  
                }

            }
            if (tracks.Last().Position.X < XPosition + (withIn / 2) && tracks.Last().Position.X > XPosition - (withIn / 2) && (tracks.Last().Position.Y < YPosition + (withIn / 2) && tracks.Last().Position.Y > YPosition - (withIn / 2)))
                return true;
            else
                return false;

        }
    }
}
