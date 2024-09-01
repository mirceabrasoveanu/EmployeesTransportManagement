using EmployeesTransportManagement.Data;
using EmployeesTransportManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;

namespace EmployeesTransportManagement.Controllers
{
    public class SettlementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettlementController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Projects = new SelectList(_context.Projects, "ProjectId", "ProjectName");
            return View(new Settlement());
        }

        [HttpPost]
        public async Task<IActionResult> Register(Settlement settlement)
        {
            settlement.EmployeeId = Guid.Parse("ee533cfe-625a-49e3-a4c6-c922537bfdbc");
            settlement.DateSubmitted = DateTime.Now;
            if (settlement.Amount < 100)
            {
                settlement.IsApproved = true;
            }
            _context.Settlements.Add(settlement);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> PendingApprovals()
        {
            // Fetch settlements that are not yet approved
            var coordId = Guid.Parse("ce624eb2-d0cb-43c6-a486-b6206ee2e99d");
            var pendingSettlements = await _context.Settlements
                .Include(s => s.Employee)
                .Include(s => s.Project)
                .Where(s => !s.IsApproved && s.Project.CoordinatorId == coordId && !s.IsRejected)
                .ToListAsync();

            return View(pendingSettlements);
        }

        [HttpGet]
        public async Task<IActionResult> Approve(Guid id)
        {
            var settlement = await _context.Settlements
                .Include(s => s.Employee)
                .Include(s => s.Project)
                .FirstOrDefaultAsync(s => s.SettlementId == id);

            if (settlement == null)
            {
                return NotFound();
            }

            return View(settlement);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(Guid id, string approval)
        {
            var settlement = await _context.Settlements.FindAsync(id);

            if (settlement == null)
            {
                return NotFound();
            }

            settlement.IsApproved = approval == "approve";
            if(settlement.IsApproved == false)
            {
                settlement.IsRejected = true;
            }
            await _context.SaveChangesAsync();

            // Send email logic here

            return RedirectToAction("PendingApprovals", "Settlement");
        }
    }

}
