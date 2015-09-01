using System;
using System.Security;
using System.Security.Permissions;

namespace Editable.Host.Resolver
{
    internal class AppDomainKeeper : IDisposable
    {
        public AppDomain Domain { get; }

        public AppDomainKeeper(string domainName)
        {
            var setup = new AppDomainSetup {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory
            };
            var permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));
            permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            Domain = AppDomain.CreateDomain(domainName, null, setup, permissions);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                AppDomain.Unload(Domain);
            }
        }
    }
}