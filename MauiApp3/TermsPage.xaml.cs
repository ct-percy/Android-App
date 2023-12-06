using __XamlGeneratedCode__;
using CommunityToolkit.Maui.Converters;
using MauiApp3.Database;


namespace MauiApp3;

public partial class TermsPage : ContentPage

{
    object selectedCourse;



    bool noCourse1;
    bool noCourse2;
    bool noCourse3;
    bool noCourse4;
    bool noCourse5;
    bool noCourse6;

    IEnumerable<terms> term1;
    IEnumerable<terms> term2;
    IEnumerable<terms> term3;
    IEnumerable<terms> term4;
    IEnumerable<terms> term5;
    IEnumerable<terms> term6;

    int term1Id;
    int term2Id;
    int term3Id;
    int term4Id;
    int term5Id;
    int term6Id;

    public async void onStart()
    {


        #region Try/Catch gets and sets data
        try
        {


            term1 = await dbQuery.GetTerm1();
            Term1.ItemsSource = term1;


            term2 = await dbQuery.GetTerm2();
            Term2.ItemsSource = term2;



            term3 = await dbQuery.GetTerm3();
            Term3.ItemsSource = term3;


            term4 = await dbQuery.GetTerm4();
            Term4.ItemsSource = term4;


            term5 = await dbQuery.GetTerm5();
            Term5.ItemsSource = term5;

            term6 = await dbQuery.GetTerm6();
            Term6.ItemsSource = term6;


            term1Id = term1.First().Id;
            var term1Courses = await dbQuery.GetCourses(term1Id);
            Term1Courses.ItemsSource = term1Courses;
            if (term1Courses.Count() < 1)
            {
                noCourse1 = true;
            }

            term2Id = term2.First().Id;
            var term2Courses = await dbQuery.GetCourses(term2Id);
            Term2Courses.ItemsSource = term2Courses;
            if (term2Courses.Count() < 1)
            {
                noCourse2 = true;
            }

            term3Id = term3.First().Id;
            var term3Courses = await dbQuery.GetCourses(term3Id);
            Term3Courses.ItemsSource = term3Courses;
            if (term3Courses.Count() < 1)
            {
                noCourse3 = true;
            }

            term4Id = term4.First().Id;
            var term4Courses = await dbQuery.GetCourses(term4Id);
            Term4Courses.ItemsSource = term4Courses;
            if (term4Courses.Count() < 1)
            {
                noCourse4 = true;
            }

            term5Id = term5.First().Id;
            var term5Courses = await dbQuery.GetCourses(term5Id);
            Term5Courses.ItemsSource = term5Courses;
            if (term5Courses.Count() < 1)
            {
                noCourse5 = true;
            }

            term6Id = term6.First().Id;
            var term6Courses = await dbQuery.GetCourses(term6Id);
            Term6Courses.ItemsSource = term6Courses;

            if (term6Courses.Count() < 1)
            {
                noCourse6 = true;
            }

        }
        catch
        {
            

        }

        //  hides if less than 6 terms
       
            collapseCount();

        #endregion
    }

    public async void collapseCount()
    {
        int termCount = await Connection._db.Table<terms>().CountAsync();
        if (termCount == 0)
        {
            collapse1.IsVisible = false;
            collapse2.IsVisible = false;
            collapse3.IsVisible = false;
            collapse4.IsVisible = false;
            collapse5.IsVisible = false;
            collapse6.IsVisible = false;
            Term1.IsVisible = false;
            Term2.IsVisible = false;
            Term3.IsVisible = false;
            Term4.IsVisible = false;
            Term5.IsVisible = false;
            Term6.IsVisible = false;

            noTermsLabel.IsVisible = true;

        }
        if (termCount == 1)
        {


            collapse2.IsVisible = false;
            collapse3.IsVisible = false;
            collapse4.IsVisible = false;
            collapse5.IsVisible = false;
            collapse6.IsVisible = false;
            Term2.IsVisible = false;
            Term3.IsVisible = false;
            Term4.IsVisible = false;
            Term5.IsVisible = false;
            Term6.IsVisible = false;

        }
        if (termCount == 2)
        {

            collapse3.IsVisible = false;
            collapse4.IsVisible = false;
            collapse5.IsVisible = false;
            collapse6.IsVisible = false;
            Term3.IsVisible = false;
            Term4.IsVisible = false;
            Term5.IsVisible = false;
            Term6.IsVisible = false;


        }
        if (termCount == 3)
        {

            collapse4.IsVisible = false;
            collapse5.IsVisible = false;
            collapse6.IsVisible = false;
           
            Term4.IsVisible = false;
            Term5.IsVisible = false;
            Term6.IsVisible = false;


        }
        if (termCount == 4)
        {

            collapse5.IsVisible = false;
            collapse6.IsVisible = false;
       
            Term5.IsVisible = false;
            Term6.IsVisible = false;


        }
        if (termCount == 5)
        {

            collapse6.IsVisible = false;

            Term6.IsVisible = false;

        }

    }

