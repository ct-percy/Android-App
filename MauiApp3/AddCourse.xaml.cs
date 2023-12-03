
using MauiApp3.Database;


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



        #region Exceptions and Validation

        if (string.IsNullOrWhiteSpace(courseNameEntry.Text) == true)
        {
            await DisplayAlert("Missing Value", "Course Title can not be empty", "Ok");
            return;
        }
        else if (string.IsNullOrWhiteSpace(profEntry.Text) == true)
        {
            await DisplayAlert("Missing Value", "Instructor can not be empty", "Ok");
            return;
        }

        else if (emailValidator.IsNotValid || string.IsNullOrWhiteSpace(emailEntry.Text) == true)
        {
            await DisplayAlert("Invalid Email", "Email must be in correct format", "Ok");

            return;
        }
        else if (phoneValidator.IsNotValid || string.IsNullOrWhiteSpace(phoneEntry.Text) == true)
        {
            await DisplayAlert("Invalid Phone", "Phone must be in xxx-xxx-xxxx format", "Ok");
            return;
        }
        else if (endEntry.Date < startEntry.Date)
        {
            await DisplayAlert("Error", "Start Date can not be after End Date", "Ok");
            return;
        }
        else if (dueEntry.Date < startEntry.Date)
        {
            await DisplayAlert("Error", "Start Date can not be after Due Date", "Ok");
            return;
        }
        else if (statusEntry.SelectedIndex == -1)
        {
            await DisplayAlert("Missing Value", "Course Status must be selected", "Ok");
            return;
        }
        #endregion
        else
        {
            int instructorId = await dbQuery.AddInstructor(profEntry.Text, phoneEntry.Text, emailEntry.Text);

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

            
            if (notifyCheckbox.IsChecked == false)
            {
                notifyBool = false;
                await dbQuery.AddCourse(termId, courseNameEntry.Text, startEntry.Date.ToShortDateString(), endEntry.Date.ToShortDateString(), dueEntry.Date.ToShortDateString(), instructorId, status, "Click to add new notes to this course.", "Click Edit to add a course description!", notifyBool);

            }
            else if (notifyCheckbox.IsChecked == true)
            {
                notifyBool = true;
                int courseId = await dbQuery.AddCourse(termId, courseNameEntry.Text, startEntry.Date.ToShortDateString(), endEntry.Date.ToShortDateString(), dueEntry.Date.ToShortDateString(), instructorId, status, "Click to add new notes to this course.", "Click Edit to add a course description!", notifyBool);
                await dbQuery.AddNotifyCourse(courseId, courseNameEntry.Text, startEntry.Date.ToShortDateString(), endEntry.Date.ToShortDateString(), dueEntry.Date.ToShortDateString());

            }


            await Navigation.PushModalAsync(new TermsPage());
        }
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

  
}