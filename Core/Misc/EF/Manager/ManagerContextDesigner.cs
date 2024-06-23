using Microsoft.EntityFrameworkCore.Design;

namespace Core.Misc.EF.Manager
{
    internal class ManagerContextDesigner : IDesignTimeDbContextFactory<ManagerContext>
    {
        //dotnet ef migrations add Init --context managercontext --output-dir Migrations/Manager

        public ManagerContext CreateDbContext(string[] args)
        {
            return new(new());
        }
    }
}
