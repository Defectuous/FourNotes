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
        { return "1.0.1.26"; }
        public static string GetPluginDescription()
        { return "4 Notes: Party/Raid Songcraft Buffs Plugin"; }
        
        // [ Configuration Section Start ]
        
        private bool _followMode = true; // To follow the party / raid leader
        private Double _followRange = 5.0;
        string _soup = "Hearty Soup"; // Mana Food
        int    _mana = 60; // Percentage to eat food at
        
        // pick and choose your spells if you want to
        private bool _BulwarkBallad  = true;
        private bool _BloodyChantey = true;
        private bool _OdetoRecovery = true;
        private bool _Quickstep = true;
        
        // [ Configuration Section End ]
        
        // Do not EDIT Below this line 
        // ##########################################################
        //Call on plugin start
        private Creature _leader = null;
        
        public void PluginRun()
        {
            ClearLogs();
            Log(Time() + "[INFO]: STARTING 4 NOTES");
            _leader = getPartyLeaderObj();    

            // Starting Threads
            Thread followThread = new Thread(new ThreadStart(FollowTheLeader));
            //Thread buffThread   = new Thread(new ThreadStart(BuffCheck));
            
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
                Thread.Sleep(1000);     
                if (me.dist(_leader) >= _followRange)
                { Log(Time() + "[INFO]: Distance to Leader: " + me.dist(_leader)); moveToPlayer(_leader); }
                
            } 
                
        }
        
        public void moveToPlayer(Creature obj)
        {
            ComeTo(obj, _followRange);
        }
        
        public void ManaCheck()
        {
            if (mpp(me) <= _mana && itemCooldown(_soup) == 0){
                UseItem(_soup); 
                Log(Time() + "[INFO]: Mana Below " + _mana + " Percent. Consuming " + _soup + " for mana");
                } Log(Time() + "[INFO]: Mana at " + mpp(me) + "%");
        }

        //public void BuffCheck()
        //{}
            
        // Play that Funky Music White Boy
        public string Songs()
        {
            Log(Time() + "[INFO]: Starting Rotation");
            while (true)
            {

                if (isSkillLearned("[Perform] Bulwark Ballad") == true && skillCooldown("[Perform] Bulwark Ballad") == 0 && _BulwarkBallad == true)
                {
                    UseSkill("[Perform] Bulwark Ballad");
                    Log(Time() + "[INFO]: Casting Bulwark Ballad");
                } Thread.Sleep(2000);

                if (isSkillLearned("[Perform] Bloody Chantey") == true && skillCooldown("[Perform] Bloody Chantey") == 0 && _BloodyChantey == true)
                {
                    UseSkill("[Perform] Bloody Chantey");
                    Log(Time() + "[INFO]: Casting Bloody Chantey");
                } Thread.Sleep(2000);

                if (isSkillLearned("[Perform] Ode to Recovery") == true && skillCooldown("[Perform] Ode to Recovery") == 0 && _OdetoRecovery == true)
                {
                    UseSkill("[Perform] Ode to Recovery");
                    Log(Time() + "[INFO]: Casting Ode to Recovery");
                } Thread.Sleep(2000);

                if (isSkillLearned("[Perform] Quickstep") == true && skillCooldown("[Perform] Quickstep") == 0 && _Quickstep == true)
                {
                    UseSkill("[Perform] Quickstep");
                    Log(Time() + "[INFO]: Casting Quickstep");
                } Thread.Sleep(2000);
                
                ManaCheck();
                Log(Time() + "[INFO]: Next Song Starts in 22 Seconds");
                Thread.Sleep(21500);
                Log(Time() + "[INFO]: Starting Next Rotation");
                
            }
        }
        
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
