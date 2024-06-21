using CommunityToolkit.Mvvm.Input;
using Core.Abstract.Model;
using Core.Misc.DI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Core.Abstract.ViewModel
{
    public interface IVmDictionaryModelsEditor<TModel>
        where TModel : IDictionaryModel
    {
        string SearchString { get; set; }
        ObservableCollection<TModel> Items { get; set; }
        TModel? SelectedItem { get; set; }
        bool IsEditing { get; set; }
        AsyncRelayCommand CmdRefresh { get; set; }
        RelayCommand CmdEdit { get; set; }
        RelayCommand CmdAdd { get; set; }
        RelayCommand CmdRemove { get; set; }
        AsyncRelayCommand CmdSave { get; set; }
        AsyncRelayCommand CmdSaveAndQuit { get; set; }

        Task Refresh();
        void Edit();
        void Add();
        void Remove();
        Task Save();
        Task SaveAndQuit();
    }

    public abstract class VmDictionaryModelsEditor<TContext, TModel> : VmBase, IVmDictionaryModelsEditor<TModel>
        where TContext : DbContext
        where TModel : class, IDictionaryModel
    {
        private TContext context = null!;
        protected TContext Context
        {
            get => context;
            set
            {
                context = value;
                CmdEdit.NotifyCanExecuteChanged();
            }
        }

        private string searchString = string.Empty;
        public string SearchString
        {
            get => searchString;
            set
            {
                SetProperty(ref searchString, value);
                UpdateItems();
            }
        }

        private ObservableCollection<TModel> dataSource = [];
        protected ObservableCollection<TModel> DataSource
        {
            get => dataSource;
            set
            {
                SetProperty(ref dataSource, value);
                UpdateItems();
            }
        }

        private ObservableCollection<TModel> items = [];
        public ObservableCollection<TModel> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

        private TModel? selectedItem;
        public TModel? SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        private bool isEditing;
        public bool IsEditing
        {
            get => isEditing;
            set
            {
                SetProperty(ref isEditing, value);
                CmdEdit.NotifyCanExecuteChanged();
                CmdAdd.NotifyCanExecuteChanged();
                CmdRemove.NotifyCanExecuteChanged();
                CmdSave.NotifyCanExecuteChanged();
                CmdSaveAndQuit.NotifyCanExecuteChanged();
            }
        }

        public AsyncRelayCommand CmdRefresh { get; set; }

        public RelayCommand CmdEdit { get; set; }

        public RelayCommand CmdAdd { get; set; }

        public RelayCommand CmdRemove { get; set; }

        public AsyncRelayCommand CmdSave { get; set; }

        public AsyncRelayCommand CmdSaveAndQuit { get; set; }

        public VmDictionaryModelsEditor()
        {
            CmdRefresh = new(Refresh);
            CmdEdit = new(Edit, CanEdit);
            CmdAdd = new(Add, CanAdd);
            CmdRemove = new(Remove, CanRemove);
            CmdSave = new(Save, CanSave);
            CmdSaveAndQuit = new(SaveAndQuit, CanSaveAndQuit);
        }

        public async Task Refresh()
        {
            Context = DIService.ServiceProvider.GetRequiredService<TContext>();
            await Context.Set<TModel>().LoadAsync();
            DataSource = Context.Set<TModel>().Local.ToObservableCollection();
        }

        public void Edit()
        {
            IsEditing = true;
        }
        private bool CanEdit()
        {
            return Context != null && !IsEditing;
        }

        public void Add()
        {
            TModel newItem = DIService.ServiceProvider.GetRequiredService<TModel>();
            DataSource.Add(newItem);
            SelectedItem = newItem;
            Items.Add(newItem);
        }
        private bool CanAdd()
        {
            return IsEditing;
        }

        public void Remove()
        {
            if (SelectedItem != null)
            {
                DataSource.Remove(SelectedItem);
                SelectedItem = null;
                UpdateItems();
            }
        }
        private bool CanRemove()
        {
            return IsEditing && SelectedItem != null;
        }

        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }
        private bool CanSave()
        {
            return IsEditing;
        }

        public async Task SaveAndQuit()
        {
            await Context.SaveChangesAsync();
            IsEditing = false;
        }
        private bool CanSaveAndQuit()
        {
            return IsEditing;
        }

        private void UpdateItems()
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                Items = new(DataSource);
            }
            else
            {
                Items = new(DataSource.Where(t => Search(t, SearchString)));
            }
        }
        protected virtual bool Search(TModel item, string value)
        {
            return item.Id.ToString().Contains(value);
        }
    }
}
