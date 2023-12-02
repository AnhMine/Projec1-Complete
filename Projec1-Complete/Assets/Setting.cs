using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.Assets
{
    public class Setting : ApplicationSettingsBase
    {
        [UserScopedSetting]
        public string Username
        {
            get { return (string)this[nameof(Username)]; }
            set { this[nameof(Username)] = value; }
        }

        [UserScopedSetting]
        public string Password
        {
            get { return (string)this[nameof(Password)]; }
            set { this[nameof(Password)] = value; }
        }
    }
}
