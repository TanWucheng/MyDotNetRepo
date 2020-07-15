using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows.Controls;
using CoreWpfDemo.Domain;
using CoreWpfDemo.UserControls;
using CoreWpfDemo.Widget;
using MaterialDesignThemes.Wpf;

namespace CoreWpfDemo.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<DemoItem> _allItems;
        private ObservableCollection<DemoItem> _demoItems;

        private string _searchKeyword;
        private int _selectedIndex;
        private DemoItem _selectedItem;

        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            _allItems = GenerateDemoItems(snackbarMessageQueue);
            FilterItems(null);

            MovePrevCommand = new AnotherCommandImplementation(
                _ => SelectedIndex--,
                _ => SelectedIndex > 0);

            MoveNextCommand = new AnotherCommandImplementation(
                _ => SelectedIndex++,
                _ => SelectedIndex < _allItems.Count - 1);
        }


        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DemoItems)));
                FilterItems(_searchKeyword);
            }
        }

        public ObservableCollection<DemoItem> DemoItems
        {
            get => _demoItems;
            set
            {
                _demoItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DemoItems)));
            }
        }

        public DemoItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value == null || value.Equals(_selectedItem)) return;

                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedIndex)));
            }
        }

        public AnotherCommandImplementation MovePrevCommand { get; }
        public AnotherCommandImplementation MoveNextCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<DemoItem> GenerateDemoItems(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null)
                throw new ArgumentNullException(nameof(snackbarMessageQueue));

            return new ObservableCollection<DemoItem>
            {
                new DemoItem("Home", new Home(),
                    new[]
                    {
                        new DocumentationLink(DocumentationLinkType.Wiki,
                            $"{ConfigurationManager.AppSettings["GitHub"]}/wiki", "WIKI"),
                        DocumentationLink.DemoPageLink<Home>()
                    }
                )
                {
                    HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto,
                    VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                },
                new DemoItem("Buttons", new Buttons { DataContext = new ButtonsViewModel() } ,
                    new []
                    {
                        DocumentationLink.WikiLink("Button-Styles", "Buttons"),
                        DocumentationLink.DemoPageLink<Buttons>("Demo View"),
                        DocumentationLink.DemoPageLink<ButtonsViewModel>("Demo View Model"),
                        DocumentationLink.StyleLink("Button"),
                        DocumentationLink.StyleLink("PopupBox"),
                        DocumentationLink.ApiLink<PopupBox>()
                    })
                {
                    VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                },
            };
        }

        private void FilterItems(string keyword)
        {
            var filteredItems =
                string.IsNullOrWhiteSpace(keyword)
                    ? _allItems
                    : _allItems.Where(i => i.Name.ToLower().Contains(keyword.ToLower()));

            DemoItems = new ObservableCollection<DemoItem>(filteredItems);
        }
    }
}