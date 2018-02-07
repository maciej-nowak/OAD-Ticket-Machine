// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Biletomat
{
    using System;

    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.WebUI;


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        /// <summary>
        /// Poprawny PIN
        /// </summary>
        private readonly string correctPin;

        /// <summary>
        /// Gotowka do zaplaty za bilety
        /// </summary>
        private double cash;

        /// <summary>
        /// Czy użyszkodnik włożył kartę
        /// </summary>
        private bool isCardInside;

        /// <summary>
        /// Wpisywany zakodowany PIN 
        /// </summary>
        private string inputString;

        /// <summary>
        /// Wyświetlane gwiazdki PINu
        /// </summary>
        private string showPin;

        /// <summary>
        /// Czy program oczekuje na zbliżenie karty
        /// </summary>
        private bool isTouchRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.cash = 4.30;
            this.isCardInside = false;
            this.inputString = string.Empty;
            this.showPin = string.Empty;
            this.correctPin = "2580";
            this.isTouchRequest = false;

            this.InitializeComponent();

            this.ChoosenTickets.Text = "- 2 x Ulgowy 30min \n- 1 x Normalny 15min";
        }

        /// <summary>
        /// Funkcja od wrzucania kasy przez użyszkodnika
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnUserMoneyClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var cashFromUser = button.Content as string;
            this.cash -= double.Parse(cashFromUser);

            if (this.cash < 0)
            {
                var chargeToUser = 0 - this.cash;
                this.ChangeText.Text = string.Format("{0} zł", chargeToUser);
                this.cash = 0;
                this.ShowConfirmDialog();
            }

            this.CashText.Text = string.Format("{0} zł", this.cash);
        }

        /// <summary>
        /// Metoda wywoływana gdy użytkownik włoży kartę płatniczą do czytnika
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnUserInsertCard(object sender, RoutedEventArgs e)
        {
            if (this.isCardInside)
            {
                this.CardText.Text = "Nie wykryto karty";
                this.InserCardButton.Content = "Włóż kartę";
                this.isCardInside = false;
                this.inputString = string.Empty;
                this.showPin = string.Empty;
                this.ResultText.Text = string.Empty;
            }
            else
            {
                this.CardText.Text = "Podaj PIN";
                this.InserCardButton.Content = "Wyjmij kartę";
                this.isCardInside = true;
                this.TouchButton.Content = "Zapłać zbliżeniowo";
                this.isTouchRequest = false;
            }
        }

        /// <summary>
        /// Gdy użytkownik zbliża kartę do czytnika
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnUserCardTouch(object sender, RoutedEventArgs e)
        {
            if (this.isTouchRequest)
            {
                this.ShowConfirmDialog();
            }
        }

        /// <summary>
        /// Gdy użytkownik kasuje znaki PINu
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            this.inputString = this.inputString.Substring(0, this.inputString.Length - 1);
            this.showPin = this.showPin.Substring(0, this.showPin.Length - 1);
            this.ResultText.Text = this.showPin;
            this.DeleteButton.IsEnabled = this.inputString.Length > 0;
        }

        /// <summary>
        /// Kiedy użytkownik wprowadza PIN
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnCharButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.isCardInside)
            {
                var button = sender as Button;
                if (button != null)
                {
                    if (this.inputString.Length < 8)
                    {
                        this.inputString += button.Content as string;
                        this.showPin += "*";
                        this.ResultText.Text = this.showPin;
                    }

                    this.DeleteButton.IsEnabled = this.inputString.Length > 0;
                }
            }
        }

        /// <summary>
        /// Zmiana trybu płacenia kartą
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnTouchButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.isTouchRequest)
            {
                this.TouchButton.Content = "Zapłać zbliżeniowo";
                this.CardText.Text = "Nie wykryto karty";
                this.isTouchRequest = false;
            }
            else
            {
                this.TouchButton.Content = "Zapłać kartą";
                this.CardText.Text = "Zbliż kartę do czujnika";
                this.isTouchRequest = true;
            }
        }

        /// <summary>
        /// Anulowanie wprowadzania PINu
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.inputString = string.Empty;
            this.showPin = string.Empty;
            this.ResultText.Text = this.showPin;
            this.DeleteButton.IsEnabled = false;
        }

        /// <summary>
        /// Potwierdzenie wprowadzonego PINu
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.isCardInside)
            {
                if (this.inputString.Equals(this.correctPin))
                {
                    this.ShowConfirmDialog();
                }
                else
                {
                    this.inputString = string.Empty;
                    this.showPin = string.Empty;
                    this.ResultText.Text = string.Empty;
                    this.ShowErrorDialog();
                }
            }          
        }

        /// <summary>
        /// Wyświetl komunikat potwierdzenia zakupu
        /// </summary>
        private async void ShowConfirmDialog()
        {
            MessageDialog messageDialog = new MessageDialog("Transakcja zatwierdzona. Dziękujemy za zakup biletu.", "Potwierdzenie");
            messageDialog.Commands.Add(new UICommand("Zamknij"));

            IUICommand command = await messageDialog.ShowAsync();
        }

        /// <summary>
        /// Wyświetl komunikat o błędnym PINie
        /// </summary>
        private async void ShowErrorDialog()
        {
            MessageDialog messageDialog = new MessageDialog("Podano błędny PIN, prosimy spróbować ponownie", "Błąd");
            messageDialog.Commands.Add(new UICommand("Zamknij"));

            IUICommand command = await messageDialog.ShowAsync();
        }

        /// <summary>
        /// Zdarzenie tapnięcia w panel wybranych biletów
        /// </summary>
        private void ChoosenTicketsTapped(object sender, TappedRoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            rootFrame.Navigate(typeof(BlankPage1));
        }

        /// <summary>
        /// Wyświetla komunikat o powrocie do okna wyboru biletów
        /// </summary>
        private async void ShowBackToTocketsDialog()
        {
            MessageDialog messageDialog = new MessageDialog("Klikając w panel \"Wybrane bilety\" wracasz do ekranu wyboru biletów", "Wybór biletów");
            messageDialog.Commands.Add(new UICommand("OK"));

            IUICommand command = await messageDialog.ShowAsync();
        }
    }
}
