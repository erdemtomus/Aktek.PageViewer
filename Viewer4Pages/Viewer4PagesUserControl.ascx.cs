using System;
using System.Data;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;
using Microsoft.SharePoint.Publishing.Fields;
using System.Text.RegularExpressions;
using System.Data;
using System.Globalization;
using System.Collections.Generic;

namespace Aktek.Viewer4Pages.Viewer4Pages
{
    public partial class Viewer4PagesUserControl : UserControl
    {
        protected string messageStr = null;

        protected string outputHTML = null;

        private string cachePrefix = HttpContext.Current.User.Identity.Name + "_PageViewerWebPart_";

        //filter combos
        protected DropDownList ddlMonths;
        protected DropDownList ddlYears;
        protected DropDownList ddlCompanies;
        protected ImageButton btnShow;
        protected SPListItemCollection collection = null;
        protected string listSitePath = String.Empty;
        protected string listWebPath = String.Empty;
        //

        protected string templateFileName2 = "";
        protected string templateFileName = "";
        protected string siteUrl = "";
        protected string configPath = "";
        protected string listName = "";
        protected string listStyle = "";
        protected int listSize;
        protected int currentPage = 1;
        protected int totalPage = 1;
        protected int listCount = 1;
        protected int maxSize;
        protected string orderField;
        protected int colCount;
        protected int sliding;
        protected string whereClause;
        protected string contentType;
        protected bool navigationShow = true;
        protected SPList myList;
        protected string errorStr = "";
        protected string charLimit = "";
        int cLimit = 0;
        protected string topforeach = "";

        protected string prefix = "";
        //private int totalPage;
        //private int currentPage;
        private string ListViewerListTemplateBoard = "";
        private string ListViewerImagePath = "";


        public string Prefix
        {
            get
            {
                return prefix;
            }
            set
            {
                prefix = value;
            }
        }
        public int TotalPage
        {
            get
            {
                return totalPage;
            }
            set
            {
                totalPage = value;
            }
        }

