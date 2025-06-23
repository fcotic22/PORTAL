using Bussiness_Logic_Layer.Services;
using GroqSharp.Models;
using Notifications.Wpf.Core;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Presentation_Layer.UserControls
{
    public partial class ChatbotUC : UserControl
    {
        public ChatbotService ChatbotService { get; set; }
        public ObservableCollection<Message> Messages { get; set; }

        public ChatbotUC()
        {
            InitializeComponent();

            var apiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY", EnvironmentVariableTarget.Machine);
            var apiModel = "llama3-70b-8192";

            ChatbotService = new ChatbotService(apiKey, apiModel);
            Messages = new ObservableCollection<Message>();

            var systemMessage = new Message
            {
                Role = MessageRoleType.System,
                Content = "Ti si chatbot koji je razvijen da pomaže korisniku sa njegovim upitima. Korisnik može pitati opće informacije i možeš mu odgovorati na njih međutim tvoja glavna domena su pitanja vezana za proizvodnju i ugradnju prozora, vrata, fasada, izolacijskog stakla. " +
                "Pitanja mogu biti takva da pružaš savjete, ali mogu biti i tehničke prirode da moraš nešto odgovoriti vezano za ugradnju ili proizvodnju. Uvijek odgovaraj sa prijateljskim tonom osim ako korisnik upita ozbiljniji ton (na primjer generiranje poslovnog maila). Uvijek odgovaraj striktno na Hrvatskom jeziku. Ukoliko nisi upoznat sa pojmom ili ti nije u bazi znanja lijepo obavijesti korisnika da taj pojam nije u tvojoj domeni, te nemoj pokušavati objašnjavati nešto što ne znaš"
            };

            Messages.Add(systemMessage);

            Messages.Add(new Message
            {
                Role = MessageRoleType.Assistant,
                Content = "Pozdrav, kako ti mogu pomoći danas?",
            });

            DataContext = this;
        }
            
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            chatMessages.ItemsSource = System.Windows.Data.CollectionViewSource.GetDefaultView(Messages);
            ((System.Windows.Data.CollectionView)chatMessages.ItemsSource).Filter = x => ((Message)x).Role != MessageRoleType.System;
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            var message = txtMessage.Text.Trim();
            if (string.IsNullOrEmpty(message))
            {
                UCHelper.DisplayNotification("Molimo unesite poruku.", "CHATBOT", NotificationType.Warning);
                return;
            }

            var userMsg = new Message
            {
                Role = MessageRoleType.User,
                Content = message,
            };
            Messages.Add(userMsg);

            txtMessage.Text = string.Empty;
            txtMessage.Focus();

            try
            {
                var response = await ChatbotService.GetChatResponse(Messages);
                Messages.Add(new Message
                {
                    Role = MessageRoleType.Assistant,
                    Content = response
                });
            }
            catch (Exception ex)
            {
                UCHelper.DisplayNotification("CHATBOT", "Došlo je do greške prilikom slanja poruke", NotificationType.Error);
                Console.WriteLine(ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            GUIManager.Close();
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !e.KeyboardDevice.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                e.Handled = true;
                btnSend_Click(sender, e);
            }
        }

        private void btnDeleteChat_Click(object sender, RoutedEventArgs e)
        {
            while (Messages.Count > 2)
            {
                Messages.RemoveAt(2);
            }
            UserControl_Loaded(sender, e);
        }
    }

    public class ChatMessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate UserTemplate { get; set; }
        public DataTemplate AssistantTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Message message)
            {
                return message.Role == MessageRoleType.User ? UserTemplate : AssistantTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}