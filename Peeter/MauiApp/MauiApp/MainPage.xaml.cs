using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiApp
{
    public partial class MainPage : ContentPage
    {
        private readonly DbService _dbService;
        private int _editItemId;
        private ObservableCollection<Item> _items;

        public MainPage(DbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            _items = new ObservableCollection<Item>();
            Task.Run(async () => await LoadItems());
        }

        private async Task LoadItems()
        {
            var items = await _dbService.GetItems();
            foreach (var item in items)
            {
                _items.Add(item);
            }
            itemListView.ItemsSource = _items;
        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            if (_editItemId == 0)
            {
                if (decimal.TryParse(priceEntryField.Text, out decimal price))
                {
                    await _dbService.Create(new Item
                    {
                        Name = nameEntryField.Text,
                        Price = price
                    });
                }
                else
                {
                    // Handle invalid price
                }
            }
            else
            {
                if (decimal.TryParse(priceEntryField.Text, out decimal price))
                {
                    var item = _items[_editItemId];
                    item.Name = nameEntryField.Text;
                    item.Price = price;
                    await _dbService.Update(item);
                }
                else
                {
                    // Handle invalid price
                }
                _editItemId = 0;
            }
            await LoadItems();
        }

        private void itemListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (Item)e.Item;
            nameEntryField.Text = item.Name;
            priceEntryField.Text = item.Price.ToString();
            _editItemId = _items.IndexOf(item);
        }
    }
}
