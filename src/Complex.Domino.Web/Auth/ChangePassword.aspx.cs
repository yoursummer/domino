﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security;
using System.Text;
using Complex.Domino.Lib;

namespace Complex.Domino.Web.Auth
{
    public partial class ChangePassword : PageBase
    {
        private Lib.User item;

        public static string GetUrl(Uri returnUrl)
        {
            return GetUrl(returnUrl.ToString());
        }

        public static string GetUrl(string returnUrl)
        {
            return String.Format("~/Auth/ChangePassword.aspx?returnUrl={0}", HttpUtility.UrlEncode(returnUrl));
        }

        public static string GetResetUrl(string activationCode)
        {
            return String.Format("~/Auth/ChangePassword.aspx?code={0}", activationCode);
        }

        protected string ActivationCode
        {
            get { return (string)Request.QueryString[Constants.RequestActivationCode]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ActivationCode != null)
            {
                var uf = new UserFactory(DatabaseContext);
                item = uf.LoadByActivationCode(ActivationCode);

                OldPasswordRow.Visible = false;
            }
            else if (DatabaseContext.User != null)
            {
                item = new Lib.User(DatabaseContext);
                item.Load(DatabaseContext.User.ID);
            }
            else
            {
                throw Lib.Error.AccessDenied();
            }
        }
        
        protected void Ok_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                item.ResetPassword(PasswordNew.Text);

                SendEmail();

                if (ActivationCode != null)
                {
                    Util.Url.RedirectTo(SignIn.GetUrl(ReturnUrl));
                }
                else
                {
                    formPanel.Visible = false;
                    messagePanel.Visible = true;
                }
            }
        }

        private void SendEmail()
        {
            var body = new StringBuilder(Resources.EmailTemplates.ChangePassword);

            var tokens = new Dictionary<string, string>()
                {
                     { "Name", item.Description },
                     { "DateTime", DateTime.Now.ToString() }
                };

            Util.Email.ReplaceTokens(body, tokens);

            Util.Email.SendFromDomino(
                item,
                Resources.EmailTemplates.ChangePasswordSubject,
                body.ToString());
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Util.Url.RedirectTo(ReturnUrl);
        }

        protected void PasswordValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = item.VerifyPassword(args.Value);
        }

        protected void PasswordComplexityValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool upper = false;
            bool lower = false;
            bool digits = false;

            for (int i = 0; i < args.Value.Length; i++)
            {
                var c = args.Value[i];

                upper |= Char.IsUpper(c);
                lower |= Char.IsLower(c);
                digits |= Char.IsDigit(c);
            }

            args.IsValid = args.Value.Length > 3 && upper && lower && digits;
        }

        protected void PasswordConfirmValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = StringComparer.InvariantCulture.Compare(PasswordNew.Text, PasswordConfirm.Text) == 0;
        }
    }
}