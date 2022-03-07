using RenewalTML.Data.Model;
using System;
using System.Collections.Generic;
using RenewalTML.Shared.Exstention.ClassAddons;
using System.Threading.Tasks;
using System.Linq;

namespace RenewalTML.Shared.Exstention.ClassAddons
{
    public class TransactionView
    {
        private Transaction transaction;

        public TransactionView(Transaction transaction, bool _isEarn)
        {
            this.transaction = transaction;

            Name = transaction.Name;
            ReadyDate = DateTimeAddon.StringToDateTimeFormat(transaction.Date);
            RealDateTime = DateTimeAddon.StringToDateTime(transaction.Date);

            ValueFormat = CurrencyAddon.Format(transaction.Value);
            Value = transaction.Value;

            isEarn = _isEarn;
            Id = transaction.Id;
            LogInformation = "[TransactionType]: " + transaction.TransactionType + " [OutEntityId:" + transaction.OutEntityId + "] -> [ToEntityId:" + transaction.ToEntityId + "]";
        }

        public async Task GenerateHtmlRawText(IRawTextHtmlizer rawTextHtmlizer)
        {
            HTMLInfo = await rawTextHtmlizer.RawTextToHtml(transaction.Information);
        }

        public string LogInformation { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string HTMLInfo { get; set; }
        public string ReadyDate { get; private set; } // Готовая дата ( по типу день назад, 3 месяца назад и т.д )
        public DateTime RealDateTime { get; private set; } // Изначальная дата для сортировки
        public string ValueFormat { get; set; }
        public int Value { get; set; }
        public bool isEarn { get; set; } // в плюс или в минус?
    }

    public class TransactionModule
    {
        public TransactionModule()
        {
            TransactionList = new List<TransactionView>();
        }

        public async Task TransactionModuleComplete(List<Transaction> listEarn, List<Transaction> listGave, IRawTextHtmlizer rawTextHtmlizer, int AllTransactionCount)
        {
            this.AllTransactionCount = AllTransactionCount;

            foreach (var s in listEarn)
            {
                var trs = new TransactionView(s, false);
                await trs.GenerateHtmlRawText(rawTextHtmlizer);
                TransactionList.Add(trs);
            }

            foreach (var s in listGave)
            {
                var trs = new TransactionView(s, true);
                await trs.GenerateHtmlRawText(rawTextHtmlizer);
                TransactionList.Add(trs);
            }

            TransactionList = TransactionList.OrderByDescending(m => m.RealDateTime.Ticks).ToList();
        }

        public List<TransactionView> TransactionList { get; set; }
        public int AllTransactionCount { get; set; }
    }
}
