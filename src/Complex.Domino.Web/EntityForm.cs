﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web
{
    public abstract class EntityForm<T> : PageBase
        where T : Lib.Entity, new()
    {
        private T item;

        protected ITextControl Name;
        protected CheckBox Enabled;
        protected CheckBox Visible;

        protected T Item
        {
            get { return item; }
        }

        protected void CreateItem()
        {
            item = new T()
            {
                Context = DatabaseContext
            };

            item.ID = int.Parse(Request["ID"] ?? "0");

            if (item.ID > 0)
            {
                item.Load();
            }
        }

        protected virtual void UpdateForm()
        {
            Name.Text = item.Name;
            Enabled.Checked = item.Enabled;
            Visible.Checked = item.Visible;
        }

        protected virtual void SaveForm()
        {
            item.Name = Name.Text;
            item.Enabled = Enabled.Checked;
            item.Visible = Visible.Checked;
        }

        protected virtual void DataBindForm()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CreateItem();

            if (!IsPostBack)
            {
                UpdateForm();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            DataBindForm();
        }

        protected void Ok_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                SaveForm();
                item.Save();

                Response.Redirect(OriginalReferer);
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(OriginalReferer);
        }
    }
}