        public int CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
            }
        }

        public void ListItems(string templateFileName, string templateFileName2, string topforeach, string listName, string listStyle, int listSize, int maxSize, string orderField, string siteUrl, string whereClause, string contentType, string charLimit, int colCount)
        {
            this.topforeach = topforeach;
            this.templateFileName = templateFileName;
            this.templateFileName2 = templateFileName2;
            this.siteUrl = siteUrl;
            this.listName = listName;
            this.listStyle = listStyle;
            this.listSize = listSize;
            this.maxSize = maxSize;
            this.orderField = orderField;
            this.colCount = colCount;
            this.whereClause = whereClause;
            this.contentType = contentType;
            this.charLimit = charLimit;


            MakeRender();

        }

        #region fill the ddl for filter
        //protected void fillddlMonths()
        //{
        //    int id = 0;

        //    if (SPContext.Current.Site.Url.ToLower().Contains("tr-tr"))
        //    {
        //        ddlMonths.Items.Insert(0, new ListItem("- All -", "All"));
        //        ddlMonths.Items.Insert(1, new ListItem("January", "January"));
        //        ddlMonths.Items.Insert(2, new ListItem("February", "February"));
        //        ddlMonths.Items.Insert(3, new ListItem("March", "March"));
        //        ddlMonths.Items.Insert(4, new ListItem("April", "April"));
        //        ddlMonths.Items.Insert(5, new ListItem("May", "Mayıs"));
        //        ddlMonths.Items.Insert(6, new ListItem("June", "June"));
        //        ddlMonths.Items.Insert(7, new ListItem("July", "July"));
        //        ddlMonths.Items.Insert(8, new ListItem("August", "August"));
        //        ddlMonths.Items.Insert(9, new ListItem("September", "September"));
        //        ddlMonths.Items.Insert(10, new ListItem("October", "October"));
        //        ddlMonths.Items.Insert(11, new ListItem("November", "November"));
        //        ddlMonths.Items.Insert(12, new ListItem("December", "December"));
        //    }
        //    else
        //    {
        //        ddlMonths.Items.Insert(0, new ListItem("- Hepsi -", "All"));
        //        ddlMonths.Items.Insert(1, new ListItem("Ocak", "Ocak"));
        //        ddlMonths.Items.Insert(2, new ListItem("Şubat", "Şubat"));
        //        ddlMonths.Items.Insert(3, new ListItem("Mart", "Mart"));
        //        ddlMonths.Items.Insert(4, new ListItem("Nisan", "Nisan"));
        //        ddlMonths.Items.Insert(5, new ListItem("Mayıs", "Mayıs"));
        //        ddlMonths.Items.Insert(6, new ListItem("Haziran", "Haziran"));
        //        ddlMonths.Items.Insert(7, new ListItem("Temmuz", "Temmuz"));
        //        ddlMonths.Items.Insert(8, new ListItem("Ağustos", "Ağustos"));
        //        ddlMonths.Items.Insert(9, new ListItem("Eylül", "Eylül"));
        //        ddlMonths.Items.Insert(10, new ListItem("Ekim", "Ekim"));
        //        ddlMonths.Items.Insert(11, new ListItem("Kasım", "Kasım"));
        //        ddlMonths.Items.Insert(12, new ListItem("Aralık", "Aralık"));

        //    }
        //}

        //protected void fillddlYears()
        //{
        //    int id = 0;
        //    for (int i = DateTime.Now.Year; i >= 2000; i--)
        //    {
        //        ddlYears.Items.Insert(id, new ListItem(i.ToString(), i.ToString()));
        //        id++;
        //    }
        //    if (SPContext.Current.Site.Url.ToLower().Contains("tr-tr"))
        //    {
        //        ddlYears.Items.Insert(0, new ListItem("- All -", "All"));

        //    }
        //    else
        //    {
        //        ddlYears.Items.Insert(0, new ListItem("- Hepsi -", "All"));
        //    }
        //}

        //protected void fillddlCompanies()
        //{
        //    collection = prepareCollection(false);

        //    int ddlIndex = 0;
        //    if (ddlCompanies.Items.Count == 0)
        //    {
        //        SPFieldCollection fieldCollection = collection.List.Fields;
        //        SPFieldChoice fieldChoices = new SPFieldChoice(fieldCollection, "SIRKET");
        //        foreach (string choices in fieldChoices.Choices)
        //        {
        //            ddlCompanies.Items.Insert(ddlIndex, new ListItem(choices, choices));
        //            ddlIndex++;
        //        }

        //        if (SPContext.Current.Site.Url.ToLower().Contains("en-US"))
        //        {
        //            ddlCompanies.Items.Insert(0, new ListItem("- All -", "All"));
        //        }
        //        else
        //        {
        //            ddlCompanies.Items.Insert(0, new ListItem("- Hepsi -", "All"));
        //        }
        //    }
        //}
        #endregion


        private SPListItemCollection prepareCollection(bool posted)
        {
            SPWeb myweb = null;
            SPSite mySiteCol = null;

            try
            {
                using (mySiteCol = new SPSite(siteUrl))
                {
                    using (myweb = mySiteCol.OpenWeb())
                    {
                        myList = myweb.Lists[listName];
                        SPQuery query = new SPQuery();
                        string prepareQuery = String.Format("<OrderBy><FieldRef Name='{0}' Ascending='False' /></OrderBy>", orderField);
                        query.Query = String.Format("{0}", prepareQuery);
                        collection = myList.GetItems(query);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (mySite != null)
                {
                    mySite.Dispose();
                    mySite = null;
                }
            }

            return collection;
        }


        private void Page_Load(object sender, System.EventArgs e)
        {

            prefix = this.UniqueID.ToString();
            navigationShow = true;


            Page.DataBind();

        }


        public string GetHtml(SPListItem item, string templateFileName, int cLimit)
        {

            string templateParameters = "";
            try
            {
                SPSite currentSiteCollection = null;
                if (String.IsNullOrEmpty(siteUrl))
                {
                    currentSiteCollection = new SPSite(SPContext.Current.Web.Url);
                    siteUrl = SPContext.Current.Web.Url.ToString();
                }
                else
                {
                    currentSiteCollection = new SPSite(siteUrl);
                }

                SPWeb currentWebSite = currentSiteCollection.OpenWeb();

                string url = siteUrl;
                if (siteUrl.EndsWith("/"))
                {
                    url = siteUrl.Substring(0, siteUrl.Length - 1);
                }

                
                string newsImageUrl = "";
                try
                {
                    ImageFieldValue h = (Microsoft.SharePoint.Publishing.Fields.ImageFieldValue)item["News_ContentImage"];
                    newsImageUrl = h.ImageUrl;
                }
                catch (Exception)
                {
                    newsImageUrl = "";
                }


                string newsThumbImageUrl = "";
                try
                {
                    ImageFieldValue h = (Microsoft.SharePoint.Publishing.Fields.ImageFieldValue)item["News_Images"];
                    newsThumbImageUrl = h.ImageUrl;
                }
                catch (Exception)
                {
                    newsThumbImageUrl = "";
                }


                string Description = "";
                try
                {
                    Description = item["News_Description"].ToString();
                }
                catch (Exception)
                {
                    Description = "";
                }

               
                //string Description = item["News_Description"].ToString();
                string newsContentSubbed = "";
                if (cLimit > 0 && Description !=null)
                {
                    //html tags remover
                    string a = Regex.Replace(Description, "<.*?>", string.Empty);
                    //html tags remover
                    try
                    {
                        newsContentSubbed = a.Substring(0, cLimit) + "...";
                    }
                    catch (Exception)
                    {
                        newsContentSubbed = a;
                    }

                }
                else
                {
                    newsContentSubbed = Description;
                }

                templateParameters = String.Format("<?xml version=\"1.0\" encoding=\"iso-8859-9\"?><parameters><imageSource>{0}</imageSource><listItemUrl>{1}</listItemUrl><NewsImagePath>{2}</NewsImagePath><News_Content>{3}</News_Content><NewsImageThumbPath>{4}</NewsImageThumbPath></parameters>",
                        Util.FormatXmlText(ListViewerImagePath.ToString()),
                        url + "/" + item.Url,
                        Util.FormatXmlText(newsImageUrl.ToString()),
                        newsContentSubbed,
                        Util.FormatXmlText(newsThumbImageUrl.ToString())
                        );


                //return Util.ProcessTemplate(item, templateFileName, templateParameters, "utf-8");
                return Util.ProcessTemplateWithoutTPL(item, templateFileName, templateParameters, "utf-8");


            }
            catch (Exception ex)
            {
                errorStr = "Bilinmeyen Bir Hata oluştu GetHtml. + templateParameters=" + item + " ::::: " + templateParameters +" ::::: "+ ex.Message.ToString();
            }
            return "";

        }


        private void MakeRender()
        {
            //System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\test.txt");

            SPWeb myweb = null;
            SPSite mySiteCol = null;
            string prepareWhereQuery = "";
            try
            {

                //SPSite currentSiteCollection = new SPSite(siteUrl);

                if (String.IsNullOrEmpty(siteUrl))
                {
                    mySiteCol = new SPSite(SPContext.Current.Web.Url);
                    siteUrl = SPContext.Current.Web.Url.ToString();
                }
                else
                {
                    mySiteCol = new SPSite(siteUrl);
                }

                mySiteCol.Dispose();


                prefix = this.UniqueID.ToString();

                
                
                using (mySiteCol = new SPSite(siteUrl))
                {
                    using (myweb = mySiteCol.OpenWeb())
                    {
                        myweb.AllowUnsafeUpdates = true;

                        myList = myweb.Lists[listName];


                        SPQuery query = new SPQuery();

                        prepareWhereQuery = whereClause;

                        //Where query'e ContentType ekle..
                        if (contentType != null && contentType != "")
                        {
                            prepareWhereQuery = prepareWhereQuery.Trim();
                            if (prepareWhereQuery.StartsWith("<Where>"))
                            {
                                prepareWhereQuery = prepareWhereQuery.Substring(7);
                            }
                            if (prepareWhereQuery.EndsWith("</Where>"))
                            {
                                prepareWhereQuery = prepareWhereQuery.Substring(0, prepareWhereQuery.Length - 8);
                            }

                            if (prepareWhereQuery != null && prepareWhereQuery != "")
                            {
                                prepareWhereQuery = String.Format("<Where><And>{1}<Eq><FieldRef Name='ContentType' /><Value Type='Choice'>{0}</Value></Eq></And></Where>", contentType, prepareWhereQuery);
                            }
                            else
                            {
                                prepareWhereQuery = String.Format("<Where><Eq><FieldRef Name='ContentType' /><Value Type='Choice'>{0}</Value></Eq></Where>", contentType);
                            }
                          
                            // COMBOBOX FILTERS AT TOP START...
                            #region ComboBoxFilters

                            ////if (prepareWhereQuery != null && prepareWhereQuery != "")
                            ////{
                            ////    prepareWhereQuery = String.Format("<Where><And>{1}<Eq><FieldRef Name='News_Title' /><Value Type='Text'>{0}</Value></Eq></And></Where>", "AKKÖK KASIM 2010 BÜLTENİ", prepareWhereQuery);
                            ////}
                            //if (ddlMonths.SelectedValue == "All" && ddlYears.SelectedValue == "All" && ddlCompanies.SelectedValue == "All")
                            //{

                            //}
                            //else
                            //{
                            //    if (prepareWhereQuery.StartsWith("<Where>"))
                            //    {
                            //        prepareWhereQuery = prepareWhereQuery.Substring(7);
                            //    }
                            //    if (prepareWhereQuery.EndsWith("</Where>"))
                            //    {
                            //        prepareWhereQuery = prepareWhereQuery.Substring(0, prepareWhereQuery.Length - 8);
                            //    }

                            //    if (ddlMonths.SelectedValue == "All" && ddlYears.SelectedValue == "All" && ddlCompanies.SelectedValue != "All")
                            //    {
                            //        prepareWhereQuery = String.Format("<Where><And>{1}<Eq><FieldRef Name='SIRKET' /><Value Type='Choice'>{0}</Value></Eq></And></Where>", ddlCompanies.SelectedValue, prepareWhereQuery);
                            //    }
                            //    else if (ddlMonths.SelectedValue == "All" && ddlYears.SelectedValue != "All" && ddlCompanies.SelectedValue == "All")
                            //    {
                            //        prepareWhereQuery = String.Format("<Where><And>{1}<Eq><FieldRef Name='YIL' /><Value Type='Choice'>{0}</Value></Eq></And></Where>", ddlYears.SelectedValue, prepareWhereQuery);
                            //    }
                            //    else if (ddlMonths.SelectedValue != "All" && ddlYears.SelectedValue == "All" && ddlCompanies.SelectedValue == "All")
                            //    {
                            //        prepareWhereQuery = String.Format("<Where><And>{1}<Eq><FieldRef Name='AY' /><Value Type='Choice'>{0}</Value></Eq></And></Where>", ddlMonths.SelectedValue, prepareWhereQuery);
                            //    }
                            //    else if (ddlMonths.SelectedValue != "All" && ddlYears.SelectedValue != "All" && ddlCompanies.SelectedValue == "All")
                            //    {
                            //        prepareWhereQuery = String.Format("<Where><And>{2}<And><Eq><FieldRef Name='AY' /><Value Type='Choice'>{0}</Value></Eq><Eq><FieldRef Name='YIL' /><Value Type='Choice'>{1}</Value></Eq></And></And></Where>", ddlMonths.SelectedValue, ddlYears.SelectedValue, prepareWhereQuery);
                            //    }
                            //    else if (ddlMonths.SelectedValue != "All" && ddlYears.SelectedValue == "All" && ddlCompanies.SelectedValue != "All")
                            //    {
                            //        prepareWhereQuery = String.Format("<Where><And>{2}<And><Eq><FieldRef Name='AY' /><Value Type='Choice'>{0}</Value></Eq><Eq><FieldRef Name='SIRKET' /><Value Type='Choice'>{1}</Value></Eq></And></And></Where>", ddlMonths.SelectedValue, ddlCompanies.SelectedValue, prepareWhereQuery);
                            //    }
                            //    else if (ddlMonths.SelectedValue == "All" && ddlYears.SelectedValue != "All" && ddlCompanies.SelectedValue != "All")
                            //    {
                            //        prepareWhereQuery = String.Format("<Where><And>{2}<And><Eq><FieldRef Name='YIL' /><Value Type='Choice'>{0}</Value></Eq><Eq><FieldRef Name='SIRKET' /><Value Type='Choice'>{1}</Value></Eq></And></And></Where>", ddlYears.SelectedValue, ddlCompanies.SelectedValue, prepareWhereQuery);
                            //    }
                            //    else if (ddlMonths.SelectedValue != "All" && ddlYears.SelectedValue != "All" && ddlCompanies.SelectedValue != "All")
                            //    {
                            //        prepareWhereQuery = String.Format("<Where><And>{3}<And><And><Eq><FieldRef Name='AY' /><Value Type='Choice'>{0}</Value></Eq><Eq><FieldRef Name='YIL' /><Value Type='Choice'>{1}</Value></Eq></And><Eq><FieldRef Name='SIRKET' /><Value Type='Choice'>{2}</Value></Eq></And></And></Where>", ddlMonths.SelectedValue, ddlYears.SelectedValue, ddlCompanies.SelectedValue, prepareWhereQuery);
                            //    }
                            //}

                            ////if (ddlMonths.SelectedValue == "All" && ddlYears.SelectedValue == "All" && ddlCompanies.SelectedValue == "All")
                            ////    prepareQuery = String.Format("<OrderBy><FieldRef Name='{0}' Ascending='False' /></OrderBy>", orderName);
                            ////else
                            ////    prepareQuery = String.Format("<Where>{0}</Where><OrderBy><FieldRef Name='DATE' Ascending='False' /></OrderBy>", prepareWhereQuery);

                            #endregion
                            //// COMBOBOX FILTERS AT TOP END...

                        }


                        //where combo filter...
                        //



                        // Begin.. Order Query 14.11.2006 Hilal Celep

                        string prepareOrderQuery = "";

                        if (orderField != null && orderField != "")
                        {
                            if (!(orderField.StartsWith("<OrderBy>")))
                            {
                                if (orderField.Contains(","))
                                {
                                    string[] orderFields = orderField.Split(',');
                                    if (orderFields.Length > 1)
                                    {
                                        foreach (string orderItem in orderFields)
                                        {
                                            string[] orderArr = orderItem.Split(' ');
                                            string orderType = "Ascending='TRUE'";
                                            if (orderArr.Length > 1 && orderArr[1] == "DESC")
                                            {
                                                orderType = "Ascending='FALSE'";
                                            }
                                            prepareOrderQuery = String.Format("{0}<FieldRef Name='{1}' {2} />", prepareOrderQuery, orderArr[0].ToString(), orderType);
                                        }
                                        if (prepareOrderQuery != "")
                                        {
                                            prepareOrderQuery = String.Format("<OrderBy>{0}</OrderBy>", prepareOrderQuery);
                                        }
                                    }
                                }
                                else
                                {
                                    string[] orderArr = orderField.Split(' ');
                                    string orderType = "Ascending='TRUE'";
                                    if (orderArr.Length > 1 && orderArr[1] == "DESC")
                                    {
                                        orderType = "Ascending='FALSE'";
                                    }
                                    prepareOrderQuery = String.Format("{0}<FieldRef Name='{1}' {2} />", prepareOrderQuery, orderArr[0].ToString(), orderType);

                                    if (prepareOrderQuery != "")
                                    {
                                        prepareOrderQuery = String.Format("<OrderBy>{0}</OrderBy>", prepareOrderQuery);
                                    }
                                }
                            }
                            else
                            {
                                prepareOrderQuery = orderField;
                            }
                        }


                        //string rateQuery = " <OrderBy><FieldRef Name='Rate'  Ascending='False'/></OrderBy>";

                        query.Query = String.Format("{0}{1}", prepareWhereQuery, prepareOrderQuery);

                        //query.Query = String.Format("<Where><And><Eq><FieldRef Name='ContentType' /><Value Type='Choice'>NewsCP</Value></Eq><Or><Eq><FieldRef Name='ID' /><Value Type='Counter'>3</Value></Eq><Eq><FieldRef Name='ID' /><Value Type='Counter'>5</Value> </Eq></Or></And></Where>");


                        SPListItemCollection collection = myList.GetItems(query);

                       #region random pages

                       // DataTable dtPages = new DataTable("Pages");
                       // dtPages.Columns.Add("News_Title", typeof(string));
                       // dtPages.Columns.Add("News_Content", typeof(string));
                       // dtPages.Columns.Add("News_Category", typeof(string));
                       // dtPages.Columns.Add("News_Images", typeof(string));
                       // dtPages.Columns.Add("Title", typeof(string));
                       // dtPages.Columns.Add("News_Summary", typeof(string));
                       // dtPages.Columns.Add("News_Date", typeof(DateTime));
                       // dtPages.Columns.Add("Rate", typeof(int));
                       // dtPages.Columns.Add("ID", typeof(int));
                
                       // DataTable dtRandomPages = new DataTable("Pages");
                       // dtRandomPages.Columns.Add("News_Title", typeof(string));
                       // dtRandomPages.Columns.Add("News_Content", typeof(string));
                       // dtRandomPages.Columns.Add("News_Category", typeof(string));
                       // dtRandomPages.Columns.Add("News_Images", typeof(string));
                       // dtRandomPages.Columns.Add("Title", typeof(string));
                       // dtRandomPages.Columns.Add("News_Summary", typeof(string));
                       // dtRandomPages.Columns.Add("News_Date", typeof(DateTime));
                       // dtRandomPages.Columns.Add("Rate", typeof(int));
                       // dtRandomPages.Columns.Add("ID", typeof(int));


                       // int x = 0;
                       // int highrate = 0;
                       // foreach (SPListItem item in collection)
                       // {
                       //     if (Convert.ToInt32(collection[x]["Rate"]) == 5) //rate i 5 olanları ilk başta diziye atıyor
                       //     {

                       //         dtPages.Rows.Add(item["News_Title"],
                       //         item["News_Content"],
                       //         item["News_Category"],
                       //         item["News_Images"],
                       //         item["Title"],
                       //         item["News_Summary"],
                       //         item["News_Date"],
                       //         item["Rate"],
                       //         item["ID"]);
                       //         highrate++;
                       //     }

                       //     #region 5 ten sonrakileri random getirmek için array
                       //     else
                       //     {
                       //         if (Convert.ToInt32(collection[x]["Rate"]) == 4)
                       //         {

                       //             for (int i = 0; i < 4 * 3; i++)
                       //             {
                       //                 dtRandomPages.Rows.Add(item["News_Title"],
                       //               item["News_Content"],
                       //               item["News_Category"],
                       //               item["News_Images"],
                       //               item["Title"],
                       //               item["News_Summary"],
                       //               item["News_Date"],
                       //               item["Rate"],
                       //               item["ID"]);
                       //             }

                       //         }
                       //         if (Convert.ToInt32(collection[x]["Rate"]) == 3)
                       //         {

                       //             for (int i = 0; i < 3 * 3; i++)
                       //             {
                       //                 dtRandomPages.Rows.Add(item["News_Title"],
                       //               item["News_Content"],
                       //               item["News_Category"],
                       //               item["News_Images"],
                       //               item["Title"],
                       //               item["News_Summary"],
                       //               item["News_Date"],
                       //               item["Rate"],
                       //               item["ID"]);
                       //             }

                       //         }
                       //         if (Convert.ToInt32(collection[x]["Rate"]) == 2)
                       //         {

                       //             for (int i = 0; i < 2 * 3; i++)
                       //             {
                       //                 dtRandomPages.Rows.Add(item["News_Title"],
                       //               item["News_Content"],
                       //               item["News_Category"],
                       //               item["News_Images"],
                       //               item["Title"],
                       //               item["News_Summary"],
                       //               item["News_Date"],
                       //               item["Rate"],
                       //               item["ID"]);
                       //             }

                       //         }
                       //         if (Convert.ToInt32(collection[x]["Rate"]) == 1)
                       //         {

                       //             for (int i = 0; i < 1 * 3; i++)
                       //             {
                       //                 dtRandomPages.Rows.Add(item["News_Title"],
                       //               item["News_Content"],
                       //               item["News_Category"],
                       //               item["News_Images"],
                       //               item["Title"],
                       //               item["News_Summary"],
                       //               item["News_Date"],
                       //               item["Rate"],
                       //               item["ID"]);
                       //             }

                       //         }
                       //     #endregion

                       //     }
                       //     x++;
                       // }


                       // for (int i = 0; i < (collection.Count - highrate); i++)
                       // {
                       //     Random rand = new Random();
                       //     int toSkip = rand.Next(0, dtRandomPages.Rows.Count);

                       //     try
                       //     {

                       //         if (dtRandomPages.Rows[toSkip] != null)
                       //         {
                       //             dtPages.ImportRow(dtRandomPages.Rows[toSkip]);

                       //             int groupId = (int)dtRandomPages.Rows[toSkip]["ID"];

                       //             for (int a = dtRandomPages.Rows.Count - 1; a >= 0; a--)
                       //             {

                       //                 if ((int)dtRandomPages.Rows[a]["ID"] == (int)groupId)
                       //                 {
                       //                     dtRandomPages.Rows.Remove(dtRandomPages.Rows[a]);
                       //                 }

                       //             }
                       //         }


                       //     }
                       //     catch (Exception ex)
                       //     {
                       //         //throw new Exception("---------" + ex.Message.ToString() + "--------" );
                       //     }

                       // }



                       // List<SPListItem> collection2 = new List<SPListItem>();

                       // SPSecurity.RunWithElevatedPrivileges(delegate()
                       //             {
                       //                 using (mySiteCol = new SPSite(siteUrl))
                       //                 {
                       //                     using (myweb = mySiteCol.OpenWeb())
                       //                     {
                       //                         myweb.AllowUnsafeUpdates = true;

                       //                         SPList libNews = (SPDocumentLibrary)myweb.Lists[listName]; //pages list

                       //                         foreach (DataRow dataRow in dtPages.Rows)
                       //                         {
                       //                             int pageItemID = int.Parse(dataRow["ID"].ToString());

                       //                             SPListItem pageListItem = libNews.GetItemById(pageItemID);
                       //                             collection2.Add(pageListItem);
                       //                         }
                       //                     }
                       //                 }
                       //             });

                        #endregion


                        if (listStyle == "AllPages")
                        {
                            if (maxSize == 0)
                            {
                                listCount = collection.Count;
                            }
                            else
                            {
                                if (maxSize < collection.Count)
                                    listCount = maxSize;
                                else
                                    listCount = collection.Count;
                            }
                            if (listSize == 0)
                            {
                                navigationShow = false;
                                listSize = listCount;
                            }
                            if (listSize == listCount)
                            {
                                navigationShow = false;
                            }
                            AdjustCurrentandTotalPage();
                           

                            int controlListSize = 0;

                            try
                            {
                                cLimit = Convert.ToInt32(charLimit);
                            }
                            catch (Exception)
                            {
                                cLimit = 0;
                            }

                            for (int i = ((currentPage - 1) * listSize); ; i++)
                            {
                                if (controlListSize == listSize || i == listCount)
                                {
                                    break;
                                }
                                if (!string.IsNullOrEmpty(templateFileName2))
                                {
                                    if (((i + 1) % colCount) == 0)
                                    {
                                        outputHTML = String.Format("{0} {1}", outputHTML, GetHtml(collection[i], templateFileName2, cLimit));
                                    }
                                    else
                                    {
                                        outputHTML = String.Format("{0} {1}", outputHTML, GetHtml(collection[i], templateFileName, cLimit));
                                    }
                                }
                                else
                                {
                                    outputHTML = String.Format("{0} {1}", outputHTML, GetHtml(collection[i], templateFileName, cLimit));
                                }

                                controlListSize++;
                            }
                            
                            outputHTML = topforeach.Replace("<!>CONTENT</!>", outputHTML);
                        }
                        else
                        {
                            SPListItem myListItem = collection[0];
                            outputHTML = GetHtml(collection[0], templateFileName, cLimit);
                            outputHTML = topforeach.Replace("<!>CONTENT</!>", outputHTML);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                errorStr = ex.ToString();
            }
            finally
            {
                if (mySiteCol != null)
                {
                    mySiteCol.Dispose();
                    mySiteCol = null;
                }

                if (myweb != null)
                {
                    myweb.Dispose();
                    myweb = null;
                }
            }

        }


        private void AdjustCurrentandTotalPage()
        {
            try
            {
                prefix = this.UniqueID.ToString();
                if (Request[prefix + "_PostBack"] == null)
                {
                    currentPage = 1;
                    int res = listCount;
                    if (res == 1)
                    {
                        totalPage = 1;
                    }
                    else
                    {
                        totalPage = (int)((res - 1) / listSize) + 1;
                    }

                }
                else
                {
                    try
                    {
                        currentPage = Convert.ToInt32(Request[prefix + "_CurrentPage"]);
                    }
                    catch
                    {

                    }
                    try
                    {
                        totalPage = Convert.ToInt32(Request[prefix + "_TotalPage"]);
                    }
                    catch
                    {
                    }

                    if (Request["btnFirst_" + prefix + ".x"] != null)
                        currentPage = 1;
                    else if (Request["btnBack_" + prefix + ".x"] != null)
                        currentPage = currentPage - 1;
                    else if (Request["btnNext_" + prefix + ".x"] != null)
                        currentPage = currentPage + 1;
                    else if (Request["btnLast_" + prefix + ".x"] != null)
                        currentPage = totalPage;
                    else if (Request["cmbPage_" + prefix] != null)
                    {
                        if (Request.Form.GetValues("cmbPage_" + prefix)[0] != Request[prefix + "_CurrentPage"])
                            currentPage = Convert.ToInt32(Request.Form.GetValues("cmbPage_" + prefix)[0]);
                        //else
                        //currentPage = Convert.ToInt32(Request.Form.GetValues("cmbPage_" + prefix)[1]);
                        //currentPage = Convert.ToInt32(Request["cmbPage_"+prefix].Substring(0,1));
                    }
                }

            }
            catch (Exception ex)
            {

                errorStr = "Bilinmeyen Bir Hata oluştu. AdjustCurrentandTotalPage";
            }

        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }


        #endregion

        protected void Click_btnShow(object sender, ImageClickEventArgs e)
        {

        }

        public SPSite mySite { get; set; }


    }
}
