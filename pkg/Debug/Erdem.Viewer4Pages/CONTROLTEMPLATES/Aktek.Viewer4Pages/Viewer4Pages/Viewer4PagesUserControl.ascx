<%@ Assembly Name="Aktek.Viewer4Pages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9e7b62c840dc8300" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Aktek.Viewer4Pages.Viewer4Pages" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Viewer4PagesUserControl.ascx.cs"
    Inherits="Aktek.Viewer4Pages.Viewer4Pages.Viewer4PagesUserControl" %>
<% 
    try
    {
        if (errorStr != null && errorStr != "")
        {%>
<%=errorStr%>
<%
}
        else
        {
%>
<input type="hidden" name="<%=prefix%>_PostBack" id="<%=prefix%>_PostBack" value="yes">
<input type="hidden" name="<%=prefix%>_CurrentPage" id="<%=prefix%>_CurrentPage"
    value="<%=currentPage%>">
<input type="hidden" name="<%=prefix%>_TotalPage" id="<%=prefix%>_TotalPage" value="<%=totalPage%>">
<asp:Panel ID="comboFilter" runat="server" Visible="false">
    AY :<asp:DropDownList runat="server" ID="ddlMonths">
    </asp:DropDownList>
    YIL:
    <asp:DropDownList runat="server" ID="ddlYears">
    </asp:DropDownList>
    ŞİRKET:
    <asp:DropDownList runat="server" ID="ddlCompanies">
    </asp:DropDownList>
    <asp:ImageButton runat="server" ID="btnShow" Text="Göster" OnClick="Click_btnShow" />
    <br />
</asp:Panel>
<%
    if (outputHTML != null)
    {
%>
<%if (sliding == 1)
  {%>
<%}
  else
  {%>
<%if (listStyle == "AllPages")
  {%>
<%if (navigationShow)
  {%>
<%
    try
    {
        if (CurrentPage > 1)
        {
%>
<input id="btnFirst_<%=Prefix%>" type="image" src="/SiteAssets/Images/btn_first.gif"
    name="btnFirst_<%=Prefix%>" align="absmiddle" title="İlk Sayfa">
<input id="btnBack_<%=Prefix%>" type="image" src="/SiteAssets/Images/btn_back.gif"
    name="btnBack_<%=Prefix%>" align="absmiddle" title="Önceki Sayfa">
<% 
}
        else
        {		
%>
<% 
}
%>
<%if (TotalPage > 1)
  {%>
<b>Sayfa</b>
<select id="cmbPage_<%=Prefix%>" name="cmbPage_<%=Prefix%>" onchange="javascript:document.forms[0].submit();"
    class="formDDL">
    <%
	
        for (int i = 1; i <= TotalPage; i++)
        {
    %>
    <option value="<%=i%>" <%if(i==CurrentPage){%> selected <% } %>>
        <% =i%>
    </option>
    <%
        }
    %>
</select>
<%}%>
<%
    if (CurrentPage < TotalPage)
    {
%>
<input id="btnNext_<%=Prefix%>" type="image" src="/SiteAssets/Images/btn_forward.gif"
    name="btnNext_<%=Prefix%>" align="absmiddle" alt="Sonraki Sayfa">
<input id="btnLast_<%=Prefix%>" type="image" src="/SiteAssets/Images/btn_last.gif"
    name="btnLast_<%=Prefix%>" align="absmiddle" alt="Son Sayfa">
<% 
    }
    else
    {		
%>
<% 
    }
    }
    catch (Exception ex)
    {
        //ExceptionPolicy.HandleException(ex, "Log Only Policy");
    }
%>
<%}%>
<!--Aktek START-->
<%=outputHTML%>
<!--Aktek END-->
<%}
  }
  if (messageStr != null)
  {
%>
<%=messageStr%>
<%
   
    }
    }
        }
    }
    catch (Exception ex)
    {
    }
%>
