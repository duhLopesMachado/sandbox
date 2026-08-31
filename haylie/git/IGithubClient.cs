using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using haylie.git;
using haylie.git.dtos;

namespace haylie.git.interfaces {

    public interface IGitHubClient
    {
        Task<GitHubUser> GetAuthenticatedUserAsync(
            CancellationToken cancellationToken = default(CancellationToken));

        Task<GitHubOrganization> GetOrganizationAsync(
            string organization,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IReadOnlyList<GitHubRepository>> GetOrganizationRepositoriesAsync(
            string organization,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<GitHubRepository> GetRepositoryAsync(
            string owner,
            string repository,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IReadOnlyList<GitHubIssue>> GetRepositoryIssuesAsync(
            string owner,
            string repository,
            string state = "all",
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IReadOnlyList<GitHubPullRequest>> GetPullRequestsAsync(
            string owner,
            string repository,
            string state = "all",
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IReadOnlyList<GitHubCommit>> GetCommitsAsync(
            string owner,
            string repository,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IReadOnlyList<GitHubEvent>> GetRepositoryEventsAsync(
            string owner,
            string repository,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IReadOnlyList<GitHubEvent>> GetOrganizationEventsAsync(
            string organization,
            CancellationToken cancellationToken = default(CancellationToken));
    }

}