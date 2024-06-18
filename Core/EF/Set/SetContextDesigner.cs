using Microsoft.EntityFrameworkCore.Design;

namespace Core.EF.Set
{
    internal class SetContextDesigner : IDesignTimeDbContextFactory<SetContext>
    {
        //dotnet ef migrations add Init --context setcontext --output-dir Migrations/Set

        public SetContext CreateDbContext(string[] args)
        {
            return new(new());
        }
    }
}