    public TermsPage()
    {

        notify();
        onStart();
        
        InitializeComponent();



    }

    private async void addCourse_Clicked(object sender, EventArgs e)
    {

        if (sender == addCourse1)
        {
            await Navigation.PushModalAsync(new AddCourse(term1Id));
        }
        if (sender == addCourse2)
        {
            await Navigation.PushModalAsync(new AddCourse(term2Id));
        }
        if (sender == addCourse3)
        {
            await Navigation.PushModalAsync(new AddCourse(term3Id));
        }
        if (sender == addCourse4)
        {
            await Navigation.PushModalAsync(new AddCourse(term4Id));
        }
        if (sender == addCourse5)
        {
            await Navigation.PushModalAsync(new AddCourse(term5Id));
        }
        if (sender == addCourse6)
        {
            await Navigation.PushModalAsync(new AddCourse(term6Id));
        }
    }

    public async void addTerm_Clicked(object sender, EventArgs e)
    {

        if (await Connection._db.Table<terms>().CountAsync() == 6)
        {
            await DisplayAlert("", "Only up to 6 terms allowed. Delete a term to add more", "Ok");
            return;
        }

        else
        {
            addTerm.IsVisible = false;

            addTermName.IsVisible = true;
            addTermEnd.IsVisible = true;
            addTermStart.IsVisible = true;
            startLabel.IsVisible = true;
            endLabel.IsVisible = true;
            saveTerm.IsVisible = true;
            cancelTerm.IsVisible = true;


        }
    }

    private void editTermButton_Clicked(object sender, EventArgs e)
    {

        #region Term1
        if (sender == editTerm1)
        {
            Term1.IsVisible = false;

            string termName = term1.First().termName;
            

            editStart1.IsVisible = true;
            editDash1.IsVisible = true;
            editEnd1.IsVisible = true;
            editTerm1Entry.IsVisible = true;
            editTerm1Entry.Text = termName;
            editStart1.Date = DateTime.Parse(term1.First().startDate);
            editEnd1.Date = DateTime.Parse(term1.First().endDate);
            cancelEdit1.IsVisible = true;
            editTerm1.IsVisible = false;
            save1.IsVisible = true;
            delete1.IsVisible = true;
            deleteCourse1.IsVisible = true;
            addCourse1.IsVisible = false;

        }
        #endregion


        #region Term2

        if (sender == editTerm2)
        {
            Term2.IsVisible = false;

            string termName = term2.First().termName;

            editStart2.IsVisible = true;
            editDash2.IsVisible = true;
            editEnd2.IsVisible = true;
            editTerm2Entry.IsVisible = true;
            editTerm2Entry.Text = termName;
            editStart2.Date = DateTime.Parse(term2.First().startDate);
            editEnd2.Date = DateTime.Parse(term2.First().endDate);
            cancelEdit2.IsVisible = true;
            editTerm2.IsVisible = false;
            save2.IsVisible = true;
            delete2.IsVisible = true;
            deleteCourse2.IsVisible = true;
            addCourse2.IsVisible = false;

        }

        #endregion


        #region Term3

        if (sender == editTerm3)
        {
            Term3.IsVisible = false;


            string termName = term3.First().termName;

            editStart3.IsVisible = true;
            editDash3.IsVisible = true;
            editEnd3.IsVisible = true;
            editTerm3Entry.IsVisible = true;
            editTerm3Entry.Text = termName;
            editStart3.Date = DateTime.Parse(term3.First().startDate);
            editEnd3.Date = DateTime.Parse(term3.First().endDate);
            cancelEdit3.IsVisible = true;
            editTerm3.IsVisible = false;
            save3.IsVisible = true;
            delete3.IsVisible = true;
            deleteCourse3.IsVisible = true;
            addCourse3.IsVisible = false;

        }

        #endregion


        #region Term4

        if (sender == editTerm4)
        {
            Term4.IsVisible = false;
            ;
            string termName = term4.First().termName;

            editStart4.IsVisible = true;
            editDash4.IsVisible = true;
            editEnd4.IsVisible = true;
            editTerm4Entry.IsVisible = true;
            editTerm4Entry.Text = termName;
            editStart4.Date = DateTime.Parse(term4.First().startDate);
            editEnd4.Date = DateTime.Parse(term4.First().endDate);
            cancelEdit4.IsVisible = true;
            editTerm4.IsVisible = false;
            save4.IsVisible = true;
            delete4.IsVisible = true;
            deleteCourse4.IsVisible = true;
            addCourse4.IsVisible = false;

        }

        #endregion


        #region Term5

        if (sender == editTerm5)
        {
            Term5.IsVisible = false;

            string termName = term5.First().termName;

            editStart5.IsVisible = true;
            editDash5.IsVisible = true;
            editEnd5.IsVisible = true;
            editTerm5Entry.IsVisible = true;
            editTerm5Entry.Text = termName;
            editStart5.Date = DateTime.Parse(term5.First().startDate);
            editEnd5.Date = DateTime.Parse(term5.First().endDate);
            cancelEdit5.IsVisible = true;
            editTerm5.IsVisible = false;
            save5.IsVisible = true;
            delete5.IsVisible = true;
            deleteCourse5.IsVisible = true;
            addCourse5.IsVisible = false;

        }

        #endregion


        #region Term6

        if (sender == editTerm6)
        {
            Term6.IsVisible = false;

            string termName = term6.First().termName;

            editStart6.IsVisible = true;
            editDash6.IsVisible = true;
            editEnd6.IsVisible = true;
            editTerm6Entry.IsVisible = true;
            editTerm6Entry.Text = termName;
            editStart6.Date = DateTime.Parse(term6.First().startDate);
            editEnd6.Date = DateTime.Parse(term6.First().endDate);
            cancelEdit6.IsVisible = true;
            editTerm6.IsVisible = false;
            save6.IsVisible = true;
            delete6.IsVisible = true;
            deleteCourse6.IsVisible = true;
            addCourse6.IsVisible = false;

        }

        #endregion

    }

