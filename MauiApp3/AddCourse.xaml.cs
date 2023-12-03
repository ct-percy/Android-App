

using MauiApp3.Database;
using Plugin.LocalNotification;

namespace MauiApp3;

public partial class AddCourse : ContentPage
{

    readonly int termId;

    string status;

    bool notifyBool;
    public async void onStart(int termId)
    {

        var term = await Connection._db.QueryAsync<terms>("SELECT * FROM terms WHERE Id =" + termId);


        termNameCV.ItemsSource = term;

    }


    public AddCourse(int termId)
    {


        InitializeComponent();

        this.termId = termId;
        onStart(termId);

        //termNameCV.ItemsSource

    }

    private async void saveButton_Clicked(object sender, EventArgs e)
    {

        if (statusEntry.SelectedIndex == 0)
        {
            statusEntry.SelectedItem = "Active";
        }
        if (statusEntry.SelectedIndex == 1)
        {

            statusEntry.SelectedItem = "In Progress";
        }
        if (statusEntry.SelectedIndex == 2)
        {

            statusEntry.SelectedItem = "Completed";
        }

        status = statusEntry.SelectedItem.ToString();


        int instructorId = await dbQuery.AddInstructor(profEntry.Text, phoneEntry.Text, emailEntry.Text);

        if (notifyCheckbox.IsChecked == false)
        {
            notifyBool = false;
        }
        else if (notifyCheckbox.IsChecked == true)
        {
            notifyBool = true;
        }

        int courseId = await dbQuery.AddCourse(termId, courseNameEntry.Text, startEntry.Date.ToShortDateString(), endEntry.Date.ToShortDateString(), dueEntry.Date.ToShortDateString(), instructorId, status, "Click to add new notes to this course.", "Click Edit to add a course description!", notifyBool);

        notify(courseId);
        
        await Navigation.PushModalAsync(new TermsPage());
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

   

    private void notifyCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (notifyCheckbox.IsChecked == true)
        {

            DisplayAlert("Notify", "You will be notified of upcoming start, end, and due dates", "Ok");
        }
        if (notifyCheckbox.IsChecked == false)
        {

            DisplayAlert("Notify", "You will NOT be notified of upcoming start, end, and due dates", "Ok");
        }
    }

    private async void notify(int iD)   
    {
        if (notifyCheckbox.IsChecked == true)
        {
            await dbQuery.AddNotifyCourse(iD, courseNameEntry.Text, startEntry.Date.ToShortDateString(), endEntry.Date.ToShortDateString(), dueEntry.Date.ToShortDateString());
        }
    }
}