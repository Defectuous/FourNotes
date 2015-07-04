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
        { return "1.0.1.45"; }
        public static string GetPluginDescription()
        { return "4 Notes: Party/Raid Songcraft Buffs Plugin"; }
        
        // [ Configuration Section Start ]
        
        private bool _followMode = true; // To follow the party / raid leader
        private Double _followRange = 5.0; // follow range
        
        // Looting Management  [ Currently This looks very bottish ]
        private bool _deadloot = false; // Looting the Dead so the leader doesn't have to.
        int _lootdist = 25; // The range to go pick up loot from
        
        // Mana Mangment
        private bool _ManaManage = true; // to enable Mana Mangement with food or pots.
        string _soup = "Hearty Soup"; // Mana Food or Pot
        int    _mana = 60; // Percentage to eat food or put up at
        
        // Buffs 
        private bool _BuffChecks  = true; // Check Each Party Member for the following Buffs.
        private bool _HummingbirdDitty   = true; // To give HUmmingbird Ditty Buff to all Party members in range
        private bool _AranzebsBoon   = true; // To give Aranzeb's Boon to all Party members in range
        private bool _HealthLift = true;
        
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
            Log(Time() + "[INFO] STARTING 4 NOTES");
            _leader = getPartyLeaderObj();    

            // Starting Threads
            Thread followThread = new Thread(new ThreadStart(FollowTheLeader));
            
            if (_leader == null || _leader == me)
            { Log(Time() + "[WARN] Please set a Leader other than yourself or join a Party/Raid"); } 
                else  { 
                    if (_followMode == true){ followThread.Start(); Log(Time() + "[INFO]: Starting Following Thread"); }
                    
                    Songs();
                    
                    if (_followMode == true){ followThread.Abort(); Log(Time() + "[INFO]: Ending Following Thread"); }
                }
            Log(Time() + "[INFO] ENDING 4 NOTES");
        }
        
        // Follow the Leader
        public void FollowTheLeader()
        {
            while(true) {
                Thread.Sleep(1000);     
                if (me.dist(_leader) >= _followRange)
                { Log(Time() + "[INFO]: Distance to Leader: " + me.dist(_leader)); moveToPlayer(_leader); }
                if (_deadloot == true) { LootDead(); }
                
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

        // Buff Check
        public void BuffCheck()
        {
            List<Creature> DeadPpl = new List<Creature>(); 
            
            // [ Hummingbird Ditty ]
            if (isSkillLearned(11377) == true && skillCooldown(11377) == 0 && _HummingbirdDitty == true) { 
                foreach (PartyMember member in getPartyMembers()) { 
                    if (!DeadPpl.Contains(member.obj)) { 
                        if (!member.obj.getBuffs().Exists(b => b.id == 462) &&
                            !member.obj.getBuffs().Exists(b => b.id == 463) &&
                            !member.obj.getBuffs().Exists(b => b.id == 464) &&
                            !member.obj.getBuffs().Exists(b => b.id == 465) &&
                            !member.obj.getBuffs().Exists(b => b.id == 466)) { 
                            if (me.dist(member.obj) <=20) { 
                                SetTarget(member.obj); 
                                UseSkill(11377);
                                Log(Time() + "[INFO]: Casting Hummingbird Ditty on " + member.obj.name );
                                Thread.Sleep(2000);
                            } 
                        } 
                    } 
                }
            }
            
            // [ Aranzeb's Boon ]
            if (isSkillLearned(16004) == true && skillCooldown(16004) == 0 && _AranzebsBoon == true) { 
                foreach (PartyMember member in getPartyMembers()) { 
                    if (!DeadPpl.Contains(member.obj))  { 
                        if (!member.obj.getBuffs().Exists(b => b.id == 2955) &&
                            !member.obj.getBuffs().Exists(b => b.id == 2956) &&
                            !member.obj.getBuffs().Exists(b => b.id == 7661)) { 
                            if (me.dist(member.obj) <= 20) { 
                                SetTarget(member.obj); 
                                UseSkill(16004); 
                                Log(Time() + "[INFO]: Casting Aranzeb's Boon on " + member.obj.name );
                                Thread.Sleep(2250);
                            } 
                        } 
                    } 
                } 
            }
            // [DEBUG] Health Lift (Rank 3) => 796
            // [ Health Lift ]
            if (isSkillLearned(11991) == true && skillCooldown(11991) == 0 && _HealthLift == true) { 
                foreach (PartyMember member in getPartyMembers()) { 
                    if (!DeadPpl.Contains(member.obj))  { 
                        if (//!member.obj.getBuffs().Exists(b => b.id == 796) &&
                            !member.obj.getBuffs().Exists(b => b.name == "Health Lift (Rank 1)") &&
                            !member.obj.getBuffs().Exists(b => b.name == "Health Lift (Rank 2)") &&
                            !member.obj.getBuffs().Exists(b => b.name == "Health Lift (Rank 3)") &&
                            !member.obj.getBuffs().Exists(b => b.id == 7655)) { 
                            if (me.dist(member.obj) <= 20) { 
                                SetTarget(member.obj); 
                                UseSkill(11991); 
                                Log(Time() + "[INFO]: Casting Health Lift on " + member.obj.name );
                                Thread.Sleep(2250);
                            } 
                        } 
                    }
                }
            }
        }  
        
        // Looting Dead
        public void LootDead() 
        { 
            foreach (var dead in getCreatures()) 
            { 
                if (dead.dropAvailable && me.dist(dead) <= _lootdist) 
                { 
                    if (me.dist(dead) <= 2) 
                    { PickupAllDrop(dead); }
                    else { 
                        ComeTo(dead, 2); 
                        PickupAllDrop(dead); 
                    } 
                    Thread.Sleep(100); 
                } 
            } 
        }
        
        // Play that Funky Music White Boy
        public string Songs()
        {
            Log(Time() + "[INFO]: Starting BuffCheck & Rotation");
            while (true)
            {
                if (_BuffChecks == true) { BuffCheck(); }
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
                
                if (_ManaManage == true) { ManaCheck(); }
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
