﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="CourseList.aspx.cs" Inherits="Complex.Domino.Web.Admin.CourseList" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All courses</h1>
    <div class="toolbar">
        <asp:HyperLink runat="server" ID="ToolbarCreate" Text="Create Course" />
    </div>
    <asp:ObjectDataSource runat="server" ID="courseDataSource" DataObjectTypeName="Complex.Domino.Lib.Course"
        OnObjectCreating="courseDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.CourseFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        EnablePaging="true" />
    <domino:multiselectgridview id="courseList" runat="server" datasourceid="courseDataSource"
        autogeneratecolumns="false" datakeynames="ID"
        allowpaging="true" pagersettings-mode="NumericFirstLast" pagesize="25">
        <Columns>
            <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="course.aspx?ID={0}"
                DataTextField="Name"
                HeaderText="Name"/>
            <asp:BoundField HeaderText="Visible" DataField="Visible" />
            <asp:BoundField HeaderText="Enabled" DataField="Enabled" />
            <asp:BoundField HeaderText="Start date" DataField="StartDate" />
            <asp:BoundField HeaderText="End date" DataField="EndDate" />
            <asp:BoundField HeaderText="Grade type" DataField="GradeType" />
            <asp:HyperLinkField
                DataNavigateUrlFields="Url"
                DataNavigateUrlFormatString="{0}"
                Text="URL"
                HeaderText="Url"/>
        </Columns>
        <EmptyDataTemplate>
            <p>No semesters match the query.</p>
        </EmptyDataTemplate>
    </domino:multiselectgridview>
    <div class="toolbar">
        <asp:LinkButton runat="server" ID="Delete" Text="Delete" OnClick="Delete_Click" 
            OnClientClick="return confirm('Are you sure you want to delete the selected items?')"
            ValidationGroup="Delete" />
    </div>
</asp:Content>