    private async void collapse_Clicked(object sender, EventArgs e)
    {
        await Connection._db.DropTableAsync<terms>();
        await Connection._db.DropTableAsync<courses>();
        await Connection._db.DropTableAsync<instructors>();
        await Connection._db.DropTableAsync<PAs>();
        await Connection._db.DropTableAsync<OAs>();
        await Connection._db.DropTableAsync<notifyCourse>();
        await Connection._db.DropTableAsync<notifyPA>();
        await Connection._db.DropTableAsync<notifyOA>();


        #region Term1 
        if (sender == collapse1)
        {

            if (Term1Courses.IsVisible == false)
            {

                if (noCourse1 == true)
                {
                    noCourses1.IsVisible = true;
                    noCourses1.Opacity = 0;
                    noCourses1.FadeTo(1, 350);

                }

                Term1Courses.IsVisible = true;
                editTerm1.IsVisible = true;
                addCourse1.IsVisible = true;

                Term1Courses.Opacity = 0;
                Term1Courses.FadeTo(1, 350);
                editTerm1.Opacity = 0;
                editTerm1.FadeTo(1, 350);
                addCourse1.Opacity = 0;
                addCourse1.FadeTo(1, 350);

                await collapse1.RotateTo(-180, 250, null);



            }
            else if (Term1Courses.IsVisible == true)
            {

                await collapse1.RotateTo(0, 250, null);

                noCourses1.IsVisible = false;
                Term1Courses.IsVisible = false;
                editTerm1.IsVisible = false;
                Term1.IsVisible = true;
                editStart1.IsVisible = false;
                editDash1.IsVisible = false;
                editEnd1.IsVisible = false;
                editTerm1Entry.IsVisible = false;
                cancelEdit1.IsVisible = false;
                editTerm1.IsVisible = false;
                save1.IsVisible = false;
                delete1.IsVisible = false;
                addCourse1.IsVisible = false;
                deleteCourse1.IsVisible = false;


            }
        }
        #endregion


        #region Term2
        if (sender == collapse2)
        {

            if (Term2Courses.IsVisible == false)
            {

                if (noCourse2 == true)
                {
                    noCourses2.IsVisible = true;
                    noCourses2.Opacity = 0;
                    noCourses2.FadeTo(1, 350);

                }

                Term2Courses.IsVisible = true;
                editTerm2.IsVisible = true;
                addCourse2.IsVisible = true;

                Term2Courses.Opacity = 0;
                Term2Courses.FadeTo(1, 350);
                editTerm2.Opacity = 0;
                editTerm2.FadeTo(1, 350);
                addCourse2.Opacity = 0;
                addCourse2.FadeTo(1, 350);

                await collapse2.RotateTo(-180, 250, null);


            }
            else if (Term2Courses.IsVisible == true)
            {
                await collapse2.RotateTo(0, 250, null);

                Term2Courses.IsVisible = false;
                noCourses2.IsVisible = false;

                editTerm2.IsVisible = false;

                Term2.IsVisible = true;
                editStart2.IsVisible = false;
                editDash2.IsVisible = false;
                editEnd2.IsVisible = false;
                editTerm2Entry.IsVisible = false;

                cancelEdit2.IsVisible = false;
                editTerm2.IsVisible = false;
                save2.IsVisible = false;
                delete2.IsVisible = false;
                addCourse2.IsVisible = false;
                deleteCourse2.IsVisible = false;

            }
        }
        #endregion


        #region Term3
        if (sender == collapse3)
        {

            if (Term3Courses.IsVisible == false)
            {


                if (noCourse3 == true)
                {
                    noCourses3.IsVisible = true;
                    noCourses3.Opacity = 0;
                    noCourses3.FadeTo(1, 350);

                }

                Term3Courses.IsVisible = true;
                editTerm3.IsVisible = true;
                addCourse3.IsVisible = true;

                Term3Courses.Opacity = 0;
                Term3Courses.FadeTo(1, 350);
                editTerm3.Opacity = 0;
                editTerm3.FadeTo(1, 350);
                addCourse3.Opacity = 0;
                addCourse3.FadeTo(1, 350);

                await collapse3.RotateTo(-180, 250, null);


            }
            else if (Term3Courses.IsVisible == true)
            {
                await collapse3.RotateTo(0, 250, null);

                noCourses3.IsVisible = false;
                Term3Courses.IsVisible = false;

                editTerm3.IsVisible = false;

                Term3.IsVisible = true;
                editStart3.IsVisible = false;
                editDash3.IsVisible = false;
                editEnd3.IsVisible = false;
                editTerm3Entry.IsVisible = false;

                cancelEdit3.IsVisible = false;
                editTerm3.IsVisible = false;
                save3.IsVisible = false;
                delete3.IsVisible = false;
                addCourse3.IsVisible = false;
                deleteCourse3.IsVisible = false;

            }
        }
        #endregion


        #region Term4

        if (sender == collapse4)
        {

            if (Term4Courses.IsVisible == false)
            {

                if (noCourse4 == true)
                {
                    noCourses4.IsVisible = true;
                    noCourses4.Opacity = 0;
                    noCourses4.FadeTo(1, 350);

                }

                Term4Courses.IsVisible = true;
                editTerm4.IsVisible = true;
                addCourse4.IsVisible = true;

                Term4Courses.Opacity = 0;
                Term4Courses.FadeTo(1, 350);
                editTerm4.Opacity = 0;
                editTerm4.FadeTo(1, 350);
                addCourse4.Opacity = 0;
                addCourse4.FadeTo(1, 350);

                await collapse4.RotateTo(-180, 250, null);


            }
            else if (Term4Courses.IsVisible == true)
            {
                await collapse4.RotateTo(0, 250, null);

                noCourses4.IsVisible = false;

                Term4Courses.IsVisible = false;

                editTerm4.IsVisible = false;

                Term4.IsVisible = true;
                editStart4.IsVisible = false;
                editDash4.IsVisible = false;
                editEnd4.IsVisible = false;
                editTerm4Entry.IsVisible = false;

                cancelEdit4.IsVisible = false;
                editTerm4.IsVisible = false;
                save4.IsVisible = false;
                delete4.IsVisible = false;
                addCourse4.IsVisible = false;
                deleteCourse4.IsVisible = false;

            }
        }

        #endregion


        #region Term5
        if (sender == collapse5)
        {

            if (Term5Courses.IsVisible == false)
            {


                if (noCourse5 == true)
                {
                    noCourses5.IsVisible = true;
                    noCourses5.Opacity = 0;
                    noCourses5.FadeTo(1, 350);

                }

                Term5Courses.IsVisible = true;
                editTerm5.IsVisible = true;
                addCourse5.IsVisible = true;

                Term5Courses.Opacity = 0;
                Term5Courses.FadeTo(1, 350);
                editTerm5.Opacity = 0;
                editTerm5.FadeTo(1, 350);
                addCourse5.Opacity = 0;
                addCourse5.FadeTo(1, 350);

                await collapse5.RotateTo(-180, 250, null);


            }
            else if (Term5Courses.IsVisible == true)
            {
                await collapse5.RotateTo(0, 250, null);

                noCourses5.IsVisible = false;

                Term5Courses.IsVisible = false;

                editTerm5.IsVisible = false;

                Term5.IsVisible = true;
                editStart5.IsVisible = false;
                editDash5.IsVisible = false;
                editEnd5.IsVisible = false;
                editTerm5Entry.IsVisible = false;

                cancelEdit5.IsVisible = false;
                editTerm5.IsVisible = false;
                save5.IsVisible = false;
                delete5.IsVisible = false;
                addCourse5.IsVisible = false;
                deleteCourse5.IsVisible = false;
            }
        }
        #endregion


        #region Term6

        if (sender == collapse6)
        {

            if (Term6Courses.IsVisible == false)
            {


                if (noCourse6 == true)
                {
                    noCourses6.IsVisible = true;
                    noCourses6.Opacity = 0;
                    noCourses6.FadeTo(1, 350);

                }

                Term6Courses.IsVisible = true;
                editTerm6.IsVisible = true;
                addCourse6.IsVisible = true;

                Term6Courses.Opacity = 0;
                Term6Courses.FadeTo(1, 350);
                editTerm6.Opacity = 0;
                editTerm6.FadeTo(1, 350);
                addCourse6.Opacity = 0;
                addCourse6.FadeTo(1, 350);

                await collapse6.RotateTo(-180, 250, null);




            }
            else if (Term6Courses.IsVisible == true)
            {
                await collapse6.RotateTo(0, 250, null);

                noCourses6.IsVisible = false;

                Term6Courses.IsVisible = false;

                editTerm6.IsVisible = false;

                Term6.IsVisible = true;
                editStart6.IsVisible = false;
                editDash6.IsVisible = false;
                editEnd6.IsVisible = false;
                editTerm6Entry.IsVisible = false;

                cancelEdit6.IsVisible = false;
                editTerm6.IsVisible = false;
                save6.IsVisible = false;
                delete6.IsVisible = false;
                addCourse6.IsVisible = false;
                deleteCourse6.IsVisible = false;

            }
        }
        #endregion

    }

