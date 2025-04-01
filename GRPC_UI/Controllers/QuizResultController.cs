using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;
using GRPC_UI.Models;
using NET1718_PRN231_PRJ_G2_PremaritalCounseling.GrpcService.Protos;

namespace GRPC_UI.Controllers
{
    public class QuizResultController : Controller
    {
        private readonly QuizResultGRPC.QuizResultGRPCClient _client;

        public QuizResultController()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7172"); // Adjust port as needed
            _client = new QuizResultGRPC.QuizResultGRPCClient(channel);
        }

        // GET: Index - List all quiz results
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAllAsync(new Empty());
            var quizResults = response.Data.Select(q => new QuizResultViewModel
            {
                Id = q.Id,
                Score = q.Score,
                UserId = q.UserId,
                QuizId = q.QuizId,
                QuizResultCode = q.QuizResultCode,
                TimeCompleted = q.TimeCompleted,
                AttemptTime = q.AttemptTime,
                SuggestionContent = q.SuggestionContent,
                Feedback = q.Feedback,
                DoHaveFeedback = q.DoHaveFeedback,
                UserAnswerData = q.UserAnswerData,
                RecommendedAnswerData = q.RecommendedAnswerData
            }).ToList();

            return View(quizResults);
        }

        // GET: Details
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetByIdAsync(new IdRequest { Id = id });
            var model = new QuizResultViewModel
            {
                Id = response.Id,
                Score = response.Score,
                UserId = response.UserId,
                QuizId = response.QuizId,
                QuizResultCode = response.QuizResultCode,
                TimeCompleted = response.TimeCompleted,
                AttemptTime = response.AttemptTime,
                SuggestionContent = response.SuggestionContent,
                Feedback = response.Feedback,
                DoHaveFeedback = response.DoHaveFeedback,
                UserAnswerData = response.UserAnswerData,
                RecommendedAnswerData = response.RecommendedAnswerData
            };
            return View(model);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View(new QuizResultViewModel());
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuizResultViewModel model)
        {

            var request = new QuizResultModel
            {
                Score = model.Score,
                UserId = model.UserId,
                QuizId = model.QuizId,
                TimeCompleted = model.TimeCompleted,
                AttemptTime = model.AttemptTime,
                SuggestionContent = model.SuggestionContent ?? "",
                Feedback = model.Feedback ?? "",
                DoHaveFeedback = model.DoHaveFeedback,
                UserAnswerData = model.UserAnswerData ?? "",
                RecommendedAnswerData = model.RecommendedAnswerData ?? "",
                QuizResultCode = "RESULT-" + Guid.NewGuid().ToString()
            };

            try
            {
                var response = await _client.CreateAsync(request);
                if (response.Status == 201)
                {
                    TempData["SuccessMessage"] = "Quiz result created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", $"Failed to create quiz result: {response.Message}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating quiz result: {ex.Message}");
            }

            return View(model);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _client.GetByIdAsync(new IdRequest { Id = id });
                if (response.Id == 0) // Assuming Id == 0 means not found
                {
                    TempData["ErrorMessage"] = "Quiz result not found.";
                    return RedirectToAction(nameof(Index));
                }

                var model = new QuizResultViewModel
                {
                    Id = response.Id,
                    Score = response.Score,
                    UserId = response.UserId,
                    QuizId = response.QuizId,
                    QuizResultCode = response.QuizResultCode,
                    TimeCompleted = response.TimeCompleted,
                    AttemptTime = response.AttemptTime,
                    SuggestionContent = response.SuggestionContent,
                    Feedback = response.Feedback,
                    DoHaveFeedback = response.DoHaveFeedback,
                    UserAnswerData = response.UserAnswerData,
                    RecommendedAnswerData = response.RecommendedAnswerData
                };
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading quiz result: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(QuizResultViewModel model)
        {
            var request = new QuizResultModel
            {
                Id = model.Id,
                Score = model.Score,
                UserId = model.UserId,
                QuizId = model.QuizId,
                QuizResultCode = model.QuizResultCode,
                TimeCompleted = model.TimeCompleted,
                AttemptTime = model.AttemptTime,
                SuggestionContent = model.SuggestionContent ?? "",
                Feedback = model.Feedback ?? "",
                DoHaveFeedback = model.DoHaveFeedback,
                UserAnswerData = model.UserAnswerData ?? "",
                RecommendedAnswerData = model.RecommendedAnswerData ?? ""
            };

            try
            {
                var response = await _client.UpdateAsync(request);
                if (response.Status == 200)
                {
                    TempData["SuccessMessage"] = "Quiz result updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", $"Failed to update quiz result: {response.Message}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating quiz result: {ex.Message}");
            }

            // If update fails, return the view with the model
            return View(model);
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteByIdAsync(new IdRequest { Id = id });
            if (response.Status == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            // If deletion fails, return to Index with an error message (optional)
            TempData["ErrorMessage"] = response.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}