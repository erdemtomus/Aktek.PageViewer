using System;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint.WebPartPages.Communication;
using System.Xml.Serialization;

using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;


namespace Aktek.Viewer4Pages.Viewer4Pages
{
    [Guid("ee7aca38-ecb0-447c-a300-1fd3e08af507")]
    [ToolboxData("<{0}:Q2SPageViewer runat=server></{0}:Q2SPageViewer>")]
    [XmlRoot(Namespace = "Q2SWPPageViewer")]

    public class Viewer4Pages : System.Web.UI.WebControls.WebParts.WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private string _userControl = @"~/_CONTROLTEMPLATES/Aktek.Viewer4Pages/Viewer4Pages/Viewer4PagesUserControl.ascx";
        private Control uc = null;
        private string _templateFileName2 = "";
        private string _templateFileName = "";
        private ListStyle _listStyle;
        private string _siteUrl = "";
        private string _listName = "Pages";
        private string _listSize = "";
        private string _maxSize = "";
        private string _orderField = "";
        private string _colCount = "2";
        //private string _isSliding = "";
        private string _whereField = "";
        private string _contentType = "";
        //private string _configPath = "";
        private string _topforeach = "<!>CONTENT</!>";
        private string _charLimit = "0";
        private int _configList = 0;

        public enum ListStyle : int
        {
            AllPages = 0,
            FirstPageOnly = 1
        }

        [SPWebCategoryName("Aktek Viewer Settings"),
          WebBrowsable(true),
        WebDescription("Enter the Site url"),
         WebDisplayName("*Site url"),
          Personalizable(true)]
        public string SiteUrl
        {
            get
            {
                return _siteUrl;
            }

            set
            {
                _siteUrl = value;
            }
        }


        [SPWebCategoryName("Aktek Viewer Settings"),
         WebBrowsable(true),
       WebDescription("Select Config List Name"),
        WebDisplayName("*Select Config List"),
         Personalizable(true)]
        public int ConfigList
        {
            get
            {
                return _configList;
            }

            set
            {
                _configList = value;
            }
        }


      //  [SPWebCategoryName("Aktek Viewer Settings"),
      //  WebBrowsable(true),
      //WebDescription("Enter the Tpl root Path with port/e.g:www.akkok.com.tr5683/"),
      // WebDisplayName("*TPL Root Site Path"),
      //  Personalizable(true)]
      //  public string ConfigPath
      //  {
      //      get
      //      {
      //          return _configPath;
      //      }

      //      set
      //      {
      //          _configPath = value;
      //      }
      //  }



        [SPWebCategoryName("Aktek Viewer Settings"),
         WebBrowsable(true),
         WebDescription("List Name"),
         WebDisplayName("*List Name"),
         Personalizable(true)]
        public string ListName
        {
            get
            {
                return _listName;
            }

            set
            {
                _listName = value;
            }
        }

        [SPWebCategoryName("Aktek Viewer Settings"),
          WebBrowsable(true),
           WebDescription("Template Repeating Part"),
            WebDisplayName("Template Repeating Part"),
          Personalizable(true)]
        public string TemplateFileName
        {
            get
            {
                return _templateFileName;
            }

            set
            {
                _templateFileName = value;
            }
        }

        [SPWebCategoryName("Aktek Viewer Settings"),
         WebBrowsable(true),
       WebDescription("Enter the non repeat css settings"),
        WebDisplayName("*Items-non repeat part"),
         Personalizable(true)]
        public string TopForEach
        {
            get
            {
                return _topforeach;
            }

            set
            {
                _topforeach = value;
            }
        }

        [SPWebCategoryName("Aktek Viewer Settings"),
          WebBrowsable(true),
          WebDescription("Template Alternative Part-Column"),
          WebDisplayName("*Seperator_Last Item"),
          Personalizable(true)]
        public string TemplateFileName2
        {
            get
            {
                return _templateFileName2;
            }

            set
            {
                _templateFileName2 = value;
            }
        }

        [SPWebCategoryName("Aktek Viewer Settings"),
         WebBrowsable(true),
         WebDescription("List Style"),
         WebDisplayName("*List Style"),
         Personalizable(true)]
        public ListStyle DisplayListStyle
        {
            get
            {
                return _listStyle;
            }

            set
            {
                _listStyle = value;
            }
        }

        [SPWebCategoryName("Aktek Viewer Settings"),
         WebBrowsable(true),
         WebDescription("List Size"),
         WebDisplayName("*List Size"),
         Personalizable(true)]
        public string ListSize
        {
            get
            {
                return _listSize;
            }

            set
            {
                _listSize = value;
            }
        }

