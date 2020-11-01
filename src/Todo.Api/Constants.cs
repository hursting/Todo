using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Api
{
    static class Constants
    {
        public const string queue_person = "queue-person-insert";

        public static class Person
        {
            public const string Insert = "queue-person-insert";
        }

        public static class HttpActions
        {
            public const string Get = "get";
            public const string Post = "post";
            public const string Delete = "delete";
        }
    }
}
