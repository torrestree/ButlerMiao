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
        ObservableCollection<TModel> Items { get; set; }
        TModel? SelectedItem { get; set; }
        bool IsEditing { get; set; }
        AsyncRelayCommand CmdRefresh { get; set; }
        AsyncRelayCommand CmdEdit { get; set; }
        RelayCommand CmdAdd { get; set; }
        RelayCommand CmdRemove { get; set; }
        AsyncRelayCommand CmdSave { get; set; }
        AsyncRelayCommand CmdSaveAndQuit { get; set; }

        Task Refresh();
        Task Edit();
        void Add();
        void Remove();
        Task Save();
        Task SaveAndQuit();
    }

    public abstract class VmDictionaryModelsEditor<TContext, TModel> : VmBase, IVmDictionaryModelsEditor<TModel>
        where TContext : DbContext
        where TModel : class, IDictionaryModel
    {
        protected TContext Context { get; set; } = null!;

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
            set => SetProperty(ref isEditing, value);
        }

        public AsyncRelayCommand CmdRefresh { get; set; }

        public AsyncRelayCommand CmdEdit { get; set; }

        public RelayCommand CmdAdd { get; set; }

        public RelayCommand CmdRemove { get;set; }

        public AsyncRelayCommand CmdSave { get; set; }

        public AsyncRelayCommand CmdSaveAndQuit { get; set; }

        protected VmDictionaryModelsEditor()
        {
            CmdRefresh = new(Refresh);
            CmdEdit = new(Edit, CanEdit);
            CmdAdd = new(Add, CanAdd);
            CmdRemove = new(Remove, CanRemove);
            CmdSave = new(Save, CanSave);
        }

        public async Task Refresh()
        {
            Context = DIService.ServiceProvider.GetRequiredService<TContext>();
            await Context.Set<TModel>().LoadAsync();
            Items = Context.Set<TModel>().Local.ToObservableCollection();
        }

        public async Task Edit()
        {
            IsEditing = true;
        }
        private bool CanEdit()
        {
            return !IsEditing;
        }

        public void Add()
        {
            TModel newItem = DIService.ServiceProvider.GetRequiredService<TModel>();
            Context.Set<TModel>().Add(newItem);
            SelectedItem = newItem;
        }
        private bool CanAdd()
        {
            return IsEditing;
        }

        public void Remove()
        {
            if (SelectedItem != null)
            {
                Context.Set<TModel>().Remove(SelectedItem);
                SelectedItem = null;
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
    }
}
