using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FolderUpdated
{
    class Scheduler
    {

       public static System.Timers.Timer oTimer = null;
        public static System.Timers.Timer oTimer1 = null;
        double interval = 1000;

        public void Start()
        {
            oTimer = new System.Timers.Timer(1* 60* interval);
            oTimer.AutoReset = true;
            oTimer.Enabled = true;
            oTimer.Start();

            oTimer.Elapsed += new System.Timers.ElapsedEventHandler(oTimer_Elapsed);


        }

        private void oTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CheckFolderUpdates();
        }


      

    Boolean CheckTImer()
        {
            if(oTimer.Interval==3600)
            {
               
            }
            return false;
           // return oTimer.Interval;
                
        }

    void CheckFolderUpdates()
        {
            string fileName = "test.txt";
            string fileName1 = "test1.txt";
            string sourcePath = @"E:\Assignment2IPT\Q3\3a\Q2\Update1";
            string targetPath = @"E:\Assignment2IPT\Q3\3a\Q2\Update2";

            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName1);

            if (System.IO.Directory.Exists(targetPath))
            {
                if (System.IO.Directory.Exists(sourcePath))
                {
                    string[] files = System.IO.Directory.GetFiles(sourcePath);
                    string[] files1 = System.IO.Directory.GetFiles(targetPath);
                    Boolean check1 = true;
                    // Copy the files and overwrite destination files if they already exist.
                    string s2 = "";
                    foreach (string s1 in files)
                    {
                        Boolean check = true;
                        fileName1 = System.IO.Path.GetFileName(s1);
                        foreach (string s in files1)
                        {
                            fileName = System.IO.Path.GetFileName(s);
                            if (fileName != fileName1)
                            {
                                s2 = s;
                                check = false;
                                break;
                            }


                            // Use static Path methods to extract only the file name from the path.


                        }

                        if (check == false)
                        {
                            destFile = System.IO.Path.Combine(targetPath, fileName1);
                            System.IO.File.Copy(s2, destFile, true);
                        }
                    }//for

                    if (check1 == false)
                    {
                        ///increment timer by 2
                        ///  oTimer = new System.Timers.Timer(1* 60* interval);
                        oTimer.Interval = oTimer.Interval + (2*60*interval);
                        
                        /// 
                    }




                }
                else
                {
                    Console.WriteLine("Source path does not exist!");
                }
            }
            else
            {
                Console.WriteLine("Dest path does not exist!");
            }

            //obj.display();

        }
    }
}


