using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.git { 

    public class GitHubOptions
    {
        public string ApiBaseUrl { get; set; } = "https://api.github.com/";
        public string ApiVersion { get; set; } = "2026-03-10";
        public string Token { get; set; } = "";
        public string UserAgent { get; set; } = "BWA-GitDash/1.0";
    }

    public class GitHubRepository
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Full_Name { get; set; }
        public string Html_Url { get; set; }
        public string Description { get; set; }

        public bool Private { get; set; }
        public bool Archived { get; set; }

        public string Default_Branch { get; set; }

        public GitHubOwner Owner { get; set; }

        public int Open_Issues_Count { get; set; }

        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public DateTime Pushed_At { get; set; }
    }

    public class GitHubOwner
    {
        public string Login { get; set; }
        public long Id { get; set; }
        public string Avatar_Url { get; set; }
    }

}