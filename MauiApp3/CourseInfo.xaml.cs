using CommunityToolkit.Maui.Converters;
using MauiApp3.Database;
using Plugin.LocalNotification;
using System.Data.SqlTypes;

namespace MauiApp3;

public partial class CourseInfo : ContentPage
{

    private courses selectedCourse;
    string status;
    bool notifyBool;


    public async void onStart()
    {
        /* await Connection._db.DropTableAsync<PAs>();
         await Connection._db.DropTableAsync<OAs>();
 */
        courseCV.ItemsSource = await dbQuery.GetCourse(selectedCourse.coursesId);

        dueDateCV.ItemsSource = await dbQuery.GetCourse(selectedCourse.coursesId);

        courseCV2.ItemsSource = await dbQuery.GetInstructor(selectedCourse.instructorId);

       

        notifyBool = selectedCourse.notify;

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

   

  


    private async void backButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new TermsPage());
    }

    private async void editButton_Clicked(object sender, EventArgs e)
    {
        courseCV.IsVisible = false;
        courseCV2.IsVisible = false;
        dueDateCV.IsVisible = false;
       

        editCourseGrid.IsVisible = true;
        editInstructorGrid.IsVisible = true;
        editDueDateGrid.IsVisible = true;
        statusPicker.IsVisible = true;
       

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
       

        editCourseGrid.IsVisible = false;
        editInstructorGrid.IsVisible = false;
        editDueDateGrid.IsVisible = false;
        statusPicker.IsVisible = false;
        

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
        else if (statusEntry.SelectedIndex == -1)
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

            if (notifyBool == true)
            {
                notifyCheckbox.IsChecked = true;
            }
            else if (notifyBool == false)
            {
                notifyCheckbox.IsChecked = false;
            }



            await dbQuery.updateCourse(selectedCourse.coursesId, courseNameEntry.Text, descriptionEntry.Text, startEntry.Date.ToShortDateString(), endEntry.Date.ToShortDateString(), status, "temp", dueDateEntry.Date.ToShortDateString(), notifyBool);


            IEnumerable<courses> updatedCourse;

            updatedCourse = await dbQuery.GetCourse(selectedCourse.coursesId);

            selectedCourse = updatedCourse.FirstOrDefault() as courses;

            await Navigation.PushModalAsync(new CourseInfo(selectedCourse));
        }

    }
}