<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PPPrinter.Default" %>

<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxDocumentViewer ID="ASPxDocumentViewer1" runat="server" ReportTypeName="PPPrinter.XReport">
                <SettingsReportViewer EnableRequestParameters="False" />
                <StylesViewer>
                    <BookmarkSelectionBorder BorderColor="Gray" BorderStyle="Dashed" BorderWidth="3px"></BookmarkSelectionBorder>
                </StylesViewer>

                <StylesSplitter>
                    <Pane>
                        <Paddings Padding="16px"></Paddings>
                    </Pane>
                </StylesSplitter>
            </dx:ASPxDocumentViewer>

        </div>
    </form>
</body>
</html>
