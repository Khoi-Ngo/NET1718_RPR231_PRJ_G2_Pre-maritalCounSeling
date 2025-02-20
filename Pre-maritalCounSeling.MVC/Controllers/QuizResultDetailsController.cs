using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pre_maritalCounSeling.DAL.Entities;

namespace Pre_maritalCounSeling.MVC.Controllers
{
    public class QuizResultDetailsController : Controller
    {
        private readonly NET1718_PRN231_PRJ_G2_PremaritalCounselingContext _context;

        public QuizResultDetailsController(NET1718_PRN231_PRJ_G2_PremaritalCounselingContext context)
        {
            _context = context;
        }

        // GET: QuizResultDetails
        public async Task<IActionResult> Index()
        {
            var nET1718_RPR231_PRJ_G2_PremaritalCounselingContext = _context.QuizResultDetails.Include(q => q.QuizResult);
            return View(await nET1718_RPR231_PRJ_G2_PremaritalCounselingContext.ToListAsync());
        }

        // GET: QuizResultDetails/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizResultDetail = await _context.QuizResultDetails
                .Include(q => q.QuizResult)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizResultDetail == null)
            {
                return NotFound();
            }

            return View(quizResultDetail);
        }

        // GET: QuizResultDetails/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Content");
            ViewData["QuizResultId"] = new SelectList(_context.QuizResults, "Id", "QuizResultCode");
            return View();
        }

        // POST: QuizResultDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuizResultId,QuestionId,UserAnswer,RecommendedAnswer,CreatedAt,ModifiedAt,CreatedBy,ModifiedBy,IsActive,IsDeleted")] QuizResultDetail quizResultDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quizResultDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuizResultId"] = new SelectList(_context.QuizResults, "Id", "QuizResultCode", quizResultDetail.QuizResultId);
            return View(quizResultDetail);
        }

        // GET: QuizResultDetails/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizResultDetail = await _context.QuizResultDetails.FindAsync(id);
            if (quizResultDetail == null)
            {
                return NotFound();
            }
            ViewData["QuizResultId"] = new SelectList(_context.QuizResults, "Id", "QuizResultCode", quizResultDetail.QuizResultId);
            return View(quizResultDetail);
        }

        // POST: QuizResultDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,QuizResultId,QuestionId,UserAnswer,RecommendedAnswer,CreatedAt,ModifiedAt,CreatedBy,ModifiedBy,IsActive,IsDeleted")] QuizResultDetail quizResultDetail)
        {
            if (id != quizResultDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quizResultDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizResultDetailExists(quizResultDetail.Id))
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
            ViewData["QuizResultId"] = new SelectList(_context.QuizResults, "Id", "QuizResultCode", quizResultDetail.QuizResultId);
            return View(quizResultDetail);
        }

        // GET: QuizResultDetails/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizResultDetail = await _context.QuizResultDetails
                .Include(q => q.QuizResult)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizResultDetail == null)
            {
                return NotFound();
            }

            return View(quizResultDetail);
        }

        // POST: QuizResultDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var quizResultDetail = await _context.QuizResultDetails.FindAsync(id);
            if (quizResultDetail != null)
            {
                _context.QuizResultDetails.Remove(quizResultDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizResultDetailExists(long id)
        {
            return _context.QuizResultDetails.Any(e => e.Id == id);
        }
    }
}
