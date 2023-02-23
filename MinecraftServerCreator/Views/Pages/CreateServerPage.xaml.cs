using Wpf.Ui.Common.Interfaces;

namespace MinecraftServerCreator.Views.Pages
{
    /// <summary>
    /// Interaction logic for CreateServerPage.xaml
    /// </summary>
    public partial class CreateServerPage : INavigableView<ViewModels.CreateServerViewModel>
    {
        public ViewModels.CreateServerViewModel ViewModel
        {
            get;
        }

        public CreateServerPage(ViewModels.CreateServerViewModel viewModel)
        {
            ViewModel = viewModel;
            
            InitializeComponent();
        }
    }
}
