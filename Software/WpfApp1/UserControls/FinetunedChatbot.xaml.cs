using Entities.Models;
using LLama;
using LLama.Common;
using Microsoft.MarkedNet;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TorchSharp.Modules;
using Xceed.Wpf.AvalonDock.Themes;
using static Entities.Enumerations;
using static Plotly.NET.StyleParam.DrawingStyle;
using static Tensorboard.ApiDef.Types;

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
                //string modelPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "tinyllama-merged.q4_k_m (4).gguf");
                string modelPath = System.IO.Path.Combine(KnownFolders.GetPath(KnownFolder.Downloads), "tinyllama-merged.q4_k_m (4).gguf");

                var parameters = new ModelParams(modelPath)
                {

                    ContextSize = 2048,
                    GpuLayerCount = 5
                };
                var weights = LLamaWeights.LoadFromFile(parameters);
                var context = weights.CreateContext(parameters);
                executor = new InteractiveExecutor(context);

                
                var chatHistory = new ChatHistory();
                chatHistory.AddMessage(AuthorRole.System, "You are a chatbot that has been developed to help the user with their queries.The user can ask for general information and you can answer them, however, your main domain is questions related to the production and installation of windows, doors, facades, insulating glass. Questions can be such that you provide advice, but they can also be of a technical nature that you have to answer something related to installation or production. Always answer in a friendly tone unless the user asks in a more serious tone (for example, generating a business email). Always answer strictly in English. If you are not familiar with a term or it is not in your knowledge base, kindly inform the user that this term is not in your domain, and do not try to explain something you do not know.");
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

            AppendText($"{input}\n");
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
                OutputTextBox.AppendText("\n\n");
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
