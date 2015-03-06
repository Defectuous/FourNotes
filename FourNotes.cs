using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using ArcheBuddy.Bot.Classes;

namespace DefaultNameSpace{
    public class FourNotes : Core
   {
       public static string GetPluginAuthor()
       { return "Defectuous"; }
       public static string GetPluginVersion()
       { return "1.0.0.9"; }
       public static string GetPluginDescription()
       { return "Four Notes: Part/Raid Songcraft Buffs Plugin"; }
        
       //Call on plugin start
       public void PluginRun()
       {
                while (me.target == null)
                {
                    Thread.Sleep(50);
                }
                
                while (me.target != null)
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
                    Thread.Sleep(18000);
                    Log("Sleeping for 18 Seconds");
                } 
       }
       //Call on plugin stop
       public void PluginStop()
       {
       }
