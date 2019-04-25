using System;
using System.Collections.Generic;
using System.Text;

using crm = Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk;

namespace UCL.ISM.Authentication
{
    public class Service
    {
        /// <summary>
        /// Local variables
        /// </summary>
        private string _discoveryService;
        private string _orgUniqueName;

        private crm.Client.IServiceManagement<crm.Discovery.IDiscoveryService> _discoveryManagement;
        private crm.Client.DiscoveryServiceProxy _discoveryProxy;

        private string _orgUri;

        //private crm.Client.AuthenticationProviderType _type;
        private Guid _userId;

        public Service(string discoveryService)
        {
            _orgUri = string.Empty;
            _discoveryManagement = crm.Client.ServiceConfigurationFactory.CreateManagement<crm.Discovery.IDiscoveryService>(new Uri(discoveryService));
            //_type = _discoveryManagement.AuthenticationType;

            using (_discoveryProxy)
            {
                if (_discoveryProxy != null)
                {
                    crm.Discovery.OrganizationDetailCollection organizationDetails = DiscoverOrganizations(_discoveryProxy);
                    _orgUri = FindOrganization(_orgUniqueName, organizationDetails.ToArray()).Endpoints[crm.Discovery.EndpointType.OrganizationService];
                }
            }
        }

        /// <summary>
        /// Discovers the organizations that the calling user belongs to.
        /// </summary>
        /// <param name="service">A Discovery service proxy instance.</param>
        /// <returns>Array containing detailed information on each organization that 
        /// the user belongs to.</returns>
        public crm.Discovery.OrganizationDetailCollection DiscoverOrganizations(
            crm.Discovery.IDiscoveryService service)
        {
            if (service == null) throw new ArgumentNullException("service");
            crm.Discovery.RetrieveOrganizationsRequest orgRequest = new crm.Discovery.RetrieveOrganizationsRequest();
            crm.Discovery.RetrieveOrganizationsResponse orgResponse =
                (crm.Discovery.RetrieveOrganizationsResponse)service.Execute(orgRequest);

            return orgResponse.Details;
        }

        /// <summary>
        /// Finds a specific organization detail in the array of organization details
        /// returned from the Discovery service.
        /// </summary>
        /// <param name="orgUniqueName">The unique name of the organization to find.</param>
        /// <param name="orgDetails">Array of organization detail object returned from the discovery service.</param>
        /// <returns>Organization details or null if the organization was not found.</returns>
        /// <seealso cref="DiscoveryOrganizations"/>
        public crm.Discovery.OrganizationDetail FindOrganization(string orgUniqueName,
            crm.Discovery.OrganizationDetail[] orgDetails)
        {
            if (String.IsNullOrWhiteSpace(orgUniqueName))
                throw new ArgumentNullException("orgUniqueName");
            if (orgDetails == null)
                throw new ArgumentNullException("orgDetails");
            crm.Discovery.OrganizationDetail orgDetail = null;

            foreach (crm.Discovery.OrganizationDetail detail in orgDetails)
            {
                if (String.Compare(detail.UniqueName, orgUniqueName,
                    StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    orgDetail = detail;
                    break;
                }
            }
            return orgDetail;
        }
    }
}
