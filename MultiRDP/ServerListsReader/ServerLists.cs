using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
//using System.Windows.Forms;
using System.IO;

namespace ServerListXMLReader
{
    public class ServerLists
    {
        string settings_path = string.Empty;
        XPathNavigator nav;
        XPathDocument docNav;
        XPathNodeIterator NodeIter;

        ArrayList _alServers = new ArrayList();
        ArrayList _alGroups = new ArrayList();

        public ServerLists()
        {
            settings_path = Path.Combine(System.Windows.Forms.Application.StartupPath, "ServerLists.xml");

            if (!File.Exists(this.settings_path))
            {
                using (StreamWriter writer = new StreamWriter(this.settings_path))
                {
                    writer.Write("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><groups></groups><serverlists></serverlists>");
                }
            }
        }

        public ArrayList ArrayListServers
        {
            get
            {
                return this._alServers;
            }
        }

        public ArrayList ArrayListGroups
        {
            get
            {
                return this._alGroups;
            }
        }

        public void Read()
        {
            if (!File.Exists(settings_path))
            {
                throw new Exception("ServerLists.xml file is missing");
            }

            ReadGroups();
            ReadServers();
        }

        private void ReadGroups()
        {
            docNav = new XPathDocument(settings_path);
            nav = docNav.CreateNavigator();
            NodeIter = nav.Select("/serverlists/groups/group");
            
            this._alGroups.Clear();

            while (NodeIter.MoveNext())
            {
                Groups g = new Groups(NodeIter.Current);
                this._alGroups.Add(g);
            }
        }

        private void ReadServers()
        {
            docNav = new XPathDocument(settings_path);
            nav = docNav.CreateNavigator();
            NodeIter = nav.Select("/serverlists/server");

            this._alServers.Clear();

            while (NodeIter.MoveNext())
            {
                ServerSettings s = new ServerSettings(NodeIter.Current);
                this._alServers.Add(s);
            }
        }

        public bool Save(ServerSettings ss)
        {
            bool ret = false;

            XmlDocument doc = new XmlDocument();
            doc.Load(this.settings_path);

            if (Check(ss, doc.DocumentElement))
            {
                throw new SettingsException(SettingsException.ExceptionTypes.DUPLICATE_ENTRY);
            }

            #region xml fragment settings
            string xmlfrag = @"
    <server dummy='{10}'>
        <uid>{0}</uid>
	    <servername>{1}</servername>
	    <server>{2}</server>
	    <username>{3}</username>
	    <password>{4}</password>
	    <description>{5}</description>
	    <colordepth>{6}</colordepth>
	    <desktopwidth>{7}</desktopwidth>
	    <desktopheight>{8}</desktopheight>
	    <fullscreen>{9}</fullscreen>
        <port>{11}</port>
        <gid>{12}</gid>
    </server>
";
            xmlfrag = string.Format(xmlfrag,
                ss.UID,
                ss.ServerName,
                ss.Server,
                ss.Username,
                (ss.Password == string.Empty ? string.Empty : RijndaelSettings.Encrypt(ss.Password)),
                ss.Description,
                ss.ColorDepth,
                ss.DesktopWidth,
                ss.DesktopHeight,
                ss.Fullscreen,
                ss.UID,
                ss.Port
            );
            #endregion

            XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
            docFrag.InnerXml = xmlfrag;

            XmlNode curNode = doc.DocumentElement;
            curNode.InsertAfter(docFrag, curNode.LastChild);

            doc.Save(this.settings_path);

            ret = true;

            return ret;
        }

