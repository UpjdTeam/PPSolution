<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XtraDocumentView.aspx.cs" Inherits="PPWEB.XtraDocumentView" %>

<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>条码生成，基于条码+互联网技术的应用</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/landing-page.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <!--<link href="https://fonts.googleapis.com/css?family=Lato:300,400,700,300italic,400italic,700italic" rel="stylesheet" type="text/css">-->

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>

<body>
    <!-- Navigation -->
    <nav class="navbar navbar-default navbar-fixed-top topnav" role="navigation">
        <div class="container topnav">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand topnav" href="#">条码+互联网应用</a>
            </div>
            <!-- Collect the nav links, forms, and other content for toggling -->
            <li class="collapse navbar-collapse" id="bs-example-navbar-collapse-1" />
            <ul class="nav navbar-nav navbar-right">
                <li><a href="Index.html#about">简介</a></li>
                <li><a href="Index.html#services">服务</a></li>
                <li><a href="Index.html#contact">联系我们</a></li>
                <li><a href="StoreDetail.html">入库明细</a></li>
                <li><a href="DeliveryDetail.html">出库明细</a></li>
                <li><a href="#">库存汇总</a></li>
            </ul>
            <!-- /.navbar-collapse -->
        </div>
        <!-- /.container -->
    </nav>
    <br />
    <br />
    <br />
    <form id="form1" runat="server">
        <div style="margin-top: 10px">
            <label style="width: 70px">条码号</label>
            <asp:TextBox ID="txtBarCode" runat="server" Width="200"></asp:TextBox>
        </div>
        <div style="margin-top: 10px">
            <label style="width: 70px">产品名称</label>
            <asp:TextBox ID="txtInvName" runat="server" Width="200"></asp:TextBox>
        </div>
        <div style="margin-top: 10px">
            <label style="width: 70px">产品规格</label>
            <asp:TextBox ID="txtInvStd" runat="server" Width="200"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="生成条码" CssClass="btn btn-info"/>
        </div>
        
        <div style="margin-top: 10px">
            <dx:ASPxDocumentViewer ID="DevXtraDocument" runat="server" EnableViewState="False" Width="500" Height="300">
                <SettingsReportViewer UseIFrame="False" />
                <SettingsSplitter ParametersPanelCollapsed="True" RightPaneVisible="False" />
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
