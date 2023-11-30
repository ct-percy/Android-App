namespace MauiApp3;

public partial class Assessment : ContentPage
{
	public Assessment(courses selectedCourse)
	{
		InitializeComponent();
	}

    public Assessment(courses selectedCourse, OAs oa)
    {
        InitializeComponent();
    }
    public Assessment(courses selectedCourse, PAs pa)
    {
        InitializeComponent();
    }
}