        public bool Update(ServerSettings ss, ServerSettings oldSS)
        {
            bool ret = false;

            XmlDocument doc = new XmlDocument();
            doc.Load(this.settings_path);

            // check if the user made some changes on Server Name and Server address
            if ((ss.ServerName != oldSS.ServerName) || (ss.Server != oldSS.Server))
            {
                // if so, then check if both of these settings has a duplicate on our db
                if (Check(ss, doc.DocumentElement))
                {
                    throw new SettingsException(SettingsException.ExceptionTypes.DUPLICATE_ENTRY);
                }
            }

            #region xml fragment settings
            string xmlfrag = @"
        <uid>{0}</uid>
	    <servername>{1}</servername>
	    <server>{2}</server>
	    <username>{3}</username>
	    <password>{4}</password>
	    <description>{5}</description>
	    <colordepth>{6}</colordepth>
	    <desktopwidth>{7}</desktopwidth>
	    <desktopheight>{8}</desktopheight>
	    <fullscreen>{9}</fullscreen>
        <port>{10}</port>
        <gid>{11}</gid>
";
            xmlfrag = string.Format(xmlfrag,
                oldSS.UID,
                ss.ServerName,
                ss.Server,
                ss.Username,
                RijndaelSettings.Encrypt(ss.Password),
                ss.Description,
                ss.ColorDepth,
                ss.DesktopWidth,
                ss.DesktopHeight,
                ss.Fullscreen,
                //ss.ServerName, ss.Server,
                ss.Port
            );
            #endregion

            XmlElement curNode = doc.DocumentElement;
            XmlNode oldNode = curNode.SelectSingleNode("/serverlists/server[@dummy=\"" + oldSS.UID + "\"]");
            
            XmlElement newNode = doc.CreateElement("server");

            newNode.SetAttribute("dummy", oldSS.UID);
            newNode.InnerXml = xmlfrag;

            curNode.ReplaceChild(newNode, oldNode);

            doc.Save(this.settings_path);

            ret = true;

            return ret;
        }

        public bool Delete(string ServerUID)
        {
            bool ret = false;

            XmlDocument d = new XmlDocument();

            d.Load(settings_path);

            XmlNode t = d.SelectSingleNode("/serverlists/server[@dummy=\"" + ServerUID + "\"]");

            t.ParentNode.RemoveChild(t);

            d.Save(settings_path);

            ret = true;

            return ret;
        }

        private bool Check(ServerSettings ss, XmlElement xmlElem)
        {
            bool ret = false;

            string xpath = "/serverlists/server[servername='{0}' and server='{1}']";
            xpath = string.Format(xpath, ss.ServerName, ss.Server);

            XmlNode node = xmlElem.SelectSingleNode(xpath);
            if (node != null)
            {
                ret = true;
            }

            return ret;
        }

        public string GetMD5(string data)
        {
            string result = string.Empty;

            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(data);
            bs = x.ComputeHash(bs);

            StringBuilder s = new StringBuilder();

            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }

            result = s.ToString();

            return result;
        }

        public class Groups
        {
            XPathNavigator nav = null;

            int _groupID = 0;
            string _group_name = string.Empty;

            public Groups(XPathNavigator n)
            {
                this.nav = n;
                GetDatas();
            }

            private void GetDatas()
            {
                if (nav.Value != null)
                {
                    this._group_name = nav.Value;
                    this._groupID = int.Parse(nav.GetAttribute("gid", string.Empty));

                    System.Diagnostics.Debug.WriteLine(string.Format("gid={0}, gname={1}", this._groupID, this._group_name));
                }
            }

            public int GroupID
            {
                set
                {
                    this._groupID = value;
                }
                get
                {
                    return this._groupID;
                }
            }

            public string GroupName
            {
                set
                {
                    this._group_name = value;
                }
                get
                {
                    return this._group_name;
                }
            }
        }
        
        public class ServerSettings
        {
            XPathNavigator nav = null;

            string _uid = string.Empty;
            string _serverName = string.Empty;
            string _server = string.Empty;
            int _port = 0;
            string _username = string.Empty;
            string _password = string.Empty;
            string _description = string.Empty;

            int _colorDepth = 0;
            int _desktopWidth = 0;
            int _desktopHeight = 0;
            bool _fullScreen = false;

            int _groupID = 0;

            public ServerSettings()
            {
            }

            public ServerSettings(XPathNavigator n)
            {
                this.nav = n;
                GetDatas();
            }

