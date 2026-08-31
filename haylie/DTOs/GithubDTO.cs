using System;

namespace haylie.git.dtos
{
    public class GitHubUser
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Avatar_Url { get; set; }
        public string Html_Url { get; set; }

        public string Type { get; set; }
        public bool Site_Admin { get; set; }

        public int Public_Repos { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }

        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }

    public class GitHubOrganization
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Html_Url { get; set; }
        public string Avatar_Url { get; set; }

        public string Type { get; set; }

        public int Public_Repos { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }

        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }

    public class GitHubCommit
    {
        public string Sha { get; set; }
        public string Html_Url { get; set; }

        public GitHubCommitAuthor Author { get; set; }
        public GitHubCommitAuthor Committer { get; set; }

        public GitHubCommitDetails Commit { get; set; }
    }

    public class GitHubCommitDetails
    {
        public GitHubCommitMessage Message { get; set; }

        public GitHubCommitAuthor Author { get; set; }
        public GitHubCommitAuthor Committer { get; set; }
    }

    public class GitHubCommitMessage
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }

    public class GitHubCommitAuthor
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Avatar_Url { get; set; }
        public string Html_Url { get; set; }
    }

    public class GitHubIssue
    {
        public long Id { get; set; }
        public long Number { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }

        public string Html_Url { get; set; }
        public string State { get; set; }

        public bool Locked { get; set; }

        public GitHubUser User { get; set; }
        public GitHubUser Assignee { get; set; }

        public GitHubLabel[] Labels { get; set; }

        public int Comments { get; set; }

        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public DateTime Closed_At { get; set; }

        // GitHub returns pull requests as a property
        // when the issue represents a PR.
        public GitHubPullRequestLink Pull_Request { get; set; }
    }

    public class GitHubPullRequest
    {
        public long Id { get; set; }
        public long Number { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Html_Url { get; set; }
        public string State { get; set; }
        public bool Locked { get; set; }
        public GitHubUser User { get; set; }
        public GitHubUser Assignee { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public DateTime Closed_At { get; set; }
        public DateTime Merged_At { get; set; }
    }

    public class GitHubLabel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }

        public bool Default { get; set; }
    }

    public class GitHubPullRequestLink
    {
        public string Url { get; set; }
        public string Html_Url { get; set; }
        public string Diff_Url { get; set; }
        public string Patch_Url { get; set; }
    }

    /*--- [ activity ] ------------------------------------------ . ---*/

    public class GitHubEvent
    {
        public string Id { get; set; }
        public string Type { get; set; }

        public GitHubUser Actor { get; set; }
        public GitHubEventRepository Repo { get; set; }

        public string Public { get; set; }

        public DateTime Created_At { get; set; }

        // Event-specific payload.
        // Keep this flexible for v1 because GitHub has
        // many different event payload structures.
        public object Payload { get; set; }
    }

    public class GitHubEventRepository
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

}