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
       { return "1.1.0.3"; }
       public static string GetPluginDescription()
       { return "Four Notes: Songcraft four songs"; }
       
       // Generic Configuration
       private Creature _leader = null;
       private Double _bardRange = 19.5;
       private Double _followRange = 19.5;
	   
	   // The Four Songs to change the order in which they are played.
	   string Song1 = "[Perform] Bulwark Ballad";
	   string Song2 = "[Perform] Bloody Chantey";
	   string Song3 = "[Perform] Ode to Recovery";
	   string Song4 = "[Perform] Quickstep";
	   
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
            if (skillCooldown(Song1) == 0 )
                {
                    UseSkill(Song1);
                    Log("Casting" + Song1);
                    Thread.Sleep(2075);
                }
                    
            if (skillCooldown(Song2) == 0 )
                {
                    UseSkill(Song2);
                    Log("Casting" + Song2);
                    Thread.Sleep(2075);
                }
                    
            if (skillCooldown(Song3) == 0 )
                {
                    UseSkill(Song3);
                    Log("Casting" + Song3);
                    Thread.Sleep(2075);
                }
                
            if (skillCooldown(Song4) == 0 )
                {
                    UseSkill(Song4);
                    Log("Casting" + Song4);
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