            private void GetDatas()
            {
                if (nav.SelectSingleNode("./uid") != null)
                {
                    this._uid = nav.SelectSingleNode("./uid").Value;
                }

                if (nav.SelectSingleNode("./servername") != null)
                {
                    this._serverName = nav.SelectSingleNode("./servername").Value;
                }

                if (nav.SelectSingleNode("./server") != null)
                {
                    this._server = nav.SelectSingleNode("./server").Value;
                }

                if (nav.SelectSingleNode("./port") != null)
                {
                    string val = nav.SelectSingleNode("./port").Value;
                    this._port = int.Parse(val == string.Empty ? "0" : val);
                }

                if (nav.SelectSingleNode("./username") != null)
                {
                    this._username = nav.SelectSingleNode("./username").Value;
                }

                if (nav.SelectSingleNode("./password") != null)
                {
                    string password = nav.SelectSingleNode("./password").Value;

                    if (password != string.Empty)
                    {
                        this._password = RijndaelSettings.Decrypt(password);
                    }
                }

                if (nav.SelectSingleNode("./description") != null)
                {
                    this._description = nav.SelectSingleNode("./description").Value;
                }

                if (nav.SelectSingleNode("./colordepth") != null)
                {
                    this._colorDepth = int.Parse(nav.SelectSingleNode("./colordepth").Value);
                }

                if (nav.SelectSingleNode("./desktopwidth") != null)
                {
                    this._desktopWidth = int.Parse(nav.SelectSingleNode("./desktopwidth").Value);
                }

                if (nav.SelectSingleNode("./desktopheight") != null)
                {
                    this._desktopHeight = int.Parse(nav.SelectSingleNode("./desktopheight").Value);
                }

                if (nav.SelectSingleNode("./fullscreen") != null)
                {
                    this._fullScreen = bool.Parse(nav.SelectSingleNode("./fullscreen").Value);
                }

                if (nav.SelectSingleNode("./gid") != null)
                {
                    this._groupID = int.Parse(nav.SelectSingleNode("./gid").Value);
                }
            }

            public string UID
            {
                set
                {
                    this._uid = value;
                }
                get
                {
                    return this._uid;
                }
            }

            public string ServerName
            {
                set
                {
                    this._serverName = value;
                }
                get
                {
                    return this._serverName;
                }
            }

            public string Server
            {
                set
                {
                    this._server = value;
                }
                get
                {
                    return this._server;
                }
            }

            public int Port
            {
                set
                {
                    this._port = value;
                }
                get
                {
                    return this._port;
                }
            }

            public string Username
            {
                set
                {
                    this._username = value;
                }
                get
                {
                    return this._username;
                }
            }

            public string Password
            {
                set
                {
                    this._password = value;
                }
                get
                {
                    return this._password;
                }
            }

            public string Description
            {
                set
                {
                    this._description = value;
                }
                get
                {
                    return this._description;
                }
            }

            public int ColorDepth
            {
                set
                {
                    this._colorDepth = value;
                }
                get
                {
                    return this._colorDepth;
                }
            }

            public int DesktopWidth
            {
                set
                {
                    this._desktopWidth = value;
                }
                get
                {
                    return this._desktopWidth;
                }
            }

            public int DesktopHeight
            {
                set
                {
                    this._desktopHeight = value;
                }
                get
                {
                    return this._desktopHeight;
                }
            }

            public bool Fullscreen
            {
                set
                {
                    this._fullScreen = value;
                }
                get
                {
                    return this._fullScreen;
                }
            }

            public int GroupID
            {
                set
                {
                    this._groupID = value;
                }
                get
                {
                    return this._groupID;
                }
            }
        }
    }

    // nested class test
    public class test
    {
        public test()
        {
            System.Diagnostics.Debug.WriteLine("test class");
        }

        public class double_test
        {
            public double_test()
            {
                System.Diagnostics.Debug.WriteLine("double_test class");
            }
        }
    }
}
