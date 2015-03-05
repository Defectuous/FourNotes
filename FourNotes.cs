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
       { return "1.1.0.0"; }
       public static string GetPluginDescription()
       { return "Four Notes: Songcraft four songs"; }
       
       private Creature _leader = null;
       private Double _bardRange = 19.5;
       private Double _followRange = 10.0;
       
       public void PluginRun()
        {    
            _leader = getPartyLeaderObj();
            if(_leader == null || _leader == me){
                Log("Please set leader");
            }else{
                Start();
            }
            
        }  
        
        public void Start(){
            while(true){
                Thread.Sleep(50);
                if(_leader != null){
                    if(_leader.inFight && me.dist(_leader) < _bardRange){
                        bardRoutine();
                    }else{
                        moveToPlayer(_leader);  
                    }
                    
                }
                
            }
        }
            
         public void moveToPlayer(Creature obj){
            ComeTo(obj, _followRange);
        }
        
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
                    
            if (skillCooldown("[Perform] Ode to Revery") == 0 )
                {
                    UseSkill("[Perform] Ode to Revery");
                    Log("Casting Ode to Revery");
                    Thread.Sleep(2075);
                }
                
            if (skillCooldown("[Perform] Quickstep") == 0 )
                {
                    UseSkill("[Perform] Quickstep");
                    Log("Casting Quickstep");
                    Thread.Sleep(2075);
                }
            Thread.Sleep(3000);
            Log("Sleeping for 3 Seconds");
        }

        //Call on plugin stop
        public void PluginStop()
        {
        }
    }
}
