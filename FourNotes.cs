using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using ArcheBuddy.Bot.Classes;

namespace ArcheAgeFourNotes
{
    public class FourNotes : Core
    {
        public static string GetPluginAuthor()
        { return "Defectuous"; }
        public static string GetPluginVersion()
        { return "1.0.1.19"; }
        public static string GetPluginDescription()
        { return "4 Notes: Party/Raid Songcraft Buffs Plugin"; }

        
        private bool _followMode = true;
        private Double _followRange = 5.0;
        private Creature _leader = null;
        
        // Do not EDIT Below this line 
        // ##########################################################
        //Call on plugin start
        public void PluginRun()
        {
            ClearLogs();
            Log(Time() + "[INFO]: STARTING 4 NOTES");
            _leader = getPartyLeaderObj();    

            // Starting Threads
            Thread followThread = new Thread(new ThreadStart(FollowTheLeader));
            
            if (_leader == null || _leader == me)
            { Log(Time() + "[WARNING]: Please set a Leader other than yourself or join a Party/Raid"); } 
                else  { 
                    if (_followMode == true){ followThread.Start(); Log(Time() + "[INFO]: Starting Following Thread"); }
                    Songs();
                    if (_followMode == true){ followThread.Abort(); Log(Time() + "[INFO]: Ending Following Thread"); }
                }
            Log(Time() + "[INFO]: ENDING 4 NOTES");
            
        }
        
        // Follow the Leader
        public void FollowTheLeader()
        {
            while(true) {
                Log(Time() + "Distance to Leader:" +me.dist(_leader));
                Thread.Sleep(3500);     
                if (me.dist(_leader) >= _followRange)
                { moveToPlayer(_leader); Thread.Sleep(3500); }
            } 
                
        }
        
        public void moveToPlayer(Creature obj)
        {
            ComeTo(obj, _followRange);
        }
        
        // Play that Funky Music White Boy
        public string Songs()
        {
            Log(Time() + "[INFO]: Starting Rotation");
            while (true)
            {

                if (isSkillLearned("[Perform] Bulwark Ballad") && skillCooldown("[Perform] Bulwark Ballad") == 0)
                {
                    UseSkill("[Perform] Bulwark Ballad");
                    Log(Time() + "[INFO]: Casting Bulwark Ballad");
                } Thread.Sleep(2000);

                if (isSkillLearned("[Perform] Bloody Chantey") && skillCooldown("[Perform] Bloody Chantey") == 0)
                {
                    UseSkill("[Perform] Bloody Chantey");
                    Log(Time() + "[INFO]: Casting Bloody Chantey");
                } Thread.Sleep(2000);

                if (isSkillLearned("[Perform] Ode to Recovery") && skillCooldown("[Perform] Ode to Recovery") == 0)
                {
                    UseSkill("[Perform] Ode to Recovery");
                    Log(Time() + "[INFO]: Casting Ode to Recovery");
                } Thread.Sleep(2000);

                if (isSkillLearned("[Perform] Quickstep") && skillCooldown("[Perform] Quickstep") == 0)
                {
                    UseSkill("[Perform] Quickstep");
                    Log(Time() + "[INFO]: Casting Quickstep");
                } Thread.Sleep(2000);
                
                Log(Time() + "[INFO]: Sleeping for 22 Seconds");
                Thread.Sleep(21500);
                Log(Time() + "[INFO]: Starting Next Rotation");
                
            }
        }
        // Looting The Dead
        
        // Roll Management
        
        public string Time()
        {
            string A = DateTime.Now.ToString("[hh:mm:ss] ");
            return A;
        }
        
        //Call on plugin stop
        public void PluginStop()
        {

        }
    }
}
