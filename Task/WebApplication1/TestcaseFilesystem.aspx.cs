using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestcaseFilesystem
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        String path="";
        DirectoryInfo dir;
        const int Mb = 1048576;
        public List<DirectoryInfo> dirList;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);
                if (!hasAdministrativeRight)
                {
                    Response.Write("Error! You need to start this program as Administrator!!");
                }

                path = (String)ViewState["path"];

                if (!IsPostBack || path==null || String.Compare(path,"root")==0)
                {
                    DriveListMaker();
                    current.Text = "root";
                    less10.InnerText = "";
                    from10to50.InnerText = "";
                    more100.InnerText = "";

                }
                else
                {
                    dir = new DirectoryInfo(path);
                    FileCounter(dir);
                    FileListMaker(dir);
                }
            }
            catch (Exception ex) { Response.Write("Page loading error: " + ex.Message); }
        }
        protected void file_link_Click(object sender, EventArgs e)
        {
            ViewState["path"] = (sender as LinkButton).CommandArgument;
            current.Text = (String)ViewState["path"];
            Page_Load(sender,e);
        }
        protected void FileCounter(DirectoryInfo dirInfo)
        {
            try
            {
                int count = 0;
                int less_ten = 0, ten_fifty = 0, hundred_more = 0;
                foreach (var f in dirInfo.GetFiles("*", SearchOption.AllDirectories))
                {
                    count++;
                    if (f.Length < 10 * Mb) less_ten++;
                    if (f.Length >= 10 * Mb && f.Length <= 50 * Mb) ten_fifty++;
                    if (f.Length > 100 * Mb) hundred_more++;
                }
                less10.InnerText = less_ten.ToString();
                from10to50.InnerText = ten_fifty.ToString();
                more100.InnerText = hundred_more.ToString();
            }
            catch (Exception ex) { Response.Write("File count error: " + ex.Message); }
        }
        protected void DriveListMaker()
        {
            try
            {
                form1.Controls.Clear();
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Fixed)
                    {
                        LinkButton tlink = new LinkButton();
                        tlink.CommandArgument = drive.Name;
                        tlink.Text = drive.Name;
                        tlink.Click += new EventHandler(file_link_Click);
                        form1.Controls.Add(tlink);
                        form1.Controls.Add(new LiteralControl("<br />"));
                    }
                }
            }
            catch (Exception ex) { Response.Write("Drive list builder error: " + ex.Message); }
        }
        protected void FileListMaker(DirectoryInfo dirInfo)
        {
            try
            {
                form1.Controls.Clear();
                LinkButton parent = new LinkButton();
                parent.ID = "parent";
                if (dirInfo.Parent == null) 
                    parent.CommandArgument = "root";
                else 
                    parent.CommandArgument = dirInfo.Parent.FullName;
                parent.Text = "..";
                parent.Click += new EventHandler(file_link_Click);
                form1.Controls.Add(parent);
                form1.Controls.Add(new LiteralControl("<br />"));

                foreach (DirectoryInfo d in dirInfo.GetDirectories())
                {
                    LinkButton tlink = new LinkButton();
                    tlink.ID = d.Name;
                    tlink.CommandArgument = d.FullName;
                    tlink.Text = d.Name;
                    tlink.Click += new EventHandler(file_link_Click);
                    form1.Controls.Add(tlink);
                    form1.Controls.Add(new LiteralControl("<br />"));
                }
                foreach (FileInfo f in dirInfo.GetFiles())
                {
                    LinkButton tlink = new LinkButton();
                    tlink.Text = f.Name;
                    form1.Controls.Add(tlink);
                    form1.Controls.Add(new LiteralControl("<br />"));
                }
            }
            catch (Exception ex) { Response.Write("File list builder error: " + ex.Message); };
        }
    }
}