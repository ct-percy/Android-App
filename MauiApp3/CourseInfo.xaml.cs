
using MauiApp3.Database;


namespace MauiApp3;

public partial class CourseInfo : ContentPage
{

    private courses selectedCourse;
    string status;
    bool notifyBool;


    public async void onStart()
    {
        
        courseCV.ItemsSource = await dbQuery.GetCourse(selectedCourse.coursesId);

        dueDateCV.ItemsSource = await dbQuery.GetCourse(selectedCourse.coursesId);

        courseCV2.ItemsSource = await dbQuery.GetInstructor(selectedCourse.instructorId);

        notesCV.ItemsSource = await dbQuery.GetNotes(selectedCourse.coursesId);
        paCV.ItemsSource = await dbQuery.GetPas(selectedCourse.coursesId);
        oaCV.ItemsSource = await dbQuery.GetOas(selectedCourse.coursesId);

        notifyBool = selectedCourse.notify;

        notifyCheckbox.IsEnabled = false;

        if (notifyBool == true)
        {
            notifyCheckbox.IsChecked = true;
        }
        else if (notifyBool == false)
        {
            notifyCheckbox.IsChecked = false;
        }




    }
    public CourseInfo(courses selectedCourse)
    {
        InitializeComponent();
        this.selectedCourse = selectedCourse;

        onStart();

    }


    private async void addAssessment_Clicked(object sender, EventArgs e)
    {

        await Navigation.PushModalAsync(new Assessment(selectedCourse));

    }

