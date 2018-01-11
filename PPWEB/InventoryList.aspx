<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryList.aspx.cs" Inherits="PPWEB.InventoryList" %>

<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>产品列表，基于条码+互联网技术的应用</title>

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
        <div>
            
            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="cInvCode" VisibleIndex="0" Caption="产品编码"> 
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="cInvName" VisibleIndex="1" Caption="产品名称">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="cInvStd" VisibleIndex="2" Caption="规格型号">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="iWeight" VisibleIndex="3" Caption="重量">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="iBoxQty" VisibleIndex="4" Caption="箱规">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="cMemo" VisibleIndex="5" Caption="备注">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="dAddTime" VisibleIndex="6" Caption="添加时间">
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="dUpdateTime" VisibleIndex="7" Caption="更新时间">
                    </dx:GridViewDataDateColumn>
                </Columns>
            </dx:ASPxGridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PPWEB.Properties.Settings.SqlCon %>" SelectCommand="SELECT [cInvCode], [cInvName], [cInvStd], [iWeight], [iBoxQty], [cMemo], [dAddTime], [dUpdateTime] FROM [Inventory]"></asp:SqlDataSource>
            
        </div>
    </form>
</body>
</html>
