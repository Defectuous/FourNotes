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
        { return "1.0.1.17"; }
        public static string GetPluginDescription()
        { return "4 Notes: Party/Raid Songcraft Buffs Plugin"; }

        
        private bool _followMode = true;
        private Double _followRange = 10.0;
                // Do not EDIT Below this line 
        // ##########################################################
        private Creature _leader = null;
        
        //Call on plugin start
        public void PluginRun()
        {
            ClearLogs();
        
            // Starting Threads
            Log(Time() + "[INFO]: STARTING 4 NOTES");
            _leader = getPartyLeaderObj();    
            
            Thread followThread = new Thread(new ThreadStart(FollowTheLeader));
            
            if (_leader == null || _leader == me)
            { Log(Time() + "[WARNING]: Please set a Leader other than yourself or join a Party/Raid"); } 
                else  { 
                    if (_followMode == true){ followThread.Start(); }
                    Songs();
                    if (_followMode == true){ followThread.Abort(); }
                }
            Log(Time() + "[INFO]: ENDING 4 NOTES");
            
        }
        
        // Follow the Leader
        public void FollowTheLeader()
        {
            while(true) {
                Log(Time() + me.dist(_leader));
                    
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

                if (skillCooldown("[Perform] Bulwark Ballad") == 0)
                {
                    UseSkill("[Perform] Bulwark Ballad");
                    Log(Time() + "[INFO]: Casting Bulwark Ballad");
                    Thread.Sleep(2000);
                }

                if (skillCooldown("[Perform] Bloody Chantey") == 0)
                {
                    UseSkill("[Perform] Bloody Chantey");
                    Log(Time() + "[INFO]: Casting Bloody Chantey");
                    Thread.Sleep(2000);
                }

                if (skillCooldown("[Perform] Ode to Recovery") == 0)
                {
                    UseSkill("[Perform] Ode to Recovery");
                    Log(Time() + "[INFO]: Casting Ode to Recovery");
                    Thread.Sleep(2000);
                }

                if (skillCooldown("[Perform] Quickstep") == 0)
                {
                    UseSkill("[Perform] Quickstep");
                    Log(Time() + "[INFO]: Casting Quickstep");
                    Thread.Sleep(2000);
                }
                Log(Time() + "[INFO]: Sleeping for 22 Seconds");
                Thread.Sleep(21500);
                Log(Time() + "[INFO]: Next Rotation");
                
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
