using HRIS.Data;
using HRIS.IProviders;

namespace HRIS.Providers
{
    public class HrProvider : IHrProvider
    {
        private readonly HrDbContext _context;

        public HrProvider(HrDbContext context)
        {
            _context = context;
        }
    }
}
