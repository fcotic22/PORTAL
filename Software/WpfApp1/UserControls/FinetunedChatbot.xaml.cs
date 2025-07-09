using Entities.Models;
using LLama;
using LLama.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Presentation_Layer.UserControls
{
    /// <summary>
    /// Interaction logic for FinetunedChatbot.xaml
    /// </summary>
    public partial class FinetunedChatbot : UserControl
    {
        public FinetunedChatbot()
        {
            InitializeComponent();
            InitializeModel();
        }

        private ChatSession? chatSession;
        private InteractiveExecutor? executor;
        private InferenceParams? inferenceParams;

        private void InitializeModel()
        {   
            try
            {
                //string modelPath = "C:\\Users\\user\\Downloads\\tinyllama-merged.q4_k_m (4).gguf";
                string modelPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "tinyllama.gguf");

                var parameters = new ModelParams(modelPath)
                {
                    ContextSize = 2048,
                    GpuLayerCount = 5
                };
                var weights = LLamaWeights.LoadFromFile(parameters);
                var context = weights.CreateContext(parameters);
                executor = new InteractiveExecutor(context);

                
                var chatHistory = new ChatHistory();
                chatHistory.AddMessage(AuthorRole.System, "You are a helpful assistant with techical know how in window production. You always answear the best you know, and if you are not sure you say that to the user. " +
                    "You know everything about the window and door production process. If the user ask you for a list you provide it in a structured form.");
                chatHistory.AddMessage(AuthorRole.User, "Hello");
                chatHistory.AddMessage(AuthorRole.Assistant, "Hello. How may I help you today?");

                chatSession = new ChatSession(executor, chatHistory);

                inferenceParams = new InferenceParams()
                {
                    MaxTokens = 1024,
                    AntiPrompts = new List<string> { "User:" } 
                };
            }
            catch (Exception ex)
            {
                AppendText($"Greška kod učitavanja modela: {ex.Message} {ex.InnerException} {ex.StackTrace} {ex.Source} {ex.HelpLink} {ex.Data}\n");
            }
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (chatSession == null || inferenceParams == null)
            {
                AppendText("Model još nije učitan.\n");
                return;
            }

            var input = InputTextBox.Text.Trim();
            if (string.IsNullOrEmpty(input))
                return;

            AppendText($"Ti: {input}\n");
            InputTextBox.Clear();

            try
            {   await foreach (var token in chatSession.ChatAsync(
                    new ChatHistory.Message(AuthorRole.User, input),
                    inferenceParams))
                {
                    Dispatcher.Invoke(() =>
                    {
                        OutputTextBox.AppendText(token);
                        OutputTextBox.ScrollToEnd();
                    });
                }
            }
            catch (Exception ex)
            {
                AppendText($"Greška tijekom generiranja: {ex.Message}\n");
            }
            AppendText("\n");
        }

        private void AppendText(string text)
        {
            OutputTextBox.AppendText(text);
            OutputTextBox.ScrollToEnd();
        }
    }
}
