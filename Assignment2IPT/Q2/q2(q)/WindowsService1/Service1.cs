using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WindowsService1
{
   
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer oTimer = null;
        double interval = 1000;
        public Service1()
        {
            InitializeComponent();
            Start();
        }



        public void Start()
        {
            oTimer = new System.Timers.Timer(5 * 60 * interval);
            oTimer.AutoReset = true;
            oTimer.Enabled = true;
            oTimer.Start();

            oTimer.Elapsed += new System.Timers.ElapsedEventHandler(oTimer_Elapsed);


        }

        private void oTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            RSSTOXML();
        }


        void RSSTOXML()
        {
            XmlDocument rssXmlDoc = new XmlDocument();
            string path = @"E:\\q2(q)\\RssFeedToXml\\News.Xml";

            List<string> RSSLinkList = new List<string>();
            RSSLinkList.Add("http://dawn.jpl.nasa.gov/RSS/dawnenews.xml");
            RSSLinkList.Add("https://tribune.com.pk/sports/feed/");
            for (int i = 0; i < RSSLinkList.Count; i++)
            {
                // Load the RSS file from the RSS URL
                // rssXmlDoc.Load("https://tribune.com.pk/sports/feed/");
                rssXmlDoc.Load(RSSLinkList[i]);

                // Parse the Items in the RSS file
                XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

                StringBuilder rssContent = new StringBuilder();

                // Iterate through the items in the RSS file
                string s = "";
                string s1 = "";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
                {
                    s1 = s1 + "<Newsitem>" + System.Environment.NewLine;
                    file.Write(s1);
                    foreach (XmlNode rssNode in rssNodes)
                    {

                        XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                        string title = rssSubNode != null ? rssSubNode.InnerText : "";
                        //   Console.WriteLine(title);


                        rssSubNode = rssNode.SelectSingleNode("pubDate");
                        string link = rssSubNode != null ? rssSubNode.InnerText : "";
                        //   Console.WriteLine(link);

                        rssSubNode = rssNode.SelectSingleNode("description");
                        string description = rssSubNode != null ? rssSubNode.InnerText : "";
                        // Console.WriteLine(description);
                        rssSubNode = rssNode.SelectSingleNode("channel");
                        string channel = rssSubNode != null ? rssSubNode.InnerText : "";
                        //  Console.WriteLine(channel);
                        s = s + "<Title>" + title + "</Title>" + System.Environment.NewLine + "<Description>" + description + "</Description>" + System.Environment.NewLine + "<PublishedDate>" + link + "</PublishedDate>" + System.Environment.NewLine + "<NewsChannel>" + channel + "</NewsChannel>" + System.Environment.NewLine;
                        //  rssContent.Append("<a href='" + link + "'>" + title + "</a><br>" + description);

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////write to xml/////////////////////////////////////////////////////////////////

                        file.Write(s);
                    }
                    s1 = "";
                    s1 = s1 + "</Newsitem>" + System.Environment.NewLine;
                    file.Write(s1);

                }
            }///RSSLinkList





            //XmlTextWriter textWriter = new XmlTextWriter("E:\\2example\\RssFeedToXml\\News.Xml", null);
            //textWriter.WriteStartDocument();
            // textWriter.LoadXml("Name", "");
            //  ..textWriter.Close();


            //////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////Sort//////////////////////////////////////////////////
            /* XmlDocument myDoc = new XmlDocument();

             myDoc.LoadXml(@"E:\\2example\\RssFeedToXml\\News.Xml");
             var sortedItems = myDoc.GetElementsByTagName("Newsitem").OfType<XmlElement>()
     .OrderBy(item => DateTime.ParseExact(item.GetAttribute("PublishedDate"), " MM/dd/yyyy h:mm:ss tt", null));

             foreach (var item in sortedItems)
             {
                 Console.WriteLine(item.OuterXml);
             }
             */

            string folderPath = "E:\\q2(q)\\RssFeedToXml\\News.Xml";
            DirectoryInfo di = new DirectoryInfo(folderPath);
            //  LinkedList obj = new LinkedList();
            FileInfo[] rgFiles = di.GetFiles("*.Xml");
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
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine(list[i].ToString());
                }


            }//for
            //    Console.Write(s);
        //    Console.ReadLine();
            // Return the string that contain the RSS items
            ///return rssContent.ToString();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
