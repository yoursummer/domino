﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.master" AutoEventWireup="true" CodeBehind="AssignmentList.aspx.cs" Inherits="Complex.Domino.Web.Student.AssignmentList" %>

<asp:Content ContentPlaceHolderID="main" runat="server">
    <h1>All assignments</h1>
    <asp:ObjectDataSource runat="server" ID="assignmentDataSource" DataObjectTypeName="Complex.Domino.Lib.Assignment"
        OnObjectCreating="assignmentDataSource_ObjectCreating" TypeName="Complex.Domino.Lib.AssignmentFactory"
        SelectMethod="Find"
        SelectCountMethod="Count"
        StartRowIndexParameterName="from"
        MaximumRowsParameterName="max"
        EnablePaging="true" />
    <domino:multiselectgridview id="assignmentList" runat="server" datasourceid="assignmentDataSource"
        autogeneratecolumns="false" datakeynames="ID"
        allowpaging="true" pagersettings-mode="NumericFirstLast" pagesize="25">
        <Columns>
            <domino:SelectionField ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:HyperLinkField
                DataNavigateUrlFields="CourseID"
                DataNavigateUrlFormatString="course.aspx?ID={0}"
                DataTextField="CourseName"
                HeaderText="Course"/>
            <asp:HyperLinkField
                DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="assignment.aspx?ID={0}"
                DataTextField="Name"
                HeaderText="Name"/>
            <asp:BoundField HeaderText="Visible" DataField="Visible" />
            <asp:BoundField HeaderText="Enabled" DataField="Enabled" />
            <asp:BoundField HeaderText="Start date" DataField="StartDate" />
            <asp:BoundField HeaderText="End date" DataField="EndDate" />
            <asp:BoundField HeaderText="Grade type" DataField="GradeType" />
            <asp:BoundField HeaderText="Grade weight" DataField="GradeWeight" />
            <asp:HyperLinkField
                DataNavigateUrlFields="Url"
                DataNavigateUrlFormatString="{0}"
                Text="URL"
                HeaderText="Url"/>
        </Columns>
        <EmptyDataTemplate>
            <p>No assignments match the query.</p>
        </EmptyDataTemplate>
    </domino:multiselectgridview>
</asp:Content>
