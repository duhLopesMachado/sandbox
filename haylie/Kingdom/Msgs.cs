using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Kingdom
{
    public static class Msgs
    {
        public const string USER_NOTFOUND = @"USUÁRIO NÃO ENCONTRADO";
        public const string USER_ALREADY_EXISTS = @"USUÁRIO/EMAIL JÁ EXISTE";
        public const string USER_LOGOUT = @"SEE U LATER BOSS ;]";

        public const string WWW_REQUEST = @"Hey boss []´<br/>Click to request integratrion with your site and manage all from here";
        public const string WWW_REQUEST_WAITING = @"Hey boss []´<br/>Processing your request, building the future ;] ";

        public const string SYS_ERROR = @"SYSTEM ERROR, SORRY";

        public const string MAIL_WELCOME = @"Welcome to the IOT ;]";
        //public const string MAIL_RECOVER = @"Click to <a href='http://localhost:9866/User/GuessWhosBack?p_token={0}'>change your password</a> <br/>";
        public const string MAIL_RECOVER = @"Click to <a href='#'>change your password</a> <br/>";
        public const string MAIL_RESET = @"Your password has been modified";
        public const string MAIL_CUSTOM = @"Welcome to the IOT ;]";
    }
}