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
        }
        if (termCount == 1)
        {


            collapse2.IsVisible = false;
            collapse3.IsVisible = false;
            collapse4.IsVisible = false;
            collapse5.IsVisible = false;
            collapse6.IsVisible = false;

        }
        if (termCount == 2)
        {

            collapse3.IsVisible = false;
            collapse4.IsVisible = false;
            collapse5.IsVisible = false;
            collapse6.IsVisible = false;


        }
        if (termCount == 3)
        {

            collapse4.IsVisible = false;
            collapse5.IsVisible = false;
            collapse6.IsVisible = false;


        }
        if (termCount == 4)
        {

            collapse5.IsVisible = false;
            collapse6.IsVisible = false;


        }
        if (termCount == 5)
        {

            collapse6.IsVisible = false;



        }

    }

    public TermsPage()
    {


        onStart();
        InitializeComponent();



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
            cancelEdit1.IsVisible = true;
            editTerm1.IsVisible = false;
            save1.IsVisible = true;
            delete1.IsVisible = true;
           

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
            cancelEdit2.IsVisible = true;
            editTerm2.IsVisible = false;
            save2.IsVisible = true;
            delete2.IsVisible = true;
          
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
            cancelEdit3.IsVisible = true;
            editTerm3.IsVisible = false;
            save3.IsVisible = true;
            delete3.IsVisible = true;
          
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
            cancelEdit4.IsVisible = true;
            editTerm4.IsVisible = false;
            save4.IsVisible = true;
            delete4.IsVisible = true;
          

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
            cancelEdit5.IsVisible = true;
            editTerm5.IsVisible = false;
            save5.IsVisible = true;
            delete5.IsVisible = true;
          
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
            cancelEdit6.IsVisible = true;
            editTerm6.IsVisible = false;
            save6.IsVisible = true;
            delete6.IsVisible = true;
          
        }

        #endregion

    }

    private async void collapse_Clicked(object sender, EventArgs e)
    {
       

        #region Term1 
        if (sender == collapse1)
        {

           
                editTerm1.IsVisible = true;
                

               
                editTerm1.Opacity = 0;
                editTerm1.FadeTo(1, 350);
               

                await collapse1.RotateTo(-180, 250, null);



            }
          
        
        #endregion


        #region Term2
        if (sender == collapse2)
        {

           

               
                editTerm2.IsVisible = true;
              
                editTerm2.Opacity = 0;
                editTerm2.FadeTo(1, 350);


                await collapse2.RotateTo(-180, 250, null);


            }
          
        
        #endregion


        #region Term3
        if (sender == collapse3)
        {

          
                editTerm3.IsVisible = true;
               
                editTerm3.Opacity = 0;
                editTerm3.FadeTo(1, 350);
               
                await collapse3.RotateTo(-180, 250, null);


            }
        
        #endregion


        #region Term4

        if (sender == collapse4)
        {

          
                editTerm4.IsVisible = true;
              
                editTerm4.Opacity = 0;
                editTerm4.FadeTo(1, 350);
               

                await collapse4.RotateTo(-180, 250, null);


            }
           
        

        #endregion


        #region Term5
        if (sender == collapse5)
        {

           
                editTerm5.IsVisible = true;
               
                editTerm5.Opacity = 0;
                editTerm5.FadeTo(1, 350);
              

                await collapse5.RotateTo(-180, 250, null);


            }
         
        #endregion


        #region Term6

        if (sender == collapse6)
        {

          
                editTerm6.IsVisible = true;
               
                editTerm6.Opacity = 0;
                editTerm6.FadeTo(1, 350);
              

                await collapse6.RotateTo(-180, 250, null);




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

            await dbQuery.AddTerm(addTermName.Text, addTermStart.Date.ToShortDateString(), addTermEnd.Date.ToShortDateString());


        };



        await Navigation.PushModalAsync(new TermsPage());

    }

    private async void deleteTerm_Clicked(object sender, EventArgs e)
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



}