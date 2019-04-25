using System;
using System.Collections.Generic;
using System.Text;

using crm = Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk;
using System.ServiceModel;

namespace UCL.ISM.Authentication
{
    public class Class1
    {
        private crm.Client.IServiceManagement<crm.IOrganizationService> _orgManagement;
        private crm.Client.OrganizationServiceProxy _orgProxy;
        private crm.Client.AuthenticationCredentials _credentials;

        private Guid _userId;

        public Class1(crm.Client.IServiceManagement<crm.Discovery.IDiscoveryService> discoveryManagement, string uri, string userName, string password, string domain)
        {
            if (discoveryManagement.AuthenticationType != crm.Client.AuthenticationProviderType.ActiveDirectory)
            {
                _credentials = new crm.Client.AuthenticationCredentials();
                _credentials.ClientCredentials.UserName.UserName = userName;
                _credentials.ClientCredentials.UserName.Password = password.ToString();
                _credentials.SupportingCredentials = new crm.Client.AuthenticationCredentials();

                //_credentials.SupportingCredentials.ClientCredentials = Microsoft.Crm.Services.Utility.DeviceIdManager.LoadOrRegisterDevice();
                _orgManagement = crm.Client.ServiceConfigurationFactory.CreateManagement<crm.IOrganizationService>(new Uri(uri));
                crm.Client.AuthenticationCredentials token = _orgManagement.Authenticate(_credentials);
                _orgProxy = new crm.Client.OrganizationServiceProxy(_orgManagement, token.SecurityTokenResponse);
            }
            else
            {
                _credentials = new crm.Client.AuthenticationCredentials();
                _credentials.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(userName, password, domain);

                _orgManagement = crm.Client.ServiceConfigurationFactory.CreateManagement<crm.IOrganizationService>(new Uri(uri));
                _orgProxy = new crm.Client.OrganizationServiceProxy(_orgManagement, _credentials.ClientCredentials);
            }
        }
    }
}
