<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testwebform.aspx.cs" Inherits="SdlTest.testwebform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h2>Test</h2>
    <img src="asda" onclick="<%= Request["click_string"]%>"/>    
</body>
</html>

