using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Q1
{
    class Scheduler
    {

        System.Timers.Timer oTimer = null;
        static List<string> mycheckmail = new List<string>();
        static int check = 0;
        double interval = 1000;

        public void Start()
        {
            oTimer = new System.Timers.Timer(15*60*interval);
            oTimer.AutoReset = true;
            oTimer.Enabled = true;
            oTimer.Start();

            oTimer.Elapsed += new System.Timers.ElapsedEventHandler(oTimer_Elapsed);


        }

        private void oTimer_Elapsed(object sender,System.Timers.ElapsedEventArgs e)
        {
            SendEmail();
        }




        void SendEmail()
        {
            string folderPath = "E:\\Question1\\Email";
            DirectoryInfo di = new DirectoryInfo(folderPath);
            //  LinkedList obj = new LinkedList();
            FileInfo[] rgFiles = di.GetFiles("*.xml");
            foreach (FileInfo fi in rgFiles)
            {
                XmlTextReader reader = new XmlTextReader(fi.FullName);
                ArrayList list = new ArrayList();

                //     Console.WriteLine(fi.FullName);
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                                                  //   Console.Write("<" + reader.Name);
                                                  //   Console.WriteLine(">");
                            break;
                        case XmlNodeType.Text: //Display the text in each element.
                            //Console.WriteLine(reader.Value);
                            list.Add(reader.Value);
                            break;
                        case XmlNodeType.EndElement: //Display the end of the element.
                                                     // Console.Write("</" + reader.Name);
                                                     // Console.WriteLine(">");
                            break;
                    }
                }
                if(check==0)
                {
                    string s2 = list[0].ToString();    
                    mycheckmail.Add(s2);
                    
                }
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine(list[i].ToString());
                }

                ////Email Sending work

                if (!mycheckmail.Contains(list[0].ToString()) || check==0)
                {
                    mycheckmail.Add(list[0].ToString());
                    try
                    {
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                        client.EnableSsl = true;

                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential("rafitaha36@gmail.com", "AMMIabu12345");
                        MailMessage msg = new MailMessage();
                        msg.To.Add(list[0].ToString());
                        msg.From = new MailAddress("rafitaha36@gmail.com");
                        msg.Subject = list[1].ToString();
                        msg.Body = list[2].ToString();
                        client.Send(msg);
                        check++;
                        // break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

               




                //////////////End's here




                //  obj.insertAtStart(list[0].ToString(), list[1].ToString(), list[2].ToString());
            }

            //obj.display();
            Console.ReadLine();
        }
    }
    }
    