        [SPWebCategoryName("Aktek Viewer Settings"),
         WebBrowsable(true),
         WebDescription("Max Size"),
         WebDisplayName("*Max Size"),
         Personalizable(true)]
        public string MaxSize
        {
            get
            {
                return _maxSize;
            }

            set
            {
                _maxSize = value;
            }
        }

        [SPWebCategoryName("Aktek Viewer Settings"),
         WebBrowsable(true),
         WebDescription("Where Field(Write Where Clause (Starting with <Where> ending with </Where>))"),
        WebDisplayName("Where Field"),
         Personalizable(true)]
        public string WhereField
        {
            get
            {
                return _whereField;
            }

            set
            {
                _whereField = value;
            }
        }

        [SPWebCategoryName("Aktek Viewer Settings"),
         WebBrowsable(true),
         WebDescription("Order Field(order field, and direction, forexample: Modified DESC,if writing clause start with <OrderBy> end with </OrderBy>)"),
         WebDisplayName("Order Field"),
         Personalizable(true)]
        public string OrderField
        {
            get
            {
                return _orderField;
            }

            set
            {
                _orderField = value;
            }
        }

        [SPWebCategoryName("Aktek Viewer Settings"),
         WebBrowsable(true),
         WebDescription("Char Limit for Summary"),
         WebDisplayName("*Char Limit"),
         Personalizable(true)]
        public string CharLimit
        {
            get
            {
                return _charLimit;
            }

            set
            {
                _charLimit = value;
            }
        }


        [SPWebCategoryName("Aktek Viewer Settings"),
        WebBrowsable(true),
        WebDescription("Column Count"),
        WebDisplayName("*Column Count"),
        Personalizable(true)]
        public string ColCount
        {
            get
            {
                return _colCount;
            }

            set
            {
                _colCount = value;
            }
        }


        [SPWebCategoryName("Aktek Viewer Settings"),
         WebBrowsable(true),
         WebDescription("Content Type"),
         WebDisplayName("Content Type"),
         Personalizable(true)]
        public string ContentType
        {
            get
            {
                return _contentType;
            }

            set
            {
                _contentType = value;
            }
        }

        //[SPWebCategoryName("Aktek"),
        //WebBrowsable(true),
        //WebDescription("Path to the User Control (.ascx)"),
        //WebDisplayName("*User Control (.ascx)"),
        //Personalizable(true)]
        //public string UserControl
        //{
        //    get
        //    {
        //        return _userControl;
        //    }

        //    set
        //    {
        //        _userControl = value;
        //    }
        //}

        protected override void CreateChildControls()
        {
            try
            {
                if (_userControl != null && _userControl != "")
                {
                    if (_templateFileName != null && _templateFileName != "" && _listName != null && _listName != "" && _listStyle != null && _listStyle.ToString() != "" && _listSize != null && _listSize != "" && _maxSize != null && _maxSize != "")
                    {
                        uc = this.Page.LoadControl(_userControl);
                        Viewer4PagesUserControl pwControl = uc as Viewer4PagesUserControl;
                        this.Controls.Add(pwControl);
                        pwControl.ListItems(_templateFileName, _templateFileName2, _topforeach, _listName, _listStyle.ToString(), Convert.ToInt32(_listSize), Convert.ToInt32(_maxSize), _orderField, _siteUrl, _whereField, _contentType, _charLimit,Convert.ToInt32(_colCount));
                    }
                    else
                    {
                        uc = new LiteralControl(string.Format("Please fill the required information from the 'Modify WebPart' part"));
                        this.Controls.Add(uc);
                    }
                }
                else
                {
                    uc = new LiteralControl(string.Format("To link to content, <a href=\"javascript:MSOTlPn_ShowToolPaneWrapper('{0}','{1}','{2}');\">open the tool pane</a> and then type a URL in the Link text box.", 1, 129, this.ID));
                    this.Controls.Add(uc);
                }
            }
            catch (Exception ex)
            {
                uc = new LiteralControl(string.Format("<b>Error:</b> unable to load {0}<br /><b>Details:</b> {1}", uc, ex.Message));
                this.Controls.Add(uc);
            }
        }

        /// <summary>
        /// Render this Web Part to the output parameter specified.
        /// </summary>
        /// <param name="output"> The HTML writer to write out to </param>
        protected override void Render(HtmlTextWriter output)
        {
            try
            {
                this.EnsureChildControls();
                if (uc != null)
                {
                    this.uc.RenderControl(output);
                }
            }
            catch (Exception ex)
            {
                output.Write("Unexpected error occurred...");

            }
        }

        //protected override void CreateChildControls()
        //{
        //    Control control = Page.LoadControl(_ascxPath);
        //    Controls.Add(control);
        //}
    }
}
