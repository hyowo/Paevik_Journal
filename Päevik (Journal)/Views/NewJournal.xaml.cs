using Journal.Models;

namespace Journal.Views;

public partial class NewJournal : ContentPage
{
	public NewJournal()
	{
		InitializeComponent();
	}

    private async Task ShowToast(string message, int durationInSeconds = 3)
    {
        ToastMessage.Text = message;
        ToastContainer.IsVisible = true;
        await ToastContainer.FadeTo(1, 250);
        await Task.Delay(durationInSeconds * 1000);
        await ToastContainer.FadeTo(0, 250);
        ToastContainer.IsVisible = false;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        JournalModel model = new()
        {
            Title = TitleEntry.Text ?? "",
            OriginalPoster = NameEntry.Text == string.Empty ? "Anonymous" : NameEntry.Text,
            PostedAt = DateTime.Now,
            IsPrivate = IsPrivateCheckbox.IsChecked,
            Content = ContentEditor.Text ?? ""
        };

        bool errorToast = false;
        string message = (model.Title.Length < 5)
            ? $"Journal title needs to be at least 5 characters long. {(errorToast = true).ToString().Replace("True", "")}"
            : (model.Content.Length < 20)
                ? $"Journal content needs to be at least 20 characters long. {(errorToast = true).ToString().Replace("True", "")}"
                : "Journal posted successfully!";
        if (errorToast)
            await ShowToast(message);
        else
        {
            await Application.Current.MainPage.Navigation.PopAsync();
            if (Application.Current.MainPage is NavigationPage navigationPage && navigationPage.CurrentPage is AllJournals allJournalsPage)
                await allJournalsPage.ShowToast(message);
        }
    }
}