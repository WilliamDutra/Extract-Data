using ExtractData.Domain.Interfaces;
using ExtractData.Domain.Services;
using ExtractData.Entities.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;

namespace ExtractData.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public Command ExtrairCommand { get; set; }

        private ShowDatabase _DatabaseSelected;

        public ShowDatabase DatabaseSelected
        {
            get 
            { 
                return _DatabaseSelected; 
            }
            set 
            { 
                SetProperty(ref _DatabaseSelected, value); 
                Tables = LoadTables(); 
            }
        }

        private ShowTable _TableSelected;

        public ShowTable TableSelected
        {
            get { return _TableSelected; }
            set { SetProperty(ref _TableSelected, value); }
        }

        private ObservableCollection<ShowDatabase> _Databases;

        public ObservableCollection<ShowDatabase> Databases
        {
            get { return _Databases; }
            set { SetProperty(ref _Databases, value); }
        }

        private ObservableCollection<ShowTable> _Tables;

        public ObservableCollection<ShowTable> Tables
        {
            get { return _Tables; }
            set { SetProperty(ref _Tables, value); }
        }

        private ObservableCollection<Provider> _Providers;

        public ObservableCollection<Provider> Providers
        {
            get { return _Providers; }
            set { SetProperty(ref _Providers, value); }
        }

        private Provider _ProviderSelected;

        public Provider ProviderSelected 
        {
            get 
            { 
                return _ProviderSelected; 
            } 
            set 
            { 
                SetProperty(ref _ProviderSelected, value); 
                Databases = LoadDatabase(); 
            }
        }

        private string _ConnectionString;

        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { SetProperty(ref _ConnectionString, value); }
        }

        private bool _IsTexto;

        public bool IsTexto
        {
            get { return _IsTexto; }
            set { SetProperty(ref _IsTexto, value); }
        }

        private bool _IsExcel;

        public bool IsExcel
        {
            get { return _IsExcel; }
            set { SetProperty(ref _IsExcel, value); }
        }

        private IMySql _MySql;

        private ISqlServer _SqlServer;

        public MainWindowViewModel()
        {
            
        }

        public MainWindowViewModel(IMySql MySql, ISqlServer SqlServer)
        {
            _MySql = MySql;
            _SqlServer = SqlServer;
            Providers = LoadProviders();
            ExtrairCommand = new Command(() => GenerateSQLTextScripts(DatabaseSelected.Database, TableSelected.Table));
        }

        private ObservableCollection<ShowDatabase> LoadSqlServerDatabases()
        {
            var databases = new ObservableCollection<ShowDatabase>();

            _SqlServer.SetConnectionStrings(ConnectionString);
            var data = _SqlServer.ShowDatabase();

            foreach (var database in data)
            {
                databases.Add(database);
            }

            return databases;
        }

        private ObservableCollection<ShowDatabase> LoadMySqlDatabases()
        {
          
            var databases = new ObservableCollection<ShowDatabase>();

            _MySql.SetConnectionStrings(ConnectionString);
            var data = _MySql.ShowDatabase();

            foreach (var database in data)
            {
                databases.Add(database);
            }

            return databases;
        }

        private ObservableCollection<ShowTable> LoadMySqlTables()
        {
            var data = _MySql.ShowTable(DatabaseSelected.Database);
            var tables = new ObservableCollection<ShowTable>();

            foreach (var table in data)
            {
                tables.Add(table);
            }

            return tables;
        }

        private ObservableCollection<ShowTable> LoadSqlServerTables()
        {
            var data = _SqlServer.ShowTable(DatabaseSelected.Database);
            var tables = new ObservableCollection<ShowTable>();

            foreach (var table in data)
            {
                tables.Add(table);
            }

            return tables;
        }

        private void GenerateSQLTextScripts(string Database, string Table)
        {

            if (IsTexto)
            {
                var data = _MySql.ShowColumn(Database, Table);
                var ScriptSQL = _MySql.GenerateScriptsSql(data, Database, Table);

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                    File.WriteAllText(saveFileDialog.FileName, ScriptSQL);

                MessageBox.Show("Arquivo salvo com sucesso!");
            }
        }

        private ObservableCollection<ShowDatabase> LoadDatabase()
        {
            int providerId = ProviderSelected.Id;

            switch (providerId)
            {
                case 1:
                    return LoadMySqlDatabases();
                    break;
                case 2:
                    return LoadSqlServerDatabases();
                default:
                    return LoadSqlServerDatabases();
                    break;
            }

        }

        private ObservableCollection<ShowTable> LoadTables()
        {
            int providerId = ProviderSelected.Id;

            switch (providerId)
            {
                case 1:
                    return LoadMySqlTables();
                    break;
                case 2:
                    return LoadSqlServerTables();
                    break;
                default:
                    return LoadSqlServerTables();
                    break;
            }

        }
        
        private ObservableCollection<Provider> LoadProviders()
        {
            var providers = new ObservableCollection<Provider>();

            providers.Add(new Provider
            {
                Id = 1,
                Name = "MYSQL SERVER"
            });

            providers.Add(new Provider
            {
                Id = 2,
                Name = "SQL SERVER"
            });

            return providers;
        }

    }
}
