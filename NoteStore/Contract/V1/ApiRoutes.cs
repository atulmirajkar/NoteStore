﻿using System;
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
            public const string Create = Base + "/create";
        }
    }
}
