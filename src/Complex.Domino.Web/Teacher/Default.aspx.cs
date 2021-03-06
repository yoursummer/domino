﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Default : PageBase
    {
        public static string GetUrl()
        {
            return "~/Teacher";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NameLabel.Text = String.Format(Resources.Labels.Hello, DatabaseContext.User.Description);
        }
    }
}