    private void cancel_Clicked(object sender, EventArgs e)
    {
        #region Term1
        if (sender == cancelEdit1)
        {
            Term1.IsVisible = true;
            editStart1.IsVisible = false;
            editDash1.IsVisible = false;
            editEnd1.IsVisible = false;
            editTerm1Entry.IsVisible = false;
            cancelEdit1.IsVisible = false;
            editTerm1.IsVisible = true;
            save1.IsVisible = false;
            delete1.IsVisible = false;
            addCourse1.IsVisible = true;
            deleteCourse1.IsVisible = false;
        }
        #endregion

        #region Term2
        if (sender == cancelEdit2)
        {
            Term2.IsVisible = true;
            editStart2.IsVisible = false;
            editDash2.IsVisible = false;
            editEnd2.IsVisible = false;
            editTerm2Entry.IsVisible = false;
            cancelEdit2.IsVisible = false;
            editTerm2.IsVisible = true;
            save2.IsVisible = false;
            delete2.IsVisible = false;
            addCourse2.IsVisible = true;
            deleteCourse2.IsVisible = false;
        }
        #endregion

        #region Term3
        if (sender == cancelEdit3)
        {
            Term3.IsVisible = true;
            editStart3.IsVisible = false;
            editDash3.IsVisible = false;
            editEnd3.IsVisible = false;
            editTerm3Entry.IsVisible = false;
            cancelEdit3.IsVisible = false;
            editTerm3.IsVisible = true;
            save3.IsVisible = false;
            delete3.IsVisible = false;
            addCourse3.IsVisible = true;
            deleteCourse3.IsVisible = false;
        }
        #endregion

        #region Term4
        if (sender == cancelEdit4)
        {
            Term4.IsVisible = true;
            editStart4.IsVisible = false;
            editDash4.IsVisible = false;
            editEnd4.IsVisible = false;
            editTerm4Entry.IsVisible = false;
            cancelEdit4.IsVisible = false;
            editTerm4.IsVisible = true;
            save4.IsVisible = false;
            delete4.IsVisible = false;
            addCourse4.IsVisible = true;
            deleteCourse4.IsVisible = false;
        }
        #endregion

        #region Term5
        if (sender == cancelEdit5)
        {
            Term5.IsVisible = true;
            editStart5.IsVisible = false;
            editDash5.IsVisible = false;
            editEnd5.IsVisible = false;
            editTerm5Entry.IsVisible = false;
            cancelEdit5.IsVisible = false;
            editTerm5.IsVisible = true;
            save5.IsVisible = false;
            delete5.IsVisible = false;
            addCourse5.IsVisible = true;
            deleteCourse5.IsVisible = false;
        }
        #endregion

        #region Term6
        if (sender == cancelEdit6)
        {
            Term6.IsVisible = true;
            editStart6.IsVisible = false;
            editDash6.IsVisible = false;
            editEnd6.IsVisible = false;
            editTerm6Entry.IsVisible = false;
            cancelEdit6.IsVisible = false;
            editTerm6.IsVisible = true;
            save6.IsVisible = false;
            delete6.IsVisible = false;
            addCourse6.IsVisible = true;
            deleteCourse6.IsVisible = false;
        }
        #endregion

        #region New Term
        else
        {
            addTerm.IsVisible = true;

            addTermName.IsVisible = false;
            addTermEnd.IsVisible = false;
            addTermStart.IsVisible = false;
            startLabel.IsVisible = false;
            endLabel.IsVisible = false;
            saveTerm.IsVisible = false;
            cancelTerm.IsVisible = false;

        }
        #endregion
    }

