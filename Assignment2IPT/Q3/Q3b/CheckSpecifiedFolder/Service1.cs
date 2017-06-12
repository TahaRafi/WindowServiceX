using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CheckSpecifiedFolder
{
    public partial class Service1 : ServiceBase
    {
        static List<string> ArrayFolder = new List<string>();
        public static System.Timers.Timer oTimer = null;
        public static System.Timers.Timer oTimer1 = null;
        double interval = 1000;
        public Service1()
        {
            InitializeComponent();
            FirstTime();
            InitializeScheduler();
        }


        private void InitializeScheduler()
        {
            oTimer = new System.Timers.Timer(15 * 60 * interval);
            oTimer.AutoReset = true;
            oTimer.Enabled = true;
            oTimer.Start();

            oTimer.Elapsed += new System.Timers.ElapsedEventHandler(oTimer_Elapsed);
        }

        private void oTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CheckAndSendEmail();
        }

        protected override void OnStart(string[] args)
        {
        }


        void FirstTime()
        {
            string fileName = "test.txt";
            string sourcePath = @"E:\Q2\Update1";


            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);



            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);
             ///   ArrayFolder = files;
                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    fileName = System.IO.Path.GetFileName(s);
                    ArrayFolder.Add(fileName);
                    Console.WriteLine(fileName);
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }
            
        }



        void CheckAndSendEmail()
        {
            string fileName = "test.txt";
            string sourcePath = @"E:\Q2\Update1";
            Boolean check = false;

            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
           
            List<string> myList = new List<string>();
            string ans = "";

            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);
                //ArrayFolder = files;
                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    fileName = System.IO.Path.GetFileName(s);
                    if (!ArrayFolder.Contains(fileName))
                    {
                        check = true;
                        ArrayFolder.Add(fileName);
                        long length = new System.IO.FileInfo(s).Length;
                        ans = ans + " " +"Filename:"+fileName + " " +"FileSize:"+ length + " ";
                      //  myList.Add(s);
                    }
                    // Use static Path methods to extract only the file name from the path.
                  
                    //Console.WriteLine(fileName);
                }
              //  ArrayFolder = files;
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////Check file size /////////////////////////////////////////////////////////
            if(check==true)
            {
                try
                {
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;

                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("email", "password");
                    MailMessage msg = new MailMessage();
                    msg.To.Add("email");
                    msg.From = new MailAddress("email");
                    msg.Subject = "Inform About Updated Folder";
                    msg.Body = ans;
                    client.Send(msg);
                    // break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            


        }

        protected override void OnStop()
        {
        }
    }
}
