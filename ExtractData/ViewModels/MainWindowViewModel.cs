using ExtractData.Domain.Services;
using ExtractData.Entities.ValueObjects.MySqlServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ExtractData.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<ShowDatabase> _Databases;

        public ObservableCollection<ShowDatabase> Databases 
        {
            get { return _Databases; } set { SetProperty(ref _Databases, value); }
        }

        private string _Provider;

        public string Provider 
        {
            get { return _Provider; }
            set { SetProperty(ref _Provider, value); Databases = LoadDatabases(); }
        }

        private string _ConnectionString;

        public string ConnectionString 
        {
            get { return _ConnectionString; } set { SetProperty(ref _ConnectionString, value);  } 
        }

        private ObservableCollection<ShowDatabase> LoadDatabases()
        {
            MysqlServerService mysql = new MysqlServerService(ConnectionString);
            var data = mysql.ShowDatabase();

            var databases = new ObservableCollection<ShowDatabase>();

            foreach (var database in data)
            {
                databases.Add(database);
            }

            return databases;
        }

    }
}
