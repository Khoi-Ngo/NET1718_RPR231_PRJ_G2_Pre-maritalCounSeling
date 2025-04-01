using System.ComponentModel.DataAnnotations;

namespace GRPC_UI.Models
{
        public class QuizResultViewModel
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Score is required")]
            [Range(0, int.MaxValue, ErrorMessage = "Score must be a positive number")]
            public int Score { get; set; }

            [Required(ErrorMessage = "User ID is required")]
            [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive number")]
            public int UserId { get; set; }

            [Required(ErrorMessage = "Quiz ID is required")]
            [Range(1, int.MaxValue, ErrorMessage = "Quiz ID must be a positive number")]
            public int QuizId { get; set; }

            public string QuizResultCode { get; set; } // Generated server-side, not required here

            [Required(ErrorMessage = "Time Completed is required")]
            [Range(0, double.MaxValue, ErrorMessage = "Time Completed must be a positive number")]
            public double TimeCompleted { get; set; }

            [Required(ErrorMessage = "Attempt Time is required")]
            [Range(0, int.MaxValue, ErrorMessage = "Attempt Time must be a positive number")]
            public int AttemptTime { get; set; }

            public string SuggestionContent { get; set; } // Optional
            public string Feedback { get; set; } // Optional
            public bool DoHaveFeedback { get; set; } // Optional, default false
            public string UserAnswerData { get; set; } // Optional
            public string RecommendedAnswerData { get; set; } // Optional
    }
}