    private void pa_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (oaCV.SelectedItem != null)
        {
            oaCV.SelectedItem = null;
        }

    }

    private void oaCV_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (paCV.SelectedItem != null)
        {
            paCV.SelectedItem = null;
        }
    }

    private async void deleteAssessment_Clicked(object sender, EventArgs e)
    {
        if (oaCV.SelectedItem != null)
        {
            OAs oa = oaCV.SelectedItem as OAs;

            await dbQuery.deleteOA(oa.oaId);
            await dbQuery.deleteOaNotify(oa.oaId);
            oaCV.ItemsSource = await dbQuery.GetOas(selectedCourse.coursesId);
        }
        else if (paCV.SelectedItem != null)
        {
            PAs pa = paCV.SelectedItem as PAs;
            await dbQuery.deletePA(pa.paId);
            await dbQuery.deletePaNotify(pa.paId);
            paCV.ItemsSource = await dbQuery.GetPas(selectedCourse.coursesId);
        }
        else if (oaCV.SelectedItem == null && paCV.SelectedItem == null)
        {
            await DisplayAlert("Error", "Select an Assessment to edit", "Ok");
        }




    }

    private async void editAssessment_Clicked(object sender, EventArgs e)
    {
        if (oaCV.SelectedItem != null)
        {
            OAs oa = oaCV.SelectedItem as OAs;
            await Navigation.PushModalAsync(new Assessment(selectedCourse, oa));
        }
        if (paCV.SelectedItem != null)
        {
            PAs pa = paCV.SelectedItem as PAs;
            await Navigation.PushModalAsync(new Assessment(selectedCourse, pa));

        }
        if (oaCV.SelectedItem == null && paCV.SelectedItem == null)
        {
            await DisplayAlert("Error", "Select an Assessment to edit", "Ok");
        }


    }


    private  void backButton_Clicked(object sender, EventArgs e)
    {
         Navigation.PushModalAsync(new TermsPage());
    }

    private async void editButton_Clicked(object sender, EventArgs e)
    {
        courseCV.IsVisible = false;
        courseCV2.IsVisible = false;
        dueDateCV.IsVisible = false;
        notesCV.IsVisible = false;
        notesFrame.IsVisible = false;
        shareButton.IsVisible = false;

        editCourseGrid.IsVisible = true;
        editInstructorGrid.IsVisible = true;
        editDueDateGrid.IsVisible = true;
        statusPicker.IsVisible = true;
        notesEditor.IsVisible = true;
        notesEditorFrame.IsVisible = true;
        notifyCheckbox.IsEnabled = true;
        pendingButton.IsVisible = true;

        if (selectedCourse.status == "Active")
        {
            statusPicker.SelectedIndex = 0;
        }
        if (selectedCourse.status == "In Progress")
        {
            statusPicker.SelectedIndex = 1;
        }
        if (selectedCourse.status == "Completed")
        {
            statusPicker.SelectedIndex = 2;
        }

        courseNameEntry.Text = selectedCourse.courseName;
        descriptionEntry.Text = selectedCourse.description;
        startEntry.Date = DateTime.Parse(selectedCourse.startDate);
        endEntry.Date = DateTime.Parse(selectedCourse.endDate);
        dueDateEntry.Date = DateTime.Parse(selectedCourse.dueDate);
        notesEditor.Text = selectedCourse.notes;

        IEnumerable<instructors> instructor;

        instructor = await dbQuery.GetInstructor(selectedCourse.instructorId);


        instructorEntry.Text = instructor.ElementAt(0).instructorName.ToString();
        emailEntry.Text = instructor.ElementAt(0).eMail.ToString();
        phoneEntry.Text = instructor.ElementAt(0).phone.ToString();




    }

    private void cancelEditCourseButton_Clicked(object sender, EventArgs e)
    {

        courseCV.IsVisible = true;
        courseCV2.IsVisible = true;
        dueDateCV.IsVisible = true;
        notesCV.IsVisible = true;
        shareButton.IsVisible = true;
        notesFrame.IsVisible = true;

        editCourseGrid.IsVisible = false;
        editInstructorGrid.IsVisible = false;
        editDueDateGrid.IsVisible = false;
        statusPicker.IsVisible = false;
        notesEditor.IsVisible = false;
        notesEditorFrame.IsVisible = false;
        notifyCheckbox.IsEnabled = false;
        pendingButton.IsVisible = false;


    }

    private async void saveCourseEditButton_Clicked(object sender, EventArgs e)
    {
        #region Exceptions and Validation

        if (string.IsNullOrWhiteSpace(courseNameEntry.Text) == true)
        {
            await DisplayAlert("Missing Value", "Course Title can not be empty", "Ok");
            return;
        }
        else if (string.IsNullOrWhiteSpace(instructorEntry.Text) == true)
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
        else if (dueDateEntry.Date < startEntry.Date)
        {
            await DisplayAlert("Error", "Start Date can not be after Due Date", "Ok");
            return;
        }
        else if (statusPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Missing Value", "Course Status must be selected", "Ok");
            return;
        }
        #endregion


        else
        {
            await dbQuery.updateInstructor(selectedCourse.instructorId, instructorEntry.Text, emailEntry.Text, phoneEntry.Text);

            if (statusPicker.SelectedIndex == 0)
            {
                status = "Active";
            }
            if (statusPicker.SelectedIndex == 1)
            {
                status = "In Progress";
            }
            if (statusPicker.SelectedIndex == 2)
            {
                status = "Completed";
            }

            if (notifyCheckbox.IsChecked == true)
            {
                notifyBool = true;
            }
            else if (notifyCheckbox.IsChecked == false)
            {
                notifyBool = false;
            }



            await dbQuery.updateCourse(selectedCourse.coursesId, courseNameEntry.Text, descriptionEntry.Text, startEntry.Date.ToShortDateString(), endEntry.Date.ToShortDateString(), status, notesEditor.Text, dueDateEntry.Date.ToShortDateString(), notifyBool);

            if (notifyCheckbox.IsChecked == true)
            {
                try
                {

                  await  dbQuery.updateCourseNotify(selectedCourse.coursesId, courseNameEntry.Text, startEntry.Date.ToShortDateString(), endEntry.Date.ToShortDateString(), dueDateEntry.Date.ToShortDateString());
                    
                }
                catch 
                { 
                    await dbQuery.AddNotifyCourse(selectedCourse.coursesId, courseNameEntry.Text, startEntry.Date.ToShortDateString(), endEntry.Date.ToShortDateString(), dueDateEntry.Date.ToShortDateString());
                    
                }

            }
            else if (notifyCheckbox.IsChecked == false) 
            {
                try
                {
                   await dbQuery.deleteCourseNotify(selectedCourse.coursesId);
                }
                catch
                {
                    
                }
            }

            notifyCheckbox.IsEnabled = false;

            IEnumerable<courses> updatedCourse;

            updatedCourse = await dbQuery.GetCourse(selectedCourse.coursesId);

            selectedCourse = updatedCourse.FirstOrDefault() as courses;

            await Navigation.PushModalAsync(new CourseInfo(selectedCourse));
        }
    }

    public async Task shareNotes(string text)
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = text,
            Title = "Share Text"
        });
    }

    private async void shareButton_Clicked(object sender, EventArgs e)
    {
        await shareNotes("Notes for " + selectedCourse.courseName + " : " + selectedCourse.notes);
    }

    private void notifyCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (notifyCheckbox.IsEnabled == true && notifyCheckbox.IsChecked == true)
        {

            DisplayAlert("Notify", "You will be notified of upcoming start, end, and due dates", "Ok");
        }
        if (notifyCheckbox.IsEnabled == true && notifyCheckbox.IsChecked == false)
        {

            DisplayAlert("Notify", "You will NOT be notified of upcoming start, end, and due dates", "Ok");
        }
    }


}