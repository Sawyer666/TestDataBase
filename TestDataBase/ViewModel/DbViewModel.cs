using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TestDataBase.Command;
using TestDataBase.Model;

namespace TestDataBase.ViewModel
{
    public enum Mode
    {
        Insert = 0,
        Update = 1
    }

    /// <summary>
    /// Invoker , User Control ViewModel
    /// </summary>
    public class DbViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// object reciever 
        /// </summary>
        DbBase dbAccess = null;

        List<DbRecord> dbRecordList;
        public List<DbRecord> DbRecordList
        {
            get { return dbRecordList; }
            set
            {
                dbRecordList = value;
                RaisePropertyChanged("DbRecordList");
            }
        }
        /// <summary>
        /// combobox selected row in view
        /// </summary>
        private DbRecord selectedRecord;
        public DbRecord SelectedRecord
        {
            get { return selectedRecord; }
            set
            {
                selectedRecord = value;
                RaisePropertyChanged("SelectedRecord");
            }
        }

        Mode mode;
        public Mode VMMode
        {
            get => mode;
        }

        public DbViewModel() { }
        /// <summary>
        /// устанавливаем интерфейс управления бд
        /// </summary>
        public void SetDatabaseAbility(DbBase db)
        {
            dbAccess = db;
            init();
        }

        #region private

        void init()
        {
            GetRecordList();

            UpdateViewMode(1, null);
        }
        /// <summary>
        /// Get message list from database
        /// </summary>
        void GetRecordList()
        {
            var cmd = new ReadRecordsCommand(dbAccess);
            List<DbRecord> dataDb = cmd.Execute();
            dataDb.Insert(0, new DbRecord("not select"));
            SelectedRecord = dataDb[0];
            DbRecordList = dataDb;
        }

        #endregion

        /// <summary>
        /// add new message
        /// </summary>
        /// <param name="text"></param>
        /// <param name="UpdateViewHandler"></param>
        /// <returns></returns>
        public async Task<int> AddNewMessage(string text)
        {
            return await Task<int>.Factory.StartNew(() =>
               {
                   var cmd = new AddMessageCommand(dbAccess, new DbRecord(text));
                   if (cmd.Execute() == 0)
                   {
                       GetRecordList();
                       return 0;
                   }
                   return -1;
               });
        }
        /// <summary>
        /// update message
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newMessage"></param>
        /// <returns></returns>
        public async Task<bool> UpdateMessage(DbRecord record)
        {
            return await Task<bool>.Factory.StartNew(() =>
            {
                if (SelectedRecord == null)
                    return false;
                var cmd = new UpdateMessageCommand(dbAccess, record);
                bool res = cmd.Execute();
                if (res)
                    GetRecordList();
                return res;
            });
        }
        /// <summary>
        /// button title in view
        /// </summary>
        string modeTitle = string.Empty;
        public string ModeTitle
        {
            get { return modeTitle; }
            set
            {
                modeTitle = value;
                RaisePropertyChanged("ModeTitle");
            }
        }
        /// <summary>
        /// button enable in view
        /// </summary>
        bool modeEnabled;
        public bool ModeEnabled
        {
            get { return modeEnabled; }
            set
            {
                modeEnabled = value;
                RaisePropertyChanged("ModeEnabled");
            }
        }

        public void UpdateViewMode(int index, Action updateEditTextHandler)
        {
            if (index == 0)
            {
                ModeTitle = "Add";
                mode = Mode.Insert;
                ModeEnabled = true;
            }
            else
            {
                ModeTitle = "Save";
                mode = Mode.Update;
                ModeEnabled = false;
            }
            updateEditTextHandler?.Invoke();
        }

        public void CompareEditMessage(string newMessage)
        {
            ModeEnabled = (String.Compare(SelectedRecord.Message, newMessage) != 0) ? true : false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
