using Delivery_Order_Management.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery_Order_Management.Controllers
{
    [Authorize]
    public class DeliveryOrderItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveryOrderItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeliveryOrderItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DeliveryOrderItems.Include(d => d.DeliveryOrder).Include(d => d.Item);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DeliveryOrderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryOrderItem = await _context.DeliveryOrderItems
                .Include(d => d.DeliveryOrder)
                .Include(d => d.Item)
                .FirstOrDefaultAsync(m => m.DeliveryOrderItemId == id);
            if (deliveryOrderItem == null)
            {
                return NotFound();
            }

            return View(deliveryOrderItem);
        }

        // GET: DeliveryOrderItems/Create
        public IActionResult Create()
        {
            ViewData["DeliveryOrderId"] = new SelectList(_context.DeliveryOrders, "DeliveryOrderId", "OrderNumber");
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemCode");
            return View();
        }

        // POST: DeliveryOrderItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryOrderItemId,DeliveryOrderId,ItemId,Quantity")] DeliveryOrderItem deliveryOrderItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryOrderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeliveryOrderId"] = new SelectList(_context.DeliveryOrders, "DeliveryOrderId", "DeliveryTiming", deliveryOrderItem.DeliveryOrderId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "Description", deliveryOrderItem.ItemId);
            return View(deliveryOrderItem);
        }

        // GET: DeliveryOrderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryOrderItem = await _context.DeliveryOrderItems.FindAsync(id);
            if (deliveryOrderItem == null)
            {
                return NotFound();
            }
            ViewData["DeliveryOrderId"] = new SelectList(_context.DeliveryOrders, "DeliveryOrderId", "OrderNumber", deliveryOrderItem.DeliveryOrderId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemCode", deliveryOrderItem.ItemId);
            return View(deliveryOrderItem);
        }

        // POST: DeliveryOrderItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryOrderItemId,DeliveryOrderId,ItemId,Quantity")] DeliveryOrderItem deliveryOrderItem)
        {
            if (id != deliveryOrderItem.DeliveryOrderItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryOrderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryOrderItemExists(deliveryOrderItem.DeliveryOrderItemId))
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
            ViewData["DeliveryOrderId"] = new SelectList(_context.DeliveryOrders, "DeliveryOrderId", "DeliveryTiming", deliveryOrderItem.DeliveryOrderId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "Description", deliveryOrderItem.ItemId);
            return View(deliveryOrderItem);
        }

        // GET: DeliveryOrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryOrderItem = await _context.DeliveryOrderItems
                .Include(d => d.DeliveryOrder)
                .Include(d => d.Item)
                .FirstOrDefaultAsync(m => m.DeliveryOrderItemId == id);
            if (deliveryOrderItem == null)
            {
                return NotFound();
            }

            return View(deliveryOrderItem);
        }

        // POST: DeliveryOrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryOrderItem = await _context.DeliveryOrderItems.FindAsync(id);
            if (deliveryOrderItem != null)
            {
                _context.DeliveryOrderItems.Remove(deliveryOrderItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryOrderItemExists(int id)
        {
            return _context.DeliveryOrderItems.Any(e => e.DeliveryOrderItemId == id);
        }
    }
}
