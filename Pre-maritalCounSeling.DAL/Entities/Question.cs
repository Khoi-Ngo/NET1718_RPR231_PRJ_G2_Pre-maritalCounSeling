﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Pre_maritalCounSeling.DAL.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pre_maritalCounSeling.DAL.Entities;

public partial class Question : BaseEntity
{
    public long Id { get; set; }
    //multiple choice, true/false, or written
    private string _questionType;
    public string QuestionType
    {
        get => _questionType;
        set
        {
            if (value != "WRITING" && value != "MULTIPLE_CHOICES")
            {
                throw new ArgumentException("QuestionType must be either 'WRITING' or 'MULTIPLE_CHOICES'.");
            }
            _questionType = value;
        }
    }

    public string Content { get; set; }

    public string? Image { get; set; }
    //if the question is not multiple choice, the recommended answer must not be null or empty
    public string? RecommendedAnswer { get; set; }

    public virtual ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
    public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();
    public virtual ICollection<QuizResultDetail> QuizResultDetails { get; set; } = new List<QuizResultDetail>();
}