﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuildControl.ascx.cs" Inherits="Complex.Domino.Plugins.BuildControl, Complex.Domino.Plugins" %>


<table class="form">
    <tr>
        <td class="label"></td>
        <td class="field">
            <asp:TextBox runat="server" ID="commandLine" /></td>
    </tr>
</table>
<toolbar class="form">
    <asp:LinkButton runat="server" ID="ok" OnClick="Ok_Click">
        <asp:Image runat="server" SkinID="OkButton" />
        <p><asp:Label runat="server" Text="<%$ Resources:Labels, Ok %>" /></p>
    </asp:LinkButton>
</toolbar>
<asp:Panel runat="server" ID="console" Visible="false">
<asp:Label runat="server" Text="Build output" />:
    <console>
<asp:Literal runat="server" ID="output" />
    </console>
</asp:Panel>