    private async void saveTerm_Clicked(object sender, EventArgs e)
    {


        //For Saving Edits
        try
        {
            if (sender == save1)
            {

                await dbQuery.updateTerm(term1Id, editTerm1Entry.Text, editStart1.Date.ToShortDateString(), editEnd1.Date.ToShortDateString());
            }
            else if (sender == save2)
            {

                await dbQuery.updateTerm(term2Id, editTerm2Entry.Text, editStart2.Date.ToShortDateString(), editEnd2.Date.ToShortDateString());
            }
            else if (sender == save3)
            {

                await dbQuery.updateTerm(term3Id, editTerm3Entry.Text, editStart3.Date.ToShortDateString(), editEnd3.Date.ToShortDateString());
            }
            else if (sender == save4)
            {

                await dbQuery.updateTerm(term4Id, editTerm4Entry.Text, editStart4.Date.ToShortDateString(), editEnd4.Date.ToShortDateString());
            }
            else if (sender == save5)
            {

                await dbQuery.updateTerm(term5Id, editTerm5Entry.Text, editStart5.Date.ToShortDateString(), editEnd5.Date.ToShortDateString());
            }
            else if (sender == save6)
            {

                await dbQuery.updateTerm(term6Id, editTerm6Entry.Text, editStart6.Date.ToShortDateString(), editEnd6.Date.ToShortDateString());
            }
            else
            {
                //For Saving New Term
                if (string.IsNullOrWhiteSpace(addTermName.Text) == true)
                {
                    await DisplayAlert("Error", "Term name can not be empty", "Ok");
                    return;
                }
                else
                {
                    await dbQuery.AddTerm(addTermName.Text, addTermStart.Date.ToShortDateString(), addTermEnd.Date.ToShortDateString());
                }

            };
        }
        catch { }

        await Navigation.PushModalAsync(new TermsPage());
    }

