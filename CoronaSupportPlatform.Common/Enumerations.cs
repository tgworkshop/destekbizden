using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSupportPlatform.Common
{
    // ENTITIES
    public enum EntityStatus : short
    {
        Unknown = -999,

        Deleted = -99,

        Blocked = -12,

        Freezed = -11,

        Passive = -1,

        Draft = 0,

        Active = 1
    }

    // SERVICES
    public enum ServiceResponseTypes
    {
        Error = -99,

        Declined = -1,

        Unknown = 0,

        Success = 1,

        Completed = 10
    }   

    // LOGGING
    public enum LogMode
    {
        All,

        Debug,

        Info,

        Warn,

        Error,

        Fatal,

        None
    }

    // TAGS
    public enum TagType : short
    {
        Other = 0,

        Attribute = 1,

        Badge = 2,

        Group = 3,

        Administrative = 99
    }

    //PASSWORDS
    public enum PasswordChange
    {
        Success = 1,

        Error = 0,

        FormatNotApplicable = -77,

        PasswordsDoNotMatch = -88,

        WrongPassword = -99
    }
}
