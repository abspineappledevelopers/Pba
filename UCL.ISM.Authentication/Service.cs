using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace UCL.ISM.Authentication
{
    public class Service
    {
        /// <summary>
        /// Local variables
        /// </summary>
        private readonly WebOptions _webOptions;
        private GraphServiceClient _serviceClient;

        public Service(IOptions<WebOptions> webOptions, GraphServiceClient serviceClient)
        {
            _webOptions = webOptions.Value;
            _serviceClient = serviceClient;
        }

        public Service(IOptions<WebOptions> webOption)
        {
            _webOptions = webOption.Value;
        }

        public async Task<User> GetMeAsync(string accessToken)
        {
            User currentUserObject;

            try
            {
                PrepareAuthenticatedClient(accessToken);
                currentUserObject = await _serviceClient.Me.Request().GetAsync();
            }
            catch (ServiceException ex)
            {

                throw;
            }
            return currentUserObject;
        }

        public async Task<IList<Group>> GetMyGroupsAsync(string accessToken)
        {
            IList<Group> groups = new List<Group>();

            try
            {
                // Get groups the current user is a direct member of.
                IUserMemberOfCollectionWithReferencesPage memberOfGroups = await _serviceClient.Me.MemberOf.Request().GetAsync();
                if (memberOfGroups?.Count > 0)
                {
                    foreach (var directoryObject in memberOfGroups)
                    {
                        // We only want groups, so ignore DirectoryRole objects.
                        if (directoryObject is Group)
                        {
                            Group group = directoryObject as Group;
                            groups.Add(group);
                        }
                    }
                }

                // If paginating
                while (memberOfGroups.NextPageRequest != null)
                {
                    memberOfGroups = await memberOfGroups.NextPageRequest.GetAsync();

                    if (memberOfGroups?.Count > 0)
                    {
                        foreach (var directoryObject in memberOfGroups)
                        {
                            // We only want groups, so ignore DirectoryRole objects.
                            if (directoryObject is Group)
                            {
                                Group group = directoryObject as Group;
                                groups.Add(group);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return groups;
        }

        private void PrepareAuthenticatedClient(string accessToken)
        {
            if (_serviceClient == null)
            {
                try
                {
                    _serviceClient = new GraphServiceClient(_webOptions.GraphApiUrl,
                                                                new DelegateAuthenticationProvider(
                                                                    async (requestMessage) =>
                                                                    {
                                                                        await Task.Run(() =>
                                                                        {
                                                                            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
                                                                        });
                                                                    }));
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
