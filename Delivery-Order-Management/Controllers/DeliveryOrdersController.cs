using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Delivery_Order_Management.Data;

namespace Delivery_Order_Management.Controllers
{
    public class DeliveryOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveryOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeliveryOrders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DeliveryOrders.Include(d => d.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DeliveryOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryOrder = await _context.DeliveryOrders
                .Include(d => d.Customer)
                .FirstOrDefaultAsync(m => m.DeliveryOrderId == id);
            if (deliveryOrder == null)
            {
                return NotFound();
            }

            return View(deliveryOrder);
        }

        // GET: DeliveryOrders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Address");
            return View();
        }

        // POST: DeliveryOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryOrderId,OrderNumber,OrderDate,DeliveryDate,CustomerId,Status,DeliveryTiming")] DeliveryOrder deliveryOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Address", deliveryOrder.CustomerId);
            return View(deliveryOrder);
        }

        // GET: DeliveryOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryOrder = await _context.DeliveryOrders.FindAsync(id);
            if (deliveryOrder == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Address", deliveryOrder.CustomerId);
            return View(deliveryOrder);
        }

        // POST: DeliveryOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryOrderId,OrderNumber,OrderDate,DeliveryDate,CustomerId,Status,DeliveryTiming")] DeliveryOrder deliveryOrder)
        {
            if (id != deliveryOrder.DeliveryOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryOrderExists(deliveryOrder.DeliveryOrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Address", deliveryOrder.CustomerId);
            return View(deliveryOrder);
        }

        // GET: DeliveryOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryOrder = await _context.DeliveryOrders
                .Include(d => d.Customer)
                .FirstOrDefaultAsync(m => m.DeliveryOrderId == id);
            if (deliveryOrder == null)
            {
                return NotFound();
            }

            return View(deliveryOrder);
        }

        // POST: DeliveryOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryOrder = await _context.DeliveryOrders.FindAsync(id);
            if (deliveryOrder != null)
            {
                _context.DeliveryOrders.Remove(deliveryOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryOrderExists(int id)
        {
            return _context.DeliveryOrders.Any(e => e.DeliveryOrderId == id);
        }
    }
}
