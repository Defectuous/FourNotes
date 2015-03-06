using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using ArcheBuddy.Bot.Classes;

//
// Special Thanks to 2face for the following thread " [Plugin] [Example] Autofollower/Healer Base "  
// 


namespace ArcheAgeFourNotes{
    public class FourNotes : Core
   {
       public static string GetPluginAuthor()
       { return "Defectuous"; }
       public static string GetPluginVersion()
       { return "1.1.0.1"; }
       public static string GetPluginDescription()
       { return "Four Notes: Songcraft four songs"; }
       
       // Generic Configuration
       private Creature _leader = null;
       private Double _bardRange = 19.5;
       private Double _followRange = 19.5;

       // Verify you are not the party/raid leader
       public void PluginRun()
        {    
            _leader = getPartyLeaderObj();
            if(_leader == null || _leader == me){
                Log("Please set a leader other than yourself");
            }else{
                Start();
            }
            
        }  
        
        // Lets get this party started
        public void Start(){
            while(true){
                if(_leader != null){
                    if(_leader.inFight && me.dist(_leader) < _bardRange){
                        bardRoutine();
                    }else{
                        moveToPlayer(_leader);  
                    }
                    
                }
                
            }
        }
        
        // If the spell is not on cooldown play the four songs.
        public void bardRoutine()
        {
            Log("----------------------------------------------");
            Log("Starting Rotation");
            if (skillCooldown("[Perform] Bulwark Ballad") == 0 )
                {
                    UseSkill("[Perform] Bulwark Ballad");
                    Log("Casting Bulwark Ballad");
                    Thread.Sleep(2075);
                }
                    
            if (skillCooldown("[Perform] Bloody Chantey") == 0 )
                {
                    UseSkill("[Perform] Bloody Chantey");
                    Log("Casting Bloody Chantey");
                    Thread.Sleep(2075);
                }
                    
            if (skillCooldown("[Perform] Ode to Recovery") == 0 )
                {
                    UseSkill("[Perform] Ode to Recovery");
                    Log("Casting Ode to Recovery");
                    Thread.Sleep(2075);
                }
                
            if (skillCooldown("[Perform] Quickstep") == 0 )
                {
                    UseSkill("[Perform] Quickstep");
                    Log("Casting Quickstep");
                    Thread.Sleep(2075);
                }
            Log("Sleeping for 18 Seconds");
            Thread.Sleep(18000);
            }

        // Move towards leader            
         public void moveToPlayer(Creature obj){
            ComeTo(obj, _followRange);
        }
        
        //Call on plugin stop
        public void PluginStop()
        {
        }
    }
}
