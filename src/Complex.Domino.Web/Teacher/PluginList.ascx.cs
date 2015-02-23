﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Complex.Domino.Lib;
using Complex.Domino.Plugins;

namespace Complex.Domino.Web.Teacher
{
    public partial class PluginList : UserControlBase
    {
        public int SemesterID
        {
            get { return (int)(ViewState["SemesterID"] ?? -1); }
            set { ViewState["SemesterID"] = value; }
        }

        public int CourseID
        {
            get { return (int)(ViewState["CourseID"] ?? -1); }
            set { ViewState["CourseID"] = value; }
        }

        public int AssignmentID
        {
            get { return (int)(ViewState["AssignmentID"] ?? -1); }
            set { ViewState["AssignmentID"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshPluginList();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            CreatePluginControls();
        }

        protected void AddPlugin_Click(object sender, EventArgs e)
        {
            var pm = new PluginManager(DatabaseContext);

            var pp = pm.GetPlugin(PluginType.SelectedValue);

            pp.Instance.SemesterID = SemesterID;
            pp.Instance.CourseID = CourseID;
            pp.Instance.AssignmentID = AssignmentID;

            pp.Save();
        }

        private void RefreshPluginList()
        {
            var pm = new PluginManager(DatabaseContext);

            PluginType.Items.Clear();
            PluginType.Items.Add(new ListItem(Resources.Labels.SelectPlugin, ""));

            foreach (var plugin in pm.EnumeratePlugins())
            {
                PluginType.Items.Add(new ListItem(plugin.Description, plugin.TypeName));
            }
        }

        private void CreatePluginControls()
        {
            var pm = new PluginManager(DatabaseContext);

            // Load plugins associated with entity and create controls
            pluginsPlaceholder.Controls.Clear();

            var pf = new PluginFactory(DatabaseContext)
            {
                SemesterID = SemesterID,
                CourseID = CourseID,
                AssignmentID = AssignmentID,
            };

            foreach (var plugin in pf.Find())
            {
                var pp = pm.GetPlugin(plugin);
                var control = LoadControl(pp.ControlFileName);

                pluginsPlaceholder.Controls.Add(control);
            }
        }
    }
}