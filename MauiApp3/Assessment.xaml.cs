using MauiApp3.Database;


namespace MauiApp3;

public partial class Assessment : ContentPage
{

    OAs oa;
    PAs pa;
    int oaId;
    int paId;
    bool notifyBool;
    courses selectedCourse;
    public async void onStart()
    {
        courseNameCV.ItemsSource = await dbQuery.GetCourse(selectedCourse.coursesId);

        if (oa != null)
        {
            assessmentType.SelectedIndex = 0;
            assessmentName.Text = oa.oaName;
            startDate.Date = DateTime.Parse(oa.startDate);
            endDate.Date = DateTime.Parse(oa.endDate);
            notifyBool = oa.notify;

        }
        if (pa != null)
        {
            assessmentType.SelectedIndex = 1;
            assessmentName.Text = pa.paName;
            startDate.Date = DateTime.Parse(pa.startDate);
            endDate.Date = DateTime.Parse(pa.endDate);
            notifyBool = pa.notify;

        }
        if (notifyBool == true)
        {
            notifyCheck.IsChecked = true;
        }


    }
    public Assessment(courses selectedCourse)
    {

        InitializeComponent();
        this.selectedCourse = selectedCourse;
        onStart();
    }
    public Assessment(courses selectedCourse, OAs oa)
    {

        InitializeComponent();
        this.selectedCourse = selectedCourse;
        this.oa = oa;
        onStart();
    }
    public Assessment(courses selectedCourse, PAs pa)
    {
        InitializeComponent();
        this.selectedCourse = selectedCourse;
        this.pa = pa;
        onStart();
    }

    private async void save_Clicked(object sender, EventArgs e)
    {
        if (notifyCheck.IsChecked == true)
        {
            notifyBool = true;
        }
        else if (notifyCheck.IsChecked == false)
        {
            notifyBool = false;
        }


        //Add new OA
        if (oa == null && pa == null && assessmentType.SelectedIndex == 0)

        {
            oaId = await dbQuery.AddOa(assessmentName.Text, selectedCourse.coursesId, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString(), dueDate.Date.ToShortDateString(), notifyBool);
            if (notifyCheck.IsChecked == true)
            {
                await dbQuery.AddNotifyOA(oaId, assessmentName.Text, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString(), dueDate.Date.ToShortDateString());
            }

        }
        //Add new PA
        else if (oa == null && pa == null && assessmentType.SelectedIndex == 1)
        {
            paId = await dbQuery.AddPa(assessmentName.Text, selectedCourse.coursesId, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString(), dueDate.Date.ToShortDateString(), notifyBool);
            if (notifyCheck.IsChecked == true)
            {
                await dbQuery.AddNotifyPA(paId, assessmentName.Text, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString(), dueDate.Date.ToShortDateString());
            }
        }

        //Edit OA
        else if (oa != null && assessmentType.SelectedIndex == 0)
        {

            oaId = await dbQuery.editOA(oa.oaId, assessmentName.Text, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString(), dueDate.Date.ToShortDateString(), notifyBool);

            if (notifyCheck.IsChecked == true)
            {
                try
                {
                    await dbQuery.updateOaNotify(oaId, assessmentName.Text, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString(), dueDate.Date.ToShortDateString());
                }
                catch
                {
                    await dbQuery.AddNotifyOA(oaId, assessmentName.Text, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString(), dueDate.Date.ToShortDateString());

                }
            }
            else if (notifyCheck.IsChecked == false)
            {
                try
                {
                    await dbQuery.deleteOaNotify(oaId);
                }
                catch
                {

                }
            }


        }
        //Change OA to PA
        else if (oa != null && assessmentType.SelectedIndex == 1)
        {
            await dbQuery.deleteOA(oa.oaId);

            paId = await dbQuery.AddPa(assessmentName.Text, selectedCourse.coursesId, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString(), dueDate.Date.ToShortDateString(), notifyBool);
            try
            {
                await dbQuery.deleteOaNotify(oa.oaId);
            }
            catch
            {

            }
            if (notifyCheck.IsChecked == true)
            {
                await dbQuery.AddNotifyPA(paId, assessmentName.Text, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString(), dueDate.Date.ToShortDateString());
            }
        }
        //Edit PA
        else if (pa != null && assessmentType.SelectedIndex == 1)
        {
            paId = await dbQuery.editPA(pa.paId, assessmentName.Text, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString(), dueDate.Date.ToShortDateString(), notifyBool);

            if (notifyCheck.IsChecked == true)
            {
                try
                {
                    await dbQuery.updatePaNotify(paId, assessmentName.Text, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString(), dueDate.Date.ToShortDateString());
                }
                catch
                {
                    await dbQuery.AddNotifyPA(paId, assessmentName.Text, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString(), dueDate.Date.ToShortDateString());

                }
            }
            else if (notifyCheck.IsChecked == false)
            {
                try
                {
                    await dbQuery.deletePaNotify(paId);
                }
                catch
                {

                }
            }



        }
        //Change PA to OA
        else if (pa != null && assessmentType.SelectedIndex == 0)
        {
            await dbQuery.deletePA(pa.paId);
            oaId = await dbQuery.AddOa(assessmentName.Text, selectedCourse.coursesId, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString(), dueDate.Date.ToShortDateString(), notifyBool);

            try
            {
                await dbQuery.deletePaNotify(pa.paId);
            }
            catch
            {

            }
            if (notifyCheck.IsChecked == true)
            {
                await dbQuery.AddNotifyOA(oaId, assessmentName.Text, startDate.Date.ToShortDateString(), endDate.Date.ToShortDateString(), dueDate.Date.ToShortDateString());
            }


        }







        await Navigation.PushModalAsync(new CourseInfo(selectedCourse as courses));




    }

    private void notifyCheck_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (notifyCheck.IsChecked == true)
        {

            DisplayAlert("Notify", "You will be notified of upcoming start, end, and due dates", "Ok");
        }
        if (notifyCheck.IsChecked == false)
        {

            DisplayAlert("Notify", "You will NOT be notified of upcoming start, end, and due dates", "Ok");
        }

    }



    private async void backButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}