    private async void deleteTerm_Clicked(object sender, EventArgs e)
    {

        bool delete = await DisplayAlert("Are you sure?", "If you delete this term, it will also delete all associated courses, assessments, instructors, and notifications. Delete Term?", "Yes", "No");

        if (delete == true)
        {

            if (sender == delete1)
            {
                await dbQuery.deleteTerm(term1Id);


            }
            if (sender == delete2)
            {
                await dbQuery.deleteTerm(term2Id);
            }

            if (sender == delete3)
            {
                await dbQuery.deleteTerm(term3Id);
            }
            if (sender == delete4)
            {
                await dbQuery.deleteTerm(term4Id);
            }
            if (sender == delete5)
            {
                await dbQuery.deleteTerm(term5Id);
            }
            if (sender == delete6)
            {
                await dbQuery.deleteTerm(term6Id);
            }
            await Navigation.PushModalAsync(new TermsPage());
        }

        else
        {
            return;
        }
    }

    #region Open Course Info
    private async void term1CourseTapped(object sender, ItemTappedEventArgs e)
    {



        Term2Courses.SelectedItem = null;
        Term3Courses.SelectedItem = null;
        Term4Courses.SelectedItem = null;
        Term5Courses.SelectedItem = null;
        Term6Courses.SelectedItem = null;

        selectedCourse = Term1Courses.SelectedItem;
        


        if (deleteCourse1.IsVisible != true)
        {
            await Navigation.PushModalAsync(new CourseInfo(selectedCourse as courses));
        }


    }
    private async void term2CourseTapped(object sender, ItemTappedEventArgs e)
    {
        Term1Courses.SelectedItem = null;
        Term3Courses.SelectedItem = null;
        Term4Courses.SelectedItem = null;
        Term5Courses.SelectedItem = null;
        Term6Courses.SelectedItem = null;

        selectedCourse = Term2Courses.SelectedItem;
        

        if (deleteCourse2.IsVisible != true)
        {
            await Navigation.PushModalAsync(new CourseInfo(selectedCourse as courses));
        }
    }
    private async void term3CourseTapped(object sender, ItemTappedEventArgs e)
    {

        Term1Courses.SelectedItem = null;
        Term2Courses.SelectedItem = null;
        Term4Courses.SelectedItem = null;
        Term5Courses.SelectedItem = null;
        Term6Courses.SelectedItem = null;

        selectedCourse = Term3Courses.SelectedItem;

        if (deleteCourse3.IsVisible != true)
        {
            await Navigation.PushModalAsync(new CourseInfo(selectedCourse as courses));
        }
    }
    private async void term4CourseTapped(object sender, ItemTappedEventArgs e)
    {
        Term1Courses.SelectedItem = null;
        Term2Courses.SelectedItem = null;
        Term3Courses.SelectedItem = null;
        Term5Courses.SelectedItem = null;
        Term6Courses.SelectedItem = null;

        selectedCourse = Term4Courses.SelectedItem;

        if (deleteCourse4.IsVisible != true)
        {
            await Navigation.PushModalAsync(new CourseInfo(selectedCourse as courses));
        }
    }
    private async void term5CourseTapped(object sender, ItemTappedEventArgs e)
    {
        Term1Courses.SelectedItem = null;
        Term2Courses.SelectedItem = null;
        Term3Courses.SelectedItem = null;
        Term4Courses.SelectedItem = null;
        Term6Courses.SelectedItem = null;

        selectedCourse = Term5Courses.SelectedItem;

        if (deleteCourse5.IsVisible != true)
        {
            await Navigation.PushModalAsync(new CourseInfo(selectedCourse as courses));
        }
    }
    private async void term6CourseTapped(object sender, ItemTappedEventArgs e)
    {
        Term1Courses.SelectedItem = null;
        Term2Courses.SelectedItem = null;
        Term3Courses.SelectedItem = null;
        Term4Courses.SelectedItem = null;
        Term5Courses.SelectedItem = null;


        selectedCourse = Term6Courses.SelectedItem;

        if (deleteCourse6.IsVisible != true)
        {
            await Navigation.PushModalAsync(new CourseInfo(selectedCourse as courses));
        }
    }
    #endregion

