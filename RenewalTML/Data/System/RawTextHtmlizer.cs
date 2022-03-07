using RenewalTML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace RenewalTML.Shared.Exstention.ClassAddons
{
    public interface IRawTextHtmlizer
    {
        Task<string> RawTextToHtml(string rawText);
    }

    public class RawTextHtmlizer: ApplicationService, IRawTextHtmlizer
    {
        // Трансформирует какой либо текст в другой текст с html разметкой по правилу {}:

        // Текущий стек правил:
        // user
        // .. Id - Уникальный индификатор пользователя, если пользователь не найден, то delete
        // .. .. + userName
        // .. .. userLogin
        // .. .. userImageUrl - только ссылка на картинку пользователя
        // .. .. + 64x64croppedImage - 64x64 user image
        // .. .. userImageComplete - готовая картинка пользователя
        // .. .. .. default - картинка будет в стиле default
        // .. .. + userProfileUrl - ссылка на профиль пользователя ( в качестве текста - имя пользователя )
        // .. .. + userImgName - картинка и ник пользователя - всё ссылка.
        // .. .. .. value - css styles ( не обязательно )

        // money  - позже сделаю
        // .. value - количество трансформированных денег
        // .. .. .. default - деньги серого цвета
        // .. .. .. economy - зеленый цвет
        // .. .. .. declaim - красный цвет
        // .. .. complex - сжатие кол-ва денег до 3-5 символов
        // .. .. none - без сжатия

        // url
        // .. value - ссылка
        // .. .. value - текст в ссылке
        // .. .. .. value - css styles ( не обязательно ) 

        // memlarLogotype
        // .. classes - классы

        private readonly ClientManager _userServices;
        private readonly VirtualCroopedFileManager _fileServices;
 
        public RawTextHtmlizer(ClientManager userServices, VirtualCroopedFileManager fileServices)
        {
            _userServices = userServices;
            _fileServices = fileServices;
        }

        public async Task<string> RawTextToHtml(string rawText)
        {
            var regex = new Regex(@"(?<={).*?(?=})");

            foreach (Match match in regex.Matches(rawText))
            {
                // получаем текст ввида type:arg1:arg2:arg3

                var completeReplaceText = "undefined"; // готовый текст в резаультате, по умолчанию undefined

                // очищаем системный ввод
                var value = match.Value;

                var index = rawText.IndexOf(value); // error fix

                rawText = rawText.Remove(index, value.Length);


                var arguments = value.Split(':'); // разделяем строку на аргументы, где разделитель аргументов ':'

                switch(arguments[0]) // проверяем первый аргумент
                {
                    case "user":
                        var user = await _userServices.GetAsync(Convert.ToInt32(arguments[1])); // получаем пользователя по его id

                        switch(arguments[2])
                        {
                            case "userName":
                                completeReplaceText = user.ScreenName;
                                break;

                            case "64x64croppedImage":
                                var avatart = await _fileServices.GetPhysicFileCropperUrl(user.UserAvatar_cropp64x64);
                                var uniqimgId = ClientAuthServices.GenerateRandomString(10, false);

                                completeReplaceText = $@"
                                <a onclick=""event.preventDefault();"" href=""profile/{user.Id}"" class=""js-dynaimcNavigate preletive"">
                                <svg viewBox=""0 0 32 35.63"" style=""width: 32px;height: 32px;"" xmlns=""http://www.w3.org/2000/svg"">
                                        <defs>
                                            <clipPath id=""hexagon_clip{uniqimgId}"">
                                                <path id=""hexagon{uniqimgId}"" d=""M32,26.07V9.55a1.27,1.27,0,0,0-.65-1.1L16.62.16a1.3,1.3,0,0,0-1.24,0L.65,8.45A1.27,1.27,0,0,0,0,9.55V26.07a1.28,1.28,0,0,0,.65,1.11l14.73,8.28a1.25,1.25,0,0,0,1.24,0l14.73-8.28A1.28,1.28,0,0,0,32,26.07Z""></path>
                                            </clipPath>
                                        </defs>
                                            <image width=""32"" height=""35.5"" clip-path=""url(#hexagon_clip{uniqimgId})"" href=""{avatart}""></image>
                                        <use xlink:href=""#hexagon{uniqimgId}"" x=""0"" y=""0"" fill=""transparent""></use>
                                    </svg>
                                    <img class=""user-level-badge mini raw"" src=""/imgs/userlevels/userlevel_{user.Level}.svg"">
                                </a>";
                                
                                break;

                            case "userProfileUrl":
                                completeReplaceText = "<a onclick=\"event.preventDefault();\" href=\"profile/" + user.Id + "\" class=\"js-dynaimcNavigate user-nav-url\">" + user.ScreenName + "</a>";
                                break;


                            case "userImgName":
                                var cssuserStyles = "";

                                var uniqId = ClientAuthServices.GenerateRandomString(10, false);

                                if (arguments.Length == 4)
                                    cssuserStyles = " " + arguments[4];

                                var avatarUser = await _fileServices.GetPhysicFileCropperUrl(user.UserAvatar_cropp64x64);

                                completeReplaceText = $@"
                                <a onclick=""event.preventDefault();"" href=""profile/{arguments[1]}"" class=""js-dynaimcNavigate transaction-module-info preletive {cssuserStyles}"">
                                <svg viewBox=""0 0 32 35.63"" style=""width: 32px;height: 32px;"" xmlns=""http://www.w3.org/2000/svg"">
                                        <defs>
                                            <clipPath id=""hexagon_clip{uniqId}"">
                                                <path id=""hexagon{uniqId}"" d=""M32,26.07V9.55a1.27,1.27,0,0,0-.65-1.1L16.62.16a1.3,1.3,0,0,0-1.24,0L.65,8.45A1.27,1.27,0,0,0,0,9.55V26.07a1.28,1.28,0,0,0,.65,1.11l14.73,8.28a1.25,1.25,0,0,0,1.24,0l14.73-8.28A1.28,1.28,0,0,0,32,26.07Z""></path>
                                            </clipPath>
                                        </defs>
                                            <image width=""32"" height=""35.5"" clip-path=""url(#hexagon_clip{uniqId})"" href=""{avatarUser}""></image>
                                        <use xlink:href=""#hexagon{uniqId}"" x=""0"" y=""0"" fill=""transparent""></use>
                                    </svg>
                                    <img class=""user-level-badge mini raw"" src=""/imgs/userlevels/userlevel_{user.Level}.svg"">
                                    <p>{user.ScreenName}</p>
                                </a>";

                                break;

                            default: completeReplaceText = "error_user"; break;
                        }

                        break;

                    case "url":
                        var cssStyles = "";

                        if(arguments.Length == 3)
                            cssStyles = " " + arguments[3];

                        completeReplaceText = "<a onclick=\"event.preventDefault();\" href=\"" + arguments[1] + "\" class=\"js-dynaimcNavigate" + cssStyles + "\">" + arguments[2] + "</a>";
                        break;

                    case "memlarLogotype":
                        completeReplaceText = "<span class=\"memlar-icon-style " + arguments[1] + "\"><svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 312.2 308.11\"><g><g>"+
                "<path d=\"M259.72,28.59,244.63,63,215.9,128.35h0c-.85-1.91-24.67-49.8-26.24-53.36-9.64,21.91-29.14,66.26-38.83,88.28L110.24,75.88,47.86,217.71A125.61,125.61,0,0,1,159.41,34.41h.18L174.5.66Q167.33,0,160,0A160,160,0,0,0,18.74,235.19,51.29,51.29,0,0,1,20.47,280l38.26-4.77c4.09-9.3,7.1-13.68,12.46-25.86l40.12-91.22L150.82,248c9.64-21.91,29.14-66.27,38.83-88.29.44-.31,24.28,51.62,26.62,52.51L266.74,97.35A126.22,126.22,0,0,1,158,273.35l-15.25,34.52c2.88.15,5.79.24,8.72.24A160.79,160.79,0,0,0,295,75a46,46,0,0,1-1.21-38.34l7-16.93Z\" />"+
            "</g></g></svg></span>";
                        break;

                    case "money":

                        if (String.IsNullOrEmpty(arguments[2]))
                            completeReplaceText = "error_money_style";
                        else
                        {
                            var style = arguments[3];

                            switch (arguments[2])
                            {
                                case "complex":
                                    completeReplaceText = "<span class=\"raw_money " + style + "\">" + CurrencyAddon.Format(Convert.ToInt32(arguments[1])) + "</span>";
                                    break;

                                case "none":
                                    completeReplaceText = "<span class=\"raw_money " + style + "\">" + arguments[1] + "</span>";
                                    break;

                                default: completeReplaceText = "error_money_type"; break;
                            }
                        }

                        break;
                    default: completeReplaceText = "error"; break;
                }

                rawText = rawText.Insert(index, completeReplaceText);
            }

            // удаляем первы кавычки
            rawText = rawText.Replace("{", string.Empty);
            rawText = rawText.Replace("}", string.Empty);

            return rawText;
        }
    }
}
