using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using haylie.git;

namespace haylie.git.services { 

    public sealed class GitHubClient : IGitHubClient
    {
        private readonly HttpClient _httpClient;

        public GitHubClient(GitHubOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.Token))
                throw new ArgumentException(
                    "GitHub token is required.",
                    nameof(options));

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(options.ApiBaseUrl)
            };

            _httpClient.DefaultRequestHeaders.Accept.Clear();

            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(
                    "application/vnd.github+json"));

            _httpClient.DefaultRequestHeaders.Add(
                "X-GitHub-Api-Version",
                options.ApiVersion);

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                options.UserAgent);

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    options.Token);
        }

        public Task<GitHubUser> GetAuthenticatedUserAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsync<GitHubUser>("user", cancellationToken);
        }

        public Task<GitHubOrganization> GetOrganizationAsync(
            string organization,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsync<GitHubOrganization>(
                $"orgs/{organization}",
                cancellationToken);
        }

        public async Task<IReadOnlyList<GitHubRepository>>
            GetOrganizationRepositoriesAsync(
                string organization,
                CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetListAsync<GitHubRepository>(
                $"orgs/{organization}/repos?per_page=100",
                cancellationToken);
        }

        public Task<GitHubRepository> GetRepositoryAsync(
            string owner,
            string repository,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsync<GitHubRepository>(
                $"repos/{owner}/{repository}",
                cancellationToken);
        }

        public async Task<IReadOnlyList<GitHubIssue>>
            GetRepositoryIssuesAsync(
                string owner,
                string repository,
                string state = "all",
                CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetListAsync<GitHubIssue>(
                $"repos/{owner}/{repository}/issues" +
                $"?state={state}&per_page=100",
                cancellationToken);
        }

        public async Task<IReadOnlyList<GitHubPullRequest>>
            GetPullRequestsAsync(
                string owner,
                string repository,
                string state = "all",
                CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetListAsync<GitHubPullRequest>(
                $"repos/{owner}/{repository}/pulls" +
                $"?state={state}&per_page=100",
                cancellationToken);
        }

        public async Task<IReadOnlyList<GitHubCommit>>
            GetCommitsAsync(
                string owner,
                string repository,
                CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetListAsync<GitHubCommit>(
                $"repos/{owner}/{repository}/commits?per_page=100",
                cancellationToken);
        }

        public async Task<IReadOnlyList<GitHubEvent>>
            GetRepositoryEventsAsync(
                string owner,
                string repository,
                CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetListAsync<GitHubEvent>(
                $"repos/{owner}/{repository}/events?per_page=100",
                cancellationToken);
        }

        public async Task<IReadOnlyList<GitHubEvent>>
            GetOrganizationEventsAsync(
                string organization,
                CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetListAsync<GitHubEvent>(
                $"orgs/{organization}/events?per_page=100",
                cancellationToken);
        }

        private async Task<T> GetAsync<T>(
            string endpoint,
            CancellationToken cancellationToken)
        {
            using (var response =
                await _httpClient.GetAsync(endpoint, cancellationToken))
            {
                await EnsureSuccessAsync(response);

                var json =
                    await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        private async Task<IReadOnlyList<T>> GetListAsync<T>(
            string endpoint,
            CancellationToken cancellationToken)
        {
            using (var response =
                await _httpClient.GetAsync(endpoint, cancellationToken))
            {
                await EnsureSuccessAsync(response);

                var json =
                    await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<T>>(json);
            }
        }

        private static async Task EnsureSuccessAsync(
            HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            var body =
                await response.Content.ReadAsStringAsync();

            throw new GitHubApiException(
                (int)response.StatusCode,
                response.ReasonPhrase,
                body);
        }
    }

    public sealed class GitHubApiException : Exception
    {
        public int StatusCode { get; }

        public string ResponseBody { get; }

        public GitHubApiException(
            int statusCode,
            string message,
            string responseBody)
            : base(message)
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
        }
    }
}