<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestcaseFilesystem.aspx.cs" Inherits="TestcaseFilesystem.WebForm1" %>
<%@import Namespace = "System.IO"  %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="padding:10px;">
    <div style="background-color:#f7f7f7; position: absolute; width:800px; height:80%; text-align:left; padding:50px; padding-top:10px;">
        <table runat="server" style="text-align:center; width:100%; margin-bottom: 40px; border-spacing:0px;">
            <tr style="height:30px;">
                <th style="border-color: #C0C0C0; background-color: #C0C0C0; text-align: center; color: #FFFFFF;">Less 10Mb</th>
                <th style="border-color: #C0C0C0; background-color: #C0C0C0; text-align: center; color: #FFFFFF;">10Mb-50Mb</th>
                <th style="border-color: #C0C0C0; background-color: #C0C0C0; text-align: center; color: #FFFFFF;">More 100Mb</th>
            </tr>
            <tr style="height:30px;">
                <td id="less10" style="border: solid; border-color:#C0C0C0; border-width:1px;">&nbsp</td>
                <td id ="from10to50" style="border: solid; border-color:#C0C0C0; border-width:1px;">&nbsp</td>
                <td id="more100" style="border: solid; border-color:#C0C0C0; border-width:1px;">&nbsp</td>
            </tr>
        </table>

        <span style="color:darkgray; font-weight:bold">Current path:</span>
        <asp:Label runat="server" ID="current"></asp:Label>

        <div runat="server" id="files_div" style="overflow:auto; margin-top:10px; padding:20px; border: 1px solid lightgray;">
            <form runat="server" id="form1"></form>
        </div>
    </div>
</body>
</html>
