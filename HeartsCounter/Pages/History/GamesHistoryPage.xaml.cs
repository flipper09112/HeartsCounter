using HeartsCounter.ViewModels.History;

namespace HeartsCounter.Pages.History;

public partial class GamesHistoryPage : ContentPage
{
	private GamesHistoryViewModel _gamesHistoryViewModel;

	public GamesHistoryPage(GamesHistoryViewModel gamesHistoryViewModel)
	{
		InitializeComponent();

		_gamesHistoryViewModel = gamesHistoryViewModel;
        BindingContext = gamesHistoryViewModel;
    }

    private void SelectGameFilter(object sender, SelectionChangedEventArgs e)
	{
        var current = (e.CurrentSelection.FirstOrDefault() as GameFilterModel);

		_gamesHistoryViewModel.SelectGameFilterCommand.Execute(current);
    }

	private void SelectTimeFilter(object sender, SelectionChangedEventArgs e)
	{
        var current = (e.CurrentSelection.FirstOrDefault() as DatesIntervalFilterModel);

        _gamesHistoryViewModel.SelectDateIntervalFilterListCommand.Execute(current);
    }

	private void MainCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
	{
		/*var heightFirstRow = MainGridLayout.RowDefinitions.First().Height.Value - e.VerticalOffset;
        heightFirstRow = heightFirstRow > 200 ? 200 : heightFirstRow;
        heightFirstRow = heightFirstRow < 0 ? 0.1 : heightFirstRow;

        if(e.VerticalOffset >= 200)
            MainGridLayout.RowDefinitions = new RowDefinitionCollection()
            {
                new RowDefinition() { Height = heightFirstRow },
                MainGridLayout.RowDefinitions.Last()
            };
        else if(e.VerticalOffset < 200) 
            MainGridLayout.RowDefinitions = new RowDefinitionCollection()
            {
                new RowDefinition() { Height = heightFirstRow },
                MainGridLayout.RowDefinitions.Last()
            };*/

        Console.WriteLine("VerticalOffset");
		Console.WriteLine(e.VerticalOffset);
        Console.WriteLine("VerticalDelta");
        Console.WriteLine(e.VerticalDelta);
        Console.WriteLine("MainGridLayout.RowDefinitions.First().Height");
        Console.WriteLine(MainGridLayout.RowDefinitions.First().Height);
    }
}