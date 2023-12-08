﻿using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using TaHooK.Common.Models.Answer;
using TaHooK.Common.Models.Question;


namespace TaHook.Web.App.Pages.Quiz
{
    public partial class QuizLobby : IAsyncDisposable
    {
        [Parameter]
        public Guid? Id { get; set; }

        public Guid User { get; set; } // TODO: temp hardcoded

        public QuestionDetailModel? Question { get; set; }

        [Inject] private NavigationManager? Navigation { get; set; }
        private HubConnection? _hubConnection;
        private bool _quizStarted = false;
        private bool _quizFinished = false;

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"https://localhost:7273/quizhub?userId={User}")
                .Build();

            //TODO: Events for next question, quiz start, etc..
            _hubConnection.On("NextQuestion", (QuestionDetailModel? question) => OnNextQuestion(question));


            await _hubConnection.StartAsync();
            await _hubConnection.SendAsync("JoinQuiz", Id);

            await base.OnInitializedAsync();
        }
        protected void OnNextQuestion(QuestionDetailModel? question)
        {
            Console.WriteLine(question);
            if (question is null)
            {
                _quizFinished = true;
            }
            Question = question;
            InvokeAsync(StateHasChanged);
        }

        protected async Task OnStartQuizButton(MouseEventArgs e)
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.SendAsync("StartQuiz", Id);
                Console.WriteLine("Started the quiz");
                _quizStarted = true;
            }
        }

        protected async Task OnAnswerQuestion(AnswerListModel answer)
        {
            Console.WriteLine($"Seleceted the answer {answer.Text}");
        }
    }
}
