using Volo.Abp.Domain.Entities;

namespace RenewalTML.Data.Model
{
    public class Notification : Entity<int>
    {
        // Системные символы, которые нельзя будет использовать в никах, ораганизация, товарах и т.д: {}:

        public string Information { get; set; } // Небольшой текст сверху
        public string Text { get; set; } // Основной текст описания, учитывая правила {}:

        // На этапе препоказа, данный текст трансформируется в html код с правилом {}: а потом выдается на страницу. Т.е есть поддержка html

        // Пример правила:
        // {user:18:name} - будет выбран пользователь с id 18, и будет выдано его имя.

        public string Date { get; set; } // date time to second
        public string LogoType { get; set; }

        // logotype_ok - green with ok sign
        // logotype_error - red with x sign
        // logotype_info - blue with info sign
        // logotype_money - green with money sign
        // logotype_offer - offer bg and offer sign
        // logotype_award 
        // {user:22:64x64croppedImage} - return 22'user cropper image 64x64. Так же потом и для организаций и т.д

        public bool isViewed { get; set; } // была ли нотификация просмотрена.
        public int ClientOwnerId { get; set; } // чья нотификация
    }
}
