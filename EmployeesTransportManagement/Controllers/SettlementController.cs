﻿using EmployeesTransportManagement.Data;
using EmployeesTransportManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;
using System.Security.Claims;

namespace EmployeesTransportManagement.Controllers
{
    public class SettlementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettlementController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Employee")]
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Projects = new SelectList(_context.Projects, "ProjectId", "ProjectName");
            return View(new Settlement());
        }
        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<IActionResult> Register(Settlement settlement)
        {
            //settlement.EmployeeId = Guid.Parse("ee533cfe-625a-49e3-a4c6-c922537bfdbc");
            if (settlement.Employee == null)
            {
                var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (!string.IsNullOrEmpty(email))
                {
                    settlement.Employee = await _context.Employees.Where(e => e.Email == email).FirstOrDefaultAsync();
                }
                if (settlement.Employee == null)
                {
                    return NotFound();
                }
                settlement.EmployeeId = settlement.Employee.Id;
            }
            settlement.DateSubmitted = DateTime.Now;
            if (settlement.Amount < 100)
            {
                settlement.IsApproved = true;
                if (settlement.Employee == null)
                {
                    settlement.Employee = await _context.Employees.FindAsync(settlement.EmployeeId);
                }
                if (settlement.Employee != null && !string.IsNullOrEmpty(settlement.Employee.Email))
                {
                    // Send an automatic approval email
                    await new EmailService().SendEmailAsync(
                        settlement.Employee.Email,
                        "Settlement Approved",
                        $"Your settlement of {settlement.Amount} RON has been automatically approved."
                    );
                }
            }
            _context.Settlements.Add(settlement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Register));
        }
        [Authorize(Roles = "Coordinator")]
        [HttpGet]
        public async Task<IActionResult> PendingApprovals()
        {
            // Fetch settlements that are not yet approved
            //var coordId = Guid.Parse("ce624eb2-d0cb-43c6-a486-b6206ee2e99d");
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            Guid? coordId = null;
            if(!string.IsNullOrEmpty(email))
            {
                coordId = _context.Coordinators.Where(e => e.Email == email).FirstOrDefault()?.CoordinatorId;
            }
            if (coordId == null)
            {
                return NotFound();
            }
            var pendingSettlements = await _context.Settlements
            .Include(s => s.Employee)
            .Include(s => s.Project)
            .Where(s => !s.IsApproved && s.Project.CoordinatorId == coordId.Value && !s.IsRejected)
            .ToListAsync();

            return View(pendingSettlements);
        }
        [Authorize(Roles = "Coordinator")]
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
            if (settlement.IsApproved == false)
            {
                settlement.IsRejected = true;
            }
            await _context.SaveChangesAsync();

            if (settlement.Employee == null)
            {
                settlement.Employee = await _context.Employees.FindAsync(settlement.EmployeeId);
            }
            if (settlement.Employee != null && !string.IsNullOrEmpty(settlement.Employee.Email))
            {
                await new EmailService().SendEmailAsync(settlement.Employee.Email, "Settlement Approval", $"Your settlement of {settlement.Amount} has been {(settlement.IsApproved ? "approved" : "rejected")}.");
            }
            return RedirectToAction(nameof(PendingApprovals));
        }
    }

}
