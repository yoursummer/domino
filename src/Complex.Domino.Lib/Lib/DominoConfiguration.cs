﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Complex.Domino.Lib
{
    public class DominoConfiguration : ConfigurationSection
    {
        public static DominoConfiguration Instance
        {
            get
            {
                return (DominoConfiguration)ConfigurationManager.GetSection("complex.domino/domino");
            }
        }

        private static ConfigurationPropertyCollection properties;

        private static readonly ConfigurationProperty propScratchPath = new ConfigurationProperty(
            "scratchPath", typeof(string), String.Empty, ConfigurationPropertyOptions.None);

        private static readonly ConfigurationProperty propRepositoriesPath = new ConfigurationProperty(
            "repositoriesPath", typeof(string), String.Empty, ConfigurationPropertyOptions.None);

        private static readonly ConfigurationProperty propEmailFromAddress = new ConfigurationProperty(
            "emailFromAddress", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

        private static readonly ConfigurationProperty propEmailFromName = new ConfigurationProperty(
            "emailFromName", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

        private static readonly ConfigurationProperty propEmailNoreplyAddress = new ConfigurationProperty(
            "emailNoreplyAddress", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

        static DominoConfiguration()
        {
            properties = new ConfigurationPropertyCollection();

            properties.Add(propScratchPath);
            properties.Add(propRepositoriesPath);
        }

        [ConfigurationProperty("scratchPath")]
        public string ScratchPath
        {
            get { return (string)base[propScratchPath]; }
            set { base[propScratchPath] = value; }
        }

        [ConfigurationProperty("repositoriesPath")]
        public string RepositoriesPath
        {
            get { return (string)base[propRepositoriesPath]; }
            set { base[propRepositoriesPath] = value; }
        }

        [ConfigurationProperty("emailFromAddress")]
        public string EmailFromAddress
        {
            get { return (string)base[propEmailFromAddress]; }
            set { base[propEmailFromAddress] = value; }
        }

        [ConfigurationProperty("emailFromName")]
        public string EmailFromName
        {
            get { return (string)base[propEmailFromName]; }
            set { base[propEmailFromName] = value; }
        }

        [ConfigurationProperty("emailNoreplyAddress")]
        public string EmailNoreplyAddress
        {
            get { return (string)base[propEmailNoreplyAddress]; }
            set { base[propEmailNoreplyAddress] = value; }
        }
    }
}
