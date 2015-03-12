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
       { return "1.0.0.11"; }
       public static string GetPluginDescription()
       { return "Four Notes: Part/Raid Songcraft Buffs Plugin"; }


       // The Four Songs to change the order in which they are played.
       string Song1 = "[Perform] Bulwark Ballad";
       string Song2 = "[Perform] Bloody Chantey";
       string Song3 = "[Perform] Ode to Recovery";
       string Song4 = "[Perform] Quickstep";
       
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
                    Thread.Sleep(18000);
                    Log("Sleeping for 18 Seconds");
                } 
       }
       //Call on plugin stop
       public void PluginStop()
       {
       }
    }
}
