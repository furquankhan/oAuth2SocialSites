<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="facebookCallback.aspx.cs" Inherits="oauth2.web.facebookCallback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<script src="scripts/jquery-1.10.2.js"></script>
<script src="scripts/jquery-ui.js"></script>
<script src="v1/js/main.js"></script>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
    <script>
        $(document).ready(function () {
            var getString = getQueryString();
            var accessToken = getString['access_token'];
            var expireIn = getString['expires_in'];
            var data = {"accessToken":accessToken , "expireIn":expireIn};
            services.get("GetUserInfo",JSON.stringify(data), showUserInfo);
        });

        function showUserInfo(data) {
            console.log(data);
        }
        function getQueryString() {
            var temp = window.location.href.split('?#')[1].split('&');
            var jo = {};
            for (var i = 0; i <= temp.length - 1; i++) {
                var key = window.location.href.split('?#')[1].split('&')[i].split('=')[0];
                var value = window.location.href.split('?#')[1].split('&')[i].split('=')[1];
                jo[key] = value;
            }
            return jo;
        }

    </script>
</html>
