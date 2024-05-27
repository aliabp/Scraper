using Scraper.wpf.Models;
using Scraper.wpf.Services;
using System.Windows;

namespace Scraper.wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void btnSearch_Click(object sender, RoutedEventArgs e)
    {
        // create scraper model based on user input
        ScraperModel model = new ScraperModel
        {
            Url = txtUrl.Text,
            Keywords = txtKeywords.Text
        };

        try
        {
            // call scraper HTTP service 
            var scraperService = new ScraperService();
            string responseString = await scraperService.SendRequestToApi(model);
            if (!string.IsNullOrEmpty(responseString))
            {
                // Split the responseString by comma and join with new lines
                txbPositions.Text = $"positions which {model.Url} is found:" 
                                    + Environment.NewLine
                                    + Environment.NewLine
                                    + responseString;
            }
        }
        catch (Exception ex)
        {
            // show error to user
            txbError.Text = $"An error occurred: {ex.Message}";
        }
    }
}