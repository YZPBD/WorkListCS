﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace CSWorkListService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            InitServiceName();
        }

        private void InitServiceName()
        {
            using (SettingHelper setting = new SettingHelper())
            {
                serviceInstaller1.ServiceName = setting.ServiceName;
                serviceInstaller1.DisplayName = setting.DisplayName;
                serviceInstaller1.Description = setting.Description;
            }
        }
    }
}
