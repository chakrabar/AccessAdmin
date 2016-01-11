using System;

namespace AccessManagement.Application.CommandStack
{
    public interface ILdapSyncFacade
    {
        void SyncFromLdap();
    }
}
