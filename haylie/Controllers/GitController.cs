using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using haylie.git;
using haylie.git.dtos;
using haylie.git.interfaces;
using haylie.git.services;


namespace haylie.Controllers
{
    public class GitController : ApiController
    {
        private readonly GitHubOptions _opts = new GitHubOptions();
        private readonly GitHubClient _service;

        public GitController()
        {
            _service = new GitHubClient(_opts);
        }

        [HttpGet]
        [ActionName("User")]
        public async Task<HttpResponseMessage> AuthenticatedUser(CancellationToken cancellationToken)
        {
            return await Execute(() => _service.GetAuthenticatedUserAsync(cancellationToken));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Organization(string organization, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(organization))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "organization is required.");

            return await Execute(() => _service.GetOrganizationAsync(organization, cancellationToken));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Repositories(string organization, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(organization))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "organization is required.");

            return await Execute(() => _service.GetOrganizationRepositoriesAsync(organization, cancellationToken));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Repository(string owner, string repository, CancellationToken cancellationToken)
        {
            if (!HasRepository(owner, repository))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "owner and repository are required.");

            return await Execute(() => _service.GetRepositoryAsync(owner, repository, cancellationToken));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Issues(string owner, string repository, string state = "all", CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!HasRepository(owner, repository))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "owner and repository are required.");

            return await Execute(() => _service.GetRepositoryIssuesAsync(owner, repository, state, cancellationToken));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> PullRequests(string owner, string repository, string state = "all", CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!HasRepository(owner, repository))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "owner and repository are required.");

            return await Execute(() => _service.GetPullRequestsAsync(owner, repository, state, cancellationToken));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Commits(string owner, string repository, CancellationToken cancellationToken)
        {
            if (!HasRepository(owner, repository))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "owner and repository are required.");

            return await Execute(() => _service.GetCommitsAsync(owner, repository, cancellationToken));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> RepositoryEvents(string owner, string repository, CancellationToken cancellationToken)
        {
            if (!HasRepository(owner, repository))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "owner and repository are required.");

            return await Execute(() => _service.GetRepositoryEventsAsync(owner, repository, cancellationToken));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> OrganizationEvents(string organization, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(organization))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "organization is required.");

            return await Execute(() => _service.GetOrganizationEventsAsync(organization, cancellationToken));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Dashboard(string organization, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(organization))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "organization is required.");

            try
            {
                var organizationTask = _service.GetOrganizationAsync(organization, cancellationToken);
                var repositoriesTask = _service.GetOrganizationRepositoriesAsync(organization, cancellationToken);
                var eventsTask = _service.GetOrganizationEventsAsync(organization, cancellationToken);
                await Task.WhenAll(organizationTask, repositoriesTask, eventsTask);

                var repositories = repositoriesTask.Result;
                var repositoryData = await Task.WhenAll(repositories.Select(repository => LoadRepositoryData(organization, repository, cancellationToken)));
                var issues = repositoryData.SelectMany(data => data.Issues).ToList();
                var pullRequests = repositoryData.SelectMany(data => data.PullRequests).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    generatedAt = DateTime.UtcNow,
                    organization = organizationTask.Result,
                    repositories,
                    issues,
                    pullRequests,
                    commits = repositoryData.SelectMany(data => data.Commits).ToList(),
                    repositoryEvents = repositoryData.SelectMany(data => data.Events).ToList(),
                    organizationEvents = eventsTask.Result,
                    insights = new
                    {
                        repositoryCount = repositories.Count,
                        activeRepositoryCount = repositories.Count(repository => !repository.Archived),
                        openIssueCount = issues.Count(issue => string.Equals(issue.issue.State, "open", StringComparison.OrdinalIgnoreCase)),
                        highPriorityIssueCount = issues.Count(issue => issue.priority == "high"),
                        openPullRequestCount = pullRequests.Count(request => string.Equals(request.request.State, "open", StringComparison.OrdinalIgnoreCase)),
                        recentlyPushedRepositoryCount = repositories.Count(repository => repository.Pushed_At >= DateTime.UtcNow.AddDays(-30)),
                        topLabels = issues.SelectMany(issue => issue.issue.Labels ?? new GitHubLabel[0])
                            .GroupBy(label => label.Name)
                            .OrderByDescending(group => group.Count())
                            .Take(10)
                            .Select(group => new { name = group.Key, count = group.Count() })
                    }
                });
            }
            catch (GitHubApiException exception)
            {
                return Request.CreateErrorResponse((HttpStatusCode)exception.StatusCode, exception.Message);
            }
        }

        private async Task<RepositoryDashboardData> LoadRepositoryData(string organization, GitHubRepository repository, CancellationToken cancellationToken)
        {
            var owner = repository.Owner == null || string.IsNullOrWhiteSpace(repository.Owner.Login)
                ? organization
                : repository.Owner.Login;
            var issuesTask = _service.GetRepositoryIssuesAsync(owner, repository.Name, "all", cancellationToken);
            var pullRequestsTask = _service.GetPullRequestsAsync(owner, repository.Name, "all", cancellationToken);
            var commitsTask = _service.GetCommitsAsync(owner, repository.Name, cancellationToken);
            var eventsTask = _service.GetRepositoryEventsAsync(owner, repository.Name, cancellationToken);
            await Task.WhenAll(issuesTask, pullRequestsTask, commitsTask, eventsTask);

            return new RepositoryDashboardData
            {
                Issues = issuesTask.Result
                    .Where(issue => issue.Pull_Request == null)
                    .Select(issue => new DashboardIssue { issue = issue, repository = repository, priority = GetPriority(issue) })
                    .ToList(),
                PullRequests = pullRequestsTask.Result.Select(request => new DashboardPullRequest { request = request, repository = repository }).ToList(),
                Commits = commitsTask.Result.Select(commit => new DashboardCommit { commit = commit, repository = repository }).ToList(),
                Events = eventsTask.Result.Select(activity => new DashboardEvent { activity = activity, repository = repository }).ToList()
            };
        }

        private static string GetPriority(GitHubIssue issue)
        {
            var priority = (issue.Labels ?? new GitHubLabel[0])
                .Select(label => label.Name ?? string.Empty)
                .FirstOrDefault(name => name.StartsWith("priority", StringComparison.OrdinalIgnoreCase));

            if (priority == null)
                return "normal";

            if (priority.IndexOf("critical", StringComparison.OrdinalIgnoreCase) >= 0 || priority.IndexOf("high", StringComparison.OrdinalIgnoreCase) >= 0)
                return "high";

            if (priority.IndexOf("low", StringComparison.OrdinalIgnoreCase) >= 0)
                return "low";

            return "normal";
        }

        private async Task<HttpResponseMessage> Execute<T>(Func<Task<T>> operation)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, await operation());
            }
            catch (GitHubApiException exception)
            {
                return Request.CreateErrorResponse((HttpStatusCode)exception.StatusCode, exception.Message);
            }
        }

        private static bool HasRepository(string owner, string repository)
        {
            return !string.IsNullOrWhiteSpace(owner) && !string.IsNullOrWhiteSpace(repository);
        }

        private sealed class RepositoryDashboardData
        {
            public List<DashboardIssue> Issues { get; set; }
            public List<DashboardPullRequest> PullRequests { get; set; }
            public List<DashboardCommit> Commits { get; set; }
            public List<DashboardEvent> Events { get; set; }
        }

        private sealed class DashboardIssue
        {
            public GitHubIssue issue { get; set; }
            public GitHubRepository repository { get; set; }
            public string priority { get; set; }
        }

        private sealed class DashboardPullRequest
        {
            public GitHubPullRequest request { get; set; }
            public GitHubRepository repository { get; set; }
        }

        private sealed class DashboardCommit
        {
            public GitHubCommit commit { get; set; }
            public GitHubRepository repository { get; set; }
        }

        private sealed class DashboardEvent
        {
            public GitHubEvent activity { get; set; }
            public GitHubRepository repository { get; set; }
        }

        // [HttpPost]
        // public async Task<HttpResponseMessage> Create(CheckoutRequest request)
        // {
        //     var result = await _service.CreateCheckout(request);

        //     return Request.CreateResponse(HttpStatusCode.OK, result);
        // }
        // [HttpPost]
        // public async Task<HttpResponseMessage> Receive()
        // {
        //     var body = await Request.Content.ReadAsStringAsync();

        //     await _service.ProcessWebhook(body);

        //     return Request.CreateResponse(HttpStatusCode.OK);
        // }

    }
}


/*
ConfigurationSettings.AppSettings["super_nf"]

*/