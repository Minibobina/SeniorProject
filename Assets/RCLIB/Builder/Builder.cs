using RCLIB2014.Build.Rules;
using RCLIB2014.Build.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RCLIB2014.Build
{
    public class Builder
    {
        TaskHandler taskHandler;
        TaskResults lastTaskResults;

        public List<string> userRequest;
        public Coaster Coaster;
        public void Start(Coaster _Coaster, List<string> _UserRequest)
        {
            taskHandler = new TaskHandler(_Coaster);
            if(_UserRequest != null)
                userRequest = _UserRequest;
            else
                userRequest = new List<string>();

            Coaster = _Coaster;
            Globals.BuildRules = SetupBuildRules();
            Globals.RemoveRules = SetupRemoveRules();
            Globals.RulesWithFix = SetupTaskForOnRuleFail();

            CreateStartTracks();
        }

        public void Start(Coaster _Coaster)
        {
            taskHandler = new TaskHandler(_Coaster);
                userRequest = new List<string>();

            Coaster = _Coaster;
            Globals.BuildRules = SetupBuildRules();
            Globals.RemoveRules = SetupRemoveRules();
            Globals.RulesWithFix = SetupTaskForOnRuleFail();

            CreateStartTracks();
        }



        #region Public Fuctions

        public Coaster GetCoaster()
        {
            return Coaster;
        }

        public  List<string> UserRequest()
        {
            return userRequest;
        }

        public TaskResults BuildStraight()
        {
            userRequest.Add("BuildStraight()");

            lastTaskResults = taskHandler.Start(new BuildStraight());
            return lastTaskResults;
        }
        public TaskResults BuildLeft()
        {
            userRequest.Add("BuildLeft()");

            lastTaskResults = taskHandler.Start(new BuildLeft());
            return lastTaskResults;
        }

        public TaskResults BuildRight()
        {
            userRequest.Add("BuildRight()");

            lastTaskResults = taskHandler.Start(new BuildRight());
            return lastTaskResults;
        }
        public TaskResults BuildUp()
        {
            userRequest.Add("BuildUp()");

            lastTaskResults = taskHandler.Start(new BuildUp());
            return lastTaskResults;
        }
        public TaskResults BuildDown()
        {
            userRequest.Add("BuildDown()");

            lastTaskResults = taskHandler.Start(new BuildDown());
            return lastTaskResults;
        }
        public TaskResults BuildLoop()
        {
            userRequest.Add("BuildLoop()");

            lastTaskResults = taskHandler.Start(new BuildLoop());
            return lastTaskResults;
        }
        public TaskResults BuildUpward()
        {
            userRequest.Add("BuildUpward()");
            lastTaskResults = taskHandler.Start(new BuildUpward());
            return lastTaskResults;
        }
        public TaskResults BuildDownward()
        {
            userRequest.Add("BuildDownward()");
            lastTaskResults = taskHandler.Start(new BuildDownWard());
            return lastTaskResults;
        }
        public TaskResults BuildFlat()
        {
            userRequest.Add("BuildFlat()");
            lastTaskResults = taskHandler.Start(new BuildFlaten());
            return lastTaskResults;
        }

        public TaskResults BuildLevel()
        {
            userRequest.Add("BuildLevel()");
            lastTaskResults = taskHandler.Start(new BuildLevel());
            return lastTaskResults;
        }
        public TaskResults RemoveChunk()
        {
            userRequest.Add("RemoveChunk()");
            lastTaskResults = taskHandler.Start(new RemoveChunk());
            return lastTaskResults;
        }
        public TaskResults FinshCoaster()
        {
            userRequest.Add("FinshCoaster()");
            lastTaskResults = taskHandler.Start(new BuildToFinshArea());
            return lastTaskResults;
        }
        public TaskResults LastTaskResults()
        {
            return lastTaskResults;
        }
        #endregion

        #region Setup
        private void CreateStartTracks()
        {
            if (!Coaster.GetCurrentTracksStarted)
            {
                userRequest.Add("CreateStartTracks()");
                lastTaskResults = taskHandler.Start(new BuildStartTracks());
                if (lastTaskResults.Pass)
                    Coaster.SetTracksStarted = true;
            }
        }
        private List<Rule> SetupBuildRules()
        {
            List<Rule> buildRules = new List<Rule>();
            buildRules.Add(new MinX());
            buildRules.Add(new MaxX());
            buildRules.Add(new MinY());
            buildRules.Add(new MaxY());
            buildRules.Add(new MinZ());
            buildRules.Add(new AngleCheck());
            buildRules.Add(new Collison());

            return buildRules;
        }


        private List<Rule> SetupRemoveRules()
        {
            List<Rule> removeRules = new List<Rule>();

            removeRules = new List<Rule>();
            removeRules.Add(new StartTracksCheck());

            return removeRules;
        }
        private List<RuleWithFix> SetupTaskForOnRuleFail()
        {
            //NOTE: Not All Rules have a fix task. If they do, add the rule, and there fix task

            List<RuleWithFix> rulesWithFix = new List<RuleWithFix>();

            rulesWithFix.Add(new RuleWithFix(new MinX(), new FixMinX()));
            rulesWithFix.Add(new RuleWithFix(new MaxX(), new FixMaxX()));
            rulesWithFix.Add(new RuleWithFix(new MinY(), new FixMinY()));
            rulesWithFix.Add(new RuleWithFix(new MaxY(), new FixMaxY()));
            rulesWithFix.Add(new RuleWithFix(new MinZ(), new FixMinZ()));
            rulesWithFix.Add(new RuleWithFix(new Collison(), new FixTrackCollison()));

            return rulesWithFix;
        }
        #endregion



    }
}
