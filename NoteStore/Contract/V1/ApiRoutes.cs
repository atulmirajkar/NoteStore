using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteStore.Contract.V1
{

    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Notes
        {
            public const string GetAll = Base + "/notes";

            public const string Get = Base + "/notes/{noteId}";
            public const string Create = Base + "/notes";

            public const string Update = Base + "/notes/{noteId}";

            public const string Delete = Base + "/notes/{noteId}";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";

            public const string Refresh = Base + "/identity/refresh";

            public const string Logout = Base + "/identity/logout";

        }
    }
}
