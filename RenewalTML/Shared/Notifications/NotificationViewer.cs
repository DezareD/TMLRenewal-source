using RenewalTML.Data;
using RenewalTML.Data.Model;
using RenewalTML.Shared.Exstention.ClassAddons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenewalTML.Shared.Exstention.ClassAddons
{
    public class NotificationViewer
    {
        private Notification notification { get; set; }
        public NotificationViewer(Notification notification)
        {
            this.notification = notification;

            ScreenName = notification.Information;
            ReadyDate = DateTimeAddon.StringToDateTimeFormat(notification.Date);
            RealDateTime = DateTimeAddon.StringToDateTime(notification.Date);
        }

        public async Task GenerateHtmlRawText(IRawTextHtmlizer rawTextHtmlizer)
        {
            HtmlText = await rawTextHtmlizer.RawTextToHtml(notification.Text);

            await GenerateImage(rawTextHtmlizer);
        }

        public async Task GenerateImage(IRawTextHtmlizer rawTextHtmlizer)
        {
            switch(notification.LogoType)
            {
                case "logotype_ok":
                    HtmlImage = "<div class=\"ntf-lg ok\"><i class=\"fas fa-check\"></i></div>";
                    break;

                case "logotype_error":
                    HtmlImage = "<div class=\"ntf-lg error\"><i class=\"fas fa-times\"></i></div>";
                    break;

                case "logotype_info":
                    HtmlImage = "<div class=\"ntf-lg info\"><i class=\"fas fa-info\"></i></div>";
                    break;

                case "logotype_money":
                    HtmlImage = "<div class=\"ntf-lg ok\"><i class=\"fas fa-coins\"></i></div>";
                    break;

                case "logotype_offer":
                    HtmlImage = "<div class=\"ntf-lg info\"><i class=\"fas fa-handshake-alt\"></i></div>";
                    break;

                case "logotype_award":
                    HtmlImage = "<div class=\"ntf-lg info\"><i class=\"fas fa-award\"></i></div>";
                    break;

                default:
                    var htmlImage = await rawTextHtmlizer.RawTextToHtml(notification.LogoType);
                    HtmlImage = "<div class=\"ntf-lg\">" + htmlImage + "</div>";
                    break;
            }
        }

        public string ScreenName { get; private set; }
        public string HtmlText { get; private set; } // Готовый html текст по правилу {}:
        public string HtmlImage { get; private set; } // Готовая html картинка в зависимости от типа нотификации
        public string ReadyDate { get; private set; } // Готовая дата ( по типу день назад, 3 месяца назад и т.д )
        public DateTime RealDateTime { get; private set; } // Изначальная дата для сортировки
    }
}