    private async void deleteCourse_Clicked(object sender, EventArgs e)
    {

        if (selectedCourse == null)
        {
            await DisplayAlert("Select A Course", "Select a course to delete", "Ok");
        }
        else
        {
            courses course = selectedCourse as courses;

            await dbQuery.DeleteCourse(course.coursesId);

            onStart();
        }
    }

    private async void notify()
    {
        #region Notify Courses

        // Notify Start Dates
        IEnumerable<notifyCourse> notifyCourse = await dbQuery.getNotifyCourses();

        try
        {
            for (int i = 0; i < notifyCourse.Count(); i++)
            {


                if (DateTime.Parse(notifyCourse.ElementAt(i).start).AddDays(-1) ==  DateTime.Today ||
                    DateTime.Parse(notifyCourse.ElementAt(i).start).AddDays(-3) == DateTime.Today ||
                    DateTime.Parse(notifyCourse.ElementAt(i).start).AddDays(-7) == DateTime.Today )
                {
                    await DisplayAlert("Upcoming Start Date", notifyCourse.ElementAt(i).courseName + " is expected to start on " + notifyCourse.ElementAt(i).start, "Ok");

                }
            }
        }
        catch { }

        //Notify End Date

        try
        {
            for (int i = 0; i < notifyCourse.Count(); i++)
            {


                if (DateTime.Parse(notifyCourse.ElementAt(i).end).AddDays(-1) == DateTime.Today ||
                    DateTime.Parse(notifyCourse.ElementAt(i).end).AddDays(-3) == DateTime.Today ||
                    DateTime.Parse(notifyCourse.ElementAt(i).end).AddDays(-7) == DateTime.Today)
                {
                    await DisplayAlert("Upcoming End Date", notifyCourse.ElementAt(i).courseName + " is expected to end on " + notifyCourse.ElementAt(i).end, "Ok");

                }
            }
        }
        catch { }

        //Notify Due Dates

        try
        {
            for (int i = 0; i < notifyCourse.Count(); i++)
            {


                if (
                    DateTime.Parse(notifyCourse.ElementAt(i).dueDate).AddDays(-1) == DateTime.Today ||
                    DateTime.Parse(notifyCourse.ElementAt(i).dueDate).AddDays(-3) == DateTime.Today ||
                    DateTime.Parse(notifyCourse.ElementAt(i).dueDate).AddDays(-7) == DateTime.Today)
                {
                    await DisplayAlert("Upcoming Due Date", notifyCourse.ElementAt(i).courseName + " is due on " + notifyCourse.ElementAt(i).dueDate, "Ok");

                }
                else if (DateTime.Parse(notifyCourse.ElementAt(i).dueDate) == DateTime.Today)
                {
                    await DisplayAlert("Course Due", notifyCourse.ElementAt(i).courseName + " is due today!", "Ok");

                }
            }
        }
        catch { }
        #endregion


        #region Notify PA

        // Notify Start Dates
        IEnumerable<notifyPA> notifyPA = await dbQuery.getNotifyPAs();

        try
        {
            for (int i = 0; i < notifyPA.Count(); i++)
            {


                if (DateTime.Parse(notifyPA.ElementAt(i).start).AddDays(-1) == DateTime.Today ||
                    DateTime.Parse(notifyPA.ElementAt(i).start).AddDays(-3) == DateTime.Today ||
                    DateTime.Parse(notifyPA.ElementAt(i).start).AddDays(-7) == DateTime.Today)
                {
                    await DisplayAlert("Upcoming Assessment", notifyPA.ElementAt(i).paName + " is expected to start on " + notifyPA.ElementAt(i).start, "Ok");

                }
            }
        }
        catch { }

        //Notify End Date

        try
        {
            for (int i = 0; i < notifyPA.Count(); i++)
            {


                if (DateTime.Parse(notifyPA.ElementAt(i).end).AddDays(-1) == DateTime.Today ||
                    DateTime.Parse(notifyPA.ElementAt(i).end).AddDays(-3) == DateTime.Today ||
                    DateTime.Parse(notifyPA.ElementAt(i).end).AddDays(-7) == DateTime.Today)
                {
                    await DisplayAlert("Upcoming Assessment", notifyPA.ElementAt(i).paName + " is expected to end on " + notifyPA.ElementAt(i).end, "Ok");

                }
            }
        }
        catch { }

        //Notify Due Dates

        try
        {
            for (int i = 0; i < notifyPA.Count(); i++)
            {


                if (
                    DateTime.Parse(notifyPA.ElementAt(i).dueDate).AddDays(-1) == DateTime.Today ||
                    DateTime.Parse(notifyPA.ElementAt(i).dueDate).AddDays(-3) == DateTime.Today ||
                    DateTime.Parse(notifyPA.ElementAt(i).dueDate).AddDays(-7) == DateTime.Today)
                {
                    await DisplayAlert("Assessment Due", notifyPA.ElementAt(i).paName + " is due on " + notifyPA.ElementAt(i).dueDate, "Ok");

                }
                else if (DateTime.Parse(notifyPA.ElementAt(i).dueDate) == DateTime.Today)
                {
                    await DisplayAlert("Assessment Due", notifyPA.ElementAt(i).paName + " is due today!", "Ok");

                }
            }
        }
        catch { }
        #endregion


        #region Notify OA

        // Notify Start Dates
        IEnumerable<notifyOA> notifyOA = await dbQuery.getNotifyOAs();

        try
        {
            for (int i = 0; i < notifyOA.Count(); i++)
            {


                if (DateTime.Parse(notifyOA.ElementAt(i).start).AddDays(-1) == DateTime.Today ||
                    DateTime.Parse(notifyOA.ElementAt(i).start).AddDays(-3) == DateTime.Today ||
                    DateTime.Parse(notifyOA.ElementAt(i).start).AddDays(-7) == DateTime.Today)
                {
                    await DisplayAlert("Upcoming Assessment", notifyOA.ElementAt(i).oaName + " is expected to start on " + notifyOA.ElementAt(i).start, "Ok");

                }
            }
        }
        catch { }

        //Notify End Date

        try
        {
            for (int i = 0; i < notifyOA.Count(); i++)
            {


                if (DateTime.Parse(notifyOA.ElementAt(i).end).AddDays(-1) == DateTime.Today ||
                    DateTime.Parse(notifyOA.ElementAt(i).end).AddDays(-3) == DateTime.Today ||
                    DateTime.Parse(notifyOA.ElementAt(i).end).AddDays(-7) == DateTime.Today)
                {
                    await DisplayAlert("Upcoming Assessment", notifyOA.ElementAt(i).oaName + " is expected to end on " + notifyOA.ElementAt(i).end, "Ok");

                }
            }
        }
        catch { }

        //Notify Due Dates

        try
        {
            for (int i = 0; i < notifyOA.Count(); i++)
            {


                if (
                    DateTime.Parse(notifyOA.ElementAt(i).dueDate).AddDays(-1) == DateTime.Today ||
                    DateTime.Parse(notifyOA.ElementAt(i).dueDate).AddDays(-3) == DateTime.Today ||
                    DateTime.Parse(notifyOA.ElementAt(i).dueDate).AddDays(-7) == DateTime.Today)
                {
                    await DisplayAlert("Assessment Due", notifyOA.ElementAt(i).oaName + " is due on " + notifyOA.ElementAt(i).dueDate, "Ok");

                }
                else if (DateTime.Parse(notifyOA.ElementAt(i).dueDate) == DateTime.Today)
                {
                    await DisplayAlert("Assessment Due", notifyOA.ElementAt(i).oaName + " is due today!", "Ok");

                }
            }
        }
        catch { }
        #endregion

    }
}