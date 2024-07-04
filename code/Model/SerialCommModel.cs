using DLT645.BasicMVVM;
using DLT645.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLT645.Model
{
    internal class SerialCommModelRecord : ObservableObject
    {
        private string RecordMemberTimePrivate;
        private string RecordMemberTxRxPrivate;
        private string RecordMemberDataPrivate;

        public SerialCommModelRecord() { }
        public SerialCommModelRecord(string RecordMemberTimePrivate, string RecordMemberTxRxPrivate, string RecordMemberDataPrivate)
        {
            this.RecordMemberTimePrivate = RecordMemberTimePrivate;
            this.RecordMemberTxRxPrivate = RecordMemberTxRxPrivate;
            this.RecordMemberDataPrivate = RecordMemberDataPrivate;
        }

        internal string RecordMemberTime
        {
            get
            {
                return RecordMemberTimePrivate;
            }
            set
            {
                if (RecordMemberTimePrivate != value)
                {
                    RecordMemberTimePrivate = value;
                    RaisePropertyChanged(nameof(RecordMemberTime));
                }
            }
        }

        internal string RecordMemberTxRx
        {
            get
            {
                return RecordMemberTxRxPrivate;
            }
            set
            {
                if (RecordMemberTxRxPrivate != value)
                {
                    RecordMemberTxRxPrivate = value;
                    RaisePropertyChanged(nameof(RecordMemberTxRx));
                }
            }
        }

        internal string RecordMemberData
        {
            get
            {
                return RecordMemberDataPrivate;
            }
            set
            {
                if (RecordMemberDataPrivate != value)
                {
                    RecordMemberDataPrivate = value;
                    RaisePropertyChanged(nameof(RecordMemberData));
                }
            }
        }
    }

    internal class SerialCommModel : ObservableObject
    {
        public List<SerialCommModelRecord> RecordList = new List<SerialCommModelRecord>();
        private int RecordMemberSizePrivate;

        public int RecordMemberSize
        {
            get
            {
                return RecordMemberSizePrivate;
            }
            set
            {
                if (RecordMemberSizePrivate != value)
                {
                    RecordMemberSizePrivate = value;
                    RaisePropertyChanged(nameof(RecordMemberSize));
                }
            }
        }

        private dynamic RecordList4BindPrivate;
        public dynamic RecordList4Bind
        {
            get
            {
                return RecordList4BindPrivate;
            }
            set
            {
                if (RecordList4BindPrivate != value)
                {
                    RecordList4BindPrivate = value;
                    RaisePropertyChanged(nameof(RecordList4Bind));
                }
            }
        }

        public void UpdateRecordList()
        {
            RecordList4Bind = from i in RecordList
                              select new
                              {
                                  i.RecordMemberTime,
                                  i.RecordMemberTxRx,
                                  i.RecordMemberData,
                              };
        }

        public void InitializeComponent()
        {
            RecordMemberSize = 500;
        }
    }
}
